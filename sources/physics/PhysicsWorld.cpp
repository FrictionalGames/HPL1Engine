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
#include "physics/PhysicsWorld.h"

#include "physics/CollideShape.h"
#include "physics/PhysicsBody.h"
#include "physics/PhysicsMaterial.h"
#include "physics/CharacterBody.h"
#include "physics/PhysicsJoint.h"
#include "physics/PhysicsController.h"
#include "physics/SurfaceData.h"
#include "system/LowLevelSystem.h"
#include "system/System.h"
#include "math/Math.h"
#include "graphics/LowLevelGraphics.h"
#include "scene/World3D.h"
#include "scene/PortalContainer.h"

namespace hpl {

	//////////////////////////////////////////////////////////////////////////
	// CONSTRUCTORS
	//////////////////////////////////////////////////////////////////////////

	//-----------------------------------------------------------------------

	iPhysicsWorld::iPhysicsWorld()
	{
		mbLogDebug = false;
	}

	//-----------------------------------------------------------------------

	iPhysicsWorld::~iPhysicsWorld()
	{

	}

	//-----------------------------------------------------------------------

	//////////////////////////////////////////////////////////////////////////
	// PUBLIC METHODS
	//////////////////////////////////////////////////////////////////////////

	//-----------------------------------------------------------------------

	void iPhysicsWorld::Update(float afTimeStep)
	{
		//Clear all contact points.
		mvContactPoints.clear();

		////////////////////////////////////
		//Update controllers
		tPhysicsControllerListIt CtrlIt = mlstControllers.begin();
		for(; CtrlIt != mlstControllers.end(); ++CtrlIt)
		{
			iPhysicsController *pCtrl = *CtrlIt;

			pCtrl->Update(afTimeStep);
		}


		////////////////////////////////////
		//Update character bodies
		unsigned int lTime = GetApplicationTime();
		tCharacterBodyListIt CharIt = mlstCharBodies.begin();
		for(; CharIt != mlstCharBodies.end(); ++CharIt)
		{
			iCharacterBody *pBody = *CharIt;

			//for(int i=0; i<20; ++i)
			{
				pBody->Update(afTimeStep);//20.0f);
			}
		}
		//LogUpdate(" Updating chars took %d ms\n",mpWorld3D->GetSystem()->GetLowLevel()->GetTime() - lTime);


		////////////////////////////////////
		//Update the rigid bodies before simulation.
		tPhysicsBodyListIt BodyIt = mlstBodies.begin();
		for(; BodyIt != mlstBodies.end(); ++BodyIt)
		{
			iPhysicsBody *pBody = *BodyIt;

			pBody->UpdateBeforeSimulate(afTimeStep);

		}

		////////////////////////////////////
		//Simulate the physics
		lTime = GetApplicationTime();
		Simulate(afTimeStep);
		//LogUpdate(" Updating lowlevel physics took %d ms\n",mpWorld3D->GetSystem()->GetLowLevel()->GetTime() - lTime);

		////////////////////////////////////
		//Update the joints after simulation.
		tPhysicsJointListIt JointIt = mlstJoints.begin();
		for(; JointIt != mlstJoints.end(); )
		{
			iPhysicsJoint *pJoint = *JointIt;

			pJoint->OnPhysicsUpdate();

			if(pJoint->CheckBreakage())
			{
				JointIt = mlstJoints.erase(JointIt);
				hplDelete(pJoint);
			}
			else
			{
				++JointIt;
			}
		}
		////////////////////////////////////
		//Update the rigid bodies after simulation.
		BodyIt = mlstBodies.begin();
		for(; BodyIt != mlstBodies.end(); ++BodyIt)
		{
			iPhysicsBody *pBody = *BodyIt;

			pBody->UpdateAfterSimulate(afTimeStep);
		}
	}

	//-----------------------------------------------------------------------

	void iPhysicsWorld::DestroyShape(iCollideShape *apShape)
	{
		apShape->DecUserCount();
		if(apShape->HasUsers()==false)
		{
			STLFindAndDelete(mlstShapes, apShape);
		}
	}

	//-----------------------------------------------------------------------

	void iPhysicsWorld::DestroyBody(iPhysicsBody* apBody)
	{
		//STLFindAndDelete(mlstBodies, apBody);
		tPhysicsBodyListIt it = mlstBodies.begin();
		for(; it != mlstBodies.end(); ++it)
		{
			iPhysicsBody *pBody = *it;
			if(pBody == apBody)
			{
				if(mpWorld3D) mpWorld3D->GetPortalContainer()->RemoveEntity(pBody);
				pBody->Destroy();
				hplDelete(pBody);
				mlstBodies.erase(it);
				return;
			}
		}
	}

	iPhysicsBody *iPhysicsWorld::GetBody(const tString &asName)
	{
		return (iPhysicsBody*)STLFindByName(mlstBodies, asName);
	}

	cPhysicsBodyIterator iPhysicsWorld::GetBodyIterator()
	{
		return cPhysicsBodyIterator(&mlstBodies);
	}

