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
#include "impl/VertexBufferVBO.h"
#include "system/LowLevelSystem.h"
#include "math/Math.h"

#include <memory.h>

#include <GLee.h>


namespace hpl {

#define BUFFER_OFFSET(i) ((void*)(i*sizeof(float)))

	//////////////////////////////////////////////////////////////////////////
	// CONSTRUCTORS
	//////////////////////////////////////////////////////////////////////////

	//-----------------------------------------------------------------------

	cVertexBufferVBO::cVertexBufferVBO(iLowLevelGraphics* apLowLevelGraphics,tVertexFlag aFlags,
		eVertexBufferDrawType aDrawType,eVertexBufferUsageType aUsageType,
		int alReserveVtxSize,int alReserveIdxSize) :
	iVertexBuffer(apLowLevelGraphics, aFlags, aDrawType,aUsageType, alReserveVtxSize, alReserveIdxSize)
	{
		if(alReserveVtxSize>0)
		{
			for(int i=0;i< klNumOfVertexFlags; i++)
			{
				if(aFlags & kvVertexFlags[i])
				{
					mvVertexArray[i].reserve(alReserveVtxSize * kvVertexElements[i]);
				}

				mvArrayHandle[i] = 0;
			}
		}

		if(alReserveIdxSize>0)
			mvIndexArray.reserve(alReserveIdxSize);

		mlElementHandle =0;

		mbTangents = false;

		mbCompiled = false;

		mbHasShadowDouble = false;

		mpLowLevelGraphics = apLowLevelGraphics;
	}

	cVertexBufferVBO::~cVertexBufferVBO()
	{
		for(int i=0;i< klNumOfVertexFlags; i++)
		{
			mvVertexArray[i].clear();
			if(mVertexFlags & kvVertexFlags[i])
			{
				glDeleteBuffersARB(1,(GLuint *)&mvArrayHandle[i]);
			}
		}

		mvIndexArray.clear();

		glDeleteBuffersARB(1,(GLuint *)&mlElementHandle);
	}

	//-----------------------------------------------------------------------

	//////////////////////////////////////////////////////////////////////////
	// PUBLIC METHODS
	//////////////////////////////////////////////////////////////////////////

	//-----------------------------------------------------------------------

	void cVertexBufferVBO::AddVertex(tVertexFlag aType,const cVector3f& avVtx)
	{
		int idx = cMath::Log2ToInt((int)aType);

		mvVertexArray[idx].push_back(avVtx.x);
		mvVertexArray[idx].push_back(avVtx.y);
		mvVertexArray[idx].push_back(avVtx.z);
		if(kvVertexElements[idx]==4)
			mvVertexArray[idx].push_back(1);
	}

	//-----------------------------------------------------------------------

	void cVertexBufferVBO::AddColor(tVertexFlag aType,const cColor& aColor)
	{
		int idx = cMath::Log2ToInt((int)aType);

		mvVertexArray[idx].push_back(aColor.r);
		mvVertexArray[idx].push_back(aColor.g);
		mvVertexArray[idx].push_back(aColor.b);
		mvVertexArray[idx].push_back(aColor.a);
	}

	//-----------------------------------------------------------------------

	void cVertexBufferVBO::AddIndex(unsigned int alIndex)
	{
		mvIndexArray.push_back(alIndex);
	}

	//-----------------------------------------------------------------------

	cBoundingVolume cVertexBufferVBO::CreateBoundingVolume()
	{
		cBoundingVolume bv;

		int lNum = cMath::Log2ToInt((int)eVertexFlag_Position);

		bv.AddArrayPoints(&(mvVertexArray[lNum][0]), GetVertexNum());
		bv.CreateFromPoints(kvVertexElements[cMath::Log2ToInt(eVertexFlag_Position)]);

		return bv;
	}

	//-----------------------------------------------------------------------

