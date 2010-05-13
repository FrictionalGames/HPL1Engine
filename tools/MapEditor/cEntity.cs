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
using System;
using System.Drawing;

namespace Mapeditor
{
	/// <summary>
	/// Summary description for cEntity.
	/// </summary>
	public class cEntity
	{
		public int mlX=0, mlY=0;
		public float mfZ=0;
		public Rectangle mDrawRect;
		public Rectangle mCollideRect;
		public String msName;
		public bool mbActive=true;

		public cEntity(int alX,int alY, float afZ)
		{
			Random Rand = new Random();
			mlX = alX;
			mlY = alY;
			mfZ = afZ;

			mDrawRect = new Rectangle();
			mDrawRect.X = alX;
			mDrawRect.Y = alY;
			
			mCollideRect = new Rectangle();
			mCollideRect.X = alX;
			mCollideRect.Y = alY;

			msName = "";
		}

		public virtual Rectangle GetDrawRect()
		{
			return mDrawRect;
		}

		public virtual Rectangle GetCollideRect()
		{
			return mCollideRect;
		}

		public virtual void Draw(Graphics aGfx,int alWorldX,int alWorldY ,eEditMode mMode, bool abSelected)
		{

		}

		public virtual void EditProperties()
		{

		}
	}
}
