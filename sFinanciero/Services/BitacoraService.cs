using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace sFinanciero.Services
{
    
    public class BitacoraService
    {
        private static string LogFilePath = AppContext.BaseDirectory + "\\sFinanciero.log";

        public static void Log(string type,string message)
        {
            string text = DateTime.UtcNow.ToString() + "   " + type.ToUpper() + "   " + message;
            File.AppendAllLines(LogFilePath, new string[] { text });
        }
        public static string[] getBitacora()
        {
            string[] lines = File.ReadAllLines(LogFilePath, Encoding.UTF8);
            return lines;
        }
        public static string[] getBitacoraReverse()
        {
            string[] lines = File.ReadAllLines(LogFilePath, Encoding.UTF8).Reverse().ToArray();
            return lines;
        }

       public static MemoryStream GetStream()
        {
            var dataBytes = File.ReadAllBytes(LogFilePath);
            return new MemoryStream(dataBytes);
        }
    }

}