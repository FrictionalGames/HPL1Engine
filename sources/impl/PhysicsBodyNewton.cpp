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
#include "impl/PhysicsBodyNewton.h"

#include "impl/CollideShapeNewton.h"
#include "impl/PhysicsWorldNewton.h"
#include "impl/PhysicsMaterialNewton.h"
#include "system/LowLevelSystem.h"
#include "graphics/LowLevelGraphics.h"
#include "math/Math.h"
#include "scene/Node3D.h"

namespace hpl {

	bool cPhysicsBodyNewton::mbUseCallback = true;

	//////////////////////////////////////////////////////////////////////////
	// CONSTRUCTORS
	//////////////////////////////////////////////////////////////////////////

	//-----------------------------------------------------------------------

	cPhysicsBodyNewton::cPhysicsBodyNewton(const tString &asName,iPhysicsWorld *apWorld,iCollideShape *apShape)
	: iPhysicsBody(asName,apWorld, apShape)
	{
		cPhysicsWorldNewton *pWorldNewton = static_cast<cPhysicsWorldNewton*>(apWorld);
		cCollideShapeNewton *pShapeNewton = static_cast<cCollideShapeNewton*>(apShape);

		mpNewtonWorld = pWorldNewton->GetNewtonWorld();
		mpNewtonBody = NewtonCreateBody(pWorldNewton->GetNewtonWorld(),
										pShapeNewton->GetNewtonCollision(),
										cMatrixf::Identity.m[0]);

		mpCallback = hplNew( cPhysicsBodyNewtonCallback, () );

		AddCallback(mpCallback);

		// Setup the callbacks and set this body as user data
		// This is so that the transform gets updated and
		// to add gravity, forces and user sink.
		NewtonBodySetForceAndTorqueCallback(mpNewtonBody,OnUpdateCallback);
		NewtonBodySetTransformCallback(mpNewtonBody, OnTransformCallback);
		NewtonBodySetUserData(mpNewtonBody, this);

		//Set default property settings
		mbGravity = true;

		mfMaxLinearSpeed =0;
		mfMaxAngularSpeed =0;
		mfMass =0;

		mfAutoDisableLinearThreshold = 0.01f;
		mfAutoDisableAngularThreshold = 0.01f;
		mlAutoDisableNumSteps = 10;

		//Log("Creating newton body '%s' %d\n",msName.c_str(),(size_t)this);
	}

	//-----------------------------------------------------------------------

	cPhysicsBodyNewton::~cPhysicsBodyNewton()
	{
		//Log(" Destroying newton body '%s' %d\n",msName.c_str(),(size_t)this);
	}

	//-----------------------------------------------------------------------

	void cPhysicsBodyNewton::DeleteLowLevel()
	{
		//Log(" Newton body %d\n", (size_t)mpNewtonBody);
		NewtonDestroyBody(mpNewtonWorld,mpNewtonBody);
		//Log(" Callback\n");
		hplDelete(mpCallback);
	}

	//-----------------------------------------------------------------------

	//////////////////////////////////////////////////////////////////////////
	// CALLBACK METHODS
	//////////////////////////////////////////////////////////////////////////

	//-----------------------------------------------------------------------

    void cPhysicsBodyNewtonCallback::OnTransformUpdate(iEntity3D * apEntity)
	{
		if(cPhysicsBodyNewton::mbUseCallback==false) return;

		cPhysicsBodyNewton *pRigidBody = static_cast<cPhysicsBodyNewton*>(apEntity);
		NewtonBodySetMatrix(pRigidBody->mpNewtonBody,
							&apEntity->GetLocalMatrix().GetTranspose().m[0][0]);
		// Log("OnTransformUpdate '%s' -- %s\n", apEntity->GetName().c_str(),
		// 	apEntity->GetLocalMatrix().GetTranspose().ToString().c_str());


		if(pRigidBody->mpNode) pRigidBody->mpNode->SetMatrix(apEntity->GetLocalMatrix());
	}

	//-----------------------------------------------------------------------

	//////////////////////////////////////////////////////////////////////////
	// PUBLIC METHODS
	//////////////////////////////////////////////////////////////////////////

