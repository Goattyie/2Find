using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using System.Net;
using System.Net.Sockets;

namespace Game
{
    class Connection
    {
        public IPAddress Ip { get; set; }
        int Port { get; set; } = 4000;
        
        public Connection()
        {

        }
        public Connection(string Ip)
        {
            try
            {
                this.Ip = IPAddress.Parse(Ip);
            }
            catch(FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void ReceiveIp()
        {
            try
            {
                Ip = IPAddress.Parse(new WebClient().DownloadString("https://ipinfo.io/ip"));
            }catch(WebException ex)
            {
                foreach (IPAddress IPA in Dns.GetHostAddresses(Dns.GetHostName()))
                {
                    if (IPA.AddressFamily == AddressFamily.InterNetwork)
                    {
                        Ip = IPA;
                        break;
                    }
                }
            }

        }
    }
}
