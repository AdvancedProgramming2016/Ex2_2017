using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeMenu.Model;

namespace MazeMenu.Model
{
    public class SettingsModel : ISettingsModel
    {
        public SettingsModel()
        {
            Properties.Settings.Default.Reload();
        }

        public String IpAddress {
            get
            {
                return MazeMenu.Properties.Settings.Default.IpAddress;
            }
            set
            {
                MazeMenu.Properties.Settings.Default.IpAddress = value;
            }
        }

        public int Port
        {
            get
            {
                return MazeMenu.Properties.Settings.Default.Port;
            }
            set
            {
                MazeMenu.Properties.Settings.Default.Port = value;
            }
        }

        public int DefaultRows
        {
            get
            {
                return MazeMenu.Properties.Settings.Default.DefaultRows;
            }
            set
            {
                MazeMenu.Properties.Settings.Default.DefaultRows = value;
            }
        }

        public int DefaultCols
        {
            get
            {
                return MazeMenu.Properties.Settings.Default.DefaultCols;
            }
            set
            {
                MazeMenu.Properties.Settings.Default.DefaultCols = value;
            }
        }

        public int DefaultAlgo {
            get
            {
                return MazeMenu.Properties.Settings.Default.DefaultAlgo;
            }
            set
            {
                MazeMenu.Properties.Settings.Default.DefaultAlgo = value;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Save the default settings.
        /// </summary>
        public void SaveSettings()
        {
            MazeMenu.Properties.Settings.Default.Save();
        }
    }
}
