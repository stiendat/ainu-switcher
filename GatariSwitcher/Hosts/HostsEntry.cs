namespace GatariSwitcher.Hosts
{
    class HostsEntry
    {
        public HostsEntry(string address, string host, string comment)
        {
            this.Address = address;
            this.Host = host;
            this.Comment = comment;
        }

        public string Address { get; set; }

        public string Host { get; set; }

        public string Comment { get; set; }
    }
}