	bool cVertexBufferVBO::Compile(tVertexCompileFlag aFlags)
	{
		if(mbCompiled) return false;
		mbCompiled = true;

		//Create tangents
		if(aFlags & eVertexCompileFlag_CreateTangents)
		{
			mbTangents = true;

			mVertexFlags |= eVertexFlag_Texture1;

			int idx = cMath::Log2ToInt((int)eVertexFlag_Texture1);

			int lSize = GetVertexNum()*4;
			mvVertexArray[idx].resize(lSize);

			cMath::CreateTriTangentVectors(&(mvVertexArray[cMath::Log2ToInt((int)eVertexFlag_Texture1)][0]),
				&mvIndexArray[0], GetIndexNum(),

				&(mvVertexArray[cMath::Log2ToInt((int)eVertexFlag_Position)][0]),
				kvVertexElements[cMath::Log2ToInt((int)eVertexFlag_Position)],

				&(mvVertexArray[cMath::Log2ToInt((int)eVertexFlag_Texture0)][0]),
				&(mvVertexArray[cMath::Log2ToInt((int)eVertexFlag_Normal)][0]),
				GetVertexNum()
				);
		}

		GLenum usageType = GL_STATIC_DRAW_ARB;
		if(mUsageType== eVertexBufferUsageType_Dynamic) usageType = GL_DYNAMIC_DRAW_ARB;
		else if(mUsageType== eVertexBufferUsageType_Stream) usageType = GL_STREAM_DRAW_ARB;

		//Create the VBO vertex arrays
		for(int i=0;i< klNumOfVertexFlags; i++)
		{
			if(mVertexFlags & kvVertexFlags[i])
			{
				glGenBuffersARB(1,(GLuint *)&mvArrayHandle[i]);
				glBindBufferARB(GL_ARRAY_BUFFER_ARB, mvArrayHandle[i]);

				glBufferDataARB(GL_ARRAY_BUFFER_ARB, mvVertexArray[i].size()*sizeof(float),
						&(mvVertexArray[i][0]), usageType);

				glBindBufferARB(GL_ARRAY_BUFFER_ARB, 0);

				//Log("%d-Handle: %d, size: %d \n",i,mvArrayHandle[i], mvVertexArray);
			}
		}

	    //Create the VBO index array
		glGenBuffersARB(1,(GLuint *)&mlElementHandle);
		glBindBufferARB(GL_ELEMENT_ARRAY_BUFFER_ARB,mlElementHandle);
		glBufferDataARB(GL_ELEMENT_ARRAY_BUFFER_ARB, GetIndexNum()*sizeof(unsigned int),
														&mvIndexArray[0], usageType);
		glBindBufferARB(GL_ELEMENT_ARRAY_BUFFER_ARB,0);

		//Log("VBO compile done!\n");

		return true;
	}


	//-----------------------------------------------------------------------

	void cVertexBufferVBO::UpdateData(tVertexFlag aTypes, bool abIndices)
	{
		GLenum usageType = GL_STATIC_DRAW_ARB;
		if(mUsageType== eVertexBufferUsageType_Dynamic) usageType = GL_DYNAMIC_DRAW_ARB;
		else if(mUsageType== eVertexBufferUsageType_Stream) usageType = GL_STREAM_DRAW_ARB;

		//Create the VBO vertex arrays
		for(int i=0;i< klNumOfVertexFlags; i++)
		{
			if((mVertexFlags & kvVertexFlags[i]) && (aTypes & kvVertexFlags[i]))
			{
				glBindBufferARB(GL_ARRAY_BUFFER_ARB, mvArrayHandle[i]);

				//This was apparently VERY slow.
				glBufferDataARB(GL_ARRAY_BUFFER_ARB, mvVertexArray[i].size()*sizeof(float),
					NULL, usageType);//Clear memory

				glBufferDataARB(GL_ARRAY_BUFFER_ARB, mvVertexArray[i].size()*sizeof(float),
					&(mvVertexArray[i][0]), usageType);
			}
		}
		glBindBufferARB(GL_ARRAY_BUFFER_ARB, 0);

		//Create the VBO index array
		if(abIndices)
		{
			glBindBufferARB(GL_ELEMENT_ARRAY_BUFFER_ARB,mlElementHandle);

			//glBufferDataARB(GL_ELEMENT_ARRAY_BUFFER_ARB,GetIndexNum()*sizeof(unsigned int),
			//	NULL, usageType);

			glBufferDataARB(GL_ELEMENT_ARRAY_BUFFER_ARB, GetIndexNum()*sizeof(unsigned int),
				&mvIndexArray[0], usageType);

			glBindBufferARB(GL_ELEMENT_ARRAY_BUFFER_ARB,0);
		}
	}

	//-----------------------------------------------------------------------

