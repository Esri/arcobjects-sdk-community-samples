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
Imports ESRI.ArcGIS.Schematic
Imports ESRI.ArcGIS.Geodatabase
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms


Friend Class FrmNodeReductionRule
    Private m_isDirty As Boolean = False
    Private m_pageSite As ESRI.ArcGIS.Framework.IComPropertyPageSite
    Private m_diagramClass As ISchematicDiagramClass

    Public Sub New()
        InitializeComponent()
    End Sub

    Protected Overrides Sub Finalize()
        m_diagramClass = Nothing
        m_pageSite = Nothing
        MyBase.Finalize()
    End Sub

    ' For managing the IsDirty flag that specifies whether 
    ' or not controls in the custom form have been modified
    Public Property IsDirty() As Boolean
        Get
            Return m_isDirty
        End Get
        Set(ByVal value As Boolean)
            m_isDirty = value
        End Set
    End Property

    '- For managing the related IComPropertyPageSite
    Public WriteOnly Property PageSite() As ESRI.ArcGIS.Framework.IComPropertyPageSite
        Set(ByVal value As ESRI.ArcGIS.Framework.IComPropertyPageSite)
            m_pageSite = value
        End Set
    End Property

    Private Sub Changed(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtDescription.TextChanged, chkKeepVertices.CheckStateChanged, cmbReducedNodeClass.SelectedIndexChanged, cmbReducedNodeClass.Click, txtLinkAttribute.TextChanged, txtLinkAttribute.Click

        ' If the user changes something, mark the custom form dirty and 
        ' enable the apply button on the page site via the PageChanged method
        m_isDirty = True
        If (m_pageSite IsNot Nothing) Then m_pageSite.PageChanged()
    End Sub

    Public WriteOnly Property DiagramClass As ISchematicDiagramClass
        Set(ByVal value As ISchematicDiagramClass)
            m_diagramClass = value
        End Set
    End Property

    Private Function GetElementClass(ByRef enumElementClass As IEnumSchematicElementClass, ByRef linkClassName As String) As ISchematicElementClass
        enumElementClass.Reset()
        Dim elementClass As ISchematicElementClass = enumElementClass.Next()
        Do While (elementClass IsNot Nothing)
            If (elementClass.Name = linkClassName) Then Return elementClass

            elementClass = enumElementClass.Next()
        Loop

        Return Nothing
    End Function


    Private Function IsFieldNumericAndEditable(ByRef fieldName As String, ByRef elementClass As ISchematicElementClass) As Boolean
        Dim table As ITable = CType(elementClass, ITable)

        If (table Is Nothing) Then Return False

        Dim index As Integer = table.FindField(fieldName)

        If (index < 0) Then Return False

        Dim FieldType As esriFieldType = table.Fields.Field(index).Type

        If ((FieldType = esriFieldType.esriFieldTypeDouble Or FieldType = esriFieldType.esriFieldTypeInteger Or FieldType = esriFieldType.esriFieldTypeSmallInteger) And (table.Fields.Field(index).Editable)) Then Return True

        Return False
    End Function

    Private Sub FillAttNames(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbTargetSuperspanClass.SelectedIndexChanged, cmbTargetSuperspanClass.Click

        m_isDirty = True
        If (m_pageSite IsNot Nothing) Then m_pageSite.PageChanged()

        Dim linkClassName As String = ""

        If (cmbTargetSuperspanClass.SelectedItem IsNot Nothing) Then
            linkClassName = cmbTargetSuperspanClass.SelectedItem.ToString()
        End If

        If (linkClassName = "" Or m_diagramClass Is Nothing) Then
            cmbAttributeName.Items.Clear()
            Return
        End If

        cmbAttributeName.Items.Clear()

        Dim elementClass As ISchematicElementClass
        Dim enumElementClass As IEnumSchematicElementClass = m_diagramClass.AssociatedSchematicElementClasses

        elementClass = GetElementClass(enumElementClass, linkClassName)
        If (elementClass Is Nothing) Then Return
        Dim attCont As ISchematicAttributeContainer = CType(elementClass, ISchematicAttributeContainer)

        If (attCont Is Nothing) Then Return

        Dim enumAtt As IEnumSchematicAttribute = attCont.SchematicAttributes

        If (enumAtt Is Nothing) Then Return

        enumAtt.Reset()
        Dim att As ISchematicAttribute = enumAtt.Next()

        Do While (att IsNot Nothing)
            Try

                Dim attField As SchematicAttributeAssociatedField = CType(att, ISchematicAttributeAssociatedField)

                If (attField IsNot Nothing) Then
                    Dim fieldName As String = attField.Name
                    If (IsFieldNumericAndEditable(fieldName, elementClass)) Then
                        cmbAttributeName.Items.Add(fieldName)
                    End If
                End If
            Catch ex As Exception

            End Try
            att = enumAtt.Next()
        Loop
    End Sub

    Private Sub chkAttribute_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkLinkAttribute.CheckStateChanged
        m_isDirty = True
        If (m_pageSite IsNot Nothing) Then m_pageSite.PageChanged()

        If (Not chkLinkAttribute.Checked) Then
            txtLinkAttribute.Text = ""
        End If

    End Sub

End Class

