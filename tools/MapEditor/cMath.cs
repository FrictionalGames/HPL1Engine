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
	/// Summary description for cMath.
	/// </summary>
	public class cMath
	{
		static public bool BoxCollision(Rectangle aRect1, Rectangle aRect2)
		{
			return (aRect1.X>aRect2.X+(aRect2.Width-1) || aRect2.X>aRect1.X+(aRect1.Width-1) ||
				aRect1.Y>aRect2.Y+(aRect2.Height-1) || aRect2.Y>aRect1.Y+(aRect1.Height-1))==false;
		}
		
		static public bool BoxCollision(RectangleF aRect1, RectangleF aRect2)
		{
			return (aRect1.X>aRect2.X+(aRect2.Width) || aRect2.X>aRect1.X+(aRect1.Width) ||
				aRect1.Y>aRect2.Y+(aRect2.Height) || aRect2.Y>aRect1.Y+(aRect1.Height))==false;
		}
	
		static public bool PointBoxCollision(int alX, int alY, Rectangle aRect)
		{
			if(alX<aRect.X || alX>aRect.X+aRect.Width || alY<aRect.Y || alY>aRect.Y+aRect.Height)
				return false;
			else
				return true;
		}
	}
}	
