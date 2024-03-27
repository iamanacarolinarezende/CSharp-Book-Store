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
using System.Security.Cryptography;
using System.Xml.Linq;

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
                MessageBox.Show("Please enter an username.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void buttonList_Click(object sender, EventArgs e)
        {
            listViewUser.Items.Clear();
            User user = new User();
            List<User> listU = user.GetUserList();

            foreach (User uname in listU)
            {
                ListViewItem item = new ListViewItem(uname.UserName);
                item.SubItems.Add(uname.EmployeeID);
                listViewUser.Items.Add(item);
            }
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            listViewUser.Items.Clear();
            if (comboBoxSearchOption.SelectedIndex == -1 || comboBoxSearchOption.SelectedIndex == 0)
            {
                MessageBox.Show("Please select one option first", "Search option", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // search by UserName
            User uName = new User();
            string input = "";
            switch (comboBoxSearchOption.SelectedIndex)
            {
                case 1: //Search by UserName
                    input = textBoxSearch.Text.Trim();
                    if (!Validator.IsValidEmailFormat(input))
                    {
                        listViewUser.Items.Clear();
                        MessageBox.Show("Username must be an valid Email.", "Invalid Employee Email", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textBoxUserName.Clear();
                        textBoxUserName.Focus();
                        return;
                    }
                    uName = uName.SearchUser(input);
                    if (uName != null)
                    {
                        textBoxUserName.Text = uName.UserName.ToString();
                        textBoxPassword.Text = uName.Password.ToString();
                        textBoxEID.Text = uName.EmployeeID.ToString();
                    }
                    else
                    {
                        MessageBox.Show("Employee not found!", "Invalid Employee Id", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textBoxSearch.Clear();
                        textBoxSearch.Focus();

                        textBoxUserName.Clear();
                        textBoxPassword.Clear();

                    }
                    break;
                case 2: //Search by Employee ID (falta fazer o overload)
                    input = textBoxSearch.Text.Trim();
                    uName = uName.SearchUserEmpID(input);
                    if (uName != null)
                    {
                        textBoxUserName.Text = uName.UserName.ToString();
                        textBoxPassword.Text = uName.Password.ToString();
                        textBoxEID.Text = uName.EmployeeID.ToString();
                    }
                    else
                    {
                        MessageBox.Show("Employee not found!", "Invalid Employee Id", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textBoxSearch.Clear();
                        textBoxSearch.Focus();

                        textBoxUserName.Clear();
                        textBoxPassword.Clear();
                    }
                    break;
                default:
                    break;
            }
        }

        private void comboBoxSearchOption_SelectedIndexChanged(object sender, EventArgs e)
        {
            int indexSelected = comboBoxSearchOption.SelectedIndex;
            switch (indexSelected)
            {
                case 1:
                    labelMessageU.Text = "Please enter the UserName";
                    textBoxSearch.Clear();
                    textBoxSearch.Focus();

                    textBoxUserName.Clear();
                    textBoxPassword.Clear();
                    textBoxEID.Clear();
                    break;
                case 2:
                    labelMessageU.Text = "Please enter the Employee ID";
                    textBoxSearch.Clear();
                    textBoxSearch.Focus();

                    textBoxUserName.Clear();
                    textBoxPassword.Clear();
                    textBoxEID.Clear();
                    break;
            }

        }

        //===================================== EMPLOYEE FORM ==========================================
        private void Main_Load(object sender, EventArgs e)
        {
            textBoxLN.Visible = false;
            labelLN.Visible = false;
        }

        private void buttonExitE_Click(object sender, EventArgs e)
        {
            var answer = MessageBox.Show("Do you really want to exit the application?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (answer == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void buttonSaveEmp_Click(object sender, EventArgs e)
        {
            string EID = textBoxEmployeeId.Text.Trim();
            string FName = textBoxFirstName.Text.Trim();
            string LName = textBoxLastName.Text.Trim();
            string JTitle = textBoxJobTitle.Text.Trim();
            string Phone = textBoxPhoneEmp.Text.Trim();


            //Check Empty textbox
            if (string.IsNullOrEmpty(EID) && string.IsNullOrEmpty(FName) && string.IsNullOrEmpty(LName) && string.IsNullOrEmpty(JTitle))
            {
                MessageBox.Show("Please fill in all required fields (*).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //Check ID format
            if (!Validator.IsValidId(EID))
            {
                MessageBox.Show("Employee Id must be 4-digit number.", "Invalid Employee ID", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxEmployeeId.Clear();
                textBoxEmployeeId.Focus();
                return;
            }

            // Check if employee ID is unique
            Employee emp = new Employee();
            if (!emp.IsUniqueEmployeeId(Convert.ToInt32(EID)))
            {
                MessageBox.Show("Employee Id must be unique.\n" + "Please enter another EmployeeId.", "Duplicate EmployeeId", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxEmployeeId.Clear();
                textBoxEmployeeId.Focus();
                return;

            }

            //Validate First Name
            if (!Validator.IsValidName(FName))
            {
                MessageBox.Show("Invalid First Name.", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxFirstName.Clear();
                textBoxFirstName.Focus();
                return;

            }

            //Validate Last Name
            if (!Validator.IsValidName(LName))
            {
                MessageBox.Show("Invalid Last Name.", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxLastName.Clear();
                textBoxLastName.Focus();
                return;

            }

            //Validate job title
            if (!Validator.IsValidName(JTitle))
            {
                MessageBox.Show("Invalid Job Title.", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxJobTitle.Clear();
                textBoxJobTitle.Focus();
                return;

            }

            //Valid Phone
            if (!Validator.IsValidPhone(Phone))
            {
                MessageBox.Show("Invalid Phone Number. Must be: 123-456-7890 or 123 456 7890\r\n or 1234567890\r\n", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxJobTitle.Clear();
                textBoxJobTitle.Focus();
                return;

            }

            //Save Employee
            emp.EmployeeID = Convert.ToInt32(textBoxEmployeeId.Text.Trim());
            emp.FirstName = textBoxFirstName.Text.Trim();
            emp.LastName = textBoxLastName.Text.Trim();
            emp.JobTitle = textBoxJobTitle.Text.Trim();
            emp.Phone = textBoxPhoneEmp.Text.Trim();
            emp.SaveEmployee(emp);
            MessageBox.Show("Employee data has been saved successfully.", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);

            textBoxEmployeeId.Clear();
            textBoxFirstName.Clear();
            textBoxLastName.Clear();
            textBoxJobTitle.Clear();
            textBoxPhoneEmp.Clear();
            textBoxEmployeeId.Focus();

        }

        private void buttonUpdateEmp_Click(object sender, EventArgs e)
        {
            string EID = textBoxEmployeeId.Text.Trim();
            string FName = textBoxFirstName.Text.Trim();
            string LName = textBoxLastName.Text.Trim();
            string JTitle = textBoxJobTitle.Text.Trim();
            string Phone = textBoxPhoneEmp.Text.Trim();

            // Check if employee ID is exist
            Employee emp = new Employee();
            if (emp.IsUniqueEmployeeId(Convert.ToInt32(EID)))
            {
                MessageBox.Show("Employee Id does not exist.\n" + "Please enter another EmployeeId.", "Duplicate EmployeeId", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxEmployeeId.Clear();
                textBoxEmployeeId.Focus();
                return;

            }

            //Validate First Name
            if (!Validator.IsValidName(FName))
            {
                MessageBox.Show("Invalid First Name.", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxFirstName.Clear();
                textBoxFirstName.Focus();
                return;

            }

            //Validate Last Name
            if (!Validator.IsValidName(LName))
            {
                MessageBox.Show("Invalid Last Name.", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxLastName.Clear();
                textBoxLastName.Focus();
                return;

            }

            //Validate job title
            if (!Validator.IsValidName(JTitle))
            {
                MessageBox.Show("Invalid Job Title.", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxJobTitle.Clear();
                textBoxJobTitle.Focus();
                return;

            }

            //Valid Phone
            if (!Validator.IsValidPhone(Phone))
            {
                MessageBox.Show("Invalid Phone Number. Must be: 123-456-7890 or 123 456 7890\r\n or 1234567890\r\n", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxPhoneEmp.Clear();
                textBoxPhoneEmp.Focus();
                return;

            }

            Employee empUpdated = new Employee();
            empUpdated.EmployeeID = Convert.ToInt32(textBoxEmployeeId.Text.Trim());
            empUpdated.FirstName = textBoxFirstName.Text.Trim();
            empUpdated.LastName = textBoxLastName.Text.Trim();
            empUpdated.JobTitle = textBoxJobTitle.Text.Trim();
            empUpdated.Phone = textBoxPhoneEmp.Text.Trim();
            empUpdated.UpdateEmployee(empUpdated);
            MessageBox.Show("Employee data has been updated successfully.", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);

            textBoxEmployeeId.Clear();
            textBoxFirstName.Clear();
            textBoxLastName.Clear();
            textBoxJobTitle.Clear();
            textBoxPhoneEmp.Clear();
            textBoxEmployeeId.Focus();
        }

        private void buttonDeleteEmp_Click(object sender, EventArgs e)
        {
            string EID = textBoxEmployeeId.Text.Trim();
            if (string.IsNullOrEmpty(EID))
            {
                MessageBox.Show("Please fill in the Emploee ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Employee empDeleted = new Employee();
            var answer = MessageBox.Show("Do you really want to delete this employee?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (answer == DialogResult.Yes)
            {
                empDeleted.DeleteEmployee(Convert.ToInt32(textBoxEmployeeId.Text.Trim()));
                MessageBox.Show("Employee data has been deleted successfully.", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            textBoxEmployeeId.Clear();
            textBoxFirstName.Clear();
            textBoxLastName.Clear();
            textBoxJobTitle.Clear();
            textBoxPhoneEmp.Clear();
            textBoxEmployeeId.Focus();
        }

        private void buttonListAllEmployee_Click(object sender, EventArgs e)
        {
            listViewEmployee.Items.Clear();
            Employee employee = new Employee();
            List<Employee> listE = employee.GetEmployeeList();

            foreach (Employee emp in listE)
            {
                ListViewItem item = new ListViewItem(emp.EmployeeID.ToString());
                item.SubItems.Add(emp.FirstName);
                item.SubItems.Add(emp.LastName);
                item.SubItems.Add(emp.JobTitle);
                item.SubItems.Add(emp.Phone);
                listViewEmployee.Items.Add(item);
            }
        }
    }
}
