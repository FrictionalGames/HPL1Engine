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
#include "resources/FileSearcher.h"
#include "system/String.h"
#include "resources/LowLevelResources.h"
#include "system/LowLevelSystem.h"


namespace hpl {

	//////////////////////////////////////////////////////////////////////////
	// CONSTRUCTORS
	//////////////////////////////////////////////////////////////////////////

	//-----------------------------------------------------------------------

	cFileSearcher::cFileSearcher(iLowLevelResources *apLowLevelResources, tWString assetsPath)
	{
		mpLowLevelResources = apLowLevelResources;
		mpLowLevelResources->FindFilesInDirRecursive(m_mapFiles, assetsPath);
	}

	//-----------------------------------------------------------------------

	cFileSearcher::~cFileSearcher()
	{
	}

	//-----------------------------------------------------------------------

	//////////////////////////////////////////////////////////////////////////
	// PUBLIC METHODS
	//////////////////////////////////////////////////////////////////////////

	//-----------------------------------------------------------------------

	void cFileSearcher::AddDirectory(tString asPath, tString asMask)
	{
		return;
	}

	void cFileSearcher::ClearDirectories()
	{
		return;
	}

	//-----------------------------------------------------------------------

	tString cFileSearcher::GetFilePath(tString asName)
	{
		tFilePathMapIt it = m_mapFiles.find(cString::ToLowerCase(asName));
		if(it == m_mapFiles.end())return "";

		return it->second;
	}

	//-----------------------------------------------------------------------

}
