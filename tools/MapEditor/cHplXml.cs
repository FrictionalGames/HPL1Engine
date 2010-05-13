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
using System.Xml;
using System.Collections;

namespace Mapeditor
{
	/// <summary>
	/// Summary description for cHplXml.
	/// </summary>
	public class cHplXml
	{
		static public ArrayList GetIntList(XmlElement aElement, string asName, ArrayList aList)
		{
			GetStrList(aElement, asName, aList);

			for(int i=0;i<aList.Count;i++)
			{
				aList[i] = Convert.ToInt32(aList[i]);
			}

			return aList;
		}

		static public ArrayList GetStrList(XmlElement aElement, string asName, ArrayList aList)
		{
			string str = "";
			bool start = false;
			string c = "";
			
			if(aElement.HasAttribute(asName))
			{
				string sData = aElement.GetAttribute(asName);

				for(int i=0;i<sData.Length;i++)
				{
					c = sData.Substring(i,1);
            
					if(c==" " || c=="\n" || c=="\t" || c==",")
					{
						if(start)
						{
							start = false;
							aList.Add(str);
							str = "";
						}
					}
					else
					{
						start = true;
						str +=c;
						if(i==sData.Length-1)aList.Add(str);
					}
				}	
			}
			
			return aList;
		}

		static public string GetStr(XmlElement aElement, string asName, string asDefaultVal)
		{
			if(aElement.HasAttribute(asName)){
				return aElement.GetAttribute(asName);
			}
			else
			{
				return asDefaultVal;
			}
		}

		static public int GetInt(XmlElement aElement, string asName, int alDefaultVal)
		{
			if(aElement.HasAttribute(asName))
			{
				return Convert.ToInt32(aElement.GetAttribute(asName));
			}
			else
			{
				return alDefaultVal;
			}
		}

		static public float GetFloat(XmlElement aElement, string asName, float afDefaultVal)
		{
			if(aElement.HasAttribute(asName))
			{
				float fVal;
				try{
					fVal = (float)Convert.ToDouble(aElement.GetAttribute(asName));
				}
				catch{
					fVal = afDefaultVal;
				}
				return fVal;
			}
			else
			{
				return afDefaultVal;
			}
		}

		static public bool GetBool(XmlElement aElement, string asName, bool abDefaultVal)
		{
			if(aElement.HasAttribute(asName))
			{
				return Convert.ToBoolean(aElement.GetAttribute(asName));
			}
			else
			{
				return abDefaultVal;
			}
		}
	}
}
