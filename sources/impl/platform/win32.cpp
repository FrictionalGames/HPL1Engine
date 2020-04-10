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
#include "impl/Platform.h"
#include <io.h>
#include <set>

#include "system/LowLevelSystem.h"

#define _UNICODE
#include <stdio.h>

namespace hpl {

	long Platform::FileLength(const tWString& asFileName)
	{
		FILE *pFile = _wfopen(asFileName.c_str(), _W("rb"));
		long ret = Platform::FileLength(pFile);
		fclose(pFile);
		return ret;
	}

	long Platform::FileLength(FILE *pFile)
	{
		return (long)_filelength(_fileno(pFile));
	}

	void Platform::FindFileInDir(tWStringList &alstStrings,tWString asDir, tWString asMask)
	{
		//ifdef WIN32 , these windows functions only works with "\".. sucks ...
		asDir = cString::ReplaceCharToW(asDir,_W("/"),_W("\\"));

		//Get the search string
		wchar_t sSpec[256];
		wchar_t end = asDir[asDir.size()-1];

		if(end == _W('\\') || end== _W('/'))
			swprintf(sSpec,256,_W("%s%s"),asDir.c_str(),asMask.c_str());
		else
		{
			//ifdef WIN32 , these windows functions only works with "\".. sucks ...
			swprintf(sSpec,256,_W("%s\\%s"),asDir.c_str(),asMask.c_str());
		}

		//The needed structs
		intptr_t lHandle;
		struct _wfinddata_t FileInfo;

		//Find the first file:
		lHandle = _wfindfirst(sSpec, &FileInfo );
		if(lHandle==-1L)return;

		//Check so it is not a subdir
		if((FileInfo.attrib & _A_SUBDIR)==0)
		{
			alstStrings.push_back(FileInfo.name);
		}

		//Get the other files.
		while( _wfindnext( lHandle, &FileInfo ) == 0 )
		{
			if((FileInfo.attrib & _A_SUBDIR)==0)
			{
				alstStrings.push_back(FileInfo.name);
			}
		}
	}

	void Platform::FindFilesInDirRecursive(tFilePathMap& paths, tWString asDir)
	{
		asDir = cString::ReplaceCharToW(asDir, _W("/"), _W("\\"));
		
		//Get the search string
		wchar_t sSpec[256];
		wchar_t end = asDir[asDir.size() - 1];

		swprintf_s(sSpec, 256, _W("%s\\*"), asDir.c_str());

		//The needed structs
		intptr_t lHandle;
		struct _wfinddata_t FileInfo;

		tWStringSet excludedDirs;
		excludedDirs.insert(_W("."));
		excludedDirs.insert(_W(".."));

		//Find the first file:
		tWStringList left_to_search;
		lHandle = _wfindfirst(sSpec, &FileInfo);
		if (lHandle == -1L)return;

		//Get the other files.
		do
		{
			tWString fileName = FileInfo.name;
			if ((FileInfo.attrib & _A_SUBDIR) == 0)
			{
				tString sFile = cString::To8Char(FileInfo.name);
				paths.insert(tFilePathMap::value_type(cString::ToLowerCase(sFile), cString::SetFilePath(sFile, cString::To8Char(asDir))));
			}
			else if (excludedDirs.count(fileName) == 0)
			{
				left_to_search.push_back(FileInfo.name);
			}
		} while (_wfindnext(lHandle, &FileInfo) == 0);

		for (tWStringListIt it = left_to_search.begin(); it != left_to_search.end(); ++it)
		{
			wchar_t nextDir[256];
			swprintf_s(nextDir, 256, _W("%s\\%s"), asDir.c_str(), it->c_str());
			Platform::FindFilesInDirRecursive(paths, nextDir);
		}
	}
}