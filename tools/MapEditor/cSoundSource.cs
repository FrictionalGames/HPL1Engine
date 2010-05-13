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
	/// Summary description for cSoundSource.
	/// </summary>
	public class cSoundSource: cEntity
	{
		private float mfCircleSize=15;

		public string msSoundName="";
		public float mfMinDist=1;
		public float mfMaxDist=1000;
		public int mlRandom = 0;
		public int mlInterval = 0;
		public bool mbLoop = true;
		public bool mbRelative = false;
		public float mfRelX=0, mfRelY=0, mfRelZ=0;
		public float mfVolume=1;

		
		public cSoundSource(int alX,int alY, float afZ) : base(alX,alY,afZ)
		{
		
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
			
			if(mMode == eEditMode.SoundSource)
			{
				Col = abSelected?Color.FromArgb(80,255,80):Color.FromArgb(255,0,255);
				
				SolidBrush CenterBrush = new SolidBrush(Col);
				aGfx.FillEllipse(CenterBrush,mlX-10-alWorldX,mlY-10-alWorldY,
					20,20);
				
				string sText = msSoundName==""? "-None-" : msSoundName;
				
				Font tfont = new Font("Arial",8);
				
				aGfx.DrawString(sText,tfont,CenterBrush,mlX-mfCircleSize-alWorldX,
								mlY+mfCircleSize-alWorldY);
				
				tfont.Dispose();

				CenterBrush.Dispose();
			}
			else
			{
				Col = Color.Gray;
				SolidBrush CenterBrush = new SolidBrush(Col);
				aGfx.FillEllipse(CenterBrush,mlX-10-alWorldX,mlY-10-alWorldY,
					20,20);
				CenterBrush.Dispose();
			}

			Pen OuterPen = new Pen(Col);
			aGfx.DrawEllipse(OuterPen,mlX-mfCircleSize-alWorldX,mlY-mfCircleSize-alWorldY,
				mfCircleSize*2,mfCircleSize*2);
			OuterPen.Dispose();
		}

		public override void EditProperties()
		{
			frmPropertiesSound EditSound = new frmPropertiesSound(this);
			
			EditSound.ShowDialog();

			if(EditSound.mbOkWasPressed)
			{
				msName = EditSound.objNameText.Text;
				mbActive = EditSound.objActiveBox.SelectedIndex==1?true:false;

				mbLoop = EditSound.objLoopBox.SelectedIndex==1?true:false;
				mbRelative = EditSound.objRelativeBox.SelectedIndex==1?true:false;
			
				msSoundName = EditSound.objSoundText.Text;
				mfMinDist = (float)Convert.ToDouble(EditSound.objMinDistText.Text);
				mfMaxDist = (float)Convert.ToDouble(EditSound.objMaxDistText.Text);
				mlRandom = Convert.ToInt32(EditSound.objRandomText.Text);
				mlInterval = Convert.ToInt32(EditSound.objIntervalText.Text);
				mfRelX = (float)Convert.ToDouble(EditSound.objXText.Text);
				mfRelY = (float)Convert.ToDouble(EditSound.objYText.Text);
				mfRelZ = (float)Convert.ToDouble(EditSound.objZText.Text);
				mfVolume = (float)Convert.ToDouble(EditSound.objVolumeText.Text);
			}
			
			EditSound.Dispose();
		}
	
	}
}
