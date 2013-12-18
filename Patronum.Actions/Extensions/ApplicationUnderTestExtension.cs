
namespace Patronum.Actions.Extensions
{
    using Intarfaces;

    public static class ApplicationUnderTestExtension
    {
        public static object Action<T>(this IApplicationUnderTest app, params object[] parameters) where T : IAction
        {
            var action = (T)System.Activator.CreateInstance(typeof(T), new object[] { app });
            return action.Execute(parameters);
        }
    }
}
