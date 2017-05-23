using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.ComponentModel;

namespace MazeMenu.Model
{
    public class MultiPlayerModel : IMultiPlayerGame
    {

        private Maze maze;
        private Position playerPosition;
        private Position opponentPosition;
        private TcpClient tcpClient;

        public event PropertyChangedEventHandler PropertyChanged;

        public Maze Maze
        {
            get
            {
                return this.maze;
            }

            set
            {
                this.maze = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Maze"));
            }
        }

        public Position OpponentPosition
        {
            get
            {
                return this.opponentPosition;
            }

            set
            {
                this.opponentPosition = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("OpponentPosition"));
            }
        }

        public Position PlayerPosition
        {
            get
            {
                return this.playerPosition;
            }

            set
            {
                this.playerPosition = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PlayerPosition"));
            }
        }

        public MultiPlayerModel(ISettingsModel settingModel)
        {
            
            int port = settingModel.Port;
            string ip = settingModel.IpAddress;
            IPEndPoint endPoint =
                new IPEndPoint(IPAddress.Parse(ip), port);
            this.tcpClient = new TcpClient(); // Tcp Client with the server.
        }

        //public void JoinGame(Game game)
        public void JoinGame()
        {

            // Add new game to the server.

        }

        private void WaitForServerInfo()
        {

            Task task = Task.Run(() =>
            {
                NetworkStream stream;
                //var reader = new StreamReader(stream);
                //serverListener = new ServerListener(tcpClient, reader);

                //start listener
                //serverListener.StartListening();
                while (true)
                {
                    // Wait for json from server.
                    stream = this.tcpClient.GetStream();
                    //Do something.
                }
                
            });

            task.Wait();

        }

        public void StartNewGame()
        {
            throw new NotImplementedException();
        }
        
        public void CloseGame()
        {
            throw new NotImplementedException();
        }

        public void MovePlayer(Position position)
        {
            throw new NotImplementedException();
        }
    }
}