	//-----------------------------------------------------------------------

	void cPhysicsBodyNewton::SetMaterial(iPhysicsMaterial* apMaterial)
	{
		mpMaterial = apMaterial;

		if(apMaterial == NULL) return;

		cPhysicsMaterialNewton* pNewtonMat = static_cast<cPhysicsMaterialNewton*>(mpMaterial);

		NewtonBodySetMaterialGroupID(mpNewtonBody, pNewtonMat->GetId());
	}

	//-----------------------------------------------------------------------

	void cPhysicsBodyNewton::SetLinearVelocity(const cVector3f &avVel)
	{
		NewtonBodySetVelocity(mpNewtonBody, avVel.v);
	}
	cVector3f cPhysicsBodyNewton::GetLinearVelocity() const
	{
		cVector3f vVel;
		NewtonBodyGetVelocity(mpNewtonBody, vVel.v);
		return vVel;
	}

	//-----------------------------------------------------------------------

	void cPhysicsBodyNewton::SetAngularVelocity(const cVector3f &avVel)
	{
		NewtonBodySetOmega(mpNewtonBody, avVel.v);
	}
	cVector3f cPhysicsBodyNewton::GetAngularVelocity() const
	{
		cVector3f vVel;
		NewtonBodyGetOmega(mpNewtonBody, vVel.v);
		return vVel;
	}

	//-----------------------------------------------------------------------

	void cPhysicsBodyNewton::SetLinearDamping(float afDamping)
	{
		NewtonBodySetLinearDamping(mpNewtonBody,afDamping);
	}
	float cPhysicsBodyNewton::GetLinearDamping() const
	{
		return NewtonBodyGetLinearDamping(mpNewtonBody);
	}

	//-----------------------------------------------------------------------

	void cPhysicsBodyNewton::SetAngularDamping(float afDamping)
	{
		float fDamp[3] = {afDamping,afDamping,afDamping};
		NewtonBodySetAngularDamping(mpNewtonBody,fDamp);
	}
	float cPhysicsBodyNewton::GetAngularDamping() const
	{
		float fDamp[3];
		NewtonBodyGetAngularDamping(mpNewtonBody,fDamp);
		return fDamp[0];
	}

	//-----------------------------------------------------------------------

	void cPhysicsBodyNewton::SetMaxLinearSpeed(float afSpeed)
	{
		mfMaxLinearSpeed = afSpeed;
	}
	float cPhysicsBodyNewton::GetMaxLinearSpeed() const
	{
		return mfMaxLinearSpeed;
	}

	//-----------------------------------------------------------------------

	void cPhysicsBodyNewton::SetMaxAngularSpeed(float afDamping)
	{
		mfMaxAngularSpeed = afDamping;
	}
	float cPhysicsBodyNewton::GetMaxAngularSpeed() const
	{
		return mfMaxAngularSpeed;
	}

	//-----------------------------------------------------------------------

	cMatrixf cPhysicsBodyNewton::GetInertiaMatrix()
	{
		float fIxx, fIyy, fIzz, fMass;

		NewtonBodyGetMassMatrix(mpNewtonBody,&fMass, &fIxx, &fIyy, &fIzz);

        cMatrixf mtxRot = GetLocalMatrix().GetRotation();
		cMatrixf mtxTransRot = mtxRot.GetTranspose();
		cMatrixf mtxI(	fIxx,0,	  0,	0,
						0,	 fIyy,0,	0,
						0,	 0,	  fIzz,	0,
						0,	 0,	  0,	1);

		return cMath::MatrixMul(cMath::MatrixMul(mtxRot,mtxI), mtxTransRot);
	}

	//-----------------------------------------------------------------------

	void  cPhysicsBodyNewton::SetMass(float afMass)
	{
		cCollideShapeNewton *pShapeNewton = static_cast<cCollideShapeNewton*>(mpShape);

		cVector3f vInertia;// = pShapeNewton->GetInertia(afMass);
		cVector3f vOffset;

		NewtonConvexCollisionCalculateInertialMatrix(pShapeNewton->GetNewtonCollision(),
													vInertia.v, vOffset.v);
		vInertia = vInertia * afMass;

		NewtonBodySetCentreOfMass(mpNewtonBody,vOffset.v);

		NewtonBodySetMassMatrix(mpNewtonBody, afMass, vInertia.x, vInertia.y, vInertia.z);
		mfMass = afMass;
	}
	float cPhysicsBodyNewton::GetMass() const
	{
		return mfMass;
	}

