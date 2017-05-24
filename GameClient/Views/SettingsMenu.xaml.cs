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

namespace GameClient.Views
{
    /// <summary>
    /// Interaction logic for SettingsMenu.xaml
    /// </summary>
    public partial class SettingsMenu : Window
    {
        /// <summary>
        /// The view model.
        /// </summary>
        private SettingsViewModel svm;

        /// <summary>
        /// Ctor.
        /// </summary>
        public SettingsMenu()
        {
            InitializeComponent();
            ISettingsModel settingsModel = new SettingsModel();
            this.svm = new SettingsViewModel(settingsModel);
            this.DataContext = svm;
        }
    }
}
