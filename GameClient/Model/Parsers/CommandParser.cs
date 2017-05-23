using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeMenu.Model.Parsers
{
    /// <summary>
    /// Parses commands into the right formats.
    /// </summary>
    public static class CommandParser
    {
        /// <summary>
        /// Parses the generate command into the right format.
        /// </summary>
        /// <param name="name">Maze name.</param>
        /// <param name="rows">rows size.</param>
        /// <param name="columns">columns size.</param>
        /// <returns>Command in right format.</returns>
        public static string ParseToGenerateCommand(string name, string rows,
            string columns)
        {
            string finalCommand;

            //TODO change that
            // finalCommand = $"generate {name} {rows} {columns}";
            finalCommand = "generate ggg 5 5";

            return finalCommand;
        }
    }
}
