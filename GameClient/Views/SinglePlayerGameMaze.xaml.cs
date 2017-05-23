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
    /// Interaction logic for SinglePlayerGameMaze.xaml
    /// </summary>
    public partial class SinglePlayerGameMaze : Window
    {

        private SinglePlayerGameViewModel spViewModel;

        public SinglePlayerGameMaze(String numOfRows, String numOfCols, String nameOfMaze)
        {
            InitializeComponent();
            this.spViewModel = new SinglePlayerGameViewModel
                (new SinglePlayerGameModel(), new SettingsModel());
            this.DataContext = this.spViewModel;
            this.spViewModel.StartNewGame(numOfRows, numOfCols, nameOfMaze);
        }

        /// <summary>
        /// Opens the Main menu when clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }

        private void MazeBoard_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
