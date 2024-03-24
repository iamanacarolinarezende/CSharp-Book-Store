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
                MessageBox.Show("This UserName already exists.\nPlease enter another one.", "Invalid UserName", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxUserName.Clear();
                textBoxUserName.Focus();
                return;
            }

            //Validate Password
            String inputPass = textBoxPassword.Text.Trim();
            if (!Validator.IsValidPassword(inputPass))
            {
                MessageBox.Show("Password must contain at least 8 letters, one upper case,one lower case and one number.\nPlease enter another one.", "Invalid Password", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxPassword.Clear();
                textBoxPassword.Focus();
                return;
            }

            //Chack if Employee Exist
            String input = textBoxEID.Text.Trim();
            if (!user.ExistEmployeeID(input))
            {
                MessageBox.Show("This EmployeeID does not exist.\nPlease enter another one.", "Invalid EmployeeID", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            //Check if email exist
            if (!Validator.IsValidEmailFormat(userName))
            {
                MessageBox.Show("Invalid Email format.", "Invalid UserName", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxUserName.Clear();
                textBoxUserName.Focus();
                return;
            }

            //Check if employeeID exist
            if (!userUpdate.ExistEmployeeID(empID))
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
            string username = textBoxUserName.Text.Trim();
            //Check Empty
            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Please enter a username.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //Check Email format
            if (!Validator.IsValidEmailFormat(username))
            {
                MessageBox.Show("Invalid Email format.", "Invalid UserName", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxUserName.Clear();
                textBoxUserName.Focus();
                return;
            }

            //Check if UserName exist
            User userDel = new User();
            if (!userDel.ExistUsernameUser(username))
            {
                MessageBox.Show("This UserName does not exist.\nPlease enter another one.", "Invalid UserName", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxEID.Clear();
                textBoxEID.Focus();
                return;
            }

            //Delete User
            var answer = MessageBox.Show("Do you really want to delete this employee?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (answer == DialogResult.Yes)
            {
                userDel.DeleteUser(username);
                MessageBox.Show("Employee data has been deleted successfully.", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            textBoxUserName.Clear();
            textBoxPassword.Clear();
            textBoxEID.Clear();
            textBoxUserName.Focus();
        }
    }
}
