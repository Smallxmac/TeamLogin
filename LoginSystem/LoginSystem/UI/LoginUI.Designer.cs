namespace LoginSystem
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
            this.Login_Button = new System.Windows.Forms.Button();
            this.Remember__Box = new System.Windows.Forms.CheckBox();
            this.User_Box = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Register_Button = new System.Windows.Forms.Button();
            this.Pass_Box = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Login_Button
            // 
            this.Login_Button.Location = new System.Drawing.Point(3, 220);
            this.Login_Button.Name = "Login_Button";
            this.Login_Button.Size = new System.Drawing.Size(107, 32);
            this.Login_Button.TabIndex = 0;
            this.Login_Button.Text = "Login";
            this.Login_Button.UseVisualStyleBackColor = true;
            // 
            // Remember__Box
            // 
            this.Remember__Box.AutoSize = true;
            this.Remember__Box.Location = new System.Drawing.Point(191, 109);
            this.Remember__Box.Name = "Remember__Box";
            this.Remember__Box.Size = new System.Drawing.Size(95, 17);
            this.Remember__Box.TabIndex = 1;
            this.Remember__Box.Text = "Remember Me";
            this.Remember__Box.UseVisualStyleBackColor = true;
            // 
            // User_Box
            // 
            this.User_Box.Location = new System.Drawing.Point(191, 26);
            this.User_Box.Name = "User_Box";
            this.User_Box.Size = new System.Drawing.Size(172, 20);
            this.User_Box.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Email or Username:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Password: ";
            // 
            // Register_Button
            // 
            this.Register_Button.Location = new System.Drawing.Point(263, 220);
            this.Register_Button.Name = "Register_Button";
            this.Register_Button.Size = new System.Drawing.Size(100, 32);
            this.Register_Button.TabIndex = 5;
            this.Register_Button.Text = "Register";
            this.Register_Button.UseVisualStyleBackColor = true;
            // 
            // Pass_Box
            // 
            this.Pass_Box.Location = new System.Drawing.Point(191, 71);
            this.Pass_Box.Name = "Pass_Box";
            this.Pass_Box.PasswordChar = '*';
            this.Pass_Box.Size = new System.Drawing.Size(172, 20);
            this.Pass_Box.TabIndex = 6;
            this.Pass_Box.TextChanged += new System.EventHandler(this.Pass_Box_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(386, 267);
            this.Controls.Add(this.Pass_Box);
            this.Controls.Add(this.Register_Button);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.User_Box);
            this.Controls.Add(this.Remember__Box);
            this.Controls.Add(this.Login_Button);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Login_Button;
        private System.Windows.Forms.CheckBox Remember__Box;
        private System.Windows.Forms.TextBox User_Box;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Register_Button;
        private System.Windows.Forms.TextBox Pass_Box;
    }
}

