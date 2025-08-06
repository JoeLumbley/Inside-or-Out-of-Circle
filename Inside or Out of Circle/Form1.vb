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
        Public FontSize As Integer
        Public Font As Font
        Public Sub New(x As Integer, y As Integer, text As String, brush As SolidBrush, fontSize As Integer, font As Font)
            Me.X = x
            Me.Y = y
            Me.Text = text
            Me.Brush = brush
            Me.FontSize = fontSize
            Me.Font = font
        End Sub
    End Structure

    Private TextDisplays() As TextDisplay = {
    New TextDisplay(0, 0, "Heading", Brushes.Transparent, 10, New Font("Segoe UI", 10)),
    New TextDisplay(0, 0, "Mouse", Brushes.Transparent, 10, New Font("Segoe UI", 10)),
    New TextDisplay(0, 0, "Radius", Brushes.Transparent, 10, New Font("Segoe UI", 10)),
    New TextDisplay(0, 0, "Center", Brushes.Transparent, 10, New Font("Segoe UI", 10)),
    New TextDisplay(0, 0, "Footer", Brushes.Transparent, 10, New Font("Segoe UI", 10))
}

    Private Enum TextDisplayIndex
        Heading = 0
        Mouse = 1
        Radius = 2
        Center = 3
        Footer = 4
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


    Private LineDisplays() As LineDisplay = {
        New LineDisplay(CircleCenterPoint.X, CircleCenterPoint.Y, CircleCenterPoint.X + CircleRadius, CircleCenterPoint.Y, New Pen(Color.Chartreuse, 2)),
        New LineDisplay(CircleCenterPoint.X, CircleCenterPoint.Y, MousePointerLocation.X, CircleCenterPoint.Y, New Pen(Color.Chartreuse, 2)),
        New LineDisplay(MousePointerLocation.X, CircleCenterPoint.Y, MousePointerLocation.X, MousePointerLocation.Y, New Pen(Color.Chartreuse, 2)),
        New LineDisplay(CircleCenterPoint.X, CircleCenterPoint.Y, MousePointerLocation.X, MousePointerLocation.Y, New Pen(Color.Chartreuse, 2))
    }

    Private Enum LineDisplayIndex
        RadiusLine = 0
        XDistanceLine = 1
        YDistanceLine = 2
        DistanceLine = 3
    End Enum

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

    Private MouseHilightBrush As New SolidBrush(Color.FromArgb(128, Color.Yellow))

    Private CircleDisplays() As CircleDisplay = {
        New CircleDisplay(CircleCenterPoint.X - CircleRadius, CircleCenterPoint.Y - CircleRadius, CircleRadius * 2, CircleRadius * 2, Brushes.LightGray),
        New CircleDisplay(CircleCenterPoint.X + CircleRadius - 3, CircleCenterPoint.Y - 3, 6, 6, Brushes.LightGray),
        New CircleDisplay(CircleCenterPoint.X - 3, CircleCenterPoint.Y - 3, 6, 6, Brushes.LightGray),
        New CircleDisplay(MousePointerLocation.X - 20, MousePointerLocation.Y - 20, 40, 40, MouseHilightBrush),
        New CircleDisplay(MousePointerLocation.X - 3, MousePointerLocation.Y - 3, 6, 6, Brushes.LightGray)
    }

    Private Enum CircleDisplayIndex
        Circle = 0
        RadiusEndPoint = 1
        CenterPoint = 2
        MouseHilight = 3
        MousePoint = 4
    End Enum

    Private CircleCenterPoint As New Point(150, 150)
    Private MousePointerLocation As New Point(0, 0)
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

    Private HeadingFontSize As Integer = 16
    Private MouseFontSize As Integer = 12
    Private RadiusFontSize As Integer = 12
    Private CenterFontSize As Integer = 12
    Private FooterFontSize As Integer = 10

    Private gridPen As New Pen(Color.FromArgb(128, Color.LightGray), 2)

    Private Enum ViewStateIndex
        Overview
        ParametersView
    End Enum

    Private ViewState As ViewStateIndex = ViewStateIndex.Overview

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load

        InitializeApp()

    End Sub

    Private Sub Form1_MouseEnter(sender As Object, e As EventArgs) Handles Me.MouseEnter

        UpdateViewMouseEnter()

        Invalidate()

        InvaildateButtons()

    End Sub

    Private Sub Form1_MouseLeave(sender As Object, e As EventArgs) Handles Me.MouseLeave

        UpdateViewMouseLeave()

        Invalidate()

        InvaildateButtons()

    End Sub

    Protected Overrides Sub OnMouseMove(e As MouseEventArgs)
        MyBase.OnMouseMove(e)

        MousePointerLocation = e.Location

        IsPointerInsideCircle = IsPointInsideCircle(e.X, e.Y, CircleCenterPoint.X, CircleCenterPoint.Y, CircleRadius)

        UpdateViewOnMouseMove()

        Invalidate()

        InvaildateButtons()

    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)

        Dim g = e.Graphics

        g.SmoothingMode = Drawing2D.SmoothingMode.None

        DrawGrid(g)

        g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
        g.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAlias

        DrawCircles(g)

        DrawLines(g)

        DrawTextOverlays(g)

    End Sub

    Private Sub OverviewButton_Click(sender As Object, e As EventArgs) Handles OverviewButton.Click

        Switch2Overview()

        UpdateView()

        ' Hide overlays
        SetTextDisplayTransparent(TextDisplayIndex.Mouse)
        SetTextDisplayTransparent(TextDisplayIndex.Heading)
        SetTextDisplayTransparent(TextDisplayIndex.Footer)

        ' Hide mouse indicators
        SetCircleDisplayTransparent(CircleDisplayIndex.MousePoint)
        SetCircleDisplayTransparent(CircleDisplayIndex.MouseHilight)

        Invalidate()

        InvaildateButtons()

    End Sub

    Private Sub ParametersViewButton_Click(sender As Object, e As EventArgs) Handles ParametersViewButton.Click

        Switch2ParametersView()

        UpdateView()

        ' Hide overlays
        SetTextDisplayTransparent(TextDisplayIndex.Mouse)

        ' Hide mouse indicators
        SetCircleDisplayTransparent(CircleDisplayIndex.MousePoint)
        SetCircleDisplayTransparent(CircleDisplayIndex.MouseHilight)

        Invalidate()

        InvaildateButtons()

    End Sub

    Private Sub OverviewButton_MouseEnter(sender As Object, e As EventArgs) Handles OverviewButton.MouseEnter
        InvaildateButtons()
    End Sub

    Private Sub ParametersViewButton_MouseEnter(sender As Object, e As EventArgs) Handles ParametersViewButton.MouseEnter
        InvaildateButtons()
    End Sub

    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)

        UpdateButtonLayout()

        UpdateCircleGeometry()

        UpdateFontSizes()

        UpdateTextDisplaysOnResize()

        UpdateLineDisplays()

        UpdateCircleDisplaysOnResize()

        Invalidate()

        InvaildateButtons()

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

    Private Sub DrawCircles(g As Graphics)

        ' 🔵 Draw filled circles
        For Each circleDisplay As CircleDisplay In CircleDisplays
            g.FillEllipse(circleDisplay.Brush, circleDisplay.X, circleDisplay.Y, circleDisplay.Width, circleDisplay.Height)
        Next

    End Sub

    Private Sub DrawLines(g As Graphics)

        ' 📏 Draw lines
        For Each lineDisplay As LineDisplay In LineDisplays
            g.DrawLine(lineDisplay.Pen, lineDisplay.X1, lineDisplay.Y1, lineDisplay.X2, lineDisplay.Y2)
        Next

    End Sub

    Private Sub DrawTextOverlays(g As Graphics)

        ' abc Draw text overlays
        For Each textDisplay As TextDisplay In TextDisplays
            g.DrawString(textDisplay.Text, textDisplay.Font, textDisplay.Brush, textDisplay.X, textDisplay.Y)
        Next

    End Sub

    Private Sub DrawGrid(g As Graphics)

        ' 🔲 Draw grid ( lines every 50 pixels)
        For x As Integer = 0 To ClientSize.Width Step 50
            g.DrawLine(gridPen, x, 0, x, ClientSize.Height)
        Next
        For y As Integer = 0 To ClientSize.Height Step 50
            g.DrawLine(gridPen, 0, y, ClientSize.Width, y)
        Next

    End Sub

    Private Sub UpdateButtonLayout()
        ParametersViewButton.Width = Math.Max(40, Math.Min(Me.ClientSize.Width, Me.ClientSize.Height) \ 11)
        ParametersViewButton.Height = Math.Max(40, Math.Min(Me.ClientSize.Width, Me.ClientSize.Height) \ 11)
        ParametersViewButton.Font = New Font("Segoe UI", Math.Max(10, Math.Min(Me.ClientSize.Width, Me.ClientSize.Height) \ 40))
        OverviewButton.Width = Math.Max(40, Math.Min(Me.ClientSize.Width, Me.ClientSize.Height) \ 11)
        OverviewButton.Height = Math.Max(40, Math.Min(Me.ClientSize.Width, Me.ClientSize.Height) \ 11)
        OverviewButton.Font = New Font("Segoe UI", Math.Max(16, Math.Min(Me.ClientSize.Width, Me.ClientSize.Height) \ 25))
        ParametersViewButton.SetBounds(ClientSize.Width - ParametersViewButton.Width - 10, ClientSize.Height - ParametersViewButton.Height - 10, ParametersViewButton.Width, ParametersViewButton.Height)

        OverviewButton.SetBounds(ClientSize.Width - ParametersViewButton.Width - 10 - OverviewButton.Width - 10, ClientSize.Height - OverviewButton.Height - 10, OverviewButton.Width, OverviewButton.Height)

    End Sub

    Private Sub UpdateCircleGeometry()
        CircleCenterPoint = New Point(ClientSize.Width \ 2, ClientSize.Height \ 2)
        CircleRadius = Math.Min(ClientSize.Width, ClientSize.Height) \ 3.4
        RadiusSquared = CircleRadius * CircleRadius
    End Sub

    Private Sub UpdateFontSizes()
        Dim baseSize = Math.Min(ClientSize.Width, ClientSize.Height) \ 20
        MouseFontSize = Math.Max(10, baseSize)
        RadiusFontSize = MouseFontSize
        CenterFontSize = MouseFontSize
        HeadingFontSize = MouseFontSize
        FooterFontSize = MouseFontSize
    End Sub

    Private Sub UpdateTextDisplaysOnResize()
        Using g As Graphics = CreateGraphics()

            Dim td As TextDisplay
            Dim ThisStringSize As SizeF

            td = TextDisplays(TextDisplayIndex.Heading)
            td.Text = If(ViewState = ViewStateIndex.ParametersView, "Parameters", $"Inside Circle {IsPointerInsideCircle}")
            td.FontSize = HeadingFontSize
            td.Font = New Font("Segoe UI", td.FontSize)
            ThisStringSize = g.MeasureString(td.Text, td.Font)
            td.X = ClientSize.Width \ 2 - ThisStringSize.Width \ 2
            td.Y = ((CircleCenterPoint.Y - CircleRadius) \ 2) - (ThisStringSize.Height \ 2)
            TextDisplays(TextDisplayIndex.Heading) = td

            td = TextDisplays(TextDisplayIndex.Center)
            td.Text = $"X {CircleCenterPoint.X}, Y {CircleCenterPoint.Y}"
            td.FontSize = CenterFontSize
            td.Font = New Font("Segoe UI", td.FontSize)
            ThisStringSize = g.MeasureString(td.Text, td.Font)
            td.X = CircleCenterPoint.X - ThisStringSize.Width \ 2
            td.Y = CircleCenterPoint.Y
            TextDisplays(TextDisplayIndex.Center) = td

            td = TextDisplays(TextDisplayIndex.Radius)
            td.Text = If(ViewState = ViewStateIndex.ParametersView, $"Radius {CircleRadius}", $"Radius² {RadiusSquared}")
            td.FontSize = RadiusFontSize
            td.Font = New Font("Segoe UI", td.FontSize)
            ThisStringSize = g.MeasureString(td.Text, td.Font)
            td.X = CircleCenterPoint.X + CircleRadius + 10
            td.Y = CircleCenterPoint.Y - ThisStringSize.Height \ 2
            TextDisplays(TextDisplayIndex.Radius) = td

            td = TextDisplays(TextDisplayIndex.Footer)
            td.Text = If(ViewState = ViewStateIndex.ParametersView, $"What is Known", $"{IsPointerInsideCircle} = {DistanceSquared} <= {RadiusSquared}")
            td.FontSize = FooterFontSize
            td.Font = New Font("Segoe UI", td.FontSize)
            ThisStringSize = g.MeasureString(td.Text, td.Font)
            td.X = ClientSize.Width \ 2 - ThisStringSize.Width \ 2
            td.Y = (CircleCenterPoint.Y + CircleRadius) + (ClientSize.Height - (CircleCenterPoint.Y + CircleRadius)) \ 2 - (ThisStringSize.Height \ 2)
            TextDisplays(TextDisplayIndex.Footer) = td

            td = TextDisplays(TextDisplayIndex.Mouse)
            td.Brush = Brushes.Transparent

        End Using
    End Sub

    Private Sub UpdateLineDisplays()
        For i As Integer = 0 To LineDisplays.Count - 1
            Dim ld = LineDisplays(i)

            Select Case i
                Case LineDisplayIndex.RadiusLine
                    SetLine(ld, CircleCenterPoint, New Point(CircleCenterPoint.X + CircleRadius, CircleCenterPoint.Y))

                Case LineDisplayIndex.XDistanceLine
                    SetLine(ld, CircleCenterPoint, New Point(MousePointerLocation.X, CircleCenterPoint.Y))
                    If ViewState = ViewStateIndex.Overview Then
                        ld.Pen = XYDistancePen
                    Else
                        ld.Pen = Pens.Transparent
                    End If

                Case LineDisplayIndex.YDistanceLine
                    SetLine(ld, New Point(MousePointerLocation.X, CircleCenterPoint.Y), MousePointerLocation)
                    If ViewState = ViewStateIndex.Overview Then
                        ld.Pen = XYDistancePen
                    Else
                        ld.Pen = Pens.Transparent
                    End If

                Case LineDisplayIndex.DistanceLine
                    SetLine(ld, CircleCenterPoint, MousePointerLocation)
                    If ViewState = ViewStateIndex.Overview Then
                        ld.Pen = DistancePen
                    Else
                        ld.Pen = Pens.Transparent
                    End If

            End Select

            LineDisplays(i) = ld
        Next
    End Sub

    Private Sub SetLine(ByRef ld As LineDisplay, p1 As Point, p2 As Point)
        ld.X1 = p1.X
        ld.Y1 = p1.Y
        ld.X2 = p2.X
        ld.Y2 = p2.Y
    End Sub



    Private Sub SetTextDisplayTransparent(index As TextDisplayIndex)
        Dim td = TextDisplays(index)
        td.Brush = Brushes.Transparent
        TextDisplays(index) = td
    End Sub

    Private Sub SetLineDisplayTransparent(index As LineDisplayIndex)
        Dim ld = LineDisplays(index)
        ld.Pen = Pens.Transparent
        LineDisplays(index) = ld
    End Sub

    Private Sub SetLineDisplayPen(index As LineDisplayIndex, pen As Pen)
        Dim ld = LineDisplays(index)
        ld.Pen = pen
        LineDisplays(index) = ld
    End Sub


    Private Sub SetTextDisplayBlack(index As TextDisplayIndex)
        Dim td = TextDisplays(index)
        td.Brush = Brushes.Black
        TextDisplays(index) = td
    End Sub



    Private Sub SetCircleDisplayTransparent(index As CircleDisplayIndex)
        Dim cd = CircleDisplays(index)
        cd.Brush = Brushes.Transparent
        CircleDisplays(index) = cd
    End Sub


    Private Sub SetCircleDisplayBrush(index As CircleDisplayIndex, brush As SolidBrush)
        Dim cd = CircleDisplays(index)
        cd.Brush = brush
        CircleDisplays(index) = cd
    End Sub

    Private Sub InvaildateButtons()
        ' Invalidate the buttons to update their appearance

        OverviewButton.Invalidate()

        ParametersViewButton.Invalidate()

    End Sub

    Private Sub Switch2Overview()
        ' Switch to Overview

        ViewState = ViewStateIndex.Overview

    End Sub

    Private Sub Switch2ParametersView()
        ' Switch to ParametersView

        ViewState = ViewStateIndex.ParametersView

    End Sub

    Private Sub UpdateView()

        UpdateMousePointBrush()

        UpdateCircleBrush()

        Dim distanceSquared As Double = CalculateDistances()

        Using g As Graphics = CreateGraphics()
            UpdateLineDisplays()
            UpdateCircleDisplays()
            UpdateTextDisplays(g, distanceSquared)
        End Using

        UpdateGridPen()

    End Sub

    Private Sub UpdateViewMouseLeave()
        ' Update view to mouse leave state

        UpdateBrushesPens2Transparent4MouseLeave()

    End Sub

    Private Sub UpdateViewMouseEnter()
        ' Update view to mouse enter state

        UpdateBrushesPens4MouseEnter()

    End Sub

    Private Sub UpdateBrushesPens2Transparent4MouseLeave()
        ' Update Brushes and Pens to transparent for mouse leave state

        UpdateCircleBrush()

        DistancePen = TransparentPen
        XYDistancePen = TransparentPen
        MousePointBrush = Brushes.Transparent

        For i As Integer = 0 To TextDisplays.Count - 1

            Dim td As TextDisplay = TextDisplays(i)
            Select Case i
                Case TextDisplayIndex.Heading
                    If ViewState = ViewStateIndex.Overview Then
                        td.Brush = Brushes.Transparent
                    End If
                Case TextDisplayIndex.Radius
                Case TextDisplayIndex.Center
                    If ViewState = ViewStateIndex.Overview Then
                        td.Brush = Brushes.Transparent
                    End If
                Case TextDisplayIndex.Footer
                    If ViewState = ViewStateIndex.Overview Then
                        td.Brush = Brushes.Transparent
                    End If
                Case Else
                    td.Brush = Brushes.Transparent
            End Select
            TextDisplays(i) = td

        Next

        For i As Integer = 0 To LineDisplays.Count - 1

            Dim ld As LineDisplay = LineDisplays(i)
            Select Case i
                Case LineDisplayIndex.RadiusLine
                Case Else
                    ld.Pen = Pens.Transparent
            End Select
            LineDisplays(i) = ld

        Next

        For i As Integer = 0 To CircleDisplays.Count - 1
            Dim ld = CircleDisplays(i)
            Select Case i
                Case CircleDisplayIndex.MousePoint
                    ld.Brush = Brushes.Transparent
                Case CircleDisplayIndex.MouseHilight
                    ld.Brush = Brushes.Transparent
            End Select
            CircleDisplays(i) = ld

        Next

    End Sub

    Private Sub UpdateBrushesPens4MouseEnter()
        ' Update Brushes and Pens for mouse enter state

        ' Mouse enter sets the stage for mouse interaction.
        ' Mouse Enter sets the brushes, pens and fonts to their normal state.






        UpdateMousePointBrush()

        DistancePen = ArrowBlack3Pen
        XYDistancePen = Orchid2Pen

        ' Update mouse text display
        Using g As Graphics = CreateGraphics()
            Dim td As TextDisplay
            Dim ThisStringSize As SizeF
            td = TextDisplays(TextDisplayIndex.Mouse)
            td.Text = If(ViewState = ViewStateIndex.ParametersView, $"X {MousePointerLocation.X}, Y {MousePointerLocation.Y}", $"Distance² {DistanceSquared}")
            td.Brush = Brushes.Black
            td.FontSize = MouseFontSize
            td.Font = New Font("Segoe UI", td.FontSize)
            ThisStringSize = g.MeasureString(td.Text, td.Font)
            td.X = If(MousePointerLocation.X + ThisStringSize.Width > ClientSize.Width, MousePointerLocation.X - ThisStringSize.Width, MousePointerLocation.X + 30)
            'td.Y = MousePointerLocation.Y
            td.Y = If(MousePointerLocation.Y + ThisStringSize.Height > ClientSize.Height, MousePointerLocation.Y - ThisStringSize.Height, MousePointerLocation.Y)

            td = TextDisplays(TextDisplayIndex.Heading)
            td.Text = If(ViewState = ViewStateIndex.ParametersView, "Parameters", $"Inside Circle {IsPointerInsideCircle}")
            td.Brush = Brushes.Black
            TextDisplays(TextDisplayIndex.Mouse) = td

            td.FontSize = HeadingFontSize
            td.Font = New Font("Segoe UI", td.FontSize)
            ThisStringSize = g.MeasureString(td.Text, td.Font)
            td.X = ClientSize.Width \ 2 - ThisStringSize.Width \ 2
            td.Y = ((CircleCenterPoint.Y - CircleRadius) \ 2) - (ThisStringSize.Height \ 2)
            TextDisplays(TextDisplayIndex.Heading) = td

            td = TextDisplays(TextDisplayIndex.Footer)
            td.Text = If(ViewState = ViewStateIndex.ParametersView, $"What is Known", $"{IsPointerInsideCircle} = {DistanceSquared} <= {RadiusSquared}")
            td.Brush = Brushes.Black

            td.FontSize = FooterFontSize
            td.Font = New Font("Segoe UI", td.FontSize)
            ThisStringSize = g.MeasureString(td.Text, td.Font)
            td.X = ClientSize.Width \ 2 - ThisStringSize.Width \ 2
            td.Y = (CircleCenterPoint.Y + CircleRadius) + (ClientSize.Height - (CircleCenterPoint.Y + CircleRadius)) \ 2 - (ThisStringSize.Height \ 2)
            TextDisplays(TextDisplayIndex.Footer) = td

        End Using

    End Sub

    Private Sub UpdateGridPen()

        ' Update the grid pen based on the current view state
        If ViewState = ViewStateIndex.Overview Then
            gridPen = Pens.Transparent
        Else
            gridPen = New Pen(Color.FromArgb(128, Color.LightGray), 2)
        End If

    End Sub

    Private Sub UpdateMousePointBrush()
        MousePointBrush = If(IsPointerInsideCircle, Brushes.Lime, Brushes.Tomato)
    End Sub

    Private Sub UpdateCircleBrush()
        CircleBrush = If(ViewState = ViewStateIndex.Overview,
                     If(IsPointerInsideCircle, Brushes.LightSkyBlue, Brushes.LightGray),
                     If(IsPointerInsideCircle, New SolidBrush(Color.FromArgb(128, Color.LightSkyBlue)), New SolidBrush(Color.FromArgb(128, Color.LightGray))))
    End Sub

    Private Function CalculateDistances() As Double
        Dim xDistance As Double = MousePointerLocation.X - CircleCenterPoint.X
        Dim yDistance As Double = MousePointerLocation.Y - CircleCenterPoint.Y
        Return xDistance * xDistance + yDistance * yDistance
    End Function

    Private Sub UpdateCircleDisplays()
        For i As Integer = 0 To CircleDisplays.Count - 1
            Dim ld = CircleDisplays(i)

            Select Case i
                Case CircleDisplayIndex.Circle
                    SetCircle(ld)
                Case CircleDisplayIndex.RadiusEndPoint
                    SetEndpoint(ld, CircleRadius)
                Case CircleDisplayIndex.CenterPoint
                    SetEndpoint(ld, 0)
                Case CircleDisplayIndex.MousePoint
                    SetMousePoint(ld)
                Case CircleDisplayIndex.MouseHilight
                    SetMouseHighlight(ld)
            End Select

            CircleDisplays(i) = ld
        Next
    End Sub

    Private Sub UpdateCircleDisplaysOnResize()
        For i As Integer = 0 To CircleDisplays.Count - 1
            Dim ld = CircleDisplays(i)

            Select Case i
                Case CircleDisplayIndex.Circle
                    SetCircle(ld)
                Case CircleDisplayIndex.RadiusEndPoint
                    SetEndpoint(ld, CircleRadius)
                Case CircleDisplayIndex.CenterPoint
                    SetEndpoint(ld, 0)
                Case CircleDisplayIndex.MousePoint
                    SetMousePoint(ld)
                Case CircleDisplayIndex.MouseHilight
                    SetMouseHighlightOnResize(ld)
            End Select

            CircleDisplays(i) = ld
        Next
    End Sub

    Private Sub SetCircle(ByRef ld As CircleDisplay)
        ld.X = CircleCenterPoint.X - CircleRadius
        ld.Y = CircleCenterPoint.Y - CircleRadius
        ld.Width = CircleRadius * 2
        ld.Height = CircleRadius * 2
        ld.Brush = CircleBrush
    End Sub

    Private Sub SetEndpoint(ByRef ld As CircleDisplay, offset As Double)
        ld.X = CircleCenterPoint.X + offset - 3
        ld.Y = CircleCenterPoint.Y - 3
        ld.Width = 6
        ld.Height = 6
        ld.Brush = RadiusBrush
    End Sub

    Private Sub SetMousePoint(ByRef ld As CircleDisplay)
        ld.X = MousePointerLocation.X - 3
        ld.Y = MousePointerLocation.Y - 3
        ld.Width = 6
        ld.Height = 6
        ld.Brush = MousePointBrush
    End Sub

    Private Sub SetMouseHighlight(ByRef ld As CircleDisplay)
        ld.X = MousePointerLocation.X - 20
        ld.Y = MousePointerLocation.Y - 20
        ld.Width = 40
        ld.Height = 40
        ld.Brush = MouseHilightBrush
    End Sub

    Private Sub SetMouseHighlightOnResize(ByRef ld As CircleDisplay)
        ld.Brush = Brushes.Transparent
    End Sub

    Private Sub UpdateTextDisplays(g As Graphics, distanceSquared As Double)

        Dim headingFont As New Font("Segoe UI", HeadingFontSize)
        Dim mouseFont As New Font("Segoe UI", MouseFontSize)
        Dim radiusFont As New Font("Segoe UI", RadiusFontSize)
        Dim centerFont As New Font("Segoe UI", CenterFontSize)
        Dim footerFont As New Font("Segoe UI", FooterFontSize)

        For i As Integer = 0 To TextDisplays.Count - 1
            Dim td As TextDisplay = TextDisplays(i)

            Select Case i
                Case TextDisplayIndex.Heading
                    UpdateHeadingText(td, g, headingFont)
                Case TextDisplayIndex.Mouse
                    UpdateMouseText()
                Case TextDisplayIndex.Center
                    UpdateCenterText(td, g, centerFont)
                Case TextDisplayIndex.Footer
                    UpdateFooterText(td, g, footerFont, distanceSquared)
                Case TextDisplayIndex.Radius
                    UpdateRadiusText(td, g, radiusFont)
            End Select

            TextDisplays(i) = td
        Next

    End Sub

    Private Sub UpdateMouseText()

        ' Update mouse text display
        Using g As Graphics = CreateGraphics()
            Dim td As TextDisplay
            Dim ThisStringSize As SizeF
            td = TextDisplays(TextDisplayIndex.Mouse)
            td.Text = If(ViewState = ViewStateIndex.ParametersView, $"X {MousePointerLocation.X}, Y {MousePointerLocation.Y}", $"Distance² {DistanceSquared}")
            td.Brush = Brushes.Black
            td.FontSize = MouseFontSize
            td.Font = New Font("Segoe UI", td.FontSize)
            ThisStringSize = g.MeasureString(td.Text, td.Font)
            td.X = MousePointerLocation.X + 30
            td.Y = MousePointerLocation.Y
            TextDisplays(TextDisplayIndex.Mouse) = td
        End Using

    End Sub

    Private Sub UpdateHeadingText(ByRef td As TextDisplay, g As Graphics, headingFont As Font)

        If ViewState = ViewStateIndex.Overview Then
            td.Text = $"Inside Circle {IsPointerInsideCircle}"
        Else
            td.Text = "Parameters"
        End If

        td.Brush = Brushes.Black
        td.FontSize = HeadingFontSize
        Dim size = g.MeasureString(td.Text, headingFont)
        td.X = ClientSize.Width \ 2 - size.Width \ 2
        td.Y = ((CircleCenterPoint.Y - CircleRadius) \ 2) - (size.Height \ 2)
        td.Font = headingFont

    End Sub

    Private Sub UpdateCenterText(ByRef td As TextDisplay, g As Graphics, centerFont As Font)
        td.Text = $"X {CircleCenterPoint.X}, Y {CircleCenterPoint.Y}"
        td.Brush = If(ViewState = ViewStateIndex.Overview, Brushes.Transparent, Brushes.Black)
        td.FontSize = CenterFontSize
        Dim size = g.MeasureString(td.Text, centerFont)
        td.X = CircleCenterPoint.X - size.Width \ 2
        td.Y = CircleCenterPoint.Y
        td.Font = centerFont
    End Sub

    Private Sub UpdateFooterText(ByRef td As TextDisplay, g As Graphics, footerFont As Font, distanceSquared As Double)
        td.Text = If(ViewState = ViewStateIndex.ParametersView, $"What is Known", $"{IsPointerInsideCircle} = {distanceSquared} <= {RadiusSquared}")
        td.Brush = If(ViewState = ViewStateIndex.Overview, Brushes.Black, Brushes.Black)
        td.FontSize = FooterFontSize
        Dim size = g.MeasureString(td.Text, footerFont)
        td.X = ClientSize.Width \ 2 - size.Width \ 2
        td.Y = (CircleCenterPoint.Y + CircleRadius) + (ClientSize.Height - (CircleCenterPoint.Y + CircleRadius)) \ 2 - (size.Height \ 2)
        td.Font = footerFont
    End Sub

    Private Sub UpdateRadiusText(ByRef td As TextDisplay, g As Graphics, radiusFont As Font)
        td.Text = If(ViewState = ViewStateIndex.ParametersView, $"Radius {CircleRadius}", $"Radius² {RadiusSquared}")
        td.FontSize = RadiusFontSize
        Dim size = g.MeasureString(td.Text, radiusFont)
        td.X = CircleCenterPoint.X + CircleRadius + 10
        td.Y = CircleCenterPoint.Y - size.Height \ 2
        td.Font = radiusFont
    End Sub

    Private Sub UpdateViewOnMouseMove()

        UpdateMousePointBrush()

        UpdateCircleBrush()

        Dim distanceSquared As Double = CalculateDistances()

        ' Update mouse text display
        Using g As Graphics = CreateGraphics()
            Dim td As TextDisplay
            Dim ThisStringSize As SizeF

            td = TextDisplays(TextDisplayIndex.Mouse)
            td.Text = If(ViewState = ViewStateIndex.ParametersView, $"X {MousePointerLocation.X}, Y {MousePointerLocation.Y}", $"Distance² {distanceSquared}")
            ThisStringSize = g.MeasureString(td.Text, td.Font)
            td.X = If(MousePointerLocation.X + 30 + ThisStringSize.Width > ClientSize.Width, MousePointerLocation.X - ThisStringSize.Width - 30, MousePointerLocation.X + 30)
            If MousePointerLocation.Y + ThisStringSize.Height \ 4 > ClientSize.Height Then
                td.Y = MousePointerLocation.Y - ThisStringSize.Height
            ElseIf MousePointerLocation.Y - ThisStringSize.Height \ 4 < ClientRectangle.Top Then
                td.Y = MousePointerLocation.Y
            Else
                td.Y = MousePointerLocation.Y - ThisStringSize.Height \ 2
            End If
            TextDisplays(TextDisplayIndex.Mouse) = td

            td = TextDisplays(TextDisplayIndex.Heading)
            td.Text = If(ViewState = ViewStateIndex.ParametersView, "Parameters", $"Inside Circle {IsPointerInsideCircle}")
            ThisStringSize = g.MeasureString(td.Text, td.Font)
            td.X = ClientSize.Width \ 2 - ThisStringSize.Width \ 2
            td.Y = ((CircleCenterPoint.Y - CircleRadius) \ 2) - (ThisStringSize.Height \ 2)
            TextDisplays(TextDisplayIndex.Heading) = td

            td = TextDisplays(TextDisplayIndex.Footer)
            td.Text = If(ViewState = ViewStateIndex.ParametersView, $"What is Known", $"{IsPointerInsideCircle} = {distanceSquared} <= {RadiusSquared}")
            ThisStringSize = g.MeasureString(td.Text, td.Font)
            td.X = ClientSize.Width \ 2 - ThisStringSize.Width \ 2
            td.Y = (CircleCenterPoint.Y + CircleRadius) + (ClientSize.Height - (CircleCenterPoint.Y + CircleRadius)) \ 2 - (ThisStringSize.Height \ 2)
            TextDisplays(TextDisplayIndex.Footer) = td

        End Using

        UpdateLineDisplays()

        UpdateCircleDisplays()

    End Sub

    Private Sub InitializeApp()
        ' Initialize the application state

        InitializePensBrushes()

        InitializeTextDisplays()

        InitializeLineDisplays()

        InitializeCircleDisplays()

    End Sub

    Private Sub InitializePensBrushes()
        ' Initialize Pens and Brushes used in the application

        gridPen = Pens.Transparent

        DistancePen = Pens.Transparent

        RadiusPen.CustomStartCap = RadiusArrowCap
        RadiusPen.CustomEndCap = RadiusArrowCap

        ArrowBlack3Pen.CustomStartCap = DistanceArrowCap
        ArrowBlack3Pen.CustomEndCap = DistanceArrowCap

        XYDistancePen = Pens.Transparent

        MousePointBrush = Brushes.Transparent

    End Sub

    Private Sub InitializeTextDisplays()
        ' Set TextDisplays to initial state

        SetTextDisplayTransparent(TextDisplayIndex.Center)
        SetTextDisplayTransparent(TextDisplayIndex.Footer)
        SetTextDisplayTransparent(TextDisplayIndex.Mouse)
        SetTextDisplayTransparent(TextDisplayIndex.Heading)
        SetTextDisplayBlack(TextDisplayIndex.Radius)

    End Sub

    Private Sub InitializeLineDisplays()
        ' Set LineDisplays to initial state

        SetLineDisplayTransparent(LineDisplayIndex.DistanceLine)
        SetLineDisplayTransparent(LineDisplayIndex.XDistanceLine)
        SetLineDisplayTransparent(LineDisplayIndex.YDistanceLine)
        SetLineDisplayPen(LineDisplayIndex.RadiusLine, RadiusPen)

    End Sub

    Private Sub InitializeCircleDisplays()
        ' Set CircleDisplays to initial state

        SetCircleDisplayTransparent(CircleDisplayIndex.MousePoint)
        SetCircleDisplayTransparent(CircleDisplayIndex.MouseHilight)
        SetCircleDisplayBrush(CircleDisplayIndex.Circle, CircleBrush)
        SetCircleDisplayBrush(CircleDisplayIndex.RadiusEndPoint, RadiusBrush)
        SetCircleDisplayBrush(CircleDisplayIndex.CenterPoint, RadiusBrush)

    End Sub

End Class
