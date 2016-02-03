using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Xml;
using ESRI.ArcGIS.Schematic;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;

namespace DigitTool
{
	/// <summary>
	/// Designer class of the dockable window add-in. It contains user interfaces that
	/// make up the dockable window.
	/// </summary>
	public partial class DigitDockableWindow : UserControl
	{
		private DigitTool m_digitCommand;

		private XmlDocument m_dom = null;
		private bool m_loading = true;
		private bool m_clickPanel = false;
		private long m_curfrmWidth;

		private System.Windows.Forms.SplitterPanel m_Panel1;
		private System.Windows.Forms.SplitterPanel m_Panel2;

		private System.Windows.Forms.SplitterPanel m_curPanel;
		private System.Drawing.Color m_MandatoryColor = System.Drawing.Color.White;

		private ISchematicLayer m_schematicLayer;
		private ISchematicFeature m_schematicFeature1 = null;
		private ISchematicFeature m_schematicFeature2 = null;
		private bool m_createNode = true; //update when click on panel and on new
		private bool m_isClosed = false;
		private ESRI.ArcGIS.Framework.IApplication m_app;

		//For button OK to retrieve the coordinate of the digit feature
		private int m_x;
		private int m_y;

		private XmlElement m_curLink = null;
		private XmlElement m_curNode = null;
		private XmlNodeList m_relations = null;
		private ISchematicDataset m_schDataset = null;
		private ISchematicElementClassContainer m_schEltClassCont = null;
		private ISchematicElementClass m_schEltClass = null;
		private ISchematicInMemoryDiagram m_SchematicInMemoryDiagram = null;
		private bool m_autoClear = false;


		public DigitDockableWindow(object hook)
		{
			InitializeComponent();
			this.Hook = hook;
		}

		/// <summary>
		/// Host object of the dockable window
		/// </summary>
		private object Hook
		{
			get;
			set;
		}

		/// <summary>
		/// Implementation class of the dockable window add-in. It is responsible for 
		/// creating and disposing the user interface class of the dockable window.
		/// </summary>
		public class AddinImpl : ESRI.ArcGIS.Desktop.AddIns.DockableWindow
		{
			private DigitDockableWindow m_windowUI;

			public AddinImpl()
			{
			}

			protected override IntPtr OnCreateChild()
			{
				m_windowUI = new DigitDockableWindow(this.Hook);

				CurrentDigitTool.CurrentTool.digitDockableWindow = m_windowUI;

				if (CurrentDigitTool.CurrentTool.currentDigit != null)
				{
					m_windowUI.m_digitCommand = CurrentDigitTool.CurrentTool.currentDigit;
					m_windowUI.m_digitCommand.m_dockableDigit = m_windowUI;
				}
				else
				{
					// CurrentDigitTool.CurrentTool.CurrentDigit is null when we open ArcMap, but OnCreateChild
					// is called if the dockable window was shown during the last ArcMap session.
					ESRI.ArcGIS.Framework.IDockableWindowManager dockWinMgr = ArcMap.DockableWindowManager;
					UID u = new UID();
					u.Value = "DigitTool_DockableWindowCS";
					CurrentDigitTool.CurrentTool.currentDockableWindow = dockWinMgr.GetDockableWindow(u);
				}

				return m_windowUI.Handle;
			}

			protected override void Dispose(bool disposing)
			{
				if (m_windowUI != null)
					m_windowUI.Dispose(disposing);

				base.Dispose(disposing);
			}

		}