	//-----------------------------------------------------------------------

	void iPhysicsWorld::GetBodiesInBV(cBoundingVolume *apBV, tPhysicsBodyList *apBodyList)
	{
		tPhysicsBodyListIt BodyIt = mlstBodies.begin();
		for(; BodyIt != mlstBodies.end(); ++BodyIt)
		{
			iPhysicsBody *pBody = *BodyIt;
			if(pBody->GetMass() > 0 && cMath::CheckCollisionBV(*apBV,*pBody->GetBV()))
			{
				apBodyList->push_back(pBody);
			}
		}
	}

	void iPhysicsWorld::EnableBodiesInBV(cBoundingVolume *apBV, bool abEnabled)
	{
		tPhysicsBodyListIt BodyIt = mlstBodies.begin();
		for(; BodyIt != mlstBodies.end(); ++BodyIt)
		{
			iPhysicsBody *pBody = *BodyIt;

			if(pBody->GetMass() > 0 && cMath::CheckCollisionBV(*apBV,*pBody->GetBV()))
			{
				//quick fix for oscillation, might skip
				//if(pBody->GetJointNum()>0 && pBody->GetJoint(0)->GetLimitAutoSleep()) continue;

				pBody->SetEnabled(abEnabled);
			}
		}
	}

	//-----------------------------------------------------------------------

	void iPhysicsWorld::DestroyJoint(iPhysicsJoint* apJoint)
	{
		STLFindAndDelete(mlstJoints, apJoint);
	}

	iPhysicsJoint *iPhysicsWorld::GetJoint(const tString &asName)
	{
		return (iPhysicsJoint*)STLFindByName(mlstJoints, asName);
	}

	cPhysicsJointIterator iPhysicsWorld::GetJointIterator()
	{
		return cPhysicsJointIterator(&mlstJoints);
	}

	//-----------------------------------------------------------------------

	void iPhysicsWorld::DestroyCharacterBody(iCharacterBody* apBody)
	{
		STLFindAndDelete(mlstCharBodies, apBody);
	}

	iPhysicsBody *iPhysicsWorld::GetCharacterBody(const tString &asName)
	{
		return (iPhysicsBody*)STLFindByName(mlstCharBodies, asName);
	}

	//-----------------------------------------------------------------------

	iPhysicsMaterial* iPhysicsWorld::GetMaterialFromName(const tString &asName)
	{
		tPhysicsMaterialMapIt it = m_mapMaterials.find(asName);
		if(it == m_mapMaterials.end()){
			return NULL;
		}

		iPhysicsMaterial *pMaterial =it->second;

        if(pMaterial->IsPreloaded()==false && pMaterial->GetSurfaceData())
		{
			pMaterial->SetPreloaded(true);
			pMaterial->GetSurfaceData()->PreloadData();
		}

		return pMaterial;
	}

	//-----------------------------------------------------------------------

	cPhysicsMaterialIterator iPhysicsWorld::GetMaterialIterator()
	{
		return cPhysicsMaterialIterator(&m_mapMaterials);
	}

	//-----------------------------------------------------------------------

	void iPhysicsWorld::DestroyAll()
	{
		STLDeleteAll(mlstCharBodies);

		//Bodies
		tPhysicsBodyListIt it = mlstBodies.begin();
		for(; it != mlstBodies.end(); ++it)
		{
			iPhysicsBody *pBody = *it;
			pBody->Destroy();
			hplDelete(pBody);
		}
		mlstBodies.clear();

		STLDeleteAll(mlstShapes);
		STLDeleteAll(mlstJoints);
		STLDeleteAll(mlstControllers);
		STLMapDeleteAll(m_mapMaterials);
	}

	//-----------------------------------------------------------------------

	void iPhysicsWorld::AddSaveData(cSaveDataHandler* apHandler)
	{
		////////////////////////////////
		//Add all bodies
		tPhysicsBodyListIt BodyIt = mlstBodies.begin();
		for(; BodyIt != mlstBodies.end(); ++BodyIt)
		{
			iPhysicsBody *pBody = *BodyIt;

			if(pBody->IsSaved())
			{
				iSaveData *pData =pBody->CreateSaveData();
				pBody->SaveToSaveData(pData);
				apHandler->Add(pData);
			}
		}

		////////////////////////////////
		//Add all character bodies
		tCharacterBodyListIt CharIt = mlstCharBodies.begin();
		for(; CharIt != mlstCharBodies.end(); ++CharIt)
		{
			iCharacterBody *pBody = *CharIt;

			if(pBody->IsSaved())
			{
				iSaveData *pData = pBody->CreateSaveData();
				pBody->SaveToSaveData(pData);
				apHandler->Add(pData);
			}
		}

		////////////////////////////////
		//Add all joints
		tPhysicsJointListIt JointIt = mlstJoints.begin();
		for(; JointIt != mlstJoints.end(); ++JointIt)
		{
			iPhysicsJoint *pJoint = *JointIt;

			if(pJoint->IsSaved())
			{
				iSaveData *pData =pJoint->CreateSaveData();
				pJoint->SaveToSaveData(pData);
				apHandler->Add(pData);
			}
		}
	}

