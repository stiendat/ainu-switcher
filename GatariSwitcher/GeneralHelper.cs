using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace GatariSwitcher
{
    static class GeneralHelper
    {
        public static async Task<string> GetActualGatariAddress()
        {
            string api = @"https://osu.gatari.pw/api/v1/ip";
            using (var client = new WebClient())
            {
                var result = await client.DownloadStringTaskAsync(api);
                result.Trim();
                return result;
            }
        }

    }
}
