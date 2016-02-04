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
Public Class clsGE_Transform_Scale
    Implements IGeometricEffect
    Implements IGraphicAttributes
    Implements IEditInteraction
    Implements IPersistVariant

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "CB5126D6-E183-4af9-B4B2-5AE78B377492"
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
    ' GE_Transform_Scale
    ' ArcGIS developer sample of a simple geometric effect for use in representation rules
    ' It scales the display geometry by a given x and y scaling factors
    ' ------------------------------------------------------------------------------------
    ' Declare private variables used by this class
    Dim m_dFactorX As Double ' the amount to scale in X
    Dim m_dFactorY As Double ' the amount to scale in Y
    Dim m_bDone As Boolean ' flag for no more geometries
    Dim m_pGeom As IGeometry ' current geometry being scaled
    Dim m_pTransform As ITransform2D ' geometry transformation
    Dim m_pCloneGeom As IClone ' so can replicate geometry
    Dim m_pGeomCopy As IGeometry ' handle on the copied geometry
    Dim m_pCenterPoint As IPoint ' point to scale about

    Public Sub New()
        MyBase.New()
        m_dFactorX = 1 ' default scale offset is unity ...
        m_dFactorY = 1 ' ... both ways
        m_pCenterPoint = New Point
    End Sub

#Region "Geometric effect"
    ' --------------------------------------------------------
    ' IGeometricEffect interface
    ' Applies the effect to a geometry
    ' --------------------------------------------------------
    Private ReadOnly Property IGeometricEffect_OutputType(ByVal inputType As esriGeometryType) As esriGeometryType Implements IGeometricEffect.OutputType
        Get
            IGeometricEffect_OutputType = esriGeometryType.esriGeometryNull ' assume won't work
            If inputType = esriGeometryType.esriGeometryPolygon Then Return inputType ' OK
            If inputType = esriGeometryType.esriGeometryPolyline Then Return inputType ' OK
        End Get
    End Property

    ' Start the work of applying the effect for this particular feature geometry
    Private Sub IGeometricEffect_Reset(ByVal Geometry As IGeometry) Implements IGeometricEffect.Reset
        m_pGeom = Geometry ' current geometry to be scaled
        m_pGeomCopy = Nothing ' discard any previous geometry copy
        Dim dXCenter As Double ' X coordinate of center
        Dim dYCenter As Double ' Y coordinate of center
        dXCenter = (m_pGeom.Envelope.XMin + m_pGeom.Envelope.XMax) / 2 ' find center ...
        dYCenter = (m_pGeom.Envelope.YMin + m_pGeom.Envelope.YMax) / 2 ' ... from envelope
        m_pCenterPoint.PutCoords(dXCenter, dYCenter) ' and store in a Point
        m_pCenterPoint.SpatialReference = m_pGeom.SpatialReference ' and in same coordinate space
        m_bDone = False ' still work to do
    End Sub

    ' Do the real work - calculate a scaled geometry
    Private Function IGeometricEffect_NextGeometry() As IGeometry Implements IGeometricEffect.NextGeometry
        If m_bDone Then
            IGeometricEffect_NextGeometry = Nothing
        Else
            m_pCloneGeom = m_pGeom ' but we need to copy so don't change original
            m_pGeomCopy = m_pCloneGeom.Clone ' make a copy
            m_pTransform = m_pGeomCopy ' now we need to transform the copy
            With m_pTransform
                .Scale(m_pCenterPoint, m_dFactorX, m_dFactorY) ' by the given factors
            End With
            IGeometricEffect_NextGeometry = m_pGeomCopy ' return the scaled geometry
            m_bDone = True ' no more to do for this geometry
        End If

    End Function

#End Region

