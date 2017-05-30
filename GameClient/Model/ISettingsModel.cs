using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace GameClient.Model
{
    /// <summary>
    /// Settings model interface.
    /// </summary>
    public interface ISettingsModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Ip property.
        /// </summary>
        String IpAddress { get; set; }

        /// <summary>
        /// Port property.
        /// </summary>
        int Port { get; set; }

        /// <summary>
        /// Default rows property.
        /// </summary>
        int DefaultRows { get; set; }

        /// <summary>
        /// Default cols property.
        /// </summary>
        int DefaultCols { get; set; }

        /// <summary>
        /// Default algorithm property.
        /// </summary>
        int DefaultAlgo { get; set; }

        /// <summary>
        /// Saves changes.
        /// </summary>
        void SaveSettings();
    }
}