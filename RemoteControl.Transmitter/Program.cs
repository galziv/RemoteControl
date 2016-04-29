using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace RemoteControl.Transmitter
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("123");
            const int PORT_NO = 11000;
            const string SERVER_IP = "127.0.0.1";
            //---data to send to the server---
            string textToSend = "50,100";

            //---create a TCPClient object at the IP and port no.---
            TcpClient client = new TcpClient(SERVER_IP, PORT_NO);
            NetworkStream nwStream = client.GetStream();
            byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(textToSend);

            //---send the text---
            Console.WriteLine("Sending : " + textToSend);
            nwStream.Write(bytesToSend, 0, bytesToSend.Length);

            client.Close();
        }
    }
}
