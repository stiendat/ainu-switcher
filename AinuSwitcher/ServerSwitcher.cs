using System.Linq;
using AinuSwitcher.Extensions;
using AinuSwitcher.Helpers;
using System.Threading.Tasks;

namespace AinuSwitcher
{
    class ServerSwitcher
    {
        private readonly string serverAddress;

        public ServerSwitcher(string AinuIpAddress)
        {
            this.serverAddress = AinuIpAddress;
        }

        public void SwitchToAinu()
        {
            var lines = HostsFile.ReadAllLines();
            var result = lines.Where(x => !x.Contains("ppy.sh")).ToList();
            result.AddRange
            (
                serverAddress + " osu.ppy.sh",
                serverAddress + " c.ppy.sh",
                serverAddress + " c1.ppy.sh",
                serverAddress + " c2.ppy.sh",
                serverAddress + " c3.ppy.sh",
                serverAddress + " c4.ppy.sh",
                serverAddress + " c5.ppy.sh",
				serverAddress + " c6.ppy.sh",
                serverAddress + " ce.ppy.sh",
                serverAddress + " a.ppy.sh",
                serverAddress + " i.ppy.sh"
            );
            HostsFile.WriteAllLines(result);
        }

        public void SwitchToOfficial()
        {
            HostsFile.WriteAllLines(HostsFile.ReadAllLines().Where(x => !x.Contains("ppy.sh")));
        }

        public Task<Server> GetCurrentServerAsync()
        {
            return Task.Run<Server>(() => GetCurrentServer());
        }

        public Server GetCurrentServer()
        {
            bool isAinu = HostsFile.ReadAllLines().Any(x => x.Contains("osu.ppy.sh") && !x.Contains("#"));
            return isAinu ? Server.Ainu : Server.Official;
        }
    }

    public enum Server
    {
        Official,
        Ainu
    }
}
