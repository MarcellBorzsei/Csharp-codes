using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KitolasMaui.ViewModel
{
    public class KitolasField : ViewModelBase
    {
        private String _stone = String.Empty;

        private Color backgroundColor = Colors.Wheat;

        public Color BackgroundColor
        {
            get { return backgroundColor; }
            set
            {
                if (backgroundColor != value)
                {
                    backgroundColor = value;
                    OnPropertyChanged(nameof(BackgroundColor));
                }
            }
        }

        public Int32 X { get; set; }
        public Int32 Y { get; set; }

        public String Stone
        {
            get { return _stone; }

            set
            {
                if (_stone != value)
                {
                    _stone = value;
                    OnPropertyChanged();
                }
            }
        }

    }
}
