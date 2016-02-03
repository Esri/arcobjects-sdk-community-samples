Option Strict Off

Imports System
Imports System.Collections.Generic
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Schematic
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.Geodatabase
Imports esriSystem = ESRI.ArcGIS.esriSystem



<System.Runtime.InteropServices.ClassInterface(System.Runtime.InteropServices.ClassInterfaceType.None)> _
<System.Runtime.InteropServices.Guid(BisectorRule.GUID)> _
<System.Runtime.InteropServices.ProgId(BisectorRule.PROGID)> _
Public Class BisectorRule
    Implements ESRI.ArcGIS.Schematic.ISchematicRule
    Implements ESRI.ArcGIS.Schematic.ISchematicRuleDesign
    Public Const GUID As String = "36D59619-86EB-4244-A521-CEF2187EABCC"
    Public Const PROGID As String = "CustomRulesVB.BisectorRule"

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
    Private m_distance As Double = 0.5
    Private m_parentNodeClassName As String
    Private m_targetNodeClassName As String
    Private m_targetLinkClassName As String
    Private m_description As String = "Bisector Rule VBNet"
    Private m_parentNodeClass As ISchematicInMemoryFeatureClass
    Private m_targetNodeClass As ISchematicInMemoryFeatureClass
    Private m_targetLinkClass As ISchematicInMemoryFeatureClass
    Private Const Separator As String = "_"
    Private Const extensionName As String = "BisectorRuleVB"


    Public Sub New()
    End Sub

    Protected Overrides Sub Finalize()
        m_diagramClass = Nothing
        m_parentNodeClass = Nothing
        m_targetNodeClass = Nothing
        m_targetLinkClass = Nothing
        MyBase.Finalize()
    End Sub

    Public Property Distance() As Double
        Get
            Return m_distance
        End Get
        Set(ByVal value As Double)
            m_distance = value
        End Set
    End Property

    Public Property parentNodeClassName() As String
        Get
            Return m_parentNodeClassName
        End Get
        Set(ByVal value As String)
            m_parentNodeClassName = value
        End Set
    End Property

    Public Property targetNodeClassName() As String
        Get
            Return m_targetNodeClassName
        End Get
        Set(ByVal value As String)
            m_targetNodeClassName = value
        End Set
    End Property

    Public Property targetLinkClassName() As String
        Get
            Return m_targetLinkClassName
        End Get
        Set(ByVal value As String)
            m_targetLinkClassName = value
        End Set
    End Property

