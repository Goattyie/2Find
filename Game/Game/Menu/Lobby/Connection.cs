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
        public Vector2f CurrentPos { get; set; } = new Vector2f();
        public bool Cycle { get; set; }
        public int GameStatus { get; set; }
        static public bool ThreadStop { get; set; }
        static public bool Connected { get; set; }
        static public bool StartAsClient { get; set; }
        Socket Socket { get; set; }
        int Port { get; set; } = 4444;
        EndPoint SavedEndPoint;
        EndPoint TempEP; 
        byte[] buffer { get; set; }
        float[] floatPos { get; set; } = new float[2];
        Task LobbyListen { get; set; }
        Task GameThread { get; set; }

        public Connection()
        {
            Socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            ReceiveIp();
            Socket.Bind(new IPEndPoint(IPAddress.Any, Port));
        }

        public Connection(string Ip)
        {
            Socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            this.Ip = IPAddress.Parse(Ip);
            SavedEndPoint = new IPEndPoint(this.Ip, Port);
        }

        public void Listen()
        {
            try
            {
                buffer = new byte[256];
                TempEP= new IPEndPoint(IPAddress.Any, 0);
                Socket.ReceiveFrom(buffer, ref TempEP);
                SavedEndPoint = TempEP;
                Connected = true;
                if (Encoding.UTF8.GetString(buffer).TrimEnd('\0') == "go")
                {
                    StartAsClient = true;
                    Socket.ReceiveTimeout = 5000;
                    return;
                }
                if (!ThreadStop)
                {
                    Socket.SendTo(Encoding.UTF8.GetBytes("ok"), TempEP);
                }
            }
            catch (Exception ex)
            {
                Connected = false;
                Console.Write("?");
            }
        }

        public void SendConnection()
        {
            Socket.ReceiveTimeout = 2000;
            try
            {
                Socket.SendTo(Encoding.UTF8.GetBytes("ok"), SavedEndPoint);
            }
            catch (Exception ex) 
            {
                Console.WriteLine("SendConnection: " + ex.Message);
            }
        }

        public void GameEnd()
        {
            ThreadStop = true;
            GameThread.Wait();
            ThreadStop = false;
        }

        async public void StartLobbyThread()
        {
            ThreadStop = false;
            Socket.ReceiveTimeout = 2000;
            StartAsClient = false;
            LobbyListen = Task.Run(() =>
            {
                while (!ThreadStop)
                {
                    Listen();
                }
            });
            await LobbyListen;
        }
   

        public async void StartGameThread()
        {
            GameThread = Task.Run(() =>
            {
                while (Connected && !ThreadStop)
                {
                    if (Cycle)
                    {
                        Send();
                        ReceiveCoordinates();
                        Cycle = false;
                    }
                }
            });
            await GameThread;
        }

        public int GetSeed()
        {
            int seed;
            if (!StartAsClient)
            {
                seed = DateTime.Now.Day + DateTime.Now.Millisecond + DateTime.Now.Second;
                Send(seed);
            }
            else
                seed = ReceiveInt();
            return seed;
        }

        public void SendStart()
        {
            ThreadStop = true;
            LobbyListen.Wait();
            ThreadStop = false;
            Socket.ReceiveTimeout = 5000;
            byte[] start = Encoding.UTF8.GetBytes("go");
            Socket.SendTo(start, SavedEndPoint);
        }

        public void Send(int value)
        {
            try
            {
                byte[] bytes = new byte[255];
                bytes = BitConverter.GetBytes(value);
                Socket.SendTo(bytes, SavedEndPoint);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Send(int): " + ex.Message);
            }
        }

        public void Send()
        {
            try
            {
                floatPos[0] = CurrentPos.X;
                floatPos[1] = CurrentPos.Y;
                byte[] bytecoord = new byte[8];
                Buffer.BlockCopy(floatPos, 0, bytecoord, 0, bytecoord.Length);
                Socket.SendTo(bytecoord, SavedEndPoint);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Send: " + ex.Message);
            }
        }

        public int ReceiveInt()
        {
            try
            {
                int value;
                buffer = new byte[255];
                TempEP = new IPEndPoint(IPAddress.Any, 0);
                Socket.ReceiveFrom(buffer, ref TempEP);
                value = BitConverter.ToInt32(buffer, 0);
                return value;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Receive is failed");
                Connected = false;
                return 0;
            }
        }
        
        public void ReceiveCoordinates()
        {
            try
            {
                float[] newcoord = new float[2];
                buffer = new byte[8];
                TempEP = SavedEndPoint;
                Socket.ReceiveFrom(buffer, ref TempEP);
                int value = BitConverter.ToInt32(buffer, 0);
                if (value == 1 || value == 2)
                { 
                    GameStatus = value;
                    ThreadStop = true;
                    return;
                }
                Buffer.BlockCopy(buffer, 0, newcoord, 0, buffer.Length);
                ReceivedPos = new Vector2f(newcoord[0], newcoord[1]);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ReceiveCoordinates: " + ex.Message);
                if (!ThreadStop)
                {
                    Connected = false;
                    Console.WriteLine("Another player left the game");
                }
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
    }
}
