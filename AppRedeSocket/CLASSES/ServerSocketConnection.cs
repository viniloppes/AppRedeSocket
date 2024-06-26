﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AppRedeSocket.CLASSES
{
    public class ServerSocketConnection
    {
        public Socket _socketServer { get; set; }

        public static readonly List<Socket> clientSockets = new List<Socket>();
        private const int BUFFER_SIZE = 2048;
        private static readonly byte[] buffer = new byte[BUFFER_SIZE];

        public static List<string> mensagensRecebidas;

        public static bool MensagemPendente { get; set; }

        private List<string> arrayReceiveResponse = new List<string>();

        //public List<string> ArrayReceiveResponse
        //{
        //    get { return arrayReceiveResponse; }
        //    set { arrayReceiveResponse = value; }
        //}

        public ServerSocketConnection(Socket socketServer)
        {
            _socketServer = socketServer;
        }




        //public static void Main()
        //{
        //    //Console.Title = "Server";
        //    //SetupServer();
        //    /*Console.ReadLine();*/ // When we press enter close everything
        //    CloseAllSockets();
        //}

        public void SetupServer(string ipAddress, string port)
        {
            //Console.WriteLine("Setting up server...");
            _socketServer.Bind(new IPEndPoint(IPAddress.Parse(ipAddress), int.Parse(port)));
            _socketServer.Listen(0);
            _socketServer.BeginAccept(AcceptCallback, null);

        }



        /// <summary>
        /// Close all connected client (we do not need to shutdown the server socket as its connections
        /// are already closed with the clients).
        /// </summary>
        public void CloseAllSockets()
        {
            if (clientSockets.Count != 0)
            {
                foreach (Socket socket in clientSockets)
                {
                    socket.Shutdown(SocketShutdown.Both);
                    socket.Close();
                }
            }

            _socketServer.Close();
        }

        private void AcceptCallback(IAsyncResult AR)
        {
            Socket socket;

            try
            {
                socket = _socketServer.EndAccept(AR);
            }
            catch (ObjectDisposedException) // I cannot seem to avoid this (on exit when properly closing sockets)
            {
                return;
            }

            clientSockets.Add(socket);
            DadosGerais.clientSockets.Add(socket);
            socket.BeginReceive(buffer, 0, BUFFER_SIZE, SocketFlags.None, ReceiveCallback, socket);
            DadosGerais.EnviaMensagem("Cliente conectado, esperando por conexão...");
            _socketServer.BeginAccept(AcceptCallback, null);


        }

        private static void ReceiveCallback(IAsyncResult AR)
        {
            Socket current = (Socket)AR.AsyncState;
            int received;

            try
            {
                received = current.EndReceive(AR);
            }
            catch (SocketException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);
                current.Close();
                clientSockets.Remove(current);
                return;
            }

            byte[] recBuf = new byte[received];
            Array.Copy(buffer, recBuf, received);
            string text = Encoding.ASCII.GetString(recBuf);

            //if (text.ToLower() == "get time") // Client requested time
            //{

            //    byte[] data = Encoding.ASCII.GetBytes(DateTime.Now.ToLongTimeString());
            //    current.Send(data);

            //}
            //else


            if (text.ToLower() == "exit") // Client wants to exit gracefully
            {
                // Always Shutdown before closing
                current.Shutdown(SocketShutdown.Both);
                current.Close();
                clientSockets.Remove(current);
                DadosGerais.RetornoServidor("Um cliente desconectado");
                //Console.WriteLine("Client disconnected");
                DadosGerais.EnviaMensagem("Cliente Desconectado");
                return;
            }
            //else
            //{
            byte[] data = Encoding.ASCII.GetBytes(text);

            foreach (Socket currentClient in clientSockets)
            {
                currentClient.Send(data);
            }
            DadosGerais.listaMensagensServer.Add(text);
            DadosGerais.RetornoServidor(text);

            //MensagemPendente = true;
            //}

            current.BeginReceive(buffer, 0, BUFFER_SIZE, SocketFlags.None, ReceiveCallback, current);
        }
    }
}
