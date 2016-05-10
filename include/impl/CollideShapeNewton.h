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
#ifndef HPL_COLLIDE_SHAPE_NEWTON_H
#define HPL_COLLIDE_SHAPE_NEWTON_H

#if defined(__linux__) || defined(__APPLE__)
#include <unistd.h>
#endif
#include <Newton.h>
#include "physics/CollideShape.h"

#include "math/MathTypes.h"

namespace hpl {

	class iVertexBuffer;
	class iCollideShape;

	typedef std::vector<iCollideShape*> tCollideShapeVec;
	typedef tCollideShapeVec::iterator tCollideShapeVecIt;

	class cCollideShapeNewton : public iCollideShape
	{
	public:
		cCollideShapeNewton(eCollideShapeType aType, const cVector3f &avSize,
							cMatrixf* apOffsetMtx, const NewtonWorld* apNewtonWorld,
							iPhysicsWorld *apWorld);
		virtual ~cCollideShapeNewton();

		iCollideShape* GetSubShape(int alIdx);
		int GetSubShapeNum();

		cVector3f GetInertia(float afMass);

		void CreateFromShapeVec(tCollideShapeVec &avShapes);
		void CreateFromVertices(const unsigned int* apIndexArray, int alIndexNum,
								const float *apVertexArray, int alVtxStride, int alVtxNum);

		const NewtonCollision* GetNewtonCollision(){ return mpNewtonCollision;}

	private:
		const NewtonCollision* mpNewtonCollision;
		const NewtonWorld *mpNewtonWorld;

		tCollideShapeVec mvSubShapes;
	};
};
#endif // HPL_COLLIDE_SHAPE_NEWTON_H
