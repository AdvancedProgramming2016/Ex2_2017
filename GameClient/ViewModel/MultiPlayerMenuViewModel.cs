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

        public event PropertyChangedEventHandler PropertyChanged;
        private MultiPlayerMenuModel mpmModel;

        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(propertyName));
        }

        public void AddGameToList(String gameName)
        {
            this.mpmModel.AddGameToList(gameName);
        }

        public MultiPlayerMenuViewModel()
        {
            this.mpmModel = new MultiPlayerMenuModel();
            this.mpmModel.PropertyChanged +=
                delegate (Object sender, PropertyChangedEventArgs e)
                {
                    NotifyPropertyChanged("VM_" + e.PropertyName);
                };
        }

        public ObservableCollection<String> VM_ListOfGames
        {
            get
            {
                return VM_ListOfGames;
            }
        }

        public String VM_Maze
        {
            get
            {
                return this.mpmModel.Maze.ToString()
                    .Replace("\r\n", "")
                    .Replace("*", "0");
            }
        }

        public String VM_DefaultNumRows
        {
            get
            {
                return GameClient.Properties.Settings.Default.DefaultRows.ToString();
            }
        }

        public String VM_DefaultNumCols
        {
            get
            {
                return GameClient.Properties.Settings.Default.DefaultCols.ToString();
            }
        }
    }
}
