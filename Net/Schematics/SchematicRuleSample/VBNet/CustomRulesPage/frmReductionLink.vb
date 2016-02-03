Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms

Friend Class frmReductionLink
    Private m_isDirty As Boolean = False
    Private m_pageSite As ESRI.ArcGIS.Framework.IComPropertyPageSite

    Public Sub New()
        InitializeComponent()
    End Sub

    Protected Overrides Sub Finalize()
        m_pageSite = Nothing
        MyBase.Finalize()
    End Sub

    ' For managing the IsDirty flag that specifies whether 
    ' or not controls in the custom form have been modified
    Public Property IsDirty() As Boolean
        Get
            Return m_isDirty
        End Get
        Set(ByVal value As Boolean)
            m_isDirty = value
        End Set
    End Property

    '- For managing the related IComPropertyPageSite
    Public WriteOnly Property PageSite() As ESRI.ArcGIS.Framework.IComPropertyPageSite
        Set(ByVal value As ESRI.ArcGIS.Framework.IComPropertyPageSite)
            m_pageSite = value
        End Set
    End Property

    Private Sub Changed(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDescription.TextChanged, chkUsePort.CheckStateChanged, cboReduce.SelectedIndexChanged, cboReduce.Click
        ' If the user changes something, mark the custom form dirty and 
        ' enable the apply button on the page site via the PageChanged method
        m_isDirty = True
        If (m_pageSite IsNot Nothing) Then m_pageSite.PageChanged()
    End Sub
End Class
