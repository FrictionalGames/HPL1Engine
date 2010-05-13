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
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace Mapeditor
{
	public enum eEditMode
	{
		None, 
		Tiles,
		Lights,
		Props,
		Items,
		TileBrush,
		PlayerStart,
		Area,
		SoundSource,
		Particles,
	};
	
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class frmMain : System.Windows.Forms.Form
	{
		
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuFile;
		private System.Windows.Forms.MenuItem menuFileNew;
		private System.Windows.Forms.MenuItem menuFileOpen;
		private System.Windows.Forms.MenuItem menuFileSave;
		private System.Windows.Forms.MenuItem menuFileSaveAs;
		private System.Windows.Forms.MenuItem menuEdit;
		private System.Windows.Forms.MenuItem menuView;
		private System.Windows.Forms.GroupBox groupEditMode;
		private System.Windows.Forms.Button objModeTilesButton;
		private System.Windows.Forms.Button objModeLightsButton;
		private System.Windows.Forms.Button objModeSoundsSourceButon;
		private System.Windows.Forms.Button objModeTileBrushButton;
		private System.Windows.Forms.Button objModeAreaMode;
		private System.Windows.Forms.Button objModePropsButton;

		public frmTileSets mTilesetsForm;
		public frmLayers mLayersForm;
		public frmLight mLightForm;
		public frmSound mSoundForm;
		public frmParticles mParticlesForm;
		public frmAreas mAreasForm;
		public frmProps mPropsForm;
		private frmNewMap mNewMapForm;
		private cMapIO mMapIO=null;
		public cTile mSelectedTile=null;
		public ContextMenu mPopupMenu=null;
		private OpenFileDialog mOpenFileDialog=null;
		private SaveFileDialog mSaveFileDialog=null;
		private frmMiniMap mMiniMapForm=null;

		private eEditMode mEditMode = eEditMode.None;

		public ArrayList mlstCurrentEntityList=null;
		public ArrayList mlstEnityLists=null;
		public int mlCurrentEntity=-1;

		/*Selections vars */
		public bool mbSelectIsOn = false;
		private Font mSelectFontOn=null;
		private Font mSelectFontOff=null;
		private Color mSelectColorOff;
		public ArrayList mlstSelectedEntities=null;		
		private Rectangle mSelectRect;
		private bool mbSelectRectOn = false;

		/*Copy vars */
		public ArrayList mlstCopiedEntities=null;	
		public int mlCopyX;
		public int mlCopyY;
		
	
		/*Map vars */
		public int mlMapWidth=0;
		public int mlMapHeight=0;
		public string msMapName="";
		public int mlDefaultTileSize=0;
		public string msMapFile="";
		public float mfLightZ = 10;
		public Color mAmbientColor = Color.Black;

		private bool mbMouseIsDown=false;
		private int mlMouseX;
		private int mlMouseY;

		
		/*private bool mbCtrlPressed=false;
		private bool mbAltPressed=false;
		private bool mbShiftPressed=false;*/

		private ArrayList mlstActiveForms;
		private System.Windows.Forms.MenuItem menuFileLine01;
		private System.Windows.Forms.MenuItem menuFileExit;
		public System.Windows.Forms.VScrollBar objVertMapScroll;
		public System.Windows.Forms.HScrollBar objHoriMapScroll;
		private System.Windows.Forms.Button objModeSelectButton;
		private System.Windows.Forms.MenuItem menuViewMiniMap;
		private System.Windows.Forms.MenuItem menuEditMapProp;
		public System.Windows.Forms.CheckBox objTileGridCheckBox;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button objModeParticlesButton;
		public Mapeditor.cTilePanel objMapPanel;

		class cEntityComapre : IComparer
		{
			public int Compare(object x, object y)
			{
				cEntity EntityA = (cEntity)x;
				cEntity EntityB = (cEntity)y;

				if(EntityA.mfZ < EntityB.mfZ)return -1;
				if(EntityA.mfZ > EntityB.mfZ)return 1;
				return 0;
			}
		}
			

		
		public frmMain()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			mTilesetsForm = new frmTileSets(this);
			mLayersForm = new frmLayers(this);
			mPropsForm = new frmProps(this);
			mSoundForm = new frmSound(this);
			mParticlesForm = new frmParticles(this);
			mAreasForm = new frmAreas(this);

			mNewMapForm = new frmNewMap();
			mLightForm = new frmLight(this);
			mMiniMapForm = new frmMiniMap(this);

			mlstEnityLists = new ArrayList();
			mlstEnityLists.Add(mPropsForm.mlstProps);
			mlstEnityLists.Add(mLightForm.mlstLights);
			mlstEnityLists.Add(mSoundForm.mlstSounds);
			mlstEnityLists.Add(mParticlesForm.mlstParticles);
			mlstEnityLists.Add(mAreasForm.mlstAreas);
			
			mlstActiveForms = new ArrayList();

			mPopupMenu = new ContextMenu();
			objMapPanel.ContextMenu = mPopupMenu;

			mMapIO = new cMapIO(this);

			mOpenFileDialog = new OpenFileDialog();
			mOpenFileDialog.Filter = "HPL Map files (*.hpl)|*.hpl";
			mOpenFileDialog.InitialDirectory = (string)Directory.GetCurrentDirectory().Clone();
			
			mSaveFileDialog = new SaveFileDialog();
			mSaveFileDialog.Filter = "HPL Map files (*.hpl)|*.hpl";
			mSaveFileDialog.InitialDirectory = (string)Directory.GetCurrentDirectory().Clone();
			
			mSelectFontOn = new Font("Microsoft Sans Serif",8,FontStyle.Bold);
			mSelectFontOff = objModeSelectButton.Font;
			mSelectColorOff = objModeSelectButton.BackColor;
			mlstSelectedEntities = new ArrayList();
			mSelectRect = new Rectangle(0,0,0,0);

			mlstCopiedEntities = new ArrayList();
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
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuFile = new System.Windows.Forms.MenuItem();
			this.menuFileNew = new System.Windows.Forms.MenuItem();
			this.menuFileOpen = new System.Windows.Forms.MenuItem();
			this.menuFileSave = new System.Windows.Forms.MenuItem();
			this.menuFileSaveAs = new System.Windows.Forms.MenuItem();
			this.menuFileLine01 = new System.Windows.Forms.MenuItem();
			this.menuFileExit = new System.Windows.Forms.MenuItem();
			this.menuEdit = new System.Windows.Forms.MenuItem();
			this.menuEditMapProp = new System.Windows.Forms.MenuItem();
			this.menuView = new System.Windows.Forms.MenuItem();
			this.menuViewMiniMap = new System.Windows.Forms.MenuItem();
			this.groupEditMode = new System.Windows.Forms.GroupBox();
			this.objModeSelectButton = new System.Windows.Forms.Button();
			this.objModeParticlesButton = new System.Windows.Forms.Button();
			this.objModeAreaMode = new System.Windows.Forms.Button();
			this.objModeTileBrushButton = new System.Windows.Forms.Button();
			this.objModeSoundsSourceButon = new System.Windows.Forms.Button();
			this.objModePropsButton = new System.Windows.Forms.Button();
			this.objModeLightsButton = new System.Windows.Forms.Button();
			this.objModeTilesButton = new System.Windows.Forms.Button();
			this.objVertMapScroll = new System.Windows.Forms.VScrollBar();
			this.objHoriMapScroll = new System.Windows.Forms.HScrollBar();
			this.objMapPanel = new Mapeditor.cTilePanel();
			this.objTileGridCheckBox = new System.Windows.Forms.CheckBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupEditMode.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuFile,
																					  this.menuEdit,
																					  this.menuView});
			// 
			// menuFile
			// 
			this.menuFile.Index = 0;
			this.menuFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					 this.menuFileNew,
																					 this.menuFileOpen,
																					 this.menuFileSave,
																					 this.menuFileSaveAs,
																					 this.menuFileLine01,
																					 this.menuFileExit});
			this.menuFile.Text = "File";
			// 
			// menuFileNew
			// 
			this.menuFileNew.Index = 0;
			this.menuFileNew.Text = "New..";
			this.menuFileNew.Click += new System.EventHandler(this.menuFileNew_Click);
			// 
			// menuFileOpen
			// 
			this.menuFileOpen.Index = 1;
			this.menuFileOpen.Text = "Open...";
			this.menuFileOpen.Click += new System.EventHandler(this.menuFileOpen_Click);
			// 
			// menuFileSave
			// 
			this.menuFileSave.Enabled = false;
			this.menuFileSave.Index = 2;
			this.menuFileSave.Text = "Save";
			this.menuFileSave.Click += new System.EventHandler(this.menuFileSave_Click);
			// 
			// menuFileSaveAs
			// 
			this.menuFileSaveAs.Enabled = false;
			this.menuFileSaveAs.Index = 3;
			this.menuFileSaveAs.Text = "Save as...";
			this.menuFileSaveAs.Click += new System.EventHandler(this.menuFileSaveAs_Click);
			// 
			// menuFileLine01
			// 
			this.menuFileLine01.Index = 4;
			this.menuFileLine01.Text = "-";
			// 
			// menuFileExit
			// 
			this.menuFileExit.Index = 5;
			this.menuFileExit.Text = "Exit";
			this.menuFileExit.Click += new System.EventHandler(this.menuFileExit_Click);
			// 
			// menuEdit
			// 
			this.menuEdit.Index = 1;
			this.menuEdit.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					 this.menuEditMapProp});
			this.menuEdit.Text = "Edit";
			// 
			// menuEditMapProp
			// 
			this.menuEditMapProp.Enabled = false;
			this.menuEditMapProp.Index = 0;
			this.menuEditMapProp.Text = "Map Properties";
			this.menuEditMapProp.Click += new System.EventHandler(this.menuEditMapProp_Click);
			// 
			// menuView
			// 
			this.menuView.Index = 2;
			this.menuView.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					 this.menuViewMiniMap});
			this.menuView.Text = "View";
			// 
			// menuViewMiniMap
			// 
			this.menuViewMiniMap.Index = 0;
			this.menuViewMiniMap.Text = "Mini map";
			this.menuViewMiniMap.Click += new System.EventHandler(this.menuViewMiniMap_Click);
			// 
			// groupEditMode
			// 
			this.groupEditMode.Controls.Add(this.objModeSelectButton);
			this.groupEditMode.Controls.Add(this.objModeParticlesButton);
			this.groupEditMode.Controls.Add(this.objModeAreaMode);
			this.groupEditMode.Controls.Add(this.objModeTileBrushButton);
			this.groupEditMode.Controls.Add(this.objModeSoundsSourceButon);
			this.groupEditMode.Controls.Add(this.objModePropsButton);
			this.groupEditMode.Controls.Add(this.objModeLightsButton);
			this.groupEditMode.Controls.Add(this.objModeTilesButton);
			this.groupEditMode.Location = new System.Drawing.Point(8, 8);
			this.groupEditMode.Name = "groupEditMode";
			this.groupEditMode.Size = new System.Drawing.Size(136, 176);
			this.groupEditMode.TabIndex = 1;
			this.groupEditMode.TabStop = false;
			this.groupEditMode.Text = "Edit Mode";
			// 
			// objModeSelectButton
			// 
			this.objModeSelectButton.Enabled = false;
			this.objModeSelectButton.ForeColor = System.Drawing.Color.Black;
			this.objModeSelectButton.Location = new System.Drawing.Point(8, 16);
			this.objModeSelectButton.Name = "objModeSelectButton";
			this.objModeSelectButton.Size = new System.Drawing.Size(56, 32);
			this.objModeSelectButton.TabIndex = 9;
			this.objModeSelectButton.Text = "Select";
			this.objModeSelectButton.Click += new System.EventHandler(this.objModeSelectButton_Click);
			// 
			// objModeParticlesButton
			// 
			this.objModeParticlesButton.Enabled = false;
			this.objModeParticlesButton.Location = new System.Drawing.Point(8, 136);
			this.objModeParticlesButton.Name = "objModeParticlesButton";
			this.objModeParticlesButton.Size = new System.Drawing.Size(56, 32);
			this.objModeParticlesButton.TabIndex = 8;
			this.objModeParticlesButton.Text = "Particle System";
			this.objModeParticlesButton.Click += new System.EventHandler(this.objModeParticlesButton_Click);
			// 
			// objModeAreaMode
			// 
			this.objModeAreaMode.Enabled = false;
			this.objModeAreaMode.Location = new System.Drawing.Point(72, 96);
			this.objModeAreaMode.Name = "objModeAreaMode";
			this.objModeAreaMode.Size = new System.Drawing.Size(56, 32);
			this.objModeAreaMode.TabIndex = 7;
			this.objModeAreaMode.Text = "Area";
			this.objModeAreaMode.Click += new System.EventHandler(this.objModeAreaMode_Click);
			// 
			// objModeTileBrushButton
			// 
			this.objModeTileBrushButton.Enabled = false;
			this.objModeTileBrushButton.Location = new System.Drawing.Point(72, 136);
			this.objModeTileBrushButton.Name = "objModeTileBrushButton";
			this.objModeTileBrushButton.Size = new System.Drawing.Size(56, 32);
			this.objModeTileBrushButton.TabIndex = 5;
			this.objModeTileBrushButton.Text = "Tile Brush";
			// 
			// objModeSoundsSourceButon
			// 
			this.objModeSoundsSourceButon.Enabled = false;
			this.objModeSoundsSourceButon.Location = new System.Drawing.Point(8, 96);
			this.objModeSoundsSourceButon.Name = "objModeSoundsSourceButon";
			this.objModeSoundsSourceButon.Size = new System.Drawing.Size(56, 32);
			this.objModeSoundsSourceButon.TabIndex = 4;
			this.objModeSoundsSourceButon.Text = "Sounds Source";
			this.objModeSoundsSourceButon.Click += new System.EventHandler(this.objModeSoundsSourceButon_Click);
			// 
			// objModePropsButton
			// 
			this.objModePropsButton.Enabled = false;
			this.objModePropsButton.Location = new System.Drawing.Point(72, 56);
			this.objModePropsButton.Name = "objModePropsButton";
			this.objModePropsButton.Size = new System.Drawing.Size(56, 32);
			this.objModePropsButton.TabIndex = 2;
			this.objModePropsButton.Text = "Entities";
			this.objModePropsButton.Click += new System.EventHandler(this.objModePropsButton_Click);
			// 
			// objModeLightsButton
			// 
			this.objModeLightsButton.Enabled = false;
			this.objModeLightsButton.Location = new System.Drawing.Point(72, 16);
			this.objModeLightsButton.Name = "objModeLightsButton";
			this.objModeLightsButton.Size = new System.Drawing.Size(56, 32);
			this.objModeLightsButton.TabIndex = 1;
			this.objModeLightsButton.Text = "Lights";
			this.objModeLightsButton.Click += new System.EventHandler(this.objModeLightsButton_Click);
			// 
			// objModeTilesButton
			// 
			this.objModeTilesButton.Enabled = false;
			this.objModeTilesButton.Location = new System.Drawing.Point(8, 56);
			this.objModeTilesButton.Name = "objModeTilesButton";
			this.objModeTilesButton.Size = new System.Drawing.Size(56, 32);
			this.objModeTilesButton.TabIndex = 0;
			this.objModeTilesButton.Text = "Tiles";
			this.objModeTilesButton.Click += new System.EventHandler(this.objModeTilesButton_Click);
			// 
			// objVertMapScroll
			// 
			this.objVertMapScroll.Enabled = false;
			this.objVertMapScroll.LargeChange = 1;
			this.objVertMapScroll.Location = new System.Drawing.Point(864, 8);
			this.objVertMapScroll.Maximum = 0;
			this.objVertMapScroll.Name = "objVertMapScroll";
			this.objVertMapScroll.Size = new System.Drawing.Size(16, 560);
			this.objVertMapScroll.TabIndex = 2;
			this.objVertMapScroll.ValueChanged += new System.EventHandler(this.objVertMapScroll_ValueChanged);
			// 
			// objHoriMapScroll
			// 
			this.objHoriMapScroll.Enabled = false;
			this.objHoriMapScroll.LargeChange = 1;
			this.objHoriMapScroll.Location = new System.Drawing.Point(152, 576);
			this.objHoriMapScroll.Maximum = 0;
			this.objHoriMapScroll.Name = "objHoriMapScroll";
			this.objHoriMapScroll.Size = new System.Drawing.Size(704, 16);
			this.objHoriMapScroll.TabIndex = 3;
			this.objHoriMapScroll.ValueChanged += new System.EventHandler(this.objHoriMapScroll_ValueChanged);
			// 
			// objMapPanel
			// 
			this.objMapPanel.BackColor = System.Drawing.SystemColors.ControlText;
			this.objMapPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.objMapPanel.Enabled = false;
			this.objMapPanel.Location = new System.Drawing.Point(152, 8);
			this.objMapPanel.Name = "objMapPanel";
			this.objMapPanel.Size = new System.Drawing.Size(704, 560);
			this.objMapPanel.TabIndex = 0;
			this.objMapPanel.Visible = false;
			this.objMapPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.objMapPanel_MouseUp);
			this.objMapPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.objMapPanel_Paint);
			this.objMapPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.objMapPanel_MouseMove);
			this.objMapPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.objMapPanel_MouseDown);
			// 
			// objTileGridCheckBox
			// 
			this.objTileGridCheckBox.Checked = true;
			this.objTileGridCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.objTileGridCheckBox.Location = new System.Drawing.Point(16, 16);
			this.objTileGridCheckBox.Name = "objTileGridCheckBox";
			this.objTileGridCheckBox.Size = new System.Drawing.Size(112, 32);
			this.objTileGridCheckBox.TabIndex = 4;
			this.objTileGridCheckBox.Text = "Show Current Layer Grid";
			this.objTileGridCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.objTileGridCheckBox.CheckedChanged += new System.EventHandler(this.objTileGridCheckBox_CheckedChanged);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.objTileGridCheckBox);
			this.groupBox1.Location = new System.Drawing.Point(8, 248);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(136, 184);
			this.groupBox1.TabIndex = 5;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Options";
			// 
			// frmMain
			// 
			this.AutoScale = false;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(890, 603);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.objHoriMapScroll);
			this.Controls.Add(this.objVertMapScroll);
			this.Controls.Add(this.groupEditMode);
			this.Controls.Add(this.objMapPanel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Menu = this.mainMenu1;
			this.Name = "frmMain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "HPL MapEditor";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmMain_KeyDown);
			this.Resize += new System.EventHandler(this.frmMain_Resize);
			this.groupEditMode.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
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
		
		private Bitmap mBackBuffer=null;
		
		private void objMapPanel_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			if(mBackBuffer==null)
			{
				mBackBuffer = new Bitmap(objMapPanel.Width, objMapPanel.Height);
			}

			Graphics Gfx = Graphics.FromImage(mBackBuffer);
			
			//Clear bitmap
			Gfx.Clear(Color.Black);

			//Draw the layers
			mLayersForm.DrawAllLayers(Gfx,objHoriMapScroll.Value,objVertMapScroll.Value,
										objMapPanel.Width,objMapPanel.Height);

			/*Gfx.DrawString("StartPos: "+objHoriMapScroll.Value+" : "+objVertMapScroll.Value,new Font("Arial", 8),
						new SolidBrush(Color.White),0,0);
			
			Gfx.DrawString("EndPos: "+(objHoriMapScroll.Value+objMapPanel.Width)+" : "
							+(objVertMapScroll.Value+objMapPanel.Height),new Font("Arial", 8),
				new SolidBrush(Color.White),0,12);*/

			//Draw mode specific stuff:
			if(mEditMode == eEditMode.Tiles)
			{
				/*int lTileX = (mlMouseX/mlDefaultTileSize)*mlDefaultTileSize;
				int lTileY = (mlMouseY/mlDefaultTileSize)*mlDefaultTileSize;
				Gfx.DrawRectangle(new Pen(Color.Magenta),lTileX,lTileY, 
					mlDefaultTileSize-1,mlDefaultTileSize-1);*/
			}
			
			//Draw all entities...
			//Maybe they should be put in list that is sorted??
			Rectangle ScreenRect = new Rectangle(objHoriMapScroll.Value,objVertMapScroll.Value,
								objMapPanel.Width,objMapPanel.Height);

			ArrayList mlstDrawEntities = new ArrayList();

			foreach(ArrayList EntityList in mlstEnityLists)
			{
				foreach(cEntity EData in EntityList)
				{
					if(cMath.BoxCollision(ScreenRect,EData.GetDrawRect()))
					{
						
						mlstDrawEntities.Add(EData);
					}
				}	
			}
						
			mlstDrawEntities.Sort(new cEntityComapre());

			foreach(cEntity EData in mlstDrawEntities)
			{
				bool bSelected = false;
				foreach(cEntity ESelectData in mlstSelectedEntities)
				{
					if(ESelectData == EData)
					{
						bSelected = true;
						break;
					}
				}

				EData.Draw(Gfx,objHoriMapScroll.Value,objVertMapScroll.Value,mEditMode,bSelected);
			}
			
			
			
			/*Paint the Select rectangle*/
			if(mbSelectRectOn){
				Pen RectPen = new Pen(Color.White);
				
				int lX = mSelectRect.X; int lY = mSelectRect.Y;
				int lW = mSelectRect.Width; int lH = mSelectRect.Height;
				
				if(lW<0) 
				{
					lX += mSelectRect.Width;
					lW = -mSelectRect.Width;
				}
				if(lH<0) {
					lY += mSelectRect.Height;
					lH = -mSelectRect.Height;
				}

				Gfx.DrawRectangle(RectPen,lX - objHoriMapScroll.Value,lY-objVertMapScroll.Value,lW,lH);
								
				RectPen.Dispose();
			}
		
            e.Graphics.DrawImageUnscaled(mBackBuffer,0,0);
		}



		
		
		private void objModeTilesButton_Click(object sender, System.EventArgs e)
		{
			if(mEditMode != eEditMode.Tiles)
			{
				HideAllActiveForms();
				mlstActiveForms.Clear();
	
				mTilesetsForm.Show();
				mlstActiveForms.Add(mTilesetsForm);
				mLayersForm.Show();
				mlstActiveForms.Add(mLayersForm);
				
				mEditMode = eEditMode.Tiles;
				mlstCurrentEntityList = null;
				mlCurrentEntity=-1;
				
				mlstCopiedEntities.Clear();
				objMapPanel.Refresh();
			}
		}

		private void objModeLightsButton_Click(object sender, System.EventArgs e)
		{
			if(mEditMode != eEditMode.Lights)
			{
				HideAllActiveForms();
				mlstActiveForms.Clear();
	
				mLightForm.Show();
				mlstActiveForms.Add(mLightForm);
				
				mEditMode = eEditMode.Lights;
				mlstCurrentEntityList = mLightForm.mlstLights;
				mlCurrentEntity=-1;
				
				mlstCopiedEntities.Clear();
				objMapPanel.Refresh();
			}
		}

		private void objModeSoundsSourceButon_Click(object sender, System.EventArgs e)
		{
			if(mEditMode != eEditMode.SoundSource)
			{
				HideAllActiveForms();
				mlstActiveForms.Clear();
	
				mSoundForm.Show();
				mlstActiveForms.Add(mSoundForm);
				
				mEditMode = eEditMode.SoundSource;
				mlstCurrentEntityList = mSoundForm.mlstSounds;
				mlCurrentEntity=-1;
				
				mlstCopiedEntities.Clear();
				objMapPanel.Refresh();
			}
		}


		private void objModePropsButton_Click(object sender, System.EventArgs e)
		{
			if(mEditMode != eEditMode.Props)
			{
				HideAllActiveForms();
				mlstActiveForms.Clear();
	
				mPropsForm.Show();
				mlstActiveForms.Add(mPropsForm);
				mLayersForm.Show();
				mlstActiveForms.Add(mLayersForm);
				
				mEditMode = eEditMode.Props;
				mlstCurrentEntityList = mPropsForm.mlstProps;
				mlCurrentEntity=-1;
				
				mlstCopiedEntities.Clear();
				objMapPanel.Refresh();
			}
		}

		private void objModeParticlesButton_Click(object sender, System.EventArgs e)
		{
			if(mEditMode != eEditMode.Particles)
			{
				HideAllActiveForms();
				mlstActiveForms.Clear();
	
				mParticlesForm.Show();
				mlstActiveForms.Add(mParticlesForm);
				mLayersForm.Show();
				mlstActiveForms.Add(mLayersForm);
				
				
				mEditMode = eEditMode.Particles;
				mlstCurrentEntityList = mParticlesForm.mlstParticles;
				mlCurrentEntity=-1;
				
				mlstCopiedEntities.Clear();
				objMapPanel.Refresh();
			}
		}

		private void objModeAreaMode_Click(object sender, System.EventArgs e)
		{
			if(mEditMode != eEditMode.Area)
			{
				HideAllActiveForms();
				mlstActiveForms.Clear();
	
				mAreasForm.Show();
				mlstActiveForms.Add(mAreasForm);
								
				mEditMode = eEditMode.Area;
				mlstCurrentEntityList = mAreasForm.mlstAreas;
				mlCurrentEntity=-1;
				
				mlstCopiedEntities.Clear();
				objMapPanel.Refresh();
			}
		}
	
		private void HideAllActiveForms()
		{
			foreach(Form TempForm in mlstActiveForms)
			{
				TempForm.Hide();
			}
		}

		private void ShowAllActiveForms()
		{
			foreach(Form TempForm in mlstActiveForms)
			{
				TempForm.Show();
			}
		}

		private void menuFileExit_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void frmMain_Resize(object sender, System.EventArgs e)
		{
			if(this.WindowState == FormWindowState.Minimized)
			{
				HideAllActiveForms();
				if(menuViewMiniMap.Checked)mMiniMapForm.Hide();
			}
			else
			{
				ShowAllActiveForms();
				if(menuViewMiniMap.Checked)mMiniMapForm.Show();
			}
		}

		private void menuFileNew_Click(object sender, System.EventArgs e)
		{
			mNewMapForm.ShowDialog();

			
			if(mNewMapForm.mbOKPressed)
			{
				menuFileSave.Enabled = false;
				mSaveFileDialog.FileName = "";

				ResetAllData();
				EnableAllButtons();
								
				mlMapWidth = mNewMapForm.mlWidth;
				mlMapHeight = mNewMapForm.mlHeight;
				mlDefaultTileSize = mNewMapForm.mlTileSize;
				
				msMapName = mNewMapForm.msName;
				
				if(mNewMapForm.mbCreateLayers)
				{
					mLayersForm.AddLayer("Background",0,false,true,mlDefaultTileSize,-1);
					mLayersForm.AddLayer("Foreground",12.0f,true,true,mlDefaultTileSize,-1);
					mLayersForm.mlSelectedLayer = 1;
					mLayersForm.objLayerList.SelectedIndex = 1;
					mLayersForm.objShadowLayerList.SelectedIndex = 1;
				}

				RefreshAllData();
			}
		}

		private void EnableAllButtons()
		{
			objModeTilesButton.Enabled =true;
			objModeLightsButton.Enabled =true;
			objModeSoundsSourceButon.Enabled =true;
			objModeTileBrushButton.Enabled =true;
			objModeAreaMode.Enabled =true;
			objModePropsButton.Enabled =true;
			objModeParticlesButton.Enabled =true;
			objModeSelectButton.Enabled=true;

			objVertMapScroll.Enabled = true;
			objHoriMapScroll.Enabled = true;
			objMapPanel.Enabled = true;
			objMapPanel.Show();

			menuFileSaveAs.Enabled = true;
			menuEditMapProp.Enabled = true;
		}

		private void RefreshAllData()
		{
			objMapPanel.Refresh();

			mLayersForm.RefreshData();
			mTilesetsForm.RefreshData();
			mLightForm.RefreshData();
			mPropsForm.RefreshData();
			mSoundForm.RefreshData();
			mParticlesForm.RefreshData();
			mAreasForm.RefreshData();

            UpdateScollBars();
			mMiniMapForm.RefreshData();
		}

		private void ResetAllData()
		{
			mlMapWidth=0;
			mlMapHeight=0;
			msMapName="";
			mlDefaultTileSize=0;
			msMapFile="";
			mbMouseIsDown=false;
			mSelectedTile = null;
			mfLightZ = 10;
			mAmbientColor = Color.Black;

            mlstCopiedEntities.Clear();
			mlstSelectedEntities.Clear();
			
			mLayersForm.ResetData();
			mTilesetsForm.ResetData();
			mLightForm.ResetData();
			mPropsForm.ResetData();
			mSoundForm.ResetData();
			mParticlesForm.ResetData();
			mAreasForm.ResetData();

			mMiniMapForm.ResetData();
			
			RefreshAllData();
		}

		private void objMapPanel_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			int lWorldX = e.X + objHoriMapScroll.Value;
			int lWorldY = e.Y + objVertMapScroll.Value;

			mbMouseIsDown = true;
					
			if(e.Button == MouseButtons.Left)
			{
				//////////////////////////////////////////
				/// LEFTCLICK TILES ///////////////////////
				/////////////////////////////////////////
				if(mEditMode == eEditMode.Tiles)
				{
					if(mTilesetsForm.objDeleteCheckbox.Checked)
					{
						mLayersForm.DeleteTile(lWorldX,lWorldY);
					}
					else if(mTilesetsForm.objBreakCheckBox.Checked)
					{
						mLayersForm.ToggleTileBreak(lWorldX,lWorldY);
						mbMouseIsDown = false;
					}
					else
					{
						mLayersForm.SetTile(lWorldX,lWorldY);
					}
					

					objMapPanel.Refresh();
				}
				//////////////////////////////////////////
				/// LEFTCLICK ELSE ///////////////////////
				/////////////////////////////////////////
				else if(mEditMode!=eEditMode.None)
				{
					if(mbSelectIsOn)
					{
						bool bPickedIsSelected=false;
						cEntity PickedEntity=null;
						
						//Check if the user clicked any of the objects.
						foreach(cEntity EData in mlstCurrentEntityList)
						{
							if(cMath.PointBoxCollision(lWorldX, lWorldY, EData.GetCollideRect()))
							{
								foreach(cEntity EPicked in mlstSelectedEntities){
									if(EData==EPicked){
										bPickedIsSelected=true;
										break;
									}
								}
								if(!bPickedIsSelected)PickedEntity = EData;
								break;
							}
						}

						if(PickedEntity!=null)
						{
							mlstSelectedEntities.Clear();
							mlstSelectedEntities.Add(PickedEntity);
							mbSelectRectOn = false;
						}
						else if(!bPickedIsSelected)
						{
							mlstSelectedEntities.Clear();
							mSelectRect.X = lWorldX;
							mSelectRect.Y = lWorldY;
							mSelectRect.Width=0;
							mSelectRect.Height=0;
							mbSelectRectOn = true;
						}
					}
					else 
					{
						cTileLayer Layer;
						switch(mEditMode)
						{
							case eEditMode.Lights: 
								mlstCurrentEntityList.Add(new cLight(lWorldX, lWorldY,80.0f));
								break;
							case eEditMode.SoundSource: 
								mlstCurrentEntityList.Add(new cSoundSource(lWorldX, lWorldY,0.0f));
								break;
							case eEditMode.Area: 
								mlstCurrentEntityList.Add(new cArea(mAreasForm,lWorldX, lWorldY,0.0f));
								break;
							case eEditMode.Particles:
								Layer = (cTileLayer)mLayersForm.mlstTileLayers[mLayersForm.mlSelectedLayer];
								mlstCurrentEntityList.Add(new cParticle(mParticlesForm,lWorldX, lWorldY,Layer.mfZ+0.5f));
								break;
							case eEditMode.Props:
								cPropData PData = mPropsForm.GetCurrentData();
								Random Rand = new Random();
 
								Layer= (cTileLayer)mLayersForm.mlstTileLayers[mLayersForm.mlSelectedLayer];
								mlstCurrentEntityList.Add(new cProp(PData,lWorldX, lWorldY,
									Layer.mfZ+0.5f+(float)(1.0 -  (2.0*Rand.NextDouble()))*0.5f));
								
								break;
						}
					}
					objMapPanel.Refresh();
				}

				mMiniMapForm.Refresh();
			}
			else if(e.Button == MouseButtons.Right)
			{
				//////////////////////////////////////////
				/// RIGHT CLICK TILES ////////////////////
				/////////////////////////////////////////
				if(mEditMode == eEditMode.Tiles)
				{
					mPopupMenu.MenuItems.Clear();
					cTile TData = mLayersForm.GetTile(lWorldX,lWorldY);
					mSelectedTile = TData;
						
					if(TData != null && TData.mlNum>=0 && TData.mlSet>=0)
					{
						mPopupMenu.MenuItems.Clear();
						mPopupMenu.MenuItems.Add("Select", new EventHandler(PopupMenu_TileSelect));
						MenuItem MenuRot = mPopupMenu.MenuItems.Add("Rotation");
						
						MenuRot.MenuItems.Add("0 Deg", new EventHandler(PopupMenu_TileRotation));
						MenuRot.MenuItems.Add("90 Deg", new EventHandler(PopupMenu_TileRotation));
						MenuRot.MenuItems.Add("180 Deg", new EventHandler(PopupMenu_TileRotation));
						MenuRot.MenuItems.Add("270 Deg", new EventHandler(PopupMenu_TileRotation));

						MenuRot.MenuItems[TData.mlRotation].Checked = true;
						
						mPopupMenu.MenuItems.Add("-");
						mPopupMenu.MenuItems.Add("Delete", new EventHandler(PopupMenu_Delete));
					}
				}
				//////////////////////////////////////////
				/// RIGHT CLICK ENTITY ////////////////////
				/////////////////////////////////////////
				else if(mEditMode != eEditMode.None)
				{
					if(mbSelectIsOn)
					{
						bool bDelete=false;
						bool bProperties=false;

						mPopupMenu.MenuItems.Clear();
						foreach(cEntity EData in mlstSelectedEntities)
							if(cMath.PointBoxCollision(lWorldX, lWorldY, EData.GetCollideRect()))
							{
								bDelete = true;
								if(mlstSelectedEntities.Count==1)bProperties=true;
								break;
							}
						if(!bDelete) 
						{
							mlstSelectedEntities.Clear();
							foreach(cEntity EData in mlstCurrentEntityList)
								if(cMath.PointBoxCollision(lWorldX, lWorldY, EData.GetCollideRect()))
								{
									bDelete = true;bProperties=true;
									mlstSelectedEntities.Add(EData);
									break;
								}
						}

						if(bProperties)	
						{
							mPopupMenu.MenuItems.Add("Properties", new EventHandler(PopupMenu_Properties));
							mPopupMenu.MenuItems.Add("-");
						}
						if(bDelete)mPopupMenu.MenuItems.Add("Copy", new EventHandler(PopupMenu_Copy));
						if(bDelete)mPopupMenu.MenuItems.Add("Delete", new EventHandler(PopupMenu_Delete));

						if(!bDelete)mPopupMenu.MenuItems.Add("Paste", new EventHandler(PopupMenu_Paste));
					}
				}
				else
				{
					mPopupMenu.MenuItems.Clear();	
				}

			}
		}
		
		

		private void objMapPanel_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			mbMouseIsDown = false;

			if(mEditMode != eEditMode.None)
			{	
				if(mbSelectRectOn)
				{
					mbSelectRectOn =false;
					objMapPanel.Refresh();
				}
			}
		}


		private void UpdateScollBars()
		{
			objHoriMapScroll.Maximum = mlMapWidth*mlDefaultTileSize;//- objMapPanel.Width;
			objHoriMapScroll.Minimum = 0;
			objHoriMapScroll.SmallChange = objMapPanel.Width/10;
			objHoriMapScroll.LargeChange = objMapPanel.Width;

			objVertMapScroll.Maximum = mlMapHeight*mlDefaultTileSize;// - objMapPanel.Height;
			objVertMapScroll.Minimum = 0;
			objVertMapScroll.SmallChange = objMapPanel.Height/10;
			objVertMapScroll.LargeChange = objMapPanel.Height;
		}

		private void objVertMapScroll_ValueChanged(object sender, System.EventArgs e)
		{
			objMapPanel.Refresh();
			if(menuViewMiniMap.Checked)mMiniMapForm.Refresh();
		}

		private void objHoriMapScroll_ValueChanged(object sender, System.EventArgs e)
		{
			objMapPanel.Refresh();
			if(menuViewMiniMap.Checked)mMiniMapForm.Refresh();
		}
		
		public int mlLastMouseX = 0;
		public int mlLastMouseY = 0;
		public int mlMouseDX = 0;
		public int mlMouseDY = 0;
		
		private void objMapPanel_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			mlLastMouseX = mlMouseX;
			mlLastMouseY = mlMouseY;
			mlMouseX = e.X;
			mlMouseY = e.Y;

			mlMouseDX = mlMouseX - mlLastMouseX;
			mlMouseDY = mlMouseY - mlLastMouseY;

			//objMapPanel.Refresh();
			
			if(mEditMode == eEditMode.Tiles)
			{
				if(mbMouseIsDown && e.Button == MouseButtons.Left)
				{
					int lX = e.X + objHoriMapScroll.Value;
					int lY = e.Y + objVertMapScroll.Value;

					if(mTilesetsForm.objDeleteCheckbox.Checked){ 
						mLayersForm.DeleteTile(lX,lY);
					}
					else{
						mLayersForm.SetTile(lX,lY);
					}
					
					objMapPanel.Refresh();
					mMiniMapForm.Refresh();
				}
				else
				{
					mbMouseIsDown = false;
				}
			}
			else if(mEditMode!=eEditMode.None)
			{
				if(mbMouseIsDown)
				{
					if(mlstSelectedEntities.Count>0 && !mbSelectRectOn)
					{
						foreach(cEntity EData in mlstSelectedEntities)
						{
							EData.mlX +=mlMouseDX;
							EData.mlY +=mlMouseDY;
						}
					}
					else
					{
						mSelectRect.Width += mlMouseDX;
						mSelectRect.Height += mlMouseDY;
						
						int lX = mSelectRect.X; int lY = mSelectRect.Y;	int lW = mSelectRect.Width; int lH = mSelectRect.Height;
						if(lW<0) 
						{
							lX += mSelectRect.Width;
							lW = -mSelectRect.Width;
						}
						if(lH<0) 
						{
							lY += mSelectRect.Height;
							lH = -mSelectRect.Height;
						}

						mlstSelectedEntities.Clear();
						foreach(cEntity EData in mlstCurrentEntityList)
						{
							if(cMath.BoxCollision(new Rectangle(lX, lY, lW, lH), EData.GetCollideRect()))
							{
								mlstSelectedEntities.Add(EData);
							}
						}
					
					}

					objMapPanel.Refresh();
				}
			}
		}

		private void PopupMenu_Delete(object sender, EventArgs e)
		{
			if(mEditMode == eEditMode.Tiles)
			{
				mSelectedTile.mlNum = -1;
				mSelectedTile.mlSet = -1;
			}
			else if(mEditMode != eEditMode.None)
			{
				foreach(cEntity EData in mlstSelectedEntities)
				{
					mlstCurrentEntityList.Remove(EData);
				}
				mlstSelectedEntities.Clear();
			}
			objMapPanel.Refresh();
		}

		private void PopupMenu_Copy(object sender, EventArgs e)
		{
			if(mEditMode == eEditMode.Tiles)
			{
			}
			else if(mEditMode != eEditMode.None)
			{
				mlstCopiedEntities.Clear();
				foreach(cEntity EData in mlstSelectedEntities)
				{
					mlstCopiedEntities.Add(EData);
				}
				mlCopyX = mlMouseX + objHoriMapScroll.Value;
				mlCopyY = mlMouseY + objVertMapScroll.Value;
			}
			objMapPanel.Refresh();
		}

		private void PopupMenu_Paste(object sender, EventArgs e)
		{
			if(mEditMode == eEditMode.Tiles)
			{
			}
			else if(mEditMode != eEditMode.None)
			{
				/*foreach(cEntity EData in mlstCopiedEntities)
				{
					cEntity CopyData = (cEntity)EData.;
					//CopyData.mlX=C
					
					mlstCurrentEntityList.Add(CopyData);
				}*/
			}
			objMapPanel.Refresh();
		}
		
		private void PopupMenu_Properties(object sender, EventArgs e)
		{
			if(mEditMode != eEditMode.None)
			{
				if(mlstSelectedEntities.Count==1){
					((cEntity)mlstSelectedEntities[0]).EditProperties();
				}
			}
			objMapPanel.Refresh();
		}
		
		private void PopupMenu_TileSelect(object sender, EventArgs e)
		{
			mTilesetsForm.SetSelectedTile(mSelectedTile.mlSet, mSelectedTile.mlNum,
											mSelectedTile.mlRotation);
		}
		
		private void PopupMenu_TileRotation(object sender, EventArgs e)
		{
			MenuItem MRot = mPopupMenu.MenuItems[1];
			MRot.MenuItems[mSelectedTile.mlRotation].Checked = false;
			int lIndex=0;

			if(((MenuItem)sender).Text == "0 Deg"){
				lIndex =0;
			}
			else if(((MenuItem)sender).Text == "90 Deg"){
				lIndex =1;
			} 
			else if(((MenuItem)sender).Text == "180 Deg"){
				lIndex =2;
			}
			else if(((MenuItem)sender).Text == "270 Deg"){
				lIndex =3;
			}
			
			MRot.MenuItems[lIndex].Checked = true;
			mSelectedTile.mlRotation = lIndex;
			objMapPanel.Refresh();
		}

		private void frmMain_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			MessageBox.Show("Control!!");
			/*if(e.KeyCode == Keys.ControlKey){
				mbCtrlPressed = true;
				MessageBox.Show("Control!!");
			}
			else if(e.KeyCode == Keys.Alt)
			{
				mbAltPressed = true;
				MessageBox.Show("Control!!");
			}
			else if(e.KeyCode == Keys.ShiftKey)
			{
				mbShiftPressed = true;
				MessageBox.Show("Control!!");
			}*/
		}

		private void menuFileSaveAs_Click(object sender, System.EventArgs e)
		{
			string sTemp = Directory.GetCurrentDirectory();
			if(mSaveFileDialog.ShowDialog()== DialogResult.OK)
			{
				mSaveFileDialog.InitialDirectory = Path.GetDirectoryName(mSaveFileDialog.FileName);
				menuFileSave.Enabled = true;
				//Fix this somehow.
				Directory.SetCurrentDirectory(sTemp);
				
				mMapIO.Save(mSaveFileDialog.FileName);
			}
		}


		private void menuFileSave_Click(object sender, System.EventArgs e)
		{
			if(mSaveFileDialog.FileName != "")
				mMapIO.Save(mSaveFileDialog.FileName);	
		}

		private void menuFileOpen_Click(object sender, System.EventArgs e)
		{
			string sTemp = Directory.GetCurrentDirectory();
			if(mOpenFileDialog.ShowDialog()== DialogResult.OK)
			{
				ResetAllData();

				mOpenFileDialog.InitialDirectory = Path.GetDirectoryName(mOpenFileDialog.FileName);
				//Fix this somehow.
				Directory.SetCurrentDirectory(sTemp);
				
				if(mMapIO.Load(mOpenFileDialog.FileName)) {
					RefreshAllData();
					EnableAllButtons();
					menuFileSave.Enabled = true;
					mSaveFileDialog.InitialDirectory = Path.GetDirectoryName(mOpenFileDialog.FileName);
					mSaveFileDialog.FileName = mOpenFileDialog.FileName;
					mOpenFileDialog.FileName ="";
				}
				else {
					ResetAllData();
				}
			}		
		}
		
		private void objModeSelectButton_Click(object sender, System.EventArgs e)
		{
			if(!mbSelectIsOn){
				objModeSelectButton.Font = mSelectFontOn;
				objModeSelectButton.BackColor = Color.Red;
				objModeSelectButton.ForeColor = Color.White;
				mbSelectIsOn = true;
			}
			else
			{
				objModeSelectButton.Font = mSelectFontOff;
				objModeSelectButton.BackColor =  mSelectColorOff;
				objModeSelectButton.ForeColor = Color.Black;
				mbSelectIsOn = false;

				mlstSelectedEntities.Clear();
				mlstCopiedEntities.Clear();
			}
		}

		private void menuViewMiniMap_Click(object sender, System.EventArgs e)
		{
			if(!menuViewMiniMap.Checked)
			{
				mMiniMapForm.Show();
				menuViewMiniMap.Checked = true;
			}
			else
			{
				mMiniMapForm.Hide();
				menuViewMiniMap.Checked = false;
			}
		}

		private void menuEditMapProp_Click(object sender, System.EventArgs e)
		{
			
			frmMapProp MapProp = new frmMapProp(this);
			
			MapProp.ShowDialog();
			if(MapProp.mbOkWasPressed)
			{
				RefreshAllData();
			}

            MapProp.Dispose();
		}

		private void objTileGridCheckBox_CheckedChanged(object sender, System.EventArgs e)
		{
			objMapPanel.Refresh();
		}
		
	}
}
