using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace KZ_ShtrikhM_FiscalDevice
{
    public partial class ConfirmationWindow : Form
    {
        public delegate void NumReady(byte type, bool isOk);
        public event NumReady NumReadyEvent;
        int summ, prevSumm;
        byte cashFlowType;

        public void OnNumReady(byte type, bool isOk)
        {
            if (this.NumReadyEvent != null)
                this.NumReadyEvent(type, isOk);
        }
        
        public ConfirmationWindow(byte type, int prevSum)
        {
            InitializeComponent();
            this.prevSumm = prevSum;
            cashFlowType = type;
        }

        private void txbCashConfirm_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && (e.KeyChar != 8) && (e.KeyChar != 44) && ((e.KeyChar != 46));
            if (e.KeyChar == 13)
            {
                btnDone_Click(sender, null);
            }
            if (e.KeyChar == 27)
            {
                btnCancel_Click(sender, null);
            }
        }

        private void txbCashConfirm_TextChanged(object sender, EventArgs e)
        {
            int.TryParse(txbCashConfirm.Text, out summ);
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            int.TryParse(txbCashConfirm.Text, out summ);
            if (prevSumm == summ)
                OnNumReady(cashFlowType, true);
            else
                NumReadyEvent(cashFlowType,false);
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
