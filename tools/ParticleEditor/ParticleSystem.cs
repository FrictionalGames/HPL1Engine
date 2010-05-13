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
using System.Collections;
using System.Xml;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using System.ComponentModel;

namespace ParticleEditor
{
	public class HplSystem
	{
		const int ERROR_FILE_NOT_FOUND =2;
		const int ERROR_ACCESS_DENIED = 5;
		
		public HplSystem()
		{
		}

		static public String MakePathRelativeToCurrent(String asPath)
		{
			String sCurrent = Directory.GetCurrentDirectory();
				
			//MessageBox.Show("Path:"+sPath+" Curr:"+sCurrent,"Test");

			int lPos =0;
			for(int i=0; i< asPath.Length; i++)
			{
				if(i>= sCurrent.Length || i>= asPath.Length || asPath[i] != sCurrent[i])
				{
					lPos =i;
					break;
				}
			}

			return asPath.Substring(lPos,asPath.Length - lPos);
		}

		static public bool RunProgam(String asFile, String asArgs)
		{
			Process myProcess = new Process();
            
			try
			{
				myProcess.StartInfo.FileName = asFile; 
				myProcess.StartInfo.Arguments = asArgs;
				myProcess.Start();
			}
			catch (Win32Exception e)
			{
				if(e.NativeErrorCode == ERROR_FILE_NOT_FOUND)
				{
					return false;
				} 

				else if (e.NativeErrorCode == ERROR_ACCESS_DENIED)
				{
					return false;
				}
			}

			return true;
		}
	}
	
	//----------------------------------------------------

	public class Vector3
	{
		public Vector3()
		{
		}

		public Vector3(String aX,String aY,String aZ)
		{
			x = aX;
			y = aY;
			z = aZ;
		}

		public String GetText()
		{
			return x+" "+y+" "+z;
		}

		public void FromText(String asText)
		{
			int lMaxVals = 3;
			char[] vSepp = new char[1];
			vSepp[0] = ' ';

			String[] vVals = asText.Split(vSepp,lMaxVals);

			if(vVals.Length < lMaxVals) return;
			
			x = vVals[0];
			y = vVals[1];
			z = vVals[2];
		}

		public String x="0";
		public String y="0";
		public String z="0";
	}

	//-------------------------------------------------

	public class Vector2
	{
		public Vector2()
		{
		}

		public Vector2(String aX,String aY)
		{
			x = aX;
			y = aY;
		}

		public String GetText()
		{
			return x+" "+y;
		}

		public void FromText(String asText)
		{
			int lMaxVals = 2;
			char[] vSepp = new char[1];
			vSepp[0] = ' ';

			String[] vVals = asText.Split(vSepp,lMaxVals);

			if(vVals.Length < lMaxVals) return;
			
			x = vVals[0];
			y = vVals[1];
		}

		public String x="0";
		public String y="0";
	}

	public class Color4
	{
		public Color4()
		{
		}

		public Color4(String aR,String aG,String aB, String aA)
		{
			r = aR;
			g = aG;
			b = aB;
			a = aA;
		}

		public String GetText()
		{
			return r+" "+g+" "+b+" "+a;
		}

		public void FromText(String asText)
		{
			int lMaxVals = 4;
			char[] vSepp = new char[1];
			vSepp[0] = ' ';

			String[] vVals = asText.Split(vSepp,lMaxVals);

			if(vVals.Length < lMaxVals) return;
			
			r = vVals[0];
			g = vVals[1];
			b = vVals[2];
			a = vVals[3];
			
			/*int lLastStart=0;
			int lVal =0;
			
			for(int i=0; i< asText.Length; ++i)
			{
				if(asText[i]==' ')
				{
					vVals[lVal] = asText.Substring(lLastStart, (i - lLastStart) +1);

					lLastStart = i+1;
				}
			}*/
		}

		public String r="0";
		public String g="0";
		public String b="0";
		public String a="0";
	}

	//-------------------------------------------------	
	
	public class ParticleEmitter
	{
		public ParticleEmitter(Form1 aMainForm)
		{
			mMainForm = aMainForm;
		}

		//---------------------------------

