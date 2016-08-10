using RedRock_Freshman.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Collections.ObjectModel;

namespace RedRock_Freshman.ViewModel
{
     public class StrategyViewModel:BasePageViewModel
    {
        private ObservableCollection<StrategyHeader> header;

        public ObservableCollection<StrategyHeader> Header
        {
            get
            {
                return header;
            }

            set
            {
                header = value;
                RaisePropertyChanged(nameof(Header));
            }
        }
    }
}
