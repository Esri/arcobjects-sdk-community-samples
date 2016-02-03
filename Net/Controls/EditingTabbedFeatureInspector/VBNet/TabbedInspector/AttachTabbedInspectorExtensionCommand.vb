Imports System
Imports System.Drawing
Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Geodatabase
Imports System.Diagnostics

Namespace TabbedFeatureInspector
    '/ <summary>
    '/ A command that attaches/detaches the 'tabbed inspector' extension class from 
    '/ the feature class selected in the table of contents.
    '/ In order to work correctly, the hosting application must implement and pass an 
    '/ instance of IApplicationServices in the CustomProperty of its toolbar control.
    '/ </summary>
  <Guid("61B2CFFB-35DB-4aec-8DA2-C40C20C76901")> _
  <ClassInterface(ClassInterfaceType.None)> _
  <ProgId("TabbedFeatureInspectorVB.AttachTabbedInspectorExtensionCommand")> _
  Public Class AttachTabbedInspectorExtensionCommand
    Inherits BaseCommand
#Region "COM Registration Function(s)"
    <ComRegisterFunction()> _
    <ComVisible(False)> _
    Public Shared Sub RegisterFunction(ByVal registerType As Type)
      ' Required for ArcGIS Component Category Registrar support
      ArcGISCategoryRegistration(registerType)
    End Sub

    <ComUnregisterFunction()> _
    <ComVisible(False)> _
    Public Shared Sub UnregisterFunction(ByVal registerType As Type)
      ' Required for ArcGIS Component Category Registrar support
      ArcGISCategoryUnregistration(registerType)
    End Sub

#Region "ArcGIS Component Category Registrar generated code"
    '/ <summary>
    '/ Required method for ArcGIS Component Category registration -
    '/ Do not modify the contents of this method with the code editor.
    '/ </summary>
    Private Shared Sub ArcGISCategoryRegistration(ByVal registerType As Type)
      Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
      ControlsCommands.Register(regKey)
    End Sub
    '/ <summary>
    '/ Required method for ArcGIS Component Category unregistration -
    '/ Do not modify the contents of this method with the code editor.
    '/ </summary>
    Private Shared Sub ArcGISCategoryUnregistration(ByVal registerType As Type)
      Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
      ControlsCommands.Unregister(regKey)
    End Sub

