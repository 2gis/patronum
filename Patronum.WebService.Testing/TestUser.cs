using System.Net;

namespace Patronum.WebService.Testing
{
    public class TestUser
    {
        public TestUser(long id, string name, string passwd, string username = "")
        {
            Id = id;
            Name = name;
            Password = passwd;
            UserName = string.IsNullOrEmpty(username) ? (name + ',' + name) : username;
        }

        public long Id { get; set; }

        public string Name { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }


        public NetworkCredential GetNetworkCredential()
        {
            return new NetworkCredential("2gis\\" + Name, Password);
        }
    }
}
