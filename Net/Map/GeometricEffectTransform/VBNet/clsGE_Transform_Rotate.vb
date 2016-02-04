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
Public Class clsGE_Transform_Rotate
    Implements IGeometricEffect
    Implements IGraphicAttributes
    Implements IEditInteraction
    Implements IPersistVariant

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "BA911714-92DF-4691-9700-5F3E67B08E70"
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
    ' GE_Transform_Rotate
    ' ArcGIS developer sample of a simple geometric effect for use in representation rules
    ' It rotates the display geometry by a given x,y amount
    ' ------------------------------------------------------------------------------------
    ' Declare private variables used by this class
    Dim m_dAngle As Double ' the angle to rotate
    Dim m_bDone As Boolean ' flag for no more geometries
    Dim m_pGeom As IGeometry ' current geometry being rotated
    Dim m_pTransform As ITransform2D ' geometry transformation
    Dim m_pCloneGeom As IClone ' so can replicate geometry
    Dim m_pGeomCopy As IGeometry ' handle on the copied geometry
    Dim m_pCenterPoint As IPoint ' point to rotate about

    ' Initialize method gets called once, to set things up
    Private Sub Class_Initialize_Renamed()
        m_dAngle = 0 ' default rotate angle is zero
        m_pCenterPoint = New Point
    End Sub
    Public Sub New()
        MyBase.New()
        Class_Initialize_Renamed()
    End Sub

    ' --------------------------------------------------------
    ' IGeometricEffect interface
    ' Applies the effect to a geometry
    ' --------------------------------------------------------

    ' Determine whether line, polygon, or point geometry
    Private ReadOnly Property OutputType(ByVal inputType As esriGeometryType) As esriGeometryType Implements IGeometricEffect.OutputType
        Get
            If inputType = esriGeometryType.esriGeometryPolygon Then Return inputType ' OK
            If inputType = esriGeometryType.esriGeometryPolyline Then Return inputType ' OK
            Return esriGeometryType.esriGeometryNull ' assume won't work
        End Get
    End Property

    ' Start the work of applying the effect for this particular feature geometry
    Private Sub IGeometricEffect_Reset(ByVal Geometry As IGeometry) Implements IGeometricEffect.Reset
        m_pGeom = Geometry ' current geometry to be rotated
        m_pGeomCopy = Nothing ' discard any previous geometry copy
        Dim dXCenter As Double ' X coordinate of center
        Dim dYCenter As Double ' Y coordinate of center
        dXCenter = (m_pGeom.Envelope.XMin + m_pGeom.Envelope.XMax) / 2 ' find center ...
        dYCenter = (m_pGeom.Envelope.YMin + m_pGeom.Envelope.YMax) / 2 ' ... from envelope
        m_pCenterPoint.PutCoords(dXCenter, dYCenter) ' and store in a Point
        m_pCenterPoint.SpatialReference = m_pGeom.SpatialReference ' and in same coordinate space
        m_bDone = False ' still work to do
    End Sub

    ' Do the real work - calculate a rotated geometry
    Private Function IGeometricEffect_NextGeometry() As IGeometry Implements IGeometricEffect.NextGeometry
        If m_bDone Then
            IGeometricEffect_NextGeometry = Nothing
        Else
            m_pCloneGeom = m_pGeom ' but we need to copy so don't change original
            m_pGeomCopy = m_pCloneGeom.Clone ' make a copy
            m_pTransform = m_pGeomCopy ' now we need to transform the copy
            m_pTransform.Rotate(m_pCenterPoint, m_dAngle) ' by the angle
            IGeometricEffect_NextGeometry = m_pGeomCopy ' return the rotated geometry
            m_bDone = True ' no more to do for this geometry
        End If

    End Function

    ' --------------------------------------------------------
    ' IGraphicAttributes Interface
    ' specifies how this effect appears in the graphic attributes form
    ' --------------------------------------------------------

    ' Friendly name of effect is 'Transform Rotate'
    Private ReadOnly Property ClassName() As String Implements IGraphicAttributes.ClassName
        Get
            Return "Transform Rotate VBNet"
        End Get
    End Property

    ' Effect has one editable attribute ...
    Private ReadOnly Property GraphicAttributeCount() As Integer Implements IGraphicAttributes.GraphicAttributeCount
        Get
            Return 1
        End Get
    End Property

    ' ... with attribute ID of 0
    Private ReadOnly Property IGraphicAttributes_ID(ByVal attrIdx As Integer) As Integer Implements IGraphicAttributes.ID
        Get
            IGraphicAttributes_ID = -1
            If attrIdx >= 0 And attrIdx < 1 Then Return attrIdx
        End Get
    End Property

    ' The attribute to the effect is called Angle ...
    Private ReadOnly Property IGraphicAttributes_IDByName(ByVal Name As String) As Integer Implements IGraphicAttributes.IDByName
        Get
            IGraphicAttributes_IDByName = -1
            If Name = "Transform Angle" Then Return 0
        End Get
    End Property

    ' ... corresponding to number 0
    Private ReadOnly Property IGraphicAttributes_Name(ByVal Index As Integer) As String Implements IGraphicAttributes.Name
        Get
            If Index = 0 Then Return "Transform Angle"
            Return ""
        End Get
    End Property

    ' ... and it is a double attribute
    Private ReadOnly Property IGraphicAttributes_Type(ByVal Index As Integer) As IGraphicAttributeType Implements IGraphicAttributes.Type
        Get
            If Index = 0 Then
                Return New GraphicAttributeDoubleType
            End If
            If Index = 1 Then
                Return New GraphicAttributeDoubleType
            End If
            Return Nothing
        End Get
    End Property

    ' ... set to value

    ' ... or get current value
    Private Property IGraphicAttributes_Value(ByVal Index As Integer) As Object Implements IGraphicAttributes.Value
        Get
            If Index = 0 Then Return m_dAngle * 360.0# / 3.141592653
            Return 0
        End Get
        Set(ByVal Value As Object)
            If Index = 0 Then m_dAngle = Value / 360.0# * 3.141592653
        End Set
    End Property


    ' --------------------------------------------------------
    ' IeditInteraction interface
    ' Allows use of edit tools to set rotate angles
    ' --------------------------------------------------------

    ' Report back that it has an editable attributes
    Private ReadOnly Property IsEditableAttribute(ByVal editParams As Object, ByVal attrIndex As Integer) As Boolean Implements IEditInteraction.IsEditableAttribute
        Get
            Dim pRotate As IRotateInteraction
            If TypeOf editParams Is IRotateInteraction Then
                pRotate = editParams
                If Not pRotate Is Nothing Then
                    If attrIndex = 0 Then
                        Return True
                    End If
                End If
            End If
            Return False
        End Get
    End Property


    ' --------------------------------------------------------
    ' IPersistVariant interface
    ' Sets up persistence for this class, to store the current angle value
    ' --------------------------------------------------------

    ' Allocate a unique ID GUID for it
    Private ReadOnly Property IPersistVariant_ID() As UID Implements IPersistVariant.ID
        Get
            Dim pUID As IUID
            pUID = New UID
            pUID.Value = "GETransformVBNet.clsGE_Transform_Rotate"
            Return pUID
        End Get
    End Property


    ' Get the angle from the Rotate tool
    Private Sub IEditInteraction_ModifyAttributes(ByVal editParams As Object, ByVal attrs As Object) Implements IEditInteraction.ModifyAttributes
        Dim pRotate As IRotateInteraction
        If TypeOf editParams Is IRotateInteraction Then

            pRotate = editParams
            If Not pRotate Is Nothing Then
                If attrs(0) = True Then
                    m_dAngle = pRotate.Angle
                End If
            End If
        End If
    End Sub

    ' read the angle value from the stream
    Private Sub IPersistVariant_Load(ByVal Stream As IVariantStream) Implements IPersistVariant.Load
        Dim version As Integer
        version = Stream.Read
        m_dAngle = Stream.Read
    End Sub

    ' Write the angle value to the stream
    Private Sub IPersistVariant_Save(ByVal Stream As IVariantStream) Implements IPersistVariant.Save
        Dim version As Integer
        version = 1
        Stream.Write(version)
        Stream.Write(m_dAngle)
    End Sub

    
End Class