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
Option Strict Off
Option Explicit On

Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Geometry
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.ADF.CATIDs

Public Class AroundPoint
    Implements IGraphicAttributes
    Implements IMarkerPlacement
    Implements IPersistVariant


#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "0021E82A-D5E4-45a9-879F-DB6CD5AA07B9"
#End Region

#Region "Component Category Registration"
    <ComRegisterFunction(), ComVisibleAttribute(False)> _
    Public Shared Sub RegisterFunction(ByVal regkey As String)
        MarkerPlacement.Register(regkey)
    End Sub
    <ComUnregisterFunction(), ComVisibleAttribute(False)> _
    Public Shared Sub UnRegisterFunction(ByVal regkey As String)
        MarkerPlacement.Unregister(regkey)
    End Sub
#End Region

    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.

    '------------------------------------------------------------------------------------
    ' This class has to be registered in COM category ESRI Representation Marker placement
    '------------------------------------------------------------------------------------

    Dim m_Radius As Double
    Dim m_Number As Integer
    Dim m_X0 As Double
    Dim m_Y0 As Double
    Dim m_Iter As Integer
    Dim m_pT As IAffineTransformation2D

    Public ReadOnly Property ClassName() As String Implements ESRI.ArcGIS.Display.IGraphicAttributes.ClassName
        Get
            ClassName = "AroundPoint"
        End Get
    End Property

    Public ReadOnly Property GraphicAttributeCount() As Integer Implements ESRI.ArcGIS.Display.IGraphicAttributes.GraphicAttributeCount
        Get
            GraphicAttributeCount = 2
        End Get
    End Property

    Public ReadOnly Property ID(ByVal attrIndex As Integer) As Integer Implements ESRI.ArcGIS.Display.IGraphicAttributes.ID
        Get
            ID = -1
            If attrIndex >= 0 And attrIndex < 2 Then ID = attrIndex
        End Get
    End Property

    Public ReadOnly Property IDByName(ByVal Name As String) As Integer Implements ESRI.ArcGIS.Display.IGraphicAttributes.IDByName
        Get
            IDByName = -1
            If Name = "Radius" Then IDByName = 0
            If Name = "Number" Then IDByName = 1
        End Get
    End Property

    Public ReadOnly Property Name(ByVal attrId As Integer) As String Implements ESRI.ArcGIS.Display.IGraphicAttributes.Name
        Get
            Name = ""
            If attrId = 0 Then Name = "Radius"
            If attrId = 1 Then Name = "Number"
        End Get
    End Property

    Public ReadOnly Property Type(ByVal attrId As Integer) As ESRI.ArcGIS.Display.IGraphicAttributeType Implements ESRI.ArcGIS.Display.IGraphicAttributes.Type
        Get
            Type = Nothing

            If attrId = 0 Then
                Type = New GraphicAttributeSizeType
            End If
            If attrId = 1 Then
                Type = New GraphicAttributeIntegerType
            End If
        End Get
    End Property

    Public Property Value(ByVal attrId As Integer) As Object Implements ESRI.ArcGIS.Display.IGraphicAttributes.Value
        Get
            If attrId = 0 Then Value = m_Radius
            If attrId = 1 Then Value = m_Number
        End Get
        Set(ByVal value As Object)
            If attrId = 0 Then m_Radius = value
            If attrId = 1 Then m_Number = value
        End Set
    End Property

    Public ReadOnly Property AcceptGeometryType(ByVal inputType As ESRI.ArcGIS.Geometry.esriGeometryType) As Boolean Implements ESRI.ArcGIS.Display.IMarkerPlacement.AcceptGeometryType
        Get
            If inputType = esriGeometryType.esriGeometryPoint Then
                AcceptGeometryType = True
            Else
                AcceptGeometryType = False
            End If
        End Get
    End Property

    Public Function NextTransformation() As ESRI.ArcGIS.Geometry.IAffineTransformation2D Implements ESRI.ArcGIS.Display.IMarkerPlacement.NextTransformation
        NextTransformation = Nothing
        Dim angle As Double
        Dim x As Object
        Dim y As Double
        If m_Iter <> m_Number Then
            angle = m_Iter * 2 * 3.141592 / m_Number
            x = m_X0 + m_Radius * System.Math.Cos(angle)
            y = m_Y0 + m_Radius * System.Math.Sin(angle)
            m_pT.Reset()
            m_pT.Rotate(angle)
            m_pT.Move(x, y)
            NextTransformation = m_pT
            m_Iter = m_Iter + 1
        End If
    End Function

    Public Sub Reset(ByVal geom As ESRI.ArcGIS.Geometry.IGeometry) Implements ESRI.ArcGIS.Display.IMarkerPlacement.Reset
        m_Iter = -1
        Dim pP As IPoint
        pP = geom
        If Not pP Is Nothing Then
            pP.QueryCoords(m_X0, m_Y0)
            m_Iter = 0
        End If
    End Sub

    Public ReadOnly Property ID1() As ESRI.ArcGIS.esriSystem.UID Implements ESRI.ArcGIS.esriSystem.IPersistVariant.ID
        Get
            Dim pUID As UID
            pUID = New UID
            pUID.Value = "AroundPoint_MP.AroundPoint"
            ID1 = pUID
        End Get
    End Property

    Public Sub Load(ByVal Stream As ESRI.ArcGIS.esriSystem.IVariantStream) Implements ESRI.ArcGIS.esriSystem.IPersistVariant.Load
        Dim version As Integer
        version = CType(Stream.Read, Integer)
        m_Radius = CType(Stream.Read, Double)
        m_Number = CType(Stream.Read, Double)
    End Sub

    Public Sub Save(ByVal Stream As ESRI.ArcGIS.esriSystem.IVariantStream) Implements ESRI.ArcGIS.esriSystem.IPersistVariant.Save
        Dim version As Integer
        version = 1
        Stream.Write(version)
        Stream.Write(m_Radius)
        Stream.Write(m_Number)
    End Sub

    Public Sub New()
        m_Radius = 10
        m_Number = 6
        m_pT = New AffineTransformation2D
    End Sub
End Class


