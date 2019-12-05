using System.Net;
using System.Threading.Tasks;

namespace AinuSwitcher
{
    static class GeneralHelper
    {
        public async static Task<string> GetAinuAddressAsync()
        {
            using (var webClient = new WebClient())
            {
                string result = string.Empty;
                try
                {
                    var line = await webClient.DownloadStringTaskAsync(Constants.AinuIpApiAddress);
                    result = line;
                }
                catch { }
                return result.Trim();
            }
        }
    }
}