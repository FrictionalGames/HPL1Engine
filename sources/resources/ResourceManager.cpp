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
#include "resources/ResourceManager.h"
#include "system/String.h"

#include "resources/LowLevelResources.h"
#include "resources/FileSearcher.h"
#include "resources/ResourceBase.h"

#include "system/LowLevelSystem.h"

#include <algorithm>

namespace hpl {

	int iResourceManager::mlTabCount=0;

	//////////////////////////////////////////////////////////////////////////
	// CONSTRUCTORS
	//////////////////////////////////////////////////////////////////////////

	//-----------------------------------------------------------------------

	iResourceManager::iResourceManager(cFileSearcher *apFileSearcher,
										iLowLevelResources *apLowLevelResources,
										iLowLevelSystem *apLowLevelSystem)
	{
		mpFileSearcher = apFileSearcher;
		mpLowLevelResources = apLowLevelResources;
		mpLowLevelSystem = apLowLevelSystem;
		mlHandleCount=0;
	}

	//-----------------------------------------------------------------------

	//////////////////////////////////////////////////////////////////////////
	// PUBLIC METHODS
	//////////////////////////////////////////////////////////////////////////

	//-----------------------------------------------------------------------

	iResourceBase* iResourceManager::GetByName(const tString& asName)
	{
		tString sName = cString::ToLowerCase(asName);
		//Log("Looking for '%s' \n", sName.c_str());

		tResourceNameMapIt it = m_mapNameResources.find(sName);
		if(it == m_mapNameResources.end())return NULL;

		return it->second;
	}

	//-----------------------------------------------------------------------

	iResourceBase* iResourceManager::GetByHandle(unsigned long alHandle)
	{
		tResourceHandleMapIt it = m_mapHandleResources.find(alHandle);
		if(it == m_mapHandleResources.end())return NULL;

		return it->second;
	}

	//-----------------------------------------------------------------------

	cResourceBaseIterator iResourceManager::GetResourceBaseIterator()
	{
		return cResourceBaseIterator(&m_mapNameResources);
	}

	//-----------------------------------------------------------------------

	/*void iResourceManager::Destroy(iResourceBase* apResource)
	{
		apResource->DecUserCount();
		if(apResource->HasUsers()==false){
			m_mapHandleResources.erase(apResource->GetHandle());
			m_mapNameResources.erase(apResource->GetName());
			hplDelete(apResource);
		}
	}*/

	//-----------------------------------------------------------------------

	class cSortResources
	{
	public:
		bool operator()(iResourceBase* apResourceA, iResourceBase* apResourceB)
		{
			if(apResourceA->GetUserCount() != apResourceB->GetUserCount())
			{
				return apResourceA->GetUserCount() > apResourceB->GetUserCount();
			}

			return apResourceA->GetTime() > apResourceB->GetTime();
		}
	};

	//-----------------------------------------------------------------------

    void iResourceManager::DestroyUnused(int alMaxToKeep)
	{
		//Log("Start Num Of: %d\n",m_mapHandleResources.size());
		//Check if there are too many resources.
		if((int)m_mapHandleResources.size() <= alMaxToKeep) return;

		//Add resources to a vector
		std::vector<iResourceBase*> vResources;
		vResources.reserve(m_mapHandleResources.size());

		tResourceHandleMapIt it = m_mapHandleResources.begin();
		for(;it != m_mapHandleResources.end();++it)
		{
			vResources.push_back(it->second);
		}

		//Sort the sounds according to num of users and then time.
		std::sort(vResources.begin(), vResources.end(), cSortResources());

		//Log("-------------Num: %d-----------------\n",vResources.size());
		for(size_t i=alMaxToKeep; i<vResources.size(); ++i)
		{
			iResourceBase *pRes = vResources[i];
			//Log("%s count:%d time:%d\n",pRes->GetName().c_str(),
			//							pRes->GetUserCount(),
			//							pRes->GetTime());

			if(pRes->HasUsers()==false)
			{
				RemoveResource(pRes);
				hplDelete(pRes);
			}
		}
		//Log("--------------------------------------\n");
		//Log("End Num Of: %d\n",m_mapHandleResources.size());

	}

	//-----------------------------------------------------------------------

	void iResourceManager::DestroyAll()
	{
		tResourceHandleMapIt it = m_mapHandleResources.begin();
		while(it != m_mapHandleResources.end())
		{
			//Log("Start destroy...");

			iResourceBase* pResource = it->second;

			//Log(" res: : %d ...",pResource->GetName().c_str(),pResource->GetUserCount());

			while(pResource->HasUsers()) pResource->DecUserCount();

			Destroy(pResource);

			it = m_mapHandleResources.begin();

			//Log(" Done!\n");
		}
	}

	//-----------------------------------------------------------------------

	//////////////////////////////////////////////////////////////////////////
	// PROTECTED METHODS
	//////////////////////////////////////////////////////////////////////////

	//-----------------------------------------------------------------------

	void iResourceManager::BeginLoad(const tString& asFile)
	{
		mlTimeStart = GetApplicationTime();

		//Log("Begin resource: %s\n",asFile.c_str());

		mlTabCount++;
	}

	//-----------------------------------------------------------------------

	void iResourceManager::EndLoad()
	{
		mlTabCount--;
	}

	//-----------------------------------------------------------------------

	iResourceBase* iResourceManager::FindLoadedResource(const tString &asName, tString &asFilePath)
	{
		iResourceBase* pResource = GetByName(asName);
		if(pResource==NULL){
			asFilePath = mpFileSearcher->GetFilePath(asName);
		}
		else {
			asFilePath = "";
		}

		return pResource;
	}

	//-----------------------------------------------------------------------

	tString iResourceManager::GetTabs()
	{
		tString sTabs ="";
		for(int i=0; i<mlTabCount; ++i) sTabs+="  ";
		return sTabs;
	}

	void iResourceManager::AddResource(iResourceBase* apResource, bool abLog)
	{
		apResource->SetHandle(GetHandle());

		tString sName = cString::ToLowerCase(apResource->GetName());

		m_mapHandleResources.insert(tResourceHandleMap::value_type(
									apResource->GetHandle(), apResource));
		m_mapNameResources.insert(tResourceNameMap::value_type(
									sName, apResource));

		if(abLog && iResourceBase::GetLogCreateAndDelete())
		{
			unsigned long lTime = GetApplicationTime() - mlTimeStart;
            Log("%sLoaded resource %s in %lu ms\n",GetTabs().c_str(), apResource->GetName().c_str(),lTime);
			apResource->SetLogDestruction(true);
		}

		//Log("End resource: %s\n",apResource->GetName().c_str());
	}

	//-----------------------------------------------------------------------

	void iResourceManager::RemoveResource(iResourceBase* apResource)
	{
		m_mapHandleResources.erase(apResource->GetHandle());
		m_mapNameResources.erase(cString::ToLowerCase(apResource->GetName()));

		//Log("Removing %s %d %d!\n", apResource->GetName().c_str(),x,y);
 	}

	//-----------------------------------------------------------------------

	unsigned long iResourceManager::GetHandle()
	{
		return mlHandleCount++;
	}

	//-----------------------------------------------------------------------


}