	void cVertexBufferVBO::CreateShadowDouble(bool abUpdateData)
	{
		int lIdx = cMath::Log2ToInt(eVertexFlag_Position);

		//Set to new size.
		int lSize = (int)mvVertexArray[lIdx].size();
		mvVertexArray[lIdx].reserve(lSize*2);

		int lCount = lSize /4;
		for(int i=0; i< lCount; i++)
		{
			mvVertexArray[lIdx].push_back(mvVertexArray[lIdx][i*4+0]);
			mvVertexArray[lIdx].push_back(mvVertexArray[lIdx][i*4+1]);
			mvVertexArray[lIdx].push_back(mvVertexArray[lIdx][i*4+2]);
			mvVertexArray[lIdx].push_back(0);//0);
		}

		mbHasShadowDouble = true;

		if(abUpdateData)
		{
			UpdateData(eVertexFlag_Position, false);
		}
	}

	//-----------------------------------------------------------------------

	void cVertexBufferVBO::Transform(const cMatrixf &a_mtxTransform)
	{
		float *pPosArray = GetArray(eVertexFlag_Position);
		float *pNormalArray = GetArray(eVertexFlag_Normal);
		float *pTangentArray = NULL;
		if(mbTangents)pTangentArray = GetArray(eVertexFlag_Texture1);

		int lVtxNum = GetVertexNum();

		cMatrixf mtxRot = a_mtxTransform.GetRotation();

		int lVtxStride = kvVertexElements[cMath::Log2ToInt(eVertexFlag_Position)];

		int lOffset = GetVertexNum()*4;

		for(int i=0; i<lVtxNum; i++)
		{
			float* pPos = &pPosArray[i*lVtxStride];
			float* pNorm = &pNormalArray[i*3];
			float* pTan = NULL;
			if(mbTangents)pTan = &pTangentArray[i*4];

			cVector3f vPos = cMath::MatrixMul(a_mtxTransform, cVector3f(pPos[0],pPos[1],pPos[2]));
			pPos[0] = vPos.x; pPos[1] = vPos.y; pPos[2] = vPos.z;

			if(mbHasShadowDouble){
				float* pExtraPos = &pPosArray[i*lVtxStride + lOffset];
				pExtraPos[0] = vPos.x; pExtraPos[1] = vPos.y; pExtraPos[2] = vPos.z;
			}

			cVector3f vNorm = cMath::MatrixMul(mtxRot, cVector3f(pNorm[0],pNorm[1],pNorm[2]));
			vNorm.Normalise();
			pNorm[0] = vNorm.x; pNorm[1] = vNorm.y; pNorm[2] = vNorm.z;

			if(mbTangents){
				cVector3f vTan = cMath::MatrixMul(mtxRot, cVector3f(pTan[0],pTan[1],pTan[2]));
				vTan.Normalise();
				pTan[0] = vTan.x; pTan[1] = vTan.y; pTan[2] = vTan.z;
			}
		}

		if(mbCompiled)
		{
			if(mbTangents)
				UpdateData(eVertexFlag_Position | eVertexFlag_Normal | eVertexFlag_Texture1,false);
			else
				UpdateData(eVertexFlag_Position | eVertexFlag_Normal,false);
		}
	}

	//-----------------------------------------------------------------------

	void cVertexBufferVBO::Draw(eVertexBufferDrawType aDrawType)
	{
		eVertexBufferDrawType drawType = aDrawType == eVertexBufferDrawType_LastEnum ? mDrawType : aDrawType;

		///////////////////////////////
		//Get the draw type
		GLenum mode = GL_TRIANGLES;
		if(drawType==eVertexBufferDrawType_Quad)		mode = GL_QUADS;
		else if(drawType==eVertexBufferDrawType_Lines)	mode = GL_LINE_STRIP;

		//////////////////////////////////
		//Bind and draw the buffer
		glBindBufferARB(GL_ELEMENT_ARRAY_BUFFER_ARB,mlElementHandle);

		int lSize = mlElementNum;
		if(mlElementNum<0) lSize = GetIndexNum();

		glDrawElements(mode,lSize,GL_UNSIGNED_INT, (char*) NULL);

		glBindBufferARB(GL_ELEMENT_ARRAY_BUFFER_ARB,0);
	}

	//-----------------------------------------------------------------------