		public void CopyToGui()
		{
			//////////////////////////////
			///////// GENERAL ////////////
			mMainForm.txtName.Text = msName;

			// NEW
 			mMainForm.objPEType.SelectedItem = mPEType;
			// ---

			mMainForm.txtMaxParticles.Text = mlMaxParticleNum;
			
			mMainForm.objRespawn.Checked = (mbRespawn =="True");
			
			mMainForm.txtParticlesPerSec.Text = mfParticlesPerSecond;
			mMainForm.txtStartTimeOffset.Text = mfStartTimeOffset;

			mMainForm.txtWarmUpTime.Text = mfWarmUpTime;
			mMainForm.txtWarmUpStepsPerSec.Text = mfWarmUpStepsPerSec;
			
			mMainForm.txtMinPauseLength.Text = mfMinPauseLength;
			mMainForm.txtMaxPauseLength.Text = mfMaxPauseLength;
			mMainForm.txtMinPauseInterval.Text = mfMinPauseInterval;
			mMainForm.txtMaxPauseInterval.Text = mfMaxPauseInterval;
			
			mMainForm.txtPosOffsetX.Text = mvPosOffset.x;
			mMainForm.txtPosOffsetY.Text = mvPosOffset.y;
			mMainForm.txtPosOffsetZ.Text = mvPosOffset.z;
			
			mMainForm.txtAngleOffsetX.Text = mvAngleOffset.x;
			mMainForm.txtAngleOffsetY.Text = mvAngleOffset.y;
			mMainForm.txtAngleOffsetZ.Text = mvAngleOffset.z;

			//////////////////////////////
			///////// MATERIAL ////////////
			mMainForm.txtMaterial.Text = msMaterial;
			mMainForm.txtMaterialNum.Text = mlMaterialNum;
			mMainForm.txtAnimationLength.Text = mfAnimationLength;

			mMainForm.txtSubDivX.Text = mvSubDiv.x;
			mMainForm.txtSubDivY.Text = mvSubDiv.y;

			mMainForm.objSubDivType.SelectedItem = mSubDivType;
			
			//////////////////////////////
			///////// START ////////////
			mMainForm.objStartPosType.SelectedItem = mStartPosType;
			
			mMainForm.txtMinStartPosX.Text = mvMinStartPos.x;
			mMainForm.txtMinStartPosY.Text = mvMinStartPos.y;
			mMainForm.txtMinStartPosZ.Text = mvMinStartPos.z;
			
			mMainForm.txtMaxStartPosX.Text = mvMaxStartPos.x;
			mMainForm.txtMaxStartPosY.Text = mvMaxStartPos.y;
			mMainForm.txtMaxStartPosZ.Text = mvMaxStartPos.z;
						
			mMainForm.txtMinStartRadius.Text = mfMinStartRadius;
			mMainForm.txtMaxStartRadius.Text = mfMaxStartRadius;

			mMainForm.txtMinStartAnglesX.Text = mvMinStartAngles.x;
			mMainForm.txtMinStartAnglesY.Text = mvMinStartAngles.y;
			mMainForm.txtMaxStartAnglesX.Text = mvMaxStartAngles.x;
			mMainForm.txtMaxStartAnglesY.Text = mvMaxStartAngles.y;

			
			//////////////////////////////
			///////// MOVEMENT ////////////
			mMainForm.objStartVelType.SelectedItem = mStartVelType;

			mMainForm.txtMinStartVelX.Text = mvMinStartVel.x;
			mMainForm.txtMinStartVelY.Text = mvMinStartVel.y;
			mMainForm.txtMinStartVelZ.Text = mvMinStartVel.z;

			mMainForm.txtMaxStartVelX.Text = mvMaxStartVel.x;
			mMainForm.txtMaxStartVelY.Text = mvMaxStartVel.y;
			mMainForm.txtMaxStartVelZ.Text = mvMaxStartVel.z;

			mMainForm.txtMinStartVelAnglesX.Text = mvMinStartVelAngles.x;
			mMainForm.txtMinStartVelAnglesY.Text = mvMinStartVelAngles.y;
			
			mMainForm.txtMaxStartVelAnglesX.Text = mvMaxStartVelAngles.x;
			mMainForm.txtMaxStartVelAnglesY.Text = mvMaxStartVelAngles.y;
			
			mMainForm.txtMinStartVelSpeed.Text = mfMinStartVelSpeed;
			mMainForm.txtMaxStartVelSpeed.Text = mfMaxStartVelSpeed;

			mMainForm.txtMinSpeedMultiply.Text = mfMinSpeedMultiply;
			mMainForm.txtMaxSpeedMultiply.Text = mfMaxSpeedMultiply;
			
			mMainForm.txtMinStartAccX.Text = mvMinStartAcc.x;
			mMainForm.txtMinStartAccY.Text = mvMinStartAcc.y;
			mMainForm.txtMinStartAccZ.Text = mvMinStartAcc.z;
			
			mMainForm.txtMaxStartAccX.Text = mvMaxStartAcc.x;
			mMainForm.txtMaxStartAccY.Text = mvMaxStartAcc.y;
			mMainForm.txtMaxStartAccZ.Text = mvMaxStartAcc.z;
		
			mMainForm.txtMinVelMaximum.Text = mfMinVelMaximum;
			mMainForm.txtMaxVelMaximum.Text = mfMaxVelMaximum;
					
			mMainForm.objUsesDirection.Checked = mbUsesDirection=="True";
			mMainForm.objGravityType.SelectedItem = mGravityType;
			
			mMainForm.txtGravityAccX.Text = mvGravityAcc.x;
			mMainForm.txtGravityAccY.Text = mvGravityAcc.y;
			mMainForm.txtGravityAccZ.Text = mvGravityAcc.z;

			mMainForm.objCoordSystem.SelectedItem = mCoordSystem;

			// NEW
			mMainForm.objUsePartSpin.Checked = ( mbUsePartSpin == "True" );

			mMainForm.objParticleSpinType.SelectedItem = mPartSpinType;

			mMainForm.txtMinSpinRange.Text = mfMinSpinRange;
			mMainForm.txtMaxSpinRange.Text = mfMaxSpinRange;

            mMainForm.objUseRevolution.Checked = ( mbUseRevolution == "True" );
			
			mMainForm.txtMinRevVelX.Text = mvMinRevVel.x;
			mMainForm.txtMinRevVelY.Text = mvMinRevVel.y;
			mMainForm.txtMinRevVelZ.Text = mvMinRevVel.z;

			mMainForm.txtMaxRevVelX.Text = mvMaxRevVel.x;
			mMainForm.txtMaxRevVelY.Text = mvMaxRevVel.y;
			mMainForm.txtMaxRevVelZ.Text = mvMaxRevVel.z;

            // ---
			
			//////////////////////////////
			///////// LIFE ////////////
			mMainForm.txtMinLifeSpan.Text = mfMinLifeSpan;
			mMainForm.txtMaxLifeSpan.Text = mfMaxLifeSpan;

			mMainForm.objDeathType.SelectedItem = mDeathType;

			mMainForm.txtDeathPS.Text = msDeathPS;

			//////////////////////////////
			///////// RENDERING ////////////
			mMainForm.objDrawType.SelectedItem = mDrawType;

			mMainForm.objMultiplyRGBWithAlpha.Checked = mbMultiplyRGBWithAlpha == "True";
			
			mMainForm.txtMinStartSizeX.Text = mvMinStartSize.x;
			mMainForm.txtMinStartSizeY.Text = mvMinStartSize.y;
			
			mMainForm.txtMaxStartSizeX.Text = mvMaxStartSize.x;
			mMainForm.txtMaxStartSizeY.Text = mvMaxStartSize.y;
			
			mMainForm.txtStartRelSize.Text  = mfStartRelSize;
			mMainForm.txtMiddleRelSize.Text  = mfMiddleRelSize;
			mMainForm.txtEndRelSize.Text  = mfEndRelSize;
			mMainForm.txtMiddleRelSizeTime.Text  = mfMiddleRelSizeTime;
			mMainForm.txtMiddleRelSizeLength.Text  = mfMiddleRelSizeLength;

			//////////////////////////////
			/////////// COLOR  ////////  
			mMainForm.txtMinStartColorR.Text = mMinStartColor.r;
			mMainForm.txtMinStartColorG.Text = mMinStartColor.g;
			mMainForm.txtMinStartColorB.Text = mMinStartColor.b;
			mMainForm.txtMinStartColorA.Text = mMinStartColor.a;

			mMainForm.txtMaxStartColorR.Text = mMaxStartColor.r;
			mMainForm.txtMaxStartColorG.Text = mMaxStartColor.g;
			mMainForm.txtMaxStartColorB.Text = mMaxStartColor.b;
			mMainForm.txtMaxStartColorA.Text = mMaxStartColor.a;


			mMainForm.txtStartRelColorR.Text = mStartRelColor.r;
			mMainForm.txtStartRelColorG.Text = mStartRelColor.g;
			mMainForm.txtStartRelColorB.Text = mStartRelColor.b;
			mMainForm.txtStartRelColorA.Text = mStartRelColor.a;
						
			mMainForm.txtMiddleRelColorR.Text = mMiddleRelColor.r;
			mMainForm.txtMiddleRelColorG.Text = mMiddleRelColor.g;
			mMainForm.txtMiddleRelColorB.Text = mMiddleRelColor.b;
			mMainForm.txtMiddleRelColorA.Text = mMiddleRelColor.a;
			
			mMainForm.txtEndRelColorR.Text = mEndRelColor.r;
			mMainForm.txtEndRelColorG.Text = mEndRelColor.g;
			mMainForm.txtEndRelColorB.Text = mEndRelColor.b;
			mMainForm.txtEndRelColorA.Text = mEndRelColor.a;
			
			mMainForm.txtMiddleRelColorTime.Text = mfMiddleRelColorTime;
			mMainForm.txtMiddleRelColorLength.Text = mfMiddleRelColorLength;

			//////////////////////////////
			///////// COLLISION ////////////
			mMainForm.objCollides.Checked = mbCollides == "True";

			mMainForm.txtMinCollisionMax.Text = mlMinCollisionMax;
			mMainForm.txtMaxCollisionMax.Text = mlMaxCollisionMax;
			
			mMainForm.txtMinBounceAmount.Text = mfMinBounceAmount;
			mMainForm.txtMaxBounceAmount.Text = mfMaxBounceAmount;

			mMainForm.txtCollisionUpdateRate.Text = mlCollisionUpdateRate;

			// NEW
			////////////////////////////////
			///////// BEAM SPECIFIC ////////
			mMainForm.objUseBeamNoise.Checked = (mbUseBeamNoise == "True");

			mMainForm.txtLowFreqPoints.Text = mlLowFreqPoints;
			mMainForm.txtMinLFNoiseX.Text = mvMinLFNoise.x;
			mMainForm.txtMinLFNoiseY.Text = mvMinLFNoise.y;
			mMainForm.txtMinLFNoiseZ.Text = mvMinLFNoise.z;
			mMainForm.txtMaxLFNoiseX.Text = mvMaxLFNoise.x;
			mMainForm.txtMaxLFNoiseY.Text = mvMaxLFNoise.y;
			mMainForm.txtMaxLFNoiseZ.Text = mvMaxLFNoise.z;


			mMainForm.txtHighFreqPoints.Text = mlHighFreqPoints;
			mMainForm.txtMinHFNoiseX.Text = mvMinHFNoise.x;
			mMainForm.txtMinHFNoiseY.Text = mvMinHFNoise.y;
			mMainForm.txtMinHFNoiseZ.Text = mvMinHFNoise.z;
			mMainForm.txtMaxHFNoiseX.Text = mvMaxHFNoise.x;
			mMainForm.txtMaxHFNoiseY.Text = mvMaxHFNoise.y;
			mMainForm.txtMaxHFNoiseZ.Text = mvMaxHFNoise.z;

			mMainForm.objApplyNoiseToStart.Checked = (mbApplyNoiseToStart == "True");
			mMainForm.objApplyNoiseToEnd.Checked = (mbApplyNoiseToEnd == "True");

			// ---
		}

