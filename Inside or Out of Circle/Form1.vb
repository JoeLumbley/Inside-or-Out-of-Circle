
' Inside or Out of Circle

' This program demonstrates how to determine if a point is inside or outside a circle.
' It calculates the squared distance from the point to the center of the circle
' and compares it to the squared radius of the circle.
' If the squared distance is less than or equal to the squared radius,
' the point is inside or on the edge of the circle.
' If the squared distance is greater than the squared radius, the point is outside the circle.
' The program also displays the radius, center of the circle, mouse pointer location,
' and the calculated distances in a graphical window.

' MIT License

' Copyright (c) 2025 Joseph W. Lumbley

' Permission is hereby granted, free of charge, to any person obtaining a copy
' of this software and associated documentation files (the "Software"), to deal
' in the Software without restriction, including without limitation the rights
' to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
' copies of the Software, and to permit persons to whom the Software is
' furnished to do so, subject to the following conditions:
' The above copyright notice and this permission notice shall be included in all
' copies or substantial portions of the Software.
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
' IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
' FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
' AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
' LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
' OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
' SOFTWARE.

Public Class Form1

    Private Structure TextDisplay
        Public X As Integer
        Public Y As Integer
        Public Text As String
        Public Brush As SolidBrush
        Public Sub New(x As Integer, y As Integer, text As String, brush As SolidBrush)
            Me.X = x
            Me.Y = y
            Me.Text = text
            Me.Brush = brush
        End Sub
    End Structure

    Private TextDisplays As New List(Of TextDisplay) From {
        New TextDisplay(10, 10, "Radius: ", Brushes.Black),
        New TextDisplay(10, 40, "Radius²: ", Brushes.Black),
        New TextDisplay(10, 70, "Center: X: , Y: ", Brushes.Black),
        New TextDisplay(10, 100, "Mouse: X: , Y: ", Brushes.Black),
        New TextDisplay(10, 130, "X Distance: ", Brushes.Black),
        New TextDisplay(10, 160, "Y Distance: ", Brushes.Black),
        New TextDisplay(10, 190, "Distance²: ", Brushes.Black),
        New TextDisplay(10, 220, "Inside Circle: ", Brushes.Black),
        New TextDisplay(0, 0, "Distance²: ", Brushes.Black),
        New TextDisplay(0, 0, "X:   ,Y: ", Brushes.Black),
        New TextDisplay(0, 0, "X:   ,Y: ", Brushes.Black),
        New TextDisplay(0, 0, "Radius: ", Brushes.Black),
        New TextDisplay(0, 0, "Radius²: ", Brushes.Black)
    }

    Private Enum TextDisplayIndex
        Radius = 0
        RadiusSquared = 1
        Center = 2
        Mouse = 3
        XDistance = 4
        YDistance = 5
        DistanceSquared = 6
        InsideCircle = 7
        DistanceSquaredAtMouse = 8
        MouseCoordinates = 9
        CircleCenterCoordinates = 10
        CircleRadiusText = 11
        CircleRadiusSquaredText = 12
    End Enum

    Private Structure LineDisplay
        Public X1 As Integer
        Public Y1 As Integer
        Public X2 As Integer
        Public Y2 As Integer
        Public Pen As Pen
        Public Sub New(x1 As Integer, y1 As Integer, x2 As Integer, y2 As Integer, pen As Pen)
            Me.X1 = x1
            Me.Y1 = y1
            Me.X2 = x2
            Me.Y2 = y2
            Me.Pen = pen
        End Sub
    End Structure

    Private LineDisplays As New List(Of LineDisplay) From {
        New LineDisplay(CircleCenterPoint.X, CircleCenterPoint.Y, CircleCenterPoint.X + CircleRadius, CircleCenterPoint.Y, New Pen(Color.Gray, 2)),
        New LineDisplay(CircleCenterPoint.X, CircleCenterPoint.Y, MousePointerLocation.X, CircleCenterPoint.Y, New Pen(Color.Gray, 2)),
        New LineDisplay(MousePointerLocation.X, CircleCenterPoint.Y, MousePointerLocation.X, MousePointerLocation.Y, New Pen(Color.Gray, 2)),
        New LineDisplay(CircleCenterPoint.X, CircleCenterPoint.Y, MousePointerLocation.X, MousePointerLocation.Y, New Pen(Color.Gray, 2))
    }

    Private Structure CircleDisplay
        Public X As Integer
        Public Y As Integer
        Public Width As Integer
        Public Height As Integer
        Public Brush As SolidBrush
        Public Sub New(x As Integer, y As Integer, width As Integer, height As Integer, brush As Brush)
            Me.X = x
            Me.Y = y
            Me.Width = width
            Me.Height = height
            Me.Brush = brush
        End Sub
    End Structure

    Private CircleDisplays As New List(Of CircleDisplay) From {
        New CircleDisplay(CircleCenterPoint.X - CircleRadius, CircleCenterPoint.Y - CircleRadius, CircleRadius * 2, CircleRadius * 2, Brushes.LightGray),
        New CircleDisplay(CircleCenterPoint.X + CircleRadius - 3, CircleCenterPoint.Y - 3, 6, 6, Brushes.LightGray),
        New CircleDisplay(CircleCenterPoint.X - 3, CircleCenterPoint.Y - 3, 6, 6, Brushes.LightGray),
        New CircleDisplay(MousePointerLocation.X - 3, MousePointerLocation.Y - 3, 6, 6, Brushes.LightGray)
    }

    Private CircleCenterPoint As Point = New Point(150, 150)
    Private MousePointerLocation As Point = New Point(0, 0)
    Private CircleRadius As Integer = 300
    Private IsPointerInsideCircle As Boolean = False
    Private DistanceSquared As Double
    Private RadiusSquared As Double = CircleRadius * CircleRadius
    Private DistanceArrowCap As New Drawing2D.AdjustableArrowCap(5, 5, True)
    Private DistancePen As New Pen(Color.Black, 3)
    Private ArrowBlack3Pen As New Pen(Color.Black, 3)
    Private TransparentPen As New Pen(Color.Transparent, 3)

    Private RadiusArrowCap As New Drawing2D.AdjustableArrowCap(4, 4, True)
    Private RadiusPen As New Pen(Color.Gray, 2)
    Private XYDistancePen As New Pen(Color.Orchid, 2)
    Private Orchid2Pen As New Pen(Color.Orchid, 2)

    Private XDistance As Double = 0
    Private YDistance As Double = 0
    Private MousePointBrush As SolidBrush = Brushes.Gray
    Private CircleBrush As SolidBrush = Brushes.LightGray
    Private RadiusBrush As SolidBrush = Brushes.Gray

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        DistancePen = TransparentPen

        RadiusPen.CustomStartCap = RadiusArrowCap
        RadiusPen.CustomEndCap = RadiusArrowCap
        ArrowBlack3Pen.CustomStartCap = DistanceArrowCap
        ArrowBlack3Pen.CustomEndCap = DistanceArrowCap
        XYDistancePen = TransparentPen
        MousePointBrush = Brushes.Transparent

        For i As Integer = 0 To TextDisplays.Count - 1

            Dim td As TextDisplay = TextDisplays(i)

            td.Brush = Brushes.Transparent
            TextDisplays(i) = td

        Next

        For i As Integer = 0 To LineDisplays.Count - 1
            Dim ld As LineDisplay = LineDisplays(i)
            ld.Pen = Pens.Transparent
            LineDisplays(i) = ld
        Next

        For i As Integer = 0 To CircleDisplays.Count - 1
            Dim ld = CircleDisplays(i)
            Select Case i
                Case 0
                    ld.X = CircleCenterPoint.X - CircleRadius
                    ld.Y = CircleCenterPoint.Y - CircleRadius
                    ld.Width = CircleRadius * 2
                    ld.Height = CircleRadius * 2
                    ld.Brush = CircleBrush

            End Select

            CircleDisplays(i) = ld

        Next

    End Sub

    Private Sub Form1_MouseEnter(sender As Object, e As EventArgs) Handles Me.MouseEnter

        DistancePen = ArrowBlack3Pen
        XYDistancePen = Orchid2Pen
        MousePointBrush = Brushes.Gray

        For i As Integer = 0 To TextDisplays.Count - 1

            Dim td As TextDisplay = TextDisplays(i)

            td.Brush = Brushes.Black
            TextDisplays(i) = td

        Next

        For i As Integer = 0 To LineDisplays.Count - 1
            Dim ld = LineDisplays(i)
            Select Case i
                Case 0
                    ld.X1 = CircleCenterPoint.X
                    ld.Y1 = CircleCenterPoint.Y
                    ld.X2 = CircleCenterPoint.X + CircleRadius
                    ld.Y2 = CircleCenterPoint.Y
                    ld.Pen = RadiusPen
                Case 1
                    ld.X1 = CircleCenterPoint.X
                    ld.Y1 = CircleCenterPoint.Y
                    ld.X2 = MousePointerLocation.X
                    ld.Y2 = CircleCenterPoint.Y
                    ld.Pen = XYDistancePen
                Case 2
                    ld.X1 = MousePointerLocation.X
                    ld.Y1 = CircleCenterPoint.Y
                    ld.X2 = MousePointerLocation.X
                    ld.Y2 = MousePointerLocation.Y
                    ld.Pen = XYDistancePen
                Case 3
                    ld.X1 = CircleCenterPoint.X
                    ld.Y1 = CircleCenterPoint.Y
                    ld.X2 = MousePointerLocation.X
                    ld.Y2 = MousePointerLocation.Y
                    ld.Pen = DistancePen

            End Select

            LineDisplays(i) = ld

        Next

        For i As Integer = 0 To CircleDisplays.Count - 1
            Dim ld = CircleDisplays(i)
            Select Case i
                Case 0
                    ld.X = CircleCenterPoint.X - CircleRadius
                    ld.Y = CircleCenterPoint.Y - CircleRadius
                    ld.Width = CircleRadius * 2
                    ld.Height = CircleRadius * 2
                    ld.Brush = CircleBrush

            End Select

            CircleDisplays(i) = ld

        Next

        Invalidate()

    End Sub

    Private Sub Form1_MouseLeave(sender As Object, e As EventArgs) Handles Me.MouseLeave
        DistancePen = TransparentPen
        XYDistancePen = TransparentPen
        MousePointBrush = Brushes.Transparent

        For i As Integer = 0 To TextDisplays.Count - 1

            Dim td As TextDisplay = TextDisplays(i)

            td.Brush = Brushes.Transparent
            TextDisplays(i) = td

        Next

        For i As Integer = 0 To LineDisplays.Count - 1

            Dim ld As LineDisplay = LineDisplays(i)

            ld.Pen = Pens.Transparent

            LineDisplays(i) = ld

        Next

        For i As Integer = 0 To CircleDisplays.Count - 1
            Dim ld = CircleDisplays(i)
            Select Case i
                Case 0
                    ld.X = CircleCenterPoint.X - CircleRadius
                    ld.Y = CircleCenterPoint.Y - CircleRadius
                    ld.Width = CircleRadius * 2
                    ld.Height = CircleRadius * 2
                    ld.Brush = CircleBrush
                Case 1
                    ld.X = CircleCenterPoint.X + CircleRadius - 3
                    ld.Y = CircleCenterPoint.Y - 3
                    ld.Width = 6
                    ld.Height = 6
                    ld.Brush = Brushes.Transparent
                Case 2
                    ld.X = CircleCenterPoint.X - 3
                    ld.Y = CircleCenterPoint.Y - 3
                    ld.Width = 6
                    ld.Height = 6
                    ld.Brush = Brushes.Transparent
                Case 3
                    ld.X = MousePointerLocation.X - 3
                    ld.Y = MousePointerLocation.Y - 3
                    ld.Width = 6
                    ld.Height = 6
                    ld.Brush = Brushes.Transparent

            End Select

            CircleDisplays(i) = ld

        Next

        Invalidate()

    End Sub

    Protected Overrides Sub OnMouseMove(e As MouseEventArgs)
        MyBase.OnMouseMove(e)

        MousePointerLocation = e.Location

        IsPointerInsideCircle = IsPointInsideCircle(e.X, e.Y, CircleCenterPoint.X, CircleCenterPoint.Y, CircleRadius)
        MousePointBrush = If(IsPointerInsideCircle, Brushes.Yellow, Brushes.Gray)
        CircleBrush = If(IsPointerInsideCircle, Brushes.LightSkyBlue, Brushes.LightGray)

        XDistance = MousePointerLocation.X - CircleCenterPoint.X
        YDistance = MousePointerLocation.Y - CircleCenterPoint.Y
        DistanceSquared = XDistance * XDistance + YDistance * YDistance


        For i As Integer = 0 To TextDisplays.Count - 1
            Dim td = TextDisplays(i)
            Select Case i
                Case TextDisplayIndex.Radius
                    td.Text = $"Radius: {CircleRadius}"
                Case TextDisplayIndex.RadiusSquared
                    td.Text = $"Radius²: {RadiusSquared} = {CircleRadius} * {CircleRadius}"
                Case TextDisplayIndex.Center
                    td.Text = $"Center: X: {CircleCenterPoint.X}, Y: {CircleCenterPoint.Y}"
                Case TextDisplayIndex.Mouse
                    td.Text = $"Mouse: X: {MousePointerLocation.X}, Y: {MousePointerLocation.Y}"
                Case TextDisplayIndex.XDistance
                    td.Text = $"X Distance: {XDistance} = {MousePointerLocation.X} - {CircleCenterPoint.X}"
                Case TextDisplayIndex.YDistance
                    td.Text = $"Y Distance: {YDistance} = {MousePointerLocation.Y} - {CircleCenterPoint.Y}"
                Case TextDisplayIndex.DistanceSquared
                    td.Text = $"Distance²: {DistanceSquared} = {XDistance} * {XDistance} + {YDistance} * {YDistance}"
                Case TextDisplayIndex.InsideCircle
                    td.Text = $"Inside Circle: {IsPointerInsideCircle} = {DistanceSquared} <= {RadiusSquared}"
                Case TextDisplayIndex.DistanceSquaredAtMouse
                    td.Text = $"Distance²: {DistanceSquared}"
                    td.X = MousePointerLocation.X + 30
                    td.Y = MousePointerLocation.Y
                Case TextDisplayIndex.MouseCoordinates
                    td.Text = $"X: {MousePointerLocation.X},Y: {MousePointerLocation.Y}"
                    td.X = MousePointerLocation.X + 30
                    td.Y = MousePointerLocation.Y + 20
                Case TextDisplayIndex.CircleCenterCoordinates
                    td.Text = $"X: {CircleCenterPoint.X}, Y: {CircleCenterPoint.Y}"
                    td.X = CircleCenterPoint.X
                    td.Y = CircleCenterPoint.Y + 10
                Case TextDisplayIndex.CircleRadiusText
                    td.Text = $"Radius: {CircleRadius}"
                    td.X = CircleCenterPoint.X + CircleRadius + 10
                    td.Y = CircleCenterPoint.Y + 10
                Case TextDisplayIndex.CircleRadiusSquaredText
                    td.Text = $"Radius²: {RadiusSquared}"
                    td.X = CircleCenterPoint.X + CircleRadius + 10
                    td.Y = CircleCenterPoint.Y - 10

            End Select

            TextDisplays(i) = td

        Next

        For i As Integer = 0 To LineDisplays.Count - 1
            Dim ld = LineDisplays(i)
            Select Case i
                Case 0
                    ld.X1 = CircleCenterPoint.X
                    ld.Y1 = CircleCenterPoint.Y
                    ld.X2 = CircleCenterPoint.X + CircleRadius
                    ld.Y2 = CircleCenterPoint.Y
                Case 1
                    ld.X1 = CircleCenterPoint.X
                    ld.Y1 = CircleCenterPoint.Y
                    ld.X2 = MousePointerLocation.X
                    ld.Y2 = CircleCenterPoint.Y
                Case 2
                    ld.X1 = MousePointerLocation.X
                    ld.Y1 = CircleCenterPoint.Y
                    ld.X2 = MousePointerLocation.X
                    ld.Y2 = MousePointerLocation.Y
                Case 3
                    ld.X1 = CircleCenterPoint.X
                    ld.Y1 = CircleCenterPoint.Y
                    ld.X2 = MousePointerLocation.X
                    ld.Y2 = MousePointerLocation.Y

            End Select

            LineDisplays(i) = ld

        Next

        For i As Integer = 0 To CircleDisplays.Count - 1
            Dim ld = CircleDisplays(i)
            Select Case i
                Case 0
                    ld.X = CircleCenterPoint.X - CircleRadius
                    ld.Y = CircleCenterPoint.Y - CircleRadius
                    ld.Width = CircleRadius * 2
                    ld.Height = CircleRadius * 2
                    ld.Brush = CircleBrush
                Case 1
                    ld.X = CircleCenterPoint.X + CircleRadius - 3
                    ld.Y = CircleCenterPoint.Y - 3
                    ld.Width = 6
                    ld.Height = 6
                    ld.Brush = RadiusBrush
                Case 2
                    ld.X = CircleCenterPoint.X - 3
                    ld.Y = CircleCenterPoint.Y - 3
                    ld.Width = 6
                    ld.Height = 6
                    ld.Brush = RadiusBrush
                Case 3
                    ld.X = MousePointerLocation.X - 3
                    ld.Y = MousePointerLocation.Y - 3
                    ld.Width = 6
                    ld.Height = 6
                    ld.Brush = MousePointBrush

            End Select

            CircleDisplays(i) = ld

        Next

        Invalidate()

    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)

        e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

        For Each circleDisplay As CircleDisplay In CircleDisplays

            e.Graphics.FillEllipse(circleDisplay.Brush, circleDisplay.X, circleDisplay.Y, circleDisplay.Width, circleDisplay.Height)

        Next

        For Each lineDisplay As LineDisplay In LineDisplays

            e.Graphics.DrawLine(lineDisplay.Pen, lineDisplay.X1, lineDisplay.Y1, lineDisplay.X2, lineDisplay.Y2)

        Next

        For Each textDisplay As TextDisplay In TextDisplays
            'e.Graphics.DrawString(textDisplay.Text, New Font("Segoe UI", 13), textDisplay.Brush, textDisplay.X, textDisplay.Y)
            'use textrender to draw text
            e.Graphics.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAlias
            e.Graphics.DrawString(textDisplay.Text, New Font("Segoe UI", 10), textDisplay.Brush, textDisplay.X, textDisplay.Y)

        Next

    End Sub

    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)

        CircleCenterPoint = New Point(Me.ClientSize.Width \ 2, Me.ClientSize.Height \ 2)

        CircleRadius = Math.Min(Me.ClientSize.Width, Me.ClientSize.Height) \ 3

        RadiusSquared = CircleRadius * CircleRadius

        For i As Integer = 0 To CircleDisplays.Count - 1
            Dim ld = CircleDisplays(i)
            Select Case i
                Case 0
                    ld.X = CircleCenterPoint.X - CircleRadius
                    ld.Y = CircleCenterPoint.Y - CircleRadius
                    ld.Width = CircleRadius * 2
                    ld.Height = CircleRadius * 2

            End Select

            CircleDisplays(i) = ld

        Next

        Invalidate()

    End Sub

    Function IsPointInsideCircle(pointX As Double, pointY As Double,
             centerX As Double, centerY As Double, radius As Double) As Boolean
        ' This function checks if a point (pointX, pointY) is inside or on the edge of a circle
        ' defined by its center (centerX, centerY) and radius.

        ' Calculate horizontal distance from the point to the center of the circle
        Dim Xdistance As Double = pointX - centerX

        ' Calculate vertical distance from the point to the center of the circle
        Dim Ydistance As Double = pointY - centerY

        ' Calculate the squared distance from the point to the center of the circle
        Dim squaredDistance As Double = Xdistance * Xdistance + Ydistance * Ydistance

        ' Check if the squared distance is less than or equal to the squared radius
        Return squaredDistance <= radius * radius

        ' It uses the squared distance to avoid the computational cost of taking a square root.
        ' The function returns True if the point is inside or on the edge of the circle,
        ' and False if the point is outside the circle.

        ' The function calculates the horizontal and vertical distances from the point to the center of the circle,
        ' squares these distances, and sums them to get the squared distance.
        ' It then compares this squared distance to the squared radius of the circle.
        ' If the squared distance is less than or equal to the squared radius, the point is inside or on the edge of the circle.
        ' If the squared distance is greater than the squared radius, the point is outside the circle.
        ' This method is particularly useful in scenarios where performance is critical,
        ' such as in graphics rendering or physics simulations, where many distance checks may be performed frequently.

    End Function

End Class
