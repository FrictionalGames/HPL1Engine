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
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace Mapeditor
{
	/// <summary>
	/// Summary description for cImageEntity.
	/// </summary>
	public class cImageEntity : cEntity
	{
		public float mfHeight;
		public float mfWidth;
		public float mfAngle=0;
		
		public bool mbFlipH=false;
		public bool mbFlipV=false;

		public int mlAnimNum=0;
		
		public cImageEntityData mEntityData=null;
	
		public cImageEntity(cImageEntityData aData,int alX,int alY, float afZ) : base(alX,alY,afZ)
		{
			mEntityData = aData;
			
			if(mEntityData.mfStretchH<0 || mEntityData.mfStretchW<0)
			{
				mfHeight = mEntityData.mImage.Height;
				mfWidth = mEntityData.mImage.Width;
			}
			else
			{
				mfHeight = mEntityData.mfStretchH;
				mfWidth = mEntityData.mfStretchW;
			}
		}

		public override Rectangle GetDrawRect()
		{
			mDrawRect.X = mlX-(int)mfWidth/2;
			mDrawRect.Y = mlY-(int)mfHeight/2;
			mDrawRect.Width = (int) mfWidth;
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
			Image Pic;
			Matrix Mtx = new Matrix();
			
			PointF vRotPoint = new PointF((float)mlX- alWorldX, (float)mlY- alWorldY);
			
			float fAngle = mfAngle;
			if(mbFlipH) fAngle = 360 - fAngle;
			if(mbFlipV) fAngle = 360 - fAngle;
			
			Mtx.RotateAt(fAngle,vRotPoint, MatrixOrder.Append);

			PointF[] vPoints = new PointF[4];

			aGfx.Transform = Mtx;
			
			if(mbFlipH) mEntityData.mImage.RotateFlip(RotateFlipType.RotateNoneFlipX);
			if(mbFlipV) mEntityData.mImage.RotateFlip(RotateFlipType.RotateNoneFlipY);

			if(mEntityData.mlFrames<0){
				Pic = mEntityData.mImage;
			}
			else
			{
				cEntityAnimation Anim = (cEntityAnimation)mEntityData.mlstAnimations[mlAnimNum];
				int lFrame = (int)Anim.mlstAnimData[0];
				Pic = mEntityData.mvFrameImages[lFrame];
			}
			
			
			/*ImageAttributes Attrib = new ImageAttributes();
			Attrib.SetColorKey(Color.FromArgb(0,Color.FromArgb(0,0,0)),
								Color.FromArgb(0,Color.FromArgb(1,1,1)));*/

						
			aGfx.DrawImage(Pic,mlX - alWorldX - (int)mfWidth/2,
								mlY - alWorldY - (int)mfHeight/2,
								(int)mfWidth, (int)mfHeight);
							
			if(mbFlipH) mEntityData.mImage.RotateFlip(RotateFlipType.RotateNoneFlipX);
			if(mbFlipV) mEntityData.mImage.RotateFlip(RotateFlipType.RotateNoneFlipY);
			
			if(mMode == eEditMode.Props)
			{
				Pen RectPen;
				if(abSelected)RectPen = new Pen(Color.FromArgb(150,255,150));
				else RectPen = new Pen(Color.FromArgb(255,0,255));
				
				aGfx.DrawRectangle(RectPen,mlX - alWorldX - (int)mfWidth/2,
									mlY - alWorldY - (int)mfHeight/2,
									(int)mfWidth,(int)mfHeight);
			}

			Mtx.Reset();
			aGfx.Transform = Mtx;
		}

		public override void EditProperties()
		{
			/*frmPropertiesLight EditLight = new frmPropertiesLight(this);
			
			EditLight.ShowDialog();

			if(EditLight.mbOkWasPressed)
			{
				msName = EditLight.objNameText.Text;
				mbActive = EditLight.objActiveBox.SelectedIndex==1?true:false;
				mfRadius = (float)Convert.ToDouble(EditLight.objRadiusText.Text);
				mColor = EditLight.objColorPanel.BackColor;
				mfZ = (float)Convert.ToDouble(EditLight.objZText.Text);
				mbAffectMaterial = EditLight.objMaterialBox.SelectedIndex==1?true:false;
				mbCastShadows = EditLight.objShadowBox.SelectedIndex==1?true:false;
			}
			
			EditLight.Dispose();*/
		}
	}
}
