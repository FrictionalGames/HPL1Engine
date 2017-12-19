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
#include "impl/PhysicsJointBallNewton.h"

#include "impl/PhysicsBodyNewton.h"
#include "impl/PhysicsWorldNewton.h"

namespace hpl {

	//////////////////////////////////////////////////////////////////////////
	// CONSTRUCTORS
	//////////////////////////////////////////////////////////////////////////

	//-----------------------------------------------------------------------

	cPhysicsJointBallNewton::cPhysicsJointBallNewton(const tString &asName,
						iPhysicsBody *apParentBody, iPhysicsBody *apChildBody,
						iPhysicsWorld *apWorld, const cVector3f &avPivotPoint)
	: iPhysicsJointNewton<iPhysicsJointBall>(asName,apParentBody,apChildBody,apWorld,avPivotPoint)
	{
		mpNewtonJoint = NewtonConstraintCreateBall(mpNewtonWorld,avPivotPoint.v,
												mpNewtonChildBody, mpNewtonParentBody);

		mvPinDir = cVector3f(0,0,0);
		mvPivotPoint = avPivotPoint;

		mfMaxConeAngle =0;
		mfMaxTwistAngle = 0;
		mvConePin = mvPinDir;
	}

	//-----------------------------------------------------------------------

	cPhysicsJointBallNewton::~cPhysicsJointBallNewton()
	{
	}

	//-----------------------------------------------------------------------

	//////////////////////////////////////////////////////////////////////////
	// PUBLIC METHODS
	//////////////////////////////////////////////////////////////////////////

	//-----------------------------------------------------------------------

	void cPhysicsJointBallNewton::SetConeLimits(const cVector3f& avPin, float afMaxConeAngle, float afMaxTwistAngle)
	{
		NewtonBallSetConeLimits(mpNewtonJoint, avPin.v,afMaxConeAngle,afMaxTwistAngle);
		mvConePin = avPin;
		mvPinDir = mvConePin;
		mfMaxConeAngle = afMaxConeAngle;
		mfMaxTwistAngle = afMaxTwistAngle;
	}

	cVector3f cPhysicsJointBallNewton::GetAngles()
	{
		cVector3f vAngles;
		NewtonBallGetJointAngle(mpNewtonJoint,&vAngles.v[0]);
		return vAngles;
	}

	//-----------------------------------------------------------------------

	cVector3f cPhysicsJointBallNewton::GetVelocity()
	{
		return cVector3f(0,0,0);
	}
	cVector3f cPhysicsJointBallNewton::GetAngularVelocity()
	{
		cVector3f vVel;
		NewtonBallGetJointOmega(mpNewtonJoint,&vVel.v[0]);
		return vVel;
	}
	cVector3f cPhysicsJointBallNewton::GetForce()
	{
		cVector3f vForce;
		NewtonBallGetJointForce(mpNewtonJoint,&vForce.v[0]);
		return vForce;
	}

	//-----------------------------------------------------------------------

	float cPhysicsJointBallNewton::GetDistance()
	{
		return 0;
	}
	float cPhysicsJointBallNewton::GetAngle()
	{
		return 0;
	}

	//-----------------------------------------------------------------------

}
