#pragma once

#include "math/MathTypes.h"
#include "resources/MeshLoader.h"
#include "graphics/VertexBuffer.h"

#include "system/String.h"

class TiXmlElement;
class TiXmlDocument;

namespace hpl {

	class cMesh;
	class cNode3D;
	class iVertexBuffer;

	class IndexedFile
	{
	public:
		tString msId;
		tString msPath;
	};
	typedef std::vector<IndexedFile> tFileIndex;

	class StaticObject
	{
	public:
		bool castsShadow;
		bool collides;
		tString fileIndex;
		tString group;
		tString id;
		tString name;
		float rotation[3];
		float scale[3];
		tString tag;
		float worldPosition[3];
	};
	typedef std::vector<StaticObject> tStaticObjects;

	typedef std::map<tString, tString> tUserVariables;

	class MapEntity
	{
	public:
		bool active;
		tString fileIndex;
		tString group;
		tString id;
		tString name;
		float rotation[3];
		float scale[3];
		tString tag;
		float worldPosition[3];
		tUserVariables variables;
	};
	typedef std::vector<MapEntity> tMapEntities;

	class MapLightEntity : public MapEntity
	{
	public:
		float radius;
	};
	typedef std::vector<MapLightEntity> tMapLightEntities;

	class Area
	{
	public:
		bool active;
		tString name;
		float rotation[3];
		float scale[3];
		float worldPosition[3];
	};
	typedef std::vector<Area> tAreas;

	class cMeshLoaderMap :
		public iMeshLoader
	{
	public:
		cMeshLoaderMap(iLowLevelGraphics* apLowLevelGraphics);
		~cMeshLoaderMap();

		cMesh* LoadMesh(const tString& asFile, tMeshLoadFlag aFlags);
		bool SaveMesh(cMesh* apMesh, const tString& asFile);

		cWorld3D* LoadWorld(const tString& asFile, cScene* apScene, tWorldLoadFlag aFlags);

		cAnimation* LoadAnimation(const tString& asFile) { return NULL; }

		bool IsSupported(const tString asFileType);

		void AddSupportedTypes(tStringVec* avFileTypes);
	private:
		tString LookupFile(tFileIndex index, tString id);
		void ReadFileIndicies(TiXmlDocument* xmlDoc, tFileIndex* fileIndicies, tString parentNodeName);
		void ReadStaticObjects(TiXmlDocument* xmlDoc, tStaticObjects* staticObjects);
		void ReadMapEntities(TiXmlDocument* xmlDoc, tMapEntities* mapEntities, tMapLightEntities* lightEntities, tAreas* startAreas);
		static void PopulateEntityData(TiXmlElement* entityElem, MapEntity& mapEntity);
		void LoadWorldGeometry(cWorld3D* world, tStaticObjects* staticObjects, tFileIndex staticObjectFiles);
		static cMatrixf CreateTransformMatrix(float* vec3position, float* vec3rotation, float* vec3scale);
		static cVector3f FloatArrayToVec3(float* vec3array, bool convertZToY);
		void LoadWorldSceneObjects(cWorld3D* world, tMapEntities* mapEntities, tMapLightEntities* lightEntities, tAreas* startAreas);
	};
}