	void cVertexBufferVBO::DrawIndices(unsigned int *apIndices, int alCount,eVertexBufferDrawType aDrawType)
	{
		eVertexBufferDrawType drawType = aDrawType == eVertexBufferDrawType_LastEnum ? mDrawType : aDrawType;

		///////////////////////////////
		//Get the draw type
		GLenum mode = GL_TRIANGLES;
		if(drawType==eVertexBufferDrawType_Quad)		mode = GL_QUADS;
		else if(drawType==eVertexBufferDrawType_Lines)	mode = GL_LINE_STRIP;

		//////////////////////////////////
		//Bind and draw the buffer
		glDrawElements(mode, alCount, GL_UNSIGNED_INT, apIndices);
	}


	//-----------------------------------------------------------------------

	void cVertexBufferVBO::Bind()
	{
		SetVertexStates(mVertexFlags);
	}

	//-----------------------------------------------------------------------

	void cVertexBufferVBO::UnBind()
	{
		glBindBufferARB(GL_ARRAY_BUFFER_ARB,0);
	}

	//-----------------------------------------------------------------------

	float* cVertexBufferVBO::GetArray(tVertexFlag aType)
	{
		int idx = cMath::Log2ToInt((int)aType);

		return &mvVertexArray[idx][0];
	}

	//-----------------------------------------------------------------------

	unsigned int* cVertexBufferVBO::GetIndices()
	{
		return &mvIndexArray[0];
	}

	//-----------------------------------------------------------------------

	void cVertexBufferVBO::ResizeArray(tVertexFlag aType, int alSize)
	{
		int idx = cMath::Log2ToInt((int)aType);

		mvVertexArray[idx].resize(alSize);
	}

	//-----------------------------------------------------------------------

	void cVertexBufferVBO::ResizeIndices(int alSize)
	{
		mvIndexArray.resize(alSize);
	}

	//-----------------------------------------------------------------------

	iVertexBuffer* cVertexBufferVBO::CreateCopy(eVertexBufferUsageType aUsageType)
	{
		cVertexBufferVBO *pVtxBuff = hplNew( cVertexBufferVBO, (mpLowLevelGraphics,
															mVertexFlags,mDrawType,aUsageType,
															GetVertexNum(),GetIndexNum()));

		//Copy the vertices to the new buffer.
        for(int i=0; i < klNumOfVertexFlags; i++)
		{
			if(kvVertexFlags[i] & mVertexFlags)
			{
				int lElements = kvVertexElements[i];
				if(mbTangents && kvVertexFlags[i] == eVertexFlag_Texture1)
					lElements=4;

				pVtxBuff->ResizeArray(kvVertexFlags[i], (int)mvVertexArray[i].size());

				memcpy(pVtxBuff->GetArray(kvVertexFlags[i]),
						&mvVertexArray[i][0], mvVertexArray[i].size() * sizeof(float));
			}
		}

		//Copy indices to the new buffer
        pVtxBuff->ResizeIndices(GetIndexNum());
		memcpy(pVtxBuff->GetIndices(), GetIndices(), GetIndexNum() * sizeof(unsigned int) );

		pVtxBuff->mbTangents = mbTangents;
		pVtxBuff->mbHasShadowDouble = mbHasShadowDouble;

		pVtxBuff->Compile(0);

		return pVtxBuff;
	}

	//-----------------------------------------------------------------------

	int cVertexBufferVBO::GetVertexNum()
	{
		int idx = cMath::Log2ToInt((int)eVertexFlag_Position);
		int lSize = (int)mvVertexArray[idx].size()/kvVertexElements[idx];

		//If there is a shadow double, just return the length of the first half.
		if(mbHasShadowDouble) return lSize / 2;
		else return lSize;
	}
	int cVertexBufferVBO::GetIndexNum()
	{
		return (int)mvIndexArray.size();
	}

	cVector3f cVertexBufferVBO::GetVector3(tVertexFlag aType, unsigned alIdx)
	{
		if(!(aType & mVertexFlags)) return cVector3f(0,0,0);

		int idx = cMath::Log2ToInt((int)aType);
		int pos = alIdx * kvVertexElements[idx];

		return cVector3f(mvVertexArray[idx][pos+0],mvVertexArray[idx][pos+1],
			mvVertexArray[idx][pos+2]);
	}
	cVector3f cVertexBufferVBO::GetVector4(tVertexFlag aType, unsigned alIdx)
	{
		if(!(aType & mVertexFlags)) return cVector3f(0,0,0);

		int idx = cMath::Log2ToInt((int)aType);
		int pos = alIdx * 4;//kvVertexElements[idx];

		return cVector3f(mvVertexArray[idx][pos+0],mvVertexArray[idx][pos+1],
			mvVertexArray[idx][pos+2]);
	}
	cColor cVertexBufferVBO::GetColor(tVertexFlag aType, unsigned alIdx)
	{
		if(!(aType & mVertexFlags)) return cColor();

		int idx = cMath::Log2ToInt((int)aType);
		int pos = alIdx * kvVertexElements[idx];

		return cColor(mvVertexArray[idx][pos+0],mvVertexArray[idx][pos+1],
			mvVertexArray[idx][pos+2],mvVertexArray[idx][pos+3]);
	}
	unsigned int cVertexBufferVBO::GetIndex(tVertexFlag aType, unsigned alIdx)
	{
		return mvIndexArray[alIdx];
	}


