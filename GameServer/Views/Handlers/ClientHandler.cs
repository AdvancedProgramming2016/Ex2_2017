using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using GameServer.Views.Handlers;
using GameServer.Controllers.Invokers;
using GameServer.Controllers.Servers;

namespace GameServer.Views.Handlers
{
    /// <summary>
    /// Handles basic client requests.
    /// </summary>
    public class ClientHandler : IClientHandler
    {
        /// <summary>
        /// Reference to the contoller of the server.
        /// </summary>
        private readonly IController controller;

        /// <summary>
        /// The command received from the client.
        /// </summary>
        private string commandLine;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="controller"></param>
        public ClientHandler(IController controller)
        {
            this.controller = controller;
        }

        public void HandleClient(TcpClient client)
        {
            //Starts the client handler task.
            new Task(() =>
            {
                //Initializes the streams.
                using (NetworkStream stream = client.GetStream())
                using (StreamReader reader = new StreamReader(stream))
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    //Contains the connected client data.
                    ConnectedClient connectedClient =
                        new ConnectedClient(client, writer);

                    //Loop while the client is connected.
                    while (connectedClient.IsConnected)
                    {
                        try
                        {
                           Console.WriteLine(
                                "Waiting for command");
                            commandLine = reader.ReadLine();

                            Console.WriteLine("Got command: {0}", commandLine);
                            string result =
                                controller.ExecuteCommand(commandLine,
                                    connectedClient);

                            //Check if the message is not empty.
                            if (result != string.Empty)
                            {
                                Console.WriteLine(
                                    $"Sending to client: {result}");

                                //Sends message to the client.
                                writer.Write(result + '\n');
                                writer.Flush();
                            }


                            //If the client is not in a multiplayer game, close the connection.
                            if (!connectedClient.IsMultiplayer)
                            {
                                connectedClient.IsConnected = false;
                            }
                        }
                        catch (Exception e)
                        {
                            break;
                        }
                    }

                    //Close the connection.
                    stream.Close();
                    client.Close();
                }
            }).Start();
        }
    }
}