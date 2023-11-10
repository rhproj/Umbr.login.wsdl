using System.Net.NetworkInformation;
using System.Net;

namespace Umbr2.Site.Services;

public static class NetworkService
{
    public static string GetIPAddress()
    {
        if (!NetworkInterface.GetIsNetworkAvailable())
        {
            throw new NetworkInformationException();
        }

        IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());

        return string.Join<IPAddress>(";", host.AddressList);
    }
}
