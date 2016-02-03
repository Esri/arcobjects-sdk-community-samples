' Copyright 2011 ESRI
' 
' All rights reserved under the copyright laws of the United States
' and applicable international laws, treaties, and conventions.
' 
' You may freely redistribute and use this sample code, with or
' without modification, provided you include the original copyright
' notice and use restrictions.
' 
' See the use restrictions at <your ArcGIS install location>/DeveloperKit10.2/userestrictions.txt.
' 

Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Schematic
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.SchematicControls
Imports ESRI.ArcGIS.SchematicUI

Public Class Autorefresh
	Inherits ESRI.ArcGIS.Desktop.AddIns.Button

  Private m_application As ESRI.ArcGIS.Framework.IApplication
	Private m_schematicTarget As ISchematicTarget
	Private m_schematicInMemoryDiagram As ISchematicInMemoryDiagram
	Protected m_formAutorefresh As FormAutorefresh

	Public Sub New()
		m_application = My.ArcMap.Application
	End Sub

	Protected Overrides Sub OnClick()
		If m_schematicTarget IsNot Nothing AndAlso m_schematicTarget.SchematicTarget IsNot Nothing AndAlso m_schematicTarget.SchematicTarget.IsEditingSchematicDiagram() Then
			If m_schematicInMemoryDiagram Is Nothing Then
				m_schematicInMemoryDiagram = m_schematicTarget.SchematicTarget.SchematicInMemoryDiagram
			End If

			If m_schematicInMemoryDiagram IsNot Nothing Then
				If (m_formAutorefresh Is Nothing) Then
					m_formAutorefresh = New FormAutorefresh()
					Try
						m_formAutorefresh.InitializeSecond()
						m_formAutorefresh.InitializeMinute()
					Catch e As Exception
						System.Windows.Forms.MessageBox.Show(e.Message)
					End Try
				End If
			End If

			m_formAutorefresh.SetSchematicInmemoryDiagram(m_schematicInMemoryDiagram)
			m_formAutorefresh.Appli(m_application)
			m_formAutorefresh.Show()

		End If
	End Sub

	Protected Overrides Sub OnUpdate()

		If m_schematicTarget Is Nothing Then
			Dim extention As ESRI.ArcGIS.esriSystem.IExtension
			Dim extensionManager As ESRI.ArcGIS.esriSystem.IExtensionManager

			extention = Nothing
			extensionManager = m_application
			For i As Integer = 0 To extensionManager.ExtensionCount - 1
				extention = extensionManager.Extension(i)
                If extention.Name.ToLower() = "esri schematic extension" Then
                    Exit For
                End If
			Next

			If extention IsNot Nothing Then
				Dim schematicExtension As SchematicExtension
				schematicExtension = TryCast(extention, SchematicExtension)
				m_schematicTarget = TryCast(schematicExtension, ISchematicTarget)
			End If
		End If

		If m_schematicTarget IsNot Nothing Then
			Dim schematicLayer As ISchematicLayer
			schematicLayer = m_schematicTarget.SchematicTarget

			If schematicLayer Is Nothing Then
				Enabled = False
				If (m_formAutorefresh IsNot Nothing) Then m_formAutorefresh.SetAutoOff(True)
			ElseIf schematicLayer.IsEditingSchematicDiagram() Then
				Enabled = True
			Else
				Enabled = False
				If (m_formAutorefresh IsNot Nothing) Then m_formAutorefresh.SetAutoOff(True)
			End If
		Else
			Enabled = False
			If (m_formAutorefresh IsNot Nothing) Then m_formAutorefresh.SetAutoOff(True)
		End If

	End Sub
End Class
