using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace GatariSwitcher
{
    class ServerSwitcher
    {
        const string GATARI_ADDRESS = "93.170.76.141";

        public void SwitchToGatari()
        {
            string hostsPath = GetHostsPath();

            bool roStatus = GetReadOnlyFlagStatus(hostsPath);
            if (roStatus)
            {
                DisableReadOnlyFlag(hostsPath);
            }

            string[] lines = File.ReadAllLines(hostsPath);

            var result = lines.Where(x => !x.Contains("ppy.sh")).ToList();

            result.Add(GATARI_ADDRESS + "   osu.ppy.sh");
            result.Add(GATARI_ADDRESS + "   c.ppy.sh");
            result.Add(GATARI_ADDRESS + "   c1.ppy.sh");
            result.Add(GATARI_ADDRESS + "   a.ppy.sh");
            result.Add(GATARI_ADDRESS + "   i.ppy.sh");

            File.WriteAllLines(hostsPath, result);

            if (roStatus)
            {
                EnableReadOnlyFlag(hostsPath);
            }
        }

        public void SwitchToOfficial()
        {
            string hostsPath = GetHostsPath();

            bool roStatus = GetReadOnlyFlagStatus(hostsPath);
            if (roStatus)
            {
                DisableReadOnlyFlag(hostsPath);
            }

            string[] lines = File.ReadAllLines(hostsPath);

            var result = lines.Where(x => !x.Contains("ppy.sh"));

            File.WriteAllLines(hostsPath, result);
            if (roStatus)
            {
                EnableReadOnlyFlag(hostsPath);
            }
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
