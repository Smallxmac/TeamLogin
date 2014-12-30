namespace LoginSystem.UI
{
    partial class RegisterUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RegisterUI));
            this.Cancel_button = new System.Windows.Forms.Button();
            this.Register_button = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.Password_Box = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.PasswordAgain_box = new System.Windows.Forms.TextBox();
            this.Username_Box = new System.Windows.Forms.TextBox();
            this.Email_Box = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Cancel_button
            // 
            this.Cancel_button.Location = new System.Drawing.Point(10, 106);
            this.Cancel_button.Name = "Cancel_button";
            this.Cancel_button.Size = new System.Drawing.Size(77, 23);
            this.Cancel_button.TabIndex = 0;
            this.Cancel_button.Text = "Cancel";
            this.Cancel_button.UseVisualStyleBackColor = true;
            // 
            // Register_button
            // 
            this.Register_button.Location = new System.Drawing.Point(145, 106);
            this.Register_button.Name = "Register_button";
            this.Register_button.Size = new System.Drawing.Size(80, 23);
            this.Register_button.TabIndex = 1;
            this.Register_button.Text = "Register";
            this.Register_button.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Username: ";
            // 
            // Password_Box
            // 
            this.Password_Box.Location = new System.Drawing.Point(88, 28);
            this.Password_Box.Name = "Password_Box";
            this.Password_Box.PasswordChar = '*';
            this.Password_Box.Size = new System.Drawing.Size(137, 20);
            this.Password_Box.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Password:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Again: ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 77);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "E-Mail:";
            // 
            // PasswordAgain_box
            // 
            this.PasswordAgain_box.Location = new System.Drawing.Point(88, 50);
            this.PasswordAgain_box.Name = "PasswordAgain_box";
            this.PasswordAgain_box.PasswordChar = '*';
            this.PasswordAgain_box.Size = new System.Drawing.Size(137, 20);
            this.PasswordAgain_box.TabIndex = 7;
            // 
            // Username_Box
            // 
            this.Username_Box.Location = new System.Drawing.Point(88, 6);
            this.Username_Box.Name = "Username_Box";
            this.Username_Box.Size = new System.Drawing.Size(137, 20);
            this.Username_Box.TabIndex = 8;
            // 
            // Email_Box
            // 
            this.Email_Box.Location = new System.Drawing.Point(88, 74);
            this.Email_Box.Name = "Email_Box";
            this.Email_Box.Size = new System.Drawing.Size(137, 20);
            this.Email_Box.TabIndex = 9;
            // 
            // RegisterUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(241, 141);
            this.Controls.Add(this.Email_Box);
            this.Controls.Add(this.Username_Box);
            this.Controls.Add(this.PasswordAgain_box);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Password_Box);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Register_button);
            this.Controls.Add(this.Cancel_button);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RegisterUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RegisterUI";
            this.Load += new System.EventHandler(this.RegisterUI_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Cancel_button;
        private System.Windows.Forms.Button Register_button;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox Password_Box;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox PasswordAgain_box;
        private System.Windows.Forms.TextBox Username_Box;
        private System.Windows.Forms.TextBox Email_Box;
    }
}