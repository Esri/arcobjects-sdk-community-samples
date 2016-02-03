Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Windows.Forms
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Geoprocessing

Namespace GeoprocessorEventHelper
    'declare the event argument classes for the different GP events
    Public NotInheritable Class GPMessageEventArgs : Inherits EventArgs
        Private m_message As String = String.Empty
        Private m_messageType As esriGPMessageType = esriGPMessageType.esriGPMessageTypeEmpty
        Private m_errorCode As Integer = -1

#Region "class constructors"
        Public Sub New()
            MyBase.New()

        End Sub

        Public Sub New(ByVal msg As String, ByVal msgType As esriGPMessageType, ByVal errCode As Integer)
            Me.New()
            m_message = msg
            m_messageType = msgType
            m_errorCode = errCode
        End Sub
#End Region

#Region "properties"
        Public Property Message() As String
            Get
                Return m_message
            End Get
            Set(ByVal value As String)
                m_message = value
            End Set
        End Property
        Public Property MessageType() As esriGPMessageType
            Get
                Return m_messageType
            End Get
            Set(ByVal value As esriGPMessageType)
                m_messageType = value
            End Set
        End Property
        Public Property ErrorCode() As Integer
            Get
                Return m_errorCode
            End Get
            Set(ByVal value As Integer)
                m_errorCode = value
            End Set
        End Property
#End Region
    End Class
    Public NotInheritable Class GPPostToolExecuteEventArgs : Inherits EventArgs
#Region "class members"
        Private m_messages As GPMessageEventArgs()
        Private m_result As Integer = 0
        Private m_displayName As String = String.Empty
        Private m_name As String = String.Empty
        Private m_pathName As String = String.Empty
        Private m_toolbox As String = String.Empty
        Private m_toolCategory As String = String.Empty
        Private m_toolType As esriGPToolType = esriGPToolType.esriGPCustomTool
        Private m_description As String = String.Empty
#End Region

#Region "class constructor"
        Public Sub New()
            MyBase.New()

        End Sub
#End Region

#Region "properties"
        Public Property Messages() As GPMessageEventArgs()
            Get
                Return m_messages
            End Get
            Set(ByVal value As GPMessageEventArgs())
                m_messages = value
            End Set
        End Property
        Public Property Result() As Integer
            Get
                Return m_result
            End Get
            Set(ByVal value As Integer)
                m_result = value
            End Set
        End Property
        Public Property DisplayName() As String
            Get
                Return m_displayName
            End Get
            Set(ByVal value As String)
                m_displayName = value
            End Set
        End Property
        Public Property Name() As String
            Get
                Return m_name
            End Get
            Set(ByVal value As String)
                m_name = value
            End Set
        End Property
        Public Property Toolbox() As String
            Get
                Return m_toolbox
            End Get
            Set(ByVal value As String)
                m_toolbox = value
            End Set
        End Property
        Public Property ToolCategory() As String
            Get
                Return m_toolCategory
            End Get
            Set(ByVal value As String)
                m_toolCategory = value
            End Set
        End Property
        Public Property ToolType() As esriGPToolType
            Get
                Return m_toolType
            End Get
            Set(ByVal value As esriGPToolType)
                m_toolType = value
            End Set
        End Property
        Public Property Description() As String
            Get
                Return m_description
            End Get
            Set(ByVal value As String)
                m_description = value
            End Set
        End Property
        Public Property PathName() As String
            Get
                Return m_pathName
            End Get
            Set(ByVal value As String)
                m_pathName = value
            End Set
        End Property
#End Region
    End Class
    Public NotInheritable Class GPPreToolExecuteEventArgs : Inherits EventArgs
#Region "class members"
        Private m_processID As Integer = 0
        Private m_displayName As String = String.Empty
        Private m_name As String = String.Empty
        Private m_pathName As String = String.Empty
        Private m_toolbox As String = String.Empty
        Private m_toolCategory As String = String.Empty
        Private m_toolType As esriGPToolType = esriGPToolType.esriGPCustomTool
        Private m_description As String = String.Empty
#End Region

#Region "class constructor"
        Public Sub New()
            MyBase.New()

        End Sub
#End Region

#Region "properties"
        Public Property ProcessID() As Integer
            Get
                Return m_processID
            End Get
            Set(ByVal value As Integer)
                m_processID = value
            End Set
        End Property
        Public Property DisplayName() As String
            Get
                Return m_displayName
            End Get
            Set(ByVal value As String)
                m_displayName = value
            End Set
        End Property
        Public Property Name() As String
            Get
                Return m_name
            End Get
            Set(ByVal value As String)
                m_name = value
            End Set
        End Property
        Public Property Toolbox() As String
            Get
                Return m_toolbox
            End Get
            Set(ByVal value As String)
                m_toolbox = value
            End Set
        End Property
        Public Property ToolCategory() As String
            Get
                Return m_toolCategory
            End Get
            Set(ByVal value As String)
                m_toolCategory = value
            End Set
        End Property
        Public Property ToolType() As esriGPToolType
            Get
                Return m_toolType
            End Get
            Set(ByVal value As esriGPToolType)
                m_toolType = value
            End Set
        End Property
        Public Property Description() As String
            Get
                Return m_description
            End Get
            Set(ByVal value As String)
                m_description = value
            End Set
        End Property
        Public Property PathName() As String
            Get
                Return m_pathName
            End Get
            Set(ByVal value As String)
                m_pathName = value
            End Set
        End Property
