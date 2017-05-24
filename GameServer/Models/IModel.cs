using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameServer.Models.Cache;
using MazeLib;
using SearchAlgorithmsLib;

namespace GameServer.Models
{
    /// <summary>
    /// The basic model interface.
    /// </summary>
    public interface IModel
    {
        // Maze GenerateMaze(string name, int rows, int columns)

        /// <summary>
        /// Storage getter.
        /// </summary>
        Storage Storage { get; }

        /// <summary>
        /// Solves a maze with a given type of algorithm.
        /// </summary>
        ///<param name="maze">A maze.</param>
        /// <param name="algorithmId">Algorithm type.</param>
        /// <returns>Solution to the maze.</returns>
        Solution<Position> Solve(Maze maze, int algorithmId);

        /// <summary>
        /// Generates a maze from a given size.
        /// </summary>
        /// <param name="name">number of rows.</param>
        /// <param name="rows">number of rows.</param>
        /// <param name="cols">number of rows.</param>
        /// <returns>A maze.</returns>
        Maze GenerateMaze(string name, int rows, int cols);

    }
}
