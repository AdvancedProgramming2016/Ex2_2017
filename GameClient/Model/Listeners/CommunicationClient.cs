using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace GameClient.Model.Listeners
{
    /// <summary>
    /// Handles communication with the server.
    /// </summary>
    public class CommunicationClient : IClientConnection, INotifyPropertyChanged
    {
        /// <summary>
        /// Port number.
        /// </summary>
        private int port;

        /// <summary>
        /// Ip address.
        /// </summary>
        private string ip;

        /// <summary>
        /// Ip end point.
        /// </summary>
        private IPEndPoint endPoint;

        /// <summary>
        /// Tcp client.
        /// </summary>
        private TcpClient tcpClient;

        /// <summary>
        /// Network stream.
        /// </summary>
        private NetworkStream stream;

        /// <summary>
        /// Stream reader.
        /// </summary>
        private StreamReader reader;

        /// <summary>
        /// Stream writer.
        /// </summary>
        private StreamWriter writer;

        /// <summary>
        /// Checks if multiplayer game.
        /// </summary>
        private bool isMultiplayer;

        /// <summary>
        /// Checks if connected.
        /// </summary>
        private bool isConnected;

        /// <summary>
        /// Command received from user.
        /// </summary>
        private string commandFromUser;

        /// <summary>
        /// Constructor.
        /// </summary>
        public CommunicationClient()
        {
            //Initialize variables.
            tcpClient = null;
            stream = null;
            reader = null;
            writer = null;
            ServerListener = null;
            isMultiplayer = false;
            isConnected = false;
        }

        /// <summary>
        /// Server listerner property.
        /// </summary>
        public IListener ServerListener { get; set; }

        /// <summary>
        /// Connects to server.
        /// </summary>
        /// <param name="port">Port.</param>
        /// <param name="ip">Ip.</param>
        public void Connect(int port, string ip)
        {
            this.port = port;
            this.ip = ip;
            endPoint = new IPEndPoint(IPAddress.Parse(this.ip), this.port);
        }

        /// <summary>
        /// Connection failed event.
        /// </summary>
        public event EventHandler ConnectionFailed;

        /// <summary>
        /// Sends command to server.
        /// </summary>
        /// <param name="command">Command.</param>
        public void SendToServer(string command)
        {
            //If not connected, Initialize connection.
            if (!isConnected || ServerListener.IsMultiplayer == false)
            {
                tcpClient = new TcpClient();

                //Connect to server.
                try
                {
                    tcpClient.Connect(endPoint);
                }
                catch (Exception e)
                {
                    throw new ArgumentNullException();
                }

                //Initialize streams.
                stream = tcpClient.GetStream();
                writer = new StreamWriter(stream);
                reader = new StreamReader(stream);
                isConnected = true;
                ServerListener = new ServerListener(tcpClient, reader);
                ServerListener.SomethingHappened += HandleServerAnswer;

                //start listener
                ServerListener.StartListening();
            }

            //CommunicateWithServer with server.
            try
            {
                string[] splitCommand = command.Split(' ');

                //Check if the connection needs to remain open.
                if (splitCommand[0] == "start" ||
                    splitCommand[0] == "join")
                {
                    isMultiplayer = true;
                    ServerListener.IsMultiplayer = true;
                }

                //Send message to server.
                writer.Write(command + '\n');
                writer.Flush();

                //Check if connection can be closed.
                if (!isMultiplayer ||
                    (splitCommand[0] == "close" && splitCommand.Length == 2))
                {
                    isMultiplayer = false;
                    ServerListener.IsMultiplayer = false;

                    //Wait for listener to end.
                    ServerListener.WaitForTask();
                    stream.Close();
                    tcpClient.Close();
                    isConnected = false;
                }
            }
            catch (Exception e)
            {
                return;
            }
        }

        /// <summary>
        /// Property changed event handler.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Notifies property has changed.
        /// </summary>
        /// <param name="propName">Property name.</param>
        public void NotifyPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(propName));
        }

        /// <summary>
        /// Command from user property.
        /// </summary>
        public string CommandFromUser
        {
            get { return commandFromUser; }
            set
            {
                commandFromUser = value;
                NotifyPropertyChanged("Command");
            }
        }

        /// <summary>
        /// Handles server response.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandleServerAnswer(object sender, EventArgs e)
        {
            CommandFromUser = ServerListener.Command;
        }
    }
}