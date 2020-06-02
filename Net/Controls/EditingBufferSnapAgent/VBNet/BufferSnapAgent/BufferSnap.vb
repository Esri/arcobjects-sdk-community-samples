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
Imports System
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Controls
 
Namespace BufferSnapVB
  
  '/ <summary>
  '/ Uses the Create Feature event to turn on the extension, which 
  '/ implements a snapping agent. The Buffer Snap agent is based on a buffer
  '/ around the points of the first editable point feature class.
  '/ A buffer of 1000 map units is created if the next point feature created
  '/ is within the tolerance it is snapped to the buffer ring. 
  '/ </summary>
  <Guid("A7BE542E-6C0D-423f-8824-FFC7B6ADF0B4"), ClassInterface(ClassInterfaceType.None), ProgId("BufferSnapVB.BufferSnap")> _
  Public Class BufferSnap
    Implements IEngineSnapAgent
    Implements IEngineSnapAgentCategory
    Implements IPersistVariant
        Implements IExtension


#Region "COM Registration Function(s)"
    <ComRegisterFunction(), ComVisible(False)> _
    Public Shared Sub RegisterFunction(ByVal registerType As Type)
      ' Required for ArcGIS Component Category Registrar support
      ArcGISCategoryRegistration(registerType)
    End Sub

    <ComUnregisterFunction(), ComVisible(False)> _
    Public Shared Sub UnregisterFunction(ByVal registerType As Type)
      ' Required for ArcGIS Component Category Registrar support
      ArcGISCategoryUnregistration(registerType)

    End Sub

#Region "ArcGIS Component Category Registrar generated code"
    '/ <summary>
    '/ Required method for ArcGIS Component Category registration -
    '/ Do not modify the contents of this method with the code editor.
    '/ </summary>
    Private Shared Sub ArcGISCategoryRegistration(ByVal registerType As Type)
      Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
      EngineSnapAgents.Register(regKey)


    End Sub
    '/ <summary>
    '/ Required method for ArcGIS Component Category unregistration -
    '/ Do not modify the contents of this method with the code editor.
    '/ </summary>
    Private Shared Sub ArcGISCategoryUnregistration(ByVal registerType As Type)
      Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
      EngineSnapAgents.Unregister(regKey)

    End Sub

#End Region
#End Region

    'declare and initialize class variables.
    Private m_featureCache As IFeatureCache
    Private m_featureClass As IFeatureClass
    Private m_editor As IEngineEditor

    Public Sub New()
    End Sub

#Region "IPersist Variant Members."

    ''' <summary>
    ''' Get the ID of the object.
    ''' </summary>
    Private ReadOnly Property ID() As ESRI.ArcGIS.esriSystem.UID Implements ESRI.ArcGIS.esriSystem.IPersistVariant.ID
      Get
        Dim pID As New UID
        pID.Value = "BufferSnapVB.BufferSnap"
        Return pID
      End Get
    End Property

    Private Sub Load(ByVal stream As ESRI.ArcGIS.esriSystem.IVariantStream) Implements ESRI.ArcGIS.esriSystem.IPersistVariant.Load

    End Sub

    Private Sub Save(ByVal Stream As ESRI.ArcGIS.esriSystem.IVariantStream) Implements ESRI.ArcGIS.esriSystem.IPersistVariant.Save

    End Sub
#End Region

#Region "IEngineSnapAgent Implementations"

    Public ReadOnly Property Name() As String Implements IEngineSnapAgent.Name, IExtension.Name
      Get
        Return "Buffer Snap VB"
      End Get
    End Property


    Public Function Snap(ByVal geom As IGeometry, ByVal point As IPoint, ByVal tolerance As Double) As Boolean Implements IEngineSnapAgent.Snap
      GetFeatureClass()

      Dim b_setNewFeatureCache As Boolean = False

      If m_featureClass Is Nothing Or m_editor Is Nothing Then
        Return False
      End If

      If m_featureClass.ShapeType <> esriGeometryType.esriGeometryPoint Then
        Return False
      End If

      'Check if a feature cache has been created.
      If Not b_setNewFeatureCache Then
        m_featureCache = New FeatureCache()
        b_setNewFeatureCache = True
      End If

      'Fill the New Cache with the geometries.
      'It is up to the developer to choose an appropriate value
      'given the map units and the scale at which editing will be undertaken.
      FillCache(m_featureClass, point, 10000)
       
      Dim proximityOp As IProximityOperator = DirectCast(point, IProximityOperator)
      Dim minDist As Double = tolerance
      Dim cachePt As IPoint = New PointClass()
      Dim snapPt As IPoint = New PointClass()
      Dim outPoly As IPolygon = New PolygonClass()
      Dim topoOp As ITopologicalOperator

      Dim feature As IFeature
      Dim Index As Integer = 0
      Dim Count As Integer
      For Count = 0 To m_featureCache.Count - 1 Step Count + 1
        feature = m_featureCache.Feature(Count)
        cachePt = feature.Shape
        topoOp = cachePt

        'Set the buffer distance to an appropriate value
        'given the map units and data being edited
        outPoly = topoOp.Buffer(1000)

        Dim Dist As Double = proximityOp.ReturnDistance(outPoly)
        If Dist < minDist Then
          Index = Count
          minDist = Dist
        End If
      Next

      'Make sure minDist is within the search tolerance.
      If minDist >= tolerance Then
        Return False
      End If

      'Retrieve the feature and its part again.
      feature = m_featureCache.Feature(Index)
      cachePt = feature.Shape
      topoOp = cachePt

      'Set the buffer distance to an appropriate value
      'given the map units and data being edited
      outPoly = topoOp.Buffer(1000)
      proximityOp = outPoly
      snapPt = proximityOp.ReturnNearestPoint(point, esriSegmentExtension.esriNoExtension)

      'Since point was passed in ByValue, we have to modify its values instead.
      'of giving it a new address.
      point.PutCoords(snapPt.X, snapPt.Y)

      Return True

    End Function

    Private Sub FillCache(ByVal FClass As IFeatureClass, ByVal pPoint As IPoint, ByVal Distance As Double)
      m_featureCache.Initialize(pPoint, Distance)
      m_featureCache.AddFeatures(FClass)
    End Sub

#End Region

#Region "IEngineSnapAgentCategory Implementation"
    Public ReadOnly Property Category() As String Implements IEngineSnapAgentCategory.Category
      Get
        Return "Buffer Snap Category VB"
      End Get
    End Property
#End Region

    Private Sub GetFeatureClass()
      Dim map As IMap = m_editor.Map
      Dim snapLayers As IEngineEditLayers = m_editor
      Dim featLayer As IFeatureLayer = snapLayers.TargetLayer

      'Search the editable layers and set the snap feature class to the point layer.
      Dim CountLayers As Integer
      For CountLayers = 0 To map.LayerCount - 1 Step CountLayers + 1
        If featLayer Is Nothing Then
          Return
        End If

        If featLayer.FeatureClass.ShapeType <> esriGeometryType.esriGeometryPoint Then
          Return
        Else
          m_featureClass = featLayer.FeatureClass
        End If
      Next
    End Sub

#Region "IExtension Members"

    Public Sub Shutdown() Implements IExtension.Shutdown
      m_editor = Nothing
    End Sub

    Public Sub Startup(ByRef initializationData As Object) Implements IExtension.Startup
      If initializationData IsNot Nothing AndAlso TypeOf initializationData Is IEngineEditor Then
        m_editor = DirectCast(initializationData, IEngineEditor)
      End If


    End Sub
#End Region
  End Class
End Namespace
