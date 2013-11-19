using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Patronum.WebService.Testing.Exceptions
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
