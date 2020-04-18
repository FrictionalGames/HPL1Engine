#pragma once

#include "math/MathTypes.h"
#include "resources/MeshLoader.h"
#include "graphics/VertexBuffer.h"

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
		void ReadFileIndicies(TiXmlDocument* xmlDoc, tFileIndex* fileIndicies, tString parentNodeName);
		void ReadStaticObjects(TiXmlDocument* xmlDoc, tStaticObjects* staticObjects);
	};
}

