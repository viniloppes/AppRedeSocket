using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AppRedeSocket.CLASSES
{
    public static class ClientSocketConnection
    {
        private static readonly Socket ClientSocket = new Socket
           (AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        //private const int PORT = 100;

        //static void Main()
        //{
        //    Console.Title = "Client";
        //    ConnectToServer();
        //    RequestLoop();
        //    Exit();
        //}

        public static bool ConnectToServer(string ipAddress, int porta)
        {
            int tentativas = 0;

            while (!ClientSocket.Connected)
            {
                try
                {
                    tentativas++;
                    
                    // Change IPAddress.Loopback to a remote IP to connect to a remote host.
                    ClientSocket.Connect(IPAddress.Parse(ipAddress), porta);
                }
                catch (SocketException)
                {
                    Console.Clear();
                    return false;
                }
            }

            //Console.Clear();
            //Console.WriteLine("Connected");
            return true;
        }

        private static void RequestLoop()
        {
            //Console.WriteLine(@"<Type ""exit"" to properly disconnect client>");

            while (true)
            {
                //SendRequest();
                ReceiveResponse();
            }
        }

        /// <summary>
        /// Close socket and exit program.
        /// </summary>
        private static void Exit()
        {
            SendString("exit"); // Tell the server we are exiting
            ClientSocket.Shutdown(SocketShutdown.Both);
            ClientSocket.Close();
            //Environment.Exit(0);
        }

        public static void SendRequest(string mensagem)
        {
            //Console.Write("Send a request: ");
            //string request = Console.ReadLine();
            SendString(mensagem);

            if (mensagem.ToLower() == "exit")
            {
                Exit();
            }
        }

        /// <summary>
        /// Sends a string to the server with ASCII encoding.
        /// </summary>
        private static void SendString(string text)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(text);
            ClientSocket.Send(buffer, 0, buffer.Length, SocketFlags.None);
        }

        public static string ReceiveResponse()
        {
            var buffer = new byte[2048];
            int received = ClientSocket.Receive(buffer, SocketFlags.None);
            if (received == 0) return "";
            var data = new byte[received];
            Array.Copy(buffer, data, received);
            string text = Encoding.ASCII.GetString(data);
            return text;
            //Console.WriteLine(text);
        }
    }
}
