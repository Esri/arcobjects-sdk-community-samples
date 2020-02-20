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
Imports System.Windows.Forms
Imports ESRI.ArcGIS.DataSourcesFile

Public Class RestrictionsForm

#Region "Public methods"

	Public Sub New()
		MyBase.New()

		'This call is required by the Windows Form Designer.
		InitializeComponent()

		m_arrRestrictions = New System.Collections.ArrayList
	End Sub

    ' Receives restriction from route and inits controls 
	Public Sub Init(ByVal objRouter As ISMRouter)
		' clear controls
		ClearAll()

		Try
			Me.SuspendLayout()
			m_pnlRestrictions.SuspendLayout()

			' Get Net attributes
			Dim objAttrColl As ISMNetAttributesCollection
			objAttrColl = objRouter.NetAttributes

			' attributes count
			Dim nCount As Integer = objAttrColl.Count

			For i As Integer = 0 To nCount - 1
				' get attribute
				Dim objAttr As ISMNetAttribute
				objAttr = objAttrColl.Item(i)

				If TypeOf (objAttr) Is ISMNetAttribute2 Then
					Dim objAttr2 As ISMNetAttribute2
					objAttr2 = objAttr

					' If Usage type is restriction
					Dim eType As esriSMNetAttributeType
					eType = objAttr2.UsageType

					If eType = esriSMNetAttributeUsageType.esriSMNAUTRestrictionBoolean Or _
						eType = esriSMNetAttributeUsageType.esriSMNAUTRestrictionMinAllowed Or _
						eType = esriSMNetAttributeUsageType.esriSMNAUTRestrictionMaxAllowed Then

						' create control for restriction
						Dim ctrlRestriction As RestrictControl
						ctrlRestriction = New RestrictControl

						' create restriction info
						Dim objRestriction As RestrictionsInfo
						objRestriction = New RestrictionsInfo

						' Init info
						objRestriction.m_objAttr2 = objAttr2
						objRestriction.m_strName = objAttr2.Name
						objRestriction.m_bChecked = False
						objRestriction.m_eType = RestrictControl.ERSType.eStrict
						objRestriction.m_bUseParameter = False

						If eType <> esriSMNetAttributeUsageType.esriSMNAUTRestrictionBoolean Then
							objRestriction.m_bUseParameter = True
							objRestriction.m_dParameter = 0.0
						End If

						' Add controls (reverse order)
						ctrlRestriction.Dock = DockStyle.Top
						m_pnlRestrictions.Controls.Add(ctrlRestriction)

						m_arrRestrictions.Add(objRestriction)
					End If
				End If
			Next

			' Set restriction info to controls
			UpdateControls()

		Catch ex As Exception
			ClearAll()
		Finally
			m_pnlRestrictions.ResumeLayout(False)
			Me.ResumeLayout(False)
		End Try
	End Sub

	' Setups router restrictions
	Public Sub SetupRouter(ByVal objRouter As ISMRouter)
        Dim objRouterSetup As ISMRouterSetup2
		objRouterSetup = objRouter

		Try
			' Clear all previous changes
			objRouterSetup.ClearAllRestrictions()

			For Each objInfo As RestrictionsInfo In m_arrRestrictions
				If objInfo.m_bChecked Then
					' New restriction
					Dim objRestr As ISMRestriction
					objRestr = New SMRestrictionClass

					objRestr.Attribute = CType(objInfo.m_objAttr2, SMNetAttribute)

					' restriction type
					If objInfo.m_eType = RestrictControl.ERSType.eStrict Then
						objRestr.Type = esriSMRestrictionType.esriSMRTStrict
					Else
						objRestr.Type = esriSMRestrictionType.esriSMRTRelaxed
					End If

					' Paramter
					If objInfo.m_bUseParameter Then
						objRestr.Param = objInfo.m_dParameter
					End If

					' Add restriction to router
					objRouterSetup.SetRestriction(objRestr)
				End If
			Next
		Catch ex As Exception
			objRouterSetup.ClearAllRestrictions()
			MessageBox.Show("Cannot set restrictions.")
		End Try
	End Sub

#End Region

#Region "Private methods"

	' Clears all
	Private Sub ClearAll()
		m_arrRestrictions.Clear()
		m_pnlRestrictions.Controls.Clear()
	End Sub

	' Updates controls values
	Private Sub UpdateControls()
		Dim nCount As Integer = m_arrRestrictions.Count
		For i As Integer = 0 To nCount - 1
			Dim ctrlRestriction As RestrictControl
			ctrlRestriction = CType(m_pnlRestrictions.Controls.Item(nCount - 1 - i), RestrictControl)

			ctrlRestriction.TabIndex = i
			Dim objRestriction As RestrictionsInfo
			objRestriction = CType(m_arrRestrictions(i), RestrictionsInfo)
			ctrlRestriction.RSName = objRestriction.m_strName
			ctrlRestriction.RSChecked = objRestriction.m_bChecked
			ctrlRestriction.RSType = objRestriction.m_eType
			ctrlRestriction.RSUseParameter = objRestriction.m_bUseParameter

			If m_arrRestrictions(i).m_bUseParameter Then
				ctrlRestriction.RSParameter = objRestriction.m_dParameter
			End If
		Next
	End Sub

	' Updates restrictions info 
	Private Sub UpdateInfo()
		Dim nCount As Integer = m_arrRestrictions.Count
		For i As Integer = 0 To nCount - 1
			Dim ctrlRestriction As RestrictControl
			ctrlRestriction = CType(m_pnlRestrictions.Controls.Item(nCount - 1 - i), RestrictControl)

			Dim objRestriction As RestrictionsInfo
			objRestriction = CType(m_arrRestrictions(i), RestrictionsInfo)

			objRestriction.m_strName = ctrlRestriction.RSName
			objRestriction.m_bChecked = ctrlRestriction.RSChecked
			objRestriction.m_eType = ctrlRestriction.RSType

			If m_arrRestrictions(i).m_bUseParameter Then
				objRestriction.m_dParameter = ctrlRestriction.RSParameter
			End If
		Next
	End Sub

	' Updates restrictions info from controls
	Private Sub m_btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles m_btnOK.Click
		UpdateInfo()
	End Sub

	' Updates controls from restrictions info 
	Private Sub m_btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles m_btnCancel.Click
		UpdateControls()
	End Sub

#End Region

#Region "Private members"
	' Restrictions data for Router.
	' NOTE: Restriction controls in reverse order
	Private m_arrRestrictions As System.Collections.ArrayList
#End Region

End Class

' Info for one restriction
Public Class RestrictionsInfo
	Public m_objAttr2 As ISMNetAttribute2
	Public m_strName As String
	Public m_bChecked As Boolean
	Public m_eType As RestrictControl.ERSType
	Public m_bUseParameter As Boolean
	Public m_dParameter As Double
End Class