		//---------------------------------

		public void Save(XmlElement aChildElem)
		{
			//////////////////////////////
			///////// GENERAL ////////////
			aChildElem.SetAttribute("Name", msName);
			
			// NEW
			aChildElem.SetAttribute("PEType", mPEType);
			// ---
			aChildElem.SetAttribute("MaxParticleNum",mlMaxParticleNum);
			aChildElem.SetAttribute("Respawn",mbRespawn);
			aChildElem.SetAttribute("ParticlesPerSecond",mfParticlesPerSecond);
			aChildElem.SetAttribute("StartTimeOffset",mfStartTimeOffset);

			aChildElem.SetAttribute("WarmUpTime",mfWarmUpTime);
			aChildElem.SetAttribute("WarmUpStepsPerSec",mfWarmUpStepsPerSec);

			aChildElem.SetAttribute("MinPauseLength",mfMinPauseLength);
			aChildElem.SetAttribute("MaxPauseLength",mfMaxPauseLength);
			aChildElem.SetAttribute("MinPauseInterval",mfMinPauseInterval);
			aChildElem.SetAttribute("MaxPauseInterval",mfMaxPauseInterval);
			
			aChildElem.SetAttribute("PosOffset",mvPosOffset.GetText());
			aChildElem.SetAttribute("AngleOffset",mvAngleOffset.GetText());

			//////////////////////////////
			///////// MATERIAL ////////////
			aChildElem.SetAttribute("Material",	msMaterial);
			aChildElem.SetAttribute("MaterialNum",mlMaterialNum);
			aChildElem.SetAttribute("AnimationLength",mfAnimationLength);

			aChildElem.SetAttribute("SubDiv",mvSubDiv.GetText());
			aChildElem.SetAttribute("SubDivType",mSubDivType);
			
			//////////////////////////////
			///////// START ////////////
			aChildElem.SetAttribute("StartPosType",mStartPosType);
			
			aChildElem.SetAttribute("MinStartPos",mvMinStartPos.GetText());
			aChildElem.SetAttribute("MaxStartPos",mvMaxStartPos.GetText());
			
			aChildElem.SetAttribute("MinStartRadius",mfMinStartRadius);
			aChildElem.SetAttribute("MaxStartRadius",mfMaxStartRadius);

			aChildElem.SetAttribute("MinStartAngles",mvMinStartAngles.GetText());
			aChildElem.SetAttribute("MaxStartAngles",mvMaxStartAngles.GetText());

			//////////////////////////////
			///////// MOVEMENT ////////////
			aChildElem.SetAttribute("StartVelType",mStartVelType);

			aChildElem.SetAttribute("MinStartVel",mvMinStartVel.GetText());
			aChildElem.SetAttribute("MaxStartVel",mvMaxStartVel.GetText());

			aChildElem.SetAttribute("MinStartVelAngles",mvMinStartVelAngles.GetText());
			aChildElem.SetAttribute("MaxStartVelAngles",mvMaxStartVelAngles.GetText());
			
			aChildElem.SetAttribute("MinStartVelSpeed",mfMinStartVelSpeed);
			aChildElem.SetAttribute("MaxStartVelSpeed",mfMaxStartVelSpeed);

			aChildElem.SetAttribute("MinSpeedMultiply",mfMinSpeedMultiply);
			aChildElem.SetAttribute("MaxSpeedMultiply",mfMaxSpeedMultiply);
			
			aChildElem.SetAttribute("MinStartAcc",mvMinStartAcc.GetText());
			aChildElem.SetAttribute("MaxStartAcc",mvMaxStartAcc.GetText());
			
			aChildElem.SetAttribute("MinVelMaximum",mfMinVelMaximum);
			aChildElem.SetAttribute("MaxVelMaximum",mfMaxVelMaximum);
					
			aChildElem.SetAttribute("UsesDirection",mbUsesDirection);
			aChildElem.SetAttribute("GravityType",mGravityType);
			
			aChildElem.SetAttribute("GravityAcc",mvGravityAcc.GetText());

			aChildElem.SetAttribute("CoordSystem",mCoordSystem);

			////////////////////////////////
			////////// ROTATION -- NEW
            aChildElem.SetAttribute("UsePartSpin",mbUsePartSpin);

			aChildElem.SetAttribute("PartSpinType",mPartSpinType);

            aChildElem.SetAttribute("MinSpinRange",mfMinSpinRange);
			aChildElem.SetAttribute("MaxSpinRange",mfMaxSpinRange);

			aChildElem.SetAttribute("UseRevolution",mbUseRevolution);

			aChildElem.SetAttribute("MinRevVel", mvMinRevVel.GetText());
			aChildElem.SetAttribute("MaxRevVel", mvMaxRevVel.GetText());
			// ---

			//////////////////////////////
			///////// LIFE ////////////
			aChildElem.SetAttribute("MinLifeSpan",mfMinLifeSpan);
			aChildElem.SetAttribute("MaxLifeSpan",mfMaxLifeSpan);

			aChildElem.SetAttribute("DeathType",mDeathType);

			aChildElem.SetAttribute("DeathPS",msDeathPS);

			//////////////////////////////
			///////// RENDERING ////////////
			aChildElem.SetAttribute("DrawType",mDrawType);

			aChildElem.SetAttribute("MultiplyRGBWithAlpha",mbMultiplyRGBWithAlpha);
			
			aChildElem.SetAttribute("MinStartSize",mvMinStartSize.GetText());
			
			aChildElem.SetAttribute("MaxStartSize",mvMaxStartSize.GetText());
			
			aChildElem.SetAttribute("StartRelSize",mfStartRelSize);
			aChildElem.SetAttribute("MiddleRelSize",mfMiddleRelSize);
			aChildElem.SetAttribute("EndRelSize",mfEndRelSize);
			aChildElem.SetAttribute("MiddleRelSizeTime",mfMiddleRelSizeTime);
			aChildElem.SetAttribute("MiddleRelSizeLength",mfMiddleRelSizeLength);
			
			//////////////////////////////
			///////// COLOR ////////////
			aChildElem.SetAttribute("MinStartColor",mMinStartColor.GetText());
			aChildElem.SetAttribute("MaxStartColor",mMaxStartColor.GetText());

			aChildElem.SetAttribute("StartRelColor",mStartRelColor.GetText());
			aChildElem.SetAttribute("MiddleRelColor",mMiddleRelColor.GetText());
			aChildElem.SetAttribute("EndRelColor",mEndRelColor.GetText());
			aChildElem.SetAttribute("MiddleRelColorTime",mfMiddleRelColorTime);
			aChildElem.SetAttribute("MiddleRelColorLength",mfMiddleRelColorLength);
			

			//////////////////////////////
			///////// COLLISION ////////////
			aChildElem.SetAttribute("Collides",mbCollides);

			aChildElem.SetAttribute("MinCollisionMax",mlMinCollisionMax);
			aChildElem.SetAttribute("MaxCollisionMax",mlMaxCollisionMax);
			
			aChildElem.SetAttribute("MinBounceAmount",mfMinBounceAmount);
			aChildElem.SetAttribute("MaxBounceAmount",mfMaxBounceAmount);

			aChildElem.SetAttribute("CollisionUpdateRate",mlCollisionUpdateRate);

			// NEW
			//////////////////////////////////
			/////////// BEAM SPECIFIC ////////
			aChildElem.SetAttribute("UseBeamNoise", mbUseBeamNoise);

			aChildElem.SetAttribute("LowFreqPoints", mlLowFreqPoints);
			aChildElem.SetAttribute("MinLFNoise", mvMinLFNoise.GetText());
			aChildElem.SetAttribute("MaxLFNoise", mvMaxLFNoise.GetText());

			aChildElem.SetAttribute("HighFreqPoints", mlHighFreqPoints);
			aChildElem.SetAttribute("MinHFNoise", mvMinHFNoise.GetText());
			aChildElem.SetAttribute("MaxHFNoise", mvMaxHFNoise.GetText());

			aChildElem.SetAttribute("ApplyNoiseToStart", mbApplyNoiseToStart);
			aChildElem.SetAttribute("ApplyNoiseToEnd", mbApplyNoiseToEnd);

			// ---
		}
		
