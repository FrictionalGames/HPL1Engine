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
#include "HOECamera.h"

cHOECamera::cHOECamera(cGame *apGame, float afSpeed,cVector3f avStartPos,bool abShowFPS)
: iUpdateable("HOECamera")
{
	mpGame = apGame;
	mfSpeed = afSpeed;

	mvAngle = cVector3f(0,0,0);
	mvStartPos = avStartPos;

	mfMaxDist = 100.0f;
	mfMinDist = 0.2f;

	mpGame->GetGraphics()->GetLowLevel()->ShowCursor(true);

	//Add actions
	mpGame->GetInput()->AddAction(new cActionKeyboard("Escape",mpGame->GetInput(),eKey_ESCAPE));

	mpGame->GetInput()->AddAction(new cActionMouseButton("Rotate",mpGame->GetInput(),eMButton_Left));
	mpGame->GetInput()->AddAction(new cActionMouseButton("Zoom",mpGame->GetInput(),eMButton_Middle));

	//mpGame->GetInput()->GetLowLevel()->LockInput(true);

	mpCamera = mpGame->GetScene()->CreateCamera3D(eCameraMoveMode_Fly);
	mpGame->GetScene()->SetCamera(mpCamera);
	mpCamera->SetPosition(avStartPos);

	if(abShowFPS)
		mpFont = mpGame->GetResources()->GetFontManager()->CreateFontData("viewer.fnt",12,32,128);
	else
		mpFont = NULL;
}

//-----------------------------------------------------------------------

cHOECamera::~cHOECamera()
{

}

//-----------------------------------------------------------------------

void cHOECamera::Update(float afFrameTime)
{
	if(mpGame->GetInput()->IsTriggerd("Escape"))
	{
		mpGame->Exit();
	}

	/*float fMul = mpGame->GetStepSize();

	if(mpGame->GetInput()->IsTriggerd("Forward")) mpCamera->MoveForward(mfSpeed * fMul);
	if(mpGame->GetInput()->IsTriggerd("Backward")) mpCamera->MoveForward(-mfSpeed* fMul);
	if(mpGame->GetInput()->IsTriggerd("Right")) mpCamera->MoveRight(mfSpeed * fMul);
	if(mpGame->GetInput()->IsTriggerd("Left")) mpCamera->MoveRight(-mfSpeed * fMul);

	cVector2f vRel = mpGame->GetInput()->GetMouse()->GetRelPosition();
	mpCamera->AddYaw(-vRel.x * 0.003f);
	mpCamera->AddPitch(-vRel.y * 0.003f);*/

	cVector2f vRel = mpGame->GetInput()->GetMouse()->GetRelPosition();

	if(mpGame->GetInput()->IsTriggerd("Rotate"))
	{
		mvAngle += cVector3f(-vRel.y*0.005f,-vRel.x*0.005f,0);

		float fMaxAngle = kPi2f - 0.01f;
		if(mvAngle.x> fMaxAngle  ) mvAngle.x=fMaxAngle;
		if(mvAngle.x< -fMaxAngle ) mvAngle.x=-fMaxAngle;
	}
	else if(mpGame->GetInput()->IsTriggerd("Zoom"))
	{
		mvStartPos.z += vRel.y*0.12f;
		if(mvStartPos.z<mfMinDist)mvStartPos.z=mfMinDist;
		if(mvStartPos.z>mfMaxDist)mvStartPos.z=mfMaxDist;
	}

	CalculateCameraPos();
}

//-----------------------------------------------------------------------

void cHOECamera::OnDraw()
{
	//if(mpFont)
	//	mpFont->Draw(cVector2f(5,5),12,cColor(1,1),eFontAlign_Left,"FPS: %.1f",mpGame->GetFPS());
}

//-----------------------------------------------------------------------

void cHOECamera::CalculateCameraPos()
{
	cVector3f vRotated;
	cVector3f vOrigin = mvStartPos;

	cMatrixf mtxRotation = cMath::MatrixMul(cMath::MatrixRotateY(mvAngle.y),cMath::MatrixRotateX(mvAngle.x));

	vRotated = cMath::MatrixMul(mtxRotation, vOrigin);

	mpCamera->SetPosition(vRotated);
	float fYaw = cMath::GetAngleFromPoints2D(cVector2f(vRotated.x,vRotated.z),cVector2f(0));
	mpCamera->SetYaw(-fYaw);
	mpCamera->SetPitch(mvAngle.x);
}


//-----------------------------------------------------------------------
