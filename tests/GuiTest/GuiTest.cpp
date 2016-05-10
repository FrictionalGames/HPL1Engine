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

using namespace hpl;

cGame *gpGame=NULL;
cSimpleCamera *gpSimpleCam;

class cSimpleUpdate : public iUpdateable
{
public:

	//--------------------------------------------------

	cGuiSkin *mpMainSkin;
	cGuiSet *mpMainSet;

	cWidgetFrame *mpFrmBase;
	cWidgetWindow *mpWndMain;
	cWidgetFrame *mpFrmWindow;
	cWidgetButton *mpBtnOne;
	cWidgetButton *mpBtnTwo;

	cWidgetLabel *mpLblOne;
	cWidgetLabel *mpLblSliderVal;

	cWidgetSlider *mpSlidOne;
	cWidgetSlider *mpSlidTwo;

	cWidgetTextBox *mpTxtOne;

	cWidgetCheckBox *mpCheckOne;

	cWidgetImage *mpImgOne;

	cWidgetListBox *mpLstOne;

	cWidgetComboBox *mpCombOne;


	//--------------------------------------------------

	class cA
	{
	public:
		cA(int alX)
		{
			mlX = alX;
		}

		int mlX;
	};

	//--------------------------------------------------


	cSimpleUpdate() : iUpdateable("SimpleUPdate")
	{
		////////////////////////////////
		//Scene Init
		mpGui = gpGame->GetGui();
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
		//Memory test
		/*cImageManager *pImageManager = gpGame->GetResources()->GetImageManager();

		cResourceImage *pImage0 = pImageManager->CreateImage("gui_def_checkbox_checked.bmp");
		cResourceImage *pImage1 = pImageManager->CreateImage("gui_def_checkbox_checked.bmp");
		pImageManager->Destroy(pImage0);
		cResourceImage *pImage2 = pImageManager->CreateImage("gui_def_checkbox_checked.bmp");
		pImageManager->Destroy(pImage2);
		cResourceImage *pImage3 = pImageManager->CreateImage("gui_def_slider_hori_arrow_d.bmp");
		pImageManager->Destroy(pImage3);
		cResourceImage *pImage4 = pImageManager->CreateImage("gui_def_slider_hori_arrow_d.bmp");
		cResourceImage *pImage5 = pImageManager->CreateImage("gui_def_slider_hori_arrow_d.bmp");
		pImageManager->Destroy(pImage5);

		return;*/

		mpGfxTex = gpGame->GetGraphics()->GetDrawer()->CreateGfxObjectFromTexture("material_floor.jpg","diffalpha2d");

		//return;
		////////////////////////////////
		//Input Init
		gpGame->GetInput()->AddAction(new cActionMouseButton("LeftClick",gpGame->GetInput(),eMButton_Left));
		gpGame->GetInput()->AddAction(new cActionMouseButton("RightClick",gpGame->GetInput(),eMButton_Right));
		gpGame->GetInput()->AddAction(new cActionKeyboard("Gui",gpGame->GetInput(),eKey_LSHIFT));
		gpGame->GetInput()->AddAction(new cActionKeyboard("To3D",gpGame->GetInput(),eKey_f));


		////////////////////////////////
		//Gui Init
		//mpLowLevelGraphics->ShowCursor(true);

		//Skin
		mpMainSkin = mpGui->CreateSkin("gui_default.skin");

		//Set
		mpMainSet = mpGui->CreateSet("Test",mpMainSkin);
		mpGui->SetFocus(mpMainSet);

		mpMainSet->SetIs3D(false);
		cMatrixf mtxTransform = cMath::MatrixRotate(cVector3f(-kPi2f,0,0),eEulerRotationOrder_XYZ);
		mtxTransform.SetTranslation(cVector3f(0,0.01f,0));
		mpMainSet->Set3DTransform(mtxTransform);
		mpMainSet->Set3DSize(cVector3f(3,2.25f,0.1f));
		mpMainSet->SetRootWidgetClips(true);

		//Base Frame
		//mpFrmBase = mpMainSet->CreateWidgetFrame(cVector3f(0,0,0),cVector2f(800,600),false);

		//Window
		mpWndMain = mpMainSet->CreateWidgetWindow(cVector3f(0,0,0),cVector2f(600,400),_W(""),NULL);//,mpFrmBase);
		mpWndMain->SetText(_W("My Window"));

		//Window Frame
		mpFrmWindow = mpMainSet->CreateWidgetFrame(cVector3f(40,85,1),cVector2f(150,170),true,mpWndMain);
		mpFrmWindow->SetDrawBackground(true);
		mpFrmWindow->SetBackGroundColor(cColor(1,1));

		//Button1
		mpBtnOne = mpMainSet->CreateWidgetButton(	cVector3f(30,30,1),cVector2f(80,40),
													_W("Press me!"), mpWndMain);
		mpBtnOne->AddCallback(eGuiMessage_ButtonPressed, this, kGuiCallback(Button_MouseDown));

		//Button2
		mpBtnTwo = mpMainSet->CreateWidgetButton(	cVector3f(60,120,1),cVector2f(140,40),
													_W("Disabled"), mpFrmWindow);
		mpBtnTwo->SetEnabled(false);

		//Label1
		mpLblOne = mpMainSet->CreateWidgetLabel(	cVector3f(40,10,1),cVector2f(200,20),
													_W("Test Label this is a test to check wordwrap and stuff like that. So text must be long."),
														mpFrmWindow);
		mpLblOne->SetWordWrap(true);

		//Label1
		mpLblSliderVal = mpMainSet->CreateWidgetLabel(	cVector3f(120,30,1),cVector2f(200,20),
													_W("Slider: 0"),mpWndMain);

		//Slider1
		mpSlidOne = mpMainSet->CreateWidgetSlider(	eWidgetSliderOrientation_Vertical,
													cVector3f(260,30,1),cVector2f(20,240),10,
													mpWndMain);
		mpSlidOne->AddCallback(eGuiMessage_SliderMove,this,kGuiCallback(Slider_Move));
		mpSlidOne->SetMaxValue(9);
		mpSlidOne->SetBarValueSize(1);

		//Slider2
		mpSlidTwo = mpMainSet->CreateWidgetSlider(	eWidgetSliderOrientation_Horisontal,
													cVector3f(230,300,1),cVector2f(130,30),10,
													mpWndMain);
		mpSlidTwo->SetMaxValue(11);
		mpSlidTwo->SetBarValueSize(6);

		//TextBox1
		mpTxtOne = mpMainSet->CreateWidgetTextBox( cVector3f(10,280,1),cVector2f(170,30),_W("Test text!"),
													mpWndMain);
		mpTxtOne->SetMaxTextLength(40);
		//mpTxtOne->SetEnabled(false);

		//CheckBox1
		mpCheckOne = mpMainSet->CreateWidgetCheckBox( cVector3f(10,330,1),cVector2f(100,20),_W("Checker"),
														mpWndMain);
		//mpCheckOne->SetEnabled(false);

		//Image1
		mpImgOne = mpMainSet->CreateWidgetImage( "gui_test_anim.jpg",
												cVector3f(200,200,1),cVector2f(-1,-1),
												eGuiMaterial_Modulative, true,
												mpWndMain);

		//List box 1
		mpLstOne = mpMainSet->CreateWidgetListBox( cVector3f(330,30,1),cVector2f(210,80),mpWndMain);
		for(int i=0; i<2; ++i)
		{
			mpLstOne->AddItem(_W("Test"));
			mpLstOne->AddItem(_W("Test more"));
			mpLstOne->AddItem(_W("Another item..."));
			mpLstOne->AddItem(_W("Yet another"));
		}

		//Combo box 1
		mpCombOne = mpMainSet->CreateWidgetComboBox( cVector3f(330,210,1),cVector2f(210,80),
													_W("Testings"),mpWndMain);
		mpCombOne->SetMaxShownItems(4);
		for(int i=0; i<2; ++i)
		{
			mpCombOne->AddItem(_W("Test"));
			mpCombOne->AddItem(_W("Test more"));
			mpCombOne->AddItem(_W("Another item... that is long long and longer!!!"));
			mpCombOne->AddItem(_W("Yet another"));
		}


	}

