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

#include "SceneCamera.h"


using namespace hpl;

cGame *gpGame=NULL;

tString gsSceneFile = "maptest01.dae";
tString gsStartPos = "Start";

cColor gvSectorColors[] ={ cColor(1,0,1), cColor(0,1,0.5f), cColor(1,1,0),cColor(0,1,1),cColor(0.5f,1,0.5f),
cColor(0.5f, 0.5f, 1),cColor(1,0,0.5f),cColor(0.5f,0.5f,0.7f), cColor(1,0.5f,1)};
int glSectorColorNum = 9;


class cSimpleUpdate : public iUpdateable
{
public:
	cSimpleUpdate() : iUpdateable("SimpleUpdate")
	{
		//Setup so only one material is used.
		//iMeshLoader::SetUseFastMaterial(true);
		//iMeshLoader::SetFastMaterialFile("thomas_temp");


		cCamera3D *pCam = static_cast<cCamera3D*>(gpGame->GetScene()->GetCamera());

		//gpGame->SetRenderOnce(true);
		//gpGame->GetGraphics()->GetRenderer3D()->SetDebugFlags(eRendererDebugFlag_LogRendering);
		//gpGame->GetGraphics()->GetRenderer3D()->SetDebugFlags(eRendererDebugFlag_DrawBoundingBox);

		//iMaterial::SetQuality(eMaterialQuality_High);

		mpLowLevelGraphics = gpGame->GetGraphics()->GetLowLevel();

		gpGame->GetGraphics()->GetRendererPostEffects()->SetActive(true);
		gpGame->GetGraphics()->GetRendererPostEffects()->SetBloomActive(true);
		gpGame->GetGraphics()->GetRendererPostEffects()->SetBloomSpread(6);

		iMeshLoader::SetRestricStaticLightToSector(true);

		/*gpGame->GetGraphics()->GetRendererPostEffects()->SetDepthOfFieldActive(true);
		gpGame->GetGraphics()->GetRendererPostEffects()->SetDepthOfFieldMaxBlur(1);
		gpGame->GetGraphics()->GetRendererPostEffects()->SetDepthOfFieldNearPlane(1);
		gpGame->GetGraphics()->GetRendererPostEffects()->SetDepthOfFieldFocalPlane(3);
		gpGame->GetGraphics()->GetRendererPostEffects()->SetDepthOfFieldFarPlane(5);*/

		//Add the engine's object plugin.
		gpGame->GetResources()->AddEntity3DLoader(new cEntityLoader_Object("Object"),true);

		//Setup physics
		gpGame->GetPhysics()->LoadSurfaceData("materials.cfg");

		unsigned long lStartTime = gpGame->GetScene()->GetSystem()->GetLowLevel()->GetTime();

		mpWorld = gpGame->GetScene()->LoadWorld3D(	cString::GetFileName(gsSceneFile),true,
													0);
		if(mpWorld==NULL) FatalError("Couldn't load world!\n");

		unsigned long lTime = gpGame->GetScene()->GetSystem()->GetLowLevel()->GetTime() - lStartTime;
		Log("Loading map '%s' took: %d ms\n",cString::GetFileName(gsSceneFile).c_str(),lTime);

		//A lil hack, set all lights to only affect sector
		cLight3DListIterator lightIt = mpWorld->GetLightIterator();
		while(lightIt.HasNext())
		{
			iLight3D *pLight = lightIt.Next();

			pLight->SetOnlyAffectInSector(true);
		}


		//Load script
		if(mpWorld->GetScript())
		{
			mpWorld->GetScript()->Run("OnStart()");
			mpWorld->GetScript()->Run("OnLoad()");
		}

		cStartPosEntity *pStartPos = mpWorld->GetStartPosEntity(gsStartPos);
		mpStart = pStartPos;
		if(pStartPos){
			pCam->SetPosition(pStartPos->GetWorldMatrix().GetTranslation());

			cMatrixf mtxInv = cMath::MatrixInverse(pStartPos->GetWorldMatrix());

			cVector3f vForward = pStartPos->GetWorldMatrix().GetForward()*-1.0f;

			cVector3f vRotation = cMath::GetAngleFromPoints3D(cVector3f(0,0,0), vForward);

			Log("Rot: %s Fwd: %s\n",vRotation.ToString().c_str(), vForward.ToString().c_str());

			pCam->SetYaw(vRotation.y);
			pCam->SetPitch(vRotation.x);
		}
		else
		{
			Warning("Couldn't find start position '%s'!\n",gsStartPos.c_str());
		}

		//Debug:
		//pCam->SetPosition(cVector3f(-4.94f, 2.23f, -15.05f));
		//pCam->SetYaw(0.423f);
		//pCam->SetPitch(0);

		gpGame->GetInput()->AddAction(new cActionKeyboard("Shadows",gpGame->GetInput(),eKey_1));
		gpGame->GetInput()->AddAction(new cActionKeyboard("RoomDebug",gpGame->GetInput(),eKey_2));
		gpGame->GetInput()->AddAction(new cActionKeyboard("LightDebug",gpGame->GetInput(),eKey_3));
		gpGame->GetInput()->AddAction(new cActionKeyboard("ColliderDebug",gpGame->GetInput(),eKey_4));
		gpGame->GetInput()->AddAction(new cActionKeyboard("BoundingBoxes",gpGame->GetInput(),eKey_5));
		gpGame->GetInput()->AddAction(new cActionKeyboard("SoundColliders",gpGame->GetInput(),eKey_6));
		gpGame->GetInput()->AddAction(new cActionKeyboard("RenderLines",gpGame->GetInput(),eKey_7));
		gpGame->GetInput()->AddAction(new cActionKeyboard("Areas",gpGame->GetInput(),eKey_8));
		gpGame->GetInput()->AddAction(new cActionKeyboard("DisplayObjects",gpGame->GetInput(),eKey_9));
		gpGame->GetInput()->AddAction(new cActionKeyboard("Bloom",gpGame->GetInput(),eKey_b));

		gpGame->GetScene()->SetWorld3D(mpWorld);

		mpFont = gpGame->GetResources()->GetFontManager()->CreateFontData("viewer.fnt");

		mbRoomDebug = false;
		mbLightDebug = false;
		mbColliderDebug = false;
		mbBoundingBoxes = false;
		mbSoundColliders = false;
		mbRenderLines = false;
		mbAreas = false;
		mbDisplayObjects = false;

		//cCamera3D *pCam = static_cast<cCamera3D*>(gpGame->GetScene()->GetCamera());

		/*pCam->SetPosition(cVector3f(-3.25, 1.63f,-2.344f));
		pCam->SetYaw(-1.65f);
		pCam->SetPitch(-0.52f);

		//Preupdate:
		for(int i=0; i< 100; ++i)
			mpWorld->Update(1.0f / 60.0f);*/

		gpGame->GetGraphics()->GetRenderer3D()->SetShowShadows(eRendererShowShadows_All);

		typedef std::multimap<int, iTexture*,std::greater<int> > tTextureMemMap;

		tTextureMemMap mapTextures;

		cResourceBaseIterator resourceIt = gpGame->GetResources()->GetTextureManager()->GetResourceBaseIterator();
		while(resourceIt.HasNext())
		{
			iTexture *pTexture = static_cast<iTexture*>(resourceIt.Next());

			int lMemory = pTexture->GetWidth() * pTexture->GetHeight() * (pTexture->GetBpp()/8);

			mapTextures.insert(tTextureMemMap::value_type(lMemory, pTexture));
		}



		//Models in scene:
		Log("##################\nSCENE MODELS:\n");
		cMeshEntityIterator meshIt = mpWorld->GetMeshEntityIterator();
		if(false)//while(meshIt.HasNext())
		{
			cMeshEntity *pEntity = meshIt.Next();

			Log(" %s\n", pEntity->GetName().c_str());
			Log("  Position: %s\n", pEntity->GetWorldPosition().ToString().c_str());
			Log("  Matrix: %s\n", pEntity->GetWorldMatrix().ToString().c_str());
			Log("  BB: (%s)-(%s)\n",pEntity->GetBoundingVolume()->GetMin().ToString().c_str(),
									pEntity->GetBoundingVolume()->GetMax().ToString().c_str());

		}
		Log("##################\n");

		//Draw all large texture
		Log("##################\nALL TEXTURES LOADED:\n");
		int lMemoryAccum =0;
		int lMax = 50;
		for(tTextureMemMap::iterator it = mapTextures.begin(); it != mapTextures.end(); ++it)
		{
			iTexture *pTexture = static_cast<iTexture*>(it->second);

			int lMemory = pTexture->GetWidth() * pTexture->GetHeight() * (pTexture->GetBpp()/8);
			lMemoryAccum += lMemory;

			tString sFile = cString::SetFileExt(pTexture->GetName(),"jpg");
			tString sDir = gpGame->GetResources()->GetFileSearcher()->GetFilePath(sFile);
			if(sDir == "")
			{
				sFile = cString::SetFileExt(pTexture->GetName(),"tga");
				sDir = gpGame->GetResources()->GetFileSearcher()->GetFilePath(sFile);
			}
			sDir = cString::GetFilePath(sDir);

			Log("%s",pTexture->GetName().c_str());
			int lSpaces = lMax - (int)pTexture->GetName().length();
			for(int i=0; i<lSpaces; ++i) Log(" ");
			Log("%d x %d pixels | %d  bpp | %d kb | \tdir: %s\n",pTexture->GetWidth(),pTexture->GetHeight(),
											pTexture->GetBpp(),lMemory / 1024,sDir.c_str());


		}
		Log("\nTotal Memory usage: %d mb\n",lMemoryAccum / (1024*1024));
		Log("##################\n");
	}