	//-----------------------------------------------------------------------

	/////////////////////////////////////////////////////////////////////////
	// PRIVATE METHODS
	/////////////////////////////////////////////////////////////////////////

	//-----------------------------------------------------------------------

	int cVertexBufferVBO::GetElementNum(tVertexFlag aFlag)
	{
		int idx = cMath::Log2ToInt((int)aFlag);

		return kvVertexElements[idx];
	}
	//-----------------------------------------------------------------------

	void cVertexBufferVBO::SetVertexStates(tVertexFlag aFlags)
	{
		/// COLOR 0 /////////////////////////
		if(aFlags & eVertexFlag_Color0)
		{
			glEnableClientState(GL_COLOR_ARRAY );

			int idx = cMath::Log2ToInt(eVertexFlag_Color0);
			glBindBufferARB(GL_ARRAY_BUFFER_ARB,mvArrayHandle[idx]);
			glColorPointer(kvVertexElements[idx],GL_FLOAT, 0, (char*)NULL);
		}
		else
		{
			glDisableClientState(GL_COLOR_ARRAY );
		}

		/// NORMAL /////////////////////////
		if(aFlags & eVertexFlag_Normal)
		{
			glEnableClientState(GL_NORMAL_ARRAY );

			int idx = cMath::Log2ToInt(eVertexFlag_Normal);
			glBindBufferARB(GL_ARRAY_BUFFER_ARB,mvArrayHandle[idx]);
			glNormalPointer(GL_FLOAT, 0, (char*)NULL);
		}
		else
		{
			glDisableClientState(GL_NORMAL_ARRAY );
		}

		/// TEXTURE 0 /////////////////////////
		if(aFlags & eVertexFlag_Texture0)
		{
			glClientActiveTextureARB(GL_TEXTURE0_ARB);
			glEnableClientState(GL_TEXTURE_COORD_ARRAY );

			int idx =  cMath::Log2ToInt(eVertexFlag_Texture0);
			glBindBufferARB(GL_ARRAY_BUFFER_ARB,mvArrayHandle[idx]);
			glTexCoordPointer(kvVertexElements[idx],GL_FLOAT,0,(char*)NULL );
		}
		else {
			glClientActiveTextureARB(GL_TEXTURE0_ARB);
			glDisableClientState(GL_TEXTURE_COORD_ARRAY );
		}

		/// TEXTURE 1 /////////////////////////
		if(aFlags & eVertexFlag_Texture1){
			glClientActiveTextureARB(GL_TEXTURE1_ARB);
			glEnableClientState(GL_TEXTURE_COORD_ARRAY );

			int idx =  cMath::Log2ToInt(eVertexFlag_Texture1);
			glBindBufferARB(GL_ARRAY_BUFFER_ARB,mvArrayHandle[idx]);

			if(mbTangents)
				glTexCoordPointer(4,GL_FLOAT,0,(char*)NULL );
			else
				glTexCoordPointer(kvVertexElements[idx],GL_FLOAT,0,(char*)NULL );
		}
		else {
			glClientActiveTextureARB(GL_TEXTURE1_ARB);
			glDisableClientState(GL_TEXTURE_COORD_ARRAY );
		}

		/// TEXTURE 2 /////////////////////////
		if(aFlags & eVertexFlag_Texture2){
			glClientActiveTextureARB(GL_TEXTURE2_ARB);
			glEnableClientState(GL_TEXTURE_COORD_ARRAY );

			int idx =  cMath::Log2ToInt(eVertexFlag_Texture2);
			glBindBufferARB(GL_ARRAY_BUFFER_ARB,mvArrayHandle[idx]);
			glTexCoordPointer(kvVertexElements[idx],GL_FLOAT,0,(char*)NULL );
		}
		else {
			glClientActiveTextureARB(GL_TEXTURE2_ARB);
			glDisableClientState(GL_TEXTURE_COORD_ARRAY );
		}

		/// TEXTURE 3 /////////////////////////
		if(aFlags & eVertexFlag_Texture3){
			glClientActiveTextureARB(GL_TEXTURE3_ARB);
			glEnableClientState(GL_TEXTURE_COORD_ARRAY );

			int idx =  cMath::Log2ToInt(eVertexFlag_Texture3);
			glBindBufferARB(GL_ARRAY_BUFFER_ARB,mvArrayHandle[idx]);
			glTexCoordPointer(kvVertexElements[idx],GL_FLOAT,0,(char*)NULL );
		}
		else {
			glClientActiveTextureARB(GL_TEXTURE3_ARB);
			glDisableClientState(GL_TEXTURE_COORD_ARRAY );
		}

		/// TEXTURE 4 /////////////////////////
		if(aFlags & eVertexFlag_Texture4){
			glClientActiveTextureARB(GL_TEXTURE4_ARB);
			glEnableClientState(GL_TEXTURE_COORD_ARRAY );

			int idx =  cMath::Log2ToInt(eVertexFlag_Texture4);
			glBindBufferARB(GL_ARRAY_BUFFER_ARB,mvArrayHandle[idx]);
			glTexCoordPointer(kvVertexElements[idx],GL_FLOAT,0,(char*)NULL );
		}
		else {
			glClientActiveTextureARB(GL_TEXTURE4_ARB);
			glDisableClientState(GL_TEXTURE_COORD_ARRAY );
		}

		/// POSITION /////////////////////////
		if(aFlags & eVertexFlag_Position){
			glEnableClientState(GL_VERTEX_ARRAY );

			int idx = cMath::Log2ToInt(eVertexFlag_Position);
			glBindBufferARB(GL_ARRAY_BUFFER_ARB,mvArrayHandle[idx]);
			glVertexPointer(kvVertexElements[idx],GL_FLOAT, 0, (char*)NULL);
		}
		else
		{
			glDisableClientState(GL_VERTEX_ARRAY );
		}

		glBindBufferARB(GL_ARRAY_BUFFER_ARB,0);
	}

