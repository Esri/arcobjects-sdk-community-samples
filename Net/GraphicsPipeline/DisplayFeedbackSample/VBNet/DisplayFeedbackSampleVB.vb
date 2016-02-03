Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.SystemUI
Imports ESRI.ArcGIS.Desktop.AddIns

Public Class DisplayFeedbackSampleVB
    Inherits ESRI.ArcGIS.Desktop.AddIns.Tool

    Private NewEnvelopeFeedback As INewEnvelopeFeedback
    Private feedbackEnvelope As IEnvelope
    Private feedbackElement As IElement
    Private feedbackScreenDisplay As IScreenDisplay
    Private feedbackLineSymbol As ISimpleLineSymbol
    Private feedbackStartPoint As ESRI.ArcGIS.Geometry.Point
    Private feedbackMovePoint As ESRI.ArcGIS.Geometry.Point


    Public Sub New()

    End Sub


    Protected Overrides Sub OnMouseDown(ByVal Args As MouseEventArgs)
        feedbackEnvelope = New Envelope
        feedbackStartPoint = New ESRI.ArcGIS.Geometry.Point
        feedbackMovePoint = New ESRI.ArcGIS.Geometry.Point
        feedbackLineSymbol = New SimpleLineSymbol
        feedbackScreenDisplay = New ScreenDisplay

        feedbackScreenDisplay = My.ArcMap.Document.ActiveView.ScreenDisplay

        feedbackLineSymbol.Style = ESRI.ArcGIS.Display.esriSimpleLineStyle.esriSLSDashDotDot

        'Initialize a new EnvelopeFeedback object
        NewEnvelopeFeedback = New NewEnvelopeFeedback
        NewEnvelopeFeedback.Display = feedbackScreenDisplay
        NewEnvelopeFeedback.Symbol = feedbackLineSymbol

        'pass the start point from the mouse position, transforming it to map coordinates.
        feedbackStartPoint = feedbackScreenDisplay.DisplayTransformation.ToMapPoint(Args.X, Args.Y)
        NewEnvelopeFeedback.Start(feedbackStartPoint)

    End Sub

    Protected Overrides Sub OnMouseMove(ByVal Args As MouseEventArgs)
        'only pass the point if the mouse button is down
        If (Args.Button.ToString = "Left") Then
            'pass X and Y to feedbackMovePoint to transfer to NewEnvelopeFeedback
            feedbackMovePoint = feedbackScreenDisplay.DisplayTransformation.ToMapPoint(Args.X, Args.Y)
            NewEnvelopeFeedback.MoveTo(feedbackMovePoint)
        End If


    End Sub

    Protected Overrides Sub OnMouseUp(ByVal Args As MouseEventArgs)
        'when mouse comes up, end the new envelope and pass it to feedbackEnvelope.
        feedbackEnvelope = NewEnvelopeFeedback.Stop()

        'initialize a new RectangleElementClass
        feedbackElement = New RectangleElement

        'pass the new rectangle element, the geometry defined by our feedback object
        feedbackElement.Geometry = feedbackEnvelope

        'make sure the element is activated in the current view
        feedbackElement.Activate(feedbackScreenDisplay)

        'now add the newly created element to the ActiveView with default symbology.
        My.ArcMap.Document.ActiveView.GraphicsContainer.AddElement(feedbackElement, 0)

        'and refresh the view so we can see the changes.
        My.ArcMap.Document.ActiveView.Refresh()

    End Sub


    Protected Overrides Sub OnUpdate()
        Enabled = My.ArcMap.Application IsNot Nothing
    End Sub
End Class
