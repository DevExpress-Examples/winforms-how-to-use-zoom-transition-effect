using DevExpress.Utils.Animation;
using DevExpress.Utils.Helpers;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.WinExplorer.ViewInfo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace dxSample {
    public partial class Form1 : XtraForm {
        public Form1() {
            InitializeComponent();
            var startPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var startNode = new NodeNavigationInfo(startPath);
            SetActivePath(startNode);
        }
        NodeNavigationInfo currentPath;
        NodeNavigationInfo CurrentNode {
            get { return currentPath; }
            set {
                if(CurrentNode == value)
                    return;
                currentPath = value;
                UpdateDataSource();
            }
        }
        Rectangle GridClientBounds { get { return new Rectangle(Point.Empty, this.gridControl1.Size); } }

        static readonly Size ImageSize = new Size(256, 256);
        void UpdateDataSource() {
            Cursor oldCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;
            try {
                if(!string.IsNullOrEmpty(CurrentNode.Path))
                    this.gridControl1.DataSource = FileSystemHelper.GetFileSystemEntries(CurrentNode.Path, IconSizeType.ExtraLarge, ImageSize);
                else
                    this.gridControl1.DataSource = null;
                this.barEditItem1.EditValue = CurrentNode.Path;
            }
            finally {
                Cursor.Current = oldCursor;
            }
        }

        private void winExplorerView1_ItemKeyDown(object sender, DevExpress.XtraGrid.Views.WinExplorer.WinExplorerViewItemKeyEventArgs e) {
            if(this.transitionManager1.IsTransition)
                return;
            switch(e.KeyInfo.KeyCode) {
                case Keys.Enter:
                    OnItemClick(e.ItemInfo);
                    break;
                case Keys.Back:
                    RunZoomOutAnimation(() => { GoBackCore(); });
                    break;
            }
        }
        private void winExplorerView1_ItemDoubleClick(object sender, DevExpress.XtraGrid.Views.WinExplorer.WinExplorerViewItemDoubleClickEventArgs e) {
            OnItemClick(e.ItemInfo);
        }

        private void OnItemClick(WinExplorerItemViewInfo itemInfo) {
            if(itemInfo == null || itemInfo.Row.RowKey is FileEntry)
                return;
            var entry = (FileSystemEntry)itemInfo.Row.RowKey;
            var activeNode = new NodeNavigationInfo(entry.Path, itemInfo.Bounds);
            RunZoomInAnimation(activeNode, () => { SetActivePath(activeNode); });
        }

        private void OnBackButtonClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if(this.transitionManager1.IsTransition)
                return;
            RunZoomOutAnimation(() => { GoBackCore(); });
        }

        private void OnForwardButtonClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if(this.transitionManager1.IsTransition)
                return;
            int currentIndex = PathHistory.IndexOf(CurrentNode);
            if(currentIndex >= PathHistory.Count - 1)
                return;
            var activeNode = PathHistory[currentIndex + 1];
            RunZoomInAnimation(activeNode, () => { GoForward(); });
        }

        List<NodeNavigationInfo> PathHistory = new List<NodeNavigationInfo>();
        private void SetActivePath(NodeNavigationInfo node) {
            int currentIndex = PathHistory.IndexOf(CurrentNode);
            CurrentNode = node;
            if(currentIndex > -1) {
                int pathCount = PathHistory.Count;
                while(pathCount > currentIndex + 1) {
                    PathHistory.RemoveAt(pathCount - 1);
                    pathCount--;
                }
            }
            PathHistory.Add(CurrentNode);
        }
        private void GoBackCore() {
            int currentIndex = PathHistory.IndexOf(CurrentNode);
            if(currentIndex < 1)
                return;
            CurrentNode = PathHistory[currentIndex - 1];
        }
        private void GoForward() {
            int currentIndex = PathHistory.IndexOf(CurrentNode);
            if(currentIndex >= PathHistory.Count - 1)
                return;
            CurrentNode = PathHistory[currentIndex + 1];
        }

        private void OnBreadCrumbValueChanged(object sender, EventArgs e) {
            var breadCrumb = (BreadCrumbEdit)sender;
            var path = breadCrumb.EditValue as string;
            if(this.transitionManager1.IsTransition || path == null)
                return;
            var activeNode = new NodeNavigationInfo(path,
                new Rectangle(GridClientBounds.Width / 2, GridClientBounds.Height / 2, 1, 1));
            RunZoomInAnimation(activeNode, () => { SetActivePath(activeNode); });
        }

        void RunZoomInAnimation(NodeNavigationInfo activeNode, Action action) {
            var zoomTransition = this.transitionManager1.GetTransition<ZoomTransition>(this.gridControl1);
            this.transitionManager1.StartTransition(this.gridControl1);
            zoomTransition.ActiveSettings = new ZoomTransitionSettings() {
                SourceBounds = activeNode.SourceBounds, TargetBounds = GridClientBounds
            };
            action();
            this.transitionManager1.EndTransition();
        }
        void RunZoomOutAnimation(Action action) {
            var zoomTransition = this.transitionManager1.GetTransition<ZoomTransition>(this.gridControl1);
            this.transitionManager1.StartTransition(this.gridControl1);
            zoomTransition.ActiveSettings = new ZoomTransitionSettings() {
                Direction = ZoomTransitionDirection.ZoomOut, SourceBounds = GridClientBounds
            };
            action();
            this.transitionManager1.EndTransition();
        }
        #region BreadCrumb node generation
        private void repositoryItemBreadCrumbEdit1_ValidatePath(object sender, BreadCrumbValidatePathEventArgs e) {
            if(!FileSystemHelper.IsDirExists(e.Path)) {
                e.ValidationResult = BreadCrumbValidatePathResult.Cancel;
                return;
            }
            e.ValidationResult = BreadCrumbValidatePathResult.CreateNodes;
        }
        private void repositoryItemBreadCrumbEdit1_NewNodeAdding(object sender, BreadCrumbNewNodeAddingEventArgs e) {
            e.Node.PopulateOnDemand = true;
        }
        private void repositoryItemBreadCrumbEdit1_QueryChildNodes(object sender, BreadCrumbQueryChildNodesEventArgs e) {
            string dir = e.Node.Path;
            if(!FileSystemHelper.IsDirExists(dir))
                return;
            string[] subDirs = FileSystemHelper.GetSubFolders(dir);
            for(int i = 0; i < subDirs.Length; i++) {
                e.Node.ChildNodes.Add(CreateNode(subDirs[i]));
            }
        }
        BreadCrumbNode CreateNode(string path) {
            string folderName = FileSystemHelper.GetDirName(path);
            return new BreadCrumbNode(folderName, folderName, true);
        }
        #endregion
    }
    class NodeNavigationInfo {
        public NodeNavigationInfo(string path) : this(path, Rectangle.Empty) { }
        public NodeNavigationInfo(string path, Rectangle sourceBounds) {
            SourceBounds = sourceBounds;
            Path = path;
        }
        public Rectangle SourceBounds { get; private set; }
        public string Path { get; private set; }
    }
}
