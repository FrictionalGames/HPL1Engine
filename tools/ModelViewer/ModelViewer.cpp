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
#include <hpl.h>
#include <impl/SDLGameSetup.h>

#include "MVCamera.h"

//#pragma comment(lib, "HPL.lib")

using namespace hpl;

#ifdef WIN32
#include <Windows.h>
#endif

cGame *gpGame=NULL;

tString gsModelFile = "donut.dae";


////////////////////////////////////////////////////

class cBodyPicker : public iPhysicsRayCallback
{
public:
	cBodyPicker()
	{
		Clear();
	}

	void Clear()
	{
		mfLastT = 100000.0f;
		mpPickedBody = NULL;
	}

	bool OnIntersect(iPhysicsBody *pBody,cPhysicsRayParams *apParams)
	{
		if(pBody->GetMass()>0)
		{
			if(apParams->mfT < mfLastT)
			{
				mpPickedBody = pBody;
				mfLastT = apParams->mfT;
				mvPos = apParams->mvPoint;
				mfDist = apParams->mfDist;
			}
		}

		return true;
	}

	iPhysicsBody* mpPickedBody;
	cVector3f mvPos;
	cVector3f mvLocalPos;
	float mfLastT;
	float mfDist;
};

////////////////////////////////////////////////////


class cSimpleUpdate : public iUpdateable
{
public:
	cSimpleUpdate() : iUpdateable("SimpleUpdate")
	{
		//////////////////////////////////////////////////
		/// Variables
		mbDrawSkeleton = false;
		mbLightMovement = true;
		mbColliders = false;

		mfFlashAlpha = 0;

		//iMaterial::SetQuality(eMaterialQuality_High);

		/////////////////////////////////////////////////
		// Set up data
		mpLowLevelGraphics = gpGame->GetGraphics()->GetLowLevel();

		//gpGame->SetRenderOnce(true);
		//gpGame->GetGraphics()->GetRenderer3D()->SetDebugFlags(eRendererDebugFlag_LogRendering);
		//gpGame->GetGraphics()->GetRenderer3D()->SetDebugFlags(	eRendererDebugFlag_DrawNormals |
		//														eRendererDebugFlag_DrawTangents);

		gpGame->GetGraphics()->GetRendererPostEffects()->SetActive(true);
		gpGame->GetGraphics()->GetRendererPostEffects()->SetBloomActive(true);
		gpGame->GetGraphics()->GetRendererPostEffects()->SetBloomSpread(6);

		//iMaterial::SetQuality(eMaterialQuality_Medium);
		//gpGame->GetGraphics()->GetRenderer3D()->SetShowShadows(eRendererShowShadows_None);

		/////////////////////////////////////////////
		// Create Actions
		gpGame->GetInput()->AddAction(new cActionKeyboard("Shadows",gpGame->GetInput(),eKey_1));
		gpGame->GetInput()->AddAction(new cActionKeyboard("Colliders",gpGame->GetInput(),eKey_2));
		gpGame->GetInput()->AddAction(new cActionKeyboard("Physics",gpGame->GetInput(),eKey_3));
		gpGame->GetInput()->AddAction(new cActionKeyboard("Animation",gpGame->GetInput(),eKey_4));
		gpGame->GetInput()->AddAction(new cActionKeyboard("LightMovement",gpGame->GetInput(),eKey_5));
		gpGame->GetInput()->AddAction(new cActionKeyboard("Bloom",gpGame->GetInput(),eKey_b));
		gpGame->GetInput()->AddAction(new cActionKeyboard("DrawSkeleton",gpGame->GetInput(),eKey_6));

		gpGame->GetInput()->AddAction(new cActionKeyboard("RagDoll",gpGame->GetInput(),eKey_r));

		gpGame->GetInput()->AddAction(new cActionMouseButton("Interact",gpGame->GetInput(),eMButton_Right));

		/////////////////////////////////////////////
		// Resource setup
		gpGame->GetResources()->AddEntity3DLoader(new cEntityLoader_Object("Object"),true);


		/////////////////////////////////////////////
		// Create World
		mpWorld = gpGame->GetScene()->CreateWorld3D("Test");
		gpGame->GetScene()->SetWorld3D(mpWorld);

		/////////////////////////////////////////////
		// Create Physics world
		// Setup physics
		gpGame->GetPhysics()->LoadSurfaceData("materials.cfg");

		mpPhysicsWorld = gpGame->GetPhysics()->CreateWorld(true);
		//mpPhysicsWorld->SetAccuracyLevel(ePhysicsAccuracy_Low);
		mpPhysicsWorld->SetWorldSize(-300,300);
		mpPhysicsWorld->SetMaxTimeStep(1.0f / 30.0f);

		mpWorld->SetPhysicsWorld(mpPhysicsWorld);

		/////////////////////////////////////////////77
		// Create MEsh / Entity

		//Add resource path
		//gpGame->GetResources()->AddResourceDir(cString::GetFilePath(gsModelFile));

		tString sFileName = cString::GetFileName(gsModelFile);
		tString sExt = cString::ToLowerCase(cString::GetFileExt(sFileName));

		if(sExt == "ent")
		{
			float fY =0;
			cMatrixf mtxTransform = cMatrixf::Identity;
			//mtxTransform = cMath::MatrixRotateY(-kPi2f);
			//mtxTransform.SetTranslation(cVector3f(2,0,2));

			//for(int i=0; i<2; ++i)
			//{
			//mtxTransform.SetTranslation(cVector3f(4,0,0));

			mpEntity = static_cast<cMeshEntity*>(mpWorld->CreateEntity("EntityName",mtxTransform,sFileName, true));

			if(mpEntity)
			{
				if(mpEntity->GetMesh()->GetSkeleton())
				{
					for(int i=0;i < mpEntity->GetBoneStateNum(); i++)
					{
						cBoneState* pState = mpEntity->GetBoneState(i);

						if(pState->GetBody())
						{
							mvBodies.push_back(pState->GetBody());
							mvBodies.push_back(pState->GetColliderBody());
						}
					}
				}
				else if(mpEntity->HasNodes())
				{
					for(int i=0; i< mpEntity->GetSubMeshEntityNum(); i++)
					{
						cSubMeshEntity *pSub = mpEntity->GetSubMeshEntity(i);
						if(pSub->GetBody())
						{
							mvBodies.push_back(pSub->GetBody());
						}
					}
					if(mpEntity->GetBody())
					{
						mvBodies.push_back(mpEntity->GetBody());
					}
				}
				else
				{
					if(mpEntity->GetBody())
					{
						mvBodies.push_back(mpEntity->GetBody());
					}
				}
			}
			//fY -= 1.1f;
			//}
		}
		else
		{
			//float fY =0;
			//float fX =0;
			cMatrixf mtxTransform = cMatrixf::Identity;
			//mtxTransform = cMath::MatrixRotateY(kPif);

			//for(int i=0; i<8; ++i)
			//{
			//mtxTransform.SetTranslation(cVector3f(1,2,0));

			//Load mesh file
			cMesh *pMesh = gpGame->GetResources()->GetMeshManager()->CreateMesh(sFileName);
			if(pMesh==NULL) FatalError("Couldn't load mesh\n");

			//Create entity from mesh
			mpEntity = mpWorld->CreateMeshEntity("EntityName",pMesh);
			mpEntity->SetCastsShadows(true);
			//mpEntity->SetMatrix(mtxTransform);

			//Create physics body
			if(pMesh->GetColliderNum()>0)
			{
				if( (pMesh->GetPhysicsJointNum()>0 || pMesh->HasSeveralBodies())
					&& pMesh->GetAnimationNum() <=0)
				{
					pMesh->CreateJointsAndBodies(&mvBodies,mpEntity,NULL,mtxTransform,
												mpPhysicsWorld);
				}
				else if(pMesh->GetAnimationNum()>0)
				{
					iPhysicsBody *pRootBody=NULL;
					pMesh->CreateNodeBodies(&pRootBody,&mvBodies,mpEntity,mpPhysicsWorld,mtxTransform);
				}
				else
				{
					iCollideShape *pShape = pMesh->CreateCollideShape(mpPhysicsWorld);
					if(pShape == NULL){
						Error("Could not create collide shape!\n");
						return;
					}
					iPhysicsBody* pEntityBody = mpPhysicsWorld->CreateBody("EntityName",pShape);

					pEntityBody->SetMass(1);
					pEntityBody->SetEnabled(false);
					pEntityBody->SetMatrix(mtxTransform);

					cNode3D *pNode = pEntityBody->CreateNode();
					pNode->AddEntity(mpEntity);

					mvBodies.push_back(pEntityBody);
				}
			}
			else
			{
				mvBodies.clear();
			}

			// Create lights
			for(int i=0; i< pMesh->GetLightNum(); i++)
			{
				pMesh->CreateLightInWorld("EntityName",pMesh->GetLight(i),mpEntity,mpWorld);
			}

			//Create billboards
			for(int i=0; i< pMesh->GetBillboardNum(); i++)
			{
				pMesh->CreateBillboardInWorld("EntityName",pMesh->GetBillboard(i),mpEntity,mpWorld);
			}

			//Create beams
			for(int i=0; i< pMesh->GetBeamNum(); i++)
			{
				cBeam *pBeam = pMesh->CreateBeamInWorld("EntityName",pMesh->GetBeam(i),mpEntity,mpWorld);
				mvBeams.push_back(pBeam);
			}

			//Create particle systems
			for(int i=0; i< pMesh->GetParticleSystemNum(); i++)
			{
				pMesh->CreateParticleSystemInWorld("EntityName",pMesh->GetParticleSystem(i),mpEntity,mpWorld);
			}

			//Create references
			for(int i=0; i< pMesh->GetReferenceNum(); i++)
			{
				pMesh->CreateReferenceInWorld("EntityName",pMesh->GetReference(i),mpEntity,mpWorld,
												cMatrixf::Identity);
			}

			//Create sound entities
			for(int i=0; i< pMesh->GetSoundEntityNum(); i++)
			{
				pMesh->CreateSoundEntityInWorld("EntityName",pMesh->GetSoundEntity(i),mpEntity,mpWorld);
			}

			//Check if entity has animation, if so play it.
			if(mpEntity->GetAnimationStateNum()>0)
			{
				cAnimationState *pAnimState = mpEntity->GetAnimationState(0);

				pAnimState->SetActive(true);
				pAnimState->SetLoop(true);
			}
			else if(pMesh->GetSkeleton())
			{
				mpEntity->SetSkeletonPhysicsActive(true);
			}
			//fY += 1.1f;
			//fX += 0.6f;
			//}


			//DEBUG:
			/*for(int i=0; i< mvBodies.size(); ++i)
			{
				iPhysicsBody *pBody = mvBodies[i];
				//pBody->SetContinuousCollision(false);
				//pBody->SetIsCharacter(true);
				//pBody->SetCollideCharacter(false);
			}*/
		}

		if(mpEntity)
		{
			mpEntity->SetSkeletonCollidersActive(true);
		}

		/////////////////////////////////////////////77
		// Create Floor
		cMesh *pMesh = gpGame->GetResources()->GetMeshManager()->CreateMesh("misc_rect.dae");
		if(pMesh==NULL) FatalError("Couldn't load mesh\n");

		cMeshEntity *pFloor = mpWorld->CreateMeshEntity("Floor",pMesh);
		pFloor->SetMatrix(cMath::MatrixScale(7));
		pFloor->SetVisible(true);
		pFloor->SetPosition(cVector3f(0,mpEntity->GetBoundingVolume()->GetMin().y-0.1f,0));

		if(mvBodies.size()>0)
		{
			cVector3f vSize = pFloor->GetBoundingVolume()->GetSize();
			vSize.y = 0.1f;

			cMatrixf mtxOffSet = cMath::MatrixTranslate(cVector3f(0,-0.05f,0));
			iCollideShape *pShape = mpPhysicsWorld->CreateBoxShape(vSize, &mtxOffSet);
			iPhysicsBody *pBody = mpPhysicsWorld->CreateBody("Floor",pShape);

			pBody->SetPosition(pFloor->GetWorldPosition());
			pBody->SetGravity(false);
		}

		/////////////////////////////////////////////77
		// Create Flares
		mpFlare  = mpWorld->CreateBillboard("Bill1",2,"misc_flare");
		mpFlare->SetVisible(true);
		mpFlare2  = mpWorld->CreateBillboard("Bill2",2,"misc_flare2");
		mpFlare2->SetVisible(true);

		/////////////////////////////////////////////77
		// Create Lights
		mpLight = mpWorld->CreateLightPoint("Light1");
		mpLight->SetDiffuseColor(cColor(1,1,1,1.0f));
		mpLight->SetFarAttenuation(280.0f);
		mpLight->SetCastShadows(true);
		//mpLight->SetVisible(false);

		mpLight2 = mpWorld->CreateLightPoint("Light2");
		mpLight2->SetDiffuseColor(cColor(1,1,1,1.0f));
		mpLight2->SetFarAttenuation(60.0f);
		mpLight2->SetCastShadows(true);
		//mpLight2->SetVisible(false);

		mpFont = gpGame->GetResources()->GetFontManager()->CreateFontData("viewer.fnt",12,32,128);

		mlCurrentAnim =0;

		//debug
		//mpEntity->SetPosition(cVector3f(3,0,0));

		//for(size_t i=0; i< mvBodies.size(); ++i)
		//{
		//	mvBodies[i]->SetAutoDisable(false);
		//}

		/*iTexture *pTex = gpGame->GetResources()->GetTextureManager()->CreateCubeMap("thomas_cube1",false);
		gpGame->GetGraphics()->GetRenderer3D()->SetSkyBox(pTex,true);
		gpGame->GetGraphics()->GetRenderer3D()->SetSkyBoxActive(true);*/

		int lVtxTotal =0;
		Log("------- Model specifications -----------\n");
		for(int i=0; i<mpEntity->GetSubMeshEntityNum(); ++i)
		{
			cSubMesh * pSubMesh = mpEntity->GetSubMeshEntity(i)->GetSubMesh();
			iVertexBuffer *pVtxBuff = pSubMesh->GetVertexBuffer();
			Log(" SubMesh: '%s'\n", pSubMesh->GetName().c_str());
			Log("  Vertex count: %d\n", pVtxBuff->GetVertexNum());
			Log("  Index count: %d\n", pVtxBuff->GetIndexNum());
			Log("  Material: %s\n",pSubMesh->GetMaterial()->GetName().c_str());
			Log("\n");

			/*Log("  Indices: ");
			for(int j=0; j<pVtxBuff->GetIndexNum(); ++j)
			{
				Log(" %d,", pVtxBuff->GetIndices()[j]);
			}
			Log("\n");
			Log("  Vertices: \n");
			float *pPosArray = pVtxBuff->GetArray(eVertexFlag_Position);
			for(int j=0; j<pVtxBuff->GetVertexNum()*2; ++j)
			{
				float *pPos = &pPosArray[j*4];
				Log("   %f, %f, %f, %f\n",pPos[0],pPos[1],pPos[2],pPos[3]);
				if(j==pVtxBuff->GetVertexNum()-1) Log("--------\n");
			}*/


			lVtxTotal += pSubMesh->GetVertexBuffer()->GetVertexNum();
		}
		Log(" Total Vertex count: %d\n",lVtxTotal);
		if(mpEntity->GetMesh()->GetSkeleton())
			Log(" Bones: %d\n", mpEntity->GetMesh()->GetSkeleton()->GetBoneNum());

		Log("\n");
		Log(" Bodies:\n");
		for(size_t i=0; i<mvBodies.size(); ++i)
		{
			iPhysicsBody *pBody = mvBodies[i];

			Log("  Name: %s\n", pBody->GetName().c_str());
			Log("  Mass: %f\n", pBody->GetMass());

			if(i!= mvBodies.size()-1) Log("  ----\n");
		}


		Log("------------------------\n");


	}

