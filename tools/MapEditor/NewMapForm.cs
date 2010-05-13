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
	/// Summary description for NewMapForm.
	/// </summary>
	public class frmNewMap : System.Windows.Forms.Form
	{
		public bool mbOKPressed;
		public string msName="";
		public int mlWidth=0;
		public int mlHeight=0;
		public int mlTileSize=0;
		public bool mbCreateLayers;
		
		private System.Windows.Forms.Button objOKButton;
		private System.Windows.Forms.Button objCancelButton;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.CheckBox objLayersCheckbox;
		private System.Windows.Forms.TextBox objNameText;
		private System.Windows.Forms.TextBox objWidthText;
		private System.Windows.Forms.TextBox objHeightText;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox objTileSizeText;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmNewMap()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			mbOKPressed = false;
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
			this.objOKButton = new System.Windows.Forms.Button();
			this.objCancelButton = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.objLayersCheckbox = new System.Windows.Forms.CheckBox();
			this.objNameText = new System.Windows.Forms.TextBox();
			this.objWidthText = new System.Windows.Forms.TextBox();
			this.objHeightText = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.objTileSizeText = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// objOKButton
			// 
			this.objOKButton.Location = new System.Drawing.Point(184, 128);
			this.objOKButton.Name = "objOKButton";
			this.objOKButton.Size = new System.Drawing.Size(72, 24);
			this.objOKButton.TabIndex = 0;
			this.objOKButton.Text = "OK";
			this.objOKButton.Click += new System.EventHandler(this.objOKButton_Click);
			// 
			// objCancelButton
			// 
			this.objCancelButton.Location = new System.Drawing.Point(272, 128);
			this.objCancelButton.Name = "objCancelButton";
			this.objCancelButton.Size = new System.Drawing.Size(72, 24);
			this.objCancelButton.TabIndex = 1;
			this.objCancelButton.Text = "Cancel";
			this.objCancelButton.Click += new System.EventHandler(this.objCancelButton_Click);
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(24, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(56, 16);
			this.label1.TabIndex = 2;
			this.label1.Text = "Name:";
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label2.Location = new System.Drawing.Point(192, 40);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(56, 16);
			this.label2.TabIndex = 3;
			this.label2.Text = "Height";
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label3.Location = new System.Drawing.Point(24, 40);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(40, 16);
			this.label3.TabIndex = 4;
			this.label3.Text = "Width";
			// 
			// objLayersCheckbox
			// 
			this.objLayersCheckbox.Checked = true;
			this.objLayersCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.objLayersCheckbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.objLayersCheckbox.Location = new System.Drawing.Point(24, 96);
			this.objLayersCheckbox.Name = "objLayersCheckbox";
			this.objLayersCheckbox.Size = new System.Drawing.Size(152, 24);
			this.objLayersCheckbox.TabIndex = 5;
			this.objLayersCheckbox.Text = "Create default layers";
			// 
			// objNameText
			// 
			this.objNameText.Location = new System.Drawing.Point(80, 16);
			this.objNameText.Name = "objNameText";
			this.objNameText.Size = new System.Drawing.Size(256, 20);
			this.objNameText.TabIndex = 6;
			this.objNameText.Text = "Untitled-1";
			// 
			// objWidthText
			// 
			this.objWidthText.Location = new System.Drawing.Point(80, 40);
			this.objWidthText.Name = "objWidthText";
			this.objWidthText.Size = new System.Drawing.Size(80, 20);
			this.objWidthText.TabIndex = 7;
			this.objWidthText.Text = "20";
			// 
			// objHeightText
			// 
			this.objHeightText.Location = new System.Drawing.Point(248, 40);
			this.objHeightText.Name = "objHeightText";
			this.objHeightText.Size = new System.Drawing.Size(88, 20);
			this.objHeightText.TabIndex = 8;
			this.objHeightText.Text = "20";
			// 
			// label4
			// 
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label4.Location = new System.Drawing.Point(24, 72);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(104, 16);
			this.label4.TabIndex = 9;
			this.label4.Text = "Defualt TileSize:";
			// 
			// objTileSizeText
			// 
			this.objTileSizeText.Location = new System.Drawing.Point(128, 72);
			this.objTileSizeText.MaxLength = 3;
			this.objTileSizeText.Name = "objTileSizeText";
			this.objTileSizeText.Size = new System.Drawing.Size(32, 20);
			this.objTileSizeText.TabIndex = 10;
			this.objTileSizeText.Text = "40";
			// 
			// frmNewMap
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(360, 165);
			this.ControlBox = false;
			this.Controls.Add(this.objTileSizeText);
			this.Controls.Add(this.objHeightText);
			this.Controls.Add(this.objWidthText);
			this.Controls.Add(this.objNameText);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.objLayersCheckbox);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.objCancelButton);
			this.Controls.Add(this.objOKButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmNewMap";
			this.ShowInTaskbar = false;
			this.Text = "New Map";
			this.ResumeLayout(false);

		}
		#endregion

		private void objOKButton_Click(object sender, System.EventArgs e)
		{
			mbOKPressed = true;

			msName = (string) objNameText.Text.Clone();
			mlWidth = Convert.ToInt32(objWidthText.Text);
			mlHeight= Convert.ToInt32(objHeightText.Text);
			mlTileSize = Convert.ToInt32(objTileSizeText.Text);

			mbCreateLayers = objLayersCheckbox.CheckState==CheckState.Checked;
			
			this.Close();
		}

		private void objCancelButton_Click(object sender, System.EventArgs e)
		{
			mbOKPressed = false;
            this.Close();
		}
	}
}
