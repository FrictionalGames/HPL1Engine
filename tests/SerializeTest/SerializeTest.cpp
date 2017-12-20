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

#include "../Common/SimpleCamera.h"

using namespace hpl;

cGame *gpGame=NULL;


class cPiggy : public iSerializable
{
	kSerializableClassInit(cPiggy)
public:
	cPiggy()
	{
		mlLegs = 4;
		msName = "Snotty";
	}

	cPiggy(int alX)
	{
		mlLegs = alX;
		msName = "Mutant";
	}

	int mlLegs;
	tString msName;
};

kBeginSerializeBase(cPiggy)
kSerializeVar(mlLegs,eSerializeType_Int32)
kSerializeVar(msName,eSerializeType_String)
kEndSerialize()

////////////////////////////

class cA : public iSerializable
{
	kSerializableClassInit(cA)
public:
	int mlX;
	int mlY;
};

kBeginSerializeBase(cA)
kSerializeVar(mlX, eSerializeType_Int32)
kSerializeVar(mlY, eSerializeType_Int32)
kEndSerialize()


////////////////////////////

class cB : public cA
{
	kSerializableClassInit(cB)
public:
	cB()
	{
		for(int i=0; i<10; i++)	mvValues[i] =0;

		for(int i=0; i<3;i++) mvPigs[i] = NULL;
		mpPig = NULL;

		mvStrings.Add("Test1");
		mvStrings.Add("Test2");

		mvInts.Add(1);
		mvInts.Add(1);
	}

	float mfVal;
	tString msName;
	bool mbBe;
	cVector3f mvVec;
	cColor mCol;
	cMatrixf mtxTest;

	cContainerVec<tString> mvStrings;
	cContainerVec<int> mvInts;

	float mvValues[10];
	cPiggy* mvPigs[3];

	cPiggy* mpPig;

	cContainerList<cPiggy*> mvVals;
};

kBeginSerialize(cB,cA)
	kSerializeVar(mfVal, eSerializeType_Float32)
	kSerializeVar(msName, eSerializeType_String)
	kSerializeVar(mbBe, eSerializeType_Bool)
	kSerializeVar(mvVec, eSerializeType_Vector3f)
	kSerializeVar(mCol, eSerializeType_Color)
	kSerializeVar(mtxTest, eSerializeType_Matrixf)

	//kSerializeVarContainer(mvStrings,eSerializeType_String) <- ILLEGAL!
	kSerializeVarContainer(mvInts,eSerializeType_Int32)

	kSerializeVarArray(mvValues,eSerializeType_Float32,10)
	kSerializeVarArray(mvPigs,eSerializeType_ClassPointer,3)

	kSerializeVar(mpPig, eSerializeType_ClassPointer)

	kSerializeClassContainer(mvVals,cPiggy,eSerializeType_ClassPointer)
kEndSerialize()

////////////////////////////

class cSimpleUpdate : public iUpdateable
{
public:
	cSimpleUpdate() : iUpdateable("test")
	{
		//gpGame->SetRenderOnce(true);
		//gpGame->GetGraphics()->GetRenderer3D()->SetDebugFlags(eRendererDebugFlag_LogRendering);
		//gpGame->GetGraphics()->GetRenderer3D()->SetDebugFlags(eRendererDebugFlag_DrawLightBoundingBox);

		//gpGame->GetGraphics()->GetRenderer3D()->SetDebugFlags(eRendererDebugFlag_DrawNormals);
		//gpGame->GetGraphics()->GetRenderer3D()->SetDebugFlags(eRendererDebugFlag_DrawBoundingBox);

		mpLowLevelGraphics = gpGame->GetGraphics()->GetLowLevel();

		///////////////////////////////////////////////////
		// Container Test

		cContainerVec<int> vVals;

		/*vVals.Add(1);
		vVals.Add(2);
		vVals.Add(3);
		vVals.Add(4);

		cContainerVecIterator<int> it = vVals.GetIterator();

		while(it.HasNext())
		{
			Log("val: %d\n",it.Next());
		}*/

		///////////////////////////////////////////////////
		// Serialize Test
		cA testSaveA;
		testSaveA.mlX =5;
		testSaveA.mlY =66;

		cB testSaveB;
		testSaveB.mlX =3;
		testSaveB.mlY =8;
		testSaveB.mfVal = 6.0f;
		testSaveB.mbBe = false;
		testSaveB.msName = "Thomas";
		testSaveB.mvVec = cVector3f(1,2,3);
		testSaveB.mCol = cColor(1,0,1,0);
		testSaveB.mtxTest = cMatrixf::Identity;
		for(int i=0; i<10; i++) testSaveB.mvValues[i] = (float)i+1;

		for(int i=0; i<3; i++) {
			testSaveB.mvPigs[i] = new cPiggy();
			testSaveB.mvPigs[i]->mlLegs =0;
			testSaveB.mvPigs[i]->msName = "DeadOne";
		}

		testSaveB.mpPig = new cPiggy();
		testSaveB.mpPig->mlLegs = 2;
		testSaveB.mpPig->msName = "TwoLegs";

		testSaveB.mvVals.Add(new cPiggy());
		testSaveB.mvVals.Add(new cPiggy(2));
		testSaveB.mvVals.Add(new cPiggy(6));
		testSaveB.mvVals.Add(new cPiggy(7));

		Log("Saaaaaaving!\n");
		cSerializeClass::SaveToFile(&testSaveB,_W("TestSave.txt"),"TestSave");
		cB testSaveB2;
		cSerializeClass::LoadFromFile(&testSaveB2,_W("TestSave.txt"));
		cSerializeClass::SaveToFile(&testSaveB2,_W("TestSave2.txt"),"TestSave2");


		//Add the engine's object plugin.
		/*gpGame->GetResources()->AddEntity3DLoader(new cEntityLoader_Object("Object"));

		mpWorld = gpGame->GetResources()->GetMeshLoaderHandler()->LoadWorld("maps/maptest0.dae");
		gpGame->GetInput()->AddAction(new cActionKeyboard("Shadows",gpGame->GetInput(),eKey_1));

		gpGame->GetScene()->SetWorld3D(mpWorld);


		mpFont = gpGame->GetResources()->GetFontManager()->CreateFontData("verdana.ttf");*/

		gpGame->Exit();
	}

	~cSimpleUpdate()
	{

	}

	void Update(float afFrameTime)
	{

	}

	void OnDraw()
	{
	}

	void OnPostSceneDraw()
	{
		return;
		cCamera3D *pCam = static_cast<cCamera3D*>(gpGame->GetScene()->GetCamera());
		mpLowLevelGraphics->SetMatrix(eMatrix_ModelView, pCam->GetViewMatrix());

	}


private:
	float mfLightAngle;
	iFontData *mpFont;

	cWorld3D* mpWorld;
	iLowLevelGraphics* mpLowLevelGraphics;
};



int hplMain(const tString &asCommandLine) {
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

	cSimpleCamera cameraUpdate(gpGame,8,cVector3f(0,1,0),true);
	gpGame->GetUpdater()->AddUpdate("Default", &cameraUpdate);


	//Run the engine
	gpGame->Run();

	//Delete the engine
	delete gpGame;
	return 0;
}