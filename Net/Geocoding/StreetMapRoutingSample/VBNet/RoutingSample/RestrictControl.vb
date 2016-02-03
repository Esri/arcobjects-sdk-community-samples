Public Class RestrictControl

#Region "Public properties"
	' Restriction name
	Public Property RSName() As String
		Get
			Return m_chkCheck.Text
		End Get

		Set(ByVal value As String)
			m_chkCheck.Text = value
		End Set
	End Property

	' Is restriction checked
	Public Property RSChecked() As Boolean
		Get
			Return m_chkCheck.Checked
		End Get

		Set(ByVal value As Boolean)
			m_chkCheck.Checked = value
		End Set
	End Property

	Public Enum ERSType
		eStrict = 0
		eRelaxed = 1
	End Enum

	' Restriction Type
	Public Property RSType() As ERSType
		Get
			Return m_cmbType.SelectedIndex
		End Get

		Set(ByVal value As ERSType)
			m_cmbType.SelectedIndex = value
		End Set
	End Property

	' Restriction Parameter
	Public Property RSParameter() As Double
		Get
			If Not m_txtParameter.Enabled Then _
				Return 0.0

			Dim nVal As Double
			Try
				nVal = Double.Parse(m_txtParameter.Text)
			Catch ex As Exception
				nVal = 0
			End Try

			Return nVal
		End Get

		Set(ByVal value As Double)
			m_txtParameter.Text = value.ToString
		End Set
	End Property

	' Is Parameter enabled
	Public WriteOnly Property RSUseParameter() As Boolean
		Set(ByVal value As Boolean)
			If Not value Then _
				m_txtParameter.Text = ""
			m_txtParameter.Visible = value
		End Set
	End Property

#End Region

End Class
