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
using System.IO;
using System.Xml;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;


namespace Mapeditor
{
	/// <summary>
	/// Summary description for cMapIO.
	/// </summary>
	public class cMapIO
	{
		private frmMain mMainForm=null;

		public cMapIO(frmMain aMainForm)
		{
			mMainForm = aMainForm;
		}
		
		////////////////////////////////////////////////////////////////////////
		// SAVE THE MAP
		//
		////////////////////////////////////////////////////////////////////////
		public bool Save(string asFileName)
		{
			XmlDocument Doc = new XmlDocument();
			XmlElement HplMapElem = Doc.CreateElement("HPLMap");
			Doc.AppendChild(HplMapElem);
			
			///////////////////////////
			////// HPL MAP ///////////
			///////////////////////////
			int lWidth = mMainForm.mlMapWidth;
			int lHeight = mMainForm.mlMapHeight;
			int lTileSize = mMainForm.mlDefaultTileSize;
			float fLightZ = mMainForm.mfLightZ;
			Color AmbientCol = mMainForm.mAmbientColor;
			
			HplMapElem.SetAttribute("Name", mMainForm.msMapName);
			HplMapElem.SetAttribute("Width", lWidth.ToString());
			HplMapElem.SetAttribute("Height", lHeight.ToString());
			HplMapElem.SetAttribute("TileSize", lTileSize.ToString());
			HplMapElem.SetAttribute("LightZ", fLightZ.ToString());
			HplMapElem.SetAttribute("AmbColR",AmbientCol.R.ToString());
			HplMapElem.SetAttribute("AmbColG",AmbientCol.G.ToString());
			HplMapElem.SetAttribute("AmbColB",AmbientCol.B.ToString());
			

			///////////////////////////
			////// PROP DATA //////////
			///////////////////////////
			XmlElement PropDataRootElem = Doc.CreateElement("PropData");
			HplMapElem.AppendChild(PropDataRootElem);
			
			foreach(cPropData PData in mMainForm.mPropsForm.mlstPropData)
			{
				XmlElement PropDataElem = Doc.CreateElement("Prop");

				string sFileName = Path.GetFullPath(PData.msFile);
				string sCurrDir = Directory.GetCurrentDirectory();
				sFileName = sFileName.Remove(0, sCurrDir.Length);
				
				PropDataElem.SetAttribute("File",sFileName);
								
				PropDataRootElem.AppendChild(PropDataElem);
			}

			///////////////////////////
			//// (IMAGE) ENTITIES /////
			///////////////////////////
			XmlElement EntityRootElem = Doc.CreateElement("Entities");
			HplMapElem.AppendChild(EntityRootElem);
			
			ArrayList lstEntityList = new ArrayList();
			lstEntityList.Add(mMainForm.mPropsForm.mlstProps);

			foreach(ArrayList List in lstEntityList)
			{
				foreach(cImageEntity Entity in List)
				{
					XmlElement EntityElem = Doc.CreateElement("Entity");

					EntityElem.SetAttribute("Class", Entity.ToString());
					EntityElem.SetAttribute("DataName", Entity.mEntityData.msName);
					EntityElem.SetAttribute("RenderType", Entity.mEntityData.msRenderType);

					EntityElem.SetAttribute("Name",Entity.msName);
					EntityElem.SetAttribute("Active",Entity.mbActive.ToString());
				
					EntityElem.SetAttribute("X",Entity.mlX.ToString());
					EntityElem.SetAttribute("Y",Entity.mlY.ToString());
					EntityElem.SetAttribute("Z",Entity.mfZ.ToString());
					
					EntityElem.SetAttribute("Width",Entity.mfWidth.ToString());
					EntityElem.SetAttribute("Height",Entity.mfHeight.ToString());
					EntityElem.SetAttribute("Angle",Entity.mfAngle.ToString());

					EntityElem.SetAttribute("FlipH",Entity.mbFlipH.ToString());
					EntityElem.SetAttribute("FlipV",Entity.mbFlipV.ToString());

					EntityElem.SetAttribute("AnimNum",Entity.mlAnimNum.ToString());

					/*Do class specific stuff here! */

					EntityRootElem.AppendChild(EntityElem);
				}
			}

			///////////////////////////
			////// SOUND SOURCE////////
			///////////////////////////
			XmlElement SoundRootElem = Doc.CreateElement("SoundSources");
			HplMapElem.AppendChild(SoundRootElem);
			
			foreach(cSoundSource SData in mMainForm.mSoundForm.mlstSounds)
			{
				XmlElement SoundElem = Doc.CreateElement("SoundSource");

				SoundElem.SetAttribute("Name",SData.msName);
				SoundElem.SetAttribute("SoundName",SData.msSoundName);
				SoundElem.SetAttribute("Active",SData.mbActive.ToString());
				SoundElem.SetAttribute("MinDist", SData.mfMinDist.ToString());
				SoundElem.SetAttribute("MaxDist", SData.mfMaxDist.ToString());
				SoundElem.SetAttribute("Random", SData.mlRandom.ToString());
				SoundElem.SetAttribute("Interval",SData.mlInterval.ToString());
				SoundElem.SetAttribute("Loop", SData.mbLoop.ToString());
				SoundElem.SetAttribute("Relative", SData.mbRelative.ToString());
				SoundElem.SetAttribute("RelX",SData.mfRelX.ToString()); 
				SoundElem.SetAttribute("RelY", SData.mfRelY.ToString()); 
				SoundElem.SetAttribute("RelZ", SData.mfRelZ.ToString());
				SoundElem.SetAttribute("Volume", SData.mfVolume.ToString());

				SoundElem.SetAttribute("X",SData.mlX.ToString());
				SoundElem.SetAttribute("Y",SData.mlY.ToString());
				SoundElem.SetAttribute("Z",SData.mfZ.ToString());


				SoundRootElem.AppendChild(SoundElem);
			}
			///////////////////////////
			////// PARTICLE SYSTEM ////
			///////////////////////////
			XmlElement ParticleRootElem = Doc.CreateElement("ParticleSystems");
			HplMapElem.AppendChild(ParticleRootElem);
			
			foreach(cParticle PartData in mMainForm.mParticlesForm.mlstParticles)
			{
				XmlElement PartElem = Doc.CreateElement("ParticleSystem");

				PartElem.SetAttribute("Name",PartData.msName);
				PartElem.SetAttribute("PartName",((cParticleType)PartData.mPForm.mlstTypes[
															PartData.mlTypeNum]).msName);
				
				PartElem.SetAttribute("Active",PartData.mbActive.ToString());
				
				PartElem.SetAttribute("SizeX", PartData.mfSizeX.ToString());
				PartElem.SetAttribute("SizeY", PartData.mfSizeY.ToString());
				PartElem.SetAttribute("SizeZ", PartData.mfSizeZ.ToString());
				
				PartElem.SetAttribute("X",PartData.mlX.ToString());
				PartElem.SetAttribute("Y",PartData.mlY.ToString());
				PartElem.SetAttribute("Z",PartData.mfZ.ToString());

				ParticleRootElem.AppendChild(PartElem);
			}
			///////////////////////////
			////// AREAS //////////////
			///////////////////////////
			XmlElement AreaRootElem = Doc.CreateElement("Areas");
			HplMapElem.AppendChild(AreaRootElem);
			
			foreach(cArea AreaData in mMainForm.mAreasForm.mlstAreas)
			{
				XmlElement AreaElem = Doc.CreateElement("Area");

				AreaElem.SetAttribute("Name",AreaData.msName);
				AreaElem.SetAttribute("AreaType",((cAreaType)AreaData.mAForm.mlstTypes[
					AreaData.mlTypeNum]).msName);
				
				AreaElem.SetAttribute("Active",AreaData.mbActive.ToString());
				
				AreaElem.SetAttribute("Width", AreaData.mfWidth.ToString());
				AreaElem.SetAttribute("Height", AreaData.mfHeight.ToString());
				
				AreaElem.SetAttribute("SizeX", AreaData.mfSizeX.ToString());
				AreaElem.SetAttribute("SizeY", AreaData.mfSizeY.ToString());
				AreaElem.SetAttribute("SizeZ", AreaData.mfSizeZ.ToString());
				
				AreaElem.SetAttribute("X",AreaData.mlX.ToString());
				AreaElem.SetAttribute("Y",AreaData.mlY.ToString());
				AreaElem.SetAttribute("Z",AreaData.mfZ.ToString());

				AreaRootElem.AppendChild(AreaElem);
			}
			
			///////////////////////////
			////// LIGHT //////////////
			///////////////////////////
			XmlElement LightRootElem = Doc.CreateElement("Lights");
			HplMapElem.AppendChild(LightRootElem);
			
			foreach(cLight LData in mMainForm.mLightForm.mlstLights)
			{
				XmlElement LightElem = Doc.CreateElement("Lights");

				LightElem.SetAttribute("Name",LData.msName);
				LightElem.SetAttribute("Active",LData.mbActive.ToString());
				LightElem.SetAttribute("Radius",LData.mfRadius.ToString());
				LightElem.SetAttribute("Specular",LData.mfSpecular.ToString());
				LightElem.SetAttribute("ColR",LData.mColor.R.ToString());
				LightElem.SetAttribute("ColG",LData.mColor.G.ToString());
				LightElem.SetAttribute("ColB",LData.mColor.B.ToString());
				
				LightElem.SetAttribute("X",LData.mlX.ToString());
				LightElem.SetAttribute("Y",LData.mlY.ToString());
				LightElem.SetAttribute("Z",LData.mfZ.ToString());

				LightElem.SetAttribute("CastShadows",LData.mbCastShadows.ToString());
				LightElem.SetAttribute("AffectMaterial",LData.mbAffectMaterial.ToString());
				
				LightRootElem.AppendChild(LightElem);
			}
			
			///////////////////////////
			////// TILE MAP ///////////
			///////////////////////////
			XmlElement TileMapElem = Doc.CreateElement("TileMap");
			HplMapElem.AppendChild(TileMapElem);
			
			TileMapElem.SetAttribute("ShadowLayer",
									mMainForm.mLayersForm.objShadowLayerList.SelectedIndex.ToString());
			TileMapElem.SetAttribute("TileSize", lTileSize.ToString());
			TileMapElem.SetAttribute("Width", lWidth.ToString());
			TileMapElem.SetAttribute("Height", lHeight.ToString());
			
            
			////////////////////////////
			////// TILE SETS ///////////
			////////////////////////////
			XmlElement TileSetRootElem = Doc.CreateElement("TileSets");
			TileMapElem.AppendChild(TileSetRootElem);

			frmTileSets TileSetsForm = mMainForm.mTilesetsForm;
			foreach(cTileSet TSData in TileSetsForm.mlstTileSets)
			{
				//Set TileSet properties
				XmlElement TileSetElem = Doc.CreateElement("TileSet");
				TileSetElem.SetAttribute("Name", TSData.msName);
				
				//Get the local path! Fix this later on perhaps?
				string sFileName = Path.GetFullPath(TSData.msTileSetFile);
				string sCurrDir = Directory.GetCurrentDirectory();
				sFileName = sFileName.Remove(0, sCurrDir.Length);
				TileSetElem.SetAttribute("FileName", sFileName);
				
				TileSetRootElem.AppendChild(TileSetElem);
			}

			////////////////////////////
			////// TILE LAYERS /////////
			////////////////////////////
			XmlElement LayerRootElem = Doc.CreateElement("Layers");
			TileMapElem.AppendChild(LayerRootElem);

			frmLayers LayerForm = mMainForm.mLayersForm;
			foreach(cTileLayer TLayer in LayerForm.mlstTileLayers)
			{
				//Set TileSet properties
				XmlElement LayerElem = Doc.CreateElement("TileSet");
				LayerElem.SetAttribute("Name", TLayer.msName.ToString());
				LayerElem.SetAttribute("Width", TLayer.mlWidth.ToString());
				LayerElem.SetAttribute("Height", TLayer.mlHeight.ToString());
				LayerElem.SetAttribute("TileSize", TLayer.mlTileSize.ToString());
				LayerElem.SetAttribute("Z", TLayer.mfZ.ToString());
				LayerElem.SetAttribute("Collide", TLayer.mbCollide.ToString());
				LayerElem.SetAttribute("Lit", TLayer.mbLit.ToString());
				
				for(int y=0;y<TLayer.mlHeight;y++)
				{
					XmlElement TilesElem = Doc.CreateElement("Tiles");
					string sData="";
					for(int x=0;x<TLayer.mlWidth;x++)
					{
						cTile TData = TLayer.mvTiles[x + y*TLayer.mlWidth];
							if(TData.mlSet <0)
							{
								sData+="|";
							}
							else
							{
								sData+=TData.mlSet.ToString();sData+=":";
								sData+=TData.mlNum.ToString();
								if(TData.mlRotation==0 && TData.mbBreakable==false){
									sData+="|";
								}
								else {
									sData+=":";
									sData+=TData.mlRotation.ToString();
									
									if(TData.mbBreakable==false){
										sData+="|";
									}
									else {
										sData+=":";
										sData+="1"; sData+="|";
									}
								}
							}
					}
					TilesElem.SetAttribute("Data",sData);
					
					LayerElem.AppendChild(TilesElem);
				}

				LayerRootElem.AppendChild(LayerElem);
			}


			///////////////////////
			////// SAVE ///////////
			///////////////////////
			Doc.Save(asFileName);
	
			//MessageBox.Show("Saved!");
			
			return true;
		}
		
