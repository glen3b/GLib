namespace WinFormsTest
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.uacButton = new System.Windows.Forms.Button();
            this.requiredTextBox1 = new Glib.WinForms.Controls.RequiredTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // errorProvider1
            // 
            this.errorProvider1.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider1.ContainerControl = this;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(612, 10);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(100, 23);
            this.progressBar1.TabIndex = 1;
            this.progressBar1.Value = 50;
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // uacButton
            // 
            this.uacButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.uacButton.Location = new System.Drawing.Point(295, 11);
            this.uacButton.Name = "uacButton";
            this.uacButton.Size = new System.Drawing.Size(207, 23);
            this.uacButton.TabIndex = 2;
            this.uacButton.Text = "Unconditional UAC Request";
            this.uacButton.UseVisualStyleBackColor = true;
            // 
            // requiredTextBox1
            // 
            this.requiredTextBox1.InvalidityError = "You don\'t have the right name.";
            this.requiredTextBox1.Location = new System.Drawing.Point(13, 13);
            this.requiredTextBox1.Name = "requiredTextBox1";
            this.requiredTextBox1.Size = new System.Drawing.Size(100, 20);
            this.requiredTextBox1.TabIndex = 0;
            this.requiredTextBox1.TextChanged += new System.EventHandler(this.requiredTextBox1_TextChanged);
            this.requiredTextBox1.Validated += new System.EventHandler(this.requiredTextBox_Validated);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(724, 403);
            this.Controls.Add(this.uacButton);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.requiredTextBox1);
            this.Name = "Form1";
            this.Text = "Test Form";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Glib.WinForms.Controls.RequiredTextBox requiredTextBox1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button uacButton;
    }
}

