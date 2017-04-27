using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GatariSwitcher.Hosts
{
    class HostsFile
    {
        private string filePath;
        
        public readonly List<HostsEntry> Items;

        private HostsFile()
        {
            Items = new List<HostsEntry>();
        }

        public static HostsFile Open(string filepath)
        {
            HostsFile result = new HostsFile();
            result.filePath = filepath;

            string[] lines = File.ReadAllLines(filepath);
            foreach (string line in lines)
            {
                string trim = line.Trim();
                if (trim.StartsWith("#"))
                {
                    result.Items.Add(new HostsEntry(null, null, trim));
                }
                else if (line.Contains(' '))
                {
                    string[] split = line.Split(new[] { ' ' }, 2);
                    string address = split[0].Trim();
                    string host = null;
                    string comment = null;
                    if (split[1].Contains(' '))
                    {
                        string[] spl = split[1].Split(new[] { ' ' }, 2);
                        host = spl[0].Trim();
                        comment = spl[1].Trim();
                    }
                    else
                    {
                        host = split[1].Trim();
                    }
                    result.Items.Add(new HostsEntry(address, host, comment));
                }
            }

            return result;
        }

        public void Write()
        {
            var lines = new List<string>();
            foreach (var item in Items)
            {
                if (item.Address != null && item.Host != null)
                {
                    var sb = new StringBuilder();
                    sb.AppendFormat("{0} {1}", item.Address, item.Host);
                    if (item.Comment != null)
                    {
                        sb.AppendFormat(" {0}", (item.Comment.StartsWith("#") ? item.Comment : "#" + item.Comment));
                    }
                    lines.Add(sb.ToString());
                }
                else if (item.Comment != null && item.Comment.StartsWith("#"))
                {
                    lines.Add(item.Comment);
                }
            }

            bool isReadOnly = GetReadOnlyFlagStatus(filePath);
            if (isReadOnly)
            {
                DisableReadOnlyFlag(filePath);
            }

            File.WriteAllLines(filePath, lines);
            if (isReadOnly)
            {
                EnableReadOnlyFlag(filePath);
            }
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
