/*
 * Copyright 2007-2010 (C) - Frictional Games
 *
 * This file is part of HPL1Engine
 *
 * For conditions of distribution and use, see copyright notice in LICENSE-tests
 */
#include <hpl.h>
#include <impl/SDLGameSetup.h>

#include "../Common/SimpleCamera.h"

#pragma comment(lib, "libogg.lib")
#pragma comment(lib, "libtheora.lib")

#include <theora/theora.h>


using namespace hpl;

cGame *gpGame=NULL;
cSimpleCamera *gpSimpleCam;

class cSimpleUpdate : public iUpdateable
{
public:

	//--------------------------------------------------

	tString msFile;

	iTexture *mpVideoTexture;
	tVertexVec mvTexRectVtx;

	iVideoStream *mpVideoStream;

	int vVec1[256][256][256];
	int vVec2[256][256][256];

	int* pVec1;
	int* pVec2;

	//--------------------------------------------------

	cSimpleUpdate() : iUpdateable("SimpleUPdate")
	{
		////////////////////////////////
		//Scene Init
		mpLowLevelGraphics = gpGame->GetGraphics()->GetLowLevel();

		cVector2f vScreenSize = mpLowLevelGraphics->GetScreenSize();
		mpLowLevelGraphics->SetVirtualSize(cVector2f((float)vScreenSize.x, (float)vScreenSize.y));

		gpGame->GetGraphics()->GetRenderer3D()->SetAmbientColor(cColor(0.0f,0.0f, 0.0f, 0.0f));


		mpWorld = gpGame->GetScene()->CreateWorld3D("Test");
		gpGame->GetScene()->SetWorld3D(mpWorld);

		cMesh *pMesh = gpGame->GetResources()->GetMeshManager()->CreateMesh("misc_rect.dae");
		if(pMesh==NULL) FatalError("Couldn't load mesh\n");

		cMeshEntity *pEntity = mpWorld->CreateMeshEntity("Floor",pMesh);
		pEntity->SetMatrix(cMath::MatrixScale(6));
		pEntity->SetVisible(true);
		pEntity->SetCastsShadows(false);

		cLight3DPoint *pLight = mpWorld->CreateLightPoint("Light1");
		pLight->SetDiffuseColor(cColor(1,1,1,1.0f));
		pLight->SetFarAttenuation(280.0f);
		pLight->SetVisible(true);
		pLight->SetCastShadows(false);
		pLight->SetPosition(cVector3f(8,8,8));


		////////////////////////////////
		//Input Init
		gpGame->GetInput()->AddAction(new cActionKeyboard("Reset",gpGame->GetInput(),eKey_r));

		//return;
		////////////////////////////////
		//Theora Init
		msFile = "textures/test_video2.ogm";


		////////////////////////////////
		//Theora Setup video texture
		mpVideoStream = gpGame->GetResources()->GetVideoManager()->CreateVideo("test_video2.ogm");
		mpVideoStream->Play();
		mpVideoStream->SetLoop(true);


		if(mpVideoStream)
		{
			cVector2l vVideoSize = mpVideoStream->GetSize();
			mpVideoTexture = gpGame->GetGraphics()->GetLowLevel()->CreateTexture(vVideoSize,32,cColor(0,1),false,
																				eTextureType_Normal,eTextureTarget_Rect);
			mvTexRectVtx.resize(4);
		}
		else
		{
			mpVideoTexture = NULL;
		}

		//ArraySpeedTest();

		//gpGame->GetSound()->GetMusicHandler()->Play("test_video2.ogm",1,1,false);

	}

	//------------------------------------------

	~cSimpleUpdate()
	{
		if(mpVideoTexture)delete mpVideoTexture;
	}

	//------------------------------------------


	void Update(float afTimeStep)
	{
		if(mpVideoStream)
		{
			mpVideoStream->CopyToTexture(mpVideoTexture);
		}
	}


	//--------------------------------------------------

	void OnPostSceneDraw()
	{
		if(mpVideoStream==NULL) return;

		cVector2f vVideoSize((float)mpVideoStream->GetSize().x, (float)mpVideoStream->GetSize().y);


		mpLowLevelGraphics->SetMatrix(eMatrix_ModelView,cMatrixf::Identity);
		mpLowLevelGraphics->SetOrthoProjection(mpLowLevelGraphics->GetVirtualSize(),-1000,1000);

		mpLowLevelGraphics->SetBlendActive(false);

		mpLowLevelGraphics->SetDepthTestActive(false);
		//mpLowLevelGraphics->SetDepthWriteActive(false);

		///////////////////////////////////
		//Draw texture
		mpLowLevelGraphics->SetTexture(0, mpVideoTexture);

		cVector3f vPos = cVector3f(200,100,0);
		mvTexRectVtx[0] = cVertex(vPos+cVector3f(0,0,0),cVector2f(0,0),cColor(1,1.0f) );
		mvTexRectVtx[1] = cVertex(vPos+cVector3f(vVideoSize.x,0,0),cVector2f(vVideoSize.x,0),cColor(1,1.0f));
		mvTexRectVtx[2] = cVertex(vPos+cVector3f(vVideoSize.x,vVideoSize.y,0),cVector2f(vVideoSize.x,vVideoSize.y),cColor(1,1.0f));
		mvTexRectVtx[3] = cVertex(vPos+cVector3f(0,vVideoSize.y,0),cVector2f(0,vVideoSize.y),cColor(1,1.0f));

		mpLowLevelGraphics->DrawQuad(mvTexRectVtx);

		mpLowLevelGraphics->SetTexture(0, NULL);

		//mpLowLevelGraphics->SetDepthTestActive(true);

		//mpLowLevelGraphics->SetMatrix(eMatrix_Projection,((cCamera3D*)gpGame->GetScene()->GetCamera())->GetProjectionMatrix());
	}

