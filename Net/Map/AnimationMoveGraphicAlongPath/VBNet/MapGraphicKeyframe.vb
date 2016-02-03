Imports Microsoft.VisualBasic
Imports System
Imports System.Windows.Forms
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

<Guid("A9250243-7A16-4F8D-AA06-29F559ADA0C5"), ClassInterface(ClassInterfaceType.None), ProgId("AnimationDeveloperSamples.MapGraphicKeyframe")> _
Public Class MapGraphicKeyframe : Implements IAGKeyframe, IAGKeyframeUI
    Private activeProps As ILongArray
    Private animType As IAGAnimationType
    Private name_Renamed As String
    Private bObjectsNeedRefresh As Boolean
    Private Position As IPoint
    Private Rotation As Double
    Private timeStamp_Renamed As Double

#Region "constructor"
    Public Sub New()
        activeProps = New LongArrayClass()
        activeProps.Add(0)
        activeProps.Add(1)
        animType = New AnimationTypeMapGraphic()
        name_Renamed = ""
        bObjectsNeedRefresh = False
        Position = New PointClass()
        Rotation = 0.0
        timeStamp_Renamed = 0.0
    End Sub
#End Region

#Region "IAGKeyframe members"
    Public Property ActiveProperties() As ILongArray Implements IAGKeyframe.ActiveProperties
        Get
            Return activeProps
        End Get
        Set(ByVal value As ILongArray)
            activeProps = value
        End Set
    End Property

    Public ReadOnly Property AnimationType() As IAGAnimationType Implements IAGKeyframe.AnimationType
        Get
            Return animType
        End Get
    End Property

    Public Sub Apply(ByVal pTrack As IAGAnimationTrack, ByVal pContainer As IAGAnimationContainer, ByVal pObject As Object) Implements IAGKeyframe.Apply
        Dim elem As IElement = CType(pObject, IElement)

        UpdateGraphicObject(elem, pContainer, pTrack, Position, Rotation)

        Return
    End Sub

    Public Sub CaptureProperties(ByVal pContainer As IAGAnimationContainer, ByVal pObject As Object) Implements IAGKeyframe.CaptureProperties
        Dim elem As IElement = TryCast(pObject, IElement)
        Dim view As IActiveView = TryCast(pContainer.CurrentView, IActiveView)
        Dim graphicsConSel As IGraphicsContainerSelect = TryCast(view, IGraphicsContainerSelect)
        Dim disp As IDisplay = TryCast(view.ScreenDisplay, IDisplay)
        Dim elemEnv As IEnvelope = GetElementBound(elem, pContainer)

        If elem.Geometry.GeometryType = esriGeometryType.esriGeometryPoint Then
            Position = TryCast(elem.Geometry, IPoint)
        Else
            Dim elementEnvelope As IEnvelope = elem.Geometry.Envelope
            Dim elementArea As IArea = TryCast(elementEnvelope, IArea)
            Position = elementArea.Centroid
        End If

        Dim elemProps As IElementProperties = CType(elem, IElementProperties)
        If Not elemProps.CustomProperty Is Nothing Then
            Rotation = CDbl(elemProps.CustomProperty)
        End If
    End Sub

    Public Sub Interpolate(ByVal pTrack As IAGAnimationTrack, ByVal pContainer As IAGAnimationContainer, ByVal pObject As Object, ByVal propertyIndex As Integer, ByVal time As Double, ByVal pNextKeyframe As IAGKeyframe, ByVal pPrevKeyframe As IAGKeyframe, ByVal pAfterNextKeyframe As IAGKeyframe) Implements IAGKeyframe.Interpolate
        If time < TimeStamp OrElse time > pNextKeyframe.TimeStamp Then
            Return
        End If

        Dim elem As IElement = CType(pObject, IElement)
        Dim new_pos As IPoint = New PointClass()

        If propertyIndex = 0 Then
            Dim x1 As Double
            Dim y1 As Double
            Dim nextPosition As IPoint = CType(pNextKeyframe.PropertyValue(0), IPoint)

            Dim timeFactor As Double
            timeFactor = (time - TimeStamp) / (pNextKeyframe.TimeStamp - TimeStamp) 'ignoring pPrevKeyframe and pAfterNextKeyframe

            x1 = Position.X * (1 - timeFactor) + nextPosition.X * timeFactor
            y1 = Position.Y * (1 - timeFactor) + nextPosition.Y * timeFactor

            new_pos.PutCoords(x1, y1)

            If Not (TypeOf elem Is ILineElement) Then
                MoveElement(elem, new_pos, pContainer)
                TracePath(elem, new_pos, pContainer, pTrack, Me)
                bObjectsNeedRefresh = True
            End If
        End If
        If propertyIndex = 1 Then
            'this property only applies to the point graphic 
            If Not (TypeOf elem Is ILineElement) Then
                RotateElement(elem, Rotation, pContainer)
                bObjectsNeedRefresh = True
            End If
        End If

        Return
    End Sub

    Public Property IsActiveProperty(ByVal propIndex As Integer) As Boolean Implements IAGKeyframe.IsActiveProperty
        Get
            Dim bIsActive As Boolean = False
            Dim count As Integer = activeProps.Count

            Dim i As Integer = 0
            Do While i < count
                Dim temp As Long = activeProps.Element(i)
                If temp = propIndex Then
                    bIsActive = True
                End If
                i += 1
            Loop
            Return bIsActive
        End Get
        Set(ByVal value As Boolean)
            If value Then
                If IsActiveProperty(propIndex) = False Then
                    activeProps.Add(propIndex)
                End If
            Else
                If IsActiveProperty(propIndex) = True Then
                    Dim i As Integer = 0
                    Dim count As Integer = activeProps.Count
                    i = 0
                    Do While i < count
                        Dim temp As Long = activeProps.Element(i)
                        If temp = propIndex Then
                            Exit Do
                        End If
                        i += 1
                    Loop

                    activeProps.Remove(i)
                End If
            End If
        End Set
    End Property

    Public Property Name() As String Implements IAGKeyframe.Name
        Get
            Return name_Renamed
        End Get

        Set(ByVal value As String)
            name_Renamed = value
        End Set
    End Property
    Public ReadOnly Property ObjectNeedsRefresh() As Boolean Implements IAGKeyframe.ObjectNeedsRefresh
        Get
            Return bObjectsNeedRefresh
        End Get
    End Property

    Public Property PropertyValue(ByVal propIndex As Integer) As Object Implements IAGKeyframe.PropertyValue
        Get
            If propIndex = 0 Then
                Return CObj(Position)
            ElseIf propIndex = 1 Then
                Return CObj(Rotation)
            Else
                Return Nothing
            End If
        End Get
        Set(ByVal value As Object)
            If propIndex = 0 Then
                Position = CType(value, IPoint)
            ElseIf propIndex = 1 Then
                Rotation = CType(value, Double)
            Else
                Return
            End If
        End Set
    End Property

    Public Sub RefreshObject(ByVal pTrack As IAGAnimationTrack, ByVal pContainer As IAGAnimationContainer, ByVal pObject As Object) Implements IAGKeyframe.RefreshObject
        RefreshGraphicObject(CType(pObject, IElement), pContainer)
    End Sub
    Public Property TimeStamp() As Double Implements IAGKeyframe.TimeStamp
        Get
            Return timeStamp_Renamed
        End Get

        Set(ByVal value As Double)
            timeStamp_Renamed = value
        End Set
    End Property
