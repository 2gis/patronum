
namespace Patronum.Actions
{
    public interface IAction
    {
        object Execute(params object[] list);
    }
}
