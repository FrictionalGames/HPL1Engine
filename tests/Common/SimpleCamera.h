/*
 * Copyright 2007-2010 (C) - Frictional Games
 *
 * This file is part of HPL1Engine
 *
 * For conditions of distribution and use, see copyright notice in LICENSE-tests
 */
#include <hpl.h>

using namespace hpl;

class cSimpleCamera : public iUpdateable
{
public:
	cSimpleCamera(cGame *apGame, float afSpeed,cVector3f avStartPos,bool abShowFPS);
	~cSimpleCamera();

	void Update(float afFrameTime);

	void OnDraw();

	void SetActive(bool abX){ mbActive = abX;}

	cMatrixf m_mtxLastView;
private:
	cGame *mpGame;
	iFontData* mpFont;
	cCamera3D* mpCamera;

	cGui *mpGui;
	cGuiSet *mpGuiSet;
	cGuiSkin *mpGuiSkin;

	cGuiGfxElement *mpTestGfx;

	float mfSpeed;
	bool mbActive;
};
