﻿namespace TestActions.Exceptions
{
    #region using

    using System;

    #endregion

    public class TestActionsException : Exception
    {
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
    }
}
