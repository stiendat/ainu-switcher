
namespace AinuSwitcher
{
    public class Constants
    {
        // Use this address if we cannot grab from ips.txt
        public const string AinuHardcodedIp = "18.225.10.113";

        // Grab address from here when possible
        public const string AinuIpApiAddress = "https://old.ainu.pw/ips.txt";

        public const string UiInstallCertificate = "Install Certificate";

        public const string UiUninstallCertificate = "Delete Certificate";

        public const string UiYouArePlayingOnAinu = "You're connected to Ainu!";

        public const string UiYouArePlayingOnOfficial = "You're playing on Bancho";

        public const string UiSwitchToAinu = "Connect to Ainu!";

        public const string UiSwitchToOfficial = "Disconnect from Ainu!";

        public const string UiUpdatingStatus = "Retrieving addresses..";
    }
}
