using System;
using System.Windows.Forms;

namespace ZTStudio
{
    /// <summary>
/// Form which briefly explains to users how to prepare files and what to expect from a batch graphic conversion.
/// </summary>
    public partial class FrmBatchConversion
    {
        public FrmBatchConversion()
        {
            InitializeComponent();
        }

        /// <summary>
    /// Handles what happens when the user clicks the Convert button.
    /// </summary>
    /// <param name="sender">Object</param>
    /// <param name="e">EventArgs</param>
        private void BtnConvert_Click(object sender, EventArgs e)
        {

            // Prevent double click, clicking too fast etc. 
            // Re-enable this when the batch process has finished.
            Enabled = false;
            if (RbPNG_to_ZT1.Checked == true)
            {

                // Convert entire folder containing PNG-files to ZT1-graphics
                MdlTasks.ConvertFolderPNGToZT1(MdlSettings.Cfg_Path_Root, PBBatchProgress);
            }
            else
            {

                // Convert entire folder containing ZT1-graphics to PNG-files
                MdlTasks.ConvertFolderZT1ToPNG(MdlSettings.Cfg_Path_Root, PBBatchProgress);
            }

            // After batch conversion, clean up of files may have happend; or new files created
            MdlZTStudioUI.UpdateExplorerPane();
            Enabled = true;
            MdlZTStudio.InfoBox(GetType().FullName, "Click", "Batch conversion finished successfully.");
        }

        /// <summary>
    /// Handles clicking on "Settings" button and shows the Settings window
    /// </summary>
    /// <param name="sender">Object</param>
    /// <param name="e">EventArgs</param>
        private void BtnSettings_Click(object sender, EventArgs e)
        {

            // Show Settings form (shortcut)
            My.MyProject.Forms.FrmSettings.ShowDialog(this);
        }

        /// <summary>
    /// Handles form loading
    /// </summary>
    /// <param name="sender">Object</param>
    /// <param name="e">EventArgs</param>
        private void FrmBatchConversion_Load(object sender, EventArgs e)
        {
            Icon = My.MyProject.Forms.FrmMain.Icon;
        }

        private void FrmBatchConversion_FormClosing(object sender, FormClosingEventArgs e)
        {
            Dispose();
        }
    }
}