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

#include "HOECamera.h"

#pragma comment(lib, "HPL.lib")

using namespace hpl;



cGame *gpGame=NULL;
cHOECamera* gpCameraUpdate;

tString gsModelFile = "donut.dae";


////////////////////////////////////////////////////

class cBodyPicker : public iPhysicsRayCallback
{
public:
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


		/////////////////////////////////////////////////
		// Set up data
		mpLowLevelGraphics = gpGame->GetGraphics()->GetLowLevel();

		//gpGame->SetRenderOnce(true);
		//gpGame->GetGraphics()->GetRenderer3D()->SetDebugFlags(eRendererDebugFlag_LogRendering);

		gpGame->GetGraphics()->GetRendererPostEffects()->SetActive(true);
		gpGame->GetGraphics()->GetRendererPostEffects()->SetBloomActive(true);
		gpGame->GetGraphics()->GetRendererPostEffects()->SetBloomSpread(6);

		/////////////////////////////////////////////
		// Create Actions
		gpGame->GetInput()->AddAction(new cActionKeyboard("AngleXInc",gpGame->GetInput(),eKey_a));
		gpGame->GetInput()->AddAction(new cActionKeyboard("AngleYInc",gpGame->GetInput(),eKey_s));
		gpGame->GetInput()->AddAction(new cActionKeyboard("AngleZInc",gpGame->GetInput(),eKey_d));
		gpGame->GetInput()->AddAction(new cActionKeyboard("AngleXDec",gpGame->GetInput(),eKey_z));
		gpGame->GetInput()->AddAction(new cActionKeyboard("AngleYDec",gpGame->GetInput(),eKey_x));
		gpGame->GetInput()->AddAction(new cActionKeyboard("AngleZDec",gpGame->GetInput(),eKey_c));

		gpGame->GetInput()->AddAction(new cActionKeyboard("XInc",gpGame->GetInput(),eKey_RIGHT));
		gpGame->GetInput()->AddAction(new cActionKeyboard("YInc",gpGame->GetInput(),eKey_UP));
		gpGame->GetInput()->AddAction(new cActionKeyboard("ZInc",gpGame->GetInput(),eKey_PAGEUP));
		gpGame->GetInput()->AddAction(new cActionKeyboard("XDec",gpGame->GetInput(),eKey_LEFT));
		gpGame->GetInput()->AddAction(new cActionKeyboard("YDec",gpGame->GetInput(),eKey_DOWN));
		gpGame->GetInput()->AddAction(new cActionKeyboard("ZDec",gpGame->GetInput(),eKey_PAGEDOWN));

		gpGame->GetInput()->AddAction(new cActionKeyboard("View1",gpGame->GetInput(),eKey_1));
		gpGame->GetInput()->AddAction(new cActionKeyboard("View2",gpGame->GetInput(),eKey_2));

		gpGame->GetInput()->AddAction(new cActionKeyboard("Print",gpGame->GetInput(),eKey_p));


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
		mpPhysicsWorld->SetWorldSize(-300,300);
		mpPhysicsWorld->SetMaxTimeStep(1.0f / 30.0f);

        mpWorld->SetPhysicsWorld(mpPhysicsWorld);


		/////////////////////////////////////////////
		// Create FPS Camera.
		mpFPSCamera = gpGame->GetScene()->CreateCamera3D(eCameraMoveMode_Fly);
		mpFPSCamera->SetFOV(cMath::ToRad(70));


		/////////////////////////////////////////////
		cMesh *pMesh = gpGame->GetResources()->GetMeshManager()->CreateMesh(gsModelFile);
		if(pMesh==NULL) FatalError("Couldn't load mesh\n");

		mpEntity = mpWorld->CreateMeshEntity("Model",pMesh);

		// Create lights
		for(int i=0; i< pMesh->GetLightNum(); i++)
		{
			iLight3D* pLight = pMesh->CreateLightInWorld("Model",pMesh->GetLight(i),mpEntity,mpWorld);
		}

		// Create billboards
		for(int i=0; i< pMesh->GetBillboardNum(); i++)
		{
			cBillboard *pBill = pMesh->CreateBillboardInWorld("Model",pMesh->GetBillboard(i),mpEntity,mpWorld);
		}

		// Create particle systems
		for(int i=0; i< pMesh->GetParticleSystemNum(); i++)
		{
			cParticleSystem3D *pPS = pMesh->CreateParticleSystemInWorld("Model",pMesh->GetParticleSystem(i),mpEntity,mpWorld);
		}



