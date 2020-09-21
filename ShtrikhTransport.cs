using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;
//using System.Threading.Tasks;
using System.Timers;

namespace KZ_ShtrikhM_FiscalDevice
{
    public class ShtrikhTransport
    {
        #region Fields
        readonly byte stx = 0x02;
        readonly byte ask = 0x06;
        readonly byte nak = 0x15;
        readonly byte enq = 0x05;
        readonly List<byte> pass = new List<byte>() { 0x1E, 0x00, 0x00, 0x00 };
        private bool dataOnPort = false;
        public FPResult.Status PrinterStatus { get; set; }
        private enum CommandType : int
        {
            Default = 0, Init = 1, Response = 2, Cmd = 3
        }
        SerialPort port;
        SendData sendData;
        //Task<FPResult> task;

        System.Timers.Timer timer1;
        static bool timeOut = false;
        private const int timerTime = 10000;
        public static string machineID = "notInitialized";
        //private const int timerAskTime = 10000;
        #endregion

        class SendData
        {
            public byte Command { get; set; }
            public List<byte> Parameter { get; set; }
        }

        #region Public Members

        public int Connect(string portName)
        {
            try
            {
                sendData = new SendData();
                port = new SerialPort();
                port.PortName = portName;
                port.BaudRate = 9600;
                port.Parity = Parity.None;
                port.DataBits = 8;
                port.StopBits = StopBits.One;
                port.Handshake = Handshake.None;
                port.RtsEnable = true;
                port.DtrEnable = true;
                port.Encoding = Encoding.Unicode;;
                if (!(port.IsOpen))
                {
                    port.Open();
                }
                return 0;
            }
            catch
            {
                return 200;
            }

        }

        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            dataOnPort = true;
        }

        public FPResult XReport()
        {
            sendData.Command = 0x40;
            sendData.Parameter = pass;
            //task = new Task<FPResult>(() => Sender(sendData));
            //task.Start();
            //task.Wait();
            //return task.Result;
            FPResult res = Sender(sendData);
            if (res.Error == 204)
            {
                res = Sender(sendData);
            }
            return res;
        }

        public FPResult ShiftOpen()
        {
            sendData.Command = 0xE0;
            sendData.Parameter = pass;
            FPResult res = Sender(sendData);
            if (res.Error == 204)
            {
                res = Sender(sendData);
            }
            return res;
        }

        public FPResult Sale(int price, int quantity, string name, int department, int tax)
        {
            sendData.Command = 0x80;
            List<byte> param = new List<byte>();
            param.AddRange(pass);
            foreach (byte bt in GetByte((UInt64)quantity * 1000, 5))
            {
                param.Add(bt);
            }
            foreach (byte bt in GetByte((UInt64)price, 5))
            {
                param.Add(bt);
            }
            param.Add((byte)department);
            param.Add((byte)tax);
            param.Add(0x00);
            param.Add(0x00);
            param.Add(0x00);
            param.AddRange(GetByte(name, 40));
            sendData.Parameter = param;
            FPResult res = Sender(sendData);
            if (res.Error == 204)
            {
                res = Sender(sendData);
            }
            return res;
        }

        public FPResult CloseReceipt(int amount, int paymentType, int tax, string text)
        {
            sendData.Command = 0x85;
            List<byte> param = new List<byte>();
            param.AddRange(pass);
            param.AddRange((paymentType == 0) ? (GetByte((UInt64)amount, 5)) : (GetByte(0, 5)));
            param.AddRange((paymentType == 1) ? (GetByte((UInt64)amount, 5)) : (GetByte(0, 5)));
            param.AddRange((paymentType == 2) ? (GetByte((UInt64)amount, 5)) : (GetByte(0, 5)));
            param.AddRange((paymentType == 3) ? (GetByte((UInt64)amount, 5)) : (GetByte(0, 5)));
            param.Add((byte)tax);
            param.Add(0x00);
            param.Add(0x00);
            param.Add(0x00);
            param.AddRange(GetByte("", 40));
            sendData.Parameter = param;
            FPResult res = Sender(sendData);
            if (res.Error == 204)
            {
                res = Sender(sendData);
            }
            return res;
        }

