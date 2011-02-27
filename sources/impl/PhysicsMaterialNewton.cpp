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
#include "impl/PhysicsMaterialNewton.h"

#include "impl/PhysicsWorldNewton.h"
#include "impl/PhysicsBodyNewton.h"
#include "physics/SurfaceData.h"

#include "system/LowLevelSystem.h"
#include <algorithm>

namespace hpl {

	//////////////////////////////////////////////////////////////////////////
	// CONSTRUCTORS
	//////////////////////////////////////////////////////////////////////////

	//-----------------------------------------------------------------------

	cPhysicsMaterialNewton::cPhysicsMaterialNewton(const tString &asName, iPhysicsWorld *apWorld,int alMatId)
		: iPhysicsMaterial(asName,apWorld)
	{
		cPhysicsWorldNewton *pNWorld = static_cast<cPhysicsWorldNewton*>(mpWorld);

		mpNewtonWorld = pNWorld->GetNewtonWorld();

		if(alMatId==-1)
		{
			mlMaterialId = NewtonMaterialCreateGroupID(mpNewtonWorld);
		}
		else
		{
			mlMaterialId = alMatId;
		}

		//Setup default properties
		mFrictionMode = ePhysicsMaterialCombMode_Average;
		mElasticityMode  = ePhysicsMaterialCombMode_Average;

		mfElasticity = 0.5f;
		mfStaticFriction = 0.3f;
		mfKineticFriction = 0.3f;

		//Log(" Created physics material '%s' with Newton id %d\n",asName.c_str(),mlMaterialId);
	}

	//-----------------------------------------------------------------------

	cPhysicsMaterialNewton::~cPhysicsMaterialNewton()
	{
		/*Might be just as well to let newton handle this*/
	}

	//-----------------------------------------------------------------------

	//////////////////////////////////////////////////////////////////////////
	// PUBLIC METHODS
	//////////////////////////////////////////////////////////////////////////

	//-----------------------------------------------------------------------

	void cPhysicsMaterialNewton::SetElasticity(float afElasticity)
	{
		mfElasticity = afElasticity;

		UpdateMaterials();
	}

	float cPhysicsMaterialNewton::GetElasticity() const
	{
		return mfElasticity;
	}

	//-----------------------------------------------------------------------

	void cPhysicsMaterialNewton::SetStaticFriction(float afElasticity)
	{
		mfStaticFriction = afElasticity;

		UpdateMaterials();
	}

	float cPhysicsMaterialNewton::GetStaticFriction() const
	{
		return mfStaticFriction;
	}

	//-----------------------------------------------------------------------

	void cPhysicsMaterialNewton::SetKineticFriction(float afElasticity)
	{
		mfKineticFriction = afElasticity;

		UpdateMaterials();
	}

	float cPhysicsMaterialNewton::GetKineticFriction() const
	{
		return mfKineticFriction;
	}

	//-----------------------------------------------------------------------

	void cPhysicsMaterialNewton::SetFrictionCombMode(ePhysicsMaterialCombMode aMode)
	{
		mFrictionMode = aMode;

		UpdateMaterials();
	}

	ePhysicsMaterialCombMode cPhysicsMaterialNewton::GetFrictionCombMode() const
	{
		return mFrictionMode;
	}

	//-----------------------------------------------------------------------

	void cPhysicsMaterialNewton::SetElasticityCombMode(ePhysicsMaterialCombMode aMode)
	{
		mElasticityMode = aMode;

		UpdateMaterials();
	}

	//-----------------------------------------------------------------------

	ePhysicsMaterialCombMode cPhysicsMaterialNewton::GetElasticityCombMode() const
	{
		return mElasticityMode;
	}

	//-----------------------------------------------------------------------

	//////////////////////////////////////////////////////////////////////////
	// PRIVATE METHODS
	//////////////////////////////////////////////////////////////////////////

	//-----------------------------------------------------------------------