	~cSimpleUpdate()
	{

	}

	void Update(float afTimeStep)
	{
		//Log("Update!\n");

		cCamera3D *pCam = static_cast<cCamera3D*>(gpGame->GetScene()->GetCamera());

		//////////////////////////////////
		//Check special event
		if(mfFlashAlpha >0)
		{
			mfFlashAlpha -= afTimeStep *2;
		}

		if(mpEntity->GetAnimationStateNum()>0)
		{
			cAnimationState *pState = mpEntity->GetAnimationState(mlCurrentAnim);
			if(mbFlashed==false && pState->IsAfterSpecialEvent() && pState->GetSpecialEventTime() >0)
			{
				mbFlashed = true;
				mfFlashAlpha =1;
			}

			if(mbFlashed && pState->IsBeforeSpecialEvent())
			{
				mbFlashed = false;
			}
		}

		//////////////////////////////////
		//Rotate light/flare
		if(mbLightMovement)
		{
			cMatrixf mtxRot = cMath::MatrixRotateY(mvFlareRotation.y);
			mtxRot = cMath::MatrixMul(mtxRot,cMath::MatrixRotateZ(mvFlareRotation.z));

			mpFlare->SetPosition(cMath::MatrixMul(mtxRot,cVector3f(0,10,10)));
			mpLight->SetPosition(cMath::MatrixMul(mtxRot,cVector3f(0,10,10)));

			mtxRot = cMath::MatrixRotateY(mvFlareRotation.y);
			mtxRot = cMath::MatrixMul(mtxRot,cMath::MatrixRotateZ(mvFlareRotation.z));

			mpFlare2->SetPosition(cMath::MatrixMul(mtxRot,cVector3f(0,10,-10)));
			mpLight2->SetPosition(cMath::MatrixMul(mtxRot,cVector3f(0,10,-10)));

			mvFlareRotation.y += cMath::ToRad(1);
		}

		////////////////////////////////////
		//Input
		//Render Shadows
		if(gpGame->GetInput()->BecameTriggerd("Shadows")){
			eRendererShowShadows mShadows = gpGame->GetGraphics()->GetRenderer3D()->GetShowShadows();

			if(mShadows == eRendererShowShadows_All)
				gpGame->GetGraphics()->GetRenderer3D()->SetShowShadows(eRendererShowShadows_None);
			else
				gpGame->GetGraphics()->GetRenderer3D()->SetShowShadows(eRendererShowShadows_All);
		}
		//Render Bloom
		if(gpGame->GetInput()->BecameTriggerd("Bloom")){
			bool bBloom = gpGame->GetGraphics()->GetRendererPostEffects()->GetBloomActive();
			gpGame->GetGraphics()->GetRendererPostEffects()->SetBloomActive(!bBloom);
		}
		//Render colliders
		if(gpGame->GetInput()->BecameTriggerd("Colliders")){
			mbColliders = !mbColliders;
		}
		//Simulate physics
		if(mvBodies.size()==1 && gpGame->GetInput()->BecameTriggerd("Physics"))
		{
			mvBodies[0]->SetEnabled(!mvBodies[0]->GetEnabled());
			if(mvBodies[0]->GetEnabled()==false){
				mvBodies[0]->SetMatrix(cMatrixf::Identity);
			}
		}
		//Change animation
		if(mpEntity->GetAnimationStateNum()>0 && gpGame->GetInput()->BecameTriggerd("Animation"))
		{
			mlCurrentAnim++;
			if(mlCurrentAnim >= mpEntity->GetAnimationStateNum()) mlCurrentAnim =0;

			mbFlashed = false;

			mpEntity->Play(mlCurrentAnim,true,true);
		}
		if(gpGame->GetInput()->BecameTriggerd("LightMovement")){
			mbLightMovement = !mbLightMovement;
		}
		if(gpGame->GetInput()->BecameTriggerd("DrawSkeleton")){
			mbDrawSkeleton = !mbDrawSkeleton;
		}

		//Set ragdoll on / off.
		if(gpGame->GetInput()->BecameTriggerd("RagDoll"))
		{
			mbFlashed = false;

			if(mpEntity->GetSkeletonPhysicsActive())
			{
				cMatrixf mtxTransform = mpEntity->CalculateTransformFromSkeleton(NULL,NULL);
				mpEntity->SetMatrix(mtxTransform);

				mpEntity->UpdateLogic(0);

				//////////////////////////////////
				//Fade to animation.
				float fFadeTime = 0.5f;

				mpEntity->FadeSkeletonPhysicsWeight(fFadeTime);

				mpEntity->Play(mlCurrentAnim,true,true);
				mpEntity->GetAnimationState(mlCurrentAnim)->SetWeight(0.0f);
				mpEntity->GetAnimationState(mlCurrentAnim)->FadeIn(fFadeTime);
			}
			else
			{
				mpEntity->AlignBodiesToSkeleton(false);

				mpEntity->SetSkeletonPhysicsActive(true);
				mpEntity->Stop();
			}

		}

		//Interact with object
		if(mvBodies.size()>0 && gpGame->GetInput()->IsTriggerd("Interact"))
		{
			cVector2f vMousePos = gpGame->GetInput()->GetMouse()->GetAbsPosition();

			if(mBodyPicker.mpPickedBody)
			{
				//Get pos of start.
				mBodyPicker.mvPos = cMath::MatrixMul(mBodyPicker.mpPickedBody->GetLocalMatrix(),
															mBodyPicker.mvLocalPos);

				//Get Drag pos
				cVector3f vDir = pCam->UnProject(vMousePos, mpLowLevelGraphics);
				mvDragPos = pCam->GetPosition() + vDir*mBodyPicker.mfDist;

				//Spring testing:
				cVector3f vForce = (mvDragPos - mBodyPicker.mvPos)*20 -
									(mBodyPicker.mpPickedBody->GetLinearVelocity()*0.4f);


				mBodyPicker.mpPickedBody->AddForceAtPosition(vForce *  mBodyPicker.mpPickedBody->GetMass(),
															mBodyPicker.mvPos);
			}
			else
			{
				cVector3f vDir = pCam->UnProject(vMousePos, mpLowLevelGraphics);
				cVector3f vOrigin = pCam->GetPosition();
				cVector3f vEnd = pCam->GetPosition() + vDir*100.0f;

				mBodyPicker.Clear();

				mpPhysicsWorld->CastRay(&mBodyPicker,vOrigin,vEnd,true,false,true);

				if(mBodyPicker.mpPickedBody)
				{
					mBodyPicker.mpPickedBody->SetAutoDisable(false);

					//mvDragPos = mBodyPicker.mvPos;

					cMatrixf mtxInvWorld = cMath::MatrixInverse(mBodyPicker.mpPickedBody->GetLocalMatrix());
					mBodyPicker.mvLocalPos = cMath::MatrixMul(mtxInvWorld, mBodyPicker.mvPos);

					mvRay0 = vOrigin;
					mvRay1 = vEnd;
				}
			}
		}
		else
		{
			if(mBodyPicker.mpPickedBody) mBodyPicker.mpPickedBody->SetAutoDisable(true);
			mBodyPicker.Clear();
		}


		//DEBUGTEST:
		//Spring test, make the object rotate at a certain speed.
		/*float fDestSpeed = 3.0f;
		float fF = 10.0f;
		float fG = 2.0f;

		cVector3f vSpeed = mvBodies[0]->GetAngularVelocity();

		//float fForce = (fDestSpeed - vSpeed.Length())*fF - fG* vSpeed.Length();
		//mvBodies[0]->AddTorque(cVector3f(0,fForce,0));

		float fForce = (fDestSpeed - vSpeed.x)*fF - fG* vSpeed.x;
		mvBodies[0]->AddTorque(cVector3f(fForce,0,0));


		//cVector3f vForce = (cVector3f(fDestSpeed,fDestSpeed,fDestSpeed) - vSpeed)*fF - vSpeed*fG;
		//mvBodies[0]->AddTorque(vForce);*/
	}

