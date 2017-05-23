using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeMenu.Model;
using System.ComponentModel;

namespace MazeMenu
{
    public class SettingsViewModel : INotifyPropertyChanged 
    {

        private SettingsModel settingModel;

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyChanged(string propName)
        {
            if(PropertyChanged != null)
            {
                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propName));
            }
        }

        public SettingsViewModel()
        {
            this.settingModel = new SettingsModel();
        }

        public int VM_SelectedAlgo
        {
            get
            {
                return this.settingModel.DefaultAlgo;
            }
            set
            {
                this.settingModel.DefaultAlgo = value;
                this.NotifyChanged("VM_SelectedAlgo");
            }
        }

        public int VM_Port
        {
            get
            {
                return this.settingModel.Port;
            }
            set
            {
                this.settingModel.Port = value;
                this.NotifyChanged("VM_Port");
            }
        }

        public int VM_DefaultCol
        {
            get
            {
                return this.settingModel.DefaultCols;
            }
            set
            {
                this.settingModel.DefaultCols = value;
                this.NotifyChanged("VM_DefaultCol");
            }
        }

        public int VM_DefaultRow
        {
            get
            {
                return this.settingModel.DefaultRows;
            }
            set
            {
                this.settingModel.DefaultRows = value;
                this.NotifyChanged("VM_DefaultRow");
            }
        }
    }
}
