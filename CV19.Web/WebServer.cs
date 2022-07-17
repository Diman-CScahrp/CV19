using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace CV19.Web
{
    internal class WebServer
    {
        //private TcpListener _Listener = new TcpListener(new IPEndPoint(IPAddress.Any, 8000));
        private HttpListener _Listener;
        private readonly int _Port;
        private bool _Enabled;
        private readonly object _SyncRoot = new object();
        public int Port => _Port;
        public bool Enabled { get => _Enabled; set { if (value) Start(); else Stop(); } }

        public WebServer(int Port) => _Port = Port;
        public void Start() 
        {
            if (_Enabled) return;
            lock (_SyncRoot)
            {
                if (_Enabled) return;
                _Listener = new HttpListener();
                _Listener.Prefixes.Add($"http://*:{_Port}");
                _Listener.Prefixes.Add($"http://+:{_Port}");
                _Enabled = true;
            }
            Listen();
        }
        public void Stop() 
        {
            if (!_Enabled) return;
            lock (_SyncRoot)
            {
                if (!_Enabled) return;

                _Listener = null;
                _Enabled = false;
            }
        }
        private void Listen()
        {

        }
    }
}
