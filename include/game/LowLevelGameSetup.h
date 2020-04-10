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
#ifndef HPL_LOWLEVELGAMESETUP_H
#define HPL_LOWLEVELGAMESETUP_H

#include "system/System.h"
#include "input/Input.h"
#include "graphics/Graphics.h"
#include "resources/Resources.h"
#include "scene/Scene.h"
#include "sound/Sound.h"
#include "physics/Physics.h"
#include "ai/AI.h"
#include "haptic/Haptic.h"

namespace hpl {

	class iLowLevelGameSetup
	{
	public:
		virtual ~iLowLevelGameSetup(){}

		virtual cInput* CreateInput(cGraphics* apGraphics)=0;
		virtual cSystem* CreateSystem()=0;
		virtual cGraphics* CreateGraphics()=0;
		virtual cResources* CreateResources(cGraphics* apGraphics, tWString assetsPath)=0;
		virtual cScene* CreateScene(cGraphics* apGraphics, cResources* apResources, cSound* apSound,
									cPhysics *apPhysics, cSystem *apSystem,cAI *apAI,cHaptic *apHaptic)=0;
		virtual cSound* CreateSound()=0;
		virtual cPhysics* CreatePhysics()=0;
		virtual cAI* CreateAI()=0;
		virtual cHaptic* CreateHaptic()=0;
	};
};
#endif // HPL_LOWLEVELGAMESETUP_H
