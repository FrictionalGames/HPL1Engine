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
using System.Windows.Forms;

namespace Mapeditor
{
	/// <summary>
	/// Summary description for Particle.
	/// </summary>
	public class cParticle : cEntity
	{
		public float mfCircleSize = 15;

		public int mlTypeNum=-1;
		public float mfSizeX=0;
		public float mfSizeY=0;
		public float mfSizeZ=0;

		public frmParticles mPForm;
		
		public cParticle(frmParticles aPForm,int alX,int alY, float afZ) : base(alX,alY,afZ)
		{
			mPForm = aPForm;
			
			mlTypeNum = aPForm.objTypeList.SelectedIndex;
		}

		public override Rectangle GetDrawRect()
		{
			mDrawRect.X = mlX-(int)mfCircleSize;
			mDrawRect.Y = mlY-(int)mfCircleSize;
			mDrawRect.Width =  (int)mfCircleSize*2;
			mDrawRect.Height =  (int)mfCircleSize*2;

			return mDrawRect;
		}

		public override Rectangle GetCollideRect()
		{
			mCollideRect.X = mlX-(int)mfCircleSize;
			mCollideRect.Y = mlY-(int)mfCircleSize;
			mCollideRect.Width =  (int)mfCircleSize*2;
			mCollideRect.Height =  (int)mfCircleSize*2;
			
			return mCollideRect;
		}

		public override void Draw(Graphics aGfx,int alWorldX,int alWorldY ,eEditMode mMode, bool abSelected)
		{
			Color Col;
			
			if(mMode == eEditMode.Particles)
			{
				Col = abSelected?Color.FromArgb(80,255,80):Color.FromArgb(255,0,255);
				
				SolidBrush CenterBrush = new SolidBrush(Col);
				aGfx.FillEllipse(CenterBrush,mlX-5-alWorldX,mlY-5-alWorldY,
					10,10);
				
				/*string sText = msSoundName==""? "-None-" : msSoundName;
				
				Font tfont = new Font("Arial",8);
				
				aGfx.DrawString(sText,tfont,CenterBrush,mlX-mfCircleSize-alWorldX,
					mlY+mfCircleSize-alWorldY);
				
				tfont.Dispose();*/

				CenterBrush.Dispose();
			}
			else
			{
				Col = Color.Gray;
				SolidBrush CenterBrush = new SolidBrush(Col);
				aGfx.FillEllipse(CenterBrush,mlX-5-alWorldX,mlY-5-alWorldY,
					10,10);
				CenterBrush.Dispose();
			}

			Pen OuterPen = new Pen(Col);
			aGfx.DrawEllipse(OuterPen,mlX-mfCircleSize-alWorldX,mlY-mfCircleSize-alWorldY,
				mfCircleSize*2,mfCircleSize*2);
			OuterPen.Dispose();
		}

		public override void EditProperties()
		{
			frmPropertiesParticle EditParticle = new frmPropertiesParticle(this);
			
			EditParticle.ShowDialog();

			if(EditParticle.mbOkWasPressed)
			{
				msName = EditParticle.objNameText.Text;
				mbActive = EditParticle.objActiveBox.SelectedIndex==1?true:false;

				mfSizeX = (float)Convert.ToDouble(EditParticle.objXText.Text);
				mfSizeY = (float)Convert.ToDouble(EditParticle.objYText.Text);
				mfSizeZ = (float)Convert.ToDouble(EditParticle.objZText.Text);

				mfZ = (float)Convert.ToDouble(EditParticle.objPosZText.Text);
							
				mlTypeNum = EditParticle.objTypeBox.SelectedIndex;
			}
			
			EditParticle.Dispose();
		}
	}
}
