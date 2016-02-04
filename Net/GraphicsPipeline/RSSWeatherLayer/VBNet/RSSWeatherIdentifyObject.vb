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
Imports System.Collections
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.DataSourcesFile
Imports ESRI.ArcGIS.Display

	''' <summary>
	''' Summary description for GlobeWeatherIdentifyObject.
	''' </summary>
	Public Class RSSWeatherIdentifyObject : Implements IIdentifyObj, IIdentifyObject, IDisposable
		Private m_weatherLayer As RSSWeatherLayerClass = Nothing
		Private m_propset As IPropertySet = Nothing
		Private m_identifyDlg As IdentifyDlg = Nothing

	#Region "Ctor"
	''' <summary>
		''' Class Ctor
		''' </summary>
	Public Sub New()
	End Sub
	#End Region

	#Region "IIdentifyObject Members"

	''' <summary>
	''' PropertySet of the identify object
	''' </summary>
	''' <remarks>The information passed by the layer to the identify dialog is encapsulated
	''' in a PropertySet</remarks>
	Public Property PropertySet() As IPropertySet Implements IIdentifyObject.PropertySet
			Get
				Return m_propset
			End Get
			Set
				m_propset = Value
			End Set
	End Property

	''' <summary>
	''' Name of the identify object.
	''' </summary>
		Public Property Name1() As String Implements IIdentifyObject.Name
			Get
				Return "WeatherInfo"
			End Get
			Set (ByVal value as String)
				' TODO:  Add GlobeWeatherIdentifyObject.Name setter implementation
			End Set
		End Property


		#End Region

		#Region "IIdentifyObj Members"

		''' <summary>
	''' Flashes the identified object on the screen.
		''' </summary>
		''' <param name="pDisplay"></param>
	Public Sub Flash(ByVal pDisplay As IScreenDisplay) Implements IIdentifyObj.Flash

	End Sub

		''' <summary>
	''' Indicates if the object can identify the specified layer
		''' </summary>
		''' <param name="pLayer"></param>
		''' <returns></returns>
	Public Function CanIdentify(ByVal pLayer As ILayer) As Boolean Implements IIdentifyObj.CanIdentify
			If Not(TypeOf pLayer Is RSSWeatherLayerClass) Then
				Return False
			End If

			'cache the layer
	  m_weatherLayer = CType(pLayer, RSSWeatherLayerClass)

			Return True

	End Function

	''' <summary>
	''' The window handle.
	''' </summary>
		Public ReadOnly Property hWnd() As Integer Implements IIdentifyObj.hWnd
			Get
				If Nothing Is m_identifyDlg OrElse m_identifyDlg.Handle.ToInt32() = 0 Then
					m_identifyDlg = New IdentifyDlg()
					m_identifyDlg.CreateControl()

					m_identifyDlg.SetProperties(m_propset)
				End If

				Return m_identifyDlg.Handle.ToInt32()
			End Get
		End Property

	''' <summary>
	''' Name of the identify object.
	''' </summary>
		Private ReadOnly Property Name() As String Implements ESRI.ArcGIS.Carto.IIdentifyObj.Name
			Get
				Return "WeatherInfo"
			End Get
		End Property

		''' <summary>
	''' Target layer for identification.
		''' </summary>
	Public ReadOnly Property Layer() As ILayer Implements IIdentifyObj.Layer
			Get
				Return m_weatherLayer
			End Get
	End Property


	''' <summary>
	''' Displays a context sensitive popup menu at the specified location.
	''' </summary>
	''' <param name="x"></param>
	''' <param name="y"></param>
		Public Sub PopUpMenu(ByVal x As Integer, ByVal y As Integer) Implements IIdentifyObj.PopUpMenu
		End Sub

		#End Region

		#Region "IDisposable Members"

	Public Sub Dispose() Implements IDisposable.Dispose
			If (Not m_identifyDlg.IsDisposed) Then
				m_identifyDlg.Dispose()
			End If

			m_weatherLayer = Nothing
			m_propset = Nothing

	End Sub

		#End Region

	End Class