	void OnDraw()
	{
		if(mfFlashAlpha >0)
		{
			mpFont->Draw(cVector3f(400,300,0),50,cColor(1,0,0,mfFlashAlpha),eFontAlign_Center,_W("SPECIAL EVENT!"));
		}

		if(mpEntity->GetAnimationStateNum()>0)
		{
			cAnimationState *pState = mpEntity->GetAnimationState(mlCurrentAnim);
			mpFont->Draw(cVector3f(5,17,0),12,cColor(1,1,1,1),eFontAlign_Left,_W("Animation: %s"),
						cString::To16Char(pState->GetName()).c_str());
		}

		if(false)
		{

			tStringVec vSoundNames;
			std::vector<cSoundEntry*> vEntries;

			tSoundEntryList *pEntryList = gpGame->GetSound()->GetSoundHandler()->GetWorldEntryList();
			for(tSoundEntryListIt it = pEntryList->begin(); it != pEntryList->end();++it)
			{
				iSoundChannel *pSound = it->mpSound;
				vSoundNames.push_back(pSound->GetData()->GetName());
				vEntries.push_back(&(*it));
			}

			mpFont->Draw(cVector3f(5,18,0),10,cColor(1,1,1,1),eFontAlign_Left,_W("Num of sounds: %d"),vSoundNames.size());

			int lRow=0, lCol=0;
			for(int i=0; i< (int)vSoundNames.size(); i++)
			{
				cSoundEntry *pEntry = vEntries[i];
				mpFont->Draw(cVector3f((float)lCol*250,26+(float)lRow*11,0),10,cColor(1,1,1,1),eFontAlign_Left,
					_W("%s(%.2f/%d)"),cString::To16Char(vSoundNames[i]).c_str(),
					pEntry->mpSound->GetVolume(),
					pEntry->mpSound->GetPaused()?1:0);

				lCol++;
				if(lCol == 3)
				{
					lCol =0;
					lRow++;
				}
			}
		}
	}

