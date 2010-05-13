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

namespace Mapeditor
{
	/// <summary>
	/// Summary description for LayerForm.
	/// </summary>
	public class frmLayers : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox groupLayers;
		private System.Windows.Forms.Button objNewButton;
		private System.Windows.Forms.Button objDeleteButton;
		private System.Windows.Forms.Button objMoveUpButton;
		private System.Windows.Forms.Button objMoveDownButton;
		private System.Windows.Forms.GroupBox groupSettings;
		private System.Windows.Forms.Label ObjShadowLabel;
		public System.Windows.Forms.ComboBox objShadowLayerList;
		private System.Windows.Forms.GroupBox groupGeneral;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private frmMain mMainForm=null;
		public System.Windows.Forms.CheckedListBox objLayerList;

		public ArrayList mlstTileLayers;
		private System.Windows.Forms.Button objEditButton;
		private System.Windows.Forms.Label objPropLabel;

		public int mlSelectedLayer=-1;

		public frmLayers(frmMain aMainForm)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//

			mMainForm = aMainForm;

			mlstTileLayers = new ArrayList();
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
			this.objLayerList = new System.Windows.Forms.CheckedListBox();
			this.groupLayers = new System.Windows.Forms.GroupBox();
			this.objEditButton = new System.Windows.Forms.Button();
			this.objMoveDownButton = new System.Windows.Forms.Button();
			this.objMoveUpButton = new System.Windows.Forms.Button();
			this.objDeleteButton = new System.Windows.Forms.Button();
			this.objNewButton = new System.Windows.Forms.Button();
			this.groupSettings = new System.Windows.Forms.GroupBox();
			this.ObjShadowLabel = new System.Windows.Forms.Label();
			this.objShadowLayerList = new System.Windows.Forms.ComboBox();
			this.groupGeneral = new System.Windows.Forms.GroupBox();
			this.objPropLabel = new System.Windows.Forms.Label();
			this.groupLayers.SuspendLayout();
			this.groupSettings.SuspendLayout();
			this.groupGeneral.SuspendLayout();
			this.SuspendLayout();
			// 
			// objLayerList
			// 
			this.objLayerList.Location = new System.Drawing.Point(8, 24);
			this.objLayerList.Name = "objLayerList";
			this.objLayerList.ScrollAlwaysVisible = true;
			this.objLayerList.Size = new System.Drawing.Size(248, 79);
			this.objLayerList.TabIndex = 0;
			this.objLayerList.SelectedIndexChanged += new System.EventHandler(this.objLayerList_SelectedIndexChanged);
			this.objLayerList.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.objLayerList_ItemCheck);
			// 
			// groupLayers
			// 
			this.groupLayers.Controls.Add(this.objEditButton);
			this.groupLayers.Controls.Add(this.objMoveDownButton);
			this.groupLayers.Controls.Add(this.objMoveUpButton);
			this.groupLayers.Controls.Add(this.objDeleteButton);
			this.groupLayers.Controls.Add(this.objNewButton);
			this.groupLayers.Controls.Add(this.objLayerList);
			this.groupLayers.Location = new System.Drawing.Point(8, 8);
			this.groupLayers.Name = "groupLayers";
			this.groupLayers.Size = new System.Drawing.Size(264, 152);
			this.groupLayers.TabIndex = 1;
			this.groupLayers.TabStop = false;
			this.groupLayers.Text = "Layers";
			// 
			// objEditButton
			// 
			this.objEditButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.objEditButton.Location = new System.Drawing.Point(56, 112);
			this.objEditButton.Name = "objEditButton";
			this.objEditButton.Size = new System.Drawing.Size(40, 24);
			this.objEditButton.TabIndex = 5;
			this.objEditButton.Text = "Edit";
			this.objEditButton.Click += new System.EventHandler(this.objEditButton_Click);
			// 
			// objMoveDownButton
			// 
			this.objMoveDownButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.objMoveDownButton.Location = new System.Drawing.Point(208, 112);
			this.objMoveDownButton.Name = "objMoveDownButton";
			this.objMoveDownButton.Size = new System.Drawing.Size(48, 24);
			this.objMoveDownButton.TabIndex = 4;
			this.objMoveDownButton.Text = "Down";
			this.objMoveDownButton.Click += new System.EventHandler(this.objMoveDownButton_Click);
			// 
			// objMoveUpButton
			// 
			this.objMoveUpButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.objMoveUpButton.Location = new System.Drawing.Point(152, 112);
			this.objMoveUpButton.Name = "objMoveUpButton";
			this.objMoveUpButton.Size = new System.Drawing.Size(48, 24);
			this.objMoveUpButton.TabIndex = 3;
			this.objMoveUpButton.Text = "Up";
			this.objMoveUpButton.Click += new System.EventHandler(this.objMoveUpButton_Click);
			// 
			// objDeleteButton
			// 
			this.objDeleteButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.objDeleteButton.Location = new System.Drawing.Point(104, 112);
			this.objDeleteButton.Name = "objDeleteButton";
			this.objDeleteButton.Size = new System.Drawing.Size(40, 24);
			this.objDeleteButton.TabIndex = 2;
			this.objDeleteButton.Text = "Delete";
			this.objDeleteButton.Click += new System.EventHandler(this.objDeleteButton_Click);
			// 
			// objNewButton
			// 
			this.objNewButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.objNewButton.Location = new System.Drawing.Point(8, 112);
			this.objNewButton.Name = "objNewButton";
			this.objNewButton.Size = new System.Drawing.Size(40, 24);
			this.objNewButton.TabIndex = 1;
			this.objNewButton.Text = "New";
			this.objNewButton.Click += new System.EventHandler(this.objNewButton_Click);
			// 
			// groupSettings
			// 
			this.groupSettings.Controls.Add(this.objPropLabel);
			this.groupSettings.Location = new System.Drawing.Point(8, 168);
			this.groupSettings.Name = "groupSettings";
			this.groupSettings.Size = new System.Drawing.Size(264, 56);
			this.groupSettings.TabIndex = 2;
			this.groupSettings.TabStop = false;
			this.groupSettings.Text = "Properties";
			// 
			// ObjShadowLabel
			// 
			this.ObjShadowLabel.Location = new System.Drawing.Point(8, 16);
			this.ObjShadowLabel.Name = "ObjShadowLabel";
			this.ObjShadowLabel.Size = new System.Drawing.Size(80, 16);
			this.ObjShadowLabel.TabIndex = 0;
			this.ObjShadowLabel.Text = "Shadow Layer:";
			// 
			// objShadowLayerList
			// 
			this.objShadowLayerList.Location = new System.Drawing.Point(88, 16);
			this.objShadowLayerList.Name = "objShadowLayerList";
			this.objShadowLayerList.Size = new System.Drawing.Size(168, 21);
			this.objShadowLayerList.TabIndex = 1;
			// 
			// groupGeneral
			// 
			this.groupGeneral.Controls.Add(this.ObjShadowLabel);
			this.groupGeneral.Controls.Add(this.objShadowLayerList);
			this.groupGeneral.Location = new System.Drawing.Point(8, 232);
			this.groupGeneral.Name = "groupGeneral";
			this.groupGeneral.Size = new System.Drawing.Size(264, 48);
			this.groupGeneral.TabIndex = 3;
			this.groupGeneral.TabStop = false;
			this.groupGeneral.Text = "General";
			// 
			// objPropLabel
			// 
			this.objPropLabel.Location = new System.Drawing.Point(8, 16);
			this.objPropLabel.Name = "objPropLabel";
			this.objPropLabel.Size = new System.Drawing.Size(248, 32);
			this.objPropLabel.TabIndex = 0;
			// 
			// frmLayers
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(280, 285);
			this.ControlBox = false;
			this.Controls.Add(this.groupSettings);
			this.Controls.Add(this.groupLayers);
			this.Controls.Add(this.groupGeneral);
			this.Location = new System.Drawing.Point(900, 550);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmLayers";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Layer Toolbox";
			this.TopMost = true;
			this.groupLayers.ResumeLayout(false);
			this.groupSettings.ResumeLayout(false);
			this.groupGeneral.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		public void DrawAllLayers(Graphics aGfx, int lX, int lY,int lW, int lH)
		{
			for(int i=0;i<mlstTileLayers.Count;i++)
			{
				if(objLayerList.GetItemCheckState(i)==CheckState.Checked)
				{
					bool bGrid = false;
					
					if(mlSelectedLayer == i && mMainForm.objTileGridCheckBox.Checked){
						bGrid = true;
					}
					
					((cTileLayer)mlstTileLayers[i]).Draw(aGfx,lX,lY,lW,lH,bGrid);
				}
			}
		}

		public void DrawAllLayersMini(Graphics aGfx, int lX, int lY,int lW, int lH)
		{
			for(int i=0;i<mlstTileLayers.Count;i++)
			{
				if(objLayerList.GetItemCheckState(i)==CheckState.Checked)
				{
					((cTileLayer)mlstTileLayers[i]).DrawMini(aGfx,lX,lY,lW,lH);
				}
			}
		}

		public void ResizeAll(int alWidth, int alHeight)
		{
			for(int i=0;i<mlstTileLayers.Count;i++)
			{
				((cTileLayer)mlstTileLayers[i]).Resize(alWidth, alHeight);
			}
		}
		
		public void AddLayer(cTileLayer aTLayer, int alPos)
		{
			if(alPos<0)
			{
				mlstTileLayers.Add(aTLayer);
				
				objLayerList.Items.Add(objLayerList.Items.Count+" : "+aTLayer.msName);
				objLayerList.SetItemChecked(objLayerList.Items.Count-1,true);
				objShadowLayerList.Items.Add(objShadowLayerList.Items.Count+" : "+aTLayer.msName);
			}
			else
			{
				mlstTileLayers.Insert(alPos,aTLayer);

				int LastShadowLayer = objShadowLayerList.SelectedIndex;

				objLayerList.Items.Clear();
				objShadowLayerList.Items.Clear();

				for(int i=0;i<mlstTileLayers.Count;i++)
				{
					objLayerList.Items.Add(i+" : "+((cTileLayer)mlstTileLayers[i]).msName);
					objShadowLayerList.Items.Add(i+" : "+((cTileLayer)mlstTileLayers[i]).msName);
				}

				for(int i=0;i<objLayerList.Items.Count;i++)
				{
					objLayerList.SetItemChecked(i,true);
				}

				if(LastShadowLayer>=alPos)
				{
					LastShadowLayer++;
					objShadowLayerList.SelectedIndex = LastShadowLayer;
				}
			}
				
			
			
			if(objLayerList.Items.Count==1)
			{
				objLayerList.SelectedIndex = 0;
				objShadowLayerList.SelectedIndex=0;
				mlSelectedLayer =0;
			}

			if(objShadowLayerList.SelectedIndex<0)objShadowLayerList.SelectedIndex=0;
		}
		
		public void AddLayer(string asName,float afZ, bool abCollide, bool abLit,int alTileSize,int alPos)
		{
			cTileLayer TLayer = new cTileLayer(mMainForm.mTilesetsForm,asName,
												mMainForm.mlMapWidth, mMainForm.mlMapHeight,
												afZ, abCollide,abLit,alTileSize);

			AddLayer(TLayer, alPos);
			
			UpdateSettings();
		}

		public cTileLayer GetCurrentLayer()
		{
			if(mlSelectedLayer<0)return null;

			return (cTileLayer)mlstTileLayers[mlSelectedLayer];
		}

		public void SetTile(int alX, int alY)
		{
			frmTileSets mTileSetsForm = mMainForm.mTilesetsForm;

			if(mTileSetsForm.mlSelectedTile<0) return;
			if(mTileSetsForm.mlSelectedTileSet<0) return;
			if(mlSelectedLayer<0)return;
			
			cTileLayer TLayer = (cTileLayer) mlstTileLayers[mlSelectedLayer];
			TLayer.SetTile(alX, alY,mTileSetsForm.mlSelectedTileSet,mTileSetsForm.mlSelectedTile,
				mTileSetsForm.objRotationList.SelectedIndex);			
		}

		public void DeleteTile(int alX, int alY)
		{
			frmTileSets mTileSetsForm = mMainForm.mTilesetsForm;

			if(mTileSetsForm.mlSelectedTile<0) return;
			if(mTileSetsForm.mlSelectedTileSet<0) return;
			if(mlSelectedLayer<0)return;
			
			cTileLayer TLayer = (cTileLayer) mlstTileLayers[mlSelectedLayer];
			TLayer.SetTile(alX, alY,-1,-1,0);			
		}

		public void ToggleTileBreak(int alX, int alY)
		{
			frmTileSets mTileSetsForm = mMainForm.mTilesetsForm;

			if(mTileSetsForm.mlSelectedTile<0) return;
			if(mTileSetsForm.mlSelectedTileSet<0) return;
			if(mlSelectedLayer<0)return;
			
			cTileLayer TLayer = (cTileLayer) mlstTileLayers[mlSelectedLayer];
			TLayer.ToggleTileBreak(alX, alY);			
		}

		public cTile GetTile(int alX, int alY)
		{
			frmTileSets mTileSetsForm = mMainForm.mTilesetsForm;

			if(mTileSetsForm.mlSelectedTile<0) return null;
			if(mTileSetsForm.mlSelectedTileSet<0) return null;
			if(mlSelectedLayer<0)return null;
			
			cTileLayer TLayer = (cTileLayer) mlstTileLayers[mlSelectedLayer];
			return TLayer.GetTile(alX, alY);			
		}
		
		private void objLayerList_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			mlSelectedLayer = objLayerList.SelectedIndex;
			UpdateSettings();
			mMainForm.mSelectedTile = null;
			mMainForm.objMapPanel.Refresh();
			UpdatePropLabel();
			mMainForm.mPopupMenu.MenuItems.Clear();
		}

		private void UpdateSettings()
		{
			if(mlSelectedLayer>=0)
			{
				//objDepthText.Text = ((cTileLayer)mlstTileLayers[mlSelectedLayer]).mfZ.ToString();
			}
			else
			{
				//objDepthText.Text = "";
			}
		}

		private void objLayerList_ItemCheck(object sender, System.Windows.Forms.ItemCheckEventArgs e)
		{
			mMainForm.objMapPanel.Refresh();
		}

		public void ResetData()
		{
			objLayerList.Items.Clear();
			objLayerList.SelectedIndex = -1;
			objShadowLayerList.Items.Clear();
			objShadowLayerList.SelectedIndex = -1;
			
			mlstTileLayers.Clear();
			mlSelectedLayer=-1;
		}

		public void RefreshData()
		{
			objLayerList.Refresh();
			objShadowLayerList.Refresh();
			UpdatePropLabel();
		}

		private void objNewButton_Click(object sender, System.EventArgs e)
		{
			cTileLayer LastLayer = (cTileLayer)mlstTileLayers[mlSelectedLayer];

			frmEditLayer EditLayer = new frmEditLayer();

			EditLayer.objNameText.Text = "Untitled";
			EditLayer.objZText.Text =  	(LastLayer.mfZ+1).ToString();
			EditLayer.objLitBox.SelectedIndex = 1;
			EditLayer.objCollideBox.SelectedIndex = 1;

			EditLayer.ShowDialog();
			
			if(EditLayer.mbOkWasPressed)
			{
				AddLayer(EditLayer.objNameText.Text,(float)Convert.ToDouble(EditLayer.objZText.Text),
					EditLayer.objCollideBox.SelectedIndex==1,EditLayer.objLitBox.SelectedIndex==1,
					LastLayer.mlTileSize,mlSelectedLayer+1);
						
				mlSelectedLayer = mlSelectedLayer+1;
				objLayerList.SelectedIndex = mlSelectedLayer;
			}

			EditLayer.Dispose();
		}

		private void objEditButton_Click(object sender, System.EventArgs e)
		{
			frmEditLayer EditLayer = new frmEditLayer();

			cTileLayer Layer = (cTileLayer)mlstTileLayers[mlSelectedLayer];

			EditLayer.objNameText.Text = Layer.msName;
			EditLayer.objZText.Text =  	Layer.mfZ.ToString();
			EditLayer.objLitBox.SelectedIndex = Layer.mbLit?1:0;
			EditLayer.objCollideBox.SelectedIndex = Layer.mbCollide?1:0;

			EditLayer.ShowDialog();
			
			if(EditLayer.mbOkWasPressed)
			{
				Layer.msName = EditLayer.objNameText.Text;
				Layer.mfZ = (float)Convert.ToDouble(EditLayer.objZText.Text);
				Layer.mbLit = EditLayer.objLitBox.SelectedIndex==1;
				Layer.mbCollide = EditLayer.objCollideBox.SelectedIndex==1;

				UpdateLayers();
				
				objLayerList.SelectedIndex = mlSelectedLayer;
			}

			EditLayer.Dispose();
		}

		private void objDeleteButton_Click(object sender, System.EventArgs e)
		{
			if(mlstTileLayers.Count==1){
				MessageBox.Show("There must be at least one layer!","Error");
				return;
			}

			DialogResult Result = MessageBox.Show("Are you sure you want to delete the layer '"+
							((cTileLayer)mlstTileLayers[mlSelectedLayer]).msName+"'?",
							"Warning",MessageBoxButtons.YesNo);
			
			if(Result == DialogResult.Yes)
			{
				cTileLayer Layer = (cTileLayer)mlstTileLayers[mlSelectedLayer];
				mlstTileLayers.Remove(Layer);

				UpdateLayers();
				
				if(mlSelectedLayer>0)mlSelectedLayer--;
				objLayerList.SelectedIndex = mlSelectedLayer;
			}
		}

		private void objMoveUpButton_Click(object sender, System.EventArgs e)
		{
			if(mlSelectedLayer<1 || mlstTileLayers.Count<=1)return;
			
            cTileLayer TempLayer = (cTileLayer)mlstTileLayers[mlSelectedLayer];
			mlstTileLayers[mlSelectedLayer] = mlstTileLayers[mlSelectedLayer-1];
			mlstTileLayers[mlSelectedLayer-1] = TempLayer;

			UpdateLayers();
			mlSelectedLayer--;
			objLayerList.SelectedIndex = mlSelectedLayer;
		}

		private void objMoveDownButton_Click(object sender, System.EventArgs e)
		{	
			if(mlSelectedLayer>=mlstTileLayers.Count-1)return;
			
			cTileLayer TempLayer = (cTileLayer)mlstTileLayers[mlSelectedLayer];
			mlstTileLayers[mlSelectedLayer] = mlstTileLayers[mlSelectedLayer+1];
			mlstTileLayers[mlSelectedLayer+1] = TempLayer;

			UpdateLayers();
			mlSelectedLayer++;
			objLayerList.SelectedIndex = mlSelectedLayer;
		}

		private void UpdatePropLabel()
		{
			if(mlSelectedLayer<0)return;
			cTileLayer Layer = (cTileLayer)mlstTileLayers[mlSelectedLayer];
			objPropLabel.Text = "Z: "+ Layer.mfZ + "\n"+
								"Collide: "+Layer.mbCollide+"  Lit: "+Layer.mbLit;
		}

		private void UpdateLayers()
		{
			int LastShadowIndex = objShadowLayerList.SelectedIndex;
			objLayerList.Items.Clear();
			objShadowLayerList.Items.Clear();

			for(int i=0;i<mlstTileLayers.Count;i++)	
			{
				objLayerList.Items.Add(i+" : "+((cTileLayer)mlstTileLayers[i]).msName);
				objShadowLayerList.Items.Add(i+" : "+((cTileLayer)mlstTileLayers[i]).msName);
			}
			for(int i=0;i<objLayerList.Items.Count;i++)	objLayerList.SetItemChecked(i,true);

			if(LastShadowIndex>=objShadowLayerList.Items.Count)
			{
				objShadowLayerList.SelectedIndex = objShadowLayerList.Items.Count-1;
			}
			else
			{
				objShadowLayerList.SelectedIndex = LastShadowIndex;
			}
		}

		
	}
}
