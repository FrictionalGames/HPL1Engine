/*
 * Copyright 2007-2010 (C) - Frictional Games
 *
 * This file is part of HPL1Engine
 *
 * For conditions of distribution and use, see copyright notice in LICENSE-tests
 */
#include <hpl.h>
#include <impl/SDLGameSetup.h>

#include <GL/GLee.h>

#include "../Common/SimpleCamera.h"


using namespace hpl;

cGame *gpGame=NULL;

class cSimpleUpdate : public iUpdateable
{
public:
	cSimpleUpdate() : iUpdateable("SimpleUpdate")
	{
		//gpGame->SetRenderOnce(true);
		//gpGame->GetGraphics()->GetRenderer3D()->SetDebugFlags(eRendererDebugFlag_LogRendering);


		//gpGame->GetGraphics()->GetRenderer3D()->SetDebugFlags(eRendererDebugFlag_DrawNormals);

														//eRendererDebugFlag_DisableLighting);
		//gpGame->GetGraphics()->GetRenderer3D()->SetDebugFlags(eRendererDebugFlag_DrawBoundingSphere);
		//gpGame->GetGraphics()->GetRenderer3D()->SetDebugFlags(eRendererDebugFlag_DrawBoundingBox);
		//gpGame->GetGraphics()->GetRenderer3D()->SetDebugFlags(eRendererDebugFlag_DrawLightBoundingBox);

		iMaterial::SetQuality(eMaterialQuality_High);

		mpLowLevelGraphics = gpGame->GetGraphics()->GetLowLevel();

		mpWorld = gpGame->GetScene()->CreateWorld3D("Test");
		gpGame->GetScene()->SetWorld3D(mpWorld);

		cMesh *pMesh =NULL;
		cMeshEntity *pBox = NULL;

		pMesh = gpGame->GetResources()->GetMeshManager()->CreateMesh("misc_rect.dae");
		if(pMesh==NULL) FatalError("Couldn't load mesh\n");

		mpFloor = mpWorld->CreateMeshEntity("Floor",pMesh);
		mpFloor->SetMatrix(cMath::MatrixMul(cMath::MatrixScale(7),
							cMath::MatrixRotate(cVector3f(0,0,0),eEulerRotationOrder_XYZ)));
		mpFloor->SetCastsShadows(false);

		pMesh = gpGame->GetResources()->GetMeshManager()->CreateMesh("mine_barrel.dae");
		cMeshEntity *pEntity = mpWorld->CreateMeshEntity("Test",pMesh);
		pEntity->SetCastsShadows(true);
		pEntity->SetPosition(cVector3f(0,0.5f,0));
		pEntity->SetVisible(true);

		/*cVector3f vScale;
		vScale.x = mpFloor->GetWorldMatrix().GetRight().Length();
		vScale.y = mpFloor->GetWorldMatrix().GetUp().Length();
		vScale.z = mpFloor->GetWorldMatrix().GetForward().Length();
		Log("SCALE: %s\n",vScale.ToString().c_str());*/


		//mpBox = pEntity;

		mpLight = mpWorld->CreateLightPoint("Light");
		mpLight->SetFarAttenuation(4);
		mpLight->SetDiffuseColor(cColor(0.0f,1.0f,1.0f,1.0));
		mpLight->SetCastShadows(true);
		mpLight->SetPosition(cVector3f(2.3f,2.2f,3.51f));
		mpLight->SetVisible(false);

		//Spot light create
		mpSpotLight = mpWorld->CreateLightSpot("SpotLight");
		iTexture *pTex = gpGame->GetResources()->GetTextureManager()->Create2D("test_gobo.tga",true);
		if(pTex==NULL) FatalError("Couldn't load spot light texture!\n");
		mpSpotLight->SetTexture(pTex);

		//Spot light setup
		mvSpotLightPos = cVector3f(1,2.5,1);
		mvSpotLightAngle = cVector3f(cMath::ToRad(-70),0,0);
		mpSpotLight->SetDiffuseColor(cColor(1,1,1,1));
		mpSpotLight->SetFOV(cMath::ToRad(85));
		mpSpotLight->SetPosition(cVector3f(0,1.5,-2));
		mpSpotLight->SetFarAttenuation(3);
		mpSpotLight->SetCastShadows(true);
		mpSpotLight->SetVisible(true);

		//Spot light gpu progs
		/*mpVertexProgram = gpGame->GetResources()->GetGpuProgramManager()->CreateProgram(
								"Diffuse_Light_Spot_vp.cg","main",eGpuProgramType_Vertex);
		mpFragmentProgram = gpGame->GetResources()->GetGpuProgramManager()->CreateProgram(
								"Diffuse_Light_Spot_fp.cg","main",eGpuProgramType_Fragment);

		if(mpFragmentProgram==NULL || mpVertexProgram==NULL)
		{
			FatalError("Frag or Vtx program loading failed!\n");
		}*/

		mpFlare = mpWorld->CreateBillboard("Flare",1,"misc_flare");
		mpFlare->SetPosition(mpLight->GetWorldPosition());
		mpFlare->SetVisible(true);

		gpGame->GetGraphics()->GetRenderer3D()->SetAmbientColor(cColor(0.2f,0.2f));

		//////////////////////////////////
		//Create input actions
		gpGame->GetInput()->AddAction(new cActionKeyboard("SpotForward",gpGame->GetInput(),eKey_UP));
		gpGame->GetInput()->AddAction(new cActionKeyboard("SpotBackward",gpGame->GetInput(),eKey_DOWN));
		gpGame->GetInput()->AddAction(new cActionKeyboard("SpotRight",gpGame->GetInput(),eKey_RIGHT));
		gpGame->GetInput()->AddAction(new cActionKeyboard("SpotLeft",gpGame->GetInput(),eKey_LEFT));
		gpGame->GetInput()->AddAction(new cActionKeyboard("SpotUp",gpGame->GetInput(),eKey_q));
		gpGame->GetInput()->AddAction(new cActionKeyboard("SpotDown",gpGame->GetInput(),eKey_e));

		gpGame->GetInput()->AddAction(new cActionKeyboard("SpotRotUp",gpGame->GetInput(),eKey_PAGEUP));
		gpGame->GetInput()->AddAction(new cActionKeyboard("SpotRotDown",gpGame->GetInput(),eKey_PAGEDOWN));

		gpGame->GetInput()->AddAction(new cActionKeyboard("SpotRotLeft",gpGame->GetInput(),eKey_HOME));
		gpGame->GetInput()->AddAction(new cActionKeyboard("SpotRotRight",gpGame->GetInput(),eKey_END));


		gpGame->GetInput()->AddAction(new cActionKeyboard("SpotFovUp",gpGame->GetInput(),eKey_INSERT));
		gpGame->GetInput()->AddAction(new cActionKeyboard("SpotFovDown",gpGame->GetInput(),eKey_DELETE));
	}

