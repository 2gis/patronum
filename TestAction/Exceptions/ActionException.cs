
namespace TestActions.Exceptions
{
    using System;

    public class ActionException : Exception
    {
        public ActionException(string message) : base(message)
        {
        }
    }
}
