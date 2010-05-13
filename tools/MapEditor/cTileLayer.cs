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
using System.Collections;
using System.Windows.Forms;

namespace Mapeditor
{
	/// <summary>
	/// Summary description for cTileLayer.
	/// </summary>
	public class cTileLayer
	{
		public string msName;
		public int mlWidth=0;
		public int mlHeight=0;
		public float mfZ;
		public bool mbCollide=false;
		public bool mbLit=false;
		public int mlTileSize=0;
		
		private frmTileSets mTileSetForm=null;
		
		public cTile[] mvTiles;

		public cTileLayer(frmTileSets aTileSetForm,string asName,int alWidth, int alHeight, 
						float afZ, bool abCollide, bool abLit, int alTileSize)
		{
			msName = asName;
			mlWidth = alWidth;
			mlHeight = alHeight;
			mfZ = afZ;
			mbCollide = abCollide;
			mbLit = abLit;
			mTileSetForm = aTileSetForm;
			mlTileSize = alTileSize;

			mvTiles = new cTile[mlWidth*mlHeight];
			for(int i=0;i<mvTiles.Length;i++){
				mvTiles[i] = new cTile();
			}
		}

		public void SetTile(int alX, int alY,int alSet, int alNum, int alRotation)
		{
			alX /= mlTileSize;
			alY /= mlTileSize;

			if(alX<0 || alX >= mlWidth || alY<0 || alY >= mlHeight)return;
			
			mvTiles[alX + alY*mlWidth].mlSet = alSet;
			mvTiles[alX + alY*mlWidth].mlNum = alNum;
			mvTiles[alX + alY*mlWidth].mlRotation = alRotation;
		}

		public void ToggleTileBreak(int alX, int alY)
		{
			alX /= mlTileSize;
			alY /= mlTileSize;

			if(alX<0 || alX >= mlWidth || alY<0 || alY >= mlHeight)return;

			if(mvTiles[alX + alY*mlWidth].mlSet<0)return;
			
			mvTiles[alX + alY*mlWidth].mbBreakable = mvTiles[alX + alY*mlWidth].mbBreakable?false:true;
		}

		public cTile GetTile(int alX, int alY)
		{
			alX /= mlTileSize;
			alY /= mlTileSize;

			if(alX<0 || alX >= mlWidth || alY<0 || alY >= mlHeight)return null;
			
			return mvTiles[alX + alY*mlWidth];
		}

		public void Draw(Graphics aGfx, int alX, int alY, int alW, int alH,bool abGrid)
		{
			if(mTileSetForm.objSetList.Items.Count<1)return;


			alW = alW/mlTileSize+2;
			alH = alH/mlTileSize+2;

			int lTileX = alX/mlTileSize;
			int lTileY = alY/mlTileSize;
			
			if(lTileX + alW > mlWidth)alW = mlWidth- lTileX;
			if(lTileY + alH > mlHeight)alH = mlHeight- lTileY;

			for(int x=0; x<alW; x++)		
			for(int y=0; y<alH; y++)
			{
				cTile T = mvTiles[(lTileY+y)*mlWidth+(lTileX+x)];
				int lSet = T.mlSet;
				int lNum = T.mlNum;
				
				if(lSet<0 || lNum<0)continue;

				if(lSet >= mTileSetForm.mlstTileSets.Count)continue;
				cTileSet TSetData = (cTileSet) mTileSetForm.mlstTileSets[lSet];
				
				if(lNum >= TSetData.mlstTileData.Count)continue;
				cTileData TData = (cTileData) TSetData.mlstTileData[lNum];
								
				TData.Draw(aGfx,(lTileX+x)*mlTileSize - alX, (lTileY+y)*mlTileSize - alY,
							abGrid,false,T.mlRotation);

				if(T.mbBreakable){
					Font BreakFont = new Font("Arial",10);
					SolidBrush BreakPen = new SolidBrush(Color.White);
					
					aGfx.DrawString("B",BreakFont,BreakPen,(mlTileSize/2-5)+ (lTileX+x)*mlTileSize - alX,
									(mlTileSize/2-5) + (lTileY+y)*mlTileSize - alY);

					BreakFont.Dispose();
					BreakPen.Dispose();
				}
				
				/*aGfx.DrawRectangle(new Pen(Color.White),x*mlTileSize,y*mlTileSize,
									mlTileSize, mlTileSize);*/
			}
		}

		public void DrawMini(Graphics aGfx, int alX, int alY, int alW, int alH)
		{
			if(mTileSetForm.objSetList.Items.Count<1)return;

			for(int x=alX; x<alW; x++)		
				for(int y=alY; y<alH; y++)
				{
					if(x<0 || x>=mlWidth || y<0 || y>=mlHeight)continue;

					cTile T = mvTiles[x + y*mlWidth];
					int lSet = T.mlSet;
					int lNum = T.mlNum;
				
					if(lSet<0 || lNum<0)continue;

					if(lSet >= mTileSetForm.mlstTileSets.Count)continue;
					cTileSet TSetData = (cTileSet) mTileSetForm.mlstTileSets[lSet];
				
					if(lNum >= TSetData.mlstTileData.Count)continue;
					cTileData TData = (cTileData) TSetData.mlstTileData[lNum];

					TData.DrawMini(aGfx,(x-alX)*2,(y-alY)*2,T.mlRotation);
				}
		}




		public void Resize(int alNewWidth, int alNewHeight)
		{
			cTile[] vNewTiles = new cTile[alNewWidth*alNewHeight];
			for(int i=0;i<vNewTiles.Length;i++)	{
				vNewTiles[i] = new cTile();
			}
			
			int lW = alNewWidth<mlWidth? alNewWidth:mlWidth;
			int lH = alNewHeight<mlHeight? alNewHeight:mlHeight;

			for(int y=0;y<lH;y++)
			for(int x=0;x<lW;x++)
			{
				vNewTiles[x +y*alNewWidth] = mvTiles[x +y*mlWidth]; 
			}

			mvTiles = vNewTiles;
			
			mlWidth = alNewWidth;
			mlHeight =alNewHeight;
		}
	}
}
