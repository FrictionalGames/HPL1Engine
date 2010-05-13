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

namespace Mapeditor
{
	/// <summary>
	/// Summary description for cProp.
	/// </summary>
	public class cProp : cImageEntity
	{
		public cProp(cImageEntityData aData,int alX,int alY, float afZ) : base(aData,alX, alY, afZ)
		{

		}

		public override void EditProperties()
		{
			frmPropertiesProp EditProp = new frmPropertiesProp(this);
			
			EditProp.ShowDialog();

			if(EditProp.mbOkWasPressed)
			{
				msName = EditProp.objNameText.Text;
				mbActive = EditProp.objActiveBox.SelectedIndex==1?true:false;
				mfWidth = (float)Convert.ToDouble(EditProp.objWidthText.Text);
				mfHeight = (float)Convert.ToDouble(EditProp.objHeightText.Text);
				mfZ = (float)Convert.ToDouble(EditProp.objZText.Text);
				mfAngle = (float)Convert.ToDouble(EditProp.objAngleText.Text);
				mbFlipH = EditProp.objFlipHBox.SelectedIndex==1?true:false;
				mbFlipV = EditProp.objFlipVBox.SelectedIndex==1?true:false;
				mlAnimNum = EditProp.objAnimBox.SelectedIndex;
			}
			
			EditProp.Dispose();
		}
	}
}