	//--------------------------------------------------

	~cSimpleUpdate()
	{

	}

	//--------------------------------------------------

	void OnDraw()
	{
		gpGame->GetGraphics()->GetDrawer()->DrawGfxObject(mpGfxTex,0,cVector2f(400,400),cColor(1,1));
	}

	//--------------------------------------------------

	void Update(float afTimeStep)
	{
		//return;
		if(mpMainSet->Is3D() && gpGame->GetInput()->IsTriggerd("Gui")==false) return;
		////////////////////////////////
		//Send Gui Input
		while(gpGame->GetInput()->GetKeyboard()->KeyIsPressed())
		{
			cKeyPress keyPress = gpGame->GetInput()->GetKeyboard()->GetKey();
			mpGui->SendKeyPress(keyPress);
		}


		mpGui->SendMousePos(gpGame->GetInput()->GetMouse()->GetAbsPosition(),
							gpGame->GetInput()->GetMouse()->GetRelPosition());

		if(gpGame->GetInput()->BecameTriggerd("LeftClick")) mpGui->SendMouseClickDown(eGuiMouseButton_Left);
		if(gpGame->GetInput()->WasTriggerd("LeftClick"))		mpGui->SendMouseClickUp(eGuiMouseButton_Left);
		if(gpGame->GetInput()->DoubleTriggerd("LeftClick",0.2f))	mpGui->SendMouseDoubleClick(eGuiMouseButton_Left);

		if(gpGame->GetInput()->BecameTriggerd("RightClick"))	mpGui->SendMouseClickDown(eGuiMouseButton_Right);
		if(gpGame->GetInput()->WasTriggerd("RightClick"))	mpGui->SendMouseClickUp(eGuiMouseButton_Right);
		if(gpGame->GetInput()->DoubleTriggerd("RightClick",0.2f))	mpGui->SendMouseDoubleClick(eGuiMouseButton_Right);

		if(	(mpMainSet->GetFocusedWidget() &&
			mpMainSet->GetFocusedWidget()->GetType() == eWidgetType_Frame) ||
			mpMainSet->GetFocusedWidget() == NULL)
		{
			gpSimpleCam->SetActive(true);
			if(gpGame->GetInput()->BecameTriggerd("To3D"))
			{
				mpMainSet->SetIs3D(!mpMainSet->Is3D());
			}
		}
		else
		{
			gpSimpleCam->SetActive(false);
		}
	}