	//-----------------------------------------------------------------------

	void iPhysicsWorld::DestroyController(iPhysicsController *apController)
	{
		STLFindAndDelete(mlstControllers, apController);
	}

	//-----------------------------------------------------------------------


	void iPhysicsWorld::RenderContactPoints(iLowLevelGraphics *apLowLevel, const cColor& aPointColor,
											const cColor& aLineColor)
	{
		for(size_t i=0; i < mvContactPoints.size(); i++)
		{
			apLowLevel->DrawSphere(mvContactPoints[i].mvPoint,0.2f, aPointColor);
			apLowLevel->DrawLine(mvContactPoints[i].mvPoint,
								mvContactPoints[i].mvNormal * mvContactPoints[i].mfDepth *0.2f,
								aLineColor);
			//Log("Rendering\n");
		}
	}

	//-----------------------------------------------------------------------

	bool iPhysicsWorld::CheckShapeWorldCollision(cVector3f *apNewPos,
							iCollideShape* apShape, const cMatrixf& a_mtxTransform,
							iPhysicsBody *apSkipBody, bool abSkipStatic, bool abIsCharacter,
							iPhysicsWorldCollisionCallback *apCallback,
							bool abCollideCharacter,
							bool abDebug)
	{
		cCollideData collideData;

		cVector3f vPushVec(0,0,0);
		bool bCollide = false;

		cBoundingVolume boundingVolume = apShape->GetBoundingVolume();
		boundingVolume.SetTransform(cMath::MatrixMul(a_mtxTransform, boundingVolume.GetTransform()));

		// Log("MAIN Position: %s Size: %s\n",boundingVolume.GetWorldCenter().ToString().c_str(),
		// 	boundingVolume.GetSize().ToString().c_str());

		//if(abDebug)Log("--------------\n");

		//tPhysicsBodyListIt it = mlstBodies.begin();
		//for(; it != mlstBodies.end(); ++it)
		cPortalContainerEntityIterator entIt = mpWorld3D->GetPortalContainer()->GetEntityIterator(&boundingVolume);
		while(entIt.HasNext())
		{
			//iPhysicsBody *pBody = *it;
			iPhysicsBody *pBody = static_cast<iPhysicsBody*>(entIt.Next());

			//if(abDebug) Log("Checking %s\n",pBody->GetName().c_str());




			if(pBody->IsCharacter() && abCollideCharacter==false) continue;
			if(pBody->IsActive()==false)continue;
			if(pBody == apSkipBody) continue;
			if(abSkipStatic && pBody->GetMass()==0) continue;
			if(abIsCharacter && pBody->GetCollideCharacter()==false) continue;
			if(abIsCharacter==false && pBody->GetCollide()==false) continue;

			if(cMath::CheckCollisionBV(boundingVolume,*pBody->GetBV())==false)
			{
				//if(abDebug) Log(" BV not collided\n");
				continue;

			}

		   	collideData.SetMaxSize(32);
			bool bRet = CheckShapeCollision(apShape,a_mtxTransform,
											pBody->GetShape(),pBody->GetLocalMatrix(),
											collideData, 32);

			if(bRet)
			{
				//if(abDebug) Log(" Collided with '%s'\n",pBody->GetName().c_str());

				if(apCallback) apCallback->OnCollision(pBody, &collideData);

				for(int i=0; i< collideData.mlNumOfPoints; i++)
				{
					cCollidePoint &point = collideData.mvContactPoints[i];

					// EXTREMLY FUCKING BIG HACK!
					// You have been warned.
					int mul = 1;
					cVector3f iv = point.mvPoint - boundingVolume.GetWorldCenter();
					iv.Normalise();
					if (cMath::Vector3Dot(iv, point.mvNormal) > 0)
						mul = -1;

					cVector3f vPush = point.mvNormal * (point.mfDepth * mul);
					// Log("Bbox %s, iv %s\n", boundingVolume.GetWorldCenter().ToString().c_str(), iv.ToString().c_str());
					if(std::abs(vPushVec.x) < std::abs(vPush.x)) vPushVec.x = vPush.x;
					if(std::abs(vPushVec.y) < std::abs(vPush.y)) vPushVec.y = vPush.y;
					if(std::abs(vPushVec.z) < std::abs(vPush.z)) vPushVec.z = vPush.z;
					//Log("xx vPush %s, vPushVec %s\n", vPush.ToString().c_str(), vPushVec.ToString().c_str());
				}
				bCollide = true;
			}
		}
		//Log("--------------\n");

		if(apNewPos)
			*apNewPos = a_mtxTransform.GetTranslation() + vPushVec;
		return bCollide;
	}

	//-----------------------------------------------------------------------

}
