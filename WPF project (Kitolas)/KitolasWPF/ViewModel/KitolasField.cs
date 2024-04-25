using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kitolas.WPF.ViewModel
{
    public class KitolasField : ViewModelBase
    {
        private String _stone = String.Empty;
        public String Stone
        {
            get { return _stone;}

            set
            {
                if(_stone != value)
                {
                    _stone = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
