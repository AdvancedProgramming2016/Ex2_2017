using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using GameServer.Controllers.Utilities;
using GameServer.Models.Cache;

namespace GameServer.Controllers.Servers
{
    /// <summary>
    /// Holds the client that is connected to the game.
    /// </summary>
    public class ConnectedClient
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="tcpClient">A tcpClient.</param>
        /// <param name="streamWriter">A stream for writing.</param>
        public ConnectedClient(TcpClient tcpClient, StreamWriter streamWriter)
        {
            this.TcpClient = tcpClient;
            this.StreamWriter = streamWriter;
            this.IsMultiplayer = false;
            this.IsConnected = true;
            this.GameRoom = null;
        }

        /// <summary>
        /// TcpClient property.
        /// </summary>
        /// <value>TcpClient.</value>
        public TcpClient TcpClient { get; private set; }

        /// <summary>
        /// StreamWriter property.
        /// </summary>
        /// <value>StreamWriter.</value>
        public StreamWriter StreamWriter { get; private set; }

        /// <summary>
        /// Is the client a multiplayer property.
        /// </summary>
        /// <value>Bool value is a multiplayer.</value>
        public bool IsMultiplayer { get; set; }

        /// <summary>
        /// Is the client connected to the server property.
        /// </summary>
        /// <value>Bool is the client connected.</value>
        public bool IsConnected { get; set; }

        /// <summary>
        /// Game room property.
        /// </summary>
        /// <value>GameRoom.</value>
        public GameRoom GameRoom { get; set; }

        /// <summary>
        /// Sends a message to the client.
        /// </summary>
        /// <param name="message">Message</param>
        public void Send(string message)
        {
            this.StreamWriter.Write(message + '\n');
            this.StreamWriter.Flush();
        }
    }
}