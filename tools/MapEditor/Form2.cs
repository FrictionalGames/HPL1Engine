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

namespace Mapeditor
{
	/// <summary>
	/// Summary description for Form2.
	/// </summary>
	public class frmTileSets : System.Windows.Forms.Form
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		public System.Windows.Forms.Button objAddButton;
		public System.Windows.Forms.Button objDelButton;
		public System.Windows.Forms.ComboBox objSetList;
		public System.Windows.Forms.Panel objTilePanel;
		public System.Windows.Forms.Button objEditButton;
		public System.Windows.Forms.VScrollBar objTileScoll;
		public System.Windows.Forms.GroupBox groupTileSet;

		public ArrayList mlstTileSets=null;
		public int mlSelectedTile=-1;
		public int mlSelectedTileSet=-1;

		public OpenFileDialog mOpenFileDialog=null;
		public System.Windows.Forms.GroupBox groupTile;
		public System.Windows.Forms.Panel objBigTilePanel;
		public System.Windows.Forms.Label objLabelName;
		public System.Windows.Forms.Label objLabelMaterial;
		public System.Windows.Forms.Label objLabelMesh;
		public System.Windows.Forms.Label objLabelSize;
		private System.Windows.Forms.Label objRotationLabel;
		public System.Windows.Forms.ComboBox objRotationList;
		public System.Windows.Forms.CheckBox objDeleteCheckbox;
		public System.Windows.Forms.CheckBox objBreakCheckBox;
		private frmMain mMainForm;
        
