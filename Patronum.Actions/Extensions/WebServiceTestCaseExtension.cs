using System;
using Patronum.WebService.Test;

namespace Patronum.Actions.Extensions
{
    public static class WebServiceTestCaseExtension
    {
        public static T GetAction<T>(this WebServiceTestCase testCase) where T : IAction
        {
            return (T)Activator.CreateInstance(typeof(T), new object[] { testCase.WebServiceUnderTest });
        }
    }
}
