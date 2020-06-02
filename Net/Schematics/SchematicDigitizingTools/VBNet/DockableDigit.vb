'Copyright 2019 Esri

'Licensed under the Apache License, Version 2.0 (the "License");
'you may not use this file except in compliance with the License.
'You may obtain a copy of the License at

'    http://www.apache.org/licenses/LICENSE-2.0

'Unless required by applicable law or agreed to in writing, software
'distributed under the License is distributed on an "AS IS" BASIS,
'WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.

'See the License for the specific language governing permissions and
'limitations under the License.
Imports System.Xml
Imports ESRI.ArcGIS.Schematic
Imports ESRI.ArcGIS.esriSystem
Imports System.Windows.Forms

''' <summary>
''' Designer class of the dockable window add-in. It contains user interfaces that
''' make up the dockable window.
''' </summary>
Public Class DigitDockableWindow

	Private m_digitCommand As DigitTool

	Dim m_dom As XmlDocument = Nothing
	Dim m_loading As Boolean = True
	Dim m_clickPanel As Boolean = False
	Dim m_curfrmWidth As Long
	Friend WithEvents m_Panel1 As System.Windows.Forms.SplitterPanel
	Friend WithEvents m_Panel2 As System.Windows.Forms.SplitterPanel
	Dim m_curPanel As System.Windows.Forms.SplitterPanel
	Dim m_mandatoryColor As System.Drawing.Color = Drawing.Color.White

	Private m_schematicLayer As ISchematicLayer
	Private m_schematicFeature1 As ISchematicFeature = Nothing
	Private m_schematicFeature2 As ISchematicFeature = Nothing
	Private m_createNode As Boolean = True 'update when click on panel and on new

	'For button OK to retrieve the coordinate of the digit feature
	Private m_x As Integer
	Private m_y As Integer

	Private m_curLink As XmlElement = Nothing
	Private m_curNode As XmlElement = Nothing
	Private m_relations As XmlNodeList = Nothing
	Private m_schDataset As ISchematicDataset = Nothing
	Private m_schEltClassCont As ISchematicElementClassContainer = Nothing
	Private m_schEltClass As ISchematicElementClass = Nothing
	Private m_schematicInMemoryDiagram As ISchematicInMemoryDiagram = Nothing
	Private m_autoClear As Boolean = False
	Private m_schematicExtension As ESRI.ArcGIS.esriSystem.IExtension

	Public Sub New(ByVal hook As Object)

		' This call is required by the Windows Form Designer.
		InitializeComponent()

		' Add any initialization after the InitializeComponent() call.
		Me.Hook = hook

	End Sub



	Private m_hook As Object
	''' <summary>
	''' Host object of the dockable window
	''' </summary> 
	Public Property Hook() As Object
		Get
			Return m_hook
		End Get
		Set(ByVal value As Object)
			m_hook = value
		End Set
	End Property

	''' <summary>
	''' Implementation class of the dockable window add-in. It is responsible for
	''' creating and disposing the user interface class for the dockable window.
	''' </summary>
	Public Class AddinImpl
		Inherits ESRI.ArcGIS.Desktop.AddIns.DockableWindow

		Private m_windowUI As DigitDockableWindow

		Protected Overrides Function OnCreateChild() As System.IntPtr
			m_windowUI = New DigitDockableWindow(Me.Hook)

			CurrentDigitTool.CurrentTool.digitDockableWindow = m_windowUI

			If (CurrentDigitTool.CurrentTool.currentDigit IsNot Nothing) Then
				m_windowUI.m_digitCommand = CurrentDigitTool.CurrentTool.currentDigit
				m_windowUI.m_digitCommand.m_dockableDigit = m_windowUI
			Else
				' CurrentDigitTool.CurrentTool.CurrentDigit is null when we open ArcMap, but OnCreateChild
				' is called if the dockable window was shown during the last ArcMap session.
				Dim windowID As UID = New UIDClass
				windowID.Value = "DigitTool_DockableWindowVB"
				CurrentDigitTool.CurrentTool.currentDockableWindow = My.ArcMap.DockableWindowManager.GetDockableWindow(windowID)
			End If

			Return m_windowUI.Handle
		End Function

		Protected Overrides Sub Dispose(ByVal Param As Boolean)
			If m_windowUI IsNot Nothing Then
				m_windowUI.Dispose(Param)
			End If

			MyBase.Dispose(Param)

		End Sub

	End Class


	Public Sub Init(ByVal schematicLayer As ISchematicLayer)
		' CR229717: Lost the ElementClass if the dockable window is deactivate
		If (schematicLayer Is m_schematicLayer) Then
			If (m_schEltClass IsNot Nothing) Then
				Return
			End If
		End If

		Try

			If schematicLayer Is Nothing Then
				Return
			End If

			m_schematicLayer = schematicLayer
			Dim col As XmlNode = Nothing
			Dim myString As String = Nothing

			m_schDataset = schematicLayer.SchematicDiagram.SchematicDiagramClass.SchematicDataset

			m_schEltClassCont = m_schDataset
			m_schematicInMemoryDiagram = schematicLayer.SchematicInMemoryDiagram

			m_dom = New XmlDocument

			Dim schematicDiagram As ISchematicDiagram
			schematicDiagram = m_schematicInMemoryDiagram.SchematicDiagram

			' get the path of the xml file that contains the definitions of the digitize dockable window
			Dim path As String

			Dim schematicDiagramClass As ISchematicDiagramClass = schematicDiagram.SchematicDiagramClass
			Dim schematicAttributeContainer As ISchematicAttributeContainer = schematicDiagramClass

			Dim schematicAttribute As ISchematicAttribute
			schematicAttribute = schematicAttributeContainer.GetSchematicAttribute("DigitizePropertiesLocation", True)

			If (schematicAttribute Is Nothing) Then
				MsgBox("Need an attribute named DigitizePropertiesLocation in the corresponding DiagramTemplate attributes")
				Return
			End If

			path = schematicAttribute.GetValue(schematicDiagram)


			If IsRelative(path) Then 'Concat the workspace's path with this path
				'current workspace path
				Dim myDataset As ISchematicDataset = schematicDiagramClass.SchematicDataset
				If Not myDataset Is Nothing Then
					Dim mySchematicWorkspace As ISchematicWorkspace = myDataset.SchematicWorkspace
					If Not mySchematicWorkspace Is Nothing Then
						Dim myWorkspace As ESRI.ArcGIS.Geodatabase.IWorkspace = mySchematicWorkspace.Workspace
						If Not myWorkspace Is Nothing Then
							Dim workspacePath As String = myWorkspace.PathName
							'add "..\" to path to step back one level...
							Dim stepBack As String = "..\"
							path = stepBack + path

							path = System.IO.Path.Combine(workspacePath, path)

						End If
					End If
				End If
			End If
			'else keep the original hard path

			Dim reader As System.Xml.XmlReader = System.Xml.XmlReader.Create(path)

			m_dom.Load(reader)

			'Load Nodes
			Dim nodes As XmlNodeList = Nothing
			nodes = m_dom.SelectNodes("descendant::NodeFeature")

			cboNodeType.Items.Clear()
			Dim node As XmlElement = Nothing
			For Each node In nodes
				Me.cboNodeType.Items.Add(node.GetAttribute("FeatureClassName").ToString)
			Next

			'Load Links
			Dim links As XmlNodeList = Nothing
			links = m_dom.SelectNodes("descendant::LinkFeature")

			cboLinkType.Items.Clear()
			Dim link As XmlElement = Nothing
			For Each link In links
				Me.cboLinkType.Items.Add(link.GetAttribute("FeatureClassName").ToString)
			Next

			col = m_dom.SelectSingleNode("descendant::MandatoryColor")
			If Not col Is Nothing Then
				myString = "System.Drawing."
				myString = col.InnerText.ToString
				m_mandatoryColor = System.Drawing.Color.FromName(myString)
			End If

			col = m_dom.SelectSingleNode("descendant::FormName")
			If Not col Is Nothing Then
				myString = col.InnerText.ToString
				Me.Text = myString
			End If


			Dim rels As XmlNodeList = m_dom.SelectNodes("descendant::Relation")
			If rels.Count > 0 Then
				m_relations = rels
			End If

			col = m_dom.SelectSingleNode("descendant::AutoClearAfterCreate")
			If Not col Is Nothing Then
				If col.InnerText.ToString = "True" Then
					m_autoClear = True
				End If
			End If
		Catch ex As Exception

		End Try

		m_Panel1 = Splitter.Panel1
		m_Panel2 = Splitter.Panel2
		m_curPanel = Splitter.Panel1
		lblMode.Text = "Create Node"
		m_loading = False
		m_clickPanel = False
		m_schEltClass = Nothing

	End Sub


	Public Function ValidateFields() As Boolean
		Dim blnValidated As Boolean = True
		Dim ctrl As Windows.Forms.Control = Nothing
		Dim mctrl As Windows.Forms.MaskedTextBox = Nothing
		Dim errors As String = ""
		Dim linkTypeChoice As String = ""
		Dim firstime As Boolean = True

		'check all mandatory fields
		For Each ctrl In m_curPanel.Controls
			If TypeOf ctrl Is Windows.Forms.Label Then
				'ignore labels
			Else
				If ctrl.Tag = "Mandatory" Then
					If TypeOf ctrl Is Windows.Forms.MaskedTextBox Then
						mctrl = ctrl
						If mctrl.MaskCompleted = False Then
							blnValidated = False
							If errors.Length > 0 Then
								errors = "Incomplete mandatory field" & Environment.NewLine + "Complete missing data and click on OK button"
							Else
								errors = errors & vbCrLf & "Incomplete mandatory field" & Environment.NewLine + "Complete missing data and click on OK button"
							End If
						End If
					Else
						If Not ctrl.Text.Length > 0 Then
							blnValidated = False
							If errors.Length > 0 Then
								errors = "Incomplete mandatory field" & Environment.NewLine + "Complete missing data and click on OK button"
							Else
								errors = errors & vbCrLf & "Incomplete mandatory field" & Environment.NewLine + "Complete missing data and click on OK button"
							End If
						End If
					End If
				End If

				'check masked edit controls
				If TypeOf ctrl Is Windows.Forms.MaskedTextBox Then
					mctrl = ctrl
					'if they typed something, but didn't complete it, then error
					'if they typed nothing and it is not mandatory, then it is OK
					If mctrl.Text.Length > 0 Then
						If mctrl.Modified = True Then
							If mctrl.MaskCompleted = False Then
								blnValidated = False
								If errors.Length > 0 Then
									errors = "Invalid entry in a masked text field"
								Else
									errors = errors & vbCrLf & "Invalid entry in a masked text field"
								End If
							End If
						End If
					End If
				End If
				'End If

				'check link connections
				If m_curPanel Is Splitter.Panel2 Then
					'make sure that the relation is correct if it exists
					Dim fields As XmlNodeList = m_curLink.SelectNodes("descendant::Field")
					Dim field As XmlElement = Nothing
					For Each field In fields
						'find the field with a type of "Relation"
						If field.GetAttribute("Type") = "Relation" Then
							Dim rel As XmlElement = Nothing

							Dim dataset1 As ESRI.ArcGIS.Geodatabase.IDataset
							Dim dataset2 As ESRI.ArcGIS.Geodatabase.IDataset

							Dim FeatureClass1Name As String
							Dim FeatureClass2Name As String

							dataset1 = m_schematicFeature1.SchematicElementClass
							dataset2 = m_schematicFeature2.SchematicElementClass

							FeatureClass1Name = dataset1.Name
							FeatureClass2Name = dataset2.Name

							For Each rel In m_relations
								'loop through the xml relations to match based on the from node and to node types

								If rel.GetAttribute("FromType") = FeatureClass1Name And rel.GetAttribute("ToType") = FeatureClass2Name Then
									'find the control with the pick list for relationships
									Dim ctrls() As Windows.Forms.Control = m_curPanel.Controls.Find(field.GetAttribute("DBColumnName"), True)
									If ctrls.Length > 0 Then
										ctrl = ctrls(0)
									End If
									Dim vals As XmlNodeList = rel.SelectNodes("descendant::Value")
									Dim val As XmlElement = Nothing
									Dim myString As String = rel.GetAttribute("FromType") & "-" & rel.GetAttribute("ToType")
									Dim linkTypeClicking As String = myString
									'validate that the current control string is correct
									'if there are values, use them
									Dim blnfound As Boolean = False
									If vals.Count > 0 Then
										For Each val In vals
											linkTypeClicking = myString + "-" + val.InnerText.ToString()

											If myString & "-" & val.InnerText.ToString = ctrl.Text Then
												blnfound = True
												Exit For
											Else
												blnfound = False
												If firstime = True Then
													linkTypeChoice = ctrl.Text
													firstime = False
												End If
											End If
										Next
										If blnfound = False Then
											blnValidated = False
											If errors.Length > 0 Then
												errors = "Invalid link connection because :"
												errors = errors & vbCrLf & "Type's link clicked : " & linkTypeClicking
												errors = errors & vbCrLf & "Type's link chosen : " & linkTypeChoice
											Else
												errors = errors & vbCrLf & "Invalid link connection because :"
												errors = errors & vbCrLf & "Type's link clicked : " & linkTypeClicking
												errors = errors & vbCrLf & "Type's link chosen : " & linkTypeChoice
											End If
										End If
									Else
										If ctrl.Text <> myString Then
											If (firstime) Then
												linkTypeChoice = ctrl.Text
												firstime = False
											End If

											blnValidated = False
											If errors.Length > 0 Then
												errors = "Invalid link connection because :"
												errors = errors & vbCrLf & "Type's link clicked : " & linkTypeClicking
												errors = errors & vbCrLf & "Type's link chosen : " & linkTypeChoice
											Else
												errors = errors & vbCrLf & "Invalid link connection because :"
												errors = errors & vbCrLf & "Type's link clicked : " & linkTypeClicking
												errors = errors & vbCrLf & "Type's link chosen : " & linkTypeChoice
											End If
										Else
											blnfound = True
										End If
									End If

									If blnfound = False Then 'fix connection list
										Dim vlist As XmlNodeList = m_dom.SelectNodes("descendant::Relation")
										Dim v As XmlElement
										Dim rellist As XmlNodeList = Nothing
										Dim r As XmlElement = Nothing
										Dim cboconn As Windows.Forms.ComboBox = ctrl
										cboconn.Items.Clear()
										For Each v In vlist
											If v.GetAttribute("LinkType").ToString = m_curLink.GetAttribute("FeatureClassName").ToString Then
												'make sure the node types are ok
												If v.GetAttribute("FromType").ToString() = FeatureClass1Name OrElse v.GetAttribute("ToType").ToString() = FeatureClass2Name Then
													rellist = v.SelectNodes("descendant::Value")
													If rellist.Count > 0 Then		 '
														For Each r In rellist
															myString = v.GetAttribute("FromType").ToString() + "-" + v.GetAttribute("ToType").ToString() & "-" & r.InnerText.ToString
															cboconn.Items.Add(myString)
														Next
													Else 'assume they are not using subtypes
														cboconn.Items.Add(v.GetAttribute("FromType").ToString() & "-" & v.GetAttribute("ToType").ToString())
													End If
												End If
											End If
										Next
									End If
								End If
							Next
						End If
					Next
				End If
			End If
		Next

		If errors.Length > 0 Then

			If m_curPanel Is Splitter.Panel1 Then
				btnOKPanel1.Visible = True

				ErrorProvider1.SetError(btnOKPanel1, errors)
			Else
				btnOKPanel2.Visible = True
				ErrorProvider1.SetError(btnOKPanel2, errors)
			End If
		End If
		Return blnValidated
	End Function

	Public Sub cboType_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboNodeType.SelectedValueChanged
		SelectionChanged()
	End Sub


	Public Sub SelectionChanged()
		Try
			Dim ctrl As System.Windows.Forms.Control = Nothing
			Dim ctrl2 As System.Windows.Forms.Control = Nothing
			Dim ctrls() As Object = Nothing
			Dim ctrlstoremove As Collection = New Collection
			Dim labelName As String = ""
			Dim featureClass As String = ""
			Dim cbo As Windows.Forms.ComboBox = Nothing
			Dim lblMain As Windows.Forms.Label = Nothing

			If m_digitCommand Is Nothing Then
				Return
			End If

			'clear any current elements
			If Not m_schematicFeature1 Is Nothing Then
				m_schematicFeature1 = Nothing
				m_digitCommand.SchematicFeature1() = m_schematicFeature1
			End If
			If Not m_schematicFeature2 Is Nothing Then
				m_schematicFeature2 = Nothing
				m_digitCommand.SchematicFeature2() = m_schematicFeature2
			End If

			If m_curPanel Is Splitter.Panel1 Then
				labelName = "lblNodeLabel"
				featureClass = "descendant::NodeFeature"
				cbo = cboNodeType
				lblMain = lblNodeLabel
			Else
				labelName = "lblLinkLabel"
				featureClass = "descendant::LinkFeature"
				cbo = cboLinkType
				lblMain = lblLinkLabel
			End If

			For Each ctrl In m_curPanel.Controls
				If ctrl.Name.StartsWith("lbl") And ctrl.Name.ToString <> labelName Then
					ctrls = m_curPanel.Controls.Find(ctrl.Name.Substring(3), True)
					ctrl2 = ctrls(0)
					ctrlstoremove.Add(ctrl)
					ctrlstoremove.Add(ctrl2)
				End If
			Next

			If ctrlstoremove.Count > 0 Then
				Dim ctrol As System.Windows.Forms.Control
				For Each ctrol In ctrlstoremove
					m_curPanel.Controls.Remove(ctrol)
					ctrol = Nothing
				Next
			End If

			Dim elem As XmlElement = Nothing
			Dim elems As XmlNodeList = Nothing
			m_curfrmWidth = m_curPanel.Width
			elems = m_dom.SelectNodes(featureClass)

			Dim blnFound As Boolean = False
			For Each elem In elems
				If elem.GetAttribute("FeatureClassName").ToString = cbo.Text.ToString Then
					blnFound = True
					Exit For
				End If
			Next
			If blnFound = False Then
				'  CR229717: If this is deactivate, lost the Schematic ElementClass and can not retrieve it
				' m_schEltClass = Nothing
				Exit Sub
			End If

			If m_curPanel Is Splitter.Panel1 Then
				m_curNode = elem
			Else
				m_curLink = elem
			End If

			'set grid
			elems = elem.SelectNodes("descendant::Field")
			Dim f As XmlElement
			Dim x As Integer = Splitter.Location.X
			Dim y As Integer = lblMain.Location.Y + lblMain.Height + 5

			Dim p As New System.Drawing.Point

			Dim rcount As Integer = 1
			For Each f In elems
				Dim lbl As New System.Windows.Forms.Label
				lbl.Name = "lbl" & f.GetAttribute("DBColumnName").ToString
				lbl.Text = f.GetAttribute("DisplayName").ToString
				lbl.AutoSize = True
				m_curPanel.Controls.Add(lbl)
				p.X = 3
				p.Y = y
				lbl.Location = p
				y = y + lbl.Height + 10
				Select Case f.GetAttribute("Type").ToString
					Case "Text"
						Dim tx As New System.Windows.Forms.TextBox
						ctrl = tx
						tx.Name = f.GetAttribute("DBColumnName").ToString
						If f.GetAttribute("Length").Length > 0 Then
							tx.MaxLength = CInt(f.GetAttribute("Length"))
						End If
						If f.GetAttribute("Default").Length > 0 Then
							tx.Text = f.GetAttribute("Default")
						End If
						m_curPanel.Controls.Add(tx)

					Case "Combo"
						Dim cb As New System.Windows.Forms.ComboBox
						Dim defaulttext As String = ""
						ctrl = cb
						cb.DropDownStyle = Windows.Forms.ComboBoxStyle.DropDownList
						cb.Name = f.GetAttribute("DBColumnName").ToString
						Dim vlist As XmlNodeList = f.SelectNodes("descendant::Value")
						Dim v As XmlElement
						For Each v In vlist
							cb.Items.Add(v.InnerText.ToString)
							If v.GetAttribute("Default").Length > 0 Then
								defaulttext = v.InnerText
							End If
						Next
						If defaulttext.Length > 0 Then
							cb.Text = defaulttext
						End If
						m_curPanel.Controls.Add(cb)

					Case "MaskText"
						Dim MaskControl As New System.Windows.Forms.MaskedTextBox
						ctrl = MaskControl
						Dim mask As String = ""
						MaskControl.Name = f.GetAttribute("DBColumnName").ToString
						If f.GetAttribute("Mask").Length > 0 Then
							mask = f.GetAttribute("Mask")
						Else
							mask = ""
						End If
						MaskControl.Mask = mask
						If f.GetAttribute("Default").Length > 0 Then
							MaskControl.Text = f.GetAttribute("Default")
						End If
						MaskControl.Modified = False
						m_curPanel.Controls.Add(MaskControl)
						AddHandler MaskControl.TextChanged, AddressOf MaskedTextBox

					Case "Number"
						Dim MaskControl As New System.Windows.Forms.MaskedTextBox
						ctrl = MaskControl
						Dim mask As String = ""
						MaskControl.Name = f.GetAttribute("DBColumnName").ToString
						Dim i As Int16 = 1
						If f.GetAttribute("Length").Length > 0 Then
							For i = 1 To CInt(f.GetAttribute("Length"))
								mask = mask & "9"
							Next
						Else
							If f.GetAttribute("Mask").Length > 0 Then
								mask = f.GetAttribute("Mask")
							Else
								mask = ""
							End If
						End If
						MaskControl.Mask = mask
						If f.GetAttribute("Default").Length > 0 Then
							MaskControl.Text = CInt(f.GetAttribute("Default"))
						End If
						MaskControl.Modified = False
						m_curPanel.Controls.Add(MaskControl)
						AddHandler MaskControl.TextChanged, AddressOf MaskedTextBox

					Case "Date"
						Dim dt As New System.Windows.Forms.DateTimePicker
						ctrl = dt
						dt.Name = f.GetAttribute("DBColumnName").ToString
						dt.Value = Now.Date
						dt.Format = Windows.Forms.DateTimePickerFormat.Short
						m_curPanel.Controls.Add(dt)

					Case "Relation"
						Dim cb As New System.Windows.Forms.ComboBox
						ctrl = cb
						cb.DropDownStyle = Windows.Forms.ComboBoxStyle.DropDownList
						cb.Name = f.GetAttribute("DBColumnName").ToString
						Dim vlist As XmlNodeList = m_dom.SelectNodes("descendant::Relation")
						Dim v As XmlElement
						Dim rellist As XmlNodeList = Nothing
						Dim r As XmlElement = Nothing
						Dim myString As String = Nothing
						For Each v In vlist
							If v.GetAttribute("LinkType").ToString = elem.GetAttribute("FeatureClassName").ToString Then
								rellist = v.SelectNodes("descendant::Value")
								If rellist.Count > 0 Then		 '
									For Each r In rellist
										myString = v.GetAttribute("FromType").ToString & "-" & v.GetAttribute("ToType").ToString & "-" & r.InnerText.ToString
										cb.Items.Add(myString)
									Next
								Else 'assume they are not using subtypes
									cb.Items.Add(v.GetAttribute("FromType").ToString & "-" & v.GetAttribute("ToType").ToString)
								End If
							End If
						Next
						'relations are always mandatory
						ctrl.BackColor = m_mandatoryColor
						ctrl.Tag = "Mandatory"
						m_curPanel.Controls.Add(cb)

				End Select
				If f.GetAttribute("Mandatory").ToString = "True" Then
					ctrl.BackColor = m_mandatoryColor
					ctrl.Tag = "Mandatory"
				End If
			Next
			ResizeForm()

			' set m_schEltClass
			Dim schElement As ISchematicElement = Nothing
			Dim curElement As XmlElement = Nothing

			If m_curPanel Is Splitter.Panel1 Then
				curElement = m_curNode
			Else
				curElement = m_curLink
			End If

			If m_schEltClass Is Nothing Then
				m_schEltClass = m_schEltClassCont.GetSchematicElementClass(curElement.GetAttribute("FeatureClassName"))
			Else
				If m_schEltClass.Name <> curElement.GetAttribute("FeatureClassName") Then
					m_schEltClass = m_schEltClassCont.GetSchematicElementClass(curElement.GetAttribute("FeatureClassName"))
				End If
			End If


		Catch ex As Exception
			MsgBox(ex.Message)
		End Try
	End Sub


	Public Function CheckValidFeature(ByVal blnFromNode As Boolean) As Boolean

		If m_curLink Is Nothing Then
			Return False
		End If

		' CR229717: check if relation finish with the good kind of node
		Dim sRelation As String = ""
		Try	 ' If ComboBox does not exist, return an error
			sRelation = (CType(Me.Splitter.Panel2.Controls("Type"), ComboBox)).Text

			If (sRelation = "") Then Return False

			If (blnFromNode) Then
				sRelation = sRelation.Substring(0, sRelation.IndexOf("-"))
			Else
				sRelation = sRelation.Substring(sRelation.IndexOf("-") + 1)
				If (sRelation.IndexOf("-") > 0) Then sRelation = sRelation.Substring(0, sRelation.IndexOf("-"))
			End If
		Catch
		End Try

		Dim fields As XmlNodeList = m_curLink.SelectNodes("descendant::Field")
		Dim field As XmlElement = Nothing
		For Each field In fields
			If field.GetAttribute("Type") = "Relation" Then
				Dim rel As XmlElement = Nothing
				For Each rel In m_relations

					' CR229717: check if relation is for this kind of link
					If (rel.GetAttribute("LinkType") <> Me.cboLinkType.Text) Then Continue For

					If blnFromNode Then
						If m_schematicFeature1 Is Nothing Then
							Return False
						End If

						' CR229717: check if relation start with the good kind of node							
						If (sRelation <> rel.GetAttribute("FromType")) Then Continue For

						If rel.GetAttribute("FromType") = m_schematicFeature1.SchematicElementClass.Name Then
							Return True
						End If
					Else
						If m_schematicFeature2 Is Nothing Then
							Return False
						End If

						' CR229717: check if relation finish with the good kind of node							
						If (sRelation <> rel.GetAttribute("ToType")) Then Continue For

						If rel.GetAttribute("ToType") = m_schematicFeature2.SchematicElementClass.Name Then
							Return True
						End If
					End If
				Next
				Return False
			End If
		Next
		Return True
	End Function

	Private Sub frmDigitize_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
		If m_loading = False Then
			ResizeForm()
		End If
	End Sub

	Private Sub ResizeForm()
		Try
			Dim ctr As System.Windows.Forms.Control
			Dim ctr2 As System.Windows.Forms.Control
			Dim ctrls() As Object
			Dim p As System.Drawing.Point
			'handle panel 1
			For Each ctr In Splitter.Panel1.Controls
				If ctr.Name.StartsWith("lbl") And ctr.Name.ToString <> "lblNodeLabel" Then
					'ctr.Width = Splitter.panel1.Width / 3
					'MsgBox(ctr.Name.Substring(3))
					ctrls = Splitter.Panel1.Controls.Find(ctr.Name.Substring(3), True)
					If ctrls.GetLength(0) > 0 Then
						ctr2 = ctrls(0)

						p.Y = ctr.Location.Y
						p.X = ctr.Width + 3
						ctr2.Location = p
						ctr2.Width = Splitter.Panel1.Width - ctr.Width - 5
					End If
				End If
			Next
			Splitter.Panel1.Refresh()
			'handle panel 2
			For Each ctr In Splitter.Panel2.Controls
				If ctr.Name.StartsWith("lbl") And ctr.Name.ToString <> "lblLinkLabel" Then
					'ctr.Width = Splitter.panel1.Width / 3
					'MsgBox(ctr.Name.Substring(3))
					ctrls = Splitter.Panel2.Controls.Find(ctr.Name.Substring(3), True)
					If ctrls.GetLength(0) > 0 Then
						ctr2 = ctrls(0)
						p.Y = ctr.Location.Y
						p.X = ctr.Width + 3
						ctr2.Location = p
						ctr2.Width = Splitter.Panel2.Width - ctr.Width - 5
					End If
				End If
			Next
			Splitter.Panel2.Refresh()
		Catch ex As Exception
			MsgBox(ex.Message)
		End Try
	End Sub

	Private Sub cboLinkType_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboLinkType.SelectedValueChanged
		SelectionChanged()
	End Sub

	Public Sub FillValue(ByRef schFeature As ISchematicFeature)

		Try

			If m_schEltClass Is Nothing Then
				Err.Raise(513, "CreateNewFeature", "Error getting Feature Class")
			End If

			Dim fldIndex As Int16 = Nothing

			Dim ctrl As Windows.Forms.Control = Nothing
			For Each ctrl In m_curPanel.Controls
				If TypeOf ctrl Is Windows.Forms.Label Or ctrl.Name = "cboNodeType" Then
					'do nothing
				Else
					If TypeOf ctrl Is Windows.Forms.TextBox Or TypeOf ctrl Is Windows.Forms.ComboBox Then
						If ctrl.Text.Length > 0 Then
							'insert in DB
							fldIndex = schFeature.Fields.FindField(ctrl.Name)
							If (fldIndex > -1) Then
								schFeature.Value(fldIndex) = ctrl.Text
								schFeature.Store()
							End If
						End If
					ElseIf TypeOf ctrl Is Windows.Forms.DateTimePicker Then
						fldIndex = schFeature.Fields.FindField(ctrl.Name)
						If (fldIndex > -1) Then
							schFeature.Value(fldIndex) = ctrl.Text
							schFeature.Store()
						End If

					ElseIf TypeOf ctrl Is Windows.Forms.MaskedTextBox Then
						Dim mctrl As Windows.Forms.MaskedTextBox = ctrl
						If mctrl.Text.Length > 0 Then
							If mctrl.Modified = True Then
								If mctrl.MaskCompleted = True Then
									fldIndex = schFeature.Fields.FindField(ctrl.Name)
									If (fldIndex > -1) Then
										schFeature.Value(fldIndex) = ctrl.Text
										schFeature.Store()
									End If
								End If
							End If
						End If

					End If

				End If
			Next

			Return

		Catch ex As Exception
			MsgBox(ex.Message)
		End Try
	End Sub


	Private Sub btnOKPanel1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOKPanel1.Click
		'try to create the node at the original point
		If Not m_digitCommand Is Nothing Then
			m_digitCommand.MyMouseUp(m_x, m_y)
		End If

		ErrorProvider1.SetError(btnOKPanel1, "")
		btnOKPanel1.Visible = False
	End Sub

	Private Sub btnOKPanel2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOKPanel2.Click
		'try to create the link with the original points
		m_digitCommand.MyMouseUp(m_x, m_y)

		ErrorProvider1.SetError(btnOKPanel1, "")
		btnOKPanel1.Visible = False
	End Sub

	Private Sub MaskedTextBox(ByVal sender As Object, ByVal e As EventArgs)
		Dim mctrl As Windows.Forms.MaskedTextBox = sender
		mctrl.Modified = True
	End Sub

	Private Sub WindowVisibleChange() Handles Me.VisibleChanged

		If (m_digitCommand Is Nothing) Then
			m_digitCommand = CurrentDigitTool.CurrentTool.currentDigit
		End If

		If (m_digitCommand Is Nothing) Then
			Return
		End If

		If Me.Visible = True And CurrentDigitTool.CurrentTool.currentDockableWindow.IsVisible() = False Then
			If m_digitCommand.FromDeactivate = False Then
				m_digitCommand.DeactivatedFromDock = True

				Dim app As ESRI.ArcGIS.Framework.IApplication = Me.Hook
				app.CurrentTool = Nothing
			End If
		End If

		m_digitCommand.EndFeedBack()
		m_digitCommand.FromDeactivate = False

	End Sub

	Friend ReadOnly Property CreateNode() As Boolean
		Get
			Return m_createNode
		End Get
	End Property

	Friend ReadOnly Property AutoClear() As Boolean
		Get
			Return m_autoClear
		End Get
	End Property

	Friend ReadOnly Property FeatureClass() As ISchematicElementClass
		Get
			Return m_schEltClass
		End Get
	End Property

	Public WriteOnly Property x() As Integer
		Set(ByVal Value As Integer)
			m_x = Value
		End Set
	End Property

	Public WriteOnly Property y() As Integer
		Set(ByVal Value As Integer)
			m_y = Value
		End Set
	End Property

	Public WriteOnly Property SchematicFeature1() As ISchematicFeature
		Set(ByVal Value As ISchematicFeature)
			m_schematicFeature1 = Value
		End Set
	End Property

	Public WriteOnly Property SchematicFeature2() As ISchematicFeature
		Set(ByVal Value As ISchematicFeature)
			m_schematicFeature2 = Value
		End Set
	End Property

	'IsRelative return true when the path start with "."
	Private Function IsRelative(ByVal path As String) As Boolean
		If path(0) = "." Then
			Return True
		Else
			Return False
		End If
	End Function

	Private Sub cboNodeType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboNodeType.SelectedIndexChanged
		SelectionChanged()
	End Sub


	Private Sub Splitter_Panel1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Splitter.Panel1.Click
		If (m_digitCommand Is Nothing) Then
			m_digitCommand = CurrentDigitTool.CurrentTool.currentDigit
		End If

		If Not m_digitCommand Is Nothing Then
			m_digitCommand.EndFeedBack()
		End If

		m_createNode = True

		If (Not m_curPanel Is Splitter.Panel1) Or (m_clickPanel = False) Then

			m_clickPanel = True

			Dim ctrl As Windows.Forms.Control
			m_curPanel = Splitter.Panel1
			For Each ctrl In Splitter.Panel2.Controls
				ctrl.Enabled = False
			Next
			For Each ctrl In Splitter.Panel1.Controls
				ctrl.Enabled = True
			Next
			lblMode.Text = "Create Node"

			If m_curPanel Is Splitter.Panel1 Then
				btnOKPanel1.Visible = False
				ErrorProvider1.SetError(btnOKPanel1, "")
			Else
				btnOKPanel2.Visible = False
				ErrorProvider1.SetError(btnOKPanel2, "")
			End If
			Dim e2 As New System.EventArgs
			cboType_SelectedValueChanged(sender, e2)
		End If
	End Sub

	Private Sub Splitter_Panel2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Splitter.Panel2.Click
		If (m_digitCommand Is Nothing) Then
			m_digitCommand = CurrentDigitTool.CurrentTool.currentDigit
		End If

		If m_digitCommand Is Nothing Then
			Return
		End If

		m_createNode = False

		If Not m_curPanel Is Splitter.Panel2 Then
			Dim ctrl As Windows.Forms.Control
			m_curPanel = Splitter.Panel2
			For Each ctrl In Splitter.Panel1.Controls
				ctrl.Enabled = False
			Next
			For Each ctrl In Splitter.Panel2.Controls
				ctrl.Enabled = True
			Next
			lblMode.Text = "Create Link"

			If Not m_schematicFeature1 Is Nothing Then
				m_schematicFeature1 = Nothing
				m_digitCommand.SchematicFeature1() = m_schematicFeature1
			End If
			If Not m_schematicFeature2 Is Nothing Then
				m_schematicFeature2 = Nothing
				m_digitCommand.SchematicFeature2() = m_schematicFeature2
			End If

			If m_curPanel Is Splitter.Panel1 Then
				btnOKPanel1.Visible = False
				ErrorProvider1.SetError(btnOKPanel1, "")
			Else
				btnOKPanel2.Visible = False
				ErrorProvider1.SetError(btnOKPanel2, "")
			End If
			Dim e2 As New System.EventArgs
			cboLinkType_SelectedValueChanged(sender, e2)
		End If
	End Sub
End Class
