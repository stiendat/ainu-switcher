using System;
using System.Collections.Generic;
using System.IO;

namespace GatariSwitcher
{
    class ServerSwitcher
    {
        const string GATARI_ADDRESS = "185.188.182.238";

        public void SwitchToGatari()
        {
            string hostsPath = getHostsPath();

            string[] lines = File.ReadAllLines(hostsPath);
            List<string> result = new List<string>();

            foreach (var i in lines)
            {
                if (!i.Contains("ppy.sh"))
                {
                    result.Add(i);
                }
            }

            result.Add(GATARI_ADDRESS + "   osu.ppy.sh");
            result.Add(GATARI_ADDRESS + "   c.ppy.sh");
            result.Add(GATARI_ADDRESS + "   c1.ppy.sh");
            result.Add(GATARI_ADDRESS + "   a.ppy.sh");
            result.Add(GATARI_ADDRESS + "   i.ppy.sh");

            File.WriteAllLines(hostsPath, result);
        }

        public void SwitchToOfficial()
        {
            string hostsPath = getHostsPath();

            string[] lines = File.ReadAllLines(hostsPath);
            List<string> result = new List<string>();

            foreach (var i in lines)
            {
                if (!i.Contains("ppy.sh"))
                {
                    result.Add(i);
                }
            }

            File.WriteAllLines(hostsPath, result); 
        }

        /// <summary>
        /// Get current osu! server
        /// </summary>
        /// <returns>true - gatari, false - official</returns>
        public bool GetCurrentServer()
        {
            string[] lines = File.ReadAllLines(getHostsPath());
            foreach (var i in lines)
            {
                if (i.Contains("osu.ppy.sh") && !i.Contains("#"))
                {
                    return true;
                }
            }
            return false;
        }

        private string getHostsPath()
        {
            string windir = Environment.GetFolderPath(Environment.SpecialFolder.Windows);
            string result = Path.Combine(windir, "System32", "drivers", "etc", "hosts");

            return result;
        }
    }
}
