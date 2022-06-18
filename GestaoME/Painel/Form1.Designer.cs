namespace Painel
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_ClickServ = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_ClickServ
            // 
            this.btn_ClickServ.Location = new System.Drawing.Point(15, 21);
            this.btn_ClickServ.Name = "btn_ClickServ";
            this.btn_ClickServ.Size = new System.Drawing.Size(75, 23);
            this.btn_ClickServ.TabIndex = 0;
            this.btn_ClickServ.Text = "ClickServ";
            this.btn_ClickServ.UseVisualStyleBackColor = true;
            this.btn_ClickServ.Click += new System.EventHandler(this.btn_ClickServ_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btn_ClickServ);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_ClickServ;
    }
}
