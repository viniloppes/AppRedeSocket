using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AppRedeSocket.CLASSES
{
    public class ClientSocketConnection
    {
        private Socket _clientSocketServer { get; set; }
        private const int BUFFER_SIZE = 2048;
        private static readonly byte[] buffer = new byte[BUFFER_SIZE];

        public ClientSocketConnection(Socket socketServer)
        {
            _clientSocketServer = socketServer;
        }

        public static List<string> arrayReceiveResponse { get; set; }

    

        //public static List<string> arrayReceiveResponse = new List<string>();




        //private const int PORT = 100;

        //static void Main()
        //{
        //    Console.Title = "Client";
        //    ConnectToServer();
        //    RequestLoop();
        //    Exit();
        //}

        public bool ConnectToServer(string ipAddress, int porta)
        {
            int tentativas = 0;

            while (!_clientSocketServer.Connected)
            {
                try
                {
                    tentativas++;

                    // Change IPAddress.Loopback to a remote IP to connect to a remote host.
                    _clientSocketServer.Connect(IPAddress.Parse(ipAddress), porta);
                    _clientSocketServer.BeginReceive(buffer, 0, BUFFER_SIZE, SocketFlags.None, ReceiveResponse, _clientSocketServer);

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

     

        /// <summary>
        /// Close socket and exit program.
        /// </summary>
        public void Exit()
        {
            SendString("exit"); // Tell the server we are exiting
            _clientSocketServer.Shutdown(SocketShutdown.Both);
            _clientSocketServer.Close();
            //Environment.Exit(0);
        }

        public void SendRequest(string mensagem)
        {
            //Console.Write("Send a request: ");
            //string request = Console.ReadLine();
            SendString(mensagem);

            //if (mensagem.ToLower() == "exit")
            //{
            //    Exit();
            //}
        }

        /// <summary>
        /// Sends a string to the server with ASCII encoding.
        /// </summary>
        private void SendString(string text)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(text);
            _clientSocketServer.Send(buffer, 0, buffer.Length, SocketFlags.None);
        }

  


        private static void ReceiveResponse(IAsyncResult AR)
        {
            try
            {
                Socket current = (Socket)AR.AsyncState;
                var buffer = new byte[2048];
                int received = current.Receive(buffer, SocketFlags.None);
                if (received == 0) return;
                var data = new byte[received];
                Array.Copy(buffer, data, received);
                string text = Encoding.ASCII.GetString(data);
                DadosGerais.RecebeRespostaCliente(text);
                DadosGerais.listaMensagens.Add(text);
                //arrayReceiveResponse.Add(text);
                //ServerSocketConnection.MensagemPendente = false;
                //return AR;
                current.BeginReceive(buffer, 0, BUFFER_SIZE, SocketFlags.None, ReceiveResponse, current);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);
            }

            //Console.WriteLine(text);
        }
    }
}
