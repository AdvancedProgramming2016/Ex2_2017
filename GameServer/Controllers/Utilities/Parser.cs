using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using Newtonsoft.Json;
using SearchAlgorithmsLib;

namespace GameServer.Controllers.Utilities
{
    /// <summary>
    /// Provides parsing services.
    /// </summary>
    static class Parser
    {
        /// <summary>
        /// Converts the solution into Json format.
        /// </summary>
        /// <param name="sol">The solution.</param>
        /// <returns>Json of the solution.</returns>
        static public string ToJson(Solution<Position> sol, string mazeName)
        {

            StringBuilder directionSb = new StringBuilder();

            foreach (State<Position> currPosition in sol.nodeList)
            {
                if (currPosition.cameFrom == null)
                {
                    break;
                }
                else if (currPosition.cameFrom.state.Col == currPosition.state.Col + 1)
                {
                    directionSb.Append((int)Direction.Right);
                }
                else if (currPosition.cameFrom.state.Col == currPosition.state.Col - 1)
                {
                    directionSb.Append((int)Direction.Left);
                }
                else if (currPosition.cameFrom.state.Row == currPosition.state.Row + 1)
                {
                    directionSb.Append((int)Direction.Up);
                }
                else if (currPosition.cameFrom.state.Row == currPosition.state.Row - 1)
                {
                    directionSb.Append((int)Direction.Down);
                }
            }

            SolutionJson sj = new SolutionJson(mazeName, directionSb.ToString(),
                sol.numOfNodesEvaluated.ToString());

            return JsonConvert.SerializeObject(sj);

        }
    }
}
