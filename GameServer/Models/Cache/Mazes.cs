using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using SearchAlgorithmsLib;

namespace GameServer.Models.Cache
{
    /// <summary>
    /// A storage for mazes.
    /// </summary>
    public class Mazes
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public Mazes()
        {
            this.GeneratedMazes = new Dictionary<string, Maze>();
            this.SolvedMazes = new Dictionary<string, Solution<Position>>();
            this.StartedMazes = new Dictionary<string, Maze>();
        }

        /// <summary>
        /// GeneratedMazes property.
        /// </summary>
        /// <value>Dictionary</value>
        public Dictionary<string, Maze> GeneratedMazes { get; private set; }

        /// <summary>
        /// SolvedMazes property.
        /// </summary>
        /// <value>Dictionary.</value>
        public Dictionary<string, Solution<Position>> SolvedMazes
        {
            get;
            private set;
        }

        /// <summary>
        /// Started mazes dictionary.
        /// </summary>
        /// <value>Dictionary</value>
        public Dictionary<string, Maze> StartedMazes // for multiplayer
        {
            get;
            private set;
        }

        /// <summary>
        /// Searches for a singleplayer maze in the storage.
        /// </summary>
        /// <param name="name">Maze name</param>
        /// <returns>Maze if exists, else null</returns>
        public Maze SearchGeneratedMaze(string name)
        {
            Maze maze;

            //Check if a maze with the given name exists in the dictionary.
            if (!GeneratedMazes.ContainsKey(name))
            {
                maze = null;
            }
            else
            {
                maze = GeneratedMazes[name];
            }

            return maze;
        }

        /// <summary>
        /// Searches for a multiplayer maze in the storage.
        /// </summary>
        /// <param name="name">Maze name</param>
        /// <returns>Maze if exists, else null</returns>
        public Maze SearchStartedMaze(string name)
        {
            Maze maze;

            //Check if a maze with the given name exists in the dictionary.
            if (!StartedMazes.ContainsKey(name))
            {
                maze = null;
            }
            else
            {
                maze = StartedMazes[name];
            }

            return maze;
        }

        /// <summary>
        /// Searches for a solved maze in the storage.
        /// </summary>
        /// <param name="name">Solution name</param>
        /// <returns>Solution if exists, else null</returns>
        public Solution<Position> SearchSolvedMaze(string name)
        {
            Solution<Position> solution;

            //Check if a solution with the given name exists in the dictionary.
            if (!SolvedMazes.ContainsKey(name))
            {
                solution = null;
            }
            else
            {
                solution = SolvedMazes[name];
            }

            return solution;
        }
    }
}