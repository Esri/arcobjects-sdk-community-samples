Imports System 
Imports System.Runtime.InteropServices 
Imports ESRI.ArcGIS.Framework 
Imports ESRI.ArcGIS.SystemUI 
Imports ESRI.ArcGIS.ADF.CATIDs 

Namespace ACME.GIS.SampleExt
  <Guid("527C02BC-2AFB-477d-A5F3-B178F4F7C633")> _
  <ClassInterface(ClassInterfaceType.None)> _
  <ProgId("ACME.MainMenuVB")> _
  Public Class AcmeMenu
    Implements IMenuDef
    Implements IRootLevelMenu
#Region "IMenuDef Members"

    Public ReadOnly Property Caption() As String Implements IMenuDef.Caption
      Get
        Return "ACME"
      End Get
    End Property

    Public Sub GetItemInfo(ByVal pos As Integer, ByVal itemDef As IItemDef) Implements IMenuDef.GetItemInfo
      ' Add some commands to the menu (don't really exists for simplicity sake) 
      Select Case pos
        Case 0
          itemDef.ID = "ACME.SomeCmd"
          itemDef.Group = False
          Exit Select
        Case 1
          itemDef.ID = "ACME.SomeCmd2"
          itemDef.Group = True
          Exit Select
      End Select
    End Sub

    Public ReadOnly Property ItemCount() As Integer Implements IMenuDef.ItemCount
      Get
        Return 2
      End Get
    End Property

    Public ReadOnly Property Name() As String Implements IMenuDef.Name
      Get
        Return "ACME Main Menu"
      End Get
    End Property

#End Region
  End Class
End Namespace