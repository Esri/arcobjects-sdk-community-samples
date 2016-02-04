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
Imports System.Collections.Generic
Imports System.Text
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.Animation
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.SystemUI
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.ADF.CATIDs

Namespace AnimationDeveloperSamples
    <Guid("AA9B2E14-686F-4411-BB84-C44B706C83E4"), ClassInterface(ClassInterfaceType.None), ProgId("AnimationDeveloperSamples.AnimationTypeLayerEffects")> _
 Public Class AnimationTypeLayerEffects : Implements IAGAnimationType, IAGAnimationTypeUI
#Region "COM Registration Function(s)"
        <ComRegisterFunction(), ComVisible(False)> _
        Private Shared Sub RegisterFunction(ByVal registerType As Type)
            ' Required for ArcGIS Component Category Registrar support
            ArcGISCategoryRegistration(registerType)

            '
            ' TODO: Add any COM registration code here
            '
        End Sub

        <ComUnregisterFunction(), ComVisible(False)> _
        Private Shared Sub UnregisterFunction(ByVal registerType As Type)
            ' Required for ArcGIS Component Category Registrar support
            ArcGISCategoryUnregistration(registerType)

            '
            ' TODO: Add any COM unregistration code here
            '
        End Sub

#Region "ArcGIS Component Category Registrar generated code"
        ''' <summary>
        ''' Required method for ArcGIS Component Category registration -
        ''' Do not modify the contents of this method with the code editor.
        ''' </summary>
        Private Shared Sub ArcGISCategoryRegistration(ByVal registerType As Type)
            Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
            MapAnimationTypes.Register(regKey)

        End Sub
        ''' <summary>
        ''' Required method for ArcGIS Component Category unregistration -
        ''' Do not modify the contents of this method with the code editor.
        ''' </summary>
        Private Shared Sub ArcGISCategoryUnregistration(ByVal registerType As Type)
            Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
            MapAnimationTypes.Unregister(regKey)

        End Sub

#End Region
#End Region
        Private propName As String()
        Private propType As esriAnimationPropertyType()
        Private typeName As String

#Region "constructor"
        Public Sub New()
            propName = New String(1) {}
            propName(0) = "Brightness"
            propName(1) = "Contrast"

            propType = New esriAnimationPropertyType(1) {}
            propType(0) = esriAnimationPropertyType.esriAnimationPropertyInt
            propType(1) = esriAnimationPropertyType.esriAnimationPropertyInt

            typeName = "Layer Effects"
        End Sub
#End Region

