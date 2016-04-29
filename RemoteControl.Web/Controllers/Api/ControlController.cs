using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Web.Http;
using RemoteControl.Actions;

namespace RemoteControl.Web.Controllers.Api
{
    public class ControlController : ApiController
    {
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

        private void SendData(string data)
        {
            TcpClient client = new TcpClient("127.0.0.1", 11000);
            NetworkStream nwStream = client.GetStream();
            byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(data);

            nwStream.Write(bytesToSend, 0, bytesToSend.Length);
            client.Close();
        }
    }
}