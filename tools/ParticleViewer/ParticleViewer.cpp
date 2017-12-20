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

#include "PVCamera.h"

//#pragma comment(lib, "HPL.lib")

using namespace hpl;



cGame *gpGame=NULL;

tString gsModelFile = "test.ps";
cVector3f gvRoomSize = cVector3f(7,7,7);
bool gbShowRoom = true;


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

		gpGame->GetInput()->AddAction(new cActionKeyboard("Bloom",gpGame->GetInput(),eKey_b));

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

		mpPS = mpWorld->CreateParticleSystem("PS",gsModelFile,1,cMatrixf::Identity);


		/////////////////////////////
		//Load Particle system XML
		tString sFile = gpGame->GetResources()->GetFileSearcher()->GetFilePath(gsModelFile);

		TiXmlDocument* pXmlDoc = new TiXmlDocument(sFile.c_str());
		if(pXmlDoc->LoadFile()==false)
		{
			Warning("Couldn't open XML file %s\n",sFile.c_str());
			delete pXmlDoc;
			return;
		}

		TiXmlElement *pRootElem = pXmlDoc->RootElement();

		gbShowRoom = cString::ToBool(pRootElem->Attribute("ShowRoom"),false);
		gvRoomSize = cString::ToVector3f(pRootElem->Attribute("RoomSize"),6);

		delete pXmlDoc;


		/////////////////////////////
		//Particle system

		//mpPS = mpWorld->CreateParticleSystem("PS",gsModelFile,1,cMatrixf::Identity);

		/////////////////////////////
		//Creates Room
		if(gbShowRoom==false) return;

		cMesh *pMesh = gpGame->GetResources()->GetMeshManager()->CreateMesh("misc_rect.dae");
		if(pMesh==NULL) FatalError("Couldn't load mesh\n");

		cMeshEntity *pWall = NULL;
		iPhysicsBody *pBody = NULL;
		iCollideShape *pShape = NULL;

		//Floor
		pWall = mpWorld->CreateMeshEntity("Floor",pMesh);
		pWall->SetMatrix(cMath::MatrixScale(cVector3f(gvRoomSize.x,gvRoomSize.y,gvRoomSize.z)*0.5f));
		pWall->SetPosition(cVector3f(0,-gvRoomSize.y/2,0));

		pShape = mpPhysicsWorld->CreateBoxShape(cVector3f(gvRoomSize.x,0.1f,gvRoomSize.z),NULL);
		pBody = mpPhysicsWorld->CreateBody("",pShape);
		pBody->SetPosition(cVector3f(0,-gvRoomSize.y/2 - 0.05f,0));


		//Roof
		pMesh->IncUserCount();
		pWall = mpWorld->CreateMeshEntity("Floor",pMesh);
		pWall->SetMatrix(cMath::MatrixScale(cVector3f(gvRoomSize.x,gvRoomSize.y,gvRoomSize.z)*0.5f));
		pWall->SetMatrix(cMath::MatrixMul(pWall->GetLocalMatrix(), cMath::MatrixRotateX(kPif)));
		pWall->SetPosition(cVector3f(0,gvRoomSize.y/2,0));

		pShape = mpPhysicsWorld->CreateBoxShape(cVector3f(gvRoomSize.x,0.1f,gvRoomSize.z),NULL);
		pBody = mpPhysicsWorld->CreateBody("",pShape);
		pBody->SetPosition(cVector3f(0,gvRoomSize.y/2 + 0.05f,0));


		//Right Wall
		pMesh->IncUserCount();
		pWall = mpWorld->CreateMeshEntity("Floor",pMesh);
		pWall->SetMatrix(cMath::MatrixScale(cVector3f(gvRoomSize.x,gvRoomSize.y,gvRoomSize.z)*0.5f));
		pWall->SetMatrix(cMath::MatrixMul(pWall->GetLocalMatrix(), cMath::MatrixRotateZ(kPi2f)));
		pWall->SetPosition(cVector3f(gvRoomSize.x/2,0,0));

		pShape = mpPhysicsWorld->CreateBoxShape(cVector3f(0.1f,gvRoomSize.x,gvRoomSize.z),NULL);
		pBody = mpPhysicsWorld->CreateBody("",pShape);
		pBody->SetPosition(cVector3f(gvRoomSize.x/2 + 0.05f,0,0));

		//Left Wall
		pMesh->IncUserCount();
		pWall = mpWorld->CreateMeshEntity("Floor",pMesh);
		pWall->SetMatrix(cMath::MatrixScale(cVector3f(gvRoomSize.x,gvRoomSize.y,gvRoomSize.z)*0.5f));
		pWall->SetMatrix(cMath::MatrixMul(pWall->GetLocalMatrix(), cMath::MatrixRotateZ(-kPi2f)));
		pWall->SetPosition(cVector3f(-gvRoomSize.x/2,0,0));

		pShape = mpPhysicsWorld->CreateBoxShape(cVector3f(0.1f,gvRoomSize.y,gvRoomSize.z),NULL);
		pBody = mpPhysicsWorld->CreateBody("",pShape);
		pBody->SetPosition(cVector3f(-gvRoomSize.x/2 - 0.05f,0,0));


		//Front Wall
		pMesh->IncUserCount();
		pWall = mpWorld->CreateMeshEntity("Floor",pMesh);
		pWall->SetMatrix(cMath::MatrixScale(cVector3f(gvRoomSize.x,gvRoomSize.y,gvRoomSize.z)*0.5f));
		pWall->SetMatrix(cMath::MatrixMul(pWall->GetLocalMatrix(), cMath::MatrixRotateX(-kPi2f)));
		pWall->SetPosition(cVector3f(0,0,gvRoomSize.z/2));

		pShape = mpPhysicsWorld->CreateBoxShape(cVector3f(gvRoomSize.x,gvRoomSize.y,0.1f),NULL);
		pBody = mpPhysicsWorld->CreateBody("",pShape);
		pBody->SetPosition(cVector3f(0,0,gvRoomSize.x/2 + 0.05f));

		//Back Wall
		pMesh->IncUserCount();
		pWall = mpWorld->CreateMeshEntity("Floor",pMesh);
		pWall->SetMatrix(cMath::MatrixScale(cVector3f(gvRoomSize.x,gvRoomSize.y,gvRoomSize.z)*0.5f));
		pWall->SetMatrix(cMath::MatrixMul(pWall->GetLocalMatrix(), cMath::MatrixRotateX(kPi2f)));
		pWall->SetPosition(cVector3f(0,0,-gvRoomSize.z/2));

		pShape = mpPhysicsWorld->CreateBoxShape(cVector3f(gvRoomSize.x,gvRoomSize.y,0.1f),NULL);
		pBody = mpPhysicsWorld->CreateBody("",pShape);
		pBody->SetPosition(cVector3f(0,0,-gvRoomSize.x/2 - 0.05f));

		iLight3D* pLight = mpWorld->CreateLightPoint();
		pLight->SetPosition(0);
		pLight->SetFarAttenuation(2000);
		pLight->SetDiffuseColor(cColor(1,1,1,1));
	}

	~cSimpleUpdate()
	{

	}

	void Update(float afTimeStep)
	{
		/*static int lTime =0;
		lTime++;
		if(lTime ==200)
		{
			mpPS->SetActive(false);
			mpPS->SetVisible(false);
		}*/


		cCamera3D *pCam = static_cast<cCamera3D*>(gpGame->GetScene()->GetCamera());


		//Render Bloom
		if(gpGame->GetInput()->BecameTriggerd("Bloom")){
			bool bBloom = gpGame->GetGraphics()->GetRendererPostEffects()->GetBloomActive();
			gpGame->GetGraphics()->GetRendererPostEffects()->SetBloomActive(!bBloom);
		}
	}

	void OnDraw()
	{
	}

	void OnPostSceneDraw()
	{
		return;
		cCamera3D *pCam = static_cast<cCamera3D*>(gpGame->GetScene()->GetCamera());
		mpLowLevelGraphics->SetMatrix(eMatrix_ModelView, pCam->GetViewMatrix());
		mpLowLevelGraphics->SetDepthTestActive(false);

		mpPhysicsWorld->RenderDebugGeometry(mpLowLevelGraphics,cColor(1,1));
	}

private:
	cParticleSystem3D *mpPS;

	iLowLevelGraphics* mpLowLevelGraphics;
	cWorld3D* mpWorld;

	iPhysicsWorld *mpPhysicsWorld;

};

/////////////////////////////////////////////////////


int hplMain(const tString& asCommands)
{
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

	SetWindowCaption("Particle Viewer");

	//iResourceBase::SetLogCreateAndDelete(true);

	//Add resources
	gpGame->GetResources()->LoadResourceDirsFile("resources.cfg");

	if(asCommands != ""){
		gsModelFile = asCommands;
	}

	//Add updates
	cSimpleUpdate Update;
	gpGame->GetUpdater()->AddUpdate("Default", &Update);

	cPVCamera cameraUpdate(gpGame,20,cVector3f(0,0,10),false);
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