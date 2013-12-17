using System;
using Patronum.Actions;

namespace Patronum.WebService.Test.Extensions
{
    public static class WebServiceTestCaseExtension
    {
        public static object Action<T>(this WebServiceTestCase testCase, params object[] parameters) where T : IAction
        {
            var action = (T)Activator.CreateInstance(typeof(T), new object[] { testCase.WebServiceUnderTest });
            return action.Execute(parameters);
        }
    }
}
