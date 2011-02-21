/*
 * Copyright 2007-2010 (C) - Frictional Games
 *
 * This file is part of HPL1Engine
 *
 * For conditions of distribution and use, see copyright notice in LICENSE-tests
 */
#include <hpl.h>
#include <impl/SDLGameSetup.h>

#include <GL/GLee.h>

#include "../Common/SimpleCamera.h"

//#ifdef DEBUG
//#pragma comment(lib, "HPL.lib")
//#else
//#pragma comment(lib, "HPLd.lib")
//#endif

using namespace hpl;

cGame *gpGame=NULL;

class cSimpleUpdate : public iUpdateable
{
public:
	cSimpleUpdate() : iUpdateable("SimpleUpdate")
	{
		mpLowLevelGraphics = gpGame->GetGraphics()->GetLowLevel();

		//Create a world for the game objects to be in.
		mpWorld = gpGame->GetScene()->CreateWorld3D("Test");
		gpGame->GetScene()->SetWorld3D(mpWorld);

		//Set up some variables
		mlRenderMode = 2;
		mbPostEffects = false;
		mbGraphics2D = true;

		//////////////////////////////////////
		//Setup some buttons
		//1: Flat
		//2: Alpha
		//3: Simple CG
		//4: Simple CG w Alpha
		//5: Lighting CG
		//6: Lighting CG w Alpha
		//P: Posteffects on and off
		//G: 2D Graphics
		for(int i=0; i<6; ++i)
		{
			gpGame->GetInput()->AddAction(new cActionKeyboard(cString::ToString(i+1),
											gpGame->GetInput(),eKey (eKey_1+i) ));
		}
		gpGame->GetInput()->AddAction(new cActionKeyboard("PostEffects",gpGame->GetInput(),eKey (eKey_p)));
		gpGame->GetInput()->AddAction(new cActionKeyboard("Graphics2D",gpGame->GetInput(),eKey (eKey_g)));

		//////////////////////////////////////
		//Create a mesh and entity
		cMesh *pMesh =NULL;
		pMesh = gpGame->GetResources()->GetMeshManager()->CreateMesh("Woodbox.dae");
		mpEntity = mpWorld->CreateMeshEntity("Test",pMesh,false);
		mpEntity->SetPosition(cVector3f(0,0,0));

		//Do not render the entity, only us the PostScene function for rendering.
		mpEntity->SetVisible(false);

		//Background
		pMesh = gpGame->GetResources()->GetMeshManager()->CreateMesh("inverse_box.dae");
		mpBackground = mpWorld->CreateMeshEntity("Back",pMesh,false);
		mpBackground->SetMatrix(cMath::MatrixScale(cVector3f(4,4,4)));
		mpBackground->SetPosition(cVector3f(0,0,0));

		mpBackground->SetVisible(false);

		//////////////////////////////////////
        //Create a light and set it up
		mpLight = mpWorld->CreateLightPoint("Light");
		mpLight->SetFarAttenuation(44);
		mpLight->SetDiffuseColor(cColor(1,1,1,1));
		mpLight->SetCastShadows(true);
		mpLight->SetPosition(cVector3f(0,1,1));

		//////////////////////////////////////
		//Create simple vertex and fragment programs
		cGpuProgramManager *pProgramManager = gpGame->GetResources()->GetGpuProgramManager();
		mpSimpleProgramVP = pProgramManager->CreateProgram("SimpleVP.cg","main",eGpuProgramType_Vertex);
		mpSimpleProgramFP = pProgramManager->CreateProgram("SimpleFP.cg","main",eGpuProgramType_Fragment);

		mpLightProgramVP = pProgramManager->CreateProgram("LightVP.cg","main",eGpuProgramType_Vertex);
		mpLightProgramFP = pProgramManager->CreateProgram("LightFP.cg","main",eGpuProgramType_Fragment);

		mpPostEffectProgramFP = pProgramManager->CreateProgram("PostEffectFP.cg","main",eGpuProgramType_Fragment);


		//////////////////////////////////////
		//Dummy texture
		cTextureManager *pTextureManager = gpGame->GetResources()->GetTextureManager();

		mpDummyTexture = pTextureManager->Create2D("testar.jpg",true);

		//////////////////////////////////////
		//Create the screen buffer
		mpScreenBuffer = mpLowLevelGraphics->CreateTexture(cVector2l(800,600),
														32,cColor(0,0,0,0),false,
														eTextureType_Normal, eTextureTarget_Rect);
		//set wrap mode to clamp
		mpScreenBuffer->SetWrapS(eTextureWrap_ClampToEdge);
		mpScreenBuffer->SetWrapT(eTextureWrap_ClampToEdge);

		//Load a font
		mpFont = gpGame->GetResources()->GetFontManager()->CreateFontData("viewer.fnt",12,32,128);

		//2d graphics
		mpGfxDrawer = gpGame->GetGraphics()->GetDrawer();
		mpGfxObj = mpGfxDrawer->CreateGfxObject("floor_grate01.tga","diffalpha2d");
	}

