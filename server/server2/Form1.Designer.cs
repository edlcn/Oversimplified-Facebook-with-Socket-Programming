namespace server2
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
            this.Port = new System.Windows.Forms.TextBox();
            this.Log = new System.Windows.Forms.RichTextBox();
            this.listen = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Port
            // 
            this.Port.Location = new System.Drawing.Point(70, 55);
            this.Port.Name = "Port";
            this.Port.Size = new System.Drawing.Size(288, 20);
            this.Port.TabIndex = 0;
            // 
            // Log
            // 
            this.Log.Location = new System.Drawing.Point(29, 107);
            this.Log.Name = "Log";
            this.Log.Size = new System.Drawing.Size(460, 266);
            this.Log.TabIndex = 1;
            this.Log.Text = "";
            // 
            // listen
            // 
            this.listen.Location = new System.Drawing.Point(381, 55);
            this.listen.Name = "listen";
            this.listen.Size = new System.Drawing.Size(108, 21);
            this.listen.TabIndex = 2;
            this.listen.Text = "listen";
            this.listen.UseVisualStyleBackColor = true;
            this.listen.Click += new System.EventHandler(this.listen_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Port:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(517, 395);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listen);
            this.Controls.Add(this.Log);
            this.Controls.Add(this.Port);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Port;
        private System.Windows.Forms.RichTextBox Log;
        private System.Windows.Forms.Button listen;
        private System.Windows.Forms.Label label1;
    }
}

