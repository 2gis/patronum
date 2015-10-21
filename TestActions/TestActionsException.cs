namespace TestActions
{
    #region using

    using System;

    #endregion

    public class TestActionsException : Exception
    {
        #region Constructors and Destructors

        public TestActionsException()
        {
        }

        public TestActionsException(string message)
            : base(message)
        {
        }

        public TestActionsException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        #endregion
    }
}
