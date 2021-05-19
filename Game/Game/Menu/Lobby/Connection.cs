using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using SFML.System;

namespace Game
{
    class Connection
    {
        public IPAddress Ip { get; set; }
        public Vector2f ReceivedPos { get; set; } = new Vector2f();
        public bool ThreadStop { get; set; }
        public bool Connected { get; set; }
        public bool Start { get; set; }
        int Port { get; set; } = 4444;
        int size { get; set; }
        Socket Socket { get; set; }
        IPEndPoint ServerEndPoint { get; set; }
        EndPoint SenderEndPoint;
        byte[] buffer { get; set; }
        byte[] msg { get; set; } = Encoding.UTF8.GetBytes("ok");
        float[] floatPos { get; set; } = new float[2];
        StringBuilder data { get; set; }

        public Connection()
        {
            ConnectionSettings();
            ReceiveIp();
            ServerEndPoint = new IPEndPoint(IPAddress.Any, Port);
            Socket.Bind(ServerEndPoint);
        }

        public Connection(string Ip)
        {
            ConnectionSettings();
            this.Ip = IPAddress.Parse(Ip);
            ServerEndPoint = new IPEndPoint(this.Ip, Port);
        }

        public void ConnectionSettings()
        {
            Socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            Socket.ReceiveTimeout = 2000;
            data = new StringBuilder();
        }

        public void Listen()
        {
            try
            {
                buffer = new byte[2];
                data.Clear();
                Socket.ReceiveFrom(buffer, ref SenderEndPoint);
                data.Append(Encoding.UTF8.GetString(buffer));
                if (data.ToString() == "go")
                {
                    Start = true;
                }
                Socket.SendTo(msg, SenderEndPoint);
                Connected = true;
            }
            catch (Exception ex)
            {
                Connected = false;
                Console.Write("?");
            }
        }

        public void SendConnection()
        {
            Socket.SendTo(msg, ServerEndPoint);
            SenderEndPoint = ServerEndPoint;
        }

        async public void StartLobbyThread()
        {
            SenderEndPoint = new IPEndPoint(IPAddress.Any, 0);
            await Task.Run(() =>
            {
                while (!ThreadStop)
                {
                    Listen();
                }
                ThreadStop = false;
            });
        }

        public void SendStart()
        {
            byte[] start = Encoding.UTF8.GetBytes("go");
            Socket.SendTo(start, SenderEndPoint);
        }

        public async void StartReceiving()
        {
            Socket.ReceiveTimeout = 5000;
            await Task.Run(() =>
            {
                while (Connected)
                {
                    if (size != 0)
                    {
                        Receive();
                        size = 0;
                    }
                }
            });
        }

        public void Send(float x, float y)
        {
            try
            {
                floatPos[0] = x;
                floatPos[1] = y;
                byte[] bytecoord = new byte[8];
                Buffer.BlockCopy(floatPos, 0, bytecoord, 0, bytecoord.Length);
                size = Socket.SendTo(bytecoord, SenderEndPoint);
            }
            catch (Exception ex)
            {
            }
        }

        public void Receive()
        {
            try
            {
                float[] newcoord = new float[2];
                buffer = new byte[8];
                SenderEndPoint = new IPEndPoint(IPAddress.Any, 0);
                int size = Socket.ReceiveFrom(buffer, ref SenderEndPoint);
                Buffer.BlockCopy(buffer, 0, newcoord, 0, buffer.Length);
                ReceivedPos = new Vector2f(newcoord[0], newcoord[1]);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Another player left the game");
                Connected = false;
            }
        }

        public void ReceiveIp()
        {
            try
            {
                Ip = IPAddress.Parse(new WebClient().DownloadString("https://ipinfo.io/ip"));
            }
            catch (WebException ex)
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

        /*public void SendReceive()
        {
            try
            {
                floatPos[0] = CurrentPos.X;
                floatPos[1] = CurrentPos.Y;
                byte[] bytecoord = new byte[8];
                Buffer.BlockCopy(floatPos, 0, bytecoord, 0, bytecoord.Length);
                Socket.SendTo(bytecoord, SenderEndPoint);

                float[] newcoord = new float[2];
                buffer = new byte[8];
                Socket.ReceiveFrom(buffer, 8, SocketFlags.None, ref SenderEndPoint);
                Buffer.BlockCopy(buffer, 0, newcoord, 0, buffer.Length);
                ReceivedPos = new Vector2f(newcoord[0], newcoord[1]);
            }
            catch (Exception ex)
            {
            }
        }*/
    }
}
