# Inside or Out of Circle 🔵 

The **Inside or Out of Circle** application is an interactive educational tool designed to demonstrate and explore the concept of hit detection using squared distance calculations. This app provides a visual and engaging way to understand how proximity to a defined area (in this case, a circle) can be quantified and represented graphically.


<img width="1920" height="1080" alt="001" src="https://github.com/user-attachments/assets/3a5666b1-bd9b-4614-a3a1-beeca76e3200" />

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


<img width="1920" height="1080" alt="002" src="https://github.com/user-attachments/assets/44510b1c-b33a-4526-9ce1-f00f7b5cdcbf" />

---

# Code Walkthrough



```vb

Public Class Form1
```

- **Public Class Form1**: This line declares a new class named `Form1`, which serves as the main form for the application.

### Structure Definitions
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
- **Structure TextDisplay**: Defines a structure to manage the properties of text overlays displayed in the application.
  - **Public X, Y**: Coordinates for the position of the text.
  - **Public Text**: The string to be displayed.
  - **Public Brush**: The brush used for rendering the text color.
  - **Public FontSize, Font**: Font size and font type for the text.
  - **Constructor**: Initializes the properties of the `TextDisplay` structure.

### Text Display Array
```vb
    Private TextDisplays() As TextDisplay = {
        New TextDisplay(0, 0, "Heading", Brushes.Transparent, 10, New Font("Segoe UI", 10)),
        New TextDisplay(0, 0, "Mouse", Brushes.Transparent, 10, New Font("Segoe UI", 10)),
        New TextDisplay(0, 0, "Radius", Brushes.Transparent, 10, New Font("Segoe UI", 10)),
        New TextDisplay(0, 0, "Center", Brushes.Transparent, 10, New Font("Segoe UI", 10)),
        New TextDisplay(0, 0, "Footer", Brushes.Transparent, 10, New Font("Segoe UI", 10))
    }
```
- **TextDisplays Array**: Initializes an array of `TextDisplay` structures to manage different text overlays (heading, mouse position, radius, center, footer).
- Each `TextDisplay` is initialized with default values, including position (0, 0), transparent brush, font size, and font type.

### Enum Definitions
```vb
    Private Enum TextDisplayIndex
        Heading = 0
        Mouse = 1
        Radius = 2
        Center = 3
        Footer = 4
    End Enum
```
- **TextDisplayIndex Enum**: Defines an enumeration to easily reference the indices of the `TextDisplays` array for better readability.

### Line Display Structure
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
- **Structure LineDisplay**: Manages properties for lines drawn on the canvas.
  - **Public X1, Y1, X2, Y2**: Coordinates for the start and end points of the line.
  - **Public Pen**: The pen used for drawing the line.
  - **Constructor**: Initializes the properties of the `LineDisplay` structure.

### Line Display Array
```vb
    Private LineDisplays() As LineDisplay = {
        New LineDisplay(CircleCenterPoint.X, CircleCenterPoint.Y, CircleCenterPoint.X + CircleRadius, CircleCenterPoint.Y, New Pen(Color.Chartreuse, 2)),
        New LineDisplay(CircleCenterPoint.X, CircleCenterPoint.Y, MousePointerLocation.X, CircleCenterPoint.Y, New Pen(Color.Chartreuse, 2)),
        New LineDisplay(MousePointerLocation.X, CircleCenterPoint.Y, MousePointerLocation.X, MousePointerLocation.Y, New Pen(Color.Chartreuse, 2)),
        New LineDisplay(CircleCenterPoint.X, CircleCenterPoint.Y, MousePointerLocation.X, MousePointerLocation.Y, New Pen(Color.Chartreuse, 2))
    }
```
- **LineDisplays Array**: Initializes an array of `LineDisplay` structures to represent lines drawn in the application.
- Each line is initialized with specific coordinates and a pen color (chartreuse) with a width of 2.

### Enum for Line Display Indices
```vb
    Private Enum LineDisplayIndex
        RadiusLine = 0
        XDistanceLine = 1
        YDistanceLine = 2
        DistanceLine = 3
    End Enum
```
- **LineDisplayIndex Enum**: Defines an enumeration for indexing the `LineDisplays` array, making the code more readable.

