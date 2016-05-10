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
#ifndef HPL_PHYSICS_MATERIAL_NEWTON_H
#define HPL_PHYSICS_MATERIAL_NEWTON_H

#if defined(__linux__) || defined(__APPLE__)
#include <unistd.h>
#endif
#include <Newton.h>

#include "physics/PhysicsMaterial.h"

namespace hpl {

	class iPhysicsBody;
	class cPhysicsContactData;

	class cPhysicsMaterialNewton : public iPhysicsMaterial
	{
	public:
		cPhysicsMaterialNewton(const tString &asName, iPhysicsWorld *apWorld, int alMatId=-1);
		virtual ~cPhysicsMaterialNewton();

		void SetElasticity(float afElasticity);
		float GetElasticity() const;
		void SetStaticFriction(float afElasticity);
		float GetStaticFriction() const;
		void SetKineticFriction(float afElasticity);
		float GetKineticFriction() const;

		void SetFrictionCombMode(ePhysicsMaterialCombMode aMode);
		ePhysicsMaterialCombMode GetFrictionCombMode() const;
		void SetElasticityCombMode(ePhysicsMaterialCombMode aMode);
		ePhysicsMaterialCombMode GetElasticityCombMode() const;

		void UpdateMaterials();

		int GetId(){ return mlMaterialId;}
	private:
		float Combine(ePhysicsMaterialCombMode aMode, float afX, float afY);

		static int BeginContactCallback(const NewtonMaterial* material,
										const NewtonBody* apBody1, const NewtonBody* apBody2, int alThreadIndex);
		static void ProcessContactCallback(const NewtonJoint* contacts, dFloat afTimeStep, int alThreadIndex);

		const NewtonWorld *mpNewtonWorld;

		int mlMaterialId;

		ePhysicsMaterialCombMode mFrictionMode;
		ePhysicsMaterialCombMode mElasticityMode;

		float mfElasticity;
		float mfStaticFriction;
		float mfKineticFriction;

	};
};
#endif // HPL_PHYSICS_MATERIAL_NEWTON_H
