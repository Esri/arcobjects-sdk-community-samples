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
Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.ObjectModel
Imports System.IO


	''' <summary>
	''' This class describes a single map - its location, the map name
	''' </summary>

    Public Class Map

      Public Sub New(ByVal path As String)
          _path = path
          _source = New Uri(path)
          _image = BitmapFrame.Create(_source)
          Dim [sub] As String = path.Substring(path.LastIndexOf("\") + 1)
          _mapName = [sub].Substring(0, [sub].Length - 4)
      End Sub

      Private _path As String
      Private _mapName As String
      Private _source As Uri
      Private _image As BitmapFrame

      Public ReadOnly Property MapName() As String
        Get
          Return _mapName
        End Get
      End Property

      Public ReadOnly Property Source() As String
        Get
          Return _path
        End Get
      End Property

      Public Property Image() As BitmapFrame
        Get
          Return _image
        End Get
        Set(ByVal value As BitmapFrame)
          _image = Value
        End Set
      End Property

    End Class

		''' <summary>
		''' This class represents a collection of map images in a directory.
		''' </summary>
		Public Class MapCollection : Inherits ObservableCollection(Of Map)
      Public Sub New()
      End Sub

      Private _directory As DirectoryInfo

      Public Sub New(ByVal mapPath As String)
        Me.New(New DirectoryInfo(mapPath))
      End Sub

      Public Sub New(ByVal directory As DirectoryInfo)
        _directory = directory
        Update()
      End Sub

			Public Property Path() As String
				Set
					_directory = New DirectoryInfo (Value)
					Update ()
				End Set
				Get
					Return _directory.FullName
				End Get
			End Property

			Public Property Directory() As DirectoryInfo
				Set
					_directory = Value
					Update ()
				End Set
				Get
					Return _directory
				End Get
			End Property

			Private Sub Update()
				Me.Clear ()
				Try
					For Each f As FileInfo In _directory.GetFiles ("*.jpg")
						Add (New Map (f.FullName))
					Next f
				Catch e1 As DirectoryNotFoundException
					System.Windows.MessageBox.Show ("No Such Directory")
				End Try
    End Sub


  End Class

		''' <summary>
''' This class returns a local data path as global variable.
		''' </summary>
		Public Class data
			Public Shared Function GetLocalDataPath() As String
				Dim rootPath As String = Environment.CurrentDirectory
				Dim position As Integer = rootPath.LastIndexOf ("\")
				Dim subString As String = rootPath.Substring (0, position)
				Return rootPath.Substring(0,subString.LastIndexOf("\"))
			End Function
		End Class




