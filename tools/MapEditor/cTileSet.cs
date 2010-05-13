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
using System.IO;
using System.Drawing;
using System.Collections;
using System.Xml;
using System.Windows.Forms;

namespace Mapeditor
{
	
	/// <summary>
	/// Summary description for cTileSet.
	/// </summary>
	public class cTileSet
	{
		public ArrayList mlstTileData=null;
		private string msDirectory="";
		public string msTileSetFile;
		public string msName="";
		public int mlTileSize;
	
		public cTileSet(string asTileSetFile)
		{
			mlstTileData = new ArrayList();
			msTileSetFile = asTileSetFile;
			msName = Path.GetFileNameWithoutExtension(msTileSetFile);
		}

		public bool LoadData()
		{
			//Load the XML file:
			XmlDocument Doc = new XmlDocument();
			Doc.Load(msTileSetFile);
			
			XmlElement DocRoot = (XmlElement)Doc.FirstChild;
			
			msDirectory = Path.GetFullPath(DocRoot.GetAttribute("dir")).Trim();
			mlTileSize = Convert.ToInt32(DocRoot.GetAttribute("size"));
			
			if(msDirectory.EndsWith("\\")==false && msDirectory.EndsWith("/")==false)
			{
				msDirectory+="/";
			}
			
			//Iterate trough all tiles:
			for(int i=0;i< DocRoot.ChildNodes.Count;i++)
			{
				XmlElement TNode = (XmlElement)DocRoot.ChildNodes[i];
				
				cTileData TData = new cTileData(TNode.GetAttribute("name"),
												msDirectory + TNode.GetAttribute("name"),
												TNode.GetAttribute("material"),
												TNode.GetAttribute("mesh"));
				TData.LoadData();

				mlstTileData.Add(TData);
			}
			return true;
		}
	}
}
