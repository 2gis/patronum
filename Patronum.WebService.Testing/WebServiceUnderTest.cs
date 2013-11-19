using System;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using Patronum.WebService.Testing.Exceptions;

namespace Patronum.WebService.Testing
{
    public class WebServiceUnderTest : IApplicationUnderTest
    {
        public WebServiceUnderTest()
        {
            Config = ConfigurationManager.AppSettings;
        }

        public NameValueCollection Config { get; set; }

        public string LogsDirectory { get; set; }
        
        #region Log

        public StreamWriter OpenLogFile()
        {
            StreamWriter log;
            string filename = LogsDirectory + @"\log_" + DateTime.Now.ToString("MMddyyyy");
            try
            {
                log = !File.Exists(filename) ? new StreamWriter(filename) : File.AppendText(filename);
            }
            catch (Exception e)
            {
                throw new LogException("Ошибка создания лог-файла:" + e.Message);
            }

            return log;
        }

        public void CloseLogFile()
        {
            using (var log = OpenLogFile())
            {
                try
                {
                    log.Close();
                }
                catch (Exception e)
                {
                    throw new LogException("Ошибка закрытия лог-файла:" + e.Message);
                }
            }
        }

        public void WriteToLog(string requestUrl, string requestMethod, string responseCode, string responseText)
        {
            using (var log = OpenLogFile())
            {
                try
                {
                    log.WriteLine("REQUEST:");
                    log.WriteLine(requestUrl);
                    log.WriteLine(requestMethod);
                    log.WriteLine("RESPONSE:");
                    log.WriteLine(responseCode);
                    log.WriteLine(responseText);
                    log.WriteLine(" ");
                }
                catch (Exception e)
                {
                    throw new LogException("Ошибка записи в лог:" + e.Message);
                }
            }
        }
        #endregion
    }
}

