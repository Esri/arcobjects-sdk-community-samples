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
Option Strict On

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
<System.Runtime.InteropServices.Guid(NodeReductionRule.GUID)> _
<System.Runtime.InteropServices.ProgId(NodeReductionRule.PROGID)> _
Public Class NodeReductionRule
    Implements ESRI.ArcGIS.Schematic.ISchematicRule
    Implements ESRI.ArcGIS.Schematic.ISchematicRuleDesign

    Public Const GUID As String = "5CA4A4C9-CBDF-4B4E-8932-53A962C92C22"
    Public Const PROGID As String = "CustomRulesVB.NodeReductionRule"

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
    Private m_reducedNodeClassName As String
    Private m_lengthAttributeName As String
    Private m_superspanLinkClassName As String
    Private m_linkAttributeName As String

    Private m_description As String = "Reduction Node Rule - Report cumulative value VBNet"
    Private m_keepVertices As Boolean = True
    Private m_linkAttribute As Boolean = False


#Region "NodeReductionRule Members"

    Public Sub New()
    End Sub

    Protected Overrides Sub Finalize()
        m_diagramClass = Nothing
        MyBase.Finalize()
    End Sub

    Public Property LinkAttribute() As Boolean
        Get
            Return m_linkAttribute
        End Get
        Set(ByVal value As Boolean)
            m_linkAttribute = value
        End Set
    End Property

    Public Property LinkAttributeName() As String
        Get
            Return m_linkAttributeName
        End Get
        Set(ByVal value As String)
            m_linkAttributeName = value
        End Set
    End Property


    Public Property KeepVertices() As Boolean
        Get
            Return m_keepVertices
        End Get
        Set(ByVal value As Boolean)
            m_keepVertices = value
        End Set
    End Property

    Public Property ReducedNodeClassName() As String
        Get
            Return m_reducedNodeClassName
        End Get
        Set(ByVal value As String)
            m_reducedNodeClassName = value
        End Set
    End Property

    Public Property LengthAttributeName() As String
        Get
            Return m_lengthAttributeName
        End Get
        Set(ByVal value As String)
            m_lengthAttributeName = value
        End Set
    End Property

    Public Property SuperpanLinkClassName() As String
        Get
            Return m_superspanLinkClassName
        End Get
        Set(ByVal value As String)
            m_superspanLinkClassName = value
        End Set
    End Property

#End Region

