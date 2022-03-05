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
            this.ScoresButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TitleLabel
            // 
            this.TitleLabel.AutoSize = true;
            this.TitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F);
            this.TitleLabel.Location = new System.Drawing.Point(69, 55);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(210, 37);
            this.TitleLabel.TabIndex = 0;
            this.TitleLabel.Text = "Gridemonium";
            // 
            // EasyMode
            // 
            this.EasyMode.Location = new System.Drawing.Point(116, 117);
            this.EasyMode.Name = "EasyMode";
            this.EasyMode.Size = new System.Drawing.Size(116, 28);
            this.EasyMode.TabIndex = 1;
            this.EasyMode.Text = "Easy Mode";
            this.EasyMode.UseVisualStyleBackColor = true;
            this.EasyMode.Click += new System.EventHandler(this.EasyMode_Click);
            // 
            // NormalMode
            // 
            this.NormalMode.Location = new System.Drawing.Point(116, 151);
            this.NormalMode.Name = "NormalMode";
            this.NormalMode.Size = new System.Drawing.Size(116, 28);
            this.NormalMode.TabIndex = 2;
            this.NormalMode.Text = "Normal Mode";
            this.NormalMode.UseVisualStyleBackColor = true;
            this.NormalMode.Click += new System.EventHandler(this.NormalMode_Click);
            // 
            // HardMode
            // 
            this.HardMode.Location = new System.Drawing.Point(116, 185);
            this.HardMode.Name = "HardMode";
            this.HardMode.Size = new System.Drawing.Size(116, 28);
            this.HardMode.TabIndex = 3;
            this.HardMode.Text = "Hard Mode";
            this.HardMode.UseVisualStyleBackColor = true;
            this.HardMode.Click += new System.EventHandler(this.HardMode_Click);
            // 
            // Guide
            // 
            this.Guide.Location = new System.Drawing.Point(116, 219);
            this.Guide.Name = "Guide";
            this.Guide.Size = new System.Drawing.Size(116, 28);
            this.Guide.TabIndex = 4;
            this.Guide.Text = "Guide";
            this.Guide.UseVisualStyleBackColor = true;
            this.Guide.Click += new System.EventHandler(this.Guide_Click);
            // 
            // ScoresButton
            // 
            this.ScoresButton.Location = new System.Drawing.Point(116, 253);
            this.ScoresButton.Name = "ScoresButton";
            this.ScoresButton.Size = new System.Drawing.Size(116, 28);
            this.ScoresButton.TabIndex = 5;
            this.ScoresButton.Text = "High Scores";
            this.ScoresButton.UseVisualStyleBackColor = true;
            this.ScoresButton.Click += new System.EventHandler(this.ScoresButton_Click);
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(348, 303);
            this.Controls.Add(this.ScoresButton);
            this.Controls.Add(this.Guide);
            this.Controls.Add(this.HardMode);
            this.Controls.Add(this.NormalMode);
            this.Controls.Add(this.EasyMode);
            this.Controls.Add(this.TitleLabel);
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
        private System.Windows.Forms.Button ScoresButton;
    }
}