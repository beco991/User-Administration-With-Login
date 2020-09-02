using Administracija.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracija.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {

        #region Polja

        private User currentUser;
        private UserCollection userList;

        #endregion

        #region Propertiji

        public User CurrentUser
        {
            get { return currentUser; }
            set
            {
                if (currentUser==value)
                {
                    return;
                }
                currentUser = value;
                OnPropertyChanged(new PropertyChangedEventArgs("CurrentUser"));
            }
        }

        public UserCollection UserList
        {
            get { return userList; }
            set
            {
                if(userList == value)
                {
                    return;
                }
                userList = value;
                OnPropertyChanged(new PropertyChangedEventArgs("UserList"));
            }
        }

        #endregion

        #region Konstruktori

        public MainWindowViewModel()
        {
            UserList = UserCollection.GetAllUsers();
            CurrentUser = new User();
        }

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }

    }
}