		public void Init(ISchematicLayer schematicLayer)
		{
			// CR229717: Lost the ElementClass if the dockable window is deactivate
			if (schematicLayer == m_schematicLayer)
				if (m_schEltClass != null)
					return;

			try
			{
				if (schematicLayer == null)
					return;

				m_schematicLayer = schematicLayer;
				XmlNode col = null;
				String myString = "";

				m_schDataset = schematicLayer.SchematicDiagram.SchematicDiagramClass.SchematicDataset;

				m_schEltClassCont = (ISchematicElementClassContainer)m_schDataset;
				m_SchematicInMemoryDiagram = schematicLayer.SchematicInMemoryDiagram;

				m_dom = new XmlDocument();

				ISchematicDiagram schematicDiagram;
				schematicDiagram = m_SchematicInMemoryDiagram.SchematicDiagram;

				// get the path of the xml file that contains the definitions of the digitize dockable window
				String path;

				ISchematicDiagramClass schematicDiagramClass = schematicDiagram.SchematicDiagramClass;
				ISchematicAttributeContainer schematicAttributeContainer = (ISchematicAttributeContainer)schematicDiagramClass;

				ISchematicAttribute schematicAttribute = schematicAttributeContainer.GetSchematicAttribute("DigitizePropertiesLocation", true);

				if (schematicAttribute == null)
				{
					System.Windows.Forms.MessageBox.Show("Need an attribute named DigitizePropertiesLocation in the corresponding DiagramTemplate attributes");
					return;
				}

				path = (string)schematicAttribute.GetValue((ISchematicObject)schematicDiagram);

				if (IsRelative(path)) //Concat the workspace's path with this path
				{
					//current workspace path
					ISchematicDataset myDataset = schematicDiagramClass.SchematicDataset;
					if (myDataset != null)
					{
						ISchematicWorkspace mySchematicWorkspace = myDataset.SchematicWorkspace;
						if (mySchematicWorkspace != null)
						{
							ESRI.ArcGIS.Geodatabase.IWorkspace myWorkspace = mySchematicWorkspace.Workspace;
							if (myWorkspace != null)
							{
								string workspacePath = myWorkspace.PathName;
								//add "..\" to path to step back one level...
								string stepBack = "..\\";
								path = stepBack + path;

								path = System.IO.Path.Combine(workspacePath, path);
							}
						}
					}
				}
				//else keep the original hard path

				XmlReader reader = XmlReader.Create(path);

				m_dom.Load(reader);

				//Load Nodes
				XmlNodeList nodes = m_dom.SelectNodes("descendant::NodeFeature");

				//Clear combo box after each call
				cboNodeType.Items.Clear();
				foreach (XmlElement node in nodes)
				{
					cboNodeType.Items.Add(node.GetAttribute("FeatureClassName").ToString());
				}


				//Load Links
				XmlNodeList links = m_dom.SelectNodes("descendant::LinkFeature");

				//Clear combo box after each call
				cboLinkType.Items.Clear();
				foreach (XmlElement link in links)
				{
					cboLinkType.Items.Add(link.GetAttribute("FeatureClassName").ToString());
				}

				col = m_dom.SelectSingleNode("descendant::MandatoryColor");
				if (col != null)
				{
					myString = "System.Drawing.";
					myString = col.InnerText.ToString();
					m_MandatoryColor = System.Drawing.Color.FromName(myString);
				}

				col = m_dom.SelectSingleNode("descendant::FormName");
				if (col != null)
				{
					myString = col.InnerText.ToString();
					Text = myString;
				}


				XmlNodeList rels = m_dom.SelectNodes("descendant::Relation");
				if (rels.Count > 0)
					m_relations = rels;

				col = m_dom.SelectSingleNode("descendant::AutoClearAfterCreate");
				if ((col != null) && col.InnerText.ToString() == "True")
					m_autoClear = true;

			}
			catch (System.Exception e)
			{
				System.Windows.Forms.MessageBox.Show(e.Message);
			}

			m_Panel1 = Splitter.Panel1;
			m_Panel2 = Splitter.Panel2;
			m_curPanel = Splitter.Panel1;
			lblMode.Text = "Create Node";
			m_loading = false;
			m_clickPanel = false;
			m_schEltClass = null;

		}