	~cSimpleUpdate()
	{
	}

	void Update(float afTimeStep)
	{
		if(mpSpotLight == NULL) return;

		if(gpGame->GetInput()->IsTriggerd("SpotForward"))mvSpotLightPos.z -= 1*afTimeStep;
		if(gpGame->GetInput()->IsTriggerd("SpotBackward"))mvSpotLightPos.z += 1*afTimeStep;
		if(gpGame->GetInput()->IsTriggerd("SpotRight"))mvSpotLightPos.x -= 1*afTimeStep;
		if(gpGame->GetInput()->IsTriggerd("SpotLeft"))mvSpotLightPos.x += 1*afTimeStep;
		if(gpGame->GetInput()->IsTriggerd("SpotUp"))mvSpotLightPos.y += 1*afTimeStep;
		if(gpGame->GetInput()->IsTriggerd("SpotDown"))mvSpotLightPos.y -= 1*afTimeStep;


		if(gpGame->GetInput()->IsTriggerd("SpotRotUp"))mvSpotLightAngle.x += cMath::ToRad(90)*afTimeStep;
		if(gpGame->GetInput()->IsTriggerd("SpotRotDown"))mvSpotLightAngle.x -= cMath::ToRad(90)*afTimeStep;

		if(gpGame->GetInput()->IsTriggerd("SpotRotLeft"))mvSpotLightAngle.y += cMath::ToRad(90)*afTimeStep;
		if(gpGame->GetInput()->IsTriggerd("SpotRotRight"))mvSpotLightAngle.y -= cMath::ToRad(90)*afTimeStep;

		float fFov = mpSpotLight->GetFOV();
		if(gpGame->GetInput()->IsTriggerd("SpotFovUp")) fFov += cMath::ToRad(20)*afTimeStep;
		if(gpGame->GetInput()->IsTriggerd("SpotFovDown")) fFov -= cMath::ToRad(20)*afTimeStep;
		mpSpotLight->SetFOV(fFov);

		cMatrixf mtxSpotLight = cMath::MatrixRotate(mvSpotLightAngle,eEulerRotationOrder_XYZ);
		mtxSpotLight.SetTranslation(mvSpotLightPos);
		mpSpotLight->SetMatrix(mtxSpotLight);
	}

	void OnDraw()
	{

	}