		//---------------------------------

		public void Load(XmlElement aChildElem)
		{
			//////////////////////////////
			///////// GENERAL ////////////
			msName = aChildElem.GetAttribute("Name");
			// NEW
			mPEType = aChildElem.GetAttribute("PEType");
			// ---
			mlMaxParticleNum  = aChildElem.GetAttribute("MaxParticleNum");
			mbRespawn  = aChildElem.GetAttribute("Respawn");
			mfParticlesPerSecond  = aChildElem.GetAttribute("ParticlesPerSecond");
			mfStartTimeOffset  = aChildElem.GetAttribute("StartTimeOffset");

			mfWarmUpTime = aChildElem.GetAttribute("WarmUpTime");
			mfWarmUpStepsPerSec = aChildElem.GetAttribute("WarmUpStepsPerSec");
			
			mfMinPauseLength = aChildElem.GetAttribute("MinPauseLength");
			mfMaxPauseLength =  aChildElem.GetAttribute("MaxPauseLength");
			mfMinPauseInterval =  aChildElem.GetAttribute("MinPauseInterval");
			mfMaxPauseInterval = aChildElem.GetAttribute("MaxPauseInterval");
			
			mvPosOffset.FromText(aChildElem.GetAttribute("PosOffset"));
			mvAngleOffset.FromText(aChildElem.GetAttribute("AngleOffset"));
			
			//////////////////////////////
			///////// MATERIAL ///////////
			msMaterial = aChildElem.GetAttribute("Material");
			mlMaterialNum = aChildElem.GetAttribute("MaterialNum");
			mfAnimationLength = aChildElem.GetAttribute("AnimationLength");

			mvSubDiv.FromText(aChildElem.GetAttribute("SubDiv"));
			mSubDivType = aChildElem.GetAttribute("SubDivType");

			//////////////////////////////
			///////// START ////////////
			mStartPosType = aChildElem.GetAttribute("StartPosType");
			
			mvMinStartPos.FromText(aChildElem.GetAttribute("MinStartPos"));
			mvMaxStartPos.FromText(aChildElem.GetAttribute("MaxStartPos"));
			
			mfMinStartRadius = aChildElem.GetAttribute("MinStartRadius");
			mfMaxStartRadius = aChildElem.GetAttribute("MaxStartRadius");

			mvMinStartAngles.FromText(aChildElem.GetAttribute("MinStartAngles"));
			mvMaxStartAngles.FromText(aChildElem.GetAttribute("MaxStartAngles"));

			//////////////////////////////
			///////// MOVEMENT ////////////
			mStartVelType = aChildElem.GetAttribute("StartVelType");

			mvMinStartVel.FromText(aChildElem.GetAttribute("MinStartVel"));
			mvMaxStartVel.FromText(aChildElem.GetAttribute("MaxStartVel"));

			mvMinStartVelAngles.FromText(aChildElem.GetAttribute("MinStartVelAngles"));
			mvMaxStartVelAngles.FromText(aChildElem.GetAttribute("MaxStartVelAngles"));
			
			mfMinStartVelSpeed = aChildElem.GetAttribute("MinStartVelSpeed");
			mfMaxStartVelSpeed = aChildElem.GetAttribute("MaxStartVelSpeed");

			mfMinSpeedMultiply = aChildElem.GetAttribute("MinSpeedMultiply");
			mfMaxSpeedMultiply = aChildElem.GetAttribute("MaxSpeedMultiply");
			
			mvMinStartAcc.FromText(aChildElem.GetAttribute("MinStartAcc"));
			mvMaxStartAcc.FromText(aChildElem.GetAttribute("MaxStartAcc"));
			
			mfMinVelMaximum = aChildElem.GetAttribute("MinVelMaximum");
			mfMaxVelMaximum = aChildElem.GetAttribute("MaxVelMaximum");
					
			mbUsesDirection = aChildElem.GetAttribute("UsesDirection");
			mGravityType = aChildElem.GetAttribute("GravityType");
			
			mvGravityAcc.FromText(aChildElem.GetAttribute("GravityAcc"));

			mCoordSystem = aChildElem.GetAttribute("CoordSystem");

			// NEW

			mbUsePartSpin = aChildElem.GetAttribute("UsePartSpin");

			mPartSpinType = aChildElem.GetAttribute("SpinType");
			mfMinSpinRange = aChildElem.GetAttribute("MinSpinRange");
			mfMaxSpinRange = aChildElem.GetAttribute("MaxSpinRange");

			mbUseRevolution = aChildElem.GetAttribute("UseRevolution");

			mvMinRevVel.FromText( aChildElem.GetAttribute("MinRevVel") );
			mvMaxRevVel.FromText( aChildElem.GetAttribute("MaxRevVel") );
			
			// ---

			//////////////////////////////
			///////// LIFE ////////////
			mfMinLifeSpan = aChildElem.GetAttribute("MinLifeSpan");
			mfMaxLifeSpan = aChildElem.GetAttribute("MaxLifeSpan");

			mDeathType = aChildElem.GetAttribute("DeathType");

			msDeathPS = aChildElem.GetAttribute("DeathPS");

			//////////////////////////////
			///////// RENDERING ////////////
			mDrawType = aChildElem.GetAttribute("DrawType");

			mbMultiplyRGBWithAlpha = aChildElem.GetAttribute("MultiplyRGBWithAlpha");
			
			mvMinStartSize.FromText(aChildElem.GetAttribute("MinStartSize"));
			
			mvMaxStartSize.FromText(aChildElem.GetAttribute("MaxStartSize"));
			
			mfStartRelSize = aChildElem.GetAttribute("StartRelSize");
			mfMiddleRelSize = aChildElem.GetAttribute("MiddleRelSize");
			mfEndRelSize = aChildElem.GetAttribute("EndRelSize");
			mfMiddleRelSizeTime = aChildElem.GetAttribute("MiddleRelSizeTime");
			mfMiddleRelSizeLength = aChildElem.GetAttribute("MiddleRelSizeLength");

			//////////////////////////////
			///////// COLOR ////////////
			mMinStartColor.FromText(aChildElem.GetAttribute("MinStartColor"));
			mMaxStartColor.FromText(aChildElem.GetAttribute("MaxStartColor"));

			mStartRelColor.FromText(aChildElem.GetAttribute("StartRelColor"));
			mMiddleRelColor.FromText(aChildElem.GetAttribute("MiddleRelColor"));
			mEndRelColor.FromText(aChildElem.GetAttribute("EndRelColor"));
			mfMiddleRelColorTime = aChildElem.GetAttribute("MiddleRelColorTime");
			mfMiddleRelColorLength = aChildElem.GetAttribute("MiddleRelColorLength");

			//////////////////////////////
			///////// COLLISION ////////////
			mbCollides = aChildElem.GetAttribute("Collides");

			mlMinCollisionMax  = aChildElem.GetAttribute("MinCollisionMax");
			mlMaxCollisionMax  = aChildElem.GetAttribute("MaxCollisionMax");
			
			mfMinBounceAmount  = aChildElem.GetAttribute("MinBounceAmount");
			mfMaxBounceAmount  = aChildElem.GetAttribute("MaxBounceAmount");

			mlCollisionUpdateRate  = aChildElem.GetAttribute("CollisionUpdateRate");

			// NEW
			////////////////////////////////
			////////// BEAM SPECIFIC ///////
			mbUseBeamNoise = aChildElem.GetAttribute("UseBeamNoise");
			
			mlLowFreqPoints = aChildElem.GetAttribute("LowFreqPoints");
			mvMinLFNoise.FromText(aChildElem.GetAttribute("MinLFNoise"));
			mvMaxLFNoise.FromText(aChildElem.GetAttribute("MaxLFNoise"));

			mlHighFreqPoints = aChildElem.GetAttribute("HighFreqPoints");
			mvMinHFNoise.FromText(aChildElem.GetAttribute("MinHFNoise"));
			mvMaxHFNoise.FromText(aChildElem.GetAttribute("MaxHFNoise"));

			mbApplyNoiseToStart = aChildElem.GetAttribute("ApplyNoiseToStart");
			mbApplyNoiseToEnd = aChildElem.GetAttribute("ApplyNoiseToEnd");
			// ---
		}
		
