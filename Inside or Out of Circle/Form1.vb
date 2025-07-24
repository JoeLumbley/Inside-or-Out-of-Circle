Public Class Form1

    Private circleCenter As Point = New Point(150, 150)
    Private MousePointerLocation As Point = New Point(0, 0)
    Private radius As Integer = 300
    Private isInside As Boolean = False
    Private distanceSquared As Double
    Private RadiusSquared As Double = radius * radius
    Private DistanceArrowCap As New Drawing2D.AdjustableArrowCap(5, 5, True)
    Private DistancePen As New Pen(Color.Black, 3)
    Private RadiusArrowCap As New Drawing2D.AdjustableArrowCap(4, 4, True)
    Private RadiusPen As New Pen(Color.Gray, 2)


    Protected Overrides Sub OnMouseMove(e As MouseEventArgs)
        MyBase.OnMouseMove(e)

        MousePointerLocation = e.Location

        isInside = IsPointInsideCircle(e.X, e.Y, circleCenter.X, circleCenter.Y, radius)

        Invalidate() ' Triggers redraw

    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)

        DrawCircle(e)

        DrawCalculationDetails(e)

        ' Draw the circle center
        e.Graphics.FillEllipse(Brushes.Gray, circleCenter.X - 3, circleCenter.Y - 3, 6, 6)
        e.Graphics.DrawString($"X:{circleCenter.X},Y:{circleCenter.Y}",
                              Me.Font,
                              Brushes.Black,
                              circleCenter.X,
                              circleCenter.Y + 10)

        Dim MousePointBrush As SolidBrush = If(isInside, Brushes.Yellow, Brushes.Gray)
        e.Graphics.FillEllipse(Brushes.Gray, circleCenter.X + radius - 3, circleCenter.Y - 3, 6, 6)

        ' Draw the radius line
        RadiusPen.CustomStartCap = RadiusArrowCap
        RadiusPen.CustomEndCap = RadiusArrowCap

        e.Graphics.DrawLine(RadiusPen, circleCenter, New Point(circleCenter.X + radius, circleCenter.Y))
        e.Graphics.DrawString($"Radius²: {RadiusSquared}", Me.Font, Brushes.Black, circleCenter.X + radius + 10, circleCenter.Y - 10)


        ' Draw the mouse pointer location as a small circle
        e.Graphics.FillEllipse(MousePointBrush, MousePointerLocation.X - 3, MousePointerLocation.Y - 3, 6, 6)

        ' Draw the distance line and distance calculation
        ' Draw a line from the circle center to the mouse pointer location 
        'Dim MyPen As New Pen(Color.Black, 2)
        DistancePen.CustomStartCap = DistanceArrowCap
        DistancePen.CustomEndCap = DistanceArrowCap

        e.Graphics.DrawLine(DistancePen, circleCenter, MousePointerLocation)
        e.Graphics.DrawString($"Distance²: {distanceSquared}",
                              Me.Font,
                              Brushes.Black,
                              MousePointerLocation.X + 30,
                              MousePointerLocation.Y)

        ' Draw the mouse pointer location
        e.Graphics.DrawString($"X:{MousePointerLocation.X},Y:{MousePointerLocation.Y}", Me.Font, Brushes.Black, MousePointerLocation.X + 30, MousePointerLocation.Y + 20)

    End Sub

    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)

        ' Recalculate center on resize
        circleCenter = New Point(Me.ClientSize.Width \ 2, Me.ClientSize.Height \ 2)

        radius = Math.Min(Me.ClientSize.Width, Me.ClientSize.Height) \ 3

        RadiusSquared = radius * radius

        Invalidate() ' Redraw to reflect new position

    End Sub

    Private Function IsPointInsideCircle(pointX As Double, pointY As Double,
                                         centerX As Double, centerY As Double,
                                         radius As Double) As Boolean

        ' calculate horizontal distance
        Dim dx As Double = pointX - centerX
        ' calculate vertical distance
        Dim dy As Double = pointY - centerY

        distanceSquared = dx * dx + dy * dy

        Return distanceSquared <= radius * radius

    End Function

    Private Sub DrawCircle(e As PaintEventArgs)

        Dim fillColor As Color = If(isInside, Color.LightSkyBlue, Color.LightGray)

        Using brush As New SolidBrush(fillColor)

            Dim rect As New Rectangle(circleCenter.X - radius,
                                  circleCenter.Y - radius,
                                  radius * 2, radius * 2)

            e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

            e.Graphics.FillEllipse(brush, rect)

        End Using

    End Sub

    Private Sub DrawCalculationDetails(e As PaintEventArgs)
        e.Graphics.DrawString($"Radius: {radius}",
                              Me.Font,
                              Brushes.Black,
                              10,
                              10)

        e.Graphics.DrawString($"Radius²: {RadiusSquared} = {radius} * {radius}",
                              Me.Font,
                              Brushes.Black,
                              10,
                              40)

        e.Graphics.DrawString($"Center: X:{circleCenter.X},Y:{circleCenter.Y}",
                              Me.Font,
                              Brushes.Black,
                              10,
                              70)

        e.Graphics.DrawString($"Mouse: X:{MousePointerLocation.X},Y:{MousePointerLocation.Y}",
                              Me.Font,
                              Brushes.Black,
                              10,
                              100)

        e.Graphics.DrawString($"X Distance: {MousePointerLocation.X - circleCenter.X} = {MousePointerLocation.X} - {circleCenter.X}",
                              Me.Font,
                              Brushes.Black,
                              10,
                              130)

        e.Graphics.DrawString($"Y Distance: {MousePointerLocation.Y - circleCenter.Y} = {MousePointerLocation.Y} - {circleCenter.Y}",
                              Me.Font,
                              Brushes.Black,
                              10,
                              160)

        e.Graphics.DrawString($"Distance²: {distanceSquared} = {MousePointerLocation.X - circleCenter.X} * {MousePointerLocation.X - circleCenter.X} + {MousePointerLocation.Y - circleCenter.Y} * {MousePointerLocation.Y - circleCenter.Y}",
                              Me.Font, Brushes.Black,
                              10,
                              190)

        e.Graphics.DrawString($"Inside Circle: {isInside} = {distanceSquared} <= {RadiusSquared}",
                              Me.Font,
                              Brushes.Black,
                              10,
                              220)
    End Sub

End Class
