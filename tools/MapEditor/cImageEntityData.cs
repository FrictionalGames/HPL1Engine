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
using System.Xml;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;

namespace Mapeditor
{
	/// <summary>
	/// Summary description for ImageEntity.
	/// </summary>
	public class cImageEntityData
	{
		static private string[] mvImageFormats = {".jpg", ".jpeg",".bmp",".png",".pcx"};
		
		public string msFile="";
		
		public Image mImage=null;
		public string msImageFile="";
		public string msMeshType="";
		public string msMaterialType="";
		
		public string msRenderType="";
		public string msType="";
		public string msSubType="";

		public float mfStretchW=-1;
		public float mfStretchH=-1;
		
		public bool mbLit;
		public bool mbCollidable;
		public bool mbCastShadows;

		public ArrayList mlstAnimations;
		public int mlFrames=0;
		public Image[] mvFrameImages;

		public string msName;

		public cImageEntityData(string asFile)
		{
			msFile = asFile;

			mlstAnimations = new ArrayList();
		}

		public Image LoadImage(string asFile)
		{
			bool bLoadOK;
			Image Pic=null;
			for(int i=0;i<mvImageFormats.Length;i++)
			{
				bLoadOK = true;
				try	
				{
					Pic = Image.FromFile(asFile+mvImageFormats[i]);
				}
				catch
				{
					bLoadOK = false;
				}
				
				if(bLoadOK)
				{
					return Pic;
				}
			}
			MessageBox.Show("Couldn't load '"+asFile+"'!","ERROR!");
			return null;
		}

		public cEntityAnimation GetAnim(string asName)
		{
			foreach(cEntityAnimation Anim in mlstAnimations)
			{
				if(asName == Anim.msName){
					return Anim;
				}
			}

			return null;
		}

		public bool LoadData()
		{
			XmlDocument Doc = new XmlDocument();
			try
			{
				Doc.Load(msFile);
			}
			catch
			{
				MessageBox.Show("Coldn't load file: '"+msFile+"'!");
				return false;
			}

			XmlElement RootElem = (XmlElement)Doc.FirstChild;

			mlFrames = -1;
			
			foreach(XmlElement ChildElem in RootElem.ChildNodes)
			{
				/// MAIN /////
				if(ChildElem.Name == "MAIN")
				{
					msName = cHplXml.GetStr(ChildElem,"Name","");
					msRenderType = cHplXml.GetStr(ChildElem,"RenderType","Image");
					msType = cHplXml.GetStr(ChildElem,"Type","");
					msSubType = cHplXml.GetStr(ChildElem,"Subtype","");
				}
					/// IMAGE /////
				else if(ChildElem.Name == "IMAGE")
				{
					string sDir = cHplXml.GetStr(ChildElem,"Dir","");
					string sImageName = cHplXml.GetStr(ChildElem,"Name","");
					if(sDir.EndsWith("\\")==false && sDir.EndsWith("/")==false)
						sDir+="/";
					msImageFile = sDir + sImageName;

					msMaterialType =  cHplXml.GetStr(ChildElem,"Material","");
					msMeshType =  cHplXml.GetStr(ChildElem,"Mesh","");

					mfStretchW =  cHplXml.GetFloat(ChildElem,"StretchWidth",-1);
					mfStretchH =  cHplXml.GetFloat(ChildElem,"StretchHeight",-1);
				}
					/// PROPERTIES /////
				else if(ChildElem.Name == "PROPERTIES")
				{
					mbCastShadows =  cHplXml.GetBool(ChildElem,"CastShadows",false);
					mbLit =  cHplXml.GetBool(ChildElem,"CastShadows",true);
					mbCollidable =  cHplXml.GetBool(ChildElem,"Collidable",false);
				}
					/// ANIMATIONS /////
				else if(ChildElem.Name == "ANIMATIONS")
				{
					mlFrames = cHplXml.GetInt(ChildElem,"Frames",1);
					
					foreach(XmlElement AnimElem in ChildElem.ChildNodes)
					{
						string sName = cHplXml.GetStr(AnimElem,"Name","");
						float fSpeed = cHplXml.GetFloat(AnimElem,"Speed",1);
						
						ArrayList lstAnim;
						lstAnim = new ArrayList();
						cHplXml.GetIntList(AnimElem,"Data",lstAnim);

						cEntityAnimation Animation;
						Animation = new cEntityAnimation(sName, fSpeed);
						Animation.mlstAnimData = lstAnim;

						mlstAnimations.Add(Animation);
					}
				}
			}
		
			if(mlFrames<0)
			{
				mImage = LoadImage(msImageFile);
				if(mImage==null)return false;
			}
			else
			{
				mvFrameImages = new Image[mlFrames];

				for(int i=0;i<mlFrames;i++)
				{
					int lNum = i+1;
					string sEnd = lNum<10? "0"+lNum.ToString() : lNum.ToString();
					string sFile = msImageFile+sEnd;

					mvFrameImages[i] = LoadImage(sFile);
					if(mvFrameImages[i]==null) return false;
				}
				mImage = mvFrameImages[0];
			}
			return true;
		}
	}
}