	//----------------------------------------------------------

	~cSimpleUpdate()
	{
		if(mpScreenBuffer) delete mpScreenBuffer;
	}

	//----------------------------------------------------------

	void Update(float afTimeStep)
	{
		for(int i=0; i<6; ++i)
		{
			if(gpGame->GetInput()->BecameTriggerd(cString::ToString(i+1)))
			{
				mlRenderMode = i;
			}
		}

		if(gpGame->GetInput()->BecameTriggerd("PostEffects"))
		{
			mbPostEffects = !mbPostEffects;
		}

		if(gpGame->GetInput()->BecameTriggerd("Graphics2D"))
		{
			mbGraphics2D = !mbGraphics2D;
		}
	}

	//----------------------------------------------------------

	void OnDraw()
	{
		if(mbGraphics2D==false) return;

		mpFont->Draw(	cVector3f(5,5,0),14,cColor(1,1),eFontAlign_Left,
						_W("WRITING SOME TEXT!"));

		mpGfxDrawer->DrawGfxObject(mpGfxObj,cVector3f(5, 40, 5),50,cColor(1,1));
	}

	//----------------------------------------------------------

	void OnPostGUIDraw()
	{
		if(mbPostEffects==false) return;

		cCamera3D *pCam = static_cast<cCamera3D*>(gpGame->GetScene()->GetCamera());

		//Set up the projection so we have 2D rendering
		mpLowLevelGraphics->SetDepthTestActive(false);
		mpLowLevelGraphics->SetIdentityMatrix(eMatrix_ModelView);
		mpLowLevelGraphics->SetOrthoProjection(mpLowLevelGraphics->GetVirtualSize(),-1000,1000);

		//Copy the current context (screen) to buffer texture
		mpLowLevelGraphics->CopyContextToTexure(mpScreenBuffer,0,cVector2l(800,600));

        //Set up a vertex vec (not that the buffer has non normalized coordinates, since it is of the
		//type "RECT", notice also that the uv coords have y=800 at top
		tVertexVec vVertexVec; vVertexVec.resize(4);
		vVertexVec[0] = cVertex(cVector3f(0,0,0),		cVector3f(0,600,0),		cColor(1,1));
		vVertexVec[1] = cVertex(cVector3f(800,0,0),		cVector3f(800,600,0),	cColor(1,1));
		vVertexVec[2] = cVertex(cVector3f(800,600,0),	cVector3f(800,0,0),		cColor(1,1));
		vVertexVec[3] = cVertex(cVector3f(0,600,0),		cVector3f(0,0,0),		cColor(1,1));

		//Setup blending
		mpLowLevelGraphics->SetBlendActive(true);
		mpLowLevelGraphics->SetBlendFunc(eBlendFunc_One,eBlendFunc_Zero);

		mpLowLevelGraphics->SetTexture(0,mpScreenBuffer);

		//mpSimpleProgramVP->Bind();
		mpPostEffectProgramFP->Bind();
		mpPostEffectProgramFP->SetFloat("timeCount",(float) gpGame->GetGameTime());

		//Draw
		mpLowLevelGraphics->DrawQuad(vVertexVec);

		mpPostEffectProgramFP->UnBind();
		//mpSimpleProgramVP->UnBind();
	}

	//----------------------------------------------------------