	//--------------------------------------------------

	void ArraySpeedTest()
	{
		////////////////////////////////
		//A speed test
		pVec1 = new int[256*256*256];
		pVec2 = new int[256*256*256];

		int lTestRuns = 3;
		int lTestIterations = 1;
		const int max_val = 255;

		/*Log("--- Normal array test [x][y] ----\n");
		for(int j=0; j<lTestRuns; j++)
		{
		unsigned long lStartTime = GetApplicationTime();
		for(int i=0; i<lTestIterations; ++i)
		{
		int x=max_val;
		int y=max_val;
		while(y)
		{
		vVec1[x][y] = vVec2[x][y];

		x--;
		if(!x){
		y--;
		x = max_val;
		}
		}
		}
		Log("Time %d: %dms\n",j,GetApplicationTime() - lStartTime);
		}*/
		Log("--- Normal array test [y][x] ----\n");
		for(int j=0; j<lTestRuns; j++)
		{
			unsigned long lStartTime = GetApplicationTime();
			for(int i=0; i<lTestIterations; ++i)
			{
				int x=max_val;
				int y=max_val;
				int z=max_val;
				while(z)
				{
					vVec1[y][x][z] = vVec2[y][x][z];

					--x;
					if(!x){
						--y;
						x = max_val;
						if(!y)
						{
							--z;
							y = max_val;
						}
					}
				}
			}
			Log("Time %d: %dms\n",j,GetApplicationTime() - lStartTime);
		}
		Log("--- Pointer array test, using mul ----\n");
		for(int j=0; j<lTestRuns; j++)
		{
			unsigned long lStartTime = GetApplicationTime();
			for(int i=0; i<lTestIterations; ++i)
			{
				int x=max_val;
				int y=max_val;
				int z=max_val;
				int maxvalpow = max_val*max_val;
				while(z)
				{
					pVec1[x + y*max_val + z*maxvalpow] = pVec2[x + y*max_val + z*maxvalpow];

					--x;
					if(!x){
						--y;
						x = max_val;
						if(!y)
						{
							--z;
							y = max_val;
						}
					}
				}
			}
			Log("Time %d: %dms\n",j,GetApplicationTime() - lStartTime);
		}
		Log("--- Pointer array test, usning shift << ----\n");
		for(int j=0; j<lTestRuns; j++)
		{
			unsigned long lStartTime = GetApplicationTime();
			for(int i=0; i<lTestIterations; ++i)
			{
				int x=max_val;
				int y=max_val;
				int z=max_val;
				while(z)
				{
					pVec1[x + (y<<8) + (z<<16)] = pVec2[x + (y<<8)+(z<<16)];

					--x;
					if(!x){
						--y;
						x = max_val;
						if(!y)
						{
							--z;
							y = max_val;
						}
					}
				}
			}
			Log("Time %d: %dms\n",j,GetApplicationTime() - lStartTime);
		}
		Log("--- Pointer array test, usning shift >> and no [] ----\n");
		for(int j=0; j<lTestRuns; j++)
		{
			unsigned long lStartTime = GetApplicationTime();
			for(int i=0; i<lTestIterations; ++i)
			{
				int x=max_val;
				int y=max_val;
				int z=max_val;
				while(z)
				{
					*(pVec1 +x + (y>>8) + (z>>16)) = *(pVec2 + x + (y>>8)+(z>>16));

					--x;
					if(!x){
						--y;
						x = max_val;
						if(!y)
						{
							--z;
							y = max_val;
						}
					}
				}
			}
			Log("Time %d: %dms\n",j,GetApplicationTime() - lStartTime);
		}

		//Delete stuff
		delete [] pVec1;
		delete [] pVec2;

	}

private:
	iLowLevelGraphics* mpLowLevelGraphics;
	cWorld3D* mpWorld;

	cGui *mpGui;
};



int hplMain(const tString &asCommandline)
{
	//Init the game engine
	gpGame = new cGame(new cSDLGameSetup(),1024,768,32,false,45);
	gpGame->GetGraphics()->GetLowLevel()->SetVsyncActive(false);
	gpGame->SetLimitFPS(false);

	//Add resources
	gpGame->GetResources()->LoadResourceDirsFile("resources.cfg");


	//Add updates
	cSimpleUpdate *pUpdate = new cSimpleUpdate();
	gpGame->GetUpdater()->AddUpdate("Default", pUpdate);

	gpSimpleCam = new cSimpleCamera(gpGame,8,cVector3f(0,2,9),true);
	gpGame->GetUpdater()->AddUpdate("Default", gpSimpleCam);

	//Run the engine
	gpGame->Run();

	delete pUpdate;
	delete gpSimpleCam;

	//Delete the engine
	delete gpGame;

	return 0;
}
