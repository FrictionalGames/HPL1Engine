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
	/// Summary description for Area.
	/// </summary>
	public class cArea : cEntity
	{
		public int mlTypeNum=-1;
	
		public float mfWidth=50;
		public float mfHeight=50;
		
		public float mfSizeX=0;
		public float mfSizeY=0;
		public float mfSizeZ=0;

		public frmAreas mAForm;

		public cArea(frmAreas aAForm,int alX,int alY, float afZ) : base(alX,alY,100)
		{
			mAForm = aAForm;

			mlTypeNum = mAForm.objTypeList.SelectedIndex;
		}

		public override Rectangle GetDrawRect()
		{
			mDrawRect.X = mlX-(int)mfWidth/2;
			mDrawRect.Y = mlY-(int)mfHeight/2;
			mDrawRect.Width =  (int)mfWidth;
			mDrawRect.Height =  (int)mfHeight;

			return mDrawRect;
		}

		public override Rectangle GetCollideRect()
		{
			mCollideRect.X = mlX-(int)mfWidth/2;
			mCollideRect.Y = mlY-(int)mfHeight/2;
			mCollideRect.Width =  (int)mfWidth;
			mCollideRect.Height =  (int)mfHeight;

			return mCollideRect;
		}

		public override void Draw(Graphics aGfx,int alWorldX,int alWorldY ,eEditMode mMode, bool abSelected)
		{
			Color Col;
			
			if(mMode == eEditMode.Area)
			{
				Col = abSelected?Color.FromArgb(80,255,80):Color.FromArgb(255,0,255);
				
				SolidBrush CenterBrush = new SolidBrush(Col);
				aGfx.FillEllipse(CenterBrush,mlX-5-alWorldX,mlY-5-alWorldY,
					10,10);

				string sText = ((cAreaType)mAForm.mlstTypes[mlTypeNum]).msName;
				
				Font tfont = new Font("Arial",8);
				
				aGfx.DrawString(sText,tfont,CenterBrush,mlX-alWorldX-(5*(sText.Length/2)),
					mlY-alWorldY+5);
				
				tfont.Dispose();
				
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
			aGfx.DrawRectangle(OuterPen,mlX-mfWidth/2-alWorldX,mlY-mfHeight/2-alWorldY,
				mfWidth,mfHeight);
			OuterPen.Dispose();
		}

		public override void EditProperties()
		{
			frmPropertiesArea EditArea = new frmPropertiesArea(this);
			
			EditArea.ShowDialog();

			if(EditArea.mbOkWasPressed)
			{
				msName = EditArea.objNameText.Text;
				mbActive = EditArea.objActiveBox.SelectedIndex==1?true:false;

				mfWidth = (float)Convert.ToDouble(EditArea.objWidthText.Text);				
				mfHeight = (float)Convert.ToDouble(EditArea.objHeightText.Text);				
				
				mfSizeX = (float)Convert.ToDouble(EditArea.objXText.Text);
				mfSizeY = (float)Convert.ToDouble(EditArea.objYText.Text);
				mfSizeZ = (float)Convert.ToDouble(EditArea.objZText.Text);
							
				mlTypeNum = EditArea.objTypeBox.SelectedIndex;
			}
			
			EditArea.Dispose();	
		}
	}
}
