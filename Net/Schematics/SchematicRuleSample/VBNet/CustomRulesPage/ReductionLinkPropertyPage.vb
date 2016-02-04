'Copyright 2016 Esri

'Licensed under the Apache License, Version 2.0 (the "License");
'you may not use this file except in compliance with the License.
'You may obtain a copy of the License at

'    http://www.apache.org/licenses/LICENSE-2.0

'Unless required by applicable law or agreed to in writing, software
'distributed under the License is distributed on an "AS IS" BASIS,
'WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.

'See the License for the specific language governing permissions and
'limitations under the License.
Imports ESRI.ArcGIS
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.ADF.CATIDs
Imports Schematic = ESRI.ArcGIS.Schematic
Imports ESRI.ArcGIS.Framework
Imports System.Windows.Forms
Imports ESRI.ArcGIS.esriSystem
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports CustomRulesVB

<System.Runtime.InteropServices.ClassInterface(System.Runtime.InteropServices.ClassInterfaceType.None)> _
<System.Runtime.InteropServices.Guid(ReductionLinkPropertyPage.GUID)> _
<System.Runtime.InteropServices.ProgId(ReductionLinkPropertyPage.PROGID)> _
Public Class ReductionLinkPropertyPage
    Implements ESRI.ArcGIS.Framework.IComPropertyPage

    Public Const GUID As String = "4E3C6551-8594-4c51-9B8B-075E745CC622"
    Public Const PROGID As String = "CustomRulesPageVB.ReductionLinkPropertyPage"

    ' Register/unregister categories for this class
#Region "Component Category Registration"
    <System.Runtime.InteropServices.ComRegisterFunction()> _
    Shared Sub Register(ByVal CLSID As String)
        SchematicRulePropertyPages.Register(CLSID)
    End Sub

    <System.Runtime.InteropServices.ComUnregisterFunction()> _
    Shared Sub Unregister(ByVal CLSID As String)
        SchematicRulePropertyPages.Unregister(CLSID)
    End Sub
#End Region

    Private m_Form As frmReductionLink = New frmReductionLink()        ' the custom form
    Private m_mySchematicRule As ReductionLinkRule                                ' the custom rule
    Private m_title As String = "Reduction Links Rule VBNet"                        ' the form title
    Private m_priority As Integer = 0                                                                        ' the IComPage priority


