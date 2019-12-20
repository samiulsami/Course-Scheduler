namespace AIUB_Course_Scheduler
{
    partial class FirstForm
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
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.appName_Label = new System.Windows.Forms.Label();
            this.enterBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // appName_Label
            // 
            this.appName_Label.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.appName_Label.AutoSize = true;
            this.appName_Label.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.appName_Label.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.appName_Label.Font = new System.Drawing.Font("Lucida Calligraphy", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.appName_Label.Location = new System.Drawing.Point(117, 48);
            this.appName_Label.Name = "appName_Label";
            this.appName_Label.Size = new System.Drawing.Size(670, 52);
            this.appName_Label.TabIndex = 0;
            this.appName_Label.Text = "AIUB COURSE SCHEDULER";
            this.appName_Label.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.appName_Label.Click += new System.EventHandler(this.appName_Label_Click);
            // 
            // enterBtn
            // 
            this.enterBtn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.enterBtn.AutoSize = true;
            this.enterBtn.BackColor = System.Drawing.Color.MediumTurquoise;
            this.enterBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.enterBtn.Font = new System.Drawing.Font("Elephant", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.enterBtn.Location = new System.Drawing.Point(370, 296);
            this.enterBtn.Name = "enterBtn";
            this.enterBtn.Size = new System.Drawing.Size(112, 41);
            this.enterBtn.TabIndex = 1;
            this.enterBtn.Text = "Enter";
            this.enterBtn.UseVisualStyleBackColor = false;
            this.enterBtn.Click += new System.EventHandler(this.enterBtn_Click);
            // 
            // FirstForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.SystemColors.HighlightText;
            this.BackgroundImage = global::AIUB_Course_Scheduler.Properties.Resources.AiubPermanentCampus_7;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(874, 503);
            this.Controls.Add(this.enterBtn);
            this.Controls.Add(this.appName_Label);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FirstForm";
            this.Text = "AIUB COURSE SCHEDULER";
            this.TransparencyKey = System.Drawing.Color.White;
            this.Load += new System.EventHandler(this.FirstForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label AppName;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.Label appName_Label;
        private System.Windows.Forms.Button enterBtn;
    }
}

