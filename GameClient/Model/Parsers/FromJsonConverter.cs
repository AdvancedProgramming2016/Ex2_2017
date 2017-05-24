using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GameClient.Model.Parsers
{
    /// <summary>
    /// Provides converters for json serialized objects.
    /// </summary>
    public static class FromJsonConverter
    {
        /// <summary>
        /// Extracts the name from the PlayMove json.
        /// </summary>
        /// <param name="jsonMove">Json.</param>
        /// <returns>Name.</returns>
        public static string PlayName(string jsonMove)
        {
            JObject moveObj = JObject.Parse(jsonMove);
            string name = (string) moveObj["name"];

            return name;
        }

        /// <summary>
        /// Extracts the direction from the PlayMove json.
        /// </summary>
        /// <param name="jsonMove">Json.</param>
        /// <returns>Direction.</returns>
        public static string PlayDirection(string jsonMove)
        {
            JObject moveObj = JObject.Parse(jsonMove);
            string direction = (string) moveObj["direction"];

            return direction;
        }

        /// <summary>
        /// Desirializes the games list.
        /// </summary>
        /// <param name="jsonList">Json.</param>
        /// <returns>List of games.</returns>
        public static IList<string> GamesList(string jsonList)
        {
            IList<string> gamesList = JsonConvert
                .DeserializeObject<List<string>>(jsonList);

            return gamesList;
        }

        /// <summary>
        /// Extracts the nodes directions from Solution.
        /// </summary>
        /// <param name="jsonFormat">Json.</param>
        /// <returns>Nodes directions</returns>
        public static string MazeSolution(string jsonFormat)
        {
            JObject solutionObject = JObject.Parse(jsonFormat);
            string solution = (string) solutionObject["solution"];

            return solution;
        }

        /// <summary>
        /// Extracts the number of nodes evaluated from Solution.
        /// </summary>
        /// <param name="jsonFormat">Json.</param>
        /// <returns>Number of nodes evaluated.</returns>
        public static int MazeSolutionNodesEvaluated(string jsonFormat)
        {
            JObject solutionObject = JObject.Parse(jsonFormat);
            int nodesEvaluated = (int)solutionObject["nodesEvaluated"];

            return nodesEvaluated;
        }
    }
}