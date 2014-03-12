using System;

namespace Patronum.Actions.Exceptions
{
    public class ActionException : Exception
    {
        public ActionException(string message) : base(message)
        {
        }
    }
}
