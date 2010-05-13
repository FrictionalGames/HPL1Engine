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
	public class frmPropertiesArea : System.Windows.Forms.Form
	{
		public bool mbOkWasPressed=false;
		cArea mArea;

        private System.Windows.Forms.Label objNameLabel;
		private System.Windows.Forms.Label label1;
		public System.Windows.Forms.Button objOkButton;
		public System.Windows.Forms.Button objCancelButtom;
		public System.Windows.Forms.TextBox objNameText;
		private System.Windows.Forms.Label label6;
		public System.Windows.Forms.ComboBox objActiveBox;
		public System.Windows.Forms.TextBox objXText;
		private System.Windows.Forms.Label objXLabel;
		private System.Windows.Forms.Label objYLabel;
		private System.Windows.Forms.Label label3;
		public System.Windows.Forms.TextBox objZText;
		private System.Windows.Forms.Label label4;
		public System.Windows.Forms.ComboBox objTypeBox;
		private System.Windows.Forms.Label label5;
		public System.Windows.Forms.TextBox objYText;
		private System.Windows.Forms.Label objZLabel;
		public System.Windows.Forms.TextBox objWidthText;
		private System.Windows.Forms.Label label7;
		public System.Windows.Forms.TextBox objHeightText;
		private System.Windows.Forms.Label label8;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmPropertiesArea(cArea aArea)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//

			mArea = aArea;

			objNameText.Text = aArea.msName;
			objActiveBox.SelectedIndex = aArea.mbActive?1:0;
			
			objHeightText.Text = aArea.mfHeight.ToString();
			objWidthText.Text = aArea.mfWidth.ToString();
			
			objXLabel.Text = ((cAreaType)aArea.mAForm.mlstTypes[aArea.mlTypeNum]).msDesc[0];
			objXText.Text = aArea.mfSizeX.ToString();
			
			objYLabel.Text = ((cAreaType)aArea.mAForm.mlstTypes[aArea.mlTypeNum]).msDesc[1];
			objYText.Text = aArea.mfSizeY.ToString();
			
			objZLabel.Text = ((cAreaType)aArea.mAForm.mlstTypes[aArea.mlTypeNum]).msDesc[2];
			objZText.Text = aArea.mfSizeZ.ToString();
			
			foreach(string sN in aArea.mAForm.objTypeList.Items)
			{
				objTypeBox.Items.Add(sN);
			}

			objTypeBox.SelectedIndex = aArea.mlTypeNum;
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
			this.objNameText = new System.Windows.Forms.TextBox();
			this.objXText = new System.Windows.Forms.TextBox();
			this.objOkButton = new System.Windows.Forms.Button();
			this.objCancelButtom = new System.Windows.Forms.Button();
			this.objXLabel = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.objActiveBox = new System.Windows.Forms.ComboBox();
			this.objYLabel = new System.Windows.Forms.Label();
			this.objYText = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.objZLabel = new System.Windows.Forms.Label();
			this.objZText = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.objTypeBox = new System.Windows.Forms.ComboBox();
			this.label5 = new System.Windows.Forms.Label();
			this.objWidthText = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.objHeightText = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
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
			this.label1.Location = new System.Drawing.Point(16, 200);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(48, 16);
			this.label1.TabIndex = 1;
			this.label1.Text = "Var X:";
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
			// objXText
			// 
			this.objXText.Location = new System.Drawing.Point(104, 192);
			this.objXText.MaxLength = 40;
			this.objXText.Name = "objXText";
			this.objXText.Size = new System.Drawing.Size(104, 20);
			this.objXText.TabIndex = 4;
			this.objXText.Text = "";
			// 
			// objOkButton
			// 
			this.objOkButton.Location = new System.Drawing.Point(24, 432);
			this.objOkButton.Name = "objOkButton";
			this.objOkButton.Size = new System.Drawing.Size(72, 24);
			this.objOkButton.TabIndex = 7;
			this.objOkButton.Text = "OK";
			this.objOkButton.Click += new System.EventHandler(this.objOkButton_Click);
			// 
			// objCancelButtom
			// 
			this.objCancelButtom.Location = new System.Drawing.Point(120, 432);
			this.objCancelButtom.Name = "objCancelButtom";
			this.objCancelButtom.Size = new System.Drawing.Size(72, 24);
			this.objCancelButtom.TabIndex = 8;
			this.objCancelButtom.Text = "Cancel";
			this.objCancelButtom.Click += new System.EventHandler(this.objCancelButtom_Click);
			// 
			// objXLabel
			// 
			this.objXLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.objXLabel.Location = new System.Drawing.Point(16, 160);
			this.objXLabel.Name = "objXLabel";
			this.objXLabel.Size = new System.Drawing.Size(200, 32);
			this.objXLabel.TabIndex = 12;
			this.objXLabel.Text = "Description...";
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
			// objYLabel
			// 
			this.objYLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.objYLabel.Location = new System.Drawing.Point(16, 224);
			this.objYLabel.Name = "objYLabel";
			this.objYLabel.Size = new System.Drawing.Size(200, 32);
			this.objYLabel.TabIndex = 19;
			this.objYLabel.Text = "Description...";
			// 
			// objYText
			// 
			this.objYText.Location = new System.Drawing.Point(104, 256);
			this.objYText.MaxLength = 40;
			this.objYText.Name = "objYText";
			this.objYText.Size = new System.Drawing.Size(104, 20);
			this.objYText.TabIndex = 18;
			this.objYText.Text = "";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(16, 264);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(48, 16);
			this.label3.TabIndex = 17;
			this.label3.Text = "Var Y:";
			// 
			// objZLabel
			// 
			this.objZLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.objZLabel.Location = new System.Drawing.Point(16, 288);
			this.objZLabel.Name = "objZLabel";
			this.objZLabel.Size = new System.Drawing.Size(200, 32);
			this.objZLabel.TabIndex = 22;
			this.objZLabel.Text = "Description...";
			// 
			// objZText
			// 
			this.objZText.Location = new System.Drawing.Point(104, 320);
			this.objZText.MaxLength = 40;
			this.objZText.Name = "objZText";
			this.objZText.Size = new System.Drawing.Size(104, 20);
			this.objZText.TabIndex = 21;
			this.objZText.Text = "";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(16, 328);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(48, 16);
			this.label4.TabIndex = 20;
			this.label4.Text = "Var Z:";
			// 
			// objTypeBox
			// 
			this.objTypeBox.Location = new System.Drawing.Point(104, 368);
			this.objTypeBox.Name = "objTypeBox";
			this.objTypeBox.Size = new System.Drawing.Size(104, 21);
			this.objTypeBox.TabIndex = 24;
			this.objTypeBox.SelectedIndexChanged += new System.EventHandler(this.objTypeBox_SelectedIndexChanged);
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(16, 368);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(48, 16);
			this.label5.TabIndex = 23;
			this.label5.Text = "Type:";
			this.label5.Click += new System.EventHandler(this.label5_Click);
			// 
			// objWidthText
			// 
			this.objWidthText.Location = new System.Drawing.Point(104, 80);
			this.objWidthText.MaxLength = 40;
			this.objWidthText.Name = "objWidthText";
			this.objWidthText.Size = new System.Drawing.Size(104, 20);
			this.objWidthText.TabIndex = 28;
			this.objWidthText.Text = "";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(16, 80);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(48, 16);
			this.label7.TabIndex = 27;
			this.label7.Text = "Width:";
			// 
			// objHeightText
			// 
			this.objHeightText.Location = new System.Drawing.Point(104, 112);
			this.objHeightText.MaxLength = 40;
			this.objHeightText.Name = "objHeightText";
			this.objHeightText.Size = new System.Drawing.Size(104, 20);
			this.objHeightText.TabIndex = 30;
			this.objHeightText.Text = "";
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(16, 112);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(48, 16);
			this.label8.TabIndex = 29;
			this.label8.Text = "Height:";
			// 
			// frmPropertiesArea
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(232, 461);
			this.ControlBox = false;
			this.Controls.Add(this.objHeightText);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.objWidthText);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.objTypeBox);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.objZLabel);
			this.Controls.Add(this.objZText);
			this.Controls.Add(this.objYText);
			this.Controls.Add(this.objXText);
			this.Controls.Add(this.objNameText);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.objYLabel);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.objActiveBox);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.objXLabel);
			this.Controls.Add(this.objCancelButtom);
			this.Controls.Add(this.objOkButton);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.objNameLabel);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmPropertiesArea";
			this.ShowInTaskbar = false;
			this.Text = "Edit Area";
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

		private void objTypeBox_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			objXLabel.Text = ((cAreaType)mArea.mAForm.mlstTypes[objTypeBox.SelectedIndex]).msDesc[0];
			objYLabel.Text = ((cAreaType)mArea.mAForm.mlstTypes[objTypeBox.SelectedIndex]).msDesc[1];
			objZLabel.Text = ((cAreaType)mArea.mAForm.mlstTypes[objTypeBox.SelectedIndex]).msDesc[2];
			
		}

		private void label5_Click(object sender, System.EventArgs e)
		{
		
		}


	}
}
