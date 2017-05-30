using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using GameClient.Model;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Windows;
using GameClient.Model.Listeners;
using GameClient.Model.Parsers;
using SearchAlgorithmsLib;

namespace GameClient.Model
{
    /// <summary>
    /// Single player model.
    /// </summary>
    public class SinglePlayerGameModel : ISinglePlayerGame
    {
        /// <summary>
        /// Maze.
        /// </summary>
        private Maze maze;

        /// <summary>
        /// Maze solution.
        /// </summary>
        private string solution;

        /// <summary>
        /// Player position.
        /// </summary>
        private Position playerPosition;

        /// <summary>
        /// Communication client.
        /// </summary>
        private CommunicationClient communicationClient;

        /// <summary>
        /// Settings model.
        /// </summary>
        private ISettingsModel settingsModel;

        /// <summary>
        /// Checks if the controls are enabled.
        /// </summary>
        private bool enable;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="settingsModel">Settings model.</param>
        public SinglePlayerGameModel(ISettingsModel settingsModel)
        {
            //Initializes members.
            this.communicationClient = new CommunicationClient();
            this.settingsModel = settingsModel;

            communicationClient.Connect(settingsModel.Port,
                settingsModel.IpAddress);

            //Sets property changed delegate.
            communicationClient.PropertyChanged +=
                delegate(Object sender, PropertyChangedEventArgs e)
                {
                    ServerResponse = communicationClient.CommandFromUser;
                };
        }

        /// <summary>
        /// Property changed event.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Notifies property has changed.
        /// </summary>
        /// <param name="propName"></param>
        public void NotifyPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(propName));
        }

        /// <summary>
        /// Server response property.
        /// </summary>
        public string ServerResponse { get; set; }

        /// <summary>
        /// Player position property.
        /// </summary>
        public Position PlayerPosition
        {
            get { return this.playerPosition; }
            set
            {
                this.playerPosition = value;
                NotifyPropertyChanged("PlayerPosition");
            }
        }

        /// <summary>
        /// Command changed property.
        /// </summary>
        public string CommandPropertyChanged { get; set; }

        /// <summary>
        /// Maze property.
        /// </summary>
        public Maze Maze
        {
            get { return this.maze; }

            set
            {
                this.maze = value;
                this.NotifyPropertyChanged("Maze");
            }
        }

        /// <summary>
        /// Moves player.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void MovePlayer(int x, int y)
        {
            PlayerPosition = new Position(x, y);
        }

        public string Solution
        {
            get { return this.solution; }
            set
            {
                this.solution = value;
                this.NotifyPropertyChanged("Solution");
            }
        }

        /// <summary>
        /// Enable controls event.
        /// </summary>
        public event EventHandler EnableControls;

        /// <summary>
        /// Enable controls property.
        /// </summary>
        public bool EnableControlsStatus
        {
            get { return this.enable; }
            set
            {
                this.enable = value;
                EnableControls?.Invoke(this, null);
            }
        }

        /// <summary>
        /// Restarts the game.
        /// </summary>
        public void Restart()
        {
            PlayerPosition = maze.InitialPos;
        }

        /// <summary>
        /// Solves the game.
        /// </summary>
        /// <param name="name">Game name.</param>
        public void SolveMaze(string name)
        {
            new Task(() =>
            {
                //Disable controls.
                EnableControlsStatus = false;

                try
                {
                    string command;

                    //Set algorithm type.
                    string algorithmType = settingsModel.DefaultAlgo.ToString();

                    //Parse to solve command.
                    command =
                        CommandParser.ParseTOSolveCommand(name, algorithmType);

                    communicationClient.SendToServer(command);

                    solution = Reverse(
                        FromJsonConverter.MazeSolution(ServerResponse));

                    PlayerPosition = maze.InitialPos;

                    //Move according to directions.
                    foreach (char direction in solution)
                    {
                        switch (direction)
                        {
                            case '0':
                                PlayerPosition =
                                    PerformMove(
                                        PlayerPosition, "right");
                                break;

                            case '1':
                                PlayerPosition =
                                    PerformMove(
                                        PlayerPosition, "left");
                                break;

                            case '2':
                                PlayerPosition =
                                    PerformMove(
                                        PlayerPosition, "up");
                                break;

                            case '3':
                                PlayerPosition =
                                    PerformMove(
                                        PlayerPosition, "down");
                                break;
                        }

                        //Sleep between each move.
                        Thread.Sleep(250);
                    }
                }
                catch (ArgumentNullException)
                {
                    this.ConnectionLost?.Invoke(this, null);
                }

                //Enable controls.
                EnableControlsStatus = true;
            }).Start();
        }

        /// <summary>
        /// Reverses a string.
        /// </summary>
        /// <param name="s">String.</param>
        /// <returns>Reversed string.</returns>
        private string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        /// <summary>
        /// Connevtion lost event.
        /// </summary>
        public event EventHandler ConnectionLost;

        /// <summary>
        /// Generates a maze.
        /// </summary>
        /// <param name="numOfRows">Rows.</param>
        /// <param name="numOfCols">Columns.</param>
        /// <param name="nameOfMaze">Maze name.</param>
        public void GenerateGame(String numOfRows, String numOfCols,
            String nameOfMaze)
        {
            string command;

            //Parse into the right format.
            command = CommandParser.ParseToGenerateCommand(nameOfMaze,
                numOfRows,
                numOfCols);

            CommandPropertyChanged = "generate";

            //Send command to the server.
            try
            {
                communicationClient.SendToServer(command);
                while (string.IsNullOrEmpty(ServerResponse))
                {
                    continue;
                }

                HandleServerResult(ServerResponse);
            }
            catch (ArgumentNullException)
            {
                this.ConnectionLost?.Invoke(this, null);
            }
        }

        /// <summary>
        /// Handels server result.
        /// </summary>
        /// <param name="command">Server result.</param>
        private void HandleServerResult(string command)
        {
            switch (CommandPropertyChanged)
            {
                case "generate":
                    HandleGenerateCommand(command);
                    break;

                case "solve":
                    //    HandleSolveCommand(command);
                    break;
            }
        }

        /// <summary>
        /// Handles generate command.
        /// </summary>
        /// <param name="command">Command.</param>
        private void HandleGenerateCommand(string command)
        {
            try
            {
                //Parse maze from Json.
                Maze = Maze.FromJSON(command);

                //Set player position.
                PlayerPosition =
                    new Position(Maze.InitialPos.Row, Maze.InitialPos.Col);
            }
            catch (Exception e)
            {
            }
        }

        /// <summary>
        /// Performs movement.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        public Position PerformMove(Position position, string direction)
        {
            //Check for direction.
            switch (direction)
            {
                case "up":
                    --position.Row;
                    break;

                case "down":
                    ++position.Row;
                    break;

                case "right":
                    ++position.Col;
                    break;

                case "left":
                    --position.Col;
                    break;
            }

            return position;
        }
    }
}