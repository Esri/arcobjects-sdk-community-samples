Imports ESRI.ArcGIS.Schematic
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Geometry

Imports ContainerManagerVB.My


Public Class StationsInContainers
	Inherits ESRI.ArcGIS.Desktop.AddIns.Extension

	' The OnAfterLoadDiagram is used to define the relations between elements
	' contained in a diagram. It is called by the AfterLoadDiagram 
	' event defined in the schematic project. The diagram contains Stations and 
	' Containers elements. For the Station element type, a particular attribute 
	' named RelatedFeeder has been created. This attribute is used to identify 
	' the container the station is related to. Near the top of the procedure, 
	' a new schematic relation is created (SchematicContainerManager). The 
	' ISchematicRelationControllerEdit CreateRelation method is used to specify 
	' that the station is related to its container. The value set for the 
	' RelatedFeeder attribute specifies whether or not the station is related 
	' to a container.

    Private m_DatasetMgr As SchematicDatasetManager
    Private s_extension As StationsInContainers


    Protected Overrides Sub OnStartup()

        s_extension = Me
        m_DatasetMgr = New SchematicDatasetManager
    End Sub

    Protected Overrides Sub OnShutdown()

        ResetEvents(False)

        s_extension = Nothing
        m_DatasetMgr = Nothing

    End Sub

    Protected Overrides Function OnSetState(ByVal state As ESRI.ArcGIS.Desktop.AddIns.ExtensionState) As Boolean
        ' Optionally check for a license here
        Me.State = state

        If state = ESRI.ArcGIS.Desktop.AddIns.ExtensionState.Enabled Then
            ResetEvents(True)
        Else
            ResetEvents(False)
        End If

        Return MyBase.OnSetState(state)
    End Function


    Protected Overrides Function OnGetState() As ESRI.ArcGIS.Desktop.AddIns.ExtensionState
        Return Me.State
    End Function

    Public Sub OnStartEditLayer(ByVal schLayer As ESRI.ArcGIS.Schematic.ISchematicLayer)
        If schLayer IsNot Nothing And Me.State = ESRI.ArcGIS.Desktop.AddIns.ExtensionState.Enabled Then
            Updatecontainers(schLayer)
        End If
    End Sub


    Public Sub OnStopEditLayer(ByVal schLayer As ESRI.ArcGIS.Schematic.ISchematicLayer)
        If schLayer IsNot Nothing And Me.State = ESRI.ArcGIS.Desktop.AddIns.ExtensionState.Enabled Then
            Updatecontainers(schLayer)
        End If
    End Sub


    ' Private methods
    Private Sub ResetEvents(ByVal bAdd As Boolean)

        ' make sure the extension is turned on
        If (s_extension Is Nothing Or m_DatasetMgr Is Nothing Or Me.State = ESRI.ArcGIS.Desktop.AddIns.ExtensionState.Unavailable) Then Return

        ' Reset event handlers
        If (bAdd = True And Me.State = ESRI.ArcGIS.Desktop.AddIns.ExtensionState.Enabled) Then
            AddHandler m_DatasetMgr.OnStartEditLayer, AddressOf OnStartEditLayer
            AddHandler m_DatasetMgr.OnStopEditLayer, AddressOf OnStopEditLayer
        Else
            RemoveHandler m_DatasetMgr.OnStartEditLayer, AddressOf OnStartEditLayer
            RemoveHandler m_DatasetMgr.OnStopEditLayer, AddressOf OnStopEditLayer
        End If

        ' Process the opened schematic layers in the maps
        ProcessMaps()
    End Sub




    Private Sub ProcessMaps()

        Dim Maps As IMaps

        Maps = ContainerManagerVB.My.ArcMap.Document.Maps

        ' get the Maps
        Dim lNbMaps As Long = Maps.Count
        Dim i As Integer

        For i = 0 To lNbMaps - 1
            Dim Map As IMap

            Map = Maps.Item(i)
            If Map Is Nothing Then Continue For
            ' browse its layers for a schematic layer
            Dim Layers As IEnumLayer
            Layers = Map.Layers(Nothing, False)
            Dim Layer As ILayer

            Layers.Reset()
            Layer = Layers.Next()
            While (Layer IsNot Nothing)
                Dim schLayer As ISchematicLayer
                schLayer = TryCast(Layer, ISchematicLayer)
                Updatecontainers(schLayer)
                Layer = Layers.Next()
            End While
        Next
    End Sub

    Private Sub Updatecontainers(ByVal schLayer As ESRI.ArcGIS.Schematic.ISchematicLayer)

        If (schLayer Is Nothing) Then Return

        ' get the inMemorydiagram if any
        Dim inMemoryDiagram As ISchematicInMemoryDiagram
        inMemoryDiagram = schLayer.SchematicInMemoryDiagram
        If (inMemoryDiagram Is Nothing) Then Return

        Dim bCreate As Boolean = False

        ' create or remove relations between containers and their contents
        If (Me.State = ESRI.ArcGIS.Desktop.AddIns.ExtensionState.Enabled) Then bCreate = True

        Dim schemContainerClass As ISchematicElementClass = Nothing
        Dim schemElementClass As ISchematicElementClass
        Dim schemStationClass As ISchematicElementClass = Nothing
        Dim enumElementsInContainer As IEnumSchematicInMemoryFeature
        Dim enumContainerElements As IEnumSchematicInMemoryFeature
        Dim schemFeature As ISchematicInMemoryFeature = Nothing
        Dim schemContainerFeature As ISchematicInMemoryFeature = Nothing
        Dim feederOID As Object
        Dim containerNameID As String
        Dim enumElementClass As IEnumSchematicElementClass
        Dim schemAttributeContainer As ISchematicAttributeContainer
        Dim schemAttributeRelatedFeeder As ISchematicAttribute
        Dim schemElement As ISchematicElement = Nothing
        Dim schemRelationController As ISchematicRelationController
        Dim schemRelationControllerEdit As ISchematicRelationControllerEdit
        Dim colContElem As New Collection

        ' Getting SchematicFeature Class Stations and Containers
        enumElementClass = inMemoryDiagram.SchematicDiagramClass.AssociatedSchematicElementClasses
        enumElementClass.Reset()
        schemElementClass = enumElementClass.Next
        While (schemElementClass IsNot Nothing)
            If schemElementClass.Name = "Stations" Then schemStationClass = schemElementClass
            If schemElementClass.Name = "Containers" Then schemContainerClass = schemElementClass
            If (schemStationClass IsNot Nothing AndAlso schemContainerClass IsNot Nothing) Then Exit While

            schemElementClass = enumElementClass.Next
        End While
        ' go out if schemStationClass or schemContainerClass are null
        If (schemStationClass Is Nothing OrElse schemContainerClass Is Nothing) Then Return

        ' Getting the Stations elements that will be displayed in the containers
        enumElementsInContainer = inMemoryDiagram.GetSchematicInMemoryFeaturesByClass(schemStationClass)
        If (enumElementsInContainer Is Nothing) Then Return

        ' Creating the Schematic Container Manager
        schemRelationController = New SchematicRelationController

        ' Creating the Schematic Container Editor that will be used to define the relation between the stations and their container
        schemRelationControllerEdit = schemRelationController

        ' Defining each Container element as a schematic container
        enumContainerElements = inMemoryDiagram.GetSchematicInMemoryFeaturesByClass(schemContainerClass)

        ' Add Container Element to a collection
        enumContainerElements.Reset()
        schemContainerFeature = enumContainerElements.Next
        While (schemContainerFeature IsNot Nothing)
            colContElem.Add(schemContainerFeature, schemContainerFeature.Name)
            schemContainerFeature = enumContainerElements.Next
        End While

        ' Setting the relation between each station and its related container
        enumElementsInContainer.Reset()
        schemFeature = enumElementsInContainer.Next

        While (schemFeature IsNot Nothing)
            ' The relation is specified by the RelatedFeeder attribute value defined for each station
            schemAttributeContainer = CType(schemFeature.SchematicElementClass, ISchematicAttributeContainer)
            schemAttributeRelatedFeeder = schemAttributeContainer.GetSchematicAttribute("RelatedFeeder")

            If schemAttributeRelatedFeeder IsNot Nothing Then
                feederOID = schemAttributeRelatedFeeder.GetValue(CType(schemFeature, ISchematicObject))
                If feederOID IsNot Nothing Then
                    containerNameID = "Container-" & feederOID.ToString()

                    Try
                        ' Retrieve Container Element in the collection
                        schemContainerFeature = CType(colContElem(containerNameID), ISchematicInMemoryFeature)

                        If (bCreate) Then
                            ' Create relation
                            schemRelationControllerEdit.CreateRelation(schemFeature, schemContainerFeature)
                        Else
                            ' delete child relation
                            schemRelationControllerEdit.DeleteRelation(schemFeature)
                        End If
                    Catch
                    End Try
                End If
            End If


            schemContainerFeature = Nothing
            schemFeature = enumElementsInContainer.Next
        End While

        If (bCreate = False) Then

            ' Force container geometry
            enumContainerElements.Reset()
            schemContainerFeature = enumContainerElements.Next()
            While (schemContainerFeature IsNot Nothing)
                Try
                    'set an empty geometry 
                    ' container does not have content at this stage
                    Dim emptyRectangle As New Polygon()
                    Dim ContainerGeometry As IGeometry = CType(emptyRectangle, IGeometry)
                    schemContainerFeature.Shape = ContainerGeometry
                Catch
                End Try

                schemContainerFeature = enumContainerElements.Next()
            End While
        End If

        Dim activeView As IActiveView
        activeView = CType(ContainerManagerVB.My.ArcMap.Document.FocusMap, IActiveView)
        If activeView IsNot Nothing Then
            activeView.ContentsChanged()
            activeView.Refresh()
        End If

    End Sub

End Class
