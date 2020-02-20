'Copyright 2019 Esri

'Licensed under the Apache License, Version 2.0 (the "License");
'you may not use this file except in compliance with the License.
'You may obtain a copy of the License at

'    http://www.apache.org/licenses/LICENSE-2.0

'Unless required by applicable law or agreed to in writing, software
'distributed under the License is distributed on an "AS IS" BASIS,
'WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.

'See the License for the specific language governing permissions and
'limitations under the License.
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports Microsoft.VisualBasic

    Public Class EnumUtils
    Public Shared Function EnumToList(Of T)() As List(Of T)

        Dim enumType As Type = GetType(T)

        ' Can't use type constraints on value types, so have to do check like this
        If Not enumType.BaseType Is GetType(System.Enum) Then
            Throw New ArgumentException("T must be of type System.Enum")
        End If

        Return New List(Of T)(TryCast(System.Enum.GetNames(enumType), IEnumerable(Of T)))

    End Function

End Class


