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
	/// Summary description for PropertiesLightForm.
	/// </summary>
	public class frmPropertiesSound : System.Windows.Forms.Form
	{
		public bool mbOkWasPressed=false; 

        private System.Windows.Forms.Label objNameLabel;
		private System.Windows.Forms.Label label1;
		public System.Windows.Forms.Button objOkButton;
		public System.Windows.Forms.Button objCancelButtom;
		public System.Windows.Forms.TextBox objNameText;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		public System.Windows.Forms.ComboBox objActiveBox;
		public System.Windows.Forms.TextBox objMinDistText;
		public System.Windows.Forms.TextBox objMaxDistText;
		private System.Windows.Forms.Label label2;
		public System.Windows.Forms.TextBox objXText;
		public System.Windows.Forms.TextBox objYText;
		public System.Windows.Forms.TextBox objZText;
		public System.Windows.Forms.ComboBox objLoopBox;
		public System.Windows.Forms.ComboBox objRelativeBox;
		private System.Windows.Forms.Label label7;
		public System.Windows.Forms.TextBox objRandomText;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		public System.Windows.Forms.TextBox objSoundText;
		private System.Windows.Forms.OpenFileDialog OpenFileDiag;
		private System.Windows.Forms.Button button1;
		public System.Windows.Forms.TextBox objIntervalText;
		public System.Windows.Forms.TextBox objVolumeText;
		private System.Windows.Forms.Label label10;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmPropertiesSound(cSoundSource aSound)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			objNameText.Text = aSound.msName;
			
			objActiveBox.SelectedIndex = aSound.mbActive?1:0;
			objLoopBox.SelectedIndex = aSound.mbLoop?1:0;
			objRelativeBox.SelectedIndex = aSound.mbRelative?1:0;
			
			objSoundText.Text = aSound.msSoundName;
			objMinDistText.Text = aSound.mfMinDist.ToString();
			objMaxDistText.Text = aSound.mfMaxDist.ToString();
			objRandomText.Text = aSound.mlRandom.ToString();
			objIntervalText.Text = aSound.mlInterval.ToString();
			objXText.Text = aSound.mfRelX.ToString();
			objYText.Text = aSound.mfRelY.ToString();
			objZText.Text = aSound.mfRelZ.ToString();
			objVolumeText.Text = aSound.mfVolume.ToString();
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
			this.objMinDistText = new System.Windows.Forms.TextBox();
			this.objOkButton = new System.Windows.Forms.Button();
			this.objCancelButtom = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.objMaxDistText = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.objLoopBox = new System.Windows.Forms.ComboBox();
			this.objRelativeBox = new System.Windows.Forms.ComboBox();
			this.label6 = new System.Windows.Forms.Label();
			this.objActiveBox = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.objXText = new System.Windows.Forms.TextBox();
			this.objYText = new System.Windows.Forms.TextBox();
			this.objZText = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.objRandomText = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.objIntervalText = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.objSoundText = new System.Windows.Forms.TextBox();
			this.OpenFileDiag = new System.Windows.Forms.OpenFileDialog();
			this.button1 = new System.Windows.Forms.Button();
			this.objVolumeText = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
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
			this.label1.Location = new System.Drawing.Point(16, 112);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(48, 16);
			this.label1.TabIndex = 1;
			this.label1.Text = "MinDist:";
			// 
			// objNameText
			// 
			this.objNameText.Location = new System.Drawing.Point(104, 16);
			this.objNameText.MaxLength = 40;
			this.objNameText.Name = "objNameText";
			this.objNameText.Size = new System.Drawing.Size(136, 20);
			this.objNameText.TabIndex = 3;
			this.objNameText.Text = "";
			// 
			// objMinDistText
			// 
			this.objMinDistText.Location = new System.Drawing.Point(104, 112);
			this.objMinDistText.MaxLength = 40;
			this.objMinDistText.Name = "objMinDistText";
			this.objMinDistText.Size = new System.Drawing.Size(136, 20);
			this.objMinDistText.TabIndex = 4;
			this.objMinDistText.Text = "";
			// 
			// objOkButton
			// 
			this.objOkButton.Location = new System.Drawing.Point(48, 376);
			this.objOkButton.Name = "objOkButton";
			this.objOkButton.Size = new System.Drawing.Size(72, 24);
			this.objOkButton.TabIndex = 7;
			this.objOkButton.Text = "OK";
			this.objOkButton.Click += new System.EventHandler(this.objOkButton_Click);
			// 
			// objCancelButtom
			// 
			this.objCancelButtom.Location = new System.Drawing.Point(152, 376);
			this.objCancelButtom.Name = "objCancelButtom";
			this.objCancelButtom.Size = new System.Drawing.Size(72, 24);
			this.objCancelButtom.TabIndex = 8;
			this.objCancelButtom.Text = "Cancel";
			this.objCancelButtom.Click += new System.EventHandler(this.objCancelButtom_Click);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(16, 144);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(48, 16);
			this.label3.TabIndex = 9;
			this.label3.Text = "MaxDist:";
			// 
			// objMaxDistText
			// 
			this.objMaxDistText.Location = new System.Drawing.Point(104, 144);
			this.objMaxDistText.MaxLength = 6;
			this.objMaxDistText.Name = "objMaxDistText";
			this.objMaxDistText.Size = new System.Drawing.Size(136, 20);
			this.objMaxDistText.TabIndex = 10;
			this.objMaxDistText.Text = "";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(16, 176);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(80, 16);
			this.label4.TabIndex = 11;
			this.label4.Text = "Loop";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(16, 208);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(80, 16);
			this.label5.TabIndex = 12;
			this.label5.Text = "Relative Used:";
			// 
			// objLoopBox
			// 
			this.objLoopBox.Items.AddRange(new object[] {
															"False",
															"True"});
			this.objLoopBox.Location = new System.Drawing.Point(104, 176);
			this.objLoopBox.Name = "objLoopBox";
			this.objLoopBox.Size = new System.Drawing.Size(136, 21);
			this.objLoopBox.TabIndex = 13;
			// 
			// objRelativeBox
			// 
			this.objRelativeBox.Items.AddRange(new object[] {
																"False",
																"True"});
			this.objRelativeBox.Location = new System.Drawing.Point(104, 208);
			this.objRelativeBox.Name = "objRelativeBox";
			this.objRelativeBox.Size = new System.Drawing.Size(136, 21);
			this.objRelativeBox.TabIndex = 14;
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
			this.objActiveBox.Size = new System.Drawing.Size(136, 21);
			this.objActiveBox.TabIndex = 16;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(16, 240);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(88, 16);
			this.label2.TabIndex = 17;
			this.label2.Text = "Rel Pos (X,Y,Z):";
			// 
			// objXText
			// 
			this.objXText.Location = new System.Drawing.Point(104, 240);
			this.objXText.MaxLength = 6;
			this.objXText.Name = "objXText";
			this.objXText.Size = new System.Drawing.Size(40, 20);
			this.objXText.TabIndex = 18;
			this.objXText.Text = "";
			// 
			// objYText
			// 
			this.objYText.Location = new System.Drawing.Point(152, 240);
			this.objYText.MaxLength = 6;
			this.objYText.Name = "objYText";
			this.objYText.Size = new System.Drawing.Size(40, 20);
			this.objYText.TabIndex = 19;
			this.objYText.Text = "";
			// 
			// objZText
			// 
			this.objZText.Location = new System.Drawing.Point(200, 240);
			this.objZText.MaxLength = 6;
			this.objZText.Name = "objZText";
			this.objZText.Size = new System.Drawing.Size(40, 20);
			this.objZText.TabIndex = 20;
			this.objZText.Text = "";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(16, 272);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(48, 16);
			this.label7.TabIndex = 21;
			this.label7.Text = "Random:";
			// 
			// objRandomText
			// 
			this.objRandomText.Location = new System.Drawing.Point(104, 272);
			this.objRandomText.MaxLength = 6;
			this.objRandomText.Name = "objRandomText";
			this.objRandomText.Size = new System.Drawing.Size(136, 20);
			this.objRandomText.TabIndex = 22;
			this.objRandomText.Text = "";
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(16, 304);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(48, 16);
			this.label8.TabIndex = 23;
			this.label8.Text = "Interval:";
			// 
			// objIntervalText
			// 
			this.objIntervalText.Location = new System.Drawing.Point(104, 304);
			this.objIntervalText.MaxLength = 6;
			this.objIntervalText.Name = "objIntervalText";
			this.objIntervalText.Size = new System.Drawing.Size(136, 20);
			this.objIntervalText.TabIndex = 24;
			this.objIntervalText.Text = "";
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(16, 80);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(72, 16);
			this.label9.TabIndex = 25;
			this.label9.Text = "Sound name:";
			// 
			// objSoundText
			// 
			this.objSoundText.Location = new System.Drawing.Point(104, 80);
			this.objSoundText.MaxLength = 40;
			this.objSoundText.Name = "objSoundText";
			this.objSoundText.Size = new System.Drawing.Size(88, 20);
			this.objSoundText.TabIndex = 26;
			this.objSoundText.Text = "";
			// 
			// button1
			// 
			this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.button1.Location = new System.Drawing.Point(200, 80);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(40, 24);
			this.button1.TabIndex = 27;
			this.button1.Text = "...";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// objVolumeText
			// 
			this.objVolumeText.Location = new System.Drawing.Point(104, 336);
			this.objVolumeText.MaxLength = 6;
			this.objVolumeText.Name = "objVolumeText";
			this.objVolumeText.Size = new System.Drawing.Size(136, 20);
			this.objVolumeText.TabIndex = 29;
			this.objVolumeText.Text = "";
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(16, 336);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(48, 16);
			this.label10.TabIndex = 28;
			this.label10.Text = "Volume:";
			// 
			// frmPropertiesSound
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(272, 405);
			this.ControlBox = false;
			this.Controls.Add(this.objVolumeText);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.objSoundText);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.objIntervalText);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.objRandomText);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.objZText);
			this.Controls.Add(this.objYText);
			this.Controls.Add(this.objXText);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.objActiveBox);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.objRelativeBox);
			this.Controls.Add(this.objLoopBox);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.objMaxDistText);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.objCancelButtom);
			this.Controls.Add(this.objOkButton);
			this.Controls.Add(this.objMinDistText);
			this.Controls.Add(this.objNameText);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.objNameLabel);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmPropertiesSound";
			this.ShowInTaskbar = false;
			this.Text = "Edit SoundSource";
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

		private void button1_Click(object sender, System.EventArgs e)
		{
			string sTemp = Directory.GetCurrentDirectory();
			if(OpenFileDiag.ShowDialog()== DialogResult.OK)
			{
				OpenFileDiag.InitialDirectory = Path.GetDirectoryName(OpenFileDiag.FileName);
				
				string sSound = Path.GetFileName(OpenFileDiag.FileName);
				sSound = Path.ChangeExtension(sSound,null);
				
				//Fix this somehow.
				Directory.SetCurrentDirectory(sTemp);

				objSoundText.Text = sSound;
			}
		}


	}
}