	~cSimpleUpdate()
	{

	}

	void Update(float afFrameTime)
	{
		//Input
		if(gpGame->GetInput()->BecameTriggerd("Shadows")){
			int lCurrent = gpGame->GetGraphics()->GetRenderer3D()->GetShowShadows();
			lCurrent++;if(lCurrent>=3)lCurrent=0;
			gpGame->GetGraphics()->GetRenderer3D()->SetShowShadows((eRendererShowShadows)lCurrent);
		}

		if(gpGame->GetInput()->BecameTriggerd("RoomDebug")){
			mbRoomDebug = !mbRoomDebug;
		}

		if(gpGame->GetInput()->BecameTriggerd("BoundingBoxes")){
			mbBoundingBoxes = !mbBoundingBoxes;
		}

		if(gpGame->GetInput()->BecameTriggerd("SoundColliders")){
			mbSoundColliders = !mbSoundColliders;
		}

		if(gpGame->GetInput()->BecameTriggerd("RenderLines")){
			mbRenderLines = !mbRenderLines;
		}

		if(gpGame->GetInput()->BecameTriggerd("LightDebug")){
			mbLightDebug = !mbLightDebug;
		}

		if(gpGame->GetInput()->BecameTriggerd("ColliderDebug")){
			mbColliderDebug = !mbColliderDebug;
		}

		if(gpGame->GetInput()->BecameTriggerd("Areas")){
			mbAreas = !mbAreas;
		}

		if(gpGame->GetInput()->BecameTriggerd("DisplayObjects")){
			mbDisplayObjects = !mbDisplayObjects;
		}

		if(gpGame->GetInput()->BecameTriggerd("Bloom")){
			bool bX = !gpGame->GetGraphics()->GetRendererPostEffects()->GetBloomActive();
			gpGame->GetGraphics()->GetRendererPostEffects()->SetBloomActive(bX);
		}

		tFlag renderFlags =0;

		if(mbLightDebug) renderFlags |= eRendererDebugFlag_DrawLightBoundingBox;
		if(mbRenderLines) renderFlags |= eRendererDebugFlag_RenderLines;

		gpGame->GetGraphics()->GetRenderer3D()->SetDebugFlags(renderFlags);
	}

