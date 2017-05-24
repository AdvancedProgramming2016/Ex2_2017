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
using GameClient.ViewModel;
using GameClient.Model;

namespace GameClient.Views
{
    /// <summary>
    /// Interaction logic for MultiplePlayerMenu.xaml
    /// </summary>
    public partial class MultiplePlayerMenu : Window
    {

        private MultiPlayerGameViewModel multiPlayerGameViewModel;
        private ISettingsModel settingsModel;
        private ISettingsViewModel settingsViewModel;

        public MultiplePlayerMenu()
        {
            InitializeComponent();
            settingsModel = new SettingsModel();
            this.multiPlayerGameViewModel = new MultiPlayerGameViewModel(new MultiPlayerModel(settingsModel));
            this.DataContext = this.multiPlayerGameViewModel;
        }
    }
}
