namespace Gridemonium
{
    partial class MainMenu
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
            this.TitleLabel = new System.Windows.Forms.Label();
            this.EasyMode = new System.Windows.Forms.Button();
            this.NormalMode = new System.Windows.Forms.Button();
            this.HardMode = new System.Windows.Forms.Button();
            this.Guide = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TitleLabel
            // 
            this.TitleLabel.AutoSize = true;
            this.TitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F);
            this.TitleLabel.Location = new System.Drawing.Point(193, 157);
            this.TitleLabel.Margin = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(619, 108);
            this.TitleLabel.TabIndex = 0;
            this.TitleLabel.Text = "Gridemonium";
            // 
            // EasyMode
            // 
            this.EasyMode.Location = new System.Drawing.Point(342, 333);
            this.EasyMode.Margin = new System.Windows.Forms.Padding(10, 9, 10, 9);
            this.EasyMode.Name = "EasyMode";
            this.EasyMode.Size = new System.Drawing.Size(367, 80);
            this.EasyMode.TabIndex = 1;
            this.EasyMode.Text = "Easy Mode";
            this.EasyMode.UseVisualStyleBackColor = true;
            // 
            // NormalMode
            // 
            this.NormalMode.Location = new System.Drawing.Point(342, 430);
            this.NormalMode.Margin = new System.Windows.Forms.Padding(10, 9, 10, 9);
            this.NormalMode.Name = "NormalMode";
            this.NormalMode.Size = new System.Drawing.Size(367, 80);
            this.NormalMode.TabIndex = 2;
            this.NormalMode.Text = "Normal Mode";
            this.NormalMode.UseVisualStyleBackColor = true;
            this.NormalMode.Click += new System.EventHandler(this.NormalMode_Click);
            // 
            // HardMode
            // 
            this.HardMode.Location = new System.Drawing.Point(342, 527);
            this.HardMode.Margin = new System.Windows.Forms.Padding(10, 9, 10, 9);
            this.HardMode.Name = "HardMode";
            this.HardMode.Size = new System.Drawing.Size(367, 80);
            this.HardMode.TabIndex = 3;
            this.HardMode.Text = "Hard Mode";
            this.HardMode.UseVisualStyleBackColor = true;
            // 
            // Guide
            // 
            this.Guide.Location = new System.Drawing.Point(342, 623);
            this.Guide.Margin = new System.Windows.Forms.Padding(10, 9, 10, 9);
            this.Guide.Name = "Guide";
            this.Guide.Size = new System.Drawing.Size(367, 80);
            this.Guide.TabIndex = 4;
            this.Guide.Text = "Guide";
            this.Guide.UseVisualStyleBackColor = true;
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(19F, 37F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(1102, 783);
            this.Controls.Add(this.Guide);
            this.Controls.Add(this.HardMode);
            this.Controls.Add(this.NormalMode);
            this.Controls.Add(this.EasyMode);
            this.Controls.Add(this.TitleLabel);
            this.Margin = new System.Windows.Forms.Padding(10, 9, 10, 9);
            this.Name = "MainMenu";
            this.Text = "MainMenu";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label TitleLabel;
        private System.Windows.Forms.Button EasyMode;
        private System.Windows.Forms.Button NormalMode;
        private System.Windows.Forms.Button HardMode;
        private System.Windows.Forms.Button Guide;
    }
}