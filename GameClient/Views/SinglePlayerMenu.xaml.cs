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
    /// Interaction logic for SinglePlayerMenu.xaml
    /// </summary>
    public partial class SinglePlayerMenu : Window
    {
        /// <summary>
        /// Single player viewModel.
        /// </summary>
        private SinglePlayerGameViewModel singlePlayerGameViewModel;

        /// <summary>
        /// Settings model.
        /// </summary>
        private ISettingsModel settingsModel;

        /// <summary>
        /// Settings viewModel.
        /// </summary>
        private ISettingsViewModel settingsViewModel;

        /// <summary>
        /// Constructor.
        /// </summary>
        public SinglePlayerMenu()
        {
            InitializeComponent();

            //Initialize members.
            settingsModel = new SettingsModel();
            this.singlePlayerGameViewModel = new SinglePlayerGameViewModel
            (new SinglePlayerGameModel(settingsModel),
                new SettingsViewModel(settingsModel));

            //Set data context.
            this.DataContext = this.singlePlayerGameViewModel;
        }

        /// <summary>
        /// Ok click.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            // Open maze window.
            try
            {
                new SinglePlayerGameMaze(numOfRowsBox.Text, numOfCols.Text,
                    MazeNameBox.Text).Show();
            }
            catch (Exception exception)
            {
                new MainWindow().Show();
            }
            this.Close();
        }

        /// <summary>
        /// Cancel click.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}