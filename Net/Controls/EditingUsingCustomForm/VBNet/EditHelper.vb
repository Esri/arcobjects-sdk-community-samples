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
Imports System
Imports System.Collections.Generic
Imports System.Text

    Public Class EditHelper
        Private Shared instance As EditHelper
        Private m_mainform As MainForm = Nothing
        Private m_editorFormOpen As Boolean

    'private constructor - external classes cannot create a 'new' EditHelper instance
    Private Sub New()
    End Sub

        Public Shared Property TheMainForm() As MainForm
            Get
                If Not instance Is Nothing Then
                    Return instance.m_mainform
                Else
                    Return Nothing
                End If
            End Get
            Set(ByVal Value As MainForm)
                If instance Is Nothing Then
                    instance = New EditHelper()
                End If

                instance.m_mainform = Value

            End Set
        End Property

        Public Shared Property IsEditorFormOpen() As Boolean
            Get
                If Not instance Is Nothing Then
                    Return instance.m_editorFormOpen
                Else
                    Return False
                End If
            End Get
            Set(ByVal Value As Boolean)
                If instance Is Nothing Then
                    instance = New EditHelper()
                End If

                instance.m_editorFormOpen = Value

            End Set
        End Property




    End Class


