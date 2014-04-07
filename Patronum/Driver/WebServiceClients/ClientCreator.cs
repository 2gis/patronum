
namespace Patronum.Driver.WebServiceClients
{
    public interface IClientCreator<out T> where T : WebServiceClient
    {
        T CreateClient();
    }
}
