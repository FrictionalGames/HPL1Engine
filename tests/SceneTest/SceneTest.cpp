/*
 * Copyright 2007-2010 (C) - Frictional Games
 *
 * This file is part of HPL1Engine
 *
 * For conditions of distribution and use, see copyright notice in LICENSE-tests
 */
#include <hpl.h>
#include <impl/SDLGameSetup.h>

#pragma comment(lib, "HPL.lib")

#include "SceneCamera.h"


using namespace hpl;

cGame *gpGame=NULL;

cSceneCamera *gpCameraUpdate=NULL;

class cSimpleUpdate : public iUpdateable, public iPhysicsRayCallback
{
public:
	cSimpleUpdate() : iUpdateable("Simpleupdate")
	{
		//gpGame->SetRenderOnce(true);
		//gpGame->GetGraphics()->GetRenderer3D()->SetDebugFlags(eRendererDebugFlag_LogRendering);
		//gpGame->GetGraphics()->GetRenderer3D()->SetDebugFlags(eRendererDebugFlag_DrawLightBoundingBox);

		//gpGame->GetGraphics()->GetRenderer3D()->SetDebugFlags(eRendererDebugFlag_DrawNormals);
		//gpGame->GetGraphics()->GetRenderer3D()->SetDebugFlags(eRendererDebugFlag_DrawBoundingBox);

		mpLowLevelGraphics = gpGame->GetGraphics()->GetLowLevel();

		//Add the engine's obejct plugin.
		gpGame->GetResources()->AddEntity3DLoader(new cEntityLoader_Object("Object"));

		//Setup physics
		gpGame->GetPhysics()->LoadSurfaceData("materials.cfg");

		mpWorld = gpGame->GetResources()->GetMeshLoaderHandler()->LoadWorld("maps/maptest0_complex.dae",0);
		//mpWorld = gpGame->GetResources()->GetMeshLoaderHandler()->LoadWorld("maps/simple.dae");
		//mpWorld = gpGame->GetResources()->GetMeshLoaderHandler()->LoadWorld("maps/testlevel2.dae");

		gpGame->GetInput()->AddAction(new cActionKeyboard("Shadows",gpGame->GetInput(),eKey_1));

		gpGame->GetScene()->SetWorld3D(mpWorld);

		//Create crate
		/*cMeshEntity *pEntity;
		iCollideShape *pShape;
		iPhysicsBody *pBody;
		cMesh* pMesh = gpGame->GetResources()->GetMeshManager()->CreateMesh("Woodbox.dae");
		if(pMesh==NULL) FatalError("Couldn't load mesh\n");

		pEntity = mpWorld->CreateMeshEntity("Box1",pMesh);
		pEntity->SetCastsShadows(true);

		pShape = pMesh->CreateCollideShape(mpWorld->GetPhysicsWorld());
		pBody = mpWorld->GetPhysicsWorld()->CreateBody("Box",pShape);
		pBody->SetPosition(cVector3f(-0.0f,1.7f,0));
		pBody->SetMass(1);
		pBody->CreateNode()->AddEntity(pEntity);*/

		mpFont = gpGame->GetResources()->GetFontManager()->CreateFontData("viewer.fnt");
	}

	~cSimpleUpdate()
	{

	}

	void Update(float afFrameTime)
	{
		//Log("Update!\n");
		//if(gpGame->GetInput()->BecameTriggerd("Shadows")){
		//	bool bShadows = gpGame->GetGraphics()->GetRenderer3D()->GetShowShadows();
		//	gpGame->GetGraphics()->GetRenderer3D()->SetShowShadows(!bShadows);
		//}

		static bool bTest =true;

		if(bTest)
		{
			bTest = false;

			cVector3f vStart = cVector3f(-16,0.6,16);
			cVector3f vEnd = cVector3f(16,0.6,-16);

			mlPreTested =0;
			mlTested =0;
			mbPretest = true;
			unsigned long lStartTime = gpGame->GetSystem()->GetLowLevel()->GetTime();
			for(int i=0; i< 10000; ++i)
				mpWorld->GetPhysicsWorld()->CastRay(this,vStart,vEnd,true,true,true,true);

			unsigned long lTime = gpGame->GetSystem()->GetLowLevel()->GetTime() - lStartTime;

			Log("Time with pretest: %d tested: %d pretested: %d\n",lTime,mlTested/10000,
																	mlPreTested/10000);

			mlPreTested =0;
			mlTested =0;
			mbPretest = false;
			lStartTime = gpGame->GetSystem()->GetLowLevel()->GetTime();
			for(int i=0; i< 10000; ++i)
				mpWorld->GetPhysicsWorld()->CastRay(this,vStart,vEnd,true,true,true,false);

			lTime = gpGame->GetSystem()->GetLowLevel()->GetTime() - lStartTime;
			Log("Time with out pretest: %d tested: %d\n",lTime,mlTested/10000);
		}
	}

