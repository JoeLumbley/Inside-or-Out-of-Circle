Imports System.Reflection

Public Class Form1

    Private CircleCenterPoint As Point = New Point(150, 150)
    Private MousePointerLocation As Point = New Point(0, 0)
    Private CircleRadius As Integer = 300
    Private IsPointerInsideCircle As Boolean = False
    Private DistanceSquared As Double
    Private RadiusSquared As Double = CircleRadius * CircleRadius
    Private DistanceArrowCap As New Drawing2D.AdjustableArrowCap(5, 5, True)
    Private DistancePen As New Pen(Color.Black, 3)
    Private RadiusArrowCap As New Drawing2D.AdjustableArrowCap(4, 4, True)
    Private RadiusPen As New Pen(Color.Gray, 2)
    Private XDistance As Double = 0
    Private YDistance As Double = 0
    Private MousePointBrush As SolidBrush = Brushes.Gray
    Private CircleBrush As SolidBrush = Brushes.LightGray

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load

        RadiusPen.CustomStartCap = RadiusArrowCap
        RadiusPen.CustomEndCap = RadiusArrowCap
        DistancePen.CustomStartCap = DistanceArrowCap
        DistancePen.CustomEndCap = DistanceArrowCap

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

        Invalidate() ' Triggers redraw

    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)

        DrawCircle(e)

        DrawCalculationDetails(e)

        ' Draw the circle center
        e.Graphics.FillEllipse(Brushes.Gray, CircleCenterPoint.X - 3, CircleCenterPoint.Y - 3, 6, 6)
        e.Graphics.DrawString($"X: {CircleCenterPoint.X}, Y: {CircleCenterPoint.Y}",
                              Me.Font,
                              Brushes.Black,
                              CircleCenterPoint.X,
                              CircleCenterPoint.Y + 10)

        e.Graphics.FillEllipse(Brushes.Gray, CircleCenterPoint.X + CircleRadius - 3, CircleCenterPoint.Y - 3, 6, 6)

        ' Draw the radius line
        e.Graphics.DrawLine(RadiusPen, CircleCenterPoint, New Point(CircleCenterPoint.X + CircleRadius, CircleCenterPoint.Y))
        e.Graphics.DrawString($"Radius: {CircleRadius}", Me.Font, Brushes.Black, CircleCenterPoint.X + CircleRadius + 10, CircleCenterPoint.Y + 10)
        e.Graphics.DrawString($"Radius²: {RadiusSquared}", Me.Font, Brushes.Black, CircleCenterPoint.X + CircleRadius + 10, CircleCenterPoint.Y - 10)

        ' Draw the mouse pointer location as a small circle
        'Dim MousePointBrush As SolidBrush = If(IsPointerInsideCircle, Brushes.Yellow, Brushes.Gray)
        e.Graphics.FillEllipse(MousePointBrush, MousePointerLocation.X - 3, MousePointerLocation.Y - 3, 6, 6)

        ' Draw the distance line and distance calculation
        ' Draw a line from the circle center to the mouse pointer location 
        e.Graphics.DrawLine(DistancePen, CircleCenterPoint, MousePointerLocation)
        e.Graphics.DrawString($"Distance²: {DistanceSquared}",
                              Me.Font,
                              Brushes.Black,
                              MousePointerLocation.X + 30,
                              MousePointerLocation.Y)

        ' Draw the mouse pointer location
        e.Graphics.DrawString($"X: {MousePointerLocation.X},Y: {MousePointerLocation.Y}", Me.Font, Brushes.Black, MousePointerLocation.X + 30, MousePointerLocation.Y + 20)

    End Sub

    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)

        ' Recalculate center on resize
        CircleCenterPoint = New Point(Me.ClientSize.Width \ 2, Me.ClientSize.Height \ 2)

        CircleRadius = Math.Min(Me.ClientSize.Width, Me.ClientSize.Height) \ 3

        RadiusSquared = CircleRadius * CircleRadius

        Invalidate() ' Redraw to reflect new position

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

        'Dim fillColor As Color = If(IsPointerInsideCircle, Color.LightSkyBlue, Color.LightGray)

        'Using brush As New SolidBrush(CircleBrush)

        Dim rect As New Rectangle(CircleCenterPoint.X - CircleRadius,
                                  CircleCenterPoint.Y - CircleRadius,
                                  CircleRadius * 2, CircleRadius * 2)

        e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

        e.Graphics.FillEllipse(CircleBrush, rect)

        'End Using

    End Sub


    Private Sub DrawCalculationDetails(e As PaintEventArgs)

        e.Graphics.DrawString($"Radius: {CircleRadius}",
                              Me.Font,
                              Brushes.Black,
                              10,
                              10)

        e.Graphics.DrawString($"Radius²: {RadiusSquared} = {CircleRadius} * {CircleRadius}",
                              Me.Font,
                              Brushes.Black,
                              10,
                              40)

        e.Graphics.DrawString($"Center: X: {CircleCenterPoint.X}, Y: {CircleCenterPoint.Y}",
                              Me.Font,
                              Brushes.Black,
                              10,
                              70)

        e.Graphics.DrawString($"Mouse: X: {MousePointerLocation.X}, Y: {MousePointerLocation.Y}",
                              Me.Font,
                              Brushes.Black,
                              10,
                              100)

        e.Graphics.DrawString($"X Distance: {XDistance} = {MousePointerLocation.X} - {CircleCenterPoint.X}",
                              Me.Font,
                              Brushes.Black,
                              10,
                              130)

        e.Graphics.DrawString($"Y Distance: {YDistance} = {MousePointerLocation.Y} - {CircleCenterPoint.Y}",
                              Me.Font,
                              Brushes.Black,
                              10,
                              160)

        e.Graphics.DrawString($"Distance²: {DistanceSquared} = {XDistance} * {XDistance} + {YDistance} * {YDistance}",
                              Me.Font, Brushes.Black,
                              10,
                              190)

        e.Graphics.DrawString($"Inside Circle: {IsPointerInsideCircle} = {DistanceSquared} <= {RadiusSquared}",
                              Me.Font,
                              Brushes.Black,
                              10,
                              220)

    End Sub

End Class
