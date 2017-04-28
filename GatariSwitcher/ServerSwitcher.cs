using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using GatariSwitcher.Extensions;

namespace GatariSwitcher
{
    class ServerSwitcher
    {
        private readonly string serverAddress;

        public ServerSwitcher(string servAddress)
        {
            this.serverAddress = servAddress;
        }

        public void SwitchToGatari()
        {
            string hostsPath = GetHostsPath();

            string[] lines = File.ReadAllLines(hostsPath);

            var result = lines.Where(x => !x.Contains("ppy.sh")).ToList();

            result.Add(serverAddress + "   osu.ppy.sh");
            result.Add(serverAddress + "   c.ppy.sh");
            result.Add(serverAddress + "   c1.ppy.sh");
            result.Add(serverAddress + "   a.ppy.sh");
            result.Add(serverAddress + "   i.ppy.sh");

            bool ro = GetReadOnlyFlagStatus(hostsPath);
            if (ro) DisableReadOnlyFlag(hostsPath);
            File.WriteAllLines(hostsPath, result);
            if (ro) EnableReadOnlyFlag(hostsPath);
        }

        public void SwitchToOfficial()
        {
            string hostsPath = GetHostsPath();

            string[] lines = File.ReadAllLines(hostsPath);

            var result = lines.Where(x => !x.Contains("ppy.sh"));

            bool ro = GetReadOnlyFlagStatus(hostsPath);
            if (ro) DisableReadOnlyFlag(hostsPath);
            File.WriteAllLines(hostsPath, result);
            if (ro) EnableReadOnlyFlag(hostsPath);
        }

        /// <summary>
        /// Get current osu! server
        /// </summary>
        /// <returns>true - gatari, false - official</returns>
        public bool GetCurrentServer()
        {
            string[] lines = File.ReadAllLines(GetHostsPath());

            return lines.Any(x => x.Contains("osu.ppy.sh") && !x.Contains("#"));
        }

        private string GetHostsPath()
        {
            string windir = Environment.GetFolderPath(Environment.SpecialFolder.Windows);
            string result = Path.Combine(windir, "System32", "drivers", "etc", "hosts");

            return result;
        }

        private bool GetReadOnlyFlagStatus(string filepath)
        {
            var attr = File.GetAttributes(filepath);
            return attr.HasFlag(FileAttributes.ReadOnly);
        }

        private void DisableReadOnlyFlag(string filepath)
        {
            var attr = File.GetAttributes(filepath);
            var a = attr ^ FileAttributes.ReadOnly;
            File.SetAttributes(filepath, a);
        }

        private void EnableReadOnlyFlag(string filepath)
        {
            var attr = File.GetAttributes(filepath);
            var a = attr | FileAttributes.ReadOnly;
            File.SetAttributes(filepath, a);
        }
    }
}