        public FPResult ZReport()
        {
            sendData.Command = 0x41;
            sendData.Parameter = pass;
            FPResult res = Sender(sendData);
            if (res.Error == 204)
            {
                res = Sender(sendData);
            }
            return res;
        }

        public FPResult Report(int type, DateTime beginDate, DateTime endDate)
        {
            sendData.Command = 0x66;
            List<byte> param = new List<byte>();
            param.AddRange(pass);
            param.AddRange(GetByteDate(beginDate));
            param.AddRange(GetByteDate(endDate));
            sendData.Parameter = param;
            FPResult res = Sender(sendData);
            if (res.Error == 204)
            {
                res = Sender(sendData);
            }
            return res;
        }

        public FPResult CashIn(int amount)
        {
            sendData.Command = 0x50;
            List<byte> param = new List<byte>();
            param.AddRange(pass);
            param.AddRange(GetByte((UInt64)amount, 5));
            sendData.Parameter = param;
            FPResult res = Sender(sendData);
            if (res.Error == 204)
            {
                res = Sender(sendData);
            }
            return res;
        }

        public FPResult CashOut(int amount)
        {
            FPResult res;
            try
            {
                sendData.Command = 0x51;
                if (amount < 0)
                    amount = -amount;
                List<byte> param = new List<byte>();
                param.AddRange(pass);
                param.AddRange(GetByte((UInt64)amount, 5));
                sendData.Parameter = param;
                res = Sender(sendData);
                if (res.Error == 204)
                {
                    res = Sender(sendData);
                }
                return res;
            }
            catch 
            {
                res = new FPResult();
                res.Error = 201;
                return res;
            }
        }

        public FPResult OpenDoc()
        {
            sendData.Command = 0xE2;
            sendData.Parameter = pass;
            FPResult res = Sender(sendData);
            if (res.Error == 204)
            {
                res = Sender(sendData);
            }
            return res;
        }

        public FPResult CloseDoc()
        {
            sendData.Command = 0xE3;
            sendData.Parameter = pass;
            FPResult res = Sender(sendData);
            if (res.Error == 204)
            {
                res = Sender(sendData);
            }
            return res;
        }

        public FPResult PrintLine(string line)
        {
            sendData.Command = 0x17;
            List<byte> param = new List<byte>();
            param.AddRange(pass);
            param.Add(0x07);
            param.AddRange(GetByte(line, 40));
            sendData.Parameter = param;
            FPResult res = Sender(sendData);
            if (res.Error == 204)
            {
                res = Sender(sendData);
            }
            return res;
        }

        public FPResult VoidReceipt()
        {
            sendData.Command = 0x88;
            sendData.Parameter = pass;
            FPResult res = Sender(sendData);
            if (res.Error == 204)
            {
                res = Sender(sendData);
            }
            return res;
        }

        public FPResult Return(int price, int quantity, string name, int department, int tax)
        {
            sendData.Command = 0x82;
            List<byte> param = new List<byte>();
            param.AddRange(pass);
            foreach (byte bt in GetByte((UInt64)quantity * 1000, 5))
            {
                param.Add(bt);
            }
            foreach (byte bt in GetByte((UInt64)price, 5))
            {
                param.Add(bt);
            }
            param.Add((byte)department);
            param.Add((byte)tax);
            param.Add(0x00);
            param.Add(0x00);
            param.Add(0x00);
            param.AddRange(GetByte(name, 40));
            sendData.Parameter = param;
            FPResult res = Sender(sendData);
            if (res.Error == 204)
            {
                res = Sender(sendData);
            }
            return res;
        }

