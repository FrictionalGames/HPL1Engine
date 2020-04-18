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
	};
}

