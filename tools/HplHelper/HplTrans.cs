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
using System.Collections;
using System.Xml;
using System.Windows.Forms;

namespace HplHelper
{
	
	public class cTransEntry
	{
		public String msName="";
		public String msText="";

		public cTransEntry()
		{

		}
	}

	//----------------------------------------

	public class cTransCategory
	{
		public String msName="";
		public ArrayList mvEntries;

		public HplTrans mpTrans;

		public cTransCategory(HplTrans apTrans)
		{
			mvEntries = new ArrayList();

			mpTrans = apTrans;
		}

		public void AddEntry(String asName)
		{
			cTransEntry Entry = new cTransEntry();
			Entry.msName = asName;
			mvEntries.Add(Entry);

			mpTrans.mpCurrentEntry = Entry;

			mpTrans.UpdateEntryList();
		}

		public void RemoveEntry(String asName)
		{
			for(int i=0; i< mvEntries.Count; i++)
			{
				cTransEntry Entry = (cTransEntry)mvEntries[i];
				if(Entry.msName == asName)
				{
					mvEntries.RemoveAt(i);
					mpTrans.mpCurrentEntry = null;
					mpTrans.UpdateEntryList();
					break;
				}
			}
		}
		
	}

	//----------------------------------------
	
	/// <summary>
	/// Summary description for HplTrans.
	/// </summary>
	public class HplTrans
	{
		public ArrayList mvDirectories;

		public ArrayList mvCategories;
		public frmMain mpMainForm;

		public cTransCategory mpCurrentCat=null;
		public cTransEntry mpCurrentEntry=null;

		public HplTrans(frmMain apMainForm)
		{
			mvCategories = new ArrayList();

			mpMainForm = apMainForm;

			mvDirectories = new ArrayList();
		}
		
		//------------------------------------------
		
		public void AddCategory(String asName)
		{
			cTransCategory Cat = new cTransCategory(this);
			Cat.msName = asName;
			mvCategories.Add(Cat);

			mpMainForm.objTransCategories.Items.Add(asName);

			//The form list object
			for(int i=0; i<mpMainForm.objTransCategories.Items.Count; i++)
			{
				String sName = (String)mpMainForm.objTransCategories.Items[i];
				if(sName == asName)
				{
					mpMainForm.objTransCategories.SelectedIndex = i;
					break;
				}
			}
			
			mpCurrentEntry = null;
			UpdateEntryList();
		}

		//------------------------------------------
		
		public void RemoveCategory(String asName)
		{
			//The internal list
			for(int i=0; i<mvCategories.Count; i++)
			{
				cTransCategory Cat = (cTransCategory)mvCategories[i];
				if(Cat.msName == asName)
				{
					mvCategories.RemoveAt(i);
					break;
				}
			}

			//The form list object
			for(int i=0; i<mpMainForm.objTransCategories.Items.Count; i++)
			{
				String sName = (String)mpMainForm.objTransCategories.Items[i];
				if(sName == asName)
				{
					mpMainForm.objTransCategories.Items.RemoveAt(i);
                    if(mpMainForm.objTransCategories.SelectedIndex<0)
						mpMainForm.objTransCategories.SelectedIndex = 0;
					
					if(mpMainForm.objTransCategories.Items.Count<=0)
						mpMainForm.objTransCategories.Text = "";
					
					break;
				}
			}
		}

		//------------------------------------------
		
		public void RenameCategory(String asName, String asNewName)
		{
			//The internal list
			for(int i=0; i<mvCategories.Count; i++)
			{
				cTransCategory Cat = (cTransCategory)mvCategories[i];
				if(Cat.msName == asName)
				{
					Cat.msName = asNewName;
					break;
				}
			}

			//The form list object
			for(int i=0; i<mpMainForm.objTransCategories.Items.Count; i++)
			{
				String sName = (String)mpMainForm.objTransCategories.Items[i];
				if(sName == asName)
				{
					mpMainForm.objTransCategories.Items[i] = asNewName;
					break;
				}
			}

			//TODO: Sort the list and set the correct new selected index.
		}

		//------------------------------------------
		
		public void AddEntry(String asName)
		{
			if(mpCurrentCat!= null)
			{
				mpCurrentCat.AddEntry(asName);
				UpdateEntryData();
			}
		}

		public void RemoveEntry(String asName)
		{
			if(mpCurrentCat!= null)
			{
				mpCurrentCat.RemoveEntry(asName);
				UpdateEntryData();
			}
		}
				
		//------------------------------------------
		