		public bool ValidateFields()
		{
			bool blnValidated = true;
			System.Windows.Forms.MaskedTextBox mctrl = null;
			string errors = "";
			string linkTypeChoice = "";
			bool firstime = true;

			//check all mandatory fields
			System.Windows.Forms.Control ctrl2;


			foreach (System.Windows.Forms.Control ctrl in m_curPanel.Controls)
			{
				ctrl2 = ctrl;

				if (ctrl2.GetType() == typeof(System.Windows.Forms.Label))
				{
				}
				//ignore labels
				else
				{
					if ((string)ctrl2.Tag == "Mandatory")
					{
						if (ctrl2.GetType() == typeof(System.Windows.Forms.MaskedTextBox))
						{
							mctrl = (System.Windows.Forms.MaskedTextBox)ctrl2;
							if (mctrl.MaskCompleted == false)
							{
								blnValidated = false;
								if (errors.Length > 0)
									errors = "Incomplete mandatory field" + Environment.NewLine + "Complete missing data and click on OK button";
								else
									errors = errors + Environment.NewLine + "Incomplete mandatory field" + Environment.NewLine + "Complete missing data and click on OK button";
							}
						}
						else
						{
							if (ctrl2.Text.Length <= 0)
							{
								blnValidated = false;
								if (errors.Length <= 0)
									errors = "Incomplete mandatory field" + Environment.NewLine + "Complete missing data and click on OK button";
								else
									errors = errors + Environment.NewLine + "Incomplete mandatory field" + Environment.NewLine + "Complete missing data and click on OK button";
							}
						}
					}


					//check masked edit controls
					if (ctrl2.GetType() == typeof(System.Windows.Forms.MaskedTextBox))
					{
						mctrl = (System.Windows.Forms.MaskedTextBox)ctrl2;
						//if they typed something, but didn't complete it, then error
						//if they typed nothing and it is not mandatory, then it is OK
						if ((mctrl.Text.Length > 0) && mctrl.Modified && (!mctrl.MaskCompleted))
						{
							blnValidated = false;
							if (errors.Length > 0)
								errors = "Invalid entry in a masked text field";
							else
								errors = errors + Environment.NewLine + "Invalid entry in a masked text field";
						}
					}

					//check link connections
					if (m_curPanel == Splitter.Panel2)
					{
						//make sure that the relation is correct if it exists
						XmlNodeList fields = m_curLink.SelectNodes("descendant::Field");
						foreach (XmlElement field in fields)
						{
							//find the field with a type of "Relation"
							if (field.GetAttribute("Type") == "Relation")
							{
								ESRI.ArcGIS.Geodatabase.IDataset pDataset1;
								ESRI.ArcGIS.Geodatabase.IDataset pDataset2;

								string FeatureClass1Name;
								string FeatureClass2Name;

								pDataset1 = (ESRI.ArcGIS.Geodatabase.IDataset)m_schematicFeature1.SchematicElementClass;
								pDataset2 = (ESRI.ArcGIS.Geodatabase.IDataset)m_schematicFeature2.SchematicElementClass;

								FeatureClass1Name = pDataset1.Name;
								FeatureClass2Name = pDataset2.Name;

								foreach (XmlElement rel in m_relations)
								{

									//loop through the xml relations to match based on the from node and to node types

									if ((rel.GetAttribute("FromType") == FeatureClass1Name) && (rel.GetAttribute("ToType") == FeatureClass2Name))
									{
										//find the control with the pick list for relationships
										Control[] ctrls = m_curPanel.Controls.Find(field.GetAttribute("DBColumnName"), true);
										if (ctrls.Length > 0)
											ctrl2 = ctrls[0];

										XmlNodeList vals = rel.SelectNodes("descendant::Value");
										string myString = rel.GetAttribute("FromType") + "-" + rel.GetAttribute("ToType");
										string linkTypeClicking = myString;

										//validate that the current control string is correct
										//if there are values, use them
										bool blnfound = false;

										if (vals.Count > 0)
										{
											foreach (XmlElement val in vals)
											{
												linkTypeClicking = myString + "-" + val.InnerText.ToString();

												if (myString + "-" + val.InnerText.ToString() == ctrl2.Text)
												{
													blnfound = true;
													break;
												}
												else
												{
													blnfound = false;
													if (firstime)
													{
														linkTypeChoice = ctrl2.Text;
														firstime = false;
													}
												}
											}

											if (!blnfound)
											{
												blnValidated = false;
												if (errors.Length > 0)
												{
													errors = "Invalid link connection because :";
													errors = errors + Environment.NewLine + "Type's link clicked : " + linkTypeClicking;
													errors = errors + Environment.NewLine + "Type's link chosen : " + linkTypeChoice;

												}
												else
												{
													errors = errors + Environment.NewLine + "Invalid link connection because :";
													errors = errors + Environment.NewLine + "Type's link clicked : " + linkTypeClicking;
													errors = errors + Environment.NewLine + "Type's link chosen : " + linkTypeChoice;
												}
											}
										}
										else
										{
											if (ctrl2.Text != myString)
											{
												if (firstime)
												{
													linkTypeChoice = ctrl2.Text;
													firstime = false;
												}

												blnValidated = false;
												if (errors.Length > 0)
												{
													errors = "Invalid link connection because :";
													errors = errors + Environment.NewLine + "Type's link clicked : " + linkTypeClicking;
													errors = errors + Environment.NewLine + "Type's link chosen : " + linkTypeChoice;
												}
												else
												{
													errors = errors + Environment.NewLine + "Invalid link connection because :";
													errors = errors + Environment.NewLine + "Type's link clicked : " + linkTypeClicking;
													errors = errors + Environment.NewLine + "Type's link chosen : " + linkTypeChoice;
												}
											}
											else //// treatment case ctrl2.Text = myString
											{
												blnfound = true;
											}
										}

										if (!blnfound) //fix connection list
										{
											XmlNodeList vlist = m_dom.SelectNodes("descendant::Relation");
											XmlNodeList rellist = null;
											System.Windows.Forms.ComboBox cboconn = (System.Windows.Forms.ComboBox)ctrl2;
											cboconn.Items.Clear();
											foreach (XmlElement v in vlist)
											{
												if ((v.GetAttribute("LinkType").ToString() == m_curLink.GetAttribute("FeatureClassName").ToString())
													//make sure the node types are ok 
														&& (v.GetAttribute("FromType").ToString() == FeatureClass1Name || v.GetAttribute("ToType").ToString() == FeatureClass2Name))
												{
													rellist = v.SelectNodes("descendant::Value");
													if (rellist.Count > 0)
													{
														foreach (XmlElement r in rellist)
														{
															myString = v.GetAttribute("FromType").ToString() + "-" + v.GetAttribute("ToType").ToString() + "-" + r.InnerText.ToString();
															cboconn.Items.Add(myString);
														}
													}
													else //assume they are not using subtypes
														cboconn.Items.Add(v.GetAttribute("FromType").ToString() + "-" + v.GetAttribute("ToType").ToString());
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}

			if (errors.Length > 0)
			{

				if (m_curPanel == Splitter.Panel1)
				{
					btnOKPanel1.Visible = true;
					ErrorProvider1.SetError(btnOKPanel1, errors);
				}
				else
				{
					btnOKPanel2.Visible = true;
					ErrorProvider1.SetError(btnOKPanel2, errors);
				}
			}
			return blnValidated;

		}

		public void SelectionChanged()
		{

			try
			{
				System.Windows.Forms.Control ctrl = null;
				System.Windows.Forms.Control ctrl2 = null;
				Control[] ctrls = null;
				System.Collections.ArrayList ctrlstoremove = new System.Collections.ArrayList();
				string labelName = "";
				string featureClass = "";
				System.Windows.Forms.ComboBox cbo = null;
				System.Windows.Forms.Label lblMain = null;

				if (m_digitCommand == null)
					return;

				//clear any current elements
				if (m_schematicFeature1 != null)
				{
					m_schematicFeature1 = null;
					m_digitCommand.SchematicFeature1(m_schematicFeature1);
				}

				if (m_schematicFeature2 != null)
				{
					m_schematicFeature2 = null;
					m_digitCommand.SchematicFeature2(m_schematicFeature2);
				}

				if (m_curPanel == Splitter.Panel1)
				{
					labelName = "lblNodeLabel";
					featureClass = "descendant::NodeFeature";
					cbo = cboNodeType;
					lblMain = lblNodeLabel;
				}
				else
				{
					labelName = "lblLinkLabel";
					featureClass = "descendant::LinkFeature";
					cbo = cboLinkType;
					lblMain = lblLinkLabel;
				}

				foreach (System.Windows.Forms.Control ctrlfor in m_curPanel.Controls)
				{
					if (ctrlfor.Name.StartsWith("lbl") && (ctrlfor.Name.ToString() != labelName))
					{
						ctrls = m_curPanel.Controls.Find(ctrlfor.Name.Substring(3), true);
						ctrl2 = ctrls[0];
						ctrlstoremove.Add(ctrlfor);
						ctrlstoremove.Add(ctrl2);
					}
				}

				if (ctrlstoremove.Count > 0)
				{
					foreach (System.Windows.Forms.Control ctrol in ctrlstoremove)
					{
						m_curPanel.Controls.Remove(ctrol);
					}
				}

				XmlNodeList elems = null;
				m_curfrmWidth = m_curPanel.Width;
				elems = m_dom.SelectNodes(featureClass);

				bool blnFound = false;
				XmlElement elem = null;
				foreach (XmlElement elem0 in elems)
				{
					if (elem0.GetAttribute("FeatureClassName").ToString() == cbo.Text.ToString())
					{
						elem = elem0;
						blnFound = true;
						break;
					}
				}

				if (blnFound == false)
				{
					// CR229717: If this is deactivate, lost the Schematic ElementClass and can not retrieve it
					//m_schEltClass = null;
					return;
				}

				if (m_curPanel == Splitter.Panel1)
					m_curNode = elem;
				else
					m_curLink = elem;

				//set grid
				elems = elem.SelectNodes("descendant::Field");
				int x = Splitter.Location.X;
				int y = lblMain.Location.Y + lblMain.Height + 5;

				System.Drawing.Point p = new System.Drawing.Point();

				foreach (XmlElement f in elems)
				{
					System.Windows.Forms.Label lbl = new System.Windows.Forms.Label();
					lbl.Name = "lbl" + f.GetAttribute("DBColumnName").ToString();
					lbl.Text = f.GetAttribute("DisplayName").ToString();
					lbl.AutoSize = true;
					m_curPanel.Controls.Add(lbl);
					p.X = 3;
					p.Y = y;
					lbl.Location = p;
					y = y + lbl.Height + 10;

					switch (f.GetAttribute("Type").ToString())
					{
						case "Text":
							System.Windows.Forms.TextBox tx = new System.Windows.Forms.TextBox();
							ctrl = tx;
							tx.Name = f.GetAttribute("DBColumnName").ToString();
							if (f.GetAttribute("Length").Length > 0)
								tx.MaxLength = System.Convert.ToInt32(f.GetAttribute("Length"));

							if (f.GetAttribute("Default").Length > 0)
								tx.Text = f.GetAttribute("Default");

							m_curPanel.Controls.Add(tx);
							break;

						case "Combo":

							System.Windows.Forms.ComboBox cb = new System.Windows.Forms.ComboBox();
							string defaulttext = "";
							ctrl = cb;
							cb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
							cb.Name = f.GetAttribute("DBColumnName").ToString();
							XmlNodeList vlist = f.SelectNodes("descendant::Value");

							foreach (XmlElement v in vlist)
							{
								cb.Items.Add(v.InnerText.ToString());
								if (v.GetAttribute("Default").Length > 0)
									defaulttext = v.InnerText;
							}

							if (defaulttext.Length > 0)
								cb.Text = defaulttext;

							m_curPanel.Controls.Add(cb);
							break;

						case "MaskText":
							System.Windows.Forms.MaskedTextBox MaskControl = new System.Windows.Forms.MaskedTextBox();
							ctrl = MaskControl;
							string mask = "";
							MaskControl.Name = f.GetAttribute("DBColumnName").ToString();
							if (f.GetAttribute("Mask").Length > 0)
								mask = f.GetAttribute("Mask");
							else
								mask = "";

							MaskControl.Mask = mask;

							if (f.GetAttribute("Default").Length > 0)
								MaskControl.Text = f.GetAttribute("Default");

							MaskControl.Modified = false;
							m_curPanel.Controls.Add(MaskControl);
							MaskControl.TextChanged += new System.EventHandler(MaskedTextBox);

							break;

						case "Number":
							System.Windows.Forms.MaskedTextBox MaskControl2 = new System.Windows.Forms.MaskedTextBox();
							ctrl = MaskControl2;
							string mask2 = "";
							MaskControl2.Name = f.GetAttribute("DBColumnName").ToString();
							int i = 1;
							if (f.GetAttribute("Length").Length > 0)
							{
								for (i = 1; i <= System.Convert.ToInt32(f.GetAttribute("Length")); i++)
									mask = mask2 + "9";
							}
							else
							{
								if (f.GetAttribute("Mask").Length > 0)
									mask2 = f.GetAttribute("Mask");
								else
									mask2 = "";
							}

							MaskControl2.Mask = mask2;
							if (f.GetAttribute("Default").Length > 0)
								MaskControl2.Text = f.GetAttribute("Default");

							MaskControl2.Modified = false;
							m_curPanel.Controls.Add(MaskControl2);
							MaskControl2.TextChanged += new System.EventHandler(MaskedTextBox);
							break;

						case "Date":
							System.Windows.Forms.DateTimePicker dt = new System.Windows.Forms.DateTimePicker();
							ctrl = dt;
							dt.Name = f.GetAttribute("DBColumnName").ToString();
							dt.Value = DateTime.Now;
							dt.Format = System.Windows.Forms.DateTimePickerFormat.Short;
							m_curPanel.Controls.Add(dt);
							break;

						case "Relation":
							System.Windows.Forms.ComboBox cb2 = new System.Windows.Forms.ComboBox();
							ctrl = cb2;
							cb2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
							cb2.Name = f.GetAttribute("DBColumnName").ToString();
							XmlNodeList vlist2 = m_dom.SelectNodes("descendant::Relation");
							XmlNodeList rellist = null;
							string str = null;
							foreach (XmlElement v in vlist2)
							{
								if (v.GetAttribute("LinkType").ToString() == elem.GetAttribute("FeatureClassName").ToString())
								{
									rellist = v.SelectNodes("descendant::Value");
									if (rellist.Count > 0)
										foreach (XmlElement r in rellist)
										{
											str = v.GetAttribute("FromType").ToString() + "-" + v.GetAttribute("ToType").ToString() + "-" + r.InnerText.ToString();
											cb2.Items.Add(str);
										}
									else //assume they are not using subtypes
										cb2.Items.Add(v.GetAttribute("FromType").ToString() + "-" + v.GetAttribute("ToType").ToString());
								}
							}
							//relations are always mandatory
							ctrl.BackColor = m_MandatoryColor;
							ctrl.Tag = "Mandatory";
							m_curPanel.Controls.Add(cb2);
							break;
					}

					if (f.GetAttribute("Mandatory").ToString() == "True")
					{
						ctrl.BackColor = m_MandatoryColor;
						ctrl.Tag = "Mandatory";
					}
				}
				ResizeForm();

				// set m_schEltClass
				XmlElement curElement = null;

				if (m_curPanel == Splitter.Panel1)
					curElement = m_curNode;
				else
					curElement = m_curLink;

				if (m_schEltClass == null)
					m_schEltClass = m_schEltClassCont.GetSchematicElementClass(curElement.GetAttribute("FeatureClassName"));
				else
					if (m_schEltClass.Name != curElement.GetAttribute("FeatureClassName"))
						m_schEltClass = m_schEltClassCont.GetSchematicElementClass(curElement.GetAttribute("FeatureClassName"));
			}

			catch (System.Exception e)
			{
				System.Windows.Forms.MessageBox.Show(e.Message);
			}
		}

		public bool CheckValidFeature(bool blnFromNode)
		{
			if (m_curLink == null)
				return false;

			// CR229717: check if relation finish with the good kind of node
			string sRelation = "";
			try  // If ComboBox does not exist, return an error
			{
				sRelation = ((ComboBox)this.Splitter.Panel2.Controls["Type"]).Text;
				if (sRelation == "")
					return false;

				if (blnFromNode)
					sRelation = sRelation.Substring(0, sRelation.IndexOf("-"));
				else
				{
					sRelation = sRelation.Substring(sRelation.IndexOf("-") + 1);
					if (sRelation.IndexOf("-") > 0)
						sRelation = sRelation.Substring(0, sRelation.IndexOf("-"));
				}
			}
			catch { }

			XmlNodeList fields = m_curLink.SelectNodes("descendant::Field");

			foreach (XmlElement field in fields)
			{
				if (field.GetAttribute("Type") == "Relation")
				{
					foreach (XmlElement rel in m_relations)
					{
						// CR229717: check if relation is for this kind of link
						if (rel.GetAttribute("LinkType") != this.cboLinkType.Text)
							continue;

						if (blnFromNode)
						{
							if (m_schematicFeature1 == null)
								return false;

							// CR229717: check if relation start with the good kind of node							
							if (sRelation != rel.GetAttribute("FromType"))
								continue;

							if (rel.GetAttribute("FromType") == m_schematicFeature1.SchematicElementClass.Name)
								return true;
						}
						else
						{
							if (m_schematicFeature2 == null)
								return false;

							// CR229717: check if relation finish with the good kind of node
							if (sRelation != rel.GetAttribute("ToType"))
								continue;

							if (rel.GetAttribute("ToType") == m_schematicFeature2.SchematicElementClass.Name)
								return true;
						}
					}
					return false;
				}
			}
			return true;
		}

		public void FillValue(ref ISchematicFeature schFeature)
		{
			try
			{
				if (m_schEltClass == null)
					throw new Exception("Error getting Feature Class");

				int fldIndex;

				foreach (System.Windows.Forms.Control ctrl in m_curPanel.Controls)
				{
					if (!((ctrl.GetType() == typeof(System.Windows.Forms.Label)) || (ctrl.Name == "cboNodeType")))
					{
						if ((ctrl.GetType() == typeof(System.Windows.Forms.TextBox)) || (ctrl.GetType() == typeof(System.Windows.Forms.ComboBox)))
						{
							if (ctrl.Text.Length > 0)
							{
								//insert in DB
								fldIndex = schFeature.Fields.FindField(ctrl.Name);
								if (fldIndex > -1)
								{
									schFeature.set_Value(fldIndex, ctrl.Text);
									schFeature.Store();
								}
							}
						}
						else if (ctrl.GetType() == typeof(System.Windows.Forms.DateTimePicker))
						{
							fldIndex = schFeature.Fields.FindField(ctrl.Name);
							if (fldIndex > -1)
							{
								schFeature.set_Value(fldIndex, ctrl.Text);
								schFeature.Store();
							}
						}
						else if (ctrl.GetType() == typeof(System.Windows.Forms.MaskedTextBox))
						{
							System.Windows.Forms.MaskedTextBox mctrl = (System.Windows.Forms.MaskedTextBox)ctrl;
							if ((mctrl.Text.Length > 0) && (mctrl.Modified) && (mctrl.MaskCompleted))
							{

								fldIndex = schFeature.Fields.FindField(ctrl.Name);
								if (fldIndex > -1)
								{
									schFeature.set_Value(fldIndex, ctrl.Text);
									schFeature.Store();
								}
							}
						}
					}
				}

				return;
			}

			catch (System.Exception e)
			{
				System.Windows.Forms.MessageBox.Show(e.Message);
			}
		}

		private void ResizeForm()
		{
			try
			{
				System.Windows.Forms.Control ctr2;
				Control[] ctrls = null;
				System.Drawing.Point p = new System.Drawing.Point();
				//handle panel 1
				foreach (System.Windows.Forms.Control ctr in Splitter.Panel1.Controls)
				{
					if ((ctr.Name.StartsWith("lbl")) && (ctr.Name.ToString() != "lblNodeLabel"))
					{
						ctrls = Splitter.Panel1.Controls.Find(ctr.Name.Substring(3), true);
						if (ctrls.GetLength(0) > 0)
						{
							ctr2 = ctrls[0];

							p.Y = ctr.Location.Y;
							p.X = ctr.Width + 3;
							ctr2.Location = p;
							ctr2.Width = Splitter.Panel1.Width - ctr.Width - 8;
						}
					}
				}

				Splitter.Panel1.Refresh();
				//handle panel 2
				foreach (System.Windows.Forms.Control ctr in Splitter.Panel2.Controls)
				{
					if ((ctr.Name.StartsWith("lbl")) && (ctr.Name.ToString() != "lblLinkLabel"))
					{
						ctrls = Splitter.Panel2.Controls.Find(ctr.Name.Substring(3), true);
						if (ctrls.GetLength(0) > 0)
						{
							ctr2 = ctrls[0];
							p.Y = ctr.Location.Y;
							p.X = ctr.Width + 3;
							ctr2.Location = p;
							ctr2.Width = Splitter.Panel2.Width - ctr.Width - 8;
						}
					}
				}

				Splitter.Panel2.Refresh();
			}
			catch (System.Exception e)
			{
				System.Windows.Forms.MessageBox.Show(e.Message);
			}
		}

		private void cboType_SelectedIndexChanged(Object sender, System.EventArgs e)
		{
			SelectionChanged();
		}

		public void cboLinkType_SelectedIndexChanged(Object sender, System.EventArgs e)
		{
			SelectionChanged();
		}

		private void MaskedTextBox(Object sender, System.EventArgs e)
		{
			System.Windows.Forms.MaskedTextBox mctrl = (System.Windows.Forms.MaskedTextBox)sender;
			mctrl.Modified = true;
		}

		/// <summary>
		/// m_createNode is true when the active panel is the one that digitize nodes.
		/// </summary>
		/// <returns></returns>
		public bool CreateNode()
		{
			return m_createNode;
		}

		public bool AutoClear()
		{
			return m_autoClear;
		}

		public bool IsClosed()
		{
			return m_isClosed;
		}

		public ISchematicElementClass FeatureClass()
		{
			return m_schEltClass;
		}

		public void x(int x)
		{
			m_x = x;
			return;
		}

		public void y(int y)
		{
			m_y = y;
			return;
		}

		public void SchematicFeature1(ISchematicFeature schematicFeature)
		{
			m_schematicFeature1 = schematicFeature;
			return;
		}

		public void SchematicFeature2(ISchematicFeature schematicFeature)
		{
			m_schematicFeature2 = schematicFeature;
			return;
		}

		public void FrmApplication(ESRI.ArcGIS.Framework.IApplication app)
		{
			m_app = app;
			return;
		}

		private void Splitter_Panel2_Click(object sender, EventArgs e)
		{
			if (m_digitCommand == null)
				m_digitCommand = CurrentDigitTool.CurrentTool.currentDigit;

			if (m_digitCommand == null)
				return;

			m_createNode = false;

			if (m_curPanel != Splitter.Panel2)
			{

				m_curPanel = Splitter.Panel2;
				foreach (System.Windows.Forms.Control ctrl in Splitter.Panel1.Controls)
					ctrl.Enabled = false;

				foreach (System.Windows.Forms.Control ctrl in Splitter.Panel2.Controls)
					ctrl.Enabled = true;

				lblMode.Text = "Create Link";

				if (m_schematicFeature1 != null)
				{
					m_schematicFeature1 = null;
					m_digitCommand.SchematicFeature1(m_schematicFeature1);
				}

				if (m_schematicFeature2 != null)
				{
					m_schematicFeature2 = null;
					m_digitCommand.SchematicFeature2(m_schematicFeature2);
				}

				if (m_curPanel == Splitter.Panel1)
				{
					btnOKPanel1.Visible = false;
					ErrorProvider1.SetError(btnOKPanel1, "");
				}
				else
				{
					btnOKPanel2.Visible = false;
					ErrorProvider1.SetError(btnOKPanel2, "");
				}

				System.EventArgs e2 = new System.EventArgs();
				cboLinkType_SelectedIndexChanged(sender, e2);

			}

		}

		private void Splitter_Panel1_Click(object sender, EventArgs e)
		{
			if (m_digitCommand == null)
				m_digitCommand = CurrentDigitTool.CurrentTool.currentDigit;

			if (m_digitCommand != null)
				m_digitCommand.EndFeedBack();

			m_createNode = true;

			if (m_curPanel != Splitter.Panel1 || m_clickPanel == false)
			{
				m_clickPanel = true;
				m_curPanel = Splitter.Panel1;

				foreach (System.Windows.Forms.Control ctrl in Splitter.Panel2.Controls)
					ctrl.Enabled = false;

				foreach (System.Windows.Forms.Control ctrl in Splitter.Panel1.Controls)
					ctrl.Enabled = true;

				lblMode.Text = "Create Node";

				if (m_curPanel == Splitter.Panel1)
				{
					btnOKPanel1.Visible = false;
					ErrorProvider1.SetError(btnOKPanel1, "");
				}
				else
				{
					btnOKPanel2.Visible = false;
					ErrorProvider1.SetError(btnOKPanel2, "");
				}
				System.EventArgs e2 = new System.EventArgs();
				cboType_SelectedIndexChanged(sender, e2);
			}
		}

		private void btnOKPanel1_Click(object sender, EventArgs e)
		{
			//try to create the node at the original point
			if (m_digitCommand != null)
				m_digitCommand.MyMouseUp(m_x, m_y);

			ErrorProvider1.SetError(btnOKPanel1, "");
			btnOKPanel1.Visible = false;
		}

		private void btnOKPanel2_Click(object sender, EventArgs e)
		{
			//try to create the link with the original points
			if (m_digitCommand != null)
				m_digitCommand.MyMouseUp(m_x, m_y);

			ErrorProvider1.SetError(btnOKPanel1, "");
			btnOKPanel1.Visible = false;
		}

		private void WindowVisibleChange(object sender, EventArgs e)
		{

			if (m_digitCommand == null)
				m_digitCommand = CurrentDigitTool.CurrentTool.currentDigit;

			if (m_digitCommand == null)
				return;

			if ((this.Visible) && !CurrentDigitTool.CurrentTool.currentDockableWindow.IsVisible())
			{
				if (!(m_digitCommand.FromDeactivate()))
				{
					m_digitCommand.DeactivatedFromDock(true);

					ESRI.ArcGIS.Framework.IApplication app = (ESRI.ArcGIS.Framework.IApplication)this.Hook;
					app.CurrentTool = null;
				}
			}
			m_digitCommand.EndFeedBack();
			m_digitCommand.FromDeactivate(false);
		}

		private void DigitWindow_Resize(object sender, EventArgs e)
		{
			if (!m_loading)
				ResizeForm();
		}


		//IsRelative return true when the path start with "."
		private bool IsRelative(string path)
		{
			bool startwith = path.StartsWith(".");
			return startwith;
		}

	}
}
