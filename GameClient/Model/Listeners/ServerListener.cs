using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MazeMenu.Model.Listeners
{
    /// <summary>
    /// Listens for server input.
    /// </summary>
    public class ServerListener : IListener//, INotifyPropertyChanged

    {
        /// <summary>
        /// Tcp client.
        /// </summary>
        private readonly TcpClient client;

        /// <summary>
        /// Stream reader.
        /// </summary>
        private readonly StreamReader reader;

        /// <summary>
        /// Thread to run.
        /// </summary>
        private Task readTask;

        /// <summary>
        /// Bool to check if connection is on.
        /// </summary>
        private bool isConnected;

        /// <summary>
        /// Bool to check if to continue listening.
        /// </summary>
        private bool Continue { get; set; }

        /// <summary>
        /// Bool to check if is a multiplayer connection.
        /// </summary>
        public bool IsMultiplayer { get; set; }

        /// <summary>
        /// Bool check if message
        /// </summary>
        private bool IsRead { get; set; }

        /// <summary>
        /// Command string.
        /// </summary>
        private string command;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="reader"></param>
        public ServerListener(TcpClient client, StreamReader reader)
        {
            this.client = client;
            this.reader = reader;
            this.Continue = true;
            this.IsRead = false;
            this.IsMultiplayer = false;
        }

        /// <summary>
        /// Command string from user.
        /// </summary>
        public string Command
        {
            get { return command; }
            set
            {
                command = value;
                SomethingHappened?.Invoke(this, null);
            }
        }

        /// <summary>
        /// Property changed event handler.
        /// </summary>
        // public event PropertyChangedEventHandler PropertyChanged;

        public event EventHandler SomethingHappened;
        /// <summary>
        /// Notifies that the property was changed.
        /// </summary>
        /// <param name="propName">property name.</param>
        /*public void NotifyPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(propName));
        }
        */

        /// <summary>
        /// Listens for server input.
        /// </summary>
        public void StartListening()
        {
            Continue = true;

            //Task to execute.
            this.readTask = new Task(() =>
            {
                //Loop while Main allows.
                while (Continue)
                {
                    try
                    {
                        string rawCommand = string.Empty;
                        string readyCommand = string.Empty;

                        //Read all the data received from the server.
                        do
                        {
                            rawCommand = reader.ReadLine();

                            //Check input is not null
                            if (rawCommand != null)
                            {
                                readyCommand += rawCommand + '\n';
                            }
                        } while (reader.Peek() > 0);


                        Command = readyCommand;

                        //Check if close command was received.
                        if (Command == "{}\n")
                        {
                            Command = "Game closed";
                            IsMultiplayer = false;
                            isConnected = false;
                            client.GetStream().Close();
                            client.Close();
                        }

                        //Check if action was legal.
                        if (Command.Split(' ')[0] == "Error:")
                        {
                            Continue = false;
                            IsMultiplayer = false;
                            isConnected = false;
                            client.Close();
                        }

                        //Print input if not empty.
                        if (Command != string.Empty)
                        {
                            Console.WriteLine("Result:\n{0}", Command);
                            //TODO might cause INotify to fail
                            //Command = string.Empty;
                        }

                        //If not a multiplayer game, stop the task.
                        if (!IsMultiplayer)
                        {
                            Continue = false;
                        }
                    }

                    catch (Exception e)
                    {
                        Continue = false;
                    }
                }
            });

            //Start task.
            this.readTask.Start();
        }

        /// <summary>
        /// Waits for the task to stop.
        /// </summary>
        public void WaitForTask()
        {
            this.readTask.Wait();
        }

        /// <summary>
        /// Stops the task.
        /// </summary>
        public void Stop()
        {
            Continue = false;
        }
    }
}