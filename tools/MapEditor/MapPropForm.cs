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
	/// Summary description for MapPropForm.
	/// </summary>
	public class frmMapProp : System.Windows.Forms.Form
	{
		public bool mbOkWasPressed=false;
		private frmMain mMainForm;

		private System.Windows.Forms.Button objOkButton;
		private System.Windows.Forms.Button objCancelButton;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox objNameText;
		private System.Windows.Forms.Label labelbajs;
		private System.Windows.Forms.Panel objAmbientPanel;
		private System.Windows.Forms.TextBox objZText;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ColorDialog ColorPicker;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		public System.Windows.Forms.TextBox objWidthText;
		public System.Windows.Forms.TextBox objHeightText;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmMapProp(frmMain aMainForm)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			mMainForm = aMainForm;

			objNameText.Text = mMainForm.msMapName;
			float fZ = mMainForm.mfLightZ;
			objZText.Text = fZ.ToString();
			objAmbientPanel.BackColor = mMainForm.mAmbientColor;
			int lW = mMainForm.mlMapWidth;
			int lH = mMainForm.mlMapHeight;
			objWidthText.Text = lW.ToString();
			objHeightText.Text = lH.ToString();
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
			this.objOkButton = new System.Windows.Forms.Button();
			this.objCancelButton = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.objNameText = new System.Windows.Forms.TextBox();
			this.labelbajs = new System.Windows.Forms.Label();
			this.objAmbientPanel = new System.Windows.Forms.Panel();
			this.objZText = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.ColorPicker = new System.Windows.Forms.ColorDialog();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.objWidthText = new System.Windows.Forms.TextBox();
			this.objHeightText = new System.Windows.Forms.TextBox();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// objOkButton
			// 
			this.objOkButton.Location = new System.Drawing.Point(224, 200);
			this.objOkButton.Name = "objOkButton";
			this.objOkButton.Size = new System.Drawing.Size(80, 24);
			this.objOkButton.TabIndex = 0;
			this.objOkButton.Text = "OK";
			this.objOkButton.Click += new System.EventHandler(this.objOkButton_Click);
			// 
			// objCancelButton
			// 
			this.objCancelButton.Location = new System.Drawing.Point(312, 200);
			this.objCancelButton.Name = "objCancelButton";
			this.objCancelButton.Size = new System.Drawing.Size(80, 24);
			this.objCancelButton.TabIndex = 1;
			this.objCancelButton.Text = "Cancel";
			this.objCancelButton.Click += new System.EventHandler(this.objCancelButton_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(48, 16);
			this.label1.TabIndex = 2;
			this.label1.Text = "Name:";
			// 
			// objNameText
			// 
			this.objNameText.Location = new System.Drawing.Point(72, 24);
			this.objNameText.Name = "objNameText";
			this.objNameText.Size = new System.Drawing.Size(256, 20);
			this.objNameText.TabIndex = 3;
			this.objNameText.Text = "";
			// 
			// labelbajs
			// 
			this.labelbajs.Location = new System.Drawing.Point(8, 56);
			this.labelbajs.Name = "labelbajs";
			this.labelbajs.Size = new System.Drawing.Size(64, 16);
			this.labelbajs.TabIndex = 4;
			this.labelbajs.Text = "Shadow Z:";
			// 
			// objAmbientPanel
			// 
			this.objAmbientPanel.BackColor = System.Drawing.SystemColors.WindowText;
			this.objAmbientPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.objAmbientPanel.Location = new System.Drawing.Point(272, 56);
			this.objAmbientPanel.Name = "objAmbientPanel";
			this.objAmbientPanel.Size = new System.Drawing.Size(56, 24);
			this.objAmbientPanel.TabIndex = 5;
			this.objAmbientPanel.DoubleClick += new System.EventHandler(this.objAmbientPanel_DoubleClick);
			// 
			// objZText
			// 
			this.objZText.Location = new System.Drawing.Point(72, 56);
			this.objZText.Name = "objZText";
			this.objZText.Size = new System.Drawing.Size(88, 20);
			this.objZText.TabIndex = 6;
			this.objZText.Text = "";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(192, 56);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(64, 24);
			this.label2.TabIndex = 7;
			this.label2.Text = "Ambient:";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.labelbajs);
			this.groupBox1.Controls.Add(this.objAmbientPanel);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.objZText);
			this.groupBox1.Controls.Add(this.objNameText);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Location = new System.Drawing.Point(24, 8);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(360, 88);
			this.groupBox1.TabIndex = 8;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Properties";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.objHeightText);
			this.groupBox2.Controls.Add(this.objWidthText);
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.Location = new System.Drawing.Point(24, 104);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(360, 80);
			this.groupBox2.TabIndex = 9;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Size";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(16, 24);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(48, 24);
			this.label3.TabIndex = 0;
			this.label3.Text = "Width:";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(184, 24);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(48, 24);
			this.label4.TabIndex = 1;
			this.label4.Text = "Height:";
			// 
			// objWidthText
			// 
			this.objWidthText.Location = new System.Drawing.Point(64, 24);
			this.objWidthText.MaxLength = 4;
			this.objWidthText.Name = "objWidthText";
			this.objWidthText.Size = new System.Drawing.Size(96, 20);
			this.objWidthText.TabIndex = 2;
			this.objWidthText.Text = "";
			// 
			// objHeightText
			// 
			this.objHeightText.Location = new System.Drawing.Point(232, 24);
			this.objHeightText.MaxLength = 4;
			this.objHeightText.Name = "objHeightText";
			this.objHeightText.Size = new System.Drawing.Size(96, 20);
			this.objHeightText.TabIndex = 3;
			this.objHeightText.Text = "";
			// 
			// frmMapProp
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(410, 231);
			this.ControlBox = false;
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.objCancelButton);
			this.Controls.Add(this.objOkButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmMapProp";
			this.Text = "Map Properties";
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void objAmbientPanel_DoubleClick(object sender, System.EventArgs e)
		{
			ColorPicker.Color = objAmbientPanel.BackColor;
			ColorPicker.ShowDialog();
			objAmbientPanel.BackColor = ColorPicker.Color;
		}

		private void objOkButton_Click(object sender, System.EventArgs e)
		{
			mbOkWasPressed = true;
			
			mMainForm.msMapName = objNameText.Text;
			mMainForm.mfLightZ = (float)Convert.ToDouble(objZText.Text);
			mMainForm.mAmbientColor = objAmbientPanel.BackColor;
			
			int lWidth = Convert.ToInt32(objWidthText.Text);
			int lHeight = Convert.ToInt32(objHeightText.Text);

			if(lWidth != mMainForm.mlMapWidth || lHeight != mMainForm.mlMapHeight)
			{
				mMainForm.mLayersForm.ResizeAll(lWidth,lHeight);

				//MessageBox.Show("New Size: "+lWidth+" : "+lHeight);

				mMainForm.mlMapWidth = lWidth;
				mMainForm.mlMapHeight = lHeight;
			}

			this.Close();
		}

		private void objCancelButton_Click(object sender, System.EventArgs e)
		{
			mbOkWasPressed = false;
			this.Close();
		}

		

	}
}