	void OnPostSceneDraw()
	{
		cCamera3D *pCam = static_cast<cCamera3D*>(gpGame->GetScene()->GetCamera());
		mpLowLevelGraphics->SetMatrix(eMatrix_ModelView, pCam->GetViewMatrix());
		mpLowLevelGraphics->SetDepthTestActive(false);

		mpLowLevelGraphics->SetBlendActive(false);

		if(mvBodies.size()>0 && mbColliders)
		{
			mpLowLevelGraphics->SetDepthTestActive(false);

			for(int i=0; i < (int)mvBodies.size(); i++)
			{
				//Log("Body: %s active: %d\n",mvBodies[i]->GetName().c_str(), mvBodies[i]->IsActive());
				if(mvBodies[i]->IsActive())
				{
					cVector3f vMassCentre = cMath::MatrixMul(mvBodies[i]->GetWorldMatrix(),
															mvBodies[i]->GetMassCentre());
					mvBodies[i]->RenderDebugGeometry(mpLowLevelGraphics,cColor(0.6f,0.6f,0.6f,1));
					mpLowLevelGraphics->DrawSphere(vMassCentre,0.05f,cColor(1,0.2f,0.4f,1));
				}
			}

			cPhysicsJointIterator JointIt = mpPhysicsWorld->GetJointIterator();

			while(JointIt.HasNext())
			{
				iPhysicsJoint *pJoint = JointIt.Next();

				//mpLowLevelGraphics->DrawSphere(pJoint->GetPivotPoint(),0.05f,cColor(1,0.2f,0.4f,1));
			}
		}

		///////////////////
		//BEGIN TEST BEAM

		/*mpLowLevelGraphics->DrawSphere(mvBeams[0]->GetWorldPosition(),0.1f,cColor(1,0,1,1));
		mpLowLevelGraphics->DrawSphere(mvBeams[0]->GetEnd()->GetWorldPosition(),0.1f,cColor(1,0,1,1));

		cVector3f vDir = mvBeams[0]->GetEnd()->GetWorldPosition() - mvBeams[0]->GetWorldPosition();
		cVector3f vMid =  mvBeams[0]->GetWorldPosition() + vDir*0.5f;

		mpLowLevelGraphics->DrawSphere(vMid,0.1f,cColor(0,1,1,1));

		mpLowLevelGraphics->DrawLine(	vMid, vMid+mvBeams[0]->GetAxis()*0.35f,
										cColor(1,0.6,1,1));*/

		//END TEST BEAM
		///////////////////


		mpLowLevelGraphics->SetDepthTestActive(true);

		//mpLowLevelGraphics->DrawLine(mvRay0, mvRay1, cColor(1,1,1,1));

		if(mBodyPicker.mpPickedBody)
		{
			//cBoundingVolume *pBV = mBodyPicker.mpPickedBody->GetBV();
			//mpLowLevelGraphics->DrawBoxMaxMin(pBV->GetMax(), pBV->GetMin(),cColor(1,0,1,1));

			mpLowLevelGraphics->DrawSphere(mBodyPicker.mvPos,0.1f, cColor(1,0,0,1));
			mpLowLevelGraphics->DrawSphere(mvDragPos,0.1f, cColor(1,0,0,1));

			mpLowLevelGraphics->DrawLine(mBodyPicker.mvPos, mvDragPos, cColor(1,1,1,1));
		}

		mpLowLevelGraphics->DrawLine(cVector3f(0,0,0),cVector3f(1,0,0),cColor(1,0,0,1));
		mpLowLevelGraphics->DrawLine(cVector3f(0,0,0),cVector3f(0,1,0),cColor(0,1,0,1));
		mpLowLevelGraphics->DrawLine(cVector3f(0,0,0),cVector3f(0,0,1),cColor(0,0,1,1));

		if(mbDrawSkeleton)
		{
			mpLowLevelGraphics->SetDepthTestActive(false);
			mpLowLevelGraphics->SetDepthWriteActive(false);

			cMesh *pMesh = mpEntity->GetMesh();
			cSkeleton *pSkeleton = pMesh->GetSkeleton();

			mpLowLevelGraphics->DrawBoxMaxMin(	mpEntity->GetBoundingVolume()->GetMax(),
												mpEntity->GetBoundingVolume()->GetMin(),
												cColor(1,1,1,1));

			if(pSkeleton)
			{

				for(int i=0;i < mpEntity->GetBoneStateNum(); i++)
				{
					cBoneState* pState = mpEntity->GetBoneState(i);

					cVector3f vStart = pState->GetWorldPosition();

					mpLowLevelGraphics->DrawSphere(vStart,0.05f,cColor(0.3f,1,0.3f,1));

					if(pState->GetParent() && pState->GetParent() != mpEntity->GetRootNode())
					{
						cVector3f vEnd = pState->GetParent()->GetWorldPosition();
						mpLowLevelGraphics->DrawLine(vStart,vEnd,cColor(0.2f,0.2f,1,1));
					}
				}
			}
			else
			{
				Warning("No skeleton in mesh!\n");
			}
		}

		mpLowLevelGraphics->DrawSphere(mpEntity->GetWorldPosition(),0.05f,cColor(1.0f,0.3f,0.3f,1));



		//////////////////////////////////////
		//Debug the root bone
		/*mpLowLevelGraphics->SetDepthTestActive(false);
		mpLowLevelGraphics->SetDepthWriteActive(false);

		cNodeIterator BoneIt = mpEntity->GetRootNode()->GetChildInterator();
		cBoneState *pBoneState = static_cast<cBoneState*>(BoneIt.Next());

		cMatrixf mtxInvBone = cMath::MatrixInverse(pBoneState->GetWorldMatrix());

		mpLowLevelGraphics->DrawSphere(pBoneState->GetWorldPosition(),0.1f,cColor(0,1,1));
		//Up
		//mpLowLevelGraphics->DrawLine(	pBoneState->GetWorldPosition(),
		//								pBoneState->GetWorldPosition() + mtxInvBone.GetUp()*0.5f,
		//								cColor(1,0,1));
		//Forward
		mpLowLevelGraphics->DrawLine(	pBoneState->GetWorldPosition(),
										pBoneState->GetWorldPosition() + mtxInvBone.GetForward()*0.5f,
										cColor(1,1,0));

		//Right
		//mpLowLevelGraphics->DrawLine(	pBoneState->GetWorldPosition(),
		//								pBoneState->GetWorldPosition() + mtxInvBone.GetRight()*0.5f,
		//								cColor(0,1,1));
		*/
		/////////////////////////////////////////////
		//Debug draw lights:
		/*tLight3DList *pLightList = mpWorld->GetLightList();
		tLight3DListIt it = pLightList->begin();
		for(; it != pLightList->end(); ++it)
		{
			iLight3D *pLight = *it;
			mpLowLevelGraphics->DrawSphere(pLight->GetWorldPosition(),0.1f,pLight->GetDiffuseColor());
		}*/

		mpLowLevelGraphics->SetDepthTestActive(true);
		mpLowLevelGraphics->SetDepthWriteActive(true);
	}



private:
	cVector3f mvRot;
	cVector3f mvPos;
	iFontData* mpFont;

