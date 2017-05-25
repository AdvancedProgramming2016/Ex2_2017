using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using GameClient.Model;
using GameClient.ViewModel;
using System.Collections.ObjectModel;

namespace GameClient.Views
{
    /// <summary>
    /// Interaction logic for MultiplePlayerMenu.xaml
    /// </summary>
    public partial class MultiplePlayerMenu : Window
    {

        private MultiPlayerMenuViewModel multiPlayerMenuViewModel;
        private ISettingsModel settingsModel;
        private ISettingsViewModel settingsViewModel;
        private ObservableCollection<string> listOfGames;

        public MultiplePlayerMenu()
        {
            InitializeComponent();
            //this.listOfGames = ObservableCollection<string>(); Might need to initialize the collection
            this.multiPlayerMenuViewModel = new MultiPlayerMenuViewModel();
            //this.settingsModel = new SettingsModel();
            //this.multiPlayerGameViewModel = new MultiPlayerGameViewModel
            //    (new MultiPlayerModel(settingsModel), new SettingsViewModel(settingsModel));
            this.DataContext = this.multiPlayerMenuViewModel;
        }
    }
}
