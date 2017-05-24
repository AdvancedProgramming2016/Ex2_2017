using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using GameServer.Controllers.Servers;
using GameServer.Controllers.Utilities;
using GameServer.Models;
using GameServer.Views.Handlers;

namespace GameServer.Controllers.Invokers
{
    /// <summary>
    /// The basic controller interface.
    /// </summary>
    public interface IController
    {
        /// <summary>
        /// The execute command the controller has.
        /// </summary>
        /// <param name="commandLine">command</param>
        /// <param name="client">connection to client</param>
        /// <returns>Result from the command</returns>
        string ExecuteCommand(string commandLine, ConnectedClient client);

        /// <summary>
        /// Sets the View.
        /// </summary>
        /// <param name="clientHandler">Client handler</param>
        void SetClientHandler(IClientHandler clientHandler);

        /// <summary>
        /// Sets the Model.
        /// </summary>
        /// <param name="model">Model</param>
        void SetModel(IModel model);
    }
}