		public frmTileSets(frmMain aMainForm)
		{
			InitializeComponent();

			mMainForm = aMainForm;
			
			mOpenFileDialog = new OpenFileDialog();
			mOpenFileDialog.Filter = "tileset files (*.tsd)|*.tsd|All files (*.*)|*.*";
			mOpenFileDialog.InitialDirectory = (string)Directory.GetCurrentDirectory().Clone();

			mlstTileSets = new ArrayList();

			objRotationList.SelectedIndex =0;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
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
			this.objAddButton = new System.Windows.Forms.Button();
			this.objDelButton = new System.Windows.Forms.Button();
			this.objSetList = new System.Windows.Forms.ComboBox();
			this.objTilePanel = new System.Windows.Forms.Panel();
			this.objEditButton = new System.Windows.Forms.Button();
			this.objTileScoll = new System.Windows.Forms.VScrollBar();
			this.groupTileSet = new System.Windows.Forms.GroupBox();
			this.objDeleteCheckbox = new System.Windows.Forms.CheckBox();
			this.objRotationList = new System.Windows.Forms.ComboBox();
			this.objRotationLabel = new System.Windows.Forms.Label();
			this.groupTile = new System.Windows.Forms.GroupBox();
			this.objLabelSize = new System.Windows.Forms.Label();
			this.objLabelMesh = new System.Windows.Forms.Label();
			this.objLabelMaterial = new System.Windows.Forms.Label();
			this.objLabelName = new System.Windows.Forms.Label();
			this.objBigTilePanel = new System.Windows.Forms.Panel();
			this.objBreakCheckBox = new System.Windows.Forms.CheckBox();
			this.groupTileSet.SuspendLayout();
			this.groupTile.SuspendLayout();
			this.SuspendLayout();
			// 
			// objAddButton
			// 
			this.objAddButton.Location = new System.Drawing.Point(32, 24);
			this.objAddButton.Name = "objAddButton";
			this.objAddButton.Size = new System.Drawing.Size(72, 24);
			this.objAddButton.TabIndex = 0;
			this.objAddButton.Text = "Add Set";
			this.objAddButton.Click += new System.EventHandler(this.objAddButton_Click);
			// 
			// objDelButton
			// 
			this.objDelButton.Location = new System.Drawing.Point(104, 24);
			this.objDelButton.Name = "objDelButton";
			this.objDelButton.Size = new System.Drawing.Size(72, 24);
			this.objDelButton.TabIndex = 1;
			this.objDelButton.Text = "Delete Set";
			// 
			// objSetList
			// 
			this.objSetList.Location = new System.Drawing.Point(24, 56);
			this.objSetList.Name = "objSetList";
			this.objSetList.Size = new System.Drawing.Size(240, 21);
			this.objSetList.TabIndex = 3;
			this.objSetList.SelectedIndexChanged += new System.EventHandler(this.objSetList_SelectedIndexChanged);
			// 
			// objTilePanel
			// 
			this.objTilePanel.BackColor = System.Drawing.Color.Gainsboro;
			this.objTilePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.objTilePanel.Location = new System.Drawing.Point(24, 88);
			this.objTilePanel.Name = "objTilePanel";
			this.objTilePanel.Size = new System.Drawing.Size(200, 184);
			this.objTilePanel.TabIndex = 4;
			this.objTilePanel.Paint += new System.Windows.Forms.PaintEventHandler(this.objTilePanel_Paint);
			this.objTilePanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.objTilePanel_MouseDown);
			// 
			// objEditButton
			// 
			this.objEditButton.Location = new System.Drawing.Point(176, 24);
			this.objEditButton.Name = "objEditButton";
			this.objEditButton.Size = new System.Drawing.Size(72, 24);
			this.objEditButton.TabIndex = 5;
			this.objEditButton.Text = "Edit Set";
			// 
			// objTileScoll
			// 
			this.objTileScoll.LargeChange = 1;
			this.objTileScoll.Location = new System.Drawing.Point(240, 88);
			this.objTileScoll.Maximum = 0;
			this.objTileScoll.Name = "objTileScoll";
			this.objTileScoll.Size = new System.Drawing.Size(16, 184);
			this.objTileScoll.TabIndex = 6;
			// 
			// groupTileSet
			// 
			this.groupTileSet.Controls.Add(this.objBreakCheckBox);
			this.groupTileSet.Controls.Add(this.objDeleteCheckbox);
			this.groupTileSet.Controls.Add(this.objRotationList);
			this.groupTileSet.Controls.Add(this.objRotationLabel);
			this.groupTileSet.Controls.Add(this.objAddButton);
			this.groupTileSet.Controls.Add(this.objTileScoll);
			this.groupTileSet.Controls.Add(this.objEditButton);
			this.groupTileSet.Controls.Add(this.objTilePanel);
			this.groupTileSet.Controls.Add(this.objSetList);
			this.groupTileSet.Controls.Add(this.objDelButton);
			this.groupTileSet.Location = new System.Drawing.Point(8, 8);
			this.groupTileSet.Name = "groupTileSet";
			this.groupTileSet.Size = new System.Drawing.Size(272, 336);
			this.groupTileSet.TabIndex = 7;
			this.groupTileSet.TabStop = false;
			this.groupTileSet.Text = "TileSet";
			// 
			// objDeleteCheckbox
			// 
			this.objDeleteCheckbox.Location = new System.Drawing.Point(24, 312);
			this.objDeleteCheckbox.Name = "objDeleteCheckbox";
			this.objDeleteCheckbox.Size = new System.Drawing.Size(120, 16);
			this.objDeleteCheckbox.TabIndex = 9;
			this.objDeleteCheckbox.Text = "Delete Tiles Mode";
			this.objDeleteCheckbox.CheckedChanged += new System.EventHandler(this.objDeleteCheckbox_CheckedChanged);
			// 
			// objRotationList
			// 
			this.objRotationList.Items.AddRange(new object[] {
																 "0 Deg",
																 "90 Deg",
																 "180 Deg",
																 "270 Deg"});
			this.objRotationList.Location = new System.Drawing.Point(80, 280);
			this.objRotationList.Name = "objRotationList";
			this.objRotationList.Size = new System.Drawing.Size(144, 21);
			this.objRotationList.TabIndex = 8;
			// 
			// objRotationLabel
			// 
			this.objRotationLabel.Location = new System.Drawing.Point(24, 280);
			this.objRotationLabel.Name = "objRotationLabel";
			this.objRotationLabel.Size = new System.Drawing.Size(48, 16);
			this.objRotationLabel.TabIndex = 7;
			this.objRotationLabel.Text = "Rotation:";
			// 
			// groupTile
			// 
			this.groupTile.Controls.Add(this.objLabelSize);
			this.groupTile.Controls.Add(this.objLabelMesh);
			this.groupTile.Controls.Add(this.objLabelMaterial);
			this.groupTile.Controls.Add(this.objLabelName);
			this.groupTile.Controls.Add(this.objBigTilePanel);
			this.groupTile.Location = new System.Drawing.Point(8, 360);
			this.groupTile.Name = "groupTile";
			this.groupTile.Size = new System.Drawing.Size(272, 120);
			this.groupTile.TabIndex = 8;
			this.groupTile.TabStop = false;
			this.groupTile.Text = "Tile";
			// 
			// objLabelSize
			// 
			this.objLabelSize.Location = new System.Drawing.Point(112, 64);
			this.objLabelSize.Name = "objLabelSize";
			this.objLabelSize.Size = new System.Drawing.Size(152, 16);
			this.objLabelSize.TabIndex = 4;
			this.objLabelSize.Text = "Size:";
			// 
			// objLabelMesh
			// 
			this.objLabelMesh.Location = new System.Drawing.Point(112, 48);
			this.objLabelMesh.Name = "objLabelMesh";
			this.objLabelMesh.Size = new System.Drawing.Size(152, 16);
			this.objLabelMesh.TabIndex = 3;
			this.objLabelMesh.Text = "Mesh:";
			// 
			// objLabelMaterial
			// 
			this.objLabelMaterial.Location = new System.Drawing.Point(112, 32);
			this.objLabelMaterial.Name = "objLabelMaterial";
			this.objLabelMaterial.Size = new System.Drawing.Size(152, 16);
			this.objLabelMaterial.TabIndex = 2;
			this.objLabelMaterial.Text = "Material:";
			// 
			// objLabelName
			// 
			this.objLabelName.Location = new System.Drawing.Point(112, 16);
			this.objLabelName.Name = "objLabelName";
			this.objLabelName.Size = new System.Drawing.Size(152, 16);
			this.objLabelName.TabIndex = 1;
			this.objLabelName.Text = "Name:";
			// 
			// objBigTilePanel
			// 
			this.objBigTilePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.objBigTilePanel.Location = new System.Drawing.Point(8, 16);
			this.objBigTilePanel.Name = "objBigTilePanel";
			this.objBigTilePanel.Size = new System.Drawing.Size(96, 96);
			this.objBigTilePanel.TabIndex = 0;
			this.objBigTilePanel.Paint += new System.Windows.Forms.PaintEventHandler(this.objBigTilePanel_Paint);
			// 
			// objBreakCheckBox
			// 
			this.objBreakCheckBox.Location = new System.Drawing.Point(152, 312);
			this.objBreakCheckBox.Name = "objBreakCheckBox";
			this.objBreakCheckBox.Size = new System.Drawing.Size(112, 16);
			this.objBreakCheckBox.TabIndex = 10;
			this.objBreakCheckBox.Text = "Breakable Mode";
			this.objBreakCheckBox.CheckedChanged += new System.EventHandler(this.objBreakCheckBox_CheckedChanged);
			// 
			// frmTileSets
			// 
			this.AutoScale = false;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(290, 487);
			this.ControlBox = false;
			this.Controls.Add(this.groupTile);
			this.Controls.Add(this.groupTileSet);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Location = new System.Drawing.Point(880, 50);
			this.MaximizeBox = false;
			this.Name = "frmTileSets";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Tile Toolbar";
			this.TopMost = true;
			this.groupTileSet.ResumeLayout(false);
			this.groupTile.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void objAddButton_Click(object sender, System.EventArgs e)
		{
			string sTemp = Directory.GetCurrentDirectory();
			if(mOpenFileDialog.ShowDialog()== DialogResult.OK)
			{
				cTileSet TSData = new cTileSet(mOpenFileDialog.FileName);
				mOpenFileDialog.InitialDirectory = Path.GetDirectoryName(mOpenFileDialog.FileName);
				
				//Fix this somehow.
				Directory.SetCurrentDirectory(sTemp);
				TSData.LoadData();

				AddTileSet(TSData);
			}
		}