	cMeshEntity *mpEntity;

	iLowLevelGraphics* mpLowLevelGraphics;
	cWorld3D* mpWorld;

	std::vector<iPhysicsBody*> mvBodies;

	cVector3f mvRay0;
	cVector3f mvRay1;
	cVector3f mvDragPos;

	iPhysicsWorld *mpPhysicsWorld;

	cBillboard *mpFlare;
	cBillboard *mpFlare2;

	iLight3D *mpLight;
	iLight3D *mpLight2;
	cVector3f mvFlareRotation;

	cVector3f mvMeshRotation;

	cBodyPicker mBodyPicker;

	int mlCurrentAnim;

	bool mbColliders;

	bool mbLightMovement;

	bool mbDrawSkeleton;

	std::vector<cBeam*> mvBeams;

	bool mbFlashed;
	float mfFlashAlpha;
};


/////////////////////////////////////////////////////


int hplMain(const tString &asCommandLine)
{
	//CreateMessageBoxW(_W("Yeah"),_W("Start da app"));
	/////////////////////////////////////////////
	//Set current directory where the exe is.
#ifdef WIN32

	TCHAR buffer[MAX_PATH];
	HMODULE module = GetModuleHandle(NULL);
	GetModuleFileName(module, buffer,MAX_PATH);
	tString sDir = cString::GetFilePath(buffer);
	SetCurrentDirectory(sDir.c_str());
#endif
	//CreateMessageBoxW(_W("Path"),_W("To Exe: %s"), cString::To16Char(sDir).c_str());

	//iGpuProgram::SetLogDebugInformation(true);

	SetUpdateLogActive(true);

	///////////////////////////////////
	// Load config file
	cConfigFile *pConfig = new cConfigFile(_W("viewer_settings.cfg"));
	pConfig->Load();

	int lScreenW = pConfig->GetInt("Screen","Width",800);
	int lScreenH = pConfig->GetInt("Screen","Height",600);
	bool bFullScreen = pConfig->GetBool("Screen", "FullScreen", false);
	bool bVsync = pConfig->GetBool("Screen", "Vsync", false);

	//Init the game engine
	gpGame = new cGame(new cSDLGameSetup(),lScreenW,lScreenH,32,bFullScreen,45);
	gpGame->GetGraphics()->GetLowLevel()->SetVsyncActive(bVsync);
	gpGame->SetLimitFPS(false);

	SetWindowCaption("Model Viewer");

	//Add file where model is
	//iResourceBase::SetLogCreateAndDelete(true);

	//Add resources
	gpGame->GetResources()->LoadResourceDirsFile("resources.cfg");

	if(asCommandLine != ""){
		gsModelFile = asCommandLine;
		gsModelFile = cString::ReplaceCharTo(gsModelFile,"\"","");

		tString sModelDir = cString::GetFilePath(gsModelFile);
		if(sModelDir != "")
			gpGame->GetResources()->AddResourceDir(sModelDir);

		//CreateMessageBoxW(_W("Path"),_W("To Model: %s"), cString::To16Char(sModelDir).c_str());
	}

	//Add updates
	cSimpleUpdate Update;
	gpGame->GetUpdater()->AddUpdate("Default", &Update);

	cMVCamera cameraUpdate(gpGame,20,cVector3f(0,0,10),true);
	gpGame->GetUpdater()->AddUpdate("Default", &cameraUpdate);

	//Run the engine
	gpGame->Run();

	//Delete the engine
	delete gpGame;

	pConfig->SetInt("Screen","Width",lScreenW);
	pConfig->SetInt("Screen","Height",lScreenH);
	pConfig->SetBool("Screen", "FullScreen", bFullScreen);
	pConfig->SetBool("Screen", "Vsync", bVsync);

	pConfig->Save();
	delete pConfig;

	return 0;
}
