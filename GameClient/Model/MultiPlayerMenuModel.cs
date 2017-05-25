using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using MazeLib;
using System.Collections.ObjectModel;

namespace GameClient.Model
{
    class MultiPlayerMenuModel : INotifyPropertyChanged
    {

        private Maze maze;
        private ObservableCollection<String> listOfGames;

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(propName));
        }

        public ObservableCollection<String> ListOfGames
        {
            get
            {
                return this.listOfGames;
            }
            set
            {
                this.listOfGames = value;
                this.NotifyPropertyChanged("ListOfGames");
            }
        }

        public Maze Maze
        {
            get { return this.maze; }

            set
            {
                this.maze = value;
                this.NotifyPropertyChanged("Maze");
            }
        }

        public void AddGameToList(String gameName)
        {
            // Open session with the server and add the new game.
            ListOfGames.Add(gameName);
        }

    }
}
