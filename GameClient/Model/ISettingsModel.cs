using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace MazeMenu.Model
{
    public interface ISettingsModel : INotifyPropertyChanged
    {

    String IpAddress { get; set; }
        
    int Port { get; set; }

    int DefaultRows { get; set; }

    int DefaultCols { get; set; }

    int DefaultAlgo { get; set; }

    }
}
