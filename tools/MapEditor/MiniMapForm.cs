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
	/// Summary description for MiniMapForm.
	/// </summary>
	public class frmMiniMap : System.Windows.Forms.Form
	{
		private frmMain mMainForm;
		private Mapeditor.cTilePanel objMapPanel;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		
		public frmMiniMap(frmMain aMainForm)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			mMainForm = aMainForm;
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
			this.objMapPanel = new Mapeditor.cTilePanel();
			this.SuspendLayout();
			// 
			// objMapPanel
			// 
			this.objMapPanel.BackColor = System.Drawing.SystemColors.WindowText;
			this.objMapPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.objMapPanel.Location = new System.Drawing.Point(8, 8);
			this.objMapPanel.Name = "objMapPanel";
			this.objMapPanel.Size = new System.Drawing.Size(304, 256);
			this.objMapPanel.TabIndex = 0;
			this.objMapPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.objMapPanel_Paint);
			this.objMapPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.objMapPanel_MouseDown);
			// 
			// frmMiniMap
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(320, 269);
			this.ControlBox = false;
			this.Controls.Add(this.objMapPanel);
			this.MaximizeBox = false;
			this.Name = "frmMiniMap";
			this.ShowInTaskbar = false;
			this.Text = "MiniMapForm";
			this.TopMost = true;
			this.ResumeLayout(false);

		}
		#endregion
		
		private Bitmap mBackBuffer=null;
		private void objMapPanel_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			if(mBackBuffer==null)
			{
				mBackBuffer = new Bitmap(objMapPanel.Width, objMapPanel.Height);
			}
			
			Graphics Gfx = Graphics.FromImage(mBackBuffer);
			
			Gfx.Clear(Color.Black);

			if(mMainForm.mLayersForm.mlstTileLayers.Count<1)return;

			//Draw the layers
			mMainForm.mLayersForm.DrawAllLayersMini(Gfx,0,0, objMapPanel.Width/2,objMapPanel.Height/2);
			
			Gfx.DrawRectangle(new Pen(Color.White),
									(mMainForm.objHoriMapScroll.Value/mMainForm.mlDefaultTileSize)*2,
									(mMainForm.objVertMapScroll.Value/mMainForm.mlDefaultTileSize)*2,
									(mMainForm.objMapPanel.Width/mMainForm.mlDefaultTileSize)*2,
									(mMainForm.objMapPanel.Height/mMainForm.mlDefaultTileSize)*2);
			
			e.Graphics.DrawImageUnscaled(mBackBuffer,0,0);
		}

		private void objMapPanel_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			int lX = e.X*(mMainForm.mlDefaultTileSize/2)- mMainForm.objMapPanel.Width/2;
			int lY = e.Y*(mMainForm.mlDefaultTileSize/2) - mMainForm.objMapPanel.Height/2;
						
			if(lX<0)lX=0;
			if(lY<0)lY=0;
			if(lX >= mMainForm.objHoriMapScroll.Maximum)lX = mMainForm.objHoriMapScroll.Maximum;
			if(lY >= mMainForm.objVertMapScroll.Maximum)lY = mMainForm.objVertMapScroll.Maximum;
			mMainForm.objHoriMapScroll.Value = lX;
			mMainForm.objVertMapScroll.Value = lY; 
		}

		public void ResetData()
		{
			
		}

		public void RefreshData()
		{
			objMapPanel.Refresh();	
		}
	}
}
