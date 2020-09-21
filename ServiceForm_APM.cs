using SkiData.FiscalDevices;
using System;
using System.Windows.Forms;

namespace KZ_ShtrikhM_FiscalDevice
{
    public partial class ServiceForm_APM : Form
    {
        private ShtrikhTransport printer;
        public delegate void StatusChanged(bool isready, int errorCode, string errorMessage);
        public event StatusChanged StatusChangedEvent;

        private void OnStatusChangedEvent(bool isready, int errorCode, string errorMessage)
        {
            if (this.StatusChangedEvent != null)
                this.StatusChangedEvent(isready, errorCode, errorMessage);
        }
        ConfirmationWindow cw;
        string machineID;
        FPResult.Status status;

        public ServiceForm_APM(ShtrikhTransport printer,string machineID, FPResult.Status status)
        {
            InitializeComponent();
            this.printer = printer;
            this.machineID = machineID;
            this.status = status;
            dateTimePickerTo.Value = DateTime.Today.AddDays(-1);
            dateTimePickerFrom.Value = new DateTime(dateTimePickerTo.Value.Year, dateTimePickerTo.Value.Month, 1);
            lblSysTimeDate.Text = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
            cbxRepSel.SelectedIndex = 0;
            UpdateStatusBoxes();
        }

        private void btnXRep_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            FPResult fpResult = printer.XReport();
            if (fpResult.Error == 0)
                OnStatusChangedEvent(true, 0, "Ok");
            Logger log = new Logger(machineID);
            FPResult.Errors.TryGetValue(fpResult.Error, out string errorMess);
            log.Write($"FDSF: Print X-report. Result: {errorMess}");
            UpdateStatusBoxes();
            this.Enabled = true;
        }

