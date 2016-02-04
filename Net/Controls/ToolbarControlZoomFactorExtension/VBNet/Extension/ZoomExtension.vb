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
Option Explicit On 

Imports ESRI.ArcGIS.esriSystem

Public Class ZoomExtension
    Implements IExtension
    Implements IExtensionConfig
    Implements IZoomExtension

    Dim m_zoomFactor As Double
    Dim m_extensionState As esriExtensionState

    Private ReadOnly Property IExtension_Name() As String Implements ESRI.ArcGIS.esriSystem.IExtension.Name
        Get
            Return "Zoom Factor Extension"
        End Get
    End Property

    Private Sub IExtension_Startup(ByRef initializationData As Object) Implements ESRI.ArcGIS.esriSystem.IExtension.Startup
        'Default zoom factor
        m_zoomFactor = 2
        'Default extension state is disabled
        m_extensionState = esriExtensionState.esriESDisabled
    End Sub

    Private Sub IExtension_Shutdown() Implements ESRI.ArcGIS.esriSystem.IExtension.Shutdown
        'Not implemented
    End Sub

    Private ReadOnly Property IExtensionConfig_Description() As String Implements ESRI.ArcGIS.esriSystem.IExtensionConfig.Description
        Get
            Return "Variable ZoomExtension Sample"
        End Get
    End Property

    Private ReadOnly Property IExtensionConfig_ProductName() As String Implements ESRI.ArcGIS.esriSystem.IExtensionConfig.ProductName
        Get
            Return "ZoomExtension Sample"
        End Get
    End Property

    Private Property IExtensionConfig_State() As ESRI.ArcGIS.esriSystem.esriExtensionState Implements ESRI.ArcGIS.esriSystem.IExtensionConfig.State
        Get
            Return m_extensionState
        End Get
        Set(ByVal Value As esriExtensionState)
            m_extensionState = Value
        End Set
    End Property

    Private Property IZoomExtension_ZoomFactor() As Double Implements IZoomExtension.ZoomFactor
        Get
            Return m_zoomFactor
        End Get
        Set(ByVal Value As Double)
            m_zoomFactor = Value
        End Set
    End Property


End Class