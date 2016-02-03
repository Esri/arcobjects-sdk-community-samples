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


