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
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports ESRI.ArcGIS
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Schematic
Imports esriSystem = ESRI.ArcGIS.esriSystem

<System.Runtime.InteropServices.ClassInterface(System.Runtime.InteropServices.ClassInterfaceType.None)> _
<System.Runtime.InteropServices.Guid(ReductionLinkRule.GUID)> _
<System.Runtime.InteropServices.ProgId(ReductionLinkRule.PROGID)> _
Public Class ReductionLinkRule
    Implements ESRI.ArcGIS.Schematic.ISchematicRule
    Implements ESRI.ArcGIS.Schematic.ISchematicRuleDesign

    Public Const GUID As String = "F18765AE-DDAA-464d-812D-EB9D9F544F5B"
    Public Const PROGID As String = "CustomRulesVB.ReductionLinkRule"

    ' Register/unregister categories for this class
#Region "Component Category Registration"
    <System.Runtime.InteropServices.ComRegisterFunction()> _
    Shared Sub Register(ByVal CLSID As String)
        ESRI.ArcGIS.ADF.CATIDs.SchematicRules.Register(CLSID)
    End Sub

    <System.Runtime.InteropServices.ComUnregisterFunction()> _
    Shared Sub Unregister(ByVal CLSID As String)
        ESRI.ArcGIS.ADF.CATIDs.SchematicRules.Unregister(CLSID)
    End Sub
#End Region

    Private m_diagramClass As ESRI.ArcGIS.Schematic.ISchematicDiagramClass
    Private m_reductionLinkName As String
    Private m_usePort As Boolean = False
    Private m_description As String = "Reduction Link Rule VBNet"

    Public Sub New()
    End Sub

    Protected Overrides Sub Finalize()
        m_diagramClass = Nothing
        MyBase.Finalize()
    End Sub

    Public Property ReductionLinkName() As String
        Get
            Return m_reductionLinkName
        End Get
        Set(ByVal value As String)
            m_reductionLinkName = value
        End Set
    End Property

    Public Property UsePort() As Boolean
        Get
            Return m_usePort
        End Get
        Set(ByVal value As Boolean)
            m_usePort = value
        End Set
    End Property

