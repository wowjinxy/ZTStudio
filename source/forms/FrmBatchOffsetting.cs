using System;
using System.Drawing;

namespace ZTStudio
{


    /// <summary>
/// <para>
///     Form which allows to specify offsets which are applied to all views within the selected folder.
/// </para>
/// <para>
///     This is specifically meant for importing images from a different program, such as Blender; 
///     where it is possible the animal's (Y) offset needs to be adjusted for each frame in every view.
/// </para>
/// </summary>
    public partial class FrmBatchOffsetFix
    {
        public FrmBatchOffsetFix()
        {
            InitializeComponent();
        }

        /// <summary>
    /// Initializes window
    /// </summary>
    /// <param name="sender">Object</param>
    /// <param name="e">EventArgs</param>
        private void FrmBatchRotationFix_Load(object sender, EventArgs e)
        {

            // Take icon from main screen
            Icon = My.MyProject.Forms.FrmMain.Icon;

            // Assume default folder
            if (string.IsNullOrEmpty(TxtFolder.Text))
            {
                TxtFolder.Text = MdlSettings.Cfg_Path_Root;
            }
        }

        /// <summary>
    /// On click, run batch rotation fixing
    /// </summary>
    /// <param name="sender">Object</param>
    /// <param name="e">EventArgs</param>
        private void BtnBatchOffsetting_Click(object sender, EventArgs e)
        {

            // Runs procedure
            MdlTasks.BatchOffsetFixFolderZT1(TxtFolder.Text, new Point((int)Math.Round(numLeftRight.Value), (int)Math.Round(numUpDown.Value)), PBProgress);
        }

        /// <summary>
    /// On click, show folder selection dialog
    /// </summary>
    /// <param name="sender">Object</param>
    /// <param name="e">EventArgs</param>
        private void BtnSelect_Click(object sender, EventArgs e)
        {

            // Allows user to select a different folder
            {
                var withBlock = dlgBrowseFolder;
                withBlock.SelectedPath = TxtFolder.Text;
                withBlock.ShowNewFolderButton = false; // 20190825 this used to be true?! Why would a new folder be created at this point?
                withBlock.Description = "Select the folder which contains the ZT1 Graphics.";
                withBlock.ShowDialog();
                TxtFolder.Text = withBlock.SelectedPath;
            }
        }
    }
}