	void OnDraw()
	{
		cCamera3D *pCam = static_cast<cCamera3D*>(gpGame->GetScene()->GetCamera());

		cRenderList *pRenderList = gpGame->GetGraphics()->GetRenderer3D()->GetRenderList();

		mpFont->Draw(cVector2f(5,17),12,cColor(1,1),eFontAlign_Left,
					_W("Lights: %d"),pRenderList->GetLightNum());

		mpFont->Draw(cVector2f(5,31),12,cColor(1,1),eFontAlign_Left,
					_W("Objects: %d"),pRenderList->GetObjectNum());

		int lPolys =0;
		int lNum=0;
		cRenderableIterator it = pRenderList->GetObjectIt();
		while(it.HasNext())
		{
			iRenderable *pObj = it.Next();

			lPolys += pObj->GetVertexBuffer()->GetVertexNum();
			lNum++;
		}

		mpFont->Draw(cVector2f(5,45),12,cColor(1,1),eFontAlign_Left,
					_W("Polygons: %d"),lPolys);

		tWString sShadows[3] = {_W("All"),_W("Static"),_W("None")};
		mpFont->Draw(cVector2f(5,60),12,cColor(1,1),eFontAlign_Left,
					_W("Shadows: %s"),  sShadows[gpGame->GetGraphics()->GetRenderer3D()->GetShowShadows()].c_str());

		if(mbDisplayObjects)
		{
			mpFont->Draw(cVector2f(5,75),12,cColor(1,1),eFontAlign_Left,_W("Objects (%d) :"),lNum);
			float fY =0;
			float fX =0;
			it = pRenderList->GetObjectIt();
			while(it.HasNext())
			{
				iRenderable *pObj = it.Next();

				mpFont->Draw(cVector2f(5+fX,90+fY),10,cColor(1,1),eFontAlign_Left,_W("%s : %d"),
					cString::To16Char(pObj->GetName()).c_str(),pObj->GetVertexBuffer()->GetVertexNum());
				fY+=11;
				if(fY > 500){
					fY =0;
					fX += 230;
				}
			}
		}
		if(mbLightDebug)
		{
			mpFont->Draw(cVector2f(5,75),12,cColor(1,1),eFontAlign_Left,_W("Lights:"));
			float fY =0;
			float fX =0;
			int idx=0;

			cLight3DIterator LightIt = pRenderList->GetLightIt();
			while(LightIt.HasNext())
			{
				iLight3D *pLight = LightIt.Next();

				mpFont->Draw(cVector2f(5+fX,90+fY),10,cColor(1,1),eFontAlign_Left,_W("%s : %d"),
					cString::To16Char(pLight->GetName()).c_str(),pRenderList->GetLightObjects(idx));

				++idx;
				fY+=11;
				if(fY > 500){
					fY =0;
					fX += 230;
				}
			}
		}


		mpFont->Draw(cVector2f(800,580),12,cColor(1,1),eFontAlign_Right,
			_W("Pos: (%s) Yaw: %f Pitch %f"),cString::To16Char(pCam->GetPosition().ToString()).c_str(),
			pCam->GetYaw(),
			pCam->GetPitch());


		if(mbRoomDebug)
		{
			mpFont->Draw(cVector2f(140,15),12,cColor(1,1),eFontAlign_Left,_W("Visible Rooms:"));

			//Draw list of rooms
			tStringList *pRoomList = mpWorld->GetPortalContainer()->GetVisibleSectorsList();
			tStringListIt it = pRoomList->begin();
			float fRow=0;
			for(; it != pRoomList->end(); ++it, fRow+=1)
			{
				tString &sId = *it;

				mpFont->Draw(cVector2f(140,29+fRow*12),12,cColor(1,1),eFontAlign_Left,cString::To16Char(sId).c_str());
			}
		}
	}