	void OnPostSceneDraw()
	{
		//Set up the base rendering params
		mpLowLevelGraphics->SetDepthTestActive(true);

		mpLowLevelGraphics->SetBlendActive(true);
		mpLowLevelGraphics->SetBlendFunc(eBlendFunc_One,eBlendFunc_Zero);

		RenderBackgroundZ(mpBackground->GetSubMeshEntity(0));

		//Iterate the sub meshes of the model
		for(int i=0; i<  mpEntity->GetSubMeshEntityNum(); i++)
		{
			switch(mlRenderMode)
			{
			case 0: RenderFlatSubMesh(mpEntity->GetSubMeshEntity(i)); break;
			case 1: RenderAlphaSubMesh(mpEntity->GetSubMeshEntity(i)); break;
			case 2: RenderSimpleCgSubMesh(mpEntity->GetSubMeshEntity(i), false); break;
			case 3: RenderSimpleCgSubMesh(mpEntity->GetSubMeshEntity(i), true); break;
			case 4: RenderLightingCgSubMesh(mpEntity->GetSubMeshEntity(i),false); break;
			case 5: RenderLightingCgSubMesh(mpEntity->GetSubMeshEntity(i),true); break;
			}
		}

		RenderBackgroundColor(mpBackground->GetSubMeshEntity(0));

		mpLowLevelGraphics->SetBlendActive(false);

		mpLowLevelGraphics->SetDepthTestActive(true);
		mpLowLevelGraphics->SetDepthWriteActive(true);
	}

	//----------------------------------------------------------

	void RenderBackgroundZ(cSubMeshEntity *pSubEntity)
	{
		cCamera3D *pCam = static_cast<cCamera3D*>(gpGame->GetScene()->GetCamera());

		//Get the data of the sub mesh
		cSubMesh *pSubMesh = pSubEntity->GetSubMesh();
		iVertexBuffer *pVtxBuffer = pSubEntity->GetVertexBuffer();
		iMaterial *pMaterial = pSubEntity->GetMaterial();
		iTexture *pTexture = pMaterial->GetTexture(eMaterialTexture_Diffuse);

		//Set matrix for the sub mesh
		cMatrixf mtxModel = cMath::MatrixMul(pCam->GetViewMatrix(), pSubEntity->GetWorldMatrix());
		mpLowLevelGraphics->SetMatrix(eMatrix_ModelView, mtxModel);

		//Set the texture
		mpLowLevelGraphics->SetTexture(0,pTexture);

		//Bind vertex buffer
		pVtxBuffer->Bind();

		//Setup rendering
		// Normal depth buffer and turn off all color rendering (increases render speed)
		mpLowLevelGraphics->SetDepthWriteActive(true);
		mpLowLevelGraphics->SetDepthTestFunc(eDepthTestFunc_LessOrEqual);
		mpLowLevelGraphics->SetColorWriteActive(false,false,false,false);

		//Draw only to depth buffer
		// (Test removing this, nothing will be drawn..)
		pVtxBuffer->Draw();

		mpLowLevelGraphics->SetColorWriteActive(true,true,true,true);
		mpLowLevelGraphics->SetDepthWriteActive(false);
	}

	void RenderBackgroundColor(cSubMeshEntity *pSubEntity)
	{
		cCamera3D *pCam = static_cast<cCamera3D*>(gpGame->GetScene()->GetCamera());

		//Get the data of the sub mesh
		cSubMesh *pSubMesh = pSubEntity->GetSubMesh();
		iVertexBuffer *pVtxBuffer = pSubEntity->GetVertexBuffer();
		iMaterial *pMaterial = pSubEntity->GetMaterial();
		iTexture *pTexture = pMaterial->GetTexture(eMaterialTexture_Diffuse);

		//Set matrix for the sub mesh
		cMatrixf mtxModel = cMath::MatrixMul(pCam->GetViewMatrix(), pSubEntity->GetWorldMatrix());
		mpLowLevelGraphics->SetMatrix(eMatrix_ModelView, mtxModel);

		//Set the texture
		mpLowLevelGraphics->SetTexture(0,pTexture);

		//Bind vertex buffer
		pVtxBuffer->Bind();

		//Setup rendering
		// Normal depth buffer and turn off all color rendering (increases render speed)
		mpLowLevelGraphics->SetDepthWriteActive(false);

		//Draw only to depth buffer
		// (Test removing this, nothing will be drawn..)
		pVtxBuffer->Draw();
	}

	//----------------------------------------------------------


