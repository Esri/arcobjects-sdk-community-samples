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
Option Strict Off
Option Explicit On

Imports System
Imports System.Collections.Generic
Imports System.Text
Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.ADF.CATIDs
Imports System.Runtime.InteropServices

Public Class clsGE_Transform_Move
    Implements IGeometricEffect
    Implements IGraphicAttributes
    Implements IPersistVariant
    Implements IEditInteraction

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "39509DF8-376C-4dae-9B02-B7A5F06490A4"
#End Region

#Region "Component Category Registration"
    <ComRegisterFunction(), ComVisibleAttribute(False)> _
    Public Shared Sub RegisterFunction(ByVal regkey As String)
        GeometricEffect.Register(regkey)
    End Sub
    <ComUnregisterFunction(), ComVisibleAttribute(False)> _
    Public Shared Sub UnRegisterFunction(ByVal regkey As String)
        GeometricEffect.Unregister(regkey)
    End Sub
#End Region

    ' ------------------------------------------------------------------------------------
    ' ArcGIS developer sample of a simple geometric effect for use in representation rules
    ' It moves the display geometry by a given x,y amount
    ' ------------------------------------------------------------------------------------

    ' Declare private variables used by this class
    Dim m_dOffsetX As Double ' the amount to move in X
    Dim m_dOffsetY As Double ' the amount to move in Y
    Dim m_bDone As Boolean ' flag for no more geometries
    Dim m_pGeom As IGeometry ' current geometry being moved
    Dim m_pTransform As ITransform2D ' geometry transformation
    Dim m_pCloneGeom As ESRI.ArcGIS.esriSystem.IClone ' so can replicate geometry
    Dim m_pGeomCopy As IGeometry ' handle on the copied geometry

    ' Initialize method gets called once, to set things up
    Private Sub Class_Initialize_Renamed()
        m_dOffsetX = 0.1 ' default move offset is right a bit ...
        m_dOffsetY = -0.1 ' ... and down a bit
    End Sub
    Public Sub New()
        MyBase.New()
        Class_Initialize_Renamed()
    End Sub

    Public ReadOnly Property IsEditableAttribute(ByVal editParams As Object, ByVal attrIndex As Integer) As Boolean Implements ESRI.ArcGIS.Display.IEditInteraction.IsEditableAttribute
        Get
            Dim pMove As ESRI.ArcGIS.Display.IMoveInteraction
            If TypeOf editParams Is ESRI.ArcGIS.Display.IMoveInteraction Then
                pMove = editParams
                If Not pMove Is Nothing Then
                    If (attrIndex = 0 Or attrIndex = 1) Then
                        Return True
                    End If
                End If
            End If
            Return False
        End Get
    End Property

    Public Sub ModifyAttributes(ByVal editParams As Object, ByVal attrArray As Object) Implements ESRI.ArcGIS.Display.IEditInteraction.ModifyAttributes
        Dim pMove As ESRI.ArcGIS.Display.IMoveInteraction
        If TypeOf editParams Is ESRI.ArcGIS.Display.IMoveInteraction Then
            pMove = editParams
            If Not pMove Is Nothing Then
                If attrArray(0) = True Then
                    m_dOffsetX = m_dOffsetX + pMove.OffsetX
                End If
                If attrArray(1) = True Then
                    m_dOffsetY = m_dOffsetY + pMove.OffsetY
                End If
            End If
        End If
    End Sub

    ' --------------------------------------------------------
    ' IPersistVariant interface
    ' Sets up persistence for this class, to store the current offset values
    ' --------------------------------------------------------

    ' Allocate a unique ID GUID for it
    Public ReadOnly Property ID() As ESRI.ArcGIS.esriSystem.UID Implements ESRI.ArcGIS.esriSystem.IPersistVariant.ID
        Get
            Dim pUID As ESRI.ArcGIS.esriSystem.IUID
            pUID = New ESRI.ArcGIS.esriSystem.UID
            pUID.Value = "GETransformVBNet.clsGE_Transform_Move"
            Return pUID
        End Get
    End Property

    Public Sub Load(ByVal Stream As ESRI.ArcGIS.esriSystem.IVariantStream) Implements ESRI.ArcGIS.esriSystem.IPersistVariant.Load
        Dim version As Integer
        version = Stream.Read
        m_dOffsetX = Stream.Read
        m_dOffsetY = Stream.Read
    End Sub

    Public Sub Save(ByVal Stream As ESRI.ArcGIS.esriSystem.IVariantStream) Implements ESRI.ArcGIS.esriSystem.IPersistVariant.Save
        Dim version As Integer
        version = 1
        Stream.Write(version)
        Stream.Write(m_dOffsetX)
        Stream.Write(m_dOffsetY)
    End Sub

    Public ReadOnly Property ClassName() As String Implements ESRI.ArcGIS.Display.IGraphicAttributes.ClassName
        Get
            Return "Transform Move VBNet"
        End Get
    End Property

    Public ReadOnly Property GraphicAttributeCount() As Integer Implements ESRI.ArcGIS.Display.IGraphicAttributes.GraphicAttributeCount
        Get
            Return 2
        End Get
    End Property

    Public ReadOnly Property ID1(ByVal attrIndex As Integer) As Integer Implements ESRI.ArcGIS.Display.IGraphicAttributes.ID
        Get
            If (attrIndex >= 0 And attrIndex < 2) Then
                Return attrIndex
            End If
            Return -1
        End Get
    End Property

    Public ReadOnly Property IDByName(ByVal Name As String) As Integer Implements ESRI.ArcGIS.Display.IGraphicAttributes.IDByName
        Get
            If (Name = "X Offset Move") Then
                Return 0
            End If
            If Name = "Y Offset Move" Then
                Return 1
            End If
            Return -1
        End Get
    End Property

    Public ReadOnly Property Name(ByVal attrId As Integer) As String Implements ESRI.ArcGIS.Display.IGraphicAttributes.Name
        Get
            If (attrId = 0) Then
                Return "X Offset Move"
            End If
            If attrId = 1 Then
                Return "Y Offset Move"
            End If
            Return -1
        End Get
    End Property

    Public ReadOnly Property Type(ByVal attrId As Integer) As ESRI.ArcGIS.Display.IGraphicAttributeType Implements ESRI.ArcGIS.Display.IGraphicAttributes.Type
        Get
            If attrId = 0 Then
                Return New ESRI.ArcGIS.Display.GraphicAttributeSizeType
            End If
            If attrId = 1 Then
                Return New ESRI.ArcGIS.Display.GraphicAttributeSizeType
            End If
            Return Nothing
        End Get
    End Property

    Public Property Value(ByVal attrId As Integer) As Object Implements ESRI.ArcGIS.Display.IGraphicAttributes.Value
        Get
            If attrId = 0 Then Return m_dOffsetX
            If attrId = 1 Then Return m_dOffsetY
            Return 0
        End Get
        Set(ByVal value As Object)
            If attrId = 0 Then m_dOffsetX = value
            If attrId = 1 Then m_dOffsetY = value
        End Set
    End Property

    Public Function NextGeometry() As IGeometry Implements ESRI.ArcGIS.Display.IGeometricEffect.NextGeometry
        If m_bDone Then
            Return Nothing
        Else
            m_pCloneGeom = m_pGeom ' but we need to copy so don't change original
            m_pGeomCopy = m_pCloneGeom.Clone ' make a copy
            m_pTransform = m_pGeomCopy ' now we need to transform the copy
            m_pTransform.Move(m_dOffsetX, m_dOffsetY) ' by the offset amounts
            m_bDone = True ' no more to do for this geometry
            Return m_pGeomCopy ' return the moved geometry
        End If
    End Function

    Public ReadOnly Property OutputType(ByVal inputType As esriGeometryType) As esriGeometryType Implements ESRI.ArcGIS.Display.IGeometricEffect.OutputType
        Get
            Return inputType
        End Get
    End Property

    Public Sub Reset(ByVal Geometry As IGeometry) Implements ESRI.ArcGIS.Display.IGeometricEffect.Reset
        m_pGeom = Geometry ' current geometry to be moved
        m_pGeomCopy = Nothing ' discard any previous geometry copy
        m_bDone = False ' still work to do
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
End Class
