Imports DevExpress.Utils.Animation
Imports DevExpress.Utils.Helpers
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Views.WinExplorer.ViewInfo
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms

Namespace dxSample
	Partial Public Class Form1
		Inherits XtraForm

		Public Sub New()
			InitializeComponent()
			Dim startPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
			Dim startNode = New NodeNavigationInfo(startPath)
			SetActivePath(startNode)
		End Sub
		Private currentPath As NodeNavigationInfo
		Private Property CurrentNode() As NodeNavigationInfo
			Get
				Return currentPath
			End Get
			Set(ByVal value As NodeNavigationInfo)
				If CurrentNode Is value Then
					Return
				End If
				currentPath = value
				UpdateDataSource()
			End Set
		End Property
		Private ReadOnly Property GridClientBounds() As Rectangle
			Get
				Return New Rectangle(Point.Empty, Me.gridControl1.Size)
			End Get
		End Property

		Private Shared ReadOnly ImageSize As New Size(256, 256)
		Private Sub UpdateDataSource()
			Dim oldCursor As Cursor = Cursor.Current
			Cursor.Current = Cursors.WaitCursor
			Try
				If Not String.IsNullOrEmpty(CurrentNode.Path) Then
					Me.gridControl1.DataSource = FileSystemHelper.GetFileSystemEntries(CurrentNode.Path, IconSizeType.ExtraLarge, ImageSize)
				Else
					Me.gridControl1.DataSource = Nothing
				End If
				Me.barEditItem1.EditValue = CurrentNode.Path
			Finally
				Cursor.Current = oldCursor
			End Try
		End Sub

		Private Sub winExplorerView1_ItemKeyDown(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.WinExplorer.WinExplorerViewItemKeyEventArgs) Handles winExplorerView1.ItemKeyDown
			If Me.transitionManager1.IsTransition Then
				Return
			End If
			Select Case e.KeyInfo.KeyCode
				Case Keys.Enter
					OnItemClick(e.ItemInfo)
				Case Keys.Back
					RunZoomOutAnimation(Sub()
						GoBackCore()
					End Sub)
			End Select
		End Sub
		Private Sub winExplorerView1_ItemDoubleClick(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.WinExplorer.WinExplorerViewItemDoubleClickEventArgs) Handles winExplorerView1.ItemDoubleClick
			OnItemClick(e.ItemInfo)
		End Sub

		Private Sub OnItemClick(ByVal itemInfo As WinExplorerItemViewInfo)
			If itemInfo Is Nothing OrElse TypeOf itemInfo.Row.RowKey Is FileEntry Then
				Return
			End If
			Dim entry = CType(itemInfo.Row.RowKey, FileSystemEntry)
			Dim activeNode = New NodeNavigationInfo(entry.Path, itemInfo.Bounds)
			RunZoomInAnimation(activeNode, Sub()
				SetActivePath(activeNode)
			End Sub)
		End Sub

		Private Sub OnBackButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles backButton.ItemClick
			If Me.transitionManager1.IsTransition Then
				Return
			End If
			RunZoomOutAnimation(Sub()
				GoBackCore()
			End Sub)
		End Sub

		Private Sub OnForwardButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles forwardButton.ItemClick
			If Me.transitionManager1.IsTransition Then
				Return
			End If
			Dim currentIndex As Integer = PathHistory.IndexOf(CurrentNode)
			If currentIndex >= PathHistory.Count - 1 Then
				Return
			End If
			Dim activeNode = PathHistory(currentIndex + 1)
			RunZoomInAnimation(activeNode, Sub()
				GoForward()
			End Sub)
		End Sub

		Private PathHistory As New List(Of NodeNavigationInfo)()
		Private Sub SetActivePath(ByVal node As NodeNavigationInfo)
			Dim currentIndex As Integer = PathHistory.IndexOf(CurrentNode)
			CurrentNode = node
			If currentIndex > -1 Then
				Dim pathCount As Integer = PathHistory.Count
				Do While pathCount > currentIndex + 1
					PathHistory.RemoveAt(pathCount - 1)
					pathCount -= 1
				Loop
			End If
			PathHistory.Add(CurrentNode)
		End Sub
		Private Sub GoBackCore()
			Dim currentIndex As Integer = PathHistory.IndexOf(CurrentNode)
			If currentIndex < 1 Then
				Return
			End If
			CurrentNode = PathHistory(currentIndex - 1)
		End Sub
		Private Sub GoForward()
			Dim currentIndex As Integer = PathHistory.IndexOf(CurrentNode)
			If currentIndex >= PathHistory.Count - 1 Then
				Return
			End If
			CurrentNode = PathHistory(currentIndex + 1)
		End Sub

		Private Sub OnBreadCrumbValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles repositoryItemBreadCrumbEdit1.EditValueChanged
			Dim breadCrumb = DirectCast(sender, BreadCrumbEdit)
			Dim path = TryCast(breadCrumb.EditValue, String)
			If Me.transitionManager1.IsTransition OrElse path Is Nothing Then
				Return
			End If
			Dim activeNode = New NodeNavigationInfo(path, New Rectangle(GridClientBounds.Width \ 2, GridClientBounds.Height \ 2, 1, 1))
			RunZoomInAnimation(activeNode, Sub()
				SetActivePath(activeNode)
			End Sub)
		End Sub

		Private Sub RunZoomInAnimation(ByVal activeNode As NodeNavigationInfo, ByVal action As Action)
			Dim zoomTransition = Me.transitionManager1.GetTransition(Of ZoomTransition)(Me.gridControl1)
			Me.transitionManager1.StartTransition(Me.gridControl1)
			zoomTransition.ActiveSettings = New ZoomTransitionSettings() With {
				.SourceBounds = activeNode.SourceBounds,
				.TargetBounds = GridClientBounds
			}
			action()
			Me.transitionManager1.EndTransition()
		End Sub
		Private Sub RunZoomOutAnimation(ByVal action As Action)
			Dim zoomTransition = Me.transitionManager1.GetTransition(Of ZoomTransition)(Me.gridControl1)
			Me.transitionManager1.StartTransition(Me.gridControl1)
			zoomTransition.ActiveSettings = New ZoomTransitionSettings() With {
				.Direction = ZoomTransitionDirection.ZoomOut,
				.SourceBounds = GridClientBounds
			}
			action()
			Me.transitionManager1.EndTransition()
		End Sub
		#Region "BreadCrumb node generation"
		Private Sub repositoryItemBreadCrumbEdit1_ValidatePath(ByVal sender As Object, ByVal e As BreadCrumbValidatePathEventArgs) Handles repositoryItemBreadCrumbEdit1.ValidatePath
			If Not FileSystemHelper.IsDirExists(e.Path) Then
				e.ValidationResult = BreadCrumbValidatePathResult.Cancel
				Return
			End If
			e.ValidationResult = BreadCrumbValidatePathResult.CreateNodes
		End Sub
		Private Sub repositoryItemBreadCrumbEdit1_NewNodeAdding(ByVal sender As Object, ByVal e As BreadCrumbNewNodeAddingEventArgs) Handles repositoryItemBreadCrumbEdit1.NewNodeAdding
			e.Node.PopulateOnDemand = True
		End Sub
		Private Sub repositoryItemBreadCrumbEdit1_QueryChildNodes(ByVal sender As Object, ByVal e As BreadCrumbQueryChildNodesEventArgs) Handles repositoryItemBreadCrumbEdit1.QueryChildNodes
			Dim dir As String = e.Node.Path
			If Not FileSystemHelper.IsDirExists(dir) Then
				Return
			End If
			Dim subDirs() As String = FileSystemHelper.GetSubFolders(dir)
			For i As Integer = 0 To subDirs.Length - 1
				e.Node.ChildNodes.Add(CreateNode(subDirs(i)))
			Next i
		End Sub
		Private Function CreateNode(ByVal path As String) As BreadCrumbNode
			Dim folderName As String = FileSystemHelper.GetDirName(path)
			Return New BreadCrumbNode(folderName, folderName, True)
		End Function
		#End Region
	End Class
	Friend Class NodeNavigationInfo
		Public Sub New(ByVal path As String)
			Me.New(path, Rectangle.Empty)
		End Sub
		Public Sub New(ByVal path As String, ByVal sourceBounds As Rectangle)
			Me.SourceBounds = sourceBounds
			Me.Path = path
		End Sub
		Private privateSourceBounds As Rectangle
		Public Property SourceBounds() As Rectangle
			Get
				Return privateSourceBounds
			End Get
			Private Set(ByVal value As Rectangle)
				privateSourceBounds = value
			End Set
		End Property
		Private privatePath As String
		Public Property Path() As String
			Get
				Return privatePath
			End Get
			Private Set(ByVal value As String)
				privatePath = value
			End Set
		End Property
	End Class
End Namespace
