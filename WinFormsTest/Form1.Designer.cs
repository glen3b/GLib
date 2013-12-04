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
            this.requiredTextBox1 = new Glib.WinForms.Controls.RequiredTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // errorProvider1
            // 
            this.errorProvider1.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider1.ContainerControl = this;
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
    }
}