        public FPResult SetTime()
        {
            sendData.Command = 0x21;
            List<byte> param = new List<byte>();
            param.AddRange(pass);
            param.AddRange(GetByte(DateTime.Now));
            sendData.Parameter = param;
            FPResult res = Sender(sendData);
            if(res.Error==204)
            {
                res = Sender(sendData);
            }
            return res;
        }

        public FPResult Cut()
        {
            sendData.Command = 0x25;
            List<byte> param = new List<byte>();
            param.AddRange(pass);
            param.Add(0);
            sendData.Parameter = param;
            FPResult res = Sender(sendData);
            if (res.Error == 204)
            {
                res = Sender(sendData);
            }
            return res;
        }

        public int GetStatus()
        {
            sendData.Command = 0x10;
            sendData.Parameter = pass;
            //task = new Task<FPResult>(() => Sender(sendData));
            //task.Start();
            //task.Wait();
            FPResult res = Sender(sendData);
            Logger log = new Logger("Transport", machineID);
            
            if (res.Error == 0)
            {
                log.Write($"Get Status result: {BitConverter.ToString(res.answer.ToArray())}");
                return SetStatus(res);
            }
            else
            {
                //FPResult.Status status = new FPResult.Status();
                //status.blocked = true;
                return 2;
            }
        }

        public FPResult GetStatusExt()
        {
            sendData.Command = 0x11;
            sendData.Parameter = pass;
            //task = new Task<FPResult>(() => Sender(sendData));
            //task.Start();
            //task.Wait();
            //return task.Result;
            return Sender(sendData);
        }

        public int Close()
        {
            if (port != null)
            {
                if (port.IsOpen)
                    port.Close();
            }
            return 0;
        }
        #endregion