	//-----------------------------------------------------------------------

}

//Old code for compiling all to one array.
//Log("Compiling VBO..\n");

/*int lNum = 0;
int lSize = 0;
int lNumSize = 0;
//Deterimine the number of arrays and the size.
Log("calc size: ");
for(int i=0;i< klNumOfVertexFlags; i++)
{
if(mVertexFlags & kvVertexFlags[i]){
lNum++;

//if(lSize != -1 && lSize != mvTempVertexArray[i].size()){
//	return false;
//}

lSize += (int)mvTempVertexArray[i].size();

lNumSize = (int)mvTempVertexArray[i].size()/GetElementNum(kvVertexFlags[i]);

Log(" %d (%d * %d),",lSize,lNumSize, GetElementNum(kvVertexFlags[i]));
}
}
Log("\n");

//Get memory for the main array
mvVertexArray.resize(lSize);

//Copy the temp arrays into the main one
int lPos =0;

Log("Copy: \n");
for(int i=0;i< klNumOfVertexFlags; i++)
{
if(mVertexFlags & kvVertexFlags[i])
{
int lArraySize = (int)mvTempVertexArray[i].size();

Log(" %d (%d) -> %d \n",i,lArraySize,lPos );

memcpy(&mvVertexArray[lPos], &(mvTempVertexArray[i][0]), sizeof(float)*lArraySize);

mvArrayOffset[i] = lPos;

lPos += lArraySize;
mvTempVertexArray[i].clear();
}
}

Log("Array:\n");
for(int i=0;i< klNumOfVertexFlags; i++)
{
if(mVertexFlags & kvVertexFlags[i])
{
int lOffset =  mvArrayOffset[i];
int lElemNum = GetElementNum(kvVertexFlags[i]);

for(int j=0;j<lNumSize;j++)
{
Log("(");

for(int k=0;k<lElemNum;k++)
{
Log(" %.1f,",mvVertexArray[lOffset + j*lElemNum + k]);
}

Log(") ");
}

Log("\n");
}
}*/



