/*
 * Copyright 2007-2010 (C) - Frictional Games
 *
 * This file is part of HPL1Engine
 *
 * For conditions of distribution and use, see copyright notice in LICENSE-tests
 */
#include "SceneCamera.h"

cSceneCamera::cSceneCamera(cGame *apGame, float afSpeed,cVector3f avStartPos,bool abShowFPS)
	: iUpdateable("SceneCamera")
{
	mpGame = apGame;
	mfSpeed = afSpeed;

	//Add actions
	mpGame->GetInput()->AddAction(new cActionKeyboard("Escape",mpGame->GetInput(),eKey_ESCAPE));

	mpGame->GetInput()->AddAction(new cActionKeyboard("Forward",mpGame->GetInput(),eKey_w));
	mpGame->GetInput()->AddAction(new cActionKeyboard("Backward",mpGame->GetInput(),eKey_s));
	mpGame->GetInput()->AddAction(new cActionKeyboard("Right",mpGame->GetInput(),eKey_d));
	mpGame->GetInput()->AddAction(new cActionKeyboard("Left",mpGame->GetInput(),eKey_a));
	mpGame->GetInput()->AddAction(new cActionKeyboard("Gravity",mpGame->GetInput(),eKey_g));

	mpGame->GetInput()->GetLowLevel()->LockInput(true);

	mpCamera = mpGame->GetScene()->CreateCamera3D(eCameraMoveMode_Fly);
	mpGame->GetScene()->SetCamera(mpCamera);
	mpCamera->SetPosition(avStartPos);
	//mpCamera->SetFarClipPlane(1000);

	mpCharBody = mpGame->GetScene()->GetWorld3D()->GetPhysicsWorld()->CreateCharacterBody("Test",
									cVector3f(0.6f,1.6f,0.6f));
	mpCharBody->SetPosition(cVector3f(0,mpCharBody->GetSize().y/2 + 0.1f,0));

	mpCharBody->SetCamera(mpCamera);
	mpCharBody->SetCameraPosAdd(cVector3f(0,-0.15f,0));

	mpCharBody->SetMaxPositiveMoveSpeed(eCharDir_Forward,4.0f);
	mpCharBody->SetMaxNegativeMoveSpeed(eCharDir_Forward,-4.0f);

	mpCharBody->SetMaxPositiveMoveSpeed(eCharDir_Right,4.0f);
	mpCharBody->SetMaxNegativeMoveSpeed(eCharDir_Right,-4.0f);

	mpCharBody->SetMoveAcc(eCharDir_Forward,12.0f);
	mpCharBody->SetMoveDeacc(eCharDir_Forward,8.0f);
	mpCharBody->SetMoveAcc(eCharDir_Right,12.0f);
	mpCharBody->SetMoveDeacc(eCharDir_Right,12.0f);

	//mpCharBody->SetGravityActive(false);

	if(abShowFPS)
		mpFont = mpGame->GetResources()->GetFontManager()->CreateFontData("viewer.fnt",12,32,128);
	else
		mpFont = NULL;
}

//-----------------------------------------------------------------------

cSceneCamera::~cSceneCamera()
{

}

//-----------------------------------------------------------------------

void cSceneCamera::Update(float afFrameTime)
{
	if(mpGame->GetInput()->IsTriggerd("Escape"))
	{
		mpGame->Exit();
	}

	if(mpGame->GetInput()->BecameTriggerd("Gravity"))
	{
		mpCharBody->SetGravityActive(!mpCharBody->GravityIsActive());
	}

	float fMul = mpGame->GetStepSize();

	mpCamera->SetPosition(mpCharBody->GetPosition() + cVector3f(0,mpCharBody->GetSize().y/2-0.15f,0));

	if(mpGame->GetInput()->IsTriggerd("Forward"))
		mpCharBody->Move(eCharDir_Forward,1.0f,afFrameTime);
	else if(mpGame->GetInput()->IsTriggerd("Backward"))
		mpCharBody->Move(eCharDir_Forward,-1.0f,afFrameTime);

	if(mpGame->GetInput()->IsTriggerd("Right"))
		mpCharBody->Move(eCharDir_Right,1.0f,afFrameTime);
	else if(mpGame->GetInput()->IsTriggerd("Left"))
		mpCharBody->Move(eCharDir_Right,-1.0f,afFrameTime);

	cVector2f vRel = mpGame->GetInput()->GetMouse()->GetRelPosition();

	mpCamera->AddYaw(-vRel.x * 0.003f);
	mpCharBody->SetYaw(mpCamera->GetYaw());

	mpCamera->AddPitch(-vRel.y * 0.003f);
}

//-----------------------------------------------------------------------

void cSceneCamera::OnDraw()
{
	if(mpFont)
	{
		mpFont->Draw(cVector3f(5,5,5), cVector2f(12,12), cColor(1,1), eFontAlign_Left,
					 _W("FPS: %.1f"), mpGame->GetFPS());
		mpFont->Draw(cVector3f(5,17,5), cVector2f(12, 12), cColor(1,1), eFontAlign_Left,
					 _W("Vel: %s"), mpCharBody->GetForceVelocity().ToString().c_str());
		mpFont->Draw(cVector3f(5, 29, 5), cVector2f(12, 12), cColor(1,1), eFontAlign_Left,
					 _W("Pos: %s"), mpCharBody->GetPosition().ToString().c_str());
	}
}

//-----------------------------------------------------------------------