#Region "IComPropertyPage Members"
    Public Function Activate() As Integer Implements ESRI.ArcGIS.Framework.IComPropertyPage.Activate
        ' Create a new RemoveElementForm but do not show it 
        If m_Form Is Nothing Then m_Form = New frmReductionLink()
        Return m_Form.Handle.ToInt32
    End Function

    Public Function Applies(ByVal objects As ESRI.ArcGIS.esriSystem.ISet) As Boolean Implements ESRI.ArcGIS.Framework.IComPropertyPage.Applies
        Dim mySchematicRule As Schematic.ISchematicRule
        mySchematicRule = FindMyRule(objects)
        Return (mySchematicRule IsNot Nothing)
    End Function

    Public Sub Apply() Implements ESRI.ArcGIS.Framework.IComPropertyPage.Apply
        Try
            m_mySchematicRule.Description = m_Form.txtDescription.Text
            m_mySchematicRule.ReductionLinkName = m_Form.cboReduce.SelectedItem.ToString()
            m_mySchematicRule.UsePort = m_Form.chkUsePort.Checked
            m_Form.IsDirty = True
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Unable to initialize rule properties", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Public Sub Cancel() Implements ESRI.ArcGIS.Framework.IComPropertyPage.Cancel
        m_Form.IsDirty = False
    End Sub

    Public Sub Deactivate() Implements ESRI.ArcGIS.Framework.IComPropertyPage.Deactivate
        m_Form.Close()
    End Sub

    Public ReadOnly Property Height() As Integer Implements ESRI.ArcGIS.Framework.IComPropertyPage.Height
        Get
            Return m_Form.Height
        End Get
    End Property

    Public ReadOnly Property HelpContextID(ByVal controlID As Integer) As Integer Implements ESRI.ArcGIS.Framework.IComPropertyPage.HelpContextID
        Get
            ' TODO: return context ID if desired
            Return 0
        End Get
    End Property

    Public ReadOnly Property HelpFile() As String Implements ESRI.ArcGIS.Framework.IComPropertyPage.HelpFile
        Get
            Return ""
        End Get
    End Property

    Public Sub Hide() Implements ESRI.ArcGIS.Framework.IComPropertyPage.Hide
        m_Form.Hide()
    End Sub

    Public ReadOnly Property IsPageDirty() As Boolean Implements ESRI.ArcGIS.Framework.IComPropertyPage.IsPageDirty
        Get
            Return m_Form.IsDirty
        End Get
    End Property

    Public WriteOnly Property PageSite() As ESRI.ArcGIS.Framework.IComPropertyPageSite Implements ESRI.ArcGIS.Framework.IComPropertyPage.PageSite
        Set(ByVal value As ESRI.ArcGIS.Framework.IComPropertyPageSite)
            m_Form.PageSite = value
        End Set
    End Property

    Public Property Priority() As Integer Implements ESRI.ArcGIS.Framework.IComPropertyPage.Priority
        Get
            Return m_priority
        End Get
        Set(ByVal value As Integer)
            m_priority = value
        End Set
    End Property

    Public Sub SetObjects(ByVal objects As ESRI.ArcGIS.esriSystem.ISet) Implements ESRI.ArcGIS.Framework.IComPropertyPage.SetObjects
        ' Search for the custom rule object instance
        m_mySchematicRule = FindMyRule(objects)
    End Sub

    Public Sub Show() Implements ESRI.ArcGIS.Framework.IComPropertyPage.Show
        Try
            If (m_Form.cboReduce.Items.Count = 0) Then
                Dim diagramClass As Schematic.ISchematicDiagramClass
                diagramClass = m_mySchematicRule.SchematicDiagramClass
                If (diagramClass Is Nothing) Then Return

                Dim elementClass As Schematic.ISchematicElementClass
                Dim enumElementClass As Schematic.IEnumSchematicElementClass
                enumElementClass = diagramClass.AssociatedSchematicElementClasses
                enumElementClass.Reset()
                elementClass = enumElementClass.Next()
                While (elementClass IsNot Nothing)
                    If (elementClass.SchematicElementType = Schematic.esriSchematicElementType.esriSchematicLinkType) Then
                        m_Form.cboReduce.Items.Add(elementClass.Name)
                    End If

                    elementClass = enumElementClass.Next()
                End While
            End If

            m_Form.cboReduce.Text = m_mySchematicRule.ReductionLinkName
            m_Form.txtDescription.Text = m_mySchematicRule.Description
            m_Form.chkUsePort.Checked = m_mySchematicRule.UsePort
            m_Form.IsDirty = False

            m_Form.Visible = True
            m_Form.lblDescription.Visible = True
            m_Form.lblReduce.Visible = True
            m_Form.txtDescription.Visible = True
            m_Form.cboReduce.Visible = True
            m_Form.chkUsePort.Visible = True
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Unable to initialize property page", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Public Property Title() As String Implements ESRI.ArcGIS.Framework.IComPropertyPage.Title
        Get
            Return m_title
        End Get
        Set(ByVal value As String)
            m_title = value
        End Set
    End Property

    Public ReadOnly Property Width() As Integer Implements ESRI.ArcGIS.Framework.IComPropertyPage.Width
        Get
            Return m_Form.Width
        End Get
    End Property
#End Region

    Protected Overrides Sub Finalize()
        m_Form = Nothing
        m_mySchematicRule = Nothing
        MyBase.Finalize()
    End Sub

    ' Find and return this rule from the passed in objects 
    Private Function FindMyRule(ByVal Objectset As ESRI.ArcGIS.esriSystem.ISet) As ReductionLinkRule
        If (Objectset.Count = 0) Then Return Nothing

        Objectset.Reset()

        Dim obj As Object
        obj = Objectset.Next()

        While (obj IsNot Nothing)
            If (TypeOf (obj) Is ReductionLinkRule) Then Exit While

            obj = Objectset.Next()
        End While

        Return CType(obj, ReductionLinkRule)
    End Function
End Class