### Circle Display Structure
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
- **Structure CircleDisplay**: Manages properties for the circle and its graphical representation.
  - **Public X, Y**: Coordinates for the top-left corner of the bounding rectangle of the circle.
  - **Public Width, Height**: Dimensions of the circle.
  - **Public Brush**: The brush used for filling the circle.
  - **Constructor**: Initializes the properties of the `CircleDisplay` structure.

### Circle Display Array
```vb
    Private CircleDisplays() As CircleDisplay = {
        New CircleDisplay(CircleCenterPoint.X - CircleRadius, CircleCenterPoint.Y - CircleRadius, CircleRadius * 2, CircleRadius * 2, Brushes.LightGray),
        New CircleDisplay(CircleCenterPoint.X + CircleRadius - 3, CircleCenterPoint.Y - 3, 6, 6, Brushes.LightGray),
        New CircleDisplay(CircleCenterPoint.X - 3, CircleCenterPoint.Y - 3, 6, 6, Brushes.LightGray),
        New CircleDisplay(MousePointerLocation.X - 20, MousePointerLocation.Y - 20, 40, 40, MouseHilightBrush),
        New CircleDisplay(MousePointerLocation.X - 3, MousePointerLocation.Y - 3, 6, 6, Brushes.LightGray)
    }
```
- **CircleDisplays Array**: Initializes an array of `CircleDisplay` structures to manage the graphical representation of the circle and its components (center point, radius endpoint, mouse highlight).
- Each circle display is initialized with specific coordinates, dimensions, and brush colors.

### Enum for Circle Display Indices
```vb
    Private Enum CircleDisplayIndex
        Circle = 0
        RadiusEndPoint = 1
        CenterPoint = 2
        MouseHilight = 3
        MousePoint = 4
    End Enum
```
- **CircleDisplayIndex Enum**: Defines an enumeration for indexing the `CircleDisplays` array, improving code clarity.

### Variables for Circle Properties
```vb
    Private CircleCenterPoint As New Point(150, 150)
    Private MousePointerLocation As New Point(0, 0)
    Private CircleRadius As Integer = 300
    Private IsPointerInsideCircle As Boolean = False
    Private DistanceSquared As Double
    Private RadiusSquared As Double = CircleRadius * CircleRadius
```
- **CircleCenterPoint**: Initializes the center point of the circle.
- **MousePointerLocation**: Tracks the current location of the mouse pointer.
- **CircleRadius**: Sets the radius of the circle.
- **IsPointerInsideCircle**: Boolean flag to indicate if the mouse pointer is inside the circle.
- **DistanceSquared**: Variable to hold the squared distance from the mouse pointer to the circle's center.
- **RadiusSquared**: Pre-calculates the squared radius of the circle for efficient distance comparisons.

### Pens and Brushes
```vb
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
```
- **AdjustableArrowCap**: Creates custom arrow caps for the lines drawn.
- **Pens**: Initializes various pens with specific colors and widths for drawing lines.
- **Brushes**: Initializes brushes for filling shapes (circle, radius).

### Font Sizes
```vb
    Private HeadingFontSize As Integer = 16
    Private MouseFontSize As Integer = 12
    Private RadiusFontSize As Integer = 12
    Private CenterFontSize As Integer = 12
    Private FooterFontSize As Integer = 10
```
- **Font Sizes**: Sets default font sizes for different text displays in the application.

### Grid Pen
```vb
    Private gridPen As New Pen(Color.FromArgb(128, Color.LightGray), 2)
```
- **gridPen**: Initializes a pen for drawing a grid in the background of the application.

### View State Enum
```vb
    Private Enum ViewStateIndex
        Overview
        ParametersView
    End Enum
```
- **ViewStateIndex Enum**: Defines the different states of the application view (Overview and ParametersView).

### View State Variable
```vb
    Private ViewState As ViewStateIndex = ViewStateIndex.Overview
```
- **ViewState**: Initializes the current view state to Overview.

