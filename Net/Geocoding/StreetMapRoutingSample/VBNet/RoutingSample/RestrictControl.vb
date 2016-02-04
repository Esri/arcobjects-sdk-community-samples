'Copyright 2016 Esri

'Licensed under the Apache License, Version 2.0 (the "License");
'you may not use this file except in compliance with the License.
'You may obtain a copy of the License at

'    http://www.apache.org/licenses/LICENSE-2.0

'Unless required by applicable law or agreed to in writing, software
'distributed under the License is distributed on an "AS IS" BASIS,
'WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.

'See the License for the specific language governing permissions and
'limitations under the License.
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