		public void Load(String asFile)
		{
			mpCurrentCat = null;
			mpCurrentEntry = null;

            mvCategories.Clear();
			mvDirectories.Clear();
			
			XmlDocument Doc = new XmlDocument();
			Doc.Load(asFile);

			XmlElement DocRoot = (XmlElement)Doc.FirstChild;

			/////////////////////////////////////////////////
			//Iterate all categories
			for(int child_count=0;child_count< DocRoot.ChildNodes.Count;child_count++)
			{
				XmlElement CatElem = (XmlElement)DocRoot.ChildNodes[child_count];
				if(CatElem.Name == "RESOURCES")
				{
					for(int dir=0;dir< CatElem.ChildNodes.Count;dir++)
					{
						XmlElement DirElem = (XmlElement)CatElem.ChildNodes[dir];
						
						String sPath = DirElem.GetAttribute("Path");
						mvDirectories.Add(sPath);
					}
				}
				else
				{
					cTransCategory pCat = new cTransCategory(this);
					pCat.msName = CatElem.GetAttribute("Name");
					mvCategories.Add(pCat);
					
					for(int entry=0; entry<CatElem.ChildNodes.Count; entry++)
					{
						XmlElement EntryElem = (XmlElement)CatElem.ChildNodes[entry];
						cTransEntry pEntry = new cTransEntry();
						pEntry.msName = EntryElem.GetAttribute("Name");
	            		
						String sText = EntryElem.InnerText;
						String sNewString = "";

						
						for(int i=0; i<sText.Length; ++i)
						{
							if(sText[i]=='[')
							{
								bool bFoundCommand = true;
								String sCommand = "";
								int lCount =1;
								
								while(sText[i+lCount] != ']' && i+lCount<sText.Length && lCount < 16)
								{
									sCommand += sText[i+lCount];
									lCount++;
								}

								if(sCommand=="br")
								{
									sNewString += "\r\n";
								}
								else if(sCommand[0]=='u')
								{
									int lNum = int.Parse(sCommand.Substring(1));	
									sNewString += (char)lNum;
								}
								else
								{
									bFoundCommand = false;
								}
								
								//Go forward or add [ to string
								if(bFoundCommand)
								{
									i += lCount;
								}
								else
								{
									sNewString += sText[i];
								}
							}
							else
							{
								sNewString += sText[i];
							}
						}
						
						/*for(int i=0; i<sText.Length; ++i)
						{
							if(sText[i]=='[' && sText.Length > i + 4)
							{
								String sSign = sText.Substring(i,4);
								if(sSign == "[br]")
								{
									sNewString += "\r\n";
									i+=3;
								}
								else
								{
									sNewString += sText[i];
								}
							}
							else sNewString += sText[i];
						}*/

						pEntry.msText = sNewString;

						pCat.mvEntries.Add(pEntry);
					}
				}
			}

			UpdateCategroyList();
		}

		//------------------------------------------

		public void Save(String asFile)
		{
			XmlDocument Doc = new XmlDocument();
			
			XmlElement DocRoot = Doc.CreateElement("LANGUAGE");
			Doc.AppendChild(DocRoot);

			/////////////////////////////////
			// Save Directories
			XmlElement ResourceElem = Doc.CreateElement("RESOURCES");
			DocRoot.AppendChild(ResourceElem);
			for(int dir=0; dir < mvDirectories.Count; ++dir)
			{
				String sPath = (String)mvDirectories[dir];
				XmlElement DirElem = Doc.CreateElement("Directory");
				ResourceElem.AppendChild(DirElem);

                DirElem.SetAttribute("Path",sPath);					
			}
			
			/////////////////////////////////
			// Save categories
			for(int cat=0; cat<mvCategories.Count; cat++)
			{
				cTransCategory pCat = (cTransCategory)mvCategories[cat];

				XmlElement CatElem = Doc.CreateElement("CATEGORY");
				DocRoot.AppendChild(CatElem);
				CatElem.SetAttribute("Name",pCat.msName);

				for(int entry=0; entry< pCat.mvEntries.Count; entry++)
				{
					cTransEntry pEntry = (cTransEntry)pCat.mvEntries[entry];
					
					XmlElement EntryElem = Doc.CreateElement("Entry");
					CatElem.AppendChild(EntryElem);
					
					EntryElem.SetAttribute("Name",pEntry.msName);
					String sText = pEntry.msText;
					String sNewString = "";

					for(int i=0; i<sText.Length; ++i)
					{
						if(sText[i]=='\n')
						{
							sNewString += "[br]";
						}
						else if(sText[i]=='\r')
						{
							//Do nothing...
						}
						else if(sText[i] > 255)
						{
							sNewString += "[u"+((int)sText[i]).ToString()+"]";
						}
						else 
						{
							sNewString += sText[i];
						}
					}

					XmlText EntryTextElem = Doc.CreateTextNode(sNewString);
					EntryElem.AppendChild(EntryTextElem);
				}
			}
			
			Doc.Save(asFile);
		}

