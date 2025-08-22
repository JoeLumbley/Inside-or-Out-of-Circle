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

    Private FontFamilyName As String = "Segoe UI"

    Private InitDisplayFont = New Font(FontFamilyName, 10)

    Private InitDisplayFontSize As Integer = 10

    Private Enum TextDisplayIndex
        Heading = 0
        Mouse = 1
        Radius = 2
        Center = 3
        Footer = 4
    End Enum

    Private TextDisplays() As TextDisplay = {
        New TextDisplay(0, 0, TextDisplayIndex.Heading.ToString, Brushes.Chartreuse, InitDisplayFontSize, InitDisplayFont),
        New TextDisplay(0, 0, TextDisplayIndex.Mouse.ToString, Brushes.Chartreuse, InitDisplayFontSize, InitDisplayFont),
        New TextDisplay(0, 0, TextDisplayIndex.Radius.ToString, Brushes.Chartreuse, InitDisplayFontSize, InitDisplayFont),
        New TextDisplay(0, 0, TextDisplayIndex.Center.ToString, Brushes.Chartreuse, InitDisplayFontSize, InitDisplayFont),
        New TextDisplay(0, 0, TextDisplayIndex.Footer.ToString, Brushes.Chartreuse, InitDisplayFontSize, InitDisplayFont)
    }

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
        New LineDisplay(0, 0, 0, 0, Pens.Chartreuse),
        New LineDisplay(0, 0, 0, 0, Pens.Chartreuse),
        New LineDisplay(0, 0, 0, 0, Pens.Chartreuse),
        New LineDisplay(0, 0, 0, 0, Pens.Chartreuse),
        New LineDisplay(0, 0, 0, 0, Pens.Chartreuse),
        New LineDisplay(0, 0, 0, 0, Pens.Chartreuse),
        New LineDisplay(0, 0, 0, 0, Pens.Chartreuse),
        New LineDisplay(0, 0, 0, 0, Pens.Chartreuse)
    }

    Private Enum LineDisplayIndex
        CircleCenterVerticalLine
        MouseCenterVerticalLine
        CircleCenterHorizontallLine
        MouseCenterHorizontalLine
        RadiusLine
        XDistanceLine
        YDistanceLine
        DistanceLine
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
        New CircleDisplay(0, 0, 0, 0, Brushes.Chartreuse),
        New CircleDisplay(0, 0, 0, 0, Brushes.Chartreuse),
        New CircleDisplay(0, 0, 0, 0, Brushes.Chartreuse),
        New CircleDisplay(0, 0, 0, 0, Brushes.Chartreuse),
        New CircleDisplay(0, 0, 0, 0, Brushes.Chartreuse)
    }

    Private Enum CircleDisplayIndex
        Circle
        RadiusEndPoint
        CenterPoint
        MouseHilight
        MousePoint
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

    Private LightGray128W2Pen As New Pen(Color.FromArgb(128, Color.LightGray), 2)

    Private gridPen As Pen = LightGray128W2Pen

    Private LightGray128Brush As New SolidBrush(Color.FromArgb(128, Color.LightGray))

    Private ThisStringSize As SizeF

    Private gRuler As Graphics = CreateGraphics()

    Private Enum ViewStateIndex
        Overview
        ParametersView
        XDistanceView
        YDistanceView
        SquaredDistanceView
    End Enum

    Private ViewState As ViewStateIndex = ViewStateIndex.Overview

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load

        InitializeApp()

    End Sub

    Private Sub Form1_MouseEnter(sender As Object, e As EventArgs) Handles Me.MouseEnter

        UpdateViewMouseEnter()

        Invalidate()

        InvalidateAllButtons()

    End Sub

    Private Sub Form1_MouseLeave(sender As Object, e As EventArgs) Handles Me.MouseLeave

        UpdateViewMouseLeave()

        Invalidate()

        InvalidateAllButtons()

    End Sub

    Protected Overrides Sub OnMouseMove(e As MouseEventArgs)
        MyBase.OnMouseMove(e)

        MousePointerLocation = e.Location

        IsPointerInsideCircle = IsPointInsideCircle(e.X, e.Y, CircleCenterPoint.X, CircleCenterPoint.Y, CircleRadius)

        XDistance = e.X - CircleCenterPoint.X

        YDistance = e.Y - CircleCenterPoint.Y

        DistanceSquared = XDistance * XDistance + YDistance * YDistance

        UpdateViewOnMouseMove()

        Invalidate()

        InvalidateAllButtons()

    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)

        e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.None

        DrawGrid(e.Graphics)

        e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
        e.Graphics.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAlias

        DrawCircles(e.Graphics)

        DrawLines(e.Graphics)

        DrawTextOverlays(e.Graphics)

    End Sub

    Private Sub OverviewButton_Click(sender As Object, e As EventArgs) Handles OverviewButton.Click

        Switch2Overview()

        UpdateView()

        ' Hide overlays
        SetTextDisplayTransparent(TextDisplayIndex.Mouse)
        SetTextDisplayTransparent(TextDisplayIndex.Heading)
        SetTextDisplayTransparent(TextDisplayIndex.Footer)
        SetTextDisplayTransparent(TextDisplayIndex.Center)

        ' Hide mouse indicators
        SetCircleDisplayTransparent(CircleDisplayIndex.MousePoint)
        SetCircleDisplayTransparent(CircleDisplayIndex.MouseHilight)

        SetLineDisplayTransparent(LineDisplayIndex.CircleCenterVerticalLine)
        SetLineDisplayTransparent(LineDisplayIndex.CircleCenterHorizontallLine)

        SetLineDisplayPen(LineDisplayIndex.RadiusLine, RadiusPen)
        SetCircleDisplayBrush(CircleDisplayIndex.RadiusEndPoint, RadiusBrush)
        SetTextDisplayBlack(TextDisplayIndex.Radius)

        Invalidate()

        InvalidateAllButtons()

    End Sub

    Private Sub ParametersViewButton_Click(sender As Object, e As EventArgs) Handles ParametersViewButton.Click

        Switch2ParametersView()

        UpdateView()

        ' Hide overlays
        SetTextDisplayTransparent(TextDisplayIndex.Mouse)

        ' Hide mouse indicators
        SetCircleDisplayTransparent(CircleDisplayIndex.MousePoint)
        SetCircleDisplayTransparent(CircleDisplayIndex.MouseHilight)

        SetLineDisplayPen(LineDisplayIndex.RadiusLine, RadiusPen)
        SetLineDisplayTransparent(LineDisplayIndex.CircleCenterVerticalLine)
        SetLineDisplayTransparent(LineDisplayIndex.CircleCenterHorizontallLine)

        SetCircleDisplayBrush(CircleDisplayIndex.RadiusEndPoint, RadiusBrush)

        SetTextDisplayBlack(TextDisplayIndex.Radius)
        SetTextDisplayBlack(TextDisplayIndex.Heading)
        SetTextDisplayBlack(TextDisplayIndex.Footer)
        SetTextDisplayBlack(TextDisplayIndex.Center)

        Invalidate()

        InvalidateAllButtons()

    End Sub
    Private Sub XDistanceViewButton_Click(sender As Object, e As EventArgs) Handles XDistanceViewButton.Click

        Switch2XDistanceView()

        UpdateView()

        SetLineDisplayPen(LineDisplayIndex.CircleCenterVerticalLine, XYDistancePen)

        ' Hide overlays
        SetTextDisplayTransparent(TextDisplayIndex.Mouse)
        SetTextDisplayTransparent(TextDisplayIndex.Heading)
        SetTextDisplayTransparent(TextDisplayIndex.Footer)
        SetTextDisplayTransparent(TextDisplayIndex.Radius)

        ' Hide mouse indicators
        SetCircleDisplayTransparent(CircleDisplayIndex.MousePoint)
        SetCircleDisplayTransparent(CircleDisplayIndex.MouseHilight)
        SetCircleDisplayTransparent(CircleDisplayIndex.RadiusEndPoint)

        SetLineDisplayTransparent(LineDisplayIndex.RadiusLine)
        SetLineDisplayTransparent(LineDisplayIndex.DistanceLine)
        SetLineDisplayTransparent(LineDisplayIndex.XDistanceLine)
        SetLineDisplayTransparent(LineDisplayIndex.YDistanceLine)
        SetLineDisplayTransparent(LineDisplayIndex.CircleCenterHorizontallLine)

        SetTextDisplayBlack(TextDisplayIndex.Center)

        Invalidate()

        InvalidateAllButtons()

    End Sub

    Private Sub YDistanceViewButton_Click(sender As Object, e As EventArgs) Handles YDistanceViewButton.Click

        Switch2YDistanceView()

        UpdateView()

        ' Hide overlays
        SetTextDisplayTransparent(TextDisplayIndex.Mouse)
        SetTextDisplayTransparent(TextDisplayIndex.Heading)
        SetTextDisplayTransparent(TextDisplayIndex.Footer)
        SetTextDisplayTransparent(TextDisplayIndex.Radius)

        ' Hide mouse indicators
        SetCircleDisplayTransparent(CircleDisplayIndex.MousePoint)
        SetCircleDisplayTransparent(CircleDisplayIndex.MouseHilight)
        SetCircleDisplayTransparent(CircleDisplayIndex.RadiusEndPoint)

        SetLineDisplayTransparent(LineDisplayIndex.RadiusLine)
        SetLineDisplayTransparent(LineDisplayIndex.CircleCenterVerticalLine)

        SetLineDisplayPen(LineDisplayIndex.CircleCenterHorizontallLine, XYDistancePen)

        SetLineDisplayTransparent(LineDisplayIndex.YDistanceLine)
        SetLineDisplayPen(LineDisplayIndex.MouseCenterHorizontalLine, XYDistancePen)

        'SetLineDisplayPen(LineDisplayIndex.XDistanceLine, TransparentPen)

        SetLineDisplayTransparent(LineDisplayIndex.MouseCenterHorizontalLine)

        SetTextDisplayBlack(TextDisplayIndex.Center)

        Invalidate()

        InvalidateAllButtons()

    End Sub

    Private Sub SquaredDistanceViewButton_Click(sender As Object, e As EventArgs) Handles SquaredDistanceViewButton.Click
        ' Switch to SquaredDistanceView
        ViewState = ViewStateIndex.SquaredDistanceView

        UpdateView()

        ' Hide overlays
        SetTextDisplayTransparent(TextDisplayIndex.Mouse)
        SetTextDisplayTransparent(TextDisplayIndex.Heading)
        SetTextDisplayTransparent(TextDisplayIndex.Footer)
        SetTextDisplayTransparent(TextDisplayIndex.Radius)
        SetTextDisplayTransparent(TextDisplayIndex.Center)

        ' Hide mouse indicators
        SetCircleDisplayTransparent(CircleDisplayIndex.MousePoint)
        SetCircleDisplayTransparent(CircleDisplayIndex.MouseHilight)
        SetCircleDisplayTransparent(CircleDisplayIndex.RadiusEndPoint)

        SetLineDisplayTransparent(LineDisplayIndex.RadiusLine)
        SetLineDisplayTransparent(LineDisplayIndex.CircleCenterVerticalLine)
        SetLineDisplayTransparent(LineDisplayIndex.CircleCenterHorizontallLine)

        Invalidate()
        InvalidateAllButtons()

    End Sub

    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)

        UpdateButtonLayout()

        UpdateCircleGeometry()

        UpdateFontSizes()

        UpdateTextDisplays(DistanceSquared)

        UpdateLineDisplays()

        UpdateCircleDisplaysPostion()

        Invalidate()

        InvalidateAllButtons()

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

        ' 🔲 Draw grid ( lines every 100 pixels)
        For x As Integer = 0 To ClientSize.Width Step 100
            g.DrawLine(gridPen, x, 0, x, ClientSize.Height)
        Next
        For y As Integer = 0 To ClientSize.Height Step 100
            g.DrawLine(gridPen, 0, y, ClientSize.Width, y)
        Next

    End Sub

    Private Sub UpdateButtonLayout()

        Dim ButtonSize As Integer = Math.Max(32, Math.Min(Me.ClientSize.Width, Me.ClientSize.Height) \ 15)
        Dim Pad As Integer = 10

        SquaredDistanceViewButton.Width = ButtonSize
        SquaredDistanceViewButton.Height = ButtonSize
        SquaredDistanceViewButton.Font = New Font(FontFamilyName, Math.Max(10, Math.Min(Me.ClientSize.Width, Me.ClientSize.Height) \ 45))
        SquaredDistanceViewButton.SetBounds(ClientSize.Width - ButtonSize - Pad,
                                      ClientSize.Height - ButtonSize - Pad,
                                      ButtonSize,
                                      ButtonSize)

        YDistanceViewButton.Width = ButtonSize
        YDistanceViewButton.Height = ButtonSize
        YDistanceViewButton.Font = New Font(FontFamilyName, Math.Max(10, Math.Min(Me.ClientSize.Width, Me.ClientSize.Height) \ 45))
        YDistanceViewButton.SetBounds(ClientSize.Width - ButtonSize * 2 - Pad * 2,
                                      ClientSize.Height - ButtonSize - Pad,
                                      ButtonSize,
                                      ButtonSize)

        XDistanceViewButton.Width = ButtonSize
        XDistanceViewButton.Height = ButtonSize
        XDistanceViewButton.Font = New Font(FontFamilyName, Math.Max(10, Math.Min(Me.ClientSize.Width, Me.ClientSize.Height) \ 45))
        XDistanceViewButton.SetBounds(ClientSize.Width - ButtonSize * 3 - Pad * 3,
                                      ClientSize.Height - ButtonSize - Pad,
                                      ButtonSize,
                                      ButtonSize)

        ParametersViewButton.Width = ButtonSize
        ParametersViewButton.Height = ButtonSize
        ParametersViewButton.Font = New Font(FontFamilyName, Math.Max(9, Math.Min(Me.ClientSize.Width, Me.ClientSize.Height) \ 55))
        ParametersViewButton.SetBounds(ClientSize.Width - ButtonSize * 4 - Pad * 4,
                                       ClientSize.Height - ButtonSize - Pad,
                                       ButtonSize,
                                       ButtonSize)

        OverviewButton.Width = ButtonSize
        OverviewButton.Height = ButtonSize
        OverviewButton.Font = New Font(FontFamilyName, Math.Max(10, Math.Min(Me.ClientSize.Width, Me.ClientSize.Height) \ 45))
        OverviewButton.SetBounds(ClientSize.Width - ButtonSize * 5 - Pad * 5,
                                 ClientSize.Height - ButtonSize - Pad,
                                 ButtonSize,
                                 ButtonSize)

    End Sub

    Private Sub UpdateCircleGeometry()
        CircleCenterPoint = New Point(ClientSize.Width \ 2, ClientSize.Height \ 2)
        CircleRadius = Math.Min(ClientSize.Width, ClientSize.Height) \ 3.4
        RadiusSquared = CircleRadius * CircleRadius
    End Sub

    Private Sub UpdateFontSizes()
        Dim baseSize = Math.Min(ClientSize.Width, ClientSize.Height) \ 20

        SetTextDisplayFontSize(TextDisplayIndex.Heading, Math.Max(10, baseSize))
        SetTextDisplayFontSize(TextDisplayIndex.Mouse, Math.Max(10, baseSize))
        SetTextDisplayFontSize(TextDisplayIndex.Radius, Math.Max(10, baseSize))
        SetTextDisplayFontSize(TextDisplayIndex.Center, Math.Max(10, baseSize))
        SetTextDisplayFontSize(TextDisplayIndex.Footer, Math.Max(10, baseSize))

    End Sub

    Private Sub UpdateLineDisplays()
        For i As Integer = 0 To LineDisplays.Length - 1
            Dim ld = LineDisplays(i)

            Select Case i

                ' Circle center vertical line
                Case LineDisplayIndex.CircleCenterVerticalLine

                    SetLine(ld,
                            CircleCenterPoint.X, ClientRectangle.Top,
                            CircleCenterPoint.X, ClientRectangle.Bottom)

                Case LineDisplayIndex.CircleCenterHorizontallLine

                    SetLine(ld,
                            ClientRectangle.Left, CircleCenterPoint.Y,
                            ClientRectangle.Right, CircleCenterPoint.Y)

                Case LineDisplayIndex.MouseCenterVerticalLine

                    SetLine(ld,
                            MousePointerLocation.X, ClientRectangle.Top,
                            MousePointerLocation.X, ClientRectangle.Bottom)

                Case LineDisplayIndex.MouseCenterHorizontalLine

                    SetLine(ld,
                            ClientRectangle.Left, MousePointerLocation.Y,
                            ClientRectangle.Right, MousePointerLocation.Y)

                Case LineDisplayIndex.RadiusLine

                    SetLine(ld,
                            CircleCenterPoint.X, CircleCenterPoint.Y,
                            CircleCenterPoint.X + CircleRadius, CircleCenterPoint.Y)

                Case LineDisplayIndex.XDistanceLine

                    SetLine(ld,
                            CircleCenterPoint.X, CircleCenterPoint.Y,
                            MousePointerLocation.X, CircleCenterPoint.Y)

                Case LineDisplayIndex.YDistanceLine

                    If ViewState = ViewStateIndex.YDistanceView Then

                        SetLine(ld,
                                CircleCenterPoint.X, CircleCenterPoint.Y,
                                CircleCenterPoint.X, MousePointerLocation.Y)

                    Else

                        SetLine(ld,
                                MousePointerLocation.X, CircleCenterPoint.Y,
                                MousePointerLocation.X, MousePointerLocation.Y)

                    End If

                Case LineDisplayIndex.DistanceLine

                    SetLine(ld,
                            CircleCenterPoint.X, CircleCenterPoint.Y,
                            MousePointerLocation.X, MousePointerLocation.Y)

            End Select

            LineDisplays(i) = ld

        Next

    End Sub

    Private Sub SetLine(ByRef ld As LineDisplay, X1 As Integer, Y1 As Integer, X2 As Integer, Y2 As Integer)
        ld.X1 = X1
        ld.Y1 = Y1
        ld.X2 = X2
        ld.Y2 = Y2
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

    Private Sub SetTextDisplayFontSize(index As TextDisplayIndex, size As Integer)
        Dim td = TextDisplays(index)
        td.FontSize = size
        td.Font = New Font(FontFamilyName, size)
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

    Private Sub InvalidateAllButtons()
        InvalidateAllButtonsRecursive(Me)
    End Sub

    Private Sub InvalidateAllButtonsRecursive(parent As Control)
        For Each ctrl As Control In parent.Controls
            If TypeOf ctrl Is Button Then
                ctrl.Invalidate()
            ElseIf ctrl.HasChildren Then
                InvalidateAllButtonsRecursive(ctrl)
            End If
        Next
    End Sub

    Private Sub Switch2Overview()
        ' Switch to Overview

        ViewState = ViewStateIndex.Overview

    End Sub

    Private Sub Switch2ParametersView()
        ' Switch to ParametersView

        ViewState = ViewStateIndex.ParametersView

    End Sub

    Private Sub Switch2XDistanceView()
        ' Switch to XDistanceView

        ViewState = ViewStateIndex.XDistanceView

    End Sub

    Private Sub Switch2YDistanceView()
        ' Switch to YDistanceView

        ViewState = ViewStateIndex.YDistanceView

    End Sub

    Private Sub UpdateView()

        UpdateMousePointBrush()

        UpdateCircleBrush()

        SetCircleDisplayBrush(CircleDisplayIndex.Circle, CircleBrush)

        Dim distanceSquared As Double = CalculateDistances()

        Using g As Graphics = CreateGraphics()
            UpdateLineDisplays()
            UpdateCircleDisplaysPostion()
            UpdateTextDisplays(distanceSquared)
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

        SetCircleDisplayBrush(CircleDisplayIndex.Circle, CircleBrush)

        MousePointBrush = Brushes.Transparent

        Select Case ViewState
            Case ViewStateIndex.Overview
                SetTextDisplayTransparent(TextDisplayIndex.Heading)
                SetTextDisplayTransparent(TextDisplayIndex.Center)
                SetTextDisplayTransparent(TextDisplayIndex.Footer)
                SetTextDisplayTransparent(TextDisplayIndex.Mouse)
                SetLineDisplayTransparent(LineDisplayIndex.DistanceLine)

                SetLineDisplayTransparent(LineDisplayIndex.XDistanceLine)
                SetLineDisplayTransparent(LineDisplayIndex.YDistanceLine)

            Case ViewStateIndex.ParametersView
                SetTextDisplayTransparent(TextDisplayIndex.Mouse)

            Case ViewStateIndex.XDistanceView
                SetTextDisplayTransparent(TextDisplayIndex.Heading)
                SetTextDisplayTransparent(TextDisplayIndex.Mouse)
                SetTextDisplayTransparent(TextDisplayIndex.Footer)

                SetLineDisplayTransparent(LineDisplayIndex.MouseCenterVerticalLine)
                SetLineDisplayTransparent(LineDisplayIndex.XDistanceLine)

            Case ViewStateIndex.YDistanceView
                SetTextDisplayTransparent(TextDisplayIndex.Heading)
                SetTextDisplayTransparent(TextDisplayIndex.Mouse)
                SetTextDisplayTransparent(TextDisplayIndex.Footer)
                SetLineDisplayTransparent(LineDisplayIndex.YDistanceLine)
                SetLineDisplayTransparent(LineDisplayIndex.MouseCenterHorizontalLine)


            Case ViewStateIndex.SquaredDistanceView

                SetLineDisplayTransparent(LineDisplayIndex.DistanceLine)
                SetLineDisplayTransparent(LineDisplayIndex.XDistanceLine)
                SetLineDisplayTransparent(LineDisplayIndex.YDistanceLine)

                SetTextDisplayTransparent(TextDisplayIndex.Heading)
                SetTextDisplayTransparent(TextDisplayIndex.Mouse)
                SetTextDisplayTransparent(TextDisplayIndex.Footer)


        End Select

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

        UpdateMousePointBrush()

        Select Case ViewState

            Case ViewStateIndex.Overview
                SetLineDisplayPen(LineDisplayIndex.RadiusLine, RadiusPen)
                SetLineDisplayPen(LineDisplayIndex.DistanceLine, DistancePen)
                SetLineDisplayPen(LineDisplayIndex.XDistanceLine, XYDistancePen)
                SetLineDisplayPen(LineDisplayIndex.YDistanceLine, XYDistancePen)
                SetCircleDisplayBrush(CircleDisplayIndex.MousePoint, MousePointBrush)
                SetCircleDisplayBrush(CircleDisplayIndex.MouseHilight, MouseHilightBrush)
                SetTextDisplayBlack(TextDisplayIndex.Mouse)
                SetTextDisplayBlack(TextDisplayIndex.Heading)
                SetTextDisplayBlack(TextDisplayIndex.Footer)

            Case ViewStateIndex.ParametersView
                SetLineDisplayTransparent(LineDisplayIndex.XDistanceLine)
                SetCircleDisplayBrush(CircleDisplayIndex.MousePoint, MousePointBrush)
                SetCircleDisplayBrush(CircleDisplayIndex.MouseHilight, MouseHilightBrush)
                SetTextDisplayBlack(TextDisplayIndex.Mouse)

            Case ViewStateIndex.XDistanceView
                SetLineDisplayPen(LineDisplayIndex.XDistanceLine, DistancePen)
                SetTextDisplayTransparent(TextDisplayIndex.Radius)
                SetCircleDisplayBrush(CircleDisplayIndex.MousePoint, MousePointBrush)
                SetCircleDisplayBrush(CircleDisplayIndex.MouseHilight, MouseHilightBrush)
                SetTextDisplayBlack(TextDisplayIndex.Mouse)
                SetTextDisplayBlack(TextDisplayIndex.Heading)
                SetTextDisplayBlack(TextDisplayIndex.Footer)

                SetLineDisplayPen(LineDisplayIndex.CircleCenterVerticalLine, XYDistancePen)
                SetLineDisplayPen(LineDisplayIndex.MouseCenterVerticalLine, XYDistancePen)

            Case ViewStateIndex.YDistanceView
                SetLineDisplayPen(LineDisplayIndex.MouseCenterHorizontalLine, XYDistancePen)
                SetLineDisplayPen(LineDisplayIndex.YDistanceLine, DistancePen)

                SetTextDisplayTransparent(TextDisplayIndex.Radius)
                SetCircleDisplayBrush(CircleDisplayIndex.MousePoint, MousePointBrush)
                SetCircleDisplayBrush(CircleDisplayIndex.MouseHilight, MouseHilightBrush)
                SetTextDisplayBlack(TextDisplayIndex.Mouse)
                SetTextDisplayBlack(TextDisplayIndex.Heading)
                SetTextDisplayBlack(TextDisplayIndex.Footer)

            Case ViewStateIndex.SquaredDistanceView
                SetLineDisplayPen(LineDisplayIndex.DistanceLine, DistancePen)
                SetTextDisplayBlack(TextDisplayIndex.Heading)
                SetTextDisplayBlack(TextDisplayIndex.Footer)
                SetTextDisplayBlack(TextDisplayIndex.Mouse)

                SetCircleDisplayBrush(CircleDisplayIndex.MouseHilight, MouseHilightBrush)

                SetLineDisplayPen(LineDisplayIndex.XDistanceLine, XYDistancePen)
                SetLineDisplayPen(LineDisplayIndex.YDistanceLine, XYDistancePen)

        End Select

        UpdateTextDisplays(DistanceSquared)

    End Sub

    Private Sub UpdateGridPen()

        ' Update the grid pen based on the current view state
        Select Case ViewState
            Case ViewStateIndex.Overview
                gridPen = Pens.Transparent
            Case ViewStateIndex.ParametersView
                gridPen = LightGray128W2Pen
            Case ViewStateIndex.XDistanceView
                gridPen = LightGray128W2Pen
            Case ViewStateIndex.YDistanceView
                gridPen = LightGray128W2Pen
            Case ViewStateIndex.SquaredDistanceView
                gridPen = LightGray128W2Pen

        End Select

    End Sub

    Private Sub UpdateMousePointBrush()

        MousePointBrush = If(IsPointerInsideCircle, Brushes.Lime, Brushes.Tomato)

        SetCircleDisplayBrush(CircleDisplayIndex.MousePoint, MousePointBrush)

    End Sub

    Private Sub UpdateCircleBrush()

        Select Case ViewState
            Case ViewStateIndex.Overview
                CircleBrush = If(IsPointerInsideCircle, Brushes.LightSkyBlue, Brushes.LightGray)

            Case ViewStateIndex.ParametersView
                CircleBrush = If(IsPointerInsideCircle,
                    LightGray128Brush,
                    LightGray128Brush)

            Case ViewStateIndex.XDistanceView
                CircleBrush = If(IsPointerInsideCircle,
                    LightGray128Brush,
                    LightGray128Brush)

            Case ViewStateIndex.YDistanceView
                CircleBrush = If(IsPointerInsideCircle,
                    LightGray128Brush,
                    LightGray128Brush)

            Case ViewStateIndex.SquaredDistanceView
                CircleBrush = If(IsPointerInsideCircle,
                    LightGray128Brush,
                    LightGray128Brush)

        End Select

    End Sub

    Private Function CalculateDistances() As Double
        Dim xDistance As Double = MousePointerLocation.X - CircleCenterPoint.X
        Dim yDistance As Double = MousePointerLocation.Y - CircleCenterPoint.Y
        Return xDistance * xDistance + yDistance * yDistance
    End Function

    Private Sub UpdateCircleDisplaysPostion()

        For i As Integer = 0 To CircleDisplays.Length - 1

            Dim cd = CircleDisplays(i)

            Select Case i
                Case CircleDisplayIndex.Circle
                    SetCirclePostion(cd)
                Case CircleDisplayIndex.RadiusEndPoint
                    SetEndpointPostion(cd, CircleRadius)
                Case CircleDisplayIndex.CenterPoint
                    SetEndpointPostion(cd, 0)
                Case CircleDisplayIndex.MousePoint
                    SetMousePointPostion(cd)
                Case CircleDisplayIndex.MouseHilight
                    SetMouseHighlightPostion(cd)
            End Select

            CircleDisplays(i) = cd
        Next

    End Sub

    Private Sub SetCirclePostion(ByRef cd As CircleDisplay)
        cd.X = CircleCenterPoint.X - CircleRadius
        cd.Y = CircleCenterPoint.Y - CircleRadius
        cd.Width = CircleRadius * 2
        cd.Height = CircleRadius * 2
    End Sub

    Private Sub SetEndpointPostion(ByRef cd As CircleDisplay, offset As Double)
        cd.X = CircleCenterPoint.X + offset - 3
        cd.Y = CircleCenterPoint.Y - 3
        cd.Width = 6
        cd.Height = 6
    End Sub

    Private Sub SetMousePointPostion(ByRef cd As CircleDisplay)
        cd.X = MousePointerLocation.X - 3
        cd.Y = MousePointerLocation.Y - 3
        cd.Width = 6
        cd.Height = 6
    End Sub

    Private Sub SetMouseHighlightPostion(ByRef cd As CircleDisplay)
        cd.X = MousePointerLocation.X - 20
        cd.Y = MousePointerLocation.Y - 20
        cd.Width = 40
        cd.Height = 40
    End Sub

    Private Sub UpdateTextDisplays(distanceSquared As Double)

        'Dim headingFont As New Font("Segoe UI", HeadingFontSize)
        'Dim mouseFont As New Font("Segoe UI", MouseFontSize)
        'Dim radiusFont As New Font("Segoe UI", RadiusFontSize)
        'Dim centerFont As New Font("Segoe UI", CenterFontSize)
        'Dim footerFont As New Font("Segoe UI", FooterFontSize)

        For i As Integer = 0 To TextDisplays.Count - 1
            Dim td As TextDisplay = TextDisplays(i)

            Select Case i
                Case TextDisplayIndex.Heading
                    UpdateHeadingTextPositionContent(td)
                Case TextDisplayIndex.Mouse
                    UpdateMouseTextPositionContent(td)
                Case TextDisplayIndex.Center
                    UpdateCenterTextPositionContent(td)
                Case TextDisplayIndex.Footer
                    UpdateFooterTextPositionContent(td, distanceSquared)
                Case TextDisplayIndex.Radius
                    UpdateRadiusTextPositionContent(td)
            End Select

            TextDisplays(i) = td
        Next

    End Sub

    Private Sub UpdateMouseTextPositionContent(ByRef td As TextDisplay)
        ' Update mouse text display
        Select Case ViewState
            Case ViewStateIndex.Overview
                td.Text = $"Distance² {DistanceSquared}"
            Case ViewStateIndex.ParametersView
                td.Text = $"X {MousePointerLocation.X}, Y {MousePointerLocation.Y}"
            Case ViewStateIndex.XDistanceView
                td.Text = $"X {MousePointerLocation.X}"
            Case ViewStateIndex.YDistanceView
                td.Text = $"Y {MousePointerLocation.Y}"
            Case ViewStateIndex.SquaredDistanceView
                td.Text = $"Distance² {DistanceSquared}"

        End Select

        'td.FontSize = MouseFontSize
        'td.Font = mouseFont
        ThisStringSize = gRuler.MeasureString(td.Text, td.Font)

        td.X = If(MousePointerLocation.X + 30 + ThisStringSize.Width > ClientSize.Width, MousePointerLocation.X - ThisStringSize.Width - 30, MousePointerLocation.X + 30)
        If MousePointerLocation.Y + ThisStringSize.Height \ 4 > ClientSize.Height Then
            td.Y = MousePointerLocation.Y - ThisStringSize.Height
        ElseIf MousePointerLocation.Y - ThisStringSize.Height \ 4 < ClientRectangle.Top Then
            td.Y = MousePointerLocation.Y
        Else
            td.Y = MousePointerLocation.Y - ThisStringSize.Height \ 2
        End If
        TextDisplays(TextDisplayIndex.Mouse) = td
    End Sub

    Private Sub UpdateHeadingTextPositionContent(ByRef td As TextDisplay)
        Select Case ViewState
            Case ViewStateIndex.Overview
                td.Text = $"Inside Circle {IsPointerInsideCircle}"
            Case ViewStateIndex.ParametersView
                td.Text = "Parameters"
            Case ViewStateIndex.XDistanceView
                td.Text = $"X Distance {XDistance}"
            Case ViewStateIndex.YDistanceView
                td.Text = $"Y Distance {YDistance}"
            Case ViewStateIndex.SquaredDistanceView
                td.Text = $"Distance² = X Distance² + Y Distance²"

        End Select

        'td.FontSize = HeadingFontSize
        ThisStringSize = gRuler.MeasureString(td.Text, td.Font)
        td.X = ClientSize.Width \ 2 - ThisStringSize.Width \ 2
        td.Y = ((CircleCenterPoint.Y - CircleRadius) \ 2) - (ThisStringSize.Height \ 2)
        'td.Font = headingFont
    End Sub

    Private Sub UpdateCenterTextPositionContent(ByRef td As TextDisplay)
        Select Case ViewState
            Case ViewStateIndex.Overview
            Case ViewStateIndex.ParametersView
                td.Text = $"X {CircleCenterPoint.X}, Y {CircleCenterPoint.Y}"

            Case ViewStateIndex.XDistanceView
                td.Text = $"X {CircleCenterPoint.X}"
            Case ViewStateIndex.YDistanceView
                td.Text = $"Y {CircleCenterPoint.Y}"

        End Select

        'td.FontSize = CenterFontSize
        ThisStringSize = gRuler.MeasureString(td.Text, td.Font)
        td.X = CircleCenterPoint.X - ThisStringSize.Width \ 2
        td.Y = CircleCenterPoint.Y
        'td.Font = centerFont
    End Sub

    Private Sub UpdateFooterTextPositionContent(ByRef td As TextDisplay, distanceSquared As Double)
        Select Case ViewState
            Case ViewStateIndex.Overview
                td.Text = $"{IsPointerInsideCircle} = {distanceSquared} <= {RadiusSquared}"
            Case ViewStateIndex.ParametersView
                td.Text = $"What is Known"
            Case ViewStateIndex.XDistanceView
                td.Text = $"{XDistance} = {MousePointerLocation.X} - {CircleCenterPoint.X}"
            Case ViewStateIndex.YDistanceView
                td.Text = $"{YDistance} = {MousePointerLocation.Y} - {CircleCenterPoint.Y}"
            Case ViewStateIndex.SquaredDistanceView
                td.Text = $"{distanceSquared} = {XDistance} * {XDistance} + {YDistance} * {YDistance}"

        End Select

        'td.FontSize = FooterFontSize
        ThisStringSize = gRuler.MeasureString(td.Text, td.Font)
        td.X = ClientSize.Width \ 2 - ThisStringSize.Width \ 2
        td.Y = (CircleCenterPoint.Y + CircleRadius) + (ClientSize.Height - (CircleCenterPoint.Y + CircleRadius)) \ 2 - (ThisStringSize.Height \ 2)
        'td.Font = footerFont
    End Sub

    Private Sub UpdateRadiusTextPositionContent(ByRef td As TextDisplay)
        Select Case ViewState
            Case ViewStateIndex.Overview
                td.Text = $"Radius² {RadiusSquared}"
            Case ViewStateIndex.ParametersView
                td.Text = $"Radius {CircleRadius}"
            Case ViewStateIndex.XDistanceView

        End Select

        'td.FontSize = RadiusFontSize
        ThisStringSize = gRuler.MeasureString(td.Text, td.Font)
        td.X = CircleCenterPoint.X + CircleRadius + 10
        td.Y = CircleCenterPoint.Y - ThisStringSize.Height \ 2
        'td.Font = radiusFont
    End Sub

    Private Sub UpdateViewOnMouseMove()

        UpdateMousePointBrush()

        UpdateCircleBrush()

        SetCircleDisplayBrush(CircleDisplayIndex.Circle, CircleBrush)

        'Using g As Graphics = CreateGraphics()
        'UpdateMouseTextPositionContent(TextDisplays(TextDisplayIndex.Mouse), g, New Font("Segoe UI", MouseFontSize))
        'UpdateMouseTextPositionContent(TextDisplays(TextDisplayIndex.Mouse), g, TextDisplays(TextDisplayIndex.Mouse).Font)
        UpdateMouseTextPositionContent(TextDisplays(TextDisplayIndex.Mouse))


            'UpdateHeadingTextPositionContent(TextDisplays(TextDisplayIndex.Heading), g, New Font("Segoe UI", HeadingFontSize))
            'UpdateHeadingTextPositionContent(TextDisplays(TextDisplayIndex.Heading), g, TextDisplays(TextDisplayIndex.Heading).Font)
            UpdateHeadingTextPositionContent(TextDisplays(TextDisplayIndex.Heading))

            'UpdateCenterTextPositionContent(TextDisplays(TextDisplayIndex.Center), g, New Font("Segoe UI", CenterFontSize))
            'UpdateCenterTextPositionContent(TextDisplays(TextDisplayIndex.Center), g, TextDisplays(TextDisplayIndex.Center).Font)
            UpdateCenterTextPositionContent(TextDisplays(TextDisplayIndex.Center))


            'UpdateFooterTextPositionContent(TextDisplays(TextDisplayIndex.Footer), g, New Font("Segoe UI", FooterFontSize), DistanceSquared)
            'UpdateFooterTextPositionContent(TextDisplays(TextDisplayIndex.Footer), g, TextDisplays(TextDisplayIndex.Footer).Font, DistanceSquared)
            UpdateFooterTextPositionContent(TextDisplays(TextDisplayIndex.Footer), DistanceSquared)

        'End Using

        UpdateLineDisplays()

        UpdateCircleDisplaysPostion()

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

        DistancePen = ArrowBlack3Pen
        XYDistancePen = Orchid2Pen

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
        SetLineDisplayTransparent(LineDisplayIndex.MouseCenterVerticalLine)
        SetLineDisplayTransparent(LineDisplayIndex.CircleCenterVerticalLine)
        SetLineDisplayTransparent(LineDisplayIndex.MouseCenterHorizontalLine)
        SetLineDisplayTransparent(LineDisplayIndex.CircleCenterHorizontallLine)

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
