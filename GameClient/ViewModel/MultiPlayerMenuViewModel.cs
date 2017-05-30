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
    /// <summary>
    /// Multiplayer menu viewModel.
    /// </summary>
    class MultiPlayerMenuViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Settings model.
        /// </summary>
        private ISettingsModel settingsModel;

        /// <summary>
        /// Constructor.
        /// </summary>
        public MultiPlayerMenuViewModel()
        {
            //Set members.
            this.settingsModel = new SettingsModel();
            this.MultiPlayerMenuModel = new MultiPlayerMenuModel(settingsModel);

            this.MultiPlayerMenuModel.ConnectionLost += HandleConnectionLost;

            this.MultiPlayerMenuModel.PropertyChanged +=
                delegate(Object sender, PropertyChangedEventArgs e)
                {
                    NotifyPropertyChanged("VM_" + e.PropertyName);
                };
        }

        /// <summary>
        /// Property changed event.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Notifies property changed.
        /// </summary>
        /// <param name="propertyName"></param>
        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Multiplayer menu model property.
        /// </summary>
        public MultiPlayerMenuModel MultiPlayerMenuModel { get; set; }

        /// <summary>
        /// Adds game to list.
        /// </summary>
        /// <param name="gameName"></param>
        public void AddGameToList(String gameName)
        {
            this.MultiPlayerMenuModel.AddGameToList(gameName);
        }

        /// <summary>
        /// Games list getter.
        /// </summary>
        public void GetGameList()
        {
            this.MultiPlayerMenuModel.RequestList();
        }

        /// <summary>
        /// Games list property.
        /// </summary>
        public ObservableCollection<String> VM_ListOfGames
        {
            get { return this.MultiPlayerMenuModel.ListOfGames; }
        }

        /// <summary>
        /// Default rows property.
        /// </summary>
        public String VM_DefaultNumRows
        {
            get
            {
                return GameClient.Properties.Settings.Default.DefaultRows
                    .ToString();
            }
        }

        /// <summary>
        /// Default columns property.
        /// </summary>
        public String VM_DefaultNumCols
        {
            get
            {
                return GameClient.Properties.Settings.Default.DefaultCols
                    .ToString();
            }
        }

        /// <summary>
        /// Connection lost event.
        /// </summary>
        public event EventHandler ConnectionLost;

        /// <summary>
        /// Handles lost connection.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void HandleConnectionLost(object sender, EventArgs e)
        {
            this.ConnectionLost?.Invoke(this, null);
        }
    }
}