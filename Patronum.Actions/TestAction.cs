﻿
using Patronum.Actions.Interfaces;

namespace Patronum.Actions
{
    public abstract class TestAction : ITestAction
    {
        protected IApplicationUnderTest ApplicationUnderTest { get; set; }

        protected TestAction(IApplicationUnderTest application)
        {
            ApplicationUnderTest = application;
        }

        public abstract object Execute(params object[] parameters);
    }
}
