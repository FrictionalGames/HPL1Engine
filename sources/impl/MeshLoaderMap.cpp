#include "impl/MeshLoaderMap.h"
#include "scene/Scene.h"
#include "scene/World3D.h"
#include "system\LowLevelSystem.h"
#include "system/String.h"

#include "impl/tinyXML/tinyxml.h"

namespace hpl {
	cMeshLoaderMap::cMeshLoaderMap(iLowLevelGraphics* apLowLevelGraphics) : iMeshLoader(apLowLevelGraphics)
	{ 
	}

	cMeshLoaderMap::~cMeshLoaderMap()
	{
	}

	cMesh* cMeshLoaderMap::LoadMesh(const tString& asFile, tMeshLoadFlag aFlags)
	{
		return nullptr;
	}

	bool cMeshLoaderMap::SaveMesh(cMesh* apMesh, const tString& asFile)
	{
		return false;
	}

	cWorld3D* cMeshLoaderMap::LoadWorld(const tString& asFile, cScene* apScene, tWorldLoadFlag aFlags)
	{
		tFileIndex staticObjectFiles;
		tFileIndex entityFiles;
		tFileIndex decalFiles;

		cWorld3D* pWorld = apScene->CreateWorld3D(cString::SetFileExt(cString::GetFileName(asFile), ""));
		pWorld->SetFileName(cString::GetFileName(asFile));

		TiXmlDocument* pXmlDoc = hplNew(TiXmlDocument, (asFile.c_str()));
		if (pXmlDoc->LoadFile() == false) FatalError("Couldn't load map XML file '%s'!\n", asFile.c_str());

		ReadFileIndicies(pXmlDoc, &staticObjectFiles, "FileIndex_StaticObjects");
		ReadFileIndicies(pXmlDoc, &entityFiles, "FileIndex_Entities");
		ReadFileIndicies(pXmlDoc, &decalFiles, "FileIndex_Decals");

		return pWorld;
	}

	bool cMeshLoaderMap::IsSupported(const tString asFileType)
	{
		return asFileType == "map";
	}

	void cMeshLoaderMap::AddSupportedTypes(tStringVec* avFileTypes)
	{
	}

	void cMeshLoaderMap::ReadFileIndicies(TiXmlDocument* xmlDoc, tFileIndex* fileIndicies, tString parentNodeName)
	{
		TiXmlElement* pRootElem = xmlDoc->RootElement();
		TiXmlElement* fileIndexElem = pRootElem->FirstChildElement("MapData")
			->FirstChildElement("MapContents")
			->FirstChildElement(parentNodeName.c_str());
		if (fileIndexElem)
		{
			TiXmlElement* fileIndex = fileIndexElem->FirstChildElement("File");
			while (fileIndex)
			{
				IndexedFile indexedFile;
				indexedFile.msId = cString::ToString(fileIndex->Attribute("Id"), "");
				indexedFile.msPath = cString::ToString(fileIndex->Attribute("Path"), "");
				fileIndicies->push_back(indexedFile);
				fileIndex = fileIndex->NextSiblingElement();
			}
		}
		else
		{
			Error("Couldn't find file index %s!\n", parentNodeName.c_str());
		}
	}
}