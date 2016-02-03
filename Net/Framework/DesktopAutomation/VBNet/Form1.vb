Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.DataSourcesFile
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.esriSystem

Public Class Form1
    Private m_application As IApplication

    'Application removed event
    Private m_appHWnd As Integer = 0
    Private m_appROTEvent As IAppROTEvents_Event

    'Retrieve the hWnd of the active popup/modal dialog of an owner window
    Declare Auto Function GetLastActivePopup Lib "user32" (ByVal hwndOwnder As Integer) As Integer

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    'Bind to Engine
    ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.EngineOrDesktop)
    'Preselect option
        cboApps.SelectedIndex = 0
    End Sub

    Private Sub Form1_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        'Clean up
        m_application = Nothing
        m_appROTEvent = Nothing
    End Sub

    Private Sub btnStartApp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStartApp.Click
        Dim doc As IDocument = Nothing
        Try
            Me.Cursor = Cursors.WaitCursor
            Select Case cboApps.SelectedItem.ToString()
                Case "ArcMap"
                    doc = New ESRI.ArcGIS.ArcMapUI.MxDocumentClass()
                Case "ArcScene"
                    doc = New ESRI.ArcGIS.ArcScene.SxDocumentClass()
                Case "ArcGlobe"
                    doc = New ESRI.ArcGIS.ArcGlobe.GMxDocumentClass()
            End Select
        Catch
        Finally
            Me.Cursor = Cursors.Default
        End Try

        If doc IsNot Nothing Then
            'Advanced (AppROT event): Handle manual shutdown, comment out if not needed
            m_appROTEvent = New AppROTClass()
            AddHandler m_appROTEvent.AppRemoved, AddressOf m_appROTEvent_AppRemoved

            'Get a reference of the application and make it visible
            m_application = doc.Parent
            m_application.Visible = True
            m_appHWnd = m_application.hWnd

            'Enable/disable controls accordingly
            txtShapeFilePath.Enabled = True
            btnShutdown.Enabled = True
            btnDrive.Enabled = ShouldEnableAddLayer
            cboApps.Enabled = False
            btnStartApp.Enabled = False
        Else
            m_appROTEvent = Nothing
            m_application = Nothing

            txtShapeFilePath.Enabled = False
            btnShutdown.Enabled = False
            btnDrive.Enabled = False
            cboApps.Enabled = True
            btnStartApp.Enabled = True
        End If
    End Sub

    Private Sub txtShapeFilePath_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtShapeFilePath.TextChanged
        btnDrive.Enabled = ShouldEnableAddLayer
    End Sub

    Private ReadOnly Property ShouldEnableAddLayer()
        Get
            If System.IO.File.Exists(txtShapeFilePath.Text) Then
                Return System.IO.Path.GetExtension(txtShapeFilePath.Text).ToLower() = ".shp"
            Else
                Return False
            End If
        End Get
    End Property

    Private Sub btnDrive_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDrive.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            Dim objFactory As IObjectFactory = DirectCast(m_application, IObjectFactory)

            'Use reflection to get ClsID of ShapefileWorkspaceFactory
            Dim shpWkspFactType As Type = GetType(ShapefileWorkspaceFactoryClass)
            Dim typeClsID As String = shpWkspFactType.GUID.ToString("B")

            Dim shapeFile As String = System.IO.Path.GetFileNameWithoutExtension(txtShapeFilePath.Text)
            Dim fileFolder As String = System.IO.Path.GetDirectoryName(txtShapeFilePath.Text)
            Dim workspaceFactory As IWorkspaceFactory = DirectCast(objFactory.Create(typeClsID), IWorkspaceFactory)
            Dim featureWorkspace As IFeatureWorkspace = DirectCast(workspaceFactory.OpenFromFile(fileFolder, 0), IFeatureWorkspace)

            'Create the layer
            Dim featureLayer As IFeatureLayer = DirectCast(objFactory.Create("esriCarto.FeatureLayer"), IFeatureLayer)
            featureLayer.FeatureClass = featureWorkspace.OpenFeatureClass(shapeFile)
            featureLayer.Name = featureLayer.FeatureClass.AliasName

            'Add the layer to document
            Dim document As IBasicDocument = DirectCast(m_application.Document, IBasicDocument)
            document.AddLayer(featureLayer)
            document.UpdateContents()
        Catch
        End Try

        Me.Cursor = Cursors.Default
    End Sub

    Private Sub btnShutdown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShutdown.Click
        If m_application IsNot Nothing Then

            'Try to close any modal dialogs by sending the Escape key
            'It doesn't handle the followings: 
            '- VBA is up and has a modal dialog
            '- Modal dialog doesn't close with the Escape key
            AppActivate(m_application.Caption)
            Dim nestModalHwnd As Integer = GetLastActivePopup(m_application.hWnd)
            Do Until nestModalHwnd = m_application.hWnd
                SendKeys.SendWait("{ESC}")
                nestModalHwnd = GetLastActivePopup(m_application.hWnd)
            Loop

            'Manage document dirty flag - abandon changes
            Dim docDirtyFlag As IDocumentDirty2 = DirectCast(m_application.Document, IDocumentDirty2)
            docDirtyFlag.SetClean()

            'Exit
            RemoveHandler m_appROTEvent.AppRemoved, AddressOf m_appROTEvent_AppRemoved
            m_appROTEvent = Nothing

            m_application.Shutdown()
            m_application = Nothing
            m_appHWnd = 0


            'Reset UI for next automation
            txtShapeFilePath.Enabled = False
            btnShutdown.Enabled = False
            btnDrive.Enabled = False
            cboApps.Enabled = True
            btnStartApp.Enabled = True
        End If
    End Sub

#Region "Handle the case when the application is shutdown by user manually"
    Private Sub m_appROTEvent_AppRemoved(ByVal pApp As AppRef)
        'Application is shut down manually. Stop listening
        If pApp.hWnd = m_appHWnd Then 'compare by hwnd
            RemoveHandler m_appROTEvent.AppRemoved, AddressOf m_appROTEvent_AppRemoved
            m_appROTEvent = Nothing
            m_application = Nothing
            m_appHWnd = 0

            'Reset UI has to be in the form UI thread of this application, 
            'not the AppROT thread;
            If (Me.InvokeRequired) Then 'i.e. not on the right thread
                Me.BeginInvoke(New IAppROTEvents_AppRemovedEventHandler(AddressOf AppRemovedResetUI), pApp)
            Else
                AppRemovedResetUI(pApp) 'call directly
            End If
        End If
    End Sub

    Private Sub AppRemovedResetUI(ByVal pApp As AppRef)
        txtShapeFilePath.Enabled = False
        btnShutdown.Enabled = False
        btnDrive.Enabled = False
        cboApps.Enabled = True
        btnStartApp.Enabled = True
    End Sub
#End Region
End Class
