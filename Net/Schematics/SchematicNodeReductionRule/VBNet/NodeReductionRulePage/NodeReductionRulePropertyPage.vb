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
Option Strict On

Imports ESRI.ArcGIS
Imports ESRI.ArcGIS.ADF.CATIDs
Imports Schematic = ESRI.ArcGIS.Schematic
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.esriSystem
Imports System
Imports System.Runtime.InteropServices
Imports System.Collections.Generic
Imports System.Windows.Forms
Imports CustomRulesVB



<System.Runtime.InteropServices.ClassInterface(System.Runtime.InteropServices.ClassInterfaceType.None)> _
<System.Runtime.InteropServices.Guid(NodeReductionRulePropertyPage.GUID)> _
<System.Runtime.InteropServices.ProgId(NodeReductionRulePropertyPage.PROGID)> _
Public Class NodeReductionRulePropertyPage
    Implements ESRI.ArcGIS.Framework.IComPropertyPage

    Public Const GUID As String = "4B018649-6916-4713-A2C0-F5D2E81A86DE"
    Public Const PROGID As String = "CustomRulesPageVB.NodeReductionRulePropertyPage"

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

    Private m_Form As FrmNodeReductionRule = New FrmNodeReductionRule()        ' the custom form
    Private m_myNodeReductionRule As NodeReductionRule                                 ' the custom rule
    Private m_title As String = "Node Reduction Rule VBNet"                           ' the form title
    Private m_priority As Integer = 0                                                                        ' the IComPage priority


#Region "IComPropertyPage Members"
    Public Function Activate() As Integer Implements ESRI.ArcGIS.Framework.IComPropertyPage.Activate
        ' Create a new RemoveElementForm but do not show it 
        If m_Form Is Nothing Then m_Form = New FrmNodeReductionRule()
        Return m_Form.Handle.ToInt32
    End Function

    Public Function Applies(ByVal objects As ESRI.ArcGIS.esriSystem.ISet) As Boolean Implements ESRI.ArcGIS.Framework.IComPropertyPage.Applies
        Dim mySchematicRule As Schematic.ISchematicRule
        mySchematicRule = FindMyRule(objects)
        Return (mySchematicRule IsNot Nothing)
    End Function

    Public Sub Apply() Implements ESRI.ArcGIS.Framework.IComPropertyPage.Apply
        Try
            m_myNodeReductionRule.Description = m_Form.TxtDescription.Text
        Catch
        End Try

        Try
            m_myNodeReductionRule.LengthAttributeName = m_Form.cmbAttributeName.SelectedItem.ToString()
        Catch
        End Try

        Try
            m_myNodeReductionRule.ReducedNodeClassName = m_Form.cmbReducedNodeClass.SelectedItem.ToString()
        Catch
        End Try

        Try
            m_myNodeReductionRule.SuperpanLinkClassName = m_Form.cmbTargetSuperspanClass.SelectedItem.ToString()
        Catch
        End Try

        Try
            m_myNodeReductionRule.KeepVertices = m_Form.chkKeepVertices.Checked
        Catch
        End Try

        Try
            m_myNodeReductionRule.LinkAttribute = m_Form.chkLinkAttribute.Checked
        Catch
        End Try

        Try
            m_myNodeReductionRule.LinkAttributeName = m_Form.txtLinkAttribute.Text
        Catch
        End Try

        m_Form.IsDirty = True
    End Sub

    Public Sub Cancel() Implements ESRI.ArcGIS.Framework.IComPropertyPage.Cancel
        m_Form.IsDirty = False
    End Sub

    Public Sub Deactivate() Implements ESRI.ArcGIS.Framework.IComPropertyPage.Deactivate
        m_Form.DiagramClass = Nothing
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
        m_myNodeReductionRule = FindMyRule(objects)
    End Sub

    Public Sub Show() Implements ESRI.ArcGIS.Framework.IComPropertyPage.Show

        Dim diagramClass As Schematic.ISchematicDiagramClass
        diagramClass = CType(m_myNodeReductionRule, Schematic.ISchematicRule).SchematicDiagramClass
        If (diagramClass Is Nothing) Then Return

        m_Form.DiagramClass = diagramClass
        Dim elementClass As Schematic.ISchematicElementClass
        Dim enumElementClass As Schematic.IEnumSchematicElementClass
        enumElementClass = diagramClass.AssociatedSchematicElementClasses

        m_Form.cmbReducedNodeClass.Items.Clear()
        m_Form.cmbTargetSuperspanClass.Items.Clear()
        m_Form.cmbAttributeName.Items.Clear()

        Try

            enumElementClass.Reset()
            elementClass = enumElementClass.Next()

            While (elementClass IsNot Nothing)

                If (elementClass.SchematicElementType = ESRI.ArcGIS.Schematic.esriSchematicElementType.esriSchematicNodeType) Then
                    m_Form.cmbReducedNodeClass.Items.Add(elementClass.Name)
                ElseIf (elementClass.SchematicElementType = ESRI.ArcGIS.Schematic.esriSchematicElementType.esriSchematicLinkType) Then
                    m_Form.cmbTargetSuperspanClass.Items.Add(elementClass.Name)
                End If

                elementClass = enumElementClass.Next()
            End While

            m_Form.cmbAttributeName.Text = m_myNodeReductionRule.LengthAttributeName
            m_Form.TxtDescription.Text = m_myNodeReductionRule.Description
            m_Form.cmbReducedNodeClass.Text = m_myNodeReductionRule.ReducedNodeClassName
            m_Form.cmbTargetSuperspanClass.Text = m_myNodeReductionRule.SuperpanLinkClassName
            m_Form.chkKeepVertices.Checked = m_myNodeReductionRule.KeepVertices
            m_Form.chkLinkAttribute.Checked = m_myNodeReductionRule.LinkAttribute
            m_Form.txtLinkAttribute.Text = m_myNodeReductionRule.LinkAttributeName
            m_Form.IsDirty = False

            SetVisibleControls()

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Unable to initialize property page", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub SetVisibleControls()
        m_Form.Visible = True
        m_Form.lblDescription.Visible = True
        m_Form.lblGroup.Visible = True
        m_Form.lblReducedNode.Visible = True
        m_Form.lblTargetSuperspan.Visible = True
        m_Form.lblAttributeName.Visible = True
        m_Form.cmbReducedNodeClass.Visible = True
        m_Form.cmbTargetSuperspanClass.Visible = True
        m_Form.chkKeepVertices.Visible = True
        m_Form.txtLinkAttribute.Visible = True
        m_Form.chkLinkAttribute.Visible = True

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
        m_Form.DiagramClass = Nothing
        m_Form = Nothing
        m_myNodeReductionRule = Nothing
        MyBase.Finalize()
    End Sub

    ' Find and return this rule from the passed in objects 
    Private Function FindMyRule(ByVal Objectset As ESRI.ArcGIS.esriSystem.ISet) As NodeReductionRule
        If (Objectset.Count = 0) Then Return Nothing

        Objectset.Reset()

        Dim obj As Object
        obj = Objectset.Next()

        While (obj IsNot Nothing)
            If (TypeOf (obj) Is NodeReductionRule) Then Exit While

            obj = Objectset.Next()
        End While

        Return CType(obj, NodeReductionRule)
    End Function
End Class