#Region "Graphics Attributes"
    ' --------------------------------------------------------
    ' IGraphicAttributes Interface
    ' specifies how this effect appears in the graphic attributes form
    ' --------------------------------------------------------
    ' Friendly name of effect is 'Transform Scale'
    Private ReadOnly Property IGraphicAttributes_ClassName() As String Implements IGraphicAttributes.ClassName
        Get
            Return "Transform Scale VBNet"
        End Get
    End Property

    ' Effect has two editable attributes ...
    Private ReadOnly Property IGraphicAttributes_GraphicAttributeCount() As Integer Implements IGraphicAttributes.GraphicAttributeCount
        Get
            Return 2
        End Get
    End Property

    ' ... with attribute IDs of 0 and 1
    Private ReadOnly Property IGraphicAttributes_ID(ByVal attrIdx As Integer) As Integer Implements IGraphicAttributes.ID
        Get
            IGraphicAttributes_ID = -1
            If attrIdx >= 0 And attrIdx < 2 Then Return attrIdx
        End Get
    End Property

    ' The attributes to the effect are called X and Y Transform Scales ...
    Private ReadOnly Property IGraphicAttributes_IDByName(ByVal Name As String) As Integer Implements IGraphicAttributes.IDByName
        Get
            IGraphicAttributes_IDByName = -1
            If Name = "X Transform Scale" Then Return 0
            If Name = "Y Transform Scale" Then Return 1
        End Get
    End Property

    ' ... corresponding to numbers 0 and 1
    Private ReadOnly Property IGraphicAttributes_Name(ByVal Index As Integer) As String Implements IGraphicAttributes.Name
        Get
            If Index = 0 Then Return "X Transform Scale"
            If Index = 1 Then Return "Y Transform Scale"
            Return ""
        End Get
    End Property

    ' ... and they are size attributes
    Private ReadOnly Property IGraphicAttributes_Type(ByVal Index As Integer) As IGraphicAttributeType Implements IGraphicAttributes.Type
        Get
            IGraphicAttributes_Type = Nothing
            If Index = 0 Then
                Return New GraphicAttributeSizeType
            End If
            If Index = 1 Then
                Return New GraphicAttributeSizeType
            End If
        End Get
    End Property

    ' ... set to value

    ' ... or get current value
    Private Property IGraphicAttributes_Value(ByVal Index As Integer) As Object Implements IGraphicAttributes.Value
        Get
            If Index = 0 Then Return m_dFactorX
            If Index = 1 Then Return m_dFactorY
            Return Nothing
        End Get
        Set(ByVal Value As Object)
            If Index = 0 Then m_dFactorX = Value
            If Index = 1 Then m_dFactorY = Value
        End Set
    End Property
#End Region

#Region "EditInteraction"

    ' --------------------------------------------------------
    ' IeditInteraction interface
    ' Allows use of edit tools to set scales
    ' --------------------------------------------------------
    ' Report back that it has two editable attributes
    Private ReadOnly Property IEditInteraction_IsEditableAttribute(ByVal editParams As Object, ByVal attrIndex As Integer) As Boolean Implements IEditInteraction.IsEditableAttribute
        Get

            Dim pResize As IResizeInteraction
            If TypeOf editParams Is IResizeInteraction Then
                pResize = editParams
                If Not pResize Is Nothing Then
                    If attrIndex = 0 Or attrIndex = 1 Then
                        Return True
                    End If
                End If
            End If
            Return False
        End Get
    End Property
    ' Get the amounts in X and Y from the Resize tool
    Private Sub IEditInteraction_ModifyAttributes(ByVal editParams As Object, ByVal attrs As Object) Implements IEditInteraction.ModifyAttributes
        Dim pResize As IResizeInteraction
        If TypeOf editParams Is IResizeInteraction Then
            pResize = editParams
            If Not pResize Is Nothing Then
                If attrs(0) = True Then
                    m_dFactorX = m_dFactorX * pResize.RatioX
                End If
                If attrs(1) = True Then
                    m_dFactorY = m_dFactorY * pResize.RatioY
                End If
            End If
        End If
    End Sub
#End Region

#Region "Persistence"
    ' --------------------------------------------------------
    ' IPersistVariant interface
    ' Sets up persistence for this class, to store the current offset values
    ' --------------------------------------------------------
    ' Allocate a unique ID GUID for it
    Public ReadOnly Property ID() As ESRI.ArcGIS.esriSystem.UID Implements ESRI.ArcGIS.esriSystem.IPersistVariant.ID
        Get
            Dim pUID As IUID
            pUID = New UID
            pUID.Value = "GETransformVBNet.clsGE_Transform_Scale"
            ID = pUID
        End Get
    End Property

    ' read the offset values from the stream
    Private Sub IPersistVariant_Load(ByVal Stream As IVariantStream) Implements IPersistVariant.Load
        Dim version As Integer
        version = Stream.Read
        m_dFactorX = Stream.Read
        m_dFactorY = Stream.Read
    End Sub

    ' Write the offset values to the stream
    Private Sub IPersistVariant_Save(ByVal Stream As IVariantStream) Implements IPersistVariant.Save
        Dim version As Integer
        version = 1
        Stream.Write(version)
        Stream.Write(m_dFactorX)
        Stream.Write(m_dFactorY)
    End Sub
#End Region

End Class