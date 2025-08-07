<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        ParametersViewButton = New Button()
        OverviewButton = New Button()
        XDistanceViewButton = New Button()
        SuspendLayout()
        ' 
        ' ParametersViewButton
        ' 
        ParametersViewButton.Font = New Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        ParametersViewButton.Location = New Point(1118, 605)
        ParametersViewButton.Name = "ParametersViewButton"
        ParametersViewButton.Size = New Size(64, 64)
        ParametersViewButton.TabIndex = 0
        ParametersViewButton.Text = "(  )"
        ParametersViewButton.UseVisualStyleBackColor = True
        ' 
        ' OverviewButton
        ' 
        OverviewButton.Font = New Font("Segoe UI", 26.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        OverviewButton.Location = New Point(1048, 605)
        OverviewButton.Name = "OverviewButton"
        OverviewButton.Size = New Size(64, 64)
        OverviewButton.TabIndex = 1
        OverviewButton.Text = ""
        OverviewButton.UseVisualStyleBackColor = True
        ' 
        ' XDistanceViewButton
        ' 
        XDistanceViewButton.Font = New Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        XDistanceViewButton.Location = New Point(1188, 605)
        XDistanceViewButton.Name = "XDistanceViewButton"
        XDistanceViewButton.Size = New Size(64, 64)
        XDistanceViewButton.TabIndex = 2
        XDistanceViewButton.Text = "↔"
        XDistanceViewButton.UseVisualStyleBackColor = True
        ' 
        ' Form1
        ' 
        AutoScaleMode = AutoScaleMode.None
        ClientSize = New Size(1264, 681)
        Controls.Add(XDistanceViewButton)
        Controls.Add(OverviewButton)
        Controls.Add(ParametersViewButton)
        DoubleBuffered = True
        Font = New Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        MinimumSize = New Size(400, 256)
        Name = "Form1"
        Text = "Inside or Out of Circle - Code with Joe"
        ResumeLayout(False)
    End Sub

    Friend WithEvents ParametersViewButton As Button
    Friend WithEvents OverviewButton As Button
    Friend WithEvents XDistanceViewButton As Button

End Class
