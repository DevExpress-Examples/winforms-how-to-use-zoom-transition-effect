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

    Public Partial Class Form1
        Inherits DevExpress.XtraEditors.XtraForm

        Public Sub New()
            Me.InitializeComponent()
            Dim startPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop)
            Dim startNode = New dxSample.NodeNavigationInfo(startPath)
            Me.SetActivePath(startNode)
        End Sub

        Private currentPath As dxSample.NodeNavigationInfo

        Private Property CurrentNode As NodeNavigationInfo
            Get
                Return Me.currentPath
            End Get

            Set(ByVal value As NodeNavigationInfo)
                If Me.CurrentNode Is value Then Return
                Me.currentPath = value
                Me.UpdateDataSource()
            End Set
        End Property

        Private ReadOnly Property GridClientBounds As Rectangle
            Get
                Return New System.Drawing.Rectangle(System.Drawing.Point.Empty, Me.gridControl1.Size)
            End Get
        End Property

        Private Shared ReadOnly ImageSize As System.Drawing.Size = New System.Drawing.Size(256, 256)

        Private Sub UpdateDataSource()
            Dim oldCursor As System.Windows.Forms.Cursor = System.Windows.Forms.Cursor.Current
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            Try
                If Not String.IsNullOrEmpty(Me.CurrentNode.Path) Then
                    Me.gridControl1.DataSource = DevExpress.Utils.Helpers.FileSystemHelper.GetFileSystemEntries(Me.CurrentNode.Path, DevExpress.Utils.Helpers.IconSizeType.ExtraLarge, dxSample.Form1.ImageSize)
                Else
                    Me.gridControl1.DataSource = Nothing
                End If

                Me.barEditItem1.EditValue = Me.CurrentNode.Path
            Finally
                System.Windows.Forms.Cursor.Current = oldCursor
            End Try
        End Sub

        Private Sub winExplorerView1_ItemKeyDown(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.WinExplorer.WinExplorerViewItemKeyEventArgs)
            If Me.transitionManager1.IsTransition Then Return
            Select Case e.KeyInfo.KeyCode
                Case System.Windows.Forms.Keys.Enter
                    Me.OnItemClick(e.ItemInfo)
                Case System.Windows.Forms.Keys.Back
                    Me.RunZoomOutAnimation(Sub() Me.GoBackCore())
            End Select
        End Sub

        Private Sub winExplorerView1_ItemDoubleClick(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.WinExplorer.WinExplorerViewItemDoubleClickEventArgs)
            Me.OnItemClick(e.ItemInfo)
        End Sub

        Private Sub OnItemClick(ByVal itemInfo As DevExpress.XtraGrid.Views.WinExplorer.ViewInfo.WinExplorerItemViewInfo)
            If itemInfo Is Nothing OrElse TypeOf itemInfo.Row.RowKey Is DevExpress.Utils.Helpers.FileEntry Then Return
            Dim entry = CType(itemInfo.Row.RowKey, DevExpress.Utils.Helpers.FileSystemEntry)
            Dim activeNode = New dxSample.NodeNavigationInfo(entry.Path, itemInfo.Bounds)
            Me.RunZoomInAnimation(activeNode, Sub() Me.SetActivePath(activeNode))
        End Sub

        Private Sub OnBackButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs)
            If Me.transitionManager1.IsTransition Then Return
            Me.RunZoomOutAnimation(Sub() Me.GoBackCore())
        End Sub

        Private Sub OnForwardButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs)
            If Me.transitionManager1.IsTransition Then Return
            Dim currentIndex As Integer = Me.PathHistory.IndexOf(Me.CurrentNode)
            If currentIndex >= Me.PathHistory.Count - 1 Then Return
            Dim activeNode = Me.PathHistory(currentIndex + 1)
            Me.RunZoomInAnimation(activeNode, Sub() Me.GoForward())
        End Sub

        Private PathHistory As System.Collections.Generic.List(Of dxSample.NodeNavigationInfo) = New System.Collections.Generic.List(Of dxSample.NodeNavigationInfo)()

        Private Sub SetActivePath(ByVal node As dxSample.NodeNavigationInfo)
            Dim currentIndex As Integer = Me.PathHistory.IndexOf(Me.CurrentNode)
            Me.CurrentNode = node
            If currentIndex > -1 Then
                Dim pathCount As Integer = Me.PathHistory.Count
                While pathCount > currentIndex + 1
                    Me.PathHistory.RemoveAt(pathCount - 1)
                    pathCount -= 1
                End While
            End If

            Me.PathHistory.Add(Me.CurrentNode)
        End Sub

        Private Sub GoBackCore()
            Dim currentIndex As Integer = Me.PathHistory.IndexOf(Me.CurrentNode)
            If currentIndex < 1 Then Return
            Me.CurrentNode = Me.PathHistory(currentIndex - 1)
        End Sub

        Private Sub GoForward()
            Dim currentIndex As Integer = Me.PathHistory.IndexOf(Me.CurrentNode)
            If currentIndex >= Me.PathHistory.Count - 1 Then Return
            Me.CurrentNode = Me.PathHistory(currentIndex + 1)
        End Sub

        Private Sub OnBreadCrumbValueChanged(ByVal sender As Object, ByVal e As System.EventArgs)
            Dim breadCrumb = CType(sender, DevExpress.XtraEditors.BreadCrumbEdit)
            Dim path = TryCast(breadCrumb.EditValue, String)
            If Me.transitionManager1.IsTransition OrElse Equals(path, Nothing) Then Return
            Dim activeNode = New dxSample.NodeNavigationInfo(path, New System.Drawing.Rectangle(Me.GridClientBounds.Width \ 2, Me.GridClientBounds.Height \ 2, 1, 1))
            Me.RunZoomInAnimation(activeNode, Sub() Me.SetActivePath(activeNode))
        End Sub

        Private Sub RunZoomInAnimation(ByVal activeNode As dxSample.NodeNavigationInfo, ByVal action As System.Action)
            Dim zoomTransition = Me.transitionManager1.GetTransition(Of DevExpress.Utils.Animation.ZoomTransition)(Me.gridControl1)
            Me.transitionManager1.StartTransition(Me.gridControl1)
            zoomTransition.ActiveSettings = New DevExpress.Utils.Animation.ZoomTransitionSettings() With {.SourceBounds = activeNode.SourceBounds, .TargetBounds = Me.GridClientBounds}
            action()
            Me.transitionManager1.EndTransition()
        End Sub

        Private Sub RunZoomOutAnimation(ByVal action As System.Action)
            Dim zoomTransition = Me.transitionManager1.GetTransition(Of DevExpress.Utils.Animation.ZoomTransition)(Me.gridControl1)
            Me.transitionManager1.StartTransition(Me.gridControl1)
            zoomTransition.ActiveSettings = New DevExpress.Utils.Animation.ZoomTransitionSettings() With {.Direction = DevExpress.Utils.Animation.ZoomTransitionDirection.ZoomOut, .SourceBounds = Me.GridClientBounds}
            action()
            Me.transitionManager1.EndTransition()
        End Sub

