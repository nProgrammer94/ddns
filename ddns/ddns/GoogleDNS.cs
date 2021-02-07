using Serilog;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace ddns
{
    public class GoogleDNS
    {
        private readonly Domain domain;
        public GoogleDNS(Domain _domain)
        {
            domain = _domain;
        }
        public bool Request()
        {

            bool status = false;
            try
            {
                string content = string.Empty;     
                string ipv4 = GetExternalIPAddress();
                string url = string.Format("https://domains.google.com/nic/update?hostname={0}&myip={1}", domain.HostName, ipv4);

                WebClient wc = new WebClient();
                wc.Credentials = new NetworkCredential(domain.UserName.Trim(), domain.Password.Trim());
                var response = wc.DownloadString(url);
                if (response.Contains("good") || response.Contains("nochg"))
                    status = true;
            }
            catch (Exception ex)
            {
                status = false;
                Log.Error(ex.Message);
            }
            return status;
        }
        public static string GetExternalIPAddress()
        {
            string result = string.Empty;

            string[] checkIPUrl =
            {
    "https://ipinfo.io/ip",
    "https://checkip.amazonaws.com/",
    "https://api.ipify.org",
    "https://icanhazip.com",
    "https://wtfismyip.com/text"
};

            using (var client = new WebClient())
            {
                client.Headers["User-Agent"] = "Mozilla/4.0 (Compatible; Windows NT 5.1; MSIE 6.0) " +
                    "(compatible; MSIE 6.0; Windows NT 5.1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";

                foreach (var url in checkIPUrl)
                {
                    try
                    {
                        result = client.DownloadString(url);
                    }
                    catch
                    {
                    }

                    if (!string.IsNullOrEmpty(result))
                        break;
                }
            }

            return result.Replace("\n", "").Trim();
        }
    }
}
