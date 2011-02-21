/*
 * Copyright 2007-2010 (C) - Frictional Games
 *
 * This file is part of HPL1Engine
 *
 * For conditions of distribution and use, see copyright notice in LICENSE-tests
 */
#include "hpl.h"
#include "impl/SDLGameSetup.h"

#include "../Common/SimpleCamera.h"

#pragma comment(lib, "HPL.lib")

using namespace hpl;

cGame *gpGame=NULL;

class cSimpleUpdate : public iUpdateable
{
public:
	cSimpleUpdate() : iUpdateable("SimpleUpdate")
	{
		//gpGame->SetRenderOnce(true);

		//gpGame->GetGraphics()->GetRenderer3D()->SetDebugFlags(eRendererDebugFlag_DrawNormals |
		//												eRendererDebugFlag_DrawTangents );
														//eRendererDebugFlag_DisableLighting);
		//gpGame->GetGraphics()->GetRenderer3D()->SetDebugFlags(eRendererDebugFlag_LogRendering);
		//gpGame->GetGraphics()->GetRenderer3D()->SetDebugFlags(eRendererDebugFlag_DrawBoundingBox);
		//gpGame->GetGraphics()->GetRenderer3D()->SetDebugFlags(eRendererDebugFlag_DrawLightBoundingBox);


		mpLowLevelGraphics = gpGame->GetGraphics()->GetLowLevel();

		gpGame->GetPhysics()->LoadSurfaceData("materials.cfg");

		mpWorld = gpGame->GetScene()->CreateWorld3D("Test");
		gpGame->GetScene()->SetWorld3D(mpWorld);

		mpPhysicsWorld = gpGame->GetPhysics()->CreateWorld(true);
		mpPhysicsWorld->SetWorldSize(300,-300);
		mpPhysicsWorld->SetMaxTimeStep(1.0f / 60.0f);

		mpWorld->SetPhysicsWorld(mpPhysicsWorld);


		////////////////////////////////////////////////
		// Create materials

		////////////////////////////////////////////////
		// Create bodies and meshes
		cMesh *pMesh =NULL;
		cMeshEntity *pBox = NULL;
		cMeshEntity *pEntity=NULL;
		iCollideShape *pShape = NULL;
		iPhysicsBody *pBody = NULL;

		///////////////////////////////////////////////
		//Floor
		pMesh = gpGame->GetResources()->GetMeshManager()->CreateMesh("Floor.dae");
		if(pMesh==NULL) FatalError("Couldn't load mesh\n");

		mpFloorModel = mpWorld->CreateMeshEntity("Floor",pMesh);
		pShape = mpPhysicsWorld->CreateMeshShape(pMesh->GetSubMesh(0)->GetVertexBuffer());
		mpFloorBody = mpPhysicsWorld->CreateBody("Floor",pShape);
		mpFloorBody->SetGravity(false);

		/////////////////////////////
		//Boxes
		pMesh = gpGame->GetResources()->GetMeshManager()->CreateMesh("Woodbox.dae");
		if(pMesh==NULL) FatalError("Couldn't load mesh\n");
		int j=0;
		for(int i=0; i<2; i++)
		{
			pMesh->IncUserCount();
			pEntity = mpWorld->CreateMeshEntity("Box1",pMesh);
			pEntity->SetCastsShadows(true);

			pShape = pMesh->CreateCollideShape(mpPhysicsWorld);
			pBody = mpPhysicsWorld->CreateBody("Box",pShape);
			pBody->SetPosition(cVector3f((float)j*1.1f,2.0f*i+2.0f,cMath::RandRectf(0,0.5f)));
			pBody->SetMass(1);

			mvBoxes.push_back(pEntity);
			mvBodies.push_back(pBody);
			j++;
		}

		mpLight = mpWorld->CreateLightPoint("Light1");
		mpLight->SetDiffuseColor(cColor(1,1,1,1.0f));
		mpLight->SetFarAttenuation(280.0f);
		mpLight->SetVisible(true);
		mpLight->SetCastShadows(true);
		mpLight->SetPosition(cVector3f(50,50,50));
	}

	~cSimpleUpdate()
	{

	}

	void Update(float afTimeStep)
	{
		mpPhysicsWorld->Simulate(afTimeStep);

		for(size_t i=0; i< mvBodies.size(); i++)
		{
			mvBoxes[i]->SetMatrix(mvBodies[i]->GetLocalMatrix());
		}
	}

	void OnDraw()
	{
	}

	void OnPostSceneDraw()
	{
		cCamera3D *pCam = static_cast<cCamera3D*>(gpGame->GetScene()->GetCamera());
		mpLowLevelGraphics->SetMatrix(eMatrix_ModelView, pCam->GetViewMatrix());

		mpLowLevelGraphics->SetDepthTestActive(false);

		mpPhysicsWorld->RenderDebugGeometry(mpLowLevelGraphics,cColor(1,0,1,1));

//		mpLowLevelGraphics->DrawBoxMaxMin(mpFloorBody->GetBV()->GetMax(),
//											mpFloorBody->GetBV()->GetMin(),cColor(1,1,1,1));
//		for(size_t i=0; i< mvBodies.size(); i++)
//		{
//			mpLowLevelGraphics->DrawBoxMaxMin(mvBodies[i]->GetBV()->GetMax(),
//												mvBodies[i]->GetBV()->GetMin(), cColor(1,1,1,1));
//		}
//
//		mpLowLevelGraphics->SetDepthTestActive(true);
	}



private:

	std::vector<cMeshEntity*> mvBoxes;
	std::vector<iPhysicsBody*> mvBodies;

	iPhysicsBody *mpFloorBody;
	cMeshEntity *mpFloorModel;

	iLowLevelGraphics* mpLowLevelGraphics;
	cWorld3D* mpWorld;

	cBillboard *mpFlare;
	cBillboard *mpFlare2;

	iLight3D *mpLight;
	iLight3D *mpLight2;
	cVector3f mvFlareRotation;

	iPhysicsWorld *mpPhysicsWorld;

	cVector3f mvMeshRotation;

};



int hplMain(const tString& asCommandLine)
{
	//Init the game engine
	gpGame = new cGame(new cSDLGameSetup(),800,600,32,false,45);
	gpGame->GetGraphics()->GetLowLevel()->SetVsyncActive(false);

	//Add resources
	gpGame->GetResources()->AddResourceDir("textures");
	gpGame->GetResources()->AddResourceDir("models");
	gpGame->GetResources()->AddResourceDir("fonts");
    gpGame->GetResources()->AddResourceDir("gui");

	//Add updates
	cSimpleUpdate Update;
	gpGame->GetUpdater()->AddUpdate("Default", &Update);

	cSimpleCamera cameraUpdate(gpGame,17,cVector3f(0,4,15),true);
	gpGame->GetUpdater()->AddUpdate("Default", &cameraUpdate);

	//Run the engine
	gpGame->Run();

	//Delete the engine
	delete gpGame;
    return 0;
}