		//---------------------------------

		public Form1 mMainForm;

		//---------------------------------

		///////// GENERAL //////////
		public String msName = "Emitter01";

		// NEW

		public String mPEType = "Normal";

		// ---
        
		public String mlMaxParticleNum ="10";
			
		public String mbRespawn = "True";
			
		public String mfParticlesPerSecond = "1";
		public String mfStartTimeOffset = "0";

		public String mfWarmUpTime = "0";
		public String mfWarmUpStepsPerSec = "60";

		public String mfMinPauseLength = "0";
		public String mfMaxPauseLength = "0";

		public String mfMinPauseInterval = "0";
		public String mfMaxPauseInterval = "0";
			
		public Vector3 mvPosOffset = new Vector3("0","0","0");
		public Vector3 mvAngleOffset = new Vector3("0","0","0");

		///////// MATERIAL //////////
		
		public String msMaterial = "";
		public String mlMaterialNum = "1";
		public String mfAnimationLength = "1";

		public Vector2 mvSubDiv = new Vector2("0","0");
		public String mSubDivType = "Random";
		
		///////// START POS //////////
		public String mStartPosType = "Box";
			
		public Vector3 mvMinStartPos = new Vector3("0","0","0");
		public Vector3 mvMaxStartPos = new Vector3("0","0","0");
			
