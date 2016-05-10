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
#ifndef HPL_SQ_SCRIPT_H
#define HPL_SQ_SCRIPT_H

#include "system/Script.h"
#include "impl/LowLevelSystemSDL.h"
#include <angelscript.h>


namespace hpl {

	class cSqScript : public iScript
	{
	public:
		cSqScript(const tString& asName, asIScriptEngine *apScriptEngine,
					cScriptOutput *apScriptOutput, int alHandle); 
		~cSqScript();

		bool CreateFromFile(const tString& asFileName);

		int GetFuncHandle(const tString& asFunc);
		void AddArg(const tString& asArg);

		bool Run(const tString& asFuncLine);
		bool Run(int alHandle);

	private:
		asIScriptEngine *mpScriptEngine;
		cScriptOutput *mpScriptOutput;

		asIScriptContext *mpContext;
		
		int mlHandle;
		tString msModuleName;

	};
};
#endif // HPL_SCRIPT_H