        private void btnZRep_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            FPResult fpResult = printer.ZReport();
            if (fpResult.Error == 0)
                OnStatusChangedEvent(true, 0, "Ok");
            Logger log = new Logger(machineID);
            FPResult.Errors.TryGetValue(fpResult.Error, out string errorMess);
            log.Write($"FDSF: Print Z-report. Result: {errorMess}");
            UpdateStatusBoxes();
            this.Enabled = true;
        }

        private void btnCashIn_Click(object sender, EventArgs e)
        {
            if (txbCashIn.Text != "")
            {
                if (this.cw == null)
                {
                    int.TryParse(txbCashIn.Text, out int sum);
                    if (sum != 0)
                    {
                        this.cw = new ConfirmationWindow(0, sum);
                        this.cw.NumReadyEvent += cw_NumReadyEvent;
                        this.cw.ShowDialog();
                        this.cw = null;
                        txbCashIn.Text = "";
                    }
                }
            }
        }

        private void btnCashOut_Click(object sender, EventArgs e)
        {
            if (txbCashOut.Text != "")
            {
                if (this.cw == null)
                {
                    int.TryParse(txbCashOut.Text, out int sum);
                    if (sum != 0)
                    {
                        this.cw = new ConfirmationWindow(1, sum);
                        this.cw.NumReadyEvent += cw_NumReadyEvent;
                        this.cw.ShowDialog();
                        this.cw = null;
                        txbCashOut.Text = "";
                    }
                }
            }
        }

        private void cw_NumReadyEvent(byte type, bool isOk)
        {
            this.Enabled = false;
            if (isOk)
            {
                if (type == 0)
                {
                    this.Enabled = false;
                    try
                    {
                        int.TryParse(txbCashIn.Text, out int sum);
                        FPResult fpResult = printer.CashIn(sum * 100);
                        Logger log = new Logger(machineID);
                        FPResult.Errors.TryGetValue(fpResult.Error, out string errorMess);
                        log.Write($"FDSF: CashIn amount {sum}. Result: {errorMess}");
                    }
                    catch (Exception ex)
                    {
                        this.Enabled = true;
                        MessageBox.Show(new Form { TopMost = true }, "Ошибка!!!\n" + ex.Message);
                    }
                    this.Enabled = true;
                }
                if (type == 1)
                {
                    this.Enabled = false;
                    try
                    {
                        int.TryParse(txbCashOut.Text, out int sum);
                        FPResult fpResult = printer.CashOut(sum * 100);
                        Logger log = new Logger(machineID);
                        FPResult.Errors.TryGetValue(fpResult.Error, out string errorMess);
                        log.Write($"FDSF: CashOut amount {sum}. Result: {errorMess}");
                    }
                    catch (Exception ex)
                    {
                        this.Enabled = true;
                        MessageBox.Show(new Form { TopMost = true }, "Ошибка!!!\n" + ex.Message);
                    }
                    this.Enabled = true;
                }
            }
            UpdateStatusBoxes();
            this.Enabled = true;
        }

        private void txbCashIn_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && (e.KeyChar != 8) && (e.KeyChar != 44) && (e.KeyChar != 46);
            if (e.KeyChar == 13)
            {
                btnCashIn_Click(sender, null);
            }
        }

        private void txbCashOut_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && (e.KeyChar != 8) && (e.KeyChar != 44) && (e.KeyChar != 46);
            if (e.KeyChar == 13)
            {
                btnCashOut_Click(sender, null);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblSysTimeDate.Text = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
        }

        private void btnSale_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            FPResult fpResult = new FPResult();
            int.TryParse(txbSaleAmount.Text, out int sum);
            int type = cbxIsCashless.Checked ? 3 : 0;
            if (sum != 0)
            {
                if (!cbxIsReturn.Checked)
                {
                    fpResult = printer.Sale(sum * 100, 1, "Услуги парковки", 1, 0);
                    if (fpResult.Error == 0)
                    {
                        fpResult = printer.CloseReceipt(sum * 100, type, 0, "");
                        if (fpResult.Error != 0)
                            printer.VoidReceipt();

                    }
                    else
                        printer.VoidReceipt();
                    Logger log = new Logger(machineID);
                    FPResult.Errors.TryGetValue(fpResult.Error, out string errorMess);
                    log.Write($"FDSF: Sale amount: {sum}, type: {type}. Result: {errorMess}");
                }
                else
                {
                    fpResult = printer.Return(sum * 100, 1, "Услуги парковки", 1, 0);
                    if (fpResult.Error == 0)
                    {
                        fpResult = printer.CloseReceipt(sum * 100, type, 0, "");
                        if (fpResult.Error != 0)
                            printer.VoidReceipt();

                    }
                    else
                        printer.VoidReceipt();
                    Logger log = new Logger(machineID);
                    FPResult.Errors.TryGetValue(fpResult.Error, out string errorMess);
                    log.Write($"FDSF: Return amount: {sum}, type: {type}. Result: {errorMess}");
                }
            }
            UpdateStatusBoxes();
            this.Enabled = true;
        }

        private void txbSaleAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && (e.KeyChar != 8) && (e.KeyChar != 44) && (e.KeyChar != 46);
            if (e.KeyChar == 13)
            {
                btnSale_Click(sender, null);
            }
        }

        private void btnReportbyDate_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            FPResult fpResult = printer.Report(cbxRepSel.SelectedIndex, dateTimePickerFrom.Value.Date, dateTimePickerTo.Value.Date);
            Logger log = new Logger(machineID);
            FPResult.Errors.TryGetValue(fpResult.Error, out string errorMess);
            log.Write($"FDSF: Report by date from {dateTimePickerFrom.Value.Date.ToString("dd.MM.yyyy")} to {dateTimePickerTo.Value.Date.ToString("dd.MM.yyyy")} type: {cbxRepSel.SelectedItem}. Result: {errorMess}");
            this.Enabled = true;
        }

        private void UpdateStatusBoxes()
        {
            System.Threading.Thread.Sleep(1000); //TODO remove it or change after bugtracing
            status = FPResult.SetStatus(printer.GetStatus());
            this.cbxBlocked.Checked = status.blocked;
            this.cbxOutOfPaper.Checked = status.outOfPaper;
            this.cbxShiftOpened.Checked = status.shiftOpened;
        }
    }
}
