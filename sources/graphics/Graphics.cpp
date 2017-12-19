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
#include "graphics/Graphics.h"

#include "system/LowLevelSystem.h"
#include "graphics/LowLevelGraphics.h"
#include "resources/LowLevelResources.h"
#include "graphics/GraphicsDrawer.h"
#include "graphics/Renderer2D.h"
#include "graphics/Renderer3D.h"
#include "graphics/RendererPostEffects.h"
#include "graphics/RenderList.h"
#include "graphics/MaterialHandler.h"
#include "graphics/MeshCreator.h"
#include "game/Updateable.h"
#include "resources/Resources.h"

//2D Materials
#include "graphics/Material_BumpSpec2D.h"
#include "graphics/Material_Diffuse2D.h"
#include "graphics/Material_DiffuseAdditive2D.h"
#include "graphics/Material_DiffuseAlpha2D.h"
#include "graphics/Material_Smoke2D.h"
#include "graphics/Material_FontNormal.h"

//3D Materials
#include "graphics/Material_Alpha.h"
#include "graphics/Material_Diffuse.h"
#include "graphics/Material_DiffuseSpec.h"
#include "graphics/Material_Bump.h"
#include "graphics/Material_BumpSpec.h"
#include "graphics/Material_BumpColorSpec.h"
#include "graphics/Material_Additive.h"
#include "graphics/Material_Flat.h"
#include "graphics/Material_Modulative.h"
#include "graphics/Material_ModulativeX2.h"
#include "graphics/Material_Alpha.h"
#include "graphics/Material_EnvMap_Reflect.h"
#include "graphics/Material_Water.h"

namespace hpl {

	//////////////////////////////////////////////////////////////////////////
	// CONSTRUCTORS
	//////////////////////////////////////////////////////////////////////////

	//-----------------------------------------------------------------------

	cGraphics::cGraphics(iLowLevelGraphics *apLowLevelGraphics, iLowLevelResources *apLowLevelResources)
	{
		mpLowLevelGraphics = apLowLevelGraphics;
		mpLowLevelResources = apLowLevelResources;

		mpDrawer = NULL;
		mpMeshCreator = NULL;
		mpMaterialHandler = NULL;
		mpRenderer2D = NULL;
		mpRenderer3D = NULL;
		mpRendererPostEffects = NULL;
	}

	//-----------------------------------------------------------------------

	cGraphics::~cGraphics()
	{
		Log("Exiting Graphics Module\n");
		Log("--------------------------------------------------------\n");

		hplDelete(mpRenderer2D);
		hplDelete(mpRenderer3D);
		hplDelete(mpRendererPostEffects);
		hplDelete(mpDrawer);
		hplDelete(mpMeshCreator);
		hplDelete(mpMaterialHandler);
		hplDelete(mpRenderList);

		Log("--------------------------------------------------------\n\n");
	}

	//-----------------------------------------------------------------------

	//////////////////////////////////////////////////////////////////////////
	// PUBLIC METHODS
	//////////////////////////////////////////////////////////////////////////

	//-----------------------------------------------------------------------

	bool cGraphics::Init(	int alWidth, int alHeight, int alBpp, int abFullscreen,
							int alMultisampling,const tString &asWindowCaption,
							cResources* apResources)
	{
		Log("Initializing Graphics Module\n");
		Log("--------------------------------------------------------\n");

		//Setup the graphic directories:
		apResources->AddResourceDir("core/programs");
		apResources->AddResourceDir("core/textures");

		Log(" Init low level graphics\n");
		mpLowLevelGraphics->Init(alWidth,alHeight,alBpp,abFullscreen,alMultisampling,asWindowCaption);

		Log(" Creating graphic systems\n");
		mpMaterialHandler = hplNew( cMaterialHandler,(this, apResources));
		mpDrawer = hplNew( cGraphicsDrawer,(mpLowLevelGraphics,mpMaterialHandler,apResources));
		mpRenderer2D = hplNew( cRenderer2D,(mpLowLevelGraphics,apResources,mpDrawer));
		mpRenderList = hplNew( cRenderList,(this));
		mpMeshCreator = hplNew( cMeshCreator,(mpLowLevelGraphics, apResources));
		mpRenderer3D = hplNew( cRenderer3D,(mpLowLevelGraphics,apResources,mpMeshCreator,mpRenderList));
		mpRendererPostEffects = hplNew( cRendererPostEffects,(mpLowLevelGraphics,apResources,mpRenderList,
														mpRenderer3D));
		mpRenderer3D->SetPostEffects(mpRendererPostEffects);


		//Add all the materials.
		//2D
		Log(" Adding engine materials\n");
		mpMaterialHandler->Add(hplNew( cMaterialType_BumpSpec2D,() ) );
		mpMaterialHandler->Add(hplNew( cMaterialType_DiffuseAdditive2D,()) );
		mpMaterialHandler->Add(hplNew( cMaterialType_DiffuseAlpha2D,()) );
		mpMaterialHandler->Add(hplNew( cMaterialType_Diffuse2D,()) );
		mpMaterialHandler->Add(hplNew( cMaterialType_Smoke2D,()) );
		mpMaterialHandler->Add(hplNew( cMaterialType_FontNormal,()) );

		//3D
		mpMaterialHandler->Add(hplNew( cMaterialType_Diffuse,()) );
		mpMaterialHandler->Add(hplNew( cMaterialType_Bump,()) );
		mpMaterialHandler->Add(hplNew( cMaterialType_DiffuseSpec,()) );
		mpMaterialHandler->Add(hplNew( cMaterialType_BumpSpec,()) );
		mpMaterialHandler->Add(hplNew( cMaterialType_BumpColorSpec,()) );

		mpMaterialHandler->Add(hplNew( cMaterialType_Additive,()) );
		mpMaterialHandler->Add(hplNew( cMaterialType_Alpha,()) );
		mpMaterialHandler->Add(hplNew( cMaterialType_Flat,()) );
		mpMaterialHandler->Add(hplNew( cMaterialType_Modulative,()) );
		mpMaterialHandler->Add(hplNew( cMaterialType_ModulativeX2,()) );

		mpMaterialHandler->Add(hplNew( cMaterialType_EnvMap_Reflect,()) );

		mpMaterialHandler->Add(hplNew( cMaterialType_Water,()) );

		Log("--------------------------------------------------------\n\n");

		return true;
	}

	//-----------------------------------------------------------------------

	iLowLevelGraphics* cGraphics::GetLowLevel()
	{
		return mpLowLevelGraphics;
	}

	//-----------------------------------------------------------------------

	cGraphicsDrawer* cGraphics::GetDrawer()
	{
		return mpDrawer;
	}

	//-----------------------------------------------------------------------

	cRenderer2D* cGraphics::GetRenderer2D()
	{
		return mpRenderer2D;
	}

	//-----------------------------------------------------------------------

}
