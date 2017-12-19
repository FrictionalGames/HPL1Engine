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

	mpGame->GetInput()->GetLowLevel()->LockInput(true);

	mpCamera = mpGame->GetScene()->CreateCamera3D(eCameraMoveMode_Fly);
	mpGame->GetScene()->SetCamera(mpCamera);
	mpCamera->SetPosition(avStartPos);
	//mpCamera->SetFarClipPlane(1000);
	//mpCamera->SetYaw(-kPi2f);

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

	float fMul = mpGame->GetStepSize();

	if(mpGame->GetInput()->IsTriggerd("Forward")) mpCamera->MoveForward(mfSpeed * fMul);
	if(mpGame->GetInput()->IsTriggerd("Backward")) mpCamera->MoveForward(-mfSpeed* fMul);
	if(mpGame->GetInput()->IsTriggerd("Right")) mpCamera->MoveRight(mfSpeed * fMul);
	if(mpGame->GetInput()->IsTriggerd("Left")) mpCamera->MoveRight(-mfSpeed * fMul);

	cVector2f vRel = mpGame->GetInput()->GetMouse()->GetRelPosition();
	mpCamera->AddYaw(-vRel.x * 0.003f);
	mpCamera->AddPitch(-vRel.y * 0.003f);
}

//-----------------------------------------------------------------------

void cSceneCamera::OnDraw()
{
	if(mpFont)
		mpFont->Draw(cVector2f(5,5),12,cColor(1,1),eFontAlign_Left,_W("FPS: %.1f"),mpGame->GetFPS());
}

//-----------------------------------------------------------------------