	//////////////////////////////////////////////////
	// Draw the sub mesh flat without any shading
	void RenderFlatSubMesh(cSubMeshEntity *pSubEntity)
	{
		cCamera3D *pCam = static_cast<cCamera3D*>(gpGame->GetScene()->GetCamera());

		//Get the data of the sub mesh
		cSubMesh *pSubMesh = pSubEntity->GetSubMesh();
		iVertexBuffer *pVtxBuffer = pSubEntity->GetVertexBuffer();
		iMaterial *pMaterial = pSubEntity->GetMaterial();
		iTexture *pTexture = pMaterial->GetTexture(eMaterialTexture_Diffuse);

		//Set matrix for the sub mesh
		cMatrixf mtxModel = cMath::MatrixMul(pCam->GetViewMatrix(), pSubEntity->GetWorldMatrix());
		//mtxModel = cMath::MatrixMul(mtxModel, cMath::MatrixRotateX(cMath::ToRad(35)));
		mpLowLevelGraphics->SetMatrix(eMatrix_ModelView, mtxModel);


		//Set the texture to be used, not setting this will only draw vertex colors.
		mpLowLevelGraphics->SetTexture(0,pTexture);

		//Setup depth buffer behaviour,
		mpLowLevelGraphics->SetDepthWriteActive(true);
		mpLowLevelGraphics->SetDepthTestFunc(eDepthTestFunc_LessOrEqual);

		//cPlanef plane(0,1,0,-1);
		//plane.FromNormalPoint(cVector3f(0,1,0),cVector3f(0,0.35f,0));
		//mpLowLevelGraphics->SetClipPlane(0, plane);
		//mpLowLevelGraphics->SetClipPlaneActive(0, true);

		//Draw vertex buffer
		pVtxBuffer->Bind();
		pVtxBuffer->Draw();
		pVtxBuffer->UnBind();

		mpLowLevelGraphics->SetClipPlaneActive(0, false);

	}

	//----------------------------------------------------------

	//////////////////////////////////////////////////
	// Draw the sub mesh using alpha blending
	// Also, we try to copy the engine rendering by first drawing only to the z-buffer
	void RenderAlphaSubMesh(cSubMeshEntity *pSubEntity)
	{
		cCamera3D *pCam = static_cast<cCamera3D*>(gpGame->GetScene()->GetCamera());

		//Get the data of the sub mesh
		cSubMesh *pSubMesh = pSubEntity->GetSubMesh();
		iVertexBuffer *pVtxBuffer = pSubEntity->GetVertexBuffer();
		iMaterial *pMaterial = pSubEntity->GetMaterial();
		iTexture *pTexture = pMaterial->GetTexture(eMaterialTexture_Diffuse);

		//Set matrix for the sub mesh
		cMatrixf mtxModel = cMath::MatrixMul(pCam->GetViewMatrix(), pSubEntity->GetWorldMatrix());
		mpLowLevelGraphics->SetMatrix(eMatrix_ModelView, mtxModel);

		//Set the texture
		mpLowLevelGraphics->SetTexture(0,pTexture);

		//Alpha test
		// Here we set up a test so texels with alpha below 0.5 (1 = max) is not drawm
		mpLowLevelGraphics->SetAlphaTestActive(true);
		mpLowLevelGraphics->SetAlphaTestFunc(eAlphaTestFunc_GreaterOrEqual, 0.5f);

		//Bind vertex buffer
		pVtxBuffer->Bind();

		////////////////////////////////////////
		// FIRST PASS - Rendering z buffer

		//Setup rendering
		// Normal depth buffer and turn off all color rendering (increases render speed)
		mpLowLevelGraphics->SetDepthWriteActive(true);
		mpLowLevelGraphics->SetDepthTestFunc(eDepthTestFunc_LessOrEqual);
		mpLowLevelGraphics->SetColorWriteActive(false,false,false,false);

		//Draw only to depth buffer
		// (Test removing this, nothing will be drawn..)
		pVtxBuffer->Draw();

		//Alpha testing only needed during z buffer rendering
		mpLowLevelGraphics->SetAlphaTestActive(false);

		////////////////////////////////////////
		// SECOND PASS - Rendering color

		//Setup rendering
		// No need to write to the z buffer and only draw if it is the exact same value
		// as the z buffer, this is needed for transparent areas to work and it also
		// when you have several light sources. When you have several light sources, you simply
		// rendered the model several times additively
		mpLowLevelGraphics->SetDepthWriteActive(false);
		mpLowLevelGraphics->SetDepthTestFunc(eDepthTestFunc_Equal);
		mpLowLevelGraphics->SetColorWriteActive(true,true,true,true);
		mpLowLevelGraphics->SetAlphaTestActive(false);

		//Draw colors
		pVtxBuffer->Draw();

		pVtxBuffer->UnBind();
	}

