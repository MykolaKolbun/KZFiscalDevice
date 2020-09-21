namespace KZ_ShtrikhM_FiscalDevice
{
    partial class ServiceForm_APM
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServiceForm_APM));
            this.gbxStatus = new System.Windows.Forms.GroupBox();
            this.cbxOutOfPaper = new System.Windows.Forms.CheckBox();
            this.cbxBlocked = new System.Windows.Forms.CheckBox();
            this.cbxShiftOpened = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnSale = new System.Windows.Forms.Button();
            this.cbxIsReturn = new System.Windows.Forms.CheckBox();
            this.cbxIsCashless = new System.Windows.Forms.CheckBox();
            this.txbSaleAmount = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.cbxRepSel = new System.Windows.Forms.ComboBox();
            this.btnReceiptCopy = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txbNr = new System.Windows.Forms.TextBox();
            this.btnZRepCopy = new System.Windows.Forms.Button();
            this.btnReportbyDate = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dateTimePickerTo = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerFrom = new System.Windows.Forms.DateTimePicker();
            this.btnZRep = new System.Windows.Forms.Button();
            this.btnXRep = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txbCashOut = new System.Windows.Forms.TextBox();
            this.txbCashIn = new System.Windows.Forms.TextBox();
            this.btnCashOut = new System.Windows.Forms.Button();
            this.btnCashIn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblSysTimeDate = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.gbxStatus.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbxStatus
            // 
            this.gbxStatus.Controls.Add(this.cbxOutOfPaper);
            this.gbxStatus.Controls.Add(this.cbxBlocked);
            this.gbxStatus.Controls.Add(this.cbxShiftOpened);
            this.gbxStatus.Enabled = false;
            this.gbxStatus.Location = new System.Drawing.Point(13, 13);
            this.gbxStatus.Name = "gbxStatus";
            this.gbxStatus.Size = new System.Drawing.Size(135, 91);
            this.gbxStatus.TabIndex = 0;
            this.gbxStatus.TabStop = false;
            this.gbxStatus.Text = "Статус";
            // 
            // cbxOutOfPaper
            // 
            this.cbxOutOfPaper.AutoSize = true;
            this.cbxOutOfPaper.Location = new System.Drawing.Point(7, 68);
            this.cbxOutOfPaper.Name = "cbxOutOfPaper";
            this.cbxOutOfPaper.Size = new System.Drawing.Size(124, 17);
            this.cbxOutOfPaper.TabIndex = 2;
            this.cbxOutOfPaper.Text = "Закончилась лента";
            this.cbxOutOfPaper.UseVisualStyleBackColor = true;
            // 
            // cbxBlocked
            // 
            this.cbxBlocked.AutoSize = true;
            this.cbxBlocked.Location = new System.Drawing.Point(7, 44);
            this.cbxBlocked.Name = "cbxBlocked";
            this.cbxBlocked.Size = new System.Drawing.Size(123, 17);
            this.cbxBlocked.TabIndex = 1;
            this.cbxBlocked.Text = "РРО заблокирован";
            this.cbxBlocked.UseVisualStyleBackColor = true;
            // 
            // cbxShiftOpened
            // 
            this.cbxShiftOpened.AutoSize = true;
            this.cbxShiftOpened.Location = new System.Drawing.Point(7, 20);
            this.cbxShiftOpened.Name = "cbxShiftOpened";
            this.cbxShiftOpened.Size = new System.Drawing.Size(104, 17);
            this.cbxShiftOpened.TabIndex = 0;
            this.cbxShiftOpened.Text = "Смена открыта";
            this.cbxShiftOpened.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnSale);
            this.groupBox2.Controls.Add(this.cbxIsReturn);
            this.groupBox2.Controls.Add(this.cbxIsCashless);
            this.groupBox2.Controls.Add(this.txbSaleAmount);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(13, 105);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(135, 121);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Продажа";
            // 
            // btnSale
            // 
            this.btnSale.Location = new System.Drawing.Point(26, 94);
            this.btnSale.Name = "btnSale";
            this.btnSale.Size = new System.Drawing.Size(85, 23);
            this.btnSale.TabIndex = 4;
            this.btnSale.Text = "Продажа";
            this.btnSale.UseVisualStyleBackColor = true;
            this.btnSale.Click += new System.EventHandler(this.btnSale_Click);
            // 
            // cbxIsReturn
            // 
            this.cbxIsReturn.AutoSize = true;
            this.cbxIsReturn.Location = new System.Drawing.Point(7, 71);
            this.cbxIsReturn.Name = "cbxIsReturn";
            this.cbxIsReturn.Size = new System.Drawing.Size(68, 17);
            this.cbxIsReturn.TabIndex = 3;
            this.cbxIsReturn.Text = "Возврат";
            this.cbxIsReturn.UseVisualStyleBackColor = true;
            // 
            // cbxIsCashless
            // 
            this.cbxIsCashless.AutoSize = true;
            this.cbxIsCashless.Location = new System.Drawing.Point(7, 47);
            this.cbxIsCashless.Name = "cbxIsCashless";
            this.cbxIsCashless.Size = new System.Drawing.Size(94, 17);
            this.cbxIsCashless.TabIndex = 2;
            this.cbxIsCashless.Text = "Безналичный";
            this.cbxIsCashless.UseVisualStyleBackColor = true;
            // 
            // txbSaleAmount
            // 
            this.txbSaleAmount.Location = new System.Drawing.Point(57, 17);
            this.txbSaleAmount.Name = "txbSaleAmount";
            this.txbSaleAmount.Size = new System.Drawing.Size(72, 20);
            this.txbSaleAmount.TabIndex = 4;
            this.txbSaleAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txbSaleAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txbSaleAmount_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Сумма:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.groupBox5);
            this.groupBox3.Controls.Add(this.groupBox4);
            this.groupBox3.Location = new System.Drawing.Point(155, 13);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(292, 246);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Дополнительные функции";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.cbxRepSel);
            this.groupBox5.Controls.Add(this.btnReceiptCopy);
            this.groupBox5.Controls.Add(this.label6);
            this.groupBox5.Controls.Add(this.txbNr);
            this.groupBox5.Controls.Add(this.btnZRepCopy);
            this.groupBox5.Controls.Add(this.btnReportbyDate);
            this.groupBox5.Controls.Add(this.label5);
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Controls.Add(this.dateTimePickerTo);
            this.groupBox5.Controls.Add(this.dateTimePickerFrom);
            this.groupBox5.Controls.Add(this.btnZRep);
            this.groupBox5.Controls.Add(this.btnXRep);
            this.groupBox5.Location = new System.Drawing.Point(7, 109);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(279, 130);
            this.groupBox5.TabIndex = 1;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Отчеты";
            // 
            // cbxRepSel
            // 
            this.cbxRepSel.Enabled = false;
            this.cbxRepSel.FormattingEnabled = true;
            this.cbxRepSel.Items.AddRange(new object[] {
            "Сокращенный",
            "Полный"});
            this.cbxRepSel.Location = new System.Drawing.Point(105, 75);
            this.cbxRepSel.Name = "cbxRepSel";
            this.cbxRepSel.Size = new System.Drawing.Size(76, 21);
            this.cbxRepSel.TabIndex = 11;
            // 
            // btnReceiptCopy
            // 
            this.btnReceiptCopy.Enabled = false;
            this.btnReceiptCopy.Location = new System.Drawing.Point(7, 104);
            this.btnReceiptCopy.Name = "btnReceiptCopy";
            this.btnReceiptCopy.Size = new System.Drawing.Size(85, 23);
            this.btnReceiptCopy.TabIndex = 10;
            this.btnReceiptCopy.Text = "Копия док.";
            this.btnReceiptCopy.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(105, 109);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(21, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "№:";
            // 
            // txbNr
            // 
            this.txbNr.Enabled = false;
            this.txbNr.Location = new System.Drawing.Point(131, 106);
            this.txbNr.Name = "txbNr";
            this.txbNr.Size = new System.Drawing.Size(46, 20);
            this.txbNr.TabIndex = 5;
            this.txbNr.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnZRepCopy
            // 
            this.btnZRepCopy.Enabled = false;
            this.btnZRepCopy.Location = new System.Drawing.Point(183, 104);
            this.btnZRepCopy.Name = "btnZRepCopy";
            this.btnZRepCopy.Size = new System.Drawing.Size(85, 23);
            this.btnZRepCopy.TabIndex = 7;
            this.btnZRepCopy.Text = "Копия Z";
            this.btnZRepCopy.UseVisualStyleBackColor = true;
            // 
            // btnReportbyDate
            // 
            this.btnReportbyDate.Enabled = false;
            this.btnReportbyDate.Location = new System.Drawing.Point(183, 75);
            this.btnReportbyDate.Name = "btnReportbyDate";
            this.btnReportbyDate.Size = new System.Drawing.Size(85, 23);
            this.btnReportbyDate.TabIndex = 6;
            this.btnReportbyDate.Text = "Отчет";
            this.btnReportbyDate.UseVisualStyleBackColor = true;
            this.btnReportbyDate.Click += new System.EventHandler(this.btnReportbyDate_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(101, 50);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(25, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "До:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(109, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "С:";
            // 
            // dateTimePickerTo
            // 
            this.dateTimePickerTo.Enabled = false;
            this.dateTimePickerTo.Location = new System.Drawing.Point(132, 48);
            this.dateTimePickerTo.Name = "dateTimePickerTo";
            this.dateTimePickerTo.Size = new System.Drawing.Size(136, 20);
            this.dateTimePickerTo.TabIndex = 3;
            // 
            // dateTimePickerFrom
            // 
            this.dateTimePickerFrom.Enabled = false;
            this.dateTimePickerFrom.Location = new System.Drawing.Point(132, 19);
            this.dateTimePickerFrom.Name = "dateTimePickerFrom";
            this.dateTimePickerFrom.Size = new System.Drawing.Size(136, 20);
            this.dateTimePickerFrom.TabIndex = 2;
            // 
            // btnZRep
            // 
            this.btnZRep.Location = new System.Drawing.Point(7, 49);
            this.btnZRep.Name = "btnZRep";
            this.btnZRep.Size = new System.Drawing.Size(85, 23);
            this.btnZRep.TabIndex = 1;
            this.btnZRep.Text = "Z-Отчет";
            this.btnZRep.UseVisualStyleBackColor = true;
            this.btnZRep.Click += new System.EventHandler(this.btnZRep_Click);
            // 
            // btnXRep
            // 
            this.btnXRep.Location = new System.Drawing.Point(7, 20);
            this.btnXRep.Name = "btnXRep";
            this.btnXRep.Size = new System.Drawing.Size(85, 23);
            this.btnXRep.TabIndex = 0;
            this.btnXRep.Text = "Х-Отчет";
            this.btnXRep.UseVisualStyleBackColor = true;
            this.btnXRep.Click += new System.EventHandler(this.btnXRep_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txbCashOut);
            this.groupBox4.Controls.Add(this.txbCashIn);
            this.groupBox4.Controls.Add(this.btnCashOut);
            this.groupBox4.Controls.Add(this.btnCashIn);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Location = new System.Drawing.Point(7, 20);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(279, 83);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Внос/Вынос";
            // 
            // txbCashOut
            // 
            this.txbCashOut.Location = new System.Drawing.Point(111, 51);
            this.txbCashOut.Name = "txbCashOut";
            this.txbCashOut.Size = new System.Drawing.Size(66, 20);
            this.txbCashOut.TabIndex = 3;
            this.txbCashOut.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txbCashOut.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txbCashOut_KeyPress);
            // 
            // txbCashIn
            // 
            this.txbCashIn.Location = new System.Drawing.Point(111, 13);
            this.txbCashIn.Name = "txbCashIn";
            this.txbCashIn.Size = new System.Drawing.Size(66, 20);
            this.txbCashIn.TabIndex = 2;
            this.txbCashIn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txbCashIn.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txbCashIn_KeyPress);
            // 
            // btnCashOut
            // 
            this.btnCashOut.Location = new System.Drawing.Point(183, 49);
            this.btnCashOut.Name = "btnCashOut";
            this.btnCashOut.Size = new System.Drawing.Size(85, 23);
            this.btnCashOut.TabIndex = 3;
            this.btnCashOut.Text = "Изъять";
            this.btnCashOut.UseVisualStyleBackColor = true;
            this.btnCashOut.Click += new System.EventHandler(this.btnCashOut_Click);
            // 
            // btnCashIn
            // 
            this.btnCashIn.Location = new System.Drawing.Point(183, 11);
            this.btnCashIn.Name = "btnCashIn";
            this.btnCashIn.Size = new System.Drawing.Size(85, 23);
            this.btnCashIn.TabIndex = 2;
            this.btnCashIn.Text = "Внести";
            this.btnCashIn.UseVisualStyleBackColor = true;
            this.btnCashIn.Click += new System.EventHandler(this.btnCashIn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Сумма изъятия:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Сумма внесения:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(97, 262);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(101, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "Системное время:";
            // 
            // lblSysTimeDate
            // 
            this.lblSysTimeDate.AutoSize = true;
            this.lblSysTimeDate.Location = new System.Drawing.Point(200, 262);
            this.lblSysTimeDate.Name = "lblSysTimeDate";
            this.lblSysTimeDate.Size = new System.Drawing.Size(109, 13);
            this.lblSysTimeDate.TabIndex = 4;
            this.lblSysTimeDate.Text = "00.00.0000  00:00:00";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // ServiceForm_APM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(459, 280);
            this.Controls.Add(this.lblSysTimeDate);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.gbxStatus);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ServiceForm_APM";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Дополнительные операции";
            this.gbxStatus.ResumeLayout(false);
            this.gbxStatus.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbxStatus;
        private System.Windows.Forms.CheckBox cbxOutOfPaper;
        private System.Windows.Forms.CheckBox cbxBlocked;
        private System.Windows.Forms.CheckBox cbxShiftOpened;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnSale;
        private System.Windows.Forms.CheckBox cbxIsReturn;
        private System.Windows.Forms.CheckBox cbxIsCashless;
        private System.Windows.Forms.TextBox txbSaleAmount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btnReceiptCopy;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txbNr;
        private System.Windows.Forms.Button btnZRepCopy;
        private System.Windows.Forms.Button btnReportbyDate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dateTimePickerTo;
        private System.Windows.Forms.DateTimePicker dateTimePickerFrom;
        private System.Windows.Forms.Button btnZRep;
        private System.Windows.Forms.Button btnXRep;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox txbCashOut;
        private System.Windows.Forms.TextBox txbCashIn;
        private System.Windows.Forms.Button btnCashOut;
        private System.Windows.Forms.Button btnCashIn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblSysTimeDate;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ComboBox cbxRepSel;
    }
}