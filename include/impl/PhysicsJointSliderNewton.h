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
#ifndef HPL_PHYSICS_JOINT_SLIDER_NEWTON_H
#define HPL_PHYSICS_JOINT_SLIDER_NEWTON_H

#include "physics/PhysicsJointSlider.h"
#include "impl/PhysicsJointNewton.h"

namespace hpl {

	class cPhysicsJointSliderNewton : public iPhysicsJointNewton<iPhysicsJointSlider>
	{
	public:
		cPhysicsJointSliderNewton(const tString &asName, iPhysicsBody *apParentBody, iPhysicsBody *apChildBody, 
							iPhysicsWorld *apWorld,const cVector3f &avPivotPoint, const cVector3f avPinDir) ;
		virtual ~cPhysicsJointSliderNewton();
		
		void SetMaxDistance(float afX);
		void SetMinDistance(float afX);
		float GetMaxDistance();
		float GetMinDistance();

		cVector3f GetVelocity();
		cVector3f GetAngularVelocity();
		cVector3f GetForce();

		float GetDistance();
		float GetAngle();

	private:
		static unsigned LimitCallback(const NewtonJoint* pSlider, NewtonHingeSliderUpdateDesc* pDesc);
	};
};
#endif // HPL_PHYSICS_JOINT_SLIDER_NEWTON_H
