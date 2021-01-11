using System;
using System.Globalization;
using SkiData.FiscalDevices;
using System.Windows.Forms;
using System.Diagnostics;
using System.Timers;
using System.Collections.Generic;
using System.IO;

namespace KZ_ShtrikhM_FiscalDevice
{
    public class APM_FiscalDevice : IFiscalDevice, ICash
    {
        #region Fields
        private FiscalDeviceCapabilities fiscalDeviceCapabilities;
        public static System.Timers.Timer reCheckTimer = new System.Timers.Timer(60000);
        public static System.Timers.Timer timeCheckTimer = new System.Timers.Timer(60000);
        public static System.Timers.Timer paymentTimeoutTimer = new System.Timers.Timer(285000);
        public bool inTransaction = false;
        public bool transactionTimeOutExceed = false;
        private bool disposed;
        //public bool isReady = true;
        private bool countryControll = false;
        public StateInfo deviceState;
        public static bool shiftNotClosed = false;
        private ServiceForm_APM sf = null;
        TransactionData transaction;
        public string paymentMachineName = "";
        public string paymentMachineId = "";
        public static string MachineID = "";
        public string connectionString = "";
        public DeviceType paymentMachineType;
        List<Item> items;  //  Temp container for Item. Try to print two receipts on one list.
        //Item item;
        Payment payment; // Temp container for Payment. Try to print two receipts on one list.
        public FiscalDeviceConfiguration fiscalDeviceConfiguration;
        ShtrikhTransport printer;
        FPResult fpResult = new FPResult();
        #endregion

        #region Construction
        public APM_FiscalDevice()
        {
            OnTrace(new TraceEventArgs("Constructor called...", TraceLevel.Info));
            printer = new ShtrikhTransport();
            this.fiscalDeviceCapabilities = new FiscalDeviceCapabilities(true, false, true);
        }
        #endregion

        #region IFiscalEvents
        public event EventHandler<ErrorOccurredEventArgs> ErrorOccurred;
        public event EventHandler<ErrorClearedEventArgs> ErrorCleared;
        public event EventHandler<IrregularityDetectedEventArgs> IrregularityDetected;
        public event EventHandler<EventArgs> DeviceStateChanged;
        public event EventHandler<JournalizeEventArgs> Journalize;
        public event EventHandler<TraceEventArgs> Trace;
        public void OnTrace(TraceEventArgs args)
        {
            if (Trace != null)
                Trace(this, args);
        }

        public void OnDeviceStateChanged(EventArgs args)
        {
            if (DeviceStateChanged != null)
                DeviceStateChanged(this, args);
        }

        public void OnErrorOccurred(ErrorOccurredEventArgs args)
        {
            OnTrace(new TraceEventArgs($"FD  : Error {args.ErrorMessage} occurred", TraceLevel.Error));
            deviceState = new StateInfo(args.FiscalDeviceReady, args.ErrorCode, args.ErrorMessage);
            if (ErrorOccurred != null)
                ErrorOccurred(this, args);
        }

        public void OnErrorCleared(ErrorClearedEventArgs args)
        {
            OnTrace(new TraceEventArgs($"FD  : Error {deviceState.ErrorCode} cleared", TraceLevel.Error));
            deviceState = new StateInfo(args.FiscalDeviceReady, args.ErrorCode, "Ok");
            if (ErrorCleared != null)
                ErrorCleared(this, args);
        }

        public void OnIrregularityDetected(IrregularityDetectedEventArgs args)
        {
            if (IrregularityDetected != null)
                IrregularityDetected(this, args);
        }

        public void OnJournalize(JournalizeEventArgs args)
        {
            if (Journalize != null)
                Journalize(this, args);
        }
        #endregion

        #region IFiscalDevice Members
        public string Name => "KZ_ShtrikhM";

        public string ShortName => "KZ_ShtrikhM";

        public FiscalDeviceCapabilities Capabilities => this.fiscalDeviceCapabilities;

        public StateInfo DeviceState => this.deviceState;

