#pragma once

#include "math/MathTypes.h"
#include "resources/MeshLoader.h"
#include "graphics/VertexBuffer.h"

class TiXmlElement;

namespace hpl {

	class cMesh;
	class cNode3D;
	class iVertexBuffer;

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
	};
}

