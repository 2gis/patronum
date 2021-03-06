﻿
namespace Patronum.Test
{
    using System;
    using System.Collections.Specialized;
    using System.Configuration;
    using System.IO;

    public class WebServiceUnderTest
    {
        public WebServiceUnderTest()
        {
            Config = ConfigurationManager.AppSettings;
        }
 
        public NameValueCollection Config { get; set; }

        public string LogsDirectory { get; set; }

        #region Log

        // todo need reactoring
        public StreamWriter OpenLogFile()
        {
            try
            {
                var filename = LogsDirectory + @"\log_" + DateTime.Now.ToString("MMddyyyy");

                return !File.Exists(filename) ? new StreamWriter(filename) : File.AppendText(filename);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Cannot create log file: "  + e.Message);
            }
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
                    throw new InvalidOperationException("Cannot close log file: " + e.Message);
                }
            }
        }

        public void WriteToLog(string requestUrl, string requestMethod, string responseCode, string responseText)
        {
            try
            {
                using (var log = OpenLogFile())
                {
                    log.WriteLine("REQUEST:");
                    log.WriteLine(requestUrl);
                    log.WriteLine(requestMethod);
                    log.WriteLine("RESPONSE:");
                    log.WriteLine(responseCode);
                    log.WriteLine(responseText);
                    log.WriteLine(" ");
                }
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }
        }
        #endregion
    }
}