	//--------------------------------------------------------------

	void OnPostGUIDraw()
	{
		cCamera3D *pCam = static_cast<cCamera3D*>(gpGame->GetScene()->GetCamera());

		//Draw screen rect of lights
		mpLowLevelGraphics->SetDepthTestActive(false);
		mpLowLevelGraphics->SetIdentityMatrix(eMatrix_ModelView);

		mpLowLevelGraphics->SetOrthoProjection(mpLowLevelGraphics->GetVirtualSize(),-1000,1000);

		cVector2f vScreenSize = mpLowLevelGraphics->GetScreenSize();
		cRect2l ClipRect;

		//Draw screen rect of lights
		if(mbLightDebug)
		{
			tLight3DList *pLightList = mpWorld->GetLightList();
			tLight3DListIt LightIt = pLightList->begin();
			for(;LightIt != pLightList->end(); ++LightIt)
			{
				iLight3D *pLight = *LightIt;
				cRect2l ClipRect;

				bool bVisible = cMath::GetClipRectFromBV(ClipRect,*pLight->GetBoundingVolume(),
					pCam->GetViewMatrix(),
					pCam->GetProjectionMatrix(),
					pCam->GetNearClipPlane(),
					cVector2l((int)vScreenSize.x,(int)vScreenSize.y));

				if(bVisible)
				{
					cVector3f vVirtSize = mpLowLevelGraphics->GetVirtualSize();
					mpLowLevelGraphics->DrawLineRect2D(cRect2f((float)ClipRect.x,(float)ClipRect.y,
						(float)ClipRect.w,
						(float)ClipRect.h),
						20,cColor(1,0,0,1));
				}
				else
				{
					mpLowLevelGraphics->DrawLineRect2D(cRect2f(0,0,799,599), 20, cColor(0,1,0,1));
				}
			}
		}
	}

