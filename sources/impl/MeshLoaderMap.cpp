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

		tStaticObjects staticObjects;
		tMapEntities mapEntities;

		cWorld3D* pWorld = apScene->CreateWorld3D(cString::SetFileExt(cString::GetFileName(asFile), ""));
		pWorld->SetFileName(cString::GetFileName(asFile));

		TiXmlDocument* pXmlDoc = hplNew(TiXmlDocument, (asFile.c_str()));
		if (pXmlDoc->LoadFile() == false) FatalError("Couldn't load map XML file '%s'!\n", asFile.c_str());

		ReadFileIndicies(pXmlDoc, &staticObjectFiles, "FileIndex_StaticObjects");
		ReadFileIndicies(pXmlDoc, &entityFiles, "FileIndex_Entities");
		ReadFileIndicies(pXmlDoc, &decalFiles, "FileIndex_Decals");

		ReadStaticObjects(pXmlDoc, &staticObjects);
		ReadMapEntities(pXmlDoc, &mapEntities);

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
		TiXmlElement* fileIndexElem = pRootElem
			->FirstChildElement("MapData")
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

	void cMeshLoaderMap::ReadStaticObjects(TiXmlDocument* xmlDoc, tStaticObjects* staticObjects)
	{
		TiXmlElement* pRootElem = xmlDoc->RootElement();
		TiXmlElement* staticObjectsElem = pRootElem
			->FirstChildElement("MapData")
			->FirstChildElement("MapContents")
			->FirstChildElement("StaticObjects");
		if (staticObjectsElem)
		{
			TiXmlElement* staticObjectElem = staticObjectsElem->FirstChildElement("StaticObject");
			while (staticObjectElem)
			{
				StaticObject staticObject;

				staticObject.castsShadow = cString::ToBool(staticObjectElem->Attribute("CastsShadow"), "true");
				staticObject.collides = cString::ToBool(staticObjectElem->Attribute("Collides"), "true");
				staticObject.fileIndex = cString::ToString(staticObjectElem->Attribute("FileIndex"), "");
				staticObject.id = cString::ToString(staticObjectElem->Attribute("ID"), "");
				staticObject.group = cString::ToString(staticObjectElem->Attribute("Group"), "");
				staticObject.name = cString::ToString(staticObjectElem->Attribute("Name"), "");
				cString::FloatStringToArray(staticObject.rotation, staticObjectElem->Attribute("Rotation"), 3);
				cString::FloatStringToArray(staticObject.scale, staticObjectElem->Attribute("Scale"), 3);
				staticObject.tag = cString::ToString(staticObjectElem->Attribute("Tag"), "");
				cString::FloatStringToArray(staticObject.worldPosition, staticObjectElem->Attribute("WorldPos"), 3);

				staticObjects->push_back(staticObject);

				staticObjectElem = staticObjectElem->NextSiblingElement();
			}
		}
		else
		{
			Error("Couldn't find StaticObjects element in map file!");
		}
	}

	void cMeshLoaderMap::ReadMapEntities(TiXmlDocument* xmlDoc, tMapEntities* mapEntities)
	{
		TiXmlElement* pRootElem = xmlDoc->RootElement();
		TiXmlElement* staticObjectsElem = pRootElem
			->FirstChildElement("MapData")
			->FirstChildElement("MapContents")
			->FirstChildElement("Entities");
		if (staticObjectsElem)
		{
			TiXmlElement* entityElem = staticObjectsElem->FirstChildElement("Entity");
			while (entityElem)
			{
				MapEntity mapEntity;

				mapEntity.fileIndex = cString::ToString(entityElem->Attribute("FileIndex"), "");
				mapEntity.id = cString::ToString(entityElem->Attribute("ID"), "");
				mapEntity.group = cString::ToString(entityElem->Attribute("Group"), "");
				mapEntity.name = cString::ToString(entityElem->Attribute("Name"), "");
				cString::FloatStringToArray(mapEntity.rotation, entityElem->Attribute("Rotation"), 3);
				cString::FloatStringToArray(mapEntity.scale, entityElem->Attribute("Scale"), 3);
				mapEntity.tag = cString::ToString(entityElem->Attribute("Tag"), "");
				cString::FloatStringToArray(mapEntity.worldPosition, entityElem->Attribute("WorldPos"), 3);

				TiXmlElement* variablesElem = entityElem->FirstChildElement("UserVariables");
				if (variablesElem)
				{
					TiXmlElement* variableElem = variablesElem->FirstChildElement("Var");
					while (variableElem)
					{
						tString varName = cString::ToString(variableElem->Attribute("Name"), "");
						tString varValue = cString::ToString(variableElem->Attribute("Value"), "");

						if (varName != "")
							mapEntity.variables[varName] = varValue;

						variableElem = variableElem->NextSiblingElement("Var");
					}
				}

				mapEntities->push_back(mapEntity);

				entityElem = entityElem->NextSiblingElement("Entity");
			}
		}
		else
		{
			Error("Couldn't find Entities element in map file!");
		}
	}
}