'#Region "BreadCrumb node generation"
        Private Sub repositoryItemBreadCrumbEdit1_ValidatePath(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.BreadCrumbValidatePathEventArgs)
            If Not DevExpress.Utils.Helpers.FileSystemHelper.IsDirExists(e.Path) Then
                e.ValidationResult = DevExpress.XtraEditors.BreadCrumbValidatePathResult.Cancel
                Return
            End If

            e.ValidationResult = DevExpress.XtraEditors.BreadCrumbValidatePathResult.CreateNodes
        End Sub

        Private Sub repositoryItemBreadCrumbEdit1_NewNodeAdding(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.BreadCrumbNewNodeAddingEventArgs)
            e.Node.PopulateOnDemand = True
        End Sub

        Private Sub repositoryItemBreadCrumbEdit1_QueryChildNodes(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.BreadCrumbQueryChildNodesEventArgs)
            Dim dir As String = e.Node.Path
            If Not DevExpress.Utils.Helpers.FileSystemHelper.IsDirExists(dir) Then Return
            Dim subDirs As String() = DevExpress.Utils.Helpers.FileSystemHelper.GetSubFolders(dir)
            For i As Integer = 0 To subDirs.Length - 1
                e.Node.ChildNodes.Add(Me.CreateNode(subDirs(i)))
            Next
        End Sub

        Private Function CreateNode(ByVal path As String) As BreadCrumbNode
            Dim folderName As String = DevExpress.Utils.Helpers.FileSystemHelper.GetDirName(path)
            Return New DevExpress.XtraEditors.BreadCrumbNode(folderName, folderName, True)
        End Function
'#End Region
    End Class

    Friend Class NodeNavigationInfo

        Private _SourceBounds As Rectangle, _Path As String

        Public Sub New(ByVal path As String)
            Me.New(path, System.Drawing.Rectangle.Empty)
        End Sub

        Public Sub New(ByVal path As String, ByVal sourceBounds As System.Drawing.Rectangle)
            Me.SourceBounds = sourceBounds
            Me.Path = path
        End Sub

        Public Property SourceBounds As Rectangle
            Get
                Return _SourceBounds
            End Get

            Private Set(ByVal value As Rectangle)
                _SourceBounds = value
            End Set
        End Property

        Public Property Path As String
            Get
                Return _Path
            End Get

            Private Set(ByVal value As String)
                _Path = value
            End Set
        End Property
    End Class
End Namespace
