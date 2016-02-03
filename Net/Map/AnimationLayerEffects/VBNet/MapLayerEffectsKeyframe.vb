Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.Animation
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.SystemUI
Imports ESRI.ArcGIS.esriSystem

Namespace AnimationDeveloperSamples
    <Guid("EB5D227B-4814-4720-877B-D19519B2BBD6"), ClassInterface(ClassInterfaceType.None), ProgId("AnimationDeveloperSamples.MapLayerEffectsKeyframe")> _
 Public Class MapLayerEffectsKeyframe : Implements IAGKeyframe, IAGKeyframeUI
        Private activeProps As ILongArray
        Private animType As IAGAnimationType
        Private keyframeName As String
        Private bObjectsNeedRefresh As Boolean
        Private brightness As Short
        Private contrast As Short
        Private timeStamp1 As Double

#Region "constructor"
        Public Sub New()
            activeProps = New LongArrayClass()
            activeProps.Add(0)
            activeProps.Add(1)
            animType = New AnimationTypeLayerEffects()
            keyframeName = ""
            bObjectsNeedRefresh = False
            brightness = 0
            contrast = 0
            timeStamp1 = 0
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
            SetBrightness(CType(pObject, ILayer), brightness)
            SetContrast(CType(pObject, ILayer), contrast)
            pContainer.RefreshObject(pObject)
            Return
        End Sub

        Public Sub CaptureProperties(ByVal pContainer As IAGAnimationContainer, ByVal pObject As Object) Implements IAGKeyframe.CaptureProperties
            contrast = GetContrast(CType(pObject, ILayer))
            brightness = GetBrightness(CType(pObject, ILayer))
        End Sub

        Public Sub Interpolate(ByVal pTrack As IAGAnimationTrack, ByVal pContainer As IAGAnimationContainer, ByVal pObject As Object, ByVal propertyIndex As Integer, ByVal time As Double, ByVal pNextKeyframe As IAGKeyframe, ByVal pPrevKeyframe As IAGKeyframe, ByVal pAfterNextKeyframe As IAGKeyframe) Implements IAGKeyframe.Interpolate
            If time < TimeStamp OrElse time > pNextKeyframe.TimeStamp Then
                Return
            End If

            Dim timeFactor As Double
            timeFactor = (time - TimeStamp) / (pNextKeyframe.TimeStamp - TimeStamp) 'ignoring pPrevKeyframe and pAfterNextKeyframe
            If propertyIndex = 0 Then 'interpolate brightness
                Dim brightnessInterpolated As Short
                Dim brightnessStart As Short
                Dim brightnessEnd As Short
                brightnessStart = brightness
                brightnessEnd = System.Convert.ToInt16(pNextKeyframe.PropertyValue(0))
                brightnessInterpolated = System.Convert.ToInt16(timeFactor * (brightnessEnd - brightnessStart) + brightnessStart)
                SetBrightness(CType(pObject, ILayer), brightnessInterpolated)
                bObjectsNeedRefresh = True
            Else 'interpolate contrast
                Dim contrastInterpolated As Short
                Dim contrastStart As Short
                Dim contrastEnd As Short
                contrastStart = contrast
                contrastEnd = System.Convert.ToInt16(pNextKeyframe.PropertyValue(1))
                contrastInterpolated = System.Convert.ToInt16(timeFactor * (contrastEnd - contrastStart) + contrastStart)
                SetContrast(CType(pObject, ILayer), contrastInterpolated)
                bObjectsNeedRefresh = True
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
                Return keyframeName
            End Get

            Set(ByVal value As String)
                keyframeName = value
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
                    Return brightness
                ElseIf propIndex = 1 Then
                    Return contrast
                Else
                    Return Nothing
                End If
            End Get
            Set(ByVal value As Object)
                If propIndex = 0 Then
                    brightness = Convert.ToInt16(value)
                ElseIf propIndex = 1 Then
                    contrast = Convert.ToInt16(value)
                Else
                    Return
                End If
            End Set
        End Property

        Public Sub RefreshObject(ByVal pTrack As IAGAnimationTrack, ByVal pContainer As IAGAnimationContainer, ByVal pObject As Object) Implements IAGKeyframe.RefreshObject
            bObjectsNeedRefresh = False
            pContainer.RefreshObject(pObject)
        End Sub

        Public Property TimeStamp() As Double Implements IAGKeyframe.TimeStamp
            Get
                Return timeStamp1
            End Get

            Set(ByVal value As Double)
                timeStamp1 = value
            End Set
        End Property
#End Region

#Region "IAGKeyframeUI members"
        Public Function GetText(ByVal propIndex As Integer, ByVal columnIndex As Integer) As String Implements IAGKeyframeUI.GetText
            Dim text As String
            text = Nothing
            Select Case propIndex
                Case 0
                    text = System.Convert.ToString(brightness)
                Case 1
                    text = System.Convert.ToString(contrast)
            End Select
            Return text
        End Function

        Public Sub SetText(ByVal propIndex As Integer, ByVal columnIndex As Integer, ByVal text As String) Implements IAGKeyframeUI.SetText
            Select Case propIndex
                Case 0
                    brightness = System.Convert.ToInt16(text)
                Case 1
                    contrast = System.Convert.ToInt16(text)
            End Select

            Return
        End Sub
#End Region

#Region "private methods"
        Private Function GetBrightness(ByVal layer As ILayer) As Short
            Dim layerEffects As ILayerEffects = CType(layer, ILayerEffects)
            Return layerEffects.Brightness
        End Function

        Private Sub SetBrightness(ByVal layer As ILayer, ByVal brtness As Short)
            Dim layerEffects As ILayerEffects = CType(layer, ILayerEffects)
            layerEffects.Brightness = brtness
            Return
        End Sub

        Private Function GetContrast(ByVal layer As ILayer) As Short
            Dim layerEffects As ILayerEffects = CType(layer, ILayerEffects)
            Return layerEffects.Contrast
        End Function

        Private Sub SetContrast(ByVal layer As ILayer, ByVal ctr As Short)
            Dim layerEffects As ILayerEffects = CType(layer, ILayerEffects)
            layerEffects.Contrast = ctr
            Return
        End Sub
#End Region
    End Class
End Namespace