		////////////////////////////////////////////////////////////////////////
		// LOAD THE MAP
		//
		////////////////////////////////////////////////////////////////////////
		
		public bool Load(string asFileName)
		{
			XmlDocument Doc = new XmlDocument();
			try{
				Doc.Load(asFileName);
			}
			catch{
				return false;
			}
			XmlElement HplMapElem = (XmlElement)Doc.FirstChild;
			///////////////////////////
			////// HPL MAP ///////////
			///////////////////////////
			int lR,lG,lB;
			mMainForm.msMapName = cHplXml.GetStr(HplMapElem,"Name","");
			mMainForm.mlMapWidth = cHplXml.GetInt(HplMapElem,"Width",0);
			mMainForm.mlMapHeight = cHplXml.GetInt(HplMapElem,"Height",0);
			mMainForm.mfLightZ = cHplXml.GetFloat(HplMapElem,"LightZ",10);
			lR = cHplXml.GetInt(HplMapElem,"AmbColR",0);
			lG = cHplXml.GetInt(HplMapElem,"AmbColG",0);
			lB = cHplXml.GetInt(HplMapElem,"AmbColB",0);
			mMainForm.mAmbientColor = Color.FromArgb(lR,lG,lB);
				
			foreach(XmlElement HplMapChild in HplMapElem.ChildNodes)
			{
				///////////////////////////
				////// LIGHT //////////////
				///////////////////////////
				if(HplMapChild.Name == "Lights")
				{
					XmlElement LightRootElem = HplMapChild;
					
					foreach(XmlElement LightElem in LightRootElem.ChildNodes)
					{
						string sName = LightElem.GetAttribute("Name");
						bool bActive = cHplXml.GetBool(LightElem,"Active",true);
						float fRadius = cHplXml.GetFloat(LightElem,"Radius",0);
						float fSpecular = cHplXml.GetFloat(LightElem,"Specular",1);
						
						lR = cHplXml.GetInt(LightElem,"ColR",0);
						lG = cHplXml.GetInt(LightElem,"ColG",0);
						lB =cHplXml.GetInt(LightElem,"ColB",0);
						Color Col = Color.FromArgb(lR,lG,lB);

						int lX = cHplXml.GetInt(LightElem,"X",0);
						int lY = cHplXml.GetInt(LightElem,"Y",0);
						float fZ = cHplXml.GetFloat(LightElem,"Z",0);

						cLight LData = new cLight(lX,lY,fZ);
						LData.mColor = Col;
						LData.msName =sName;
						LData.mfRadius = fRadius;
						LData.mfSpecular = fSpecular;
						LData.mbActive = bActive;

						LData.mbCastShadows = cHplXml.GetBool(LightElem,"CastShadows",true);
						LData.mbAffectMaterial = cHplXml.GetBool(LightElem,"AffectMaterial",true);
				

						mMainForm.mLightForm.mlstLights.Add(LData);
					}
				}
				///////////////////////////
				////// SOUNDSOURCE/////////
				///////////////////////////
				if(HplMapChild.Name == "SoundSources")
				{
					XmlElement SoundRootElem = HplMapChild;
					
					foreach(XmlElement SoundElem in SoundRootElem.ChildNodes)
					{
						string sName = SoundElem.GetAttribute("Name");
						int lX = cHplXml.GetInt(SoundElem,"X",0);
						int lY = cHplXml.GetInt(SoundElem,"Y",0);
						float fZ = cHplXml.GetFloat(SoundElem,"Z",0);

						cSoundSource SData = new cSoundSource(lX,lY,fZ);
						SData.msName = sName;
						
						SData.mbActive = cHplXml.GetBool(SoundElem,"Active",true);

						SData.msSoundName = cHplXml.GetStr(SoundElem,"SoundName","");
						
						SData.mfMinDist = cHplXml.GetFloat(SoundElem,"MinDist",1);
						SData.mfMaxDist = cHplXml.GetFloat(SoundElem,"MaxDist",100);
						SData.mlRandom = cHplXml.GetInt(SoundElem,"Random", 0);
						SData. mlInterval = cHplXml.GetInt(SoundElem,"Interval",0);
						SData.mbLoop = cHplXml.GetBool(SoundElem,"Loop", true);
						SData.mbRelative = cHplXml.GetBool(SoundElem,"Relative", false);
						SData.mfRelX = cHplXml.GetFloat(SoundElem,"RelX",0); 
						SData.mfRelY = cHplXml.GetFloat(SoundElem,"RelY", 0); 
						SData.mfRelZ = cHplXml.GetFloat(SoundElem,"RelZ", 0);
						SData.mfVolume = cHplXml.GetFloat(SoundElem,"Volume", 1);


						mMainForm.mSoundForm.mlstSounds.Add(SData);
					}
				}
				///////////////////////////
				////// PARTICLE SYSTEM ////
				///////////////////////////
				if(HplMapChild.Name == "ParticleSystems")
				{
					XmlElement PartRootElem = HplMapChild;
					
					foreach(XmlElement PartElem in PartRootElem.ChildNodes)
					{
						string sName = PartElem.GetAttribute("Name");
						int lX = cHplXml.GetInt(PartElem,"X",0);
						int lY = cHplXml.GetInt(PartElem,"Y",0);
						float fZ = cHplXml.GetFloat(PartElem,"Z",0);

						cParticle PartData = new cParticle(mMainForm.mParticlesForm,lX,lY,fZ);
						PartData.msName = sName;
						
						PartData.mbActive = cHplXml.GetBool(PartElem,"Active",true);

						string sPartName = cHplXml.GetStr(PartElem,"PartName","");
						PartData.mlTypeNum = 0;
						int lCount=0;
						foreach(cParticleType PType in mMainForm.mParticlesForm.mlstTypes){
							if(sPartName == PType.msName){
								PartData.mlTypeNum = lCount;
								break;
							}
							lCount++;
						}
						
						PartData.mfSizeX = cHplXml.GetFloat(PartElem,"SizeX",0);
						PartData.mfSizeY = cHplXml.GetFloat(PartElem,"SizeY",0);
						PartData.mfSizeZ = cHplXml.GetFloat(PartElem,"SizeZ",0);
						
						mMainForm.mParticlesForm.mlstParticles.Add(PartData);
					}
				}
				///////////////////////////
				////// AREAS //////////////
				///////////////////////////
				if(HplMapChild.Name == "Areas")
				{
					XmlElement AreaRootElem = HplMapChild;
					
					foreach(XmlElement AreaElem in AreaRootElem.ChildNodes)
					{
						string sName = AreaElem.GetAttribute("Name");
						int lX = cHplXml.GetInt(AreaElem,"X",0);
						int lY = cHplXml.GetInt(AreaElem,"Y",0);
						float fZ = cHplXml.GetFloat(AreaElem,"Z",0);

						cArea AreaData = new cArea(mMainForm.mAreasForm,lX,lY,fZ);
						AreaData.msName = sName;
						
						AreaData.mbActive = cHplXml.GetBool(AreaElem,"Active",true);

						string sAreaName = cHplXml.GetStr(AreaElem,"AreaType","");
						AreaData.mlTypeNum = 0;
						int lCount=0;
						foreach(cAreaType AType in mMainForm.mAreasForm.mlstTypes)
						{
							if(sAreaName == AType.msName)
							{
								AreaData.mlTypeNum = lCount;
								break;
							}
							lCount++;
						}

						AreaData.mfWidth = cHplXml.GetFloat(AreaElem,"Width",0);
						AreaData.mfHeight = cHplXml.GetFloat(AreaElem,"Height",0);						
			
						AreaData.mfSizeX = cHplXml.GetFloat(AreaElem,"SizeX",0);
						AreaData.mfSizeY = cHplXml.GetFloat(AreaElem,"SizeY",0);
						AreaData.mfSizeZ = cHplXml.GetFloat(AreaElem,"SizeZ",0);
						
						mMainForm.mAreasForm.mlstAreas.Add(AreaData);
					}
				}
				///////////////////////////
				////// PROP DATA///////////
				///////////////////////////
				else if(HplMapChild.Name == "PropData")
				{
					foreach(XmlElement PDataElem in HplMapChild.ChildNodes)
					{
						string sFile = Directory.GetCurrentDirectory() + cHplXml.GetStr(PDataElem,"File","");
						cPropData PropData= new cPropData(sFile);

						PropData.LoadData();

						mMainForm.mPropsForm.AddProp(PropData);
					}
				}
				///////////////////////////
				////// ENTITIES ///////////
				///////////////////////////
				else if(HplMapChild.Name == "Entities")
				{
					foreach(XmlElement EntityElem in HplMapChild.ChildNodes)
					{
						string sClass = EntityElem.GetAttribute("Class");
						cImageEntity Entity=null;
						
						string sName = EntityElem.GetAttribute("Name");
						string sDataName = EntityElem.GetAttribute("DataName");
						bool bActive = cHplXml.GetBool(EntityElem,"Active",true);
												
						int lX = cHplXml.GetInt(EntityElem,"X",0);
						int lY = cHplXml.GetInt(EntityElem,"Y",0);
						float fZ = cHplXml.GetFloat(EntityElem,"Z",0);

						float fWidth = cHplXml.GetFloat(EntityElem,"Width",0);
						float fHeight = cHplXml.GetFloat(EntityElem,"Height",0);
						float fAngle = cHplXml.GetFloat(EntityElem,"Angle",0);

						bool bFlipH = cHplXml.GetBool(EntityElem,"FlipH",false);
						bool bFlipV = cHplXml.GetBool(EntityElem,"FlipV",false);

						int lAnimNum = cHplXml.GetInt(EntityElem,"AnimNum",0);
		
						if(sClass=="Mapeditor.cProp") {
							cPropData PData = mMainForm.mPropsForm.GetData(sDataName);
							Entity = new cProp(PData,lX,lY,fZ);
						}
						
						Entity.msName = sName;
						Entity.mbActive = bActive;

						Entity.mfWidth = fWidth;
						Entity.mfHeight = fHeight;
						Entity.mfAngle = fAngle;

						Entity.mbFlipH = bFlipH;
						Entity.mbFlipV = bFlipV;
						
						Entity.mlAnimNum = lAnimNum;
						
						if(sClass=="Mapeditor.cProp") mMainForm.mPropsForm.mlstProps.Add(Entity);
					}
				}
				///////////////////////////
				////// TILE MAP ///////////
				///////////////////////////
				else if(HplMapChild.Name == "TileMap")
				{
					XmlElement TileMapElem = HplMapChild;
					
					int lShadowLayerIndex = Convert.ToInt32(TileMapElem.GetAttribute("ShadowLayer"));

					mMainForm.mlDefaultTileSize = Convert.ToInt32(TileMapElem.GetAttribute("TileSize"));
					
					foreach(XmlElement TileMapChild in TileMapElem.ChildNodes)
					{
					
						////////////////////////////
						////// TILE SETS ///////////
						////////////////////////////
						if(TileMapChild.Name == "TileSets")
						{
							XmlElement TileSetRootElem = TileMapChild;
									
							frmTileSets TileSetsForm = mMainForm.mTilesetsForm;
							foreach(XmlElement TSElem in TileSetRootElem.ChildNodes)
							{
								string sPath = Directory.GetCurrentDirectory() + TSElem.GetAttribute("FileName");
								cTileSet TSData = new cTileSet(sPath);
								//MessageBox.Show(sPath);
								if(TSData.LoadData()==false)
								{
									MessageBox.Show("Couldn't load tileset '"+TSElem.GetAttribute("FileName")+"'!",
										"Error");
									return false;
								}
								
								TileSetsForm.AddTileSet(TSData);
							}
						}
							////////////////////////////
							////// TILE LAYERS /////////
							////////////////////////////
						else if(TileMapChild.Name == "Layers")
						{
							XmlElement LayerRootElem = TileMapChild;
							
							frmLayers LayerForm = mMainForm.mLayersForm;
							foreach(XmlElement TLayerElem in LayerRootElem.ChildNodes)
							{
								string sName = TLayerElem.GetAttribute("Name");
								int lWidth = Convert.ToInt32(TLayerElem.GetAttribute("Width"));						
								int lHeight = Convert.ToInt32(TLayerElem.GetAttribute("Height"));
								float fZ = (float)Convert.ToDouble(TLayerElem.GetAttribute("Z"));
								bool bCollide = Convert.ToBoolean(TLayerElem.GetAttribute("Collide"));
								bool bLit = Convert.ToBoolean(TLayerElem.GetAttribute("Lit"));
								int lTileSize = Convert.ToInt32(TLayerElem.GetAttribute("TileSize"));

								cTileLayer TLayer = new cTileLayer(mMainForm.mTilesetsForm,sName,lWidth,lHeight,
									fZ,bCollide, bLit,lTileSize);

														
								int lTileCount=0;
								foreach(XmlElement TRowElem in TLayerElem.ChildNodes)
								{
									string sData = TRowElem.GetAttribute("Data");
									int lDataCount =0;
									//MessageBox.Show(sData);
									while(lDataCount<sData.Length)
									{
										cTile T = TLayer.mvTiles[lTileCount];
										lDataCount = LoadTileData(T, sData, lDataCount);
										//MessageBox.Show("T:"+T.mlSet+" "+T.mlNum+" "+T.mlRotation+" ");
										lTileCount++;
									}
								}

								LayerForm.AddLayer(TLayer,-1);
							}
						}
					}
					//Set the shadow layer.
					mMainForm.mLayersForm.objShadowLayerList.SelectedIndex = lShadowLayerIndex;
				}
			}

			///////////////////////
			////// UPDATE ///////////
			///////////////////////
			
			//MessageBox.Show("Loaded!");
			return true;
		}
	
		private int LoadTileData(cTile aT,string asData, int alStart)
		{
			int lCount = alStart;
			int lStart = lCount;
			int lValType =0;

			while(true)
			{
				if(asData[lCount] == ':' || asData[lCount]=='|')
				{
					if(lStart != lCount)
					{
						string sVal = asData.Substring(lStart, lCount - lStart);
						int lVal = Convert.ToInt32(sVal);
					
						switch(lValType)
						{
							case 0: aT.mlSet = lVal;break;
							case 1: aT.mlNum = lVal;break;
							case 2: aT.mlRotation = lVal;break;
							case 3: aT.mbBreakable = lVal==1;break;
						}
						lValType++;
					}

					if(asData[lCount]=='|')break;
					lStart = lCount+1;
				}

				lCount++;
			}

			return lCount+1;
		}
	}
}
