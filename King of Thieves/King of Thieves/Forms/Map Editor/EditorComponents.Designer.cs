namespace King_of_Thieves.Forms.Map_Editor
{
    partial class EditorComponents
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
            this.btnPlayTest = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnPlayTest
            // 
            this.btnPlayTest.Location = new System.Drawing.Point(2, 3);
            this.btnPlayTest.Name = "btnPlayTest";
            this.btnPlayTest.Size = new System.Drawing.Size(75, 23);
            this.btnPlayTest.TabIndex = 0;
            this.btnPlayTest.Text = "Test";
            this.btnPlayTest.UseVisualStyleBackColor = true;
            this.btnPlayTest.Click += new System.EventHandler(this.btnPlayTest_Click);
            // 
            // EditorComponents
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.btnPlayTest);
            this.Name = "EditorComponents";
            this.Text = "EditorComponents";
            this.Load += new System.EventHandler(this.EditorComponents_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnPlayTest;
    }
}