#Region "ISchematicRule Members"
    Public Sub Alter(ByVal schematicDiagramClass As ESRI.ArcGIS.Schematic.ISchematicDiagramClass, ByVal propertySet As ESRI.ArcGIS.esriSystem.IPropertySet) Implements ESRI.ArcGIS.Schematic.ISchematicRule.Alter
        m_diagramClass = schematicDiagramClass

        Try
            m_description = propertySet.GetProperty("DESCRIPTION").ToString()
        Catch ex As System.Exception
            System.Diagnostics.Trace.WriteLine(ex.Message, "property DESCRIPTION")
        End Try

        Try
            m_parentNodeClassName = propertySet.GetProperty("PARENTNODECLASS").ToString()
        Catch ex As System.Exception
            System.Diagnostics.Trace.WriteLine(ex.Message, "property PARENTNODECLASS")
        End Try

        Try
            m_targetNodeClassName = propertySet.GetProperty("TARGETNODECLASS").ToString()
        Catch ex As System.Exception
            System.Diagnostics.Trace.WriteLine(ex.Message, "property TARGETNODECLASS")
        End Try

        Try
            m_targetLinkClassName = propertySet.GetProperty("TARGETLINKCLASS").ToString()
        Catch ex As System.Exception
            System.Diagnostics.Trace.WriteLine(ex.Message, "property TARGETLINKCLASS")
        End Try

        Try
            m_distance = CType(propertySet.GetProperty("DISTANCE"), Double)
        Catch ex As System.Exception
            System.Diagnostics.Trace.WriteLine(ex.Message, "property DISTANCE")
        End Try

    End Sub


    Public Sub Apply(ByVal inMemoryDiagram As ESRI.ArcGIS.Schematic.ISchematicInMemoryDiagram, Optional ByVal cancelTracker As ESRI.ArcGIS.esriSystem.ITrackCancel = Nothing) Implements ESRI.ArcGIS.Schematic.ISchematicRule.Apply
        Dim rulesHelper As ISchematicRulesHelper = New SchematicRulesHelper()
        Dim diagramClass As ISchematicDiagramClass = Nothing
        Dim elementClass As ISchematicElementClass
        Dim enumElementClass As IEnumSchematicElementClass
        Dim elementClassParentNode As ISchematicElementClass = Nothing

        rulesHelper.InitHelper(inMemoryDiagram)
        rulesHelper.KeepVertices = True
        Dim colSchfeatureNode As New System.Collections.Generic.Dictionary(Of String, ISchematicInMemoryFeature)
        Try
            diagramClass = inMemoryDiagram.SchematicDiagramClass
        Catch
        End Try

        If (diagramClass Is Nothing) Then Exit Sub
        enumElementClass = diagramClass.AssociatedSchematicElementClasses

        If (enumElementClass Is Nothing) Then Exit Sub

        enumElementClass.Reset()
        elementClass = enumElementClass.Next()
        While (elementClass IsNot Nothing)
            If (elementClass.Name = m_parentNodeClassName) Then
                elementClassParentNode = elementClass
                m_parentNodeClass = GetSchematicInMemoryFeatureClass(inMemoryDiagram, elementClass)
            End If

            If (elementClass.Name = m_targetNodeClassName) Then
                m_targetNodeClass = GetSchematicInMemoryFeatureClass(inMemoryDiagram, elementClass)
            End If

            If (elementClass.Name = m_targetLinkClassName) Then
                m_targetLinkClass = GetSchematicInMemoryFeatureClass(inMemoryDiagram, elementClass)
            End If

            elementClass = enumElementClass.Next()
        End While

        If (m_parentNodeClass Is Nothing Or m_targetNodeClass Is Nothing Or m_targetLinkClass Is Nothing) Then
            Exit Sub
        End If

        Dim enumSchematicInMemoryFeature As IEnumSchematicInMemoryFeature = Nothing

        ' list nodes degree two
        ' get all feature of parent node class


        enumSchematicInMemoryFeature = inMemoryDiagram.GetSchematicInMemoryFeaturesByClass(elementClassParentNode)
        enumSchematicInMemoryFeature.Reset()

        'add the node into collection if it contains only 2 links displayed.
        AddNodesDegreeTwo(enumSchematicInMemoryFeature, colSchfeatureNode, rulesHelper)

        Dim schFeatureParent As ISchematicInMemoryFeature
        Dim kvp As KeyValuePair(Of String, ISchematicInMemoryFeature)
        For Each kvp In colSchfeatureNode
            schFeatureParent = CType(colSchfeatureNode(kvp.Key), ISchematicInMemoryFeature)
            If (schFeatureParent Is Nothing) Then Continue For
            'get 2 links connected of eache feature node
            Dim enumLinks As IEnumSchematicInMemoryFeature

            enumLinks = rulesHelper.GetDisplayedIncidentLinks(CType(schFeatureParent, ISchematicInMemoryFeatureNode), esriSchematicEndPointType.esriSchematicOriginOrExtremityNode)
            'enumLinks surely not null and it contain 2 links displayed

            Dim angle1, angle2, angleBisector As Double
            angle1 = 0
            angle2 = 0
            angleBisector = 0
            Dim first As Boolean = True
            Dim pointParent As IPoint
            Dim geoParent As ISchematicInMemoryFeatureNodeGeometry
            geoParent = CType(schFeatureParent, ISchematicInMemoryFeatureNodeGeometry)

            pointParent = geoParent.InitialPosition
            Dim pointSon As IPoint = Nothing
            Dim schInMemoryFeature As ISchematicInMemoryFeature = enumLinks.Next()

            Dim schInMemoryLink As ISchematicInMemoryFeatureLink = CType(schInMemoryFeature, ISchematicInMemoryFeatureLink)
            Dim enableCalculate As Boolean = True
            While (schInMemoryLink IsNot Nothing)

                Dim nodeGeo As ISchematicInMemoryFeatureNodeGeometry
                'get angle of 2 links connected
                If (schInMemoryLink.FromNode Is schFeatureParent) Then
                    nodeGeo = CType(schInMemoryLink.ToNode, ISchematicInMemoryFeatureNodeGeometry)
                Else
                    nodeGeo = CType(schInMemoryLink.FromNode, ISchematicInMemoryFeatureNodeGeometry)
                End If

                If (nodeGeo Is Nothing) Then
                    enableCalculate = False
                    Exit While
                End If

                pointSon = nodeGeo.InitialPosition
                If (first) Then
                    angle1 = CalculateAngle(pointParent, pointSon)
                    first = False
                Else
                    angle2 = CalculateAngle(pointParent, pointSon)
                End If

                schInMemoryFeature = enumLinks.Next()
                schInMemoryLink = CType(schInMemoryFeature, ISchematicInMemoryFeatureLink)

            End While
            'caculate angle bisector
            If (enableCalculate) Then
                angleBisector = CalculateAngleBisector(angle1, angle2)
            Else
                Continue For
            End If

            'construct a geometry for the new node node
            'now call alterNode to create a new schematic feature
            'construct a correct name
            Dim uniqueNodeName As String
            Dim featureCreateName As String
            featureCreateName = schFeatureParent.Name & Separator & extensionName
            Dim elementType As esriSchematicElementType = esriSchematicElementType.esriSchematicNodeType
            uniqueNodeName = GetUniqueName(inMemoryDiagram, esriSchematicElementType.esriSchematicNodeType, featureCreateName)

            Dim workspace As IWorkspace = Nothing
            Try
                workspace = inMemoryDiagram.SchematicDiagramClass.SchematicDataset.SchematicWorkspace.Workspace
            Catch
            End Try

            Dim datasourceID As Integer = -1

            If (workspace IsNot Nothing) Then
                datasourceID = rulesHelper.FindDataSourceID(workspace, False)
            End If

            If (datasourceID <> -1) Then
                datasourceID = m_diagramClass.SchematicDataset.DefaultSchematicDataSource.ID
            End If

            Dim schFeatureNodeCreate As ISchematicInMemoryFeature = Nothing
            Dim pointBisector As IPoint = Nothing
            pointBisector = GetCoordPointBisector(pointParent, angleBisector, m_distance)

            Try
                schFeatureNodeCreate = rulesHelper.AlterNode(m_targetNodeClass, uniqueNodeName, Nothing, CType(pointBisector, IGeometry), datasourceID, 0)
            Catch ex As System.Exception
                System.Diagnostics.Trace.WriteLine(ex.Message, "Impossible to create a feature Node")
            End Try

            'now construct a unique link name
            Dim linkName As String = schFeatureParent.Name & Separator & uniqueNodeName
            Dim uniqueLinkName As String

            elementType = esriSchematicElementType.esriSchematicLinkType
            uniqueLinkName = GetUniqueName(inMemoryDiagram, elementType, linkName)
            'construct a link

            Dim schFeatureLinkCreate As ISchematicInMemoryFeature = Nothing
            Try
                schFeatureLinkCreate = rulesHelper.AlterLink(m_targetLinkClass, uniqueLinkName, Nothing, Nothing, datasourceID, 0, schFeatureParent.Name, uniqueNodeName, esriFlowDirection.esriFDWithFlow, 0, 0)
            Catch ex As System.Exception
                System.Diagnostics.Trace.WriteLine(ex.Message, "Impossible to create a feature link")
            End Try
        Next

        If colSchfeatureNode.Count > 0 Then
            colSchfeatureNode.Clear()
        End If

        colSchfeatureNode = Nothing
        rulesHelper = Nothing

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
            Return "Bisector Rule VBNet"
        End Get
    End Property

    Public ReadOnly Property PropertySet() As ESRI.ArcGIS.esriSystem.IPropertySet Implements ESRI.ArcGIS.Schematic.ISchematicRule.PropertySet
        Get
            Dim propSet As esriSystem.IPropertySet = New esriSystem.PropertySet()
            propSet.SetProperty("DESCRIPTION", m_description)
            propSet.SetProperty("PARENTNODECLASS", m_parentNodeClassName)
            propSet.SetProperty("TARGETNODECLASS", m_targetNodeClassName)
            propSet.SetProperty("TARGETLINKCLASS", m_targetLinkClassName)
            propSet.SetProperty("DISTANCE", m_distance)

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
        m_parentNodeClass = Nothing
        m_targetNodeClass = Nothing
        m_targetLinkClass = Nothing
    End Sub

    Public WriteOnly Property PropertySet1() As ESRI.ArcGIS.esriSystem.IPropertySet Implements ESRI.ArcGIS.Schematic.ISchematicRuleDesign.PropertySet
        Set(ByVal value As ESRI.ArcGIS.esriSystem.IPropertySet)
            m_description = value.GetProperty("DESCRIPTION").ToString()
            m_parentNodeClassName = value.GetProperty("PARENTNODECLASS").ToString()
            m_targetNodeClassName = value.GetProperty("TARGETNODECLASS").ToString()
            m_targetLinkClassName = value.GetProperty("TARGETLINKCLASS").ToString()
            m_distance = CDbl(value.GetProperty("DISTANCE"))
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