	void  cPhysicsBodyNewton::SetMassCentre(const cVector3f& avCentre)
	{
		NewtonBodySetCentreOfMass(mpNewtonBody,avCentre.v);
	}

	cVector3f cPhysicsBodyNewton::GetMassCentre() const
	{
		cVector3f vCentre;
		NewtonBodyGetCentreOfMass(mpNewtonBody,vCentre.v);
		return vCentre;
	}

	//-----------------------------------------------------------------------

	void cPhysicsBodyNewton::AddForce(const cVector3f &avForce)
	{
		mvTotalForce += avForce;
		SetEnabled(true);

		//Log("Added force %s\n",avForce.ToString().c_str());
	}

	void cPhysicsBodyNewton::AddForceAtPosition(const cVector3f &avForce, const cVector3f &avPos)
	{
		mvTotalForce += avForce;

		cVector3f vLocalPos = avPos - GetLocalPosition();
		cVector3f vMassCentre = GetMassCentre();
		if(vMassCentre != cVector3f(0,0,0))
		{
			vMassCentre = cMath::MatrixMul(GetLocalMatrix().GetRotation(),vMassCentre);
			vLocalPos -= vMassCentre;
		}

		cVector3f vTorque = cMath::Vector3Cross(vLocalPos, avForce);

		mvTotalTorque += vTorque;
		SetEnabled(true);

		//Log("Added force %s\n",avForce.ToString().c_str());
	}

	//-----------------------------------------------------------------------

	void cPhysicsBodyNewton::AddTorque(const cVector3f &avTorque)
	{
		mvTotalTorque += avTorque;
		SetEnabled(true);
	}

	//-----------------------------------------------------------------------

	void cPhysicsBodyNewton::AddImpulse(const cVector3f &avImpulse)
	{
		cVector3f vMassCentre = GetMassCentre();
		if(vMassCentre != cVector3f(0,0,0))
		{
			cVector3f vCentreOffset = cMath::MatrixMul( GetWorldMatrix().GetRotation(),
														vMassCentre);

			cVector3f vWorldPosition = GetWorldPosition() + vCentreOffset;
			NewtonBodyAddImpulse(mpNewtonBody, avImpulse.v, vWorldPosition.v);
		}
		else
		{
			NewtonBodyAddImpulse(mpNewtonBody, avImpulse.v, GetWorldPosition().v);
		}
	}
	void cPhysicsBodyNewton::AddImpulseAtPosition(const cVector3f &avImpulse, const cVector3f &avPos)
	{
		NewtonBodyAddImpulse(mpNewtonBody, avImpulse.v, avPos.v);
	}

	//-----------------------------------------------------------------------

	void cPhysicsBodyNewton::SetEnabled(bool abEnabled)
	{
		NewtonBodySetFreezeState(mpNewtonBody, abEnabled);
	}
	bool cPhysicsBodyNewton::GetEnabled() const
	{
		return NewtonBodyGetSleepState(mpNewtonBody) ==0?false: true;
	}

	//-----------------------------------------------------------------------

	void cPhysicsBodyNewton::SetAutoDisable(bool abEnabled)
	{
		NewtonBodySetAutoSleep(mpNewtonBody, abEnabled ? 1 : 0);
	}
	bool cPhysicsBodyNewton::GetAutoDisable() const
	{
		return NewtonBodyGetAutoSleep(mpNewtonBody) == 0 ? false : true;
	}

	//-----------------------------------------------------------------------

	void cPhysicsBodyNewton::SetAutoDisableLinearThreshold(float afThresold)
	{
		mfAutoDisableLinearThreshold = afThresold;
		Warning("NewtonBodySetFreezeTreshold not implemented!\n");
		/*NewtonBodySetFreezeTreshold(mpNewtonBody, mfAutoDisableLinearThreshold,
		  mfAutoDisableAngularThreshold, mlAutoDisableNumSteps);*/
	}
	float cPhysicsBodyNewton::GetAutoDisableLinearThreshold() const
	{
		return mfAutoDisableLinearThreshold;
	}

