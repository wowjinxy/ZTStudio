using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace ZTStudio
{
    [DesignerGenerated()]
    public partial class FrmBatchOffsetFix : Form
    {

        // Form overrides dispose to clean up the component list.
        [DebuggerNonUserCode()]
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing && components is object)
                {
                    components.Dispose();
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        // Required by the Windows Form Designer
        private System.ComponentModel.IContainer components;

        // NOTE: The following procedure is required by the Windows Form Designer
        // It can be modified using the Windows Form Designer.  
        // Do not modify it using the code editor.
        [DebuggerStepThrough()]
        private void InitializeComponent()
        {
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBatchOffsetFix));
            LblFolder = new Label();
            numUpDown = new NumericUpDown();
            numLeftRight = new NumericUpDown();
            lblUpDown = new Label();
            lblLeftRight = new Label();
            BtnBatchOffsettFix = new Button();
            BtnBatchOffsettFix.Click += new EventHandler(BtnBatchOffsetting_Click);
            TxtFolder = new TextBox();
            dlgBrowseFolder = new FolderBrowserDialog();
            btnSelect = new Button();
            btnSelect.Click += new EventHandler(BtnSelect_Click);
            lblHint = new Label();
            PBProgress = new ProgressBar();
            ((System.ComponentModel.ISupportInitialize)numUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numLeftRight).BeginInit();
            SuspendLayout();
            // 
            // LblFolder
            // 
            LblFolder.AutoSize = true;
            LblFolder.Location = new Point(29, 118);
            LblFolder.Name = "LblFolder";
            LblFolder.Size = new Size(43, 13);
            LblFolder.TabIndex = 0;
            LblFolder.Text = "Folder:";
            // 
            // numUpDown
            // 
            numUpDown.Location = new Point(32, 201);
            numUpDown.Minimum = new decimal(new int[] { 100, 0, 0, (int)-2147483648L });
            numUpDown.Name = "numUpDown";
            numUpDown.Size = new Size(69, 22);
            numUpDown.TabIndex = 1;
            numUpDown.Value = new decimal(new int[] { 16, 0, 0, 0 });
            // 
            // numLeftRight
            // 
            numLeftRight.Location = new Point(321, 201);
            numLeftRight.Minimum = new decimal(new int[] { 100, 0, 0, (int)-2147483648L });
            numLeftRight.Name = "numLeftRight";
            numLeftRight.Size = new Size(69, 22);
            numLeftRight.TabIndex = 2;
            // 
            // lblUpDown
            // 
            lblUpDown.AutoSize = true;
            lblUpDown.Location = new Point(107, 203);
            lblUpDown.Name = "lblUpDown";
            lblUpDown.Size = new Size(164, 13);
            lblUpDown.TabIndex = 3;
            lblUpDown.Text = "Up (positive) / down (negative)";
            // 
            // lblLeftRight
            // 
            lblLeftRight.AutoSize = true;
            lblLeftRight.Location = new Point(396, 203);
            lblLeftRight.Name = "lblLeftRight";
            lblLeftRight.Size = new Size(163, 13);
            lblLeftRight.TabIndex = 4;
            lblLeftRight.Text = "Left (positive) / right (negative)";
            // 
            // BtnBatchOffsettFix
            // 
            BtnBatchOffsettFix.Location = new Point(32, 257);
            BtnBatchOffsettFix.Name = "BtnBatchOffsettFix";
            BtnBatchOffsettFix.Size = new Size(561, 43);
            BtnBatchOffsettFix.TabIndex = 5;
            BtnBatchOffsettFix.Text = "Process entire folder";
            BtnBatchOffsettFix.UseVisualStyleBackColor = true;
            // 
            // TxtFolder
            // 
            TxtFolder.Location = new Point(32, 134);
            TxtFolder.Name = "TxtFolder";
            TxtFolder.ReadOnly = true;
            TxtFolder.Size = new Size(561, 22);
            TxtFolder.TabIndex = 6;
            // 
            // btnSelect
            // 
            btnSelect.Location = new Point(32, 158);
            btnSelect.Name = "btnSelect";
            btnSelect.Size = new Size(561, 20);
            btnSelect.TabIndex = 7;
            btnSelect.Text = "Select folder";
            btnSelect.UseVisualStyleBackColor = true;
            // 
            // lblHint
            // 
            lblHint.AutoSize = true;
            lblHint.Location = new Point(29, 18);
            lblHint.Name = "lblHint";
            lblHint.Size = new Size(513, 52);
            lblHint.TabIndex = 8;
            lblHint.Text = resources.GetString("lblHint.Text");
            // 
            // PBProgress
            // 
            PBProgress.Location = new Point(34, 303);
            PBProgress.Name = "PBProgress";
            PBProgress.Size = new Size(558, 29);
            PBProgress.TabIndex = 9;
            // 
            // FrmBatchOffsetFix
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(623, 384);
            Controls.Add(PBProgress);
            Controls.Add(lblHint);
            Controls.Add(btnSelect);
            Controls.Add(TxtFolder);
            Controls.Add(BtnBatchOffsettFix);
            Controls.Add(lblLeftRight);
            Controls.Add(lblUpDown);
            Controls.Add(numLeftRight);
            Controls.Add(numUpDown);
            Controls.Add(LblFolder);
            Font = new Font("Segoe UI", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(0));
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "FrmBatchOffsetFix";
            Text = "Batch offset fixing";
            ((System.ComponentModel.ISupportInitialize)numUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)numLeftRight).EndInit();
            Load += new EventHandler(FrmBatchRotationFix_Load);
            ResumeLayout(false);
            PerformLayout();
        }

        internal Label LblFolder;
        internal NumericUpDown numUpDown;
        internal NumericUpDown numLeftRight;
        internal Label lblUpDown;
        internal Label lblLeftRight;
        internal Button BtnBatchOffsettFix;
        internal TextBox TxtFolder;
        internal FolderBrowserDialog dlgBrowseFolder;
        internal Button btnSelect;
        internal Label lblHint;
        internal ProgressBar PBProgress;
    }
}