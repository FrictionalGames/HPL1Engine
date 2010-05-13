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
using System.Drawing.Drawing2D;

namespace Mapeditor
{
	/// <summary>
	/// Summary description for cTileData.
	/// </summary>
	public class cTileData
	{
		static private string[] mvImageFormats = {".jpg", ".jpeg",".bmp",".png",".pcx"};
		
		public Image mTileImage=null;
		public string msImageFile="";
		public string msName="";
		public string msMeshType="";
		public string msMaterialType="";
				
		public cTileData(string asName, string asImageFile,string asMaterialType, string asMeshType)
		{
			msName = asName;
			msImageFile = asImageFile;
			msMaterialType = asMaterialType;
			msMeshType = asMeshType;
		}

		public bool LoadData()
		{
			bool bLoadOK = true;
			for(int i=0;i<mvImageFormats.Length;i++)
			{
				try	{
					mTileImage = Image.FromFile(msImageFile+mvImageFormats[i]);
				}
				catch{
					//MessageBox.Show("Couldn't load '"+msImageFile+mvImageFormats[i]+"'!","ERROR!");
					bLoadOK = false;
				}
				
				if(bLoadOK){
					//MessageBox.Show("Loaded '"+msImageFile+mvImageFormats[i]+"'!","ERROR!");
					return true;
				}

				bLoadOK = true;
			}
			MessageBox.Show("Couldn't load '"+msImageFile+"'!","ERROR!");
			return false;
		}

		public void Draw(Graphics aGfx, int lX, int lY,bool abBorder,bool abHighLight, int alRotation)
		{
			Matrix Mtx = new Matrix();
			
			float fX = (float)lX;
			float fY = (float)lY;
			
			//Stupid bad fix.
			if(alRotation==1)fY+=0.95f;
			if(alRotation==3)fX+=0.95f;
			PointF vRotPoint = new PointF((float)(lX+mTileImage.Width/2),(float)(lY+mTileImage.Height/2));
			
			Mtx.RotateAt(alRotation*90,vRotPoint, MatrixOrder.Append);
			aGfx.Transform = Mtx;
			
			aGfx.DrawImage(mTileImage,fX,fY,mTileImage.Width,mTileImage.Height);
			
			Mtx.Reset();
			aGfx.Transform = Mtx;
						
			if(abBorder)
			{
				Pen BorderPen; 
				if(abHighLight)BorderPen= new Pen(Color.FromArgb(100,255,100));
				else  BorderPen= new Pen(Color.FromArgb(255,0,255));
				
				if(abHighLight)
					aGfx.DrawRectangle(BorderPen,lX,lY,mTileImage.Width-1, mTileImage.Height-1);
				else
					aGfx.DrawRectangle(BorderPen,lX,lY,mTileImage.Width, mTileImage.Height);

				BorderPen.Dispose();
			}
		}

		public void DrawMini(Graphics aGfx, int lX, int lY,int alRotation)
		{
			aGfx.DrawImage(mTileImage,lX,lY,2,2);
		}
	}
}