	//-----------------------------------------------------------------------

	void cPhysicsBodyNewton::SetAutoDisableAngularThreshold(float afThresold)
	{
		mfAutoDisableAngularThreshold = afThresold;
		Warning("NewtonBodySetFreezeTreshold not implemented!\n");
		/*NewtonBodySetFreezeTreshold(mpNewtonBody, mfAutoDisableLinearThreshold,
		  mfAutoDisableAngularThreshold, mlAutoDisableNumSteps);*/
	}
	float cPhysicsBodyNewton::GetAutoDisableAngularThreshold() const
	{
		return mfAutoDisableAngularThreshold;
	}

	//-----------------------------------------------------------------------

	void cPhysicsBodyNewton::SetAutoDisableNumSteps(int anNum)
	{
		mlAutoDisableNumSteps = anNum;
		Warning("NewtonBodySetFreezeTreshold not implemented!\n");
		/*NewtonBodySetFreezeTreshold(mpNewtonBody, mfAutoDisableLinearThreshold,
		  mfAutoDisableAngularThreshold, mlAutoDisableNumSteps);*/
	}

	int cPhysicsBodyNewton::GetAutoDisableNumSteps() const
	{
		return mlAutoDisableNumSteps;
	}

	//-----------------------------------------------------------------------

	void cPhysicsBodyNewton::SetContinuousCollision(bool abOn)
	{
		NewtonBodySetContinuousCollisionMode(mpNewtonBody,abOn ? 1 : 0);
	}

	bool cPhysicsBodyNewton::GetContinuousCollision()
	{
		return NewtonBodyGetContinuousCollisionMode(mpNewtonBody)==1 ? true : false;
	}

	//-----------------------------------------------------------------------

	void cPhysicsBodyNewton::SetGravity(bool abEnabled)
	{
		mbGravity = abEnabled;
	}
	bool cPhysicsBodyNewton::GetGravity() const
	{
		return mbGravity;
	}

	//-----------------------------------------------------------------------


	static iLowLevelGraphics *gpLowLevelGraphics;
	static cColor gDebugColor;

	////////////////////////////////////////////

	static void RenderDebugPolygon(void* apVoid,
		int alVertexCount, const dFloat* apFaceVertex, int alId)
	{
		int i;

		i = alVertexCount - 1;

		cVector3f vP0(apFaceVertex[i * 3 + 0], apFaceVertex[i * 3 + 1], apFaceVertex[i * 3 + 2]);
		for (i = 0; i < alVertexCount; ++i)
		{
			cVector3f vP1 (apFaceVertex[i * 3 + 0], apFaceVertex[i * 3 + 1], apFaceVertex[i * 3 + 2]);

			gpLowLevelGraphics->DrawLine(vP0, vP1,gDebugColor);

			vP0 = vP1;
		}
	}

	////////////////////////////////////////////

	void cPhysicsBodyNewton::RenderDebugGeometry(iLowLevelGraphics *apLowLevel,const cColor &aColor)
	{
		gpLowLevelGraphics = apLowLevel;
		gDebugColor = aColor;
		NewtonCollision* pNewtonCollision = NewtonBodyGetCollision(mpNewtonBody);
		NewtonCollisionForEachPolygonDo(pNewtonCollision,
										m_mtxLocalTransform.GetTranspose().m[0],
										RenderDebugPolygon, this);
	}

	//-----------------------------------------------------------------------

	void cPhysicsBodyNewton::ClearForces()
	{
		mvTotalForce = cVector3f(0,0,0);
		mvTotalTorque = cVector3f(0,0,0);
	}

	//-----------------------------------------------------------------------

	//////////////////////////////////////////////////////////////////////////
	// STATIC NEWTON CALLBACKS
	//////////////////////////////////////////////////////////////////////////

	//-----------------------------------------------------------------------