#End Region
    End Class

    ' A delegate type for hooking up change notifications.
    Public Delegate Sub MessageEventHandler(ByVal sender As Object, ByVal e As GPMessageEventArgs)
    Public Delegate Sub ToolboxChangedEventHandler(ByVal sender As Object, ByVal e As EventArgs)
    Public Delegate Sub PostToolExecuteEventHandler(ByVal sender As Object, ByVal e As GPPostToolExecuteEventArgs)
    Public Delegate Sub PreToolExecuteEventHandler(ByVal sender As Object, ByVal e As GPPreToolExecuteEventArgs)

    ''' <summary>
    '''A class that sends event notifications whenever the Messages are added.
    ''' </summary>
    <Guid("0CC39861-B4FE-45ea-8919-8295AF25F311"), ProgId("GeoprocessorEventHelper.GPMessageEventHandler"), ComVisible(True), Serializable()> _
    Public Class GPMessageEventHandler : Implements IGeoProcessorEvents
        ' An event that clients can use to be notified whenever a GP message is posted.
        Public Event GPMessage As MessageEventHandler
        'an event notifying that a toolbox has changed
        Public Event GPToolboxChanged As ToolboxChangedEventHandler
        'an event which gets fired right after a tool finish execute
        Public Event GPPostToolExecute As PostToolExecuteEventHandler
        'an event which gets fired before a tool gets executed
        Public Event GPPreToolExecute As PreToolExecuteEventHandler

#Region "IGeoProcessorEvents Members"

        ''' <summary>
        ''' Called when a message has been posted while executing a SchematicGeoProcessing
        ''' </summary>
        ''' <param name="message"></param>
        Private Sub OnMessageAdded(ByVal message As IGPMessage) Implements IGeoProcessorEvents.OnMessageAdded
            'fire the GPMessage event
            If Not GPMessageEvent Is Nothing Then
                RaiseEvent GPMessage(Me, New GPMessageEventArgs(message.Description, message.Type, message.ErrorCode))
            End If
        End Sub

        ''' <summary>
        ''' Called immediately after a tool is executed by the GeoProcessor.
        ''' </summary>
        ''' <param name="Tool"></param>
        ''' <param name="Values"></param>
        ''' <param name="result"></param>
        ''' <param name="Messages"></param>
        Private Sub PostToolExecute(ByVal Tool As IGPTool, ByVal Values As IArray, ByVal result As Integer, ByVal Messages As IGPMessages) Implements IGeoProcessorEvents.PostToolExecute
            Dim msg As GPMessageEventArgs() = New GPMessageEventArgs(Messages.Count - 1) {}
            Dim GPMessageEvent As IGPMessage = Nothing
            Dim i As Integer = 0
            Do While i < Messages.Count
                GPMessageEvent = Messages.GetMessage(i)
                Dim message As GPMessageEventArgs = New GPMessageEventArgs(GPMessageEvent.Description, GPMessageEvent.Type, GPMessageEvent.ErrorCode)
                msg(i) = message
                i += 1
            Loop

            'create a new instance of GPPostToolExecuteEventArgs
            Dim e As GPPostToolExecuteEventArgs = New GPPostToolExecuteEventArgs()
            e.DisplayName = Tool.DisplayName
            e.Name = Tool.Name
            e.PathName = Tool.PathName
            e.Toolbox = Tool.Toolbox.Alias
            e.ToolCategory = Tool.ToolCategory
            e.ToolType = Tool.ToolType
            e.Description = Tool.Description
            e.Result = result

            'fire the Post tool event
            If Not Nothing Is GPPostToolExecuteEvent Then
                RaiseEvent GPPostToolExecute(Me, e)
            End If
        End Sub

        ''' <summary>
        ''' Called immediately prior to the GeoProcessor executing a tool.
        ''' </summary>
        ''' <param name="Tool"></param>
        ''' <param name="Values"></param>
        ''' <param name="processID"></param>
        Private Sub PreToolExecute(ByVal Tool As IGPTool, ByVal Values As IArray, ByVal processID As Integer) Implements IGeoProcessorEvents.PreToolExecute
            'create a new instance of GPPreToolExecuteEventArgs
            Dim e As GPPreToolExecuteEventArgs = New GPPreToolExecuteEventArgs()
            e.DisplayName = Tool.DisplayName
            e.Name = Tool.Name
            e.PathName = Tool.PathName
            e.Toolbox = Tool.Toolbox.Alias
            e.ToolCategory = Tool.ToolCategory
            e.ToolType = Tool.ToolType
            e.Description = Tool.Description
            e.ProcessID = processID

            'fire the PreTool event
            If Not Nothing Is GPPreToolExecuteEvent Then
                RaiseEvent GPPreToolExecute(Me, e)
            End If

        End Sub

        ''' <summary>
        ''' Called when a toolbox is added or removed from the GeoProcessor.
        ''' </summary>
        Private Sub ToolboxChange() Implements IGeoProcessorEvents.ToolboxChange
            If Not GPToolboxChangedEvent Is Nothing Then
                RaiseEvent GPToolboxChanged(Me, New EventArgs())
            End If
        End Sub

#End Region
    End Class
End Namespace