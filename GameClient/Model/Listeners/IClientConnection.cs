using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeMenu.Model.Listeners
{
    /// <summary>
    /// Client connection interface.
    /// </summary>
    public interface IClientConnection
    {
        /// <summary>
        /// Sets the connection settings.
        /// </summary>
        /// <param name="port">port number</param>
        /// <param name="ip">device ip</param>
        void Connect(int port, string ip);

        /// <summary>
        /// Sends a message to the server.
        /// </summary>
        /// <param name="command">message</param>
        void SendToServer(string command);

        string CommandFromUser { get; set; }
    }
}