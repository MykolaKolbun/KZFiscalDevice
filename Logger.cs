using System;
using System.IO;
using System.Text;
//using System.Threading.Tasks;

namespace KZ_ShtrikhM_FiscalDevice
{
    class Logger
    {
        /// <summary>
        /// Полный путь к лог файлу.
        /// </summary>
        //string filePath = "C:\\Log\\FiscalTrace"+DateTime.Now.Date.ToString()+".txt";
        string filePath = $@"C:\Log\FiscalTrace-{DateTime.Now:yyyy-MM-dd}.txt";

        /// <summary>
        /// Конструктор класса Logger. Создает папку и файл.
        /// </summary>
        public Logger(string machineId)
        {
            filePath = $@"C:\Log\{machineId} FiscalTrace-{DateTime.Now:yyyy-MM-dd}.txt";
        }
        public Logger()
        {
            filePath = $@"C:\Log\FiscalTrace-{DateTime.Now:yyyy-MM-dd}.txt";
        }

        public Logger(string fileName, string machineId)
        {
            filePath = $@"C:\Log\{machineId} {fileName}-{DateTime.Now:yyyy-MM-dd}.txt";
        }

        /// <summary>
        /// Запись строки в лог файл. Перегруженый.
        /// </summary>
        /// <param name="mess">Сообщение (массив байтов)</param>
        /// <param name="direction">Индикатор направления. True - к фискальному принтеру; False - ответ от фискального принтера</param>
        public void logWrite(byte[] mess, bool direction)
        {
            string dir;
            StreamWriter sw = new StreamWriter(filePath, true, System.Text.Encoding.UTF8);
            if (direction)
            { dir = ">>"; } // true if to printer
            else { dir = "<<"; } // false if from printer
            sw.WriteLine($"{DateTime.Now.ToString()}: {dir} {BitConverter.ToString(mess)}");
            sw.Close();
        }

        public void logWrite(byte mess, bool direction)
        {
            string dir;
            StreamWriter sw = new StreamWriter(filePath, true, System.Text.Encoding.UTF8);
            if (direction)
            { dir = ">>"; } // true if to printer
            else { dir = "<<"; } // false if from printer
            sw.WriteLine($"{DateTime.Now.ToString()}: {dir} {mess.ToString("x2")}");
            sw.Close();
        }

        /// <summary>
        /// Запись строки в лог файл. Перегруженый.
        /// </summary>
        /// <param name="mess">Сообщение (строка)</param>
        /// <param name="direction">Индикатор направления. True - к фискальному принтеру; False - ответ от фискального принтера</param>
        public void logWrite(string mess, bool direction)
        {
            //System.Text.Encoding.Default.GetString(mess);
            string dateTime = DateTime.Now.ToString();
            string dir;
            StreamWriter sw = new StreamWriter(filePath, true, Encoding.UTF8);
            if (direction)
            { dir = ">>"; } // true if to printer
            else { dir = "<<"; } // false if from printer
            sw.WriteLine($"{dateTime}: {dir} {mess}");
            sw.Close();
        }

        public void Write(string mess)
        {
            string dateTime = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
            StreamWriter sw = new StreamWriter(filePath, true, Encoding.UTF8);
            sw.WriteLine($"{dateTime}   {mess}");
            sw.Close();
        }

        public void Write(byte[] mess)
        {
            string dateTime = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
            StreamWriter sw = new StreamWriter(filePath, true, Encoding.UTF8);
            sw.WriteLine($"{dateTime}   {BitConverter.ToString(mess)}");
            sw.Close();
        }
    }
}