        public Result Install(FiscalDeviceConfiguration configuration)
        {
            
            MachineID = configuration.DeviceId;
            CreateFolders();
            Logger log = new Logger("Fiscal Interface", MachineID);
            log.Write($"FD  : Install: {configuration.CommunicationChannel}");
            inTransaction = true;
            fiscalDeviceConfiguration = configuration;
            #region Check currency
            log.Write(configuration.Currency);
            deviceState = new StateInfo(true, SkiDataErrorCode.Ok, "Новое подключение");
            if (configuration.Currency == "KZT")
                countryControll = true;
            else
            {
                countryControll = false;
            }
            #endregion
            if (countryControll)
            {
                paymentMachineName = configuration.DeviceName;
                connectionString = configuration.CommunicationChannel;
                printer = new ShtrikhTransport();
                try
                {
                    
                    int err = printer.Connect(connectionString);
                    if(err==0)
                        this.StatusChangedEvent(true, (int)deviceState.ErrorCode, "РРО подлючен");
                    else
                        ErrorAnalizer(err);
                    System.Threading.Thread.Sleep(2000);  //TODO Remove after bug fixing
                    if (deviceState.FiscalDeviceReady)
                    {
                        fpResult.status = FPResult.SetStatus(printer.GetStatus());
                        if (fpResult.status.blocked)
                            this.StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, "РРО заблокирован");
                        if (fpResult.status.blocked24Hour)
                            this.StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, "Смена дольше 24 часов");
                        if (fpResult.status.outOfPaper)
                            this.StatusChangedEvent(false, (int)SkiDataErrorCode.OutOfPaper, "Закончилась бумага");
                    }
                    log.Write($"FD  : Install Printer Status: Blocked: {fpResult.status.blocked}, Shift Opened: {fpResult.status.shiftOpened}, Shift blocked due to 24 Hours: {fpResult.status.blocked24Hour}");
                }
                catch (Exception e)
                {
                    this.StatusChangedEvent(false, (int)SkiDataErrorCode.CommunicationError, $"Исключение: {e.Message}");
                    log.Write($"FD  :Install exception: {e.Message}");
                    inTransaction = false;
                }
            }
            else
            {
                deviceState = new StateInfo(false, SkiDataErrorCode.ConfigurationError, "Неправильный регион");
                this.StatusChangedEvent(false, (int)SkiDataErrorCode.ConfigurationError, "Dll built for Kazakhistan region. Contact provider");
                log.Write("FD  : Dll built for Kazakhistan region. Contact provider");
            }
            log.Write($"FD  : Install. Result: {deviceState.FiscalDeviceReady}");
            inTransaction = false;
            return new Result(deviceState.FiscalDeviceReady);
        }

        public void SetDisplayLanguage(CultureInfo cultureInfo)
        { }

        public void Notify(int notificationId)
        {
            switch (notificationId)
            {
                case 1:
                    if (this.sf != null)
                    {
                        this.sf.Close();
                    }
                    break;
                default:
                    break;
            }
        }

        public Result OpenTransaction(TransactionData transactionData)
        {
            items = new List<Item>();
            Logger log = new Logger("Fiscal Interface", MachineID);
            inTransaction = true;
            transaction = transactionData;
            log.Write($"FD  : OpenTransaction Transaction Nr: {transaction.ReferenceId}");
            fpResult.status = FPResult.SetStatus(printer.GetStatus());
            if (fpResult.status.blocked)
                this.StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, "РРО заблокирован");
            if (fpResult.status.blocked24Hour)
                this.StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, "Смена дольше 24 часов");
            if (fpResult.status.outOfPaper)
                this.StatusChangedEvent(false, (int)SkiDataErrorCode.OutOfPaper, "Закончилась бумага");
            return new Result(deviceState.FiscalDeviceReady);
        }

        public Result AddItem(Item item)
        {
            if (items == null)
                items = new List<Item>();
            items.Add(item);
            Logger log = new Logger("Fiscal Interface", MachineID);
            log.Write($"FD  : Add Item totalPrice: {item.TotalPrice}, quantity {item.Quantity}, name {item.Name}");
            return new Result(deviceState.FiscalDeviceReady);
        }

        public Result AddPayment(Payment payment)
        {
            this.payment = payment;
            Logger log = new Logger("Fiscal Interface", MachineID);
            log.Write($"FD  : Add Payment amount: {payment.Amount}, type: {payment.PaymentType}, name: {payment.Name}");
            return new Result(deviceState.FiscalDeviceReady);
        }

        public Result CloseTransaction()
        {
            try
            {
                Logger log = new Logger("Fiscal Interface", MachineID);
                {       
                    bool doSale = true;
                    foreach (Item item in items)
                    {
                        if (item.TotalPrice < 0)
                            doSale = doSale & false;
                    }
                    if (doSale)
                    {
                        if (deviceState.FiscalDeviceReady)
                        {
                            foreach (Item item in items)
                            {
                                ParkingItem parkingItem = item as ParkingItem;
                                fpResult = printer.Sale((int)item.TotalPrice * 100, (int)item.Quantity, item.Name, 1, 0);
                                if (fpResult.Error != 0)
                                    ErrorAnalizer(fpResult.Error);
                                FPResult.Errors.TryGetValue(fpResult.Error, out string errorMess);
                                fpResult.ErrorMessage = errorMess;
                                log.Write($"FD  : Sale: {item.TotalPrice}, {item.Quantity}, {item.Name}. Result: {fpResult.ErrorMessage}");
                                if (!deviceState.FiscalDeviceReady)
                                {
                                    int err = fpResult.Error;
                                    System.Threading.Thread.Sleep(2000);
                                    printer.VoidReceipt();
                                    System.Threading.Thread.Sleep(2000);
                                    printer.Cut();
                                    fpResult = new FPResult(err);
                                }
                                else
                                {
                                    if(payment.PaymentType != PaymentType.Cash)
                                    {
                                        SQLConnect sql = new SQLConnect();
                                        string[] lines = sql.GetTransactionFromDBbyDevice(paymentMachineId, transaction.ReferenceId).Split('\n');
                                        if (lines.Length > 0)
                                        {
                                            foreach (string line in lines)
                                            {
                                                fpResult = printer.PrintLine(line);
                                            }
                                        }
                                        else
                                        {
                                            fpResult = printer.PrintLine("Банковский чек не может быть распечатан");
                                        }
                                    }
                                    if (parkingItem != null)
                                    {
                                        fpResult = printer.PrintLine($"Номер талона: {parkingItem.TicketId}");
                                        if (fpResult.Error == 0)
                                            fpResult = printer.PrintLine($"Место въезда: {parkingItem.EntryDeviceName}");
                                        if (fpResult.Error == 0)
                                            fpResult = printer.PrintLine($"Время въезда: {parkingItem.EntryTime.ToString(@"dd.MM.yy HH:mm:ss")}");
                                        if (fpResult.Error == 0)
                                            fpResult = printer.PrintLine($" Оплачено до: {parkingItem.PaidUntil.ToString(@"dd.MM.yy HH:mm:ss")}");
                                        if (fpResult.Error != 0)
                                        {
                                            ErrorAnalizer(fpResult.Error);
                                            if (!deviceState.FiscalDeviceReady)
                                            {
                                                int err = fpResult.Error;
                                                printer.VoidReceipt();
                                                printer.Cut();
                                                fpResult = new FPResult(err);
                                                FPResult.Errors.TryGetValue(fpResult.Error, out errorMess);
                                                fpResult.ErrorMessage = errorMess;
                                                log.Write($"FD  : Print addons with error: {errorMess}");
                                            }
                                        }
                                    }
                                }
                            }
                            if (deviceState.FiscalDeviceReady)
                            {
                                fpResult = printer.CloseReceipt((int)payment.Amount * 100, (payment.PaymentType != PaymentType.Cash ? 3 : 0), 0, payment.Name);
                                if (fpResult.Error != 0)
                                    ErrorAnalizer(fpResult.Error);
                                FPResult.Errors.TryGetValue(fpResult.Error, out string errorMess);
                                fpResult.ErrorMessage = errorMess;
                                log.Write($"FD  : Payment: {payment.Amount}, {payment.PaymentType.ToString()}, {payment.Name}. Result: {fpResult.ErrorMessage}");
                                if (!deviceState.FiscalDeviceReady)
                                {
                                    int err = fpResult.Error;
                                    printer.VoidReceipt();
                                    printer.Cut();
                                    fpResult = new FPResult(err);
                                    FPResult.Errors.TryGetValue(fpResult.Error, out errorMess);
                                    fpResult.ErrorMessage = errorMess;
                                    log.Write($"FD  : Payment with error: {errorMess}");
                                }
                            }
                        }
                    }
                    else
                    {
                        return new Result(false);
                    }
                }
            }
            catch (Exception e)
            {
                this.StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, $"Исключение: {e.Message}");
                printer.VoidReceipt();
                Logger log = new Logger("Fiscal Interface", MachineID);
                log.Write($"FD  : Close Transaction exception: {e.Message}");
            }
            return new Result(deviceState.FiscalDeviceReady);
        }

        public Result AddDiscount(Discount discount) => new Result(true);

        public Result VoidTransaction() => new Result(true);

        public void StartServiceDialog(IntPtr windowHandle, ServiceLevel serviceLevel)
        {
            try
            {
                if (this.sf == null)
                {
                    this.sf = new ServiceForm_APM(printer, MachineID, fpResult.status);
                    this.sf.StatusChangedEvent += StatusChangedEvent;
                    this.sf.ShowDialog();
                    this.sf = null;
                }
            }
            catch (Exception)
            {
            }
        }

        public Result EndOfDay() => new Result(true);

        #region Destructors
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    printer.Close();
                }
            }
            this.disposed = true;
        }

        ~APM_FiscalDevice()
        {
            this.Dispose(false);
        }
        #endregion

        #endregion

        #region ICash Members
        public Result CashIn(Cash cash)
        {
            if (cash.Amount != 0)
            {
                try
                {
                    Logger log = new Logger("Fiscal Interface", MachineID);
                    fpResult = printer.PrintLine($"Платежная станция: {paymentMachineName}");
                    fpResult = printer.PrintLine($"Источник: {cash.Source}");
                    fpResult = printer.CashIn((int)cash.Amount * 100);
                    if(fpResult.Error != 0)
                        ErrorAnalizer(fpResult.Error);
                    FPResult.Errors.TryGetValue(fpResult.Error, out string errorMess);
                    fpResult.ErrorMessage = errorMess;
                    log.Write($"FD  : CashIn from {cash.Source} with amount {cash.Amount}. Result: {fpResult.ErrorMessage}");
                }
                catch (Exception e)
                {
                    this.StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, $"Исключение: {e.Message}");
                    Logger log = new Logger("Fiscal Interface", MachineID);
                    log.Write($"FD  : CashIn exception: {e.Message}");
                }
            }
            return new Result(deviceState.FiscalDeviceReady);
        }

        public Result CashOut(Cash cash)
        {
            if (cash.Amount != 0)
            {
                try
                {
                    Logger log = new Logger("Fiscal Interface", MachineID);
                    fpResult = printer.PrintLine($"Платежная станция: {paymentMachineName}");
                    fpResult = printer.PrintLine($"Источник: {cash.Source}");
                    fpResult = printer.CashOut((int)cash.Amount * 100);
                    if(fpResult.Error != 0)
                        ErrorAnalizer(fpResult.Error);
                    FPResult.Errors.TryGetValue(fpResult.Error, out string errorMess);
                    fpResult.ErrorMessage = errorMess;
                    log.Write($"FD  : CashOut from {cash.Source} with amount {cash.Amount}. Result: {fpResult.ErrorMessage}");
                }
                catch (Exception e)
                {
                    this.StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, $"Исключение: {e.Message}");
                    Logger log = new Logger("Fiscal Interface", MachineID);
                    log.Write($"FD  : CashOut exception: {e.Message}");
                }
            }
            return new Result(deviceState.FiscalDeviceReady);
        }
        #endregion

        #region Custom methods

        bool CreateFolders()
        {
            if (!Directory.Exists(StringValue.WorkingDirectory))
            {
                Directory.CreateDirectory(StringValue.WorkingDirectory);
            }
            if (!Directory.Exists($"{StringValue.WorkingDirectory}Log"))
            {
                Directory.CreateDirectory($"{StringValue.WorkingDirectory}Log");
            }
            if (!Directory.Exists($"{StringValue.WorkingDirectory}FDI"))
            {
                Directory.CreateDirectory($"{StringValue.WorkingDirectory}FDI");
            }
            return true;
        }

        private void StatusChangedEvent(bool isready, int errorCode, string errorMessage)
        {
            if (isready)
            {
                if (!deviceState.FiscalDeviceReady)
                    OnErrorCleared(new ErrorClearedEventArgs(deviceState.ErrorCode, true));
            }
            else
            {
                if (deviceState.FiscalDeviceReady)
                    OnErrorOccurred(new ErrorOccurredEventArgs(errorMessage, (SkiDataErrorCode)errorCode, false));
                else
                {
                    OnErrorCleared(new ErrorClearedEventArgs(deviceState.ErrorCode, false));
                    OnErrorOccurred(new ErrorOccurredEventArgs(errorMessage, (SkiDataErrorCode)errorCode, false));
                }
            }
        }

        public bool ErrorAnalizer(int error)
        {
            Logger log = new Logger("Fiscal Interface", MachineID);
            log.Write($"FDEA: ErrorAnalizer({error.ToString()})");
            if (!FPResult.Errors.TryGetValue(error, out string errMess))
                errMess = "NotDefined";
            switch (error)
            {
                case 1:
                    this.StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, errMess);
                    return false;
                case 2:
                    this.StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, errMess);
                    return false;
                case 3:
                    this.StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, errMess);
                    return false;
                case 22:
                    this.StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, errMess);
                    return false;
                case 31:
                    this.StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, errMess);
                    return false;
                case 49:
                    this.StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, errMess);
                    return false;
                case 50:
                    this.StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, errMess);
                    log.Write($"FDEA: Error analizer: {errMess}");
                    return false;
                case 53:
                    this.StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, errMess);
                    log.Write($"FDEA: Error analizer: {errMess}");
                    return false;
                case 56:
                    this.StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, errMess);
                    return false;
                case 57:
                    this.StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, errMess);
                    log.Write($"FDEA: Error analizer: {errMess}");
                    return false;
                case 58:
                    this.StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, errMess);
                    log.Write($"FDEA: Error analizer: {errMess}");
                    return false;
                case 59:
                    this.StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, errMess);
                    log.Write($"FDEA: Error analizer: {errMess}");
                    return false;
                case 74:
                    this.StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, errMess);
                    log.Write($"FDEA: Error analizer: {errMess}");
                    return false;
                case 78:  // Shift longer then 24 hours
                    this.StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, errMess);
                    return false;
                case 79:
                    this.StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, errMess);
                    log.Write($"FDEA: Error analizer: {errMess}");
                    return false;
                case 92:
                    this.StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, errMess);
                    log.Write($"FDEA: Error analizer: {errMess}");
                    return false;
                case 100:
                    this.StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, errMess);
                    return false;
                case 103:
                    this.StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, errMess);
                    return false;
                case 106:
                    this.StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, errMess);
                    log.Write($"FDEA: Error analizer: {errMess}");
                    return false;
                case 107:
                    this.StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, errMess);
                    log.Write($"FDEA: Error analizer: {errMess}");
                    return false;
                case 112:
                    this.StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, errMess);
                    log.Write($"FDEA: Error analizer: {errMess}");
                    return false;
                case 113:
                    this.StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, errMess);
                    return false;
                case 116:
                    this.StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, errMess);
                    log.Write($"FDEA: Error analizer: {errMess}");
                    return false;
                case 117:
                    this.StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, errMess);
                    return false;
                case 118:
                    this.StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, errMess);
                    return false;
                case 119:
                    this.StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, errMess);
                    return false;
                case 123:
                    this.StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, errMess);
                    log.Write($"FDEA: Error analizer: {errMess}");
                    return false;
                case 128:
                    this.StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, errMess);
                    return false;
                case 129:
                    this.StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, errMess);
                    return false;
                case 130:
                    this.StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, errMess);
                    return false;
                case 131:
                    this.StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, errMess);
                    return false;
                case 160:
                    this.StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, errMess);
                    return false;
                case 161:
                    this.StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, errMess);
                    return false;
                case 163:
                    this.StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, errMess);
                    log.Write($"FDEA: Error analizer: {errMess}");
                    return false;
                case 164:
                    this.StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, errMess);
                    log.Write($"FDEA: Error analizer: {errMess}");
                    return false;
                case 166:
                    this.StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, errMess);
                    log.Write($"FDEA: Error analizer: {errMess}");
                    return false;
                case 201:
                    this.StatusChangedEvent(false, (int)SkiDataErrorCode.CommunicationError, errMess);
                    return false;
                case 202:
                    this.StatusChangedEvent(false, (int)SkiDataErrorCode.CommunicationError, errMess);
                    return false;
                case 203:
                    this.StatusChangedEvent(false, (int)SkiDataErrorCode.CommunicationError, errMess);
                    return false;
                default:
                    log.Write($"FDEA: Error analizer: {errMess}");
                    OnTrace(new TraceEventArgs(errMess, System.Diagnostics.TraceLevel.Error));
                    return true;
            }
        }
        #endregion
    }
}
