using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ShopDesktop.ViewModels
{
    class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnChanged(string prop) 
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
