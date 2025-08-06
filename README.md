# Inside or Out of Circle 🔵 

The **Inside or Out of Circle** application is an interactive educational tool designed to demonstrate and explore the concept of hit detection using squared distance calculations. This app provides a visual and engaging way to understand how proximity to a defined area (in this case, a circle) can be quantified and represented graphically.

<img width="1920" height="1080" alt="013" src="https://github.com/user-attachments/assets/142d34ff-d9dd-4741-a477-e468498e269e" />


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

<img width="1920" height="1080" alt="014" src="https://github.com/user-attachments/assets/830c3bf6-79f9-4c70-a296-0a089147fdf1" />

The **Inside or Out of Circle** app is not just a simple interactive tool; it is a comprehensive educational platform that combines visual feedback with mathematical principles. By engaging with this application, users can deepen their understanding of geometry and enhance their programming skills in a fun and interactive way.



---

# Code Walkthrough

### Class Declaration

```vb
Public Class Form1
```
- This line declares a public class named `Form1`, which serves as the main form of the application.

### Structure Definitions

#### TextDisplay Structure

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
- Defines a structure named `TextDisplay` to hold information about text to be displayed in the graphical window.
- Contains properties for position (X, Y), text content, brush color, font size, and font type.
- The constructor initializes these properties.

#### LineDisplay Structure

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
- Defines a structure named `LineDisplay` to represent lines drawn on the form.
- Contains properties for the start (X1, Y1) and end (X2, Y2) coordinates of the line, as well as the pen used to draw it.
- The constructor initializes these properties.

#### CircleDisplay Structure

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
- Defines a structure named `CircleDisplay` to represent circles drawn on the form.
- Contains properties for position (X, Y), dimensions (Width, Height), and brush color.
- The constructor initializes these properties.

### Enum Declarations

#### TextDisplayIndex Enum

```vb
Private Enum TextDisplayIndex
    Heading = 0
    Mouse = 1
    Radius = 2
    Center = 3
    Footer = 4
End Enum
```
- Enumerates indices for different text displays, making it easier to reference them in arrays.

#### LineDisplayIndex Enum

```vb
Private Enum LineDisplayIndex
    RadiusLine = 0
    XDistanceLine = 1
    YDistanceLine = 2
    DistanceLine = 3
End Enum
```
- Enumerates indices for different lines to be drawn, aiding in organization.

#### CircleDisplayIndex Enum

```vb
Private Enum CircleDisplayIndex
    Circle = 0
    RadiusEndPoint = 1
    CenterPoint = 2
    MouseHilight = 3
    MousePoint = 4
End Enum
```
- Enumerates indices for different circle displays, facilitating easier management of circle-related graphics.

### Variables and Constants

```vb
Private CircleCenterPoint As New Point(150, 150)
Private MousePointerLocation As New Point(0, 0)
Private CircleRadius As Integer = 300
Private IsPointerInsideCircle As Boolean = False
Private DistanceSquared As Double
Private RadiusSquared As Double = CircleRadius * CircleRadius
```
- Initializes variables for the circle's center point, mouse pointer location, circle radius, and whether the pointer is inside the circle.
- The `RadiusSquared` variable is calculated once to optimize performance.

### Event Handlers

#### Form Load Event

```vb
Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
    InitializeApp()
End Sub
```
- This event is triggered when the form loads. It calls the `InitializeApp` method to set up the application.

#### Mouse Move Event

```vb
Protected Overrides Sub OnMouseMove(e As MouseEventArgs)
    MyBase.OnMouseMove(e)
    MousePointerLocation = e.Location
    IsPointerInsideCircle = IsPointInsideCircle(e.X, e.Y, CircleCenterPoint.X, CircleCenterPoint.Y, CircleRadius)
    UpdateViewOnMouseMove()
    Invalidate()
End Sub
```
- This method overrides the default mouse move behavior to update the mouse pointer's location and check if it is inside the circle.
- Calls `UpdateViewOnMouseMove` to refresh the display and then calls `Invalidate` to trigger a repaint.

### Core Functionality

#### IsPointInsideCircle Function

```vb
Function IsPointInsideCircle(pointX As Double, pointY As Double, centerX As Double, centerY As Double, radius As Double) As Boolean
    Dim Xdistance As Double = pointX - centerX
    Dim Ydistance As Double = pointY - centerY
    Dim squaredDistance As Double = Xdistance * Xdistance + Ydistance * Ydistance
    Return squaredDistance <= radius * radius
End Function
```
- This function checks if a given point is inside or on the edge of the circle by calculating the squared distance from the point to the circle's center.
- It avoids computing the square root for efficiency, returning `True` if the point is inside or on the edge, and `False` otherwise.

### Drawing Functions

#### DrawCircles

```vb
Private Sub DrawCircles(g As Graphics)
    For Each circleDisplay As CircleDisplay In CircleDisplays
        g.FillEllipse(circleDisplay.Brush, circleDisplay.X, circleDisplay.Y, circleDisplay.Width, circleDisplay.Height)
    Next
End Sub
```
- Draws filled circles based on the properties defined in the `CircleDisplays` array.

#### DrawLines

```vb
Private Sub DrawLines(g As Graphics)
    For Each lineDisplay As LineDisplay In LineDisplays
        g.DrawLine(lineDisplay.Pen, lineDisplay.X1, lineDisplay.Y1, lineDisplay.X2, lineDisplay.Y2)
    Next
End Sub
```
- Draws lines based on the properties defined in the `LineDisplays` array.

#### DrawTextOverlays

```vb
Private Sub DrawTextOverlays(g As Graphics)
    For Each textDisplay As TextDisplay In TextDisplays
        g.DrawString(textDisplay.Text, textDisplay.Font, textDisplay.Brush, textDisplay.X, textDisplay.Y)
    Next
End Sub
```
- Draws text overlays based on the properties defined in the `TextDisplays` array.

### Initialization and Updates

#### InitializeApp

```vb
Private Sub InitializeApp()
    gridPen = Pens.Transparent
    DistancePen = TransparentPen
    ' Other initializations...
End Sub
```
- Initializes various graphical elements and settings for the application, such as pen colors and text displays.


This program effectively demonstrates how to visualize geometric relationships using basic graphical programming techniques in Visual Basic .NET. By utilizing structures, enums, and event-driven programming, it provides a user-friendly interface to interact with geometric concepts.

