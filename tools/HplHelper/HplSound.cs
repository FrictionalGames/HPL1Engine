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
	/// <summary>
	/// Summary description for HplSound.
	/// </summary>
	public class HplSound
	{
		public String msMainSound;
		public String msStartSound;
		public String msStopSound;
		
		public float mfVolume;
		public float mfMinDistance;
		public float mfMaxDistance;

		public float mfRandom;
		public float mfInterval;

		public bool mbStream;
		public bool mbLoop;
		public bool mbUse3D;

        public bool mbFadeStart;
		public bool mbFadeEnd;

		public bool mbBlockable;
		public float mfBlockVolMul;

		public int mlPriority;

		public HplSound()
		{
			
		}

		//---------------------------------------
		
		public void ClearData(frmMain apMainForm)
		{
			msMainSound = "";
			msStartSound = "";
			msStopSound = "";
		
			mfVolume = 1;
			mfMinDistance= 1;
			mfMaxDistance = 10;

			mfRandom = 1;
			mfInterval =0;

			mbFadeStart=false;
			mbFadeEnd =false;

			mbStream =false;
			mbUse3D = true;
			mbLoop = true;

			mbBlockable = false;
			mfBlockVolMul = 0.6f;

			mlPriority =0;

			apMainForm.txtSoundFile.Text = "";

			UpdateToForm(apMainForm);
		}

		//---------------------------------------

		public void UpdateToForm(frmMain apMainForm)
		{
			apMainForm.txtMainSoundFile.Text = msMainSound;
			apMainForm.txtStartSoundFile.Text = msStartSound;
			apMainForm.txtStopSoundFile.Text = msStopSound;

			apMainForm.txtSoundVolume.Text = mfVolume.ToString();
			apMainForm.txtSoundMinDist.Text = mfMinDistance.ToString();
			apMainForm.txtSoundMaxDist.Text = mfMaxDistance.ToString();

			apMainForm.txtSoundRandom.Text = mfRandom.ToString();
			apMainForm.txtSoundInterval.Text = mfInterval.ToString();

			apMainForm.objSoundFadeEnd.SelectedIndex = mbFadeEnd ? 0 : 1;
			apMainForm.objSoundFadeStart.SelectedIndex = mbFadeStart ? 0 : 1;

			apMainForm.objSoundStream.SelectedIndex = mbStream ? 0 : 1;
			apMainForm.objSoundLoop.SelectedIndex = mbLoop ? 0 : 1;
			apMainForm.objSoundUse3D.SelectedIndex = mbUse3D ? 0 : 1;

			apMainForm.objSoundBlockable.SelectedIndex = mbBlockable ? 0 : 1;
			apMainForm.txtSoundBlockVolMul.Text = mfBlockVolMul.ToString();

			apMainForm.txtSoundPriority.Text = mlPriority.ToString();
		}

		//---------------------------------------

		public void UpdateFromForm(frmMain apMainForm)
		{
			msMainSound = apMainForm.txtMainSoundFile.Text;
			msStartSound = apMainForm.txtStartSoundFile.Text;
			msStopSound = apMainForm.txtStopSoundFile.Text;
			
			try{
				mfVolume = (float)Convert.ToDouble(apMainForm.txtSoundVolume.Text);
			}
			catch{
				mfVolume =0;
			}
			
			try	{
				mfMinDistance = (float)Convert.ToDouble(apMainForm.txtSoundMinDist.Text);
			}
			catch {
				mfMinDistance =1;
			}
			
			try
			{
				mfMaxDistance = (float)Convert.ToDouble(apMainForm.txtSoundMaxDist.Text);
			}
			catch{
				mfMaxDistance = 10;
			}
				
			try{
				mfRandom = (float)Convert.ToDouble(apMainForm.txtSoundRandom.Text);
			}
			catch{
				mfRandom =1;
			}
			
			try{
				mfInterval = (float)Convert.ToDouble(apMainForm.txtSoundInterval.Text);
			}
			catch{
				mfInterval =0;
			}
			

			mbFadeEnd = apMainForm.objSoundFadeEnd.SelectedIndex==0?true:false;
			mbFadeStart = apMainForm.objSoundFadeStart.SelectedIndex==0?true:false;

			mbStream = apMainForm.objSoundStream.SelectedIndex==0?true:false;
			mbLoop = apMainForm.objSoundLoop.SelectedIndex==0?true:false;
			mbUse3D = apMainForm.objSoundUse3D.SelectedIndex==0?true:false;

			mbBlockable = apMainForm.objSoundBlockable.SelectedIndex==0?true:false;
			try
			{
				mfBlockVolMul = (float)Convert.ToDouble(apMainForm.txtSoundBlockVolMul.Text);
			}
			catch
			{
				mfBlockVolMul =0.6f;
			}

			try
			{
				mlPriority = Convert.ToInt32(apMainForm.txtSoundPriority.Text);
			}
			catch
			{
				mlPriority =0;
			}
		}

		//---------------------------------------

		public void Load(String asFile,frmMain apMainForm)
		{
			XmlDocument Doc = new XmlDocument();
			Doc.Load(asFile);

			XmlElement DocRoot = (XmlElement)Doc.FirstChild;
						
			//Iterate trough all child elemens
			for(int child_count=0;child_count< DocRoot.ChildNodes.Count;child_count++)
			{
				XmlElement ChildNode = (XmlElement)DocRoot.ChildNodes[child_count];
				
				if(ChildNode.Name == "MAIN")
				{
					msMainSound = ChildNode.GetAttribute("MainSound");
					msStartSound = ChildNode.GetAttribute("StartSound");
					msStopSound = ChildNode.GetAttribute("StopSound");
				}
				else if(ChildNode.Name == "PROPERTIES")
				{
					mfVolume = (float)Convert.ToDouble(ChildNode.GetAttribute("Volume"));
					mfMinDistance = (float)Convert.ToDouble(ChildNode.GetAttribute("MinDistance"));
					mfMaxDistance = (float)Convert.ToDouble(ChildNode.GetAttribute("MaxDistance"));

					mfRandom = (float)Convert.ToDouble(ChildNode.GetAttribute("Random"));
					mfInterval = (float)Convert.ToDouble(ChildNode.GetAttribute("Interval"));

					mbLoop = ChildNode.GetAttribute("Loop")=="True" ?true:false;
					mbUse3D = ChildNode.GetAttribute("Use3D")=="True" ?true:false;
					mbStream = ChildNode.GetAttribute("Stream")=="True" ?true:false;
					
					mbBlockable = ChildNode.GetAttribute("Blockable")=="True" ?true:false;
					
					try{
						mfBlockVolMul = (float)Convert.ToDouble(ChildNode.GetAttribute("BlockVolumeMul"));
					}
					catch{
						mfBlockVolMul = 0.6f;
					}

					try
					{
						mlPriority = Convert.ToInt32(ChildNode.GetAttribute("Priority"));
					}
					catch
					{
						mlPriority = 0;
					}
					

					mbFadeStart = ChildNode.GetAttribute("FadeStart")=="True" ?true:false;
					mbFadeEnd = ChildNode.GetAttribute("FadeEnd")=="True" ?true:false;
				}
			}

			UpdateToForm(apMainForm);
		}

		//---------------------------------------

		public void Save(String asFile,frmMain apMainForm)
		{
			XmlDocument Doc = new XmlDocument();
			
			XmlElement DocRoot = Doc.CreateElement("SOUNDENTITY");
			Doc.AppendChild(DocRoot);

			XmlElement MainElem = Doc.CreateElement("MAIN");
			DocRoot.AppendChild(MainElem);

			MainElem.SetAttribute("MainSound",msMainSound);
			MainElem.SetAttribute("StartSound",msStartSound);
			MainElem.SetAttribute("StopSound",msStopSound);

			XmlElement PropElem = Doc.CreateElement("PROPERTIES");
			DocRoot.AppendChild(PropElem);

			
			PropElem.SetAttribute("Volume",mfVolume.ToString());
			PropElem.SetAttribute("MinDistance",mfMinDistance.ToString());
			PropElem.SetAttribute("MaxDistance",mfMaxDistance.ToString());

			PropElem.SetAttribute("Random",mfRandom.ToString());
			PropElem.SetAttribute("Interval",mfInterval.ToString());

			PropElem.SetAttribute("FadeEnd",mbFadeEnd?"True":"False");
			PropElem.SetAttribute("FadeStart",mbFadeStart?"True":"False");

			PropElem.SetAttribute("Stream",mbStream?"True":"False");
			PropElem.SetAttribute("Loop",mbLoop?"True":"False");
			PropElem.SetAttribute("Use3D",mbUse3D?"True":"False");
			
			PropElem.SetAttribute("Blockable",mbBlockable?"True":"False");
			PropElem.SetAttribute("BlockVolumeMul",mfBlockVolMul.ToString());
			
			PropElem.SetAttribute("Priority",mlPriority.ToString());
									
			Doc.Save(asFile);
		}

		//---------------------------------------
	}
}
