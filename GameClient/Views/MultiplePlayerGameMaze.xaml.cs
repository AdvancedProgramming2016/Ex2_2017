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
    /// Interaction logic for MultiplePlayerGameMaze.xaml
    /// </summary>
    public partial class MultiplePlayerGameMaze : Window
    {
        private MultiPlayerGameViewModel mpgvm;

        public MultiplePlayerGameMaze()
        {
            InitializeComponent();
            this.mpgvm = new MultiPlayerGameViewModel
                (new MultiPlayerModel(new SettingsModel()));
            this.DataContext = this.mpgvm;
            this.mpgvm.StartNewGame();
        }

        /// <summary>
        /// Go back to main menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackMainMenuButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }
    }
}
