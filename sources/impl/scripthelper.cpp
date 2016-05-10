#include <string.h>
#include "impl/scripthelper.h"
#include <string>
#include <assert.h>
#include <stdio.h>

using namespace std;

BEGIN_AS_NAMESPACE

int CompareRelation(asIScriptEngine *engine, void *lobj, void *robj, int typeId, int &result)
{
    // TODO: If a lot of script objects are going to be compared, e.g. when sorting an array, 
    //       then the method id and context should be cached between calls.
    
	int retval = -1;
	int funcId = 0;

	asIObjectType *ot = engine->GetObjectTypeById(typeId);
	if( ot )
	{
		// Check if the object type has a compatible opCmp method
		for( asUINT n = 0; n < ot->GetMethodCount(); n++ )
		{
			asIScriptFunction *func = ot->GetMethodByIndex(n);
			if( strcmp(func->GetName(), "opCmp") == 0 &&
				func->GetReturnTypeId() == asTYPEID_INT32 &&
				func->GetParamCount() == 1 )
			{
				asDWORD flags;
				int paramTypeId = func->GetParamTypeId(0, &flags);
				
				// The parameter must be an input reference of the same type
				if( flags != asTM_INREF || typeId != paramTypeId )
					break;

				// Found the method
				funcId = ot->GetMethodIdByIndex(n);
				break;
			}
		}
	}

	if( funcId )
	{
		// Call the method
		asIScriptContext *ctx = engine->CreateContext();
		ctx->Prepare(funcId);
		ctx->SetObject(lobj);
		ctx->SetArgAddress(0, robj);
		int r = ctx->Execute();
		if( r == asEXECUTION_FINISHED )
		{
			result = (int)ctx->GetReturnDWord();

			// The comparison was successful
			retval = 0;
		}
		ctx->Release();
	}

	return retval;
}

int CompareEquality(asIScriptEngine *engine, void *lobj, void *robj, int typeId, bool &result)
{
    // TODO: If a lot of script objects are going to be compared, e.g. when searching for an
	//       entry in a set, then the method id and context should be cached between calls.
    
	int retval = -1;
	int funcId = 0;

	asIObjectType *ot = engine->GetObjectTypeById(typeId);
	if( ot )
	{
		// Check if the object type has a compatible opEquals method
		for( asUINT n = 0; n < ot->GetMethodCount(); n++ )
		{
			asIScriptFunction *func = ot->GetMethodByIndex(n);
			if( strcmp(func->GetName(), "opEquals") == 0 &&
				func->GetReturnTypeId() == asTYPEID_BOOL &&
				func->GetParamCount() == 1 )
			{
				asDWORD flags;
				int paramTypeId = func->GetParamTypeId(0, &flags);
				
				// The parameter must be an input reference of the same type
				if( flags != asTM_INREF || typeId != paramTypeId )
					break;

				// Found the method
				funcId = ot->GetMethodIdByIndex(n);
				break;
			}
		}
	}

	if( funcId )
	{
		// Call the method
		asIScriptContext *ctx = engine->CreateContext();
		ctx->Prepare(funcId);
		ctx->SetObject(lobj);
		ctx->SetArgAddress(0, robj);
		int r = ctx->Execute();
		if( r == asEXECUTION_FINISHED )
		{
			result = ctx->GetReturnByte() ? true : false;

			// The comparison was successful
			retval = 0;
		}
		ctx->Release();
	}
	else
	{
		// If the opEquals method doesn't exist, then we try with opCmp instead
		int relation;
		retval = CompareRelation(engine, lobj, robj, typeId, relation);
		if( retval >= 0 )
			result = relation == 0 ? true : false;
	}

	return retval;
}

