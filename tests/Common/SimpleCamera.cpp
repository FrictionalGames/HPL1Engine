/*
 * Copyright 2007-2010 (C) - Frictional Games
 *
 * This file is part of HPL1Engine
 *
 * For conditions of distribution and use, see copyright notice in LICENSE-tests
 */
#include "stdafx.h"
#include "SimpleCamera.h"

cSimpleCamera::cSimpleCamera(cGame *apGame, float afSpeed,cVector3f avStartPos,bool abShowFPS)
: iUpdateable("SimpleCamera")
{
	mpGame = apGame;
	mfSpeed = afSpeed;

	//Add actions
	mpGame->GetInput()->AddAction(hplNew( cActionKeyboard,("Escape",mpGame->GetInput(),eKey_ESCAPE)) );

	mpGame->GetInput()->AddAction(hplNew( cActionKeyboard,("Forward",mpGame->GetInput(),eKey_w)) );
	mpGame->GetInput()->AddAction(hplNew( cActionKeyboard,("Backward",mpGame->GetInput(),eKey_s)) );
	mpGame->GetInput()->AddAction(hplNew( cActionKeyboard,("Right",mpGame->GetInput(),eKey_d)) );
	mpGame->GetInput()->AddAction(hplNew( cActionKeyboard,("Left",mpGame->GetInput(),eKey_a)) );
	mpGame->GetInput()->AddAction(hplNew(cActionKeyboard, ("Lock", mpGame->GetInput(), eKey_l)));
	mpGame->GetInput()->AddAction(hplNew(cActionKeyboard, ("UnLock", mpGame->GetInput(), eKey_u)));

	mpGame->GetInput()->GetLowLevel()->LockInput(false);

	//mpGame->GetGraphics()->GetLowLevel()->ShowCursor(true);


	mpCamera = mpGame->GetScene()->CreateCamera3D(eCameraMoveMode_Fly);
	mpGame->GetScene()->SetCamera(mpCamera);
	mpCamera->SetPosition(avStartPos);
	//mpCamera->SetFarClipPlane(1000);

	mpGui = mpGame->GetGui();
	mpGuiSkin = mpGui->CreateSkin("gui_default.skin");
	mpGuiSet = mpGui->CreateSet("Text",mpGuiSkin);

	//Debug:
	//mpGui->SetFocus(mpGuiSet);
	//mpGuiSet->SetDrawMouse(true);
	//mpTestGfx = mpGui->CreateGfxFilledRect(cColor(0,0,0,1),eGuiMaterial_Diffuse);


	if(abShowFPS)
		mpFont = mpGame->GetResources()->GetFontManager()->CreateFontData("viewer.fnt",12,32,128);
	else
		mpFont = NULL;

	mbActive = true;

	m_mtxLastView = mpCamera->GetViewMatrix();
}

//-----------------------------------------------------------------------

cSimpleCamera::~cSimpleCamera()
{

}

//-----------------------------------------------------------------------

void cSimpleCamera::Update(float afFrameTime)
{
	m_mtxLastView = mpCamera->GetViewMatrix();

	if(mpGame->GetInput()->IsTriggerd("Escape"))
	{
		mpGame->Exit();
	}

	if(mbActive== false) return;

	float fMul = mpGame->GetStepSize();

	if(mpGame->GetInput()->IsTriggerd("Forward")) mpCamera->MoveForward(mfSpeed * fMul);
	if(mpGame->GetInput()->IsTriggerd("Backward")) mpCamera->MoveForward(-mfSpeed* fMul);
	if(mpGame->GetInput()->IsTriggerd("Right")) mpCamera->MoveRight(mfSpeed * fMul);
	if(mpGame->GetInput()->IsTriggerd("Left")) mpCamera->MoveRight(-mfSpeed * fMul);
	if(mpGame->GetInput()->IsTriggerd("Lock")) mpGame->GetInput()->GetLowLevel()->LockInput(true);
	if(mpGame->GetInput()->IsTriggerd("UnLock")) mpGame->GetInput()->GetLowLevel()->LockInput(false);


	cVector2f vRel = mpGame->GetInput()->GetMouse()->GetRelPosition();
	mpCamera->AddYaw(-vRel.x * 0.003f);
	mpCamera->AddPitch(-vRel.y * 0.003f);

	//Log("Input gotten: %d\n", GetApplicationTime());
}

//-----------------------------------------------------------------------

void cSimpleCamera::OnDraw()
{
	//return;
	//cVector2f vAbs = mpGame->GetInput()->GetMouse()->GetAbsPosition();
	//mpGui->SendMousePos(vAbs, 0);


	//for(int i=0;i<600; ++i)
	//	mpGuiSet->DrawGfx(mpTestGfx,0,cVector2f(1024,768));

	if(mpFont)
	{
		mpGuiSet->DrawFont( _W("FPS: ")+ cString::ToStringW(mpGame->GetFPS()), mpFont,cVector3f(5,5,5),14,cColor(1,1),
			eFontAlign_Left,eGuiMaterial_FontNormal);
	}
}

//-----------------------------------------------------------------------