	void cPhysicsBodyNewton::OnTransformCallback(const NewtonBody* apBody,
												 const dFloat* apMatrix,
												 int alThreadIndex)
	{
		cPhysicsBodyNewton* pRigidBody = (cPhysicsBodyNewton*) NewtonBodyGetUserData(apBody);

		pRigidBody->m_mtxLocalTransform.FromTranspose(apMatrix);

		mbUseCallback = false;
		pRigidBody->SetTransformUpdated(true);
		mbUseCallback = true;

		if(pRigidBody->mpNode)pRigidBody->mpNode->SetMatrix(pRigidBody->m_mtxLocalTransform);
	}

	//-----------------------------------------------------------------------

	//callback for buoyancy
	static cPlanef gSurfacePlane;
	static int BuoyancyPlaneCallback (const int alCollisionID, void *apContext,
									const float* afGlobalSpaceMatrix, float* afGlobalSpacePlane)
	{
		afGlobalSpacePlane[0] = gSurfacePlane.a;
		afGlobalSpacePlane[1] = gSurfacePlane.b;
		afGlobalSpacePlane[2] = gSurfacePlane.c;
		afGlobalSpacePlane[3] = gSurfacePlane.d;
		return 1;
	}

	void cPhysicsBodyNewton::OnUpdateCallback(const NewtonBody* apBody,
											  float afTimeStep,
											  int alThreadIndex)
	{
		float fMass;
		float fX,fY,fZ;

		cPhysicsBodyNewton* pRigidBody = (cPhysicsBodyNewton*) NewtonBodyGetUserData(apBody);

		if(pRigidBody->IsActive()==false) return;

		cVector3f vGravity = pRigidBody->mpWorld->GetGravity();

		//Create some gravity
		if (pRigidBody->mbGravity)
		{
			NewtonBodyGetMassMatrix(apBody, &fMass, &fX, &fY, &fZ);

			float fForce[3] = { fMass * vGravity.x, fMass * vGravity.y, fMass * vGravity.z};

			NewtonBodyAddForce(apBody, &fForce[0]);
		}

		// Create Buoyancy
		if (pRigidBody->mBuoyancy.mbActive)
		{
			gSurfacePlane = pRigidBody->mBuoyancy.mSurface;
			NewtonBodyAddBuoyancyForce( apBody,
										pRigidBody->mBuoyancy.mfDensity,
										pRigidBody->mBuoyancy.mfLinearViscosity,
										pRigidBody->mBuoyancy.mfAngularViscosity,
										vGravity.v, BuoyancyPlaneCallback,
										pRigidBody);
		}

		// Add forces from calls to Addforce(..), etc
		NewtonBodyAddForce(apBody, pRigidBody->mvTotalForce.v);
		NewtonBodyAddTorque(apBody, pRigidBody->mvTotalTorque.v);

		// Check so that all speeds are within thresholds
		// Linear
		if (pRigidBody->mfMaxLinearSpeed > 0)
		{
			cVector3f vVel = pRigidBody->GetLinearVelocity();
			float fSpeed = vVel.Length();
			if (fSpeed > pRigidBody->mfMaxLinearSpeed)
			{
				vVel = cMath::Vector3Normalize(vVel) * pRigidBody->mfMaxLinearSpeed;
				pRigidBody->SetLinearVelocity(vVel);
			}
		}
		// Angular
		if (pRigidBody->mfMaxAngularSpeed > 0)
		{
			cVector3f vVel = pRigidBody->GetAngularVelocity();
			float fSpeed = vVel.Length();
			if (fSpeed > pRigidBody->mfMaxAngularSpeed)
			{
				vVel = cMath::Vector3Normalize(vVel) * pRigidBody->mfMaxAngularSpeed;
				pRigidBody->SetAngularVelocity(vVel);
			}
		}

		//cVector3f vForce;
		//NewtonBodyGetForce(apBody,vForce.v);
		//Log("Engine force %s\n",pRigidBody->mvTotalForce.ToString().c_str());
		//Log("Engine force '%s': %s, body force: %s \n",
		//	pRigidBody->GetName().c_str(),
		//	pRigidBody->mvTotalForce.ToString().c_str(),
		//	vForce.ToString().c_str());
	}

	//-----------------------------------------------------------------------

}