#Region "ISchematicRule Members"
    Public Sub Alter(ByVal schematicDiagramClass As ESRI.ArcGIS.Schematic.ISchematicDiagramClass, ByVal propertySet As ESRI.ArcGIS.esriSystem.IPropertySet) Implements ESRI.ArcGIS.Schematic.ISchematicRule.Alter
        m_diagramClass = schematicDiagramClass
        Try
            m_description = propertySet.GetProperty("DESCRIPTION").ToString()
        Catch
        End Try

        Try
            m_reducedNodeClassName = propertySet.GetProperty("REDUCEDNODECLASS").ToString()
        Catch
        End Try

        Try
            m_superspanLinkClassName = propertySet.GetProperty("SUPERSPANLINKCLASS").ToString()
        Catch
        End Try

        Try
            m_keepVertices = CBool(propertySet.GetProperty("KEEPVERTICES"))
        Catch
        End Try

        Try
            m_lengthAttributeName = propertySet.GetProperty("LENGTHATTRIBUTENAME").ToString()
        Catch
        End Try

        Try
            m_linkAttribute = CBool(propertySet.GetProperty("LINKATTRIBUTE"))
        Catch
        End Try

        Try
            m_linkAttributeName = propertySet.GetProperty("LINKATTRIBUTENAME").ToString()
        Catch
        End Try

    End Sub


    Public Sub Apply(ByVal inMemoryDiagram As ESRI.ArcGIS.Schematic.ISchematicInMemoryDiagram, Optional ByVal cancelTracker As ESRI.ArcGIS.esriSystem.ITrackCancel = Nothing) Implements ESRI.ArcGIS.Schematic.ISchematicRule.Apply
        Dim rulesHelper As ISchematicRulesHelper = New SchematicRulesHelper()
        Dim diagramClass As ISchematicDiagramClass = Nothing
        Dim elementClassReducedNode As ISchematicElementClass = Nothing
        Dim elementClassSuperspan As ISchematicElementClass = Nothing
        Dim elementClassContainer As ISchematicElementClassContainer
        Dim superspanLinkClass As ISchematicInMemoryFeatureClass
        Dim schematicDataset As ISchematicDataset
        Dim featureClassContainer As ISchematicInMemoryFeatureClassContainer
        Dim geoDataset As IGeoDataset
        Dim spatialRef As ISpatialReference = Nothing
        Dim colSchfeatureNode As New System.Collections.Generic.Dictionary(Of String, ISchematicInMemoryFeature)
        Dim kvp As KeyValuePair(Of String, ISchematicInMemoryFeature)
        Dim enumSchematicInMemoryFeature As IEnumSchematicInMemoryFeature = Nothing
        Dim msgProgressor As ESRI.ArcGIS.esriSystem.IProgressor = Nothing
        Dim stepProgressor As ESRI.ArcGIS.esriSystem.IStepProgressor
        Dim schFeatureToReduce As ISchematicInMemoryFeature

        If (m_reducedNodeClassName Is Nothing Or inMemoryDiagram Is Nothing) Then Return

        ' initialize the schematic rules helper
        rulesHelper.InitHelper(inMemoryDiagram)
        rulesHelper.KeepVertices = m_keepVertices

        'Get the feature classes processed by the rule
        Try
            diagramClass = inMemoryDiagram.SchematicDiagramClass
        Catch
        End Try
        If (diagramClass Is Nothing) Then Return

        Try
            schematicDataset = diagramClass.SchematicDataset
        Catch
            schematicDataset = Nothing
        End Try

        elementClassContainer = CType(schematicDataset, ISchematicElementClassContainer)
        If (elementClassContainer Is Nothing) Then Return

        elementClassReducedNode = elementClassContainer.GetSchematicElementClass(m_reducedNodeClassName)
        elementClassSuperspan = elementClassContainer.GetSchematicElementClass(m_superspanLinkClassName)
        If (elementClassSuperspan Is Nothing Or elementClassReducedNode Is Nothing) Then Return

        featureClassContainer = CType(inMemoryDiagram, ISchematicInMemoryFeatureClassContainer)
        If (featureClassContainer Is Nothing) Then Return

        superspanLinkClass = featureClassContainer.GetSchematicInMemoryFeatureClass(elementClassSuperspan)

        ' fetch the superspan spatial reference
        geoDataset = CType(superspanLinkClass, IGeoDataset)
        If (geoDataset IsNot Nothing) Then spatialRef = geoDataset.SpatialReference

        If (spatialRef Is Nothing) Then Return

        ' Retrieve the schematic in memory feature nodes to reduce 
        ' get all feature of parent node class
        enumSchematicInMemoryFeature = inMemoryDiagram.GetSchematicInMemoryFeaturesByClass(elementClassReducedNode)

        ' retain only the nodes of degree two
        RetainNodesDegreeTwo(enumSchematicInMemoryFeature, colSchfeatureNode, rulesHelper) ' there would be inserted a SQL query to also filter by attributes 

        If (cancelTracker IsNot Nothing) Then
            msgProgressor = cancelTracker.Progressor
            stepProgressor = CType(msgProgressor, ESRI.ArcGIS.esriSystem.IStepProgressor)
            If (stepProgressor IsNot Nothing) Then
                stepProgressor.MinRange = 0
                stepProgressor.MaxRange = colSchfeatureNode.Count
                stepProgressor.StepValue = 1
                stepProgressor.Position = 0
                stepProgressor.Message = m_description
                cancelTracker.Reset()
                cancelTracker.Progressor = msgProgressor
                stepProgressor.Show()
            End If
        End If


        For Each kvp In colSchfeatureNode
            If (cancelTracker IsNot Nothing) Then
                If (cancelTracker.Continue() = False) Then Exit For
            End If

            schFeatureToReduce = CType(colSchfeatureNode(kvp.Key), ISchematicInMemoryFeature)
            If (schFeatureToReduce IsNot Nothing) Then ReduceNode(rulesHelper, superspanLinkClass, spatialRef, schFeatureToReduce)
        Next

        colSchfeatureNode.Clear()
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
            Return "Node Reduction Rule VBNet"
        End Get
    End Property

    Public ReadOnly Property PropertySet() As ESRI.ArcGIS.esriSystem.IPropertySet Implements ESRI.ArcGIS.Schematic.ISchematicRule.PropertySet
        Get
            Dim propSet As esriSystem.IPropertySet = New esriSystem.PropertySet()

            propSet.SetProperty("DESCRIPTION", m_description)
            propSet.SetProperty("REDUCEDNODECLASS", m_reducedNodeClassName)
            propSet.SetProperty("SUPERSPANLINKCLASS", m_superspanLinkClassName)
            propSet.SetProperty("KEEPVERTICES", m_keepVertices)
            propSet.SetProperty("LENGTHATTRIBUTENAME", m_lengthAttributeName)
            propSet.SetProperty("LINKATTRIBUTENAME", m_linkAttributeName)
            propSet.SetProperty("LINKATTRIBUTE", m_linkAttribute)

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
            m_description = value.GetProperty("DESCRIPTION").ToString()
            m_reducedNodeClassName = value.GetProperty("REDUCEDNODECLASS").ToString()
            m_superspanLinkClassName = value.GetProperty("SUPERSPANLINKCLASS").ToString()
            m_keepVertices = CBool(value.GetProperty("KEEPVERTICES"))
            m_lengthAttributeName = value.GetProperty("LENGTHATTRIBUTENAME").ToString()
            m_linkAttribute = CBool(value.GetProperty("LINKATTRIBUTE"))
            m_linkAttributeName = value.GetProperty("LINKATTRIBUTENAME").ToString()
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

