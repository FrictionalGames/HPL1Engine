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
#ifndef HPL_PHYSICS_WORLD_NEWTON_H
#define HPL_PHYSICS_WORLD_NEWTON_H

#include "physics/PhysicsWorld.h"

#if defined(__linux__) || defined(__APPLE__)
#include <unistd.h>
#endif
#include <Newton.h>

namespace hpl {
	class cPhysicsWorldNewton : public iPhysicsWorld
	{
	public:
		cPhysicsWorldNewton();
		virtual ~cPhysicsWorldNewton();

		void Simulate(float afTimeStep);

		void  SetMaxTimeStep(float afTimeStep);
		float GetMaxTimeStep();

		void SetWorldSize(const cVector3f &avMin,const cVector3f &avMax);
		cVector3f GetWorldSizeMin();
		cVector3f GetWorldSizeMax();

		void SetGravity(const cVector3f& avGravity);
		cVector3f GetGravity();

		void SetAccuracyLevel(ePhysicsAccuracy aAccuracy);
		ePhysicsAccuracy GetAccuracyLevel();

		iCollideShape* CreateNullShape();
		iCollideShape* CreateBoxShape(const cVector3f &avSize, cMatrixf* apOffsetMtx);
		iCollideShape* CreateSphereShape(const cVector3f &avRadii, cMatrixf* apOffsetMtx);
		iCollideShape* CreateCylinderShape(float afRadius, float afHeight, cMatrixf* apOffsetMtx);
		iCollideShape* CreateCapsuleShape(float afRadius, float afHeight, cMatrixf* apOffsetMtx);
		iCollideShape* CreateMeshShape(iVertexBuffer *apVtxBuffer);
		iCollideShape* CreateCompundShape(tCollideShapeVec &avShapes);

		iPhysicsJointBall* CreateJointBall(const tString &asName,const cVector3f& avPivotPoint,
												iPhysicsBody* apParentBody, iPhysicsBody *apChildBody);
		iPhysicsJointHinge* CreateJointHinge(const tString &asName,const cVector3f& avPivotPoint,
												const cVector3f& avPinDir,
												iPhysicsBody* apParentBody, iPhysicsBody *apChildBody);
		iPhysicsJointSlider* CreateJointSlider(const tString &asName,const cVector3f& avPivotPoint,
												const cVector3f& avPinDir,
												iPhysicsBody* apParentBody, iPhysicsBody *apChildBody);
		iPhysicsJointScrew* CreateJointScrew(const tString &asName,const cVector3f& avPivotPoint,
												const cVector3f& avPinDir,
												iPhysicsBody* apParentBody, iPhysicsBody *apChildBody);

		iPhysicsBody* CreateBody(const tString &asName,iCollideShape *apShape);

		iCharacterBody *CreateCharacterBody(const tString &asName, const cVector3f &avSize);

		iPhysicsMaterial* CreateMaterial(const tString &asName);

		iPhysicsController *CreateController(const tString &asName);

		void CastRay(iPhysicsRayCallback *apCallback,
							const cVector3f &avOrigin, const cVector3f& avEnd,
							bool abCalcDist, bool abCalcNormal, bool abCalcPoint,
							bool abUsePrefilter = false);

		bool CheckShapeCollision(	iCollideShape* apShapeA, const cMatrixf& a_mtxA,
						iCollideShape* apShapeB, const cMatrixf& a_mtxB,
						cCollideData & aCollideData, int alMaxPoints=4);

		void RenderDebugGeometry(iLowLevelGraphics *apLowLevel, const cColor& aColor);

		const NewtonWorld* GetNewtonWorld(){ return mpNewtonWorld;}
	private:
		const NewtonWorld *mpNewtonWorld;

		float* mpTempPoints;
		float* mpTempNormals;
		float* mpTempDepths;

		cVector3f mvWorldSizeMin;
		cVector3f mvWorldSizeMax;
		cVector3f mvGravity;
		float mfMaxTimeStep;

		ePhysicsAccuracy mAccuracy;
	};
};
#endif // HPL_PHYSICS_WORLD_NEWTON_H
