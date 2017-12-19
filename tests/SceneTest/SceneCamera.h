/*
 * Copyright 2007-2010 (C) - Frictional Games
 *
 * This file is part of HPL1Engine
 *
 * For conditions of distribution and use, see copyright notice in LICENSE-tests
 */
#include <hpl.h>

using namespace hpl;

class cSceneCamera : public iUpdateable
{
public:
	cSceneCamera(cGame *apGame, float afSpeed,cVector3f avStartPos,bool abShowFPS);
	~cSceneCamera();

	void Update(float afFrameTime);

	void OnDraw();

	cGame *mpGame;
	iFontData* mpFont;
	cCamera3D* mpCamera;

	iCharacterBody *mpCharBody;

	float mfSpeed;
};
