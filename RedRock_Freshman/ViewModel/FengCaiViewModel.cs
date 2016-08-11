using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace RedRock_Freshman.ViewModel
{
    public class FengCaiViewModel : BasePageViewModel
    {
        private ObservableCollection<Model.fengcaiheaders> _header;
        public ObservableCollection<Model.fengcaiheaders> Header
        {
            get
            {
                return _header;
            }
            set
            {
                _header = value;
                RaisePropertyChanged(nameof(Header));
            }
        }

        private ObservableCollection<Model.zuzhi> _zuzhi;
        public ObservableCollection<Model.zuzhi> ZuZhi
        {
            get
            {
                return _zuzhi;
            }
            set
            {
                _zuzhi = value;
                RaisePropertyChanged(nameof(ZuZhi));
            }
        }

        private ObservableCollection<Model.zuzhi_intro> _zuzhi_intro;
        public ObservableCollection<Model.zuzhi_intro> Zuzhi_Intro
        {
            get
            {
                return _zuzhi_intro;
            }
            set
            {
                _zuzhi_intro = value;
                RaisePropertyChanged(nameof(Zuzhi_Intro));
            }
        }

        private ObservableCollection<string> _zuimei;
        public ObservableCollection<string> ZuiMei
        {
            get
            {
                return _zuimei;
            }
            set
            {
                _zuimei = value;
                RaisePropertyChanged(nameof(ZuiMei));
            }
        }

        private ObservableCollection<Model.yuanchuang> _yuanchuang;
        public ObservableCollection<Model.yuanchuang> YuanChuang
        {
            get
            {
                return _yuanchuang;
            }
            set
            {
                _yuanchuang = value;
                RaisePropertyChanged(nameof(_yuanchuang));
            }
        }
    }
}
