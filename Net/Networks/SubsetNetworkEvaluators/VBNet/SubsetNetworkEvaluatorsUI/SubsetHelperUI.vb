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
Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections.Generic
Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.NetworkAnalyst
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.NetworkAnalystUI
Imports SubsetNetworkEvaluators

Namespace SubsetNetworkEvaluatorsUI
	''' <summary>
	''' The SubsetHelperUI is a utility class to aid in determining the relevant set of parameters
	''' to auto-update when set to listen to the events and other shared utilities.
	''' </summary>	
	Friend Class SubsetHelperUI
		Public Shared Sub PushParameterValuesToNetwork(ByVal nax As INetworkAnalystExtension)
			Try
				If nax Is Nothing Then
					Return
				End If

				Dim naxEnabled As Boolean = False
				Dim naxConfig As IExtensionConfig = TryCast(nax, IExtensionConfig)
				naxEnabled = naxConfig.State = esriExtensionState.esriESEnabled

				If (Not naxEnabled) Then
					Return
				End If

				Dim naWindow As INAWindow = nax.NAWindow
				Dim naLayer As INALayer = Nothing
				Dim naContext As INAContext = Nothing
				Dim nds As INetworkDataset = Nothing

				naLayer = naWindow.ActiveAnalysis
				If Not naLayer Is Nothing Then
					naContext = naLayer.Context
				End If

				If Not naContext Is Nothing Then
					nds = naContext.NetworkDataset
				End If

				If nds Is Nothing Then
					Return
				End If

				Dim dsComponent As IDatasetComponent = TryCast(nds, IDatasetComponent)
				Dim deNet As IDENetworkDataset = TryCast(dsComponent.DataElement, IDENetworkDataset)

				Dim naSolver As INASolver = naContext.Solver
				Dim naSolverSettings2 As INASolverSettings2 = TryCast(naSolver, INASolverSettings2)

				If naSolverSettings2 Is Nothing Then
					Return
				End If

				Dim netAttribute As INetworkAttribute2
				Dim attributeName As String

				Dim netParameters As IArray
				Dim netParameter As INetworkAttributeParameter
				Dim paramName As String
				Dim cParameters As Integer

				Dim paramValue As Object

				Dim cAttributes As Integer = nds.AttributeCount
				Dim a As Integer = 0
				Do While a < cAttributes
					netAttribute = TryCast(nds.Attribute(a), INetworkAttribute2)
					attributeName = netAttribute.Name
					netParameters = netAttribute.Parameters

					cParameters = netParameters.Count
					Dim p As Integer = 0
					Do While p < cParameters
						netParameter = TryCast(netParameters.Element(p), INetworkAttributeParameter)
						paramName = netParameter.Name

						paramValue = naSolverSettings2.AttributeParameterValue(attributeName, paramName)
						netParameter.Value = paramValue
						p += 1
					Loop

					netAttribute.Refresh()
					a += 1
				Loop
			Catch ex As Exception
				MessageBox.Show(ex.Message, "Push Parameter Values To Network")
			End Try
		End Sub

		Public Shared Function ParameterExists(ByVal nds As INetworkDataset, ByVal searchName As String, ByVal vt As VarType) As Boolean
			Dim found As Boolean = False

			Dim netAttribute As INetworkAttribute2
			Dim netParams As IArray
			Dim netParam As INetworkAttributeParameter

			Dim cAttributes As Integer = nds.AttributeCount
			Dim a As Integer = 0
			Do While a < cAttributes
				netAttribute = TryCast(nds.Attribute(a), INetworkAttribute2)
				netParams = Nothing
				Dim cParams As Integer = 0
				If Not netAttribute Is Nothing Then
					netParams = netAttribute.Parameters
				End If

				If Not netParams Is Nothing Then
					cParams = netParams.Count
				End If

				Dim compareName As String
				Dim p As Integer = 0
				Do While p < cParams
					netParam = TryCast(netParams.Element(p), INetworkAttributeParameter)
					compareName = netParam.Name
					If String.Compare(searchName, compareName, True) = 0 Then
						found = True
						Exit Do
					End If
					p += 1
				Loop
				If found Then
					Exit Do
				End If
				a += 1
			Loop

			Return found
		End Function

		Public Shared Sub ClearEIDArrayParameterValues(ByVal nax As INetworkAnalystExtension, ByVal baseName As String)
			Try
				Dim naWindow As INAWindow = nax.NAWindow
				Dim naLayer As INALayer = Nothing
				Dim naContext As INAContext = Nothing
				Dim nds As INetworkDataset = Nothing

				naLayer = naWindow.ActiveAnalysis
				If Not naLayer Is Nothing Then
					naContext = naLayer.Context
				End If

				If Not naContext Is Nothing Then
					nds = naContext.NetworkDataset
				End If

				If nds Is Nothing Then
					Return
				End If

				Dim vt As VarType = SubsetHelperUI.GetEIDArrayParameterType()
				Dim sourceNames As List(Of String) = SubsetHelperUI.FindParameterizedSourceNames(nds, baseName, vt)

				SubsetHelperUI.ClearEIDArrayParameterValues(nax, sourceNames, baseName)
				SubsetHelperUI.PushParameterValuesToNetwork(nax)
			Catch ex As Exception
				Dim msg As String = SubsetHelperUI.GetFullExceptionMessage(ex)
				MessageBox.Show(msg, "Clear Network Element Array Parameters")
			End Try
		End Sub

		Private Shared Sub ClearEIDArrayParameterValues(ByVal nax As INetworkAnalystExtension, ByVal sourceNames As List(Of String), ByVal baseName As String)
			If nax Is Nothing Then
				Return
			End If

			Dim naxEnabled As Boolean = False
			Dim naxConfig As IExtensionConfig = TryCast(nax, IExtensionConfig)
			naxEnabled = naxConfig.State = esriExtensionState.esriESEnabled

			If (Not naxEnabled) Then
				Return
			End If

			Dim eidsBySourceName As Dictionary(Of String, List(Of Integer)) = New Dictionary(Of String, List(Of Integer))
			For Each sourceName As String In sourceNames
				Dim eids As List(Of Integer) = Nothing
				If (Not eidsBySourceName.TryGetValue(sourceName, eids)) Then
					eidsBySourceName.Add(sourceName, Nothing)
				End If
			Next sourceName

			UpdateEIDArrayParameterValuesFromEIDLists(nax, eidsBySourceName, baseName)
		End Sub

		Public Shared Sub UpdateEIDArrayParameterValuesFromEIDLists(ByVal nax As INetworkAnalystExtension, ByVal eidsBySourceName As Dictionary(Of String, List(Of Integer)), ByVal baseName As String)
			If nax Is Nothing Then
				Return
			End If

			Dim naxEnabled As Boolean = False
			Dim naxConfig As IExtensionConfig = TryCast(nax, IExtensionConfig)
			naxEnabled = naxConfig.State = esriExtensionState.esriESEnabled

			If (Not naxEnabled) Then
				Return
			End If

			Dim naWindow As INAWindow = nax.NAWindow
			Dim naLayer As INALayer = Nothing
			Dim naContext As INAContext = Nothing
			Dim nds As INetworkDataset = Nothing

			naLayer = naWindow.ActiveAnalysis
			If Not naLayer Is Nothing Then
				naContext = naLayer.Context
			End If

			If Not naContext Is Nothing Then
				nds = naContext.NetworkDataset
			End If

			If nds Is Nothing Then
				Return
			End If

			Dim dsComponent As IDatasetComponent = TryCast(nds, IDatasetComponent)
			Dim deNet As IDENetworkDataset = TryCast(dsComponent.DataElement, IDENetworkDataset)

			Dim naSolver As INASolver = naContext.Solver
			Dim naSolverSettings2 As INASolverSettings2 = TryCast(naSolver, INASolverSettings2)

			If naSolverSettings2 Is Nothing Then
				Return
			End If

			Dim prefix As String = GetEIDArrayPrefixFromBaseName(baseName)
			Dim vt As VarType = GetEIDArrayParameterType()

			Dim cAttributes As Integer = nds.AttributeCount
			Dim a As Integer = 0
			For a = 0 To cAttributes - 1
				Dim netAttribute As INetworkAttribute2 = TryCast(nds.Attribute(a), INetworkAttribute2)
				Dim netParams As IArray = netAttribute.Parameters
				Dim cParams As Integer = netParams.Count
				Dim paramValue As Object
				Dim p As Integer = 0
				For p = 0 To cParams - 1
					Dim param As INetworkAttributeParameter = TryCast(netParams.Element(p), INetworkAttributeParameter)
					If param.VarType <> CInt(vt) Then
						Continue For
					End If

					Dim paramName As String = param.Name
					Dim sourceName As String = GetSourceNameFromParameterName(prefix, paramName)
					If sourceName.Length = 0 Then
						Continue For
					End If

					Dim eids As List(Of Integer) = Nothing
					If eidsBySourceName.TryGetValue(sourceName, eids) Then
						If Not eids Is Nothing Then
							If eids.Count = 0 Then
								eids = Nothing
							End If
						End If
					End If

					If (Not eids Is Nothing) Then
						paramValue = eids.ToArray()
					Else
						paramValue = Nothing
					End If

					naSolverSettings2.AttributeParameterValue(netAttribute.Name, param.Name) = paramValue
				Next p
			Next a
		End Sub

		Public Shared Sub UpdateEIDArrayParameterValuesFromOIDArrays(ByVal nax As INetworkAnalystExtension, ByVal oidArraysBySourceName As Dictionary(Of String, ILongArray), ByVal baseName As String)
			Dim eidsBySourceName As Dictionary(Of String, List(Of Integer)) = GetEIDListsBySourceName(nax, oidArraysBySourceName, baseName)
			UpdateEIDArrayParameterValuesFromEIDLists(nax, eidsBySourceName, baseName)
		End Sub

		Public Shared Sub UpdateEIDArrayParameterValuesFromGeometry(ByVal nax As INetworkAnalystExtension, ByVal searchGeometry As IGeometry, ByVal baseName As String)
			Dim eidsBySourceName As Dictionary(Of String, List(Of Integer)) = GetEIDListsBySourceName(nax, searchGeometry, baseName)
			UpdateEIDArrayParameterValuesFromEIDLists(nax, eidsBySourceName, baseName)
		End Sub

		Private Shared Function GetEIDListsBySourceName(ByVal nax As INetworkAnalystExtension, ByVal searchObject As Object, ByVal baseName As String) As Dictionary(Of String, List(Of Integer))
			If nax Is Nothing Then
				Return Nothing
			End If

			Dim naxEnabled As Boolean = False
			Dim naxConfig As IExtensionConfig = TryCast(nax, IExtensionConfig)
			naxEnabled = naxConfig.State = esriExtensionState.esriESEnabled

			If (Not naxEnabled) Then
				Return Nothing
			End If

			Dim naWindow As INAWindow = nax.NAWindow
			Dim naLayer As INALayer = Nothing
			Dim naContext As INAContext = Nothing
			Dim nds As INetworkDataset = Nothing

			naLayer = naWindow.ActiveAnalysis
			If Not naLayer Is Nothing Then
				naContext = naLayer.Context
			End If

			If Not naContext Is Nothing Then
				nds = naContext.NetworkDataset
			End If

			Dim netQuery As INetworkQuery = TryCast(nds, INetworkQuery)
			If netQuery Is Nothing Then
				Return Nothing
			End If

			Dim oidSearch As Boolean = False
			Dim geometrySearch As Boolean = False

			If searchObject Is Nothing Then
				Return Nothing
			ElseIf TypeOf searchObject Is Dictionary(Of String, ILongArray) Then
				oidSearch = True
			ElseIf TypeOf searchObject Is IGeometry Then
				geometrySearch = True
			Else
				Return Nothing
			End If

			Dim vt As VarType = GetEIDArrayParameterType()
			Dim sourceNames As List(Of String) = FindParameterizedSourceNames(nds, baseName, vt)
			Dim eidsBySourceName As Dictionary(Of String, List(Of Integer)) = New Dictionary(Of String, List(Of Integer))
			For Each sourceName As String In sourceNames
				Dim netSource As INetworkSource = nds.SourceByName(sourceName)
				Dim sourceID As Integer = netSource.ID
				Dim eids As List(Of Integer) = New List(Of Integer)()

				If oidSearch Then
					Dim oidArraysBySourceName As Dictionary(Of String, ILongArray) = TryCast(searchObject, Dictionary(Of String, ILongArray))
					Dim oids As ILongArray = Nothing
					Dim enumNetElement As IEnumNetworkElement
					Dim netElement As INetworkElement

					If oidArraysBySourceName.TryGetValue(sourceName, oids) Then
						enumNetElement = netQuery.ElementsByOIDs(sourceID, oids)
						enumNetElement.Reset()
						netElement = enumNetElement.Next()
						Do While Not netElement Is Nothing
							eids.Add(netElement.EID)
							netElement = enumNetElement.Next()
						Loop
					End If
				ElseIf geometrySearch Then
					Dim searchGeometry As IGeometry = CType(searchObject, IGeometry)
					If Not searchGeometry Is Nothing AndAlso (Not searchGeometry.IsEmpty) Then
						Dim elementGeometry As IGeometry = Nothing
						Dim elementType As esriNetworkElementType = esriNetworkElementType.esriNETEdge
						Dim eid As Integer = -1

						' Search for the network dataset layer associated with the active analysis layer or create one using the
						' network dataset if matching one not found.
						' If, for example, multiple network dataset layers are added to the map, the active analysis layer
						' might not reference the current network dataset layer (nax.CurrentNetworkLayer).

						Dim ndsLayer As INetworkLayer = New NetworkLayerClass()
						ndsLayer.NetworkDataset = nds

						Dim count As Integer = nax.NetworkLayerCount
						Dim i As Integer = 0
						Do While i < count
							ndsLayer = nax.NetworkLayer(i)
							If ndsLayer.NetworkDataset Is nds Then
								Exit Do
							Else
								ndsLayer = Nothing
							End If
							i += 1
						Loop

						If ndsLayer Is Nothing Then
							ndsLayer = New NetworkLayerClass()
							ndsLayer.NetworkDataset = nds
						End If

						Dim enumLocatedNetElement As IEnumLocatedNetworkElement = Nothing
						If Not ndsLayer Is Nothing Then
							enumLocatedNetElement = ndsLayer.SearchLocatedNetworkElements(sourceName, searchGeometry)
							enumLocatedNetElement.Reset()
							eid = enumLocatedNetElement.Next(elementGeometry, elementType)
							Do While eid <> -1
								eids.Add(eid)
								eid = enumLocatedNetElement.Next(elementGeometry, elementType)
							Loop
						End If
					End If
				End If

				eidsBySourceName.Add(sourceName, eids)
			Next sourceName

			Return eidsBySourceName
		End Function

		Public Shared Function GetOIDArraysBySourceNameFromMapSelection(ByVal map As IMap, ByVal sourceNames As List(Of String)) As Dictionary(Of String, ILongArray)
			Dim uid As UIDClass = New UIDClass()
			uid.Value = "{E156D7E5-22AF-11D3-9F99-00C04F6BC78E}" 'IGeoFeatureLayer

			Dim searchEnumLayer As IEnumLayer = map.Layers(uid, True)
			searchEnumLayer.Reset()

			'create result dictionary from source names with empty oidArrays

			Dim oidArraysBySourceName As Dictionary(Of String, ILongArray) = New Dictionary(Of String, ILongArray)()
			Dim oidArray As ILongArray = Nothing

			For Each sourceName As String In sourceNames
				If (Not oidArraysBySourceName.TryGetValue(sourceName, oidArray)) Then
					oidArray = New LongArrayClass()
					oidArraysBySourceName.Add(sourceName, oidArray)
				End If
			Next sourceName

			Dim layer As ILayer = searchEnumLayer.Next()
			Do While Not layer Is Nothing
				Dim displayTable As IDisplayTable = TryCast(layer, IDisplayTable)
				Dim sourceName As String = ""
				If layer.Valid AndAlso layer.Visible AndAlso Not displayTable Is Nothing Then
					Dim ds As IDataset = TryCast(displayTable.DisplayTable, IDataset)
					If Not ds Is Nothing Then
						sourceName = ds.Name
					End If
				End If

				If sourceName.Length > 0 Then
					If oidArraysBySourceName.TryGetValue(sourceName, oidArray) Then
						Dim selSet As ISelectionSet = displayTable.DisplaySelectionSet
						Dim enumOIDs As IEnumIDs = Nothing
						If Not selSet Is Nothing Then
							enumOIDs = selSet.IDs
						End If

						If Not enumOIDs Is Nothing Then
							enumOIDs.Reset()
							Dim oid As Integer = enumOIDs.Next()
							Do While oid <> -1
								oidArray.Add(oid)
								oid = enumOIDs.Next()
							Loop
						End If
					End If
				End If

				layer = searchEnumLayer.Next()
			Loop

			Return oidArraysBySourceName
		End Function

		Public Shared Function GetSearchGeometryFromGraphics(ByVal graphics As IGraphicsContainer) As IGeometry
			Dim geometryBag As IGeometryCollection = New GeometryBagClass()
			Dim element As IElement
			Dim geometry As IGeometry

			graphics.Reset()
			element = graphics.Next()

			Dim before As Object = Type.Missing
			Dim after As Object = Type.Missing

			Do While Not element Is Nothing
				geometry = element.Geometry
				If TypeOf geometry Is IPolygon Then
					geometryBag.AddGeometry(geometry, before, after)
				End If

				element = graphics.Next()
			Loop

			Dim searchGeometry As IGeometry = TryCast(geometryBag, IGeometry)

			Return searchGeometry
		End Function

		Public Shared Function FindParameterizedSourceNames(ByVal nds As INetworkDataset, ByVal baseName As String, ByVal vt As VarType) As List(Of String)
			Dim sourceNamesList As List(Of String) = New List(Of String)()
			Dim sourceNamesDictionary As Dictionary(Of String, Nullable(Of Integer)) = New Dictionary(Of String, Nullable(Of Integer))

			Dim dummyValue As Nullable(Of Integer) = Nothing
			Dim foundDummyValue As Nullable(Of Integer) = Nothing

			Dim prefix As String = GetEIDArrayPrefixFromBaseName(baseName)

			Dim netSource As INetworkSource
			Dim sourceName As String
			Dim searchParamName As String
			Dim count As Integer = nds.SourceCount
			Dim i As Integer = 0
			For i = 0 To count - 1
				netSource = nds.Source(i)
				sourceName = netSource.Name
				If sourceNamesDictionary.TryGetValue(sourceName, foundDummyValue) Then
					Continue For
				End If

				searchParamName = GetSourceParameterName(prefix, sourceName)

				If ParameterExists(nds, searchParamName, vt) Then
					sourceNamesList.Add(sourceName)
					sourceNamesDictionary.Add(sourceName, dummyValue)
				End If
			Next i

			Return sourceNamesList
		End Function

		Public Shared Function GetEIDArrayParameterType() As VarType
			Dim vt As VarType = VarType.Array Or VarType.Integer
			Return vt
		End Function

		Public Shared ReadOnly Property SelectionEIDArrayBaseName() As String
			Get
				Return FilterSubsetEvaluator.BaseParameterName
			End Get
		End Property

		Public Shared ReadOnly Property GraphicsEIDArrayBaseName() As String
			Get
				Return ScaleSubsetEvaluator.BaseParameterName
			End Get
		End Property

		Private Shared Function GetEIDArrayPrefixFromBaseName(ByVal baseName As String) As String
			Dim baseNameEIDArrayModifer As String = "_eids"
			Dim prefix As String = baseName
			prefix &= baseNameEIDArrayModifer

			Return prefix
		End Function

		Private Shared Function GetSourceNameFromParameterName(ByVal prefix As String, ByVal paramName As String) As String
			Dim searchSubName As String = prefix & "_"

			Dim searchSubNameLen As Integer = searchSubName.Length
			Dim paramNameLen As Integer = paramName.Length
			If searchSubNameLen <= 0 OrElse searchSubNameLen >= paramNameLen Then
				Return ""
			End If

			Dim compareSubName As String = paramName.Substring(0, searchSubNameLen)
			If String.Compare(compareSubName, searchSubName, True) <> 0 Then
				Return ""
			End If

			Dim sourceName As String = paramName.Substring(searchSubNameLen)
			Return sourceName
		End Function

		Private Shared Function GetSourceParameterName(ByVal prefix As String, ByVal sourceName As String) As String
			Dim paramName As String = prefix
			paramName &= "_"
			paramName &= sourceName

			Return paramName
		End Function

		Public Shared Function GetNAXConfiguration(ByVal app As IApplication) As IExtensionConfig
			Dim extConfig As IExtensionConfig = Nothing
			Try
				If Not app Is Nothing Then
					Dim extCLSID As UID = New UIDClass()
					extCLSID.Value = "{C967BD39-1118-42EE-AAAB-B31642C89C3E}" ' Network Analyst
					extConfig = TryCast(app.FindExtensionByCLSID(extCLSID), IExtensionConfig)
				End If
			Catch
				extConfig = Nothing
			End Try

			Return extConfig
		End Function

		Public Shared Function GetFullExceptionMessage(ByVal ex As Exception) As String
			Dim msg As String = ""
			Dim subMsg As String = ""

			Do While Not ex Is Nothing
				subMsg = ex.Message
				If subMsg.Length > 0 AndAlso msg.Length > 0 Then
					msg &= Constants.vbLf
				End If

				msg &= subMsg
				ex = ex.InnerException
			Loop

			Return msg
		End Function
	End Class
End Namespace
