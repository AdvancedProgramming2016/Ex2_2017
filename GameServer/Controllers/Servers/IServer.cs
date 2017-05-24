using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameServer.Views.Handlers;

namespace GameServer.Controllers.Servers
{
    /// <summary>
    /// The basic server interface.
    /// </summary>
    public interface IServer
    {
        /// <summary>
        /// Start the server activity.
        /// </summary>
        /// <param name="ip">ip string</param>
        /// <param name="port">port number</param>
        /// <param name="clientHandler">the client handler</param>
        void Start(string ip, int port, IClientHandler clientHandler);

        /// <summary>
        /// Wait for the server task to end.
        /// </summary>
        void Wait();

        /// <summary>
        ///Stop the server activity
        /// </summary>
        void Stop();
    }
}