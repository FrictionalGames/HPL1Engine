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
using System.IO;

namespace Mapeditor
{
	
	/// <summary>
	/// Summary description for PropForm.
	/// </summary>
	public class frmProps : System.Windows.Forms.Form
	{
		public frmMain mMainForm;

		public OpenFileDialog mOpenFileDialog=null;

		public int mlSelectedProp=-1;

		public ArrayList mlstPropData;
		public ArrayList mlstProps;

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button objLoadButton;
		private System.Windows.Forms.Button objDeleteButton;
		private System.Windows.Forms.Button objEditButton;
		private System.Windows.Forms.ListBox objPropList;
		private System.Windows.Forms.Panel objImagePanel;
		private System.Windows.Forms.Label objTextLabel;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmProps(frmMain aMainFrom)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//

			mMainForm = aMainFrom;

			mlstPropData = new ArrayList();
			mlstProps = new ArrayList();

			mOpenFileDialog = new OpenFileDialog();
			mOpenFileDialog.Filter = "entity files (*.hed)|*.hed|All files (*.*)|*.*";
			mOpenFileDialog.InitialDirectory = (string)Directory.GetCurrentDirectory().Clone();

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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.objPropList = new System.Windows.Forms.ListBox();
			this.objEditButton = new System.Windows.Forms.Button();
			this.objDeleteButton = new System.Windows.Forms.Button();
			this.objLoadButton = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.objTextLabel = new System.Windows.Forms.Label();
			this.objImagePanel = new System.Windows.Forms.Panel();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.objPropList);
			this.groupBox1.Controls.Add(this.objEditButton);
			this.groupBox1.Controls.Add(this.objDeleteButton);
			this.groupBox1.Controls.Add(this.objLoadButton);
			this.groupBox1.Location = new System.Drawing.Point(8, 16);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(240, 192);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Props";
			// 
			// objPropList
			// 
			this.objPropList.Location = new System.Drawing.Point(48, 48);
			this.objPropList.Name = "objPropList";
			this.objPropList.ScrollAlwaysVisible = true;
			this.objPropList.Size = new System.Drawing.Size(160, 134);
			this.objPropList.TabIndex = 3;
			this.objPropList.SelectedIndexChanged += new System.EventHandler(this.objPropList_SelectedIndexChanged);
			// 
			// objEditButton
			// 
			this.objEditButton.Location = new System.Drawing.Point(160, 16);
			this.objEditButton.Name = "objEditButton";
			this.objEditButton.Size = new System.Drawing.Size(48, 24);
			this.objEditButton.TabIndex = 2;
			this.objEditButton.Text = "Edit";
			// 
			// objDeleteButton
			// 
			this.objDeleteButton.Location = new System.Drawing.Point(104, 16);
			this.objDeleteButton.Name = "objDeleteButton";
			this.objDeleteButton.Size = new System.Drawing.Size(48, 24);
			this.objDeleteButton.TabIndex = 1;
			this.objDeleteButton.Text = "Delete";
			// 
			// objLoadButton
			// 
			this.objLoadButton.Location = new System.Drawing.Point(48, 16);
			this.objLoadButton.Name = "objLoadButton";
			this.objLoadButton.Size = new System.Drawing.Size(48, 24);
			this.objLoadButton.TabIndex = 0;
			this.objLoadButton.Text = "Add";
			this.objLoadButton.Click += new System.EventHandler(this.objLoadButton_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.objTextLabel);
			this.groupBox2.Controls.Add(this.objImagePanel);
			this.groupBox2.Location = new System.Drawing.Point(8, 224);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(248, 112);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Properties";
			// 
			// objTextLabel
			// 
			this.objTextLabel.Location = new System.Drawing.Point(120, 16);
			this.objTextLabel.Name = "objTextLabel";
			this.objTextLabel.Size = new System.Drawing.Size(120, 88);
			this.objTextLabel.TabIndex = 1;
			// 
			// objImagePanel
			// 
			this.objImagePanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.objImagePanel.Location = new System.Drawing.Point(16, 16);
			this.objImagePanel.Name = "objImagePanel";
			this.objImagePanel.Size = new System.Drawing.Size(88, 88);
			this.objImagePanel.TabIndex = 0;
			this.objImagePanel.Paint += new System.Windows.Forms.PaintEventHandler(this.objImagePanel_Paint);
			// 
			// frmProps
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(266, 359);
			this.ControlBox = false;
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Location = new System.Drawing.Point(880, 100);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmProps";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Entity Toolbox";
			this.TopMost = true;
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void objLoadButton_Click(object sender, System.EventArgs e)
		{
			string sTemp = Directory.GetCurrentDirectory();
			if(mOpenFileDialog.ShowDialog()== DialogResult.OK)
			{
				cPropData PData = new cPropData(mOpenFileDialog.FileName);
				mOpenFileDialog.InitialDirectory = Path.GetDirectoryName(mOpenFileDialog.FileName);
				
				//Fix this somehow.
				Directory.SetCurrentDirectory(sTemp);
				if(PData.LoadData())
				{
					AddProp(PData);
				}
			}
		}
		
		
		public void AddProp(cPropData aPData)
		{
			mlstPropData.Add(aPData);
			objPropList.Items.Add(aPData.msName);
				
			if(objPropList.Items.Count==1)
			{
				objPropList.SelectedIndex =0;
				mlSelectedProp =0;
			}
		}

		private void UpdateInfo()
		{
			if(objPropList.Items.Count<0 || mlSelectedProp<0)
			{

			}
			else
			{
				cPropData PData = (cPropData)mlstPropData[mlSelectedProp];
				
				objTextLabel.Text = 
				"Type: "+PData.msType+"\nSubType:"+PData.msSubType+"\n"+
				"Material: "+PData.msMaterialType+ "\nMesh: "+PData.msMeshType+"\n";
			}
		
			objImagePanel.Refresh();
			
		}

		private void objImagePanel_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			if(objPropList.Items.Count<0 || mlSelectedProp<0)return;

			cPropData PData = (cPropData)mlstPropData[mlSelectedProp];
			
			e.Graphics.DrawImage(PData.mImage,0,0,objImagePanel.Width,objImagePanel.Height);
        }

		private void objPropList_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			mlSelectedProp = objPropList.SelectedIndex;
			UpdateInfo();
		}

		public void ResetData()
		{
			mlstPropData.Clear();
			mlstProps.Clear();
			objPropList.SelectedIndex=-1;
			objPropList.Items.Clear();
			mlSelectedProp=-1;
		}

		public void RefreshData()
		{
			UpdateInfo();
		}

		public cPropData GetCurrentData()
		{
			return (cPropData)mlstPropData[mlSelectedProp];
		}

		public cPropData GetData(string asName)
		{
			foreach(cPropData PData in mlstPropData)
			{
				if(asName == PData.msName)
				{
					return PData;
				}
			}

			return null;
		}
	}
}
