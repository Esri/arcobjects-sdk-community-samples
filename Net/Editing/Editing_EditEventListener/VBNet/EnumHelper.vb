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


