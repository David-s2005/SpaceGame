using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Media.Imaging;

namespace SpaceGame
{
    public class ViewModel : INotifyPropertyChanged
    {
        private string _yourProperty;

        public string YourProperty
        {
            get { return _yourProperty; }
            set
            {
                if (_yourProperty != value)
                {
                    _yourProperty = value;
                    OnPropertyChanged(nameof(YourProperty));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
