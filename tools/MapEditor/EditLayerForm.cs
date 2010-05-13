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
	/// Summary description for EditLayerForm.
	/// </summary>
	public class frmEditLayer : System.Windows.Forms.Form
	{
		public bool mbOkWasPressed=false;
		public System.Windows.Forms.Button objOkButton;
		public System.Windows.Forms.Button objCancelButton;
		public System.Windows.Forms.Label label1;
		public System.Windows.Forms.Label label2;
		public System.Windows.Forms.Label label3;
		public System.Windows.Forms.Label label4;
		public System.Windows.Forms.TextBox objNameText;
		public System.Windows.Forms.TextBox objZText;
		public System.Windows.Forms.ComboBox objLitBox;
		public System.Windows.Forms.ComboBox objCollideBox;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmEditLayer()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.objNameText = new System.Windows.Forms.TextBox();
			this.objZText = new System.Windows.Forms.TextBox();
			this.objLitBox = new System.Windows.Forms.ComboBox();
			this.objCollideBox = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// objOkButton
			// 
			this.objOkButton.Location = new System.Drawing.Point(120, 152);
			this.objOkButton.Name = "objOkButton";
			this.objOkButton.Size = new System.Drawing.Size(80, 24);
			this.objOkButton.TabIndex = 0;
			this.objOkButton.Text = "OK";
			this.objOkButton.Click += new System.EventHandler(this.objOkButton_Click);
			// 
			// objCancelButton
			// 
			this.objCancelButton.Location = new System.Drawing.Point(216, 152);
			this.objCancelButton.Name = "objCancelButton";
			this.objCancelButton.Size = new System.Drawing.Size(80, 24);
			this.objCancelButton.TabIndex = 1;
			this.objCancelButton.Text = "Cancel";
			this.objCancelButton.Click += new System.EventHandler(this.objCancelButton_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(80, 16);
			this.label1.TabIndex = 2;
			this.label1.Text = "Name:";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(16, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(80, 16);
			this.label2.TabIndex = 3;
			this.label2.Text = "Z:";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(168, 80);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(48, 16);
			this.label3.TabIndex = 4;
			this.label3.Text = "Collide:";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(16, 80);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(80, 16);
			this.label4.TabIndex = 5;
			this.label4.Text = "Lit:";
			this.label4.Click += new System.EventHandler(this.label4_Click);
			// 
			// objNameText
			// 
			this.objNameText.Location = new System.Drawing.Point(64, 16);
			this.objNameText.MaxLength = 30;
			this.objNameText.Name = "objNameText";
			this.objNameText.Size = new System.Drawing.Size(232, 20);
			this.objNameText.TabIndex = 6;
			this.objNameText.Text = "";
			// 
			// objZText
			// 
			this.objZText.Location = new System.Drawing.Point(64, 48);
			this.objZText.MaxLength = 8;
			this.objZText.Name = "objZText";
			this.objZText.Size = new System.Drawing.Size(80, 20);
			this.objZText.TabIndex = 7;
			this.objZText.Text = "";
			// 
			// objLitBox
			// 
			this.objLitBox.Items.AddRange(new object[] {
														   "False",
														   "True"});
			this.objLitBox.Location = new System.Drawing.Point(64, 80);
			this.objLitBox.Name = "objLitBox";
			this.objLitBox.Size = new System.Drawing.Size(80, 21);
			this.objLitBox.TabIndex = 8;
			// 
			// objCollideBox
			// 
			this.objCollideBox.Items.AddRange(new object[] {
															   "False",
															   "True"});
			this.objCollideBox.Location = new System.Drawing.Point(216, 80);
			this.objCollideBox.Name = "objCollideBox";
			this.objCollideBox.Size = new System.Drawing.Size(80, 21);
			this.objCollideBox.TabIndex = 9;
			// 
			// frmEditLayer
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(312, 189);
			this.ControlBox = false;
			this.Controls.Add(this.objCollideBox);
			this.Controls.Add(this.objLitBox);
			this.Controls.Add(this.objZText);
			this.Controls.Add(this.objNameText);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.objCancelButton);
			this.Controls.Add(this.objOkButton);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmEditLayer";
			this.ShowInTaskbar = false;
			this.Text = "Layer";
			this.ResumeLayout(false);

		}
		#endregion

		private void objOkButton_Click(object sender, System.EventArgs e)
		{
			mbOkWasPressed=true;
			this.Close();
		}

		private void objCancelButton_Click(object sender, System.EventArgs e)
		{
			mbOkWasPressed=false;
			this.Close();
		}

		private void label4_Click(object sender, System.EventArgs e)
		{
		
		}
	}
}
