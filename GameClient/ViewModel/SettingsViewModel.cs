using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameClient.Model;
using System.ComponentModel;
using GameClient.ViewModel;

namespace GameClient.ViewModel
{
    /// <summary>
    /// Settings viewModel.
    /// </summary>
    public class SettingsViewModel : ISettingsViewModel
    {
        /// <summary>
        /// Settings model reference.
        /// </summary>
        private ISettingsModel settingsModel;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="settingsModel">Settings model.</param>
        public SettingsViewModel(ISettingsModel settingsModel)
        {
            this.settingsModel = settingsModel;
        }

        /// <summary>
        /// Property changed event.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Nitifies property changed.
        /// </summary>
        /// <param name="propName"></param>
        public void NotifyChanged(string propName)
        {
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(propName));
        }

        /// <summary>
        /// Ip property.
        /// </summary>
        public string VM_IpAdrress
        {
            get { return this.settingsModel.IpAddress; }
            set
            {
                this.settingsModel.IpAddress = value;
                NotifyChanged("VM_IpAdrress");
            }
        }

        /// <summary>
        /// Port property.
        /// </summary>
        public int VM_Port
        {
            get { return this.settingsModel.Port; }
            set
            {
                this.settingsModel.Port = value;
                this.NotifyChanged("VM_Port");
            }
        }

        /// <summary>
        /// Selected algorithm property.
        /// </summary>
        public int VM_SelectedAlgo
        {
            get { return this.settingsModel.DefaultAlgo; }
            set
            {
                this.settingsModel.DefaultAlgo = value;
                this.NotifyChanged("VM_SelectedAlgo");
            }
        }

        /// <summary>
        /// Default columns property.
        /// </summary>
        public int VM_DefaultCol
        {
            get { return this.settingsModel.DefaultCols; }
            set
            {
                this.settingsModel.DefaultCols = value;
                this.NotifyChanged("VM_DefaultCol");
            }
        }

        /// <summary>
        /// Default rows property.
        /// </summary>
        public int VM_DefaultRow
        {
            get { return this.settingsModel.DefaultRows; }
            set
            {
                this.settingsModel.DefaultRows = value;
                this.NotifyChanged("VM_DefaultRow");
            }
        }

        /// <summary>
        /// Saves the changes.
        /// </summary>
        public void SaveSettings()
        {
            settingsModel.SaveSettings();
        }
    }
}