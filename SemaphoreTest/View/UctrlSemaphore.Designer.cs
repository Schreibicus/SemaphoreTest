namespace SemaphoreTest.View
{
    partial class UctrlSemaphore
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbWorkingThreads = new System.Windows.Forms.ListBox();
            this.lbWaitingThreads = new System.Windows.Forms.ListBox();
            this.lbNewThreads = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.numSemaphoreCapacity = new System.Windows.Forms.NumericUpDown();
            this.btnAddThread = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numSemaphoreCapacity)).BeginInit();
            this.SuspendLayout();
            // 
            // lbWorkingThreads
            // 
            this.lbWorkingThreads.FormattingEnabled = true;
            this.lbWorkingThreads.Location = new System.Drawing.Point(16, 35);
            this.lbWorkingThreads.Name = "lbWorkingThreads";
            this.lbWorkingThreads.Size = new System.Drawing.Size(250, 108);
            this.lbWorkingThreads.TabIndex = 0;
            // 
            // lbWaitingThreads
            // 
            this.lbWaitingThreads.FormattingEnabled = true;
            this.lbWaitingThreads.Location = new System.Drawing.Point(272, 35);
            this.lbWaitingThreads.Name = "lbWaitingThreads";
            this.lbWaitingThreads.Size = new System.Drawing.Size(250, 108);
            this.lbWaitingThreads.TabIndex = 1;
            // 
            // lbNewThreads
            // 
            this.lbNewThreads.FormattingEnabled = true;
            this.lbNewThreads.Location = new System.Drawing.Point(528, 35);
            this.lbNewThreads.Name = "lbNewThreads";
            this.lbNewThreads.Size = new System.Drawing.Size(250, 108);
            this.lbNewThreads.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Green;
            this.label1.Location = new System.Drawing.Point(73, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 19);
            this.label1.TabIndex = 3;
            this.label1.Text = "Working Threads";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label2.Location = new System.Drawing.Point(595, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 19);
            this.label2.TabIndex = 4;
            this.label2.Text = "New Threads";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.label3.Location = new System.Drawing.Point(333, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(119, 19);
            this.label3.TabIndex = 5;
            this.label3.Text = "Waiting Threads";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(13, 157);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(124, 16);
            this.label4.TabIndex = 6;
            this.label4.Text = "Semaphore Capacity";
            // 
            // numSemaphoreCapacity
            // 
            this.numSemaphoreCapacity.Location = new System.Drawing.Point(16, 177);
            this.numSemaphoreCapacity.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numSemaphoreCapacity.Name = "numSemaphoreCapacity";
            this.numSemaphoreCapacity.Size = new System.Drawing.Size(120, 20);
            this.numSemaphoreCapacity.TabIndex = 7;
            this.numSemaphoreCapacity.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // btnAddThread
            // 
            this.btnAddThread.Location = new System.Drawing.Point(528, 177);
            this.btnAddThread.Name = "btnAddThread";
            this.btnAddThread.Size = new System.Drawing.Size(75, 23);
            this.btnAddThread.TabIndex = 8;
            this.btnAddThread.Text = "Add Thread";
            this.btnAddThread.UseVisualStyleBackColor = true;
            // 
            // UctrlSemaphore
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnAddThread);
            this.Controls.Add(this.numSemaphoreCapacity);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbNewThreads);
            this.Controls.Add(this.lbWaitingThreads);
            this.Controls.Add(this.lbWorkingThreads);
            this.Name = "UctrlSemaphore";
            this.Size = new System.Drawing.Size(792, 215);
            ((System.ComponentModel.ISupportInitialize)(this.numSemaphoreCapacity)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbWorkingThreads;
        private System.Windows.Forms.ListBox lbWaitingThreads;
        private System.Windows.Forms.ListBox lbNewThreads;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numSemaphoreCapacity;
        private System.Windows.Forms.Button btnAddThread;
    }
}
