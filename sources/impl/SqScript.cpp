/*
 * Copyright (C) 2006-2010 - Frictional Games
 *
 * This file is part of HPL1 Engine.
 *
 * HPL1 Engine is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * HPL1 Engine is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with HPL1 Engine.  If not, see <http://www.gnu.org/licenses/>.
 */
#include "impl/SqScript.h"
#include "system/LowLevelSystem.h"
#include "system/String.h"
#include "math/Math.h"
#include "impl/Platform.h"
#include <stdio.h>

namespace hpl {

	//////////////////////////////////////////////////////////////////////////
	// CONSTRUCTORS
	//////////////////////////////////////////////////////////////////////////

	//-----------------------------------------------------------------------

	cSqScript::cSqScript(const tString& asName,asIScriptEngine *apScriptEngine,
							cScriptOutput *apScriptOutput, int alHandle)
		: iScript(asName)
	{
		mpScriptEngine = apScriptEngine;
		mpScriptOutput = apScriptOutput;
		mlHandle = alHandle;

		mpContext = mpScriptEngine->CreateContext();

		//Create a unique module name
		msModuleName = "Module_"+cString::ToString(cMath::RandRectl(0,1000000))+
						"_"+cString::ToString(mlHandle);

	}

	cSqScript::~cSqScript()
	{
		mpScriptEngine->Discard(msModuleName.c_str());
		mpContext->Release();
	}

	//-----------------------------------------------------------------------

	//////////////////////////////////////////////////////////////////////////
	// PUBLIC METHODS
	//////////////////////////////////////////////////////////////////////////

	//-----------------------------------------------------------------------

	bool cSqScript::CreateFromFile(const tString& asFileName)
	{
		int lLength;
		char *pCharBuffer = LoadCharBuffer(asFileName,lLength);
		if(pCharBuffer==NULL){
			Error("Couldn't load script '%s'!\n",asFileName.c_str());
			return false;
		}

		if(mpScriptEngine->AddScriptSection(msModuleName.c_str(), "main", pCharBuffer, lLength)<0)
		{
			Error("Couldn't add script '%s'!\n",asFileName.c_str());
			hplDeleteArray(pCharBuffer);
			return false;
		}

		if(mpScriptEngine->Build(msModuleName.c_str())<0)
		{
			Error("Couldn't build script '%s'!\n",asFileName.c_str());
			Log("------- SCRIPT OUTPUT BEGIN --------------------------\n");
			mpScriptOutput->Display();
			mpScriptOutput->Clear();
			Log("------- SCRIPT OUTPUT END ----------------------------\n");



			hplDeleteArray(pCharBuffer);
			return false;
		}
		mpScriptOutput->Clear();

		hplDeleteArray(pCharBuffer);
		return true;
	}

	//-----------------------------------------------------------------------

	int cSqScript::GetFuncHandle(const tString& asFunc)
	{
		return mpScriptEngine->GetFunctionIDByName(msModuleName.c_str(),asFunc.c_str());
	}

	//-----------------------------------------------------------------------

	void cSqScript::AddArg(const tString& asArg)
	{

	}

	//-----------------------------------------------------------------------

	bool cSqScript::Run(const tString& asFuncLine)
	{
		mpScriptEngine->ExecuteString(msModuleName.c_str(), asFuncLine.c_str());

		return true;
	}

	//-----------------------------------------------------------------------

	bool cSqScript::Run(int alHandle)
	{
		mpContext->Prepare(alHandle);

		/* Set all the args here */

		mpContext->Execute();

		return true;
	}

	//-----------------------------------------------------------------------

	//////////////////////////////////////////////////////////////////////////
	// PRIVATE METHODS
	//////////////////////////////////////////////////////////////////////////

	//-----------------------------------------------------------------------

	char* cSqScript::LoadCharBuffer(const tString& asFileName, int& alLength)
	{
		FILE *pFile = fopen(asFileName.c_str(), "rb");
		if(pFile==NULL){
			return NULL;
		}

		int lLength = (int)Platform::FileLength(pFile);
		alLength = lLength;

		char *pBuffer = hplNewArray(char,lLength);
		fread(pBuffer, lLength, 1, pFile);

		fclose(pFile);

		return pBuffer;
	}

	//-----------------------------------------------------------------------

	//////////////////////////////////////////////////////////////////////////
	// STATIC PRIVATE METHODS
	//////////////////////////////////////////////////////////////////////////

	//-----------------------------------------------------------------------

	//-----------------------------------------------------------------------

}
