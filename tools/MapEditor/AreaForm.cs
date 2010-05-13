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
using System.Xml;

namespace Mapeditor
{
	/// <summary>
	/// Summary description for LightForm.
	/// </summary>
	public class frmAreas : System.Windows.Forms.Form
	{
		private frmMain mMainForm;

		public ArrayList mlstAreas;
		public ArrayList mlstTypes;
		public System.Windows.Forms.ComboBox objTypeList;
		private System.Windows.Forms.Label objTypeText;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		
		public frmAreas(frmMain aMainForm)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			
			mMainForm = aMainForm;
			
			mlstAreas = new ArrayList();
			mlstTypes = new ArrayList();
			
			XmlDocument Doc = new XmlDocument();
			try	{
				Doc.Load("MapEditor.cfg");
			}
			catch(XmlException e) {

				MessageBox.Show("Couldn't load 'MapEditor.cfg'!\n"+e.ToString(), "Error");
				return;
			}
            
			XmlElement CfgElem = (XmlElement)Doc.FirstChild;
			foreach(XmlElement CfgElemChild in CfgElem.ChildNodes)
			{
				if(CfgElemChild.Name == "Areas")
				{
					foreach(XmlElement PartElem in CfgElemChild.ChildNodes)
					{
						cAreaType AType = new cAreaType();
						AType.msName = cHplXml.GetStr(PartElem,"Name","");
						AType.msDesc[0] = cHplXml.GetStr(PartElem,"XDesc","");
						AType.msDesc[1] = cHplXml.GetStr(PartElem,"YDesc","");
						AType.msDesc[2] = cHplXml.GetStr(PartElem,"ZDesc","");

						objTypeList.Items.Add(AType.msName);
						mlstTypes.Add(AType);
						objTypeList.SelectedIndex =0;
					}
					break;
				}
			}	
		}

		public void ResetData()
		{
			mlstAreas.Clear();
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
			this.objTypeText.Text = "Area type:";
			// 
			// frmAreas
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(160, 229);
			this.ControlBox = false;
			this.Controls.Add(this.objTypeList);
			this.Controls.Add(this.objTypeText);
			this.Location = new System.Drawing.Point(900, 150);
			this.Name = "frmAreas";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Area Toolbox";
			this.TopMost = true;
			this.ResumeLayout(false);

		}
		#endregion
	}
}
