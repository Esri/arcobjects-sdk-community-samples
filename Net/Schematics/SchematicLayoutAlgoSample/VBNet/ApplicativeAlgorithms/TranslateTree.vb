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
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Schematic
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Geometry

<ClassInterface(ClassInterfaceType.None)> _
<Guid(TranslateTree.GUID)> _
<ProgId(TranslateTree.PROGID)> _
Public Class TranslateTree

    Implements ISchematicAlgorithm
    Implements ISchematicJSONParameters
    Implements ITranslateTree


    ' private member data
    Public Const GUID As String = "F80339F6-9E87-4a75-AF8E-1B44C76D7D96"
    Public Const PROGID As String = "ApplicativeAlgorithms.TranslateTreeVB"

    ' property names (for the algorithm property set)
    Private Const TranslationFactorXName As String = "Translation Factor X"
    Private Const TranslationFactorYName As String = "Translation Factor Y"


    ' The JSON parameter names 
    Private Const JSONTranslationFactorX As String = "TranslationFactorX"
    Private Const JSONTranslationFactorY As String = "TranslationFactorY"

    'Algorithms parameters JSON representation Names used by the REST interface 
    Private Const JSONName As String = "name"
    Private Const JSONType As String = "type"
    Private Const JSONValue As String = "value"
    'Algorithms parameters JSON representation Types used by the REST interface
    Private Const JSONLong As String = "Long"
    Private Const JSONDouble As String = "Double"
    Private Const JSONBoolean As String = "Boolean"
    Private Const JSONString As String = "String"

    Private m_algoLabel As String = "Translate Tree VBNet"

    Private m_available As Boolean
    Private m_overridable As Boolean
    Private m_useRootNode As Boolean
    Private m_useEndNode As Boolean

    Private m_paramX As Double
    Private m_paramY As Double

    Private m_schematicDiagramClassName As ISchematicDiagramClassName

    Public Sub New()

        m_paramX = 50.0
        m_paramY = 50.0
        m_available = True    ' In this example, the algorithm is available by default
        m_overridable = True   ' user is allowed to edit the parameters
        m_useRootNode = False ' don't need the user to define root nodes
        m_useEndNode = False   ' don't need the user to define an end node

        m_schematicDiagramClassName = Nothing
    End Sub

    Protected Overrides Sub Finalize()
        m_schematicDiagramClassName = Nothing
    End Sub


#Region "COM Registration Function(s)"
    <ComRegisterFunction()> _
    <ComVisibleAttribute(True)> _
    Public Shared Sub Reg(ByVal sKey As String)
        SchematicAlgorithms.Register(sKey)
    End Sub

    <ComUnregisterFunction()> _
    <ComVisibleAttribute(True)> _
    Public Shared Sub Unreg(ByVal sKey As String)
        SchematicAlgorithms.Unregister(sKey)
    End Sub
#End Region

#Region "Implements ITranslateTree"
    Public Property TranslationFactorX() As Double Implements ITranslateTree.TranslationFactorX
        Get
            Return m_paramX
        End Get
        Set(ByVal value As Double)
            m_paramX = value
        End Set
    End Property

    Public Property TranslationFactorY() As Double Implements ITranslateTree.TranslationFactorY
        Get
            Return m_paramY
        End Get
        Set(ByVal value As Double)
            m_paramY = value
        End Set
    End Property
#End Region

