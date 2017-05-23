using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameClient.Model;

namespace GameClient.Model
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
                return GameClient.Properties.Settings.Default.IpAddress;
            }
            set
            {
                GameClient.Properties.Settings.Default.IpAddress = value;
            }
        }

        public int Port
        {
            get
            {
                return GameClient.Properties.Settings.Default.Port;
            }
            set
            {
                GameClient.Properties.Settings.Default.Port = value;
            }
        }

        public int DefaultRows
        {
            get
            {
                return GameClient.Properties.Settings.Default.DefaultRows;
            }
            set
            {
                GameClient.Properties.Settings.Default.DefaultRows = value;
            }
        }

        public int DefaultCols
        {
            get
            {
                return GameClient.Properties.Settings.Default.DefaultCols;
            }
            set
            {
                GameClient.Properties.Settings.Default.DefaultCols = value;
            }
        }

        public int DefaultAlgo {
            get
            {
                return GameClient.Properties.Settings.Default.DefaultAlgo;
            }
            set
            {
                GameClient.Properties.Settings.Default.DefaultAlgo = value;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Save the default settings.
        /// </summary>
        public void SaveSettings()
        {
            GameClient.Properties.Settings.Default.Save();
        }
    }
}
