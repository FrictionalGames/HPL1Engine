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
	/// Summary description for cLight.
	/// </summary>
	public class cLight : cEntity
	{
		public float mfRadius = 150.0f;
		public Color mColor = Color.White;

		public float mfSpecular;

		public bool mbCastShadows=true;
		public bool mbAffectMaterial=true;
		
		public bool mbFlashing =false;
		/*Flashing properties*/
		
		public bool mbPulsating=false;
		/*Pulsating properties*/

		public cLight(int alX,int alY, float afZ) : base(alX,alY,afZ)
		{
		}

		public override Rectangle GetDrawRect()
		{
			mDrawRect.X = mlX-(int)mfRadius;
			mDrawRect.Y = mlY-(int)mfRadius;
            mDrawRect.Width =  (int)mfRadius*2;
			mDrawRect.Height =  (int)mfRadius*2;

			return mDrawRect;
		}

		public override Rectangle GetCollideRect()
		{
			mCollideRect.X = mlX-10;
			mCollideRect.Y = mlY-10;
			mCollideRect.Width = 20;
			mCollideRect.Height = 20;
			
			return mCollideRect;
		}

		public override void Draw(Graphics aGfx,int alWorldX,int alWorldY ,eEditMode mMode, bool abSelected)
		{
			Color Col;
			
			if(mMode == eEditMode.Lights)
			{
				Col = abSelected?Color.FromArgb(80,255,80):Color.FromArgb(255,0,255);
				
				SolidBrush CenterBrush = new SolidBrush(Col);
				aGfx.FillEllipse(CenterBrush,mlX-10-alWorldX,mlY-10-alWorldY,
					20,20);
				CenterBrush.Dispose();
			}
			else
			{
				Col = Color.Gray;
				SolidBrush CenterBrush = new SolidBrush(Col);
				aGfx.FillEllipse(CenterBrush,mlX-3-alWorldX,mlY-3-alWorldY,
					6,6);
				CenterBrush.Dispose();
			}


            Pen OuterPen = new Pen(Col);
			aGfx.DrawEllipse(OuterPen,mlX-mfRadius-alWorldX,mlY-mfRadius-alWorldY,
							mfRadius*2,mfRadius*2);
			OuterPen.Dispose();
		}

		public override void EditProperties()
		{
			frmPropertiesLight EditLight = new frmPropertiesLight(this);
			
			EditLight.ShowDialog();

			if(EditLight.mbOkWasPressed)
			{
				msName = EditLight.objNameText.Text;
				mbActive = EditLight.objActiveBox.SelectedIndex==1?true:false;
				mfSpecular = (float)Convert.ToDouble(EditLight.objSpecularText.Text);
				mfRadius = (float)Convert.ToDouble(EditLight.objRadiusText.Text);
				mColor = EditLight.objColorPanel.BackColor;
				mfZ = (float)Convert.ToDouble(EditLight.objZText.Text);
				mbAffectMaterial = EditLight.objMaterialBox.SelectedIndex==1?true:false;
				mbCastShadows = EditLight.objShadowBox.SelectedIndex==1?true:false;
			}
			
			EditLight.Dispose();
		}
	}
}
