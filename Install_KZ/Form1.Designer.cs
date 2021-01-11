namespace Install_KZ
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnClose = new System.Windows.Forms.Button();
            this.btnInstall = new System.Windows.Forms.Button();
            this.cbxInstallType = new System.Windows.Forms.ComboBox();
            this.txbServerIP = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txbServerName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txbSQLUser = new System.Windows.Forms.TextBox();
            this.txbSQLPass = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnConnectionTest = new System.Windows.Forms.Button();
            this.lblTestConnection = new System.Windows.Forms.Label();
            this.checkBoxCopyFiles = new System.Windows.Forms.CheckBox();
            this.checkBoxAddTextToDB = new System.Windows.Forms.CheckBox();
            this.checkBoxAddDevicesToDB = new System.Windows.Forms.CheckBox();
            this.lblDone = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(312, 271);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnInstall
            // 
            this.btnInstall.Location = new System.Drawing.Point(12, 271);
            this.btnInstall.Name = "btnInstall";
            this.btnInstall.Size = new System.Drawing.Size(75, 23);
            this.btnInstall.TabIndex = 1;
            this.btnInstall.Text = "Install";
            this.btnInstall.UseVisualStyleBackColor = true;
            this.btnInstall.Click += new System.EventHandler(this.btnInstall_Click);
            // 
            // cbxInstallType
            // 
            this.cbxInstallType.FormattingEnabled = true;
            this.cbxInstallType.Items.AddRange(new object[] {
            "Server",
            "APM",
            "CashDesk",
            "Column"});
            this.cbxInstallType.Location = new System.Drawing.Point(13, 27);
            this.cbxInstallType.Name = "cbxInstallType";
            this.cbxInstallType.Size = new System.Drawing.Size(121, 21);
            this.cbxInstallType.TabIndex = 2;
            // 
            // txbServerIP
            // 
            this.txbServerIP.Location = new System.Drawing.Point(13, 86);
            this.txbServerIP.Name = "txbServerIP";
            this.txbServerIP.Size = new System.Drawing.Size(121, 20);
            this.txbServerIP.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Server IP";
            // 
            // txbServerName
            // 
            this.txbServerName.Location = new System.Drawing.Point(13, 145);
            this.txbServerName.Name = "txbServerName";
            this.txbServerName.Size = new System.Drawing.Size(121, 20);
            this.txbServerName.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Installation type";
            // 
            // txbSQLUser
            // 
            this.txbSQLUser.Location = new System.Drawing.Point(226, 46);
            this.txbSQLUser.Name = "txbSQLUser";
            this.txbSQLUser.Size = new System.Drawing.Size(161, 20);
            this.txbSQLUser.TabIndex = 8;
            // 
            // txbSQLPass
            // 
            this.txbSQLPass.Location = new System.Drawing.Point(226, 105);
            this.txbSQLPass.Name = "txbSQLPass";
            this.txbSQLPass.PasswordChar = '*';
            this.txbSQLPass.Size = new System.Drawing.Size(161, 20);
            this.txbSQLPass.TabIndex = 9;
            this.txbSQLPass.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 128);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Server Name";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(226, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "SQL user name";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(226, 88);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "SQL password";
            // 
            // btnConnectionTest
            // 
            this.btnConnectionTest.Location = new System.Drawing.Point(310, 135);
            this.btnConnectionTest.Name = "btnConnectionTest";
            this.btnConnectionTest.Size = new System.Drawing.Size(75, 23);
            this.btnConnectionTest.TabIndex = 12;
            this.btnConnectionTest.Text = "Test";
            this.btnConnectionTest.UseVisualStyleBackColor = true;
            this.btnConnectionTest.Click += new System.EventHandler(this.btnConnectionTest_Click);
            // 
            // lblTestConnection
            // 
            this.lblTestConnection.AutoSize = true;
            this.lblTestConnection.Location = new System.Drawing.Point(225, 140);
            this.lblTestConnection.Name = "lblTestConnection";
            this.lblTestConnection.Size = new System.Drawing.Size(84, 13);
            this.lblTestConnection.TabIndex = 13;
            this.lblTestConnection.Text = "Test connection";
            // 
            // checkBoxCopyFiles
            // 
            this.checkBoxCopyFiles.AutoSize = true;
            this.checkBoxCopyFiles.Enabled = false;
            this.checkBoxCopyFiles.Location = new System.Drawing.Point(13, 180);
            this.checkBoxCopyFiles.Name = "checkBoxCopyFiles";
            this.checkBoxCopyFiles.Size = new System.Drawing.Size(71, 17);
            this.checkBoxCopyFiles.TabIndex = 16;
            this.checkBoxCopyFiles.Text = "Copy files";
            this.checkBoxCopyFiles.UseVisualStyleBackColor = true;
            // 
            // checkBoxAddTextToDB
            // 
            this.checkBoxAddTextToDB.AutoSize = true;
            this.checkBoxAddTextToDB.Enabled = false;
            this.checkBoxAddTextToDB.Location = new System.Drawing.Point(13, 204);
            this.checkBoxAddTextToDB.Name = "checkBoxAddTextToDB";
            this.checkBoxAddTextToDB.Size = new System.Drawing.Size(95, 17);
            this.checkBoxAddTextToDB.TabIndex = 17;
            this.checkBoxAddTextToDB.Text = "Add text to DB";
            this.checkBoxAddTextToDB.UseVisualStyleBackColor = true;
            // 
            // checkBoxAddDevicesToDB
            // 
            this.checkBoxAddDevicesToDB.AutoSize = true;
            this.checkBoxAddDevicesToDB.Enabled = false;
            this.checkBoxAddDevicesToDB.Location = new System.Drawing.Point(13, 228);
            this.checkBoxAddDevicesToDB.Name = "checkBoxAddDevicesToDB";
            this.checkBoxAddDevicesToDB.Size = new System.Drawing.Size(115, 17);
            this.checkBoxAddDevicesToDB.TabIndex = 18;
            this.checkBoxAddDevicesToDB.Text = "Add devices to DB";
            this.checkBoxAddDevicesToDB.UseVisualStyleBackColor = true;
            // 
            // lblDone
            // 
            this.lblDone.AutoSize = true;
            this.lblDone.Location = new System.Drawing.Point(350, 240);
            this.lblDone.Name = "lblDone";
            this.lblDone.Size = new System.Drawing.Size(36, 13);
            this.lblDone.TabIndex = 19;
            this.lblDone.Text = "Done!";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(399, 306);
            this.Controls.Add(this.lblDone);
            this.Controls.Add(this.checkBoxAddDevicesToDB);
            this.Controls.Add(this.checkBoxAddTextToDB);
            this.Controls.Add(this.checkBoxCopyFiles);
            this.Controls.Add(this.lblTestConnection);
            this.Controls.Add(this.btnConnectionTest);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txbSQLPass);
            this.Controls.Add(this.txbSQLUser);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txbServerName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txbServerIP);
            this.Controls.Add(this.cbxInstallType);
            this.Controls.Add(this.btnInstall);
            this.Controls.Add(this.btnClose);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Fiscal Device installation";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnInstall;
        private System.Windows.Forms.ComboBox cbxInstallType;
        private System.Windows.Forms.TextBox txbServerIP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txbServerName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txbSQLUser;
        private System.Windows.Forms.TextBox txbSQLPass;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnConnectionTest;
        private System.Windows.Forms.Label lblTestConnection;
        private System.Windows.Forms.CheckBox checkBoxCopyFiles;
        private System.Windows.Forms.CheckBox checkBoxAddTextToDB;
        private System.Windows.Forms.CheckBox checkBoxAddDevicesToDB;
        private System.Windows.Forms.Label lblDone;
    }
}