		public String mfMinStartRadius = "0";
		public String mfMaxStartRadius = "1";

		public Vector2 mvMinStartAngles = new Vector2("-180","-180");
		public Vector2 mvMaxStartAngles = new Vector2("180","180");

		/////////// MOVEMENT ////////
		public String mStartVelType = "Box";

		public Vector3 mvMinStartVel = new Vector3("0","0","0");
		public Vector3 mvMaxStartVel = new Vector3("0","0","0");

		public Vector2 mvMinStartVelAngles = new Vector2("-180","-180");
		public Vector2 mvMaxStartVelAngles = new Vector2("180","180");
			
		public String mfMinStartVelSpeed = "0";
		public String mfMaxStartVelSpeed = "1";

		public String mfMinSpeedMultiply = "1";
		public String mfMaxSpeedMultiply = "1";
			
		public Vector3 mvMinStartAcc = new Vector3("0","0","0");
		public Vector3 mvMaxStartAcc = new Vector3("0","0","0");
			
		public String mfMinVelMaximum = "0";
		public String mfMaxVelMaximum = "0";
					
		public String mbUsesDirection = "False";
		public String mGravityType = "None";
			
		public Vector3 mvGravityAcc = new Vector3("0","0","0");

		public String mCoordSystem = "World";

		// NEW

