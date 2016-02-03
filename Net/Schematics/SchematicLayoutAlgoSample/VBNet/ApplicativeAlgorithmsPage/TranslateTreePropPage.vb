Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Schematic
Imports ApplicativeAlgorithmsVB

<ClassInterface(ClassInterfaceType.None)> _
<Guid(TranslateTreePropPage.GUID)> _
<ProgId(TranslateTreePropPage.PROGID)> _
<ComVisible(True)> _
Partial Public Class TranslateTreePropPage
    Inherits PropertyPage

    Public Const GUID As String = "33F8A5CA-9F13-494e-8064-0CE5BAB77865"
    Public Const PROGID As String = "ApplicativeAlgorithmsPage.TranslateTreePropPageVB"


#Region "Component Category Registration"
    <ComRegisterFunction()> _
    <ComVisibleAttribute(True)> _
    Public Shared Sub Reg(ByVal sKey As String)
        SchematicAlgorithmPages.Register(sKey)
    End Sub

    <ComUnregisterFunction()> _
    <ComVisibleAttribute(True)> _
    Public Shared Sub Unreg(ByVal sKey As String)
        SchematicAlgorithmPages.Unregister(sKey)
    End Sub
#End Region

#Region "internal methods"
    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub ChangedTexte(ByVal sender As Object, ByVal e As EventArgs) Handles txtYTrans.TextChanged, txtXTrans.TextChanged
        PageIsDirty = True
    End Sub

    Private Sub TexteEnter(ByVal sender As Object, ByVal e As EventArgs) Handles txtYTrans.Enter, txtXTrans.Enter
        Dim texteBox As TextBox = CType(sender, TextBox)
        texteBox.SelectAll()
    End Sub

    Private Sub btnRestore_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRestore.Click

        ' Get the saved algorithm parameters from the diagram class
        Dim newAlgo As TranslateTree = GetSavedAlgo()
        If (newAlgo Is Nothing) Then
            newAlgo = New TranslateTree()     ' otherwise revert to default algorithm parameters
        End If
        ' get the values and set the edit boxes
        txtXTrans.Text = newAlgo.TranslationFactorX.ToString()
        txtYTrans.Text = newAlgo.TranslationFactorY.ToString()
    End Sub

    Private Function GetSavedAlgo() As TranslateTree
        Dim myAlgo As TranslateTree = FindOurAlgo()
        If (myAlgo Is Nothing) Then Return Nothing

        Dim diagramClassName As ISchematicDiagramClassName = myAlgo.SchematicDiagramClassName
        Dim pName As IName = CType(diagramClassName, IName)
        If (pName Is Nothing) Then Return Nothing

        Dim unk As Object = pName.Open()
        Dim diagramClass As ISchematicDiagramClass = CType(unk, ISchematicDiagramClass)
        If (diagramClass Is Nothing) Then Return Nothing

        ' get the default algorithms for this diagram class
        Dim enumAlgorithms As IEnumSchematicAlgorithm = diagramClass.SchematicAlgorithms
        If (enumAlgorithms Is Nothing) Then Return Nothing

        enumAlgorithms.Reset()
        Dim algorithm As ISchematicAlgorithm = enumAlgorithms.Next()
        While (algorithm IsNot Nothing)
            If TypeOf (algorithm) Is TranslateTree Then Return CType(algorithm, TranslateTree)

            algorithm = enumAlgorithms.Next()
        End While

        Return Nothing
    End Function

    Private Function FindOurAlgo() As TranslateTree
        ' loop through the objects until the algorithm is found or not
        Dim enumCollection As System.Collections.IEnumerator = Objects.GetEnumerator()
        enumCollection.Reset()

        While (enumCollection.MoveNext())
            If TypeOf (enumCollection.Current) Is TranslateTree Then Return CType(enumCollection.Current, TranslateTree) ' found it
        End While

        Return Nothing
    End Function
#End Region


#Region " PropertyPage "

    Protected Overrides Sub OnPageDeactivate()
        MyBase.OnPageDeactivate()
    End Sub

    Protected Overrides Sub OnActivated(ByVal e As System.EventArgs)
        MyBase.OnActivated(e)

        Dim myAlgo As TranslateTree = FindOurAlgo()
        If (myAlgo IsNot Nothing) Then
            Me.txtXTrans.Text = myAlgo.TranslationFactorX.ToString()
            Me.txtYTrans.Text = myAlgo.TranslationFactorY.ToString()
        End If

        PageIsDirty = False
    End Sub

    Protected Overrides Sub OnPageApply()
        'OnPageApply is launched two times when you click on Apply 
        'and one time if you have more one page and change page
        'So I use a flag to authorize or not the application of algorithm
        'The timer reset the flag, in case of multi-pages
        timApply.Enabled = False
        MyBase.OnPageApply()

        Dim myAlgo As TranslateTree = FindOurAlgo()
        If (myAlgo IsNot Nothing) Then
            Try
                myAlgo.TranslationFactorX = System.Convert.ToDouble(Me.txtXTrans.Text)

            Finally
            End Try

            Try
                myAlgo.TranslationFactorY = System.Convert.ToDouble(Me.txtYTrans.Text)
            Finally
            End Try
        End If
        timApply.Enabled = True

        PageIsDirty = False
    End Sub


    Public Overrides Sub SetPageSite(ByVal pPageSite As IPropertyPageSite)
        If (pPageSite Is Nothing) Then Return

        MyBase.SetPageSite(pPageSite)
    End Sub


    'make sure our algorithm is in the input array of IUnknown
    ' otherwise throw an exception
    Public Overrides Sub SetObjects(ByVal cObjects As UInteger, ByVal ppUnk() As Object)
        If (ppUnk Is Nothing Or cObjects < 1) Then
            Throw New ArgumentNullException()
        End If

        ' remove previously stored IUnkown objects
        Objects = Nothing

        ' browse input collection 
        Dim enumCollection As System.Collections.IEnumerator = ppUnk.GetEnumerator()

        enumCollection.Reset()
        While (enumCollection.MoveNext())

            If TypeOf (enumCollection.Current) Is TranslateTree Then
                ' assumes only one object is managed by this property page
                Dim newObjects(1) As Object

                newObjects(0) = enumCollection.Current
                Objects = newObjects
                Exit While
            End If
        End While

        If (Objects Is Nothing) Then
            Throw New ArgumentNullException()
        End If
    End Sub
#End Region
End Class

