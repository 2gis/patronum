
namespace Patronum.Test.Helpers
{
    using System.Text.RegularExpressions;

    public class HtmlSourceHelper
    {
        public static string GetHiddenFieldValue(string html, string fieldName)
        {
            if (html != null)
            {
                var pattern = string.Format(@"<input id=""{0}"" (name=""{0}"" )", fieldName);
                pattern += @"?type=""hidden"" value=""(?<value>[^""]*)""";

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
