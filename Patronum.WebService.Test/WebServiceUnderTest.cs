using System;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using Patronum.Actions;

namespace Patronum.WebService.Test
{
    public class WebServiceUnderTest : IApplicationUnderTest
    {
        public WebServiceUnderTest()
        {
            this.Config = ConfigurationManager.AppSettings;
        }

        public NameValueCollection Config { get; set; }

        public string LogsDirectory { get; set; }

        #region Log

        // todo need reactoring
        public StreamWriter OpenLogFile()
        {
            try
            {
                string filename = this.LogsDirectory + @"\log_" + DateTime.Now.ToString("MMddyyyy");

                return !File.Exists(filename) ? new StreamWriter(filename) : File.AppendText(filename);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Cannot create log file: "  + e.Message);
            }
        }

        public void CloseLogFile()
        {
            using (var log = this.OpenLogFile())
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
                using (var log = this.OpenLogFile())
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
