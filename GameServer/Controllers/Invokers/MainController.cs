using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using GameServer.Controllers.AbstractCommands;
using GameServer.Controllers.ConcreteCommands;
using GameServer.Controllers.Servers;
using GameServer.Controllers.Utilities;
using GameServer.Models;
using GameServer.Views.Handlers;

namespace GameServer.Controllers.Invokers
{
    /// <summary>
    /// The main controller of the game.
    /// Sets the commnds and executes them.
    /// </summary>
    public class MainController : IController
    {
        /// <summary>
        /// Contains the available commands.
        /// </summary>
        private Dictionary<string, ICommand> commands;

      /// <summary>
        /// Reference to the client handler.
        /// </summary>
        private IClientHandler clientHandler;

        /// <summary>
        /// Reference to the model.
        /// </summary>
        private IModel model;

        /// <summary>
        /// Constructor.
        /// </summary>
        public MainController()
        {
            //Variables definition.
            commands = new Dictionary<string, ICommand>();
            }

        public void SetClientHandler(IClientHandler clientHandler)
        {
            this.clientHandler = clientHandler;
        }

        public void SetModel(IModel model)
        {
            this.model = model;

            //Load commands.
            SetCustomCommands();
        }

       /// <summary>
        /// Sets custom commands.
        /// </summary>
        private void SetCustomCommands()
        {
            //Adding commands.
            commands.Add("generate", new GenerateMazeCommand(model));
            commands.Add("solve", new SolveMazeCommand(model));
            commands.Add("start", new StartGameCommand(model));
            commands.Add("list", new ListCommand(model));
            commands.Add("join", new JoinCommand(model));
            commands.Add("play", new PlayCommand(model));
            commands.Add("close", new CloseCommand(model));
        }

        public string ExecuteCommand(string commandLine, ConnectedClient client)
        {
            string[] arr = commandLine.Split(' ');
            string commandKey = arr[0];

            //Checking if command exists.
            if (!commands.ContainsKey(commandKey))
            {
                return "Command not found";
            }

            //Executing command.
            string[] args = arr.Skip(1).ToArray();
            ICommand command = commands[commandKey];

            return command.Execute(args, client);
        }
    }
}