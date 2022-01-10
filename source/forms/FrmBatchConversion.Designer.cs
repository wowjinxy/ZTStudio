using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace ZTStudio
{
    [DesignerGenerated()]
    public partial class FrmBatchConversion : Form
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBatchConversion));
            gbType = new GroupBox();
            RbPNG_to_ZT1 = new RadioButton();
            RbZT1_to_PNG = new RadioButton();
            BtnConvert = new Button();
            BtnConvert.Click += new EventHandler(BtnConvert_Click);
            LblInfo = new Label();
            BtnSettings = new Button();
            BtnSettings.Click += new EventHandler(BtnSettings_Click);
            PBBatchProgress = new ProgressBar();
            gbType.SuspendLayout();
            SuspendLayout();
            // 
            // gbType
            // 
            gbType.Controls.Add(RbPNG_to_ZT1);
            gbType.Controls.Add(RbZT1_to_PNG);
            gbType.Location = new Point(47, 47);
            gbType.Name = "gbType";
            gbType.Size = new Size(498, 78);
            gbType.TabIndex = 11;
            gbType.TabStop = false;
            gbType.Text = "Action to perform:";
            // 
            // RbPNG_to_ZT1
            // 
            RbPNG_to_ZT1.AutoSize = true;
            RbPNG_to_ZT1.Checked = true;
            RbPNG_to_ZT1.Location = new Point(6, 28);
            RbPNG_to_ZT1.Name = "RbPNG_to_ZT1";
            RbPNG_to_ZT1.Size = new Size(220, 17);
            RbPNG_to_ZT1.TabIndex = 9;
            RbPNG_to_ZT1.TabStop = true;
            RbPNG_to_ZT1.Text = "Convert all .PNG-files to a ZT1-graphic";
            RbPNG_to_ZT1.UseVisualStyleBackColor = true;
            // 
            // RbZT1_to_PNG
            // 
            RbZT1_to_PNG.AutoSize = true;
            RbZT1_to_PNG.Location = new Point(6, 51);
            RbZT1_to_PNG.Name = "RbZT1_to_PNG";
            RbZT1_to_PNG.Size = new Size(233, 17);
            RbZT1_to_PNG.TabIndex = 8;
            RbZT1_to_PNG.Text = "Convert all ZT1 graphic files to .PNG files";
            RbZT1_to_PNG.UseVisualStyleBackColor = true;
            // 
            // BtnConvert
            // 
            BtnConvert.Font = new Font("Segoe UI", 8.25f, FontStyle.Bold, GraphicsUnit.Point, Conversions.ToByte(0));
            BtnConvert.Location = new Point(47, 294);
            BtnConvert.Name = "BtnConvert";
            BtnConvert.Size = new Size(498, 32);
            BtnConvert.TabIndex = 15;
            BtnConvert.Text = "Start batch conversion";
            BtnConvert.UseVisualStyleBackColor = true;
            // 
            // LblInfo
            // 
            LblInfo.AutoSize = true;
            LblInfo.Location = new Point(50, 156);
            LblInfo.Name = "LblInfo";
            LblInfo.Size = new Size(527, 78);
            LblInfo.TabIndex = 19;
            LblInfo.Text = resources.GetString("LblInfo.Text");
            // 
            // BtnSettings
            // 
            BtnSettings.Location = new Point(47, 258);
            BtnSettings.Name = "BtnSettings";
            BtnSettings.Size = new Size(498, 30);
            BtnSettings.TabIndex = 22;
            BtnSettings.Text = "Change settings";
            BtnSettings.UseVisualStyleBackColor = true;
            // 
            // PBBatchProgress
            // 
            PBBatchProgress.Location = new Point(47, 332);
            PBBatchProgress.Name = "PBBatchProgress";
            PBBatchProgress.Size = new Size(498, 22);
            PBBatchProgress.TabIndex = 23;
            // 
            // FrmBatchConversion
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(604, 384);
            Controls.Add(PBBatchProgress);
            Controls.Add(BtnSettings);
            Controls.Add(LblInfo);
            Controls.Add(BtnConvert);
            Controls.Add(gbType);
            Font = new Font("Segoe UI", 8.25f, FontStyle.Regular, GraphicsUnit.Point, Conversions.ToByte(0));
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "FrmBatchConversion";
            Text = "Batch graphic conversion";
            gbType.ResumeLayout(false);
            gbType.PerformLayout();
            Load += new EventHandler(FrmBatchConversion_Load);
            FormClosing += new FormClosingEventHandler(FrmBatchConversion_FormClosing);
            ResumeLayout(false);
            PerformLayout();
        }

        internal GroupBox gbType;
        internal RadioButton RbPNG_to_ZT1;
        internal RadioButton RbZT1_to_PNG;
        internal Button BtnConvert;
        internal Label LblInfo;
        internal Button BtnSettings;
        internal ProgressBar PBBatchProgress;
    }
}