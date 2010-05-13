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
	/// Summary description for PropertiesLightForm.
	/// </summary>
	public class frmPropertiesLight : System.Windows.Forms.Form
	{
		public bool mbOkWasPressed=false; 

        private System.Windows.Forms.Label objNameLabel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		public System.Windows.Forms.TextBox objRadiusText;
		public System.Windows.Forms.Panel objColorPanel;
		public System.Windows.Forms.Button objColorButton;
		public System.Windows.Forms.Button objOkButton;
		public System.Windows.Forms.Button objCancelButtom;
		public System.Windows.Forms.TextBox objNameText;
		private System.Windows.Forms.ColorDialog ColorPicker;
		private System.Windows.Forms.Label label3;
		public System.Windows.Forms.TextBox objZText;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		public System.Windows.Forms.ComboBox objShadowBox;
		public System.Windows.Forms.ComboBox objMaterialBox;
		private System.Windows.Forms.Label label6;
		public System.Windows.Forms.ComboBox objActiveBox;
		public System.Windows.Forms.TextBox objSpecularText;
		private System.Windows.Forms.Label label7;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmPropertiesLight(cLight aLight)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			objColorPanel.BackColor = aLight.mColor;
			objRadiusText.Text = aLight.mfRadius.ToString();
			objSpecularText.Text = aLight.mfSpecular.ToString();
			objNameText.Text = aLight.msName;
			objZText.Text = aLight.mfZ.ToString();
			objActiveBox.SelectedIndex = aLight.mbActive?1:0;
			objShadowBox.SelectedIndex = aLight.mbCastShadows?1:0;
			objMaterialBox.SelectedIndex = aLight.mbAffectMaterial?1:0;
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
			this.objNameLabel = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.objNameText = new System.Windows.Forms.TextBox();
			this.objRadiusText = new System.Windows.Forms.TextBox();
			this.objColorPanel = new System.Windows.Forms.Panel();
			this.objColorButton = new System.Windows.Forms.Button();
			this.objOkButton = new System.Windows.Forms.Button();
			this.objCancelButtom = new System.Windows.Forms.Button();
			this.ColorPicker = new System.Windows.Forms.ColorDialog();
			this.label3 = new System.Windows.Forms.Label();
			this.objZText = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.objShadowBox = new System.Windows.Forms.ComboBox();
			this.objMaterialBox = new System.Windows.Forms.ComboBox();
			this.label6 = new System.Windows.Forms.Label();
			this.objActiveBox = new System.Windows.Forms.ComboBox();
			this.objSpecularText = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// objNameLabel
			// 
			this.objNameLabel.Location = new System.Drawing.Point(16, 16);
			this.objNameLabel.Name = "objNameLabel";
			this.objNameLabel.Size = new System.Drawing.Size(64, 16);
			this.objNameLabel.TabIndex = 0;
			this.objNameLabel.Text = "Name:";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 80);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(48, 16);
			this.label1.TabIndex = 1;
			this.label1.Text = "Radius:";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(16, 112);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(48, 16);
			this.label2.TabIndex = 2;
			this.label2.Text = "Color:";
			// 
			// objNameText
			// 
			this.objNameText.Location = new System.Drawing.Point(104, 16);
			this.objNameText.MaxLength = 40;
			this.objNameText.Name = "objNameText";
			this.objNameText.Size = new System.Drawing.Size(104, 20);
			this.objNameText.TabIndex = 3;
			this.objNameText.Text = "";
			// 
			// objRadiusText
			// 
			this.objRadiusText.Location = new System.Drawing.Point(104, 80);
			this.objRadiusText.MaxLength = 40;
			this.objRadiusText.Name = "objRadiusText";
			this.objRadiusText.Size = new System.Drawing.Size(104, 20);
			this.objRadiusText.TabIndex = 4;
			this.objRadiusText.Text = "";
			// 
			// objColorPanel
			// 
			this.objColorPanel.BackColor = System.Drawing.SystemColors.HighlightText;
			this.objColorPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.objColorPanel.Location = new System.Drawing.Point(104, 112);
			this.objColorPanel.Name = "objColorPanel";
			this.objColorPanel.Size = new System.Drawing.Size(48, 24);
			this.objColorPanel.TabIndex = 5;
			// 
			// objColorButton
			// 
			this.objColorButton.Location = new System.Drawing.Point(160, 112);
			this.objColorButton.Name = "objColorButton";
			this.objColorButton.Size = new System.Drawing.Size(48, 23);
			this.objColorButton.TabIndex = 6;
			this.objColorButton.Text = "Pick";
			this.objColorButton.Click += new System.EventHandler(this.objColorButton_Click);
			// 
			// objOkButton
			// 
			this.objOkButton.Location = new System.Drawing.Point(32, 288);
			this.objOkButton.Name = "objOkButton";
			this.objOkButton.Size = new System.Drawing.Size(72, 24);
			this.objOkButton.TabIndex = 7;
			this.objOkButton.Text = "OK";
			this.objOkButton.Click += new System.EventHandler(this.objOkButton_Click);
			// 
			// objCancelButtom
			// 
			this.objCancelButtom.Location = new System.Drawing.Point(120, 288);
			this.objCancelButtom.Name = "objCancelButtom";
			this.objCancelButtom.Size = new System.Drawing.Size(72, 24);
			this.objCancelButtom.TabIndex = 8;
			this.objCancelButtom.Text = "Cancel";
			this.objCancelButtom.Click += new System.EventHandler(this.objCancelButtom_Click);
			// 
			// ColorPicker
			// 
			this.ColorPicker.Color = System.Drawing.Color.White;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(16, 176);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(48, 16);
			this.label3.TabIndex = 9;
			this.label3.Text = "Z:";
			// 
			// objZText
			// 
			this.objZText.Location = new System.Drawing.Point(104, 176);
			this.objZText.MaxLength = 6;
			this.objZText.Name = "objZText";
			this.objZText.Size = new System.Drawing.Size(104, 20);
			this.objZText.TabIndex = 10;
			this.objZText.Text = "";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(16, 208);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(80, 16);
			this.label4.TabIndex = 11;
			this.label4.Text = "Cast Shadows:";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(16, 240);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(80, 16);
			this.label5.TabIndex = 12;
			this.label5.Text = "Affect Material:";
			// 
			// objShadowBox
			// 
			this.objShadowBox.Items.AddRange(new object[] {
															  "False",
															  "True"});
			this.objShadowBox.Location = new System.Drawing.Point(104, 208);
			this.objShadowBox.Name = "objShadowBox";
			this.objShadowBox.Size = new System.Drawing.Size(104, 21);
			this.objShadowBox.TabIndex = 13;
			// 
			// objMaterialBox
			// 
			this.objMaterialBox.Items.AddRange(new object[] {
																"False",
																"True"});
			this.objMaterialBox.Location = new System.Drawing.Point(104, 240);
			this.objMaterialBox.Name = "objMaterialBox";
			this.objMaterialBox.Size = new System.Drawing.Size(104, 21);
			this.objMaterialBox.TabIndex = 14;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(16, 48);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(48, 16);
			this.label6.TabIndex = 15;
			this.label6.Text = "Active:";
			// 
			// objActiveBox
			// 
			this.objActiveBox.Items.AddRange(new object[] {
															  "False",
															  "True"});
			this.objActiveBox.Location = new System.Drawing.Point(104, 48);
			this.objActiveBox.Name = "objActiveBox";
			this.objActiveBox.Size = new System.Drawing.Size(104, 21);
			this.objActiveBox.TabIndex = 16;
			// 
			// objSpecularText
			// 
			this.objSpecularText.Location = new System.Drawing.Point(104, 144);
			this.objSpecularText.MaxLength = 6;
			this.objSpecularText.Name = "objSpecularText";
			this.objSpecularText.Size = new System.Drawing.Size(104, 20);
			this.objSpecularText.TabIndex = 18;
			this.objSpecularText.Text = "";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(16, 144);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(64, 16);
			this.label7.TabIndex = 17;
			this.label7.Text = "Specular:";
			// 
			// frmPropertiesLight
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(232, 317);
			this.ControlBox = false;
			this.Controls.Add(this.objSpecularText);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.objActiveBox);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.objMaterialBox);
			this.Controls.Add(this.objShadowBox);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.objZText);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.objCancelButtom);
			this.Controls.Add(this.objOkButton);
			this.Controls.Add(this.objColorButton);
			this.Controls.Add(this.objColorPanel);
			this.Controls.Add(this.objRadiusText);
			this.Controls.Add(this.objNameText);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.objNameLabel);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmPropertiesLight";
			this.ShowInTaskbar = false;
			this.Text = "Edit Light";
			this.TopMost = true;
			this.ResumeLayout(false);

		}
		#endregion

		private void objOkButton_Click(object sender, System.EventArgs e)
		{
			mbOkWasPressed=true;
			this.Close();
		}

		private void objCancelButtom_Click(object sender, System.EventArgs e)
		{
			mbOkWasPressed=false;
			this.Close();
		}

		private void objColorButton_Click(object sender, System.EventArgs e)
		{
			if(ColorPicker.ShowDialog() == DialogResult.OK)
			{
				objColorPanel.BackColor = ColorPicker.Color;
			}

		}


	}
}
