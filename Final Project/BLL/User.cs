using Final_Project.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Final_Project.BLL
{
    public class User
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string EmployeeID { get; set; }
        public string Position { get; set; }

        //Save User
        public void SaveUser(User user) =>UserDB.SaveInfo(user);
        
        //List all users
        public List<User> GetUserList()
        {
            return UserDB.GetAllInfos();
        }

        //Search user by Username
        public User SearchUser(string userName)
        {
            return UserDB.SearchRecord(userName);
        }

        //Search user by Employee ID
        public User SearchUserEmpID(string userID)
        {
            return UserDB.SearchRecordEID(userID);
        }

        //Update the user
        public void UpdateUser(User user) => UserDB.UpdateRecord(user);

        //Delete User
        public void DeleteUser(string Username) => UserDB.DeleteRecord(Username);

        //check if username is unique at table of users.
        public bool IsUniqueUName(string userName) => UserDB.IsUniqueUserName(userName);

        //check if employee exist at the table of employees.
        public bool ExistEmployeeID_ET (string employeeID) => UserDB.ExistEmployeeID_empTable(employeeID);

        //check if user ID exist at table of users.
        public bool ExistEmployeeID_UT(string employeeID) => UserDB.ExistEmpIdUser(employeeID);

        //Is valid User and Password
        public bool UserLogin (string userName, string password) => UserDB.UserExistsLogin(userName, password);

        //Get jobtitle from Employee
        public static string GetEmployeeJob(string username, string password) => UserDB.GetUserJobTitle(username, password);
    }
}
