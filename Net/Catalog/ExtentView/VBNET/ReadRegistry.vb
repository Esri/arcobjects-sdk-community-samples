Option Explicit On

Module ReadRegistry
    Public Const HKEY_LOCAL_MACHINE = &H80000002
    Public Const KEY_QUERY_VALUE = &H1
    Public Const ERROR_SUCCESS = 0&
    Public Const REG_SZ = 1
    Public Const REG_DWORD = 4

    Private Declare Function RegOpenKey Lib "advapi32.dll" Alias "RegOpenKeyA" (ByVal hKey As Long, ByVal lpSubKey As String, ByVal phkResult As Long) As Long
    Private Declare Function RegQueryValueEx Lib "advapi32.dll" Alias "RegQueryValueExA" (ByVal hKey As Long, ByVal lpValueName As String, ByVal lpReserved As Long, ByVal lpType As Long, ByVal lpData As String, ByVal lpcbData As Long) As Long
    Private Declare Function RegCloseKey Lib "advapi32.dll" (ByVal hKey As Long) As Long

    ' Function to get key value

    Public Function GetKeyValue(ByVal key As String, ByVal path As String, ByVal value As String) As String
        Dim handle, result, lpType, bufSize As Long
        Dim buf As String
        Try
            'Open key
            result = RegOpenKey(key, path, handle)
            If result <> ERROR_SUCCESS Then
                GetKeyValue = ""
                Exit Function
            End If

            'Query key
            bufSize = 256
            buf = Space$(bufSize)
            result = RegQueryValueEx(handle, value, 0&, lpType, buf, bufSize)
            If result = ERROR_SUCCESS Then
                Return Left(buf, bufSize - 1)
            Else
                Return ""
            End If
            'Close key
            RegCloseKey(key)
        Catch ex As Exception
            MsgBox(ex.ToString())
            Return ""
        End Try
    End Function
End Module
