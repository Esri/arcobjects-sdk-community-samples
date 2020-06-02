'Copyright 2019 Esri

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
Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Animation
Imports ESRI.ArcGIS.ADF
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.SystemUI
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS.Framework

<Guid("64794603-561D-48A5-B0F1-286DC26ADFA6"), ClassInterface(ClassInterfaceType.None), ProgId("AnimationDeveloperSamples.cmdMoveGraphicAlongPath")> _
Public NotInheritable Class cmdMoveGraphicAlongPath : Inherits BaseCommand
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
        MxCommands.Register(regKey)

    End Sub
    ''' <summary>
    ''' Required method for ArcGIS Component Category unregistration -
    ''' Do not modify the contents of this method with the code editor.
    ''' </summary>
    Private Shared Sub ArcGISCategoryUnregistration(ByVal registerType As Type)
        Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
        MxCommands.Unregister(regKey)

    End Sub

#End Region
#End Region

    Private m_hookHelper As IHookHelper = Nothing
    Private animExt As IAnimationExtension

    Public Sub New()
        '
        ' TODO: Define values for the public properties
        '
        MyBase.m_category = "Animation Developer Samples" 'localizable text
        MyBase.m_caption = "Move Graphic along Path..." 'localizable text
        MyBase.m_message = "Move graphic along a selected line graphic or line feature" 'localizable text
        MyBase.m_toolTip = "Move graphic along path" 'localizable text
        MyBase.m_name = "AnimationDeveloperSamples_cmdMoveGraphicAlongPath" 'unique id, non-localizable (e.g. "MyCategory_MyCommand")

        Dim res As String() = Me.GetType().Assembly.GetManifestResourceNames()
        If res.GetLength(0) > 0 Then
            MyBase.m_bitmap = New Bitmap(Me.GetType().Assembly.GetManifestResourceStream(Me.GetType(), "cmdMoveGraphicAlongPath.bmp"))
        End If

        'Dim bitmapResourceName As String = Me.GetType().Name & ".bmp"
        'MyBase.m_bitmap = New Bitmap(Me.GetType(), bitmapResourceName)
    End Sub

#Region "Overridden Class Methods"

    ''' <summary>
    ''' Occurs when this command is created
    ''' </summary>
    ''' <param name="hook">Instance of the application</param>
    Public Overrides Sub OnCreate(ByVal hook As Object)
        If hook Is Nothing Then
            Return
        End If

        Try
            m_hookHelper = New HookHelperClass()
            m_hookHelper.Hook = hook
            If m_hookHelper.ActiveView Is Nothing Then
                m_hookHelper = Nothing
            End If
        Catch
            m_hookHelper = Nothing
        End Try

        If m_hookHelper Is Nothing Then
            MyBase.m_enabled = False
        Else
            MyBase.m_enabled = True
        End If

        If TypeOf hook Is IApplication Then
            Dim app As IApplication = CType(hook, IApplication)
            Dim pUID As UID = New UIDClass()
            pUID.Value = "esriAnimation.AnimationExtension"
            animExt = CType(app.FindExtensionByCLSID(pUID), IAnimationExtension)
        End If
    End Sub

    Public Overrides Sub OnClick()
        Dim optionsForm As frmCreateGraphicTrackOptions = New frmCreateGraphicTrackOptions()
        Dim selectedPath As IGeometry = GetSelectedLineFeature()
        Dim lineElement As IElement = CType(GetSelectedLineElement(), IElement)
        Dim selectedElement As IElement = GetSelectedPointElement()
        optionsForm.lineFeature = selectedPath
        optionsForm.lineGraphic = CType(lineElement, ILineElement)
        optionsForm.pointGraphic = selectedElement
        optionsForm.AnimationExtension = animExt
        optionsForm.RefreshPathSourceOptions()
        optionsForm.ShowDialog()
    End Sub

    Public Overrides ReadOnly Property Enabled() As Boolean
        Get
            If Not m_hookHelper Is Nothing Then
                Dim selectedPath As IGeometry = GetSelectedLineFeature()
                Dim lineElement As IElement = CType(GetSelectedLineElement(), IElement)
                Dim selectedElement As IElement = GetSelectedPointElement()

                If Not selectedElement Is Nothing AndAlso (Not lineElement Is Nothing OrElse Not selectedPath Is Nothing) Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If
        End Get
    End Property

#End Region

#Region "private methods"
    Private Function GetSelectedLineFeature() As IGeometry
        Dim pMap As IMap = m_hookHelper.FocusMap
        Dim enumFeature As IEnumFeature = CType(pMap.FeatureSelection, IEnumFeature)
        Dim selectedPath As IGeometry = Nothing

        Dim pFeat As IFeature = enumFeature.Next()
        Do While Not pFeat Is Nothing
            If pFeat.Shape.GeometryType = esriGeometryType.esriGeometryPolyline Then
                selectedPath = CType(pFeat.Shape, IGeometry)
                Exit Do
            End If
            pFeat = enumFeature.Next()
        Loop
        Return selectedPath
    End Function

    Private Function GetSelectedPointElement() As IElement
        Dim activeFrame As IMap = m_hookHelper.FocusMap
        Dim graphicsSel As IGraphicsContainerSelect = TryCast(activeFrame, IGraphicsContainerSelect)
        Dim selectedElement As IElement = Nothing
        Dim graphicAnimationType As IAGAnimationType = New AnimationTypeMapGraphic()
        If graphicsSel.ElementSelectionCount > 0 Then
            Dim enumElem As IEnumElement = graphicsSel.SelectedElements
            selectedElement = enumElem.Next()
            Do While Not selectedElement Is Nothing
                If graphicAnimationType.AppliesToObject(selectedElement) Then
                    Exit Do
                End If
                selectedElement = enumElem.Next()
            Loop
        Else
            selectedElement = Nothing
        End If
        Return selectedElement
    End Function

    Private Function GetSelectedLineElement() As ILineElement
        Dim activeFrame As IMap = m_hookHelper.FocusMap
        Dim graphicsSel As IGraphicsContainerSelect = TryCast(activeFrame, IGraphicsContainerSelect)
        Dim selectedElement As IElement = Nothing
        Dim lineElement As ILineElement
        If graphicsSel.ElementSelectionCount > 0 Then
            Dim enumElem As IEnumElement = graphicsSel.SelectedElements
            selectedElement = enumElem.Next()
            Do While Not selectedElement Is Nothing
                If TypeOf selectedElement Is ILineElement Then
                    Exit Do
                End If
                selectedElement = enumElem.Next()
            Loop
        End If
        lineElement = CType(selectedElement, ILineElement)
        Return lineElement
    End Function
#End Region
End Class