#Region "IAGAnimationType members"
        Public ReadOnly Property AnimationClass() As esriAnimationClass Implements IAGAnimationType.AnimationClass
            Get
                Return esriAnimationClass.esriAnimationClassGeneric
            End Get
        End Property

        Public ReadOnly Property AnimationObjectByID(ByVal pContainer As IAGAnimationContainer, ByVal objectID As Integer) As Object Implements IAGAnimationType.AnimationObjectByID
            Get
                Dim objectArray As IArray = Me.ObjectArray(pContainer)
                Return CObj(objectArray.Element(objectID))
            End Get
        End Property

        Public ReadOnly Property AnimationObjectID(ByVal pContainer As IAGAnimationContainer, ByVal pObject As Object) As Integer Implements IAGAnimationType.AnimationObjectID
            Get
                Dim objectArray As IArray = Me.ObjectArray(pContainer)
                Dim objCount As Integer = objectArray.Count

                Dim i As Integer = 0
                i = 0
                Do While i < objCount
                    If pObject Is objectArray.Element(i) Then
                        Exit Do
                    End If
                    i += 1
                Loop
                Return i
            End Get
        End Property

        Public ReadOnly Property AnimationObjectName(ByVal pContainer As IAGAnimationContainer, ByVal pObject As Object) As String Implements IAGAnimationType.AnimationObjectName
            Get
                Dim layer As ILayer = CType(pObject, ILayer)
                If Not layer Is Nothing Then
                    Return layer.Name
                Else
                    Return ""
                End If
            End Get
        End Property

        Public ReadOnly Property AppliesToObject(ByVal pObject As Object) As Boolean Implements IAGAnimationType.AppliesToObject
            Get
                If TypeOf pObject Is ILayer Then
                    Dim layerEffects As ILayerEffects = CType(pObject, ILayerEffects)
                    If layerEffects.SupportsBrightnessChange AndAlso layerEffects.SupportsContrastChange Then
                        Return True
                    Else
                        Return False
                    End If
                Else
                    Return False
                End If
            End Get
        End Property

        Public ReadOnly Property CLSID() As UID Implements IAGAnimationType.CLSID
            Get
                Dim uid As UID = New UIDClass()
                uid.Value = "{AA9B2E14-686F-4411-BB84-C44B706C83E4}"
                Return uid
            End Get
        End Property

        Public ReadOnly Property KeyframeCLSID() As UID Implements IAGAnimationType.KeyframeCLSID
            Get
                Dim uid As UID = New UIDClass()
                uid.Value = "{EB5D227B-4814-4720-877B-D19519B2BBD6}"
                Return uid
            End Get
        End Property

        Public ReadOnly Property Name() As String Implements IAGAnimationType.Name
            Get
                Return typeName
            End Get
        End Property

        Public ReadOnly Property ObjectArray(ByVal pContainer As IAGAnimationContainer) As IArray Implements IAGAnimationType.ObjectArray
            Get
                Dim view As IActiveView = TryCast(pContainer.CurrentView, IActiveView)
                Dim array As IArray = New ArrayClass()

                Dim layer1 As ILayer
                Dim layerCount As Integer = view.FocusMap.LayerCount
                Dim i As Integer = 0
                i = 0
                Do While i < layerCount
                    layer1 = view.FocusMap.Layer(i)
                    If AppliesToObject(layer1) Then
                        array.Add(layer1)
                    End If
                    i += 1
                Loop

                Return array
            End Get
        End Property

        Public ReadOnly Property PropertyCount() As Integer Implements IAGAnimationType.PropertyCount
            Get
                Return 2
            End Get
        End Property

        Public ReadOnly Property PropertyName(ByVal index As Integer) As String Implements IAGAnimationType.PropertyName
            Get
                If index >= 0 AndAlso index < 2 Then
                    Return propName(index)
                Else
                    Return Nothing
                End If
            End Get
        End Property

        Public ReadOnly Property PropertyType(ByVal index As Integer) As esriAnimationPropertyType Implements IAGAnimationType.PropertyType
            Get
                Return propType(index)
            End Get
        End Property

        Public Sub ResetObject(ByVal pContainer As IAGAnimationContainer, ByVal pObject As Object) Implements IAGAnimationType.ResetObject
            Return
        End Sub

        Public Sub UpdateTrackExtensions(ByVal pTrack As IAGAnimationTrack) Implements IAGAnimationType.UpdateTrackExtensions
            Return
        End Sub
#End Region

#Region "IAGAnimationTypeUI members"

        Public ReadOnly Property ChoiceList(ByVal propIndex As Integer, ByVal columnIndex As Integer) As IStringArray Implements IAGAnimationTypeUI.ChoiceList
            Get
                Return Nothing
            End Get
        End Property
        Public ReadOnly Property ColumnCount(ByVal propIndex As Integer) As Integer Implements IAGAnimationTypeUI.ColumnCount
            Get
                If propIndex = 0 Then
                    Return 1
                Else
                    Return 1
                End If
            End Get
        End Property
        Public ReadOnly Property ColumnName(ByVal propIndex As Integer, ByVal columnIndex As Integer) As String Implements IAGAnimationTypeUI.ColumnName
            Get
                If propIndex = 0 Then
                    Return "Brightness"
                ElseIf propIndex = 1 Then
                    Return "Contrast"
                End If

                Return Nothing
            End Get
        End Property
#End Region

    End Class
End Namespace
