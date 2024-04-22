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
using Final_Project.DAL;
using Final_Project.VALIDATION;
using Final_Project.BLL;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Security.Cryptography;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Final_Project.GUI
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        //Turn Tabs Public
        public TabControl tabControlMain
        {
            get { return tabControl; }
        }

        //Method to get a tabPage by name
        public TabPage ShowTab(string tabPageName)
        {
            foreach (TabPage tabPage in tabControlMain.TabPages)
            {
                if (tabPage.Name == tabPageName)
                {
                    return tabPage;
                }
            }
            return null;
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
                MessageBox.Show("Invalid Email format. Use the format abd@email.com", "Invalid UserName", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("This UserName do not exists.\nPlease enter another one.", "Invalid UserName", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxUserName.Clear();
                textBoxUserName.Focus();
                return;
            }

            //Delete User
            var answer = MessageBox.Show("Do you really want to delete this user?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (answer == DialogResult.Yes)
            {
                userDel.DeleteUser(userName);
                MessageBox.Show("User data has been deleted successfully.", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);

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

            // search by 
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
                case 0:
                    textBoxSearch.Visible = false;
                    labelMessageU.Visible = false;

                    textBoxUserName.Clear();
                    textBoxPassword.Clear();
                    textBoxEID.Clear();
                    break;
                case 1:
                    textBoxSearch.Visible = true;
                    labelMessageU.Text = "Please enter the Username";
                    textBoxSearch.Clear();
                    textBoxSearch.Focus();

                    textBoxUserName.Clear();
                    textBoxPassword.Clear();
                    textBoxEID.Clear();
                    break;
                case 2:
                    textBoxSearch.Visible = true;
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
            //Hide Last Name employee textbox
            textBoxLNE.Visible = false;
            textBoxSearchE.Visible = false;

            //Hide Last Name User textbox
            textBoxSearch.Visible = false;

            //Hide Last Name customer textbox
            textBoxSearchCLN.Visible = false;

            // Populate the comboBoxEmpPositions with positions from the Positions table
            List<Positions> positions = Positions.GetPositionList();
            comboBoxEmpPositions.DataSource = positions;
            comboBoxEmpPositions.DisplayMember = "PositionName";
            comboBoxEmpPositions.ValueMember = "PositionID";
            comboBoxEmpPositions.SelectedIndex = -1;

            // Populate the listViewBookAuthors with authors from Authors Table
            List<Author> authors = Author.GetAuthorList();
            if (authors != null && authors.Count > 0)
            {
                foreach (Author author in authors)
                {
                    ListViewItem item = new ListViewItem(author.LastNameFirstName); 
                    item.Tag = author.AuthorID;
                    listViewBookAuthors.Items.Add(item);
                }
            }
            listViewBookAuthors.SelectedIndices.Clear();

            // Populate the comboBoxBookPublisher with Publisher Names
            List<Publisher> publishers = Publisher.GetPublisherList();
            comboBoxBookPublisher.DataSource = publishers;
            comboBoxBookPublisher.DisplayMember = "PublisherName";
            comboBoxBookPublisher.ValueMember = "PublisherID";
            comboBoxBookPublisher.SelectedIndex = -1;
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
            string Phone = textBoxPhoneEmp.Text.Trim();


            //Check Empty textbox
            if (string.IsNullOrEmpty(EID) || string.IsNullOrEmpty(FName) || string.IsNullOrEmpty(LName))
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

            //Valid Phone
            if (!Validator.IsValidPhone(Phone))
            {
                MessageBox.Show("Invalid Phone Number. Must be: 123-456-7890", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxPhoneEmp.Clear();
                textBoxPhoneEmp.Focus();
                return;

            }

            if (comboBoxEmpPositions.SelectedItem != null)
            {
                // Get the selected position from the combobox
                Positions selectedPosition = (Positions)comboBoxEmpPositions.SelectedItem;

                // Save Employee
                emp.EmployeeID = Convert.ToInt32(textBoxEmployeeId.Text.Trim());
                emp.FirstName = textBoxFirstName.Text.Trim();
                emp.LastName = textBoxLastName.Text.Trim();
                emp.JobTitle = selectedPosition.PositionID;
                emp.Phone = textBoxPhoneEmp.Text.Trim();
                emp.SaveEmployee(emp);
                MessageBox.Show("Employee data has been saved successfully.", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Please select a position.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            textBoxEmployeeId.Clear();
            textBoxFirstName.Clear();
            textBoxLastName.Clear();
            comboBoxEmpPositions.SelectedIndex = -1;
            textBoxPhoneEmp.Clear();
            textBoxEmployeeId.Focus();

        }

        private void buttonUpdateEmp_Click(object sender, EventArgs e)
        {
            string EID = textBoxEmployeeId.Text.Trim();
            string FName = textBoxFirstName.Text.Trim();
            string LName = textBoxLastName.Text.Trim();
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

            //Valid Phone
            if (!Validator.IsValidPhone(Phone))
            {
                MessageBox.Show("Invalid Phone Number. Must be: 123-456-7890", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxPhoneEmp.Clear();
                textBoxPhoneEmp.Focus();
                return;

            }

            if (comboBoxEmpPositions.SelectedItem != null)
            {
                Employee empUpdated = new Employee();
                Positions selectedPosition = (Positions)comboBoxEmpPositions.SelectedItem;

                empUpdated.EmployeeID = Convert.ToInt32(textBoxEmployeeId.Text.Trim());
                empUpdated.FirstName = textBoxFirstName.Text.Trim();
                empUpdated.LastName = textBoxLastName.Text.Trim();
                empUpdated.JobTitle = selectedPosition.PositionID;
                empUpdated.Phone = textBoxPhoneEmp.Text.Trim();
                empUpdated.UpdateEmployee(empUpdated);
                MessageBox.Show("Employee data has been updated successfully.", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Please select a position.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            textBoxEmployeeId.Clear();
            textBoxFirstName.Clear();
            textBoxLastName.Clear();
            comboBoxEmpPositions.SelectedIndex = -1;
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

            //Check if employeeID exist at table of users, so the employee does not have 2 acess.
            User user = new User();
            if (user.ExistEmployeeID_UT(EID))
            {
                MessageBox.Show("This EmployeeID have an User.\nPlease delete the user first.", "Erro EmployeeID", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxEmployeeId.Clear();
                textBoxEmployeeId.Focus();
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
            comboBoxEmpPositions.SelectedIndex = -1;
            textBoxPhoneEmp.Clear();
            textBoxEmployeeId.Focus();
        }

        private void buttonListAllEmployee_Click(object sender, EventArgs e)
        {
            listViewEmployee.Items.Clear();
            Employee employee = new Employee();
            
            List<Employee> listE = employee.GetEmployeeList();
            List<Positions> listP = Positions.GetPositionList();

            foreach (Employee emp in listE)
            {
                string jobTitleName = listP.FirstOrDefault(p => p.PositionID == emp.JobTitle)?.PositionName;

                ListViewItem item = new ListViewItem(emp.EmployeeID.ToString());
                item.SubItems.Add(emp.FirstName);
                item.SubItems.Add(emp.LastName);
                item.SubItems.Add(jobTitleName);
                item.SubItems.Add(emp.Phone);
                listViewEmployee.Items.Add(item);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int indexselect = comboBox1.SelectedIndex;
            switch (indexselect)
            {
                case 0:
                    listViewEmployee.Items.Clear();
                    labelSearchE.Visible = false;
                    textBoxSearchE.Visible=false;
                    labelLNE.Visible = false;
                    textBoxLNE.Visible = false;

                    textBoxEmployeeId.Clear();
                    textBoxFirstName.Clear();
                    textBoxLastName.Clear();
                    comboBoxEmpPositions.SelectedIndex = -1;
                    textBoxPhoneEmp.Clear();
                    break;

                case 1:
                    listViewEmployee.Items.Clear ();
                    labelSearchE.Visible = true;
                    textBoxSearchE.Visible = true;
                    labelSearchE.Text = "Enter the Employee ID";
                    labelLNE.Visible = false;
                    textBoxLNE.Visible = false;
                    textBoxSearchE.Clear();

                    textBoxEmployeeId.Clear();
                    textBoxFirstName.Clear();
                    textBoxLastName.Clear();
                    comboBoxEmpPositions.SelectedIndex = -1;
                    textBoxPhoneEmp.Clear();
                    break;

                case 2:
                    listViewEmployee.Items.Clear();
                    labelSearchE.Visible = true;
                    textBoxSearchE.Visible = true;
                    labelSearchE.Text = "Enter First OR Last Name";
                    labelLNE.Visible = false;
                    textBoxLNE.Visible = false;
                    textBoxSearchE.Clear();

                    textBoxEmployeeId.Clear();
                    textBoxFirstName.Clear();
                    textBoxLastName.Clear();
                    comboBoxEmpPositions.SelectedIndex = -1;
                    textBoxPhoneEmp.Clear();
                    break;

                case 3:
                    listViewEmployee.Items.Clear();
                    labelSearchE.Visible = true;
                    textBoxSearchE.Visible = true;
                    labelSearchE.Text = "Enter the First Name";
                    labelLNE.Visible = true;
                    labelLNE.Text = "Enter the Last Name";
                    textBoxLNE.Visible = true;
                    textBoxSearchE.Clear();
                    textBoxLNE.Clear();

                    textBoxEmployeeId.Clear();
                    textBoxFirstName.Clear();
                    textBoxLastName.Clear();
                    comboBoxEmpPositions.SelectedIndex = -1;
                    textBoxPhoneEmp.Clear();
                    break;
            }
        }

        private void buttonSearchEmp_Click(object sender, EventArgs e)
        {
            listViewEmployee.Items.Clear();

            Employee emp = new Employee();
            List<Positions> pos = Positions.GetPositionList();

            string input = "";
            switch (comboBox1.SelectedIndex)
            {
                case -1:
                    MessageBox.Show("Please select the Search option first", "Search option", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;

                case 0:
                    MessageBox.Show("Please select the Search option first", "Search option", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;

                //Search by Employee ID
                case 1: 
                    input = textBoxSearchE.Text.Trim();

                    if (!Validator.IsValidId(input, 4))
                    {
                        MessageBox.Show("Invalid Employee ID", "Error Employee ID", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textBoxSearch.Clear();
                        textBoxSearch.Focus();
                        return;
                    }


                    emp = emp.SearchEmployeeID(Convert.ToInt32(input));
                    if (emp != null)
                    {
                        textBoxEmployeeId.Text = emp.EmployeeID.ToString();
                        textBoxFirstName.Text = emp.FirstName.ToString();
                        textBoxLastName.Text = emp.LastName.ToString();
                        comboBoxEmpPositions.SelectedValue = emp.JobTitle;
                        textBoxPhoneEmp.Text = emp.Phone.ToString();
                    }
                    else
                    {
                        MessageBox.Show("Employee not found!", "Invalid Employee Id", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textBoxSearch.Clear();
                        textBoxSearch.Focus();
                    }
                    break;

                //Search by First Name OR Last Name
                case 2:
                    input = textBoxSearchE.Text.Trim();
                    List<Employee> listE = new List<Employee>();
                    

                    listE = emp.SearchEmployeeName(input);
                    listViewEmployee.Items.Clear();

                    if (listE != null && listE.Count > 0)
                    {
                        foreach (Employee empItem in listE)
                        {
                            string jobTitleName = pos.FirstOrDefault(p => p.PositionID == empItem.JobTitle)?.PositionName;

                            // MessageBox.Show(empItem.EmployeeId.ToString());
                            ListViewItem item = new ListViewItem(empItem.EmployeeID.ToString());
                            item.SubItems.Add(empItem.FirstName);
                            item.SubItems.Add(empItem.LastName);
                            item.SubItems.Add(jobTitleName);
                            item.SubItems.Add(empItem.Phone);
                            listViewEmployee.Items.Add(item);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Employee not found!");
                        textBoxSearch.Clear();
                        textBoxSearch.Focus();
                    }
                    break;

                //Search by First and Last Name
                case 3:
                    string input1 = textBoxSearchE.Text.Trim();
                    string input2 = textBoxLNE.Text.Trim();

                    listE = emp.SearchEmployeeFLName(input1, input2);
                    listViewEmployee.Items.Clear();

                    if (listE != null && listE.Count > 0)
                    {
                        foreach (Employee empItem in listE)
                        {
                            string jobTitleName = pos.FirstOrDefault(p => p.PositionID == empItem.JobTitle)?.PositionName;

                            // MessageBox.Show(empItem.EmployeeId.ToString());
                            ListViewItem item = new ListViewItem(empItem.EmployeeID.ToString());
                            item.SubItems.Add(empItem.FirstName);
                            item.SubItems.Add(empItem.LastName);
                            item.SubItems.Add(jobTitleName);
                            item.SubItems.Add(empItem.Phone);
                            listViewEmployee.Items.Add(item);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Employee not found!");
                        textBoxSearch.Clear();
                        textBoxSearch.Focus();
                    }
                    break;
                    break;
            }
        }

        private void labelAddJob_Click(object sender, EventArgs e)
        {
            AddJobTitle formAdd = new AddJobTitle();
            formAdd.Show();
            this.Hide();
        }

        private void comboBoxEmpPositions_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //===================================== CUSTOMER FORM ==========================================

        private void buttonExitCust_Click(object sender, EventArgs e)
        {
            var answer = MessageBox.Show("Do you really want to exit the application?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (answer == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void buttonCustomerSave_Click(object sender, EventArgs e)
        {
            string CFName = textBoxCustFN.Text.Trim();
            string CLName = textBoxCustLN.Text.Trim();
            string CPhone = textBoxCustPhone.Text.Trim();
            string CFax = textBoxCustFax.Text.Trim();
            string CStreet = textBoxCustStreet.Text.Trim();
            string CCity = textBoxCustCity.Text.Trim();
            string CZip = textBoxCustPC.Text.Trim();
            string CEmail = textBoxCustEmail.Text.Trim();
            decimal creditLimit;

            //Check Empty textbox
            if (string.IsNullOrEmpty(CFName) ||
            string.IsNullOrEmpty(CLName) ||
            string.IsNullOrEmpty(CPhone) ||
            string.IsNullOrEmpty(CStreet) ||
            string.IsNullOrEmpty(CCity) ||
            string.IsNullOrEmpty(CZip) ||
            string.IsNullOrEmpty(CEmail))
            {
                MessageBox.Show("Please fill in all required fields (*).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //Validate the email
            if (!Validator.IsValidEmailFormat(CEmail))
            {
                MessageBox.Show("Invalid Email format. Use the format abd@email.com", "Invalid Customer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxCustEmail.Clear();
                textBoxCustEmail.Focus();
                return;
            }

            //Validate First Name
            if (!Validator.IsValidName(CFName))
            {
                MessageBox.Show("Invalid First Name.", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxCustFN.Clear();
                textBoxCustFN.Focus();
                return;

            }

            //Validate Last Name
            if (!Validator.IsValidName(CLName))
            {
                MessageBox.Show("Invalid Last Name.", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxCustLN.Clear();
                textBoxCustLN.Focus();
                return;

            }

            //Valid Phone
            if (!Validator.IsValidPhone(CPhone))
            {
                MessageBox.Show("Invalid Phone Number. Must be: 123-456-7890", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxCustPhone.Clear();
                textBoxCustPhone.Focus();
                return;

            }

            //Validate address
            if (!Validator.IsValidZip(CZip))
            {
                MessageBox.Show("Invalid Zip Code. Must be: X0X 0X0 and from any city at Quebec Province", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxCustPC.Clear();
                textBoxCustPC.Focus();
                return;
            }

            //Validate Credit Limit
            if (!decimal.TryParse(textBoxCustCredit.Text.Trim(), out creditLimit))
            {
                MessageBox.Show("Invalid credit limit. Please enter a valid decimal value.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Customer cust = new Customer();
            if (!cust.IsUniqueCustomer(CPhone))
            {
                MessageBox.Show("This Phone Number already exists.\nPlease enter another one.", "Invalid Phone Number", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxCustPhone.Clear();
                textBoxCustPhone.Focus();
                return;
            }

            //Save Customer

            cust.FirstName = CFName;
            cust.LastName = CLName;
            cust.PhoneNumber = CPhone;
            cust.FaxNumber = CFax;
            cust.Street = CStreet;
            cust.City = CCity;
            cust.PostalCode = CZip;
            cust.CreditLimit = creditLimit;
            cust.Email = CEmail;
            cust.SaveCustomer(cust);

            MessageBox.Show("Customer Data has been saved successfully.", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);

            textBoxCustFN.Clear();
            textBoxCustLN.Clear();
            textBoxCustPhone.Clear();
            textBoxCustFax.Clear();
            textBoxCustCredit.Clear();
            textBoxCustStreet.Clear();
            textBoxCustCity.Clear();
            textBoxCustPC.Clear();
            textBoxCustEmail.Clear();
            textBoxCustFN.Focus();
        }

        // Update the customer
        private void buttonCustomerUpdate_Click(object sender, EventArgs e)
        {
            string CFName = textBoxCustFN.Text.Trim();
            string CLName = textBoxCustLN.Text.Trim();
            string CPhone = textBoxCustPhone.Text.Trim();
            string CFax = textBoxCustFax.Text.Trim();
            string CStreet = textBoxCustStreet.Text.Trim();
            string CCity = textBoxCustCity.Text.Trim();
            string CZip = textBoxCustPC.Text.Trim();
            string CEmail = textBoxCustEmail.Text.Trim();
            decimal creditLimit;

            //Check Empty textbox
            if (string.IsNullOrEmpty(CFName) ||
            string.IsNullOrEmpty(CLName) ||
            string.IsNullOrEmpty(CPhone) ||
            string.IsNullOrEmpty(CStreet) ||
            string.IsNullOrEmpty(CCity) ||
            string.IsNullOrEmpty(CZip) ||
            string.IsNullOrEmpty(CEmail))
            {
                MessageBox.Show("Please fill in all required fields (*).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //Validate the email
            if (!Validator.IsValidEmailFormat(CEmail))
            {
                MessageBox.Show("Invalid Email format. Use the format abd@email.com", "Invalid Customer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxCustEmail.Clear();
                textBoxCustEmail.Focus();
                return;
            }

            //Validate First Name
            if (!Validator.IsValidName(CFName))
            {
                MessageBox.Show("Invalid First Name.", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxCustFN.Clear();
                textBoxCustFN.Focus();
                return;

            }

            //Validate Last Name
            if (!Validator.IsValidName(CLName))
            {
                MessageBox.Show("Invalid Last Name.", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxCustLN.Clear();
                textBoxCustLN.Focus();
                return;

            }

            //Valid Phone
            if (!Validator.IsValidPhone(CPhone))
            {
                MessageBox.Show("Invalid Phone Number. Must be: 123-456-7890", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxCustPhone.Clear();
                textBoxCustPhone.Focus();
                return;

            }

            //Validate address
            if (!Validator.IsValidZip(CZip))
            {
                MessageBox.Show("Invalid Zip Code. Must be: X0X 0X0 and from any city at Quebec Province", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxCustPC.Clear();
                textBoxCustPC.Focus();
                return;
            }

            //Validate Credit Limit
            if (!decimal.TryParse(textBoxCustCredit.Text.Trim(), out creditLimit))
            {
                // Parsing failed, handle the error as needed
                // For example, you might display an error message to the user
                MessageBox.Show("Invalid credit limit. Please enter a valid decimal value.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Return from the method or handle the error appropriately
            }


            Customer custUp = new Customer();
            if (custUp.IsUniqueCustomer(CPhone))
            {
                MessageBox.Show("This Customer do not exists.\nPlease enter another one.", "Invalid UserName", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxCustPhone.Clear();
                textBoxCustPhone.Focus();
                return;
            }

            //Update Customer

            custUp.FirstName = CFName;
            custUp.LastName = CLName;
            custUp.PhoneNumber = CPhone;
            custUp.FaxNumber = CFax;
            custUp.Street = CStreet;
            custUp.City = CCity;
            custUp.PostalCode = CZip;
            custUp.CreditLimit = creditLimit;
            custUp.Email = CEmail;
            custUp.UpdateCustomer(custUp);

            MessageBox.Show("Customer Data has been updated successfully.", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);

            textBoxCustFN.Clear();
            textBoxCustLN.Clear();
            textBoxCustPhone.Clear();
            textBoxCustFax.Clear();
            textBoxCustCredit.Clear();
            textBoxCustStreet.Clear();
            textBoxCustCity.Clear();
            textBoxCustPC.Clear();
            textBoxCustEmail.Clear();
            textBoxCustFN.Focus();
            labelCDeleted.Text = ""; ;
        }

        private void buttonListCustomer_Click(object sender, EventArgs e)
        {
            //clean the tab
            textBoxCustFN.Clear();
            textBoxCustLN.Clear();
            textBoxCustPhone.Clear();
            textBoxCustFax.Clear();
            textBoxCustCredit.Clear();
            textBoxCustStreet.Clear();
            textBoxCustCity.Clear();
            textBoxCustPC.Clear();
            textBoxCustEmail.Clear();
            textBoxSearchCFN.Clear();
            textBoxSearchCLN.Clear();


            StringBuilder message = new StringBuilder();

            Customer cust = new Customer();
            List<Customer> listC = cust.GetCustomerList();

            foreach (Customer customer in listC)
            {
                message.AppendLine($"Name: {customer.FirstName} {customer.LastName}");
                message.AppendLine($"Phone: {customer.PhoneNumber} | Fax: {customer.FaxNumber}");
                message.AppendLine($"Email: {customer.Email}");
                message.AppendLine($"Street: {customer.Street} | City: {customer.City} | Zip: {customer.PostalCode}");
                message.AppendLine($"Credit Limit: {customer.CreditLimit}");
                message.AppendLine($"Cliente Since: {customer.DateTimeSince}");
                message.AppendLine($"-----------------------------");
            }

            MessageBox.Show(message.ToString(), "Customer List", MessageBoxButtons.OK);
        }

        private void buttonCustomerDelete_Click(object sender, EventArgs e)
        {
            string Phone = textBoxCustPhone.Text.Trim();
            if (string.IsNullOrEmpty(Phone))
            {
                MessageBox.Show("Please fill in the Phone Number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Customer custDel = new Customer();
            if (custDel.IsUniqueCustomer(Phone))
            {
                MessageBox.Show("This Customer do not exists.\nPlease enter another one.", "Invalid UserName", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxCustPhone.Clear();
                textBoxCustPhone.Focus();
                return;
            }

            Customer custDeleted = new Customer();
            var answer = MessageBox.Show("Do you really want to delete this customer?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (answer == DialogResult.Yes)
            {
                custDeleted.DeleteCustomer(Phone);
                MessageBox.Show("Customer data has been deleted successfully.", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            textBoxCustFN.Clear();
            textBoxCustLN.Clear();
            textBoxCustPhone.Clear();
            textBoxCustFax.Clear();
            textBoxCustCredit.Clear();
            textBoxCustStreet.Clear();
            textBoxCustCity.Clear();
            textBoxCustPC.Clear();
            textBoxCustEmail.Clear();
            textBoxCustFN.Focus();

        }

        private void comboBoxSearchCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            int indexselect = comboBoxSearchCustomer.SelectedIndex;
            switch (indexselect)
            {
                case 0:
                    textBoxSearchCFN.Visible = false;
                    textBoxSearchCLN.Visible = false;
                    labelSearchC.Visible = false;
                    labelSearchCLN.Visible = false;

                    textBoxCustFN.Clear();
                    textBoxCustLN.Clear();
                    textBoxCustPhone.Clear();
                    textBoxCustFax.Clear();
                    textBoxCustCredit.Clear();
                    textBoxCustStreet.Clear();
                    textBoxCustCity.Clear();
                    textBoxCustPC.Clear();
                    textBoxCustEmail.Clear();
                    textBoxSearchCFN.Focus();
                    break;

                case 1:
                    textBoxSearchCFN.Visible = true;
                    textBoxSearchCLN.Visible = false;
                    labelSearchC.Visible = true ;
                    labelSearchCLN.Visible = false;
                    labelSearchC.Text = "Enter the Phone Number";
                    textBoxSearchCFN.Clear();

                    textBoxCustFN.Clear();
                    textBoxCustLN.Clear();
                    textBoxCustPhone.Clear();
                    textBoxCustFax.Clear();
                    textBoxCustCredit.Clear();
                    textBoxCustStreet.Clear();
                    textBoxCustCity.Clear();
                    textBoxCustPC.Clear();
                    textBoxCustEmail.Clear();
                    textBoxSearchCFN.Focus();
                    break;

                case 2:
                    textBoxSearchCFN.Visible = true;
                    textBoxSearchCLN.Visible = false;
                    labelSearchC.Visible = true;
                    labelSearchCLN.Visible = false;
                    labelSearchC.Text = "Enter First OR Last Name";
                    textBoxSearchCFN.Clear();

                    textBoxCustFN.Clear();
                    textBoxCustLN.Clear();
                    textBoxCustPhone.Clear();
                    textBoxCustFax.Clear();
                    textBoxCustCredit.Clear();
                    textBoxCustStreet.Clear();
                    textBoxCustCity.Clear();
                    textBoxCustPC.Clear();
                    textBoxCustEmail.Clear();
                    textBoxSearchCFN.Focus();
                    break;

                case 3:
                    textBoxSearchCFN.Visible = true;
                    textBoxSearchCLN.Visible = true;
                    labelSearchC.Visible = true;
                    labelSearchCLN.Visible = true;
                    labelSearchC.Text = "Enter the First Name";
                    labelSearchCLN.Text = "Enter the Last Name";
                    textBoxSearchCFN.Clear();

                    textBoxCustFN.Clear();
                    textBoxCustLN.Clear();
                    textBoxCustPhone.Clear();
                    textBoxCustFax.Clear();
                    textBoxCustCredit.Clear();
                    textBoxCustStreet.Clear();
                    textBoxCustCity.Clear();
                    textBoxCustPC.Clear();
                    textBoxCustEmail.Clear();
                    textBoxSearchCFN.Focus();
                    break;

                case 4:
                    textBoxSearchCFN.Visible = true;
                    textBoxSearchCLN.Visible = false;
                    labelSearchC.Visible = true;
                    labelSearchCLN.Visible = false;
                    labelSearchC.Text = "Enter the City";
                    textBoxSearchCFN.Clear();

                    textBoxCustFN.Clear();
                    textBoxCustLN.Clear();
                    textBoxCustPhone.Clear();
                    textBoxCustFax.Clear();
                    textBoxCustCredit.Clear();
                    textBoxCustStreet.Clear();
                    textBoxCustCity.Clear();
                    textBoxCustPC.Clear();
                    textBoxCustEmail.Clear();
                    textBoxSearchCFN.Focus();
                    break;

                case 5:
                    textBoxSearchCFN.Visible = true;
                    textBoxSearchCLN.Visible = false;
                    labelSearchC.Visible = true;
                    labelSearchCLN.Visible = false;
                    labelSearchC.Text = "Enter the Postal Code";
                    textBoxSearchCFN.Clear();

                    textBoxCustFN.Clear();
                    textBoxCustLN.Clear();
                    textBoxCustPhone.Clear();
                    textBoxCustFax.Clear();
                    textBoxCustCredit.Clear();
                    textBoxCustStreet.Clear();
                    textBoxCustCity.Clear();
                    textBoxCustPC.Clear();
                    textBoxCustEmail.Clear();
                    textBoxSearchCFN.Focus();
                    break;
            }
        }

        private void buttonSearchCust_Click(object sender, EventArgs e)
        {
            if (comboBoxSearchCustomer.SelectedIndex == -1 || comboBoxSearchCustomer.SelectedIndex == 0)
            {
                MessageBox.Show("Please select one option first", "Search option", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // search by 
            Customer cust = new Customer();
            string input = "";
            switch (comboBoxSearchCustomer.SelectedIndex)
            {
                case 1: //Search by Phone Number
                    input = textBoxSearchCFN.Text.Trim();
                    if (!Validator.IsValidPhone(input))
                    {
                        listViewUser.Items.Clear();
                        MessageBox.Show("Invalid Phone Number. Must be: 123-456-7890", "Invalid Employee Phone", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textBoxCustPhone.Clear();
                        textBoxCustPhone.Focus();
                        return;
                    }
                    cust = cust.SearchCustomerPhone(input);
                    if (cust != null)
                    {
                        textBoxCustFN.Text = cust.FirstName.ToString();
                        textBoxCustLN.Text = cust.LastName.ToString();
                        textBoxCustPhone.Text = cust.PhoneNumber.ToString();
                        textBoxCustFax.Text = cust.FaxNumber.ToString();
                        textBoxCustCredit.Text = cust.CreditLimit.ToString();
                        textBoxCustStreet.Text = cust.Street.ToString();
                        textBoxCustCity.Text = cust.City.ToString();
                        textBoxCustPC.Text = cust.PostalCode.ToString();
                        textBoxCustEmail.Text = cust.Email.ToString();
                        textBoxSearchCFN.Focus();

                        //Check Customer status
                        if (!cust.ActiveCustomer(input)){
                            labelCDeleted.Text = "This Customer has been deleted. Click at UPDATE button to set as ACTIVE again";
                        }
                        else
                        {
                            labelCDeleted.Text = "";
                        }
                    }
                    else
                    {
                        MessageBox.Show("Customer not found!", "Invalid Customer Phone", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textBoxSearch.Clear();
                        textBoxSearch.Focus();

                    }
                    break;

                case 2:
                    input = textBoxSearchCFN.Text.Trim();
                    StringBuilder message = new StringBuilder();

                    if (cust != null)
                    {
                        List<Customer> listC = cust.SearchCustomerby(input);
                        foreach (Customer customer in listC)
                        {
                            message.AppendLine($"Name: {customer.FirstName} {customer.LastName}");
                            message.AppendLine($"Phone: {customer.PhoneNumber} | Fax: {customer.FaxNumber}");
                            message.AppendLine($"Email: {customer.Email}");
                            message.AppendLine($"Street: {customer.Street} | City: {customer.City} | Zip: {customer.PostalCode}");
                            message.AppendLine($"Credit Limit: {customer.CreditLimit}");
                            message.AppendLine($"Cliente Since: {customer.DateTimeSince}");
                            message.AppendLine($"-----------------------------");
                        }
                        MessageBox.Show(message.ToString(), "Customer List", MessageBoxButtons.OK);

                        //Clean the tab
                        textBoxCustFN.Clear();
                        textBoxCustLN.Clear();
                        textBoxCustPhone.Clear();
                        textBoxCustFax.Clear();
                        textBoxCustCredit.Clear();
                        textBoxCustStreet.Clear();
                        textBoxCustCity.Clear();
                        textBoxCustPC.Clear();
                        textBoxCustEmail.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Customer not found!", "Invalid Customer Name", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textBoxSearch.Clear();
                        textBoxSearch.Focus();

                    }
                    break;

                case 3:
                    string inputFN = textBoxSearchCFN.Text.Trim();
                    string inputLN = textBoxSearchCLN.Text.Trim();

                    StringBuilder messageFL = new StringBuilder();

                    if (cust != null)
                    {
                        List<Customer> listC = cust.SearchCustomerbyFullName(inputFN, inputLN);
                        foreach (Customer customer in listC)
                        {
                            messageFL.AppendLine($"Name: {customer.FirstName} {customer.LastName}");
                            messageFL.AppendLine($"Phone: {customer.PhoneNumber} | Fax: {customer.FaxNumber}");
                            messageFL.AppendLine($"Email: {customer.Email}");
                            messageFL.AppendLine($"Street: {customer.Street} | City: {customer.City} | Zip: {customer.PostalCode}");
                            messageFL.AppendLine($"Credit Limit: {customer.CreditLimit}");
                            messageFL.AppendLine($"Cliente Since: {customer.DateTimeSince}");
                            messageFL.AppendLine($"-----------------------------");
                        }
                        MessageBox.Show(messageFL.ToString(), "Customer List", MessageBoxButtons.OK);

                        //Clean the tab
                        textBoxCustFN.Clear();
                        textBoxCustLN.Clear();
                        textBoxCustPhone.Clear();
                        textBoxCustFax.Clear();
                        textBoxCustCredit.Clear();
                        textBoxCustStreet.Clear();
                        textBoxCustCity.Clear();
                        textBoxCustPC.Clear();
                        textBoxCustEmail.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Customer not found!", "Invalid Customer Name", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textBoxSearch.Clear();
                        textBoxSearch.Focus();

                    }
                    break;

                case 4:
                    string inputC = textBoxSearchCFN.Text.Trim();

                    StringBuilder messageC = new StringBuilder();

                    if (cust != null)
                    {
                        List<Customer> listC = cust.SearchCustomerby(inputC);
                        foreach (Customer customer in listC)
                        {
                            messageC.AppendLine($"Name: {customer.FirstName} {customer.LastName}");
                            messageC.AppendLine($"Phone: {customer.PhoneNumber} | Fax: {customer.FaxNumber}");
                            messageC.AppendLine($"Email: {customer.Email}");
                            messageC.AppendLine($"Street: {customer.Street} | City: {customer.City} | Zip: {customer.PostalCode}");
                            messageC.AppendLine($"Credit Limit: {customer.CreditLimit}");
                            messageC.AppendLine($"Cliente Since: {customer.DateTimeSince}");
                            messageC.AppendLine($"-----------------------------");
                        }
                        MessageBox.Show(messageC.ToString(), "Customer List", MessageBoxButtons.OK);

                        //Clean the tab
                        textBoxCustFN.Clear();
                        textBoxCustLN.Clear();
                        textBoxCustPhone.Clear();
                        textBoxCustFax.Clear();
                        textBoxCustCredit.Clear();
                        textBoxCustStreet.Clear();
                        textBoxCustCity.Clear();
                        textBoxCustPC.Clear();
                        textBoxCustEmail.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Customer not found!", "Invalid Customer Name", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textBoxSearch.Clear();
                        textBoxSearch.Focus();

                    }
                    break;

                case 5:
                    string inputZ = textBoxSearchCFN.Text.Trim();

                    StringBuilder messageZ = new StringBuilder();

                    if (cust != null)
                    {
                        List<Customer> listC = cust.SearchCustomerby(inputZ);
                        foreach (Customer customer in listC)
                        {
                            messageZ.AppendLine($"Name: {customer.FirstName} {customer.LastName}");
                            messageZ.AppendLine($"Phone: {customer.PhoneNumber} | Fax: {customer.FaxNumber}");
                            messageZ.AppendLine($"Email: {customer.Email}");
                            messageZ.AppendLine($"Street: {customer.Street} | City: {customer.City} | Zip: {customer.PostalCode}");
                            messageZ.AppendLine($"Credit Limit: {customer.CreditLimit}");
                            messageZ.AppendLine($"Cliente Since: {customer.DateTimeSince}");
                            messageZ.AppendLine($"-----------------------------");
                        }
                        MessageBox.Show(messageZ.ToString(), "Customer List", MessageBoxButtons.OK);

                        //Clean the tab
                        textBoxCustFN.Clear();
                        textBoxCustLN.Clear();
                        textBoxCustPhone.Clear();
                        textBoxCustFax.Clear();
                        textBoxCustCredit.Clear();
                        textBoxCustStreet.Clear();
                        textBoxCustCity.Clear();
                        textBoxCustPC.Clear();
                        textBoxCustEmail.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Customer not found!", "Invalid Customer Name", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textBoxSearch.Clear();
                        textBoxSearch.Focus();

                    }
                    break;

                default:
                    break;
            }
        }

        //===================================== BOOK FORM ==========================================
        private void buttonBookExit_Click(object sender, EventArgs e)
        {
            var answer = MessageBox.Show("Do you really want to exit the application?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (answer == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void label36_Click(object sender, EventArgs e)
        {
            AddPublisher formAdd = new AddPublisher();
            formAdd.Show();
            this.Hide();
        }

        private void label35_Click(object sender, EventArgs e)
        {
            AddAuthor formAdd = new AddAuthor();
            formAdd.Show();
            this.Hide();
        }

        private void buttonBookSave_Click(object sender, EventArgs e)
        {
            string ISBN = textBoxBookISBN.Text.Trim();
            string Book = textBoxBookName.Text.Trim();
            string Price = textBoxBookPrice.Text.Trim();
            string Year = textBoxBookYear.Text.Trim();
            string Qty = textBoxBookQty.Text.Trim();

            //Check Empty textbox
            if (string.IsNullOrEmpty(ISBN) || string.IsNullOrEmpty(Book) || string.IsNullOrEmpty(Price) || string.IsNullOrEmpty(Year) || string.IsNullOrEmpty(Qty))
            {
                MessageBox.Show("Please fill in all required fields (*).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //Check ISBN format
            if (!Validator.IsValidISBN(ISBN))
            {
                MessageBox.Show("Invalid ISBN, please enter a 10 or 13-digit number without spaces or hyphens.", "Invalid USBN", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxBookISBN.Clear();
                textBoxBookISBN.Focus();
                return;
            }

            // Check if Book ID is unique
            Book books = new Book();
            if (!books.UniqueISBN(ISBN))
            {
                MessageBox.Show("The book ISBN must be unique.\n" + "Please enter another ISBN.", "Duplicate Book ISBN", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxBookISBN.Clear();
                textBoxBookISBN.Focus();
                return;

            }

            //Year Validation
            if (!Validator.IsValidYear(Year))
            {
                MessageBox.Show("The year provided is not valid. Please make sure it contains 4 digits and is the current year or earlier.", "Invalid Year", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxBookYear.Clear();
                textBoxBookYear.Focus();
                return;
            }

            if (comboBoxBookPublisher.SelectedItem != null && listViewBookAuthors.SelectedItems.Count > 0)
            {
                // Get the selected publisher from combobox
                Publisher selectedPublisher = (Publisher)comboBoxBookPublisher.SelectedItem;

                // Get the selected authors from ListView

                List<int> selectedAuthorIDs = new List<int>();
                foreach (ListViewItem item in listViewBookAuthors.Items)
                {
                    if (item.Selected)
                    {
                        int authorID = (int)item.Tag;
                        selectedAuthorIDs.Add(authorID);
                    }
                }

                books.ISBN = ISBN;
                books.Title = Book;
                books.UnitPrice = Convert.ToDecimal(Price);
                books.YearPublished = Convert.ToInt32(Year);
                books.QOH = Convert.ToInt32(Qty);
                books.Publisher = selectedPublisher.PublisherID;

                books.SaveBooks(books, selectedAuthorIDs);


                MessageBox.Show("This Book data has been saved successfully.", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);

                textBoxBookISBN.Clear();
                textBoxBookName.Clear();
                textBoxBookPrice.Clear();
                textBoxBookYear.Clear();
                textBoxBookQty.Clear();
                comboBoxBookPublisher.SelectedIndex = -1;
                listViewBookAuthors.SelectedIndices.Clear();
            }
            else
            {
                MessageBox.Show("Please select a publisher and/or at least one author.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        //Update Book
        private void buttonBookUpdate_Click(object sender, EventArgs e)
        {
            string ISBN = textBoxBookISBN.Text.Trim();
            string Book = textBoxBookName.Text.Trim();
            string Price = textBoxBookPrice.Text.Trim();
            string Year = textBoxBookYear.Text.Trim();
            string Qty = textBoxBookQty.Text.Trim();

            //Check Empty textbox
            if (string.IsNullOrEmpty(ISBN) || string.IsNullOrEmpty(Book) || string.IsNullOrEmpty(Price) || string.IsNullOrEmpty(Year) || string.IsNullOrEmpty(Qty))
            {
                MessageBox.Show("Please fill in all required fields (*).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // Check if Book ID Exist
            Book booksUpdate = new Book();
            if (booksUpdate.UniqueISBN(ISBN))
            {
                MessageBox.Show("The book ISBN does not exist.\n" + "Please enter another ISBN.", "Invalid Book ISBN", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxBookISBN.Clear();
                textBoxBookISBN.Focus();
                return;
            }
            //Year Validation
            if (!Validator.IsValidYear(Year))
            {
                MessageBox.Show("The year provided is not valid. Please make sure it contains 4 digits and is the current year or earlier.", "Invalid Year", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxBookYear.Clear();
                textBoxBookYear.Focus();
                return;
            }
            
            //Update
            if (comboBoxBookPublisher.SelectedItem != null && listViewBookAuthors.SelectedItems.Count > 0)
            {
                // Get the selected publisher from combobox
                Publisher selectedPublisher = (Publisher)comboBoxBookPublisher.SelectedItem;

                // Get the selected authors from ListView
                List<int> selectedAuthorIDs = new List<int>();
                foreach (ListViewItem item in listViewBookAuthors.Items)
                {
                    if (item.Selected)
                    {
                        int authorID = (int)item.Tag;
                        selectedAuthorIDs.Add(authorID);
                    }
                }

                booksUpdate.ISBN = ISBN;
                booksUpdate.Title = Book;
                booksUpdate.UnitPrice = Convert.ToDecimal(Price);
                booksUpdate.YearPublished = Convert.ToInt32(Year);
                booksUpdate.QOH = Convert.ToInt32(Qty);
                booksUpdate.Publisher = selectedPublisher.PublisherID;

                booksUpdate.UpdateBooks(booksUpdate, selectedAuthorIDs);


                MessageBox.Show("This Book data has been updated successfully.", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);

                textBoxBookISBN.Clear();
                textBoxBookName.Clear();
                textBoxBookPrice.Clear();
                textBoxBookYear.Clear();
                textBoxBookQty.Clear();
                comboBoxBookPublisher.SelectedIndex = -1;
                listViewBookAuthors.SelectedIndices.Clear();
            }
            else
            {
                MessageBox.Show("Please select a publisher and/or at least one author.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonBookDelete_Click(object sender, EventArgs e)
        {
            string ISBN = textBoxBookISBN.Text.Trim();
            if (string.IsNullOrEmpty(ISBN))
            {
                MessageBox.Show("Please fill in the Book ISBN.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //Check ISBN format
            if (!Validator.IsValidISBN(ISBN))
            {
                MessageBox.Show("Invalid ISBN, please enter a 10 or 13-digit number without spaces or hyphens.", "Invalid USBN", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxBookISBN.Clear();
                textBoxBookISBN.Focus();
                return;
            }
            // Check if Book ID Exist
            Book bookDelete = new Book();
            if (bookDelete.UniqueISBN(ISBN))
            {
                MessageBox.Show("The book ISBN does not exist.\n" + "Please enter another ISBN.", "Invalid Book ISBN", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxBookISBN.Clear();
                textBoxBookISBN.Focus();
                return;
            }

            var answer = MessageBox.Show("Do you really want to delete this Book?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (answer == DialogResult.Yes)
            {
                bookDelete.DeleteBooks(ISBN);
                MessageBox.Show("The book data has been deleted successfully.", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            textBoxBookISBN.Clear();
            textBoxBookName.Clear();
            textBoxBookPrice.Clear();
            textBoxBookYear.Clear();
            textBoxBookQty.Clear();
            comboBoxBookPublisher.SelectedIndex = -1;
            listViewBookAuthors.SelectedIndices.Clear();
        }
    }
}
