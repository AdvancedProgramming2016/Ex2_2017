using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Schema;
using GameServer.Controllers.AbstractCommands;
using GameServer.Controllers.Servers;
using GameServer.Controllers.Utilities;
using GameServer.Models;
using GameServer.Views.Handlers;
using MazeLib;
using SearchAlgorithmsLib;

namespace GameServer.Controllers.ConcreteCommands
{
    /// <summary>
    /// The class responsible for the command of solving a maze.
    /// </summary>
    public class SolveMazeCommand : ICommand
    {
        private readonly IModel model;
        private readonly Mutex solvedMutex;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="model">Provides the logic of the game</param>
        public SolveMazeCommand(IModel model)
        {
            this.model = model;
            this.solvedMutex = new Mutex();
        }

        public string Execute(string[] args, ConnectedClient client)
        {
            //Check that all the arguments were received.
            if (args.Length != 2)
            {
                return "Error: Parameters don't match the command.\n";
            }

            string mazeName = args[0];

            //Search the requested maze in the storage.
            Maze maze = this.model.Storage.Mazes.SearchGeneratedMaze(mazeName);

            //Check that the maze exists in the storage.
            if (maze == null)
            {
                return $"Error: Maze {mazeName} doesn't exist.\n";
            }

            //Take the algorithm type.
            int algorithmType = 0;
            bool isAlgorithm = int.TryParse(args[1], out algorithmType);

            //Check that the algorithm type is a number.
            if (!isAlgorithm)
            {
                return "Error: Algorithm type must be a number.\n";
            }

            //Holds the solution in JSon format.
            string solutionInJsonFormat;

            //Search for existing solution.
            Solution<Position> solution =
                this.model.Storage.Mazes.SearchSolvedMaze(mazeName);

            //Check if solution was found.
            if (solution != null)
            {
                solutionInJsonFormat = Parser.ToJson(solution, mazeName);
            }

            else
            {
                //Solve the maze.
                solution =
                    this.model.Solve(maze, algorithmType);
                //Store the solution in the storage
                this.solvedMutex.WaitOne();
                this.model.Storage.Mazes.SolvedMazes
                    .Add(mazeName, solution);
                this.solvedMutex.ReleaseMutex();

                //Convert solution to JSon format.
                solutionInJsonFormat = Parser.ToJson(solution, mazeName);
            }

            return solutionInJsonFormat;
        }
    }
}