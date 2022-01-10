using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace ZTStudio
{
    [DesignerGenerated()]
    public partial class FrmPal : Form
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
            sBar = new StatusStrip();
            SsFileName = new ToolStripStatusLabel();
            DgvPal = new DataGridView();
            ColColor = new DataGridViewTextBoxColumn();
            BtnUseInMainPal = new Button();
            BtnUseInMainPal.Click += new EventHandler(BtnUseInMainPal_Click);
            sBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DgvPal).BeginInit();
            SuspendLayout();
            // 
            // sBar
            // 
            sBar.Items.AddRange(new ToolStripItem[] { SsFileName });
            sBar.Location = new Point(0, 260);
            sBar.Name = "sBar";
            sBar.Size = new Size(264, 22);
            sBar.TabIndex = 0;
            sBar.Text = "StatusStrip1";
            // 
            // SsFileName
            // 
            SsFileName.Name = "SsFileName";
            SsFileName.Size = new Size(55, 17);
            SsFileName.Text = "Filename";
            // 
            // DgvPal
            // 
            DgvPal.AllowUserToAddRows = false;
            DgvPal.AllowUserToDeleteRows = false;
            DgvPal.AllowUserToResizeColumns = false;
            DgvPal.AllowUserToResizeRows = false;
            DgvPal.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            DgvPal.Columns.AddRange(new DataGridViewColumn[] { ColColor });
            DgvPal.Dock = DockStyle.Fill;
            DgvPal.Location = new Point(0, 0);
            DgvPal.Name = "DgvPal";
            DgvPal.RowHeadersWidth = 75;
            DgvPal.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            DgvPal.RowTemplate.Height = 20;
            DgvPal.Size = new Size(264, 215);
            DgvPal.TabIndex = 21;
            // 
            // ColColor
            // 
            ColColor.HeaderText = "Color";
            ColColor.Name = "ColColor";
            // 
            // BtnUseInMainPal
            // 
            BtnUseInMainPal.Dock = DockStyle.Bottom;
            BtnUseInMainPal.Location = new Point(0, 215);
            BtnUseInMainPal.Name = "BtnUseInMainPal";
            BtnUseInMainPal.Size = new Size(264, 45);
            BtnUseInMainPal.TabIndex = 22;
            BtnUseInMainPal.Text = "Replace colors in main color palette" + '\r' + '\n' + "with the colors above (transparent is ignor" + "ed)";
            BtnUseInMainPal.UseVisualStyleBackColor = true;
            // 
            // FrmPal
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(264, 282);
            Controls.Add(DgvPal);
            Controls.Add(BtnUseInMainPal);
            Controls.Add(sBar);
            Font = new Font("Segoe UI", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(0));
            FormBorderStyle = FormBorderStyle.SizableToolWindow;
            MinimumSize = new Size(280, 280);
            Name = "FrmPal";
            Text = "Color Palette";
            TopMost = true;
            sBar.ResumeLayout(false);
            sBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)DgvPal).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        internal StatusStrip sBar;
        internal ToolStripStatusLabel SsFileName;
        internal DataGridView DgvPal;
        internal Button BtnUseInMainPal;
        internal DataGridViewTextBoxColumn ColColor;
    }
}