#Region "Node Reduction Rule private Members"

    Private Sub ReduceNode(ByVal rulesHelper As ISchematicRulesHelper, ByVal superspanLinkClass As ISchematicInMemoryFeatureClass, ByVal spatialRef As ISpatialReference, ByVal schFeatureToReduce As ISchematicInMemoryFeature)

        Dim enumLink As IEnumSchematicInMemoryFeature
        Dim schFeatureSuperspan As ISchematicInMemoryFeature = Nothing
        Dim schFeatureTmp As ISchematicInMemoryFeature = Nothing
        Dim schFeat1 As ISchematicInMemoryFeature = Nothing
        Dim schFeat2 As ISchematicInMemoryFeature = Nothing
        Dim schLink1 As ISchematicInMemoryFeatureLink = Nothing
        Dim schLink2 As ISchematicInMemoryFeatureLink = Nothing
        Dim schNodeToReduce As ISchematicInMemoryFeatureNode = Nothing
        Dim schFromNode As ISchematicInMemoryFeatureNode = Nothing
        Dim schToNode As ISchematicInMemoryFeatureNode = Nothing
        Dim superspanGeometry As IGeometry = Nothing

        Dim iFromPort As Integer
        Dim iToPort As Integer
        Dim lCount As Long = 1

        Dim strFromName As String
        Dim strtoName As String
        Dim strName As String

        If (schFeatureToReduce.Displayed = False Or rulesHelper Is Nothing Or spatialRef Is Nothing) Then Exit Sub

        ' get the two connected links 
        enumLink = rulesHelper.GetDisplayedIncidentLinks(CType(schFeatureToReduce, ISchematicInMemoryFeatureNode), esriSchematicEndPointType.esriSchematicOriginOrExtremityNode)
        If (enumLink Is Nothing Or enumLink.Count <> 2) Then Exit Sub

        enumLink.Reset()
        schFeat1 = enumLink.Next()
        schFeat2 = enumLink.Next()
        schLink1 = CType(schFeat1, ISchematicInMemoryFeatureLink)
        schLink2 = CType(schFeat2, ISchematicInMemoryFeatureLink)
        If (schLink1 Is Nothing Or schLink2 Is Nothing) Then Exit Sub

        schNodeToReduce = CType(schFeatureToReduce, ISchematicInMemoryFeatureNode)

        If (schLink2.FromNode Is schNodeToReduce) Then
            superspanGeometry = BuildLinkGeometry(schLink1, schNodeToReduce, schLink2, rulesHelper)

            If (schLink1.ToNode Is schNodeToReduce) Then
                schFromNode = schLink1.FromNode
                iFromPort = schLink1.FromPort
            Else
                schFromNode = schLink1.ToNode
                iFromPort = schLink1.ToPort
            End If

            schToNode = schLink2.ToNode
            iToPort = schLink2.ToPort
        Else
            superspanGeometry = BuildLinkGeometry(schLink2, schNodeToReduce, schLink1, rulesHelper)

            schFromNode = schLink2.FromNode
            iFromPort = schLink2.FromPort

            If (schLink1.FromNode Is schNodeToReduce) Then
                schToNode = schLink1.ToNode
                iToPort = schLink1.ToPort
            Else
                schToNode = schLink1.FromNode
                iToPort = schLink1.FromPort
            End If
        End If

        If (superspanGeometry IsNot Nothing) Then superspanGeometry.SpatialReference = spatialRef

        ' find a unique name for the superspan
        strFromName = schFromNode.Name
        strtoName = schToNode.Name
        lCount = 1
        While (schFeatureSuperspan Is Nothing)
            strName = strFromName + ";" + strtoName + ";" + lCount.ToString()
            If (strName.Length >= 128) Then Exit While ' too long a name

            Try
                schFeatureTmp = rulesHelper.AlterLink(superspanLinkClass, strName, Nothing, superspanGeometry, -2, -2,
                                                      strFromName, strtoName, esriFlowDirection.esriFDWithFlow, iFromPort, iToPort)
            Catch
                schFeatureTmp = Nothing
            End Try

            If (schFeatureTmp IsNot Nothing) Then
                ' valid new feature
                schFeatureSuperspan = schFeatureTmp
                Exit While
            End If
        End While

        ' last chance for a unique name
        lCount = 1
        While (schFeatureSuperspan Is Nothing)

            strName = schNodeToReduce.Name + ";" + lCount.ToString()
            If (strName.Length >= 128) Then Exit While ' too long a name
            Try
                schFeatureTmp = rulesHelper.AlterLink(superspanLinkClass, strName, Nothing, superspanGeometry, -2, -2,
                                                      strFromName, strtoName, esriFlowDirection.esriFDWithFlow, iFromPort, iToPort)
            Catch
                schFeatureTmp = Nothing
            End Try

            If (schFeatureTmp IsNot Nothing) Then
                ' valid new feature
                schFeatureSuperspan = schFeatureTmp
                Exit While
            End If
        End While

        If (schFeatureSuperspan Is Nothing) Then Exit Sub ' cannot find a unique name

        ' otherwise report the cumulated length of the reduced links to the superspan
        ReportCumulativeValues(schFeat1, schFeat2, schFeatureSuperspan)

        '  report the associations on the superspan link
        rulesHelper.ReportAssociations(schFeatureToReduce, schFeatureSuperspan)
        rulesHelper.ReportAssociations(schFeat1, schFeatureSuperspan)
        rulesHelper.ReportAssociations(schFeat2, schFeatureSuperspan)

        ' hide the reduced objects
        rulesHelper.HideFeature(schFeatureToReduce)
        rulesHelper.HideFeature(schFeat1)
        rulesHelper.HideFeature(schFeat2)
    End Sub

    Private Sub ReportCumulativeValues(ByVal schFeat1 As ISchematicInMemoryFeature, ByVal schFeat2 As ISchematicInMemoryFeature, ByVal schTargetFeat As ISchematicInMemoryFeature)

        Dim linkFields As IFields = Nothing
        Dim value1 As Object
        Dim value2 As Object
        Dim iIndex As Integer
        Dim dLength As Double
        Dim dValue1 As Double = 0
        Dim dValue2 As Double = 0

        If (schFeat1 Is Nothing Or schFeat2 Is Nothing Or schTargetFeat Is Nothing) Then Exit Sub

        ' assume the attribute field name is the same on every schematic feature link classes
        linkFields = schFeat1.Fields
        iIndex = linkFields.FindField(m_lengthAttributeName)
        If (iIndex < 0) Then Exit Sub ' attribute field does not exist
        value1 = schFeat1.Value(iIndex)

        linkFields = schFeat2.Fields
        iIndex = linkFields.FindField(m_lengthAttributeName)
        If (iIndex < 0) Then Exit Sub ' attribute field does not exist
        value2 = schFeat2.Value(iIndex)

        If (Not DBNull.Value.Equals(value1)) Then
            dValue1 = CDbl(value1)
        End If

        If (Not DBNull.Value.Equals(value2)) Then
            dValue2 = CDbl(value2)
        End If

        ' assume the values to be numeric
        dLength = dValue1 + dValue2

        linkFields = schTargetFeat.Fields
        iIndex = linkFields.FindField(m_lengthAttributeName)
        If (iIndex < 0) Then Exit Sub ' attribute field does not exist
        schTargetFeat.Value(iIndex) = dLength
    End Sub

    Private Function BuildLinkGeometry(ByVal schLink1 As ISchematicInMemoryFeatureLink, ByVal schNodeToReduce As ISchematicInMemoryFeatureNode, ByVal schLink2 As ISchematicInMemoryFeatureLink, ByVal rulesHelper As ISchematicRulesHelper) As IGeometry
        If (schLink1 Is Nothing Or schLink2 Is Nothing Or schNodeToReduce Is Nothing Or rulesHelper Is Nothing) Then Return Nothing
        If (m_keepVertices = False) Then Return Nothing 'no geometry

        Dim polyLink1 As IPolyline = Nothing
        Dim polyLink2 As IPolyline = Nothing
        Dim nodePt As IPoint = Nothing
        Dim Pt As IPoint = Nothing
        Dim newPts As IPointCollection = Nothing
        Dim link1Pts As IPointCollection = Nothing
        Dim link2Pts As IPointCollection = Nothing
        Dim buildGeometry As IGeometry = New Polyline()
        Dim iCount As Integer
        Dim i As Integer

        polyLink1 = rulesHelper.GetLinkPoints(schLink1, (schLink1.FromNode Is schNodeToReduce))
        polyLink2 = rulesHelper.GetLinkPoints(schLink2, (schLink2.ToNode Is schNodeToReduce))
        nodePt = rulesHelper.GetNodePoint(schNodeToReduce)

        newPts = CType(buildGeometry, IPointCollection)
        link1Pts = CType(polyLink1, IPointCollection)
        link2Pts = CType(polyLink2, IPointCollection)

        iCount = link1Pts.PointCount
        For i = 0 To iCount - 2 Step 1
            Pt = link1Pts.Point(i)
            newPts.AddPoint(Pt)
        Next

        newPts.AddPoint(nodePt)
        iCount = link2Pts.PointCount
        For i = 1 To iCount - 1 Step 1
            Pt = link2Pts.Point(i)
            newPts.AddPoint(Pt)
        Next
        Return buildGeometry
    End Function

    Private Sub RetainNodesDegreeTwo(ByRef enumInMemoryFeature As IEnumSchematicInMemoryFeature, ByRef colSchfeatureNode As System.Collections.Generic.Dictionary(Of String, ISchematicInMemoryFeature), ByVal ruleHelper As ISchematicRulesHelper)

        Dim schInMemoryfeature As ISchematicInMemoryFeature = Nothing

        enumInMemoryFeature.Reset()
        schInMemoryfeature = enumInMemoryFeature.Next()
        While (schInMemoryfeature IsNot Nothing)
            If (schInMemoryfeature.Displayed) Then
                Dim schInMemoryNode As ISchematicInMemoryFeatureNode = Nothing
                schInMemoryNode = CType(schInMemoryfeature, ISchematicInMemoryFeatureNode)
                Dim enumLinks As IEnumSchematicInMemoryFeature = Nothing
                enumLinks = ruleHelper.GetDisplayedIncidentLinks(schInMemoryNode, esriSchematicEndPointType.esriSchematicOriginOrExtremityNode)
                If (enumLinks IsNot Nothing And enumLinks.Count = 2) Then
                    ' Valid degree two node
                    If (Not colSchfeatureNode.ContainsKey(schInMemoryfeature.Name)) Then
                        If (Not LinkAttribute) Then
                            colSchfeatureNode.Add(schInMemoryfeature.Name, schInMemoryfeature)
                        ElseIf (SameIncidentLinkAttributeValue(enumLinks, LinkAttributeName, ruleHelper)) Then
                            colSchfeatureNode.Add(schInMemoryfeature.Name, schInMemoryfeature)
                        End If
                    End If
                End If
            End If

            schInMemoryfeature = enumInMemoryFeature.Next()

        End While
    End Sub

    Private Function SameIncidentLinkAttributeValue(ByVal enumInMemoryLinks As IEnumSchematicInMemoryFeature, ByVal attributeName As String, ByVal ruleHelper As ISchematicRulesHelper) As Boolean

        Dim inMemoryFeature As ISchematicInMemoryFeature = Nothing
        Dim bFirstVariant As Boolean = True
        Dim vPreviousValue As Object = Nothing
        Dim vCurrentValue As Object = Nothing

        enumInMemoryLinks.Reset()
        inMemoryFeature = enumInMemoryLinks.Next()

        While (inMemoryFeature IsNot Nothing)
            ' Do not take account the link if the link is not displayed
            ' Search for an attribute with the given name
            Dim schematicElementClass As ISchematicElementClass
            schematicElementClass = inMemoryFeature.SchematicElementClass
            Dim attributeContainer As ISchematicAttributeContainer = CType(schematicElementClass, ISchematicAttributeContainer)
            Dim schematicAttribute As ISchematicAttribute = Nothing

            If (attributeContainer IsNot Nothing) Then
                schematicAttribute = attributeContainer.GetSchematicAttribute(attributeName, True)
            End If

            If (schematicAttribute IsNot Nothing) Then
                Dim schematicObject As ISchematicObject = CType(inMemoryFeature, ISchematicObject)
                vCurrentValue = schematicAttribute.GetValue(schematicObject)
            Else
                'If schematic attribute not existing ==> find a field in the associated feature
                Dim iObject As IObject = Nothing
                Dim primaryAssociation As ISchematicInMemoryFeaturePrimaryAssociation = CType(inMemoryFeature, ISchematicInMemoryFeaturePrimaryAssociation)
                If (primaryAssociation IsNot Nothing) Then
                    iObject = primaryAssociation.AssociatedObject
                End If

                Dim row As IRow = CType(iObject, IRow)
                Dim fieldIndex As Integer = 0

                If (row IsNot Nothing) Then
                    Dim fields As IFields = row.Fields
                    If (fields IsNot Nothing) Then fieldIndex = fields.FindField(attributeName)
                End If

                If (fieldIndex > 0) Then
                    vCurrentValue = row.Value(fieldIndex)
                    If (DBNull.Value.Equals(vCurrentValue)) Then Return False
                Else
                    Return False
                End If

            End If

            If (bFirstVariant) Then
                vPreviousValue = vCurrentValue
                bFirstVariant = False
            Else
                ' Compare PreviousValue and CurrentValue
                If (vPreviousValue.GetType() IsNot vCurrentValue.GetType()) Then Return False

                If (DBNull.Value.Equals(vPreviousValue) Or DBNull.Value.Equals(vCurrentValue)) Then Return False

                If (TypeOf (vPreviousValue.GetType().FullName) Is System.String) Then 'Speciale Case for string.
                    Dim str1 As String = vPreviousValue.ToString()
                    Dim str2 As String = vCurrentValue.ToString()

                    If (String.Compare(str1, str2, True) <> 0) Then Return False

                ElseIf (Not vPreviousValue.Equals(vCurrentValue)) Then
                    Return False ' == or != operator compare for Variant match the right type.
                End If
            End If

            inMemoryFeature = enumInMemoryLinks.Next()

        End While

        Return True
    End Function
#End Region
End Class
