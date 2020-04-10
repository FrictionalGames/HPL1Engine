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
#ifndef PLATFORM_H_
#define PLATFORM_H_
#include "system/String.h"
#include <stdio.h>
#if defined(WIN32)
#include <io.h>
#endif

namespace hpl {
	class Platform {
	public:
		static long FileLength(const tWString& asFileName);
		static long FileLength(FILE *pFile);
		static void FindFileInDir(tWStringList &alstStrings,tWString asDir, tWString asMask);
        static void FindFilesInDirRecursive(tFilePathMap& alstStrings, tWString asDir);
	};
}

#endif /*PLATFORM_H_*/
