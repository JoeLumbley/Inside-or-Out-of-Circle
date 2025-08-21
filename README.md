# Inside or Out of Circle 🔵 

The **Inside or Out of Circle** application is an interactive tool designed to demonstrate and explore the concept of hit detection using squared distance calculations. This app provides a visual and engaging way to understand how proximity to a defined area (in this case, a circle) can be quantified and represented graphically.

<img width="1920" height="1080" alt="015" src="https://github.com/user-attachments/assets/f54e5b88-ce4d-41e8-b6ab-fb9609a13a45" />


### Key Features

 **🔵 Dynamic Circle Visualization**:
   - The app renders a large circle in the center of the window, which changes color based on the mouse pointer's position relative to the circle's radius. This immediate visual feedback helps users intuitively grasp the concept of being "inside" or "outside" the circle.

 **🎯 Efficient Hit Detection**:
   - Utilizing squared Euclidean distance for collision detection, the app efficiently determines whether the mouse pointer is inside the circle without the need for costly square root calculations. This method is particularly effective for applications requiring high-frequency interaction, such as games or educational tools.

 **🖱️ Real-Time Interaction**:
   - As users move their mouse over the circle, the app provides instant feedback by changing the circle's color: it turns blue when the pointer is inside and gray when outside. This feature enhances user engagement and understanding of spatial relationships.

 **🔍 Educational Utility**:
   - The app serves as a valuable resource for students and educators, particularly in fields such as geometry, computer graphics, and game development. It allows users to visualize and understand key concepts such as distance calculations, spatial logic, and the principles of graphical rendering.

 **🌀 Responsive Design**:
   - The circle remains perfectly centered in the client area, adjusting dynamically as the window is resized. This ensures a consistent user experience across different screen sizes and resolutions.

 **🧽 Smooth Rendering Techniques**:
   - The application employs double buffering and anti-aliasing techniques to provide clean visuals and minimize flickering, resulting in a polished and professional appearance.

### Use Cases

- **Educational Settings**: Ideal for teaching concepts related to geometry, distance measurement, and graphical programming.
- **Interactive Demonstrations**: Useful for workshops or presentations that require real-time visual feedback on spatial relationships.
- **Development Practice**: A practical example for developers learning about GDI drawing techniques and hit detection algorithms.


The **Inside or Out of Circle** app is not just a simple interactive tool; it is a comprehensive educational platform that combines visual feedback with mathematical principles. By engaging with this application, users can deepen their understanding of geometry and enhance their programming skills in a fun and interactive way.


<img width="1920" height="1080" alt="016" src="https://github.com/user-attachments/assets/f4da8a40-75f7-4b56-a646-52e819e2c3c7" />

<img width="1920" height="1080" alt="017" src="https://github.com/user-attachments/assets/fcdc7b95-53d6-4374-ba57-cd825dd77ddb" />
<img width="1920" height="1080" alt="018" src="https://github.com/user-attachments/assets/db80f46a-291d-4bbe-afcf-03dd5a235461" />
<img width="1920" height="1080" alt="019" src="https://github.com/user-attachments/assets/9e67af2b-aa9b-4606-a870-aa4e1ff1eb2e" />

---

# Code Walkthrough


This program is designed to visually demonstrate whether a point (the mouse pointer) is inside or outside a circle using squared distance calculations. In this guide, we will break down the code line by line, explaining each part in a way that is easy to understand.