### Form Load Event
```vb
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        InitializeApp()
    End Sub
```
- **Form1_Load**: Event handler that runs when the form loads, calling the `InitializeApp` method to set up the initial state of the application.

### Mouse Enter Event
```vb
    Private Sub Form1_MouseEnter(sender As Object, e As EventArgs) Handles Me.MouseEnter
        UpdateViewMouseEnter()
        Invalidate()
        InvaildateButtons()
    End Sub
```
- **Form1_MouseEnter**: Event handler that triggers when the mouse enters the form. It updates the view and invalidates the form to refresh the display.

### Mouse Leave Event
```vb
    Private Sub Form1_MouseLeave(sender As Object, e As EventArgs) Handles Me.MouseLeave
        UpdateViewMouseLeave()
        Invalidate()
        InvaildateButtons()
    End Sub
```
- **Form1_MouseLeave**: Event handler that triggers when the mouse leaves the form. It updates the view to reflect the mouse leave state and refreshes the display.

### Mouse Move Event
```vb
    Protected Overrides Sub OnMouseMove(e As MouseEventArgs)
        MyBase.OnMouseMove(e)
        MousePointerLocation = e.Location
        IsPointerInsideCircle = IsPointInsideCircle(e.X, e.Y, CircleCenterPoint.X, CircleCenterPoint.Y, CircleRadius)
        UpdateViewOnMouseMove()
        Invalidate()
        InvaildateButtons()
    End Sub
```
- **OnMouseMove**: Overrides the default mouse move event to track the mouse pointer's location and check if it is inside the circle. It updates the view and refreshes the display.

### Paint Event
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
- **OnPaint**: Overrides the paint event to draw the graphical elements on the form.
  - **g**: Represents the graphics context for drawing.
  - **SmoothingMode**: Sets the rendering quality for smoother graphics.
  - **DrawGrid, DrawCircles, DrawLines, DrawTextOverlays**: Calls methods to render the grid, circles, lines, and text overlays.

### Overview Button Click Event
```vb
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
```
- **OverviewButton_Click**: Event handler for the overview button. It switches to the overview view, updates the display, and hides specific overlays and indicators.

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
        Invalidate()
        InvaildateButtons()
    End Sub
```
- **ParametersViewButton_Click**: Event handler for the parameters view button. It switches to the parameters view and updates the display while hiding specific overlays and indicators.

### Mouse Enter for Buttons
```vb
    Private Sub OverviewButton_MouseEnter(sender As Object, e As EventArgs) Handles OverviewButton.MouseEnter
        InvaildateButtons()
    End Sub

    Private Sub ParametersViewButton_MouseEnter(sender As Object, e As EventArgs) Handles ParametersViewButton.MouseEnter
        InvaildateButtons()
    End Sub
```
- **Mouse Enter Events**: Event handlers that trigger when the mouse enters the buttons, refreshing their appearance.

### Resize Event
```vb
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
```
- **OnResize**: Overrides the resize event to adjust the layout of buttons, circle geometry, font sizes, and other graphical elements when the form is resized.

### Point Inside Circle Function
```vb
    Function IsPointInsideCircle(pointX As Double, pointY As Double, centerX As Double, centerY As Double, radius As Double) As Boolean
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
```
- **IsPointInsideCircle**: Function that checks if a given point is inside or on the edge of a circle.
  - **Parameters**: Accepts coordinates of the point, center of the circle, and the radius.
  - **Calculations**: Computes the squared distance from the point to the center of the circle and compares it to the squared radius.
  - **Return Value**: Returns `True` if the point is inside or on the edge, and `False` if outside.

### Draw Circles Method
```vb
    Private Sub DrawCircles(g As Graphics)
        ' 🔵 Draw filled circles
        For Each circleDisplay As CircleDisplay In CircleDisplays
            g.FillEllipse(circleDisplay.Brush, circleDisplay.X, circleDisplay.Y, circleDisplay.Width, circleDisplay.Height)
        Next
    End Sub