#End Region
#End Region

    Dim m_hookHelper As IHookHelper
    Dim m_appServices As IApplicationServices
    Dim workHelper As System.Windows.Forms.MethodInvoker = AddressOf Work
    Dim fc As IFeatureClass
    Dim succeeded As Boolean = False 'return value of the 'Work' delegate

    Public Sub New()
      m_category = "Developer Samples"
      m_caption = "Attach/Detach Tabbed Inspector Extension VB"
      m_message = "This command attaches or detaches the Tabbed Inspector class extension from the selected feature class."
      m_toolTip = "This command attaches or detaches the Tabbed Inspector class extension from the selected feature class."
      m_name = "TabbedInspector_AttachDetachExtension_VB"
    End Sub

    '/ <summary>
    '/ Occurs when this command is created
    '/ </summary>
    '/ <param name="hook">Instance of the application</param>
    Public Overrides Sub OnCreate(ByVal hook As Object)
      If hook Is Nothing Then
        Return
      End If

      m_hookHelper = New HookHelperClass()
      m_hookHelper.Hook = hook
      m_appServices = Nothing
    End Sub

    '/ <summary>
    '/ Occurs when this command is clicked
    '/ </summary>
    Public Overrides Sub OnClick()
      Try
        GetApplicationServices()

        Dim fl As IFeatureLayer = m_appServices.GetLayerSelectedInTOC()

        If Not fl Is Nothing Then
          fc = fl.FeatureClass
          AlterClassExtension()
        Else
          m_appServices.SetStatusMessage("Couldn't attach the 'custom inspector' extension. No feature layer was selected in the Table of Contents.", True)
        End If
      Catch ex As Exception
        MessageBox.Show("Error: Could open the feature class. Original error: " + ex.Message)
      End Try
    End Sub

    '/ <summary>
    '/ Obtains the IApplicationServices interface instance implemented by the hosting application.
    '/ This is needed so the command can determine the selected layer, and update the application's status message.
    '/ </summary>
    Private Sub GetApplicationServices()
      If m_appServices Is Nothing Then
        Dim toolbarControl As IToolbarControl2 = m_hookHelper.Hook
        If toolbarControl Is Nothing Then
          Throw New ApplicationException( _
            "Command appears to be running in an unexpected environment. Its hookHelper ought to be a toolbar control.")
        End If

        m_appServices = toolbarControl.CustomProperty
        If m_appServices Is Nothing Then
          Throw New ApplicationException( _
            "Command appears to be running in an unexpected environment. The toolbar custom property ought to be an instance of IApplicationServices.")
        End If
      End If
    End Sub

    '/ <summary>
    '/ Perform the work contained in the delegate inside an exclusive schema lock.
    '/ </summary>
    '/ <param name="fc">The feature class whose schema is to be exclusively locked.</param>
    '/ <param name="work">The work to be performed.</param>
    Shared Sub DoInSchemaLock(ByVal fc As IFeatureClass, ByVal work As MethodInvoker)
      Dim schemaLock As ISchemaLock = DirectCast(fc, ISchemaLock)
      Try
        ' Exclusively lock the class schema.
        schemaLock.ChangeSchemaLock(esriSchemaLock.esriExclusiveSchemaLock)

        ' Do the work inside the schema lock
        work()
      Finally
                ' Release the exclusive lock on the featureclass' schema.
        schemaLock.ChangeSchemaLock(esriSchemaLock.esriSharedSchemaLock)
      End Try
    End Sub

    '/ <summary>
    '/ This method attaches or detaches the "TabbedInspector" class extension to/from the specified
    '/ feature class. If the featureclass already has an extension class, and it isnt the 'TabbedInspector' extension,
    '/ the method does not modify the class extension.
    '/ </summary>
    '/ <param name="fc">The feature class to be altered.</param>
    '/ <returns>Whether the operation succeeded (successful detach or attach).</returns>
    Private Function AlterClassExtension() As Boolean
      ' Attempt to get access to schema-editing functionality on the feature class
      Dim classSchemaEdit As IClassSchemaEdit = TryCast(fc, IClassSchemaEdit)
      If classSchemaEdit Is Nothing Then
        m_appServices.SetStatusMessage("The selected feature class doesn't support attaching an extension class.", True)
        Return False
      End If

      ' Do the schema update within a schema lock.
      DoInSchemaLock(fc, workHelper)

      AlterClassExtension = succeeded
    End Function

    Public Sub Work()
      succeeded = False
      ' Attempt to get access to schema-editing functionality on the feature class
      Dim classSchemaEdit As IClassSchemaEdit = DirectCast(fc, IClassSchemaEdit)

      ' Create a UID object holding the TabbedInspector's CLSID
      Dim classUID As UID = New UIDClass()
      classUID.Value = "{" + TabbedInspectorCLSID.TabbedInspectorCLSID + "}"

      ' Does the feature class already have an extension class associated with it?
      If Not fc.EXTCLSID Is Nothing Then
        ' The featureclass already has an extension attached.
        If fc.EXTCLSID.Value.Equals(classUID.Value) Then
          ' The extension is the TabbedInspector extension. Detach it.
          classSchemaEdit.AlterClassExtensionCLSID(Nothing, Nothing)

          m_appServices.SetStatusMessage( _
                String.Format("The 'custom inspector' extension class was detached from {0}.", fc.AliasName), False)
          succeeded = True
        Else
                    'Don't mess with featureclasses that have some other existing extension class associated with them.
          m_appServices.SetStatusMessage( _
                String.Format("{0} already has another extension class attached to it. No change was made.", fc.AliasName), True)
          succeeded = False
        End If
      Else
        ' There is no extension attached to the featureclass. Attach the TabbedInspector extension.
        classSchemaEdit.AlterClassExtensionCLSID(classUID, Nothing)
        m_appServices.SetStatusMessage( _
              String.Format("The 'custom inspector' extension class was attached to {0}.", fc.AliasName), False)
        succeeded = True
      End If
    End Sub
  End Class
End Namespace