	void cPhysicsMaterialNewton::UpdateMaterials()
	{
		cPhysicsMaterialIterator MatIt = mpWorld->GetMaterialIterator();

		while(MatIt.HasNext())
		{
			cPhysicsMaterialNewton* pMat = static_cast<cPhysicsMaterialNewton*>(MatIt.Next());

			ePhysicsMaterialCombMode frictionMode =   (ePhysicsMaterialCombMode) std::max(mFrictionMode,
				pMat->mFrictionMode);
			ePhysicsMaterialCombMode elasticityMode = (ePhysicsMaterialCombMode) std::max(mElasticityMode,
				pMat->mElasticityMode);

			//If the material is the same do not blend.
			if(pMat == this){
				frictionMode = 	ePhysicsMaterialCombMode_Average;
				elasticityMode = ePhysicsMaterialCombMode_Average;
			}


			NewtonMaterialSetDefaultElasticity(mpNewtonWorld,mlMaterialId,pMat->mlMaterialId,
				Combine(elasticityMode,mfElasticity, pMat->mfElasticity));

			NewtonMaterialSetDefaultFriction(mpNewtonWorld,mlMaterialId,pMat->mlMaterialId,
				Combine(frictionMode,mfStaticFriction, pMat->mfStaticFriction),
				Combine(frictionMode,mfKineticFriction, pMat->mfKineticFriction));

			NewtonMaterialSetContinuousCollisionMode(mpNewtonWorld,mlMaterialId,pMat->mlMaterialId,
													1);

			NewtonMaterialSetCollisionCallback(mpNewtonWorld,mlMaterialId,pMat->mlMaterialId,
				(void*)NULL,BeginContactCallback,ProcessContactCallback);
		}
	}

	//-----------------------------------------------------------------------

	float cPhysicsMaterialNewton::Combine(ePhysicsMaterialCombMode aMode, float afX, float afY)
	{
		switch(aMode)
		{
		case ePhysicsMaterialCombMode_Average: return (afX + afY)/2;
		case ePhysicsMaterialCombMode_Min: return std::min(afX, afY);
		case ePhysicsMaterialCombMode_Max: return std::max(afX, afY);
		case ePhysicsMaterialCombMode_Multiply: return afX * afY;
		}

		return (afX + afY) /2;
	}

	//////////////////////////////////////////////////////////////////////////
	// STATIC NEWTON CALLBACKS
	//////////////////////////////////////////////////////////////////////////

	//-----------------------------------------------------------------------
	int cPhysicsMaterialNewton::BeginContactCallback(const NewtonMaterial* material,
									const NewtonBody* apBody1, const NewtonBody* apBody2,
									int alThreadIndex)
	{
		iPhysicsBody* pContactBody1 = (cPhysicsBodyNewton*) NewtonBodyGetUserData(apBody1);
		iPhysicsBody* pContactBody2 = (cPhysicsBodyNewton*) NewtonBodyGetUserData(apBody2);

        if(pContactBody1->GetCollide()==false) return 0;
		if(pContactBody2->GetCollide()==false) return 0;

		if(pContactBody1->IsActive()==false) return 0;
		if(pContactBody2->IsActive()==false) return 0;

		if(pContactBody1->IsRagDoll() && pContactBody2->GetCollideRagDoll()==false) return 0;
		if(pContactBody2->IsRagDoll() && pContactBody1->GetCollideRagDoll()==false) return 0;

		if(pContactBody1->IsCharacter() && pContactBody2->GetCollideCharacter()==false) return 0;
		if(pContactBody2->IsCharacter() && pContactBody1->GetCollideCharacter()==false) return 0;

		//Log("----- Begin contact between body '%s' and '%s'.\n",mpContactBody1->GetName().c_str(),
		//													mpContactBody2->GetName().c_str());

		if(pContactBody1->OnBeginCollision(pContactBody2) == false) return 0;
		if(pContactBody2->OnBeginCollision(pContactBody1) == false) return 0;

		return 1;
	}

	//-----------------------------------------------------------------------