#Region "Bisector Members"
    Private Function GetSchematicInMemoryFeatureClass(ByVal inMemoryDiagram As ESRI.ArcGIS.Schematic.ISchematicInMemoryDiagram, ByVal eltClass As ISchematicElementClass) As ISchematicInMemoryFeatureClass
        Dim SchInMemoryFeatureClassContainer As ISchematicInMemoryFeatureClassContainer
        SchInMemoryFeatureClassContainer = CType(inMemoryDiagram, ISchematicInMemoryFeatureClassContainer)
        GetSchematicInMemoryFeatureClass = SchInMemoryFeatureClassContainer.GetSchematicInMemoryFeatureClass(eltClass)
    End Function

    Private Sub AddNodesDegreeTwo(ByVal enumInMemoryFeature As IEnumSchematicInMemoryFeature, ByVal colSchfeatureNode As System.Collections.Generic.Dictionary(Of String, ISchematicInMemoryFeature), ByVal ruleHelper As ISchematicRulesHelper)
        Dim schInMemoryfeature As ISchematicInMemoryFeature

        If (enumInMemoryFeature Is Nothing Or colSchfeatureNode Is Nothing Or ruleHelper Is Nothing) Then Return

        enumInMemoryFeature.Reset()
        schInMemoryfeature = enumInMemoryFeature.Next()
        While (schInMemoryfeature IsNot Nothing)
            If (schInMemoryfeature.Displayed) Then
                Dim enumLinks As IEnumSchematicInMemoryFeature = Nothing
                enumLinks = ruleHelper.GetDisplayedIncidentLinks(CType(schInMemoryfeature, ISchematicInMemoryFeatureNode), esriSchematicEndPointType.esriSchematicOriginOrExtremityNode)

                If (enumLinks IsNot Nothing And enumLinks.Count = 2) Then
                    If (Not colSchfeatureNode.ContainsKey(schInMemoryfeature.Name)) Then colSchfeatureNode.Add(schInMemoryfeature.Name, schInMemoryfeature)
                End If
            End If

            schInMemoryfeature = enumInMemoryFeature.Next()
        End While
    End Sub

    Private Function GetUniqueName(ByVal inMemoryDiagram As ESRI.ArcGIS.Schematic.ISchematicInMemoryDiagram, ByVal elementType As esriSchematicElementType, ByVal featureName As String) As String
        Dim nameUnique As String = featureName
        Dim schInMemoryfeature As ISchematicInMemoryFeature
        Dim index As Integer = 1
        Dim endWhile As Boolean = False
        Do While Not endWhile
            schInMemoryfeature = inMemoryDiagram.GetSchematicInMemoryFeatureByType(elementType, nameUnique)
            If schInMemoryfeature Is Nothing Then
                endWhile = True
            ElseIf (schInMemoryfeature.Displayed) Then
                nameUnique = nameUnique & index.ToString()
                index = index + 1
            Else
                endWhile = True
            End If
        Loop

        GetUniqueName = nameUnique
    End Function

    Private Function CalculateAngle(ByVal pointFrom As IPoint, ByVal pointTo As IPoint) As Double
        Const radToDegre As Double = 180 / Math.PI
        Dim dX, dY As Double
        CalculateAngle = 0
        dX = pointTo.X - pointFrom.X
        dY = pointTo.Y - pointFrom.Y
        If (dX <> 0) Then
            CalculateAngle = Math.Atan(dY / dX) * radToDegre
        Else ' case 2 points are same abcisse
            If (dY < 0) Then
                CalculateAngle = 270
            Else
                CalculateAngle = 90
            End If
        End If

        If (dX < 0) Then
            CalculateAngle = CalculateAngle + 180
        End If

        If (CalculateAngle < 0) Then
            CalculateAngle = CalculateAngle + 360
        End If

    End Function

    Private Function CalculateAngleBisector(ByVal angle1 As Double, ByVal angle2 As Double) As Double
        Dim angleFinal As Double
        If (Math.Abs(angle1 - angle2) > 180) Then
            angleFinal = (angle1 + angle2) / 2 + 180
        Else
            angleFinal = (angle1 + angle2) / 2
        End If
        CalculateAngleBisector = angleFinal
    End Function

    Private Function GetCoordPointBisector(ByVal pointOrigine As IPoint, ByVal degreeBiSectore As Double, ByVal distance As Double) As IPoint
        Dim pointBisector As IPoint = New Point()
        Dim angleBisector, degreeFinal, dX, dY As Double
        Dim casAngle As Integer = 0
        If (degreeBiSectore <= 90) Then
            degreeFinal = degreeBiSectore
            casAngle = 1
        ElseIf (degreeBiSectore <= 180) Then
            'degreeFinal = degreeBiSectore - 90
            degreeFinal = 180 - degreeBiSectore
            casAngle = 2
        ElseIf (degreeBiSectore <= 270) Then
            degreeFinal = degreeBiSectore - 180
            casAngle = 3
        Else
            degreeFinal = 360 - degreeBiSectore
            casAngle = 4
        End If

        angleBisector = Math.PI * degreeFinal / 180.0

        dY = distance * Math.Sin(angleBisector)
        dX = distance * Math.Cos(angleBisector)

        Dim testx As Double = 0
        Dim testy As Double = 0

        Select Case casAngle
            Case 1 ' case which the vector is on the first of the circle
                pointBisector.X = pointOrigine.X + dX
                pointBisector.Y = pointOrigine.Y + dY
            Case 2 ' case which the vector is on the second of the circle
                pointBisector.X = pointOrigine.X - dX
                pointBisector.Y = pointOrigine.Y + dY
            Case 3 ' case which the vector is on the thirst of the circle
                pointBisector.X = pointOrigine.X - dX
                pointBisector.Y = pointOrigine.Y - dY
            Case Else ' case which the vector is on the fourth of the circle
                pointBisector.X = pointOrigine.X + dX
                pointBisector.Y = pointOrigine.Y - dY
        End Select

        GetCoordPointBisector = pointBisector


    End Function

#End Region
End Class

