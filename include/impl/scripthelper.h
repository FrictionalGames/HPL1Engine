#ifndef SCRIPTHELPER_H
#define SCRIPTHELPER_H

#include <angelscript.h>

BEGIN_AS_NAMESPACE

// Compare relation between two objects of the same type
int CompareRelation(asIScriptEngine *engine, void *lobj, void *robj, int typeId, int &result);

// Compare equality between two objects of the same type
int CompareEquality(asIScriptEngine *engine, void *lobj, void *robj, int typeId, bool &result);

// Compile and execute simple statements
// The module is optional. If given the statements can access the entities compiled in the module.
// The caller can optionally provide its own context, for example if a context should be reused.
int ExecuteString(asIScriptEngine *engine, const char *code, asIScriptModule *mod = 0, asIScriptContext *ctx = 0);

// Write the registered application interface to a file for an offline compiler.
// The format is compatible with the offline compiler in /sdk/samples/asbuild/.
int WriteConfigToFile(asIScriptEngine *engine, const char *filename);

// Print details of the script exception to the standard output
void PrintException(asIScriptContext *ctx, bool printStack = false);

END_AS_NAMESPACE

#endif
