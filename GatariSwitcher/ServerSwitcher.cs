using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using GatariSwitcher.Hosts;
using GatariSwitcher.Extensions;

namespace GatariSwitcher
{
    class ServerSwitcher
    {
        private readonly string gatariAddress;

        public ServerSwitcher(string gatariAddress)
        {
            this.gatariAddress = gatariAddress;
        }

        public void SwitchToGatari()
        {
            var hosts = HostsFile.Open(GetHostsPath());
            //remove old items
            hosts.Items
                .Where(x => x.Host != null && x.Host.Contains("ppy.sh"))
                .ToList()
                .ForEach(x => hosts.Items.Remove(x));
            // add new items
            hosts.Items.Add(new HostsEntry(gatariAddress, "osu.ppy.sh", null));
            hosts.Items.Add(new HostsEntry(gatariAddress, "c.ppy.sh", null));
            hosts.Items.Add(new HostsEntry(gatariAddress, "c1.ppy.sh", null));
            hosts.Items.Add(new HostsEntry(gatariAddress, "a.ppy.sh", null));
            hosts.Items.Add(new HostsEntry(gatariAddress, "i.ppy.sh", null));
            hosts.Write();
        }

        public void SwitchToOfficial()
        {
            var hosts = HostsFile.Open(GetHostsPath());
            hosts.Items
                .Where(x => x.Host != null && x.Host.Contains("ppy.sh"))
                .ToList()
                .ForEach(x => hosts.Items.Remove(x));
            hosts.Write();
        }

        /// <summary>
        /// Get current osu! server
        /// </summary>
        /// <returns>true - gatari, false - official</returns>
        public bool GetCurrentServer()
        {
            var hosts = HostsFile.Open(GetHostsPath());
            return hosts.Items.Any(x => x.Host != null && x.Host.Contains("ppy.sh"));
        }

        private string GetHostsPath()
        {
            string windir = Environment.GetFolderPath(Environment.SpecialFolder.Windows);
            string result = Path.Combine(windir, "System32", "drivers", "etc", "hosts");

            return result;
        }
    }
}
