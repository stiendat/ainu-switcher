
namespace AinuSwitcher
{
    public class Constants
    {
        // Use this address if we cannot grab from ips.txt
        public const string AinuHardcodedIp = "35.221.158.170";

        // Grab address from here when possible
        public const string AinuIpApiAddress = "https://old.osuvnfc.xyz/ips.txt";

        public const string UiInstallCertificate = "Install Certificate";

        public const string UiUninstallCertificate = "Delete Certificate";

        public const string UiYouArePlayingOnAinu = "You're connected to OsuVNFC!";

        public const string UiYouArePlayingOnOfficial = "You're playing on Bancho";

        public const string UiSwitchToAinu = "Connect to OsuVNFC!";

        public const string UiSwitchToOfficial = "Disconnect from OsuVNFC!";

        public const string UiUpdatingStatus = "Retrieving addresses..";
    }
}
