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
using System.Diagnostics;
using System.ComponentModel;
using System.IO;
using System.Collections;
using System.Xml;
using System.Windows.Forms;

namespace HplHelper
{
	public enum eHplMaterialType
	{
		Diffuse,
		DiffuseSpecular,
        Bump,
		BumpSpecular,
		BumpColorSpecular,
		Additive,
		Flat,
		Modulative,
		ModulativeX2,
		Alpha,
		EnvironmentMapReflect,
		Water,
		LastEnum
   	};

	public enum eHplTextureUnit
	{
		Diffuse,
		NMap,
		Illumination,
		Specular,
		CubeMap,
		Refraction,
		LastEnum
	};

	public enum eHplWrapType
	{
		Repeat,
		Clamp,
		ClampToEdge,
	};
	
	public class HplMaterialTypeData
	{
		public ArrayList mvUnitTypes;

		public HplMaterialTypeData()
		{
			mvUnitTypes = new ArrayList();
		}
	}

	public class HplTextureUnit
	{
		public String msFile;
		public bool mbMipMaps;
		public eHplWrapType mWrapMode;
		public String msAnimMode;
		public String msFrameTime;
		public String msType;

		public HplTextureUnit()
		{
			msFile ="";
			mbMipMaps = true;
			mWrapMode = eHplWrapType.Repeat;
			msAnimMode = "None";
			msFrameTime = "1";
			msType = "2D";
		}

		public void ClearData()
		{
			msFile ="";
			mbMipMaps = true;
			mWrapMode = eHplWrapType.Repeat;
			msAnimMode = "None";
			msFrameTime = "1";
			msType = "2D";
		}
	}
	
	public class HplMaterial
	{
		static public String[] mvTypeNames = {
		"Diffuse",
		"DiffuseSpecular",
		"Bump",
		"BumpSpecular",
		"BumpColorSpecular",
		"Additive",
		"Flat",
		"Modulative",
		"ModulativeX2",
		"Alpha",
	    "EnvironmentMapReflect",
		"Water"
		};

		static public String[] mvTextureUnitNames = {
		"Diffuse",
		"NMap",
		"Illumination",
		"Specular",
		"CubeMap",
		"Refraction",
		};

		public eHplMaterialType mType = eHplMaterialType.Diffuse;

		public String msPhysicsMaterial = "Default";

		public ArrayList mvTextureUnits;

		public bool mbUseAlpha=false;
		public bool mbDepthTest=false;

		public String msValue = "1";
		
		public HplMaterial()
		{
			mvTextureUnits = new ArrayList();
			for(int i=0; i< 8; i++)
				mvTextureUnits.Add(new HplTextureUnit());
		}

		public void ClearData()
		{
			mbUseAlpha = false;
			mType = eHplMaterialType.Diffuse;

			mbUseAlpha=false;
			mbDepthTest=true;
			
			msValue = "1";

			for(int i=0; i< 8; i++)
				((HplTextureUnit)mvTextureUnits[i]).ClearData();
		}

