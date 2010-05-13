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
	/// Summary description for LightForm.
	/// </summary>
	public class frmLight : System.Windows.Forms.Form
	{
		private frmMain mMainForm;

		public ArrayList mlstLights;
		
		private System.Windows.Forms.ComboBox objTypeList;
		private System.Windows.Forms.Label objTypeText;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		
		public frmLight(frmMain aMainForm)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			
			mMainForm = aMainForm;
			objTypeList.SelectedIndex =0;

			mlstLights = new ArrayList();
		}

		public void ResetData()
		{
			mlstLights.Clear();
		}

		public void RefreshData()
		{
			
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
			this.objTypeList = new System.Windows.Forms.ComboBox();
			this.objTypeText = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// objTypeList
			// 
			this.objTypeList.Items.AddRange(new object[] {
															 "PointLight"});
			this.objTypeList.Location = new System.Drawing.Point(8, 32);
			this.objTypeList.Name = "objTypeList";
			this.objTypeList.Size = new System.Drawing.Size(144, 21);
			this.objTypeList.TabIndex = 0;
			// 
			// objTypeText
			// 
			this.objTypeText.Location = new System.Drawing.Point(8, 16);
			this.objTypeText.Name = "objTypeText";
			this.objTypeText.Size = new System.Drawing.Size(112, 16);
			this.objTypeText.TabIndex = 1;
			this.objTypeText.Text = "Light type:";
			// 
			// frmLight
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(160, 229);
			this.ControlBox = false;
			this.Controls.Add(this.objTypeList);
			this.Controls.Add(this.objTypeText);
			this.Location = new System.Drawing.Point(900, 150);
			this.Name = "frmLight";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Light Toolbox";
			this.TopMost = true;
			this.ResumeLayout(false);

		}
		#endregion
	}
}
