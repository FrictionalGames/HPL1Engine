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
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;

namespace ParticleEditor
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		public System.Windows.Forms.ComboBox objEmitterList;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.TabPage tabPage4;
		private System.Windows.Forms.TabPage tabPage5;
		private System.Windows.Forms.TabPage tabPage6;
		private System.Windows.Forms.TabPage tabPage7;
		private System.Windows.Forms.TabPage tabPage8;
		private System.Windows.Forms.Button objAddEmitter;
		private System.Windows.Forms.Button objRemoveEmitter;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		public System.Windows.Forms.TextBox txtMaxParticles;
		public System.Windows.Forms.TextBox txtParticlesPerSec;
		public System.Windows.Forms.TextBox txtStartTimeOffset;
		private System.Windows.Forms.Label label5;
		public System.Windows.Forms.TextBox txtMinPauseLength;
		public System.Windows.Forms.TextBox txtMaxPauseLength;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		public System.Windows.Forms.TextBox txtMaxPauseInterval;
		public System.Windows.Forms.TextBox txtMinPauseInterval;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label11;
		public System.Windows.Forms.TextBox txtPosOffsetX;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label14;
		public System.Windows.Forms.TextBox txtPosOffsetY;
		public System.Windows.Forms.TextBox txtPosOffsetZ;
		public System.Windows.Forms.TextBox txtAngleOffsetZ;
		private System.Windows.Forms.Label label15;
		public System.Windows.Forms.TextBox txtAngleOffsetY;
		private System.Windows.Forms.Label label16;
		public System.Windows.Forms.TextBox txtAngleOffsetX;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.Label label18;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		public System.Windows.Forms.CheckBox objRespawn;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox Pause;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label19;
		public System.Windows.Forms.TextBox txtName;

		public ParticleSystem mPS=null;
		private System.Windows.Forms.MenuItem menuItem6;
		private System.Windows.Forms.MenuItem menuItem7;

		private SaveFileDialog mSaveFileDialog=null;
		private OpenFileDialog mMaterialFileDialog=null;
		private OpenFileDialog mOpenFileDialog=null;
		
		
		private System.Windows.Forms.Label label20;
		public System.Windows.Forms.TextBox txtFileName;
		private System.Windows.Forms.Button objView;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Label label21;
		public System.Windows.Forms.TextBox txtMaterial;
		public System.Windows.Forms.Button objBrowseMaterial;
		private System.Windows.Forms.Label label22;
		public System.Windows.Forms.TextBox txtMaterialNum;
		private System.Windows.Forms.Label label23;
		public System.Windows.Forms.TextBox txtAnimationLength;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.Label label24;
		public System.Windows.Forms.TextBox txtSubDivX;
		private System.Windows.Forms.Label label25;
		public System.Windows.Forms.TextBox txtSubDivY;
		private System.Windows.Forms.Label label26;
		private System.Windows.Forms.Label label27;
		private System.Windows.Forms.GroupBox groupBox5;
		private System.Windows.Forms.GroupBox groupBox6;
		private System.Windows.Forms.GroupBox groupBox7;
		private System.Windows.Forms.Label label28;
		public System.Windows.Forms.ComboBox objStartPosType;
		private System.Windows.Forms.Label label29;
		public System.Windows.Forms.TextBox txtMinStartPosX;
		private System.Windows.Forms.Label label30;
		public System.Windows.Forms.TextBox txtMinStartPosY;
		private System.Windows.Forms.Label label31;
		public System.Windows.Forms.TextBox txtMinStartPosZ;
		private System.Windows.Forms.Label label32;
		public System.Windows.Forms.TextBox txtMaxStartPosZ;
		private System.Windows.Forms.Label label33;
		public System.Windows.Forms.TextBox txtMaxStartPosY;
		private System.Windows.Forms.Label label34;
		public System.Windows.Forms.TextBox txtMaxStartPosX;
		private System.Windows.Forms.Label label35;
		private System.Windows.Forms.Label label36;
		private System.Windows.Forms.Label label37;
		private System.Windows.Forms.Label label38;
		private System.Windows.Forms.Label label39;
		private System.Windows.Forms.Label label40;
		private System.Windows.Forms.Label label41;
		public System.Windows.Forms.TextBox txtMinStartAnglesY;
		public System.Windows.Forms.TextBox txtMinStartAnglesX;
		public System.Windows.Forms.TextBox txtMaxStartAnglesX;
		public System.Windows.Forms.TextBox txtMaxStartAnglesY;
		private System.Windows.Forms.Label label42;
		private System.Windows.Forms.Label label43;
		private System.Windows.Forms.Label label44;
		private System.Windows.Forms.Label label45;
		public System.Windows.Forms.TextBox txtMinStartRadius;
		public System.Windows.Forms.TextBox txtMaxStartRadius;
		private System.Windows.Forms.GroupBox groupBox8;
		private System.Windows.Forms.Label label46;
		private System.Windows.Forms.GroupBox groupBox9;
		private System.Windows.Forms.Label label47;
		private System.Windows.Forms.Label label48;
		private System.Windows.Forms.Label label49;
		private System.Windows.Forms.Label label50;
		private System.Windows.Forms.Label label51;
		private System.Windows.Forms.Label label52;
		private System.Windows.Forms.Label label53;
		private System.Windows.Forms.Label label54;
		private System.Windows.Forms.GroupBox groupBox10;
		private System.Windows.Forms.Label label55;
		private System.Windows.Forms.Label label56;
		private System.Windows.Forms.Label label57;
		private System.Windows.Forms.Label label58;
		private System.Windows.Forms.Label label59;
		private System.Windows.Forms.Label label60;
		private System.Windows.Forms.Label label61;
		private System.Windows.Forms.Label label62;
		private System.Windows.Forms.Label label63;
		public System.Windows.Forms.ComboBox objStartVelType;
		public System.Windows.Forms.TextBox txtMaxStartVelZ;
		public System.Windows.Forms.TextBox txtMaxStartVelY;
		public System.Windows.Forms.TextBox txtMaxStartVelX;
		public System.Windows.Forms.TextBox txtMinStartVelZ;
		public System.Windows.Forms.TextBox txtMinStartVelY;
		public System.Windows.Forms.TextBox txtMinStartVelX;
		public System.Windows.Forms.TextBox txtMaxStartVelAnglesX;
		public System.Windows.Forms.TextBox txtMaxStartVelAnglesY;
		public System.Windows.Forms.TextBox txtMinStartVelAnglesX;
		public System.Windows.Forms.TextBox txtMinStartVelAnglesY;
		public System.Windows.Forms.TextBox txtMaxStartVelSpeed;
		public System.Windows.Forms.TextBox txtMinStartVelSpeed;
		private System.Windows.Forms.Label label64;
		public System.Windows.Forms.ComboBox objGravityType;
		private System.Windows.Forms.Label label65;
		private System.Windows.Forms.Label label66;
		private System.Windows.Forms.Label label67;
		private System.Windows.Forms.Label label68;
		public System.Windows.Forms.TextBox txtGravityAccZ;
		public System.Windows.Forms.TextBox txtGravityAccY;
		public System.Windows.Forms.TextBox txtGravityAccX;
		public System.Windows.Forms.CheckBox objUsesDirection;
		private System.Windows.Forms.GroupBox groupBox11;
		private System.Windows.Forms.Label label69;
		private System.Windows.Forms.Label label70;
		private System.Windows.Forms.Label label71;
		public System.Windows.Forms.TextBox txtMinVelMaximum;
		public System.Windows.Forms.TextBox txtMaxVelMaximum;
		private System.Windows.Forms.GroupBox groupBox12;
		private System.Windows.Forms.Label label72;
		private System.Windows.Forms.Label label73;
		private System.Windows.Forms.Label label74;
		private System.Windows.Forms.Label label75;
		private System.Windows.Forms.Label label77;
		private System.Windows.Forms.Label label78;
		private System.Windows.Forms.Label label79;
		public System.Windows.Forms.TextBox txtMaxStartAccZ;
		public System.Windows.Forms.TextBox txtMaxStartAccY;
		public System.Windows.Forms.TextBox txtMaxStartAccX;
		public System.Windows.Forms.TextBox txtMinStartAccX;
		private System.Windows.Forms.Label label76;
		private System.Windows.Forms.Label label80;
		private System.Windows.Forms.Label label81;
		public System.Windows.Forms.TextBox txtMinSpeedMultiply;
		public System.Windows.Forms.TextBox txtMaxSpeedMultiply;
		private System.Windows.Forms.Label label82;
		public System.Windows.Forms.ComboBox objCoordSystem;
		public System.Windows.Forms.TextBox txtMinStartAccZ;
		private System.Windows.Forms.Label lbiasas;
		public System.Windows.Forms.TextBox txtMinStartAccY;
		private System.Windows.Forms.GroupBox groupBox13;
		private System.Windows.Forms.Label label83;
		public System.Windows.Forms.TextBox txtMinLifeSpan;
		private System.Windows.Forms.Label label84;
		public System.Windows.Forms.TextBox txtMaxLifeSpan;
		private System.Windows.Forms.Label label85;
		private System.Windows.Forms.GroupBox groupBox14;
		private System.Windows.Forms.Label label86;
		public System.Windows.Forms.ComboBox objDeathType;
		private System.Windows.Forms.Label label87;
		public System.Windows.Forms.TextBox txtDeathPS;
		private System.Windows.Forms.GroupBox groupBox15;
		private System.Windows.Forms.Label label88;
		public System.Windows.Forms.ComboBox objDrawType;
		private System.Windows.Forms.GroupBox groupBox16;
		private System.Windows.Forms.Label label89;
		public System.Windows.Forms.TextBox txtMinStartSizeX;
		private System.Windows.Forms.Label label90;
		private System.Windows.Forms.Label label91;
		public System.Windows.Forms.TextBox txtMinStartSizeY;
		private System.Windows.Forms.Label label92;
		public System.Windows.Forms.TextBox txtMaxStartSizeY;
		private System.Windows.Forms.Label label93;
		public System.Windows.Forms.TextBox txtMaxStartSizeX;
		private System.Windows.Forms.Label label94;
		public System.Windows.Forms.CheckBox objMultiplyRGBWithAlpha;
		private System.Windows.Forms.Label label95;
		private System.Windows.Forms.Label label96;
		private System.Windows.Forms.Label label97;
		public System.Windows.Forms.TextBox txtStartRelSize;
		public System.Windows.Forms.TextBox txtMiddleRelSize;
		public System.Windows.Forms.TextBox txtEndRelSize;
		private System.Windows.Forms.Label label98;
		private System.Windows.Forms.Label label99;
		public System.Windows.Forms.TextBox txtMiddleRelSizeTime;
		public System.Windows.Forms.TextBox txtMiddleRelSizeLength;
		private System.Windows.Forms.GroupBox groupBox17;
		private System.Windows.Forms.GroupBox groupBox18;
		private System.Windows.Forms.Label label100;
		private System.Windows.Forms.Label label101;
		private System.Windows.Forms.Label label102;
		public System.Windows.Forms.TextBox txtMinStartColorR;
		public System.Windows.Forms.TextBox txtMinStartColorG;
		private System.Windows.Forms.Label label103;
		public System.Windows.Forms.TextBox txtMinStartColorB;
		private System.Windows.Forms.Label label104;
		public System.Windows.Forms.TextBox txtMinStartColorA;
		private System.Windows.Forms.Label label105;
		public System.Windows.Forms.TextBox txtMaxStartColorA;
		private System.Windows.Forms.Label label106;
		public System.Windows.Forms.TextBox txtMaxStartColorB;
		private System.Windows.Forms.Label label107;
		public System.Windows.Forms.TextBox txtMaxStartColorG;
		private System.Windows.Forms.Label label108;
		public System.Windows.Forms.TextBox txtMaxStartColorR;
		private System.Windows.Forms.Label label109;
		private System.Windows.Forms.GroupBox groupBox19;
		private System.Windows.Forms.Label label110;
		private System.Windows.Forms.Label label111;
		private System.Windows.Forms.Label label112;
		private System.Windows.Forms.Label label113;
		private System.Windows.Forms.Label label114;
		private System.Windows.Forms.Label label115;
		private System.Windows.Forms.Label label116;
		private System.Windows.Forms.Label label117;
		private System.Windows.Forms.Label label118;
		private System.Windows.Forms.Label label119;
		private System.Windows.Forms.Label label120;
		private System.Windows.Forms.Label label121;
		private System.Windows.Forms.Label label122;
		private System.Windows.Forms.Label label123;
		private System.Windows.Forms.Label label124;
		private System.Windows.Forms.Label label125;
		private System.Windows.Forms.Label label126;
		public System.Windows.Forms.TextBox txtMiddleRelColorTime;
		public System.Windows.Forms.TextBox txtMiddleRelColorLength;
		public System.Windows.Forms.TextBox txtStartRelColorA;
		public System.Windows.Forms.TextBox txtStartRelColorB;
		public System.Windows.Forms.TextBox txtStartRelColorG;
		public System.Windows.Forms.TextBox txtStartRelColorR;
		public System.Windows.Forms.TextBox txtMiddleRelColorA;
		public System.Windows.Forms.TextBox txtMiddleRelColorB;
		public System.Windows.Forms.TextBox txtMiddleRelColorG;
		public System.Windows.Forms.TextBox txtMiddleRelColorR;
		public System.Windows.Forms.TextBox txtEndRelColorR;
		public System.Windows.Forms.TextBox txtEndRelColorA;
		public System.Windows.Forms.TextBox txtEndRelColorB;
		public System.Windows.Forms.TextBox txtEndRelColorG;
		private System.Windows.Forms.Label label127;
		private System.Windows.Forms.Label label128;
		public System.Windows.Forms.CheckBox objCollides;
		private System.Windows.Forms.GroupBox txtBounceAmount;
		private System.Windows.Forms.Label label129;
		private System.Windows.Forms.Label label130;
		public System.Windows.Forms.TextBox txtWarmUpTime;
		public System.Windows.Forms.TextBox txtWarmUpStepsPerSec;
		private System.Windows.Forms.Button button1;
		public System.Windows.Forms.TextBox txtMinCollisionMax;
		private System.Windows.Forms.Label label131;
		private System.Windows.Forms.Label label132;
		private System.Windows.Forms.Label label133;
		private System.Windows.Forms.Label label134;
		public System.Windows.Forms.TextBox txtMaxCollisionMax;
		public System.Windows.Forms.TextBox txtMinBounceAmount;
		public System.Windows.Forms.TextBox txtMaxBounceAmount;
		private System.Windows.Forms.Label label135;
		public System.Windows.Forms.TextBox txtCollisionUpdateRate;
		public System.Windows.Forms.CheckBox objShowRoom;
		private System.Windows.Forms.Label label136;
		private System.Windows.Forms.Label label137;
		private System.Windows.Forms.Label label138;
		private System.Windows.Forms.Label label139;
		public System.Windows.Forms.TextBox txtRoomSizeX;
		public System.Windows.Forms.TextBox txtRoomSizeY;
		public System.Windows.Forms.TextBox txtRoomSizeZ;
		private System.Windows.Forms.GroupBox groupBox20;
		private System.Windows.Forms.Label label140;
		private System.Windows.Forms.Label label141;
		private System.Windows.Forms.Label label142;
		public System.Windows.Forms.ComboBox objParticleSpinType;
		private System.Windows.Forms.Label label146;
		public System.Windows.Forms.CheckBox objUseRevolution;
		public System.Windows.Forms.CheckBox objUsePartSpin;
		public System.Windows.Forms.TextBox txtMaxSpinRange;
		public System.Windows.Forms.TextBox txtMinSpinRange;
		private System.Windows.Forms.Label label147;
		private System.Windows.Forms.Label label148;
		private System.Windows.Forms.Label label149;
		private System.Windows.Forms.Label label150;
		public System.Windows.Forms.TextBox txtMinRevVelZ;
		public System.Windows.Forms.TextBox txtMinRevVelY;
		public System.Windows.Forms.TextBox txtMinRevVelX;
		public System.Windows.Forms.TextBox txtMaxRevVelZ;
		private System.Windows.Forms.Label label143;
		public System.Windows.Forms.TextBox txtMaxRevVelY;
		private System.Windows.Forms.Label label144;
		public System.Windows.Forms.TextBox txtMaxRevVelX;
		private System.Windows.Forms.Label label145;
		private System.Windows.Forms.Label label151;
		private System.Windows.Forms.Label label152;
		private System.Windows.Forms.TabPage tabPage9;
		public System.Windows.Forms.ComboBox objPEType;
		private System.Windows.Forms.Label label153;
		private System.Windows.Forms.TabPage tabPage10;
		private System.Windows.Forms.GroupBox groupBox21;
		private System.Windows.Forms.GroupBox groupBox22;
		private System.Windows.Forms.Label label154;
		private System.Windows.Forms.Label label156;
		private System.Windows.Forms.Label label157;
		private System.Windows.Forms.Label label158;
		private System.Windows.Forms.Label label159;
		private System.Windows.Forms.Label label160;
		private System.Windows.Forms.Label label161;
		private System.Windows.Forms.Label label162;
		private System.Windows.Forms.Label label163;
		private System.Windows.Forms.Label label164;
		private System.Windows.Forms.Label label165;
		private System.Windows.Forms.Label label166;
		private System.Windows.Forms.Label label167;
		private System.Windows.Forms.Label label168;
		private System.Windows.Forms.Label label169;
		private System.Windows.Forms.Label label170;
		private System.Windows.Forms.Label label171;
		public System.Windows.Forms.TextBox txtMinLFNoiseX;
		public System.Windows.Forms.TextBox txtMinLFNoiseZ;
		public System.Windows.Forms.TextBox txtMinLFNoiseY;
		private System.Windows.Forms.Label label155;
		public System.Windows.Forms.TextBox txtMaxLFNoiseY;
		public System.Windows.Forms.TextBox txtMaxLFNoiseZ;
		public System.Windows.Forms.TextBox txtMaxLFNoiseX;
		private System.Windows.Forms.GroupBox groupBox23;
		public System.Windows.Forms.CheckBox objUseBeamNoise;
		public System.Windows.Forms.CheckBox objApplyNoiseToStart;
		public System.Windows.Forms.CheckBox objApplyNoiseToEnd;
		public System.Windows.Forms.TextBox txtHighFreqPoints;
		public System.Windows.Forms.TextBox txtLowFreqPoints;
		public System.Windows.Forms.TextBox txtMinHFNoiseZ;
		public System.Windows.Forms.TextBox txtMinHFNoiseY;
		public System.Windows.Forms.TextBox txtMinHFNoiseX;
		public System.Windows.Forms.TextBox txtMaxHFNoiseZ;
		public System.Windows.Forms.TextBox txtMaxHFNoiseY;
		public System.Windows.Forms.TextBox txtMaxHFNoiseX;
		private System.Windows.Forms.Label label172;
		public System.Windows.Forms.ComboBox objSubDivType;
		
		public Form1()
		{
			mPS = new ParticleSystem(this);
			
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
            
			mPS.Reset();
			mPS.mCurrentEmitter.CopyToGui();

			mSaveFileDialog = new SaveFileDialog();
			mSaveFileDialog.InitialDirectory = (string)Directory.GetCurrentDirectory().Clone();
			mSaveFileDialog.RestoreDirectory = true;

			mMaterialFileDialog = new OpenFileDialog();
			mMaterialFileDialog.InitialDirectory = (string)Directory.GetCurrentDirectory().Clone();
			mMaterialFileDialog.RestoreDirectory = true;

			mOpenFileDialog = new OpenFileDialog();
			mOpenFileDialog.InitialDirectory = (string)Directory.GetCurrentDirectory().Clone();
			mOpenFileDialog.RestoreDirectory = true;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label153 = new System.Windows.Forms.Label();
			this.objPEType = new System.Windows.Forms.ComboBox();
			this.txtWarmUpStepsPerSec = new System.Windows.Forms.TextBox();
			this.label130 = new System.Windows.Forms.Label();
			this.txtWarmUpTime = new System.Windows.Forms.TextBox();
			this.label129 = new System.Windows.Forms.Label();
			this.txtName = new System.Windows.Forms.TextBox();
			this.label19 = new System.Windows.Forms.Label();
			this.txtMaxParticles = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.txtParticlesPerSec = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.txtStartTimeOffset = new System.Windows.Forms.TextBox();
			this.objRespawn = new System.Windows.Forms.CheckBox();
			this.Pause = new System.Windows.Forms.GroupBox();
			this.label7 = new System.Windows.Forms.Label();
			this.txtMinPauseLength = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.txtMinPauseInterval = new System.Windows.Forms.TextBox();
			this.txtMaxPauseInterval = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.txtMaxPauseLength = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.txtAngleOffsetX = new System.Windows.Forms.TextBox();
			this.txtPosOffsetX = new System.Windows.Forms.TextBox();
			this.txtPosOffsetY = new System.Windows.Forms.TextBox();
			this.txtAngleOffsetY = new System.Windows.Forms.TextBox();
			this.txtAngleOffsetZ = new System.Windows.Forms.TextBox();
			this.txtPosOffsetZ = new System.Windows.Forms.TextBox();
			this.label12 = new System.Windows.Forms.Label();
			this.label18 = new System.Windows.Forms.Label();
			this.label17 = new System.Windows.Forms.Label();
			this.label16 = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.tabPage8 = new System.Windows.Forms.TabPage();
			this.txtBounceAmount = new System.Windows.Forms.GroupBox();
			this.txtCollisionUpdateRate = new System.Windows.Forms.TextBox();
			this.label135 = new System.Windows.Forms.Label();
			this.txtMaxBounceAmount = new System.Windows.Forms.TextBox();
			this.label133 = new System.Windows.Forms.Label();
			this.label134 = new System.Windows.Forms.Label();
			this.txtMinBounceAmount = new System.Windows.Forms.TextBox();
			this.txtMaxCollisionMax = new System.Windows.Forms.TextBox();
			this.label132 = new System.Windows.Forms.Label();
			this.label131 = new System.Windows.Forms.Label();
			this.txtMinCollisionMax = new System.Windows.Forms.TextBox();
			this.objCollides = new System.Windows.Forms.CheckBox();
			this.label128 = new System.Windows.Forms.Label();
			this.label127 = new System.Windows.Forms.Label();
			this.tabPage10 = new System.Windows.Forms.TabPage();
			this.groupBox23 = new System.Windows.Forms.GroupBox();
			this.groupBox22 = new System.Windows.Forms.GroupBox();
			this.objApplyNoiseToEnd = new System.Windows.Forms.CheckBox();
			this.objApplyNoiseToStart = new System.Windows.Forms.CheckBox();
			this.label165 = new System.Windows.Forms.Label();
			this.label166 = new System.Windows.Forms.Label();
			this.label167 = new System.Windows.Forms.Label();
			this.txtMaxLFNoiseY = new System.Windows.Forms.TextBox();
			this.txtMaxLFNoiseZ = new System.Windows.Forms.TextBox();
			this.txtMaxLFNoiseX = new System.Windows.Forms.TextBox();
			this.label162 = new System.Windows.Forms.Label();
			this.label163 = new System.Windows.Forms.Label();
			this.label164 = new System.Windows.Forms.Label();
			this.txtMinHFNoiseZ = new System.Windows.Forms.TextBox();
			this.txtMinHFNoiseY = new System.Windows.Forms.TextBox();
			this.txtMinHFNoiseX = new System.Windows.Forms.TextBox();
			this.label161 = new System.Windows.Forms.Label();
			this.label160 = new System.Windows.Forms.Label();
			this.label159 = new System.Windows.Forms.Label();
			this.label158 = new System.Windows.Forms.Label();
			this.label157 = new System.Windows.Forms.Label();
			this.label156 = new System.Windows.Forms.Label();
			this.txtMaxHFNoiseZ = new System.Windows.Forms.TextBox();
			this.txtMinLFNoiseY = new System.Windows.Forms.TextBox();
			this.txtMaxHFNoiseY = new System.Windows.Forms.TextBox();
			this.txtMinLFNoiseZ = new System.Windows.Forms.TextBox();
			this.txtMinLFNoiseX = new System.Windows.Forms.TextBox();
			this.txtMaxHFNoiseX = new System.Windows.Forms.TextBox();
			this.txtHighFreqPoints = new System.Windows.Forms.TextBox();
			this.txtLowFreqPoints = new System.Windows.Forms.TextBox();
			this.objUseBeamNoise = new System.Windows.Forms.CheckBox();
			this.label168 = new System.Windows.Forms.Label();
			this.label169 = new System.Windows.Forms.Label();
			this.label170 = new System.Windows.Forms.Label();
			this.label171 = new System.Windows.Forms.Label();
			this.label154 = new System.Windows.Forms.Label();
			this.label155 = new System.Windows.Forms.Label();
			this.groupBox21 = new System.Windows.Forms.GroupBox();
			this.tabPage9 = new System.Windows.Forms.TabPage();
			this.groupBox20 = new System.Windows.Forms.GroupBox();
			this.label152 = new System.Windows.Forms.Label();
			this.label151 = new System.Windows.Forms.Label();
			this.txtMaxRevVelZ = new System.Windows.Forms.TextBox();
			this.label143 = new System.Windows.Forms.Label();
			this.txtMaxRevVelY = new System.Windows.Forms.TextBox();
			this.label144 = new System.Windows.Forms.Label();
			this.txtMaxRevVelX = new System.Windows.Forms.TextBox();
			this.label145 = new System.Windows.Forms.Label();
			this.label150 = new System.Windows.Forms.Label();
			this.txtMinRevVelZ = new System.Windows.Forms.TextBox();
			this.label147 = new System.Windows.Forms.Label();
			this.txtMinRevVelY = new System.Windows.Forms.TextBox();
			this.label148 = new System.Windows.Forms.Label();
			this.txtMinRevVelX = new System.Windows.Forms.TextBox();
			this.label149 = new System.Windows.Forms.Label();
			this.objUseRevolution = new System.Windows.Forms.CheckBox();
			this.objUsePartSpin = new System.Windows.Forms.CheckBox();
			this.label146 = new System.Windows.Forms.Label();
			this.objParticleSpinType = new System.Windows.Forms.ComboBox();
			this.label142 = new System.Windows.Forms.Label();
			this.txtMaxSpinRange = new System.Windows.Forms.TextBox();
			this.txtMinSpinRange = new System.Windows.Forms.TextBox();
			this.label140 = new System.Windows.Forms.Label();
			this.label141 = new System.Windows.Forms.Label();
			this.tabPage6 = new System.Windows.Forms.TabPage();
			this.groupBox17 = new System.Windows.Forms.GroupBox();
			this.txtEndRelSize = new System.Windows.Forms.TextBox();
			this.txtStartRelSize = new System.Windows.Forms.TextBox();
			this.label96 = new System.Windows.Forms.Label();
			this.label98 = new System.Windows.Forms.Label();
			this.label97 = new System.Windows.Forms.Label();
			this.txtMiddleRelSize = new System.Windows.Forms.TextBox();
			this.label99 = new System.Windows.Forms.Label();
			this.label95 = new System.Windows.Forms.Label();
			this.txtMiddleRelSizeLength = new System.Windows.Forms.TextBox();
			this.txtMiddleRelSizeTime = new System.Windows.Forms.TextBox();
			this.groupBox16 = new System.Windows.Forms.GroupBox();
			this.label92 = new System.Windows.Forms.Label();
			this.txtMaxStartSizeY = new System.Windows.Forms.TextBox();
			this.label93 = new System.Windows.Forms.Label();
			this.txtMaxStartSizeX = new System.Windows.Forms.TextBox();
			this.label94 = new System.Windows.Forms.Label();
			this.label91 = new System.Windows.Forms.Label();
			this.txtMinStartSizeY = new System.Windows.Forms.TextBox();
			this.label90 = new System.Windows.Forms.Label();
			this.txtMinStartSizeX = new System.Windows.Forms.TextBox();
			this.label89 = new System.Windows.Forms.Label();
			this.groupBox15 = new System.Windows.Forms.GroupBox();
			this.objMultiplyRGBWithAlpha = new System.Windows.Forms.CheckBox();
			this.objDrawType = new System.Windows.Forms.ComboBox();
			this.label88 = new System.Windows.Forms.Label();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.groupBox7 = new System.Windows.Forms.GroupBox();
			this.txtMaxStartRadius = new System.Windows.Forms.TextBox();
			this.label45 = new System.Windows.Forms.Label();
			this.label44 = new System.Windows.Forms.Label();
			this.label43 = new System.Windows.Forms.Label();
			this.txtMinStartRadius = new System.Windows.Forms.TextBox();
			this.txtMaxStartAnglesX = new System.Windows.Forms.TextBox();
			this.txtMaxStartAnglesY = new System.Windows.Forms.TextBox();
			this.label40 = new System.Windows.Forms.Label();
			this.label41 = new System.Windows.Forms.Label();
			this.txtMinStartAnglesX = new System.Windows.Forms.TextBox();
			this.txtMinStartAnglesY = new System.Windows.Forms.TextBox();
			this.label39 = new System.Windows.Forms.Label();
			this.label38 = new System.Windows.Forms.Label();
			this.label37 = new System.Windows.Forms.Label();
			this.label42 = new System.Windows.Forms.Label();
			this.groupBox6 = new System.Windows.Forms.GroupBox();
			this.txtMaxStartPosZ = new System.Windows.Forms.TextBox();
			this.label33 = new System.Windows.Forms.Label();
			this.txtMaxStartPosY = new System.Windows.Forms.TextBox();
			this.label34 = new System.Windows.Forms.Label();
			this.txtMaxStartPosX = new System.Windows.Forms.TextBox();
			this.label35 = new System.Windows.Forms.Label();
			this.label36 = new System.Windows.Forms.Label();
			this.txtMinStartPosZ = new System.Windows.Forms.TextBox();
			this.label32 = new System.Windows.Forms.Label();
			this.txtMinStartPosY = new System.Windows.Forms.TextBox();
			this.label31 = new System.Windows.Forms.Label();
			this.txtMinStartPosX = new System.Windows.Forms.TextBox();
			this.label30 = new System.Windows.Forms.Label();
			this.label29 = new System.Windows.Forms.Label();
			this.groupBox5 = new System.Windows.Forms.GroupBox();
			this.objStartPosType = new System.Windows.Forms.ComboBox();
			this.label28 = new System.Windows.Forms.Label();
			this.tabPage7 = new System.Windows.Forms.TabPage();
			this.groupBox19 = new System.Windows.Forms.GroupBox();
			this.txtMiddleRelColorLength = new System.Windows.Forms.TextBox();
			this.txtMiddleRelColorTime = new System.Windows.Forms.TextBox();
			this.label126 = new System.Windows.Forms.Label();
			this.label125 = new System.Windows.Forms.Label();
			this.txtEndRelColorR = new System.Windows.Forms.TextBox();
			this.label120 = new System.Windows.Forms.Label();
			this.txtEndRelColorA = new System.Windows.Forms.TextBox();
			this.txtEndRelColorB = new System.Windows.Forms.TextBox();
			this.txtEndRelColorG = new System.Windows.Forms.TextBox();
			this.label121 = new System.Windows.Forms.Label();
			this.label122 = new System.Windows.Forms.Label();
			this.label123 = new System.Windows.Forms.Label();
			this.label124 = new System.Windows.Forms.Label();
			this.txtMiddleRelColorR = new System.Windows.Forms.TextBox();
			this.label119 = new System.Windows.Forms.Label();
			this.txtMiddleRelColorA = new System.Windows.Forms.TextBox();
			this.txtMiddleRelColorB = new System.Windows.Forms.TextBox();
			this.txtMiddleRelColorG = new System.Windows.Forms.TextBox();
			this.label115 = new System.Windows.Forms.Label();
			this.label116 = new System.Windows.Forms.Label();
			this.label117 = new System.Windows.Forms.Label();
			this.label118 = new System.Windows.Forms.Label();
			this.txtStartRelColorA = new System.Windows.Forms.TextBox();
			this.txtStartRelColorB = new System.Windows.Forms.TextBox();
			this.txtStartRelColorG = new System.Windows.Forms.TextBox();
			this.txtStartRelColorR = new System.Windows.Forms.TextBox();
			this.label112 = new System.Windows.Forms.Label();
			this.label111 = new System.Windows.Forms.Label();
			this.label110 = new System.Windows.Forms.Label();
			this.label114 = new System.Windows.Forms.Label();
			this.label113 = new System.Windows.Forms.Label();
			this.groupBox18 = new System.Windows.Forms.GroupBox();
			this.txtMaxStartColorA = new System.Windows.Forms.TextBox();
			this.label106 = new System.Windows.Forms.Label();
			this.txtMaxStartColorB = new System.Windows.Forms.TextBox();
			this.label107 = new System.Windows.Forms.Label();
			this.txtMaxStartColorG = new System.Windows.Forms.TextBox();
			this.label108 = new System.Windows.Forms.Label();
			this.txtMaxStartColorR = new System.Windows.Forms.TextBox();
			this.label109 = new System.Windows.Forms.Label();
			this.txtMinStartColorA = new System.Windows.Forms.TextBox();
			this.label105 = new System.Windows.Forms.Label();
			this.txtMinStartColorB = new System.Windows.Forms.TextBox();
			this.label104 = new System.Windows.Forms.Label();
			this.txtMinStartColorG = new System.Windows.Forms.TextBox();
			this.label103 = new System.Windows.Forms.Label();
			this.txtMinStartColorR = new System.Windows.Forms.TextBox();
			this.label101 = new System.Windows.Forms.Label();
			this.label100 = new System.Windows.Forms.Label();
			this.label102 = new System.Windows.Forms.Label();
			this.tabPage4 = new System.Windows.Forms.TabPage();
			this.groupBox11 = new System.Windows.Forms.GroupBox();
			this.txtGravityAccZ = new System.Windows.Forms.TextBox();
			this.txtGravityAccY = new System.Windows.Forms.TextBox();
			this.txtGravityAccX = new System.Windows.Forms.TextBox();
			this.label68 = new System.Windows.Forms.Label();
			this.label67 = new System.Windows.Forms.Label();
			this.label66 = new System.Windows.Forms.Label();
			this.label65 = new System.Windows.Forms.Label();
			this.objGravityType = new System.Windows.Forms.ComboBox();
			this.label64 = new System.Windows.Forms.Label();
			this.groupBox8 = new System.Windows.Forms.GroupBox();
			this.label82 = new System.Windows.Forms.Label();
			this.objCoordSystem = new System.Windows.Forms.ComboBox();
			this.objUsesDirection = new System.Windows.Forms.CheckBox();
			this.objStartVelType = new System.Windows.Forms.ComboBox();
			this.label46 = new System.Windows.Forms.Label();
			this.groupBox9 = new System.Windows.Forms.GroupBox();
			this.txtMaxStartVelZ = new System.Windows.Forms.TextBox();
			this.label47 = new System.Windows.Forms.Label();
			this.txtMaxStartVelY = new System.Windows.Forms.TextBox();
			this.label48 = new System.Windows.Forms.Label();
			this.txtMaxStartVelX = new System.Windows.Forms.TextBox();
			this.label49 = new System.Windows.Forms.Label();
			this.label50 = new System.Windows.Forms.Label();
			this.txtMinStartVelZ = new System.Windows.Forms.TextBox();
			this.label51 = new System.Windows.Forms.Label();
			this.txtMinStartVelY = new System.Windows.Forms.TextBox();
			this.label52 = new System.Windows.Forms.Label();
			this.txtMinStartVelX = new System.Windows.Forms.TextBox();
			this.label53 = new System.Windows.Forms.Label();
			this.label54 = new System.Windows.Forms.Label();
			this.groupBox10 = new System.Windows.Forms.GroupBox();
			this.txtMaxStartVelSpeed = new System.Windows.Forms.TextBox();
			this.label55 = new System.Windows.Forms.Label();
			this.label56 = new System.Windows.Forms.Label();
			this.label57 = new System.Windows.Forms.Label();
			this.txtMinStartVelSpeed = new System.Windows.Forms.TextBox();
			this.txtMaxStartVelAnglesX = new System.Windows.Forms.TextBox();
			this.txtMaxStartVelAnglesY = new System.Windows.Forms.TextBox();
			this.label58 = new System.Windows.Forms.Label();
			this.label59 = new System.Windows.Forms.Label();
			this.txtMinStartVelAnglesX = new System.Windows.Forms.TextBox();
			this.txtMinStartVelAnglesY = new System.Windows.Forms.TextBox();
			this.label60 = new System.Windows.Forms.Label();
			this.label61 = new System.Windows.Forms.Label();
			this.label62 = new System.Windows.Forms.Label();
			this.label63 = new System.Windows.Forms.Label();
			this.groupBox12 = new System.Windows.Forms.GroupBox();
			this.txtMaxSpeedMultiply = new System.Windows.Forms.TextBox();
			this.txtMinSpeedMultiply = new System.Windows.Forms.TextBox();
			this.label76 = new System.Windows.Forms.Label();
			this.txtMaxStartAccZ = new System.Windows.Forms.TextBox();
			this.label72 = new System.Windows.Forms.Label();
			this.txtMaxStartAccY = new System.Windows.Forms.TextBox();
			this.label73 = new System.Windows.Forms.Label();
			this.txtMaxStartAccX = new System.Windows.Forms.TextBox();
			this.label74 = new System.Windows.Forms.Label();
			this.label75 = new System.Windows.Forms.Label();
			this.txtMinStartAccZ = new System.Windows.Forms.TextBox();
			this.lbiasas = new System.Windows.Forms.Label();
			this.txtMinStartAccY = new System.Windows.Forms.TextBox();
			this.label77 = new System.Windows.Forms.Label();
			this.txtMinStartAccX = new System.Windows.Forms.TextBox();
			this.label78 = new System.Windows.Forms.Label();
			this.label79 = new System.Windows.Forms.Label();
			this.label80 = new System.Windows.Forms.Label();
			this.label81 = new System.Windows.Forms.Label();
			this.label69 = new System.Windows.Forms.Label();
			this.txtMinVelMaximum = new System.Windows.Forms.TextBox();
			this.txtMaxVelMaximum = new System.Windows.Forms.TextBox();
			this.label70 = new System.Windows.Forms.Label();
			this.label71 = new System.Windows.Forms.Label();
			this.tabPage5 = new System.Windows.Forms.TabPage();
			this.groupBox14 = new System.Windows.Forms.GroupBox();
			this.label87 = new System.Windows.Forms.Label();
			this.objDeathType = new System.Windows.Forms.ComboBox();
			this.label86 = new System.Windows.Forms.Label();
			this.txtDeathPS = new System.Windows.Forms.TextBox();
			this.groupBox13 = new System.Windows.Forms.GroupBox();
			this.txtMaxLifeSpan = new System.Windows.Forms.TextBox();
			this.label85 = new System.Windows.Forms.Label();
			this.txtMinLifeSpan = new System.Windows.Forms.TextBox();
			this.label84 = new System.Windows.Forms.Label();
			this.label83 = new System.Windows.Forms.Label();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.objSubDivType = new System.Windows.Forms.ComboBox();
			this.label27 = new System.Windows.Forms.Label();
			this.txtSubDivY = new System.Windows.Forms.TextBox();
			this.label26 = new System.Windows.Forms.Label();
			this.txtSubDivX = new System.Windows.Forms.TextBox();
			this.label25 = new System.Windows.Forms.Label();
			this.label24 = new System.Windows.Forms.Label();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.label23 = new System.Windows.Forms.Label();
			this.txtAnimationLength = new System.Windows.Forms.TextBox();
			this.txtMaterialNum = new System.Windows.Forms.TextBox();
			this.label22 = new System.Windows.Forms.Label();
			this.objBrowseMaterial = new System.Windows.Forms.Button();
			this.txtMaterial = new System.Windows.Forms.TextBox();
			this.label21 = new System.Windows.Forms.Label();
			this.objEmitterList = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.objAddEmitter = new System.Windows.Forms.Button();
			this.objRemoveEmitter = new System.Windows.Forms.Button();
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.menuItem6 = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.menuItem7 = new System.Windows.Forms.MenuItem();
			this.menuItem5 = new System.Windows.Forms.MenuItem();
			this.label20 = new System.Windows.Forms.Label();
			this.txtFileName = new System.Windows.Forms.TextBox();
			this.objView = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.objShowRoom = new System.Windows.Forms.CheckBox();
			this.label136 = new System.Windows.Forms.Label();
			this.txtRoomSizeX = new System.Windows.Forms.TextBox();
			this.txtRoomSizeY = new System.Windows.Forms.TextBox();
			this.txtRoomSizeZ = new System.Windows.Forms.TextBox();
			this.label137 = new System.Windows.Forms.Label();
			this.label138 = new System.Windows.Forms.Label();
			this.label139 = new System.Windows.Forms.Label();
			this.label172 = new System.Windows.Forms.Label();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.Pause.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.tabPage8.SuspendLayout();
			this.txtBounceAmount.SuspendLayout();
			this.tabPage10.SuspendLayout();
			this.groupBox22.SuspendLayout();
			this.tabPage9.SuspendLayout();
			this.groupBox20.SuspendLayout();
			this.tabPage6.SuspendLayout();
			this.groupBox17.SuspendLayout();
			this.groupBox16.SuspendLayout();
			this.groupBox15.SuspendLayout();
			this.tabPage3.SuspendLayout();
			this.groupBox7.SuspendLayout();
			this.groupBox6.SuspendLayout();
			this.groupBox5.SuspendLayout();
			this.tabPage7.SuspendLayout();
			this.groupBox19.SuspendLayout();
			this.groupBox18.SuspendLayout();
			this.tabPage4.SuspendLayout();
			this.groupBox11.SuspendLayout();
			this.groupBox8.SuspendLayout();
			this.groupBox9.SuspendLayout();
			this.groupBox10.SuspendLayout();
			this.groupBox12.SuspendLayout();
			this.tabPage5.SuspendLayout();
			this.groupBox14.SuspendLayout();
			this.groupBox13.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage6);
			this.tabControl1.Controls.Add(this.tabPage8);
			this.tabControl1.Controls.Add(this.tabPage10);
			this.tabControl1.Controls.Add(this.tabPage9);
			this.tabControl1.Controls.Add(this.tabPage3);
			this.tabControl1.Controls.Add(this.tabPage7);
			this.tabControl1.Controls.Add(this.tabPage4);
			this.tabControl1.Controls.Add(this.tabPage5);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Location = new System.Drawing.Point(8, 128);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(464, 384);
			this.tabControl1.TabIndex = 0;
			this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.groupBox1);
			this.tabPage1.Controls.Add(this.Pause);
			this.tabPage1.Controls.Add(this.groupBox2);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Size = new System.Drawing.Size(456, 358);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "General";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label153);
			this.groupBox1.Controls.Add(this.objPEType);
			this.groupBox1.Controls.Add(this.txtWarmUpStepsPerSec);
			this.groupBox1.Controls.Add(this.label130);
			this.groupBox1.Controls.Add(this.txtWarmUpTime);
			this.groupBox1.Controls.Add(this.label129);
			this.groupBox1.Controls.Add(this.txtName);
			this.groupBox1.Controls.Add(this.label19);
			this.groupBox1.Controls.Add(this.txtMaxParticles);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.txtParticlesPerSec);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.txtStartTimeOffset);
			this.groupBox1.Controls.Add(this.objRespawn);
			this.groupBox1.Location = new System.Drawing.Point(8, 8);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(184, 344);
			this.groupBox1.TabIndex = 5;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "General";
			// 
			// label153
			// 
			this.label153.Location = new System.Drawing.Point(8, 56);
			this.label153.Name = "label153";
			this.label153.Size = new System.Drawing.Size(96, 16);
			this.label153.TabIndex = 38;
			this.label153.Text = "Type";
			// 
			// objPEType
			// 
			this.objPEType.Items.AddRange(new object[] {
														   "Normal",
														   "Beam"});
			this.objPEType.Location = new System.Drawing.Point(8, 72);
			this.objPEType.Name = "objPEType";
			this.objPEType.Size = new System.Drawing.Size(120, 21);
			this.objPEType.TabIndex = 37;
			this.objPEType.SelectedIndexChanged += new System.EventHandler(this.objPEType_SelectedIndexChanged);
			// 
			// txtWarmUpStepsPerSec
			// 
			this.txtWarmUpStepsPerSec.Location = new System.Drawing.Point(8, 304);
			this.txtWarmUpStepsPerSec.Name = "txtWarmUpStepsPerSec";
			this.txtWarmUpStepsPerSec.Size = new System.Drawing.Size(104, 20);
			this.txtWarmUpStepsPerSec.TabIndex = 36;
			this.txtWarmUpStepsPerSec.Text = "";
			this.txtWarmUpStepsPerSec.TextChanged += new System.EventHandler(this.txtWarmUpStepsPerSec_TextChanged);
			// 
			// label130
			// 
			this.label130.Location = new System.Drawing.Point(8, 288);
			this.label130.Name = "label130";
			this.label130.Size = new System.Drawing.Size(112, 16);
			this.label130.TabIndex = 35;
			this.label130.Text = "Warmup Steps / Sec";
			// 
			// txtWarmUpTime
			// 
			this.txtWarmUpTime.Location = new System.Drawing.Point(8, 264);
			this.txtWarmUpTime.Name = "txtWarmUpTime";
			this.txtWarmUpTime.Size = new System.Drawing.Size(104, 20);
			this.txtWarmUpTime.TabIndex = 34;
			this.txtWarmUpTime.Text = "";
			this.txtWarmUpTime.TextChanged += new System.EventHandler(this.txtWarmUpTime_TextChanged);
			// 
			// label129
			// 
			this.label129.Location = new System.Drawing.Point(8, 248);
			this.label129.Name = "label129";
			this.label129.Size = new System.Drawing.Size(104, 16);
			this.label129.TabIndex = 33;
			this.label129.Text = "Warmup Time (s)";
			// 
			// txtName
			// 
			this.txtName.Location = new System.Drawing.Point(8, 32);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(104, 20);
			this.txtName.TabIndex = 32;
			this.txtName.Text = "";
			this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
			// 
			// label19
			// 
			this.label19.Location = new System.Drawing.Point(8, 16);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(96, 16);
			this.label19.TabIndex = 31;
			this.label19.Text = "Name";
			// 
			// txtMaxParticles
			// 
			this.txtMaxParticles.Location = new System.Drawing.Point(8, 112);
			this.txtMaxParticles.Name = "txtMaxParticles";
			this.txtMaxParticles.Size = new System.Drawing.Size(104, 20);
			this.txtMaxParticles.TabIndex = 3;
			this.txtMaxParticles.Text = "";
			this.txtMaxParticles.TextChanged += new System.EventHandler(this.txtMaxParticles_TextChanged);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 96);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(96, 16);
			this.label2.TabIndex = 0;
			this.label2.Text = "Max Particles";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 136);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(96, 16);
			this.label3.TabIndex = 1;
			this.label3.Text = "Particles per sec";
			// 
			// txtParticlesPerSec
			// 
			this.txtParticlesPerSec.Location = new System.Drawing.Point(8, 152);
			this.txtParticlesPerSec.Name = "txtParticlesPerSec";
			this.txtParticlesPerSec.Size = new System.Drawing.Size(104, 20);
			this.txtParticlesPerSec.TabIndex = 4;
			this.txtParticlesPerSec.Text = "";
			this.txtParticlesPerSec.TextChanged += new System.EventHandler(this.txtParticlesPerSec_TextChanged);
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 176);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(104, 16);
			this.label4.TabIndex = 2;
			this.label4.Text = "Start time offset (s)";
			// 
			// txtStartTimeOffset
			// 
			this.txtStartTimeOffset.Location = new System.Drawing.Point(8, 192);
			this.txtStartTimeOffset.Name = "txtStartTimeOffset";
			this.txtStartTimeOffset.Size = new System.Drawing.Size(104, 20);
			this.txtStartTimeOffset.TabIndex = 5;
			this.txtStartTimeOffset.Text = "";
			this.txtStartTimeOffset.TextChanged += new System.EventHandler(this.txtStartTimeOffset_TextChanged);
			// 
			// objRespawn
			// 
			this.objRespawn.Location = new System.Drawing.Point(8, 216);
			this.objRespawn.Name = "objRespawn";
			this.objRespawn.Size = new System.Drawing.Size(152, 24);
			this.objRespawn.TabIndex = 30;
			this.objRespawn.Text = "Respawn Dead Particles";
			this.objRespawn.CheckedChanged += new System.EventHandler(this.objRespawn_CheckedChanged);
			// 
			// Pause
			// 
			this.Pause.Controls.Add(this.label7);
			this.Pause.Controls.Add(this.txtMinPauseLength);
			this.Pause.Controls.Add(this.label5);
			this.Pause.Controls.Add(this.label10);
			this.Pause.Controls.Add(this.txtMinPauseInterval);
			this.Pause.Controls.Add(this.txtMaxPauseInterval);
			this.Pause.Controls.Add(this.label8);
			this.Pause.Controls.Add(this.label9);
			this.Pause.Controls.Add(this.txtMaxPauseLength);
			this.Pause.Controls.Add(this.label6);
			this.Pause.Location = new System.Drawing.Point(200, 8);
			this.Pause.Name = "Pause";
			this.Pause.Size = new System.Drawing.Size(248, 120);
			this.Pause.TabIndex = 5;
			this.Pause.TabStop = false;
			this.Pause.Text = "Pause";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(112, 40);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(32, 16);
			this.label7.TabIndex = 10;
			this.label7.Text = "Max:";
			// 
			// txtMinPauseLength
			// 
			this.txtMinPauseLength.Location = new System.Drawing.Point(40, 40);
			this.txtMinPauseLength.Name = "txtMinPauseLength";
			this.txtMinPauseLength.Size = new System.Drawing.Size(64, 20);
			this.txtMinPauseLength.TabIndex = 7;
			this.txtMinPauseLength.Text = "";
			this.txtMinPauseLength.TextChanged += new System.EventHandler(this.txtMinPauseLength_TextChanged);
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(8, 24);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(96, 16);
			this.label5.TabIndex = 6;
			this.label5.Text = "Length (s)";
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(8, 72);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(96, 16);
			this.label10.TabIndex = 11;
			this.label10.Text = "Interval (s)";
			// 
			// txtMinPauseInterval
			// 
			this.txtMinPauseInterval.Location = new System.Drawing.Point(40, 88);
			this.txtMinPauseInterval.Name = "txtMinPauseInterval";
			this.txtMinPauseInterval.Size = new System.Drawing.Size(64, 20);
			this.txtMinPauseInterval.TabIndex = 12;
			this.txtMinPauseInterval.Text = "";
			this.txtMinPauseInterval.TextChanged += new System.EventHandler(this.txtMinPauseInterval_TextChanged);
			// 
			// txtMaxPauseInterval
			// 
			this.txtMaxPauseInterval.Location = new System.Drawing.Point(144, 88);
			this.txtMaxPauseInterval.Name = "txtMaxPauseInterval";
			this.txtMaxPauseInterval.Size = new System.Drawing.Size(72, 20);
			this.txtMaxPauseInterval.TabIndex = 13;
			this.txtMaxPauseInterval.Text = "";
			this.txtMaxPauseInterval.TextChanged += new System.EventHandler(this.txtMaxPauseInterval_TextChanged);
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(112, 88);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(32, 16);
			this.label8.TabIndex = 15;
			this.label8.Text = "Max:";
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(8, 88);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(32, 16);
			this.label9.TabIndex = 14;
			this.label9.Text = "Min:";
			// 
			// txtMaxPauseLength
			// 
			this.txtMaxPauseLength.Location = new System.Drawing.Point(144, 40);
			this.txtMaxPauseLength.Name = "txtMaxPauseLength";
			this.txtMaxPauseLength.Size = new System.Drawing.Size(72, 20);
			this.txtMaxPauseLength.TabIndex = 8;
			this.txtMaxPauseLength.Text = "";
			this.txtMaxPauseLength.TextChanged += new System.EventHandler(this.txtMaxPauseLength_TextChanged);
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(8, 40);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(32, 16);
			this.label6.TabIndex = 9;
			this.label6.Text = "Min:";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.txtAngleOffsetX);
			this.groupBox2.Controls.Add(this.txtPosOffsetX);
			this.groupBox2.Controls.Add(this.txtPosOffsetY);
			this.groupBox2.Controls.Add(this.txtAngleOffsetY);
			this.groupBox2.Controls.Add(this.txtAngleOffsetZ);
			this.groupBox2.Controls.Add(this.txtPosOffsetZ);
			this.groupBox2.Controls.Add(this.label12);
			this.groupBox2.Controls.Add(this.label18);
			this.groupBox2.Controls.Add(this.label17);
			this.groupBox2.Controls.Add(this.label16);
			this.groupBox2.Controls.Add(this.label15);
			this.groupBox2.Controls.Add(this.label11);
			this.groupBox2.Controls.Add(this.label14);
			this.groupBox2.Controls.Add(this.label13);
			this.groupBox2.Location = new System.Drawing.Point(200, 128);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(248, 128);
			this.groupBox2.TabIndex = 5;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Offset";
			// 
			// txtAngleOffsetX
			// 
			this.txtAngleOffsetX.Location = new System.Drawing.Point(24, 96);
			this.txtAngleOffsetX.Name = "txtAngleOffsetX";
			this.txtAngleOffsetX.Size = new System.Drawing.Size(48, 20);
			this.txtAngleOffsetX.TabIndex = 24;
			this.txtAngleOffsetX.Text = "";
			this.txtAngleOffsetX.TextChanged += new System.EventHandler(this.txtAngleOffsetX_TextChanged);
			// 
			// txtPosOffsetX
			// 
			this.txtPosOffsetX.Location = new System.Drawing.Point(24, 40);
			this.txtPosOffsetX.Name = "txtPosOffsetX";
			this.txtPosOffsetX.Size = new System.Drawing.Size(48, 20);
			this.txtPosOffsetX.TabIndex = 17;
			this.txtPosOffsetX.Text = "";
			this.txtPosOffsetX.TextChanged += new System.EventHandler(this.txtPosOffsetX_TextChanged);
			// 
			// txtPosOffsetY
			// 
			this.txtPosOffsetY.Location = new System.Drawing.Point(96, 40);
			this.txtPosOffsetY.Name = "txtPosOffsetY";
			this.txtPosOffsetY.Size = new System.Drawing.Size(48, 20);
			this.txtPosOffsetY.TabIndex = 19;
			this.txtPosOffsetY.Text = "";
			this.txtPosOffsetY.TextChanged += new System.EventHandler(this.txtPosOffsetY_TextChanged);
			// 
			// txtAngleOffsetY
			// 
			this.txtAngleOffsetY.Location = new System.Drawing.Point(96, 96);
			this.txtAngleOffsetY.Name = "txtAngleOffsetY";
			this.txtAngleOffsetY.Size = new System.Drawing.Size(48, 20);
			this.txtAngleOffsetY.TabIndex = 26;
			this.txtAngleOffsetY.Text = "";
			this.txtAngleOffsetY.TextChanged += new System.EventHandler(this.txtAngleOffsetY_TextChanged);
			// 
			// txtAngleOffsetZ
			// 
			this.txtAngleOffsetZ.Location = new System.Drawing.Point(168, 96);
			this.txtAngleOffsetZ.Name = "txtAngleOffsetZ";
			this.txtAngleOffsetZ.Size = new System.Drawing.Size(48, 20);
			this.txtAngleOffsetZ.TabIndex = 28;
			this.txtAngleOffsetZ.Text = "";
			this.txtAngleOffsetZ.TextChanged += new System.EventHandler(this.txtAngleOffsetZ_TextChanged);
			// 
			// txtPosOffsetZ
			// 
			this.txtPosOffsetZ.Location = new System.Drawing.Point(168, 40);
			this.txtPosOffsetZ.Name = "txtPosOffsetZ";
			this.txtPosOffsetZ.Size = new System.Drawing.Size(48, 20);
			this.txtPosOffsetZ.TabIndex = 21;
			this.txtPosOffsetZ.Text = "";
			this.txtPosOffsetZ.TextChanged += new System.EventHandler(this.txtPosOffsetZ_TextChanged);
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(8, 40);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(32, 16);
			this.label12.TabIndex = 18;
			this.label12.Text = "X:";
			// 
			// label18
			// 
			this.label18.Location = new System.Drawing.Point(8, 80);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(96, 16);
			this.label18.TabIndex = 23;
			this.label18.Text = "Angles (deg)";
			// 
			// label17
			// 
			this.label17.Location = new System.Drawing.Point(8, 96);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(32, 16);
			this.label17.TabIndex = 25;
			this.label17.Text = "X:";
			// 
			// label16
			// 
			this.label16.Location = new System.Drawing.Point(80, 96);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(32, 16);
			this.label16.TabIndex = 27;
			this.label16.Text = "Y:";
			// 
			// label15
			// 
			this.label15.Location = new System.Drawing.Point(152, 96);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(32, 16);
			this.label15.TabIndex = 29;
			this.label15.Text = "Z:";
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(8, 24);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(96, 16);
			this.label11.TabIndex = 16;
			this.label11.Text = "Position (m)";
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(152, 40);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(32, 16);
			this.label14.TabIndex = 22;
			this.label14.Text = "Z:";
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(80, 40);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(32, 16);
			this.label13.TabIndex = 20;
			this.label13.Text = "Y:";
			// 
			// tabPage8
			// 
			this.tabPage8.Controls.Add(this.txtBounceAmount);
			this.tabPage8.Location = new System.Drawing.Point(4, 22);
			this.tabPage8.Name = "tabPage8";
			this.tabPage8.Size = new System.Drawing.Size(456, 358);
			this.tabPage8.TabIndex = 7;
			this.tabPage8.Text = "Collision";
			// 
			// txtBounceAmount
			// 
			this.txtBounceAmount.Controls.Add(this.txtCollisionUpdateRate);
			this.txtBounceAmount.Controls.Add(this.label135);
			this.txtBounceAmount.Controls.Add(this.txtMaxBounceAmount);
			this.txtBounceAmount.Controls.Add(this.label133);
			this.txtBounceAmount.Controls.Add(this.label134);
			this.txtBounceAmount.Controls.Add(this.txtMinBounceAmount);
			this.txtBounceAmount.Controls.Add(this.txtMaxCollisionMax);
			this.txtBounceAmount.Controls.Add(this.label132);
			this.txtBounceAmount.Controls.Add(this.label131);
			this.txtBounceAmount.Controls.Add(this.txtMinCollisionMax);
			this.txtBounceAmount.Controls.Add(this.objCollides);
			this.txtBounceAmount.Controls.Add(this.label128);
			this.txtBounceAmount.Controls.Add(this.label127);
			this.txtBounceAmount.Location = new System.Drawing.Point(8, 8);
			this.txtBounceAmount.Name = "txtBounceAmount";
			this.txtBounceAmount.Size = new System.Drawing.Size(440, 120);
			this.txtBounceAmount.TabIndex = 0;
			this.txtBounceAmount.TabStop = false;
			this.txtBounceAmount.Text = "General";
			// 
			// txtCollisionUpdateRate
			// 
			this.txtCollisionUpdateRate.Location = new System.Drawing.Point(240, 32);
			this.txtCollisionUpdateRate.Name = "txtCollisionUpdateRate";
			this.txtCollisionUpdateRate.Size = new System.Drawing.Size(56, 20);
			this.txtCollisionUpdateRate.TabIndex = 12;
			this.txtCollisionUpdateRate.Text = "";
			this.txtCollisionUpdateRate.TextChanged += new System.EventHandler(this.txtCollisionUpdateRate_TextChanged);
			// 
			// label135
			// 
			this.label135.Location = new System.Drawing.Point(240, 16);
			this.label135.Name = "label135";
			this.label135.Size = new System.Drawing.Size(88, 16);
			this.label135.TabIndex = 11;
			this.label135.Text = "Update Rate";
			// 
			// txtMaxBounceAmount
			// 
			this.txtMaxBounceAmount.Location = new System.Drawing.Point(352, 88);
			this.txtMaxBounceAmount.Name = "txtMaxBounceAmount";
			this.txtMaxBounceAmount.Size = new System.Drawing.Size(48, 20);
			this.txtMaxBounceAmount.TabIndex = 10;
			this.txtMaxBounceAmount.Text = "";
			this.txtMaxBounceAmount.TextChanged += new System.EventHandler(this.txtMaxBounceAmount_TextChanged);
			// 
			// label133
			// 
			this.label133.Location = new System.Drawing.Point(320, 88);
			this.label133.Name = "label133";
			this.label133.Size = new System.Drawing.Size(32, 16);
			this.label133.TabIndex = 9;
			this.label133.Text = "Max";
			// 
			// label134
			// 
			this.label134.Location = new System.Drawing.Point(240, 88);
			this.label134.Name = "label134";
			this.label134.Size = new System.Drawing.Size(24, 16);
			this.label134.TabIndex = 8;
			this.label134.Text = "Min";
			// 
			// txtMinBounceAmount
			// 
			this.txtMinBounceAmount.Location = new System.Drawing.Point(264, 88);
			this.txtMinBounceAmount.Name = "txtMinBounceAmount";
			this.txtMinBounceAmount.Size = new System.Drawing.Size(48, 20);
			this.txtMinBounceAmount.TabIndex = 7;
			this.txtMinBounceAmount.Text = "";
			this.txtMinBounceAmount.TextChanged += new System.EventHandler(this.txtMinBounceAmount_TextChanged);
			// 
			// txtMaxCollisionMax
			// 
			this.txtMaxCollisionMax.Location = new System.Drawing.Point(120, 88);
			this.txtMaxCollisionMax.Name = "txtMaxCollisionMax";
			this.txtMaxCollisionMax.Size = new System.Drawing.Size(48, 20);
			this.txtMaxCollisionMax.TabIndex = 6;
			this.txtMaxCollisionMax.Text = "";
			this.txtMaxCollisionMax.TextChanged += new System.EventHandler(this.txtMaxCollisionMax_TextChanged);
			// 
			// label132
			// 
			this.label132.Location = new System.Drawing.Point(88, 88);
			this.label132.Name = "label132";
			this.label132.Size = new System.Drawing.Size(32, 16);
			this.label132.TabIndex = 5;
			this.label132.Text = "Max";
			// 
			// label131
			// 
			this.label131.Location = new System.Drawing.Point(8, 88);
			this.label131.Name = "label131";
			this.label131.Size = new System.Drawing.Size(24, 16);
			this.label131.TabIndex = 4;
			this.label131.Text = "Min";
			// 
			// txtMinCollisionMax
			// 
			this.txtMinCollisionMax.Location = new System.Drawing.Point(32, 88);
			this.txtMinCollisionMax.Name = "txtMinCollisionMax";
			this.txtMinCollisionMax.Size = new System.Drawing.Size(48, 20);
			this.txtMinCollisionMax.TabIndex = 3;
			this.txtMinCollisionMax.Text = "";
			this.txtMinCollisionMax.TextChanged += new System.EventHandler(this.txtMinCollisionMax_TextChanged);
			// 
			// objCollides
			// 
			this.objCollides.Location = new System.Drawing.Point(8, 24);
			this.objCollides.Name = "objCollides";
			this.objCollides.Size = new System.Drawing.Size(152, 16);
			this.objCollides.TabIndex = 2;
			this.objCollides.Text = "Use Collisions";
			this.objCollides.CheckedChanged += new System.EventHandler(this.objCollides_CheckedChanged);
			// 
			// label128
			// 
			this.label128.Location = new System.Drawing.Point(240, 72);
			this.label128.Name = "label128";
			this.label128.Size = new System.Drawing.Size(88, 16);
			this.label128.TabIndex = 1;
			this.label128.Text = "Bounce Amount";
			// 
			// label127
			// 
			this.label127.Location = new System.Drawing.Point(8, 72);
			this.label127.Name = "label127";
			this.label127.Size = new System.Drawing.Size(136, 16);
			this.label127.TabIndex = 0;
			this.label127.Text = "Maximum Collisions";
			// 
			// tabPage10
			// 
			this.tabPage10.Controls.Add(this.groupBox23);
			this.tabPage10.Controls.Add(this.groupBox22);
			this.tabPage10.Controls.Add(this.groupBox21);
			this.tabPage10.Location = new System.Drawing.Point(4, 22);
			this.tabPage10.Name = "tabPage10";
			this.tabPage10.Size = new System.Drawing.Size(456, 358);
			this.tabPage10.TabIndex = 9;
			this.tabPage10.Text = "Beam Specific";
			// 
			// groupBox23
			// 
			this.groupBox23.Location = new System.Drawing.Point(224, 8);
			this.groupBox23.Name = "groupBox23";
			this.groupBox23.Size = new System.Drawing.Size(224, 80);
			this.groupBox23.TabIndex = 2;
			this.groupBox23.TabStop = false;
			this.groupBox23.Text = "Branching";
			// 
			// groupBox22
			// 
			this.groupBox22.Controls.Add(this.objApplyNoiseToEnd);
			this.groupBox22.Controls.Add(this.objApplyNoiseToStart);
			this.groupBox22.Controls.Add(this.label165);
			this.groupBox22.Controls.Add(this.label166);
			this.groupBox22.Controls.Add(this.label167);
			this.groupBox22.Controls.Add(this.txtMaxLFNoiseY);
			this.groupBox22.Controls.Add(this.txtMaxLFNoiseZ);
			this.groupBox22.Controls.Add(this.txtMaxLFNoiseX);
			this.groupBox22.Controls.Add(this.label162);
			this.groupBox22.Controls.Add(this.label163);
			this.groupBox22.Controls.Add(this.label164);
			this.groupBox22.Controls.Add(this.txtMinHFNoiseZ);
			this.groupBox22.Controls.Add(this.txtMinHFNoiseY);
			this.groupBox22.Controls.Add(this.txtMinHFNoiseX);
			this.groupBox22.Controls.Add(this.label161);
			this.groupBox22.Controls.Add(this.label160);
			this.groupBox22.Controls.Add(this.label159);
			this.groupBox22.Controls.Add(this.label158);
			this.groupBox22.Controls.Add(this.label157);
			this.groupBox22.Controls.Add(this.label156);
			this.groupBox22.Controls.Add(this.txtMaxHFNoiseZ);
			this.groupBox22.Controls.Add(this.txtMinLFNoiseY);
			this.groupBox22.Controls.Add(this.txtMaxHFNoiseY);
			this.groupBox22.Controls.Add(this.txtMinLFNoiseZ);
			this.groupBox22.Controls.Add(this.txtMinLFNoiseX);
			this.groupBox22.Controls.Add(this.txtMaxHFNoiseX);
			this.groupBox22.Controls.Add(this.txtHighFreqPoints);
			this.groupBox22.Controls.Add(this.txtLowFreqPoints);
			this.groupBox22.Controls.Add(this.objUseBeamNoise);
			this.groupBox22.Controls.Add(this.label168);
			this.groupBox22.Controls.Add(this.label169);
			this.groupBox22.Controls.Add(this.label170);
			this.groupBox22.Controls.Add(this.label171);
			this.groupBox22.Controls.Add(this.label154);
			this.groupBox22.Controls.Add(this.label155);
			this.groupBox22.Location = new System.Drawing.Point(8, 88);
			this.groupBox22.Name = "groupBox22";
			this.groupBox22.Size = new System.Drawing.Size(440, 264);
			this.groupBox22.TabIndex = 1;
			this.groupBox22.TabStop = false;
			this.groupBox22.Text = "Beam Noise";
			// 
			// objApplyNoiseToEnd
			// 
			this.objApplyNoiseToEnd.Location = new System.Drawing.Point(248, 64);
			this.objApplyNoiseToEnd.Name = "objApplyNoiseToEnd";
			this.objApplyNoiseToEnd.Size = new System.Drawing.Size(160, 16);
			this.objApplyNoiseToEnd.TabIndex = 34;
			this.objApplyNoiseToEnd.Text = "Apply Noise to End Point";
			this.objApplyNoiseToEnd.CheckedChanged += new System.EventHandler(this.objApplyNoiseToEnd_CheckedChanged);
			// 
			// objApplyNoiseToStart
			// 
			this.objApplyNoiseToStart.Location = new System.Drawing.Point(248, 40);
			this.objApplyNoiseToStart.Name = "objApplyNoiseToStart";
			this.objApplyNoiseToStart.Size = new System.Drawing.Size(160, 16);
			this.objApplyNoiseToStart.TabIndex = 33;
			this.objApplyNoiseToStart.Text = "Apply Noise to Start Point";
			this.objApplyNoiseToStart.CheckedChanged += new System.EventHandler(this.objApplyNoiseToStart_CheckedChanged);
			// 
			// label165
			// 
			this.label165.Location = new System.Drawing.Point(144, 120);
			this.label165.Name = "label165";
			this.label165.Size = new System.Drawing.Size(16, 24);
			this.label165.TabIndex = 28;
			this.label165.Text = "Z";
			// 
			// label166
			// 
			this.label166.Location = new System.Drawing.Point(80, 120);
			this.label166.Name = "label166";
			this.label166.Size = new System.Drawing.Size(16, 24);
			this.label166.TabIndex = 27;
			this.label166.Text = "Y";
			// 
			// label167
			// 
			this.label167.Location = new System.Drawing.Point(16, 120);
			this.label167.Name = "label167";
			this.label167.Size = new System.Drawing.Size(16, 24);
			this.label167.TabIndex = 26;
			this.label167.Text = "X";
			// 
			// txtMaxLFNoiseY
			// 
			this.txtMaxLFNoiseY.Location = new System.Drawing.Point(96, 120);
			this.txtMaxLFNoiseY.Name = "txtMaxLFNoiseY";
			this.txtMaxLFNoiseY.Size = new System.Drawing.Size(40, 20);
			this.txtMaxLFNoiseY.TabIndex = 25;
			this.txtMaxLFNoiseY.Text = "";
			this.txtMaxLFNoiseY.TextChanged += new System.EventHandler(this.txtMaxLFNoiseY_TextChanged);
			// 
			// txtMaxLFNoiseZ
			// 
			this.txtMaxLFNoiseZ.Location = new System.Drawing.Point(160, 120);
			this.txtMaxLFNoiseZ.Name = "txtMaxLFNoiseZ";
			this.txtMaxLFNoiseZ.Size = new System.Drawing.Size(40, 20);
			this.txtMaxLFNoiseZ.TabIndex = 24;
			this.txtMaxLFNoiseZ.Text = "";
			this.txtMaxLFNoiseZ.TextChanged += new System.EventHandler(this.txtMaxLFNoiseZ_TextChanged);
			// 
			// txtMaxLFNoiseX
			// 
			this.txtMaxLFNoiseX.Location = new System.Drawing.Point(32, 120);
			this.txtMaxLFNoiseX.Name = "txtMaxLFNoiseX";
			this.txtMaxLFNoiseX.Size = new System.Drawing.Size(40, 20);
			this.txtMaxLFNoiseX.TabIndex = 23;
			this.txtMaxLFNoiseX.Text = "";
			this.txtMaxLFNoiseX.TextChanged += new System.EventHandler(this.txtMaxLFNoiseX_TextChanged);
			// 
			// label162
			// 
			this.label162.Location = new System.Drawing.Point(144, 192);
			this.label162.Name = "label162";
			this.label162.Size = new System.Drawing.Size(16, 24);
			this.label162.TabIndex = 22;
			this.label162.Text = "Z";
			// 
			// label163
			// 
			this.label163.Location = new System.Drawing.Point(16, 192);
			this.label163.Name = "label163";
			this.label163.Size = new System.Drawing.Size(16, 24);
			this.label163.TabIndex = 21;
			this.label163.Text = "X";
			// 
			// label164
			// 
			this.label164.Location = new System.Drawing.Point(80, 192);
			this.label164.Name = "label164";
			this.label164.Size = new System.Drawing.Size(16, 24);
			this.label164.TabIndex = 20;
			this.label164.Text = "Y";
			// 
			// txtMinHFNoiseZ
			// 
			this.txtMinHFNoiseZ.Location = new System.Drawing.Point(160, 192);
			this.txtMinHFNoiseZ.Name = "txtMinHFNoiseZ";
			this.txtMinHFNoiseZ.Size = new System.Drawing.Size(40, 20);
			this.txtMinHFNoiseZ.TabIndex = 19;
			this.txtMinHFNoiseZ.Text = "";
			this.txtMinHFNoiseZ.TextChanged += new System.EventHandler(this.txtMinHFNoiseZ_TextChanged);
			// 
			// txtMinHFNoiseY
			// 
			this.txtMinHFNoiseY.Location = new System.Drawing.Point(96, 192);
			this.txtMinHFNoiseY.Name = "txtMinHFNoiseY";
			this.txtMinHFNoiseY.Size = new System.Drawing.Size(40, 20);
			this.txtMinHFNoiseY.TabIndex = 18;
			this.txtMinHFNoiseY.Text = "";
			this.txtMinHFNoiseY.TextChanged += new System.EventHandler(this.txtMinHFNoiseY_TextChanged);
			// 
			// txtMinHFNoiseX
			// 
			this.txtMinHFNoiseX.Location = new System.Drawing.Point(32, 192);
			this.txtMinHFNoiseX.Name = "txtMinHFNoiseX";
			this.txtMinHFNoiseX.Size = new System.Drawing.Size(40, 20);
			this.txtMinHFNoiseX.TabIndex = 17;
			this.txtMinHFNoiseX.Text = "";
			this.txtMinHFNoiseX.TextChanged += new System.EventHandler(this.txtMinHFNoiseX_TextChanged);
			// 
			// label161
			// 
			this.label161.Location = new System.Drawing.Point(144, 232);
			this.label161.Name = "label161";
			this.label161.Size = new System.Drawing.Size(16, 24);
			this.label161.TabIndex = 16;
			this.label161.Text = "Z";
			// 
			// label160
			// 
			this.label160.Location = new System.Drawing.Point(16, 232);
			this.label160.Name = "label160";
			this.label160.Size = new System.Drawing.Size(16, 24);
			this.label160.TabIndex = 15;
			this.label160.Text = "X";
			// 
			// label159
			// 
			this.label159.Location = new System.Drawing.Point(144, 80);
			this.label159.Name = "label159";
			this.label159.Size = new System.Drawing.Size(16, 24);
			this.label159.TabIndex = 14;
			this.label159.Text = "Z";
			// 
			// label158
			// 
			this.label158.Location = new System.Drawing.Point(80, 80);
			this.label158.Name = "label158";
			this.label158.Size = new System.Drawing.Size(16, 24);
			this.label158.TabIndex = 13;
			this.label158.Text = "Y";
			// 
			// label157
			// 
			this.label157.Location = new System.Drawing.Point(80, 232);
			this.label157.Name = "label157";
			this.label157.Size = new System.Drawing.Size(16, 24);
			this.label157.TabIndex = 12;
			this.label157.Text = "Y";
			// 
			// label156
			// 
			this.label156.Location = new System.Drawing.Point(16, 80);
			this.label156.Name = "label156";
			this.label156.Size = new System.Drawing.Size(16, 24);
			this.label156.TabIndex = 11;
			this.label156.Text = "X";
			// 
			// txtMaxHFNoiseZ
			// 
			this.txtMaxHFNoiseZ.Location = new System.Drawing.Point(160, 232);
			this.txtMaxHFNoiseZ.Name = "txtMaxHFNoiseZ";
			this.txtMaxHFNoiseZ.Size = new System.Drawing.Size(40, 20);
			this.txtMaxHFNoiseZ.TabIndex = 8;
			this.txtMaxHFNoiseZ.Text = "";
			this.txtMaxHFNoiseZ.TextChanged += new System.EventHandler(this.txtMaxHFNoiseZ_TextChanged);
			// 
			// txtMinLFNoiseY
			// 
			this.txtMinLFNoiseY.Location = new System.Drawing.Point(96, 80);
			this.txtMinLFNoiseY.Name = "txtMinLFNoiseY";
			this.txtMinLFNoiseY.Size = new System.Drawing.Size(40, 20);
			this.txtMinLFNoiseY.TabIndex = 7;
			this.txtMinLFNoiseY.Text = "";
			this.txtMinLFNoiseY.TextChanged += new System.EventHandler(this.txtMinLFNoiseY_TextChanged);
			// 
			// txtMaxHFNoiseY
			// 
			this.txtMaxHFNoiseY.Location = new System.Drawing.Point(96, 232);
			this.txtMaxHFNoiseY.Name = "txtMaxHFNoiseY";
			this.txtMaxHFNoiseY.Size = new System.Drawing.Size(40, 20);
			this.txtMaxHFNoiseY.TabIndex = 6;
			this.txtMaxHFNoiseY.Text = "";
			this.txtMaxHFNoiseY.TextChanged += new System.EventHandler(this.txtMaxHFNoiseY_TextChanged);
			// 
			// txtMinLFNoiseZ
			// 
			this.txtMinLFNoiseZ.Location = new System.Drawing.Point(160, 80);
			this.txtMinLFNoiseZ.Name = "txtMinLFNoiseZ";
			this.txtMinLFNoiseZ.Size = new System.Drawing.Size(40, 20);
			this.txtMinLFNoiseZ.TabIndex = 5;
			this.txtMinLFNoiseZ.Text = "";
			this.txtMinLFNoiseZ.TextChanged += new System.EventHandler(this.txtMinLFNoiseZ_TextChanged);
			// 
			// txtMinLFNoiseX
			// 
			this.txtMinLFNoiseX.Location = new System.Drawing.Point(32, 80);
			this.txtMinLFNoiseX.Name = "txtMinLFNoiseX";
			this.txtMinLFNoiseX.Size = new System.Drawing.Size(40, 20);
			this.txtMinLFNoiseX.TabIndex = 4;
			this.txtMinLFNoiseX.Text = "";
			this.txtMinLFNoiseX.TextChanged += new System.EventHandler(this.txtMinLFNoiseX_TextChanged);
			// 
			// txtMaxHFNoiseX
			// 
			this.txtMaxHFNoiseX.Location = new System.Drawing.Point(32, 232);
			this.txtMaxHFNoiseX.Name = "txtMaxHFNoiseX";
			this.txtMaxHFNoiseX.Size = new System.Drawing.Size(40, 20);
			this.txtMaxHFNoiseX.TabIndex = 3;
			this.txtMaxHFNoiseX.Text = "";
			this.txtMaxHFNoiseX.TextChanged += new System.EventHandler(this.txtMaxHFNoiseX_TextChanged);
			// 
			// txtHighFreqPoints
			// 
			this.txtHighFreqPoints.Location = new System.Drawing.Point(128, 152);
			this.txtHighFreqPoints.Name = "txtHighFreqPoints";
			this.txtHighFreqPoints.Size = new System.Drawing.Size(40, 20);
			this.txtHighFreqPoints.TabIndex = 2;
			this.txtHighFreqPoints.Text = "";
			this.txtHighFreqPoints.TextChanged += new System.EventHandler(this.txtHighFreqPoints_TextChanged);
			// 
			// txtLowFreqPoints
			// 
			this.txtLowFreqPoints.Location = new System.Drawing.Point(128, 40);
			this.txtLowFreqPoints.Name = "txtLowFreqPoints";
			this.txtLowFreqPoints.Size = new System.Drawing.Size(40, 20);
			this.txtLowFreqPoints.TabIndex = 1;
			this.txtLowFreqPoints.Text = "";
			this.txtLowFreqPoints.TextChanged += new System.EventHandler(this.txtLowFreqPoints_TextChanged);
			// 
			// objUseBeamNoise
			// 
			this.objUseBeamNoise.Checked = true;
			this.objUseBeamNoise.CheckState = System.Windows.Forms.CheckState.Checked;
			this.objUseBeamNoise.Location = new System.Drawing.Point(8, 16);
			this.objUseBeamNoise.Name = "objUseBeamNoise";
			this.objUseBeamNoise.Size = new System.Drawing.Size(112, 16);
			this.objUseBeamNoise.TabIndex = 0;
			this.objUseBeamNoise.Text = "Use Beam Noise";
			this.objUseBeamNoise.CheckedChanged += new System.EventHandler(this.objUseBeamNoise_CheckedChanged);
			// 
			// label168
			// 
			this.label168.Location = new System.Drawing.Point(16, 176);
			this.label168.Name = "label168";
			this.label168.Size = new System.Drawing.Size(152, 24);
			this.label168.TabIndex = 29;
			this.label168.Text = "Min High Frequency Noise";
			// 
			// label169
			// 
			this.label169.Location = new System.Drawing.Point(16, 216);
			this.label169.Name = "label169";
			this.label169.Size = new System.Drawing.Size(152, 24);
			this.label169.TabIndex = 30;
			this.label169.Text = "Max High Frequency Noise";
			// 
			// label170
			// 
			this.label170.Location = new System.Drawing.Point(16, 64);
			this.label170.Name = "label170";
			this.label170.Size = new System.Drawing.Size(136, 24);
			this.label170.TabIndex = 31;
			this.label170.Text = "Min Low Frequency Noise";
			// 
			// label171
			// 
			this.label171.Location = new System.Drawing.Point(16, 104);
			this.label171.Name = "label171";
			this.label171.Size = new System.Drawing.Size(144, 24);
			this.label171.TabIndex = 32;
			this.label171.Text = "Max Low Frequency Noise";
			// 
			// label154
			// 
			this.label154.Location = new System.Drawing.Point(16, 40);
			this.label154.Name = "label154";
			this.label154.Size = new System.Drawing.Size(144, 24);
			this.label154.TabIndex = 9;
			this.label154.Text = "Low Frequency Points";
			// 
			// label155
			// 
			this.label155.Location = new System.Drawing.Point(16, 152);
			this.label155.Name = "label155";
			this.label155.Size = new System.Drawing.Size(144, 24);
			this.label155.TabIndex = 10;
			this.label155.Text = "High Frequency Points";
			// 
			// groupBox21
			// 
			this.groupBox21.Location = new System.Drawing.Point(8, 8);
			this.groupBox21.Name = "groupBox21";
			this.groupBox21.Size = new System.Drawing.Size(208, 80);
			this.groupBox21.TabIndex = 0;
			this.groupBox21.TabStop = false;
			this.groupBox21.Text = "Endpoint";
			// 
			// tabPage9
			// 
			this.tabPage9.Controls.Add(this.groupBox20);
			this.tabPage9.Location = new System.Drawing.Point(4, 22);
			this.tabPage9.Name = "tabPage9";
			this.tabPage9.Size = new System.Drawing.Size(456, 358);
			this.tabPage9.TabIndex = 8;
			this.tabPage9.Text = "Rotation";
			// 
			// groupBox20
			// 
			this.groupBox20.Controls.Add(this.label152);
			this.groupBox20.Controls.Add(this.label151);
			this.groupBox20.Controls.Add(this.txtMaxRevVelZ);
			this.groupBox20.Controls.Add(this.label143);
			this.groupBox20.Controls.Add(this.txtMaxRevVelY);
			this.groupBox20.Controls.Add(this.label144);
			this.groupBox20.Controls.Add(this.txtMaxRevVelX);
			this.groupBox20.Controls.Add(this.label145);
			this.groupBox20.Controls.Add(this.label150);
			this.groupBox20.Controls.Add(this.txtMinRevVelZ);
			this.groupBox20.Controls.Add(this.label147);
			this.groupBox20.Controls.Add(this.txtMinRevVelY);
			this.groupBox20.Controls.Add(this.label148);
			this.groupBox20.Controls.Add(this.txtMinRevVelX);
			this.groupBox20.Controls.Add(this.label149);
			this.groupBox20.Controls.Add(this.objUseRevolution);
			this.groupBox20.Controls.Add(this.objUsePartSpin);
			this.groupBox20.Controls.Add(this.label146);
			this.groupBox20.Controls.Add(this.objParticleSpinType);
			this.groupBox20.Controls.Add(this.label142);
			this.groupBox20.Controls.Add(this.txtMaxSpinRange);
			this.groupBox20.Controls.Add(this.txtMinSpinRange);
			this.groupBox20.Controls.Add(this.label140);
			this.groupBox20.Controls.Add(this.label141);
			this.groupBox20.Location = new System.Drawing.Point(8, 8);
			this.groupBox20.Name = "groupBox20";
			this.groupBox20.Size = new System.Drawing.Size(440, 144);
			this.groupBox20.TabIndex = 33;
			this.groupBox20.TabStop = false;
			this.groupBox20.Text = "General";
			// 
			// label152
			// 
			this.label152.Location = new System.Drawing.Point(240, 56);
			this.label152.Name = "label152";
			this.label152.Size = new System.Drawing.Size(24, 16);
			this.label152.TabIndex = 60;
			this.label152.Text = "Min";
			// 
			// label151
			// 
			this.label151.Location = new System.Drawing.Point(240, 96);
			this.label151.Name = "label151";
			this.label151.Size = new System.Drawing.Size(32, 16);
			this.label151.TabIndex = 59;
			this.label151.Text = "Max";
			// 
			// txtMaxRevVelZ
			// 
			this.txtMaxRevVelZ.Location = new System.Drawing.Point(384, 112);
			this.txtMaxRevVelZ.Name = "txtMaxRevVelZ";
			this.txtMaxRevVelZ.Size = new System.Drawing.Size(40, 20);
			this.txtMaxRevVelZ.TabIndex = 57;
			this.txtMaxRevVelZ.Text = "";
			this.txtMaxRevVelZ.TextChanged += new System.EventHandler(this.txtMaxRevVelZ_TextChanged);
			// 
			// label143
			// 
			this.label143.Location = new System.Drawing.Point(368, 112);
			this.label143.Name = "label143";
			this.label143.Size = new System.Drawing.Size(20, 16);
			this.label143.TabIndex = 58;
			this.label143.Text = "Z";
			// 
			// txtMaxRevVelY
			// 
			this.txtMaxRevVelY.Location = new System.Drawing.Point(320, 112);
			this.txtMaxRevVelY.Name = "txtMaxRevVelY";
			this.txtMaxRevVelY.Size = new System.Drawing.Size(40, 20);
			this.txtMaxRevVelY.TabIndex = 55;
			this.txtMaxRevVelY.Text = "";
			this.txtMaxRevVelY.TextChanged += new System.EventHandler(this.txtMaxRevVelY_TextChanged);
			// 
			// label144
			// 
			this.label144.Location = new System.Drawing.Point(304, 112);
			this.label144.Name = "label144";
			this.label144.Size = new System.Drawing.Size(20, 16);
			this.label144.TabIndex = 56;
			this.label144.Text = "Y";
			// 
			// txtMaxRevVelX
			// 
			this.txtMaxRevVelX.Location = new System.Drawing.Point(256, 112);
			this.txtMaxRevVelX.Name = "txtMaxRevVelX";
			this.txtMaxRevVelX.Size = new System.Drawing.Size(40, 20);
			this.txtMaxRevVelX.TabIndex = 53;
			this.txtMaxRevVelX.Text = "";
			this.txtMaxRevVelX.TextChanged += new System.EventHandler(this.txtMaxRevVelX_TextChanged);
			// 
			// label145
			// 
			this.label145.Location = new System.Drawing.Point(240, 112);
			this.label145.Name = "label145";
			this.label145.Size = new System.Drawing.Size(20, 16);
			this.label145.TabIndex = 54;
			this.label145.Text = "X";
			// 
			// label150
			// 
			this.label150.Location = new System.Drawing.Point(224, 40);
			this.label150.Name = "label150";
			this.label150.Size = new System.Drawing.Size(168, 16);
			this.label150.TabIndex = 52;
			this.label150.Text = "Revolution Velocity (rad/s)";
			// 
			// txtMinRevVelZ
			// 
			this.txtMinRevVelZ.Location = new System.Drawing.Point(384, 72);
			this.txtMinRevVelZ.Name = "txtMinRevVelZ";
			this.txtMinRevVelZ.Size = new System.Drawing.Size(40, 20);
			this.txtMinRevVelZ.TabIndex = 50;
			this.txtMinRevVelZ.Text = "";
			this.txtMinRevVelZ.TextChanged += new System.EventHandler(this.txtMinRevVelZ_TextChanged);
			// 
			// label147
			// 
			this.label147.Location = new System.Drawing.Point(368, 72);
			this.label147.Name = "label147";
			this.label147.Size = new System.Drawing.Size(20, 16);
			this.label147.TabIndex = 51;
			this.label147.Text = "Z";
			// 
			// txtMinRevVelY
			// 
			this.txtMinRevVelY.Location = new System.Drawing.Point(320, 72);
			this.txtMinRevVelY.Name = "txtMinRevVelY";
			this.txtMinRevVelY.Size = new System.Drawing.Size(40, 20);
			this.txtMinRevVelY.TabIndex = 48;
			this.txtMinRevVelY.Text = "";
			this.txtMinRevVelY.TextChanged += new System.EventHandler(this.txtMinRevVelY_TextChanged);
			// 
			// label148
			// 
			this.label148.Location = new System.Drawing.Point(304, 72);
			this.label148.Name = "label148";
			this.label148.Size = new System.Drawing.Size(20, 16);
			this.label148.TabIndex = 49;
			this.label148.Text = "Y";
			// 
			// txtMinRevVelX
			// 
			this.txtMinRevVelX.Location = new System.Drawing.Point(256, 72);
			this.txtMinRevVelX.Name = "txtMinRevVelX";
			this.txtMinRevVelX.Size = new System.Drawing.Size(40, 20);
			this.txtMinRevVelX.TabIndex = 46;
			this.txtMinRevVelX.Text = "";
			this.txtMinRevVelX.TextChanged += new System.EventHandler(this.txtMinRevVelX_TextChanged);
			// 
			// label149
			// 
			this.label149.Location = new System.Drawing.Point(240, 72);
			this.label149.Name = "label149";
			this.label149.Size = new System.Drawing.Size(20, 16);
			this.label149.TabIndex = 47;
			this.label149.Text = "X";
			// 
			// objUseRevolution
			// 
			this.objUseRevolution.Checked = true;
			this.objUseRevolution.CheckState = System.Windows.Forms.CheckState.Checked;
			this.objUseRevolution.Location = new System.Drawing.Point(224, 16);
			this.objUseRevolution.Name = "objUseRevolution";
			this.objUseRevolution.Size = new System.Drawing.Size(136, 16);
			this.objUseRevolution.TabIndex = 45;
			this.objUseRevolution.Text = "Use Revolution";
			this.objUseRevolution.CheckedChanged += new System.EventHandler(this.objUseRevolution_CheckedChanged);
			// 
			// objUsePartSpin
			// 
			this.objUsePartSpin.Checked = true;
			this.objUsePartSpin.CheckState = System.Windows.Forms.CheckState.Checked;
			this.objUsePartSpin.Location = new System.Drawing.Point(8, 16);
			this.objUsePartSpin.Name = "objUsePartSpin";
			this.objUsePartSpin.Size = new System.Drawing.Size(136, 16);
			this.objUsePartSpin.TabIndex = 44;
			this.objUsePartSpin.Text = "Use Particle Spinning";
			this.objUsePartSpin.CheckedChanged += new System.EventHandler(this.objUsePartSpin_CheckedChanged);
			// 
			// label146
			// 
			this.label146.Location = new System.Drawing.Point(8, 40);
			this.label146.Name = "label146";
			this.label146.Size = new System.Drawing.Size(56, 16);
			this.label146.TabIndex = 43;
			this.label146.Text = "Spin Type";
			// 
			// objParticleSpinType
			// 
			this.objParticleSpinType.Items.AddRange(new object[] {
																	 "Constant",
																	 "Movement"});
			this.objParticleSpinType.Location = new System.Drawing.Point(8, 56);
			this.objParticleSpinType.Name = "objParticleSpinType";
			this.objParticleSpinType.Size = new System.Drawing.Size(112, 21);
			this.objParticleSpinType.TabIndex = 42;
			this.objParticleSpinType.SelectedIndexChanged += new System.EventHandler(this.objParticleSpinType_SelectedIndexChanged);
			// 
			// label142
			// 
			this.label142.Location = new System.Drawing.Point(8, 88);
			this.label142.Name = "label142";
			this.label142.Size = new System.Drawing.Size(184, 16);
			this.label142.TabIndex = 33;
			// 
			// txtMaxSpinRange
			// 
			this.txtMaxSpinRange.Location = new System.Drawing.Point(128, 104);
			this.txtMaxSpinRange.Name = "txtMaxSpinRange";
			this.txtMaxSpinRange.Size = new System.Drawing.Size(56, 20);
			this.txtMaxSpinRange.TabIndex = 30;
			this.txtMaxSpinRange.Text = "";
			this.txtMaxSpinRange.TextChanged += new System.EventHandler(this.txtMaxSpinRange_TextChanged);
			// 
			// txtMinSpinRange
			// 
			this.txtMinSpinRange.Location = new System.Drawing.Point(32, 104);
			this.txtMinSpinRange.Name = "txtMinSpinRange";
			this.txtMinSpinRange.Size = new System.Drawing.Size(56, 20);
			this.txtMinSpinRange.TabIndex = 29;
			this.txtMinSpinRange.Text = "";
			this.txtMinSpinRange.TextChanged += new System.EventHandler(this.txtMinSpinRange_TextChanged);
			// 
			// label140
			// 
			this.label140.Location = new System.Drawing.Point(8, 104);
			this.label140.Name = "label140";
			this.label140.Size = new System.Drawing.Size(32, 16);
			this.label140.TabIndex = 32;
			this.label140.Text = "Min";
			// 
			// label141
			// 
			this.label141.Location = new System.Drawing.Point(104, 104);
			this.label141.Name = "label141";
			this.label141.Size = new System.Drawing.Size(32, 16);
			this.label141.TabIndex = 31;
			this.label141.Text = "Max";
			// 
			// tabPage6
			// 
			this.tabPage6.Controls.Add(this.groupBox17);
			this.tabPage6.Controls.Add(this.groupBox16);
			this.tabPage6.Controls.Add(this.groupBox15);
			this.tabPage6.Location = new System.Drawing.Point(4, 22);
			this.tabPage6.Name = "tabPage6";
			this.tabPage6.Size = new System.Drawing.Size(456, 358);
			this.tabPage6.TabIndex = 5;
			this.tabPage6.Text = "Rendering";
			this.tabPage6.Click += new System.EventHandler(this.tabPage6_Click);
			// 
			// groupBox17
			// 
			this.groupBox17.Controls.Add(this.txtEndRelSize);
			this.groupBox17.Controls.Add(this.txtStartRelSize);
			this.groupBox17.Controls.Add(this.label96);
			this.groupBox17.Controls.Add(this.label98);
			this.groupBox17.Controls.Add(this.label97);
			this.groupBox17.Controls.Add(this.txtMiddleRelSize);
			this.groupBox17.Controls.Add(this.label99);
			this.groupBox17.Controls.Add(this.label95);
			this.groupBox17.Controls.Add(this.txtMiddleRelSizeLength);
			this.groupBox17.Controls.Add(this.txtMiddleRelSizeTime);
			this.groupBox17.Location = new System.Drawing.Point(8, 200);
			this.groupBox17.Name = "groupBox17";
			this.groupBox17.Size = new System.Drawing.Size(440, 104);
			this.groupBox17.TabIndex = 2;
			this.groupBox17.TabStop = false;
			this.groupBox17.Text = "Size Fading";
			// 
			// txtEndRelSize
			// 
			this.txtEndRelSize.Location = new System.Drawing.Point(120, 72);
			this.txtEndRelSize.Name = "txtEndRelSize";
			this.txtEndRelSize.Size = new System.Drawing.Size(56, 20);
			this.txtEndRelSize.TabIndex = 17;
			this.txtEndRelSize.Text = "";
			this.txtEndRelSize.TextChanged += new System.EventHandler(this.txtEndRelSize_TextChanged);
			// 
			// txtStartRelSize
			// 
			this.txtStartRelSize.Location = new System.Drawing.Point(120, 24);
			this.txtStartRelSize.Name = "txtStartRelSize";
			this.txtStartRelSize.Size = new System.Drawing.Size(56, 20);
			this.txtStartRelSize.TabIndex = 15;
			this.txtStartRelSize.Text = "";
			this.txtStartRelSize.TextChanged += new System.EventHandler(this.txtStartRelSize_TextChanged);
			// 
			// label96
			// 
			this.label96.Location = new System.Drawing.Point(8, 48);
			this.label96.Name = "label96";
			this.label96.Size = new System.Drawing.Size(112, 16);
			this.label96.TabIndex = 13;
			this.label96.Text = "Middle Relative Size:";
			// 
			// label98
			// 
			this.label98.Location = new System.Drawing.Point(240, 24);
			this.label98.Name = "label98";
			this.label98.Size = new System.Drawing.Size(112, 16);
			this.label98.TabIndex = 18;
			this.label98.Text = "Middle Relative start";
			// 
			// label97
			// 
			this.label97.Location = new System.Drawing.Point(8, 72);
			this.label97.Name = "label97";
			this.label97.Size = new System.Drawing.Size(104, 16);
			this.label97.TabIndex = 14;
			this.label97.Text = "End Relative Size:";
			// 
			// txtMiddleRelSize
			// 
			this.txtMiddleRelSize.Location = new System.Drawing.Point(120, 48);
			this.txtMiddleRelSize.Name = "txtMiddleRelSize";
			this.txtMiddleRelSize.Size = new System.Drawing.Size(56, 20);
			this.txtMiddleRelSize.TabIndex = 16;
			this.txtMiddleRelSize.Text = "";
			this.txtMiddleRelSize.TextChanged += new System.EventHandler(this.txtMiddleRelSize_TextChanged);
			// 
			// label99
			// 
			this.label99.Location = new System.Drawing.Point(240, 48);
			this.label99.Name = "label99";
			this.label99.Size = new System.Drawing.Size(120, 16);
			this.label99.TabIndex = 19;
			this.label99.Text = "Middle Relative length";
			// 
			// label95
			// 
			this.label95.Location = new System.Drawing.Point(8, 24);
			this.label95.Name = "label95";
			this.label95.Size = new System.Drawing.Size(104, 16);
			this.label95.TabIndex = 12;
			this.label95.Text = "Start Relative Size:";
			// 
			// txtMiddleRelSizeLength
			// 
			this.txtMiddleRelSizeLength.Location = new System.Drawing.Point(360, 48);
			this.txtMiddleRelSizeLength.Name = "txtMiddleRelSizeLength";
			this.txtMiddleRelSizeLength.Size = new System.Drawing.Size(56, 20);
			this.txtMiddleRelSizeLength.TabIndex = 21;
			this.txtMiddleRelSizeLength.Text = "";
			this.txtMiddleRelSizeLength.TextChanged += new System.EventHandler(this.txtMiddleRelSizeLength_TextChanged);
			// 
			// txtMiddleRelSizeTime
			// 
			this.txtMiddleRelSizeTime.Location = new System.Drawing.Point(360, 24);
			this.txtMiddleRelSizeTime.Name = "txtMiddleRelSizeTime";
			this.txtMiddleRelSizeTime.Size = new System.Drawing.Size(56, 20);
			this.txtMiddleRelSizeTime.TabIndex = 20;
			this.txtMiddleRelSizeTime.Text = "";
			this.txtMiddleRelSizeTime.TextChanged += new System.EventHandler(this.txtMiddleRelSizeTime_TextChanged);
			// 
			// groupBox16
			// 
			this.groupBox16.Controls.Add(this.label172);
			this.groupBox16.Controls.Add(this.label92);
			this.groupBox16.Controls.Add(this.txtMaxStartSizeY);
			this.groupBox16.Controls.Add(this.label93);
			this.groupBox16.Controls.Add(this.txtMaxStartSizeX);
			this.groupBox16.Controls.Add(this.label94);
			this.groupBox16.Controls.Add(this.label91);
			this.groupBox16.Controls.Add(this.txtMinStartSizeY);
			this.groupBox16.Controls.Add(this.label90);
			this.groupBox16.Controls.Add(this.txtMinStartSizeX);
			this.groupBox16.Controls.Add(this.label89);
			this.groupBox16.Location = new System.Drawing.Point(8, 96);
			this.groupBox16.Name = "groupBox16";
			this.groupBox16.Size = new System.Drawing.Size(440, 96);
			this.groupBox16.TabIndex = 1;
			this.groupBox16.TabStop = false;
			this.groupBox16.Text = "Size";
			// 
			// label92
			// 
			this.label92.Location = new System.Drawing.Point(344, 40);
			this.label92.Name = "label92";
			this.label92.Size = new System.Drawing.Size(16, 16);
			this.label92.TabIndex = 11;
			this.label92.Text = "Y";
			// 
			// txtMaxStartSizeY
			// 
			this.txtMaxStartSizeY.Location = new System.Drawing.Point(360, 40);
			this.txtMaxStartSizeY.Name = "txtMaxStartSizeY";
			this.txtMaxStartSizeY.Size = new System.Drawing.Size(56, 20);
			this.txtMaxStartSizeY.TabIndex = 10;
			this.txtMaxStartSizeY.Text = "";
			this.txtMaxStartSizeY.TextChanged += new System.EventHandler(this.txtMaxStartSizeY_TextChanged);
			// 
			// label93
			// 
			this.label93.Location = new System.Drawing.Point(240, 40);
			this.label93.Name = "label93";
			this.label93.Size = new System.Drawing.Size(16, 16);
			this.label93.TabIndex = 9;
			this.label93.Text = "X";
			// 
			// txtMaxStartSizeX
			// 
			this.txtMaxStartSizeX.Location = new System.Drawing.Point(256, 40);
			this.txtMaxStartSizeX.Name = "txtMaxStartSizeX";
			this.txtMaxStartSizeX.Size = new System.Drawing.Size(56, 20);
			this.txtMaxStartSizeX.TabIndex = 8;
			this.txtMaxStartSizeX.Text = "";
			this.txtMaxStartSizeX.TextChanged += new System.EventHandler(this.txtMaxStartSizeX_TextChanged);
			// 
			// label94
			// 
			this.label94.Location = new System.Drawing.Point(240, 16);
			this.label94.Name = "label94";
			this.label94.Size = new System.Drawing.Size(72, 16);
			this.label94.TabIndex = 7;
			this.label94.Text = "Max Size (m)";
			// 
			// label91
			// 
			this.label91.Location = new System.Drawing.Point(104, 40);
			this.label91.Name = "label91";
			this.label91.Size = new System.Drawing.Size(16, 16);
			this.label91.TabIndex = 6;
			this.label91.Text = "Y";
			// 
			// txtMinStartSizeY
			// 
			this.txtMinStartSizeY.Location = new System.Drawing.Point(120, 40);
			this.txtMinStartSizeY.Name = "txtMinStartSizeY";
			this.txtMinStartSizeY.Size = new System.Drawing.Size(56, 20);
			this.txtMinStartSizeY.TabIndex = 5;
			this.txtMinStartSizeY.Text = "";
			this.txtMinStartSizeY.TextChanged += new System.EventHandler(this.txtMinStartSizeY_TextChanged);
			// 
			// label90
			// 
			this.label90.Location = new System.Drawing.Point(16, 40);
			this.label90.Name = "label90";
			this.label90.Size = new System.Drawing.Size(16, 16);
			this.label90.TabIndex = 4;
			this.label90.Text = "X";
			// 
			// txtMinStartSizeX
			// 
			this.txtMinStartSizeX.Location = new System.Drawing.Point(32, 40);
			this.txtMinStartSizeX.Name = "txtMinStartSizeX";
			this.txtMinStartSizeX.Size = new System.Drawing.Size(56, 20);
			this.txtMinStartSizeX.TabIndex = 3;
			this.txtMinStartSizeX.Text = "";
			this.txtMinStartSizeX.TextChanged += new System.EventHandler(this.txtMinStartSizeX_TextChanged);
			// 
			// label89
			// 
			this.label89.Location = new System.Drawing.Point(16, 16);
			this.label89.Name = "label89";
			this.label89.Size = new System.Drawing.Size(72, 16);
			this.label89.TabIndex = 2;
			this.label89.Text = "Min Size (m)";
			// 
			// groupBox15
			// 
			this.groupBox15.Controls.Add(this.objMultiplyRGBWithAlpha);
			this.groupBox15.Controls.Add(this.objDrawType);
			this.groupBox15.Controls.Add(this.label88);
			this.groupBox15.Location = new System.Drawing.Point(8, 8);
			this.groupBox15.Name = "groupBox15";
			this.groupBox15.Size = new System.Drawing.Size(440, 80);
			this.groupBox15.TabIndex = 0;
			this.groupBox15.TabStop = false;
			this.groupBox15.Text = "General";
			// 
			// objMultiplyRGBWithAlpha
			// 
			this.objMultiplyRGBWithAlpha.Location = new System.Drawing.Point(240, 16);
			this.objMultiplyRGBWithAlpha.Name = "objMultiplyRGBWithAlpha";
			this.objMultiplyRGBWithAlpha.Size = new System.Drawing.Size(144, 24);
			this.objMultiplyRGBWithAlpha.TabIndex = 2;
			this.objMultiplyRGBWithAlpha.Text = "Multiply RGB with alpha";
			this.objMultiplyRGBWithAlpha.CheckedChanged += new System.EventHandler(this.objMultiplyRGBWithAlpha_CheckedChanged);
			// 
			// objDrawType
			// 
			this.objDrawType.Items.AddRange(new object[] {
															 "Point",
															 "Line",
															 "Axis"});
			this.objDrawType.Location = new System.Drawing.Point(16, 32);
			this.objDrawType.Name = "objDrawType";
			this.objDrawType.Size = new System.Drawing.Size(96, 21);
			this.objDrawType.TabIndex = 1;
			this.objDrawType.SelectedIndexChanged += new System.EventHandler(this.objDrawType_SelectedIndexChanged);
			// 
			// label88
			// 
			this.label88.Location = new System.Drawing.Point(16, 16);
			this.label88.Name = "label88";
			this.label88.Size = new System.Drawing.Size(64, 16);
			this.label88.TabIndex = 0;
			this.label88.Text = "Draw Type";
			// 
			// tabPage3
			// 
			this.tabPage3.Controls.Add(this.groupBox7);
			this.tabPage3.Controls.Add(this.groupBox6);
			this.tabPage3.Controls.Add(this.groupBox5);
			this.tabPage3.Location = new System.Drawing.Point(4, 22);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Size = new System.Drawing.Size(456, 358);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "Start";
			// 
			// groupBox7
			// 
			this.groupBox7.Controls.Add(this.txtMaxStartRadius);
			this.groupBox7.Controls.Add(this.label45);
			this.groupBox7.Controls.Add(this.label44);
			this.groupBox7.Controls.Add(this.label43);
			this.groupBox7.Controls.Add(this.txtMinStartRadius);
			this.groupBox7.Controls.Add(this.txtMaxStartAnglesX);
			this.groupBox7.Controls.Add(this.txtMaxStartAnglesY);
			this.groupBox7.Controls.Add(this.label40);
			this.groupBox7.Controls.Add(this.label41);
			this.groupBox7.Controls.Add(this.txtMinStartAnglesX);
			this.groupBox7.Controls.Add(this.txtMinStartAnglesY);
			this.groupBox7.Controls.Add(this.label39);
			this.groupBox7.Controls.Add(this.label38);
			this.groupBox7.Controls.Add(this.label37);
			this.groupBox7.Controls.Add(this.label42);
			this.groupBox7.Location = new System.Drawing.Point(8, 152);
			this.groupBox7.Name = "groupBox7";
			this.groupBox7.Size = new System.Drawing.Size(440, 64);
			this.groupBox7.TabIndex = 2;
			this.groupBox7.TabStop = false;
			this.groupBox7.Text = "Sphere Start";
			// 
			// txtMaxStartRadius
			// 
			this.txtMaxStartRadius.Location = new System.Drawing.Point(384, 32);
			this.txtMaxStartRadius.Name = "txtMaxStartRadius";
			this.txtMaxStartRadius.Size = new System.Drawing.Size(40, 20);
			this.txtMaxStartRadius.TabIndex = 28;
			this.txtMaxStartRadius.Text = "";
			this.txtMaxStartRadius.TextChanged += new System.EventHandler(this.txtMaxStartRadius_TextChanged_1);
			// 
			// label45
			// 
			this.label45.Location = new System.Drawing.Point(360, 32);
			this.label45.Name = "label45";
			this.label45.Size = new System.Drawing.Size(32, 16);
			this.label45.TabIndex = 30;
			this.label45.Text = "Max";
			// 
			// label44
			// 
			this.label44.Location = new System.Drawing.Point(288, 32);
			this.label44.Name = "label44";
			this.label44.Size = new System.Drawing.Size(24, 16);
			this.label44.TabIndex = 29;
			this.label44.Text = "Min";
			// 
			// label43
			// 
			this.label43.Location = new System.Drawing.Point(288, 16);
			this.label43.Name = "label43";
			this.label43.Size = new System.Drawing.Size(96, 16);
			this.label43.TabIndex = 27;
			this.label43.Text = "Radius (m)";
			// 
			// txtMinStartRadius
			// 
			this.txtMinStartRadius.Location = new System.Drawing.Point(312, 32);
			this.txtMinStartRadius.Name = "txtMinStartRadius";
			this.txtMinStartRadius.Size = new System.Drawing.Size(40, 20);
			this.txtMinStartRadius.TabIndex = 26;
			this.txtMinStartRadius.Text = "";
			this.txtMinStartRadius.TextChanged += new System.EventHandler(this.txtMinStartRadius_TextChanged);
			// 
			// txtMaxStartAnglesX
			// 
			this.txtMaxStartAnglesX.Location = new System.Drawing.Point(160, 30);
			this.txtMaxStartAnglesX.Name = "txtMaxStartAnglesX";
			this.txtMaxStartAnglesX.Size = new System.Drawing.Size(40, 20);
			this.txtMaxStartAnglesX.TabIndex = 22;
			this.txtMaxStartAnglesX.Text = "";
			this.txtMaxStartAnglesX.TextChanged += new System.EventHandler(this.txtMaxStartAnglesX_TextChanged);
			// 
			// txtMaxStartAnglesY
			// 
			this.txtMaxStartAnglesY.Location = new System.Drawing.Point(224, 30);
			this.txtMaxStartAnglesY.Name = "txtMaxStartAnglesY";
			this.txtMaxStartAnglesY.Size = new System.Drawing.Size(40, 20);
			this.txtMaxStartAnglesY.TabIndex = 24;
			this.txtMaxStartAnglesY.Text = "";
			this.txtMaxStartAnglesY.TextChanged += new System.EventHandler(this.txtMaxStartAnglesY_TextChanged);
			// 
			// label40
			// 
			this.label40.Location = new System.Drawing.Point(144, 14);
			this.label40.Name = "label40";
			this.label40.Size = new System.Drawing.Size(96, 16);
			this.label40.TabIndex = 21;
			this.label40.Text = "Max Angles (deg)";
			// 
			// label41
			// 
			this.label41.Location = new System.Drawing.Point(144, 30);
			this.label41.Name = "label41";
			this.label41.Size = new System.Drawing.Size(20, 16);
			this.label41.TabIndex = 23;
			this.label41.Text = "X";
			// 
			// txtMinStartAnglesX
			// 
			this.txtMinStartAnglesX.Location = new System.Drawing.Point(24, 32);
			this.txtMinStartAnglesX.Name = "txtMinStartAnglesX";
			this.txtMinStartAnglesX.Size = new System.Drawing.Size(40, 20);
			this.txtMinStartAnglesX.TabIndex = 17;
			this.txtMinStartAnglesX.Text = "";
			this.txtMinStartAnglesX.TextChanged += new System.EventHandler(this.txtMinStartAnglesX_TextChanged);
			// 
			// txtMinStartAnglesY
			// 
			this.txtMinStartAnglesY.Location = new System.Drawing.Point(88, 32);
			this.txtMinStartAnglesY.Name = "txtMinStartAnglesY";
			this.txtMinStartAnglesY.Size = new System.Drawing.Size(40, 20);
			this.txtMinStartAnglesY.TabIndex = 19;
			this.txtMinStartAnglesY.Text = "";
			this.txtMinStartAnglesY.TextChanged += new System.EventHandler(this.txtMinStartAnglesY_TextChanged);
			// 
			// label39
			// 
			this.label39.Location = new System.Drawing.Point(8, 16);
			this.label39.Name = "label39";
			this.label39.Size = new System.Drawing.Size(96, 16);
			this.label39.TabIndex = 16;
			this.label39.Text = "Min Angles (deg)";
			// 
			// label38
			// 
			this.label38.Location = new System.Drawing.Point(8, 32);
			this.label38.Name = "label38";
			this.label38.Size = new System.Drawing.Size(20, 16);
			this.label38.TabIndex = 18;
			this.label38.Text = "X";
			// 
			// label37
			// 
			this.label37.Location = new System.Drawing.Point(72, 32);
			this.label37.Name = "label37";
			this.label37.Size = new System.Drawing.Size(20, 16);
			this.label37.TabIndex = 20;
			this.label37.Text = "Y";
			// 
			// label42
			// 
			this.label42.Location = new System.Drawing.Point(208, 32);
			this.label42.Name = "label42";
			this.label42.Size = new System.Drawing.Size(20, 16);
			this.label42.TabIndex = 25;
			this.label42.Text = "Y";
			// 
			// groupBox6
			// 
			this.groupBox6.Controls.Add(this.txtMaxStartPosZ);
			this.groupBox6.Controls.Add(this.label33);
			this.groupBox6.Controls.Add(this.txtMaxStartPosY);
			this.groupBox6.Controls.Add(this.label34);
			this.groupBox6.Controls.Add(this.txtMaxStartPosX);
			this.groupBox6.Controls.Add(this.label35);
			this.groupBox6.Controls.Add(this.label36);
			this.groupBox6.Controls.Add(this.txtMinStartPosZ);
			this.groupBox6.Controls.Add(this.label32);
			this.groupBox6.Controls.Add(this.txtMinStartPosY);
			this.groupBox6.Controls.Add(this.label31);
			this.groupBox6.Controls.Add(this.txtMinStartPosX);
			this.groupBox6.Controls.Add(this.label30);
			this.groupBox6.Controls.Add(this.label29);
			this.groupBox6.Location = new System.Drawing.Point(8, 80);
			this.groupBox6.Name = "groupBox6";
			this.groupBox6.Size = new System.Drawing.Size(440, 64);
			this.groupBox6.TabIndex = 1;
			this.groupBox6.TabStop = false;
			this.groupBox6.Text = "Box Start";
			// 
			// txtMaxStartPosZ
			// 
			this.txtMaxStartPosZ.Location = new System.Drawing.Point(384, 32);
			this.txtMaxStartPosZ.Name = "txtMaxStartPosZ";
			this.txtMaxStartPosZ.Size = new System.Drawing.Size(40, 20);
			this.txtMaxStartPosZ.TabIndex = 14;
			this.txtMaxStartPosZ.Text = "";
			this.txtMaxStartPosZ.TextChanged += new System.EventHandler(this.txtMaxStartPosZ_TextChanged);
			// 
			// label33
			// 
			this.label33.Location = new System.Drawing.Point(368, 32);
			this.label33.Name = "label33";
			this.label33.Size = new System.Drawing.Size(20, 16);
			this.label33.TabIndex = 15;
			this.label33.Text = "Z";
			// 
			// txtMaxStartPosY
			// 
			this.txtMaxStartPosY.Location = new System.Drawing.Point(320, 32);
			this.txtMaxStartPosY.Name = "txtMaxStartPosY";
			this.txtMaxStartPosY.Size = new System.Drawing.Size(40, 20);
			this.txtMaxStartPosY.TabIndex = 12;
			this.txtMaxStartPosY.Text = "";
			// 
			// label34
			// 
			this.label34.Location = new System.Drawing.Point(304, 32);
			this.label34.Name = "label34";
			this.label34.Size = new System.Drawing.Size(20, 16);
			this.label34.TabIndex = 13;
			this.label34.Text = "Y";
			// 
			// txtMaxStartPosX
			// 
			this.txtMaxStartPosX.Location = new System.Drawing.Point(256, 32);
			this.txtMaxStartPosX.Name = "txtMaxStartPosX";
			this.txtMaxStartPosX.Size = new System.Drawing.Size(40, 20);
			this.txtMaxStartPosX.TabIndex = 10;
			this.txtMaxStartPosX.Text = "";
			this.txtMaxStartPosX.TextChanged += new System.EventHandler(this.txtMaxStartPosX_TextChanged);
			// 
			// label35
			// 
			this.label35.Location = new System.Drawing.Point(240, 32);
			this.label35.Name = "label35";
			this.label35.Size = new System.Drawing.Size(20, 16);
			this.label35.TabIndex = 11;
			this.label35.Text = "X";
			// 
			// label36
			// 
			this.label36.Location = new System.Drawing.Point(240, 16);
			this.label36.Name = "label36";
			this.label36.Size = new System.Drawing.Size(96, 16);
			this.label36.TabIndex = 9;
			this.label36.Text = "Max Position (m)";
			// 
			// txtMinStartPosZ
			// 
			this.txtMinStartPosZ.Location = new System.Drawing.Point(152, 32);
			this.txtMinStartPosZ.Name = "txtMinStartPosZ";
			this.txtMinStartPosZ.Size = new System.Drawing.Size(40, 20);
			this.txtMinStartPosZ.TabIndex = 7;
			this.txtMinStartPosZ.Text = "";
			this.txtMinStartPosZ.TextChanged += new System.EventHandler(this.txtMinStartPosZ_TextChanged);
			// 
			// label32
			// 
			this.label32.Location = new System.Drawing.Point(136, 32);
			this.label32.Name = "label32";
			this.label32.Size = new System.Drawing.Size(20, 16);
			this.label32.TabIndex = 8;
			this.label32.Text = "Z";
			// 
			// txtMinStartPosY
			// 
			this.txtMinStartPosY.Location = new System.Drawing.Point(88, 32);
			this.txtMinStartPosY.Name = "txtMinStartPosY";
			this.txtMinStartPosY.Size = new System.Drawing.Size(40, 20);
			this.txtMinStartPosY.TabIndex = 5;
			this.txtMinStartPosY.Text = "";
			this.txtMinStartPosY.TextChanged += new System.EventHandler(this.txtMinStartPosY_TextChanged);
			// 
			// label31
			// 
			this.label31.Location = new System.Drawing.Point(72, 32);
			this.label31.Name = "label31";
			this.label31.Size = new System.Drawing.Size(20, 16);
			this.label31.TabIndex = 6;
			this.label31.Text = "Y";
			// 
			// txtMinStartPosX
			// 
			this.txtMinStartPosX.Location = new System.Drawing.Point(24, 32);
			this.txtMinStartPosX.Name = "txtMinStartPosX";
			this.txtMinStartPosX.Size = new System.Drawing.Size(40, 20);
			this.txtMinStartPosX.TabIndex = 3;
			this.txtMinStartPosX.Text = "";
			this.txtMinStartPosX.TextChanged += new System.EventHandler(this.txtMinStartPosX_TextChanged);
			// 
			// label30
			// 
			this.label30.Location = new System.Drawing.Point(8, 32);
			this.label30.Name = "label30";
			this.label30.Size = new System.Drawing.Size(20, 16);
			this.label30.TabIndex = 4;
			this.label30.Text = "X";
			// 
			// label29
			// 
			this.label29.Location = new System.Drawing.Point(8, 16);
			this.label29.Name = "label29";
			this.label29.Size = new System.Drawing.Size(88, 16);
			this.label29.TabIndex = 2;
			this.label29.Text = "Min Position (m)";
			// 
			// groupBox5
			// 
			this.groupBox5.Controls.Add(this.objStartPosType);
			this.groupBox5.Controls.Add(this.label28);
			this.groupBox5.Location = new System.Drawing.Point(8, 8);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.Size = new System.Drawing.Size(440, 64);
			this.groupBox5.TabIndex = 0;
			this.groupBox5.TabStop = false;
			this.groupBox5.Text = "General";
			this.groupBox5.Enter += new System.EventHandler(this.groupBox5_Enter);
			// 
			// objStartPosType
			// 
			this.objStartPosType.Items.AddRange(new object[] {
																 "Box",
																 "Sphere"});
			this.objStartPosType.Location = new System.Drawing.Point(8, 32);
			this.objStartPosType.Name = "objStartPosType";
			this.objStartPosType.Size = new System.Drawing.Size(120, 21);
			this.objStartPosType.TabIndex = 1;
			this.objStartPosType.SelectedIndexChanged += new System.EventHandler(this.objStartPosType_SelectedIndexChanged);
			// 
			// label28
			// 
			this.label28.Location = new System.Drawing.Point(8, 16);
			this.label28.Name = "label28";
			this.label28.Size = new System.Drawing.Size(128, 16);
			this.label28.TabIndex = 0;
			this.label28.Text = "Start type";
			// 
			// tabPage7
			// 
			this.tabPage7.Controls.Add(this.groupBox19);
			this.tabPage7.Controls.Add(this.groupBox18);
			this.tabPage7.Location = new System.Drawing.Point(4, 22);
			this.tabPage7.Name = "tabPage7";
			this.tabPage7.Size = new System.Drawing.Size(456, 358);
			this.tabPage7.TabIndex = 6;
			this.tabPage7.Text = "Color";
			// 
			// groupBox19
			// 
			this.groupBox19.Controls.Add(this.txtMiddleRelColorLength);
			this.groupBox19.Controls.Add(this.txtMiddleRelColorTime);
			this.groupBox19.Controls.Add(this.label126);
			this.groupBox19.Controls.Add(this.label125);
			this.groupBox19.Controls.Add(this.txtEndRelColorR);
			this.groupBox19.Controls.Add(this.label120);
			this.groupBox19.Controls.Add(this.txtEndRelColorA);
			this.groupBox19.Controls.Add(this.txtEndRelColorB);
			this.groupBox19.Controls.Add(this.txtEndRelColorG);
			this.groupBox19.Controls.Add(this.label121);
			this.groupBox19.Controls.Add(this.label122);
			this.groupBox19.Controls.Add(this.label123);
			this.groupBox19.Controls.Add(this.label124);
			this.groupBox19.Controls.Add(this.txtMiddleRelColorR);
			this.groupBox19.Controls.Add(this.label119);
			this.groupBox19.Controls.Add(this.txtMiddleRelColorA);
			this.groupBox19.Controls.Add(this.txtMiddleRelColorB);
			this.groupBox19.Controls.Add(this.txtMiddleRelColorG);
			this.groupBox19.Controls.Add(this.label115);
			this.groupBox19.Controls.Add(this.label116);
			this.groupBox19.Controls.Add(this.label117);
			this.groupBox19.Controls.Add(this.label118);
			this.groupBox19.Controls.Add(this.txtStartRelColorA);
			this.groupBox19.Controls.Add(this.txtStartRelColorB);
			this.groupBox19.Controls.Add(this.txtStartRelColorG);
			this.groupBox19.Controls.Add(this.txtStartRelColorR);
			this.groupBox19.Controls.Add(this.label112);
			this.groupBox19.Controls.Add(this.label111);
			this.groupBox19.Controls.Add(this.label110);
			this.groupBox19.Controls.Add(this.label114);
			this.groupBox19.Controls.Add(this.label113);
			this.groupBox19.Location = new System.Drawing.Point(8, 128);
			this.groupBox19.Name = "groupBox19";
			this.groupBox19.Size = new System.Drawing.Size(440, 160);
			this.groupBox19.TabIndex = 1;
			this.groupBox19.TabStop = false;
			this.groupBox19.Text = "Fading";
			// 
			// txtMiddleRelColorLength
			// 
			this.txtMiddleRelColorLength.Location = new System.Drawing.Point(328, 80);
			this.txtMiddleRelColorLength.Name = "txtMiddleRelColorLength";
			this.txtMiddleRelColorLength.Size = new System.Drawing.Size(88, 20);
			this.txtMiddleRelColorLength.TabIndex = 48;
			this.txtMiddleRelColorLength.Text = "";
			this.txtMiddleRelColorLength.TextChanged += new System.EventHandler(this.txtMiddleRelColorLength_TextChanged);
			// 
			// txtMiddleRelColorTime
			// 
			this.txtMiddleRelColorTime.Location = new System.Drawing.Point(328, 32);
			this.txtMiddleRelColorTime.Name = "txtMiddleRelColorTime";
			this.txtMiddleRelColorTime.Size = new System.Drawing.Size(88, 20);
			this.txtMiddleRelColorTime.TabIndex = 47;
			this.txtMiddleRelColorTime.Text = "";
			this.txtMiddleRelColorTime.TextChanged += new System.EventHandler(this.txtMiddleRelColorTime_TextChanged);
			// 
			// label126
			// 
			this.label126.Location = new System.Drawing.Point(328, 64);
			this.label126.Name = "label126";
			this.label126.Size = new System.Drawing.Size(112, 16);
			this.label126.TabIndex = 46;
			this.label126.Text = "Middle relative length";
			// 
			// label125
			// 
			this.label125.Location = new System.Drawing.Point(328, 16);
			this.label125.Name = "label125";
			this.label125.Size = new System.Drawing.Size(104, 16);
			this.label125.TabIndex = 45;
			this.label125.Text = "Middle relative start";
			// 
			// txtEndRelColorR
			// 
			this.txtEndRelColorR.Location = new System.Drawing.Point(24, 128);
			this.txtEndRelColorR.Name = "txtEndRelColorR";
			this.txtEndRelColorR.Size = new System.Drawing.Size(40, 20);
			this.txtEndRelColorR.TabIndex = 37;
			this.txtEndRelColorR.Text = "";
			this.txtEndRelColorR.TextChanged += new System.EventHandler(this.txtEndRelColorR_TextChanged);
			// 
			// label120
			// 
			this.label120.Location = new System.Drawing.Point(8, 128);
			this.label120.Name = "label120";
			this.label120.Size = new System.Drawing.Size(24, 16);
			this.label120.TabIndex = 44;
			this.label120.Text = "R";
			// 
			// txtEndRelColorA
			// 
			this.txtEndRelColorA.Location = new System.Drawing.Point(216, 128);
			this.txtEndRelColorA.Name = "txtEndRelColorA";
			this.txtEndRelColorA.Size = new System.Drawing.Size(40, 20);
			this.txtEndRelColorA.TabIndex = 42;
			this.txtEndRelColorA.Text = "";
			this.txtEndRelColorA.TextChanged += new System.EventHandler(this.txtEndRelColorA_TextChanged);
			// 
			// txtEndRelColorB
			// 
			this.txtEndRelColorB.Location = new System.Drawing.Point(152, 128);
			this.txtEndRelColorB.Name = "txtEndRelColorB";
			this.txtEndRelColorB.Size = new System.Drawing.Size(40, 20);
			this.txtEndRelColorB.TabIndex = 40;
			this.txtEndRelColorB.Text = "";
			this.txtEndRelColorB.TextChanged += new System.EventHandler(this.txtEndRelColorB_TextChanged);
			// 
			// txtEndRelColorG
			// 
			this.txtEndRelColorG.Location = new System.Drawing.Point(88, 128);
			this.txtEndRelColorG.Name = "txtEndRelColorG";
			this.txtEndRelColorG.Size = new System.Drawing.Size(40, 20);
			this.txtEndRelColorG.TabIndex = 38;
			this.txtEndRelColorG.Text = "";
			this.txtEndRelColorG.TextChanged += new System.EventHandler(this.txtEndRelColorG_TextChanged);
			// 
			// label121
			// 
			this.label121.Location = new System.Drawing.Point(72, 128);
			this.label121.Name = "label121";
			this.label121.Size = new System.Drawing.Size(24, 16);
			this.label121.TabIndex = 39;
			this.label121.Text = "G";
			// 
			// label122
			// 
			this.label122.Location = new System.Drawing.Point(136, 128);
			this.label122.Name = "label122";
			this.label122.Size = new System.Drawing.Size(24, 16);
			this.label122.TabIndex = 41;
			this.label122.Text = "B";
			// 
			// label123
			// 
			this.label123.Location = new System.Drawing.Point(200, 128);
			this.label123.Name = "label123";
			this.label123.Size = new System.Drawing.Size(24, 16);
			this.label123.TabIndex = 43;
			this.label123.Text = "A";
			// 
			// label124
			// 
			this.label124.Location = new System.Drawing.Point(8, 112);
			this.label124.Name = "label124";
			this.label124.Size = new System.Drawing.Size(120, 16);
			this.label124.TabIndex = 36;
			this.label124.Text = "End relative color";
			// 
			// txtMiddleRelColorR
			// 
			this.txtMiddleRelColorR.Location = new System.Drawing.Point(24, 80);
			this.txtMiddleRelColorR.Name = "txtMiddleRelColorR";
			this.txtMiddleRelColorR.Size = new System.Drawing.Size(40, 20);
			this.txtMiddleRelColorR.TabIndex = 28;
			this.txtMiddleRelColorR.Text = "";
			this.txtMiddleRelColorR.TextChanged += new System.EventHandler(this.txtMiddleRelColorR_TextChanged);
			// 
			// label119
			// 
			this.label119.Location = new System.Drawing.Point(8, 80);
			this.label119.Name = "label119";
			this.label119.Size = new System.Drawing.Size(24, 16);
			this.label119.TabIndex = 35;
			this.label119.Text = "R";
			// 
			// txtMiddleRelColorA
			// 
			this.txtMiddleRelColorA.Location = new System.Drawing.Point(216, 80);
			this.txtMiddleRelColorA.Name = "txtMiddleRelColorA";
			this.txtMiddleRelColorA.Size = new System.Drawing.Size(40, 20);
			this.txtMiddleRelColorA.TabIndex = 33;
			this.txtMiddleRelColorA.Text = "";
			this.txtMiddleRelColorA.TextChanged += new System.EventHandler(this.txtMiddleRelColorA_TextChanged);
			// 
			// txtMiddleRelColorB
			// 
			this.txtMiddleRelColorB.Location = new System.Drawing.Point(152, 80);
			this.txtMiddleRelColorB.Name = "txtMiddleRelColorB";
			this.txtMiddleRelColorB.Size = new System.Drawing.Size(40, 20);
			this.txtMiddleRelColorB.TabIndex = 31;
			this.txtMiddleRelColorB.Text = "";
			this.txtMiddleRelColorB.TextChanged += new System.EventHandler(this.txtMiddleRelColorB_TextChanged);
			// 
			// txtMiddleRelColorG
			// 
			this.txtMiddleRelColorG.Location = new System.Drawing.Point(88, 80);
			this.txtMiddleRelColorG.Name = "txtMiddleRelColorG";
			this.txtMiddleRelColorG.Size = new System.Drawing.Size(40, 20);
			this.txtMiddleRelColorG.TabIndex = 29;
			this.txtMiddleRelColorG.Text = "";
			this.txtMiddleRelColorG.TextChanged += new System.EventHandler(this.txtMiddleRelColorG_TextChanged);
			// 
			// label115
			// 
			this.label115.Location = new System.Drawing.Point(72, 80);
			this.label115.Name = "label115";
			this.label115.Size = new System.Drawing.Size(24, 16);
			this.label115.TabIndex = 30;
			this.label115.Text = "G";
			// 
			// label116
			// 
			this.label116.Location = new System.Drawing.Point(136, 80);
			this.label116.Name = "label116";
			this.label116.Size = new System.Drawing.Size(24, 16);
			this.label116.TabIndex = 32;
			this.label116.Text = "B";
			// 
			// label117
			// 
			this.label117.Location = new System.Drawing.Point(200, 80);
			this.label117.Name = "label117";
			this.label117.Size = new System.Drawing.Size(24, 16);
			this.label117.TabIndex = 34;
			this.label117.Text = "A";
			// 
			// label118
			// 
			this.label118.Location = new System.Drawing.Point(8, 64);
			this.label118.Name = "label118";
			this.label118.Size = new System.Drawing.Size(120, 16);
			this.label118.TabIndex = 27;
			this.label118.Text = "Middle relative color";
			// 
			// txtStartRelColorA
			// 
			this.txtStartRelColorA.Location = new System.Drawing.Point(216, 32);
			this.txtStartRelColorA.Name = "txtStartRelColorA";
			this.txtStartRelColorA.Size = new System.Drawing.Size(40, 20);
			this.txtStartRelColorA.TabIndex = 25;
			this.txtStartRelColorA.Text = "";
			this.txtStartRelColorA.TextChanged += new System.EventHandler(this.txtStartRelColorA_TextChanged);
			// 
			// txtStartRelColorB
			// 
			this.txtStartRelColorB.Location = new System.Drawing.Point(152, 32);
			this.txtStartRelColorB.Name = "txtStartRelColorB";
			this.txtStartRelColorB.Size = new System.Drawing.Size(40, 20);
			this.txtStartRelColorB.TabIndex = 23;
			this.txtStartRelColorB.Text = "";
			this.txtStartRelColorB.TextChanged += new System.EventHandler(this.txtStartRelColorB_TextChanged);
			// 
			// txtStartRelColorG
			// 
			this.txtStartRelColorG.Location = new System.Drawing.Point(88, 32);
			this.txtStartRelColorG.Name = "txtStartRelColorG";
			this.txtStartRelColorG.Size = new System.Drawing.Size(40, 20);
			this.txtStartRelColorG.TabIndex = 21;
			this.txtStartRelColorG.Text = "";
			this.txtStartRelColorG.TextChanged += new System.EventHandler(this.txtStartRelColorG_TextChanged);
			// 
			// txtStartRelColorR
			// 
			this.txtStartRelColorR.Location = new System.Drawing.Point(24, 32);
			this.txtStartRelColorR.Name = "txtStartRelColorR";
			this.txtStartRelColorR.Size = new System.Drawing.Size(40, 20);
			this.txtStartRelColorR.TabIndex = 19;
			this.txtStartRelColorR.Text = "";
			this.txtStartRelColorR.TextChanged += new System.EventHandler(this.txtStartRelColorR_TextChanged);
			// 
			// label112
			// 
			this.label112.Location = new System.Drawing.Point(72, 32);
			this.label112.Name = "label112";
			this.label112.Size = new System.Drawing.Size(24, 16);
			this.label112.TabIndex = 22;
			this.label112.Text = "G";
			// 
			// label111
			// 
			this.label111.Location = new System.Drawing.Point(136, 32);
			this.label111.Name = "label111";
			this.label111.Size = new System.Drawing.Size(24, 16);
			this.label111.TabIndex = 24;
			this.label111.Text = "B";
			// 
			// label110
			// 
			this.label110.Location = new System.Drawing.Point(200, 32);
			this.label110.Name = "label110";
			this.label110.Size = new System.Drawing.Size(24, 16);
			this.label110.TabIndex = 26;
			this.label110.Text = "A";
			// 
			// label114
			// 
			this.label114.Location = new System.Drawing.Point(8, 32);
			this.label114.Name = "label114";
			this.label114.Size = new System.Drawing.Size(24, 16);
			this.label114.TabIndex = 20;
			this.label114.Text = "R";
			// 
			// label113
			// 
			this.label113.Location = new System.Drawing.Point(8, 16);
			this.label113.Name = "label113";
			this.label113.Size = new System.Drawing.Size(104, 16);
			this.label113.TabIndex = 18;
			this.label113.Text = "Start relative color";
			// 
			// groupBox18
			// 
			this.groupBox18.Controls.Add(this.txtMaxStartColorA);
			this.groupBox18.Controls.Add(this.label106);
			this.groupBox18.Controls.Add(this.txtMaxStartColorB);
			this.groupBox18.Controls.Add(this.label107);
			this.groupBox18.Controls.Add(this.txtMaxStartColorG);
			this.groupBox18.Controls.Add(this.label108);
			this.groupBox18.Controls.Add(this.txtMaxStartColorR);
			this.groupBox18.Controls.Add(this.label109);
			this.groupBox18.Controls.Add(this.txtMinStartColorA);
			this.groupBox18.Controls.Add(this.label105);
			this.groupBox18.Controls.Add(this.txtMinStartColorB);
			this.groupBox18.Controls.Add(this.label104);
			this.groupBox18.Controls.Add(this.txtMinStartColorG);
			this.groupBox18.Controls.Add(this.label103);
			this.groupBox18.Controls.Add(this.txtMinStartColorR);
			this.groupBox18.Controls.Add(this.label101);
			this.groupBox18.Controls.Add(this.label100);
			this.groupBox18.Controls.Add(this.label102);
			this.groupBox18.Location = new System.Drawing.Point(8, 8);
			this.groupBox18.Name = "groupBox18";
			this.groupBox18.Size = new System.Drawing.Size(440, 112);
			this.groupBox18.TabIndex = 0;
			this.groupBox18.TabStop = false;
			this.groupBox18.Text = "General";
			// 
			// txtMaxStartColorA
			// 
			this.txtMaxStartColorA.Location = new System.Drawing.Point(216, 80);
			this.txtMaxStartColorA.Name = "txtMaxStartColorA";
			this.txtMaxStartColorA.Size = new System.Drawing.Size(40, 20);
			this.txtMaxStartColorA.TabIndex = 16;
			this.txtMaxStartColorA.Text = "";
			this.txtMaxStartColorA.TextChanged += new System.EventHandler(this.txtMaxStartColorA_TextChanged);
			// 
			// label106
			// 
			this.label106.Location = new System.Drawing.Point(200, 80);
			this.label106.Name = "label106";
			this.label106.Size = new System.Drawing.Size(24, 16);
			this.label106.TabIndex = 17;
			this.label106.Text = "A";
			// 
			// txtMaxStartColorB
			// 
			this.txtMaxStartColorB.Location = new System.Drawing.Point(152, 80);
			this.txtMaxStartColorB.Name = "txtMaxStartColorB";
			this.txtMaxStartColorB.Size = new System.Drawing.Size(40, 20);
			this.txtMaxStartColorB.TabIndex = 14;
			this.txtMaxStartColorB.Text = "";
			this.txtMaxStartColorB.TextChanged += new System.EventHandler(this.txtMaxStartColorB_TextChanged);
			// 
			// label107
			// 
			this.label107.Location = new System.Drawing.Point(136, 80);
			this.label107.Name = "label107";
			this.label107.Size = new System.Drawing.Size(24, 16);
			this.label107.TabIndex = 15;
			this.label107.Text = "B";
			// 
			// txtMaxStartColorG
			// 
			this.txtMaxStartColorG.Location = new System.Drawing.Point(88, 80);
			this.txtMaxStartColorG.Name = "txtMaxStartColorG";
			this.txtMaxStartColorG.Size = new System.Drawing.Size(40, 20);
			this.txtMaxStartColorG.TabIndex = 12;
			this.txtMaxStartColorG.Text = "";
			this.txtMaxStartColorG.TextChanged += new System.EventHandler(this.txtMaxStartColorG_TextChanged);
			// 
			// label108
			// 
			this.label108.Location = new System.Drawing.Point(72, 80);
			this.label108.Name = "label108";
			this.label108.Size = new System.Drawing.Size(24, 16);
			this.label108.TabIndex = 13;
			this.label108.Text = "G";
			// 
			// txtMaxStartColorR
			// 
			this.txtMaxStartColorR.Location = new System.Drawing.Point(24, 80);
			this.txtMaxStartColorR.Name = "txtMaxStartColorR";
			this.txtMaxStartColorR.Size = new System.Drawing.Size(40, 20);
			this.txtMaxStartColorR.TabIndex = 10;
			this.txtMaxStartColorR.Text = "";
			this.txtMaxStartColorR.TextChanged += new System.EventHandler(this.txtMaxStartColorR_TextChanged);
			// 
			// label109
			// 
			this.label109.Location = new System.Drawing.Point(8, 80);
			this.label109.Name = "label109";
			this.label109.Size = new System.Drawing.Size(24, 16);
			this.label109.TabIndex = 11;
			this.label109.Text = "R";
			// 
			// txtMinStartColorA
			// 
			this.txtMinStartColorA.Location = new System.Drawing.Point(216, 32);
			this.txtMinStartColorA.Name = "txtMinStartColorA";
			this.txtMinStartColorA.Size = new System.Drawing.Size(40, 20);
			this.txtMinStartColorA.TabIndex = 8;
			this.txtMinStartColorA.Text = "";
			this.txtMinStartColorA.TextChanged += new System.EventHandler(this.txtMinStartColorA_TextChanged);
			// 
			// label105
			// 
			this.label105.Location = new System.Drawing.Point(200, 32);
			this.label105.Name = "label105";
			this.label105.Size = new System.Drawing.Size(24, 16);
			this.label105.TabIndex = 9;
			this.label105.Text = "A";
			// 
			// txtMinStartColorB
			// 
			this.txtMinStartColorB.Location = new System.Drawing.Point(152, 32);
			this.txtMinStartColorB.Name = "txtMinStartColorB";
			this.txtMinStartColorB.Size = new System.Drawing.Size(40, 20);
			this.txtMinStartColorB.TabIndex = 6;
			this.txtMinStartColorB.Text = "";
			this.txtMinStartColorB.TextChanged += new System.EventHandler(this.txtMinStartColorB_TextChanged);
			// 
			// label104
			// 
			this.label104.Location = new System.Drawing.Point(136, 32);
			this.label104.Name = "label104";
			this.label104.Size = new System.Drawing.Size(24, 16);
			this.label104.TabIndex = 7;
			this.label104.Text = "B";
			// 
			// txtMinStartColorG
			// 
			this.txtMinStartColorG.Location = new System.Drawing.Point(88, 32);
			this.txtMinStartColorG.Name = "txtMinStartColorG";
			this.txtMinStartColorG.Size = new System.Drawing.Size(40, 20);
			this.txtMinStartColorG.TabIndex = 4;
			this.txtMinStartColorG.Text = "";
			this.txtMinStartColorG.TextChanged += new System.EventHandler(this.txtMinStartColorG_TextChanged);
			// 
			// label103
			// 
			this.label103.Location = new System.Drawing.Point(72, 32);
			this.label103.Name = "label103";
			this.label103.Size = new System.Drawing.Size(24, 16);
			this.label103.TabIndex = 5;
			this.label103.Text = "G";
			// 
			// txtMinStartColorR
			// 
			this.txtMinStartColorR.Location = new System.Drawing.Point(24, 32);
			this.txtMinStartColorR.Name = "txtMinStartColorR";
			this.txtMinStartColorR.Size = new System.Drawing.Size(40, 20);
			this.txtMinStartColorR.TabIndex = 2;
			this.txtMinStartColorR.Text = "";
			this.txtMinStartColorR.TextChanged += new System.EventHandler(this.txtMinStartColorR_TextChanged);
			// 
			// label101
			// 
			this.label101.Location = new System.Drawing.Point(8, 64);
			this.label101.Name = "label101";
			this.label101.Size = new System.Drawing.Size(104, 16);
			this.label101.TabIndex = 1;
			this.label101.Text = "Max Color";
			// 
			// label100
			// 
			this.label100.Location = new System.Drawing.Point(8, 16);
			this.label100.Name = "label100";
			this.label100.Size = new System.Drawing.Size(104, 16);
			this.label100.TabIndex = 0;
			this.label100.Text = "Min Color";
			// 
			// label102
			// 
			this.label102.Location = new System.Drawing.Point(8, 32);
			this.label102.Name = "label102";
			this.label102.Size = new System.Drawing.Size(24, 16);
			this.label102.TabIndex = 3;
			this.label102.Text = "R";
			// 
			// tabPage4
			// 
			this.tabPage4.Controls.Add(this.groupBox11);
			this.tabPage4.Controls.Add(this.groupBox8);
			this.tabPage4.Controls.Add(this.groupBox9);
			this.tabPage4.Controls.Add(this.groupBox10);
			this.tabPage4.Controls.Add(this.groupBox12);
			this.tabPage4.Location = new System.Drawing.Point(4, 22);
			this.tabPage4.Name = "tabPage4";
			this.tabPage4.Size = new System.Drawing.Size(456, 358);
			this.tabPage4.TabIndex = 3;
			this.tabPage4.Text = "Movement";
			// 
			// groupBox11
			// 
			this.groupBox11.Controls.Add(this.txtGravityAccZ);
			this.groupBox11.Controls.Add(this.txtGravityAccY);
			this.groupBox11.Controls.Add(this.txtGravityAccX);
			this.groupBox11.Controls.Add(this.label68);
			this.groupBox11.Controls.Add(this.label67);
			this.groupBox11.Controls.Add(this.label66);
			this.groupBox11.Controls.Add(this.label65);
			this.groupBox11.Controls.Add(this.objGravityType);
			this.groupBox11.Controls.Add(this.label64);
			this.groupBox11.Location = new System.Drawing.Point(264, 8);
			this.groupBox11.Name = "groupBox11";
			this.groupBox11.Size = new System.Drawing.Size(184, 88);
			this.groupBox11.TabIndex = 32;
			this.groupBox11.TabStop = false;
			this.groupBox11.Text = "Gravity";
			// 
			// txtGravityAccZ
			// 
			this.txtGravityAccZ.Location = new System.Drawing.Point(136, 56);
			this.txtGravityAccZ.Name = "txtGravityAccZ";
			this.txtGravityAccZ.Size = new System.Drawing.Size(32, 20);
			this.txtGravityAccZ.TabIndex = 21;
			this.txtGravityAccZ.Text = "";
			this.txtGravityAccZ.TextChanged += new System.EventHandler(this.txtGravityAccZ_TextChanged);
			// 
			// txtGravityAccY
			// 
			this.txtGravityAccY.Location = new System.Drawing.Point(80, 56);
			this.txtGravityAccY.Name = "txtGravityAccY";
			this.txtGravityAccY.Size = new System.Drawing.Size(32, 20);
			this.txtGravityAccY.TabIndex = 19;
			this.txtGravityAccY.Text = "";
			this.txtGravityAccY.TextChanged += new System.EventHandler(this.txtGravityAccY_TextChanged);
			// 
			// txtGravityAccX
			// 
			this.txtGravityAccX.Location = new System.Drawing.Point(24, 56);
			this.txtGravityAccX.Name = "txtGravityAccX";
			this.txtGravityAccX.Size = new System.Drawing.Size(32, 20);
			this.txtGravityAccX.TabIndex = 17;
			this.txtGravityAccX.Text = "";
			this.txtGravityAccX.TextChanged += new System.EventHandler(this.txtGravityAccX_TextChanged);
			// 
			// label68
			// 
			this.label68.Location = new System.Drawing.Point(8, 40);
			this.label68.Name = "label68";
			this.label68.Size = new System.Drawing.Size(120, 16);
			this.label68.TabIndex = 16;
			this.label68.Text = "Acceleration  (m/s^2)";
			// 
			// label67
			// 
			this.label67.Location = new System.Drawing.Point(8, 56);
			this.label67.Name = "label67";
			this.label67.Size = new System.Drawing.Size(20, 16);
			this.label67.TabIndex = 18;
			this.label67.Text = "X";
			// 
			// label66
			// 
			this.label66.Location = new System.Drawing.Point(64, 56);
			this.label66.Name = "label66";
			this.label66.Size = new System.Drawing.Size(20, 16);
			this.label66.TabIndex = 20;
			this.label66.Text = "Y";
			// 
			// label65
			// 
			this.label65.Location = new System.Drawing.Point(120, 56);
			this.label65.Name = "label65";
			this.label65.Size = new System.Drawing.Size(20, 16);
			this.label65.TabIndex = 22;
			this.label65.Text = "Z";
			// 
			// objGravityType
			// 
			this.objGravityType.Items.AddRange(new object[] {
																"None",
																"Vector",
																"Center"});
			this.objGravityType.Location = new System.Drawing.Point(64, 16);
			this.objGravityType.Name = "objGravityType";
			this.objGravityType.Size = new System.Drawing.Size(104, 21);
			this.objGravityType.TabIndex = 2;
			this.objGravityType.SelectedIndexChanged += new System.EventHandler(this.objGravityType_SelectedIndexChanged);
			// 
			// label64
			// 
			this.label64.Location = new System.Drawing.Point(8, 16);
			this.label64.Name = "label64";
			this.label64.Size = new System.Drawing.Size(104, 16);
			this.label64.TabIndex = 3;
			this.label64.Text = "Type";
			// 
			// groupBox8
			// 
			this.groupBox8.Controls.Add(this.label82);
			this.groupBox8.Controls.Add(this.objCoordSystem);
			this.groupBox8.Controls.Add(this.objUsesDirection);
			this.groupBox8.Controls.Add(this.objStartVelType);
			this.groupBox8.Controls.Add(this.label46);
			this.groupBox8.Location = new System.Drawing.Point(8, 8);
			this.groupBox8.Name = "groupBox8";
			this.groupBox8.Size = new System.Drawing.Size(248, 88);
			this.groupBox8.TabIndex = 2;
			this.groupBox8.TabStop = false;
			this.groupBox8.Text = "General";
			// 
			// label82
			// 
			this.label82.Location = new System.Drawing.Point(136, 16);
			this.label82.Name = "label82";
			this.label82.Size = new System.Drawing.Size(88, 16);
			this.label82.TabIndex = 25;
			this.label82.Text = "Coord System";
			// 
			// objCoordSystem
			// 
			this.objCoordSystem.Items.AddRange(new object[] {
																"World",
																"Local"});
			this.objCoordSystem.Location = new System.Drawing.Point(136, 32);
			this.objCoordSystem.Name = "objCoordSystem";
			this.objCoordSystem.Size = new System.Drawing.Size(104, 21);
			this.objCoordSystem.TabIndex = 24;
			this.objCoordSystem.SelectedIndexChanged += new System.EventHandler(this.objCoordSystem_SelectedIndexChanged);
			// 
			// objUsesDirection
			// 
			this.objUsesDirection.Location = new System.Drawing.Point(8, 64);
			this.objUsesDirection.Name = "objUsesDirection";
			this.objUsesDirection.Size = new System.Drawing.Size(104, 16);
			this.objUsesDirection.TabIndex = 23;
			this.objUsesDirection.Text = "Uses Direction";
			this.objUsesDirection.CheckedChanged += new System.EventHandler(this.objUsesDirection_CheckedChanged);
			// 
			// objStartVelType
			// 
			this.objStartVelType.Items.AddRange(new object[] {
																 "Box",
																 "Sphere"});
			this.objStartVelType.Location = new System.Drawing.Point(8, 32);
			this.objStartVelType.Name = "objStartVelType";
			this.objStartVelType.Size = new System.Drawing.Size(104, 21);
			this.objStartVelType.TabIndex = 1;
			this.objStartVelType.SelectedIndexChanged += new System.EventHandler(this.objStartVelType_SelectedIndexChanged);
			// 
			// label46
			// 
			this.label46.Location = new System.Drawing.Point(8, 16);
			this.label46.Name = "label46";
			this.label46.Size = new System.Drawing.Size(88, 16);
			this.label46.TabIndex = 0;
			this.label46.Text = "Velocity type";
			// 
			// groupBox9
			// 
			this.groupBox9.Controls.Add(this.txtMaxStartVelZ);
			this.groupBox9.Controls.Add(this.label47);
			this.groupBox9.Controls.Add(this.txtMaxStartVelY);
			this.groupBox9.Controls.Add(this.label48);
			this.groupBox9.Controls.Add(this.txtMaxStartVelX);
			this.groupBox9.Controls.Add(this.label49);
			this.groupBox9.Controls.Add(this.label50);
			this.groupBox9.Controls.Add(this.txtMinStartVelZ);
			this.groupBox9.Controls.Add(this.label51);
			this.groupBox9.Controls.Add(this.txtMinStartVelY);
			this.groupBox9.Controls.Add(this.label52);
			this.groupBox9.Controls.Add(this.txtMinStartVelX);
			this.groupBox9.Controls.Add(this.label53);
			this.groupBox9.Controls.Add(this.label54);
			this.groupBox9.Location = new System.Drawing.Point(8, 104);
			this.groupBox9.Name = "groupBox9";
			this.groupBox9.Size = new System.Drawing.Size(440, 64);
			this.groupBox9.TabIndex = 16;
			this.groupBox9.TabStop = false;
			this.groupBox9.Text = "Box Velocity";
			// 
			// txtMaxStartVelZ
			// 
			this.txtMaxStartVelZ.Location = new System.Drawing.Point(384, 32);
			this.txtMaxStartVelZ.Name = "txtMaxStartVelZ";
			this.txtMaxStartVelZ.Size = new System.Drawing.Size(40, 20);
			this.txtMaxStartVelZ.TabIndex = 14;
			this.txtMaxStartVelZ.Text = "";
			this.txtMaxStartVelZ.TextChanged += new System.EventHandler(this.txtMaxStartVelZ_TextChanged);
			// 
			// label47
			// 
			this.label47.Location = new System.Drawing.Point(368, 32);
			this.label47.Name = "label47";
			this.label47.Size = new System.Drawing.Size(20, 16);
			this.label47.TabIndex = 15;
			this.label47.Text = "Z";
			// 
			// txtMaxStartVelY
			// 
			this.txtMaxStartVelY.Location = new System.Drawing.Point(320, 32);
			this.txtMaxStartVelY.Name = "txtMaxStartVelY";
			this.txtMaxStartVelY.Size = new System.Drawing.Size(40, 20);
			this.txtMaxStartVelY.TabIndex = 12;
			this.txtMaxStartVelY.Text = "";
			this.txtMaxStartVelY.TextChanged += new System.EventHandler(this.txtMaxStartVelY_TextChanged);
			// 
			// label48
			// 
			this.label48.Location = new System.Drawing.Point(304, 32);
			this.label48.Name = "label48";
			this.label48.Size = new System.Drawing.Size(20, 16);
			this.label48.TabIndex = 13;
			this.label48.Text = "Y";
			// 
			// txtMaxStartVelX
			// 
			this.txtMaxStartVelX.Location = new System.Drawing.Point(256, 32);
			this.txtMaxStartVelX.Name = "txtMaxStartVelX";
			this.txtMaxStartVelX.Size = new System.Drawing.Size(40, 20);
			this.txtMaxStartVelX.TabIndex = 10;
			this.txtMaxStartVelX.Text = "";
			this.txtMaxStartVelX.TextChanged += new System.EventHandler(this.txtMaxStartVelX_TextChanged);
			// 
			// label49
			// 
			this.label49.Location = new System.Drawing.Point(240, 32);
			this.label49.Name = "label49";
			this.label49.Size = new System.Drawing.Size(20, 16);
			this.label49.TabIndex = 11;
			this.label49.Text = "X";
			// 
			// label50
			// 
			this.label50.Location = new System.Drawing.Point(240, 16);
			this.label50.Name = "label50";
			this.label50.Size = new System.Drawing.Size(112, 16);
			this.label50.TabIndex = 9;
			this.label50.Text = "Max Velocity (m/s)";
			// 
			// txtMinStartVelZ
			// 
			this.txtMinStartVelZ.Location = new System.Drawing.Point(152, 32);
			this.txtMinStartVelZ.Name = "txtMinStartVelZ";
			this.txtMinStartVelZ.Size = new System.Drawing.Size(40, 20);
			this.txtMinStartVelZ.TabIndex = 7;
			this.txtMinStartVelZ.Text = "";
			this.txtMinStartVelZ.TextChanged += new System.EventHandler(this.txtMinStartVelZ_TextChanged);
			// 
			// label51
			// 
			this.label51.Location = new System.Drawing.Point(136, 32);
			this.label51.Name = "label51";
			this.label51.Size = new System.Drawing.Size(20, 16);
			this.label51.TabIndex = 8;
			this.label51.Text = "Z";
			// 
			// txtMinStartVelY
			// 
			this.txtMinStartVelY.Location = new System.Drawing.Point(88, 32);
			this.txtMinStartVelY.Name = "txtMinStartVelY";
			this.txtMinStartVelY.Size = new System.Drawing.Size(40, 20);
			this.txtMinStartVelY.TabIndex = 5;
			this.txtMinStartVelY.Text = "";
			this.txtMinStartVelY.TextChanged += new System.EventHandler(this.txtMinStartVelY_TextChanged);
			// 
			// label52
			// 
			this.label52.Location = new System.Drawing.Point(72, 32);
			this.label52.Name = "label52";
			this.label52.Size = new System.Drawing.Size(20, 16);
			this.label52.TabIndex = 6;
			this.label52.Text = "Y";
			// 
			// txtMinStartVelX
			// 
			this.txtMinStartVelX.Location = new System.Drawing.Point(24, 32);
			this.txtMinStartVelX.Name = "txtMinStartVelX";
			this.txtMinStartVelX.Size = new System.Drawing.Size(40, 20);
			this.txtMinStartVelX.TabIndex = 3;
			this.txtMinStartVelX.Text = "";
			this.txtMinStartVelX.TextChanged += new System.EventHandler(this.txtMinStartVelX_TextChanged);
			// 
			// label53
			// 
			this.label53.Location = new System.Drawing.Point(8, 32);
			this.label53.Name = "label53";
			this.label53.Size = new System.Drawing.Size(20, 16);
			this.label53.TabIndex = 4;
			this.label53.Text = "X";
			// 
			// label54
			// 
			this.label54.Location = new System.Drawing.Point(8, 16);
			this.label54.Name = "label54";
			this.label54.Size = new System.Drawing.Size(104, 16);
			this.label54.TabIndex = 2;
			this.label54.Text = "Min Velocity  (m/s)";
			// 
			// groupBox10
			// 
			this.groupBox10.Controls.Add(this.txtMaxStartVelSpeed);
			this.groupBox10.Controls.Add(this.label55);
			this.groupBox10.Controls.Add(this.label56);
			this.groupBox10.Controls.Add(this.label57);
			this.groupBox10.Controls.Add(this.txtMinStartVelSpeed);
			this.groupBox10.Controls.Add(this.txtMaxStartVelAnglesX);
			this.groupBox10.Controls.Add(this.txtMaxStartVelAnglesY);
			this.groupBox10.Controls.Add(this.label58);
			this.groupBox10.Controls.Add(this.label59);
			this.groupBox10.Controls.Add(this.txtMinStartVelAnglesX);
			this.groupBox10.Controls.Add(this.txtMinStartVelAnglesY);
			this.groupBox10.Controls.Add(this.label60);
			this.groupBox10.Controls.Add(this.label61);
			this.groupBox10.Controls.Add(this.label62);
			this.groupBox10.Controls.Add(this.label63);
			this.groupBox10.Location = new System.Drawing.Point(8, 176);
			this.groupBox10.Name = "groupBox10";
			this.groupBox10.Size = new System.Drawing.Size(440, 64);
			this.groupBox10.TabIndex = 31;
			this.groupBox10.TabStop = false;
			this.groupBox10.Text = "Sphere Velocity";
			// 
			// txtMaxStartVelSpeed
			// 
			this.txtMaxStartVelSpeed.Location = new System.Drawing.Point(384, 32);
			this.txtMaxStartVelSpeed.Name = "txtMaxStartVelSpeed";
			this.txtMaxStartVelSpeed.Size = new System.Drawing.Size(40, 20);
			this.txtMaxStartVelSpeed.TabIndex = 28;
			this.txtMaxStartVelSpeed.Text = "";
			this.txtMaxStartVelSpeed.TextChanged += new System.EventHandler(this.txtMaxStartVelSpeed_TextChanged);
			// 
			// label55
			// 
			this.label55.Location = new System.Drawing.Point(360, 32);
			this.label55.Name = "label55";
			this.label55.Size = new System.Drawing.Size(32, 16);
			this.label55.TabIndex = 30;
			this.label55.Text = "Max";
			// 
			// label56
			// 
			this.label56.Location = new System.Drawing.Point(288, 32);
			this.label56.Name = "label56";
			this.label56.Size = new System.Drawing.Size(24, 16);
			this.label56.TabIndex = 29;
			this.label56.Text = "Min";
			// 
			// label57
			// 
			this.label57.Location = new System.Drawing.Point(288, 16);
			this.label57.Name = "label57";
			this.label57.Size = new System.Drawing.Size(96, 16);
			this.label57.TabIndex = 27;
			this.label57.Text = "Speed (m/s)";
			// 
			// txtMinStartVelSpeed
			// 
			this.txtMinStartVelSpeed.Location = new System.Drawing.Point(312, 32);
			this.txtMinStartVelSpeed.Name = "txtMinStartVelSpeed";
			this.txtMinStartVelSpeed.Size = new System.Drawing.Size(40, 20);
			this.txtMinStartVelSpeed.TabIndex = 26;
			this.txtMinStartVelSpeed.Text = "";
			this.txtMinStartVelSpeed.TextChanged += new System.EventHandler(this.txtMinStartVelSpeed_TextChanged);
			// 
			// txtMaxStartVelAnglesX
			// 
			this.txtMaxStartVelAnglesX.Location = new System.Drawing.Point(160, 30);
			this.txtMaxStartVelAnglesX.Name = "txtMaxStartVelAnglesX";
			this.txtMaxStartVelAnglesX.Size = new System.Drawing.Size(40, 20);
			this.txtMaxStartVelAnglesX.TabIndex = 22;
			this.txtMaxStartVelAnglesX.Text = "";
			this.txtMaxStartVelAnglesX.TextChanged += new System.EventHandler(this.txtMaxStartVelAnglesX_TextChanged);
			// 
			// txtMaxStartVelAnglesY
			// 
			this.txtMaxStartVelAnglesY.Location = new System.Drawing.Point(224, 30);
			this.txtMaxStartVelAnglesY.Name = "txtMaxStartVelAnglesY";
			this.txtMaxStartVelAnglesY.Size = new System.Drawing.Size(40, 20);
			this.txtMaxStartVelAnglesY.TabIndex = 24;
			this.txtMaxStartVelAnglesY.Text = "";
			this.txtMaxStartVelAnglesY.TextChanged += new System.EventHandler(this.txtMaxStartVelAnglesY_TextChanged);
			// 
			// label58
			// 
			this.label58.Location = new System.Drawing.Point(144, 14);
			this.label58.Name = "label58";
			this.label58.Size = new System.Drawing.Size(96, 16);
			this.label58.TabIndex = 21;
			this.label58.Text = "Max Angles (deg)";
			// 
			// label59
			// 
			this.label59.Location = new System.Drawing.Point(144, 30);
			this.label59.Name = "label59";
			this.label59.Size = new System.Drawing.Size(20, 16);
			this.label59.TabIndex = 23;
			this.label59.Text = "X";
			// 
			// txtMinStartVelAnglesX
			// 
			this.txtMinStartVelAnglesX.Location = new System.Drawing.Point(24, 32);
			this.txtMinStartVelAnglesX.Name = "txtMinStartVelAnglesX";
			this.txtMinStartVelAnglesX.Size = new System.Drawing.Size(40, 20);
			this.txtMinStartVelAnglesX.TabIndex = 17;
			this.txtMinStartVelAnglesX.Text = "";
			this.txtMinStartVelAnglesX.TextChanged += new System.EventHandler(this.txtMinStartVelAnglesX_TextChanged);
			// 
			// txtMinStartVelAnglesY
			// 
			this.txtMinStartVelAnglesY.Location = new System.Drawing.Point(88, 32);
			this.txtMinStartVelAnglesY.Name = "txtMinStartVelAnglesY";
			this.txtMinStartVelAnglesY.Size = new System.Drawing.Size(40, 20);
			this.txtMinStartVelAnglesY.TabIndex = 19;
			this.txtMinStartVelAnglesY.Text = "";
			this.txtMinStartVelAnglesY.TextChanged += new System.EventHandler(this.txtMinStartVelAnglesY_TextChanged);
			// 
			// label60
			// 
			this.label60.Location = new System.Drawing.Point(8, 16);
			this.label60.Name = "label60";
			this.label60.Size = new System.Drawing.Size(96, 16);
			this.label60.TabIndex = 16;
			this.label60.Text = "Min Angles (deg)";
			// 
			// label61
			// 
			this.label61.Location = new System.Drawing.Point(8, 32);
			this.label61.Name = "label61";
			this.label61.Size = new System.Drawing.Size(20, 16);
			this.label61.TabIndex = 18;
			this.label61.Text = "X";
			// 
			// label62
			// 
			this.label62.Location = new System.Drawing.Point(72, 32);
			this.label62.Name = "label62";
			this.label62.Size = new System.Drawing.Size(20, 16);
			this.label62.TabIndex = 20;
			this.label62.Text = "Y";
			// 
			// label63
			// 
			this.label63.Location = new System.Drawing.Point(208, 32);
			this.label63.Name = "label63";
			this.label63.Size = new System.Drawing.Size(20, 16);
			this.label63.TabIndex = 25;
			this.label63.Text = "Y";
			// 
			// groupBox12
			// 
			this.groupBox12.Controls.Add(this.txtMaxSpeedMultiply);
			this.groupBox12.Controls.Add(this.txtMinSpeedMultiply);
			this.groupBox12.Controls.Add(this.label76);
			this.groupBox12.Controls.Add(this.txtMaxStartAccZ);
			this.groupBox12.Controls.Add(this.label72);
			this.groupBox12.Controls.Add(this.txtMaxStartAccY);
			this.groupBox12.Controls.Add(this.label73);
			this.groupBox12.Controls.Add(this.txtMaxStartAccX);
			this.groupBox12.Controls.Add(this.label74);
			this.groupBox12.Controls.Add(this.label75);
			this.groupBox12.Controls.Add(this.txtMinStartAccZ);
			this.groupBox12.Controls.Add(this.lbiasas);
			this.groupBox12.Controls.Add(this.txtMinStartAccY);
			this.groupBox12.Controls.Add(this.label77);
			this.groupBox12.Controls.Add(this.txtMinStartAccX);
			this.groupBox12.Controls.Add(this.label78);
			this.groupBox12.Controls.Add(this.label79);
			this.groupBox12.Controls.Add(this.label80);
			this.groupBox12.Controls.Add(this.label81);
			this.groupBox12.Controls.Add(this.label69);
			this.groupBox12.Controls.Add(this.txtMinVelMaximum);
			this.groupBox12.Controls.Add(this.txtMaxVelMaximum);
			this.groupBox12.Controls.Add(this.label70);
			this.groupBox12.Controls.Add(this.label71);
			this.groupBox12.Location = new System.Drawing.Point(8, 248);
			this.groupBox12.Name = "groupBox12";
			this.groupBox12.Size = new System.Drawing.Size(440, 104);
			this.groupBox12.TabIndex = 17;
			this.groupBox12.TabStop = false;
			this.groupBox12.Text = "Acceleration";
			// 
			// txtMaxSpeedMultiply
			// 
			this.txtMaxSpeedMultiply.Location = new System.Drawing.Point(136, 72);
			this.txtMaxSpeedMultiply.Name = "txtMaxSpeedMultiply";
			this.txtMaxSpeedMultiply.Size = new System.Drawing.Size(56, 20);
			this.txtMaxSpeedMultiply.TabIndex = 18;
			this.txtMaxSpeedMultiply.Text = "";
			this.txtMaxSpeedMultiply.TextChanged += new System.EventHandler(this.txtMaxSpeedMultiply_TextChanged);
			// 
			// txtMinSpeedMultiply
			// 
			this.txtMinSpeedMultiply.Location = new System.Drawing.Point(32, 72);
			this.txtMinSpeedMultiply.Name = "txtMinSpeedMultiply";
			this.txtMinSpeedMultiply.Size = new System.Drawing.Size(56, 20);
			this.txtMinSpeedMultiply.TabIndex = 17;
			this.txtMinSpeedMultiply.Text = "";
			this.txtMinSpeedMultiply.TextChanged += new System.EventHandler(this.txtMinSpeedMultiply_TextChanged);
			// 
			// label76
			// 
			this.label76.Location = new System.Drawing.Point(8, 56);
			this.label76.Name = "label76";
			this.label76.Size = new System.Drawing.Size(88, 16);
			this.label76.TabIndex = 16;
			this.label76.Text = "Speed multiplier";
			// 
			// txtMaxStartAccZ
			// 
			this.txtMaxStartAccZ.Location = new System.Drawing.Point(384, 32);
			this.txtMaxStartAccZ.Name = "txtMaxStartAccZ";
			this.txtMaxStartAccZ.Size = new System.Drawing.Size(40, 20);
			this.txtMaxStartAccZ.TabIndex = 14;
			this.txtMaxStartAccZ.Text = "";
			this.txtMaxStartAccZ.TextChanged += new System.EventHandler(this.txtMaxStartAccZ_TextChanged);
			// 
			// label72
			// 
			this.label72.Location = new System.Drawing.Point(368, 32);
			this.label72.Name = "label72";
			this.label72.Size = new System.Drawing.Size(20, 16);
			this.label72.TabIndex = 15;
			this.label72.Text = "Z";
			// 
			// txtMaxStartAccY
			// 
			this.txtMaxStartAccY.Location = new System.Drawing.Point(320, 32);
			this.txtMaxStartAccY.Name = "txtMaxStartAccY";
			this.txtMaxStartAccY.Size = new System.Drawing.Size(40, 20);
			this.txtMaxStartAccY.TabIndex = 12;
			this.txtMaxStartAccY.Text = "";
			this.txtMaxStartAccY.TextChanged += new System.EventHandler(this.txtMaxStartAccY_TextChanged);
			// 
			// label73
			// 
			this.label73.Location = new System.Drawing.Point(304, 32);
			this.label73.Name = "label73";
			this.label73.Size = new System.Drawing.Size(20, 16);
			this.label73.TabIndex = 13;
			this.label73.Text = "Y";
			// 
			// txtMaxStartAccX
			// 
			this.txtMaxStartAccX.Location = new System.Drawing.Point(256, 32);
			this.txtMaxStartAccX.Name = "txtMaxStartAccX";
			this.txtMaxStartAccX.Size = new System.Drawing.Size(40, 20);
			this.txtMaxStartAccX.TabIndex = 10;
			this.txtMaxStartAccX.Text = "";
			this.txtMaxStartAccX.TextChanged += new System.EventHandler(this.txtMaxStartAccX_TextChanged);
			// 
			// label74
			// 
			this.label74.Location = new System.Drawing.Point(240, 32);
			this.label74.Name = "label74";
			this.label74.Size = new System.Drawing.Size(20, 16);
			this.label74.TabIndex = 11;
			this.label74.Text = "X";
			// 
			// label75
			// 
			this.label75.Location = new System.Drawing.Point(240, 16);
			this.label75.Name = "label75";
			this.label75.Size = new System.Drawing.Size(144, 16);
			this.label75.TabIndex = 9;
			this.label75.Text = "Max Acceleration  (m/s^2)";
			// 
			// txtMinStartAccZ
			// 
			this.txtMinStartAccZ.Location = new System.Drawing.Point(152, 32);
			this.txtMinStartAccZ.Name = "txtMinStartAccZ";
			this.txtMinStartAccZ.Size = new System.Drawing.Size(40, 20);
			this.txtMinStartAccZ.TabIndex = 7;
			this.txtMinStartAccZ.Text = "";
			this.txtMinStartAccZ.TextChanged += new System.EventHandler(this.txtMinStartAccZ_TextChanged);
			// 
			// lbiasas
			// 
			this.lbiasas.Location = new System.Drawing.Point(136, 32);
			this.lbiasas.Name = "lbiasas";
			this.lbiasas.Size = new System.Drawing.Size(20, 16);
			this.lbiasas.TabIndex = 8;
			this.lbiasas.Text = "Z";
			// 
			// txtMinStartAccY
			// 
			this.txtMinStartAccY.Location = new System.Drawing.Point(88, 32);
			this.txtMinStartAccY.Name = "txtMinStartAccY";
			this.txtMinStartAccY.Size = new System.Drawing.Size(40, 20);
			this.txtMinStartAccY.TabIndex = 5;
			this.txtMinStartAccY.Text = "";
			this.txtMinStartAccY.TextChanged += new System.EventHandler(this.txtMinStartAccY_TextChanged);
			// 
			// label77
			// 
			this.label77.Location = new System.Drawing.Point(72, 32);
			this.label77.Name = "label77";
			this.label77.Size = new System.Drawing.Size(20, 16);
			this.label77.TabIndex = 6;
			this.label77.Text = "Y";
			// 
			// txtMinStartAccX
			// 
			this.txtMinStartAccX.Location = new System.Drawing.Point(24, 32);
			this.txtMinStartAccX.Name = "txtMinStartAccX";
			this.txtMinStartAccX.Size = new System.Drawing.Size(40, 20);
			this.txtMinStartAccX.TabIndex = 3;
			this.txtMinStartAccX.Text = "";
			this.txtMinStartAccX.TextChanged += new System.EventHandler(this.txtMinStartAccX_TextChanged);
			// 
			// label78
			// 
			this.label78.Location = new System.Drawing.Point(8, 32);
			this.label78.Name = "label78";
			this.label78.Size = new System.Drawing.Size(20, 16);
			this.label78.TabIndex = 4;
			this.label78.Text = "X";
			// 
			// label79
			// 
			this.label79.Location = new System.Drawing.Point(8, 16);
			this.label79.Name = "label79";
			this.label79.Size = new System.Drawing.Size(160, 16);
			this.label79.TabIndex = 2;
			this.label79.Text = "Min Acceleration  (m/s^2)";
			// 
			// label80
			// 
			this.label80.Location = new System.Drawing.Point(8, 72);
			this.label80.Name = "label80";
			this.label80.Size = new System.Drawing.Size(32, 16);
			this.label80.TabIndex = 28;
			this.label80.Text = "Min";
			// 
			// label81
			// 
			this.label81.Location = new System.Drawing.Point(104, 72);
			this.label81.Name = "label81";
			this.label81.Size = new System.Drawing.Size(32, 16);
			this.label81.TabIndex = 28;
			this.label81.Text = "Max";
			// 
			// label69
			// 
			this.label69.Location = new System.Drawing.Point(240, 56);
			this.label69.Name = "label69";
			this.label69.Size = new System.Drawing.Size(96, 16);
			this.label69.TabIndex = 24;
			this.label69.Text = "Maximum Speed";
			// 
			// txtMinVelMaximum
			// 
			this.txtMinVelMaximum.Location = new System.Drawing.Point(264, 72);
			this.txtMinVelMaximum.Name = "txtMinVelMaximum";
			this.txtMinVelMaximum.Size = new System.Drawing.Size(64, 20);
			this.txtMinVelMaximum.TabIndex = 16;
			this.txtMinVelMaximum.Text = "";
			this.txtMinVelMaximum.TextChanged += new System.EventHandler(this.txtMinVelMaximum_TextChanged);
			// 
			// txtMaxVelMaximum
			// 
			this.txtMaxVelMaximum.Location = new System.Drawing.Point(360, 72);
			this.txtMaxVelMaximum.Name = "txtMaxVelMaximum";
			this.txtMaxVelMaximum.Size = new System.Drawing.Size(64, 20);
			this.txtMaxVelMaximum.TabIndex = 26;
			this.txtMaxVelMaximum.Text = "";
			this.txtMaxVelMaximum.TextChanged += new System.EventHandler(this.txtMaxVelMaximum_TextChanged);
			// 
			// label70
			// 
			this.label70.Location = new System.Drawing.Point(240, 72);
			this.label70.Name = "label70";
			this.label70.Size = new System.Drawing.Size(32, 16);
			this.label70.TabIndex = 25;
			this.label70.Text = "Min";
			// 
			// label71
			// 
			this.label71.Location = new System.Drawing.Point(336, 72);
			this.label71.Name = "label71";
			this.label71.Size = new System.Drawing.Size(32, 16);
			this.label71.TabIndex = 27;
			this.label71.Text = "Max";
			// 
			// tabPage5
			// 
			this.tabPage5.Controls.Add(this.groupBox14);
			this.tabPage5.Controls.Add(this.groupBox13);
			this.tabPage5.Location = new System.Drawing.Point(4, 22);
			this.tabPage5.Name = "tabPage5";
			this.tabPage5.Size = new System.Drawing.Size(456, 358);
			this.tabPage5.TabIndex = 4;
			this.tabPage5.Text = "Life";
			// 
			// groupBox14
			// 
			this.groupBox14.Controls.Add(this.label87);
			this.groupBox14.Controls.Add(this.objDeathType);
			this.groupBox14.Controls.Add(this.label86);
			this.groupBox14.Controls.Add(this.txtDeathPS);
			this.groupBox14.Location = new System.Drawing.Point(168, 8);
			this.groupBox14.Name = "groupBox14";
			this.groupBox14.Size = new System.Drawing.Size(280, 96);
			this.groupBox14.TabIndex = 1;
			this.groupBox14.TabStop = false;
			this.groupBox14.Text = "Death";
			// 
			// label87
			// 
			this.label87.Location = new System.Drawing.Point(112, 16);
			this.label87.Name = "label87";
			this.label87.Size = new System.Drawing.Size(116, 16);
			this.label87.TabIndex = 7;
			this.label87.Text = "PS created at death";
			// 
			// objDeathType
			// 
			this.objDeathType.Items.AddRange(new object[] {
															  "Age"});
			this.objDeathType.Location = new System.Drawing.Point(8, 32);
			this.objDeathType.Name = "objDeathType";
			this.objDeathType.Size = new System.Drawing.Size(80, 21);
			this.objDeathType.TabIndex = 6;
			this.objDeathType.SelectedIndexChanged += new System.EventHandler(this.objDeathType_SelectedIndexChanged);
			// 
			// label86
			// 
			this.label86.Location = new System.Drawing.Point(8, 16);
			this.label86.Name = "label86";
			this.label86.Size = new System.Drawing.Size(72, 16);
			this.label86.TabIndex = 5;
			this.label86.Text = "Type";
			// 
			// txtDeathPS
			// 
			this.txtDeathPS.Location = new System.Drawing.Point(112, 32);
			this.txtDeathPS.Name = "txtDeathPS";
			this.txtDeathPS.Size = new System.Drawing.Size(144, 20);
			this.txtDeathPS.TabIndex = 5;
			this.txtDeathPS.Text = "";
			this.txtDeathPS.TextChanged += new System.EventHandler(this.txtDeathPS_TextChanged);
			// 
			// groupBox13
			// 
			this.groupBox13.Controls.Add(this.txtMaxLifeSpan);
			this.groupBox13.Controls.Add(this.label85);
			this.groupBox13.Controls.Add(this.txtMinLifeSpan);
			this.groupBox13.Controls.Add(this.label84);
			this.groupBox13.Controls.Add(this.label83);
			this.groupBox13.Location = new System.Drawing.Point(8, 8);
			this.groupBox13.Name = "groupBox13";
			this.groupBox13.Size = new System.Drawing.Size(152, 96);
			this.groupBox13.TabIndex = 0;
			this.groupBox13.TabStop = false;
			this.groupBox13.Text = "Life";
			// 
			// txtMaxLifeSpan
			// 
			this.txtMaxLifeSpan.Location = new System.Drawing.Point(40, 56);
			this.txtMaxLifeSpan.Name = "txtMaxLifeSpan";
			this.txtMaxLifeSpan.Size = new System.Drawing.Size(96, 20);
			this.txtMaxLifeSpan.TabIndex = 3;
			this.txtMaxLifeSpan.Text = "";
			this.txtMaxLifeSpan.TextChanged += new System.EventHandler(this.txtMaxLifeSpan_TextChanged);
			// 
			// label85
			// 
			this.label85.Location = new System.Drawing.Point(8, 56);
			this.label85.Name = "label85";
			this.label85.Size = new System.Drawing.Size(72, 16);
			this.label85.TabIndex = 4;
			this.label85.Text = "Max";
			// 
			// txtMinLifeSpan
			// 
			this.txtMinLifeSpan.Location = new System.Drawing.Point(40, 32);
			this.txtMinLifeSpan.Name = "txtMinLifeSpan";
			this.txtMinLifeSpan.Size = new System.Drawing.Size(96, 20);
			this.txtMinLifeSpan.TabIndex = 1;
			this.txtMinLifeSpan.Text = "";
			this.txtMinLifeSpan.TextChanged += new System.EventHandler(this.txtMinLifeSpan_TextChanged);
			// 
			// label84
			// 
			this.label84.Location = new System.Drawing.Point(8, 32);
			this.label84.Name = "label84";
			this.label84.Size = new System.Drawing.Size(72, 16);
			this.label84.TabIndex = 2;
			this.label84.Text = "Min";
			// 
			// label83
			// 
			this.label83.Location = new System.Drawing.Point(8, 16);
			this.label83.Name = "label83";
			this.label83.Size = new System.Drawing.Size(72, 16);
			this.label83.TabIndex = 0;
			this.label83.Text = "Life Span (s)";
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.groupBox4);
			this.tabPage2.Controls.Add(this.groupBox3);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Size = new System.Drawing.Size(456, 358);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Material";
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.objSubDivType);
			this.groupBox4.Controls.Add(this.label27);
			this.groupBox4.Controls.Add(this.txtSubDivY);
			this.groupBox4.Controls.Add(this.label26);
			this.groupBox4.Controls.Add(this.txtSubDivX);
			this.groupBox4.Controls.Add(this.label25);
			this.groupBox4.Controls.Add(this.label24);
			this.groupBox4.Location = new System.Drawing.Point(256, 8);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(192, 120);
			this.groupBox4.TabIndex = 1;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Sub Division";
			// 
			// objSubDivType
			// 
			this.objSubDivType.Items.AddRange(new object[] {
															   "Random",
															   "Animation"});
			this.objSubDivType.Location = new System.Drawing.Point(8, 88);
			this.objSubDivType.Name = "objSubDivType";
			this.objSubDivType.Size = new System.Drawing.Size(104, 21);
			this.objSubDivType.TabIndex = 12;
			this.objSubDivType.SelectedIndexChanged += new System.EventHandler(this.objSubDivType_SelectedIndexChanged);
			// 
			// label27
			// 
			this.label27.Location = new System.Drawing.Point(8, 72);
			this.label27.Name = "label27";
			this.label27.Size = new System.Drawing.Size(64, 16);
			this.label27.TabIndex = 11;
			this.label27.Text = "Type";
			// 
			// txtSubDivY
			// 
			this.txtSubDivY.Location = new System.Drawing.Point(104, 40);
			this.txtSubDivY.Name = "txtSubDivY";
			this.txtSubDivY.Size = new System.Drawing.Size(48, 20);
			this.txtSubDivY.TabIndex = 9;
			this.txtSubDivY.Text = "";
			this.txtSubDivY.TextChanged += new System.EventHandler(this.txtSubDivY_TextChanged);
			// 
			// label26
			// 
			this.label26.Location = new System.Drawing.Point(88, 40);
			this.label26.Name = "label26";
			this.label26.Size = new System.Drawing.Size(24, 16);
			this.label26.TabIndex = 10;
			this.label26.Text = "Y:";
			// 
			// txtSubDivX
			// 
			this.txtSubDivX.Location = new System.Drawing.Point(24, 40);
			this.txtSubDivX.Name = "txtSubDivX";
			this.txtSubDivX.Size = new System.Drawing.Size(48, 20);
			this.txtSubDivX.TabIndex = 7;
			this.txtSubDivX.Text = "";
			this.txtSubDivX.TextChanged += new System.EventHandler(this.txtSubDivX_TextChanged);
			// 
			// label25
			// 
			this.label25.Location = new System.Drawing.Point(8, 40);
			this.label25.Name = "label25";
			this.label25.Size = new System.Drawing.Size(24, 16);
			this.label25.TabIndex = 8;
			this.label25.Text = "X:";
			// 
			// label24
			// 
			this.label24.Location = new System.Drawing.Point(8, 24);
			this.label24.Name = "label24";
			this.label24.Size = new System.Drawing.Size(64, 16);
			this.label24.TabIndex = 7;
			this.label24.Text = "Amount";
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.label23);
			this.groupBox3.Controls.Add(this.txtAnimationLength);
			this.groupBox3.Controls.Add(this.txtMaterialNum);
			this.groupBox3.Controls.Add(this.label22);
			this.groupBox3.Controls.Add(this.objBrowseMaterial);
			this.groupBox3.Controls.Add(this.txtMaterial);
			this.groupBox3.Controls.Add(this.label21);
			this.groupBox3.Location = new System.Drawing.Point(8, 8);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(240, 120);
			this.groupBox3.TabIndex = 0;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Material";
			// 
			// label23
			// 
			this.label23.Location = new System.Drawing.Point(104, 72);
			this.label23.Name = "label23";
			this.label23.Size = new System.Drawing.Size(112, 16);
			this.label23.TabIndex = 6;
			this.label23.Text = "Animation Length (s)";
			// 
			// txtAnimationLength
			// 
			this.txtAnimationLength.Location = new System.Drawing.Point(104, 88);
			this.txtAnimationLength.Name = "txtAnimationLength";
			this.txtAnimationLength.Size = new System.Drawing.Size(88, 20);
			this.txtAnimationLength.TabIndex = 5;
			this.txtAnimationLength.Text = "";
			this.txtAnimationLength.TextChanged += new System.EventHandler(this.txtAnimationLength_TextChanged);
			// 
			// txtMaterialNum
			// 
			this.txtMaterialNum.Location = new System.Drawing.Point(8, 88);
			this.txtMaterialNum.Name = "txtMaterialNum";
			this.txtMaterialNum.Size = new System.Drawing.Size(80, 20);
			this.txtMaterialNum.TabIndex = 4;
			this.txtMaterialNum.Text = "";
			this.txtMaterialNum.TextChanged += new System.EventHandler(this.txtMaterialNum_TextChanged);
			// 
			// label22
			// 
			this.label22.Location = new System.Drawing.Point(8, 72);
			this.label22.Name = "label22";
			this.label22.Size = new System.Drawing.Size(80, 16);
			this.label22.TabIndex = 3;
			this.label22.Text = "Number of files";
			// 
			// objBrowseMaterial
			// 
			this.objBrowseMaterial.Location = new System.Drawing.Point(184, 40);
			this.objBrowseMaterial.Name = "objBrowseMaterial";
			this.objBrowseMaterial.Size = new System.Drawing.Size(32, 24);
			this.objBrowseMaterial.TabIndex = 2;
			this.objBrowseMaterial.Text = "...";
			this.objBrowseMaterial.Click += new System.EventHandler(this.objBrowseMaterial_Click);
			// 
			// txtMaterial
			// 
			this.txtMaterial.Location = new System.Drawing.Point(8, 40);
			this.txtMaterial.Name = "txtMaterial";
			this.txtMaterial.Size = new System.Drawing.Size(160, 20);
			this.txtMaterial.TabIndex = 1;
			this.txtMaterial.Text = "";
			this.txtMaterial.TextChanged += new System.EventHandler(this.txtMaterial_TextChanged);
			// 
			// label21
			// 
			this.label21.Location = new System.Drawing.Point(8, 24);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(64, 16);
			this.label21.TabIndex = 0;
			this.label21.Text = "File";
			// 
			// objEmitterList
			// 
			this.objEmitterList.Location = new System.Drawing.Point(8, 32);
			this.objEmitterList.Name = "objEmitterList";
			this.objEmitterList.Size = new System.Drawing.Size(160, 21);
			this.objEmitterList.TabIndex = 1;
			this.objEmitterList.SelectedIndexChanged += new System.EventHandler(this.objEmitterList_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(112, 24);
			this.label1.TabIndex = 2;
			this.label1.Text = "Emitters:";
			// 
			// objAddEmitter
			// 
			this.objAddEmitter.Location = new System.Drawing.Point(8, 64);
			this.objAddEmitter.Name = "objAddEmitter";
			this.objAddEmitter.Size = new System.Drawing.Size(40, 24);
			this.objAddEmitter.TabIndex = 3;
			this.objAddEmitter.Text = "Add";
			this.objAddEmitter.Click += new System.EventHandler(this.objAddEmitter_Click);
			// 
			// objRemoveEmitter
			// 
			this.objRemoveEmitter.Location = new System.Drawing.Point(112, 64);
			this.objRemoveEmitter.Name = "objRemoveEmitter";
			this.objRemoveEmitter.Size = new System.Drawing.Size(56, 24);
			this.objRemoveEmitter.TabIndex = 4;
			this.objRemoveEmitter.Text = "Remove";
			this.objRemoveEmitter.Click += new System.EventHandler(this.objRemoveEmitter_Click);
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem1});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem2,
																					  this.menuItem3,
																					  this.menuItem6,
																					  this.menuItem4,
																					  this.menuItem7,
																					  this.menuItem5});
			this.menuItem1.Text = "File";
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 0;
			this.menuItem2.Text = "New";
			this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click_1);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 1;
			this.menuItem3.Text = "-";
			// 
			// menuItem6
			// 
			this.menuItem6.Index = 2;
			this.menuItem6.Text = "Save";
			this.menuItem6.Click += new System.EventHandler(this.menuItem6_Click);
			// 
			// menuItem4
			// 
			this.menuItem4.Index = 3;
			this.menuItem4.Text = "Save as...";
			this.menuItem4.Click += new System.EventHandler(this.menuItem4_Click);
			// 
			// menuItem7
			// 
			this.menuItem7.Index = 4;
			this.menuItem7.Text = "-";
			// 
			// menuItem5
			// 
			this.menuItem5.Index = 5;
			this.menuItem5.Text = "Open";
			this.menuItem5.Click += new System.EventHandler(this.menuItem5_Click);
			// 
			// label20
			// 
			this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label20.Location = new System.Drawing.Point(216, 8);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(112, 24);
			this.label20.TabIndex = 5;
			this.label20.Text = "File name:";
			// 
			// txtFileName
			// 
			this.txtFileName.Location = new System.Drawing.Point(216, 32);
			this.txtFileName.Name = "txtFileName";
			this.txtFileName.ReadOnly = true;
			this.txtFileName.Size = new System.Drawing.Size(256, 20);
			this.txtFileName.TabIndex = 33;
			this.txtFileName.Text = "";
			// 
			// objView
			// 
			this.objView.Location = new System.Drawing.Point(216, 64);
			this.objView.Name = "objView";
			this.objView.Size = new System.Drawing.Size(256, 24);
			this.objView.TabIndex = 34;
			this.objView.Text = "Save and View Particle System";
			this.objView.Click += new System.EventHandler(this.objView_Click);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(56, 64);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(48, 24);
			this.button1.TabIndex = 35;
			this.button1.Text = "Copy";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// objShowRoom
			// 
			this.objShowRoom.Location = new System.Drawing.Point(8, 96);
			this.objShowRoom.Name = "objShowRoom";
			this.objShowRoom.Size = new System.Drawing.Size(88, 16);
			this.objShowRoom.TabIndex = 36;
			this.objShowRoom.Text = "Show Room";
			this.objShowRoom.CheckedChanged += new System.EventHandler(this.objShowRoom_CheckedChanged);
			// 
			// label136
			// 
			this.label136.Location = new System.Drawing.Point(216, 96);
			this.label136.Name = "label136";
			this.label136.Size = new System.Drawing.Size(64, 16);
			this.label136.TabIndex = 37;
			this.label136.Text = "Room size:";
			// 
			// txtRoomSizeX
			// 
			this.txtRoomSizeX.Location = new System.Drawing.Point(304, 96);
			this.txtRoomSizeX.Name = "txtRoomSizeX";
			this.txtRoomSizeX.Size = new System.Drawing.Size(40, 20);
			this.txtRoomSizeX.TabIndex = 38;
			this.txtRoomSizeX.Text = "";
			this.txtRoomSizeX.TextChanged += new System.EventHandler(this.txtRoomSizeX_TextChanged);
			// 
			// txtRoomSizeY
			// 
			this.txtRoomSizeY.Location = new System.Drawing.Point(368, 96);
			this.txtRoomSizeY.Name = "txtRoomSizeY";
			this.txtRoomSizeY.Size = new System.Drawing.Size(40, 20);
			this.txtRoomSizeY.TabIndex = 39;
			this.txtRoomSizeY.Text = "";
			this.txtRoomSizeY.TextChanged += new System.EventHandler(this.txtRoomSizeY_TextChanged);
			// 
			// txtRoomSizeZ
			// 
			this.txtRoomSizeZ.Location = new System.Drawing.Point(432, 96);
			this.txtRoomSizeZ.Name = "txtRoomSizeZ";
			this.txtRoomSizeZ.Size = new System.Drawing.Size(40, 20);
			this.txtRoomSizeZ.TabIndex = 40;
			this.txtRoomSizeZ.Text = "";
			this.txtRoomSizeZ.TextChanged += new System.EventHandler(this.txtRoomSizeZ_TextChanged);
			// 
			// label137
			// 
			this.label137.Location = new System.Drawing.Point(288, 96);
			this.label137.Name = "label137";
			this.label137.Size = new System.Drawing.Size(16, 16);
			this.label137.TabIndex = 41;
			this.label137.Text = "X";
			// 
			// label138
			// 
			this.label138.Location = new System.Drawing.Point(352, 96);
			this.label138.Name = "label138";
			this.label138.Size = new System.Drawing.Size(16, 16);
			this.label138.TabIndex = 42;
			this.label138.Text = "Y";
			// 
			// label139
			// 
			this.label139.Location = new System.Drawing.Point(416, 96);
			this.label139.Name = "label139";
			this.label139.Size = new System.Drawing.Size(16, 16);
			this.label139.TabIndex = 43;
			this.label139.Text = "Z";
			// 
			// label172
			// 
			this.label172.Location = new System.Drawing.Point(8, 72);
			this.label172.Name = "label172";
			this.label172.Size = new System.Drawing.Size(408, 16);
			this.label172.TabIndex = 12;
			this.label172.Text = "If Y min and max is 0 then X is used for Y size.(useful when you want a square.) " +
				" ";
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(482, 523);
			this.Controls.Add(this.label139);
			this.Controls.Add(this.label138);
			this.Controls.Add(this.label137);
			this.Controls.Add(this.txtRoomSizeZ);
			this.Controls.Add(this.txtRoomSizeY);
			this.Controls.Add(this.txtRoomSizeX);
			this.Controls.Add(this.txtFileName);
			this.Controls.Add(this.label136);
			this.Controls.Add(this.objShowRoom);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.objView);
			this.Controls.Add(this.label20);
			this.Controls.Add(this.objRemoveEmitter);
			this.Controls.Add(this.objAddEmitter);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.objEmitterList);
			this.Controls.Add(this.tabControl1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Menu = this.mainMenu1;
			this.Name = "Form1";
			this.Text = "HPL Particle Editor";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.Pause.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.tabPage8.ResumeLayout(false);
			this.txtBounceAmount.ResumeLayout(false);
			this.tabPage10.ResumeLayout(false);
			this.groupBox22.ResumeLayout(false);
			this.tabPage9.ResumeLayout(false);
			this.groupBox20.ResumeLayout(false);
			this.tabPage6.ResumeLayout(false);
			this.groupBox17.ResumeLayout(false);
			this.groupBox16.ResumeLayout(false);
			this.groupBox15.ResumeLayout(false);
			this.tabPage3.ResumeLayout(false);
			this.groupBox7.ResumeLayout(false);
			this.groupBox6.ResumeLayout(false);
			this.groupBox5.ResumeLayout(false);
			this.tabPage7.ResumeLayout(false);
			this.groupBox19.ResumeLayout(false);
			this.groupBox18.ResumeLayout(false);
			this.tabPage4.ResumeLayout(false);
			this.groupBox11.ResumeLayout(false);
			this.groupBox8.ResumeLayout(false);
			this.groupBox9.ResumeLayout(false);
			this.groupBox10.ResumeLayout(false);
			this.groupBox12.ResumeLayout(false);
			this.tabPage5.ResumeLayout(false);
			this.groupBox14.ResumeLayout(false);
			this.groupBox13.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.groupBox4.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		private void menuItem2_Click(object sender, System.EventArgs e)
		{
		
		}

		private void Form1_Load(object sender, System.EventArgs e)
		{
		
		}
		
		///////////////////////////////////////////////////////////////
		//                     MAIN MENU
		///////////////////////////////////////////////////////////////
		
		private void menuItem6_Click(object sender, System.EventArgs e)
		{
			if(txtFileName.Text == "")
			{
				menuItem4_Click(sender, e);
			}
			else
			{
				mPS.Save(txtFileName.Text);
			}
		}

		private void menuItem4_Click(object sender, System.EventArgs e)
		{
			mSaveFileDialog.Filter = "Particle files (*.ps)|*.ps";
			mSaveFileDialog.AddExtension = true;

			if(mSaveFileDialog.ShowDialog() == DialogResult.OK)
			{
				mPS.Save(mSaveFileDialog.FileName);

				txtFileName.Text = mSaveFileDialog.FileName;
				mOpenFileDialog.InitialDirectory = Path.GetDirectoryName(mSaveFileDialog.FileName);
				mOpenFileDialog.FileName ="";
			}
		}

		//-----------------------------------------------------------

		private void menuItem5_Click(object sender, System.EventArgs e)
		{
			mOpenFileDialog.Filter = "Particle files (*.ps)|*.ps";
			
			if(mOpenFileDialog.ShowDialog()== DialogResult.OK)
			{
				//Reset the file dialog.
				mOpenFileDialog.InitialDirectory = Path.GetDirectoryName(mOpenFileDialog.FileName);
				mSaveFileDialog.InitialDirectory = mOpenFileDialog.InitialDirectory;
				mSaveFileDialog.FileName = mOpenFileDialog.FileName;
			
				txtFileName.Text = mOpenFileDialog.FileName;
				mOpenFileDialog.FileName ="";

				mPS.Load(txtFileName.Text);
			}	
		}

		//-----------------------------------------------------------

		private void menuItem2_Click_1(object sender, System.EventArgs e)
		{
			DialogResult Res = MessageBox.Show("Are you sure you want to create a new particle system?",
												"Warning!",MessageBoxButtons.OKCancel);
			if(Res == DialogResult.OK)
			{
				mPS.Reset();
				mPS.mCurrentEmitter.CopyToGui();
			}
		}

		///////////////////////////////////////////////////////////////
		//                       MAIN FORM
		///////////////////////////////////////////////////////////////
		
		//-----------------------------------------------------------
		
		private void objEmitterList_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			mPS.ChangeCurrentEmitter(objEmitterList.SelectedIndex);
		}

		private void objAddEmitter_Click(object sender, System.EventArgs e)
		{
			mPS.AddEmitter();		
		}

		private void objRemoveEmitter_Click(object sender, System.EventArgs e)
		{
			mPS.RemoveEmitter(objEmitterList.SelectedIndex);
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			mPS.CopyEmitter(objEmitterList.SelectedIndex);
		}
		
		private void objView_Click(object sender, System.EventArgs e)
		{
			if(txtFileName.Text == "")
			{
				MessageBox.Show("The particle system must be saved before it can be viewed.",
								"Alert!",MessageBoxButtons.OK);
				return;
			}

			if(txtMaterial.Text == "")
			{
				MessageBox.Show("No material file specified, using default.",
								"Info", MessageBoxButtons.OK);
				txtMaterial.Text = "particle_default.mat";
			}
			
			mPS.Save(txtFileName.Text);

			String sFile = Path.GetFileName(txtFileName.Text);
			HplSystem.RunProgam("ParticleViewer.exe",sFile);
		}

		//-----------------------------------------------------------
		
		private void objShowRoom_CheckedChanged(object sender, System.EventArgs e)
		{
			mPS.mbShowRoom = objShowRoom.Checked ? "True" : "False";	
		}

		//-----------------------------------------------------------
		
		private void txtRoomSizeX_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mvRoomSize.x = txtRoomSizeX.Text;
		}

		private void txtRoomSizeY_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mvRoomSize.y = txtRoomSizeY.Text;
		}

		private void txtRoomSizeZ_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mvRoomSize.z = txtRoomSizeZ.Text;
		}

		//-----------------------------------------------------------
		
		///////////////////////////////////////////////////////////////
		//                       GENERAL
		///////////////////////////////////////////////////////////////
		
		private void txtName_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.msName = txtName.Text;
		
			objEmitterList.Items[objEmitterList.SelectedIndex] = txtName.Text;
		}

		private void txtMaxParticles_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mlMaxParticleNum = txtMaxParticles.Text;
		}

		private void txtParticlesPerSec_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mfParticlesPerSecond = txtParticlesPerSec.Text;
		}

		private void txtStartTimeOffset_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mfStartTimeOffset = txtStartTimeOffset.Text;
		}

		private void objRespawn_CheckedChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mbRespawn = objRespawn.Checked ? "True" : "False";
		}

		//------------------------------------------------------------

		private void txtWarmUpTime_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mfWarmUpTime = txtWarmUpTime.Text;	
		}

		private void txtWarmUpStepsPerSec_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mfWarmUpStepsPerSec = txtWarmUpStepsPerSec.Text;
		}
		
		//------------------------------------------------------------

		private void txtMinPauseLength_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mfMinPauseLength = 	txtMinPauseLength.Text;		
		}

		private void txtMaxPauseLength_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mfMaxPauseLength = 	txtMaxPauseLength.Text;
		}

		private void txtMinPauseInterval_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mfMinPauseInterval = txtMinPauseInterval.Text;
		}

		private void txtMaxPauseInterval_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mfMaxPauseInterval = txtMaxPauseInterval.Text;
		}

		//------------------------------------------------------------

		private void txtPosOffsetX_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mvPosOffset.x = txtPosOffsetX.Text;
		}

		private void txtPosOffsetY_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mvPosOffset.y = txtPosOffsetY.Text;
		}

		private void txtPosOffsetZ_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mvPosOffset.z = txtPosOffsetZ.Text;
		}

		private void txtAngleOffsetX_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mvAngleOffset.x = txtAngleOffsetX.Text;
		}

		private void txtAngleOffsetY_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mvAngleOffset.y = txtAngleOffsetY.Text;
		}

		private void txtAngleOffsetZ_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mvAngleOffset.z = txtAngleOffsetZ.Text;
		}
		
		//-----------------------------------------------------------
		
		///////////////////////////////////////////////////////////////
		//                       MATERIAL
		///////////////////////////////////////////////////////////////
		
		//-----------------------------------------------------------
				
		private void objBrowseMaterial_Click(object sender, System.EventArgs e)
		{
			mMaterialFileDialog.Filter = "Material files (*.mat)|*.mat";
			
			if(mMaterialFileDialog.ShowDialog()== DialogResult.OK)
			{
				//Reset the file dialog.
				mMaterialFileDialog.InitialDirectory = Path.GetDirectoryName(mMaterialFileDialog.FileName);
				
				txtMaterial.Text = Path.GetFileName(mMaterialFileDialog.FileName);
			}	
		}

		private void txtMaterial_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.msMaterial = txtMaterial.Text;
		}

		private void txtMaterialNum_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mlMaterialNum = txtMaterialNum.Text;
		}

		private void txtAnimationLength_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mfAnimationLength = txtAnimationLength.Text;
		}

		private void txtSubDivX_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mvSubDiv.x = txtSubDivX.Text;
		}

		private void txtSubDivY_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mvSubDiv.y = txtSubDivY.Text;
		}

		private void objSubDivType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mSubDivType = (String)objSubDivType.SelectedItem;
		}

		private void groupBox5_Enter(object sender, System.EventArgs e)
		{
		
		}

		///////////////////////////////////////////////////////////////
		//                       START
		///////////////////////////////////////////////////////////////
		
		//-----------------------------------------------------------
		
		private void objStartPosType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mStartPosType = (String)objStartPosType.SelectedItem;

			if(mPS.mCurrentEmitter.mStartPosType == "Box")
			{
				//Box
				txtMinStartPosX.Enabled = true;txtMinStartPosY.Enabled = true;txtMinStartPosZ.Enabled = true;
				txtMaxStartPosX.Enabled = true;txtMaxStartPosY.Enabled = true;txtMaxStartPosZ.Enabled = true;
				
				//Sphere
				txtMinStartAnglesX.Enabled = false; txtMinStartAnglesY.Enabled = false;
				txtMaxStartAnglesX.Enabled = false; txtMaxStartAnglesY.Enabled = false;
				txtMinStartRadius.Enabled = false; txtMaxStartRadius.Enabled = false;
			}
			else if(mPS.mCurrentEmitter.mStartPosType == "Sphere")
			{
				//Box
				txtMinStartPosX.Enabled = false;txtMinStartPosY.Enabled = false;txtMinStartPosZ.Enabled = false;
				txtMaxStartPosX.Enabled = false;txtMaxStartPosY.Enabled = false;txtMaxStartPosZ.Enabled = false;
				
				//Sphere
				txtMinStartAnglesX.Enabled = true; txtMinStartAnglesY.Enabled = true;
				txtMaxStartAnglesX.Enabled = true; txtMaxStartAnglesY.Enabled = true;
				txtMinStartRadius.Enabled = true; txtMaxStartRadius.Enabled = true;
			}
		}

		//-----------------------------------------------------------

		private void txtMinStartPosX_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mvMinStartPos.x = txtMinStartPosX.Text;
		}

		private void txtMinStartPosY_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mvMinStartPos.y = txtMinStartPosY.Text;
		}

		private void txtMinStartPosZ_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mvMinStartPos.z = txtMinStartPosZ.Text;
		}

		//-----------------------------------------------------------

		private void txtMaxStartPosX_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mvMaxStartPos.x = txtMaxStartPosX.Text;
		}

		private void txtMaxStartPosY_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mvMaxStartPos.y = txtMaxStartPosY.Text;
		}
		
		private void txtMaxStartPosZ_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mvMaxStartPos.z = txtMaxStartPosZ.Text;
		}

		//-----------------------------------------------------------
		

		private void txtMinStartAnglesX_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mvMinStartAngles.x = txtMinStartAnglesX.Text;
		}

		private void txtMinStartAnglesY_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mvMinStartAngles.y = txtMinStartAnglesY.Text;
		}

		//-----------------------------------------------------------

		private void txtMaxStartAnglesX_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mvMaxStartAngles.x = txtMaxStartAnglesX.Text;
		}

		private void txtMaxStartAnglesY_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mvMaxStartAngles.y = txtMaxStartAnglesY.Text;
		}

		//-----------------------------------------------------------

		private void txtMinStartRadius_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mfMinStartRadius = txtMinStartRadius.Text;
		}

		private void txtMaxStartRadius_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mfMaxStartRadius = txtMaxStartRadius.Text;
		}

		private void txtMaxStartRadius_TextChanged_1(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mfMaxStartRadius = txtMaxStartRadius.Text;
		}	

		//-----------------------------------------------------------
		
		///////////////////////////////////////////////////////////////
		//                       MOVEMENT
		///////////////////////////////////////////////////////////////
		
		//-----------------------------------------------------------
		

		private void objStartVelType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mStartVelType = (String)objStartVelType.SelectedItem;

			if(mPS.mCurrentEmitter.mStartVelType == "Box")
			{
				//Box
				txtMinStartVelX.Enabled = true;txtMinStartVelY.Enabled = true;txtMinStartVelZ.Enabled = true;
				txtMaxStartVelX.Enabled = true;txtMaxStartVelY.Enabled = true;txtMaxStartVelZ.Enabled = true;
				
				//Sphere
				txtMinStartVelAnglesX.Enabled = false; txtMinStartVelAnglesY.Enabled = false;
				txtMaxStartVelAnglesX.Enabled = false; txtMaxStartVelAnglesY.Enabled = false;
				txtMinStartVelSpeed.Enabled = false; txtMaxStartVelSpeed.Enabled = false;
			}
			else if(mPS.mCurrentEmitter.mStartVelType == "Sphere")
			{
				//Box
				txtMinStartVelX.Enabled = false;txtMinStartVelY.Enabled = false;txtMinStartVelZ.Enabled = false;
				txtMaxStartVelX.Enabled = false;txtMaxStartVelY.Enabled = false;txtMaxStartVelZ.Enabled = false;
				
				//Sphere
				txtMinStartVelAnglesX.Enabled = true; txtMinStartVelAnglesY.Enabled = true;
				txtMaxStartVelAnglesX.Enabled = true; txtMaxStartVelAnglesY.Enabled = true;
				txtMinStartVelSpeed.Enabled = true; txtMaxStartVelSpeed.Enabled = true;
			}
		}

		private void objCoordSystem_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mCoordSystem = (String)objCoordSystem.SelectedItem;
		}

		//-----------------------------------------------------------

		private void objUsesDirection_CheckedChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mbUsesDirection = objUsesDirection.Checked ? "True" : "False";
		}

		private void objGravityType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mGravityType = (String)objGravityType.SelectedItem;
		}

		//-----------------------------------------------------------

		private void txtGravityAccX_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mvGravityAcc.x = txtGravityAccX.Text;
		}		

		private void txtGravityAccY_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mvGravityAcc.y = txtGravityAccY.Text;
		}

		private void txtGravityAccZ_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mvGravityAcc.z = txtGravityAccZ.Text;
		}

		//-----------------------------------------------------------

		private void txtMinStartVelX_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mvMinStartVel.x = txtMinStartVelX.Text;
		}

		private void txtMinStartVelY_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mvMinStartVel.y = txtMinStartVelY.Text;
		}

		private void txtMinStartVelZ_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mvMinStartVel.z = txtMinStartVelZ.Text;
		}

		//-----------------------------------------------------------

		private void txtMaxStartVelX_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mvMaxStartVel.x = txtMaxStartVelX.Text;
		}

		private void txtMaxStartVelY_TextChanged(object sender, System.EventArgs e)
		{	
			mPS.mCurrentEmitter.mvMaxStartVel.y = txtMaxStartVelY.Text;
		}

		//-----------------------------------------------------------

		private void txtMaxStartVelZ_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mvMaxStartVel.z = txtMaxStartVelZ.Text;
		}

		private void txtMinStartVelAnglesX_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mvMinStartVelAngles.x = txtMinStartVelAnglesX.Text;
		}

		private void txtMinStartVelAnglesY_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mvMinStartVelAngles.y = txtMinStartVelAnglesY.Text;
		}

		//-----------------------------------------------------------

		private void txtMaxStartVelAnglesX_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mvMaxStartVelAngles.x = txtMaxStartVelAnglesX.Text;
		}

		private void txtMaxStartVelAnglesY_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mvMaxStartVelAngles.y = txtMaxStartVelAnglesY.Text;
		}

		//-----------------------------------------------------------

		private void txtMinStartVelSpeed_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mfMinStartVelSpeed = txtMinStartVelSpeed.Text;
		}

		private void txtMaxStartVelSpeed_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mfMaxStartVelSpeed = txtMaxStartVelSpeed.Text;
		}

		//-----------------------------------------------------------

		private void txtMinStartAccX_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mvMinStartAcc.x = txtMinStartAccX.Text;
		}

		private void txtMinStartAccY_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mvMinStartAcc.y = txtMinStartAccY.Text;
		}

		private void txtMinStartAccZ_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mvMinStartAcc.z = txtMinStartAccZ.Text;
		}

		//-----------------------------------------------------------
		
		private void txtMaxStartAccX_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mvMaxStartAcc.x = txtMaxStartAccX.Text;
		}

		private void txtMaxStartAccY_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mvMaxStartAcc.y = txtMaxStartAccY.Text;
		}

		private void txtMaxStartAccZ_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mvMaxStartAcc.z = txtMaxStartAccZ.Text;
		}

		//-----------------------------------------------------------

		private void txtMinSpeedMultiply_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mfMinSpeedMultiply = txtMinSpeedMultiply.Text;
		}

		private void txtMaxSpeedMultiply_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mfMaxSpeedMultiply = txtMaxSpeedMultiply.Text;
		}
		
		//-----------------------------------------------------------

		private void txtMinVelMaximum_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mfMinVelMaximum = txtMinVelMaximum.Text;
		}

		private void txtMaxVelMaximum_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mfMaxVelMaximum = txtMaxVelMaximum.Text;
		}
		
		//-----------------------------------------------------------

		// NEW
		private void txtMinSpinRange_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mfMinSpinRange = txtMinSpinRange.Text;
		}

		private void txtMaxSpinRange_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mfMaxSpinRange = txtMaxSpinRange.Text;		
		}

		
		private void txtMinRevVelX_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mvMinRevVel.x = txtMinRevVelX.Text;
		}

		private void txtMinRevVelY_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mvMinRevVel.y = txtMinRevVelY.Text;
		}

		private void txtMinRevVelZ_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mvMinRevVel.z = txtMinRevVelZ.Text;
		}

		private void txtMaxRevVelX_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mvMaxRevVel.x = txtMaxRevVelX.Text;
		}

		private void txtMaxRevVelY_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mvMaxRevVel.y = txtMaxRevVelY.Text;
		}

		private void txtMaxRevVelZ_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mvMaxRevVel.z = txtMaxRevVelZ.Text;
		}

		private void objUsePartSpin_CheckedChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mbUsePartSpin = objUsePartSpin.Checked ? "True" : "False";

			objParticleSpinType.Enabled = objUsePartSpin.Checked;
			txtMinSpinRange.Enabled = objUsePartSpin.Checked;
			txtMaxSpinRange.Enabled = objUsePartSpin.Checked;
		}

		private void objUseRevolution_CheckedChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mbUseRevolution = objUseRevolution.Checked ? "True" : "False";

			txtMinRevVelX.Enabled = objUseRevolution.Checked;
			txtMinRevVelY.Enabled = objUseRevolution.Checked;
			txtMinRevVelZ.Enabled = objUseRevolution.Checked;
			txtMaxRevVelX.Enabled = objUseRevolution.Checked;
			txtMaxRevVelY.Enabled = objUseRevolution.Checked;
			txtMaxRevVelZ.Enabled = objUseRevolution.Checked;
		}


		//-----------------------------------------------------------
		// ---

		///////////////////////////////////////////////////////////////
		//                       LIFE
		///////////////////////////////////////////////////////////////
		
		//-----------------------------------------------------------
		
		private void txtMinLifeSpan_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mfMinLifeSpan = txtMinLifeSpan.Text;
		}

		private void txtMaxLifeSpan_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mfMaxLifeSpan = txtMaxLifeSpan.Text;
		}

		//-----------------------------------------------------------
		
		private void objDeathType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mDeathType = (String)objDeathType.SelectedItem;
		}

		//-----------------------------------------------------------
		
		private void txtDeathPS_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.msDeathPS = txtDeathPS.Text;
		}

		//-----------------------------------------------------------
		
		///////////////////////////////////////////////////////////////
		//                       RENDERING
		///////////////////////////////////////////////////////////////
		
		//-----------------------------------------------------------
		
		private void objMultiplyRGBWithAlpha_CheckedChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mbMultiplyRGBWithAlpha = objMultiplyRGBWithAlpha.Checked ? "True" : "False";
		}

		//-----------------------------------------------------------
		
		private void objDrawType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mDrawType = (String)objDrawType.SelectedItem;
		}

		//-----------------------------------------------------------
		
		private void txtMinStartSizeX_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mvMinStartSize.x = txtMinStartSizeX.Text;
		}

		private void txtMinStartSizeY_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mvMinStartSize.y = txtMinStartSizeY.Text;
		}

		//-----------------------------------------------------------
		
		private void txtMaxStartSizeX_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mvMaxStartSize.x = txtMaxStartSizeX.Text;
		}

		private void txtMaxStartSizeY_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mvMaxStartSize.y = txtMaxStartSizeY.Text;
		}

		//-----------------------------------------------------------
		
		private void txtStartRelSize_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mfStartRelSize = txtStartRelSize.Text;
		}

		private void txtMiddleRelSize_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mfMiddleRelSize = txtMiddleRelSize.Text;
		}

		private void txtEndRelSize_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mfEndRelSize = txtEndRelSize.Text;
		}

		//-----------------------------------------------------------
		
		private void txtMiddleRelSizeTime_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mfMiddleRelSizeTime = txtMiddleRelSizeTime.Text;
		}

		private void txtMiddleRelSizeLength_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mfMiddleRelSizeLength = txtMiddleRelSizeLength.Text;
		}

		private void tabPage6_Click(object sender, System.EventArgs e)
		{
		
		}
		
		//-----------------------------------------------------------
		
		///////////////////////////////////////////////////////////////
		//                       COLOR
		///////////////////////////////////////////////////////////////
		
		//-----------------------------------------------------------
		
		private void txtMinStartColorR_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mMinStartColor.r = txtMinStartColorR.Text;
		}

		private void txtMinStartColorG_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mMinStartColor.g = txtMinStartColorG.Text;
		}

		private void txtMinStartColorB_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mMinStartColor.b = txtMinStartColorB.Text;
		}

		private void txtMinStartColorA_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mMinStartColor.a = txtMinStartColorA.Text;
		}

		//-----------------------------------------------------------

		private void txtMaxStartColorR_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mMaxStartColor.r = txtMaxStartColorR.Text;
		}

		private void txtMaxStartColorG_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mMaxStartColor.g = txtMaxStartColorG.Text;
		}

		private void txtMaxStartColorB_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mMaxStartColor.b = txtMaxStartColorB.Text;
		}

		private void txtMaxStartColorA_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mMaxStartColor.a = txtMaxStartColorA.Text;
		}
		
		//-----------------------------------------------------------

		private void txtStartRelColorR_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mStartRelColor.r = txtStartRelColorR.Text;
		}

		private void txtStartRelColorG_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mStartRelColor.g = txtStartRelColorG.Text;
		}

		private void txtStartRelColorB_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mStartRelColor.b = txtStartRelColorB.Text;
		}

		private void txtStartRelColorA_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mStartRelColor.a = txtStartRelColorA.Text;
		}
		
		//-----------------------------------------------------------

		private void txtMiddleRelColorR_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mMiddleRelColor.r = txtMiddleRelColorR.Text;
		}

		private void txtMiddleRelColorG_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mMiddleRelColor.g = txtMiddleRelColorG.Text;
		}

		private void txtMiddleRelColorB_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mMiddleRelColor.b = txtMiddleRelColorB.Text;
		}

		private void txtMiddleRelColorA_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mMiddleRelColor.a = txtMiddleRelColorA.Text;
		}

		//-----------------------------------------------------------

		private void txtEndRelColorR_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mEndRelColor.r = txtEndRelColorR.Text;
		}

		private void txtEndRelColorG_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mEndRelColor.g = txtEndRelColorG.Text;
		}

		private void txtEndRelColorB_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mEndRelColor.b = txtEndRelColorB.Text;
		}

		private void txtEndRelColorA_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mEndRelColor.a = txtEndRelColorA.Text;
		}
		
		//-----------------------------------------------------------

		private void txtMiddleRelColorTime_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mfMiddleRelColorTime = txtMiddleRelColorTime.Text;
		}

		private void txtMiddleRelColorLength_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mfMiddleRelColorLength = txtMiddleRelColorLength.Text;
		}
		
		///////////////////////////////////////////////////////////////
		//                       COLLISION
		///////////////////////////////////////////////////////////////
		
		//-----------------------------------------------------------
		
		private void objCollides_CheckedChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mbCollides = objCollides.Checked ? "True" : "False";
		}

		//-----------------------------------------------------------
		
		private void txtCollisionUpdateRate_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mlCollisionUpdateRate = txtCollisionUpdateRate.Text;
		}

		//-----------------------------------------------------------
		
		private void txtMinCollisionMax_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mlMinCollisionMax = txtMinCollisionMax.Text;
		}

		private void txtMaxCollisionMax_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mlMaxCollisionMax = txtMaxCollisionMax.Text;
		}

		//-----------------------------------------------------------
		
		private void txtMinBounceAmount_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mfMinBounceAmount = txtMinBounceAmount.Text;
		}

		private void txtMaxBounceAmount_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mfMaxBounceAmount = txtMaxBounceAmount.Text;
		}

		private void objParticleSpinType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
            mPS.mCurrentEmitter.mPartSpinType = (String) objParticleSpinType.SelectedItem;

			if ( mPS.mCurrentEmitter.mPartSpinType == "Constant" )
			{
				label142.Text = "Particle Spin Velocity Range (rad/s)";
			}
			else if ( mPS.mCurrentEmitter.mPartSpinType == "Movement" )
			{
                label142.Text = "Spin Factor (radians per m/s)";				
			}
		}

		private void objPEType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mPEType = (String) objPEType.SelectedItem;
		
		}

		private void txtLowFreqPoints_TextChanged(object sender, System.EventArgs e)
		{
			if ( (txtHighFreqPoints.Text != "") && (txtLowFreqPoints.Text != "") )
			{
				if (int.Parse(txtLowFreqPoints.Text) < 2)
				{
					MessageBox.Show("Low Frequency Points and High Frequency Points must be greater or equal to 2", "Warning", MessageBoxButtons.OK);
					txtLowFreqPoints.Text = mPS.mCurrentEmitter.mlLowFreqPoints;
				}
				else
				{
					if( int.Parse(txtHighFreqPoints.Text) >= int.Parse(txtLowFreqPoints.Text) )
						mPS.mCurrentEmitter.mlLowFreqPoints = txtLowFreqPoints.Text;
					else
					{
						MessageBox.Show("Low Frequency Points must have a lower or equal value than that of High Frequency Points","Warning",MessageBoxButtons.OK);
						txtLowFreqPoints.Text = mPS.mCurrentEmitter.mlLowFreqPoints;
					}
				}
			}
   		}

		private void txtMinLFNoiseX_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mvMinLFNoise.x = txtMinLFNoiseX.Text;
		}

		private void txtMinLFNoiseY_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mvMinLFNoise.y = txtMinLFNoiseY.Text;
		}

		private void txtMinLFNoiseZ_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mvMinLFNoise.z = txtMinLFNoiseZ.Text;
		}

		private void txtMaxLFNoiseX_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mvMaxLFNoise.x = txtMaxLFNoiseX.Text;
		}

		private void txtMaxLFNoiseY_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mvMaxLFNoise.y = txtMaxLFNoiseY.Text;
		}

		private void txtMaxLFNoiseZ_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mvMaxLFNoise.z = txtMaxLFNoiseZ.Text;
		}

		private void txtHighFreqPoints_TextChanged(object sender, System.EventArgs e)
		{
			if ( (txtHighFreqPoints.Text != "") && (txtLowFreqPoints.Text != ""))
			{
				if (int.Parse(txtHighFreqPoints.Text) < 2)
				{
					MessageBox.Show("High Frequency Points must be greater or equal to 2", "Warning", MessageBoxButtons.OK);
					txtHighFreqPoints.Text = mPS.mCurrentEmitter.mlHighFreqPoints;
				}
				else
				{
					if ( int.Parse(txtHighFreqPoints.Text) >= int.Parse(txtLowFreqPoints.Text) )
						mPS.mCurrentEmitter.mlHighFreqPoints = txtHighFreqPoints.Text;
					else 
					{
						MessageBox.Show("High Frequency Points must have a greater or equal value than that of Low Frequency Points","Warning", MessageBoxButtons.OK);
						txtHighFreqPoints.Text = mPS.mCurrentEmitter.mlHighFreqPoints;
					}
				}
			}
		}

		private void txtMinHFNoiseX_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mvMinHFNoise.x = txtMinHFNoiseX.Text;
		}

		private void txtMinHFNoiseY_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mvMinHFNoise.y = txtMinHFNoiseY.Text;
        }

		private void txtMinHFNoiseZ_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mvMinHFNoise.z = txtMinHFNoiseZ.Text;
		}

		private void txtMaxHFNoiseX_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mvMaxHFNoise.x = txtMaxHFNoiseX.Text;
		}

		private void txtMaxHFNoiseY_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mvMaxHFNoise.y = txtMaxHFNoiseY.Text;
		}

		private void txtMaxHFNoiseZ_TextChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mvMaxHFNoise.z = txtMaxHFNoiseZ.Text;
		}

		private void objApplyNoiseToStart_CheckedChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mbApplyNoiseToStart = (objApplyNoiseToStart.Checked ? "True" : "False");
		}

		private void objApplyNoiseToEnd_CheckedChanged(object sender, System.EventArgs e)
		{
            mPS.mCurrentEmitter.mbApplyNoiseToEnd = (objApplyNoiseToEnd.Checked ? "True" : "False");		
		}

		private void objUseBeamNoise_CheckedChanged(object sender, System.EventArgs e)
		{
			mPS.mCurrentEmitter.mbUseBeamNoise = (objUseBeamNoise.Checked ? "True" : "False");

			txtLowFreqPoints.Enabled = objUseBeamNoise.Checked;
			txtMinLFNoiseX.Enabled = objUseBeamNoise.Checked;
			txtMinLFNoiseY.Enabled = objUseBeamNoise.Checked;
			txtMinLFNoiseZ.Enabled = objUseBeamNoise.Checked;
			txtMaxLFNoiseX.Enabled = objUseBeamNoise.Checked;
			txtMaxLFNoiseY.Enabled = objUseBeamNoise.Checked;
			txtMaxLFNoiseZ.Enabled = objUseBeamNoise.Checked;

			txtHighFreqPoints.Enabled = objUseBeamNoise.Checked;
			txtMinHFNoiseX.Enabled = objUseBeamNoise.Checked;
			txtMinHFNoiseY.Enabled = objUseBeamNoise.Checked;
			txtMinHFNoiseZ.Enabled = objUseBeamNoise.Checked;
			txtMaxHFNoiseX.Enabled = objUseBeamNoise.Checked;
			txtMaxHFNoiseY.Enabled = objUseBeamNoise.Checked;
			txtMaxHFNoiseZ.Enabled = objUseBeamNoise.Checked;

		}

		private void tabControl1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}


		

	

		
		//-----------------------------------------------------------
		
	}
}