		/////////////////////////////////////////////77
		// Create Floor
		pMesh = gpGame->GetResources()->GetMeshManager()->CreateMesh("misc_rect.dae");
		if(pMesh==NULL) FatalError("Couldn't load mesh\n");

		cMeshEntity *pFloor = mpWorld->CreateMeshEntity("Floor",pMesh);
		pFloor->SetMatrix(cMath::MatrixScale(7));
		pFloor->SetVisible(true);
		pFloor->SetPosition(cVector3f(0,-1.5,0));

		/////////////////////////////////////////////77
		// Create Lights
		mpLight = mpWorld->CreateLightPoint("Light1");
		mpLight->SetDiffuseColor(cColor(1,1,1,1.0f));
		mpLight->SetFarAttenuation(280.0f);
		mpLight->SetCastShadows(true);
		mpLight->SetPosition(cVector3f(8,8,8));
		//mpLight->SetVisible(false);

		mpLight2 = mpWorld->CreateLightPoint("Light2");
		mpLight2->SetDiffuseColor(cColor(1,1,1,1.0f));
		mpLight2->SetFarAttenuation(60.0f);
		mpLight2->SetCastShadows(true);
		mpLight2->SetPosition(cVector3f(-8,8,-8));
		//mpLight2->SetVisible(false);

		/////////////////////////////////////////////77
		// Create Flares
		mpFlare  = mpWorld->CreateBillboard("Bill1",2,"misc_flare");
		mpFlare->SetVisible(true);
		mpFlare->SetPosition(mpLight->GetWorldPosition());
		mpFlare2  = mpWorld->CreateBillboard("Bill2",2,"misc_flare2");
		mpFlare2->SetVisible(true);
		mpFlare2->SetPosition(mpLight2->GetWorldPosition());

		/////////////////////////////////////////////77
		// Misc

		mpFont = gpGame->GetResources()->GetFontManager()->CreateFontData("viewer.fnt",12,32,128);

		mvPosition = cVector3f(0,0,0);
		mvRotation = cVector3f(0,0,0);

		cVector3f vRot = cVector3f(0,0,0);
		Log("Rot BEfore:%s\n",vRot.ToString().c_str());
		cMatrixf mtxTest = cMath::MatrixRotate(vRot,eEulerRotationOrder_XYZ);
		vRot = cMath::MatrixToEulerAngles(mtxTest,eEulerRotationOrder_XYZ);
		Log("Rot After:%s\n",vRot.ToString().c_str());


		vRot = cVector3f(kPif,0,0);
		Log("Rot BEfore:%s\n",vRot.ToString().c_str());
		mtxTest = cMath::MatrixRotate(vRot,eEulerRotationOrder_XYZ);
		vRot = cMath::MatrixToEulerAngles(mtxTest,eEulerRotationOrder_XYZ);
		Log("Rot After:%s\n",vRot.ToString().c_str());

		vRot = cVector3f(0,kPif,0);
		Log("Rot BEfore:%s\n",vRot.ToString().c_str());
		mtxTest = cMath::MatrixRotate(vRot,eEulerRotationOrder_XYZ);
		vRot = cMath::MatrixToEulerAngles(mtxTest,eEulerRotationOrder_XYZ);
		Log("Rot After:%s\n",vRot.ToString().c_str());

		//vRot = cVector3f(0,kPif,0);
		vRot = cVector3f(kPif,0,kPif);
		mtxTest = cMath::MatrixRotate(vRot,eEulerRotationOrder_XYZ);

