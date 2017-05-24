using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClient.Model.Parsers
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
        /// <param name="rows">Number of rows.</param>
        /// <param name="columns">Number of columns.</param>
        /// <returns>Command in right format.</returns>
        public static string ParseToGenerateCommand(string name, string rows,
            string columns)
        {
            string finalCommand;

            //TODO change that
             finalCommand = $"generate {name} {rows} {columns}";
           // finalCommand = "generate ggg 5 5";

            return finalCommand;
        }

        /// <summary>
        /// Parses the solve command into the right format.
        /// </summary>
        /// <param name="name">Maze name.</param>
        /// <param name="algorithmType">Algorithm type.</param>
        /// <returns>Command in right format.</returns>
        public static string ParseTOSolveCommand(string name,
            string algorithmType)
        {
            string finalCommand;

            finalCommand = $"solve {name} {algorithmType}";

            return finalCommand;
        }


        /// <summary>
        /// Parses the start command into the right format.
        /// </summary>
        /// <param name="name">Game name.</param>
        /// <param name="rows">Number of rows.</param>
        /// <param name="columns">Number of columns.</param>
        /// <returns>Command in right format.</returns>
        public static string ParseToStartCommand(string name, string rows,
            string columns)
        {
            string finalCommand;

            finalCommand = $"start {name} {rows} {columns}";

            return finalCommand;
        }

        public static string ParseToListCommand()
        {
            string finalCommand;

            finalCommand = "list";

            return finalCommand;
        }

        /// <summary>
        /// Parses the join command into the right format.
        /// </summary>
        /// <param name="name">Game name.</param>
        /// <returns>Command in right format.</returns>
        public static string ParseToJoinCommand(string name)
        {
            string finalCommand;

            finalCommand = $"join {name}";

            return finalCommand;
        }

        /// <summary>
        /// Parses the play command into the right format.
        /// </summary>
        /// <param name="move">Movement direction.</param>
        /// <returns>Command in right format.</returns>
        public static string ParseToPlayCommand(string move)
        {
            string finalCommand;

            finalCommand = $"play {move}";

            return finalCommand;
        }

        /// <summary>
        /// Parses the close command into the right format.
        /// </summary>
        /// <param name="name">Game name.</param>
        /// <returns>Command in right format.</returns>
        public static string ParseToCloseCommand(string name)
        {
            string finalCommand;

            finalCommand = $"close {name}";

            return finalCommand;
        }
    }
}