	void cPhysicsMaterialNewton::ProcessContactCallback(const NewtonJoint* apJoint,
													   float afTimeStep,
													   int alThreadIndex)
	{
		cPhysicsContactData ContactData;
		int lContactNum = 0;
		iPhysicsBody * pContactBody1 = (iPhysicsBody*) NewtonBodyGetUserData(NewtonJointGetBody0(apJoint));
		iPhysicsBody * pContactBody2 = (iPhysicsBody*) NewtonBodyGetUserData(NewtonJointGetBody1(apJoint));

		for (void* pContact = NewtonContactJointGetFirstContact(apJoint);
			 pContact;
			 pContact = NewtonContactJointGetNextContact(apJoint, pContact))
		{
			NewtonMaterial* pMaterial = NewtonContactGetMaterial(pContact);
			//Log(" Process contact between body '%s' and '%s'.\n",pContactBody1->GetName().c_str(),
			//													pContactBody2->GetName().c_str());

			//Normal speed
			float fNormSpeed = NewtonMaterialGetContactNormalSpeed(pMaterial);
			if (ContactData.mfMaxContactNormalSpeed < fNormSpeed)
				ContactData.mfMaxContactNormalSpeed = fNormSpeed;

			//Tangent speed
			float fTanSpeed0 = NewtonMaterialGetContactTangentSpeed(pMaterial, 0);
			float fTanSpeed1 = NewtonMaterialGetContactTangentSpeed(pMaterial, 1);
			if(std::abs(ContactData.mfMaxContactTangentSpeed) < std::abs(fTanSpeed0))
				ContactData.mfMaxContactTangentSpeed = fTanSpeed0;
			if(std::abs(ContactData.mfMaxContactTangentSpeed) < std::abs(fTanSpeed1))
				ContactData.mfMaxContactTangentSpeed = fTanSpeed1;

			//Force
			cVector3f vForce;
			NewtonMaterialGetContactForce(pMaterial,vForce.v);
			ContactData.mvForce += vForce;

			//Position and normal
			cVector3f vPos, vNormal;
			NewtonMaterialGetContactPositionAndNormal(pMaterial,vPos.v, vNormal.v);

			ContactData.mvContactNormal += vNormal;
			ContactData.mvContactPosition += vPos;

			//Log(" Norm: %f Tan0: %f Tan1: %f\n",fNormSpeed, fTanSpeed0, fTanSpeed1);
			//Log("Force: %s step %f\n", vForce.ToString().c_str(), afTimestep);

			if(pContactBody1->GetWorld()->GetSaveContactPoints())
			{
				cCollidePoint collidePoint;
				collidePoint.mfDepth = 1;
				NewtonMaterialGetContactPositionAndNormal (pMaterial, collidePoint.mvPoint.v,
														   collidePoint.mvNormal.v);

				pContactBody1->GetWorld()->GetContactPoints()->push_back(collidePoint);
			}

			lContactNum++;
		}

		//Log("--- End contact between body '%s' and '%s'.\n",mpContactBody1->GetName().c_str(),
		//													mpContactBody2->GetName().c_str());

		if(lContactNum <= 0) return;

		iPhysicsMaterial *pMaterial1 = pContactBody1->GetMaterial();
		iPhysicsMaterial *pMaterial2 = pContactBody2->GetMaterial();

		ContactData.mvContactNormal = ContactData.mvContactNormal / (float)lContactNum;
		ContactData.mvContactPosition = ContactData.mvContactPosition / (float)lContactNum;

		pMaterial1->GetSurfaceData()->CreateImpactEffect(ContactData.mfMaxContactNormalSpeed,
													ContactData.mvContactPosition,
													lContactNum,pMaterial2->GetSurfaceData());

		int lPrio1 = pMaterial1->GetSurfaceData()->GetPriority();
		int lPrio2 = pMaterial2->GetSurfaceData()->GetPriority();

		if(lPrio1 >= lPrio2)
		{
			if(std::abs(ContactData.mfMaxContactNormalSpeed) > 0)
				pMaterial1->GetSurfaceData()->OnImpact(ContactData.mfMaxContactNormalSpeed,
														ContactData.mvContactPosition,
														lContactNum,pContactBody1);
			if(std::abs(ContactData.mfMaxContactTangentSpeed) > 0)
				pMaterial1->GetSurfaceData()->OnSlide(ContactData.mfMaxContactTangentSpeed,
														ContactData.mvContactPosition,
														lContactNum,pContactBody1,pContactBody2);
		}

		if(lPrio2 >= lPrio1 && pMaterial2 != pMaterial1)
		{
			if(std::abs(ContactData.mfMaxContactNormalSpeed) > 0)
				pMaterial2->GetSurfaceData()->OnImpact(ContactData.mfMaxContactNormalSpeed,
														ContactData.mvContactPosition,
														lContactNum,pContactBody2);
			if(std::abs(ContactData.mfMaxContactTangentSpeed) > 0)
				pMaterial2->GetSurfaceData()->OnSlide(ContactData.mfMaxContactTangentSpeed,
														ContactData.mvContactPosition,
														lContactNum,pContactBody2,pContactBody1);
		}

		pContactBody1->OnCollide(pContactBody2,&ContactData);
		pContactBody2->OnCollide(pContactBody1,&ContactData);
	}

	//-----------------------------------------------------------------------

}