		public String mbUsePartSpin = "False";

		public String mPartSpinType = "Constant";

		public String mfMinSpinRange = "0";
		public String mfMaxSpinRange = "0";

		public String mbUseRevolution = "False";

		public Vector3 mvMinRevVel = new Vector3("0", "0", "0");
		public Vector3 mvMaxRevVel = new Vector3("0", "0", "0");

		// ---	
		/////////// LIFESPAN ////////
		public String mfMinLifeSpan = "10";
		public String mfMaxLifeSpan = "10";

		public String mDeathType = "Age";

		public String msDeathPS = "";

		/////////// RENDERING ////////
		public String mDrawType = "Point";
			
		public Vector2 mvMinStartSize = new Vector2("1","1");
		public Vector2 mvMaxStartSize = new Vector2("1","1");

		public String mfStartRelSize = "1";
		public String mfMiddleRelSize = "1";
		public String mfEndRelSize = "1";
		public String mfMiddleRelSizeTime = "0";
		public String mfMiddleRelSizeLength  = "0";
		
		public String mbMultiplyRGBWithAlpha = "False";
		
		/////////// COLOR  ////////  

		public Color4 mMinStartColor = new Color4("1","1","1","1");
		public Color4 mMaxStartColor = new Color4("1","1","1","1");

		public Color4 mStartRelColor = new Color4("1","1","1","1");
		public Color4 mMiddleRelColor = new Color4("1","1","1","1");
		public Color4 mEndRelColor = new Color4("1","1","1","1");
		public String mfMiddleRelColorTime = "0";
		public String mfMiddleRelColorLength= "0";
		

		/////////// COLLISION  ////////  
		public String mbCollides = "False";
		
		public String mfMinBounceAmount = "0.6";
		public String mfMaxBounceAmount = "0.6";
		
		public String mlMinCollisionMax = "10";	
		public String mlMaxCollisionMax = "10";

		public String mlCollisionUpdateRate = "10";

		// NEW
		//////////// BEAM SPECIFIC //////////
		
		public String mbUseBeamNoise = "False";

		public String mlLowFreqPoints = "2";
		public Vector3 mvMinLFNoise = new Vector3("0", "0", "0");
		public Vector3 mvMaxLFNoise = new Vector3("0", "0", "0");
		
		public String mlHighFreqPoints = "2";
		public Vector3 mvMinHFNoise = new Vector3("0", "0", "0");
		public Vector3 mvMaxHFNoise = new Vector3("0", "0", "0");

		public String mbApplyNoiseToStart = "False";
		public String mbApplyNoiseToEnd = "False";
        
		// ---
	}

	//-----------------------------------------------
		
	public class ParticleSystem
	{
		//---------------------------------

		public ParticleSystem(Form1 aMainForm)
		{
			mMainForm = aMainForm;

			mvEmitters = new ArrayList();
		}

		//---------------------------------
		