		public void AddTileSet(cTileSet aTSData)
		{
			mlstTileSets.Add(aTSData);
			objSetList.Items.Add(objSetList.Items.Count + " : " + aTSData.msName);
				
			if(objSetList.Items.Count==1)
			{
				objSetList.SelectedIndex =0;
				mlSelectedTileSet =0;
			}

			objTilePanel.Refresh();

			/*Update the scrollbar*/
		}

		private void objTilePanel_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			Graphics Gfx = e.Graphics;

			if(objSetList.Items.Count<1)return;
			
			int lIndex = mlSelectedTileSet;
			if(lIndex<0)return;
			
			cTileSet TSetData = (cTileSet)mlstTileSets[lIndex];
			int lTileSize = TSetData.mlTileSize;
			
            int lX=0,lY=0,lCount=0;
			//Note lY at start should depend on the scroller!
			foreach(cTileData TData in TSetData.mlstTileData)
			{
				bool bHighLight = mlSelectedTile==lCount;
				TData.Draw(Gfx,lX,lY,true, bHighLight,0);
				
				lX+=lTileSize;
				if(lX >= objTilePanel.Width){
					lX=0;
					lY+=lTileSize;
				}
				lCount++;
			}
		}

		private void objSetList_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			mlSelectedTileSet = objSetList.SelectedIndex;
			mlSelectedTile = 0;
			objTilePanel.Refresh();
			UpdateTileInfo();
		}

		private void objTilePanel_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if(objSetList.Items.Count<1)return;
			int lIndex = mlSelectedTileSet;
			if(lIndex<0)return;
			
			cTileSet TSetData = (cTileSet)mlstTileSets[lIndex];
			
			int lRowSize = objTilePanel.Width / TSetData.mlTileSize;
            
		    int lTileIndex = (e.Y/TSetData.mlTileSize)*lRowSize	+ (e.X/TSetData.mlTileSize);

			if(lTileIndex >= TSetData.mlstTileData.Count)return;
			
			mlSelectedTile = lTileIndex;
			objTilePanel.Refresh();
			UpdateTileInfo();
		}

		private void objBigTilePanel_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			if(objSetList.Items.Count<1)return;
			int lIndex = mlSelectedTileSet;
			if(lIndex<0)return;
			if(mlSelectedTile<0)return;
			
			cTileSet TSetData = (cTileSet)mlstTileSets[lIndex];
			cTileData TData = (cTileData)TSetData.mlstTileData[mlSelectedTile];

			e.Graphics.DrawImage(TData.mTileImage,
								0,0,
								objBigTilePanel.Width,objBigTilePanel.Height);
		}

		private void UpdateTileInfo()
		{
			objBigTilePanel.Refresh();
			
			if(objSetList.Items.Count<0 || mlSelectedTileSet<0 || mlSelectedTile<0)
			{
				objLabelName.Text = "Name: ";
				objLabelMaterial.Text = "Material: ";
				objLabelMesh.Text = "Mesh: ";
				objLabelSize.Text = "Size: ";
			}
			else
			{
				int lIndex = mlSelectedTileSet;
				cTileSet TSetData = (cTileSet)mlstTileSets[lIndex];
				cTileData TData = (cTileData)TSetData.mlstTileData[mlSelectedTile];
				objLabelName.Text = "Name: " + TData.msName;
				objLabelMaterial.Text = "Material: " + TData.msMaterialType;
				objLabelMesh.Text = "Mesh: " + TData.msMeshType;
				objLabelSize.Text = "Size: " + TData.mTileImage.Width +"x"+TData.mTileImage.Height;
			}		
		}

		public void SetSelectedTile(int alSet, int alNum, int alRot)
		{
			mlSelectedTileSet = alSet;
			mlSelectedTile = alNum;
			objSetList.SelectedIndex = alSet;
			objTilePanel.Refresh();
			objRotationList.SelectedIndex = alRot;

			UpdateTileInfo();
		}

		public void ResetData()
		{
			mlstTileSets.Clear();
			objSetList.SelectedIndex=-1;
			objSetList.Items.Clear();
			mlSelectedTile=-1;
			mlSelectedTileSet=-1;
		}

		public void RefreshData()
		{
			objTilePanel.Refresh();
			UpdateTileInfo();
		}

		private void objBreakCheckBox_CheckedChanged(object sender, System.EventArgs e)
		{
			if(objBreakCheckBox.Checked)
			{
				objDeleteCheckbox.Checked = false;
			}
		}

		private void objDeleteCheckbox_CheckedChanged(object sender, System.EventArgs e)
		{
			if(objDeleteCheckbox.Checked)
			{
				objBreakCheckBox.Checked = false;
			}
		}
	}
}