        #region Private Members
        private FPResult Sender(object sendData)
        {
            timer1 = new Timer(timerTime);
            timer1.Elapsed += Timer1_Elapsed;
            timer1.AutoReset = false;
            SendData send = (SendData)sendData;
            FPResult fPResult = new FPResult();
            fPResult.Error = 202;
            timeOut = false;
            timer1.Enabled = true;
            byte resInit = 0xFF;
            port.DataReceived += DataReceivedHandler;
            dataOnPort = false;
            try
            {
                resInit = SendInit();
                if (resInit == nak) ///FP is ready for new cmd
                {
                    List<byte> data = new List<byte>();
                    data.Add(send.Command);
                    data.AddRange(send.Parameter);
                    byte len = (byte)data.Count;
                    data.Insert(0, len);
                    data.Add(CRC(data));
                    data.Insert(0, stx);
                    dataOnPort = false;
                    byte resCmd = SendCommand(data);
                    if (resCmd == ask)  ///FP confirmed data
                    {
                        while (port.BytesToRead == 0)
                        { }
                        if ((byte)port.ReadByte() == stx)
                        {
                            Logger log = new Logger("Transport", machineID);
                            fPResult.answer.Add((byte)port.ReadByte());
                            log.Write($"Len={fPResult.answer[0]}");
                            byte packLen = fPResult.answer[0];
                            List<byte> tempAnswer = new List<byte>();
                            for (int i = 1; i <= packLen; i++)
                            {
                                fPResult.answer.Add((byte)port.ReadByte());
                            }
                            if (CRC(fPResult.answer) == (byte)port.ReadByte())
                            {
                                if (Send(new byte[] { ask }) == 0)
                                {
                                    log.Write($"Error={fPResult.answer[2]}");
                                    fPResult.Error = fPResult.answer[2];
                                    log.Write(fPResult.answer.ToArray());
                                    if (fPResult.Error == 0)
                                    {
                                        for (int t = 3; t <= packLen; t++)
                                        {
                                            tempAnswer.Add(fPResult.answer[t]);
                                        }
                                        log.Write($"Temp count={tempAnswer.Count}");
                                        fPResult.answer = tempAnswer;
                                        log.Write(fPResult.answer.ToArray());
                                        log.Write("-   -   -   -   -   -");
                                    }
                                    else
                                        fPResult.answer.Clear();
                                }
                                else
                                {
                                    fPResult.answer.Clear();
                                    fPResult.Error = 203;
                                    //Logger log = new Logger("Transport", machineID);
                                    log.Write($"Send command (Init NAK, send ASK, step 1) error {fPResult.Error}");
                                }
                            }
                            else
                            {
                                fPResult.answer.Clear();
                                fPResult.Error = 203;
                                //Logger log = new Logger("Transport", machineID);
                                log.Write($"Send command (Init NAK, send ASK, CRC faild, step 2) error {fPResult.Error}");
                                Send(new byte[] { nak });
                            }
                        }
                        else
                        {
                            fPResult.answer.Clear();
                            fPResult.Error = 202;
                            Logger log = new Logger("Transport", machineID);
                            log.Write($"Send command (Init NAK, send ASK, first byte not STX step 3) error {fPResult.Error}");
                        }
                    }
                    if (resCmd == nak) /// FP decline data
                    {
                        int tryes = 3;
                        bool sent = false;
                        while (tryes > 0 && !sent)
                        {
                            dataOnPort = false;
                            resCmd = SendCommand(data);
                            if (resCmd == ask)
                            {
                                while (port.BytesToRead == 0)
                                { }
                                if ((byte)port.ReadByte() == stx)
                                {
                                    fPResult.answer.Add((byte)port.ReadByte());
                                    byte packLen = fPResult.answer[0];
                                    List<byte> tempAnswer = new List<byte>();
                                    for (int i = 1; i <= packLen; i++)
                                    {
                                        fPResult.answer.Add((byte)port.ReadByte());
                                    }
                                    if (CRC(fPResult.answer) == (byte)port.ReadByte())
                                    {
                                        if (Send(new byte[] { ask }) == 0)
                                        {
                                            fPResult.Error = fPResult.answer[2];
                                            for (int t = 3; t <= packLen; t++)
                                                tempAnswer.Add(fPResult.answer[t]);
                                            fPResult.answer = tempAnswer;
                                            Logger log = new Logger("Transport", machineID);
                                            log.Write(fPResult.answer.ToArray());
                                            log.Write("-   -   -   -   -   -");
                                        }
                                        else
                                        {
                                            fPResult.Error = 203;
                                            Logger log = new Logger("Transport", machineID);
                                            log.Write($"Send command Init NAK, send NAK, resend ASK, step 1 error {fPResult.Error}");
                                        }
                                    }
                                    else
                                    {
                                        fPResult.Error = 203;
                                        Logger log = new Logger("Transport", machineID);
                                        log.Write($"Send command (Init NAK, send NAK, resend ASK, CRC faild, step 2) error {fPResult.Error}");
                                        Send(new byte[] { nak });
                                    }
                                }
                                else
                                {
                                    fPResult.Error = 202;
                                    Logger log = new Logger("Transport", machineID);
                                    log.Write($"Send command (Init NAK, send NAK, resend ASK, First byte not STX, step 3) error {fPResult.Error}");
                                }
                            }
                            else
                            {
                                tryes--;
                            }
                        }
                        if (tryes == 0 && !sent)
                        {
                            Logger log = new Logger("Transport", machineID);
                            fPResult.Error = 203;
                            log.Write($"Send command (Init NAK, send NAK, step 4 (tryes over)) error {fPResult.Error}");
                        }
                    }
                    if (resCmd == 0xFF)
                    {
                        fPResult.Error = 202;
                        Logger log = new Logger("Transport", machineID);
                        log.Write($"Send command (Init NAK, step 5 (0xFF)) error {fPResult.Error}");
                    }
                }
                if (resInit == ask) /// FP not ready yet for cmd
                {
                    dataOnPort = false;
                    FPResult tempResult = new FPResult();
                    while (!dataOnPort && !timeOut)
                    { }
                    if (!timeOut)
                    {
                        while (port.BytesToRead == 0)
                        { }
                        while (port.BytesToRead == 0)
                        { }
                        if ((byte)port.ReadByte() == stx)
                        {
                            fPResult.answer.Add((byte)port.ReadByte());
                            byte packLen = fPResult.answer[0];
                            //List<byte> tempForCRC = new List<byte>();
                            for (int i = 1; i <= packLen; i++)
                            {
                                fPResult.answer.Add((byte)port.ReadByte());
                            }
                            if (CRC(fPResult.answer) == (byte)port.ReadByte())
                            {
                                if (Send(new byte[] { ask }) == 0)
                                {
                                    Logger log = new Logger("Transport", machineID);
                                    log.Write(fPResult.answer.ToArray());
                                    log.Write("-   -   -   -   -   -");
                                }
                                else
                                {
                                    fPResult.Error = 203;
                                    Logger log = new Logger("Transport", machineID);
                                    log.Write($"Send command Init ASK, step 1 error {fPResult.Error}");
                                }
                            }
                            else
                            {
                                fPResult.Error = 203;
                                Logger log = new Logger("Transport", machineID);
                                log.Write($"Send command Init ASK, CRC faild step 2 error {fPResult.Error}");
                                Send(new byte[] { nak });
                            }
                        }
                        else
                        {
                            fPResult.Error = 202;
                            Logger log = new Logger("Transport", machineID);
                            log.Write($"Send command Init ASK, First byte not STX step 3 error {fPResult.Error}");
                        }
                    }
                    else
                    {
                        fPResult.Error = 202;
                        Logger log = new Logger("Transport", machineID);
                        log.Write($"Send command Init ASK, TimeOut step 4 error {fPResult.Error}");
                    }

                }
                if (resInit == 0xFF) /// Time Out
                {
                    fPResult.Error = 202;
                    Logger log = new Logger("Transport", machineID);
                    log.Write($"Send command Init 0xFF error {fPResult.Error}");
                }
            }
            catch (Exception e)
            {
                Logger log = new Logger("Transport", machineID);
                log.Write($"Sending exception: {e.Message}");
                fPResult.Error = 203;
            }
            timer1.Enabled = false;
            return fPResult;
        }

