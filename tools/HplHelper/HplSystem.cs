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
using System.Diagnostics;
using System.ComponentModel;
using System.IO;

namespace HplHelper
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	public class HplSystem
	{
		const int ERROR_FILE_NOT_FOUND =2;
		const int ERROR_ACCESS_DENIED = 5;
		
		public HplSystem()
		{
		}

		static public String MakePathRelativeToCurrent(String asPath)
		{
			String sCurrent = Directory.GetCurrentDirectory();
				
			//MessageBox.Show("Path:"+sPath+" Curr:"+sCurrent,"Test");

			int lPos =0;
			for(int i=0; i< asPath.Length; i++)
			{
				if(i>= sCurrent.Length || i>= asPath.Length || asPath[i] != sCurrent[i])
				{
					lPos =i;
					break;
				}
			}

			return asPath.Substring(lPos,asPath.Length - lPos);
		}

		static public bool RunProgam(String asFile, String asArgs)
		{
			Process myProcess = new Process();
            
			try
			{
				myProcess.StartInfo.FileName = asFile; 
				myProcess.StartInfo.Arguments = asArgs;
				myProcess.Start();
			}
			catch (Win32Exception e)
			{
				if(e.NativeErrorCode == ERROR_FILE_NOT_FOUND)
				{
					return false;
				} 

				else if (e.NativeErrorCode == ERROR_ACCESS_DENIED)
				{
					return false;
				}
			}

			return true;
		}
	}
}
