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
    public class SinglePlayerGameModel : ISinglePlayerGame
    {
        private Maze maze;
        private string solution;
        private Position playerPosition;
        private CommunicationClient communicationClient;
        private ISettingsModel settingsModel;
        private bool enable;

        public SinglePlayerGameModel(ISettingsModel settingsModel)
        {
            string resultCommand;
            this.communicationClient = new CommunicationClient();
            this.settingsModel = settingsModel;

            communicationClient.Connect(settingsModel.Port,
                settingsModel.IpAddress);

            communicationClient.PropertyChanged +=
                delegate(Object sender, PropertyChangedEventArgs e)
                {
                    ServerResponse = communicationClient.CommandFromUser;
                };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(propName));
        }

        public string ServerResponse { get; set; }

        public Position PlayerPosition
        {
            get { return this.playerPosition; }
            set
            {
                this.playerPosition = value;
                NotifyPropertyChanged("PlayerPosition");
            }
        }

        public string CommandPropertyChanged { get; set; }

        public Maze Maze
        {
            get { return this.maze; }

            set
            {
                this.maze = value;
                this.NotifyPropertyChanged("Maze");
            }
        }

        public void MovePlayer(int x, int y)
        {
            //TODO there was an if before, check if needed?
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

        public event EventHandler EnableControls;

        public bool EnableControlsStatus
        {
            get { return this.enable; }
            set
            {
                this.enable = value;
                EnableControls?.Invoke(this, null);
            }
        }

        public void Restart()
        {
            PlayerPosition = maze.InitialPos;
        }

        public void SolveMaze(string name)
        {
            new Task(() =>
            {
                EnableControlsStatus = false;

                try
                {
                    string command;
                    string algorithmType = settingsModel.DefaultAlgo.ToString();

                    command =
                        CommandParser.ParseTOSolveCommand(name, algorithmType);

                    communicationClient.SendToServer(command);

                    solution = Reverse(
                        FromJsonConverter.MazeSolution(ServerResponse));

                    PlayerPosition = maze.InitialPos;

                    foreach (char direction in solution)
                    {
                        switch (direction)
                        {
                            case '0':
                                PlayerPosition =
                                    ApplyMovement(
                                        PlayerPosition, "right");
                                break;
                            case '1':
                                PlayerPosition =
                                    ApplyMovement(
                                        PlayerPosition, "left");
                                break;
                            case '2':
                                PlayerPosition =
                                    ApplyMovement(
                                        PlayerPosition, "up");
                                break;
                            case '3':
                                PlayerPosition =
                                    ApplyMovement(
                                        PlayerPosition, "down");
                                break;
                        }
                        Thread.Sleep(500);
                    }
                }
                catch (ArgumentNullException)
                {
                    this.ConnectionLost?.Invoke(this, null);
                }

                EnableControlsStatus = true;
            }).Start();
        }

        private string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        public event EventHandler ConnectionLost;

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

        private void HandleServerResult(string command)
        {
            if (command.StartsWith("Error"))
            {
                //Handle error
            }

            switch (CommandPropertyChanged)
            {
                case "generate":
                    HandleGenerateCommand(command);
                    break;

                case "solve":
                    //    HandleSolveCommand(command);
                    break;
            }

            // ServerResponse = null;
        }

        private void HandleGenerateCommand(string command)
        {
            try
            {
                Maze = Maze.FromJSON(command);
                PlayerPosition =
                    new Position(Maze.InitialPos.Row, Maze.InitialPos.Col);
            }
            catch (Exception e)
            {
                MessageBox.Show("A maze with the same name already exists.");
            }
        }

        public Position ApplyMovement(Position position, string direction)
        {
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