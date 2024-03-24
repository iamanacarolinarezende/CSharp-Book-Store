using Final_Project.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project.BLL
{
    public class User
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string EmployeeID { get; set; }

        // save user
        public void SaveUser(User user)
        {
            UserDB.SaveInfo(user);
        }

        public List<User> GetUserList()
        {
            return UserDB.GetAllInfos();
        }

        public User SearchUser(string userID)
        {
            return UserDB.SearchRecord(userID);
        }

        public void UpdateUser(User user) => UserDB.UpdateRecord(user);
        public void DeleteUser(string Username) => UserDB.DeleteRecord(Username);

        public bool IsUniqueUName(string userName) => UserDB.IsUniqueUserName(userName);

        public bool ExistEmployeeID (string employeeID) => UserDB.ExistEmployeeID (employeeID);

        public bool ExistUsernameUser(string employeeID) => UserDB.ExistUsernameUser(employeeID);
    }
}
