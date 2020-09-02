using Administracija.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracija.ViewModel
{
    public class LoginWindowViewModel : INotifyPropertyChanged
    {

        private User currentUser;
        public User CurrentUser
        {
            get { return CurrentUser; }
            set
            {
                if (CurrentUser == value)
                {
                    return;
                }
                CurrentUser = value;
                OnPropertyChanged(new PropertyChangedEventArgs("CurrentUser"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }
    }
}