		//mpEntity->SetMatrix(mtxTest);
	}

	~cSimpleUpdate()
	{

	}

	void OnExit()
	{
	}

	void Update(float afTimeStep)
	{
		float fPosSpeed =0.15f *afTimeStep;
		float fRotSpeed =2.2f *afTimeStep;

		cMatrixf mtxTransform = cMath::MatrixRotate(mvRotation,eEulerRotationOrder_XYZ);

		////////////////////////////////////
		//Input
		//Render Shadows
		if(gpGame->GetInput()->IsTriggerd("XInc")) mvPosition.x += fPosSpeed;
		if(gpGame->GetInput()->IsTriggerd("YInc")) mvPosition.y += fPosSpeed;
		if(gpGame->GetInput()->IsTriggerd("ZInc")) mvPosition.z += fPosSpeed;
		if(gpGame->GetInput()->IsTriggerd("XDec")) mvPosition.x -= fPosSpeed;
		if(gpGame->GetInput()->IsTriggerd("YDec")) mvPosition.y -= fPosSpeed;
		if(gpGame->GetInput()->IsTriggerd("ZDec")) mvPosition.z -= fPosSpeed;

		if(gpGame->GetInput()->IsTriggerd("AngleXInc")){
			mtxTransform = cMath::MatrixMul(mtxTransform, cMath::MatrixRotateX(fRotSpeed));
		}
		if(gpGame->GetInput()->IsTriggerd("AngleYInc")) {
			mtxTransform = cMath::MatrixMul(mtxTransform, cMath::MatrixRotateY(fRotSpeed));
		}
		if(gpGame->GetInput()->IsTriggerd("AngleZInc")) {
			mtxTransform = cMath::MatrixMul(mtxTransform, cMath::MatrixRotateZ(fRotSpeed));
		}

		if(gpGame->GetInput()->IsTriggerd("AngleXDec")) {
			mtxTransform = cMath::MatrixMul(mtxTransform, cMath::MatrixRotateX(-fRotSpeed));
		}
		if(gpGame->GetInput()->IsTriggerd("AngleYDec")) {
			mtxTransform = cMath::MatrixMul(mtxTransform, cMath::MatrixRotateY(-fRotSpeed));
		}
		if(gpGame->GetInput()->IsTriggerd("AngleZDec")) {
			mtxTransform = cMath::MatrixMul(mtxTransform, cMath::MatrixRotateZ(-fRotSpeed));
		}

		if(gpGame->GetInput()->BecameTriggerd("View1"))
		{
			gpGame->GetScene()->SetCamera(gpCameraUpdate->GetCamera());
		}
		if(gpGame->GetInput()->BecameTriggerd("View2"))
		{
			gpGame->GetScene()->SetCamera(mpFPSCamera);
		}

		if(gpGame->GetInput()->BecameTriggerd("Print"))
		{
			Log("-------- PRINT ---------------------\n");
			Log("Pos: %.3f %.3f %.3f\n",mvPosition.x,mvPosition.y,mvPosition.z);
			Log("Rot: %.1f %.1f %.1f\n",cMath::ToDeg(mvRotation.x),cMath::ToDeg(mvRotation.y),cMath::ToDeg(mvRotation.z));
			Log("------------------------------------\n");
		}

		mvRotation = cMath::MatrixToEulerAngles(mtxTransform,eEulerRotationOrder_XYZ);

		cMatrixf mtxPose = cMath::MatrixRotate(mvRotation,eEulerRotationOrder_XYZ);
		mtxPose.SetTranslation(mvPosition);

		cVector3f vRot = cVector3f(mpFPSCamera->GetPitch(), mpFPSCamera->GetYaw(),mpFPSCamera->GetRoll());
		cMatrixf mtxSmoothCam = cMath::MatrixRotate(vRot * -1.0f, eEulerRotationOrder_YXZ);
		cVector3f vUp = mtxSmoothCam.GetUp();
		cVector3f vRight = mtxSmoothCam.GetRight();
		cVector3f vForward = mtxSmoothCam.GetForward()*-1.0f;

		////////////////////
		//Set rotation
		cMatrixf mtxEntity = cMath::MatrixMul(
								cMath::MatrixRotate(vRot, eEulerRotationOrder_XYZ),
								mtxPose.GetRotation()
									);

		/////////////////////////
		//Set position
		const cVector3f &vLocalPos = mtxPose.GetTranslation();
		cVector3f vRealLocalPos =	vUp *		vLocalPos.y +
									vRight *	vLocalPos.x +
									vForward *	vLocalPos.z;

		mtxEntity.SetTranslation(mpFPSCamera->GetPosition() + vRealLocalPos);

		mpEntity->SetMatrix(mtxEntity);
	}

	void OnDraw()
	{
		mpFont->Draw(cVector3f(5,5,0),13,cColor(1,1),eFontAlign_Left,_W("PosX: %.3f"),mvPosition.x);
		mpFont->Draw(cVector3f(5,5+14,0),13,cColor(1,1),eFontAlign_Left,_W("PosY: %.3f"),mvPosition.y);
		mpFont->Draw(cVector3f(5,5+28,0),13,cColor(1,1),eFontAlign_Left,_W("PosZ: %.3f"),mvPosition.z);

		mpFont->Draw(cVector3f(5+160,5,0),13,cColor(1,1),eFontAlign_Left,_W("RotX: %.1f"),cMath::ToDeg(mvRotation.x));
		mpFont->Draw(cVector3f(5+160,5+14,0),13,cColor(1,1),eFontAlign_Left,_W("RotY: %.1f"),cMath::ToDeg(mvRotation.y));
		mpFont->Draw(cVector3f(5+160,5+28,0),13,cColor(1,1),eFontAlign_Left,_W("RotZ: %.1f"),cMath::ToDeg(mvRotation.z));
	}

	void OnPostSceneDraw()
	{
		cCamera3D *pCam = static_cast<cCamera3D*>(gpGame->GetScene()->GetCamera());
		mpLowLevelGraphics->SetMatrix(eMatrix_ModelView, pCam->GetViewMatrix());
		mpLowLevelGraphics->SetDepthTestActive(false);

		mpLowLevelGraphics->SetBlendActive(false);

		mpLowLevelGraphics->SetDepthTestActive(false);
		mpLowLevelGraphics->SetDepthWriteActive(false);

		mpLowLevelGraphics->SetDepthTestActive(true);
		mpLowLevelGraphics->SetDepthWriteActive(true);

		mpFPSCamera->GetFrustum()->Draw(mpLowLevelGraphics);

		/*cVector3f vX = cVector3f(1,0,0);
		cVector3f vY = cVector3f(0,1,0);
		cVector3f vZ = cVector3f(0,0,1);

		vY = cMath::MatrixMul(cMath::MatrixRotate(cVector3f(mvRotation.x,0,0),eEulerRotationOrder_XYZ),vY);
		vZ = cMath::MatrixMul(cMath::MatrixRotate(cVector3f(mvRotation.x,mvRotation.y,0),eEulerRotationOrder_XYZ),vZ);

		mpLowLevelGraphics->DrawLine(0,vX * 0.2f,cColor(1,0,0));
		mpLowLevelGraphics->DrawLine(0,vY * 0.2f,cColor(0,1,0));
		mpLowLevelGraphics->DrawLine(0,vZ * 0.2f,cColor(0,0,1));*/
	}