#Region "Implements ISchematicAlgorithm"

    Public ReadOnly Property Enabled(Optional ByVal schematicLayer As ESRI.ArcGIS.Schematic.ISchematicLayer = Nothing) As Boolean Implements ESRI.ArcGIS.Schematic.ISchematicAlgorithm.Enabled
        Get

            Dim enumFeatures As IEnumSchematicFeature
            Dim schemFeature As ISchematicFeature

            Dim iCount As Integer = 0

            If (schematicLayer Is Nothing) Then Return False

            ' an algorithm needs the diagram to be in editing mode in order to run
            If (Not schematicLayer.IsEditingSchematicDiagram()) Then Return False

            enumFeatures = schematicLayer.GetSchematicSelectedFeatures(True)
            If (enumFeatures Is Nothing) Then Return False

            ' Count the selected nodes
            enumFeatures.Reset()
            schemFeature = enumFeatures.Next()
            While (schemFeature IsNot Nothing And iCount < 2)

                Dim inMemoryFeatureClass As ISchematicInMemoryFeatureClass = Nothing
                inMemoryFeatureClass = TryCast(schemFeature.Class, ISchematicInMemoryFeatureClass)

                If (inMemoryFeatureClass Is Nothing) Then
                    schemFeature = enumFeatures.Next()
                    Continue While
                End If

                If (inMemoryFeatureClass.SchematicElementClass.SchematicElementType = esriSchematicElementType.esriSchematicNodeType) Then
                    iCount += 1
                End If
                schemFeature = enumFeatures.Next()
            End While

            If (iCount = 1) Then
                Return True     'just want one selected node
            Else
                Return False
            End If
        End Get
    End Property


    Public Property Available() As Boolean Implements ISchematicAlgorithm.Available
        Get
            Return m_available
        End Get
        Set(ByVal value As Boolean)
            m_available = value
        End Set
    End Property

    ' enclose the name of the property by brackets since the word is reserved (in VBNet)
    Public Property [Overridable]() As Boolean Implements ISchematicAlgorithm.Overridable
        Get
            Return m_overridable
        End Get
        Set(ByVal value As Boolean)
            m_overridable = value
        End Set
    End Property

    Public Property SchematicDiagramClassName() As ISchematicDiagramClassName Implements ISchematicAlgorithm.SchematicDiagramClassName
        Get
            Return m_schematicDiagramClassName
        End Get
        Set(ByVal value As ISchematicDiagramClassName)
            m_schematicDiagramClassName = value
        End Set
    End Property

    Public Property Label() As String Implements ISchematicAlgorithm.Label
        Get
            Return m_algoLabel
        End Get
        Set(ByVal value As String)
            m_algoLabel = value
        End Set
    End Property

    Public ReadOnly Property UseRootNode() As Boolean Implements ISchematicAlgorithm.UseRootNode
        Get
            Return m_useRootNode
        End Get
    End Property

    Public ReadOnly Property UseEndNode() As Boolean Implements ISchematicAlgorithm.UseEndNode
        Get
            Return m_useEndNode
        End Get
    End Property

    Public Property PropertySet() As IPropertySet Implements ISchematicAlgorithm.PropertySet
        Get
            ' build the property set
            Dim builtPropertySet As New ESRI.ArcGIS.esriSystem.PropertySet

            If (builtPropertySet Is Nothing) Then
                Return Nothing
            End If

            builtPropertySet.SetProperty(TranslationFactorXName, m_paramX)
            builtPropertySet.SetProperty(TranslationFactorYName, m_paramY)

            Return builtPropertySet
        End Get

        Set(ByVal value As IPropertySet)
            Dim propSet As IPropertySet = value
            Dim oneParameter As Object

            If (propSet IsNot Nothing) Then
                Try
                    oneParameter = propSet.GetProperty(TranslationFactorXName)
                    m_paramX = CDbl(oneParameter)

                    oneParameter = propSet.GetProperty(TranslationFactorYName)
                    m_paramY = CDbl(oneParameter)
                Catch ex As Exception
                End Try
            End If

        End Set
    End Property


    Public ReadOnly Property AlgorithmCLSID() As String Implements ISchematicAlgorithm.AlgorithmCLSID
        Get
            Return "{" & GUID & "}"
        End Get
    End Property

    Public Sub Execute(Optional ByVal schematicLayer As ESRI.ArcGIS.Schematic.ISchematicLayer = Nothing, Optional ByVal cancelTracker As ESRI.ArcGIS.esriSystem.ITrackCancel = Nothing) Implements ESRI.ArcGIS.Schematic.ISchematicAlgorithm.Execute

        If (schematicLayer Is Nothing) Then Return

        Dim inMemoryDiagram As ISchematicInMemoryDiagram
        inMemoryDiagram = schematicLayer.SchematicInMemoryDiagram

        ' Core algorithm
        InternalExecute(schematicLayer, inMemoryDiagram, cancelTracker)

        ' Release the COM objects
        If (inMemoryDiagram IsNot Nothing) Then
            While (System.Runtime.InteropServices.Marshal.ReleaseComObject(inMemoryDiagram) > 0)
            End While
        End If

        While (System.Runtime.InteropServices.Marshal.ReleaseComObject(schematicLayer) > 0)
        End While

    End Sub

 

    Private Sub InternalExecute(ByVal schematicLayer As ESRI.ArcGIS.Schematic.ISchematicLayer,
                                ByVal inMemoryDiagram As ESRI.ArcGIS.Schematic.ISchematicInMemoryDiagram,
                                Optional ByVal cancelTracker As ESRI.ArcGIS.esriSystem.ITrackCancel = Nothing)

        If (schematicLayer Is Nothing Or inMemoryDiagram Is Nothing) Then Return

        '''''''''''''''''''''''''''''''''''''''''''

        ' get the diagram spatial reference for geometry transformation
        Dim geoDataset As IGeoDataset = CType(inMemoryDiagram, IGeoDataset)
        If (geoDataset Is Nothing) Then Return
        Dim spatialReference As ISpatialReference = geoDataset.SpatialReference

        Dim diagramClass As ISchematicDiagramClass
        diagramClass = inMemoryDiagram.SchematicDiagramClass
        If (diagramClass Is Nothing) Then Return

        Dim schemDataset As ISchematicDataset
        schemDataset = diagramClass.SchematicDataset
        If (schemDataset Is Nothing) Then Return

        Dim algorithmEventsTrigger As ISchematicAlgorithmEventsTrigger
        algorithmEventsTrigger = CType(schemDataset, ISchematicAlgorithmEventsTrigger)
        If (algorithmEventsTrigger Is Nothing) Then Return

        Dim layer As ESRI.ArcGIS.Carto.ILayer = CType(schematicLayer, ESRI.ArcGIS.Carto.ILayer)
        Dim algorithm As ISchematicAlgorithm = CType(Me, ISchematicAlgorithm)
        Dim canExecute As Boolean

        algorithmEventsTrigger.FireBeforeExecuteAlgorithm(layer, algorithm, canExecute)
        If Not canExecute Then Return ' cannot execute

        ' Get the selected Features
        Dim enumFeatures As IEnumSchematicFeature = schematicLayer.GetSchematicSelectedFeatures(True)
        If (enumFeatures Is Nothing) Then Return

        ' Count the selected nodes
        Dim inMemoryFeatureClass As ISchematicInMemoryFeatureClass
        Dim selectedFeature As ISchematicFeature = Nothing
        Dim iCount As Integer = 0
        Dim schemFeature As ISchematicFeature
        enumFeatures.Reset()
        schemFeature = enumFeatures.Next()
        While (schemFeature IsNot Nothing AndAlso iCount < 2)
            ' just want SchematicFeatureNode
            inMemoryFeatureClass = CType(schemFeature.Class, ISchematicInMemoryFeatureClass)

            If (inMemoryFeatureClass.SchematicElementClass.SchematicElementType = esriSchematicElementType.esriSchematicNodeType) Then
                selectedFeature = schemFeature
                iCount += 1
            End If
            schemFeature = enumFeatures.Next()
        End While

        If (iCount <> 1 OrElse selectedFeature Is Nothing) Then Return ' must be only one

        ' Create a new SchematicAnalystFindConnected algorithm
        Dim analystFindConnected As ISchematicAnalystFindConnected = Nothing

        analystFindConnected = CType(New SchematicAnalystFindConnected(), ISchematicAnalystFindConnected)
        If (analystFindConnected Is Nothing) Then Return

        ' Modifying parameters value for this SchematicAnalystFindConnected algorithm so that when it is launched the trace result appears   a selection set{
        analystFindConnected.SelectLink = True
        analystFindConnected.SelectNode = True
        analystFindConnected.UseFlow = False
        'pAnalystFindConnected.FlowDirection = 1
        ' Execute the algorithm
        analystFindConnected.Execute(schematicLayer, cancelTracker)

        ' Retrieving the trace result (if any)
        Dim resultFeatures As IEnumSchematicFeature
        resultFeatures = analystFindConnected.TraceResult

        ' free the schematic analyst COM object
        While (System.Runtime.InteropServices.Marshal.ReleaseComObject(analystFindConnected) > 0)
        End While

        If (resultFeatures Is Nothing OrElse resultFeatures.Count < 1) Then Return

        ' Apply the translation to the result
        ' Translating each traced elements according to the TranslationFactorX and TranslationFactorY parameters current values
        Dim inMemoryFeature As ISchematicInMemoryFeature
        resultFeatures.Reset()
        inMemoryFeature = CType(resultFeatures.Next(), ISchematicInMemoryFeature)
        While (inMemoryFeature IsNot Nothing)
            Dim geometry As IGeometry
            Dim transform As ITransform2D
            Dim elemType As esriSchematicElementType

            inMemoryFeatureClass = CType(inMemoryFeature.Class, ISchematicInMemoryFeatureClass)
            elemType = inMemoryFeatureClass.SchematicElementClass.SchematicElementType
            If (elemType = esriSchematicElementType.esriSchematicLinkType OrElse elemType = esriSchematicElementType.esriSchematicNodeType) Then
                ' get a copy of the feature geometry
                ' then process the cloned geometry rather than the feature geometry directly
                ' Thus the modifications are stored in the heap of the current operation
                ' meaning it can be undone then redo (undo/redo)
                geometry = inMemoryFeature.ShapeCopy
                ' Convert the geometry into the SpatialReference of diagram class
                geometry.Project(spatialReference)
                ' Move the geometry
                transform = CType(geometry, ITransform2D)
                If (transform IsNot Nothing) Then
                    transform.Move(m_paramX, m_paramY)

                    ' Convert the moved geometry into the spatial reference of storage
                    ' and feed it back to the feature
                    Dim table As IObjectClass = inMemoryFeature.Class
                    If (table Is Nothing) Then Continue While

                    Dim featureGeoDataset As IGeoDataset = CType(table, IGeoDataset)
                    If (featureGeoDataset Is Nothing) Then Continue While

                    Dim featureSpatialRef As ISpatialReference = featureGeoDataset.SpatialReference
                    If (featureSpatialRef Is Nothing) Then Continue While

                    Dim movedGeometry As IGeometry = CType(transform, IGeometry)
                    movedGeometry.Project(featureSpatialRef)

                    inMemoryFeature.Shape = movedGeometry
                End If
            End If
            inMemoryFeature = CType(resultFeatures.Next(), ISchematicInMemoryFeature)
        End While

        ' After Execute part
        algorithmEventsTrigger.FireAfterExecuteAlgorithm(layer, algorithm)

        ' update the diagram extent
        schematicLayer.UpdateExtent()

    End Sub