	bool BeforeIntersect(iPhysicsBody *pBody)
	{
		mlPreTested++;

		if(pBody->IsCharacter())return false;
		if(!pBody->GetCollide())return false;
		if(pBody->GetMass()== 0)return false;
		if(!pBody->GetCollideCharacter())return false;

		//Log("PreIntersected: '%s'\n",pBody->GetName().c_str());
		return true;
	}

	bool OnIntersect(iPhysicsBody *pBody,cPhysicsRayParams *apParams)
	{
		mlTested++;

		if(mbPretest==false)
		{
			if(pBody->IsCharacter())x = false;
			if(!pBody->HasCollision())x = false;
			if(pBody->GetMass()> 0)x = false;
			if(!pBody->GetCollideCharacter())x = false;
		}

		//Log("RealIntersected: '%s'\n",pBody->GetName().c_str());
		return true;
	}

	void OnDraw()
	{
	}

	void OnPostSceneDraw()
	{
		//return;
		cCamera3D *pCam = static_cast<cCamera3D*>(gpGame->GetScene()->GetCamera());
		mpLowLevelGraphics->SetMatrix(eMatrix_ModelView, pCam->GetViewMatrix());

		mpLowLevelGraphics->SetTexture(0,NULL);
		mpLowLevelGraphics->SetBlendActive(false);

		//mpLowLevelGraphics->SetDepthTestActive(false);
		//mpLowLevelGraphics->SetDepthWriteActive(false);

		//mpWorld->GetPhysicsWorld()->RenderDebugGeometry(mpLowLevelGraphics,cColor(1,1,1));

		cBoundingVolume *pBV = gpCameraUpdate->mpCharBody->GetBody()->GetBV();
		cPortalContainerEntityIterator entIt = mpWorld->GetPortalContainer()->GetEntityIterator(pBV);
		while(entIt.HasNext())
		{
			iPhysicsBody *pBody = static_cast<iPhysicsBody*>(entIt.Next());
			if(pBody->IsCharacter()==false)
				pBody->RenderDebugGeometry(mpLowLevelGraphics,cColor(1,1,1));
		}

		cVector3f vStart = cVector3f(-16,0.6,16);
		cVector3f vEnd = cVector3f(16,0.6,-16);

		mpLowLevelGraphics->DrawLine(vStart,vEnd,cColor(1,0.5f,0,0));

		mpLowLevelGraphics->SetDepthTestActive(true);
		mpLowLevelGraphics->SetDepthWriteActive(true);

		return;
		////////////////////////////////////////////
		// Draw boxes around sectors and portals
		if(mpWorld ==NULL) return;

		tSectorMap *pSectorMap = mpWorld->GetPortalContainer()->GetSectorMap();
		tSectorMapIt SectorIt = pSectorMap->begin();
		for(; SectorIt != pSectorMap->end(); SectorIt++)
		{
			cSector *pSector = SectorIt->second;

			mpLowLevelGraphics->DrawBoxMaxMin(pSector->GetBV()->GetMax(),pSector->GetBV()->GetMin(),
												cColor(1,0,1,1));

			tPortalList *pPortalList = pSector->GetPortalList();
			tPortalListIt PortIt = pPortalList->begin();
			for(; PortIt != pPortalList->end(); PortIt++)
			{
				cPortal *pPortal = *PortIt;

				mpLowLevelGraphics->DrawBoxMaxMin(pPortal->GetBV()->GetMax(),pPortal->GetBV()->GetMin(),
					cColor(0,1,1,1));

				cVector3f vStart = pPortal->GetBV()->GetWorldCenter();
				cVector3f vEnd = vStart + pPortal->GetNormal()*0.1f;

				mpLowLevelGraphics->DrawLine(vStart, vEnd,cColor(0,1,1,1));
			}
		}
	}


private:
	float mfLightAngle;
	iFontData *mpFont;

	cWorld3D* mpWorld;
	iLowLevelGraphics* mpLowLevelGraphics;

	bool mbPretest;
	bool x;

	int mlTested,mlPreTested;
};



int hplMain(const tString &asCommandLine)
{
	//Init the game engine
	gpGame = new cGame(new cSDLGameSetup(),800,600,32,false,45);
	gpGame->GetGraphics()->GetLowLevel()->SetVsyncActive(false);

	//Add resources
	gpGame->GetResources()->AddResourceDir("textures");
	gpGame->GetResources()->AddResourceDir("models");
	gpGame->GetResources()->AddResourceDir("fonts");
	gpGame->GetResources()->AddResourceDir("maps");

	//Add updates
   	cSimpleUpdate Update;
	gpGame->GetUpdater()->AddUpdate("Default", &Update);
	gpCameraUpdate = new cSceneCamera(gpGame,4,cVector3f(0,1.5f,0.0f),true);
	gpGame->GetUpdater()->AddUpdate("Default", gpCameraUpdate);

	//Run the engine
	gpGame->Run();

	//Delete the engine
	delete gpGame;
	delete gpCameraUpdate;
}
