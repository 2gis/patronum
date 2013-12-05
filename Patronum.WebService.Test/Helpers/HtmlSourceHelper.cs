using System.Text.RegularExpressions;

namespace Patronum.WebService.Test.Helpers
{
    public class HtmlSourceHelper
    {
        public static string GetHiddenFieldValue(string html, string fieldName)
        {
            if (html != null)
            {
                var pattern = string.Format(@"<input id=""{0}"" (name=""{0}"" )?type=""hidden"" value=""(?<value>[^""]*)""", fieldName);

                var m = Regex.Match(html, pattern);
                if (m.Success)
                {
                    return m.Groups["value"].Value;
                }
            }  
      
            return string.Empty;
        }
    }
}
