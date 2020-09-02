using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracija.Model
{
    public class User : INotifyPropertyChanged
    {
        #region Polja

        private int _id;
        private string _userName;
        private string _userPass;
        private bool _isAdmin;
      

        public event PropertyChangedEventHandler PropertyChanged;
        

        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }

        #endregion

        #region Propertiji

        public int Id
        {
            get { return _id; }
            set
            {
                if (_id == value)
                {
                    return;
                }
                _id = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Id"));
            }
        }

        public string UserName
        {
            get { return _userName; }
            set
            {
                if (_userName == value)
                {
                    return;
                }
                _userName = value;
                OnPropertyChanged(new PropertyChangedEventArgs("UserName"));
            }
        }


        public string UserPass
        {
            get { return _userPass; }
            set
            {
                if (_userPass == value)
                {
                    return;
                }
                _userPass = value;
                OnPropertyChanged(new PropertyChangedEventArgs("UserPass"));
            }
        }

        public bool IsAdmin
        {
            get { return _isAdmin; }
            set
            {
                if (_isAdmin == value)
                {
                    return;
                }
                _isAdmin = value;
                OnPropertyChanged(new PropertyChangedEventArgs("IsAdmin"));
            }
        }


        #endregion

        #region Konstruktori

        public User(int Id, string UserName, string UserPass)
        {
            this.Id = Id;
            this.UserName = UserName;
            this.UserPass = UserPass;
        }

        public User(int Id, string UserName, string UserPass, bool IsAdmin) : this(Id, UserName, UserPass)
        {
            this.IsAdmin = IsAdmin;
        }

        public User()
        {
        }

        #endregion

        #region DataAcess

        public static User GetUserFromResultSet(SqlDataReader reader)
        {
            User user = new User((int)reader["Id"], (string)reader["UserName"], (string)reader["UserPass"], (bool)reader["IsAdmin"]);
            return user;
        }

        public void Insert()
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnString"].ToString();
                conn.Open();

                SqlCommand command = new SqlCommand("INSERT INTO Users(UserName, UserPass, IsAdmin) VALUES(@UserName, @UserPass, @IsAdmin, 0); SELECT IDENT_CURRENT('Users');", conn);

                SqlParameter userNameParam = new SqlParameter("@UserName", SqlDbType.NVarChar);
                userNameParam.Value = this.UserName;

                SqlParameter userPassParam = new SqlParameter("@UserPass", SqlDbType.NVarChar);
                userPassParam.Value = this.UserPass;

                SqlParameter isAdminParam = new SqlParameter("@IsAdmin", SqlDbType.Bit);
                isAdminParam.Value = this.IsAdmin;

                command.Parameters.Add(userNameParam);
                command.Parameters.Add(userPassParam);
                command.Parameters.Add(isAdminParam);

                var id = command.ExecuteScalar();

                if (id != null)
                {
                    this.Id = Convert.ToInt32(id);
                }

            }
        }

        public void Update()
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnString"].ToString();
                conn.Open();

                SqlCommand command = new SqlCommand("UPDATE Users SET UserName=@UserName, UserPass=@UserPass, IsAdmin=@IsAdmin WHERE id=@Id", conn);

                SqlParameter userNameParam = new SqlParameter("@UserName", SqlDbType.NVarChar);
                userNameParam.Value = this.UserName;

                SqlParameter userPassParam = new SqlParameter("@UserPass", SqlDbType.NVarChar);
                userNameParam.Value = this.UserName;

                SqlParameter isAdminParam = new SqlParameter("@IsAdmin", SqlDbType.Bit);
                isAdminParam.Value = this.IsAdmin;

                command.Parameters.Add(userNameParam);
                command.Parameters.Add(userPassParam);
                command.Parameters.Add(isAdminParam);
            }
        }

        public IEnumerable GetErrors(string propertyName)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