        private void Timer1_Elapsed(object sender, ElapsedEventArgs e)
        {
            timer1.Enabled = false;
            timeOut = true;
        }

        private byte SendInit()
        {
            int res = Send(new byte[] { enq });
            while(!dataOnPort && !timeOut)
            { }
            if (timeOut)
                return 0xFF;
            else
                return (byte)port.ReadByte();
        }

        private byte SendCommand(List<byte> data)
        {
            int res = Send(data.ToArray());
            while (!dataOnPort && !timeOut)
            { }
            if (timeOut)
                return 0xFF;
            else
                return (byte)port.ReadByte();
        }

        /// <summary>
        /// Отправка данных в COM-порт
        /// </summary>
        /// <param name="data">массив с данными для отправки</param>
        /// <returns></returns>
        private int Send(byte[] data)
        {
            try
            {
                port.Write(data, 0, data.Length);
                return 0;
            }
            catch
            {
                return 201;
            }
        }

        /// <summary>
        /// Calculate CRC(RLC) for message
        /// </summary>
        /// <param name="message">message to calculate CRC (exclude 0 byte)</param>
        /// <returns></returns>
        private static byte CRC(List<byte> message)
        {
            byte crc = 0;
            foreach (byte bt in message)
            {
                crc ^= bt;
            }
            return crc;
        }

