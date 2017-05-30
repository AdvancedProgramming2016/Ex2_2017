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

namespace GameClient.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Single player click.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void singlePlayer_Click(object sender, RoutedEventArgs e)
        {
            SinglePlayerMenu sp = new SinglePlayerMenu();
            sp.Show();
            this.Close();
        }

        /// <summary>
        /// Multiplayer click.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void multiPlayer_Click(object sender, RoutedEventArgs e)
        {
            MultiplePlayerMenu mp = new MultiplePlayerMenu();
            mp.Show();
            this.Close();
        }

        /// <summary>
        /// Settings click.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void settings_Click(object sender, RoutedEventArgs e)
        {
            SettingsMenu sm = new SettingsMenu();
            sm.Show();
            this.Close();
        }
    }
}