private:
	cVector3f mvRot;
	cVector3f mvPos;
	iFontData* mpFont;

	cCamera3D* mpFPSCamera;

	cMeshEntity *mpEntity;

	iLowLevelGraphics* mpLowLevelGraphics;
	cWorld3D* mpWorld;

	iPhysicsWorld *mpPhysicsWorld;

	cBillboard *mpFlare;
	cBillboard *mpFlare2;

	iLight3D *mpLight;
	iLight3D *mpLight2;

	cVector3f mvPosition;
	cVector3f mvRotation;
};


/////////////////////////////////////////////////////


int hplMain(const tString& asCmdLine)
{
	// Load config file
	cConfigFile *pConfig = new cConfigFile(_W("viewer_settings.cfg"));
	pConfig->Load();

	int lScreenW = pConfig->GetInt("Screen","Width",800);
	int lScreenH = pConfig->GetInt("Screen","Height",600);
	bool bFullScreen = pConfig->GetBool("Screen", "FullScreen", false);
	bool bVsync = pConfig->GetBool("Screen", "Vsync", false);

	SetLogFile(_W("hoe.log"));

	//Init the game engine
	gpGame = new cGame(new cSDLGameSetup(),lScreenW,lScreenH,32,bFullScreen,45);
	gpGame->GetGraphics()->GetLowLevel()->SetVsyncActive(bVsync);

	SetWindowCaption("Model Viewer");

	//iResourceBase::SetLogCreateAndDelete(true);

	//Add resources
	gpGame->GetResources()->LoadResourceDirsFile("resources.cfg");

	if(asCmdLine != ""){
		gsModelFile = asCmdLine;
	}


	//Add updates
	gpCameraUpdate = new cHOECamera(gpGame,5,cVector3f(0,0,2),true);
	gpGame->GetUpdater()->AddUpdate("Default", gpCameraUpdate);

	cSimpleUpdate Update;
	gpGame->GetUpdater()->AddUpdate("Default", &Update);

	//Run the engine
	gpGame->Run();

	//Delete the engine
	delete gpGame;
	delete gpCameraUpdate;

	pConfig->SetInt("Screen","Width",lScreenW);
	pConfig->SetInt("Screen","Height",lScreenH);
	pConfig->SetBool("Screen", "FullScreen", bFullScreen);
	pConfig->SetBool("Screen", "Vsync", bVsync);

	pConfig->Save();
	delete pConfig;
}
