Namespace dxSample
	Partial Public Class Form1
		''' <summary>
		''' Required designer variable.
		''' </summary>
		Private components As System.ComponentModel.IContainer = Nothing

		''' <summary>
		''' Clean up any resources being used.
		''' </summary>
		''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		Protected Overrides Sub Dispose(ByVal disposing As Boolean)
			If disposing AndAlso (components IsNot Nothing) Then
				components.Dispose()
			End If
			MyBase.Dispose(disposing)
		End Sub

		#Region "Windows Form Designer generated code"

		''' <summary>
		''' Required method for Designer support - do not modify
		''' the contents of this method with the code editor.
		''' </summary>
		Private Sub InitializeComponent()
			Me.components = New System.ComponentModel.Container()
			Dim transition2 As New DevExpress.Utils.Animation.Transition()
			Dim zoomTransition2 As New DevExpress.Utils.Animation.ZoomTransition()
			Dim resources As New System.ComponentModel.ComponentResourceManager(GetType(Form1))
			Me.gridControl1 = New DevExpress.XtraGrid.GridControl()
			Me.winExplorerView1 = New DevExpress.XtraGrid.Views.WinExplorer.WinExplorerView()
			Me.columnName = New DevExpress.XtraGrid.Columns.GridColumn()
			Me.columnImage = New DevExpress.XtraGrid.Columns.GridColumn()
			Me.transitionManager1 = New DevExpress.Utils.Animation.TransitionManager(Me.components)
			Me.barManager1 = New DevExpress.XtraBars.BarManager(Me.components)
			Me.bar2 = New DevExpress.XtraBars.Bar()
			Me.backButton = New DevExpress.XtraBars.BarButtonItem()
			Me.forwardButton = New DevExpress.XtraBars.BarButtonItem()
			Me.barEditItem1 = New DevExpress.XtraBars.BarEditItem()
			Me.repositoryItemBreadCrumbEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemBreadCrumbEdit()
			Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
			Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
			Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
			Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
			Me.barButtonItem1 = New DevExpress.XtraBars.BarButtonItem()
			CType(Me.gridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.winExplorerView1, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.barManager1, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.repositoryItemBreadCrumbEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.SuspendLayout()
			' 
			' gridControl1
			' 
			Me.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill
			Me.gridControl1.Location = New System.Drawing.Point(0, 36)
			Me.gridControl1.MainView = Me.winExplorerView1
			Me.gridControl1.Name = "gridControl1"
			Me.gridControl1.Size = New System.Drawing.Size(1170, 631)
			Me.gridControl1.TabIndex = 0
			Me.gridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() { Me.winExplorerView1})
			' 
			' winExplorerView1
			' 
			Me.winExplorerView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() { Me.columnName, Me.columnImage})
			Me.winExplorerView1.ColumnSet.ExtraLargeImageColumn = Me.columnImage
			Me.winExplorerView1.ColumnSet.TextColumn = Me.columnName
			Me.winExplorerView1.GridControl = Me.gridControl1
			Me.winExplorerView1.Name = "winExplorerView1"
			Me.winExplorerView1.OptionsBehavior.Editable = False
			Me.winExplorerView1.OptionsView.ImageLayoutMode = DevExpress.Utils.Drawing.ImageLayoutMode.Stretch
			Me.winExplorerView1.OptionsView.Style = DevExpress.XtraGrid.Views.WinExplorer.WinExplorerViewStyle.ExtraLarge
			Me.winExplorerView1.OptionsViewStyles.ExtraLarge.ImageSize = New System.Drawing.Size(128, 128)
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.winExplorerView1.ItemDoubleClick += new DevExpress.XtraGrid.Views.WinExplorer.WinExplorerViewItemDoubleClickEventHandler(this.winExplorerView1_ItemDoubleClick);
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.winExplorerView1.ItemKeyDown += new DevExpress.XtraGrid.Views.WinExplorer.WinExplorerViewItemKeyDownEventHandler(this.winExplorerView1_ItemKeyDown);
			' 
			' columnName
			' 
			Me.columnName.Caption = "columnName"
			Me.columnName.FieldName = "Name"
			Me.columnName.MinWidth = 30
			Me.columnName.Name = "columnName"
			Me.columnName.Visible = True
			Me.columnName.VisibleIndex = 0
			Me.columnName.Width = 112
			' 
			' columnImage
			' 
			Me.columnImage.Caption = "columnImage"
			Me.columnImage.FieldName = "Image"
			Me.columnImage.MinWidth = 30
			Me.columnImage.Name = "columnImage"
			Me.columnImage.Visible = True
			Me.columnImage.VisibleIndex = 0
			Me.columnImage.Width = 112
			' 
			' transitionManager1
			' 
			Me.transitionManager1.FrameCount = 450
			transition2.BarWaitingIndicatorProperties.Caption = ""
			transition2.BarWaitingIndicatorProperties.Description = ""
			transition2.Control = Me.gridControl1
			transition2.LineWaitingIndicatorProperties.AnimationElementCount = 5
			transition2.LineWaitingIndicatorProperties.Caption = ""
			transition2.LineWaitingIndicatorProperties.Description = ""
			transition2.RingWaitingIndicatorProperties.AnimationElementCount = 5
			transition2.RingWaitingIndicatorProperties.Caption = ""
			transition2.RingWaitingIndicatorProperties.Description = ""
			transition2.TransitionType = zoomTransition2
			transition2.WaitingIndicatorProperties.Caption = ""
			transition2.WaitingIndicatorProperties.Description = ""
			Me.transitionManager1.Transitions.Add(transition2)
			Me.transitionManager1.UseDirectXPaint = DevExpress.Utils.DefaultBoolean.True
			' 
			' barManager1
			' 
			Me.barManager1.Bars.AddRange(New DevExpress.XtraBars.Bar() { Me.bar2})
			Me.barManager1.DockControls.Add(Me.barDockControlTop)
			Me.barManager1.DockControls.Add(Me.barDockControlBottom)
			Me.barManager1.DockControls.Add(Me.barDockControlLeft)
			Me.barManager1.DockControls.Add(Me.barDockControlRight)
			Me.barManager1.Form = Me
			Me.barManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() { Me.forwardButton, Me.backButton, Me.barButtonItem1, Me.barEditItem1})
			Me.barManager1.MainMenu = Me.bar2
			Me.barManager1.MaxItemId = 6
			Me.barManager1.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() { Me.repositoryItemBreadCrumbEdit1})
			' 
			' bar2
			' 
			Me.bar2.BarName = "Main menu"
			Me.bar2.DockCol = 0
			Me.bar2.DockRow = 0
			Me.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
			Me.bar2.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {
				New DevExpress.XtraBars.LinkPersistInfo(Me.backButton),
				New DevExpress.XtraBars.LinkPersistInfo(Me.forwardButton),
				New DevExpress.XtraBars.LinkPersistInfo(Me.barEditItem1)
			})
			Me.bar2.OptionsBar.AllowQuickCustomization = False
			Me.bar2.OptionsBar.DisableCustomization = True
			Me.bar2.OptionsBar.DrawDragBorder = False
			Me.bar2.Text = "Main menu"
			' 
			' backButton
			' 
			Me.backButton.Caption = "Back"
			Me.backButton.Id = 3
			Me.backButton.ImageOptions.SvgImage = (CType(resources.GetObject("barButtonItem4.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage))
			Me.backButton.Name = "backButton"
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.backButton.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OnBackButtonClick);
			' 
			' forwardButton
			' 
			Me.forwardButton.Caption = "Forward"
			Me.forwardButton.Id = 2
			Me.forwardButton.ImageOptions.SvgImage = (CType(resources.GetObject("barButtonItem3.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage))
			Me.forwardButton.Name = "forwardButton"
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.forwardButton.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OnForwardButtonClick);
			' 
			' barEditItem1
			' 
			Me.barEditItem1.AutoFillWidth = True
			Me.barEditItem1.Caption = "Path"
			Me.barEditItem1.Edit = Me.repositoryItemBreadCrumbEdit1
			Me.barEditItem1.Id = 5
			Me.barEditItem1.Name = "barEditItem1"
			' 
			' repositoryItemBreadCrumbEdit1
			' 
			Me.repositoryItemBreadCrumbEdit1.AutoHeight = False
			Me.repositoryItemBreadCrumbEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() { New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
			Me.repositoryItemBreadCrumbEdit1.Name = "repositoryItemBreadCrumbEdit1"
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.repositoryItemBreadCrumbEdit1.QueryChildNodes += new DevExpress.XtraEditors.BreadCrumbQueryChildNodesEventHandler(this.repositoryItemBreadCrumbEdit1_QueryChildNodes);
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.repositoryItemBreadCrumbEdit1.ValidatePath += new DevExpress.XtraEditors.BreadCrumbValidatePathEventHandler(this.repositoryItemBreadCrumbEdit1_ValidatePath);
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.repositoryItemBreadCrumbEdit1.NewNodeAdding += new DevExpress.XtraEditors.BreadCrumbNewNodeAddingEventHandler(this.repositoryItemBreadCrumbEdit1_NewNodeAdding);
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.repositoryItemBreadCrumbEdit1.EditValueChanged += new System.EventHandler(this.OnBreadCrumbValueChanged);
			' 
			' barDockControlTop
			' 
			Me.barDockControlTop.CausesValidation = False
			Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
			Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
			Me.barDockControlTop.Manager = Me.barManager1
			Me.barDockControlTop.Size = New System.Drawing.Size(1170, 36)
			' 
			' barDockControlBottom
			' 
			Me.barDockControlBottom.CausesValidation = False
			Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
			Me.barDockControlBottom.Location = New System.Drawing.Point(0, 667)
			Me.barDockControlBottom.Manager = Me.barManager1
			Me.barDockControlBottom.Size = New System.Drawing.Size(1170, 0)
			' 
			' barDockControlLeft
			' 
			Me.barDockControlLeft.CausesValidation = False
			Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
			Me.barDockControlLeft.Location = New System.Drawing.Point(0, 36)
			Me.barDockControlLeft.Manager = Me.barManager1
			Me.barDockControlLeft.Size = New System.Drawing.Size(0, 631)
			' 
			' barDockControlRight
			' 
			Me.barDockControlRight.CausesValidation = False
			Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
			Me.barDockControlRight.Location = New System.Drawing.Point(1170, 36)
			Me.barDockControlRight.Manager = Me.barManager1
			Me.barDockControlRight.Size = New System.Drawing.Size(0, 631)
			' 
			' barButtonItem1
			' 
			Me.barButtonItem1.Caption = "barButtonItem1"
			Me.barButtonItem1.Id = 4
			Me.barButtonItem1.ImageOptions.SvgImage = (CType(resources.GetObject("barButtonItem1.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage))
			Me.barButtonItem1.Name = "barButtonItem1"
			' 
			' Form1
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(9F, 19F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.ClientSize = New System.Drawing.Size(1170, 667)
			Me.Controls.Add(Me.gridControl1)
			Me.Controls.Add(Me.barDockControlLeft)
			Me.Controls.Add(Me.barDockControlRight)
			Me.Controls.Add(Me.barDockControlBottom)
			Me.Controls.Add(Me.barDockControlTop)
			Me.Margin = New System.Windows.Forms.Padding(4)
			Me.Name = "Form1"
			Me.Text = "Form1"
			CType(Me.gridControl1, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.winExplorerView1, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.barManager1, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.repositoryItemBreadCrumbEdit1, System.ComponentModel.ISupportInitialize).EndInit()
			Me.ResumeLayout(False)
			Me.PerformLayout()

		End Sub

		#End Region

		Private gridControl1 As DevExpress.XtraGrid.GridControl
		Private transitionManager1 As DevExpress.Utils.Animation.TransitionManager
		Private barManager1 As DevExpress.XtraBars.BarManager
		Private bar2 As DevExpress.XtraBars.Bar
		Private columnImage As DevExpress.XtraGrid.Columns.GridColumn
		Private columnName As DevExpress.XtraGrid.Columns.GridColumn
		Private barDockControlTop As DevExpress.XtraBars.BarDockControl
		Private barDockControlBottom As DevExpress.XtraBars.BarDockControl
		Private barDockControlLeft As DevExpress.XtraBars.BarDockControl
		Private barDockControlRight As DevExpress.XtraBars.BarDockControl
		Private WithEvents winExplorerView1 As DevExpress.XtraGrid.Views.WinExplorer.WinExplorerView
		Private WithEvents backButton As DevExpress.XtraBars.BarButtonItem
		Private WithEvents forwardButton As DevExpress.XtraBars.BarButtonItem
		Private barButtonItem1 As DevExpress.XtraBars.BarButtonItem
		Private barEditItem1 As DevExpress.XtraBars.BarEditItem
		Private WithEvents repositoryItemBreadCrumbEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemBreadCrumbEdit
	End Class
End Namespace

