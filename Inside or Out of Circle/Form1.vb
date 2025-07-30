
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
        Public Sub New(x As Integer, y As Integer, text As String, brush As SolidBrush, fontSize As Integer)
            Me.X = x
            Me.Y = y
            Me.Text = text
            Me.Brush = brush
            Me.FontSize = fontSize
        End Sub
    End Structure

    Private TextDisplays As New List(Of TextDisplay) From {
        New TextDisplay(0, 0, "Heading", Brushes.Transparent, 10),
        New TextDisplay(0, 0, "Mouse", Brushes.Transparent, 10),
        New TextDisplay(0, 0, "Radius", Brushes.Transparent, 10),
        New TextDisplay(0, 0, "Center", Brushes.Transparent, 10),
        New TextDisplay(0, 0, "Footer", Brushes.Transparent, 10)
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
        MouseHight = 3
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

    'Private HeadingDisplay As New TextDisplay(10, 10, "Inside or Out of Circle", Brushes.Black)


    Dim circleInfo = TextDisplays
    Dim mouseInfo = TextDisplays
    Dim resultInfo = TextDisplays

    Dim groupedText As String = String.Empty

    'Private Font As New Font("Segoe UI", 10)
    Private ThisFontSize As Integer = 10
    Private HeadingFontSize As Integer = 16
    Private MouseFontSize As Integer = 12
    Private RadiusFontSize As Integer = 12
    Private CenterFontSize As Integer = 12
    Private FooterFontSize As Integer = 10


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load

        'StartPosition = FormStartPosition.CenterScreen
        'ClientSize = New Size(800, 600)

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

            'td.Brush = Brushes.Transparent
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
                Case CircleDisplayIndex.MouseHight
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

            Select Case i
                Case TextDisplayIndex.Radius
                Case Else
                    Dim td As TextDisplay = TextDisplays(i)

                    td.Brush = Brushes.Transparent
                    TextDisplays(i) = td

            End Select



        Next

        'For i As Integer = 0 To LineDisplays.Count - 1

        '    Dim ld As LineDisplay = LineDisplays(i)

        '    ld.Pen = Pens.Transparent

        '    LineDisplays(i) = ld

        'Next

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


        'For i As Integer = 0 To CircleDisplays.Count - 1
        '    Dim ld = CircleDisplays(i)
        '    Select Case i
        '        Case CircleDisplayIndex.Circle
        '            'ld.X = CircleCenterPoint.X - CircleRadius
        '            'ld.Y = CircleCenterPoint.Y - CircleRadius
        '            'ld.Width = CircleRadius * 2
        '            'ld.Height = CircleRadius * 2
        '            ld.Brush = CircleBrush
        '        Case CircleDisplayIndex.RadiusEndPoint
        '            'ld.X = CircleCenterPoint.X + CircleRadius - 3
        '            'ld.Y = CircleCenterPoint.Y - 3
        '            'ld.Width = 6
        '            'ld.Height = 6
        '            ld.Brush = Brushes.Transparent
        '        Case CircleDisplayIndex.CenterPoint
        '            'ld.X = CircleCenterPoint.X - 3
        '            'ld.Y = CircleCenterPoint.Y - 3
        '            'ld.Width = 6
        '            'ld.Height = 6
        '            ld.Brush = Brushes.Transparent
        '        Case CircleDisplayIndex.MousePoint
        '            'ld.X = MousePointerLocation.X - 3
        '            'ld.Y = MousePointerLocation.Y - 3
        '            'ld.Width = 6
        '            'ld.Height = 6
        '            ld.Brush = Brushes.Transparent
        '        Case CircleDisplayIndex.MouseHight
        '            'ld.X = MousePointerLocation.X - 3
        '            'ld.Y = MousePointerLocation.Y - 3
        '            'ld.Width = 6
        '            'ld.Height = 6
        '            ld.Brush = Brushes.Transparent

        '    End Select

        '    CircleDisplays(i) = ld

        'Next

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
                Case CircleDisplayIndex.MouseHight
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
        MousePointBrush = If(IsPointerInsideCircle, Brushes.LimeGreen, Brushes.Tomato)
        CircleBrush = If(IsPointerInsideCircle, Brushes.LightSkyBlue, Brushes.LightGray)
        'MouseHilightBrush = If(IsPointerInsideCircle, New SolidBrush(Color.FromArgb(128, Color.Yellow)), New SolidBrush(Color.FromArgb(255, Color.LightGray)))

        XDistance = MousePointerLocation.X - CircleCenterPoint.X
        YDistance = MousePointerLocation.Y - CircleCenterPoint.Y
        DistanceSquared = XDistance * XDistance + YDistance * YDistance


        For i As Integer = 0 To TextDisplays.Count - 1
            Dim td = TextDisplays(i)
            Select Case i
                Case TextDisplayIndex.Heading
                    td.Text = $"Inside Circle {IsPointerInsideCircle}"
                    td.Brush = Brushes.Black
                    td.FontSize = HeadingFontSize
                    Dim graphicsUni As Graphics = CreateGraphics()
                    Dim ThisFontWidth As Single = graphicsUni.MeasureString($"Inside Circle {IsPointerInsideCircle}", New Font("Segoe UI", HeadingFontSize)).Width
                    Dim ThisFontHeight As Single = graphicsUni.MeasureString($"Inside Circle {IsPointerInsideCircle}", New Font("Segoe UI", HeadingFontSize)).Height

                    td.X = ClientSize.Width \ 2 - ThisFontWidth \ 2
                    td.Y = ((CircleCenterPoint.Y - CircleRadius) \ 2) - (ThisFontHeight \ 2)

                Case TextDisplayIndex.Mouse
                    td.Text = $"Distance² {DistanceSquared}"
                    Dim graphicsUni As Graphics = CreateGraphics()
                    Dim ThisFontHeight As Single = graphicsUni.MeasureString($"Distance² {DistanceSquared}", New Font("Segoe UI", MouseFontSize)).Height
                    td.FontSize = MouseFontSize
                    td.X = MousePointerLocation.X + 30

                    td.Y = MousePointerLocation.Y - ThisFontHeight \ 2
                Case TextDisplayIndex.Radius
                    td.Text = $"Radius² {RadiusSquared}"
                    Dim graphicsUni As Graphics = CreateGraphics()
                    Dim ThisFontHeight As Single = graphicsUni.MeasureString($"Radius² {RadiusSquared}", New Font("Segoe UI", RadiusFontSize)).Height
                    td.FontSize = RadiusFontSize

                    td.X = CircleCenterPoint.X + CircleRadius + 10
                    td.Y = CircleCenterPoint.Y - ThisFontHeight \ 2
                Case TextDisplayIndex.Center
                    td.Text = $"X {CircleCenterPoint.X}, Y {CircleCenterPoint.Y}"
                    Dim graphicsUni As Graphics = CreateGraphics()
                    Dim ThisFontWidth As Single = graphicsUni.MeasureString($"X {CircleCenterPoint.X}, Y {CircleCenterPoint.Y}", New Font("Segoe UI", CenterFontSize)).Width
                    td.FontSize = CenterFontSize

                    td.X = CircleCenterPoint.X - ThisFontWidth \ 2
                    td.Y = CircleCenterPoint.Y

                Case TextDisplayIndex.Footer





                    td.Text = $"{IsPointerInsideCircle} = {DistanceSquared} <= {RadiusSquared}"
                    td.Brush = Brushes.Black
                    td.FontSize = FooterFontSize
                    Dim graphicsUni As Graphics = CreateGraphics()
                    Dim ThisFontWidth As Single = graphicsUni.MeasureString($"{IsPointerInsideCircle} = {DistanceSquared} <= {RadiusSquared}", New Font("Segoe UI", FooterFontSize)).Width
                    Dim ThisFontHeight As Single = graphicsUni.MeasureString($"{IsPointerInsideCircle} = {DistanceSquared} <= {RadiusSquared}", New Font("Segoe UI", FooterFontSize)).Height

                    td.X = ClientSize.Width \ 2 - ThisFontWidth \ 2
                    'td.Y = ((CircleCenterPoint.Y - CircleRadius) \ 2) - (ThisFontHeight \ 2)
                    'td.Y = ClientSize.Height - ThisFontHeight - 10
                    td.Y = (CircleCenterPoint.Y + CircleRadius) + (ClientSize.Height - (CircleCenterPoint.Y + CircleRadius)) \ 2 - (ThisFontHeight \ 2)

            End Select

            TextDisplays(i) = td

        Next

        'CombinedText = String.Join(Environment.NewLine, TextDisplays.Select(Function(td) td.Text))

        '    circleInfo = New TextDisplays.Where(Function(td, i) i <= TextDisplayIndex.Center).Select(Function(td) td.Text)
        '    mouseInfo = TextDisplays.Where(Function(td, i) i >= TextDisplayIndex.Mouse AndAlso i <= TextDisplayIndex.DistanceSquared).Select(Function(td) td.Text)
        '    resultInfo = TextDisplays.Where(Function(td, i) i >= TextDisplayIndex.InsideCircle).Select(Function(td) td.Text)

        '    groupedText = String.Join(Environment.NewLine & Environment.NewLine,
        '{String.Join(Environment.NewLine, circleInfo),
        ' String.Join(Environment.NewLine, mouseInfo),
        ' String.Join(Environment.NewLine, resultInfo)})


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
                    ld.X = MousePointerLocation.X - 3
                    ld.Y = MousePointerLocation.Y - 3
                    ld.Width = 6
                    ld.Height = 6
                    ld.Brush = MousePointBrush
                Case CircleDisplayIndex.MouseHight
                    ld.X = MousePointerLocation.X - 20
                    ld.Y = MousePointerLocation.Y - 20
                    ld.Width = 40
                    ld.Height = 40
                    ld.Brush = MouseHilightBrush

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
            e.Graphics.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAlias
            e.Graphics.DrawString(textDisplay.Text, New Font("Segoe UI", textDisplay.FontSize), textDisplay.Brush, textDisplay.X, textDisplay.Y)
        Next

        'e.Graphics.DrawString(CombinedText, New Font("Segoe UI", 10), Brushes.Black, 20, 20)
        'e.Graphics.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAlias
        'Dim FontHeight As Single = e.Graphics.MeasureString($"Radius² {RadiusSquared}", New Font("Segoe UI", fontSize)).Height
        'e.Graphics.DrawString($"Radius²: {RadiusSquared}", New Font("Segoe UI", fontSize), Brushes.Black, CircleCenterPoint.X + CircleRadius + 10, CircleCenterPoint.Y - FontHeight \ 4)
        'e.Graphics.DrawString($"Radius² {RadiusSquared}", New Font("Segoe UI", fontSize), Brushes.Black, CircleCenterPoint.X + CircleRadius + 10, CircleCenterPoint.Y - FontHeight \ 2)

        'FontHeight = e.Graphics.MeasureString($"Distance² {DistanceSquared}", New Font("Segoe UI", fontSize)).Height

        'e.Graphics.DrawString($"Distance² {DistanceSquared}", New Font("Segoe UI", fontSize), Brushes.Black, MousePointerLocation.X + 30, MousePointerLocation.Y - FontHeight \ 2)


        'FontHeight = e.Graphics.MeasureString($"Inside Circle {IsPointerInsideCircle}", New Font("Segoe UI", fontSize)).Height
        'Dim FontWidth As Single = e.Graphics.MeasureString($"Inside Circle {IsPointerInsideCircle}", New Font("Segoe UI", fontSize)).Width
        'e.Graphics.DrawString($"Inside Circle {IsPointerInsideCircle}", New Font("Segoe UI", fontSize), Brushes.Black, ClientSize.Width \ 2 - FontWidth \ 2, 20)

    End Sub

    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)

        CircleCenterPoint = New Point(Me.ClientSize.Width \ 2, Me.ClientSize.Height \ 2)

        CircleRadius = Math.Min(Me.ClientSize.Width, Me.ClientSize.Height) \ 3.4

        RadiusSquared = CircleRadius * CircleRadius

        ThisFontSize = Math.Max(5, Me.ClientSize.Width \ 50)

        MouseFontSize = Math.Max(10, Math.Min(Me.ClientSize.Width, Me.ClientSize.Height) \ 20)


        'RadiusFontSize = Math.Max(1, Me.ClientSize.Width \ 50)
        RadiusFontSize = Math.Max(10, Math.Min(Me.ClientSize.Width, Me.ClientSize.Height) \ 20)

        'Math.Min(Me.ClientSize.Width, Me.ClientSize.Height)

        CenterFontSize = Math.Max(5, Me.ClientSize.Width \ 50)
        HeadingFontSize = Math.Max(10, Math.Min(Me.ClientSize.Width, Me.ClientSize.Height) \ 20)
        FooterFontSize = Math.Max(10, Math.Min(Me.ClientSize.Width, Me.ClientSize.Height) \ 20)


        For i As Integer = 0 To TextDisplays.Count - 1
            Dim td = TextDisplays(i)
            Select Case i
                'Case TextDisplayIndex.Heading
                '    td.Text = $"Inside Circle {IsPointerInsideCircle}"
                '    td.Brush = Brushes.Black
                '    td.FontSize = HeadingFontSize
                '    Dim graphicsUni As Graphics = CreateGraphics()
                '    Dim ThisFontWidth As Single = graphicsUni.MeasureString($"Inside Circle {IsPointerInsideCircle}", New Font("Segoe UI", HeadingFontSize)).Width
                '    Dim ThisFontHeight As Single = graphicsUni.MeasureString($"Inside Circle {IsPointerInsideCircle}", New Font("Segoe UI", HeadingFontSize)).Height

                '    td.X = ClientSize.Width \ 2 - ThisFontWidth \ 2
                '    td.Y = ((CircleCenterPoint.Y - CircleRadius) \ 2) - (ThisFontHeight \ 2)

                'Case TextDisplayIndex.Mouse
                '    td.Text = $"X {MousePointerLocation.X},Y {MousePointerLocation.Y}"
                '    Dim graphicsUni As Graphics = CreateGraphics()
                '    Dim ThisFontHeight As Single = graphicsUni.MeasureString($"X {MousePointerLocation.X},Y {MousePointerLocation.Y}", New Font("Segoe UI", MouseFontSize)).Height
                '    td.FontSize = MouseFontSize
                '    td.X = MousePointerLocation.X + 30

                '    td.Y = MousePointerLocation.Y - ThisFontHeight \ 2
                Case TextDisplayIndex.Radius
                    'td.Text = $"Radius: {CircleRadius}"
                    td.Text = $"Radius² {RadiusSquared}"

                    Dim graphicsUni As Graphics = CreateGraphics()
                    Dim ThisFontHeight As Single = graphicsUni.MeasureString($"Radius² {RadiusSquared}", New Font("Segoe UI", RadiusFontSize)).Height
                    td.FontSize = RadiusFontSize

                    td.X = CircleCenterPoint.X + CircleRadius + 10
                    td.Y = CircleCenterPoint.Y - ThisFontHeight \ 2
                    'Case TextDisplayIndex.Center
                    '    td.Text = $"X {CircleCenterPoint.X}, Y {CircleCenterPoint.Y}"
                    '    Dim graphicsUni As Graphics = CreateGraphics()
                    '    Dim ThisFontWidth As Single = graphicsUni.MeasureString($"X {CircleCenterPoint.X}, Y {CircleCenterPoint.Y}", New Font("Segoe UI", CenterFontSize)).Width
                    '    td.FontSize = CenterFontSize

                    '    td.X = CircleCenterPoint.X - ThisFontWidth \ 2
                    '    td.Y = CircleCenterPoint.Y

                    'Case TextDisplayIndex.Footer


            End Select

            TextDisplays(i) = td

        Next


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

End Class
