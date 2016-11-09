namespace SemaphoreTest.View
{
    partial class FormMain
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
            if (disposing && (components != null)) {
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
            this.uctrlSemaphore = new SemaphoreTest.View.UctrlSemaphore();
            this.SuspendLayout();
            // 
            // uctrlSemaphore
            // 
            this.uctrlSemaphore.Location = new System.Drawing.Point(13, 13);
            this.uctrlSemaphore.Name = "uctrlSemaphore";
            this.uctrlSemaphore.Size = new System.Drawing.Size(792, 215);
            this.uctrlSemaphore.TabIndex = 0;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(819, 245);
            this.Controls.Add(this.uctrlSemaphore);
            this.Name = "FormMain";
            this.Text = "FormMain";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private UctrlSemaphore uctrlSemaphore;
    }
}