	//--------------------------------------------------

	bool Button_MouseDown(iWidget* apWidget,cGuiMessageData& aData)
	{
		mpMainSet->CreatePopUpMessageBox(_W("Testing"),
										_W("A long test text that does not make any sense!"),
										_W("OK"),_W("Cancel"),
										this, kGuiCallback(PopUpPressed));

		return true;
	}
	kGuiCalllbackFuncEnd(cSimpleUpdate,Button_MouseDown)

	bool Slider_Move(iWidget* apWidget,cGuiMessageData& aData)
	{
		mpLblSliderVal->SetText(_W("Slider: ") + cString::ToStringW(mpSlidOne->GetValue()));
		return true;
	}
	kGuiCalllbackFuncEnd(cSimpleUpdate,Slider_Move)

	bool PopUpPressed(iWidget* apWidget,cGuiMessageData& aData)
	{
		Log("PRessed: %d\n",aData.mlVal);
		return true;
	}
	kGuiCalllbackFuncEnd(cSimpleUpdate,PopUpPressed)



    //--------------------------------------------------

private:
	iLowLevelGraphics* mpLowLevelGraphics;
	cWorld3D* mpWorld;

	cGfxObject *mpGfxTex;

	cGui *mpGui;
};



int hplMain(const tString &asCommandline)
{
	//Init the game engine
	iLowLevelGameSetup *pSetup = hplNew(cSDLGameSetup,());
	gpGame = hplNew(cGame, (pSetup,800,600,32,false,45) );
	gpGame->GetGraphics()->GetLowLevel()->SetVsyncActive(false);

	//Add resources
	gpGame->GetResources()->LoadResourceDirsFile("resources.cfg");


	//Add updates
	cSimpleUpdate Update;
	gpGame->GetUpdater()->AddUpdate("Default", &Update);

	gpSimpleCam = hplNew( cSimpleCamera, (gpGame,8,cVector3f(0,2,9),false) );
	gpGame->GetUpdater()->AddUpdate("Default", gpSimpleCam);

	//Run the engine
	gpGame->Run();

	hplDelete(gpSimpleCam);

	//Delete the engine
	hplDelete(gpGame);

	cMemoryManager::LogResults();

	return 0;
}
