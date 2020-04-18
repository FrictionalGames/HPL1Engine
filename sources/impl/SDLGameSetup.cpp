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
#ifdef WIN32
#pragma comment(lib, "SDL2.lib")
#pragma comment(lib, "SDLmain.lib")
#endif

#include "impl/SDLGameSetup.h"
#include "impl/KeyboardSDL.h"
#include "impl/MouseSDL.h"
#include "impl/LowLevelGraphicsSDL.h"
#include "impl/LowLevelResourcesSDL.h"
#include "impl/LowLevelSystemSDL.h"
#include "impl/LowLevelInputSDL.h"
#include "impl/LowLevelSoundOpenAL.h"
#include "impl/LowLevelPhysicsNewton.h"
#ifdef INCLUDE_HAPTIC
#include "impl/LowLevelHapticHaptX.h"
#endif
#include "haptic/Haptic.h"

#include "SDL/SDL.h"

namespace hpl {

	//////////////////////////////////////////////////////////////////////////
	// CONSTRUCTORS
	//////////////////////////////////////////////////////////////////////////

	//-----------------------------------------------------------------------

	cSDLGameSetup::cSDLGameSetup()
	{
		if (SDL_Init( SDL_INIT_VIDEO | SDL_INIT_TIMER ) < 0) {
			FatalError("Error Initializing Display: %s",SDL_GetError());
			exit(1);
		}

		mpLowLevelSystem = hplNew( cLowLevelSystemSDL, () );
		mpLowLevelGraphics = hplNew( cLowLevelGraphicsSDL,() );
		mpLowLevelInput = hplNew( cLowLevelInputSDL,(mpLowLevelGraphics) );
		mpLowLevelResources = hplNew( cLowLevelResourcesSDL,((cLowLevelGraphicsSDL *)mpLowLevelGraphics) );
//		#ifdef WIN32
//			mpLowLevelSound = hplNew( cLowLevelSoundFmod, () );
//		#else
			mpLowLevelSound	= hplNew( cLowLevelSoundOpenAL,() );
//		#endif
		mpLowLevelPhysics = hplNew( cLowLevelPhysicsNewton,() );

#ifdef INCLUDE_HAPTIC
		mpLowLevelHaptic = hplNew( cLowLevelHapticHaptX,() );
#else
		mpLowLevelHaptic = NULL;
#endif

	}

	//-----------------------------------------------------------------------

	cSDLGameSetup::~cSDLGameSetup()
	{
		Log("- Deleting lowlevel stuff.\n");

		Log("  Physics\n");
		hplDelete(mpLowLevelPhysics);
		Log("  Sound\n");
		hplDelete(mpLowLevelSound);
		Log("  Input\n");
		hplDelete(mpLowLevelInput);
		Log("  Resources\n");
		hplDelete(mpLowLevelResources);
		Log("  System\n");
		hplDelete(mpLowLevelSystem);
		Log("  Graphics\n");
		hplDelete(mpLowLevelGraphics);
		Log("  Haptic\n");
		if(mpLowLevelHaptic) hplDelete(mpLowLevelHaptic);

		SDL_Quit();
	}

	//-----------------------------------------------------------------------

	//////////////////////////////////////////////////////////////////////////
	// PUBLIC METHODS
	//////////////////////////////////////////////////////////////////////////

	//-----------------------------------------------------------------------

	cScene* cSDLGameSetup::CreateScene(cGraphics* apGraphics, cResources *apResources, cSound* apSound,
										cPhysics *apPhysics, cSystem *apSystem,cAI *apAI,
										cHaptic *apHaptic)
	{
		cScene *pScene = hplNew( cScene, (apGraphics,apResources, apSound,apPhysics, apSystem,apAI,apHaptic) );
		return pScene;
	}

	//-----------------------------------------------------------------------


	/**
	 * \todo Lowlevelresource and resource both use lowlevel graphics. Can this be fixed??
	 * \param apGraphics
	 * \return
	 */
	cResources* cSDLGameSetup::CreateResources(cGraphics* apGraphics, tWString assetsPath)
	{
		cResources *pResources = hplNew( cResources, (mpLowLevelResources,mpLowLevelGraphics, assetsPath) );
		return pResources;
	}

	//-----------------------------------------------------------------------

	cInput* cSDLGameSetup::CreateInput(cGraphics* apGraphics)
	{
		cInput *pInput = hplNew( cInput, (mpLowLevelInput) );
		return pInput;
	}

	//-----------------------------------------------------------------------

	cSystem* cSDLGameSetup::CreateSystem()
	{
		cSystem *pSystem = hplNew( cSystem, (mpLowLevelSystem) );
		return pSystem;
	}

	//-----------------------------------------------------------------------

	cGraphics* cSDLGameSetup::CreateGraphics()
	{
		cGraphics *pGraphics = hplNew( cGraphics, (mpLowLevelGraphics,mpLowLevelResources) );
		return pGraphics;
	}
	//-----------------------------------------------------------------------

	cSound* cSDLGameSetup::CreateSound()
	{
		cSound *pSound = hplNew( cSound, (mpLowLevelSound) );
		return pSound;
	}

	//-----------------------------------------------------------------------

	cPhysics* cSDLGameSetup::CreatePhysics()
	{
		cPhysics *pPhysics = hplNew( cPhysics, (mpLowLevelPhysics) );
		return pPhysics;
	}

	//-----------------------------------------------------------------------

	cAI* cSDLGameSetup::CreateAI()
	{
		cAI *pAI = hplNew( cAI,() );
		return pAI;
	}

	//-----------------------------------------------------------------------

	cHaptic* cSDLGameSetup::CreateHaptic()
	{
		cHaptic *pHaptic = hplNew( cHaptic, (mpLowLevelHaptic) );
		return pHaptic;
	}

	//-----------------------------------------------------------------------

}