[Table of Contents](#table-of-contents)

## Overview

This program calculates the squared distance from a point to the center of a circle and compares it to the squared radius. If the squared distance is less than or equal to the squared radius, the point is considered to be inside or on the edge of the circle. If it is greater, the point is outside the circle. The application also displays various information such as the radius, center of the circle, mouse pointer location, and calculated distances in a graphical window.

```vb
' MIT License
' Copyright (c) 2025 Joseph W. Lumbley
```
This section specifies the licensing terms under which the code is published, allowing others to use and modify it under certain conditions.

## Class Declaration

```vb
Public Class Form1
```
Here, we declare a public class named `Form1`. This class represents the main form of our application where all the graphical elements will be drawn and where interactions will occur.

[Table of Contents](#table-of-contents)

## Structures and Enumerations

### TextDisplay Structure

```vb
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
```
- **Purpose**: This structure is used to manage the properties of text that will be displayed on the form.
- **Members**:
  - `X` and `Y`: Coordinates for the text position.
  - `Text`: The string to be displayed.
  - `Brush`: The brush used for rendering the text color.
  - `FontSize` and `Font`: Specify the size and type of font for the text.
- **Constructor**: Initializes the properties of the `TextDisplay` structure.

### Text Displays Array

```vb
Private TextDisplays() As TextDisplay = {
    New TextDisplay(0, 0, "Heading", Brushes.Transparent, 10, New Font("Segoe UI", 10)),
    New TextDisplay(0, 0, "Mouse", Brushes.Transparent, 10, New Font("Segoe UI", 10)),
    New TextDisplay(0, 0, "Radius", Brushes.Transparent, 10, New Font("Segoe UI", 10)),
    New TextDisplay(0, 0, "Center", Brushes.Transparent, 10, New Font("Segoe UI", 10)),
    New TextDisplay(0, 0, "Footer", Brushes.Transparent, 10, New Font("Segoe UI", 10))
}
```
- **Purpose**: Initializes an array of `TextDisplay` structures to manage different text overlays (like headings, mouse position, radius, etc.) that will be shown on the form.

### TextDisplayIndex Enumeration

```vb
Private Enum TextDisplayIndex
    Heading = 0
    Mouse = 1
    Radius = 2
    Center = 3
    Footer = 4
End Enum
```
- **Purpose**: This enumeration provides easier access to the indices of the `TextDisplays` array, making the code more readable.

### LineDisplay Structure

```vb
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
```
- **Purpose**: This structure manages the properties for lines drawn on the canvas.
- **Members**:
  - `X1`, `Y1`: Coordinates for the starting point of the line.
  - `X2`, `Y2`: Coordinates for the ending point of the line.
  - `Pen`: The pen used for drawing the line.
- **Constructor**: Initializes the properties of the `LineDisplay` structure.

### Line Displays Array

```vb
Private LineDisplays() As LineDisplay = {
    New LineDisplay(CircleCenterPoint.X, CircleCenterPoint.Y, CircleCenterPoint.X + CircleRadius, CircleCenterPoint.Y, New Pen(Color.Chartreuse, 2)),
    New LineDisplay(CircleCenterPoint.X, CircleCenterPoint.Y, MousePointerLocation.X, CircleCenterPoint.Y, New Pen(Color.Chartreuse, 2)),
    New LineDisplay(MousePointerLocation.X, CircleCenterPoint.Y, MousePointerLocation.X, MousePointerLocation.Y, New Pen(Color.Chartreuse, 2)),
    New LineDisplay(CircleCenterPoint.X, CircleCenterPoint.Y, MousePointerLocation.X, MousePointerLocation.Y, New Pen(Color.Chartreuse, 2))
}
```
- **Purpose**: Initializes an array of `LineDisplay` structures to represent lines drawn in the application, such as the radius line and distance lines.

### LineDisplayIndex Enumeration

```vb
Private Enum LineDisplayIndex
    RadiusLine = 0
    XDistanceLine = 1
    YDistanceLine = 2
    DistanceLine = 3
End Enum
```
- **Purpose**: This enumeration provides easier access to the indices of the `LineDisplays` array.

### CircleDisplay Structure

```vb
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
```
- **Purpose**: This structure manages properties for circles and their graphical representation.
- **Members**:
  - `X`, `Y`: Coordinates for the top-left corner of the bounding rectangle of the circle.
  - `Width`, `Height`: Dimensions of the circle.
  - `Brush`: The brush used for filling the circle.
- **Constructor**: Initializes the properties of the `CircleDisplay` structure.

### Circle Displays Array

```vb
Private CircleDisplays() As CircleDisplay = {
    New CircleDisplay(CircleCenterPoint.X - CircleRadius, CircleCenterPoint.Y - CircleRadius, CircleRadius * 2, CircleRadius * 2, Brushes.LightGray),
    New CircleDisplay(CircleCenterPoint.X + CircleRadius - 3, CircleCenterPoint.Y - 3, 6, 6, Brushes.LightGray),
    New CircleDisplay(CircleCenterPoint.X - 3, CircleCenterPoint.Y - 3, 6, 6, Brushes.LightGray),
    New CircleDisplay(MousePointerLocation.X - 20, MousePointerLocation.Y - 20, 40, 40, MouseHilightBrush),
    New CircleDisplay(MousePointerLocation.X - 3, MousePointerLocation.Y - 3, 6, 6, Brushes.LightGray)
}
```
- **Purpose**: Initializes an array of `CircleDisplay` structures to manage the graphical representation of the circle and its components (center point, radius endpoint, mouse highlight).

### CircleDisplayIndex Enumeration

```vb
Private Enum CircleDisplayIndex
    Circle = 0
    RadiusEndPoint = 1
    CenterPoint = 2
    MouseHilight = 3
    MousePoint = 4
End Enum
```
- **Purpose**: This enumeration provides easier access to the indices of the `CircleDisplays` array.

[Table of Contents](#table-of-contents)

## Variables for Circle Properties

```vb
Private CircleCenterPoint As New Point(150, 150)
Private MousePointerLocation As New Point(0, 0)
Private CircleRadius As Integer = 300
Private IsPointerInsideCircle As Boolean = False
Private DistanceSquared As Double
Private RadiusSquared As Double = CircleRadius * CircleRadius
```
- **Purpose**: These variables store important properties for the circle and the mouse pointer.
- **Members**:
  - `CircleCenterPoint`: Initializes the center point of the circle.
  - `MousePointerLocation`: Tracks the current location of the mouse pointer.
  - `CircleRadius`: Sets the radius of the circle.
  - `IsPointerInsideCircle`: A boolean flag indicating if the mouse pointer is inside the circle.
  - `DistanceSquared`: Holds the squared distance from the mouse pointer to the circle's center.
  - `RadiusSquared`: Pre-calculates the squared radius of the circle for efficient distance comparisons.

[Table of Contents](#table-of-contents)

## Pens and Brushes

```vb
Private DistanceArrowCap As New Drawing2D.AdjustableArrowCap(5, 5, True)
Private DistancePen As New Pen(Color.Black, 3)
Private ArrowBlack3Pen As New Pen(Color.Black, 3)
Private TransparentPen As New Pen(Color.Transparent, 3)
Private RadiusArrowCap As New Drawing2D.AdjustableArrowCap(4, 4, True)
Private RadiusPen As New Pen(Color.Gray, 2)
Private XYDistancePen As New Pen(Color.Orchid, 2)
Private Orchid2Pen As New Pen(Color.Orchid, 2)
```
- **Purpose**: These variables define the pens and brushes used for drawing elements in the application.
- **Members**:
  - `DistanceArrowCap`: Custom arrow cap for distance lines.
  - `DistancePen`, `ArrowBlack3Pen`: Pens used for drawing lines.
  - `TransparentPen`: A pen that is transparent.
  - `RadiusPen`: A pen used for drawing the radius line.
  - `XYDistancePen`, `Orchid2Pen`: Pens used for drawing distance lines.

[Table of Contents](#table-of-contents)

## Font Sizes

```vb
Private HeadingFontSize As Integer = 16
Private MouseFontSize As Integer = 12
Private RadiusFontSize As Integer = 12
Private CenterFontSize As Integer = 12
Private FooterFontSize As Integer = 10
```
- **Purpose**: These variables set the default font sizes for different text displays in the application.

[Table of Contents](#table-of-contents)

## Grid Pen

```vb
Private gridPen As New Pen(Color.FromArgb(128, Color.LightGray), 2)
```
- **Purpose**: Initializes a pen for drawing a grid in the background of the application.

[Table of Contents](#table-of-contents)

## View State Enumeration

```vb
Private Enum ViewStateIndex
    Overview
    ParametersView
    XDistanceView
    YDistanceView
End Enum
```
- **Purpose**: This enumeration defines the different states of the application view (Overview, ParametersView, XDistanceView, YDistanceView).

[Table of Contents](#table-of-contents)

## View State Variable

```vb
Private ViewState As ViewStateIndex = ViewStateIndex.Overview
```
- **Purpose**: Initializes the current view state to Overview.

[Table of Contents](#table-of-contents)

## Form Load Event

```vb
Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
    InitializeApp()
End Sub
```
- **Purpose**: This event handler runs when the form loads. It calls the `InitializeApp` method to set up the initial state of the application.

[Table of Contents](#table-of-contents)

## Mouse Events

### Mouse Enter Event

```vb
Private Sub Form1_MouseEnter(sender As Object, e As EventArgs) Handles Me.MouseEnter
    UpdateViewMouseEnter()
    Invalidate()
    InvalidateAllButtons()
End Sub
```
- **Purpose**: This event handler triggers when the mouse enters the form. It updates the view and refreshes the display.

### Mouse Leave Event

```vb
Private Sub Form1_MouseLeave(sender As Object, e As EventArgs) Handles Me.MouseLeave
    UpdateViewMouseLeave()
    Invalidate()
    InvalidateAllButtons()
End Sub
```
- **Purpose**: This event handler triggers when the mouse leaves the form. It updates the view to reflect the mouse leave state and refreshes the display.

### Mouse Move Event

```vb
Protected Overrides Sub OnMouseMove(e As MouseEventArgs)
    MyBase.OnMouseMove(e)
    MousePointerLocation = e.Location
    IsPointerInsideCircle = IsPointInsideCircle(e.X, e.Y, CircleCenterPoint.X, CircleCenterPoint.Y, CircleRadius)
    XDistance = e.X - CircleCenterPoint.X
    YDistance = e.Y - CircleCenterPoint.Y
    DistanceSquared = CalculateDistances()
    UpdateViewOnMouseMove()
    Invalidate()
    InvalidateAllButtons()
End Sub
```
- **Purpose**: This method overrides the default mouse move event to track the mouse pointer's location and check if it is inside the circle. It updates the view and refreshes the display.

[Table of Contents](#table-of-contents)

## Paint Event

```vb
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
```
- **Purpose**: This method overrides the paint event to draw the graphical elements on the form.
- **Details**:
  - `g`: Represents the graphics context for drawing.
  - `SmoothingMode`: Sets the rendering quality for smoother graphics.
  - Calls methods to render the grid, circles, lines, and text overlays.

[Table of Contents](#table-of-contents)

## Button Click Events

### Overview Button Click Event

```vb
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
    SetLineDisplayPen(LineDisplayIndex.RadiusLine, RadiusPen)
    SetCircleDisplayBrush(CircleDisplayIndex.RadiusEndPoint, RadiusBrush)
    SetTextDisplayBlack(TextDisplayIndex.Radius)
    Invalidate()
    InvalidateAllButtons()
End Sub
```
- **Purpose**: This event handler is triggered when the overview button is clicked. It switches to the overview view and updates the display while hiding specific overlays and indicators.

### Parameters View Button Click Event

```vb
Private Sub ParametersViewButton_Click(sender As Object, e As EventArgs) Handles ParametersViewButton.Click
    Switch2ParametersView()
    UpdateView()
    ' Hide overlays
    SetTextDisplayTransparent(TextDisplayIndex.Mouse)
    ' Hide mouse indicators
    SetCircleDisplayTransparent(CircleDisplayIndex.MousePoint)
    SetCircleDisplayTransparent(CircleDisplayIndex.MouseHilight)
    SetLineDisplayPen(LineDisplayIndex.RadiusLine, RadiusPen)
    SetCircleDisplayBrush(CircleDisplayIndex.RadiusEndPoint, RadiusBrush)
    SetTextDisplayBlack(TextDisplayIndex.Radius)
    SetTextDisplayBlack(TextDisplayIndex.Heading)
    SetTextDisplayBlack(TextDisplayIndex.Footer)
    SetTextDisplayBlack(TextDisplayIndex.Center)
    Invalidate()
    InvalidateAllButtons()
End Sub
```
- **Purpose**: This event handler is triggered when the parameters view button is clicked. It switches to the parameters view and updates the display while hiding specific overlays and indicators.

### XDistance View Button Click Event

```vb
Private Sub XDistanceViewButton_Click(sender As Object, e As EventArgs) Handles XDistanceViewButton.Click
    Switch2XDistanceView()
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
    SetTextDisplayBlack(TextDisplayIndex.Center)
    Invalidate()
    InvalidateAllButtons()
End Sub
```
- **Purpose**: This event handler is triggered when the X distance view button is clicked. It switches to the X distance view and updates the display while hiding specific overlays and indicators.

### YDistance View Button Click Event

```vb
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
    SetTextDisplayBlack(TextDisplayIndex.Center)
    Invalidate()
    InvalidateAllButtons()
End Sub
```
- **Purpose**: This event handler is triggered when the Y distance view button is clicked. It switches to the Y distance view and updates the display while hiding specific overlays and indicators.

[Table of Contents](#table-of-contents)

## Resize Event

```vb
Protected Overrides Sub OnResize(e As EventArgs)
    MyBase.OnResize(e)
    UpdateButtonLayout()
    UpdateCircleGeometry()
    UpdateFontSizes()
    UpdateTextDisplays(CreateGraphics, DistanceSquared)
    UpdateLineDisplays()
    UpdateCircleDisplaysPostion()
    Invalidate()
    InvalidateAllButtons()
End Sub
```
- **Purpose**: This method overrides the resize event to adjust the layout of buttons, circle geometry, font sizes, and other graphical elements when the form is resized.

[Table of Contents](#table-of-contents)

## Point Inside Circle Function

```vb
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
End Function
```
- **Purpose**: This function checks if a given point is inside or on the edge of a circle.
- **Details**:
  - It calculates the horizontal and vertical distances from the point to the center of the circle.
  - It computes the squared distance and compares it to the squared radius of the circle.
  - Returns `True` if the point is inside or on the edge, and `False` if outside.

[Table of Contents](#table-of-contents)

## Drawing Methods

### Draw Circles Method

```vb
Private Sub DrawCircles(g As Graphics)
    ' 🔵 Draw filled circles
    For Each circleDisplay As CircleDisplay In CircleDisplays
        g.FillEllipse(circleDisplay.Brush, circleDisplay.X, circleDisplay.Y, circleDisplay.Width, circleDisplay.Height)
    Next
End Sub
```
- **Purpose**: This method draws filled circles on the graphical interface.
- **Parameter**: `g`: Represents the graphics context for drawing.

### Draw Lines Method

```vb
Private Sub DrawLines(g As Graphics)
    ' 📏 Draw lines
    For Each lineDisplay As LineDisplay In LineDisplays
        g.DrawLine(lineDisplay.Pen, lineDisplay.X1, lineDisplay.Y1, lineDisplay.X2, lineDisplay.Y2)
    Next
End Sub
```
- **Purpose**: This method draws lines on the graphical interface.
- **Parameter**: `g`: Represents the graphics context for drawing.

### Draw Text Overlays Method

```vb
Private Sub DrawTextOverlays(g As Graphics)
    ' abc Draw text overlays
    For Each textDisplay As TextDisplay In TextDisplays
        g.DrawString(textDisplay.Text, textDisplay.Font, textDisplay.Brush, textDisplay.X, textDisplay.Y)
    Next
End Sub
```
- **Purpose**: This method draws text overlays on the graphical interface.
- **Parameter**: `g`: Represents the graphics context for drawing.

### Draw Grid Method

```vb
Private Sub DrawGrid(g As Graphics)

    ' 🔲 Draw grid (lines every 50 pixels)
    For x As Integer = 0 To ClientSize.Width Step 50
        g.DrawLine(gridPen, x, 0, x, ClientSize.Height)
    Next
    For y As Integer = 0 To ClientSize.Height Step 50
        g.DrawLine(gridPen, 0, y, ClientSize.Width, y)
    Next
End Sub
```
- **Purpose**: This method draws a grid on the background of the graphical interface.
- **Parameter**: `g`: Represents the graphics context for drawing.
- **Details**:
  - Vertical lines are drawn every 50 pixels across the width of the form.
  - Horizontal lines are drawn every 50 pixels across the height of the form.

[Table of Contents](#table-of-contents)

## Update Methods

### Update Button Layout Method

```vb
Private Sub UpdateButtonLayout()
    Dim ButtonSize As Integer = Math.Max(40, Math.Min(Me.ClientSize.Width, Me.ClientSize.Height) \ 11)
    Dim Pad As Integer = 10

    YDistanceViewButton.Width = ButtonSize
    YDistanceViewButton.Height = ButtonSize
    YDistanceViewButton.Font = New Font("Segoe UI", Math.Max(12, Math.Min(Me.ClientSize.Width, Me.ClientSize.Height) \ 30))
    YDistanceViewButton.SetBounds(ClientSize.Width - ButtonSize - Pad,
                                  ClientSize.Height - ButtonSize - Pad,
                                  ButtonSize,
                                  ButtonSize)

    XDistanceViewButton.Width = ButtonSize
    XDistanceViewButton.Height = ButtonSize
    XDistanceViewButton.Font = New Font("Segoe UI", Math.Max(12, Math.Min(Me.ClientSize.Width, Me.ClientSize.Height) \ 25))
    XDistanceViewButton.SetBounds(ClientSize.Width - ButtonSize * 2 - Pad * 2,
                                  ClientSize.Height - ButtonSize - Pad,
                                  ButtonSize,
                                  ButtonSize)

    ParametersViewButton.Width = ButtonSize
    ParametersViewButton.Height = ButtonSize
    ParametersViewButton.Font = New Font("Segoe UI", Math.Max(9, Math.Min(Me.ClientSize.Width, Me.ClientSize.Height) \ 40))
    ParametersViewButton.SetBounds(ClientSize.Width - ButtonSize * 3 - Pad * 3,
                                   ClientSize.Height - ButtonSize - Pad,
                                   ButtonSize,
                                   ButtonSize)

    OverviewButton.Width = ButtonSize
    OverviewButton.Height = ButtonSize
    OverviewButton.Font = New Font("Segoe UI", Math.Max(12, Math.Min(Me.ClientSize.Width, Me.ClientSize.Height) \ 30))
    OverviewButton.SetBounds(ClientSize.Width - ButtonSize * 4 - Pad * 4,
                             ClientSize.Height - ButtonSize - Pad,
                             ButtonSize,
                             ButtonSize)
End Sub
```
- **Purpose**: This method adjusts the size and position of the buttons based on the current client size of the form.
- **Details**:
  - The size of each button is calculated to ensure it fits well within the form.
  - The buttons are positioned at the bottom right of the form, taking into account padding.

### Update Circle Geometry Method

```vb
Private Sub UpdateCircleGeometry()
    CircleCenterPoint = New Point(ClientSize.Width \ 2, ClientSize.Height \ 2)
    CircleRadius = Math.Min(ClientSize.Width, ClientSize.Height) \ 3.4
    RadiusSquared = CircleRadius * CircleRadius
End Sub
```
- **Purpose**: This method recalculates the center point and radius of the circle based on the current size of the form.
- **Details**:
  - The circle is centered in the form.
  - The radius is set to a fraction of the smaller dimension of the form for better responsiveness.
  - The squared radius is updated for efficient distance calculations.

### Update Font Sizes Method

```vb
Private Sub UpdateFontSizes()
    Dim baseSize = Math.Min(ClientSize.Width, ClientSize.Height) \ 20
    MouseFontSize = Math.Max(10, baseSize)
    RadiusFontSize = MouseFontSize
    CenterFontSize = MouseFontSize
    HeadingFontSize = MouseFontSize
    FooterFontSize = MouseFontSize
End Sub
```
- **Purpose**: This method adjusts the font sizes for text displays based on the current size of the form.
- **Details**:
  - A base size is calculated based on the smaller dimension of the form.
  - Ensures that font sizes do not fall below a minimum value.

### Update Line Displays Method

```vb
Private Sub UpdateLineDisplays()
    For i As Integer = 0 To LineDisplays.Count - 1
        Dim ld = LineDisplays(i)

        Select Case i
            Case LineDisplayIndex.RadiusLine
                SetLine(ld, CircleCenterPoint, New Point(CircleCenterPoint.X + CircleRadius, CircleCenterPoint.Y))
            Case LineDisplayIndex.XDistanceLine
                SetLine(ld, CircleCenterPoint, New Point(MousePointerLocation.X, CircleCenterPoint.Y))
            Case LineDisplayIndex.YDistanceLine
                SetLine(ld, New Point(MousePointerLocation.X, CircleCenterPoint.Y), MousePointerLocation)
            Case LineDisplayIndex.DistanceLine
                SetLine(ld, CircleCenterPoint, MousePointerLocation)
        End Select

        LineDisplays(i) = ld
    Next
End Sub
```
- **Purpose**: This method updates the positions and visibility of lines based on the current state of the application.
- **Details**:
  - Iterates through the `LineDisplays` array and sets their positions based on the current mouse pointer location and circle properties.

### Set Line Method

```vb
Private Sub SetLine(ByRef ld As LineDisplay, p1 As Point, p2 As Point)
    ld.X1 = p1.X
    ld.Y1 = p1.Y
    ld.X2 = p2.X
    ld.Y2 = p2.Y
End Sub
```
- **Purpose**: This helper method sets the coordinates of a line display based on two points.

### Update Circle Displays Position Method

```vb
Private Sub UpdateCircleDisplaysPostion()
    For i As Integer = 0 To CircleDisplays.Count - 1
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
```
- **Purpose**: This method updates the positions of the circle displays based on the current state of the application.
- **Details**:
  - Each display is updated based on its type (main circle, radius endpoint, center point, mouse point, highlight).

### Set Circle Position Method

```vb
Private Sub SetCirclePostion(ByRef cd As CircleDisplay)
    cd.X = CircleCenterPoint.X - CircleRadius
    cd.Y = CircleCenterPoint.Y - CircleRadius
    cd.Width = CircleRadius * 2
    cd.Height = CircleRadius * 2
End Sub
```
- **Purpose**: This method sets the position and size of the main circle based on its center point and radius.

### Set Endpoint Position Method

```vb
Private Sub SetEndpointPostion(ByRef cd As CircleDisplay, offset As Double)
    cd.X = CircleCenterPoint.X + offset - 3
    cd.Y = CircleCenterPoint.Y - 3
    cd.Width = 6
    cd.Height = 6
End Sub
```
- **Purpose**: This method sets the position of the radius endpoint based on the circle's center and radius.

### Set Mouse Point Position Method

```vb
Private Sub SetMousePointPostion(ByRef cd As CircleDisplay)
    cd.X = MousePointerLocation.X - 3
    cd.Y = MousePointerLocation.Y - 3
    cd.Width = 6
    cd.Height = 6
End Sub
```
- **Purpose**: This method sets the position of the mouse pointer display.

### Set Mouse Highlight Position Method

```vb
Private Sub SetMouseHighlightPostion(ByRef cd As CircleDisplay)
    cd.X = MousePointerLocation.X - 20
    cd.Y = MousePointerLocation.Y - 20
    cd.Width = 40
    cd.Height = 40
End Sub
```
- **Purpose**: This method sets the position of the mouse highlight display.

[Table of Contents](#table-of-contents)

## Update Text Displays Method

```vb
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
                UpdateHeadingTextPositionContent(td, g, headingFont)
            Case TextDisplayIndex.Mouse
                UpdateMouseTextPositionContent(td, g, mouseFont)
            Case TextDisplayIndex.Center
                UpdateCenterTextPositionContent(td, g, centerFont)
            Case TextDisplayIndex.Footer
                UpdateFooterTextPositionContent(td, g, footerFont, distanceSquared)
            Case TextDisplayIndex.Radius
                UpdateRadiusTextPositionContent(td, g, radiusFont)
        End Select

        TextDisplays(i) = td
    Next
End Sub
```
- **Purpose**: This method updates the positions and contents of text displays based on the current state of the application.
- **Details**:
  - Each text display is updated according to its type, using specific methods to set the text and position.

### Update Mouse Text Position Content Method

```vb
Private Sub UpdateMouseTextPositionContent(ByRef td As TextDisplay, g As Graphics, mouseFont As Font)
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
    End Select

    td.FontSize = MouseFontSize
    td.Font = mouseFont
    Dim ThisStringSize As SizeF = g.MeasureString(td.Text, td.Font)

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
```
- **Purpose**: This method updates the position and content of the mouse text display based on the current view state.

### Update Heading Text Position Content Method

```vb
Private Sub UpdateHeadingTextPositionContent(ByRef td As TextDisplay, g As Graphics, headingFont As Font)
    Select Case ViewState
        Case ViewStateIndex.Overview
            td.Text = $"Inside Circle {IsPointerInsideCircle}"
        Case ViewStateIndex.ParametersView
            td.Text = "Parameters"
        Case ViewStateIndex.XDistanceView
            td.Text = $"X Distance {XDistance}"
        Case ViewStateIndex.YDistanceView
            td.Text = $"Y Distance {YDistance}"
    End Select

    td.FontSize = HeadingFontSize
    Dim ThisStringSize = g.MeasureString(td.Text, headingFont)
    td.X = ClientSize.Width \ 2 - ThisStringSize.Width \ 2
    td.Y = ((CircleCenterPoint.Y - CircleRadius) \ 2) - (ThisStringSize.Height \ 2)
    td.Font = headingFont
End Sub
```
- **Purpose**: This method updates the position and content of the heading text display based on the current view state.

### Update Center Text Position Content Method

```vb
Private Sub UpdateCenterTextPositionContent(ByRef td As TextDisplay, g As Graphics, centerFont As Font)
    Select Case ViewState
        Case ViewStateIndex.Overview
        Case ViewStateIndex.ParametersView
            td.Text = $"X {CircleCenterPoint.X}, Y {CircleCenterPoint.Y}"
        Case ViewStateIndex.XDistanceView
            td.Text = $"X {CircleCenterPoint.X}"
        Case ViewStateIndex.YDistanceView
            td.Text = $"Y {CircleCenterPoint.Y}"
    End Select

    td.FontSize = CenterFontSize
    Dim size = g.MeasureString(td.Text, centerFont)
    td.X = CircleCenterPoint.X - size.Width \ 2
    td.Y = CircleCenterPoint.Y
    td.Font = centerFont
End Sub
```
- **Purpose**: This method updates the position and content of the center text display based on the current view state.

### Update Footer Text Position Content Method

```vb
Private Sub UpdateFooterTextPositionContent(ByRef td As TextDisplay, g As Graphics, footerFont As Font, distanceSquared As Double)
    Select Case ViewState
        Case ViewStateIndex.Overview
            td.Text = $"{IsPointerInsideCircle} = {distanceSquared} <= {RadiusSquared}"
        Case ViewStateIndex.ParametersView
            td.Text = $"What is Known"
        Case ViewStateIndex.XDistanceView
            td.Text = $"{XDistance} = {MousePointerLocation.X} - {CircleCenterPoint.X}"
        Case ViewStateIndex.YDistanceView
            td.Text = $"{YDistance} = {MousePointerLocation.Y} - {CircleCenterPoint.Y}"
    End Select

    td.FontSize = FooterFontSize
    Dim size = g.MeasureString(td.Text, footerFont)
    td.X = ClientSize.Width \ 2 - size.Width \ 2
    td.Y = (CircleCenterPoint.Y + CircleRadius) + (ClientSize.Height - (CircleCenterPoint.Y + CircleRadius)) \ 2 - (size.Height \ 2)
    td.Font = footerFont
End Sub
```
- **Purpose**: This method updates the position and content of the footer text display based on the current view state.

### Update Radius Text Position Content Method

```vb
Private Sub UpdateRadiusTextPositionContent(ByRef td As TextDisplay, g As Graphics, radiusFont As Font)
    Select Case ViewState
        Case ViewStateIndex.Overview
            td.Text = $"Radius² {RadiusSquared}"
        Case ViewStateIndex.ParametersView
            td.Text = $"Radius {CircleRadius}"
    End Select

    td.FontSize = RadiusFontSize
    Dim size = g.MeasureString(td.Text, radiusFont)
    td.X = CircleCenterPoint.X + CircleRadius + 10
    td.Y = CircleCenterPoint.Y - size.Height \ 2
    td.Font = radiusFont
End Sub
```
- **Purpose**: This method updates the position and content of the radius text display based on the current view state.

[Table of Contents](#table-of-contents)

## Initialize Application Method

```vb
Private Sub InitializeApp()
    ' Initialize the application state
    InitializePensBrushes()
    InitializeTextDisplays()
    InitializeLineDisplays()
    InitializeCircleDisplays()
End Sub
```
- **Purpose**: This method sets up the initial state of various graphical elements in the application.

### Initialize Pens and Brushes Method

```vb
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
```
- **Purpose**: This method initializes the pens and brushes used throughout the application.

### Initialize Text Displays Method

```vb
Private Sub InitializeTextDisplays()
    ' Set TextDisplays to initial state
    SetTextDisplayTransparent(TextDisplayIndex.Center)
    SetTextDisplayTransparent(TextDisplayIndex.Footer)
    SetTextDisplayTransparent(TextDisplayIndex.Mouse)
    SetTextDisplayTransparent(TextDisplayIndex.Heading)
    SetTextDisplayBlack(TextDisplayIndex.Radius)
End Sub
```
- **Purpose**: This method sets the text displays to their initial states, making some of them transparent and others visible.

### Initialize Line Displays Method

```vb
Private Sub InitializeLineDisplays()
    ' Set LineDisplays to initial state
    SetLineDisplayTransparent(LineDisplayIndex.DistanceLine)
    SetLineDisplayTransparent(LineDisplayIndex.XDistanceLine)
    SetLineDisplayTransparent(LineDisplayIndex.YDistanceLine)
    SetLineDisplayPen(LineDisplayIndex.RadiusLine, RadiusPen)
End Sub
```
- **Purpose**: This method sets the line displays to their initial states, making some of them transparent and configuring the radius line.

### Initialize Circle Displays Method

```vb
Private Sub InitializeCircleDisplays()
    ' Set CircleDisplays to initial state
    SetCircleDisplayTransparent(CircleDisplayIndex.MousePoint)
    SetCircleDisplayTransparent(CircleDisplayIndex.MouseHilight)
    SetCircleDisplayBrush(CircleDisplayIndex.Circle, CircleBrush)
    SetCircleDisplayBrush(CircleDisplayIndex.RadiusEndPoint, RadiusBrush)
    SetCircleDisplayBrush(CircleDisplayIndex.CenterPoint, RadiusBrush)
End Sub
```
- **Purpose**: This method sets the circle displays to their initial states, making some of them transparent and configuring the main circle and its components.



This detailed walkthrough covers the entire code structure and functionality of the **Inside or Out of Circle** application. The application is designed to provide a visual representation of whether a point (the mouse pointer) is inside or outside a circle, utilizing efficient squared distance calculations to enhance performance.

By engaging with this application, users can deepen their understanding of geometry and enhance their programming skills in a fun and interactive way. 










---




# Table of Contents

1. [Overview](#overview)
2. [Class Declaration](#class-declaration)
3. [Structures and Enumerations](#structures-and-enumerations)
   - 3.1 [TextDisplay Structure](#textdisplay-structure)
   - 3.2 [Text Displays Array](#text-displays-array)
   - 3.3 [TextDisplayIndex Enumeration](#textdisplayindex-enumeration)
   - 3.4 [LineDisplay Structure](#linedisplay-structure)
   - 3.5 [Line Displays Array](#line-displays-array)
   - 3.6 [LineDisplayIndex Enumeration](#linedisplayindex-enumeration)
   - 3.7 [CircleDisplay Structure](#circledisplay-structure)
   - 3.8 [Circle Displays Array](#circle-displays-array)
   - 3.9 [CircleDisplayIndex Enumeration](#circledisplayindex-enumeration)
4. [Variables for Circle Properties](#variables-for-circle-properties)
5. [Pens and Brushes](#pens-and-brushes)
6. [Font Sizes](#font-sizes)
7. [Grid Pen](#grid-pen)
8. [View State Enumeration](#view-state-enumeration)
9. [View State Variable](#view-state-variable)
10. [Form Load Event](#form-load-event)
11. [Mouse Events](#mouse-events)
    - 11.1 [Mouse Enter Event](#mouse-enter-event)
    - 11.2 [Mouse Leave Event](#mouse-leave-event)
    - 11.3 [Mouse Move Event](#mouse-move-event)
12. [Paint Event](#paint-event)
13. [Button Click Events](#button-click-events)
    - 13.1 [Overview Button Click Event](#overview-button-click-event)
    - 13.2 [Parameters View Button Click Event](#parameters-view-button-click-event)
    - 13.3 [XDistance View Button Click Event](#xdistance-view-button-click-event)
    - 13.4 [YDistance View Button Click Event](#ydistance-view-button-click-event)
14. [Resize Event](#resize-event)
15. [Point Inside Circle Function](#point-inside-circle-function)
16. [Drawing Methods](#drawing-methods)
    - 16.1 [Draw Circles Method](#draw-circles-method)
    - 16.2 [Draw Lines Method](#draw-lines-method)
    - 16.3 [Draw Text Overlays Method](#draw-text-overlays-method)
    - 16.4 [Draw Grid Method](#draw-grid-method)
17. [Update Methods](#update-methods)
    - 17.1 [Update Button Layout Method](#update-button-layout-method)
    - 17.2 [Update Circle Geometry Method](#update-circle-geometry-method)
    - 17.3 [Update Font Sizes Method](#update-font-sizes-method)
    - 17.4 [Update Line Displays Method](#update-line-displays-method)
    - 17.5 [Set Line Method](#set-line-method)
    - 17.6 [Update Circle Displays Position Method](#update-circle-displays-position-method)
    - 17.7 [Set Circle Position Method](#set-circle-position-method)
    - 17.8 [Set Endpoint Position Method](#set-endpoint-position-method)
    - 17.9 [Set Mouse Point Position Method](#set-mouse-point-position-method)
    - 17.10 [Set Mouse Highlight Position Method](#set-mouse-highlight-position-method)
18. [Update Text Displays Method](#update-text-displays-method)
    - 18.1 [Update Mouse Text Position Content Method](#update-mouse-text-position-content-method)
    - 18.2 [Update Heading Text Position Content Method](#update-heading-text-position-content-method)
    - 18.3 [Update Center Text Position Content Method](#update-center-text-position-content-method)
    - 18.4 [Update Footer Text Position Content Method](#update-footer-text-position-content-method)
    - 18.5 [Update Radius Text Position Content Method](#update-radius-text-position-content-method)
19. [Initialize Application Method](#initialize-application-method)
    - 19.1 [Initialize Pens and Brushes Method](#initialize-pens-and-brushes-method)
    - 19.2 [Initialize Text Displays Method](#initialize-text-displays-method)
    - 19.3 [Initialize Line Displays Method](#initialize-line-displays-method)
    - 19.4 [Initialize Circle Displays Method](#initialize-circle-displays-method)











---


# Clones






<img width="1920" height="1080" alt="004" src="https://github.com/user-attachments/assets/761cde86-2340-4122-9b3e-9f4feb7dfdd0" />

































