namespace Lab08
{
    partial class FrmPaint
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.pnlToolbar = new System.Windows.Forms.Panel();
            this.btnBgColor = new System.Windows.Forms.Button();
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnUndo = new System.Windows.Forms.Button();
            this.chkFill = new System.Windows.Forms.CheckBox();
            this.pnlColorPreview = new System.Windows.Forms.Panel();
            this.btnColor = new System.Windows.Forms.Button();
            this.nudWidth = new System.Windows.Forms.NumericUpDown();
            this.lblWidth = new System.Windows.Forms.Label();
            this.cboMode = new System.Windows.Forms.ComboBox();
            this.lblMode = new System.Windows.Forms.Label();
            this.ssMain = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.canvas = new System.Windows.Forms.Panel();
            this.pnlToolbar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudWidth)).BeginInit();
            this.ssMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.BackColor = System.Drawing.SystemColors.Control;
            this.pnlToolbar.Controls.Add(this.btnBgColor);
            this.pnlToolbar.Controls.Add(this.btnOpen);
            this.pnlToolbar.Controls.Add(this.btnSave);
            this.pnlToolbar.Controls.Add(this.btnClear);
            this.pnlToolbar.Controls.Add(this.btnUndo);
            this.pnlToolbar.Controls.Add(this.chkFill);
            this.pnlToolbar.Controls.Add(this.pnlColorPreview);
            this.pnlToolbar.Controls.Add(this.btnColor);
            this.pnlToolbar.Controls.Add(this.nudWidth);
            this.pnlToolbar.Controls.Add(this.lblWidth);
            this.pnlToolbar.Controls.Add(this.cboMode);
            this.pnlToolbar.Controls.Add(this.lblMode);
            this.pnlToolbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolbar.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.pnlToolbar.Location = new System.Drawing.Point(0, 0);
            this.pnlToolbar.Name = "pnlToolbar";
            this.pnlToolbar.Size = new System.Drawing.Size(984, 52);
            this.pnlToolbar.TabIndex = 0;
            // 
            // btnBgColor
            // 
            this.btnBgColor.Location = new System.Drawing.Point(882, 11);
            this.btnBgColor.Name = "btnBgColor";
            this.btnBgColor.Size = new System.Drawing.Size(90, 30);
            this.btnBgColor.TabIndex = 11;
            this.btnBgColor.Text = "Màu nền";
            this.btnBgColor.UseVisualStyleBackColor = true;
            this.btnBgColor.Click += new System.EventHandler(this.btnBgColor_Click);
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(796, 11);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(80, 30);
            this.btnOpen.TabIndex = 10;
            this.btnOpen.Text = "Mở ảnh";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(710, 11);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(80, 30);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "Lưu PNG";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(628, 11);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(76, 30);
            this.btnClear.TabIndex = 8;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnUndo
            // 
            this.btnUndo.Location = new System.Drawing.Point(546, 11);
            this.btnUndo.Name = "btnUndo";
            this.btnUndo.Size = new System.Drawing.Size(76, 30);
            this.btnUndo.TabIndex = 7;
            this.btnUndo.Text = "Undo";
            this.btnUndo.UseVisualStyleBackColor = true;
            this.btnUndo.Click += new System.EventHandler(this.btnUndo_Click);
            // 
            // chkFill
            // 
            this.chkFill.AutoSize = true;
            this.chkFill.Location = new System.Drawing.Point(445, 16);
            this.chkFill.Name = "chkFill";
            this.chkFill.Size = new System.Drawing.Size(95, 21);
            this.chkFill.TabIndex = 6;
            this.chkFill.Text = "Tô màu hình";
            this.chkFill.UseVisualStyleBackColor = true;
            // 
            // pnlColorPreview
            // 
            this.pnlColorPreview.BackColor = System.Drawing.Color.Black;
            this.pnlColorPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlColorPreview.Location = new System.Drawing.Point(408, 13);
            this.pnlColorPreview.Name = "pnlColorPreview";
            this.pnlColorPreview.Size = new System.Drawing.Size(26, 26);
            this.pnlColorPreview.TabIndex = 5;
            // 
            // btnColor
            // 
            this.btnColor.Location = new System.Drawing.Point(322, 11);
            this.btnColor.Name = "btnColor";
            this.btnColor.Size = new System.Drawing.Size(80, 30);
            this.btnColor.TabIndex = 4;
            this.btnColor.Text = "Màu vẽ...";
            this.btnColor.UseVisualStyleBackColor = true;
            this.btnColor.Click += new System.EventHandler(this.btnColor_Click);
            // 
            // nudWidth
            // 
            this.nudWidth.Location = new System.Drawing.Point(245, 15);
            this.nudWidth.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.nudWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudWidth.Name = "nudWidth";
            this.nudWidth.Size = new System.Drawing.Size(65, 24);
            this.nudWidth.TabIndex = 3;
            this.nudWidth.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nudWidth.ValueChanged += new System.EventHandler(this.nudWidth_ValueChanged);
            // 
            // lblWidth
            // 
            this.lblWidth.AutoSize = true;
            this.lblWidth.Location = new System.Drawing.Point(185, 17);
            this.lblWidth.Name = "lblWidth";
            this.lblWidth.Size = new System.Drawing.Size(54, 17);
            this.lblWidth.TabIndex = 2;
            this.lblWidth.Text = "Độ dày:";
            // 
            // cboMode
            // 
            this.cboMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMode.FormattingEnabled = true;
            this.cboMode.Location = new System.Drawing.Point(72, 14);
            this.cboMode.Name = "cboMode";
            this.cboMode.Size = new System.Drawing.Size(100, 24);
            this.cboMode.TabIndex = 1;
            this.cboMode.SelectedIndexChanged += new System.EventHandler(this.cboMode_SelectedIndexChanged);
            // 
            // lblMode
            // 
            this.lblMode.AutoSize = true;
            this.lblMode.Location = new System.Drawing.Point(12, 17);
            this.lblMode.Name = "lblMode";
            this.lblMode.Size = new System.Drawing.Size(54, 17);
            this.lblMode.TabIndex = 0;
            this.lblMode.Text = "Chế độ:";
            // 
            // ssMain
            // 
            this.ssMain.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ssMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
            this.ssMain.Location = new System.Drawing.Point(0, 619);
            this.ssMain.Name = "ssMain";
            this.ssMain.Size = new System.Drawing.Size(984, 22);
            this.ssMain.TabIndex = 1;
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(250, 17);
            this.lblStatus.Text = "Tọa độ: X = 0, Y = 0 | Chế độ: Pen | Nét: 2px";
            // 
            // canvas
            // 
            this.canvas.BackColor = System.Drawing.Color.White;
            this.canvas.Cursor = System.Windows.Forms.Cursors.Cross;
            this.canvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.canvas.Location = new System.Drawing.Point(0, 52);
            this.canvas.Name = "canvas";
            this.canvas.Size = new System.Drawing.Size(984, 567);
            this.canvas.TabIndex = 2;
            this.canvas.Paint += new System.Windows.Forms.PaintEventHandler(this.canvas_Paint);
            this.canvas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.canvas_MouseDown);
            this.canvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.canvas_MouseMove);
            this.canvas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.canvas_MouseUp);
            // 
            // FrmPaint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 641);
            this.Controls.Add(this.canvas);
            this.Controls.Add(this.ssMain);
            this.Controls.Add(this.pnlToolbar);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.KeyPreview = true;
            this.Name = "FrmPaint";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "COMP1019 - Lab 08: Simple Paint";
            this.Load += new System.EventHandler(this.FrmPaint_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmPaint_KeyDown);
            this.pnlToolbar.ResumeLayout(false);
            this.pnlToolbar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudWidth)).EndInit();
            this.ssMain.ResumeLayout(false);
            this.ssMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel pnlToolbar;
        private System.Windows.Forms.Label lblMode;
        private System.Windows.Forms.ComboBox cboMode;
        private System.Windows.Forms.Label lblWidth;
        private System.Windows.Forms.NumericUpDown nudWidth;
        private System.Windows.Forms.Button btnColor;
        private System.Windows.Forms.Panel pnlColorPreview;
        private System.Windows.Forms.CheckBox chkFill;
        private System.Windows.Forms.Button btnUndo;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button btnBgColor;
        private System.Windows.Forms.StatusStrip ssMain;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.Panel canvas;
    }
}
