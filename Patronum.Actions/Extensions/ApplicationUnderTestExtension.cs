
using System;
using Patronum.Actions.Exceptions;
using Patronum.Actions.Interfaces;

namespace Patronum.Actions.Extensions
{
    public static class ApplicationUnderTestExtension
    {
        /// <exception cref="ActionException"/>
        public static object Action<TAction>(this IApplicationUnderTest app, params object[] parameters) where TAction : ITestAction
        {
            var action = (TAction)Activator.CreateInstance(typeof(TAction), new object[] { app });

            try
            {
                return action.Execute(parameters);
            }
            catch (Exception e)
            {
                throw new ActionException("Ошибка при выполнении действия " + typeof(TAction).Name + ": " + e.Message);
            }
        }
    }
}
