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
#include "resources/MeshLoaderHandler.h"

#include "resources/MeshLoader.h"
#include "system/String.h"
#include "system/LowLevelSystem.h"
#include "resources/Resources.h"
#include "scene/Scene.h"

namespace hpl {

	bool iMeshLoader::mbRestricStaticLightToSector = false;
	bool iMeshLoader::mbUseFastMaterial = false;
	tString iMeshLoader::msFastMaterialFile = "";
	tString iMeshLoader::msCacheDir = "core/cache/";

	//////////////////////////////////////////////////////////////////////////
	// CONSTRUCTORS
	//////////////////////////////////////////////////////////////////////////

	//-----------------------------------------------------------------------

	cMeshLoaderHandler::cMeshLoaderHandler(cResources* apResources, cScene *apScene)
	{
		mpResources = apResources;
		mpScene = apScene;
	}

	//-----------------------------------------------------------------------

	cMeshLoaderHandler::~cMeshLoaderHandler()
	{
		tMeshLoaderListIt it = mlstLoaders.begin();
		for(;it != mlstLoaders.end();it++)
		{
			hplDelete(*it);
		}

		mlstLoaders.clear();
	}

	//-----------------------------------------------------------------------

	//////////////////////////////////////////////////////////////////////////
	// PUBLIC METHODS
	//////////////////////////////////////////////////////////////////////////

	//-----------------------------------------------------------------------

	cMesh* cMeshLoaderHandler::LoadMesh(const tString& asFile,tMeshLoadFlag aFlags)
	{
		tString sType = cString::ToLowerCase(cString::GetFileExt(asFile));

		tMeshLoaderListIt it = mlstLoaders.begin();
		for(;it != mlstLoaders.end();it++)
		{
			iMeshLoader *pLoader = *it;

			if(pLoader->IsSupported(sType))
			{
				return pLoader->LoadMesh(asFile,aFlags);
			}
		}

		Log("No loader for '%s' found!\n", sType.c_str());
		return NULL;
	}

	//-----------------------------------------------------------------------

	bool cMeshLoaderHandler::SaveMesh(cMesh* apMesh,const tString& asFile)
	{
		tString sType = cString::ToLowerCase(cString::GetFileExt(asFile));

		tMeshLoaderListIt it = mlstLoaders.begin();
		for(;it != mlstLoaders.end();it++)
		{
			iMeshLoader *pLoader = *it;

			if(pLoader->IsSupported(sType))
			{
				return pLoader->SaveMesh(apMesh,asFile);
			}
		}

		Log("No loader for '%s' found!\n", sType.c_str());
		return false;
	}

	//-----------------------------------------------------------------------

	cWorld3D* cMeshLoaderHandler::LoadWorld(const tString& asFile, tWorldLoadFlag aFlags)
	{
		tString sType = cString::ToLowerCase(cString::GetFileExt(asFile));

		tMeshLoaderListIt it = mlstLoaders.begin();
		for(;it != mlstLoaders.end();it++)
		{
			iMeshLoader *pLoader = *it;

			if(pLoader->IsSupported(sType))
			{
				return pLoader->LoadWorld(asFile,mpScene, aFlags);
			}
		}

		FatalError("No loader for '%s' found!\n", sType.c_str());
		return NULL;
	}

	//-----------------------------------------------------------------------

	cAnimation* cMeshLoaderHandler::LoadAnimation(const tString& asFile)
	{
		tString sType = cString::ToLowerCase(cString::GetFileExt(asFile));

		tMeshLoaderListIt it = mlstLoaders.begin();
		for(;it != mlstLoaders.end();it++)
		{
			iMeshLoader *pLoader = *it;

			if(pLoader->IsSupported(sType))
			{
				return pLoader->LoadAnimation(asFile);
			}
		}

		Log("No loader for '%s' found!\n", sType.c_str());
		return NULL;
	}


	//-----------------------------------------------------------------------

	void cMeshLoaderHandler::AddLoader(iMeshLoader *apLoader)
	{
		mlstLoaders.push_back(apLoader);

		apLoader->mpMaterialManager = mpResources->GetMaterialManager();
		apLoader->mpMeshManager = mpResources->GetMeshManager();
		apLoader->mpAnimationManager = mpResources->GetAnimationManager();
		apLoader->mpSystem = mpScene->GetSystem();

		apLoader->AddSupportedTypes(&mvSupportedTypes);
	}

	//-----------------------------------------------------------------------
}