	//----------------------------------------------------------

	//////////////////////////////////////////////////
	// Render the cube using a simple cg program
	void RenderSimpleCgSubMesh(cSubMeshEntity *pSubEntity, bool abUseAlpha)
	{
		cCamera3D *pCam = static_cast<cCamera3D*>(gpGame->GetScene()->GetCamera());

		//Get the data of the sub mesh
		cSubMesh *pSubMesh = pSubEntity->GetSubMesh();
		iVertexBuffer *pVtxBuffer = pSubEntity->GetVertexBuffer();
		iMaterial *pMaterial = pSubEntity->GetMaterial();
		iTexture *pTexture = pMaterial->GetTexture(eMaterialTexture_Diffuse);

		//Set matrix for the sub mesh
		cMatrixf mtxModel = cMath::MatrixMul(pCam->GetViewMatrix(), pSubEntity->GetWorldMatrix());
		mpLowLevelGraphics->SetMatrix(eMatrix_ModelView, mtxModel);

		//Set the texture
		//Since we are binding the texture directly to the program this can be set to NULL.
		mpLowLevelGraphics->SetTexture(0,NULL);

		//Bind vertex buffer
		pVtxBuffer->Bind();

		//Setup alpha test if needed.
		if(abUseAlpha)
		{
			mpLowLevelGraphics->SetAlphaTestActive(true);
			mpLowLevelGraphics->SetAlphaTestFunc(eAlphaTestFunc_GreaterOrEqual, 0.5f);
		}

		///////////////////////////////////////////////
		// FIRST PASS - Rendering z buffer

		//Set up rendering
		mpLowLevelGraphics->SetDepthWriteActive(true);
		mpLowLevelGraphics->SetDepthTestFunc(eDepthTestFunc_LessOrEqual);
		mpLowLevelGraphics->SetColorWriteActive(false,false,false,false);

		//Setup vertex program
		//This is needed for ATI cards, else the vertex buffer will not be transformted in the
		//same way as with the vertex buffer using the vertex program and z problems will occur.
		mpSimpleProgramVP->Bind();
		mpSimpleProgramVP->SetMatrixf(	"worldViewProj",eGpuProgramMatrix_ViewProjection,
										eGpuProgramMatrixOp_Identity);

		//Set texture
		mpLowLevelGraphics->SetTexture(0,pTexture);

		//Draw only to depth buffer
		pVtxBuffer->Draw();

		//Unbind program
		mpSimpleProgramVP->UnBind();

		//Alpha testing only needed during z buffer rendering
		if(abUseAlpha)
		{
			mpLowLevelGraphics->SetAlphaTestActive(false);
		}

		///////////////////////////////////////////////
		// SECOND PASS - Rendering color

		//Setup rendering
		mpLowLevelGraphics->SetDepthWriteActive(false);
		mpLowLevelGraphics->SetDepthTestFunc(eDepthTestFunc_Equal);
		mpLowLevelGraphics->SetColorWriteActive(true,true,true,true);

		//Setup vertex program
		mpSimpleProgramVP->Bind();
		mpSimpleProgramVP->SetMatrixf(	"worldViewProj",eGpuProgramMatrix_ViewProjection,
										eGpuProgramMatrixOp_Identity);

		//Setup fragment program
		mpSimpleProgramFP->Bind();
		mpSimpleProgramFP->SetFloat("timeCount",(float) gpGame->GetGameTime());

		//Set the texture directly to the fragment program using parameter name
		//mpSimpleProgramFP->SetTexture("diffuseMap",pTexture);

		//Set texture direct to fragment program unit
		mpSimpleProgramFP->SetTextureToUnit(0,pTexture);

		//Draw vertex buffer
		pVtxBuffer->Draw();

		//Unbinf vertex buffer
		pVtxBuffer->UnBind();

		//Unbind programs
		mpSimpleProgramFP->UnBind();
		mpSimpleProgramVP->UnBind();
	}

	//----------------------------------------------------------

