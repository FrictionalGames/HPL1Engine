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
	public class frmPropertiesProp : System.Windows.Forms.Form
	{
		public bool mbOkWasPressed=false; 

        private System.Windows.Forms.Label objNameLabel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		public System.Windows.Forms.Button objOkButton;
		public System.Windows.Forms.Button objCancelButtom;
		public System.Windows.Forms.TextBox objNameText;
		private System.Windows.Forms.ColorDialog ColorPicker;
		private System.Windows.Forms.Label label3;
		public System.Windows.Forms.TextBox objZText;
		private System.Windows.Forms.Label label6;
		public System.Windows.Forms.ComboBox objActiveBox;
		public System.Windows.Forms.TextBox objWidthText;
		public System.Windows.Forms.TextBox objHeightText;
		public System.Windows.Forms.TextBox objAngleText;
		private System.Windows.Forms.Label label4;
		public System.Windows.Forms.ComboBox objFlipHBox;
		private System.Windows.Forms.Label label5;
		public System.Windows.Forms.ComboBox objFlipVBox;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		public System.Windows.Forms.ComboBox objAnimBox;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmPropertiesProp(cProp aProp)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			objWidthText.Text = aProp.mfWidth.ToString();
			objHeightText.Text = aProp.mfHeight.ToString();
			objAngleText.Text = aProp.mfAngle.ToString();
			objZText.Text = aProp.mfZ.ToString();
			objFlipHBox.SelectedIndex = aProp.mbFlipH?1:0;
			objFlipVBox.SelectedIndex = aProp.mbFlipV?1:0;
			
			objNameText.Text = aProp.msName;
			objActiveBox.SelectedIndex = aProp.mbActive?1:0;

			objAnimBox.Items.Clear();
			if(aProp.mEntityData.mlFrames<0)
			{
				objAnimBox.Items.Add("0: Default");
				objAnimBox.SelectedIndex =0;
			}
			else
			{
				for(int i=0;i<aProp.mEntityData.mlstAnimations.Count;i++)
				{
					cEntityAnimation Anim = (cEntityAnimation)aProp.mEntityData.mlstAnimations[i];

					objAnimBox.Items.Add(i+": "+ Anim.msName);
				}
				objAnimBox.SelectedIndex = aProp.mlAnimNum;
			}
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
			this.objOkButton = new System.Windows.Forms.Button();
			this.objCancelButtom = new System.Windows.Forms.Button();
			this.ColorPicker = new System.Windows.Forms.ColorDialog();
			this.label3 = new System.Windows.Forms.Label();
			this.objZText = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.objActiveBox = new System.Windows.Forms.ComboBox();
			this.objWidthText = new System.Windows.Forms.TextBox();
			this.objHeightText = new System.Windows.Forms.TextBox();
			this.objAngleText = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.objFlipHBox = new System.Windows.Forms.ComboBox();
			this.label5 = new System.Windows.Forms.Label();
			this.objFlipVBox = new System.Windows.Forms.ComboBox();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.objAnimBox = new System.Windows.Forms.ComboBox();
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
			this.label1.Text = "Width:";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(16, 112);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(48, 16);
			this.label2.TabIndex = 2;
			this.label2.Text = "Height:";
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
			// objOkButton
			// 
			this.objOkButton.Location = new System.Drawing.Point(32, 328);
			this.objOkButton.Name = "objOkButton";
			this.objOkButton.Size = new System.Drawing.Size(72, 24);
			this.objOkButton.TabIndex = 7;
			this.objOkButton.Text = "OK";
			this.objOkButton.Click += new System.EventHandler(this.objOkButton_Click);
			// 
			// objCancelButtom
			// 
			this.objCancelButtom.Location = new System.Drawing.Point(120, 328);
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
			this.label3.Location = new System.Drawing.Point(16, 144);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(48, 16);
			this.label3.TabIndex = 9;
			this.label3.Text = "Z:";
			// 
			// objZText
			// 
			this.objZText.Location = new System.Drawing.Point(104, 144);
			this.objZText.MaxLength = 6;
			this.objZText.Name = "objZText";
			this.objZText.Size = new System.Drawing.Size(104, 20);
			this.objZText.TabIndex = 10;
			this.objZText.Text = "";
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
			// objWidthText
			// 
			this.objWidthText.Location = new System.Drawing.Point(104, 80);
			this.objWidthText.MaxLength = 8;
			this.objWidthText.Name = "objWidthText";
			this.objWidthText.Size = new System.Drawing.Size(104, 20);
			this.objWidthText.TabIndex = 4;
			this.objWidthText.Text = "";
			// 
			// objHeightText
			// 
			this.objHeightText.Location = new System.Drawing.Point(104, 112);
			this.objHeightText.MaxLength = 8;
			this.objHeightText.Name = "objHeightText";
			this.objHeightText.Size = new System.Drawing.Size(104, 20);
			this.objHeightText.TabIndex = 17;
			this.objHeightText.Text = "";
			// 
			// objAngleText
			// 
			this.objAngleText.Location = new System.Drawing.Point(104, 176);
			this.objAngleText.MaxLength = 8;
			this.objAngleText.Name = "objAngleText";
			this.objAngleText.Size = new System.Drawing.Size(104, 20);
			this.objAngleText.TabIndex = 18;
			this.objAngleText.Text = "";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(16, 176);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(48, 16);
			this.label4.TabIndex = 19;
			this.label4.Text = "Angle:";
			// 
			// objFlipHBox
			// 
			this.objFlipHBox.Items.AddRange(new object[] {
															 "False",
															 "True"});
			this.objFlipHBox.Location = new System.Drawing.Point(104, 208);
			this.objFlipHBox.Name = "objFlipHBox";
			this.objFlipHBox.Size = new System.Drawing.Size(104, 21);
			this.objFlipHBox.TabIndex = 21;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(16, 208);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(80, 16);
			this.label5.TabIndex = 20;
			this.label5.Text = "Flip Horisontal:";
			// 
			// objFlipVBox
			// 
			this.objFlipVBox.Items.AddRange(new object[] {
															 "False",
															 "True"});
			this.objFlipVBox.Location = new System.Drawing.Point(104, 240);
			this.objFlipVBox.Name = "objFlipVBox";
			this.objFlipVBox.Size = new System.Drawing.Size(104, 21);
			this.objFlipVBox.TabIndex = 23;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(16, 240);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(72, 16);
			this.label7.TabIndex = 22;
			this.label7.Text = "Flip Vertical:";
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(16, 272);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(80, 24);
			this.label8.TabIndex = 24;
			this.label8.Text = "Animation:";
			// 
			// objAnimBox
			// 
			this.objAnimBox.Location = new System.Drawing.Point(104, 272);
			this.objAnimBox.Name = "objAnimBox";
			this.objAnimBox.Size = new System.Drawing.Size(104, 21);
			this.objAnimBox.TabIndex = 25;
			// 
			// frmPropertiesProp
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(232, 357);
			this.ControlBox = false;
			this.Controls.Add(this.objAnimBox);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.objFlipVBox);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.objFlipHBox);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.objAngleText);
			this.Controls.Add(this.objHeightText);
			this.Controls.Add(this.objZText);
			this.Controls.Add(this.objWidthText);
			this.Controls.Add(this.objNameText);
			this.Controls.Add(this.objActiveBox);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.objCancelButtom);
			this.Controls.Add(this.objOkButton);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.objNameLabel);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmPropertiesProp";
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
	}
}
