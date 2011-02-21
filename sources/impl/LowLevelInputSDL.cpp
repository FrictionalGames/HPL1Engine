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
#include "impl/LowLevelInputSDL.h"

#include "impl/MouseSDL.h"
#include "impl/KeyboardSDL.h"

#include "system/LowLevelSystem.h"

namespace hpl {

	//////////////////////////////////////////////////////////////////////////
	// CONSTRUCTORS
	//////////////////////////////////////////////////////////////////////////

	//-----------------------------------------------------------------------

	cLowLevelInputSDL::cLowLevelInputSDL(iLowLevelGraphics *apLowLevelGraphics)
	{
		mpLowLevelGraphics = apLowLevelGraphics;
		LockInput(false);
	}

	//-----------------------------------------------------------------------

	cLowLevelInputSDL::~cLowLevelInputSDL()
	{
	}

	//-----------------------------------------------------------------------

	//////////////////////////////////////////////////////////////////////////
	// PUBLIC METHOD
	//////////////////////////////////////////////////////////////////////////

	//-----------------------------------------------------------------------

	void cLowLevelInputSDL::LockInput(bool abX)
	{
		SDL_WM_GrabInput(abX ? SDL_GRAB_ON : SDL_GRAB_OFF);
	}

	//-----------------------------------------------------------------------

	void cLowLevelInputSDL::BeginInputUpdate()
	{
		//SDL_PumpEvents();

		SDL_Event sdlEvent;

		while(SDL_PollEvent(&sdlEvent)!=0)
		{
			mlstEvents.push_back(sdlEvent);
		}
	}

	//-----------------------------------------------------------------------

	void cLowLevelInputSDL::EndInputUpdate()
	{
		mlstEvents.clear();
	}

	//-----------------------------------------------------------------------

	iMouse* cLowLevelInputSDL::CreateMouse()
	{
		return hplNew( cMouseSDL,(this,mpLowLevelGraphics));
	}

	//-----------------------------------------------------------------------

	iKeyboard* cLowLevelInputSDL::CreateKeyboard()
	{
		return hplNew( cKeyboardSDL,(this) );
	}

	//-----------------------------------------------------------------------

}
