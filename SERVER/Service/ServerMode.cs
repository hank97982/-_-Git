using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SERVER
{
    public delegate void ClientReceiveEventHandler(ServerMode clientAsync, ReceiveEventArgs e);

    public class ServerMode
    {   
        private TcpListener server;
        private TcpListener threadListener;
        private NetworkStream ns;
        private int connections = 0;
        private string stringData = "";
        private Encoding _encoding;
        public event ClientReceiveEventHandler _receiveEvent;

        public ServerMode(Encoding encoding)
        {
            _encoding = encoding;
            server = new TcpListener(IPAddress.Any, 9050);
            server.Start();
            Task ta = new Task(Connecter);
            ta.Start();
        }

        public void SocketSend(string sendString)
        {
            //發送資料至伺服器端
            int recv;
            byte[] data = _encoding.GetBytes(sendString);
            ns.Write(data, 0, data.Length);
        }

        private void Connecter()
        {
            while (true)
            {
                Thread.Sleep(1000);
                threadListener = this.server;
                ThreadPool.QueueUserWorkItem(new
                WaitCallback(HandleConnection));
            }
        }

        private void HandleConnection(object? state)
        {
            try
            {
                int recv;
                TcpClient client = threadListener.AcceptTcpClient();
                ns = client.GetStream();
                connections++;

                while (true)
                {
                    Thread.Sleep(1000);
                    if (this._receiveEvent != null)
                    {
                        byte[] data = new byte[client.ReceiveBufferSize];
                        recv = ns.Read(data, 0, data.Length);
                        if (recv == 0)
                            break;
                        ReceiveEventArgs e = new ReceiveEventArgs();
                        e.resoult = _encoding.GetString(data, 0, recv);
                        this._receiveEvent.Invoke(this, e);
                    }
                }
                ns.Close();
                client.Close();
                connections--;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                return;
            }
        }
    }
    public class ReceiveEventArgs
    {
        public string resoult { get; set; }
    }
}
