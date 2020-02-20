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
Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text

Namespace GlobeFlyTool
	Public Class PointZ
		Public x As Double
		Public y As Double
		Public z As Double

		Public Sub New()
			x = 0
			y = 0
			z = 0
		End Sub
		Public Sub New(ByVal x As Double, ByVal y As Double, ByVal z As Double)
			Me.x = x
			Me.y = y
			Me.z = z
		End Sub

		Public Function Norm() As Double
			Dim val As Double = Math.Sqrt((Me.x * Me.x) + (Me.y * Me.y) + (Me.z * Me.z))
			Return val
		End Function

		Public Shared Operator +(ByVal p1 As PointZ, ByVal p2 As PointZ) As PointZ
			Dim newPoint As New PointZ()
			newPoint.x = p1.x + p2.x
			newPoint.y = p1.y + p2.y
			newPoint.z = p1.z + p2.z
			Return newPoint
		End Operator

		Public Shared Operator -(ByVal p1 As PointZ, ByVal p2 As PointZ) As PointZ
			Dim newPoint As New PointZ()
			newPoint.x = p1.x - p2.x
			newPoint.y = p1.y - p2.y
			newPoint.z = p1.z - p2.z
			Return newPoint
		End Operator

		Public Shared Operator *(ByVal p As PointZ, ByVal factor As Double) As PointZ
			Dim newPoint As New PointZ(factor * p.x, factor * p.y, factor * p.z)
			Return newPoint
		End Operator
	End Class
End Namespace