		public void Load(String asFile)
		{
			XmlDocument Doc = new XmlDocument();
			Doc.Load(asFile);

			XmlElement DocRoot = (XmlElement)Doc.FirstChild;
						
			//Iterate trough all directories.
			for(int child_count=0;child_count< DocRoot.ChildNodes.Count;child_count++)
			{
				XmlElement ChildNode = (XmlElement)DocRoot.ChildNodes[child_count];
				
                if(ChildNode.Name == "Main")
				{
					String sType = ChildNode.GetAttribute("Type");
					for(int i=0; i< mvTypeNames.Length; i++)
					{
						if(sType == mvTypeNames[i]){
							mType = (eHplMaterialType)i;
							break;
						}
					}

					msPhysicsMaterial = ChildNode.GetAttribute("PhysicsMaterial");

					mbUseAlpha = ChildNode.GetAttribute("UseAlpha")=="True"? true : false;
					mbDepthTest = ChildNode.GetAttribute("DepthTest")=="False"? false : true;

					try{
						msValue = ChildNode.GetAttribute("Value");
					}
					catch{
						msValue = "1";
					}
				}
				else if(ChildNode.Name == "TextureUnits")
				{
                    for(int tex=0; tex < ChildNode.ChildNodes.Count; tex++)
					{
						XmlElement TexNode = (XmlElement)ChildNode.ChildNodes[tex];
						HplTextureUnit TexUnit = (HplTextureUnit)mvTextureUnits[tex];

						//MessageBox.Show("MIp: "+TexNode.GetAttribute("MipMaps"),"Test");

                        TexUnit.msFile = TexNode.GetAttribute("File");
						TexUnit.mbMipMaps = TexNode.GetAttribute("Mipmaps").ToLower()=="true";
						TexUnit.msAnimMode = TexNode.GetAttribute("AnimMode");
						TexUnit.msFrameTime = TexNode.GetAttribute("AnimFrameTime");
						try{
							TexUnit.msType = TexNode.GetAttribute("Type");
						}
						catch{
							TexUnit.msType = "2D";
						}
						
						
						String sWrapMode = TexNode.GetAttribute("Wrap").ToLower();
						if(sWrapMode=="repeat") TexUnit.mWrapMode = eHplWrapType.Repeat;
						else if(sWrapMode=="clamp") TexUnit.mWrapMode = eHplWrapType.Clamp;
						else if(sWrapMode=="clamptoedge") TexUnit.mWrapMode = eHplWrapType.ClampToEdge;
					}
				}
			}
		}

		public void Save(String asFile,frmMain apMainForm)
		{
			XmlDocument Doc = new XmlDocument();
			
			XmlElement DocRoot = Doc.CreateElement("Material");
			Doc.AppendChild(DocRoot);


			XmlElement MainElem = Doc.CreateElement("Main");
			DocRoot.AppendChild(MainElem);

            MainElem.SetAttribute("Type",(String)apMainForm.objMaterialTypes.SelectedItem);
			MainElem.SetAttribute("PhysicsMaterial",msPhysicsMaterial);
			MainElem.SetAttribute("UseAlpha",mbUseAlpha?"True" : "False");
			MainElem.SetAttribute("DepthTest",mbDepthTest?"True" : "False");
			MainElem.SetAttribute("Value",msValue);
			
			XmlElement TexElem = Doc.CreateElement("TextureUnits");
			DocRoot.AppendChild(TexElem);
			
			//Textures
			for(int i=0; i< apMainForm.objTextureUnitTypes.Items.Count; i++)
			{
				HplTextureUnit TexUnit = (HplTextureUnit)mvTextureUnits[i];
				
				String sTexType = (String)apMainForm.objTextureUnitTypes.Items[i];
				XmlElement ChildElem = Doc.CreateElement(sTexType);

				/*if(TexUnit.msFile == ""){
					MessageBox.Show("Could not save! Material has no "+(String)apMainForm.objTextureUnitTypes.Items[i]+" texture file!","Error");
					return;
				}*/
				
				ChildElem.SetAttribute("File", TexUnit.msFile);
				ChildElem.SetAttribute("Compress", "false");
				ChildElem.SetAttribute("Type", "2D");
                ChildElem.SetAttribute("Mipmaps",TexUnit.mbMipMaps? "true" : "false");
				ChildElem.SetAttribute("Wrap",(String)apMainForm.objWrapModes.Items[(int)TexUnit.mWrapMode] );
				ChildElem.SetAttribute("AnimMode", TexUnit.msAnimMode);
				ChildElem.SetAttribute("AnimFrameTime", TexUnit.msFrameTime);
				ChildElem.SetAttribute("Type",TexUnit.msType);
				
				TexElem.AppendChild(ChildElem);
			}
			
			Doc.Save(asFile);
		}
	}
}
