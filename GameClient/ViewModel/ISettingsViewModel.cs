using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClient.ViewModel
{
    public interface ISettingsViewModel : INotifyPropertyChanged
    {
        event PropertyChangedEventHandler PropertyChanged;

        int VM_SelectedAlgo { get; set; }

        int VM_Port { get; set; }

        int VM_DefaultCol { get; set; }

        int VM_DefaultRow { get; set; }
    }
}