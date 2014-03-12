using System.ServiceModel;

namespace Patronum.WebService.Test.Exceptions
{
    public class NotFoundTestDataException : FaultException
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
