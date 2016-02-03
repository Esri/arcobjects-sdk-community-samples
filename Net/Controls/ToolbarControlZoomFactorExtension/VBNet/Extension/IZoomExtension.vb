Option Explicit On 

'IZoomExtension interface
Public Interface IZoomExtension
    Property ZoomFactor() As Double
End Interface

Public Class myIZoomExtension
    Implements IZoomExtension

    Public Property ZoomFactor() As Double Implements IZoomExtension.ZoomFactor
        Get
        End Get
        Set(ByVal Value As Double)
        End Set
    End Property
End Class