```
- **DrawCircles**: Method that draws filled circles on the graphical interface.
  - **Parameter g**: Represents the graphics context for drawing.
  - **Loop**: Iterates through `CircleDisplays` to draw each circle using the specified brush and dimensions.

### Draw Lines Method
```vb
    Private Sub DrawLines(g As Graphics)
        ' 📏 Draw lines
        For Each lineDisplay As LineDisplay In LineDisplays
            g.DrawLine(lineDisplay.Pen, lineDisplay.X1, lineDisplay.Y1, lineDisplay.X2, lineDisplay.Y2)
        Next
    End Sub
```
- **DrawLines**: Method that draws lines on the graphical interface.
  - **Parameter g**: Represents the graphics context for drawing.
  - **Loop**: Iterates through `LineDisplays` to draw each line using the specified pen.

### Draw Text Overlays Method
```vb
    Private Sub DrawTextOverlays(g As Graphics)
        ' abc Draw text overlays
        For Each textDisplay As TextDisplay In TextDisplays
            g.DrawString(textDisplay.Text, textDisplay.Font, textDisplay.Brush, textDisplay.X, textDisplay.Y)
        Next
    End Sub
```
- **DrawTextOverlays**: Method that draws text overlays on the graphical interface.
  - **Parameter g**: Represents the graphics context for drawing.
  - **Loop**: Iterates through `TextDisplays` to render each text string at the specified coordinates with the specified font and brush.

### Draw Grid Method
```vb
    Private Sub DrawGrid(g As Graphics)
        ' 🔲 Draw grid ( lines every 50 pixels)
        For x As Integer = 0 To ClientSize.Width Step 50
            g.DrawLine(gridPen, x, 0, x, ClientSize.Height)
        Next
        For y As Integer = 0 To ClientSize.Height Step 50
            g.DrawLine(gridPen, 0, y, ClientSize.Width, y)
        Next
    End Sub
```
- **DrawGrid**: Method that draws a grid on the background of the graphical interface.
  - **Parameter g**: Represents the graphics context for drawing.
  - **Loops**: Draw vertical lines every 50 pixels across the width and horizontal lines every 50 pixels across the height.

### Update Button Layout Method
```vb
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
```
- **UpdateButtonLayout**: Method that adjusts the size and position of the buttons based on the current client size of the form.
  - **Width and Height**: Sets minimum dimensions for the buttons.
  - **Font**: Adjusts the font size based on the form size.
  - **SetBounds**: Positions the buttons at the bottom right of the form.

### Update Circle Geometry Method
```vb
    Private Sub UpdateCircleGeometry()
        CircleCenterPoint = New Point(ClientSize.Width \ 2, ClientSize.Height \ 2)
        CircleRadius = Math.Min(ClientSize.Width, ClientSize.Height) \ 3.4
        RadiusSquared = CircleRadius * CircleRadius
    End Sub
```
- **UpdateCircleGeometry**: Method that recalculates the center point and radius of the circle based on the current size of the form.
  - **CircleCenterPoint**: Centers the circle in the form.
  - **CircleRadius**: Sets the radius to a fraction of the smaller dimension of the form.
  - **RadiusSquared**: Updates the squared radius for efficient distance calculations.

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
- **UpdateFontSizes**: Method that adjusts the font sizes for text displays based on the current size of the form.
  - **baseSize**: Sets a base size based on the smaller dimension of the form.
  - **Font Sizes**: Ensures that font sizes do not fall below a minimum value.

### Update Text Displays on Resize Method
```vb
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
```
- **UpdateTextDisplaysOnResize**: Method that updates the positions and contents of text displays when the form is resized.
  - **Graphics Context**: Creates a graphics context for measuring text sizes.
  - **Text Updates**: Adjusts the text and positions of each display based on the current view state and form size.

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
```
- **UpdateLineDisplays**: Method that updates the positions and visibility of lines based on the current state of the application.
  - **Loop**: Iterates through the `LineDisplays` to set their positions and pens based on the view state.

### Set Line Method
```vb
    Private Sub SetLine(ByRef ld As LineDisplay, p1 As Point, p2 As Point)
        ld.X1 = p1.X
        ld.Y1 = p1.Y
        ld.X2 = p2.X
        ld.Y2 = p2.Y
    End Sub
```
- **SetLine**: Helper method that sets the coordinates of a line display based on two points.