	//--------------------------------------------------------------

	void OnPostSceneDraw()
	{
		cCamera3D *pCam = static_cast<cCamera3D*>(gpGame->GetScene()->GetCamera());
		mpLowLevelGraphics->SetMatrix(eMatrix_ModelView, pCam->GetViewMatrix());

		mpLowLevelGraphics->SetTexture(0,NULL);
		mpLowLevelGraphics->SetBlendActive(false);

		mpLowLevelGraphics->SetDepthTestActive(true);

		//mpWorld->DrawMeshBoundingBoxes(cColor(1,1),true);
		/*cMeshEntityIterator meshIt = mpWorld->GetMeshEntityIterator();
		int lColor=0;
		while(meshIt.HasNext())
		{
			cMeshEntity *pMesh = meshIt.Next();

			cColor col = gvSectorColors[lColor % glSectorColorNum];

			mpLowLevelGraphics->DrawBoxMaxMin(pMesh->GetBoundingVolume()->GetMax(),
												pMesh->GetBoundingVolume()->GetMin(),
												col);

			lColor++;
		}*/

		/*if(mpStart)
		{
			cVector3f vPos = mpStart->GetWorldMatrix().GetTranslation();

			cMatrixf mtxInv = cMath::MatrixInverse(mpStart->GetWorldMatrix());

			cVector3f vForward = mtxInv.GetForward();
			cVector3f vUp = mtxInv.GetUp();
			cVector3f vRight = mtxInv.GetRight();

			mpLowLevelGraphics->DrawLine(vPos,vPos + vForward,cColor(0,0,1,1));
			mpLowLevelGraphics->DrawLine(vPos,vPos + vUp,cColor(0,1,0,1));
			mpLowLevelGraphics->DrawLine(vPos,vPos + vRight,cColor(1,0,0,1));
		}*/

		if(mbAreas)
		{
			tAreaEntityMap *pAreaMap = mpWorld->GetAreaEntityMap();

			tAreaEntityMapIt it = pAreaMap->begin();
			for(;it != pAreaMap->end(); ++it)
			{
				cAreaEntity *pArea = it->second;

				mpLowLevelGraphics->SetMatrix(eMatrix_ModelView,
									cMath::MatrixMul(pCam->GetViewMatrix(),pArea->m_mtxTransform));

				mpLowLevelGraphics->DrawBoxMaxMin(pArea->mvSize*0.5f, pArea->mvSize* -0.5f, cColor(1,0.5f,1));
			}

			mpLowLevelGraphics->SetMatrix(eMatrix_ModelView, pCam->GetViewMatrix());
		}


		if(mbColliderDebug){
			mpWorld->GetPhysicsWorld()->RenderDebugGeometry(mpLowLevelGraphics,cColor(1,1,1,1));
		}

		if(mbBoundingBoxes)
		{
			mpWorld->DrawMeshBoundingBoxes(cColor(1,0.5f,1),true);
		}

		if(mbSoundColliders)
		{
			cPhysicsBodyIterator BodyIt = mpWorld->GetPhysicsWorld()->GetBodyIterator();

			while(BodyIt.HasNext())
			{
				iPhysicsBody* pBody = BodyIt.Next();

				if(pBody->GetBlocksSound())
				{
					pBody->RenderDebugGeometry(mpLowLevelGraphics, cColor(0.9f,1,0.1f));
				}
			}
		}

		///////////////////////////////
		/// DEBUG PROTAL COLLIDERS

		/*static bool bOnce = true;

		if(bOnce)
		{
			cBoundingVolume cameraBV;
			cameraBV.SetSize(25.0f);
			cameraBV.SetPosition(pCam->GetPosition());

			cPortalContainerEntityIterator it = mpWorld->GetPortalContainer()->GetEntityIterator(
																						&cameraBV);
			while(it.HasNext())
			{
				iPhysicsBody *pBody = static_cast<iPhysicsBody*>(it.Next());

				Log("Body: %s\n",pBody->GetName().c_str());

				pBody->RenderDebugGeometry(mpLowLevelGraphics,cColor(1,1));
			}
			if(bOnce) bOnce = false;
		}*/


		/// DEBUG PROTAL COLLIDERS
		///////////////////////////////

		if(mbRoomDebug==false) return;
		////////////////////////////////////////////
		// Draw boxes around sectors and portals
		if(mpWorld ==NULL) return;


		tSectorMap *pSectorMap = mpWorld->GetPortalContainer()->GetSectorMap();
		tSectorMapIt SectorIt = pSectorMap->begin();
		int lNum=0;
		for(; SectorIt != pSectorMap->end(); SectorIt++,lNum++)
		{
			cSector *pSector = SectorIt->second;

			cColor Col = gvSectorColors[lNum % glSectorColorNum];

			mpLowLevelGraphics->DrawBoxMaxMin(pSector->GetBV()->GetMax(),pSector->GetBV()->GetMin(),
												Col);

			tPortalList *pPortalList = pSector->GetPortalList();
			tPortalListIt PortIt = pPortalList->begin();
			for(; PortIt != pPortalList->end(); PortIt++)
			{
				cPortal *pPortal = *PortIt;

				mpLowLevelGraphics->DrawBoxMaxMin(pPortal->GetBV()->GetMax(),pPortal->GetBV()->GetMin(),
					cColor(1,0.0f,0.0f,1));

				cVector3f vStart = pPortal->GetBV()->GetWorldCenter();
				cVector3f vEnd = vStart + pPortal->GetNormal()*0.1f;

				mpLowLevelGraphics->DrawLine(vStart, vEnd,Col);

				//GetColor of target sector
				cColor TargCol(0.5f,0.5f,0.5f);
				cSector *pTargetSector =pPortal->GetTargetSector();
				int lPortNum=0;
				tSectorMapIt TargSectorIt = pSectorMap->begin();
				for(; TargSectorIt != pSectorMap->end(); TargSectorIt++,lPortNum++){
					if(pTargetSector == TargSectorIt->second){
						TargCol = gvSectorColors[lPortNum % glSectorColorNum];
						break;
					}
				}

				mpLowLevelGraphics->DrawSphere(vEnd,0.08f,TargCol);
			}
		}
	}


private:
	iFontData *mpFont;

