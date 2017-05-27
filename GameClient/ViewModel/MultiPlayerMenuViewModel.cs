using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using GameClient.Model;

namespace GameClient.ViewModel
{
    class MultiPlayerMenuViewModel : INotifyPropertyChanged
    {
        //private MultiPlayerMenuModel mpmModel;
        private ObservableCollection<string> vm_listOfGames;
        private ISettingsModel settingsModel;

        public MultiPlayerMenuViewModel()
        {
            this.settingsModel = new SettingsModel();
            this.MultiPlayerMenuModel = new MultiPlayerMenuModel(settingsModel);

            this.MultiPlayerMenuModel.PropertyChanged +=
                delegate(Object sender, PropertyChangedEventArgs e)
                {
                    NotifyPropertyChanged("VM_" + e.PropertyName);
                };

            //Request list from server.
           // this.mpmModel.RequestList();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(propertyName));
        }

        public MultiPlayerMenuModel MultiPlayerMenuModel { get; set; }

        public void AddGameToList(String gameName)
        {
            this.MultiPlayerMenuModel.AddGameToList(gameName);
        }

        public void StartNewGame(string nameOfGame, string rows, string columns)
        {
            this.MultiPlayerMenuModel.StartNewGame(rows, columns, nameOfGame);
        }

        public void JoinGame(string nameOfGame)
        {
            this.MultiPlayerMenuModel.JoinGame(nameOfGame);
        }

        public void GetGameList()
        {
            this.MultiPlayerMenuModel.RequestList();
        }

        public ObservableCollection<String> VM_ListOfGames
        {
            get { return this.MultiPlayerMenuModel.ListOfGames; }
            set
            {
                this.vm_listOfGames = value;
                NotifyPropertyChanged("VM_ListOfGames");
            }
        }

        public String VM_Maze
        {
            get
            {
                return this.MultiPlayerMenuModel.Maze.ToString()
                    .Replace("\r\n", "")
                    .Replace("*", "0");
            }
        }

        public String VM_DefaultNumRows
        {
            get
            {
                return GameClient.Properties.Settings.Default.DefaultRows
                    .ToString();
            }
        }

        public String VM_DefaultNumCols
        {
            get
            {
                return GameClient.Properties.Settings.Default.DefaultCols
                    .ToString();
            }
        }
    }
}