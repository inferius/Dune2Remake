using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;

namespace Dune2RemakeTest.Multiplayer
{
    public class Server
    {
        public int Port { get; set; }
        public string PublicIP { get; private set; }
        public List<Error> ErrorList { get; private set; }
        public string ServerId { get; private set; }
        public Stack<string> ServerConsoleStack { get; private set; }

        private TcpListener _serverSocket;
        private TcpClient _clientSocket;

        public Server(int port = 26542)
        {
            Port = port;
            ServerConsoleStack = new Stack<string>();
            ErrorList = new List<Error>(100);
            GetExternalIP();
            ServerId = Guid.NewGuid().ToString();
        }

        public void Start()
        {
            if (string.IsNullOrEmpty(PublicIP))
            {
                var er = new Error(ErrorStatus.NetworkError, "IP Address not valid.", "PUBLIC_IP_NOT_VALID");
                ErrorList.Add(er);
                ServerConsoleStack.Push(er.ToString());
                return;
            }

            _serverSocket = new TcpListener(IPAddress.Parse(PublicIP), Port);
            _clientSocket = default(TcpClient);
            
            _serverSocket.Start();
            ServerConsoleStack.Push(string.Format("Server started on {0}:{1}", PublicIP, Port));
            ulong counter = 0;

            while ((counter + 1) < ulong.MaxValue)
            {
                counter++;
                _serverSocket.AcceptTcpClientAsync();
            }

        }

        private async void GetExternalIP()
        {
            try
            {
                PublicIP = await (new WebClient()).DownloadStringTaskAsync("http://checkip.dyndns.org/");
                PublicIP = (new Regex(@"\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}")).Matches(PublicIP)[0].ToString();
            }
            catch
            {
                PublicIP = null;
                ErrorList.Add(new Error(ErrorStatus.NetworkError, "Public IP not detect.", "NO_PUBLIC_IP_DETECT"));
            }
        }
    }
}
