using System.Configuration;
using System.Net.Sockets;
using System.Text;
using System.Web.Http;

namespace RemoteControl.Server.WebServer
{
    public class ControlController : ApiController
    {
        string listeningPortValue = ConfigurationManager.AppSettings.Get("listeningPort") ?? "3334";

        public class Point
        {
            public int X { get; set; }
            public int Y { get; set; }
        }

        [HttpGet]
        public void MoveMouseBy(int xDelta, int yDelta)
        {
            //string textToSend = $"{point.X},{point.Y}";
            SendData($"{xDelta},{yDelta}");
        }

        [HttpGet]
        public void MouseLeftClick()
        {
            SendData("LeftClick");
        }

        [HttpGet]
        public void MouseRightClick()
        {
            SendData("RightClick");
        }

        [HttpGet]
        public void KeyboardMute()
        {
            SendData("Mute");
        }

        [HttpGet]
        public void KeyboardVolumeUp()
        {
            SendData("VolumeUp");
        }

        [HttpGet]
        public void KeyboardVolumeDown()
        {
            SendData("VolumeDown");
        }

        [HttpGet]
        public void KeyboardUp()
        {
            SendData("Up");
        }

        [HttpGet]
        public void KeyboardDown()
        {
            SendData("Down");
        }

        [HttpGet]
        public void KeyboardLeft()
        {
            SendData("Left");
        }

        [HttpGet]
        public void KeyboardRight()
        {
            SendData("Right");
        }

        [HttpGet]
        public void KeyboardKey(string key)
        {
            SendData($"key,{key}");
        }

        private void SendData(string data)
        {
            TcpClient client = new TcpClient("127.0.0.1", int.Parse(listeningPortValue));
            NetworkStream nwStream = client.GetStream();
            byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(data);

            nwStream.Write(bytesToSend, 0, bytesToSend.Length);
            client.Close();
        }
    }
}