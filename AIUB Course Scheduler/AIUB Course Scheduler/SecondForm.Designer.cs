namespace AIUB_Course_Scheduler
{
    partial class SecondForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.deptLabel = new System.Windows.Forms.Label();
            this.backBtn = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.confirmBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // deptLabel
            // 
            this.deptLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.deptLabel.AutoSize = true;
            this.deptLabel.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deptLabel.Location = new System.Drawing.Point(12, 49);
            this.deptLabel.Name = "deptLabel";
            this.deptLabel.Size = new System.Drawing.Size(260, 29);
            this.deptLabel.TabIndex = 0;
            this.deptLabel.Text = "Select your department :";
            this.deptLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.deptLabel.Click += new System.EventHandler(this.label1_Click);
            // 
            // backBtn
            // 
            this.backBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.backBtn.AutoSize = true;
            this.backBtn.BackColor = System.Drawing.SystemColors.MenuBar;
            this.backBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.backBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.backBtn.Location = new System.Drawing.Point(17, 141);
            this.backBtn.Name = "backBtn";
            this.backBtn.Size = new System.Drawing.Size(102, 44);
            this.backBtn.TabIndex = 1;
            this.backBtn.Text = "Back";
            this.backBtn.UseVisualStyleBackColor = false;
            this.backBtn.Click += new System.EventHandler(this.backBtn_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Computer Science and Engineering",
            "Software Engineering"});
            this.comboBox1.Location = new System.Drawing.Point(311, 49);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(176, 32);
            this.comboBox1.TabIndex = 2;
            this.comboBox1.Tag = "";
            this.comboBox1.Text = "Select From here";
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // confirmBtn
            // 
            this.confirmBtn.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.confirmBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.confirmBtn.Enabled = false;
            this.confirmBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.confirmBtn.Location = new System.Drawing.Point(311, 141);
            this.confirmBtn.Name = "confirmBtn";
            this.confirmBtn.Size = new System.Drawing.Size(176, 44);
            this.confirmBtn.TabIndex = 3;
            this.confirmBtn.Text = "CONFIRM";
            this.confirmBtn.UseVisualStyleBackColor = false;
            this.confirmBtn.Click += new System.EventHandler(this.confirmBtn_Click);
            // 
            // SecondForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(565, 210);
            this.Controls.Add(this.confirmBtn);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.backBtn);
            this.Controls.Add(this.deptLabel);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "SecondForm";
            this.Text = "Department Selection";
            this.Load += new System.EventHandler(this.SecondForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label deptLabel;
        private System.Windows.Forms.Button backBtn;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button confirmBtn;
    }
}