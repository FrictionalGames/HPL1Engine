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
#ifndef HPL_RENDERER2D_H
#define HPL_RENDERER2D_H

#include <set>
#include <list>
#include <math.h>

#include "graphics/GraphicsTypes.h"
#include "math/MathTypes.h"
#include "scene/Light2D.h"
#include "graphics/Material.h"
#include "graphics/Mesh2d.h"

namespace hpl {

	#define MAX_SHADOW_POINTS (100)
	#define MAX_ONSCREEN_SHADOWCASTERS (100)

	class cScene;
	class cResources;
	class iLowLevelGraphics;
	class iLowLevelResources;
	class cRenderObject2D;
	class cGraphicsDrawer;
	class cCamera2D;
	class cGridMap2D;
	class iTexture;

	//For Normal obejcts:
	class cRenderObject2DCompare
	{
	public:
		bool operator()(const cRenderObject2D &aObjectA,const cRenderObject2D &aObjectB) const;
	};

	//For transparent objects
	class cRenderTransObjectCompare
	{
	public:
		bool operator()(const cRenderObject2D &aObjectA,const cRenderObject2D &aObjectB) const;
	};

	typedef std::multiset<cRenderObject2D,cRenderObject2DCompare> tRenderObjectSet;
	typedef tRenderObjectSet::iterator tRenderObjectSetIt;

	typedef std::multiset<cRenderObject2D,cRenderTransObjectCompare> tRenderTransObjectSet;
	typedef tRenderTransObjectSet::iterator tRenderTransObjectSetIt;

	typedef std::list<cRenderObject2D> tRenderObjectList;
	typedef tRenderObjectList::iterator tRenderObjectListIt;

	class cResources;
	class cWorld2D;

	class cRenderer2D
	{
	public:
		cRenderer2D(iLowLevelGraphics *apLowLevelGraphics,cResources* apResources,cGraphicsDrawer *apGraphicsDrawer);
		~cRenderer2D();

		void RenderObjects(cCamera2D *apCamera, cGridMap2D *apMapLights,cWorld2D* apWorld);
		void AddObject(cRenderObject2D &aObject);

		void SetShadowZ(float afZ){ mfShadowZ = afZ;}
		float GetShadowZ(){ return mfShadowZ;}

		void SetAmbientLight(cColor aCol){ mAmbientLight = aCol;}
		cColor GetAmbientLight(){ return mAmbientLight;}

		iTexture* GetLightMap(int alNum){
			if(alNum<0 || alNum>1)return NULL;
			return mpLightMap[alNum];
		}

	private:
		iLowLevelGraphics *mpLowLevelGraphics;
		iLowLevelResources *mpLowLevelResources;
		cResources* mpResources;
		cGraphicsDrawer *mpGraphicsDrawer;

		tRenderObjectSet m_mapObject;
		tRenderTransObjectSet m_mapTransObject;

		/////// LIGHTING  ///////////////////////
		iTexture* mpLightMap[2];

		cColor mAmbientLight;

		cRect2f mPrevLightRect;

		int mvShadowPoints[MAX_SHADOW_POINTS][2];
		int mlShadowPointSize;

		float mfShadowZ;

		tLightList mlstLights;
		tLightList mlstFastLights;

		//////// RENDERING ///////////////////////
		void ClearShadows();
		bool RenderShadows(cCamera2D *apCamera,iLight2D* pLight,cWorld2D* apWorld);

		inline void RenderObject(const cRenderObject2D& aObject, unsigned int &aIdxAdd, iMaterial* pMat,
									iLight2D* pLight, eMaterialRenderType aRenderType, cCamera2D* apCam);


		/////// LIGHTING  ///////////////////////

		inline cVector2f CalcLineEnd(cVector3f avLight,cVector3f avPoint, float afRadius,
									cVector2f &avSide, cVector2f avClipPos);

		inline void FindShadowPoints(tMesh2DEdgeVec* apEdgeVec, cVector2f avLightPos, cVector2f avTilePos);

		inline int CreateVertexes(cVector2f vLightPos,cRect2f LightRect, float fRadius, bool bNonFit,
			cVector2f vTilePos,tVertexVec* apVtxVec,cColor ShadowColor,int lFirstIndex,float fSourceSize);

		inline bool ClipPoints(cVector3f *avPoint,cRect2f aRect,cVector2f avPos, float afSize);
	};

};
#endif // HPL_RENDERER2D_H
