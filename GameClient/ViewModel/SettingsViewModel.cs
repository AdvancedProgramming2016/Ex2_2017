using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameClient.Model;
using System.ComponentModel;
using GameClient.ViewModel;

namespace GameClient
{
    public class SettingsViewModel : ISettingsViewModel 
    {

        private ISettingsModel settingsModel;


        public SettingsViewModel(ISettingsModel settingsModel)
        {
            this.settingsModel = settingsModel;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyChanged(string propName)
        {
            if(PropertyChanged != null)
            {
                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propName));
            }
        }

        public string VM_IpAdrress
        {
            get { return this.settingsModel.IpAddress; }
            set
            {
                this.settingsModel.IpAddress = value;
                NotifyChanged("VM_IpAdrress");
            }
        }

        public int VM_Port
        {
            get
            {
                return this.settingsModel.Port;
            }
            set
            {
                this.settingsModel.Port = value;
                this.NotifyChanged("VM_Port");
            }
        }

        public int VM_SelectedAlgo
        {
            get
            {
                return this.settingsModel.DefaultAlgo;
            }
            set
            {
                this.settingsModel.DefaultAlgo = value;
                this.NotifyChanged("VM_SelectedAlgo");
            }
        }

        public int VM_DefaultCol
        {
            get
            {
                return this.settingsModel.DefaultCols;
            }
            set
            {
                this.settingsModel.DefaultCols = value;
                this.NotifyChanged("VM_DefaultCol");
            }
        }

        public int VM_DefaultRow
        {
            get
            {
                return this.settingsModel.DefaultRows;
            }
            set
            {
                this.settingsModel.DefaultRows = value;
                this.NotifyChanged("VM_DefaultRow");
            }
        }
    }
}
