using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClient.ViewModel
{
    /// <summary>
    /// Settings viewModel interface.
    /// </summary>
    public interface ISettingsViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Property changed event.
        /// </summary>
        event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Selected algorithm property.
        /// </summary>
        int VM_SelectedAlgo { get; set; }

        /// <summary>
        /// Port property.
        /// </summary>
        int VM_Port { get; set; }

        /// <summary>
        /// Default columns property.
        /// </summary>
        int VM_DefaultCol { get; set; }

        /// <summary>
        /// Default rows property.
        /// </summary>
        int VM_DefaultRow { get; set; }

        /// <summary>
        /// Saves the changes.
        /// </summary>
        void SaveSettings();
    }
}