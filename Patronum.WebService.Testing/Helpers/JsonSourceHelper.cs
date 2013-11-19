using System;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Patronum.WebService.Testing.Helpers
{
    public static class JsonSourceHelper
    {
        public static JObject ParseJson(string text)
        {
            try
            {
                return JObject.Parse(text);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static JObject TryParseObject(string result)
        {
            if (result == null)
            {
                return null;
            }

            try
            {
                var json = JObject.Parse(result);
                if (!json["ValidationMessages"].Any() &&
                    (string.IsNullOrEmpty(json["Message"].ToString()) ||
                     json["MessageType"].ToString() != "CriticalError"))
                {
                    return json;
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static JArray TryParseArray(string result)
        {
            try
            {
                var json = JArray.Parse(result);
                if (json["Message"].ToString().Length <= 0)
                {
                    return json;
                }

                return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
