
namespace Patronum.Actions.Extensions
{
    public static class ApplicationUnderTestExtension
    {
        public static object Action<T>(this Intarfaces.IApplicationUnderTest app, params object[] parameters) where T : Action
        {
            var action = (T)System.Activator.CreateInstance(typeof(T), new object[] { app });
            return action.Execute(parameters);
        }
    }
}