#End Region

#Region "IAGKeyframeUI members"
    Public Function GetText(ByVal propIndex As Integer, ByVal columnIndex As Integer) As String Implements IAGKeyframeUI.GetText
        Dim text As String
        text = Nothing
        Select Case propIndex
            Case 0
                If columnIndex = 0 Then
                    text = System.Convert.ToString(Position.X)
                End If
                If columnIndex = 1 Then
                    text = System.Convert.ToString(Position.Y)
                End If
            Case 1
                text = System.Convert.ToString(Rotation)
        End Select
        Return text
    End Function

    Public Sub SetText(ByVal propIndex As Integer, ByVal columnIndex As Integer, ByVal text As String) Implements IAGKeyframeUI.SetText
        Select Case propIndex
            Case 0
                If columnIndex = 0 Then
                    Position.X = System.Convert.ToDouble(text)
                End If
                If columnIndex = 1 Then
                    Position.Y = System.Convert.ToDouble(text)
                End If
            Case 1
                Rotation = System.Convert.ToDouble(text)
        End Select

        Return
    End Sub
#End Region

#Region "private methods"
    Private Sub RotateElement(ByVal elem As IElement, ByVal new_angle As Double, ByVal pContainer As IAGAnimationContainer)
        Dim transform2D As ITransform2D = TryCast(elem, ITransform2D)
        Dim rotateOrigin As IPoint

        If elem.Geometry.GeometryType = esriGeometryType.esriGeometryPoint Then
            rotateOrigin = TryCast(elem.Geometry, IPoint)
        Else
            Dim elementEnvelope As IEnvelope = elem.Geometry.Envelope
            Dim elementArea As IArea = TryCast(elementEnvelope, IArea)
            rotateOrigin = elementArea.Centroid
        End If

        AddPropertySet(elem)

        Dim prop As IElementProperties = CType(elem, IElementProperties) 'record the old properties
        Dim propSet As IPropertySet
        propSet = CType(prop.CustomProperty, IPropertySet)
        Dim old_angle As Double
        old_angle = CDbl(propSet.GetProperty("Angle"))

        propSet.SetProperty("Angle", new_angle) 'update old angle

        transform2D.Rotate(rotateOrigin, new_angle - old_angle)
    End Sub

    Private Sub TracePath(ByVal elem As IElement, ByVal new_pos As IPoint, ByVal pContainer As IAGAnimationContainer, ByVal pTrack As IAGAnimationTrack, ByVal pKeyframe As IAGKeyframe)
        Dim trackExtensions As IAGAnimationTrackExtensions = CType(pTrack, IAGAnimationTrackExtensions)
        Dim graphicTrackExtension As IMapGraphicTrackExtension
        If trackExtensions.ExtensionCount = 0 Then 'if there is no extension, add one
            graphicTrackExtension = New MapGraphicTrackExtension()
            trackExtensions.AddExtension(graphicTrackExtension)
        Else
            graphicTrackExtension = CType(trackExtensions.Extension(0), IMapGraphicTrackExtension)
        End If

        Dim path As ILineElement = graphicTrackExtension.TraceElement

        Dim showTrace As Boolean = graphicTrackExtension.ShowTrace
        If (Not showTrace) Then
            If CheckGraphicExistance(CType(path, IElement), pContainer) Then
                RemoveGraphicFromDisplay(CType(path, IElement), pContainer)
            End If
            Return
        End If

        'Add the path to the graphic container
        If (Not CheckGraphicExistance(CType(path, IElement), pContainer)) Then
            AddGraphicToDisplay(CType(path, IElement), pContainer)
        End If

        RecreateLineGeometry(CType(path, IElement), pTrack, pKeyframe, new_pos)
    End Sub

    Private Sub AddGraphicToDisplay(ByVal elem As IElement, ByVal animContainer As IAGAnimationContainer)
        Dim view As IActiveView = TryCast(animContainer.CurrentView, IActiveView)
        Dim graphicsContainer As IGraphicsContainer = TryCast(view, IGraphicsContainer)
        graphicsContainer.AddElement(elem, 0)
        elem.Activate(view.ScreenDisplay)
    End Sub

    Private Sub RemoveGraphicFromDisplay(ByVal elem As IElement, ByVal animContainer As IAGAnimationContainer)
        Dim view As IActiveView = TryCast(animContainer.CurrentView, IActiveView)
        Dim graphicsContainer As IGraphicsContainer = TryCast(view, IGraphicsContainer)
        graphicsContainer.DeleteElement(elem)
    End Sub

    Private Function CheckGraphicExistance(ByVal elem As IElement, ByVal animContainer As IAGAnimationContainer) As Boolean
        Dim view As IActiveView = TryCast(animContainer.CurrentView, IActiveView)
        Dim graphicsContainer As IGraphicsContainer = TryCast(view, IGraphicsContainer)
        graphicsContainer.Reset()

        Dim exists As Boolean = False
        Dim temp As IElement = graphicsContainer.Next()
        Do While Not temp Is Nothing
            If temp Is elem Then
                exists = True
                Exit Do
            End If
            temp = graphicsContainer.Next()
        Loop
        Return exists
    End Function

    Private Sub MoveElement(ByVal elem As IElement, ByVal new_pos As IPoint, ByVal pContainer As IAGAnimationContainer)
        Dim transform2D As ITransform2D = TryCast(elem, ITransform2D)
        Dim origin As IPoint

        If elem.Geometry.GeometryType = esriGeometryType.esriGeometryPoint Then
            origin = TryCast(elem.Geometry, IPoint)
        Else
            Dim elementEnvelope As IEnvelope = elem.Geometry.Envelope
            Dim elementArea As IArea = TryCast(elementEnvelope, IArea)
            origin = elementArea.Centroid
        End If

        AddPropertySet(elem)

        Dim prop As IElementProperties = CType(elem, IElementProperties) 'record the old properties
        Dim propSet As IPropertySet
        propSet = CType(prop.CustomProperty, IPropertySet)
        Dim oldEnv As IEnvelope = GetElementBound(elem, pContainer)
        propSet.SetProperty("Envelope", oldEnv)

        transform2D.Move(new_pos.X - origin.X, new_pos.Y - origin.Y)
    End Sub

    Private Sub RecreateLineGeometry(ByVal elem As IElement, ByVal pTrack As IAGAnimationTrack, ByVal pKeyframe As IAGKeyframe, ByVal new_pos As IPoint)
        Dim newGeometry As IGeometry = New PolylineClass()
        Dim newPointCol As IPointCollection = CType(newGeometry, IPointCollection)

        Dim trackKeyframes As IAGAnimationTrackKeyframes = CType(pTrack, IAGAnimationTrackKeyframes)
        Dim tCount As Integer = trackKeyframes.KeyframeCount
        Dim missing As Object = Type.Missing
        Dim i As Integer = 0
        Do While i < tCount
            Dim tempKeyframe As IAGKeyframe = trackKeyframes.Keyframe(i)
            newPointCol.AddPoint(CType(tempKeyframe.PropertyValue(0), IPoint), missing, missing)
            If CType(tempKeyframe.PropertyValue(0), IPoint) Is CType(pKeyframe.PropertyValue(0), IPoint) Then
                Exit Do
            End If
            i += 1
        Loop
        If Not new_pos Is Nothing Then
            newPointCol.AddPoint(new_pos, missing, missing)
        End If

        elem.Geometry = newGeometry
    End Sub

    Private Function GetElementBound(ByVal elem As IElement, ByVal pContainer As IAGAnimationContainer) As IEnvelope
        Dim view As IActiveView = TryCast(pContainer.CurrentView, IActiveView)
        Dim graphicsContainerSelect As IGraphicsContainerSelect = TryCast(view, IGraphicsContainerSelect)
        Dim disp As IDisplay = TryCast(view.ScreenDisplay, IDisplay)

        Dim elementEnvelope As IEnvelope = New EnvelopeClass()
        elem.QueryBounds(disp, elementEnvelope)

        If graphicsContainerSelect.ElementSelected(elem) Then
            elementEnvelope = elem.SelectionTracker.Bounds(disp)
        End If

        Return elementEnvelope
    End Function

    Private Sub UpdateGraphicObject(ByVal elem As IElement, ByVal pContainer As IAGAnimationContainer, ByVal pTrack As IAGAnimationTrack, ByVal new_pos As IPoint, ByVal new_angle As Double)
        If elem Is Nothing OrElse new_pos Is Nothing Then
            Return 'invalidate parameter
        End If

        MoveElement(elem, new_pos, pContainer)
        RotateElement(elem, new_angle, pContainer)

        Return
    End Sub

    Private Sub AddPropertySet(ByVal elem As IElement)
        If elem Is Nothing Then
            Return 'invalidate parameter
        End If

        Dim prop As IElementProperties = CType(elem, IElementProperties) 'record the old properties
        Dim propSet As IPropertySet
        If prop.CustomProperty Is Nothing Then
            propSet = New PropertySetClass()
            propSet.SetProperty("Envelope", Nothing)
            propSet.SetProperty("Angle", 0.0)
            prop.CustomProperty = propSet
        End If
    End Sub

    Private Sub RefreshGraphicObject(ByVal elem As IElement, ByVal pContainer As IAGAnimationContainer)
        Dim view As IActiveView = TryCast(pContainer.CurrentView, IActiveView)
        Dim elemEnv As IEnvelope = GetElementBound(elem, pContainer)

        Dim oldEnv As IEnvelope = New EnvelopeClass()
        Dim elemProps2 As IElementProperties2 = CType(elem, IElementProperties2)
        oldEnv = CType(elemProps2.CustomProperty, IEnvelope)

        Dim animRefresh As IViewRefresh = CType(view, IViewRefresh)

        If Not oldEnv Is Nothing Then
            elemEnv.Union(oldEnv)
        End If

        animRefresh.AnimationRefresh(esriViewDrawPhase.esriViewGraphics, elem, elemEnv)
    End Sub
#End Region
End Class

