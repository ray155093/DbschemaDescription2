namespace DbschemaDescription
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnLogin = new System.Windows.Forms.Button();
            this.txtDbConnect = new System.Windows.Forms.TextBox();
            this.txtDbAccount = new System.Windows.Forms.TextBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.txtDbPassword = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDbTableName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "資料庫位置";
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(314, 22);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(75, 23);
            this.btnLogin.TabIndex = 1;
            this.btnLogin.Text = "登入";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // txtDbConnect
            // 
            this.txtDbConnect.Location = new System.Drawing.Point(100, 16);
            this.txtDbConnect.Name = "txtDbConnect";
            this.txtDbConnect.Size = new System.Drawing.Size(192, 25);
            this.txtDbConnect.TabIndex = 2;
            // 
            // txtDbAccount
            // 
            this.txtDbAccount.Location = new System.Drawing.Point(100, 47);
            this.txtDbAccount.Name = "txtDbAccount";
            this.txtDbAccount.Size = new System.Drawing.Size(192, 25);
            this.txtDbAccount.TabIndex = 4;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(57, 57);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(37, 15);
            this.Label2.TabIndex = 3;
            this.Label2.Text = "帳號";
            // 
            // txtDbPassword
            // 
            this.txtDbPassword.Location = new System.Drawing.Point(100, 82);
            this.txtDbPassword.Name = "txtDbPassword";
            this.txtDbPassword.Size = new System.Drawing.Size(192, 25);
            this.txtDbPassword.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(57, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "密碼";
            // 
            // txtDbTableName
            // 
            this.txtDbTableName.Location = new System.Drawing.Point(100, 120);
            this.txtDbTableName.Name = "txtDbTableName";
            this.txtDbTableName.Size = new System.Drawing.Size(192, 25);
            this.txtDbTableName.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 123);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 15);
            this.label4.TabIndex = 7;
            this.label4.Text = "資料庫名稱";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(758, 253);
            this.Controls.Add(this.txtDbTableName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtDbPassword);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtDbAccount);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.txtDbConnect);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "DbschemaDescription";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.TextBox txtDbConnect;
        private System.Windows.Forms.TextBox txtDbAccount;
        private System.Windows.Forms.Label Label2;
        private System.Windows.Forms.TextBox txtDbPassword;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDbTableName;
        private System.Windows.Forms.Label label4;
    }
}

