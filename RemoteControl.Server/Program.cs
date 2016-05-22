using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;
using RemoteControl.Actions;
using RemoteControl.Server.Helpers;

namespace RemoteControl.Server
{
    class Program
    {
        private static TcpListener listener;
        private static TcpClient client;
        private static Mouse mouse;
        private static Keyboard keyboard;
        private static IDisposable webApp;
        static void Main(string[] args)
        {
            var webServerPortValue = ConfigurationManager.AppSettings.Get("webServerPort") ?? "3333";
            var listeningPortValue = ConfigurationManager.AppSettings.Get("listeningPort") ?? "3334";

            int webServerPort;
            int listeningPort;

            mouse = new Mouse();
            keyboard = new Keyboard();

            if (!int.TryParse(webServerPortValue, out webServerPort))
            {
                Console.WriteLine("Value for webServerPort in app.config must be a valid port number");
                return;
            }

            if (!int.TryParse(listeningPortValue, out listeningPort))
            {
                Console.WriteLine("Value for listeningPort in app.config must be a valid port number");
                return;
            }

            Console.WriteLine("Initializing...");
            try
            {
                StartWebServer(webServerPort);
            }
            catch (MissingWebServerRootPathException webServerException)
            {
                Console.WriteLine("please specify webserver path root in app.config key 'webServerRootPath'.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Cannot start web server. Verify port {webServerPort} is available.");
                Console.WriteLine($"Try to execute from cmd: 'netstat -an | findstr {webServerPort}'. If there are results kill the process which uses the port or change the webServerPort at app.config.");
                Console.WriteLine(ex);
                return;
            }

            RunServerAsync(listeningPort);
            Console.WriteLine("Ready");
            Console.WriteLine("Press any enter to close...");
            Console.ReadLine();

            client?.Close();

            listener.Stop();
            webApp.Dispose();
        }

        private static void StartWebServer(int port)
        {
            StartOptions options = new StartOptions();
            options.Urls.Add($"http://+:{port}");
            webApp = WebApp.Start<Startup>(options);
            Console.WriteLine("Web Server is running.");
        }

        private static void HandleClientAsync(TcpClient client)
        {
            //---get the incoming data through a network stream---
            NetworkStream nwStream = client.GetStream();
            byte[] buffer = new byte[client.ReceiveBufferSize];

            //---read incoming stream---
            int bytesRead = nwStream.Read(buffer, 0, client.ReceiveBufferSize);

            //---convert the data received into a string---
            string dataReceived = Encoding.ASCII.GetString(buffer, 0, bytesRead);
            Console.WriteLine("Received : " + dataReceived);

            var splitted = dataReceived.Split('|');

            if (splitted.Length == 1)
            {
                switch (splitted[0])
                {
                    case "LeftClick":
                        mouse.LeftClick();
                        break;
                    case "RightClick":
                        mouse.RightClick();
                        break;
                    case "Mute":
                        keyboard.PressMute();
                        break;
                    case "VolumeUp":
                        keyboard.PressVolumeUp();
                        break;
                    case "VolumeDown":
                        keyboard.PressVolumeDown();
                        break;
                    case "Left":
                        keyboard.PressLeft();
                        break;
                    case "Right":
                        keyboard.PressRight();
                        break;
                    case "Up":
                        keyboard.PressUp();
                        break;
                    case "Down":
                        keyboard.PressDown();
                        break;
                }
            }
            else
            {
                switch (splitted[0])
                {
                    case "key":
                        KeyHelper.Handle(splitted[1]);
                        break;
                    case "media":
                        MediaHelper.Handle(splitted[1]);
                        break;
                    case "sys":
                        SysHelper.Handle(splitted[1]);
                        break;
                    default:
                        mouse.MoveBy(int.Parse(splitted[0]), int.Parse(splitted[1]));
                        break;
                }
            }
        }

        private static async void RunServerAsync(int port)
        {
            IPAddress localAdd = IPAddress.Parse("127.0.0.1");
            listener = new TcpListener(localAdd, port);
            listener.Start();
            Console.WriteLine("Listening...");

            while (true)
            {
                try
                {
                    client = await listener.AcceptTcpClientAsync();
                    HandleClientAsync(client);
                }
                catch (ObjectDisposedException ex)
                {
                }
                catch (InvalidOperationException ex)
                {
                }
            }
        }
    }
}
