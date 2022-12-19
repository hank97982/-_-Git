using WinFormsAppClient.Controller;
using HankLibrary;
using System.Text;
using System.Configuration;
using System.Diagnostics;
using HankLibrary.SocketTool;

namespace WinFormsAppClient.Model
{
    public class MainModel
    {
        private MainController _controller;
        //private SockClientCallBack _sockClient;
        private SockClientCallBack _sockClientCallBack = null;
        private Stopwatch _stopwatch = new Stopwatch();

        public MainModel(MainController controller)
        {
            _controller = controller;
            _sockClientCallBack = new SockClientCallBack(Encoding.UTF8);
            _sockClientCallBack._receiveEvent += sockClientCallBack__receiveEvent;
            _sockClientCallBack.SocketStart("127.0.0.1", 9050);
            _sockClientCallBack.SocketKeepReceive();
        }

        private void sockClientCallBack__receiveEvent(SockClientCallBack serverAsync, ReceiveEventArgs e)
        {
            _stopwatch.Stop();
            Debug.WriteLine(e.resoult);
            _controller.AppendToListBox(e.resoult);
            _controller.AppendToListBox("發送到接收資料共花費"+_stopwatch.ElapsedMilliseconds.ToString()+"毫秒");
        }

        public void RequestSender(string formatStr)
        {
            try
            {
                _stopwatch.Start();
                _sockClientCallBack.SocketSend(formatStr);
                //receiveJsonStr = _sockClient.SocketReceive(4096);
            }
            catch (Exception ex)
            {
                _controller.MessageAlert(ex.Message);
            }
        }
    }
}