		//------------------------------------------
		
		public void SetCurrentCat()
		{
			String sName = (String)mpMainForm.objTransCategories.SelectedItem;
			//The internal list
			for(int i=0; i<mvCategories.Count; i++)
			{
				cTransCategory Cat = (cTransCategory)mvCategories[i];
				if(Cat.msName == sName)
				{
					mpCurrentCat = Cat;
					UpdateEntryList();
					return;
				}
			}

			mpCurrentCat = null;

			UpdateEntryList();
		}

		//------------------------------------------

		public void SetCurrentEntry()
		{
			if(mpCurrentCat==null) return;
			
			String sName = (String)mpMainForm.objTransEntries.SelectedItem;
			//The internal list
			for(int i=0; i<mpCurrentCat.mvEntries.Count; i++)
			{
				cTransEntry Entry = (cTransEntry)mpCurrentCat.mvEntries[i];
				if(Entry.msName == sName)
				{
					mpCurrentEntry = Entry;
					UpdateEntryData();
					return;
				}
			}

			mpCurrentEntry = null;

			UpdateEntryData();
		}

		//------------------------------------------

		public void UpdateEntryData()
		{
			if(mpCurrentEntry==null || mpCurrentCat==null)
			{
				mpMainForm.objTransEntryName.Text = "";
				mpMainForm.objTransEntryText.Text = "";
				mpMainForm.txtTransScriptName.Text = "";
			}
			else
			{
				mpMainForm.objTransEntryName.Text = mpCurrentEntry.msName;
				mpMainForm.objTransEntryText.Text =  mpCurrentEntry.msText;
				mpMainForm.txtTransScriptName.Text = 
						"Translate(\""+ mpCurrentCat.msName + "\", \"" + mpCurrentEntry.msName+"\")";
			}
		}

		//------------------------------------------

		
		public void UpdateEntryList()
		{
			String sSelName = "";
			
			if(mpCurrentEntry!=null)
				sSelName = mpCurrentEntry.msName;

			cTransCategory Cat = GetCateogry((String)mpMainForm.objTransCategories.SelectedItem);

			mpMainForm.objTransEntries.Items.Clear();

			if(Cat==null){
				mpMainForm.objTransEntries.Text="";
				UpdateEntryData();
				return;
			}

			if(Cat.mvEntries.Count<=0)
			{
				mpMainForm.objTransEntries.Text="";
				UpdateEntryData();
				return;
			}

			for(int i=0; i < Cat.mvEntries.Count; i++)
			{
				mpMainForm.objTransEntries.Items.Add(((cTransEntry)Cat.mvEntries[i]).msName);
			}

			mpMainForm.objTransEntries.SelectedIndex =0;

			if(sSelName!="")
			{
				for(int i=0; i< mpMainForm.objTransEntries.Items.Count;i++)
				{
					String sName = (String)mpMainForm.objTransEntries.Items[i];

					if(sName == sSelName)
					{
						mpMainForm.objTransEntries.SelectedIndex = i;
						break;
					}
				}
			}

			UpdateEntryData();
		}

		//------------------------------------------
		
		public void UpdateCategroyList()
		{
			mpMainForm.objTransCategories.Items.Clear();

			if(mvCategories.Count<0){
				mpMainForm.objTransCategories.Text="";
				return;
			}

			for(int i=0; i<mvCategories.Count; i++)
			{
				cTransCategory Cat = (cTransCategory)mvCategories[i];
				
				mpMainForm.objTransCategories.Items.Add(Cat.msName);
			}

			mpMainForm.objTransCategories.SelectedIndex =0;

			UpdateEntryList();
			UpdateEntryData();
		}
		

		//------------------------------------------
		
		public cTransCategory GetCateogry(String asName)
		{
			for(int i=0; i<mvCategories.Count; i++)
			{
				cTransCategory Cat = (cTransCategory)mvCategories[i];
				if(Cat.msName == asName)
				{
					return Cat;
				}
			}

			return null;
		}
	}
}