#End Region


#Region "Implements ISchematicJSONParameters"

    ' ISchematicJSONParameters interface : Defines its properties and methods (mandatory to run on server)
    Public ReadOnly Property JSONParametersArray1() As IJSONArray Implements ISchematicJSONParameters.JSONParametersArray
        Get

            Dim aJSONArray As New JSONArray
            Dim oJSONObject1 As IJSONObject = New JSONObject
            Dim oJSONObject2 As IJSONObject = New JSONObject

            ' build JSON object for the first parameter
            oJSONObject1.AddString(JSONName, JSONTranslationFactorX)
            oJSONObject1.AddString(JSONType, JSONDouble)
            oJSONObject1.AddDouble(JSONValue, m_paramX)

            aJSONArray.AddJSONObject(oJSONObject1)

            'build JSON object for the second parameter
            oJSONObject2.AddString(JSONName, JSONTranslationFactorY)
            oJSONObject2.AddString(JSONType, JSONDouble)
            oJSONObject2.AddDouble(JSONValue, m_paramY)

            aJSONArray.AddJSONObject(oJSONObject2)

            Return aJSONArray
        End Get
    End Property

    Public WriteOnly Property JSONParametersObject1() As IJSONObject Implements ISchematicJSONParameters.JSONParametersObject
        Set(ByVal value As IJSONObject)

            Dim oJSONObject As IJSONObject = value
            Dim paramX As Double
            Dim paramY As Double

            If (oJSONObject IsNot Nothing) Then            'decode input JSONparameters
                If (oJSONObject.TryGetValueAsDouble(JSONTranslationFactorX, paramX)) Then
                    m_paramX = paramX
                End If 'otherwise use current value

                If (oJSONObject.TryGetValueAsDouble(JSONTranslationFactorX, paramY)) Then
                    m_paramY = paramY
                End If 'otherwise use current value
            End If
        End Set
    End Property

#End Region

End Class

'End Namespace