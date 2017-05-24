using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Views.Handlers
{
    /// <summary>
    /// The interface to handle a client.
    /// </summary>
    public interface IClientHandler
    {
        /// <summary>
        /// The method will handle the received client.
        /// </summary>
        /// <param name="client">A connection to the client</param>
        void HandleClient(TcpClient client);
    }
}
