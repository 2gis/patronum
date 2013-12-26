
namespace Patronum.Actions.Extensions
{
    using Intarfaces;

    public static class ApplicationUnderTestExtension
    {
        public static object Action<T>(this IApplicationUnderTest app, params object[] parameters) where T : ITestAction
        {
            var action = (T)System.Activator.CreateInstance(typeof(T), new object[] { app });
            return action.Execute(parameters);
        }
    }
}
