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

    Private TextDisplays As New List(Of TextDisplay) From {
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

    Private LineDisplays As New List(Of LineDisplay) From {
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

    Private CircleDisplays As New List(Of CircleDisplay) From {
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
    Private CombinedText As String = String.Empty

    Dim circleInfo = TextDisplays
    Dim mouseInfo = TextDisplays
    Dim resultInfo = TextDisplays

    'Dim groupedText As String = String.Empty

    'Private ThisFontSize As Integer = 10
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

        gridPen = Pens.Transparent

        DistancePen = TransparentPen

        RadiusPen.CustomStartCap = RadiusArrowCap
        RadiusPen.CustomEndCap = RadiusArrowCap
        ArrowBlack3Pen.CustomStartCap = DistanceArrowCap
        ArrowBlack3Pen.CustomEndCap = DistanceArrowCap
        XYDistancePen = TransparentPen
        MousePointBrush = Brushes.Transparent

        For i As Integer = 0 To TextDisplays.Count - 1

            Dim td As TextDisplay = TextDisplays(i)
            Select Case i
                Case TextDisplayIndex.Radius
                    td.Text = $"Radius² {RadiusSquared}"

                    td.Brush = Brushes.Black

                Case Else
                    td.Brush = Brushes.Transparent
            End Select

            TextDisplays(i) = td

        Next

        For i As Integer = 0 To LineDisplays.Count - 1

            Dim ld As LineDisplay = LineDisplays(i)
            Select Case i
                Case LineDisplayIndex.RadiusLine
                    ld.X1 = CircleCenterPoint.X
                    ld.Y1 = CircleCenterPoint.Y
                    ld.X2 = CircleCenterPoint.X + CircleRadius
                    ld.Y2 = CircleCenterPoint.Y
                    ld.Pen = RadiusPen
                Case Else
                    ld.X1 = 0
                    ld.Y1 = 0
                    ld.X2 = 0
                    ld.Y2 = 0
                    ld.Pen = Pens.Transparent
            End Select

            LineDisplays(i) = ld

        Next

        For i As Integer = 0 To CircleDisplays.Count - 1
            Dim ld = CircleDisplays(i)
            Select Case i
                Case CircleDisplayIndex.Circle
                    ld.X = CircleCenterPoint.X - CircleRadius
                    ld.Y = CircleCenterPoint.Y - CircleRadius
                    ld.Width = CircleRadius * 2
                    ld.Height = CircleRadius * 2
                    ld.Brush = CircleBrush
                Case CircleDisplayIndex.RadiusEndPoint
                    ld.X = CircleCenterPoint.X + CircleRadius - 3
                    ld.Y = CircleCenterPoint.Y - 3
                    ld.Width = 6
                    ld.Height = 6
                    ld.Brush = RadiusBrush
                Case CircleDisplayIndex.CenterPoint
                    ld.X = CircleCenterPoint.X - 3
                    ld.Y = CircleCenterPoint.Y - 3
                    ld.Width = 6
                    ld.Height = 6
                    ld.Brush = RadiusBrush
                Case CircleDisplayIndex.MousePoint
                    ld.Brush = Brushes.Transparent
                Case CircleDisplayIndex.MouseHilight
                    ld.Brush = Brushes.Transparent

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
            Select Case i
                Case TextDisplayIndex.Center
                Case TextDisplayIndex.Footer
                Case Else
                    td.Brush = Brushes.Black
                    TextDisplays(i) = td

            End Select

        Next

        For i As Integer = 0 To LineDisplays.Count - 1
            Dim ld = LineDisplays(i)
            Select Case i
                Case LineDisplayIndex.RadiusLine
                    ld.X1 = CircleCenterPoint.X
                    ld.Y1 = CircleCenterPoint.Y
                    ld.X2 = CircleCenterPoint.X + CircleRadius
                    ld.Y2 = CircleCenterPoint.Y
                    ld.Pen = RadiusPen
                Case LineDisplayIndex.XDistanceLine
                    ld.X1 = CircleCenterPoint.X
                    ld.Y1 = CircleCenterPoint.Y
                    ld.X2 = MousePointerLocation.X
                    ld.Y2 = CircleCenterPoint.Y
                    ld.Pen = XYDistancePen
                Case LineDisplayIndex.YDistanceLine
                    ld.X1 = MousePointerLocation.X
                    ld.Y1 = CircleCenterPoint.Y
                    ld.X2 = MousePointerLocation.X
                    ld.Y2 = MousePointerLocation.Y
                    ld.Pen = XYDistancePen
                Case LineDisplayIndex.DistanceLine
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
                Case CircleDisplayIndex.Circle
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

            Select Case i
                Case TextDisplayIndex.Radius
                Case TextDisplayIndex.Center
                    If ViewState = ViewStateIndex.Overview Then
                        td.Brush = Brushes.Transparent
                    Else
                        td.Brush = Brushes.Black

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
                    ld.X1 = CircleCenterPoint.X
                    ld.Y1 = CircleCenterPoint.Y
                    ld.X2 = CircleCenterPoint.X + CircleRadius
                    ld.Y2 = CircleCenterPoint.Y
                    ld.Pen = RadiusPen
                Case Else
                    ld.X1 = 0
                    ld.Y1 = 0
                    ld.X2 = 0
                    ld.Y2 = 0
                    ld.Pen = Pens.Transparent
            End Select
            LineDisplays(i) = ld
        Next

        For i As Integer = 0 To CircleDisplays.Count - 1
            Dim ld = CircleDisplays(i)
            Select Case i
                Case CircleDisplayIndex.Circle
                    ld.X = CircleCenterPoint.X - CircleRadius
                    ld.Y = CircleCenterPoint.Y - CircleRadius
                    ld.Width = CircleRadius * 2
                    ld.Height = CircleRadius * 2
                    ld.Brush = CircleBrush
                Case CircleDisplayIndex.RadiusEndPoint
                    ld.X = CircleCenterPoint.X + CircleRadius - 3
                    ld.Y = CircleCenterPoint.Y - 3
                    ld.Width = 6
                    ld.Height = 6
                    ld.Brush = RadiusBrush
                Case CircleDisplayIndex.CenterPoint
                    ld.X = CircleCenterPoint.X - 3
                    ld.Y = CircleCenterPoint.Y - 3
                    ld.Width = 6
                    ld.Height = 6
                    ld.Brush = RadiusBrush
                Case CircleDisplayIndex.MousePoint
                    ld.Brush = Brushes.Transparent
                Case CircleDisplayIndex.MouseHilight
                    ld.Brush = Brushes.Transparent

            End Select

            CircleDisplays(i) = ld

        Next

        Invalidate()
        OverviewButton.Invalidate()
        ParametersViewButton.Invalidate()

    End Sub

    Protected Overrides Sub OnMouseMove(e As MouseEventArgs)
        MyBase.OnMouseMove(e)

        MousePointerLocation = e.Location

        IsPointerInsideCircle = IsPointInsideCircle(e.X, e.Y, CircleCenterPoint.X, CircleCenterPoint.Y, CircleRadius)

        UpdateView()

        Invalidate()
        OverviewButton.Invalidate()
        ParametersViewButton.Invalidate()

    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)

        e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.None

        ' 🔲 Draw grid (light gray lines every 20 pixels)
        'Dim gridPen As New Pen(Color.PowderBlue, 1)
        For x As Integer = 0 To ClientSize.Width Step 50
            e.Graphics.DrawLine(gridPen, x, 0, x, ClientSize.Height)
        Next
        For y As Integer = 0 To ClientSize.Height Step 50
            e.Graphics.DrawLine(gridPen, 0, y, ClientSize.Width, y)
        Next

        e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
        e.Graphics.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAlias

        ' 🟢 Draw filled circles
        For Each circleDisplay As CircleDisplay In CircleDisplays
            e.Graphics.FillEllipse(circleDisplay.Brush, circleDisplay.X, circleDisplay.Y, circleDisplay.Width, circleDisplay.Height)
        Next

        ' 📏 Draw lines
        For Each lineDisplay As LineDisplay In LineDisplays
            e.Graphics.DrawLine(lineDisplay.Pen, lineDisplay.X1, lineDisplay.Y1, lineDisplay.X2, lineDisplay.Y2)
        Next

        ' 🔤 Draw text overlays
        For Each textDisplay As TextDisplay In TextDisplays
            e.Graphics.DrawString(textDisplay.Text, textDisplay.Font, textDisplay.Brush, textDisplay.X, textDisplay.Y)
        Next

    End Sub

    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)

        ParametersViewButton.Left = Me.ClientSize.Width - 100
        ParametersViewButton.Top = Me.ClientSize.Height - 100
        ParametersViewButton.Width = 90
        ParametersViewButton.Height = 90

        OverviewButton.Left = Me.ClientSize.Width - 200
        OverviewButton.Top = Me.ClientSize.Height - 100
        OverviewButton.Width = 90
        OverviewButton.Height = 90

        CircleCenterPoint = New Point(Me.ClientSize.Width \ 2, Me.ClientSize.Height \ 2)

        CircleRadius = Math.Min(Me.ClientSize.Width, Me.ClientSize.Height) \ 3.4

        RadiusSquared = CircleRadius * CircleRadius

        'ThisFontSize = Math.Max(5, Me.ClientSize.Width \ 50)

        MouseFontSize = Math.Max(10, Math.Min(Me.ClientSize.Width, Me.ClientSize.Height) \ 20)

        RadiusFontSize = Math.Max(10, Math.Min(Me.ClientSize.Width, Me.ClientSize.Height) \ 20)

        CenterFontSize = Math.Max(10, Math.Min(Me.ClientSize.Width, Me.ClientSize.Height) \ 20)

        HeadingFontSize = Math.Max(10, Math.Min(Me.ClientSize.Width, Me.ClientSize.Height) \ 20)
        FooterFontSize = Math.Max(10, Math.Min(Me.ClientSize.Width, Me.ClientSize.Height) \ 20)

        Dim radiusFont As New Font("Segoe UI", RadiusFontSize)

        Using g As Graphics = CreateGraphics()
            For i As Integer = 0 To TextDisplays.Count - 1
                Dim td = TextDisplays(i)

                If i = TextDisplayIndex.Radius Then
                    If ViewState = ViewStateIndex.Overview Then
                        td.Text = $"Radius² {RadiusSquared}"
                    Else
                        td.Text = $"Radius {CircleRadius}"
                    End If

                    'td.Text = $"Radius² {RadiusSquared}"

                    Dim ThisFontHeight As Single = g.MeasureString(td.Text, radiusFont).Height
                    td.FontSize = RadiusFontSize
                    td.Font = New Font("Segoe UI", RadiusFontSize)
                    td.X = CircleCenterPoint.X + CircleRadius + 10
                    td.Y = CircleCenterPoint.Y - ThisFontHeight \ 2
                End If

                If i = TextDisplayIndex.Center Then
                    If ViewState = ViewStateIndex.Overview Then
                        td.Text = $"X {CircleCenterPoint.X}, Y {CircleCenterPoint.Y}"
                        td.Brush = Brushes.Transparent

                    Else
                        td.Text = $"X {CircleCenterPoint.X}, Y {CircleCenterPoint.Y}"
                        td.Brush = Brushes.Black

                    End If
                    Dim centerFont As New Font("Segoe UI", CenterFontSize)

                    td.FontSize = CenterFontSize
                    Dim size = g.MeasureString(td.Text, centerFont)
                    td.X = CircleCenterPoint.X - size.Width \ 2
                    td.Y = CircleCenterPoint.Y
                    td.Font = centerFont

                End If

                TextDisplays(i) = td
            Next
        End Using

        For i As Integer = 0 To LineDisplays.Count - 1
            Dim ld = LineDisplays(i)
            Select Case i
                Case LineDisplayIndex.RadiusLine
                    ld.X1 = CircleCenterPoint.X
                    ld.Y1 = CircleCenterPoint.Y
                    ld.X2 = CircleCenterPoint.X + CircleRadius
                    ld.Y2 = CircleCenterPoint.Y
                Case LineDisplayIndex.XDistanceLine
                    ld.X1 = CircleCenterPoint.X
                    ld.Y1 = CircleCenterPoint.Y
                    ld.X2 = MousePointerLocation.X
                    ld.Y2 = CircleCenterPoint.Y
                Case LineDisplayIndex.YDistanceLine
                    ld.X1 = MousePointerLocation.X
                    ld.Y1 = CircleCenterPoint.Y
                    ld.X2 = MousePointerLocation.X
                    ld.Y2 = MousePointerLocation.Y
                Case LineDisplayIndex.DistanceLine
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
                Case CircleDisplayIndex.Circle
                    ld.X = CircleCenterPoint.X - CircleRadius
                    ld.Y = CircleCenterPoint.Y - CircleRadius
                    ld.Width = CircleRadius * 2
                    ld.Height = CircleRadius * 2
                Case CircleDisplayIndex.RadiusEndPoint
                    ld.X = CircleCenterPoint.X + CircleRadius - 3
                    ld.Y = CircleCenterPoint.Y - 3
                    ld.Width = 6
                    ld.Height = 6
                    ld.Brush = RadiusBrush
                Case CircleDisplayIndex.CenterPoint
                    ld.X = CircleCenterPoint.X - 3
                    ld.Y = CircleCenterPoint.Y - 3
                    ld.Width = 6
                    ld.Height = 6
                    ld.Brush = RadiusBrush

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

    Private Sub OverviewButton_Click(sender As Object, e As EventArgs) Handles OverviewButton.Click
        ViewState = ViewStateIndex.Overview

        UpdateView()

        Invalidate()

    End Sub

    Private Sub ParametersViewButton_Click(sender As Object, e As EventArgs) Handles ParametersViewButton.Click
        ViewState = ViewStateIndex.ParametersView

        UpdateView()

        Invalidate()

    End Sub

    ' update the grid brush in ParametersView
    Private Sub UpdateGridPen()

        ' Update the grid pen based on the current view state
        If ViewState = ViewStateIndex.Overview Then
            gridPen = Pens.Transparent
        Else
            gridPen = Pens.LightGray
        End If

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

    Private Sub UpdateLineDisplays()
        For i As Integer = 0 To LineDisplays.Count - 1
            Dim ld = LineDisplays(i)

            Select Case i
                Case LineDisplayIndex.RadiusLine
                    SetRadiusLine(ld)
                Case LineDisplayIndex.XDistanceLine
                    SetDistanceLine(ld, True)
                Case LineDisplayIndex.YDistanceLine
                    SetDistanceLine(ld, False)
                Case LineDisplayIndex.DistanceLine
                    SetDistanceLine(ld, True, True)
            End Select

            LineDisplays(i) = ld
        Next
    End Sub

    Private Sub SetRadiusLine(ByRef ld As LineDisplay)
        ld.X1 = CircleCenterPoint.X
        ld.Y1 = CircleCenterPoint.Y
        ld.X2 = CircleCenterPoint.X + CircleRadius
        ld.Y2 = CircleCenterPoint.Y
    End Sub

    Private Sub SetDistanceLine(ByRef ld As LineDisplay, isXLine As Boolean, Optional isDistanceLine As Boolean = False)
        If ViewState = ViewStateIndex.Overview Then
            ld.Pen = If(isDistanceLine, DistancePen, XYDistancePen)
        Else
            ld.Pen = Pens.Transparent
        End If

        ld.X1 = If(isXLine, CircleCenterPoint.X, MousePointerLocation.X)
        ld.Y1 = CircleCenterPoint.Y
        ld.X2 = If(isXLine, MousePointerLocation.X, MousePointerLocation.X)
        ld.Y2 = If(isXLine, CircleCenterPoint.Y, MousePointerLocation.Y)
        If isDistanceLine Then
            ld.X2 = MousePointerLocation.X
            ld.Y2 = MousePointerLocation.Y
        End If
    End Sub

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
                    UpdateMouseText(td, g, mouseFont, distanceSquared)
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

    Private Sub UpdateMouseText(ByRef td As TextDisplay, g As Graphics, mouseFont As Font, distanceSquared As Double)
        If ViewState = ViewStateIndex.Overview Then
            td.Text = $"Distance² {distanceSquared}"
        Else
            td.Text = $"X {MousePointerLocation.X}, Y {MousePointerLocation.Y}"
        End If

        td.Brush = Brushes.Black
        td.FontSize = MouseFontSize
        Dim size = g.MeasureString(td.Text, mouseFont)
        td.X = MousePointerLocation.X + 30
        td.Y = MousePointerLocation.Y - size.Height \ 2
        td.Font = mouseFont
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
        td.Text = $"{IsPointerInsideCircle} = {distanceSquared} <= {RadiusSquared}"
        td.Brush = If(ViewState = ViewStateIndex.Overview, Brushes.Black, Brushes.Transparent)
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

    Private Sub OverviewButton_MouseMove(sender As Object, e As MouseEventArgs) Handles OverviewButton.MouseMove
        OverviewButton.Invalidate()
        ParametersViewButton.Invalidate()

    End Sub

    Private Sub ParametersViewButton_MouseMove(sender As Object, e As MouseEventArgs) Handles ParametersViewButton.MouseMove
        OverviewButton.Invalidate()
        ParametersViewButton.Invalidate()

    End Sub

End Class
