using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using GameServer.Views.Handlers;
using GameServer.Controllers.Servers;

namespace GameServer.Controllers.Servers
{
    /// <summary>
    /// The server controlls the communication with the clients.
    /// </summary>
    public class TcpServer : IServer
    {
        /// <summary>
        /// Tcp listener.
        /// </summary>
        private TcpListener listener;

        /// <summary>
        /// Reference to a client handler.
        /// </summary>
        private IClientHandler clientHandler;

        public void Start(string ip, int port, IClientHandler clientHandler)
        {
            //Initialize connection settings.
            IPEndPoint ep = new
                IPEndPoint(IPAddress.Parse(ip), port);
            listener = new TcpListener(ep);

            //Start listening for connections.
            listener.Start();
            Console.WriteLine("Waiting for connections...");

            //The server task.
            this.ServerTask = new Task(() =>
            {
                while (true)
                {
                    try
                    {
                        //Accept new connection.
                        TcpClient client = listener.AcceptTcpClient();
                        Console.WriteLine("Got new connection");
                        clientHandler.HandleClient(client);
                    }
                    catch (SocketException)
                    {
                        break;
                    }
                }
                Console.WriteLine("Server stopped");
            });
            this.ServerTask.Start();
        }

        /// <summary>
        /// Wait for the server task to finish.
        /// </summary>
        public void Wait()
        {
            ServerTask.Wait();
        }

        /// <summary>
        /// Server task property
        /// </summary>
        /// <value>Task.</value>
        public Task ServerTask { get; private set; }

        /// <summary>
        /// Stops the server activity.
        /// </summary>
        public void Stop()
        {
            listener.Stop();
        }
    }
}