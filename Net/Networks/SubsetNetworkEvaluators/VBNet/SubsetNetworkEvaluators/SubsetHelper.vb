Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Runtime.InteropServices
Imports System.Reflection
Imports System.ComponentModel
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Geodatabase

''' <summary>
''' Utility methods for working with parameter values, and other shared utilities in setting up
''' these custom subset evaluators.
''' </summary>

Namespace SubsetNetworkEvaluators
	<Flags()> _
	Public Enum VarType
		Empty = &H0	'VT_EMPTY
		Null = &H1 'VT_NULL
		[Short] = &H2 'VT_I2
		[Integer] = &H3	'VT_I4
		Float = &H4	'VT_R4
		[Double] = &H5 'VT_R8
		[Date] = &H7 'VT_DATE
		[String] = &H8 'VT_BSTR
		Bool = &HB 'VT_BOOL
		ComObject = &HD	'VT_UNKNOWN
		Array = &H2000 'VT_ARRAY
	End Enum ' enum VarType

	Public Class SubsetHelper
		Public Shared Function FindParameter(ByVal netAttributeParams As IArray, ByVal searchName As String) As Integer
			If netAttributeParams Is Nothing OrElse searchName.Length <= 0 Then
				Return -1
			End If

			Dim compareName As String
			Dim netAttributeParam As INetworkAttributeParameter
			Dim count As Integer = netAttributeParams.Count
			Dim i As Integer = 0
			Do While i < count
				netAttributeParam = TryCast(netAttributeParams.Element(i), INetworkAttributeParameter)
				If Not netAttributeParam Is Nothing Then
					compareName = netAttributeParam.Name
					If String.Compare(searchName, compareName, True) = 0 Then
						Return i
					End If
				End If
				i += 1
			Loop

			Return -1
		End Function

		Public Shared Function GetSourceNames(ByVal netSources As List(Of INetworkSource)) As List(Of String)
			Dim sourceNames As List(Of String) = New List(Of String)()
			If netSources Is Nothing Then
				Return sourceNames
			End If

			For Each netSource As INetworkSource In netSources
				sourceNames.Add(netSource.Name)
			Next netSource

			Return sourceNames
		End Function

		Public Shared Function GetSourceList(ByVal netSourcesArray As IArray) As List(Of INetworkSource)
			Dim netSources As List(Of INetworkSource) = New List(Of INetworkSource)()
			Dim count As Integer = netSourcesArray.Count
			Dim netSource As INetworkSource
			Dim i As Integer = 0
			Do While i < count
				netSource = TryCast(netSourcesArray.Element(i), INetworkSource)
				If Not netSource Is Nothing Then
					netSources.Add(netSource)
				End If
				i += 1
			Loop
			Return netSources
		End Function

		Public Shared Function GetSourceList(ByVal netSources As List(Of INetworkSource), ByVal eType As esriNetworkElementType) As List(Of INetworkSource)
			Dim eTypes As List(Of esriNetworkElementType) = New List(Of esriNetworkElementType)()
			eTypes.Add(eType)
			Return GetSourceList(netSources, eTypes)
		End Function

		Public Shared Function GetSourceList(ByVal netSources As List(Of INetworkSource), ByVal eTypes As List(Of esriNetworkElementType)) As List(Of INetworkSource)
			Dim subList As List(Of INetworkSource) = New List(Of INetworkSource)()
			If netSources Is Nothing OrElse eTypes Is Nothing Then
				Return subList
			End If

			For Each netSource As INetworkSource In netSources
				For Each eType As esriNetworkElementType In eTypes
					If netSource.ElementType = eType Then
						subList.Add(netSource)
						Exit For
					End If
				Next eType
			Next netSource
			Return subList
		End Function

		Public Shared Function RemoveAttributeByName(ByVal netAttributes As IArray, ByVal name As String) As IArray
			Return RemoveAttributesByKeyName(netAttributes, name, True)
		End Function

		Public Shared Function RemoveAttributesByPrefix(ByVal netAttributes As IArray, ByVal prefix As String) As IArray
			Return RemoveAttributesByKeyName(netAttributes, prefix, True)
		End Function

		Public Shared Function RemoveAttributesBySuffix(ByVal netAttributes As IArray, ByVal suffix As String) As IArray
			Return RemoveAttributesByKeyName(netAttributes, suffix, False)
		End Function

		Public Shared Function RemoveAttributesByKeyName(ByVal netAttributes As IArray, ByVal keyName As String, ByVal keyIsPrefix As Boolean) As IArray
			Dim preservedNetAttributes As IArray = New ArrayClass()

			Dim keyNameLen As Integer = keyName.Length
			Dim netAttributeNameLen As Integer
			Dim netAttribute As INetworkAttribute
			Dim netAttributeName As String
			Dim isKeyAttribute As Boolean
			Dim ignoreCase As Boolean = True

			Dim count As Integer = netAttributes.Count
			Dim i As Integer = 0
			For i = 0 To count - 1
				netAttribute = TryCast(netAttributes.Element(i), INetworkAttribute)
				If netAttribute Is Nothing Then
					Continue For
				End If

				netAttributeName = netAttribute.Name
				netAttributeNameLen = netAttributeName.Length

				isKeyAttribute = False
				If keyNameLen = 0 Then
					isKeyAttribute = False
				ElseIf netAttributeNameLen < keyNameLen Then
					isKeyAttribute = False
				Else
					Dim startIndex As Integer = 0
					If (Not keyIsPrefix) Then
						startIndex = netAttributeNameLen - keyNameLen
					End If

					If String.Compare(netAttributeName.Substring(startIndex, keyNameLen), keyName, ignoreCase) = 0 Then
						isKeyAttribute = True
					End If
				End If

				If (Not isKeyAttribute) Then
					preservedNetAttributes.Add(netAttribute)
				End If
			Next i

			Return preservedNetAttributes
		End Function

		Public Shared Function FindAttributeIndexes(ByVal netAttributes As IArray, ByVal usage As esriNetworkAttributeUsageType, ByVal dataType As esriNetworkAttributeDataType, ByVal searchTimeUnits As Boolean, ByVal ignoreDataType As Boolean) As List(Of Integer)
			Dim netAttribute As INetworkAttribute2 = Nothing
			Dim units As esriNetworkAttributeUnits = esriNetworkAttributeUnits.esriNAUUnknown
			Dim isSearchUnits As Boolean = False
			Dim isUnknownUnits As Boolean = False
			Dim isTimeUnits As Boolean = False

			Dim netAttributeIndexes As List(Of Integer) = New List(Of Integer)()
			Dim count As Integer = netAttributes.Count

			Dim i As Integer = 0
			For i = 0 To count - 1
				netAttribute = TryCast(netAttributes.Element(i), INetworkAttribute2)
				If netAttribute Is Nothing Then
					Continue For
				End If

				If netAttribute.UsageType = usage AndAlso (ignoreDataType OrElse netAttribute.DataType = dataType) Then
					units = netAttribute.Units
					isSearchUnits = False

					If usage <> esriNetworkAttributeUsageType.esriNAUTCost Then
						isSearchUnits = True
					Else
						isUnknownUnits = False
						If units = esriNetworkAttributeUnits.esriNAUUnknown Then
							isUnknownUnits = True
						End If

						isTimeUnits = False
						If (Not isUnknownUnits) Then
							If units = esriNetworkAttributeUnits.esriNAUMinutes OrElse units = esriNetworkAttributeUnits.esriNAUSeconds OrElse units = esriNetworkAttributeUnits.esriNAUHours OrElse units = esriNetworkAttributeUnits.esriNAUDays Then
								isTimeUnits = True
							End If

							If searchTimeUnits Then
								isSearchUnits = isTimeUnits
							Else
								isSearchUnits = Not isTimeUnits
							End If
						End If
					End If
					If isSearchUnits Then
						netAttributeIndexes.Add(i)
					End If
				End If
			Next i

			Return netAttributeIndexes
		End Function

		Public Shared Function FindAttributes(ByVal netAttributesArray As IArray, ByVal netAttributeIndexes As List(Of Integer)) As List(Of INetworkAttribute2)
			Dim netAttributes As List(Of INetworkAttribute2) = New List(Of INetworkAttribute2)()
			For Each i As Integer In netAttributeIndexes
				Dim netAttribute As INetworkAttribute2 = TryCast(netAttributesArray.Element(i), INetworkAttribute2)
				If Not netAttribute Is Nothing Then
					netAttributes.Add(netAttribute)
				End If
			Next i

			Return netAttributes
		End Function

		Public Shared Sub SetDefaultEvaluator(ByVal netAttribute As IEvaluatedNetworkAttribute, ByVal defaultValue As Object, ByVal eType As esriNetworkElementType)
			Dim constEvaluator As INetworkConstantEvaluator = New NetworkConstantEvaluatorClass()
			constEvaluator.ConstantValue = defaultValue
			Dim eval As INetworkEvaluator = TryCast(constEvaluator, INetworkEvaluator)
			netAttribute.DefaultEvaluator(eType) = eval
		End Sub

		Public Shared Sub SetEvaluators(ByVal netAttribute As IEvaluatedNetworkAttribute, ByVal netSource As INetworkSource, ByVal t As Type)
			Dim eType As esriNetworkElementType = netSource.ElementType
			If eType = esriNetworkElementType.esriNETEdge Then
				SetEvaluator(netAttribute, netSource, t, esriNetworkEdgeDirection.esriNEDAlongDigitized)
				SetEvaluator(netAttribute, netSource, t, esriNetworkEdgeDirection.esriNEDAgainstDigitized)
			Else
				SetEvaluator(netAttribute, netSource, t, esriNetworkEdgeDirection.esriNEDNone)
			End If
		End Sub

		Public Shared Sub SetEvaluator(ByVal netAttribute As IEvaluatedNetworkAttribute, ByVal netSource As INetworkSource, ByVal t As Type, ByVal dirType As esriNetworkEdgeDirection)
			Dim obj As Object = Activator.CreateInstance(t)
			Dim eval As INetworkEvaluator = TryCast(obj, INetworkEvaluator)
			netAttribute.Evaluator(netSource, dirType) = eval
		End Sub
	End Class
End Namespace