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
using GameClient.Model.Listeners;
using GameClient.Model.Parsers;
using SearchAlgorithmsLib;

namespace GameClient.Model
{
    public class SinglePlayerGameModel : ISinglePlayerGame
    {
        private Maze maze;
        private string solution;
        private CommunicationClient communicationClient;
        private ISettingsModel settingsModel;

        public SinglePlayerGameModel(ISettingsModel settingsModel)
        {
            string resultCommand;
            this.communicationClient = new CommunicationClient();
            this.settingsModel = settingsModel;
            //TODO get ip and port from Settings
            communicationClient.Connect(settingsModel.Port, settingsModel.IpAddress);

            communicationClient.PropertyChanged +=
                delegate(Object sender, PropertyChangedEventArgs e)
                {
                    ServerResponse = communicationClient.CommandFromUser;
                   // HandleServerResult(ServerResponse);
                };
            //  TcpClient tcpClient = new TcpClient();
            //  this.serverListener = new ServerListener(tcpClient, new StreamReader(tcpClient.GetStream()));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(propName));
        }

        public string ServerResponse { get; set; }

        public Position PlayerPosition { get; set; }
        public string CommandPropertyChanged { get; set; }

        public Maze Maze
        {
            get { return this.maze; }

            set
            {
                this.maze = value;
                this.NotifyPropertyChanged("FullMaze");
            }
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

        /* public Position PlayerPosition
           {
               get { return this.playerPosition; }
   
               set
               {
                   this.playerPosition = value;
                   this.NotifyPropertyChanged("PlayerPosition");
               }
           }*/

       /* public void MovePlayer()
         {
             throw new NotImplementedException();
         }*/

        public void Restart()
        {
            throw new NotImplementedException();
        }

        public void SolveMaze(string name)
        {
            string command;
            string algorithmType = settingsModel.DefaultAlgo.ToString();

            command = CommandParser.ParseTOSolveCommand(name, algorithmType);

            CommandPropertyChanged = "solve";

            communicationClient.SendToServer(command);

             HandleServerResult(ServerResponse);
           // while (ServerResponse == null) { }
        }

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
            communicationClient.SendToServer(command);

             HandleServerResult(ServerResponse);
           // while (ServerResponse == null) { }
            //string generateResult;

            /* communicationClient.ServerListener.PropertyChanged +=
                 delegate(Object sender, PropertyChangedEventArgs e)
                 {
                     //TODO the xaml gives only rows and name, columns missing
                     generateResult = communicationClient.ServerListener.Command;
 
                     maze = Maze.FromJSON(generateResult);
                 };*/
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
                    HandleSolveCommand(command);
                    break;
            }

           // ServerResponse = null;
        }

        private void HandleGenerateCommand(string command)
        {
            Maze = Maze.FromJSON(command);
        }

        private void HandleSolveCommand(string command)
        {
            string solution = FromJsonConverter.MazeSolution(command);
            Solution = solution;
        }
    }
}