#Region "ISchematicRule Members"
    Public Sub Alter(ByVal schematicDiagramClass As ESRI.ArcGIS.Schematic.ISchematicDiagramClass, ByVal propertySet As ESRI.ArcGIS.esriSystem.IPropertySet) Implements ESRI.ArcGIS.Schematic.ISchematicRule.Alter
        On Error Resume Next
        m_diagramClass = schematicDiagramClass

        m_description = propertySet.GetProperty("DESCRIPTION").ToString()
        m_reductionLinkName = propertySet.GetProperty("REDUCTIONLINKNAME").ToString()
        m_usePort = CType(propertySet.GetProperty("USEPORT"), Boolean)
    End Sub

    Public Sub Apply(ByVal inMemoryDiagram As ESRI.ArcGIS.Schematic.ISchematicInMemoryDiagram, Optional ByVal cancelTracker As ESRI.ArcGIS.esriSystem.ITrackCancel = Nothing) Implements ESRI.ArcGIS.Schematic.ISchematicRule.Apply

        If (m_reductionLinkName = "") Then Return

        Dim enumSchematicElement As IEnumSchematicInMemoryFeature
        Dim schemElement As ISchematicInMemoryFeature
        Dim diagramClass As ISchematicDiagramClass = Nothing
        Dim elementClass As ISchematicElementClass
        Dim enumElementClass As IEnumSchematicElementClass
        Dim allreadyUsed As Collection = New Microsoft.VisualBasic.Collection

        Try
            diagramClass = inMemoryDiagram.SchematicDiagramClass
        Catch
        End Try
        If (diagramClass Is Nothing) Then Return

        enumElementClass = diagramClass.AssociatedSchematicElementClasses
        enumElementClass.Reset()
        elementClass = enumElementClass.Next()
        While (elementClass IsNot Nothing)
            If (elementClass.Name = m_reductionLinkName) Then Exit While
            elementClass = enumElementClass.Next()
        End While

        If (elementClass Is Nothing) Then Return

        enumSchematicElement = inMemoryDiagram.GetSchematicInMemoryFeaturesByClass(elementClass)
        enumSchematicElement.Reset()

        Dim link As ISchematicInMemoryFeatureLink = Nothing
        Dim fromNode As ISchematicInMemoryFeatureNode = Nothing
        Dim toNode As ISchematicInMemoryFeatureNode = Nothing
        Dim fromPort As Integer = 0
        Dim toPort As Integer = 0
        Dim newElem As ISchematicInMemoryFeature = Nothing
        Dim enumIncidentLinks As IEnumSchematicInMemoryFeatureLink
        Dim schemLinker As ISchematicInMemoryFeatureLinkerEdit = CType(New SchematicLinkerClass(), ISchematicInMemoryFeatureLinkerEdit)
        Dim reduction As Boolean = False

        schemElement = enumSchematicElement.Next()
        While (schemElement IsNot Nothing)
            Try
                Dim elemName As String = allreadyUsed(schemElement.Name).ToString()
                ' if found, this link is allready used
                schemElement = enumSchematicElement.Next()
                Continue While
            Catch
                ' Add link to collection
                allreadyUsed.Add(schemElement.Name, schemElement.Name)
            End Try

            ' Get from node and to node
            link = CType(schemElement, ISchematicInMemoryFeatureLink)

            fromNode = link.FromNode
            toNode = link.ToNode
            If (m_usePort) Then
                fromPort = link.FromPort
                toPort = link.ToPort
            End If

            ' Get all links from this node
            enumIncidentLinks = fromNode.GetIncidentLinks(esriSchematicEndPointType.esriSchematicOriginOrExtremityNode)
            enumIncidentLinks.Reset()
            newElem = enumIncidentLinks.Next()
            While (newElem IsNot Nothing)
                reduction = False
                If newElem Is schemElement Then
                    '  the new link is the same link we works on
                    newElem = enumIncidentLinks.Next()
                    Continue While
                End If
                link = CType(newElem, ISchematicInMemoryFeatureLink)

                ' 1st case of comparison
                If (fromNode Is link.FromNode AndAlso toNode Is link.ToNode) Then
                    If (m_usePort) Then
                        reduction = (fromPort = link.FromPort AndAlso toPort = link.ToPort)
                    Else
                        reduction = True
                    End If
                    ' 2nd case of comparison
                ElseIf (fromNode Is link.ToNode AndAlso toNode Is link.FromNode) Then
                    If (m_usePort) Then
                        reduction = (fromPort = link.ToPort AndAlso toPort = link.FromPort)
                    Else
                        reduction = True
                    End If
                End If

                If (reduction) Then
                    Try
                        schemLinker.ReportAssociations(newElem, schemElement)    ' Reports asssociation to first link
                        allreadyUsed.Add(newElem.Name, newElem.Name) '  Add link to collection
                        newElem.Displayed = False    ' this link is not visible

                    Catch
                    End Try
                End If
                newElem = enumIncidentLinks.Next()

            End While
            schemElement.Displayed = True
            schemElement = enumSchematicElement.Next()
        End While
    End Sub

    Public ReadOnly Property ClassID() As ESRI.ArcGIS.esriSystem.UID Implements ESRI.ArcGIS.Schematic.ISchematicRule.ClassID
        Get

            Dim ruleID As esriSystem.UID = New esriSystem.UID()
            ruleID.Value = PROGID
            Return ruleID
        End Get
    End Property

    Public ReadOnly Property Description1() As String Implements ESRI.ArcGIS.Schematic.ISchematicRule.Description
        Get
            Return m_description
        End Get
    End Property

    Public Property Description() As String
        Get
            Return m_description
        End Get
        Set(ByVal value As String)
            m_description = value
        End Set
    End Property

    Public ReadOnly Property Name() As String Implements ESRI.ArcGIS.Schematic.ISchematicRule.Name
        Get
            Return "Reduction Link Rule VBNet"
        End Get
    End Property

    Public ReadOnly Property PropertySet() As ESRI.ArcGIS.esriSystem.IPropertySet Implements ESRI.ArcGIS.Schematic.ISchematicRule.PropertySet
        Get
            Dim propSet As esriSystem.IPropertySet = New esriSystem.PropertySet()

            propSet.SetProperty("DESCRIPTION", m_description)
            propSet.SetProperty("USEPORT", m_usePort)
            propSet.SetProperty("REDUCTIONLINKNAME", m_reductionLinkName)

            Return propSet
        End Get
    End Property

    Public ReadOnly Property SchematicDiagramClass() As ESRI.ArcGIS.Schematic.ISchematicDiagramClass Implements ESRI.ArcGIS.Schematic.ISchematicRule.SchematicDiagramClass
        Get
            Return m_diagramClass
        End Get
    End Property
#End Region

#Region "ISchematicRuleDesign Members"

    Public Sub Detach() Implements ESRI.ArcGIS.Schematic.ISchematicRuleDesign.Detach
        m_diagramClass = Nothing
    End Sub

    Public WriteOnly Property PropertySet1() As ESRI.ArcGIS.esriSystem.IPropertySet Implements ESRI.ArcGIS.Schematic.ISchematicRuleDesign.PropertySet
        Set(ByVal value As ESRI.ArcGIS.esriSystem.IPropertySet)
            m_description = value.GetProperty("DESCRIPTION").ToString
            m_usePort = CBool(value.GetProperty("USEPORT"))
            m_reductionLinkName = value.GetProperty("REDUCTIONLINKNAME").ToString
        End Set
    End Property

    Public Property SchematicDiagramClass1() As ESRI.ArcGIS.Schematic.ISchematicDiagramClass Implements ESRI.ArcGIS.Schematic.ISchematicRuleDesign.SchematicDiagramClass
        Get
            Return m_diagramClass
        End Get
        Set(ByVal value As ESRI.ArcGIS.Schematic.ISchematicDiagramClass)
            m_diagramClass = value
        End Set
    End Property
#End Region

End Class
