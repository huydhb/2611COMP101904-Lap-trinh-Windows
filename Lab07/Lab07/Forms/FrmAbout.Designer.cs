namespace Lab07.Forms
{
    partial class FrmAbout
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
            this.lblAppName = new System.Windows.Forms.Label();
            this.lblSubject = new System.Windows.Forms.Label();
            this.lblStudent = new System.Windows.Forms.Label();
            this.lblMSSV = new System.Windows.Forms.Label();
            this.lblClass = new System.Windows.Forms.Label();
            this.lblSchool = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.grpInfo = new System.Windows.Forms.GroupBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.grpInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblAppName
            // 
            this.lblAppName.AutoSize = true;
            this.lblAppName.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblAppName.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblAppName.Location = new System.Drawing.Point(85, 20);
            this.lblAppName.Name = "lblAppName";
            this.lblAppName.Size = new System.Drawing.Size(280, 30);
            this.lblAppName.TabIndex = 0;
            this.lblAppName.Text = "ShopManagerMini  v1.0";
            this.lblAppName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSubject
            // 
            this.lblSubject.AutoSize = true;
            this.lblSubject.Location = new System.Drawing.Point(20, 60);
            this.lblSubject.Name = "lblSubject";
            this.lblSubject.Size = new System.Drawing.Size(258, 17);
            this.lblSubject.TabIndex = 1;
            this.lblSubject.Text = "Môn học: COMP1019 - Lập trình Windows";
            // 
            // lblStudent
            // 
            this.lblStudent.AutoSize = true;
            this.lblStudent.Location = new System.Drawing.Point(20, 90);
            this.lblStudent.Name = "lblStudent";
            this.lblStudent.Size = new System.Drawing.Size(217, 17);
            this.lblStudent.TabIndex = 2;
            this.lblStudent.Text = "Sinh viên: Đặng Hoàng Bảo Huy";
            // 
            // lblMSSV
            // 
            this.lblMSSV.AutoSize = true;
            this.lblMSSV.Location = new System.Drawing.Point(20, 120);
            this.lblMSSV.Name = "lblMSSV";
            this.lblMSSV.Size = new System.Drawing.Size(134, 17);
            this.lblMSSV.TabIndex = 3;
            this.lblMSSV.Text = "MSSV: 51.01.104.033";
            // 
            // lblClass
            // 
            this.lblClass.AutoSize = true;
            this.lblClass.Location = new System.Drawing.Point(20, 150);
            this.lblClass.Name = "lblClass";
            this.lblClass.Size = new System.Drawing.Size(123, 17);
            this.lblClass.TabIndex = 4;
            this.lblClass.Text = "Lớp: 51.01.CNTT.C";
            // 
            // lblSchool
            // 
            this.lblSchool.AutoSize = true;
            this.lblSchool.Location = new System.Drawing.Point(20, 180);
            this.lblSchool.Name = "lblSchool";
            this.lblSchool.Size = new System.Drawing.Size(277, 17);
            this.lblSchool.TabIndex = 5;
            this.lblSchool.Text = "Trường: Đại học Sư phạm TP.HCM (HCMUE)";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(175, 290);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 32);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Đóng";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // grpInfo
            // 
            this.grpInfo.Controls.Add(this.lblDescription);
            this.grpInfo.Controls.Add(this.lblSchool);
            this.grpInfo.Controls.Add(this.lblSubject);
            this.grpInfo.Controls.Add(this.lblClass);
            this.grpInfo.Controls.Add(this.lblStudent);
            this.grpInfo.Controls.Add(this.lblMSSV);
            this.grpInfo.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.grpInfo.Location = new System.Drawing.Point(30, 60);
            this.grpInfo.Name = "grpInfo";
            this.grpInfo.Size = new System.Drawing.Size(390, 215);
            this.grpInfo.TabIndex = 0;
            this.grpInfo.TabStop = false;
            this.grpInfo.Text = "Thông tin đề tài & tác giả";
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(20, 30);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(325, 17);
            this.lblDescription.TabIndex = 0;
            this.lblDescription.Text = "Ứng dụng quản lý cửa hàng mini nhiều Form (MDI/SDI)";
            // 
            // FrmAbout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(454, 341);
            this.Controls.Add(this.grpInfo);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblAppName);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAbout";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Giới thiệu ứng dụng";
            this.grpInfo.ResumeLayout(false);
            this.grpInfo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblAppName;
        private System.Windows.Forms.Label lblSubject;
        private System.Windows.Forms.Label lblStudent;
        private System.Windows.Forms.Label lblMSSV;
        private System.Windows.Forms.Label lblClass;
        private System.Windows.Forms.Label lblSchool;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.GroupBox grpInfo;
        private System.Windows.Forms.Label lblDescription;
    }
}
