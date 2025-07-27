
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
            Dim td = TextDisplays(i)

            td = TextDisplays(i)
            td.Brush = Brushes.Transparent
            TextDisplays(i) = td
            td = TextDisplays(i)
            td.Brush = Brushes.Transparent
            TextDisplays(i) = td

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

        Invalidate() ' Redraw to apply the new pen

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
                Case 0
                    td.Text = $"Radius: {CircleRadius}"
                Case 1
                    td.Text = $"Radius²: {RadiusSquared} = {CircleRadius} * {CircleRadius}"
                Case 2
                    td.Text = $"Center: X: {CircleCenterPoint.X}, Y: {CircleCenterPoint.Y}"
                Case 3
                    td.Text = $"Mouse: X: {MousePointerLocation.X}, Y: {MousePointerLocation.Y}"
                Case 4
                    td.Text = $"X Distance: {XDistance} = {MousePointerLocation.X} - {CircleCenterPoint.X}"
                Case 5
                    td.Text = $"Y Distance: {YDistance} = {MousePointerLocation.Y} - {CircleCenterPoint.Y}"
                Case 6
                    td.Text = $"Distance²: {DistanceSquared} = {XDistance} * {XDistance} + {YDistance} * {YDistance}"
                Case 7
                    td.Text = $"Inside Circle: {IsPointerInsideCircle} = {DistanceSquared} <= {RadiusSquared}"
                Case 8
                    td.Text = $"Distance²: {DistanceSquared}"
                    td.X = MousePointerLocation.X + 30
                    td.Y = MousePointerLocation.Y
                Case 9
                    td.Text = $"X: {MousePointerLocation.X},Y: {MousePointerLocation.Y}"
                    td.X = MousePointerLocation.X + 30
                    td.Y = MousePointerLocation.Y + 20
                Case 10
                    td.Text = $"X: {CircleCenterPoint.X}, Y: {CircleCenterPoint.Y}"
                    td.X = CircleCenterPoint.X
                    td.Y = CircleCenterPoint.Y + 10
                Case 11
                    td.Text = $"Radius: {CircleRadius}"
                    td.X = CircleCenterPoint.X + CircleRadius + 10
                    td.Y = CircleCenterPoint.Y + 10
                Case 12
                    td.Text = $"Radius²: {RadiusSquared}"
                    td.X = CircleCenterPoint.X + CircleRadius + 10
                    td.Y = CircleCenterPoint.Y - 10


            End Select
            TextDisplays(i) = td
        Next

        Invalidate() ' Triggers redraw

    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)

        DrawCircle(e)

        ' Draw the radius line
        e.Graphics.DrawLine(RadiusPen, CircleCenterPoint, New Point(CircleCenterPoint.X + CircleRadius, CircleCenterPoint.Y))
        e.Graphics.FillEllipse(RadiusBrush, CircleCenterPoint.X + CircleRadius - 3, CircleCenterPoint.Y - 3, 6, 6)


        ' Draw YX distance lines
        Dim basePt = New Point(MousePointerLocation.X, CircleCenterPoint.Y)
        e.Graphics.DrawLine(XYDistancePen, CircleCenterPoint, basePt)
        e.Graphics.DrawLine(XYDistancePen, basePt, MousePointerLocation)

        ' Draw the circle center
        e.Graphics.FillEllipse(Brushes.Gray, CircleCenterPoint.X - 3, CircleCenterPoint.Y - 3, 6, 6)

        ' Draw the mouse pointer location as a small circle
        e.Graphics.FillEllipse(MousePointBrush, MousePointerLocation.X - 3, MousePointerLocation.Y - 3, 6, 6)

        ' Draw the distance line 
        e.Graphics.DrawLine(DistancePen, CircleCenterPoint, MousePointerLocation)

        DrawCalculationDetails(e)

    End Sub

    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)

        CircleCenterPoint = New Point(Me.ClientSize.Width \ 2, Me.ClientSize.Height \ 2)

        CircleRadius = Math.Min(Me.ClientSize.Width, Me.ClientSize.Height) \ 3

        RadiusSquared = CircleRadius * CircleRadius

        Invalidate()

    End Sub

    Function IsPointInsideCircle(pointX As Double, pointY As Double,
             centerX As Double, centerY As Double, radius As Double) As Boolean
        ' Note: This function is a simplified version of the distance check.
        ' It is efficient and avoids unnecessary calculations by using squared distances.

        ' Calculate horizontal distance from the point to the center of the circle
        Dim Xdistance As Double = pointX - centerX

        ' Calculate vertical distance from the point to the center of the circle
        Dim Ydistance As Double = pointY - centerY

        ' Calculate the squared distance from the point to the center of the circle
        Dim squaredDistance As Double = Xdistance * Xdistance + Ydistance * Ydistance

        ' Check if the squared distance is less than or equal to the squared radius
        Return squaredDistance <= radius * radius

        ' If the squared distance from the point to the center of the circle
        ' (dx)^2 + (dy)^2 is less than or equal <= to the squared radius (radius^2),
        ' then the point is inside or on the edge of the circle.

        ' If the squared distance is greater than > the squared radius, then the point is outside the circle.
        ' This optimization is particularly useful in performance-critical applications
        ' such as real-time graphics rendering or physics simulations.

    End Function

    Private Sub DrawCircle(e As PaintEventArgs)

        Dim rect As New Rectangle(CircleCenterPoint.X - CircleRadius,
                                  CircleCenterPoint.Y - CircleRadius,
                                  CircleRadius * 2, CircleRadius * 2)

        e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

        e.Graphics.FillEllipse(CircleBrush, rect)

    End Sub

    Private Sub DrawCalculationDetails(e As PaintEventArgs)


        For Each textDisplay As TextDisplay In TextDisplays
            e.Graphics.DrawString(textDisplay.Text, Me.Font, textDisplay.Brush, textDisplay.X, textDisplay.Y)
        Next




        'e.Graphics.DrawString($"Radius: {CircleRadius}",
        '                      Me.Font,
        '                      Brushes.Black,
        '                      10,
        '                      10)

        'e.Graphics.DrawString($"Radius²: {RadiusSquared} = {CircleRadius} * {CircleRadius}",
        '                      Me.Font,
        '                      Brushes.Black,
        '                      10,
        '                      40)

        'e.Graphics.DrawString($"Center: X: {CircleCenterPoint.X}, Y: {CircleCenterPoint.Y}",
        '                      Me.Font,
        '                      Brushes.Black,
        '                      10,
        '                      70)

        'e.Graphics.DrawString($"Mouse: X: {MousePointerLocation.X}, Y: {MousePointerLocation.Y}",
        '                      Me.Font,
        '                      Brushes.Black,
        '                      10,
        '                      100)

        'e.Graphics.DrawString($"X Distance: {XDistance} = {MousePointerLocation.X} - {CircleCenterPoint.X}",
        '                      Me.Font,
        '                      Brushes.Black,
        '                      10,
        '                      130)

        'e.Graphics.DrawString($"Y Distance: {YDistance} = {MousePointerLocation.Y} - {CircleCenterPoint.Y}",
        '                      Me.Font,
        '                      Brushes.Black,
        '                      10,
        '                      160)

        'e.Graphics.DrawString($"Distance²: {DistanceSquared} = {XDistance} * {XDistance} + {YDistance} * {YDistance}",
        '                      Me.Font, Brushes.Black,
        '                      10,
        '                      190)

        'e.Graphics.DrawString($"Inside Circle: {IsPointerInsideCircle} = {DistanceSquared} <= {RadiusSquared}",
        '                      Me.Font,
        '                      Brushes.Black,
        '                      10,
        '                      220)

    End Sub

End Class
