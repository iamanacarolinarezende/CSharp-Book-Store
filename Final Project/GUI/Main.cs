using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Final_Project.DAL;
using Final_Project.VALIDATION;
using Final_Project.BLL;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Final_Project.GUI
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlConnection conn = UtilityDB.GetDBConnection();
            MessageBox.Show("Database connection is " + conn.State.ToString());
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            var answer = MessageBox.Show("Do you really want to exit the application?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (answer == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void buttonSaveUser_Click(object sender, EventArgs e)
        {
            string userName = textBoxUserName.Text.Trim();
            string inputPass = textBoxPassword.Text.Trim();
            string input = textBoxEID.Text.Trim();

            //Check Empty
            if (string.IsNullOrEmpty(userName))
            {
                MessageBox.Show("Please enter a username.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //Check Email format
            if (!Validator.IsValidEmailFormat(userName))
            {
                MessageBox.Show("Invalid Email format.", "Invalid UserName", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxUserName.Clear();
                textBoxUserName.Focus();
                return;
            }

            // Check is email exist
            User user = new User();
            if (!user.IsUniqueUName(userName))
            {
                MessageBox.Show("This Email already exists.\nPlease enter another one.", "Invalid UserName", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxUserName.Clear();
                textBoxUserName.Focus();
                return;
            }

            //Validate Password
            if (!Validator.IsValidPassword(inputPass))
            {
                MessageBox.Show("Password must contain at least 8 letters, one upper case,one lower case and one number.\nPlease enter another one.", "Invalid Password", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxPassword.Clear();
                textBoxPassword.Focus();
                return;
            }

            //Check if Employee Exist at table of employee
            if (!user.ExistEmployeeID_ET(input))
            {
                MessageBox.Show("This EmployeeID does not exist or cannot be empty.\nPlease enter another one.", "Invalid EmployeeID", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxEID.Clear();
                textBoxEID.Focus();
                return;
            }

            //Check if employeeID exist at table of users, so the employee does not have 2 acess.
            if (user.ExistEmployeeID_UT(input))
            {
                MessageBox.Show("This EmployeeID already have an User.\nPlease enter another one.", "Invalid EmployeeID", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxEID.Clear();
                textBoxEID.Focus();
                return;
            }

            //Save User
            user.UserName = textBoxUserName.Text.Trim();
            user.Password = textBoxPassword.Text.Trim();
            user.EmployeeID = textBoxEID.Text.Trim();
            user.SaveUser(user);
            MessageBox.Show("User Data has been saved successfully.", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            textBoxUserName.Clear();
            textBoxPassword.Clear();
            textBoxEID.Clear();
            textBoxUserName.Focus();

        }

        private void buttonUpdateUser_Click(object sender, EventArgs e)
        {
            string userName = textBoxUserName.Text.Trim();
            string empID = textBoxEID.Text.Trim();
            User userUpdate = new User();

            //Check Empty
            if (string.IsNullOrEmpty(userName))
            {
                MessageBox.Show("Please enter a username.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //Check Email format
            if (!Validator.IsValidEmailFormat(userName))
            {
                MessageBox.Show("Invalid Email format.", "Invalid UserName", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxUserName.Clear();
                textBoxUserName.Focus();
                return;
            }

            //Check if employeeID exist at table of users
            if (!userUpdate.ExistEmployeeID_UT(empID))
            {
                MessageBox.Show("This EmployeeID does not exist.\nPlease enter another one.", "Invalid EmployeeID", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxEID.Clear();
                textBoxEID.Focus();
                return;
            }

            
            userUpdate.UserName = textBoxUserName.Text.Trim();
            userUpdate.Password = textBoxPassword.Text.Trim();
            userUpdate.EmployeeID = textBoxEID.Text.Trim();
            userUpdate.UpdateUser(userUpdate);
            MessageBox.Show("User data has been updated successfully.", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            textBoxUserName.Clear();
            textBoxPassword.Clear();
            textBoxEID.Clear();
            textBoxUserName.Focus();
        }

        private void buttonDeleteUser_Click(object sender, EventArgs e)
        {
            string userName = textBoxUserName.Text.Trim();
            //Check Empty
            if (string.IsNullOrEmpty(userName))
            {
                MessageBox.Show("Please enter a username.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //Check Email format
            if (!Validator.IsValidEmailFormat(userName))
            {
                MessageBox.Show("Invalid Email format.", "Invalid UserName", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxUserName.Clear();
                textBoxUserName.Focus();
                return;
            }

            //Check if UserName exist, if email exist
            User userDel = new User();
            if (userDel.IsUniqueUName(userName))
            {
                MessageBox.Show("This UserName already exists.\nPlease enter another one.", "Invalid UserName", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxUserName.Clear();
                textBoxUserName.Focus();
                return;
            }

            //Delete User
            var answer = MessageBox.Show("Do you really want to delete this employee?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (answer == DialogResult.Yes)
            {
                userDel.DeleteUser(userName);
                MessageBox.Show("Employee data has been deleted successfully.", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            textBoxUserName.Clear();
            textBoxPassword.Clear();
            textBoxEID.Clear();
            textBoxUserName.Focus();
        }
    }
}
