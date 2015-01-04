namespace LoginSystem.UI
{
    partial class LoginUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginUI));
            this.Login_Button = new System.Windows.Forms.Button();
            this.Remember_Check = new System.Windows.Forms.CheckBox();
            this.Username_Box = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Register_Button = new System.Windows.Forms.Button();
            this.Password_Box = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Login_Button
            // 
            this.Login_Button.Location = new System.Drawing.Point(12, 72);
            this.Login_Button.Name = "Login_Button";
            this.Login_Button.Size = new System.Drawing.Size(81, 21);
            this.Login_Button.TabIndex = 4;
            this.Login_Button.Text = "Login";
            this.Login_Button.UseVisualStyleBackColor = true;
            this.Login_Button.Click += new System.EventHandler(this.Login_Button_Click);
            // 
            // Remember_Check
            // 
            this.Remember_Check.AutoSize = true;
            this.Remember_Check.Location = new System.Drawing.Point(98, 75);
            this.Remember_Check.Name = "Remember_Check";
            this.Remember_Check.Size = new System.Drawing.Size(95, 17);
            this.Remember_Check.TabIndex = 3;
            this.Remember_Check.Text = "Remember Me";
            this.Remember_Check.UseVisualStyleBackColor = true;
            // 
            // Username_Box
            // 
            this.Username_Box.Location = new System.Drawing.Point(106, 12);
            this.Username_Box.Name = "Username_Box";
            this.Username_Box.Size = new System.Drawing.Size(184, 20);
            this.Username_Box.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Email/Username:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Password: ";
            // 
            // Register_Button
            // 
            this.Register_Button.Location = new System.Drawing.Point(199, 72);
            this.Register_Button.Name = "Register_Button";
            this.Register_Button.Size = new System.Drawing.Size(91, 21);
            this.Register_Button.TabIndex = 0;
            this.Register_Button.Text = "Register";
            this.Register_Button.UseVisualStyleBackColor = true;
            this.Register_Button.Click += new System.EventHandler(this.Register_Button_Click);
            // 
            // Password_Box
            // 
            this.Password_Box.Location = new System.Drawing.Point(106, 38);
            this.Password_Box.Name = "Password_Box";
            this.Password_Box.PasswordChar = '*';
            this.Password_Box.Size = new System.Drawing.Size(184, 20);
            this.Password_Box.TabIndex = 2;
            // 
            // LoginUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(302, 106);
            this.Controls.Add(this.Password_Box);
            this.Controls.Add(this.Register_Button);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Username_Box);
            this.Controls.Add(this.Remember_Check);
            this.Controls.Add(this.Login_Button);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LoginUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login UI";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button Login_Button;
        public System.Windows.Forms.CheckBox Remember_Check;
        public System.Windows.Forms.TextBox Username_Box;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.Button Register_Button;
        public System.Windows.Forms.TextBox Password_Box;
    }
}

