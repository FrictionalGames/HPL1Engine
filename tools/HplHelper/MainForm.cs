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
using System.Xml;


namespace HplHelper
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class frmMain : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.TextBox txtModelFile;
		private System.Windows.Forms.Label lblModelFile;
		private System.Windows.Forms.Button objModelBrowse;
		private System.Windows.Forms.Button objModelViewButton;
		private System.Windows.Forms.Label lblModelActions;
		private System.Windows.Forms.Button objModelConvert;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.TabPage tabPage4;
		private System.Windows.Forms.Label lblResourceDirs;
		private System.Windows.Forms.ListBox objResourceDirs;
		private System.Windows.Forms.Button objAddResourceDir;
		private System.Windows.Forms.Button objRemoveResourceDir;


		private OpenFileDialog mOpenFileDialog=null;
		private System.Windows.Forms.Button objSaveResources;
		private FolderBrowserDialog mSelectDirDialog=null;
		private System.Windows.Forms.TextBox txtMaterialFile;
		private System.Windows.Forms.Label lblMaterialFile;
		private System.Windows.Forms.Button objMaterialOpenButton;
		private System.Windows.Forms.Button objSaveMaterialButton;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TabPage tabPage3;
		public System.Windows.Forms.ComboBox objMaterialTypes;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label2;

		private String msResourceDirFile="resources.cfg";
		private String msPhysicsMaterialsFile="materials.cfg";
		public System.Windows.Forms.ComboBox objTextureUnitTypes;
		private System.Windows.Forms.TextBox txtTextureUnitFile;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button objTexttureFileButton;
		private System.Windows.Forms.ComboBox objMipMaps;
		private System.Windows.Forms.Label label4;

		private System.Windows.Forms.Label label5;
		public System.Windows.Forms.ComboBox objWrapModes;
		private System.Windows.Forms.Button objMaterialSaveAsButton;

		public HplMaterial mpMaterial=null;
		public ArrayList mvMaterialTypeData=null;
		private System.Windows.Forms.TextBox txtSceneFile;
		private System.Windows.Forms.Button objSceneBrowseButton;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Button objSceneViewButton;
		private System.Windows.Forms.Button objSceneConvertButton;
		private System.Windows.Forms.Button objMaterialClearButton;
		private System.Windows.Forms.Label label8;
		public System.Windows.Forms.ComboBox objPhysicsMaterials;
		private System.Windows.Forms.TabPage tabPage5;
		private System.Windows.Forms.Label label9;
		public System.Windows.Forms.TextBox txtSoundFile;
		private System.Windows.Forms.Button objSoundOpen;
		private System.Windows.Forms.Button objSoundSave;
		private System.Windows.Forms.Button objSoundSaveAs;
		private System.Windows.Forms.GroupBox groupBox3;
		public System.Windows.Forms.TextBox txtMainSoundFile;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label12;
		public System.Windows.Forms.TextBox txtStartSoundFile;
		public System.Windows.Forms.TextBox txtStopSoundFile;
		private System.Windows.Forms.Button objMainSoundButton;
		private System.Windows.Forms.Button objStartSoundButton;
		private System.Windows.Forms.Button objStopSoundButton;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label label15;
		public System.Windows.Forms.ComboBox objSoundLoop;
		public System.Windows.Forms.ComboBox objSoundUse3D;
		public System.Windows.Forms.ComboBox objSoundStream;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.Label label18;
		public System.Windows.Forms.TextBox txtSoundVolume;
		public System.Windows.Forms.TextBox txtSoundMinDist;
		public System.Windows.Forms.TextBox txtSoundMaxDist;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.Label label20;
		public System.Windows.Forms.TextBox txtSoundRandom;
		public System.Windows.Forms.TextBox txtSoundInterval;
		private System.Windows.Forms.Label lblFadeS1;
		private System.Windows.Forms.Label label21;
		public System.Windows.Forms.ComboBox objSoundFadeStart;
		public System.Windows.Forms.ComboBox objSoundFadeEnd;
		
		private SaveFileDialog mSaveFileDialog=null;
		private System.Windows.Forms.Button objClearSoundDataButton;
		public System.Windows.Forms.ComboBox objSoundBlockable;
		private System.Windows.Forms.Label label22;
		private System.Windows.Forms.Label label23;
		public System.Windows.Forms.TextBox txtSoundBlockVolMul;
		private System.Windows.Forms.Label label24;
		private System.Windows.Forms.TextBox txtSceneStartPos;
		private System.Windows.Forms.Label label25;
		public System.Windows.Forms.TextBox txtSoundPriority;
		private System.Windows.Forms.TabPage tabPage6;
		private System.Windows.Forms.TextBox txtTransFile;
		private System.Windows.Forms.Label label26;
		public System.Windows.Forms.Button objTransOpen;
		public System.Windows.Forms.Button objTransSave;
		public System.Windows.Forms.Button objTransSaveAs;
		private HplSound mpSoundData=null;
		private System.Windows.Forms.GroupBox groupBox5;
		public System.Windows.Forms.ComboBox objTransCategories;
		private System.Windows.Forms.Label label27;
		private System.Windows.Forms.Label label28;
		public System.Windows.Forms.ComboBox objTransEntries;
		private System.Windows.Forms.Button objTransCatButton;
		private System.Windows.Forms.Button objTransEntryButton;
		private System.Windows.Forms.GroupBox groupBox6;
		public System.Windows.Forms.TextBox objTransEntryName;
		private System.Windows.Forms.Label label29;
		public System.Windows.Forms.TextBox objTransEntryText;
		private HplTrans mpTransData=null;
		private System.Windows.Forms.Button objTransEntryRemove;
		private System.Windows.Forms.Button objTransCatRemove;
		private System.Windows.Forms.Button objTransCatRename;
		private System.Windows.Forms.Button objTransEntryChangeName;
		public System.Windows.Forms.TextBox txtTransScriptName;
		private System.Windows.Forms.Label label30;
		private System.Windows.Forms.ComboBox objUseAlpha;
		private System.Windows.Forms.Label label31;
		private System.Windows.Forms.ComboBox objDepthTest;
		private System.Windows.Forms.Label label32;
		public System.Windows.Forms.ComboBox objTexAnimMode;
		private System.Windows.Forms.Label label33;
		private System.Windows.Forms.TextBox txtAnimFrameTime;
		private System.Windows.Forms.Label label34;
		public System.Windows.Forms.ComboBox objTextureType;
		private System.Windows.Forms.Label label35;
		private System.Windows.Forms.TextBox txtMatValue;

		private frmEnterName mpEnterNameForm =null;
		
		public frmMain()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			
			//Create open file dialog
			mOpenFileDialog = new OpenFileDialog();
			mOpenFileDialog.InitialDirectory = (string)Directory.GetCurrentDirectory().Clone();
			mOpenFileDialog.RestoreDirectory = true;

			mSaveFileDialog = new SaveFileDialog();
			mSaveFileDialog.InitialDirectory = (string)Directory.GetCurrentDirectory().Clone();
			mSaveFileDialog.RestoreDirectory = true;

			//Create select directory dialog
			mSelectDirDialog = new FolderBrowserDialog();
			mSelectDirDialog.Description = "Select the resource directory to add. The directory MUST be a subdirectory in redist folder!";
			mSelectDirDialog.RootFolder=  Environment.SpecialFolder.MyComputer;
			mSelectDirDialog.SelectedPath = Directory.GetCurrentDirectory();
			
			mpMaterial = new HplMaterial();
			
			//init sound data
			mpSoundData = new HplSound();
			mpSoundData.ClearData(this);

			mpTransData = new HplTrans(this);

			mpEnterNameForm = new frmEnterName();
			
			//Load resource directories
			LoadResourceDirs(msResourceDirFile);

			//Load physics material name
			LoadPhysicsMaterialNames(msPhysicsMaterialsFile);
			
			//Add the types of materials available
			AddMaterialTypes();
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
			this.tabPage4 = new System.Windows.Forms.TabPage();
			this.objSaveResources = new System.Windows.Forms.Button();
			this.objRemoveResourceDir = new System.Windows.Forms.Button();
			this.objAddResourceDir = new System.Windows.Forms.Button();
			this.objResourceDirs = new System.Windows.Forms.ListBox();
			this.lblResourceDirs = new System.Windows.Forms.Label();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.objMaterialClearButton = new System.Windows.Forms.Button();
			this.objMaterialSaveAsButton = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.objTextureType = new System.Windows.Forms.ComboBox();
			this.label34 = new System.Windows.Forms.Label();
			this.label33 = new System.Windows.Forms.Label();
			this.objTexAnimMode = new System.Windows.Forms.ComboBox();
			this.label32 = new System.Windows.Forms.Label();
			this.objWrapModes = new System.Windows.Forms.ComboBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.objMipMaps = new System.Windows.Forms.ComboBox();
			this.objTexttureFileButton = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.txtTextureUnitFile = new System.Windows.Forms.TextBox();
			this.objTextureUnitTypes = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.txtAnimFrameTime = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label35 = new System.Windows.Forms.Label();
			this.objDepthTest = new System.Windows.Forms.ComboBox();
			this.label31 = new System.Windows.Forms.Label();
			this.objUseAlpha = new System.Windows.Forms.ComboBox();
			this.label30 = new System.Windows.Forms.Label();
			this.objPhysicsMaterials = new System.Windows.Forms.ComboBox();
			this.label8 = new System.Windows.Forms.Label();
			this.objMaterialTypes = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.txtMatValue = new System.Windows.Forms.TextBox();
			this.objSaveMaterialButton = new System.Windows.Forms.Button();
			this.lblMaterialFile = new System.Windows.Forms.Label();
			this.objMaterialOpenButton = new System.Windows.Forms.Button();
			this.txtMaterialFile = new System.Windows.Forms.TextBox();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.objModelConvert = new System.Windows.Forms.Button();
			this.lblModelActions = new System.Windows.Forms.Label();
			this.objModelViewButton = new System.Windows.Forms.Button();
			this.objModelBrowse = new System.Windows.Forms.Button();
			this.lblModelFile = new System.Windows.Forms.Label();
			this.txtModelFile = new System.Windows.Forms.TextBox();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.txtSceneStartPos = new System.Windows.Forms.TextBox();
			this.label24 = new System.Windows.Forms.Label();
			this.objSceneConvertButton = new System.Windows.Forms.Button();
			this.objSceneViewButton = new System.Windows.Forms.Button();
			this.label7 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.objSceneBrowseButton = new System.Windows.Forms.Button();
			this.txtSceneFile = new System.Windows.Forms.TextBox();
			this.tabPage5 = new System.Windows.Forms.TabPage();
			this.objClearSoundDataButton = new System.Windows.Forms.Button();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.txtSoundPriority = new System.Windows.Forms.TextBox();
			this.label25 = new System.Windows.Forms.Label();
			this.txtSoundBlockVolMul = new System.Windows.Forms.TextBox();
			this.label23 = new System.Windows.Forms.Label();
			this.label22 = new System.Windows.Forms.Label();
			this.objSoundBlockable = new System.Windows.Forms.ComboBox();
			this.txtSoundInterval = new System.Windows.Forms.TextBox();
			this.txtSoundRandom = new System.Windows.Forms.TextBox();
			this.label20 = new System.Windows.Forms.Label();
			this.label19 = new System.Windows.Forms.Label();
			this.txtSoundMaxDist = new System.Windows.Forms.TextBox();
			this.txtSoundMinDist = new System.Windows.Forms.TextBox();
			this.txtSoundVolume = new System.Windows.Forms.TextBox();
			this.label18 = new System.Windows.Forms.Label();
			this.label17 = new System.Windows.Forms.Label();
			this.label16 = new System.Windows.Forms.Label();
			this.objSoundStream = new System.Windows.Forms.ComboBox();
			this.objSoundUse3D = new System.Windows.Forms.ComboBox();
			this.objSoundLoop = new System.Windows.Forms.ComboBox();
			this.label15 = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.objStopSoundButton = new System.Windows.Forms.Button();
			this.objStartSoundButton = new System.Windows.Forms.Button();
			this.objMainSoundButton = new System.Windows.Forms.Button();
			this.txtStopSoundFile = new System.Windows.Forms.TextBox();
			this.txtStartSoundFile = new System.Windows.Forms.TextBox();
			this.label12 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.txtMainSoundFile = new System.Windows.Forms.TextBox();
			this.lblFadeS1 = new System.Windows.Forms.Label();
			this.objSoundFadeStart = new System.Windows.Forms.ComboBox();
			this.objSoundFadeEnd = new System.Windows.Forms.ComboBox();
			this.label21 = new System.Windows.Forms.Label();
			this.objSoundSaveAs = new System.Windows.Forms.Button();
			this.objSoundSave = new System.Windows.Forms.Button();
			this.objSoundOpen = new System.Windows.Forms.Button();
			this.txtSoundFile = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.tabPage6 = new System.Windows.Forms.TabPage();
			this.groupBox6 = new System.Windows.Forms.GroupBox();
			this.txtTransScriptName = new System.Windows.Forms.TextBox();
			this.objTransEntryChangeName = new System.Windows.Forms.Button();
			this.objTransEntryText = new System.Windows.Forms.TextBox();
			this.label29 = new System.Windows.Forms.Label();
			this.objTransEntryName = new System.Windows.Forms.TextBox();
			this.groupBox5 = new System.Windows.Forms.GroupBox();
			this.objTransCatRename = new System.Windows.Forms.Button();
			this.objTransEntryRemove = new System.Windows.Forms.Button();
			this.objTransCatRemove = new System.Windows.Forms.Button();
			this.objTransEntryButton = new System.Windows.Forms.Button();
			this.objTransCatButton = new System.Windows.Forms.Button();
			this.objTransEntries = new System.Windows.Forms.ComboBox();
			this.label28 = new System.Windows.Forms.Label();
			this.label27 = new System.Windows.Forms.Label();
			this.objTransCategories = new System.Windows.Forms.ComboBox();
			this.objTransSaveAs = new System.Windows.Forms.Button();
			this.objTransSave = new System.Windows.Forms.Button();
			this.objTransOpen = new System.Windows.Forms.Button();
			this.label26 = new System.Windows.Forms.Label();
			this.txtTransFile = new System.Windows.Forms.TextBox();
			this.tabControl1.SuspendLayout();
			this.tabPage4.SuspendLayout();
			this.tabPage3.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.tabPage5.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.tabPage6.SuspendLayout();
			this.groupBox6.SuspendLayout();
			this.groupBox5.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage4);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Controls.Add(this.tabPage5);
			this.tabControl1.Controls.Add(this.tabPage3);
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage6);
			this.tabControl1.Location = new System.Drawing.Point(0, 8);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(608, 472);
			this.tabControl1.TabIndex = 0;
			// 
			// tabPage4
			// 
			this.tabPage4.Controls.Add(this.objSaveResources);
			this.tabPage4.Controls.Add(this.objRemoveResourceDir);
			this.tabPage4.Controls.Add(this.objAddResourceDir);
			this.tabPage4.Controls.Add(this.objResourceDirs);
			this.tabPage4.Controls.Add(this.lblResourceDirs);
			this.tabPage4.Location = new System.Drawing.Point(4, 22);
			this.tabPage4.Name = "tabPage4";
			this.tabPage4.Size = new System.Drawing.Size(600, 446);
			this.tabPage4.TabIndex = 3;
			this.tabPage4.Text = "Settings";
			// 
			// objSaveResources
			// 
			this.objSaveResources.Location = new System.Drawing.Point(232, 120);
			this.objSaveResources.Name = "objSaveResources";
			this.objSaveResources.Size = new System.Drawing.Size(80, 24);
			this.objSaveResources.TabIndex = 4;
			this.objSaveResources.Text = "Save to file";
			this.objSaveResources.Click += new System.EventHandler(this.objSaveResources_Click);
			// 
			// objRemoveResourceDir
			// 
			this.objRemoveResourceDir.Location = new System.Drawing.Point(232, 88);
			this.objRemoveResourceDir.Name = "objRemoveResourceDir";
			this.objRemoveResourceDir.Size = new System.Drawing.Size(80, 24);
			this.objRemoveResourceDir.TabIndex = 3;
			this.objRemoveResourceDir.Text = "Remove";
			this.objRemoveResourceDir.Click += new System.EventHandler(this.objRemoveResourceDir_Click);
			// 
			// objAddResourceDir
			// 
			this.objAddResourceDir.Location = new System.Drawing.Point(232, 56);
			this.objAddResourceDir.Name = "objAddResourceDir";
			this.objAddResourceDir.Size = new System.Drawing.Size(80, 24);
			this.objAddResourceDir.TabIndex = 2;
			this.objAddResourceDir.Text = "Add";
			this.objAddResourceDir.Click += new System.EventHandler(this.objAddResourceDir_Click);
			// 
			// objResourceDirs
			// 
			this.objResourceDirs.Location = new System.Drawing.Point(16, 56);
			this.objResourceDirs.Name = "objResourceDirs";
			this.objResourceDirs.Size = new System.Drawing.Size(208, 147);
			this.objResourceDirs.TabIndex = 1;
			// 
			// lblResourceDirs
			// 
			this.lblResourceDirs.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblResourceDirs.Location = new System.Drawing.Point(16, 32);
			this.lblResourceDirs.Name = "lblResourceDirs";
			this.lblResourceDirs.Size = new System.Drawing.Size(128, 16);
			this.lblResourceDirs.TabIndex = 0;
			this.lblResourceDirs.Text = "Resource Directories:";
			// 
			// tabPage3
			// 
			this.tabPage3.Controls.Add(this.objMaterialClearButton);
			this.tabPage3.Controls.Add(this.objMaterialSaveAsButton);
			this.tabPage3.Controls.Add(this.groupBox2);
			this.tabPage3.Controls.Add(this.groupBox1);
			this.tabPage3.Controls.Add(this.objSaveMaterialButton);
			this.tabPage3.Controls.Add(this.lblMaterialFile);
			this.tabPage3.Controls.Add(this.objMaterialOpenButton);
			this.tabPage3.Controls.Add(this.txtMaterialFile);
			this.tabPage3.Location = new System.Drawing.Point(4, 22);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Size = new System.Drawing.Size(600, 406);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "Materials";
			// 
			// objMaterialClearButton
			// 
			this.objMaterialClearButton.Location = new System.Drawing.Point(8, 320);
			this.objMaterialClearButton.Name = "objMaterialClearButton";
			this.objMaterialClearButton.Size = new System.Drawing.Size(96, 24);
			this.objMaterialClearButton.TabIndex = 7;
			this.objMaterialClearButton.Text = "Clear Data";
			this.objMaterialClearButton.Click += new System.EventHandler(this.objMaterialClearButton_Click);
			// 
			// objMaterialSaveAsButton
			// 
			this.objMaterialSaveAsButton.Location = new System.Drawing.Point(520, 32);
			this.objMaterialSaveAsButton.Name = "objMaterialSaveAsButton";
			this.objMaterialSaveAsButton.Size = new System.Drawing.Size(56, 24);
			this.objMaterialSaveAsButton.TabIndex = 6;
			this.objMaterialSaveAsButton.Text = "Save As";
			this.objMaterialSaveAsButton.Click += new System.EventHandler(this.objMaterialSaveAsButton_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.objTextureType);
			this.groupBox2.Controls.Add(this.label34);
			this.groupBox2.Controls.Add(this.label33);
			this.groupBox2.Controls.Add(this.objTexAnimMode);
			this.groupBox2.Controls.Add(this.label32);
			this.groupBox2.Controls.Add(this.objWrapModes);
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.Controls.Add(this.objMipMaps);
			this.groupBox2.Controls.Add(this.objTexttureFileButton);
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.Controls.Add(this.txtTextureUnitFile);
			this.groupBox2.Controls.Add(this.objTextureUnitTypes);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Controls.Add(this.txtAnimFrameTime);
			this.groupBox2.Location = new System.Drawing.Point(8, 184);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(568, 128);
			this.groupBox2.TabIndex = 5;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Texture Units";
			// 
			// objTextureType
			// 
			this.objTextureType.Items.AddRange(new object[] {
																"1D",
																"2D",
																"Cube"});
			this.objTextureType.Location = new System.Drawing.Point(424, 80);
			this.objTextureType.Name = "objTextureType";
			this.objTextureType.Size = new System.Drawing.Size(104, 21);
			this.objTextureType.TabIndex = 14;
			this.objTextureType.SelectedIndexChanged += new System.EventHandler(this.objTextureType_SelectedIndexChanged);
			// 
			// label34
			// 
			this.label34.Location = new System.Drawing.Point(344, 80);
			this.label34.Name = "label34";
			this.label34.Size = new System.Drawing.Size(72, 24);
			this.label34.TabIndex = 13;
			this.label34.Text = "Type:";
			// 
			// label33
			// 
			this.label33.Location = new System.Drawing.Point(168, 96);
			this.label33.Name = "label33";
			this.label33.Size = new System.Drawing.Size(72, 24);
			this.label33.TabIndex = 11;
			this.label33.Text = "Frame time:";
			// 
			// objTexAnimMode
			// 
			this.objTexAnimMode.Items.AddRange(new object[] {
																"None",
																"Loop",
																"Oscillate"});
			this.objTexAnimMode.Location = new System.Drawing.Point(80, 96);
			this.objTexAnimMode.Name = "objTexAnimMode";
			this.objTexAnimMode.Size = new System.Drawing.Size(80, 21);
			this.objTexAnimMode.TabIndex = 10;
			this.objTexAnimMode.SelectedIndexChanged += new System.EventHandler(this.objTexAnimMode_SelectedIndexChanged);
			// 
			// label32
			// 
			this.label32.Location = new System.Drawing.Point(16, 96);
			this.label32.Name = "label32";
			this.label32.Size = new System.Drawing.Size(72, 24);
			this.label32.TabIndex = 9;
			this.label32.Text = "Anim mode:";
			// 
			// objWrapModes
			// 
			this.objWrapModes.Items.AddRange(new object[] {
															  "Repeat",
															  "Clamp",
															  "ClampToEdge"});
			this.objWrapModes.Location = new System.Drawing.Point(424, 48);
			this.objWrapModes.Name = "objWrapModes";
			this.objWrapModes.Size = new System.Drawing.Size(104, 21);
			this.objWrapModes.TabIndex = 8;
			this.objWrapModes.SelectedIndexChanged += new System.EventHandler(this.objWrapModes_SelectedIndexChanged);
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(344, 48);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(72, 24);
			this.label5.TabIndex = 7;
			this.label5.Text = "Wrap mode:";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(344, 16);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(64, 24);
			this.label4.TabIndex = 6;
			this.label4.Text = "Mip maps:";
			// 
			// objMipMaps
			// 
			this.objMipMaps.Items.AddRange(new object[] {
															"True",
															"False"});
			this.objMipMaps.Location = new System.Drawing.Point(424, 16);
			this.objMipMaps.Name = "objMipMaps";
			this.objMipMaps.Size = new System.Drawing.Size(104, 21);
			this.objMipMaps.TabIndex = 5;
			this.objMipMaps.SelectedIndexChanged += new System.EventHandler(this.objMipMaps_SelectedIndexChanged);
			// 
			// objTexttureFileButton
			// 
			this.objTexttureFileButton.Location = new System.Drawing.Point(272, 56);
			this.objTexttureFileButton.Name = "objTexttureFileButton";
			this.objTexttureFileButton.Size = new System.Drawing.Size(32, 24);
			this.objTexttureFileButton.TabIndex = 4;
			this.objTexttureFileButton.Text = "...";
			this.objTexttureFileButton.Click += new System.EventHandler(this.objTexttureFileButton_Click);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(16, 56);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(40, 16);
			this.label3.TabIndex = 3;
			this.label3.Text = "File:";
			// 
			// txtTextureUnitFile
			// 
			this.txtTextureUnitFile.Location = new System.Drawing.Point(56, 56);
			this.txtTextureUnitFile.Name = "txtTextureUnitFile";
			this.txtTextureUnitFile.Size = new System.Drawing.Size(208, 20);
			this.txtTextureUnitFile.TabIndex = 2;
			this.txtTextureUnitFile.Text = "";
			this.txtTextureUnitFile.TextChanged += new System.EventHandler(this.txtTextureUnitFile_TextChanged);
			// 
			// objTextureUnitTypes
			// 
			this.objTextureUnitTypes.Location = new System.Drawing.Point(56, 16);
			this.objTextureUnitTypes.Name = "objTextureUnitTypes";
			this.objTextureUnitTypes.Size = new System.Drawing.Size(248, 21);
			this.objTextureUnitTypes.TabIndex = 1;
			this.objTextureUnitTypes.SelectedIndexChanged += new System.EventHandler(this.objTextureUnitTypes_SelectedIndexChanged);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(16, 16);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(40, 24);
			this.label2.TabIndex = 0;
			this.label2.Text = "Unit:";
			// 
			// txtAnimFrameTime
			// 
			this.txtAnimFrameTime.Location = new System.Drawing.Point(240, 96);
			this.txtAnimFrameTime.MaxLength = 6;
			this.txtAnimFrameTime.Name = "txtAnimFrameTime";
			this.txtAnimFrameTime.Size = new System.Drawing.Size(64, 20);
			this.txtAnimFrameTime.TabIndex = 12;
			this.txtAnimFrameTime.Text = "";
			this.txtAnimFrameTime.TextChanged += new System.EventHandler(this.txtAnimFrameTime_TextChanged);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label35);
			this.groupBox1.Controls.Add(this.objDepthTest);
			this.groupBox1.Controls.Add(this.label31);
			this.groupBox1.Controls.Add(this.objUseAlpha);
			this.groupBox1.Controls.Add(this.label30);
			this.groupBox1.Controls.Add(this.objPhysicsMaterials);
			this.groupBox1.Controls.Add(this.label8);
			this.groupBox1.Controls.Add(this.objMaterialTypes);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.txtMatValue);
			this.groupBox1.Location = new System.Drawing.Point(8, 64);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(568, 112);
			this.groupBox1.TabIndex = 4;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "General";
			// 
			// label35
			// 
			this.label35.Location = new System.Drawing.Point(360, 80);
			this.label35.Name = "label35";
			this.label35.Size = new System.Drawing.Size(64, 16);
			this.label35.TabIndex = 13;
			this.label35.Text = "Value:";
			// 
			// objDepthTest
			// 
			this.objDepthTest.Items.AddRange(new object[] {
															  "True",
															  "False"});
			this.objDepthTest.Location = new System.Drawing.Point(432, 48);
			this.objDepthTest.Name = "objDepthTest";
			this.objDepthTest.Size = new System.Drawing.Size(120, 21);
			this.objDepthTest.TabIndex = 7;
			this.objDepthTest.SelectedIndexChanged += new System.EventHandler(this.objDepthTest_SelectedIndexChanged);
			// 
			// label31
			// 
			this.label31.Location = new System.Drawing.Point(360, 48);
			this.label31.Name = "label31";
			this.label31.Size = new System.Drawing.Size(64, 16);
			this.label31.TabIndex = 6;
			this.label31.Text = "Depth Test:";
			// 
			// objUseAlpha
			// 
			this.objUseAlpha.Items.AddRange(new object[] {
															 "True",
															 "False"});
			this.objUseAlpha.Location = new System.Drawing.Point(432, 16);
			this.objUseAlpha.Name = "objUseAlpha";
			this.objUseAlpha.Size = new System.Drawing.Size(120, 21);
			this.objUseAlpha.TabIndex = 5;
			this.objUseAlpha.SelectedIndexChanged += new System.EventHandler(this.objUseAlpha_SelectedIndexChanged);
			// 
			// label30
			// 
			this.label30.Location = new System.Drawing.Point(360, 16);
			this.label30.Name = "label30";
			this.label30.Size = new System.Drawing.Size(64, 16);
			this.label30.TabIndex = 4;
			this.label30.Text = "Use Alpha:";
			// 
			// objPhysicsMaterials
			// 
			this.objPhysicsMaterials.Location = new System.Drawing.Point(72, 48);
			this.objPhysicsMaterials.Name = "objPhysicsMaterials";
			this.objPhysicsMaterials.Size = new System.Drawing.Size(224, 21);
			this.objPhysicsMaterials.TabIndex = 3;
			this.objPhysicsMaterials.SelectedIndexChanged += new System.EventHandler(this.objPhysicsMaterials_SelectedIndexChanged);
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(16, 40);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(48, 24);
			this.label8.TabIndex = 2;
			this.label8.Text = "Physics Material";
			// 
			// objMaterialTypes
			// 
			this.objMaterialTypes.Location = new System.Drawing.Point(72, 16);
			this.objMaterialTypes.Name = "objMaterialTypes";
			this.objMaterialTypes.Size = new System.Drawing.Size(224, 21);
			this.objMaterialTypes.TabIndex = 1;
			this.objMaterialTypes.SelectedIndexChanged += new System.EventHandler(this.objMaterialTypes_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(32, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "Type:";
			// 
			// txtMatValue
			// 
			this.txtMatValue.Location = new System.Drawing.Point(432, 80);
			this.txtMatValue.MaxLength = 6;
			this.txtMatValue.Name = "txtMatValue";
			this.txtMatValue.Size = new System.Drawing.Size(120, 20);
			this.txtMatValue.TabIndex = 15;
			this.txtMatValue.Text = "";
			this.txtMatValue.TextChanged += new System.EventHandler(this.txtMatValue_TextChanged);
			// 
			// objSaveMaterialButton
			// 
			this.objSaveMaterialButton.Location = new System.Drawing.Point(464, 32);
			this.objSaveMaterialButton.Name = "objSaveMaterialButton";
			this.objSaveMaterialButton.Size = new System.Drawing.Size(48, 24);
			this.objSaveMaterialButton.TabIndex = 3;
			this.objSaveMaterialButton.Text = "Save";
			this.objSaveMaterialButton.Click += new System.EventHandler(this.objSaveMaterialButton_Click);
			// 
			// lblMaterialFile
			// 
			this.lblMaterialFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblMaterialFile.Location = new System.Drawing.Point(8, 16);
			this.lblMaterialFile.Name = "lblMaterialFile";
			this.lblMaterialFile.Size = new System.Drawing.Size(200, 16);
			this.lblMaterialFile.TabIndex = 2;
			this.lblMaterialFile.Text = "Material File";
			// 
			// objMaterialOpenButton
			// 
			this.objMaterialOpenButton.Location = new System.Drawing.Point(416, 32);
			this.objMaterialOpenButton.Name = "objMaterialOpenButton";
			this.objMaterialOpenButton.Size = new System.Drawing.Size(40, 24);
			this.objMaterialOpenButton.TabIndex = 1;
			this.objMaterialOpenButton.Text = "Open";
			this.objMaterialOpenButton.Click += new System.EventHandler(this.objMaterialBrowseButton_Click);
			// 
			// txtMaterialFile
			// 
			this.txtMaterialFile.Location = new System.Drawing.Point(8, 32);
			this.txtMaterialFile.Name = "txtMaterialFile";
			this.txtMaterialFile.ReadOnly = true;
			this.txtMaterialFile.Size = new System.Drawing.Size(400, 20);
			this.txtMaterialFile.TabIndex = 0;
			this.txtMaterialFile.Text = "";
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.objModelConvert);
			this.tabPage1.Controls.Add(this.lblModelActions);
			this.tabPage1.Controls.Add(this.objModelViewButton);
			this.tabPage1.Controls.Add(this.objModelBrowse);
			this.tabPage1.Controls.Add(this.lblModelFile);
			this.tabPage1.Controls.Add(this.txtModelFile);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Size = new System.Drawing.Size(600, 406);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Models";
			// 
			// objModelConvert
			// 
			this.objModelConvert.Location = new System.Drawing.Point(8, 136);
			this.objModelConvert.Name = "objModelConvert";
			this.objModelConvert.Size = new System.Drawing.Size(80, 24);
			this.objModelConvert.TabIndex = 5;
			this.objModelConvert.Text = "Convert";
			// 
			// lblModelActions
			// 
			this.lblModelActions.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblModelActions.Location = new System.Drawing.Point(8, 72);
			this.lblModelActions.Name = "lblModelActions";
			this.lblModelActions.Size = new System.Drawing.Size(88, 24);
			this.lblModelActions.TabIndex = 4;
			this.lblModelActions.Text = "Actions:";
			// 
			// objModelViewButton
			// 
			this.objModelViewButton.Location = new System.Drawing.Point(8, 96);
			this.objModelViewButton.Name = "objModelViewButton";
			this.objModelViewButton.Size = new System.Drawing.Size(80, 24);
			this.objModelViewButton.TabIndex = 3;
			this.objModelViewButton.Text = "View";
			this.objModelViewButton.Click += new System.EventHandler(this.objModelViewButton_Click);
			// 
			// objModelBrowse
			// 
			this.objModelBrowse.Location = new System.Drawing.Point(416, 32);
			this.objModelBrowse.Name = "objModelBrowse";
			this.objModelBrowse.Size = new System.Drawing.Size(96, 24);
			this.objModelBrowse.TabIndex = 2;
			this.objModelBrowse.Text = "Browse";
			this.objModelBrowse.Click += new System.EventHandler(this.objModelBrowse_Click);
			// 
			// lblModelFile
			// 
			this.lblModelFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblModelFile.Location = new System.Drawing.Point(8, 16);
			this.lblModelFile.Name = "lblModelFile";
			this.lblModelFile.Size = new System.Drawing.Size(136, 16);
			this.lblModelFile.TabIndex = 1;
			this.lblModelFile.Text = "Model File:";
			// 
			// txtModelFile
			// 
			this.txtModelFile.Location = new System.Drawing.Point(8, 32);
			this.txtModelFile.Name = "txtModelFile";
			this.txtModelFile.ReadOnly = true;
			this.txtModelFile.Size = new System.Drawing.Size(400, 20);
			this.txtModelFile.TabIndex = 0;
			this.txtModelFile.Text = "";
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.txtSceneStartPos);
			this.tabPage2.Controls.Add(this.label24);
			this.tabPage2.Controls.Add(this.objSceneConvertButton);
			this.tabPage2.Controls.Add(this.objSceneViewButton);
			this.tabPage2.Controls.Add(this.label7);
			this.tabPage2.Controls.Add(this.label6);
			this.tabPage2.Controls.Add(this.objSceneBrowseButton);
			this.tabPage2.Controls.Add(this.txtSceneFile);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Size = new System.Drawing.Size(600, 406);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Scenes";
			// 
			// txtSceneStartPos
			// 
			this.txtSceneStartPos.Location = new System.Drawing.Point(176, 96);
			this.txtSceneStartPos.Name = "txtSceneStartPos";
			this.txtSceneStartPos.Size = new System.Drawing.Size(136, 20);
			this.txtSceneStartPos.TabIndex = 7;
			this.txtSceneStartPos.Text = "";
			// 
			// label24
			// 
			this.label24.Location = new System.Drawing.Point(112, 96);
			this.label24.Name = "label24";
			this.label24.Size = new System.Drawing.Size(56, 24);
			this.label24.TabIndex = 6;
			this.label24.Text = "StartPos:";
			// 
			// objSceneConvertButton
			// 
			this.objSceneConvertButton.Location = new System.Drawing.Point(8, 160);
			this.objSceneConvertButton.Name = "objSceneConvertButton";
			this.objSceneConvertButton.Size = new System.Drawing.Size(80, 24);
			this.objSceneConvertButton.TabIndex = 5;
			this.objSceneConvertButton.Text = "Convert";
			// 
			// objSceneViewButton
			// 
			this.objSceneViewButton.Location = new System.Drawing.Point(8, 96);
			this.objSceneViewButton.Name = "objSceneViewButton";
			this.objSceneViewButton.Size = new System.Drawing.Size(80, 24);
			this.objSceneViewButton.TabIndex = 4;
			this.objSceneViewButton.Text = "View";
			this.objSceneViewButton.Click += new System.EventHandler(this.objSceneViewButton_Click);
			// 
			// label7
			// 
			this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label7.Location = new System.Drawing.Point(8, 72);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(112, 16);
			this.label7.TabIndex = 3;
			this.label7.Text = "Actions:";
			// 
			// label6
			// 
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label6.Location = new System.Drawing.Point(8, 16);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(136, 16);
			this.label6.TabIndex = 2;
			this.label6.Text = "Scene File:";
			// 
			// objSceneBrowseButton
			// 
			this.objSceneBrowseButton.Location = new System.Drawing.Point(416, 32);
			this.objSceneBrowseButton.Name = "objSceneBrowseButton";
			this.objSceneBrowseButton.Size = new System.Drawing.Size(96, 24);
			this.objSceneBrowseButton.TabIndex = 1;
			this.objSceneBrowseButton.Text = "Browse";
			this.objSceneBrowseButton.Click += new System.EventHandler(this.objSceneBrowseButton_Click);
			// 
			// txtSceneFile
			// 
			this.txtSceneFile.Location = new System.Drawing.Point(8, 32);
			this.txtSceneFile.Name = "txtSceneFile";
			this.txtSceneFile.ReadOnly = true;
			this.txtSceneFile.Size = new System.Drawing.Size(400, 20);
			this.txtSceneFile.TabIndex = 0;
			this.txtSceneFile.Text = "";
			// 
			// tabPage5
			// 
			this.tabPage5.Controls.Add(this.objClearSoundDataButton);
			this.tabPage5.Controls.Add(this.groupBox4);
			this.tabPage5.Controls.Add(this.groupBox3);
			this.tabPage5.Controls.Add(this.objSoundSaveAs);
			this.tabPage5.Controls.Add(this.objSoundSave);
			this.tabPage5.Controls.Add(this.objSoundOpen);
			this.tabPage5.Controls.Add(this.txtSoundFile);
			this.tabPage5.Controls.Add(this.label9);
			this.tabPage5.Location = new System.Drawing.Point(4, 22);
			this.tabPage5.Name = "tabPage5";
			this.tabPage5.Size = new System.Drawing.Size(600, 406);
			this.tabPage5.TabIndex = 4;
			this.tabPage5.Text = "Sounds";
			// 
			// objClearSoundDataButton
			// 
			this.objClearSoundDataButton.Location = new System.Drawing.Point(8, 376);
			this.objClearSoundDataButton.Name = "objClearSoundDataButton";
			this.objClearSoundDataButton.Size = new System.Drawing.Size(104, 24);
			this.objClearSoundDataButton.TabIndex = 7;
			this.objClearSoundDataButton.Text = "Clear Data";
			this.objClearSoundDataButton.Click += new System.EventHandler(this.objClearSoundDataButton_Click);
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.txtSoundPriority);
			this.groupBox4.Controls.Add(this.label25);
			this.groupBox4.Controls.Add(this.txtSoundBlockVolMul);
			this.groupBox4.Controls.Add(this.label23);
			this.groupBox4.Controls.Add(this.label22);
			this.groupBox4.Controls.Add(this.objSoundBlockable);
			this.groupBox4.Controls.Add(this.txtSoundInterval);
			this.groupBox4.Controls.Add(this.txtSoundRandom);
			this.groupBox4.Controls.Add(this.label20);
			this.groupBox4.Controls.Add(this.label19);
			this.groupBox4.Controls.Add(this.txtSoundMaxDist);
			this.groupBox4.Controls.Add(this.txtSoundMinDist);
			this.groupBox4.Controls.Add(this.txtSoundVolume);
			this.groupBox4.Controls.Add(this.label18);
			this.groupBox4.Controls.Add(this.label17);
			this.groupBox4.Controls.Add(this.label16);
			this.groupBox4.Controls.Add(this.objSoundStream);
			this.groupBox4.Controls.Add(this.objSoundUse3D);
			this.groupBox4.Controls.Add(this.objSoundLoop);
			this.groupBox4.Controls.Add(this.label15);
			this.groupBox4.Controls.Add(this.label14);
			this.groupBox4.Controls.Add(this.label13);
			this.groupBox4.Location = new System.Drawing.Point(8, 200);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(568, 168);
			this.groupBox4.TabIndex = 6;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Properties";
			// 
			// txtSoundPriority
			// 
			this.txtSoundPriority.Location = new System.Drawing.Point(400, 88);
			this.txtSoundPriority.MaxLength = 8;
			this.txtSoundPriority.Name = "txtSoundPriority";
			this.txtSoundPriority.Size = new System.Drawing.Size(72, 20);
			this.txtSoundPriority.TabIndex = 21;
			this.txtSoundPriority.Text = "";
			this.txtSoundPriority.TextChanged += new System.EventHandler(this.txtSoundPriority_TextChanged);
			// 
			// label25
			// 
			this.label25.Location = new System.Drawing.Point(336, 88);
			this.label25.Name = "label25";
			this.label25.Size = new System.Drawing.Size(56, 16);
			this.label25.TabIndex = 20;
			this.label25.Text = "Priority:";
			// 
			// txtSoundBlockVolMul
			// 
			this.txtSoundBlockVolMul.Location = new System.Drawing.Point(240, 136);
			this.txtSoundBlockVolMul.MaxLength = 8;
			this.txtSoundBlockVolMul.Name = "txtSoundBlockVolMul";
			this.txtSoundBlockVolMul.Size = new System.Drawing.Size(72, 20);
			this.txtSoundBlockVolMul.TabIndex = 19;
			this.txtSoundBlockVolMul.Text = "";
			this.txtSoundBlockVolMul.TextChanged += new System.EventHandler(this.txtSoundBlockVolMul_TextChanged);
			// 
			// label23
			// 
			this.label23.Location = new System.Drawing.Point(160, 136);
			this.label23.Name = "label23";
			this.label23.Size = new System.Drawing.Size(80, 16);
			this.label23.TabIndex = 18;
			this.label23.Text = "Block Volume:";
			// 
			// label22
			// 
			this.label22.Location = new System.Drawing.Point(8, 136);
			this.label22.Name = "label22";
			this.label22.Size = new System.Drawing.Size(56, 16);
			this.label22.TabIndex = 17;
			this.label22.Text = "Blockable:";
			// 
			// objSoundBlockable
			// 
			this.objSoundBlockable.Items.AddRange(new object[] {
																   "True",
																   "False"});
			this.objSoundBlockable.Location = new System.Drawing.Point(72, 136);
			this.objSoundBlockable.Name = "objSoundBlockable";
			this.objSoundBlockable.Size = new System.Drawing.Size(72, 21);
			this.objSoundBlockable.TabIndex = 16;
			this.objSoundBlockable.SelectedIndexChanged += new System.EventHandler(this.objSoundBlockable_SelectedIndexChanged);
			// 
			// txtSoundInterval
			// 
			this.txtSoundInterval.Location = new System.Drawing.Point(400, 56);
			this.txtSoundInterval.MaxLength = 8;
			this.txtSoundInterval.Name = "txtSoundInterval";
			this.txtSoundInterval.Size = new System.Drawing.Size(72, 20);
			this.txtSoundInterval.TabIndex = 15;
			this.txtSoundInterval.Text = "";
			this.txtSoundInterval.TextChanged += new System.EventHandler(this.txtSoundInterval_TextChanged);
			// 
			// txtSoundRandom
			// 
			this.txtSoundRandom.Location = new System.Drawing.Point(400, 24);
			this.txtSoundRandom.MaxLength = 8;
			this.txtSoundRandom.Name = "txtSoundRandom";
			this.txtSoundRandom.Size = new System.Drawing.Size(72, 20);
			this.txtSoundRandom.TabIndex = 14;
			this.txtSoundRandom.Text = "";
			this.txtSoundRandom.TextChanged += new System.EventHandler(this.txtSoundRandom_TextChanged);
			// 
			// label20
			// 
			this.label20.Location = new System.Drawing.Point(336, 56);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(56, 16);
			this.label20.TabIndex = 13;
			this.label20.Text = "Interval:";
			// 
			// label19
			// 
			this.label19.Location = new System.Drawing.Point(336, 24);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(56, 16);
			this.label19.TabIndex = 12;
			this.label19.Text = "Random:";
			// 
			// txtSoundMaxDist
			// 
			this.txtSoundMaxDist.Location = new System.Drawing.Point(240, 88);
			this.txtSoundMaxDist.MaxLength = 8;
			this.txtSoundMaxDist.Name = "txtSoundMaxDist";
			this.txtSoundMaxDist.Size = new System.Drawing.Size(72, 20);
			this.txtSoundMaxDist.TabIndex = 11;
			this.txtSoundMaxDist.Text = "";
			this.txtSoundMaxDist.TextChanged += new System.EventHandler(this.txtSoundMaxDist_TextChanged);
			// 
			// txtSoundMinDist
			// 
			this.txtSoundMinDist.Location = new System.Drawing.Point(240, 56);
			this.txtSoundMinDist.MaxLength = 8;
			this.txtSoundMinDist.Name = "txtSoundMinDist";
			this.txtSoundMinDist.Size = new System.Drawing.Size(72, 20);
			this.txtSoundMinDist.TabIndex = 10;
			this.txtSoundMinDist.Text = "";
			this.txtSoundMinDist.TextChanged += new System.EventHandler(this.txtSoundMinDist_TextChanged);
			// 
			// txtSoundVolume
			// 
			this.txtSoundVolume.Location = new System.Drawing.Point(240, 24);
			this.txtSoundVolume.MaxLength = 8;
			this.txtSoundVolume.Name = "txtSoundVolume";
			this.txtSoundVolume.Size = new System.Drawing.Size(72, 20);
			this.txtSoundVolume.TabIndex = 9;
			this.txtSoundVolume.Text = "";
			this.txtSoundVolume.TextChanged += new System.EventHandler(this.txtSoundVolume_TextChanged);
			// 
			// label18
			// 
			this.label18.Location = new System.Drawing.Point(160, 88);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(80, 16);
			this.label18.TabIndex = 8;
			this.label18.Text = "Max Distance:";
			// 
			// label17
			// 
			this.label17.Location = new System.Drawing.Point(160, 56);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(80, 16);
			this.label17.TabIndex = 7;
			this.label17.Text = "Min Distance:";
			// 
			// label16
			// 
			this.label16.Location = new System.Drawing.Point(160, 24);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(80, 16);
			this.label16.TabIndex = 6;
			this.label16.Text = "Volume:";
			// 
			// objSoundStream
			// 
			this.objSoundStream.Items.AddRange(new object[] {
																"True",
																"False"});
			this.objSoundStream.Location = new System.Drawing.Point(64, 88);
			this.objSoundStream.Name = "objSoundStream";
			this.objSoundStream.Size = new System.Drawing.Size(64, 21);
			this.objSoundStream.TabIndex = 5;
			this.objSoundStream.SelectedIndexChanged += new System.EventHandler(this.objSoundStream_SelectedIndexChanged);
			// 
			// objSoundUse3D
			// 
			this.objSoundUse3D.Items.AddRange(new object[] {
															   "True",
															   "False"});
			this.objSoundUse3D.Location = new System.Drawing.Point(64, 56);
			this.objSoundUse3D.Name = "objSoundUse3D";
			this.objSoundUse3D.Size = new System.Drawing.Size(64, 21);
			this.objSoundUse3D.TabIndex = 4;
			this.objSoundUse3D.SelectedIndexChanged += new System.EventHandler(this.objSoundUse3D_SelectedIndexChanged);
			// 
			// objSoundLoop
			// 
			this.objSoundLoop.Items.AddRange(new object[] {
															  "True",
															  "False"});
			this.objSoundLoop.Location = new System.Drawing.Point(64, 24);
			this.objSoundLoop.Name = "objSoundLoop";
			this.objSoundLoop.Size = new System.Drawing.Size(64, 21);
			this.objSoundLoop.TabIndex = 3;
			this.objSoundLoop.SelectedIndexChanged += new System.EventHandler(this.objSoundLoop_SelectedIndexChanged);
			// 
			// label15
			// 
			this.label15.Location = new System.Drawing.Point(8, 88);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(56, 16);
			this.label15.TabIndex = 2;
			this.label15.Text = "Stream:";
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(8, 56);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(56, 16);
			this.label14.TabIndex = 1;
			this.label14.Text = "Use 3D:";
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(8, 24);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(56, 16);
			this.label13.TabIndex = 0;
			this.label13.Text = "Loop:";
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.objStopSoundButton);
			this.groupBox3.Controls.Add(this.objStartSoundButton);
			this.groupBox3.Controls.Add(this.objMainSoundButton);
			this.groupBox3.Controls.Add(this.txtStopSoundFile);
			this.groupBox3.Controls.Add(this.txtStartSoundFile);
			this.groupBox3.Controls.Add(this.label12);
			this.groupBox3.Controls.Add(this.label11);
			this.groupBox3.Controls.Add(this.label10);
			this.groupBox3.Controls.Add(this.txtMainSoundFile);
			this.groupBox3.Controls.Add(this.lblFadeS1);
			this.groupBox3.Controls.Add(this.objSoundFadeStart);
			this.groupBox3.Controls.Add(this.objSoundFadeEnd);
			this.groupBox3.Controls.Add(this.label21);
			this.groupBox3.Location = new System.Drawing.Point(8, 64);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(568, 120);
			this.groupBox3.TabIndex = 5;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "General";
			// 
			// objStopSoundButton
			// 
			this.objStopSoundButton.Location = new System.Drawing.Point(288, 88);
			this.objStopSoundButton.Name = "objStopSoundButton";
			this.objStopSoundButton.Size = new System.Drawing.Size(24, 24);
			this.objStopSoundButton.TabIndex = 8;
			this.objStopSoundButton.Text = "...";
			this.objStopSoundButton.Click += new System.EventHandler(this.objStopSoundButton_Click);
			// 
			// objStartSoundButton
			// 
			this.objStartSoundButton.Location = new System.Drawing.Point(288, 56);
			this.objStartSoundButton.Name = "objStartSoundButton";
			this.objStartSoundButton.Size = new System.Drawing.Size(24, 24);
			this.objStartSoundButton.TabIndex = 7;
			this.objStartSoundButton.Text = "...";
			this.objStartSoundButton.Click += new System.EventHandler(this.objStartSoundButton_Click);
			// 
			// objMainSoundButton
			// 
			this.objMainSoundButton.Location = new System.Drawing.Point(288, 24);
			this.objMainSoundButton.Name = "objMainSoundButton";
			this.objMainSoundButton.Size = new System.Drawing.Size(24, 24);
			this.objMainSoundButton.TabIndex = 6;
			this.objMainSoundButton.Text = "...";
			this.objMainSoundButton.Click += new System.EventHandler(this.objMainSoundButton_Click);
			// 
			// txtStopSoundFile
			// 
			this.txtStopSoundFile.Location = new System.Drawing.Point(80, 88);
			this.txtStopSoundFile.Name = "txtStopSoundFile";
			this.txtStopSoundFile.Size = new System.Drawing.Size(200, 20);
			this.txtStopSoundFile.TabIndex = 5;
			this.txtStopSoundFile.Text = "";
			this.txtStopSoundFile.TextChanged += new System.EventHandler(this.txtStopSoundFile_TextChanged);
			// 
			// txtStartSoundFile
			// 
			this.txtStartSoundFile.Location = new System.Drawing.Point(80, 56);
			this.txtStartSoundFile.Name = "txtStartSoundFile";
			this.txtStartSoundFile.Size = new System.Drawing.Size(200, 20);
			this.txtStartSoundFile.TabIndex = 4;
			this.txtStartSoundFile.Text = "";
			this.txtStartSoundFile.TextChanged += new System.EventHandler(this.txtStartSoundFile_TextChanged);
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(8, 88);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(72, 16);
			this.label12.TabIndex = 3;
			this.label12.Text = "Stop Sound:";
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(8, 56);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(72, 16);
			this.label11.TabIndex = 2;
			this.label11.Text = "Start Sound:";
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(8, 24);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(72, 16);
			this.label10.TabIndex = 1;
			this.label10.Text = "Main Sound:";
			// 
			// txtMainSoundFile
			// 
			this.txtMainSoundFile.Location = new System.Drawing.Point(80, 24);
			this.txtMainSoundFile.Name = "txtMainSoundFile";
			this.txtMainSoundFile.Size = new System.Drawing.Size(200, 20);
			this.txtMainSoundFile.TabIndex = 0;
			this.txtMainSoundFile.Text = "";
			this.txtMainSoundFile.TextChanged += new System.EventHandler(this.txtMainSoundFile_TextChanged);
			// 
			// lblFadeS1
			// 
			this.lblFadeS1.Location = new System.Drawing.Point(360, 24);
			this.lblFadeS1.Name = "lblFadeS1";
			this.lblFadeS1.Size = new System.Drawing.Size(64, 16);
			this.lblFadeS1.TabIndex = 16;
			this.lblFadeS1.Text = "Fade Start:";
			// 
			// objSoundFadeStart
			// 
			this.objSoundFadeStart.Items.AddRange(new object[] {
																   "True",
																   "False"});
			this.objSoundFadeStart.Location = new System.Drawing.Point(432, 24);
			this.objSoundFadeStart.Name = "objSoundFadeStart";
			this.objSoundFadeStart.Size = new System.Drawing.Size(64, 21);
			this.objSoundFadeStart.TabIndex = 18;
			this.objSoundFadeStart.SelectedIndexChanged += new System.EventHandler(this.objSoundFadeStart_SelectedIndexChanged);
			// 
			// objSoundFadeEnd
			// 
			this.objSoundFadeEnd.Items.AddRange(new object[] {
																 "True",
																 "False"});
			this.objSoundFadeEnd.Location = new System.Drawing.Point(432, 56);
			this.objSoundFadeEnd.Name = "objSoundFadeEnd";
			this.objSoundFadeEnd.Size = new System.Drawing.Size(64, 21);
			this.objSoundFadeEnd.TabIndex = 19;
			this.objSoundFadeEnd.SelectedIndexChanged += new System.EventHandler(this.objSoundFadeEnd_SelectedIndexChanged);
			// 
			// label21
			// 
			this.label21.Location = new System.Drawing.Point(360, 56);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(64, 16);
			this.label21.TabIndex = 17;
			this.label21.Text = "Fade End:";
			// 
			// objSoundSaveAs
			// 
			this.objSoundSaveAs.Location = new System.Drawing.Point(520, 32);
			this.objSoundSaveAs.Name = "objSoundSaveAs";
			this.objSoundSaveAs.Size = new System.Drawing.Size(56, 24);
			this.objSoundSaveAs.TabIndex = 4;
			this.objSoundSaveAs.Text = "Save As";
			this.objSoundSaveAs.Click += new System.EventHandler(this.objSoundSaveAs_Click);
			// 
			// objSoundSave
			// 
			this.objSoundSave.Location = new System.Drawing.Point(464, 32);
			this.objSoundSave.Name = "objSoundSave";
			this.objSoundSave.Size = new System.Drawing.Size(48, 24);
			this.objSoundSave.TabIndex = 3;
			this.objSoundSave.Text = "Save";
			this.objSoundSave.Click += new System.EventHandler(this.objSoundSave_Click);
			// 
			// objSoundOpen
			// 
			this.objSoundOpen.Location = new System.Drawing.Point(416, 32);
			this.objSoundOpen.Name = "objSoundOpen";
			this.objSoundOpen.Size = new System.Drawing.Size(40, 24);
			this.objSoundOpen.TabIndex = 2;
			this.objSoundOpen.Text = "Open";
			this.objSoundOpen.Click += new System.EventHandler(this.objSoundOpen_Click);
			// 
			// txtSoundFile
			// 
			this.txtSoundFile.Location = new System.Drawing.Point(8, 32);
			this.txtSoundFile.Name = "txtSoundFile";
			this.txtSoundFile.ReadOnly = true;
			this.txtSoundFile.Size = new System.Drawing.Size(400, 20);
			this.txtSoundFile.TabIndex = 1;
			this.txtSoundFile.Text = "";
			// 
			// label9
			// 
			this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label9.Location = new System.Drawing.Point(8, 16);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(144, 16);
			this.label9.TabIndex = 0;
			this.label9.Text = "Sound File";
			// 
			// tabPage6
			// 
			this.tabPage6.Controls.Add(this.groupBox6);
			this.tabPage6.Controls.Add(this.groupBox5);
			this.tabPage6.Controls.Add(this.objTransSaveAs);
			this.tabPage6.Controls.Add(this.objTransSave);
			this.tabPage6.Controls.Add(this.objTransOpen);
			this.tabPage6.Controls.Add(this.label26);
			this.tabPage6.Controls.Add(this.txtTransFile);
			this.tabPage6.Location = new System.Drawing.Point(4, 22);
			this.tabPage6.Name = "tabPage6";
			this.tabPage6.Size = new System.Drawing.Size(600, 446);
			this.tabPage6.TabIndex = 5;
			this.tabPage6.Text = "Translation";
			// 
			// groupBox6
			// 
			this.groupBox6.Controls.Add(this.txtTransScriptName);
			this.groupBox6.Controls.Add(this.objTransEntryChangeName);
			this.groupBox6.Controls.Add(this.objTransEntryText);
			this.groupBox6.Controls.Add(this.label29);
			this.groupBox6.Controls.Add(this.objTransEntryName);
			this.groupBox6.Location = new System.Drawing.Point(16, 176);
			this.groupBox6.Name = "groupBox6";
			this.groupBox6.Size = new System.Drawing.Size(560, 264);
			this.groupBox6.TabIndex = 6;
			this.groupBox6.TabStop = false;
			this.groupBox6.Text = "Text Entry";
			// 
			// txtTransScriptName
			// 
			this.txtTransScriptName.Location = new System.Drawing.Point(288, 16);
			this.txtTransScriptName.Name = "txtTransScriptName";
			this.txtTransScriptName.ReadOnly = true;
			this.txtTransScriptName.Size = new System.Drawing.Size(264, 20);
			this.txtTransScriptName.TabIndex = 4;
			this.txtTransScriptName.Text = "";
			// 
			// objTransEntryChangeName
			// 
			this.objTransEntryChangeName.Location = new System.Drawing.Point(192, 16);
			this.objTransEntryChangeName.Name = "objTransEntryChangeName";
			this.objTransEntryChangeName.Size = new System.Drawing.Size(88, 24);
			this.objTransEntryChangeName.TabIndex = 3;
			this.objTransEntryChangeName.Text = "Change Name";
			this.objTransEntryChangeName.Click += new System.EventHandler(this.objTransEntryChangeName_Click);
			// 
			// objTransEntryText
			// 
			this.objTransEntryText.Location = new System.Drawing.Point(16, 48);
			this.objTransEntryText.Multiline = true;
			this.objTransEntryText.Name = "objTransEntryText";
			this.objTransEntryText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.objTransEntryText.Size = new System.Drawing.Size(536, 208);
			this.objTransEntryText.TabIndex = 2;
			this.objTransEntryText.Text = "";
			this.objTransEntryText.TextChanged += new System.EventHandler(this.objTransEntryText_TextChanged);
			// 
			// label29
			// 
			this.label29.Location = new System.Drawing.Point(16, 16);
			this.label29.Name = "label29";
			this.label29.Size = new System.Drawing.Size(40, 16);
			this.label29.TabIndex = 1;
			this.label29.Text = "Name:";
			// 
			// objTransEntryName
			// 
			this.objTransEntryName.Location = new System.Drawing.Point(56, 16);
			this.objTransEntryName.Name = "objTransEntryName";
			this.objTransEntryName.Size = new System.Drawing.Size(128, 20);
			this.objTransEntryName.TabIndex = 0;
			this.objTransEntryName.Text = "";
			this.objTransEntryName.TextChanged += new System.EventHandler(this.objTransEntryName_TextChanged);
			// 
			// groupBox5
			// 
			this.groupBox5.Controls.Add(this.objTransCatRename);
			this.groupBox5.Controls.Add(this.objTransEntryRemove);
			this.groupBox5.Controls.Add(this.objTransCatRemove);
			this.groupBox5.Controls.Add(this.objTransEntryButton);
			this.groupBox5.Controls.Add(this.objTransCatButton);
			this.groupBox5.Controls.Add(this.objTransEntries);
			this.groupBox5.Controls.Add(this.label28);
			this.groupBox5.Controls.Add(this.label27);
			this.groupBox5.Controls.Add(this.objTransCategories);
			this.groupBox5.Location = new System.Drawing.Point(16, 64);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.Size = new System.Drawing.Size(560, 96);
			this.groupBox5.TabIndex = 5;
			this.groupBox5.TabStop = false;
			this.groupBox5.Text = "Texts";
			// 
			// objTransCatRename
			// 
			this.objTransCatRename.Location = new System.Drawing.Point(384, 16);
			this.objTransCatRename.Name = "objTransCatRename";
			this.objTransCatRename.Size = new System.Drawing.Size(64, 24);
			this.objTransCatRename.TabIndex = 8;
			this.objTransCatRename.Text = "Rename";
			this.objTransCatRename.Click += new System.EventHandler(this.objTransCatRename_Click);
			// 
			// objTransEntryRemove
			// 
			this.objTransEntryRemove.Location = new System.Drawing.Point(424, 56);
			this.objTransEntryRemove.Name = "objTransEntryRemove";
			this.objTransEntryRemove.Size = new System.Drawing.Size(104, 24);
			this.objTransEntryRemove.TabIndex = 7;
			this.objTransEntryRemove.Text = "Remove Entry";
			this.objTransEntryRemove.Click += new System.EventHandler(this.objTransEntryRemove_Click);
			// 
			// objTransCatRemove
			// 
			this.objTransCatRemove.Location = new System.Drawing.Point(456, 16);
			this.objTransCatRemove.Name = "objTransCatRemove";
			this.objTransCatRemove.Size = new System.Drawing.Size(72, 24);
			this.objTransCatRemove.TabIndex = 6;
			this.objTransCatRemove.Text = "Remove";
			this.objTransCatRemove.Click += new System.EventHandler(this.objTransCatRemove_Click);
			// 
			// objTransEntryButton
			// 
			this.objTransEntryButton.Location = new System.Drawing.Point(320, 56);
			this.objTransEntryButton.Name = "objTransEntryButton";
			this.objTransEntryButton.Size = new System.Drawing.Size(88, 24);
			this.objTransEntryButton.TabIndex = 5;
			this.objTransEntryButton.Text = "Add Entry";
			this.objTransEntryButton.Click += new System.EventHandler(this.objTransEntryButton_Click);
			// 
			// objTransCatButton
			// 
			this.objTransCatButton.Location = new System.Drawing.Point(320, 16);
			this.objTransCatButton.Name = "objTransCatButton";
			this.objTransCatButton.Size = new System.Drawing.Size(56, 24);
			this.objTransCatButton.TabIndex = 4;
			this.objTransCatButton.Text = "Add";
			this.objTransCatButton.Click += new System.EventHandler(this.objTransCatButton_Click);
			// 
			// objTransEntries
			// 
			this.objTransEntries.Location = new System.Drawing.Point(88, 56);
			this.objTransEntries.MaxDropDownItems = 18;
			this.objTransEntries.Name = "objTransEntries";
			this.objTransEntries.Size = new System.Drawing.Size(216, 21);
			this.objTransEntries.Sorted = true;
			this.objTransEntries.TabIndex = 3;
			this.objTransEntries.SelectedIndexChanged += new System.EventHandler(this.objTransEntries_SelectedIndexChanged);
			// 
			// label28
			// 
			this.label28.Location = new System.Drawing.Point(8, 56);
			this.label28.Name = "label28";
			this.label28.Size = new System.Drawing.Size(72, 24);
			this.label28.TabIndex = 2;
			this.label28.Text = "Entry:";
			// 
			// label27
			// 
			this.label27.Location = new System.Drawing.Point(8, 16);
			this.label27.Name = "label27";
			this.label27.Size = new System.Drawing.Size(72, 16);
			this.label27.TabIndex = 1;
			this.label27.Text = "Category:";
			// 
			// objTransCategories
			// 
			this.objTransCategories.Location = new System.Drawing.Point(88, 16);
			this.objTransCategories.Name = "objTransCategories";
			this.objTransCategories.Size = new System.Drawing.Size(216, 21);
			this.objTransCategories.Sorted = true;
			this.objTransCategories.TabIndex = 0;
			this.objTransCategories.SelectedIndexChanged += new System.EventHandler(this.objTransCategories_SelectedIndexChanged);
			// 
			// objTransSaveAs
			// 
			this.objTransSaveAs.Location = new System.Drawing.Point(520, 32);
			this.objTransSaveAs.Name = "objTransSaveAs";
			this.objTransSaveAs.Size = new System.Drawing.Size(56, 24);
			this.objTransSaveAs.TabIndex = 4;
			this.objTransSaveAs.Text = "Save As";
			this.objTransSaveAs.Click += new System.EventHandler(this.objTransSaveAs_Click);
			// 
			// objTransSave
			// 
			this.objTransSave.Location = new System.Drawing.Point(464, 32);
			this.objTransSave.Name = "objTransSave";
			this.objTransSave.Size = new System.Drawing.Size(48, 24);
			this.objTransSave.TabIndex = 3;
			this.objTransSave.Text = "Save";
			this.objTransSave.Click += new System.EventHandler(this.objTransSave_Click);
			// 
			// objTransOpen
			// 
			this.objTransOpen.Location = new System.Drawing.Point(416, 32);
			this.objTransOpen.Name = "objTransOpen";
			this.objTransOpen.Size = new System.Drawing.Size(40, 24);
			this.objTransOpen.TabIndex = 2;
			this.objTransOpen.Text = "Open";
			this.objTransOpen.Click += new System.EventHandler(this.objTransOpen_Click);
			// 
			// label26
			// 
			this.label26.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label26.Location = new System.Drawing.Point(8, 16);
			this.label26.Name = "label26";
			this.label26.Size = new System.Drawing.Size(96, 16);
			this.label26.TabIndex = 1;
			this.label26.Text = "Translation File";
			// 
			// txtTransFile
			// 
			this.txtTransFile.Location = new System.Drawing.Point(8, 32);
			this.txtTransFile.Name = "txtTransFile";
			this.txtTransFile.ReadOnly = true;
			this.txtTransFile.Size = new System.Drawing.Size(400, 20);
			this.txtTransFile.TabIndex = 0;
			this.txtTransFile.Text = "";
			// 
			// frmMain
			// 
			this.AutoScale = false;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(594, 479);
			this.Controls.Add(this.tabControl1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "frmMain";
			this.Text = "HPL Helper";
			this.tabControl1.ResumeLayout(false);
			this.tabPage4.ResumeLayout(false);
			this.tabPage3.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.tabPage5.ResumeLayout(false);
			this.groupBox4.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.tabPage6.ResumeLayout(false);
			this.groupBox6.ResumeLayout(false);
			this.groupBox5.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new frmMain());
		}

		//----------------------------------------------------------
		
		private void LoadPhysicsMaterialNames(String asFile)
		{
			XmlDocument Doc = new XmlDocument();
			
			try
			{
				Doc.Load(asFile);
			}
			catch (XmlException e)
			{
				MessageBox.Show("Couldn't load "+asFile+" Error:" +e,"Error");
				return;
			}

			XmlElement DocRoot = (XmlElement)Doc.FirstChild;
						
			//Iterate trough children
			for(int i=0;i< DocRoot.ChildNodes.Count;i++)
			{
				XmlElement MatNode = (XmlElement)DocRoot.ChildNodes[i];
				
				String sName = MatNode.GetAttribute("Name");
				
				objPhysicsMaterials.Items.Add(sName);
			}

			objPhysicsMaterials.SelectedIndex =0;
		}


		//----------------------------------------------------------

		private void LoadResourceDirs(String asFile)
		{
			XmlDocument Doc = new XmlDocument();
			Doc.Load(asFile);

			XmlElement DocRoot = (XmlElement)Doc.FirstChild;
						
			//Iterate trough all directories.
			for(int i=0;i< DocRoot.ChildNodes.Count;i++)
			{
				XmlElement DirNode = (XmlElement)DocRoot.ChildNodes[i];
				
				String sPath = DirNode.GetAttribute("Path");
				
				objResourceDirs.Items.Add(sPath);
			}
		}

		private void SaveResourceDirs(String asFile)
		{
			XmlDocument Doc = new XmlDocument();

            XmlElement DocRoot = Doc.CreateElement("Resources");
			Doc.AppendChild(DocRoot);
			
			//Iterate resource list
			for(int i=0; i< objResourceDirs.Items.Count; i++)
			{
				String sPath = (String)objResourceDirs.Items[i];
				
				XmlElement ChildElem = Doc.CreateElement("Directory");
				
				ChildElem.SetAttribute("Path", sPath);
				
				DocRoot.AppendChild(ChildElem);
			}

			Doc.Save(msResourceDirFile);
		}

		//----------------------------------------------------------

		private void LoadMaterial(String asFile)
		{
			mpMaterial.Load(asFile);

			objMaterialTypes.SelectedIndex = (int)mpMaterial.mType;
			objTextureUnitTypes.SelectedIndex =0;
			objUseAlpha.SelectedIndex = mpMaterial.mbUseAlpha?0:1;
			objDepthTest.SelectedIndex = mpMaterial.mbDepthTest?0:1;
			txtMatValue.Text = mpMaterial.msValue;
			
			
			UpdateTextureUnitData();
		}

		private void UpdateTextureUnitData()
		{
			int lIdx = objTextureUnitTypes.SelectedIndex;

			HplTextureUnit TexUnit = (HplTextureUnit)mpMaterial.mvTextureUnits[lIdx];
			
			txtTextureUnitFile.Text = TexUnit.msFile;
			objMipMaps.SelectedIndex = TexUnit.mbMipMaps ? 0:1;
			objWrapModes.SelectedIndex = (int)TexUnit.mWrapMode;
			objTextureType.SelectedItem = TexUnit.msType;
			txtAnimFrameTime.Text = TexUnit.msFrameTime;
			
			for(int i=0; i< objTexAnimMode.Items.Count; ++i)
			{
				if((String)objTexAnimMode.Items[i] == TexUnit.msAnimMode)
				{
					objTexAnimMode.SelectedIndex = i;
					break;
				}
			}
			
		}

		//----------------------------------------------------------

		private void objModelBrowse_Click(object sender, System.EventArgs e)
		{
			mOpenFileDialog.Filter = 
			"All Valid files (*.dae;*.ent)|*.dae;*.ent|Collada files (*.dae)|*.dae|Entity files (*.ent)|*.ent";
			
			if(mOpenFileDialog.ShowDialog()== DialogResult.OK)
			{
				//Reset the file dialog.
				mOpenFileDialog.InitialDirectory = Path.GetDirectoryName(mOpenFileDialog.FileName);
			
				txtModelFile.Text = mOpenFileDialog.FileName;
				mOpenFileDialog.FileName ="";
			}			
		}

		//----------------------------------------------------------

		private void objModelViewButton_Click(object sender, System.EventArgs e)
		{
			String sFile = Path.GetFileName(txtModelFile.Text);
			HplSystem.RunProgam("ModelViewer.exe",sFile);
		}

		//----------------------------------------------------------

		private void objRemoveResourceDir_Click(object sender, System.EventArgs e)
		{
			if(objResourceDirs.SelectedIndex>=0)
			{
				objResourceDirs.Items.RemoveAt(objResourceDirs.SelectedIndex);
			}
		}

		private void objAddResourceDir_Click(object sender, System.EventArgs e)
		{
			if(mSelectDirDialog.ShowDialog()== DialogResult.OK)
			{
				String sPath = mSelectDirDialog.SelectedPath;
				
				sPath = HplSystem.MakePathRelativeToCurrent(sPath);
				
				sPath = sPath.Replace("\\","/");

				objResourceDirs.Items.Add(sPath);
			}
		}

		//----------------------------------------------------------

		private void objSaveResources_Click(object sender, System.EventArgs e)
		{
			SaveResourceDirs(msResourceDirFile);
		}

		//----------------------------------------------------------

		private void objMaterialBrowseButton_Click(object sender, System.EventArgs e)
		{
			mOpenFileDialog.Filter = "Material files (*.mat)|*.mat";
			
			if(mOpenFileDialog.ShowDialog()== DialogResult.OK)
			{
				//Reset the file dialog.
				mOpenFileDialog.InitialDirectory = Path.GetDirectoryName(mOpenFileDialog.FileName);
				mSaveFileDialog.InitialDirectory = mOpenFileDialog.InitialDirectory;
				mSaveFileDialog.FileName = mOpenFileDialog.FileName;
			
				txtMaterialFile.Text = mOpenFileDialog.FileName;
				mOpenFileDialog.FileName ="";

				LoadMaterial(txtMaterialFile.Text);
				
				//Set the correct physics material.
				bool bFound = false;
				for(int i=0; i < objPhysicsMaterials.Items.Count; i++)
				{
					if((String)objPhysicsMaterials.Items[i] == (String)mpMaterial.msPhysicsMaterial)
					{
						bFound = true;
						objPhysicsMaterials.SelectedIndex = i;
						break;
					}
				}
				if(bFound == false) objPhysicsMaterials.SelectedIndex =0;
				objUseAlpha.SelectedIndex = mpMaterial.mbUseAlpha? 0 : 1;
				objDepthTest.SelectedIndex = mpMaterial.mbDepthTest? 0 : 1;
				txtMatValue.Text = mpMaterial.msValue;
			}	
		}

		//----------------------------------------------------------

		private void AddMaterialTypes()
		{
			for(int i=0; i< HplMaterial.mvTypeNames.Length; i++)
			{
				objMaterialTypes.Items.Add(HplMaterial.mvTypeNames[i]);
			}
			objMaterialTypes.SelectedIndex = 0;

			txtMatValue.Text = "1";

			objUseAlpha.SelectedIndex = 1;
			objDepthTest.SelectedIndex = 0;

			////////////////////////////////////
			// Add the material types
			HplMaterialTypeData MatData=null;
			mvMaterialTypeData = new ArrayList();

			//Diffuse
            MatData = new HplMaterialTypeData();
			MatData.mvUnitTypes.Add(0);MatData.mvUnitTypes.Add(2);
			mvMaterialTypeData.Add(MatData);

			//Diffuse Specular
			MatData = new HplMaterialTypeData();
			MatData.mvUnitTypes.Add(0);MatData.mvUnitTypes.Add(2);
			mvMaterialTypeData.Add(MatData);

			//Bump
			MatData = new HplMaterialTypeData();
			MatData.mvUnitTypes.Add(0);MatData.mvUnitTypes.Add(1);MatData.mvUnitTypes.Add(2);
			mvMaterialTypeData.Add(MatData);
			
			//Bump Specular
			MatData = new HplMaterialTypeData();
			MatData.mvUnitTypes.Add(0);MatData.mvUnitTypes.Add(1);MatData.mvUnitTypes.Add(2);
			mvMaterialTypeData.Add(MatData);

			//Bump Color Specular
			MatData = new HplMaterialTypeData();
			MatData.mvUnitTypes.Add(0);MatData.mvUnitTypes.Add(1);MatData.mvUnitTypes.Add(2);
			MatData.mvUnitTypes.Add(3);
			mvMaterialTypeData.Add(MatData);

			//Additive
			MatData = new HplMaterialTypeData();
			MatData.mvUnitTypes.Add(0);
			MatData.mvUnitTypes.Add(5);
			mvMaterialTypeData.Add(MatData);

			//Flat
			MatData = new HplMaterialTypeData();
			MatData.mvUnitTypes.Add(0);
			mvMaterialTypeData.Add(MatData);

			//Modulative
			MatData = new HplMaterialTypeData();
			MatData.mvUnitTypes.Add(0);
			MatData.mvUnitTypes.Add(5);
			mvMaterialTypeData.Add(MatData);

			//ModulativeX2
			MatData = new HplMaterialTypeData();
			MatData.mvUnitTypes.Add(0);
			MatData.mvUnitTypes.Add(5);
			mvMaterialTypeData.Add(MatData);

			//Alpha
			MatData = new HplMaterialTypeData();
			MatData.mvUnitTypes.Add(0);
			MatData.mvUnitTypes.Add(5);
			mvMaterialTypeData.Add(MatData);

			//EnvironmentMapReflect
			MatData = new HplMaterialTypeData();
			MatData.mvUnitTypes.Add(0);
			MatData.mvUnitTypes.Add(4);
			mvMaterialTypeData.Add(MatData);

			//Water
			MatData = new HplMaterialTypeData();
			MatData.mvUnitTypes.Add(0);
			MatData.mvUnitTypes.Add(5);
			mvMaterialTypeData.Add(MatData);
			
			////////////////////////////////////
			// Add the texture unit types
			ArrayList vUnitTypeList = ((HplMaterialTypeData)mvMaterialTypeData[objMaterialTypes.SelectedIndex]).mvUnitTypes;
			for(int i=0; i< vUnitTypeList.Count; i++)
			{
				objTextureUnitTypes.Items.Add(HplMaterial.mvTextureUnitNames[(int)vUnitTypeList[i]]);
			}
			objTextureUnitTypes.SelectedIndex = 0;

			objTextureType.SelectedItem = "2D";
			
		}

		//----------------------------------------------------------

		private void objMaterialTypes_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(objMaterialTypes.SelectedIndex<0 || mvMaterialTypeData==null) return;
			
			objTextureUnitTypes.Items.Clear();
			
			ArrayList vUnitTypeList = ((HplMaterialTypeData)mvMaterialTypeData[objMaterialTypes.SelectedIndex]).mvUnitTypes;
			for(int i=0; i< vUnitTypeList.Count; i++)
			{
				objTextureUnitTypes.Items.Add(HplMaterial.mvTextureUnitNames[(int)vUnitTypeList[i]]);
			}
			objTextureUnitTypes.SelectedIndex = 0;
		}

		private void objUseAlpha_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(objUseAlpha.SelectedIndex<0 || mpMaterial==null)	return;

			mpMaterial.mbUseAlpha = objUseAlpha.SelectedIndex==0;
		}

		private void objDepthTest_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(objDepthTest.SelectedIndex<0 || mpMaterial==null)	return;

			mpMaterial.mbDepthTest = objDepthTest.SelectedIndex==0;
		}

		private void txtMatValue_TextChanged(object sender, System.EventArgs e)
		{
			if(mpMaterial==null)	return;

			mpMaterial.msValue = txtMatValue.Text;
		}

		private void objTextureUnitTypes_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			UpdateTextureUnitData();
		}

		private void objTexttureFileButton_Click(object sender, System.EventArgs e)
		{
			mOpenFileDialog.Filter = "All image files (*.*)|*.*";
			
			if(mOpenFileDialog.ShowDialog()== DialogResult.OK)
			{
				//Reset the file dialog.
				mOpenFileDialog.InitialDirectory = Path.GetDirectoryName(mOpenFileDialog.FileName);
			
				String sFileName = mOpenFileDialog.FileName;
				sFileName = Path.GetFileName(sFileName);
                
				int lIdx = objTextureUnitTypes.SelectedIndex;
				HplTextureUnit TexUnit = (HplTextureUnit)mpMaterial.mvTextureUnits[lIdx];
			
				TexUnit.msFile = sFileName;

                UpdateTextureUnitData();				
			}	
		}

		//----------------------------------------------------------

		private void objSaveMaterialButton_Click(object sender, System.EventArgs e)
		{
			if(txtMaterialFile.Text == "")
			{
				objMaterialSaveAsButton_Click(sender, e);
			}
			else
			{
				mpMaterial.Save(txtMaterialFile.Text,this);
			}
		}

		private void objMaterialSaveAsButton_Click(object sender, System.EventArgs e)
		{
			mSaveFileDialog.Filter = "Material files (*.mat)|*.mat";
			mSaveFileDialog.AddExtension = true;

			if(mSaveFileDialog.ShowDialog() == DialogResult.OK)
			{
				mpMaterial.Save(mSaveFileDialog.FileName,this);

				txtMaterialFile.Text = mSaveFileDialog.FileName;
				mOpenFileDialog.InitialDirectory = Path.GetDirectoryName(mSaveFileDialog.FileName);
				mOpenFileDialog.FileName ="";
			}
		}

		//----------------------------------------------------------

		private void objMipMaps_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			int lIdx = objTextureUnitTypes.SelectedIndex;
			HplTextureUnit TexUnit = (HplTextureUnit)mpMaterial.mvTextureUnits[lIdx];
			
			TexUnit.mbMipMaps = objMipMaps.SelectedIndex==0? true : false;
		}

		private void objWrapModes_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			int lIdx = objTextureUnitTypes.SelectedIndex;
			HplTextureUnit TexUnit = (HplTextureUnit)mpMaterial.mvTextureUnits[lIdx];
			
			TexUnit.mWrapMode = (eHplWrapType)objWrapModes.SelectedIndex;
		}

		private void objTextureType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			int lIdx = objTextureUnitTypes.SelectedIndex;
			HplTextureUnit TexUnit = (HplTextureUnit)mpMaterial.mvTextureUnits[lIdx];
			
			TexUnit.msType = (String)objTextureType.SelectedItem;
		}
		
		private void objTexAnimMode_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			int lIdx = objTextureUnitTypes.SelectedIndex;
			HplTextureUnit TexUnit = (HplTextureUnit)mpMaterial.mvTextureUnits[lIdx];
			TexUnit.msAnimMode = (String)objTexAnimMode.SelectedItem;
		}

		private void txtAnimFrameTime_TextChanged(object sender, System.EventArgs e)
		{
			int lIdx = objTextureUnitTypes.SelectedIndex;
			HplTextureUnit TexUnit = (HplTextureUnit)mpMaterial.mvTextureUnits[lIdx];
			TexUnit.msFrameTime = txtAnimFrameTime.Text;
		}
		
		//----------------------------------------------------------

		private void objSceneViewButton_Click(object sender, System.EventArgs e)
		{
			String sFile = Path.GetFileName(txtSceneFile.Text);
			HplSystem.RunProgam("SceneViewer.exe",sFile + " " + txtSceneStartPos.Text);
		}

		private void objSceneBrowseButton_Click(object sender, System.EventArgs e)
		{
			mOpenFileDialog.Filter = "Collada files (*.dae)|*.dae";
			
			if(mOpenFileDialog.ShowDialog()== DialogResult.OK)
			{
				//Reset the file dialog.
				mOpenFileDialog.InitialDirectory = Path.GetDirectoryName(mOpenFileDialog.FileName);
							
				txtSceneFile.Text = mOpenFileDialog.FileName;
				mOpenFileDialog.FileName ="";
			}	
		}

		private void objMaterialClearButton_Click(object sender, System.EventArgs e)
		{
			if(mpMaterial!=null)
			{
				mpMaterial.ClearData();
				objMaterialTypes.SelectedIndex = 0;
				objPhysicsMaterials.SelectedIndex =0;

				txtTextureUnitFile.Text = "";
				objTextureUnitTypes.SelectedIndex =0;
				objMipMaps.SelectedIndex =0;
				objWrapModes.SelectedIndex =0;
				objTextureType.SelectedItem = "2D";
				objTexAnimMode.SelectedIndex =0;
				txtAnimFrameTime.Text = "";
				objUseAlpha.SelectedIndex = 1;
				objDepthTest.SelectedIndex = 0;
				txtMatValue.Text = "1";
			}
		}

		private void objPhysicsMaterials_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			mpMaterial.msPhysicsMaterial = (String)objPhysicsMaterials.SelectedItem;
		}

		//----------------------------------------------------------

		private void objSoundFadeStart_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(mpSoundData!=null) 
			{
				mpSoundData.mbFadeStart = objSoundFadeStart.SelectedIndex==0?true:false;
			}
		}

		//----------------------------------------------------------

		private void objClearSoundDataButton_Click(object sender, System.EventArgs e)
		{
			if(mpSoundData!=null) mpSoundData.ClearData(this);
		}

		//----------------------------------------------------------

		private void objSoundOpen_Click(object sender, System.EventArgs e)
		{
			mOpenFileDialog.Filter = "Sound Entity files (*.snt)|*.snt";
			
			if(mOpenFileDialog.ShowDialog()== DialogResult.OK)
			{
				//Reset the file dialog.
				mOpenFileDialog.InitialDirectory = Path.GetDirectoryName(mOpenFileDialog.FileName);
				mSaveFileDialog.InitialDirectory = mOpenFileDialog.InitialDirectory;
				mSaveFileDialog.FileName = mOpenFileDialog.FileName;
			
				txtSoundFile.Text = mOpenFileDialog.FileName;
				mOpenFileDialog.FileName ="";

				mpSoundData.Load(txtSoundFile.Text,this);
			}	
		}

		private void objSoundSave_Click(object sender, System.EventArgs e)
		{
			if(txtSoundFile.Text == "")
			{
				objSoundSaveAs_Click(sender, e);
			}
			else
			{
				mpSoundData.Save(txtSoundFile.Text,this);
			}
		}

		private void objSoundSaveAs_Click(object sender, System.EventArgs e)
		{
			mSaveFileDialog.Filter = "Sound Entity files (*.snt)|*.snt";
			mSaveFileDialog.AddExtension = true;

			if(mSaveFileDialog.ShowDialog() == DialogResult.OK)
			{
				mpSoundData.Save(mSaveFileDialog.FileName,this);

				txtSoundFile.Text = mSaveFileDialog.FileName;
				mOpenFileDialog.InitialDirectory = Path.GetDirectoryName(mSaveFileDialog.FileName);
				mOpenFileDialog.FileName ="";
			}
		}

		private void txtMainSoundFile_TextChanged(object sender, System.EventArgs e)
		{
			if(mpSoundData!=null) 
			{
				mpSoundData.msMainSound = txtMainSoundFile.Text;
			}
		}

		private void txtStartSoundFile_TextChanged(object sender, System.EventArgs e)
		{
			if(mpSoundData!=null) 
			{
				mpSoundData.msStartSound = txtStartSoundFile.Text;
			}
		}

		private void txtStopSoundFile_TextChanged(object sender, System.EventArgs e)
		{
			if(mpSoundData!=null) 
			{
				mpSoundData.msStopSound = txtStopSoundFile.Text;
			}
		}

		private void objSoundFadeEnd_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(mpSoundData!=null) 
			{
				mpSoundData.mbFadeEnd= objSoundFadeEnd.SelectedIndex==0?true:false;
			}
		}

		private void objSoundLoop_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(mpSoundData!=null) 
			{
				mpSoundData.mbLoop = objSoundLoop.SelectedIndex==0?true:false;
			}
		}

		private void objSoundUse3D_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(mpSoundData!=null) 
			{
				mpSoundData.mbUse3D = objSoundUse3D.SelectedIndex==0?true:false;
			}
		}

		private void objSoundStream_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(mpSoundData!=null) 
			{
				mpSoundData.mbStream = objSoundStream.SelectedIndex==0?true:false;
			}
		}

		private void objSoundBlockable_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(mpSoundData!=null) 
			{
				mpSoundData.mbBlockable = objSoundBlockable.SelectedIndex==0?true:false;
			}
		}

		private void txtSoundVolume_TextChanged(object sender, System.EventArgs e)
		{
			if(mpSoundData!=null) 
			{
				try{
					mpSoundData.mfVolume = (float)Convert.ToDouble(txtSoundVolume.Text);
				}
				catch{
					mpSoundData.mfVolume = 1;
				}
				
			}
		}

		private void txtSoundMinDist_TextChanged(object sender, System.EventArgs e)
		{
			if(mpSoundData!=null) 
			{
				try	{
					mpSoundData.mfMinDistance = (float)Convert.ToDouble(txtSoundMinDist.Text);
				}
				catch{
					mpSoundData.mfMinDistance = 1;
				}

			}
		}

		private void txtSoundMaxDist_TextChanged(object sender, System.EventArgs e)
		{
			if(mpSoundData!=null) 
			{
				try{
					mpSoundData.mfMaxDistance = (float)Convert.ToDouble(txtSoundMaxDist.Text);
				}
				catch{
					mpSoundData.mfMaxDistance = 10;
				}
				
			}
		}

		private void txtSoundRandom_TextChanged(object sender, System.EventArgs e)
		{
			if(mpSoundData!=null) 
			{
				try{
					mpSoundData.mfRandom = (float)Convert.ToDouble(txtSoundRandom.Text);
				}
				catch{
					mpSoundData.mfRandom = 1;
				}
				
			}
		}

		private void txtSoundInterval_TextChanged(object sender, System.EventArgs e)
		{
			if(mpSoundData!=null) 
			{
				try{
					mpSoundData.mfInterval = (float)Convert.ToDouble(txtSoundInterval.Text);
				}
				catch{
					mpSoundData.mfInterval = 0;
				}
				
			}
		}

		private void txtSoundBlockVolMul_TextChanged(object sender, System.EventArgs e)
		{
			if(mpSoundData!=null) 
			{
				try
				{
					mpSoundData.mfBlockVolMul = (float)Convert.ToDouble(txtSoundBlockVolMul.Text);
				}
				catch
				{
					mpSoundData.mfBlockVolMul = 0.6f;
				}
				
			}
		}

		private void txtSoundPriority_TextChanged(object sender, System.EventArgs e)
		{
			if(mpSoundData!=null) 
			{
				try
				{
					mpSoundData.mlPriority = Convert.ToInt32(txtSoundPriority.Text);
				}
				catch
				{
					mpSoundData.mlPriority = 0;
				}
				
			}
		}

		private String LoadSoundFromFile()
		{
			mOpenFileDialog.Filter = "All sound files (*.ogg;*.wav;*.mp3)|*.ogg;*.wav;*.mp3";
			
			if(mOpenFileDialog.ShowDialog()== DialogResult.OK)
			{
				//Reset the file dialog.
				mOpenFileDialog.InitialDirectory = Path.GetDirectoryName(mOpenFileDialog.FileName);
			
				String sFileName = mOpenFileDialog.FileName;
				sFileName = Path.GetFileName(sFileName);
                				
				return sFileName;
			}	
			else
			{
				return "";
			}
		}

		private void objMainSoundButton_Click(object sender, System.EventArgs e)
		{
			String sFile = LoadSoundFromFile();
			if(sFile!="") txtMainSoundFile.Text = sFile;
		}

		private void objStartSoundButton_Click(object sender, System.EventArgs e)
		{
			String sFile = LoadSoundFromFile();
			if(sFile!="") txtStartSoundFile.Text = sFile;
		}

		private void objStopSoundButton_Click(object sender, System.EventArgs e)
		{
			String sFile = LoadSoundFromFile();
			if(sFile!="") txtStopSoundFile.Text = sFile;
		}

		private void txtTextureUnitFile_TextChanged(object sender, System.EventArgs e)
		{
			if(mpMaterial!=null && objTextureUnitTypes.SelectedIndex>=0)
			{
				int lIdx = objTextureUnitTypes.SelectedIndex;
				HplTextureUnit TexUnit = (HplTextureUnit)mpMaterial.mvTextureUnits[lIdx];
			
				TexUnit.msFile = txtTextureUnitFile.Text;
			}
		}

		//----------------------------------------------------------

		private void objTransOpen_Click(object sender, System.EventArgs e)
		{
			mOpenFileDialog.Filter = "Translation files (*.lang)|*.lang";
			
			if(mOpenFileDialog.ShowDialog()== DialogResult.OK)
			{
				//Reset the file dialog.
				mOpenFileDialog.InitialDirectory = Path.GetDirectoryName(mOpenFileDialog.FileName);
				mSaveFileDialog.InitialDirectory = mOpenFileDialog.InitialDirectory;
				mSaveFileDialog.FileName = mOpenFileDialog.FileName;
			
				txtTransFile.Text = mOpenFileDialog.FileName;
				mOpenFileDialog.FileName ="";

				mpTransData.Load(txtTransFile.Text);
			}	
		}

		private void objTransSave_Click(object sender, System.EventArgs e)
		{
			if(txtTransFile.Text == "")
			{
				objTransSaveAs_Click(sender, e);
			}
			else
			{
				mpTransData.Save(txtTransFile.Text);
			}
		}

		private void objTransSaveAs_Click(object sender, System.EventArgs e)
		{
			mSaveFileDialog.Filter = "Translation files (*.lang)|*.lang";
			mSaveFileDialog.AddExtension = true;

			if(mSaveFileDialog.ShowDialog() == DialogResult.OK)
			{
				mpTransData.Save(mSaveFileDialog.FileName);

				txtTransFile.Text = mSaveFileDialog.FileName;
				mOpenFileDialog.InitialDirectory = Path.GetDirectoryName(mSaveFileDialog.FileName);
				mOpenFileDialog.FileName ="";
			}
		}
		
		private void objTransCatButton_Click(object sender, System.EventArgs e)
		{
			mpEnterNameForm.txtInput.Text = "";
			mpEnterNameForm.ShowDialog();
			
			if(mpEnterNameForm.mbOKPressed && mpEnterNameForm.txtInput.Text!="")
			{
				mpTransData.AddCategory(mpEnterNameForm.txtInput.Text);
			}
		}

		private void objTransCatRemove_Click(object sender, System.EventArgs e)
		{
			if(objTransCategories.SelectedIndex<0) return;
			
			DialogResult Res = MessageBox.Show("Removing the category will remove all it's entries. Continue?",
										"Warning!",MessageBoxButtons.OKCancel);
			if(Res == DialogResult.OK)
			{
				mpTransData.RemoveCategory((String)objTransCategories.SelectedItem);
			}
		}

		private void objTransCatRename_Click(object sender, System.EventArgs e)
		{
			mpEnterNameForm.txtInput.Text = (String)objTransCategories.SelectedItem;
			mpEnterNameForm.ShowDialog();
			
			if(mpEnterNameForm.mbOKPressed)
			{
				mpTransData.RenameCategory((String)objTransCategories.SelectedItem,
											mpEnterNameForm.txtInput.Text);
			}
		}
		
		//----------------------------------------------------------

		private void objTransEntryName_TextChanged(object sender, System.EventArgs e)
		{
			
		}

		private void objTransCategories_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			mpTransData.SetCurrentCat();
		}

		private void objTransEntryButton_Click(object sender, System.EventArgs e)
		{
			mpEnterNameForm.txtInput.Text = "";
			mpEnterNameForm.ShowDialog();
			
			if(mpEnterNameForm.mbOKPressed && mpEnterNameForm.txtInput.Text!="")
			{
				mpTransData.AddEntry(mpEnterNameForm.txtInput.Text);
			}
		}

		private void objTransEntries_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			mpTransData.SetCurrentEntry();
		}

		private void objTransEntryText_TextChanged(object sender, System.EventArgs e)
		{
			if(mpTransData.mpCurrentEntry!=null)
			{
				mpTransData.mpCurrentEntry.msText = objTransEntryText.Text;
			}
		}

		private void objTransEntryChangeName_Click(object sender, System.EventArgs e)
		{
			if(mpTransData.mpCurrentEntry!=null)
			{
				mpTransData.mpCurrentEntry.msName = objTransEntryName.Text;
			}

			mpTransData.UpdateEntryList();
		}

		private void objTransEntryRemove_Click(object sender, System.EventArgs e)
		{
			if(objTransEntries.SelectedIndex<0) return;
			
			DialogResult Res = MessageBox.Show("Sure you want to remove this entry?",
				"Warning!",MessageBoxButtons.OKCancel);
			if(Res == DialogResult.OK)
			{
				mpTransData.RemoveEntry((String)objTransEntries.SelectedItem);
			}
		}
		
		//----------------------------------------------------------
	}
}
