namespace Gridemonium
{
    partial class HighScores
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
            this.ReturnButton2 = new System.Windows.Forms.Button();
            this.HighScoresLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ReturnButton2
            // 
            this.ReturnButton2.Location = new System.Drawing.Point(79, 322);
            this.ReturnButton2.Name = "ReturnButton2";
            this.ReturnButton2.Size = new System.Drawing.Size(134, 37);
            this.ReturnButton2.TabIndex = 18;
            this.ReturnButton2.Text = "Return to Menu";
            this.ReturnButton2.UseVisualStyleBackColor = true;
            this.ReturnButton2.Click += new System.EventHandler(this.ReturnButton2_Click);
            // 
            // HighScoresLabel
            // 
            this.HighScoresLabel.AutoSize = true;
            this.HighScoresLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HighScoresLabel.Location = new System.Drawing.Point(98, 41);
            this.HighScoresLabel.Name = "HighScoresLabel";
            this.HighScoresLabel.Size = new System.Drawing.Size(96, 200);
            this.HighScoresLabel.TabIndex = 19;
            this.HighScoresLabel.Text = "High Scores\r\n---\r\n---\r\n---\r\n---\r\n---\r\n---\r\n---\r\n---\r\n---";
            this.HighScoresLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // HighScores
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 380);
            this.Controls.Add(this.HighScoresLabel);
            this.Controls.Add(this.ReturnButton2);
            this.Name = "HighScores";
            this.Text = "HighScores";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ReturnButton2;
        private System.Windows.Forms.Label HighScoresLabel;
    }
}