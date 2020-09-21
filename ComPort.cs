using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;

namespace KZ_ShtrikhM_FiscalDevice
{
    class ComPort
    {
        SerialPort Port = new SerialPort();
        public delegate void Received();
        public event Received ReceivedEvent;
        private void OnRecievedEvent()
        {
            if (this.ReceivedEvent != null)
                this.ReceivedEvent();
        }

        public int Connect(string _portname)
        {
            //Насторойка порта
            Port.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
            Port.PortName = _portname;
            Port.BaudRate = 38400;
            Port.Parity = Parity.None;
            Port.DataBits = 8;
            Port.StopBits = StopBits.One;
            Port.Handshake = Handshake.None;
            Port.RtsEnable = true;
            Port.DtrEnable = true;
            Port.Encoding = Encoding.Unicode;
            try
            {
                if (!(Port.IsOpen))
                    Port.Open();
                return 0;
            }
            catch (Exception)
            {
                return 3;
            }
        }
        /// <summary>
        /// Обработчик события прихода данных в COM-порт
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        public void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            //SerialPort port = (SerialPort)sender;
            //port.ReadTimeout = 5000;
            //var buffer = new byte[256];
            //int i = 3;
            //buffer[0] = (byte)port.ReadByte();
            //buffer[1] = (byte)port.ReadByte();
            //buffer[2] = (byte)port.ReadByte();
            //short len = BitConverter.ToInt16(buffer, 1);
            //do
            //{
            //    buffer[i] = (byte)port.ReadByte();
            //    i++;
            //}
            //while (i < len);
            this.OnRecievedEvent();
        }

        /// <summary>
        /// Отправка данных в COM-порт
        /// </summary>
        /// <param name="data">массив с данными для отправки</param>
        /// <returns></returns>
        public int Send(byte[] data)
        {
            try
            {
                Port.Write(data, 0, data.Length);
                return 0;
            }
            catch (Exception)
            {
                return 3;
            }
        }

        /// <summary>
        /// Закрыть COM-порт
        /// </summary>
        public int Close()
        {
            try
            {
                Port.Close();
                return 0;
            }
            catch (Exception)
            {
                return 3;
            }
        }
    }
}
