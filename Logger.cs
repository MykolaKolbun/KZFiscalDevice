using System;
using System.IO;
using System.Text;
//using System.Threading.Tasks;

namespace KZ_ShtrikhM_FiscalDevice
{
    class Logger
    {
        string File { get; set; }
        string Path { get; set; }

        public Logger(string fileName, string machineID)
        {
            this.File = fileName;
            this.Path = $@"{StringValue.WorkingDirectory}Log\{File}Trace-{machineID} {DateTime.Now:yyyy-MM-dd}.txt";

        }

        public void Write(string mess)
        {
            try
            {
                StreamWriter sw = new StreamWriter(Path, true, Encoding.UTF8);
                string dateTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                sw.WriteLine("{0}: {1}", dateTime, mess);
                sw.Close();
            }
            catch { }
        }

        /// <summary>
        /// Запись строки в лог файл. Перегруженый.
        /// </summary>
        /// <param name="mess">Сообщение (массив байтов)</param>
        /// <param name="direction">Индикатор направления. True - к фискальному принтеру; False - ответ от фискального принтера</param>
        public void logWrite(byte[] mess, bool direction)
        {
            string dir;
            StreamWriter sw = new StreamWriter(Path, true, System.Text.Encoding.UTF8);
            if (direction)
            { dir = ">>"; } // true if to printer
            else { dir = "<<"; } // false if from printer
            sw.WriteLine($"{DateTime.Now.ToString()}: {dir} {BitConverter.ToString(mess)}");
            sw.Close();
        }

        public void logWrite(byte mess, bool direction)
        {
            string dir;
            StreamWriter sw = new StreamWriter(Path, true, System.Text.Encoding.UTF8);
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
            StreamWriter sw = new StreamWriter(Path, true, Encoding.UTF8);
            if (direction)
            { dir = ">>"; } // true if to printer
            else { dir = "<<"; } // false if from printer
            sw.WriteLine($"{dateTime}: {dir} {mess}");
            sw.Close();
        }

        public void Write(byte[] mess)
        {
            string dateTime = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
            StreamWriter sw = new StreamWriter(Path, true, Encoding.UTF8);
            sw.WriteLine($"{dateTime}   {BitConverter.ToString(mess)}");
            sw.Close();
        }
    }
}