int ExecuteString(asIScriptEngine *engine, const char *code, asIScriptModule *mod, asIScriptContext *ctx)
{
	// Wrap the code in a function so that it can be compiled and executed
	string funcCode = "void ExecuteString() {\n";
	funcCode += code;
	funcCode += "\n;}";
	
	// If no module was provided, get a dummy from the engine
	asIScriptModule *execMod = mod ? mod : engine->GetModule("ExecuteString", asGM_ALWAYS_CREATE);
	
	// Compile the function that can be executed
	asIScriptFunction *func = 0;
	int r = execMod->CompileFunction("ExecuteString", funcCode.c_str(), -1, 0, &func);
	if( r < 0 )
		return r;

	// If no context was provided, request a new one from the engine
	asIScriptContext *execCtx = ctx ? ctx : engine->CreateContext();
	r = execCtx->Prepare(func->GetId());
	if( r < 0 )
	{
		func->Release();
		if( !ctx ) execCtx->Release();
		return r;
	}

	// Execute the function
	r = execCtx->Execute();
	
	// Clean up
	func->Release();
	if( !ctx ) execCtx->Release();

	return r;
}

int WriteConfigToFile(asIScriptEngine *engine, const char *filename)
{
	int c, n;

	FILE *f = 0;
#if _MSC_VER >= 1400 // MSVC 8.0 / 2005
	fopen_s(&f, filename, "wt");
#else
	f = fopen(filename, "wt");
#endif
	if( f == 0 )
		return -1;

	// Make sure the default array type is expanded to the template form 
	bool expandDefArrayToTempl = engine->GetEngineProperty(asEP_EXPAND_DEF_ARRAY_TO_TMPL) ? true : false;
	engine->SetEngineProperty(asEP_EXPAND_DEF_ARRAY_TO_TMPL, true);

	// Write enum types and their values
	fprintf(f, "// Enums\n");
	c = engine->GetEnumCount();
	for( n = 0; n < c; n++ )
	{
		int typeId;
		const char *enumName = engine->GetEnumByIndex(n, &typeId);
		fprintf(f, "enum %s\n", enumName);
		for( int m = 0; m < engine->GetEnumValueCount(typeId); m++ )
		{
			const char *valName;
			int val;
			valName = engine->GetEnumValueByIndex(typeId, m, &val);
			fprintf(f, "enumval %s %s %d\n", enumName, valName, val);
		}
	}

	// Enumerate all types
	fprintf(f, "\n// Types\n");

	c = engine->GetObjectTypeCount();
	for( n = 0; n < c; n++ )
	{
		asIObjectType *type = engine->GetObjectTypeByIndex(n);
		if( type->GetFlags() & asOBJ_SCRIPT_OBJECT )
		{
			// This should only be interfaces
			assert( type->GetSize() == 0 );

			fprintf(f, "intf %s\n", type->GetName());
		}
		else
		{
			// Only the type flags are necessary. The application flags are application 
			// specific and doesn't matter to the offline compiler. The object size is also
			// unnecessary for the offline compiler
			fprintf(f, "objtype \"%s\" %u\n", engine->GetTypeDeclaration(type->GetTypeId()), (unsigned int)(type->GetFlags() & 0xFF));
		}
	}

	c = engine->GetTypedefCount();
	for( n = 0; n < c; n++ )
	{
		int typeId;
		const char *typeDef = engine->GetTypedefByIndex(n, &typeId);
		fprintf(f, "typedef %s \"%s\"\n", typeDef, engine->GetTypeDeclaration(typeId));
	}

	c = engine->GetFuncdefCount();
	for( n = 0; n < c; n++ )
	{
		asIScriptFunction *funcDef = engine->GetFuncdefByIndex(n);
		fprintf(f, "funcdef \"%s\"\n", funcDef->GetDeclaration());
	}

	// Write the object types members
	fprintf(f, "\n// Type members\n");
	
	c = engine->GetObjectTypeCount();
	for( n = 0; n < c; n++ )
	{
		asIObjectType *type = engine->GetObjectTypeByIndex(n);
		string typeDecl = engine->GetTypeDeclaration(type->GetTypeId());
		if( type->GetFlags() & asOBJ_SCRIPT_OBJECT )
		{
			for( asUINT m = 0; m < type->GetMethodCount(); m++ )
			{
				asIScriptFunction *func = type->GetMethodByIndex(m);
				fprintf(f, "intfmthd %s \"%s\"\n", typeDecl.c_str(), func->GetDeclaration(false));
			}
		}
		else
		{
			asUINT m;
			for( m = 0; m < type->GetFactoryCount(); m++ )
			{
				asIScriptFunction *func = engine->GetFunctionById(type->GetFactoryIdByIndex(m));
				fprintf(f, "objbeh \"%s\" %d \"%s\"\n", typeDecl.c_str(), asBEHAVE_FACTORY, func->GetDeclaration(false));
			}
			for( m = 0; m < type->GetBehaviourCount(); m++ )
			{
				asEBehaviours beh;
				asIScriptFunction *func = engine->GetFunctionById(type->GetBehaviourByIndex(m, &beh));
				fprintf(f, "objbeh \"%s\" %d \"%s\"\n", typeDecl.c_str(), beh, func->GetDeclaration(false));
			}
			for( m = 0; m < type->GetMethodCount(); m++ )
			{
				asIScriptFunction *func = type->GetMethodByIndex(m);
				fprintf(f, "objmthd \"%s\" \"%s\"\n", typeDecl.c_str(), func->GetDeclaration(false));
			}
			for( m = 0; m < type->GetPropertyCount(); m++ )
			{
				fprintf(f, "objprop \"%s\" \"%s\"\n", typeDecl.c_str(), type->GetPropertyDeclaration(m));
			}
		}
	}

	// Write functions
	fprintf(f, "\n// Functions\n");

	c = engine->GetGlobalFunctionCount();
	for( n = 0; n < c; n++ )
	{
		asIScriptFunction *func = engine->GetFunctionById(engine->GetGlobalFunctionIdByIndex(n));
		fprintf(f, "func \"%s\"\n", func->GetDeclaration());
	}

	// Write global properties
	fprintf(f, "\n// Properties\n");

	c = engine->GetGlobalPropertyCount();
	for( n = 0; n < c; n++ )
	{
		const char *name;
		int typeId;
		bool isConst;
		engine->GetGlobalPropertyByIndex(n, &name, &typeId, &isConst); 
		fprintf(f, "prop \"%s%s %s\"\n", isConst ? "const " : "", engine->GetTypeDeclaration(typeId), name);
	}

	// Write string factory
	fprintf(f, "\n// String factory\n");
	int typeId = engine->GetStringFactoryReturnTypeId();
	if( typeId > 0 )
		fprintf(f, "strfactory \"%s\"\n", engine->GetTypeDeclaration(typeId));

	// Write default array type
	fprintf(f, "\n// Default array type\n");
	typeId = engine->GetDefaultArrayTypeId();
	if( typeId > 0 )
		fprintf(f, "defarray \"%s\"\n", engine->GetTypeDeclaration(typeId));

	fclose(f);

	// Restore original settings
	engine->SetEngineProperty(asEP_EXPAND_DEF_ARRAY_TO_TMPL, expandDefArrayToTempl);

	return 0;
}

void PrintException(asIScriptContext *ctx, bool printStack)
{
	if( ctx->GetState() != asEXECUTION_EXCEPTION ) return;

	asIScriptEngine *engine = ctx->GetEngine();
	int funcId = ctx->GetExceptionFunction();
	const asIScriptFunction *function = engine->GetFunctionById(funcId);
	printf("func: %s\n", function->GetDeclaration());
	printf("modl: %s\n", function->GetModuleName());
	printf("sect: %s\n", function->GetScriptSectionName());
	printf("line: %d\n", ctx->GetExceptionLineNumber());
	printf("desc: %s\n", ctx->GetExceptionString());

	if( printStack )
	{
		printf("--- call stack ---\n");
		for( asUINT n = 1; n < ctx->GetCallstackSize(); n++ )
		{
			function = ctx->GetFunction(n);
			printf("%s (%d): %s\n", function->GetScriptSectionName(),
			                        ctx->GetLineNumber(n),
								    function->GetDeclaration());
		}
	}
}

END_AS_NAMESPACE