	bool mbRoomDebug;
	bool mbColliderDebug;
	bool mbLightDebug;
	bool mbBoundingBoxes;
	bool mbSoundColliders;
	bool mbRenderLines;
	bool mbAreas;
	bool mbDisplayObjects;

	cStartPosEntity *mpStart;

	cWorld3D* mpWorld;
	iLowLevelGraphics* mpLowLevelGraphics;
};



int hplMain(const tString &asCommandLine)
{
	//iResourceBase::SetLogCreateAndDelete(true);
	//iGpuProgram::SetLogDebugInformation(true);

	// Load config file
	cConfigFile *pConfig = new cConfigFile(_W("viewer_settings.cfg"));
	pConfig->Load();

	int lScreenW = pConfig->GetInt("Screen","Width",800);
	int lScreenH = pConfig->GetInt("Screen","Height",600);
	bool bFullScreen = pConfig->GetBool("Screen", "FullScreen", false);
	bool bVsync = pConfig->GetBool("Screen", "Vsync", false);

	//Init the game engine
	gpGame = new cGame(hplNew( cSDLGameSetup,() ),lScreenW,lScreenH,32,bFullScreen,45);
	gpGame->GetGraphics()->GetLowLevel()->SetVsyncActive(bVsync);

	gpGame->SetLimitFPS(false);

	SetWindowCaption("Scene Viewer");

	//Add resources
	gpGame->GetResources()->LoadResourceDirsFile("resources.cfg");

	if(asCommandLine != ""){

		tStringVec vArgs;
		tString sSepp = " ";
		cString::GetStringVec(asCommandLine,vArgs,&sSepp);

		if(vArgs.size()>=1)	gsSceneFile= vArgs[0];
		//if(vArgs.size()>=2)	gsStartPos = vArgs[1];
		gsStartPos = "link01";
	}


	//Add updates
	cSceneCamera cameraUpdate(gpGame,4,cVector3f(0,0.5f,0),true);
	gpGame->GetUpdater()->AddUpdate("Default", &cameraUpdate);
	cSimpleUpdate Update;
	gpGame->GetUpdater()->AddUpdate("Default", &Update);

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

	cMemoryManager::LogResults();

	return 0;
}