### Set Text Display Transparent Method
```vb
    Private Sub SetTextDisplayTransparent(index As TextDisplayIndex)
        Dim td = TextDisplays(index)
        td.Brush = Brushes.Transparent
        TextDisplays(index) = td
    End Sub
```
- **SetTextDisplayTransparent**: Method that makes a text display transparent by setting its brush to transparent.

### Set Circle Display Transparent Method
```vb
    Private Sub SetCircleDisplayTransparent(index As CircleDisplayIndex)
        Dim cd = CircleDisplays(index)
        cd.Brush = Brushes.Transparent
        CircleDisplays(index) = cd
    End Sub
```
- **SetCircleDisplayTransparent**: Method that makes a circle display transparent by setting its brush to transparent.

### Invalidate Buttons Method
```vb
    Private Sub InvaildateButtons()
        ' Invalidate the buttons to update their appearance
        OverviewButton.Invalidate()
        ParametersViewButton.Invalidate()
    End Sub
```
- **InvaildateButtons**: Method that refreshes the appearance of the buttons to reflect any changes in state.

### Switch to Overview Method
```vb
    Private Sub Switch2Overview()
        ' Switch to Overview
        ViewState = ViewStateIndex.Overview
    End Sub
```
- **Switch2Overview**: Method that sets the view state to Overview.

### Switch to Parameters View Method
```vb
    Private Sub Switch2ParametersView()
        ' Switch to ParametersView
        ViewState = ViewStateIndex.ParametersView
    End Sub
```
- **Switch2ParametersView**: Method that sets the view state to ParametersView.

### Update View Method
```vb
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
```
- **UpdateView**: Method that updates the visual elements of the application based on the current state.
  - **Update Methods**: Calls methods to update the brushes, line displays, circle displays, text displays, and grid pen.

### Update View on Mouse Move Method
```vb
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
```
- **UpdateViewOnMouseMove**: Method that updates the visual elements based on the mouse movement.
  - **Text Updates**: Adjusts the text displays for mouse position and other relevant information.

### Initialize Application Method
```vb
    Private Sub InitializeApp()
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
```
- **InitializeApp**: This method sets up the initial state of various graphical elements in the application.
  - **Grid Pen**: Sets the grid pen to transparent initially.
  - **Line Pens**: Configures the start and end caps for the radius and distance lines.
  - **Mouse Point Brush**: Initializes the mouse point brush as transparent.
  
- **Text Displays Loop**: Iterates through each `TextDisplay` in the `TextDisplays` array:
  - **Radius Display**: Sets the text for the radius display and assigns a black brush.
  - **Other Displays**: Sets the brush to transparent for all other text displays.

- **Line Displays Loop**: Iterates through each `LineDisplay` in the `LineDisplays` array:
  - **Radius Line**: Sets the coordinates for the radius line and assigns the radius pen.
  - **Other Lines**: Initializes other lines to start and end at (0,0) with a transparent pen.

- **Circle Displays Loop**: Iterates through each `CircleDisplay` in the `CircleDisplays` array:
  - **Circle**: Sets the position and size of the main circle based on the center point and radius.
  - **Radius End Point**: Sets the position for the endpoint of the radius line.
  - **Center Point**: Sets the position for the center point of the circle.
  - **Mouse Point and Highlight**: Initializes these displays to be transparent.


This detailed breakdown covers the entire code structure and functionality of the **Inside or Out of Circle** application. The application is designed to provide a visual representation of whether a point (the mouse pointer) is inside or outside a circle, utilizing efficient squared distance calculations to enhance performance.

### Summary of Key Components
- **Structures and Enums**: Define how text, lines, and circles are represented.
- **Event Handlers**: Manage user interactions such as mouse movements and button clicks.
- **Drawing Methods**: Handle the rendering of circles, lines, text, and grids on the form.
- **Initialization Methods**: Set up the application state and graphical elements when the form loads or resizes.
