        /// <summary>
        /// Convert integer to byte array
        /// </summary>
        /// <param name="inData"></param>
        /// <param name="length">desired length of the array</param>
        /// <returns></returns>
        private static List<byte> GetByte(UInt64 inData, int length)
        {
            List<byte> outData = new List<byte>();
            for (int i = 0; i < length; i++)
            {
                outData.Add((byte)(inData >> 8 * i));
            }
            return outData;
        }

        /// <summary>
        /// Convert string to byte array
        /// </summary>
        /// <param name="inData"></param>
        /// <param name="length">desired length of the array</param>
        /// <returns></returns>
        private static List<byte> GetByte(string inData, int length)
        {
            List<byte> outData = new List<byte>();
            byte[] tempArray = new byte[length];
            int tempLength = (inData.Length >= length) ? length : inData.Length;
            for (int i = 0; i < length; i++)
            {
                tempArray[i] = 0x00;
            }
            byte[] bytes = Encoding.GetEncoding(1251).GetBytes(inData);
            for (int i = 0; i < tempLength; i++)
            {
                tempArray[i] = bytes[i];
            }
            outData.AddRange(tempArray);
            return outData;
        }

        private static List<byte> GetByte(DateTime time)
        {
            List<byte> outData = new List<byte>();
            outData.Add(Convert.ToByte(time.Hour));
            outData.Add(Convert.ToByte(time.Minute));
            outData.Add(Convert.ToByte(time.Second));
            return outData;
        }

        private static List<byte> GetByteDate(DateTime date)
        {
            List<byte> outData = new List<byte>();
            outData.Add(Convert.ToByte(date.Day));
            outData.Add(Convert.ToByte(date.Month));
            outData.Add(Convert.ToByte(date.Year-2000));
            return outData;
        }
        #endregion

        #region Error Analize
        private static int SetStatus(FPResult result)
        {
            int intStatus = 0;
            //FPResult.Status status = new FPResult.Status();
            Logger log = new Logger("Transport", machineID);
            if (result.answer.Count > 0)
            {
                log.Write($"FP Mode: {result.answer[3]}");
                log.Write($"FP SubMode: {result.answer[4]}");
                try
                {
                    //if (result.answer[4] == 0)
                    //    status.outOfPaper = false;
                    //else
                    //{
                    //    status.outOfPaper = true;
                    //}

                    //if (result.answer[3] == 0x03)
                    //{
                    //    status.blocked24Hour = true;
                    //    status.shiftOpened = true;
                    //}
                    //else
                    //    status.blocked24Hour = false;

                    //if (result.answer[3] == 0x04)
                    //    status.shiftOpened = false;

                    //if ((result.answer[3] == 0x02) || (result.answer[3] == 0x03) || (result.answer[3] == 0x01) || (result.answer[3] == 0x00))
                    //    status.shiftOpened = true;

                    //if (status.blocked24Hour || status.outOfPaper)
                    //    status.blocked = true;
                    //else
                    //    status.blocked = false;
                    //log.Write($"Set status Blocked {status.blocked}, Blocked24Hours {status.blocked24Hour}, ShiftOpened {status.shiftOpened}, OutOfPaper {status.outOfPaper}");

                    if (result.answer[3] == 0x02)
                        intStatus = 1;
                    if (result.answer[3] == 0x03)
                        intStatus = 5;
                    if (result.answer[3] == 0x04)
                        intStatus = 0;
                    if (result.answer[4] != 0)
                        intStatus = intStatus + 8;
                    return intStatus;
                }
                catch (Exception e)
                {
                    log.Write($"Set status Exception{e.Message}");
                    //status.blocked = true;
                    //status.blocked24Hour = true;
                    //status.shiftOpened = false;
                    //status.outOfPaper = true;
                    
                    return 2;
                }
            }
            else
            {
                log.Write($"Set status error. Answer is null");
                //status.blocked = true;
                //status.blocked24Hour = true;
                //status.shiftOpened = false;
                //status.outOfPaper = true;
                return 2;
            }
        }
        #endregion
    }
}
