#include "impl/MeshLoaderMap.h"
#include "system\LowLevelSystem.h"

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
		FatalError("Not implemented");
		return nullptr;
	}

	bool cMeshLoaderMap::IsSupported(const tString asFileType)
	{
		return asFileType == "map";
	}

	void cMeshLoaderMap::AddSupportedTypes(tStringVec* avFileTypes)
	{
	}
}