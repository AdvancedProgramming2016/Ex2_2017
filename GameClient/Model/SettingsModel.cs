using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameClient.Model;

namespace GameClient.Model
{
    /// <summary>
    /// Settings model.
    /// </summary>
    public class SettingsModel : ISettingsModel
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public SettingsModel()
        {
            Properties.Settings.Default.Reload();
        }

        /// <summary>
        /// Ip property.
        /// </summary>
        public String IpAddress
        {
            get { return GameClient.Properties.Settings.Default.IpAddress; }
            set { GameClient.Properties.Settings.Default.IpAddress = value; }
        }


        /// <summary>
        /// Port property.
        /// </summary>
        public int Port
        {
            get { return GameClient.Properties.Settings.Default.Port; }
            set { GameClient.Properties.Settings.Default.Port = value; }
        }


        /// <summary>
        /// Default rows property.
        /// </summary>
        public int DefaultRows
        {
            get { return GameClient.Properties.Settings.Default.DefaultRows; }
            set { GameClient.Properties.Settings.Default.DefaultRows = value; }
        }

        /// <summary>
        /// Default columns property.
        /// </summary>
        public int DefaultCols
        {
            get { return GameClient.Properties.Settings.Default.DefaultCols; }
            set { GameClient.Properties.Settings.Default.DefaultCols = value; }
        }

        /// <summary>
        /// Default algorithm property.
        /// </summary>
        public int DefaultAlgo
        {
            get { return GameClient.Properties.Settings.Default.DefaultAlgo; }
            set { GameClient.Properties.Settings.Default.DefaultAlgo = value; }
        }

        /// <summary>
        /// Property changed event.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Saves the default settings.
        /// </summary>
        public void SaveSettings()
        {
            GameClient.Properties.Settings.Default.Save();
        }
    }
}