	void OnPostSceneDraw()
	{
		if(mpSpotLight==NULL)return;

		cCamera3D *pCam = static_cast<cCamera3D*>(gpGame->GetScene()->GetCamera());

		mpLowLevelGraphics->SetMatrix(eMatrix_ModelView,pCam->GetViewMatrix());

		//Draw the Spotlight!
		mpLowLevelGraphics->SetStencilActive(false);
		mpLowLevelGraphics->DrawSphere(mpSpotLight->GetWorldPosition(),0.2f,cColor(0,1,1));
		mpLowLevelGraphics->DrawLine(mpSpotLight->GetWorldPosition(),
			mpSpotLight->GetWorldPosition() +
			mpSpotLight->GetViewMatrix().GetForward()*mpSpotLight->GetFarAttenuation()*-1,
			cColor(1,1,0));

		//mpLowLevelGraphics->DrawBoxMaxMin(mpSpotLight->GetBoundingVolume()->GetMax(),
		//									mpSpotLight->GetBoundingVolume()->GetMin(),
		//									cColor(1,1,1,1));

		mpSpotLight->GetFrustum()->Draw(mpLowLevelGraphics);
		//cSpheref sphere = mpSpotLight->GetFrustum()->GetBoundingSphere();
		//mpLowLevelGraphics->DrawSphere(sphere.center, sphere.r, cColor(1,0,1,1));

		/*
		cMeshEntity* pEntities[]={mpFloor, mpBox};
		for(int i=0; i<2; i++)
		{
			//Draw an overlay texture on the floor
			cMeshEntity *pEntity = pEntities[i];

			iVertexBuffer *pVtxBuff = pEntity->GetSubMeshEntity(0)->GetVertexBuffer();
			iTexture *pSpotTex = mpSpotLight->GetTexture();

			//Set model view matrix
			//mpLowLevelGraphics->SetMatrix(eMatrix_ModelView,
			//						cMath::MatrixMul(pCam->GetViewMatrix(), pEntity->GetWorldMatrix()));

			//Set texture
			mpLowLevelGraphics->SetTexture(0, pSpotTex);

			//Set Blend mode
			mpLowLevelGraphics->SetBlendActive(true);
			mpLowLevelGraphics->SetBlendFunc(eBlendFunc_One,eBlendFunc_One);//eBlendFunc_DestColor, eBlendFunc_Zero);

			//GPU programs
			//mpVertexProgram->SetMatrixf("worldViewProj",
			//					eGpuProgramMatrix_ViewProjection,
			//					eGpuProgramMatrixOp_Identity);
			cMatrixf mtxModelView = cMath::MatrixMul(pCam->GetViewMatrix(), pEntity->GetWorldMatrix());
			mpVertexProgram->SetMatrixf("worldViewProj",
								cMath::MatrixMul(pCam->GetProjectionMatrix(), mtxModelView));

			mpVertexProgram->SetMatrixf("spotViewProj",
								cMath::MatrixMul(mpSpotLight->GetViewProjMatrix(), pEntity->GetWorldMatrix()));

			mpVertexProgram->Bind();
			mpFragmentProgram->Bind();

			//Vertex buffer
			pVtxBuff->Bind();

			pVtxBuff->Draw();

			//Reset
			pVtxBuff->UnBind();

			mpFragmentProgram->UnBind();
			mpVertexProgram->UnBind();

			mpLowLevelGraphics->SetTexture(0, NULL);
			mpLowLevelGraphics->SetBlendActive(false);
		}*/
	}


private:
	cMeshEntity *mpFloor;
	cMeshEntity *mpBox;

	iFontData *mpFont;

	iLowLevelGraphics* mpLowLevelGraphics;
	cWorld3D* mpWorld;

	cBillboard *mpFlare;

	iLight3D *mpLight;

	cLight3DSpot *mpSpotLight;
	cVector3f mvSpotLightPos;
	cVector3f mvSpotLightAngle;
	iGpuProgram *mpVertexProgram, *mpFragmentProgram;
};



int hplMain(const tString& asCommandLine)
{
	iResourceBase::SetLogCreateAndDelete(true);
	iGpuProgram::SetLogDebugInformation(true);

	//Init the game engine
	gpGame = new cGame(new cSDLGameSetup(),800,600,32,false,45);
	gpGame->GetGraphics()->GetLowLevel()->SetVsyncActive(false);

	//Add resources
	gpGame->GetResources()->LoadResourceDirsFile("resources.cfg");

	//Add updates
	cSimpleUpdate Update;
	gpGame->GetUpdater()->AddUpdate("Default", &Update);

	cSimpleCamera cameraUpdate(gpGame,10,cVector3f(0,2,5),true);
	gpGame->GetUpdater()->AddUpdate("Default", &cameraUpdate);

	//Run the engine
	gpGame->Run();

	//Delete the engine
	delete gpGame;

	return 0;
}
