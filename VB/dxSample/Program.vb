Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows.Forms
Imports DevExpress.UserSkins
Imports DevExpress.Skins
Imports DevExpress.LookAndFeel
Imports DevExpress.XtraEditors

Namespace dxSample
	Friend Module Program
		''' <summary>
		''' The main entry point for the application.
		''' </summary>
		<STAThread>
		Sub Main()
			WindowsFormsSettings.ForceDirectXPaint()
			UserLookAndFeel.Default.SetSkinStyle(SkinSvgPalette.Office2019Colorful)
			Application.EnableVisualStyles()
			Application.SetCompatibleTextRenderingDefault(False)

			Application.Run(New Form1())
		End Sub
	End Module
End Namespace
