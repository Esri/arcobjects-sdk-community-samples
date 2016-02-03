Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.Animation
Imports ESRI.ArcGIS.ADF
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.SystemUI
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS.ADF.CATIDs

<Guid("0FF67964-84F1-4B60-9FDD-ABF2A7D0FDF8"), ClassInterface(ClassInterfaceType.None), ProgId("AnimationDeveloperSamples.AnimationTypeMapGraphic")> _
Public Class AnimationTypeMapGraphic : Implements IAGAnimationType, IAGAnimationTypeUI
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
        propName(0) = "Position"
        propName(1) = "Rotation"

        propType = New esriAnimationPropertyType(1) {}
        propType(0) = esriAnimationPropertyType.esriAnimationPropertyPoint
        propType(1) = esriAnimationPropertyType.esriAnimationPropertyDouble

        typeName = "Map Graphic"
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
            Dim array As IArray
            array = Me.ObjectArray(pContainer)
            Dim elem As IElement = CType(array.Element(objectID), IElement)

            Return elem
        End Get
    End Property
    Public ReadOnly Property AnimationObjectID(ByVal pContainer As IAGAnimationContainer, ByVal pObject As Object) As Integer Implements IAGAnimationType.AnimationObjectID
        Get
            Dim array As IArray = Me.ObjectArray(pContainer)
            Dim count As Integer = array.Count
            Dim objectID As Integer = 0
            Dim i As Integer = 0
            Do While i < count
                Dim elem As IElement = CType(array.Element(i), IElement)
                If elem Is pObject Then
                    Exit Do
                End If
                objectID += 1
                i += 1
            Loop

            Return objectID
        End Get
    End Property
    Public ReadOnly Property AnimationObjectName(ByVal pContainer As IAGAnimationContainer, ByVal pObject As Object) As String Implements IAGAnimationType.AnimationObjectName
        Get
            Dim objectName As String
            Dim elemProps As IElementProperties = CType(pObject, IElementProperties)
            objectName = elemProps.Name

            Return objectName
        End Get
    End Property
    Public ReadOnly Property AppliesToObject(ByVal pObject As Object) As Boolean Implements IAGAnimationType.AppliesToObject
        Get
            If (TypeOf pObject Is IMarkerElement) OrElse (TypeOf pObject Is IGroupElement) OrElse (TypeOf pObject Is ITextElement) Then
                Return True
            Else
                Return False
            End If
        End Get
    End Property
    Public ReadOnly Property CLSID() As UID Implements IAGAnimationType.CLSID
        Get
            Dim uid As UID = New UIDClass()
            uid.Value = "{0FF67964-84F1-4B60-9FDD-ABF2A7D0FDF8}"
            Return uid
        End Get
    End Property
    Public ReadOnly Property KeyframeCLSID() As UID Implements IAGAnimationType.KeyframeCLSID
        Get
            Dim uid As UID = New UIDClass()
            uid.Value = "{A9250243-7A16-4F8D-AA06-29F559ADA0C5}"
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
            Dim graphicsContainer As IGraphicsContainer = TryCast(view, IGraphicsContainer)
            graphicsContainer.Reset()

            Dim array As IArray = New ArrayClass()
            Dim elem As IElement = graphicsContainer.Next()

            Do While Not elem Is Nothing
                If AppliesToObject(elem) Then
                    array.Add(elem)
                End If
                elem = graphicsContainer.Next()
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
                Return 2
            Else
                Return 1
            End If
        End Get
    End Property
    Public ReadOnly Property ColumnName(ByVal propIndex As Integer, ByVal columnIndex As Integer) As String Implements IAGAnimationTypeUI.ColumnName
        Get
            If propIndex = 0 Then
                If columnIndex = 0 Then
                    Return "Position:X"
                Else
                    Return "Position:Y"
                End If
            ElseIf propIndex = 1 Then
                Return "Rotation"
            End If

            Return Nothing
        End Get
    End Property
#End Region
End Class
