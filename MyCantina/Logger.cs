using System;
using System.IO;

namespace MyCantina
{
    static class Logger
    {
        static string _logPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\MyCantinaTemp\log.ini";

        public static void Log(string str)
        {
            string tolog = "[" + DateTime.Now + "]\t" + str;
            using (var sw = new StreamWriter(_logPath, true))
            {
                sw.WriteLine(tolog);
            }
        }
    }
}
