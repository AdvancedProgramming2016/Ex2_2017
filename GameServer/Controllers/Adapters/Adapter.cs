using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using SearchAlgorithmsLib;
using Newtonsoft.Json;

namespace GameServer.Controllers.Adapters
{
    /// <summary>
    /// Adapter design pattern.
    /// </summary>
    public class Adapter : ISearchable<Position>
    {
        /// <summary>
        /// The maze instance.
        /// </summary>
        public Maze maze { get; set; }

        /// <summary>
        /// The state pool that holds all the instansiated states.
        /// </summary>
        private readonly StatePool<Position> statePool;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="row">number of rows of maze.</param>
        /// <param name="col">number of cols of maze.</param>
        /// <param name="startX">x coordinate of start point.</param>
        /// <param name="startY">y coordinate of start point.</param>
        /// <param name="endX">x coordinate of end point.</param>
        /// <param name="endY">y coordinate of end point.</param>
        /// <param name="mazeName">name of maze.</param>
        /// <param name="sp">state pool.</param>
        public Adapter(int row, int col, int startX, int startY, int endX,
            int endY,
            string mazeName, StatePool<Position> sp)
        {
            this.maze = new Maze(row, col);
            Console.WriteLine(this.maze.ToString());
            maze.Name = mazeName;
            maze.InitialPos = new Position(startX, startY);
            maze.GoalPos = new Position(endX, endY);
            this.statePool = sp;
        }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="maze">maze object.</param>
        /// <param name="sp">state pool.</param>
        public Adapter(Maze maze, StatePool<Position> sp)
        {
            this.maze = maze;
            this.statePool = sp;
        }

        /// <summary>
        /// Returns the inital state.
        /// </summary>
        /// <returns>position of initial state.</returns>
        public State<Position> GetInitialState()
        {
            return new State<Position>(this.maze.InitialPos);
        }

        /// <summary>
        /// Returns the goal state.
        /// </summary>
        /// <returns>position of goal state.</returns>
        public State<Position> GetGoalState()
        {
            return new State<Position>(this.maze.GoalPos);
        }

        /// <summary>
        /// Returns all the possible states.
        /// </summary>
        /// <param name="s">Position current state.</param>
        /// <returns>States relative to s.</returns>
        public List<State<Position>> GetAllPossibleStates(State<Position> s)
        {
            List<State<Position>> positionList = new List<State<Position>>();

            // If the block above is free add to list.
            if (s.state.Col - 1 >= 0 &&
                this.maze[s.state.Row, s.state.Col - 1] == CellType.Free)
            {
                Position tempPos = new Position(s.state.Row, s.state.Col - 1);
                this.statePool.AddToStatePool(tempPos);
                positionList.Add(this.statePool.GetState(tempPos));
            }
            // If the block to the left is free add to list.
            if (s.state.Row - 1 >= 0 &&
                this.maze[s.state.Row - 1, s.state.Col] == CellType.Free)
            {
                Position tempPos = new Position(s.state.Row - 1, s.state.Col);
                this.statePool.AddToStatePool(tempPos);
                positionList.Add(this.statePool.GetState(tempPos));
            }
            // If the block to the right is free add to list.
            if (s.state.Col + 1 < this.maze.Cols &&
                this.maze[s.state.Row, s.state.Col + 1] == CellType.Free)
            {
                Position tempPos = new Position(s.state.Row, s.state.Col + 1);
                this.statePool.AddToStatePool(tempPos);
                positionList.Add(this.statePool.GetState(tempPos));
            }
            // If the block below is free add to list.
            if (s.state.Row + 1 < this.maze.Rows &&
                this.maze[s.state.Row + 1, s.state.Col] == CellType.Free)
            {
                Position tempPos = new Position(s.state.Row + 1, s.state.Col);
                this.statePool.AddToStatePool(tempPos);
                positionList.Add(this.statePool.GetState(tempPos));
            }

            return positionList;
        }
    }
}