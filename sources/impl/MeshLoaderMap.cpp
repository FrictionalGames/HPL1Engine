#include "impl/MeshLoaderMap.h"

#include "physics/Physics.h"
#include "physics/PhysicsWorld.h"
#include "physics/PhysicsBody.h"
#include "physics/PhysicsMaterial.h"
#include "physics/SurfaceData.h"

#include "resources/MeshManager.h"

#include "scene/Light3DPoint.h"
#include "scene/MeshEntity.h"
#include "scene/Scene.h"
#include "scene/World3D.h"

#include "system/LowLevelSystem.h"
#include "system/String.h"

#include "impl/tinyXML/tinyxml.h"

#include "math/Math.h"

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
		tMapLightEntities lightEntities;

		cWorld3D* pWorld = apScene->CreateWorld3D(cString::SetFileExt(cString::GetFileName(asFile), ""));
		pWorld->SetFileName(cString::GetFileName(asFile));

		iPhysicsWorld* pPhysiscsWorld = pWorld->GetPhysics()->CreateWorld(true);
		pWorld->SetPhysicsWorld(pPhysiscsWorld, true);

		TiXmlDocument* pXmlDoc = hplNew(TiXmlDocument, (asFile.c_str()));
		if (pXmlDoc->LoadFile() == false) FatalError("Couldn't load map XML file '%s'!\n", asFile.c_str());

		ReadFileIndicies(pXmlDoc, &staticObjectFiles, "FileIndex_StaticObjects");
		ReadFileIndicies(pXmlDoc, &entityFiles, "FileIndex_Entities");
		ReadFileIndicies(pXmlDoc, &decalFiles, "FileIndex_Decals");

		ReadStaticObjects(pXmlDoc, &staticObjects);
		ReadMapEntities(pXmlDoc, &mapEntities, &lightEntities);

		LoadWorldGeometry(pWorld, &staticObjects, staticObjectFiles);
		LoadWorldSceneObjects(pWorld, &mapEntities, &lightEntities);

		pWorld->SetUpData();

		return pWorld;
	}

	bool cMeshLoaderMap::IsSupported(const tString asFileType)
	{
		return asFileType == "map";
	}

	void cMeshLoaderMap::AddSupportedTypes(tStringVec* avFileTypes)
	{
	}

	tString cMeshLoaderMap::LookupFile(tFileIndex index, tString id)
	{
		for (int i = 0; i < index.size(); i++)
			if (index[i].msId == id)
				return index[i].msPath;
		return "";
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

	void cMeshLoaderMap::ReadMapEntities(TiXmlDocument* xmlDoc, tMapEntities* mapEntities, tMapLightEntities* lightEntities)
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

				PopulateEntityData(entityElem, mapEntity);
				mapEntity.fileIndex = cString::ToString(entityElem->Attribute("FileIndex"), "");
				mapEntity.id = cString::ToString(entityElem->Attribute("ID"), "");
				mapEntity.group = cString::ToString(entityElem->Attribute("Group"), "");

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
			TiXmlElement* lightElem = staticObjectsElem->FirstChildElement("PointLight");
			while (lightElem)
			{
				MapLightEntity lightEntity;
				PopulateEntityData(lightElem, lightEntity);
				lightEntity.radius = cString::ToFloat(lightElem->Attribute("Radius"), 1);
				lightEntities->push_back(lightEntity);
				lightElem = lightElem->NextSiblingElement("PointLight");
			}
		}
		else
		{
			Error("Couldn't find Entities element in map file!");
		}
	}

	void cMeshLoaderMap::PopulateEntityData(TiXmlElement* entityElem, MapEntity& mapEntity)
	{
		mapEntity.name = cString::ToString(entityElem->Attribute("Name"), "");
		cString::FloatStringToArray(mapEntity.rotation, entityElem->Attribute("Rotation"), 3);
		cString::FloatStringToArray(mapEntity.scale, entityElem->Attribute("Scale"), 3);
		mapEntity.tag = cString::ToString(entityElem->Attribute("Tag"), "");
		cString::FloatStringToArray(mapEntity.worldPosition, entityElem->Attribute("WorldPos"), 3);
	}

	void cMeshLoaderMap::LoadWorldGeometry(cWorld3D* world, tStaticObjects* staticObjects, tFileIndex staticObjectFiles)
	{
		for(int i = 0; i < staticObjects->size(); i++)
		{
			StaticObject staticObject = staticObjects->at(i);
			tString fileName = LookupFile(staticObjectFiles, staticObject.fileIndex);
			cMesh* pMesh = mpMeshManager->CreateMesh(fileName);
			if (!pMesh)
				Error("Error loading external mesh entity %s!", fileName);
			else
			{
				cMeshEntity* pEntity = world->CreateMeshEntity(staticObject.name, pMesh, false);
				cMatrixf transform = CreateTransformMatrix(staticObject.worldPosition, staticObject.rotation, staticObject.scale);
				pEntity->SetMatrix(transform);
				pEntity->SetCastsShadows(staticObject.castsShadow);
			}
		}
	}

	cMatrixf cMeshLoaderMap::CreateTransformMatrix(float* vec3position, float* vec3rotation, float* vec3scale)
	{
		cMatrixf transform = cMatrixf::Identity;
		cQuaternion rotation;

		// Apply position
		transform = cMath::MatrixMul(transform, cMath::MatrixTranslate(FloatArrayToVec3(vec3position, false)));
		// Apply rotation
		rotation.FromAngleAxis(vec3rotation[0], cVector3f(1, 0, 0)); // X
		transform = cMath::MatrixMul(transform, cMath::MatrixQuaternion(rotation));
		rotation.FromAngleAxis(vec3rotation[1], cVector3f(0, 1, 0)); // Y
		transform = cMath::MatrixMul(transform, cMath::MatrixQuaternion(rotation));
		rotation.FromAngleAxis(vec3rotation[2], cVector3f(0, 0, 1)); // Z
		transform = cMath::MatrixMul(transform, cMath::MatrixQuaternion(rotation));
		// Apply scale
		transform = cMath::MatrixMul(transform, cMath::MatrixScale(FloatArrayToVec3(vec3scale, false)));

		return transform;
	}

	cVector3f cMeshLoaderMap::FloatArrayToVec3(float* vec3array, bool convertZToY)
	{
		cVector3f result = convertZToY ?
			cVector3f(-vec3array[0], vec3array[2], vec3array[1]) :
			cVector3f(vec3array[0], vec3array[1], vec3array[2]);
		return result;
	}

	void cMeshLoaderMap::LoadWorldSceneObjects(cWorld3D* world, tMapEntities* mapEntities, tMapLightEntities* lightEntities)
	{
		for (int i = 0; i < lightEntities->size(); i++)
		{
			MapLightEntity lightEntity = lightEntities->at(i);
			cLight3DPoint* pLight = world->CreateLightPoint(lightEntity.name, false);
			cMatrixf lightTransform = CreateTransformMatrix(lightEntity.worldPosition, lightEntity.rotation, lightEntity.scale);
			pLight->SetMatrix(lightTransform);
			pLight->SetDiffuseColor(cColor(1, 1, 1));
		}
	}
}