		public void Reset()
		{
			mCurrentEmitter = null;
			mvEmitters.Clear();

			ParticleEmitter pe = new ParticleEmitter(mMainForm);
			mvEmitters.Add(pe);
			mCurrentEmitter = pe;
			mlCurrentNum =0;

			mMainForm.objEmitterList.Items.Clear();
            
			mMainForm.objEmitterList.Items.Add("Emitter01");
			mMainForm.objEmitterList.SelectedIndex = 0;

			mMainForm.objShowRoom.Checked = false;
			mMainForm.txtRoomSizeX.Text = "6";
			mMainForm.txtRoomSizeY.Text = "6";
			mMainForm.txtRoomSizeZ.Text = "6";

			mMainForm.txtFileName.Text = "";
		}

		//---------------------------------

		public void AddEmitter()
		{
			ParticleEmitter pe = new ParticleEmitter(mMainForm);
			
			int lCount = mvEmitters.Count +1;
			pe.msName = lCount <= 9 ? "Emitter0"+lCount : "Emitter"+lCount;

			mvEmitters.Add(pe);
			
			mMainForm.objEmitterList.Items.Add(pe.msName);
			mMainForm.objEmitterList.SelectedIndex = lCount-1;
		}

		//---------------------------------

		public void RemoveEmitter(int alIdx)
		{
			if(mvEmitters.Count ==1)
			{
				MessageBox.Show("There must be at least one emitter in a system!",
								"Alert!",MessageBoxButtons.OK);
				return;
			}

			DialogResult Res = MessageBox.Show("Are you sure you want to remove the emitter '"+
												((ParticleEmitter)mvEmitters[alIdx]).msName+"'?",
												"Warning!",MessageBoxButtons.YesNo);
			if(Res == DialogResult.No)
			{
				return;	
			}
			
			mvEmitters.RemoveAt(alIdx);
			mMainForm.objEmitterList.Items.RemoveAt(alIdx);

			if(alIdx >0) 
				mMainForm.objEmitterList.SelectedIndex = alIdx - 1;
			else
				mMainForm.objEmitterList.SelectedIndex =0;

		}

		//---------------------------------

		public void CopyEmitter(int alIdx)
		{
			ParticleEmitter pe = new ParticleEmitter(mMainForm);
			ParticleEmitter oldPE = (ParticleEmitter)mvEmitters[alIdx];
			
			XmlDocument TempDoc = new XmlDocument();
			XmlElement TempElem = TempDoc.CreateElement("Element");

            oldPE.Save(TempElem);
			pe.Load(TempElem);
			pe.msName = "Copy of "+ oldPE.msName;

			mvEmitters.Add(pe);
			
			mMainForm.objEmitterList.Items.Add(pe.msName);
			mMainForm.objEmitterList.SelectedIndex = mvEmitters.Count-1;
		}

		//---------------------------------

		public void ChangeCurrentEmitter(int alIdx)
		{
			mCurrentEmitter = (ParticleEmitter)mvEmitters[alIdx];
            mCurrentEmitter.CopyToGui();
		}

		//---------------------------------

		public void Load(String asFile)
		{
			//Clear main form and emitter vector
			mMainForm.objEmitterList.Items.Clear();
			mvEmitters.Clear();

			//Load the document
			XmlDocument Doc = new XmlDocument();
			Doc.Load(asFile);

			XmlElement DocRoot = (XmlElement)Doc.FirstChild;
						
			//Iterate trough all emitters
			for(int child_count=0; child_count< DocRoot.ChildNodes.Count; ++child_count)
			{
				XmlElement ChildNode = (XmlElement)DocRoot.ChildNodes[child_count];
				ParticleEmitter pe = new ParticleEmitter(mMainForm);

				pe.Load(ChildNode);
				
				mvEmitters.Add(pe);
				mMainForm.objEmitterList.Items.Add(pe.msName);
			}

			//Variables
			mbShowRoom = DocRoot.GetAttribute("ShowRoom");
			mvRoomSize.FromText(DocRoot.GetAttribute("RoomSize"));

			mMainForm.objShowRoom.Checked = mbShowRoom == "True";
			mMainForm.txtRoomSizeX.Text = mvRoomSize.x;
			mMainForm.txtRoomSizeY.Text = mvRoomSize.y;
			mMainForm.txtRoomSizeZ.Text = mvRoomSize.z;
		
            
			//Set current particle system
			mCurrentEmitter = (ParticleEmitter)mvEmitters[0];

			mMainForm.objEmitterList.SelectedIndex = 0;

			mCurrentEmitter.CopyToGui();
		}

		//---------------------------------

		public void Save(String asFile)
		{
			XmlDocument Doc = new XmlDocument();
			
			XmlElement DocRoot = Doc.CreateElement("ParticleSystem");
			Doc.AppendChild(DocRoot);

			//Emitters
			for(int i=0; i< mvEmitters.Count; i++)
			{
				XmlElement ChildElem = Doc.CreateElement("ParticleEmitter");
				DocRoot.AppendChild(ChildElem);
				
				((ParticleEmitter)mvEmitters[i]).Save(ChildElem);
			}

			//Variables
			DocRoot.SetAttribute("ShowRoom",mbShowRoom);
			DocRoot.SetAttribute("RoomSize",mvRoomSize.GetText());
			
			Doc.Save(asFile);
		}

		//---------------------------------

		public Form1 mMainForm;
		public ArrayList mvEmitters;

		public String mbShowRoom="False";
		public Vector3 mvRoomSize = new Vector3("6","6","6");

		public ParticleEmitter mCurrentEmitter=null;
		public int mlCurrentNum=0;
		
	}
}
