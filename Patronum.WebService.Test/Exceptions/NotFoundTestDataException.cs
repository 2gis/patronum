using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Patronum.WebService.Test.Exceptions
{
    public class NotFoundTestDataException : UnitTestAssertException
    {
        public NotFoundTestDataException()
        {
            // Add implementation.
        }

        public NotFoundTestDataException(string message) : base(message)
        {
            // Add implementation.
        }
    }
}