	//////////////////////////////////////////////////
	// Render the cube using a lighting cg program
	void RenderLightingCgSubMesh(cSubMeshEntity *pSubEntity, bool abUseAlpha)
	{
		cCamera3D *pCam = static_cast<cCamera3D*>(gpGame->GetScene()->GetCamera());

		//Get the data of the sub mesh
		cSubMesh *pSubMesh = pSubEntity->GetSubMesh();
		iVertexBuffer *pVtxBuffer = pSubEntity->GetVertexBuffer();
		iMaterial *pMaterial = pSubEntity->GetMaterial();
		iTexture *pTexture = pMaterial->GetTexture(eMaterialTexture_Diffuse);

		//Set matrix for the sub mesh
		cMatrixf mtxModel = cMath::MatrixMul(pCam->GetViewMatrix(), pSubEntity->GetWorldMatrix());
		mpLowLevelGraphics->SetMatrix(eMatrix_ModelView, mtxModel);

		//Set the texture
		mpLowLevelGraphics->SetTexture(0,pTexture);

		//Bind vertex buffer
		pVtxBuffer->Bind();

		//Setup alpha test if needed.
		if(abUseAlpha)
		{
			mpLowLevelGraphics->SetAlphaTestActive(true);
			mpLowLevelGraphics->SetAlphaTestFunc(eAlphaTestFunc_GreaterOrEqual, 0.5f);
		}

		///////////////////////////////////////////////
		// FIRST PASS - Rendering z buffer

		//Set up rendering
		mpLowLevelGraphics->SetDepthWriteActive(true);
		mpLowLevelGraphics->SetDepthTestFunc(eDepthTestFunc_LessOrEqual);
		mpLowLevelGraphics->SetColorWriteActive(false,false,false,false);

		//Setup vertex program
		//This is needed for ATI cards, else the vertex buffer will not be transformted in the
		//same way as with the vertex buffer using the vertex program and z problems will occur.
		mpSimpleProgramVP->Bind();
		mpSimpleProgramVP->SetMatrixf(	"worldViewProj",eGpuProgramMatrix_ViewProjection,
										eGpuProgramMatrixOp_Identity);


		//Draw only to depth buffer
		pVtxBuffer->Draw();

		//Unbind program
		mpSimpleProgramVP->UnBind();

		//Alpha testing only needed during z buffer rendering
		if(abUseAlpha)
		{
			mpLowLevelGraphics->SetAlphaTestActive(false);
		}

		///////////////////////////////////////////////
		// SECOND PASS - Rendering color

		//Setup rendering
		mpLowLevelGraphics->SetDepthWriteActive(false);
		mpLowLevelGraphics->SetDepthTestFunc(eDepthTestFunc_Equal);
		mpLowLevelGraphics->SetColorWriteActive(true,true,true,true);

		//Setup vertex program
		mpLightProgramVP->Bind();
		mpLightProgramVP->SetMatrixf(	"worldViewProj",eGpuProgramMatrix_ViewProjection,
										eGpuProgramMatrixOp_Identity);
		mpLightProgramVP->SetVec3f("LightPos", mpLight->GetWorldPosition());
		mpLightProgramVP->SetColor4f("LightColor",mpLight->GetDiffuseColor());

		//Setup fragment program
		mpLightProgramFP->Bind();

		//Draw in color
		pVtxBuffer->Draw();

		//Unbind programs
		mpLightProgramFP->UnBind();
		mpLightProgramVP->UnBind();

		//Unbind vertex buffer
		pVtxBuffer->UnBind();
	}

	//----------------------------------------------------------



private:
	cMeshEntity *mpEntity;
	cMeshEntity *mpBackground;

	iFontData *mpFont;
	cGfxObject *mpGfxObj;
	cGraphicsDrawer *mpGfxDrawer;

	iGpuProgram *mpSimpleProgramVP;
	iGpuProgram *mpSimpleProgramFP;

	iGpuProgram *mpLightProgramVP;
	iGpuProgram *mpLightProgramFP;

	iGpuProgram *mpPostEffectProgramFP;

	iTexture *mpDummyTexture;
	iTexture *mpDummyTexture2;

	iTexture *mpScreenBuffer;

	int mlRenderMode;
	bool mbPostEffects;
	bool mbGraphics2D;

	iLowLevelGraphics* mpLowLevelGraphics;
	cWorld3D* mpWorld;

	iLight3D *mpLight;
	iLight3D *mpLight2;
};



int hplMain(const tString& asArg)
{
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

	cSimpleCamera cameraUpdate(gpGame,5,cVector3f(0,0,2),false);
	gpGame->GetUpdater()->AddUpdate("Default", &cameraUpdate);

	//Run the engine
	gpGame->Run();

	//Delete the engine
	delete gpGame;

	return 0;
}
