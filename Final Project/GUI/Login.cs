using Final_Project.BLL;
using Final_Project.DAL;
using Final_Project.VALIDATION;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Final_Project.GUI
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void buttonLoginExit_Click(object sender, EventArgs e)
        {
            var answer = MessageBox.Show("Do you really want to exit the application?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (answer == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
        private void checkBoxShowPass_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxShowPass.Checked == true)
            {
                textBoxLoginPass.UseSystemPasswordChar = false;
            }
            else
            {
                textBoxLoginPass.UseSystemPasswordChar= true;
            }
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            string username = textBoxLoginUser.Text.Trim();
            string password = textBoxLoginPass.Text;

            User user = new User();

            if (user.UserLogin(username, password))
            {
                int jobTitleNumber = Convert.ToInt32(User.GetEmployeeJob(username, password));
                //MessageBox.Show($"Your Job Title Number is: {jobTitleNumber}", "Job Title Number", MessageBoxButtons.OK);

                Main mainForm = new Main();

                if (jobTitleNumber == 2)
                {
                    mainForm.tabControlMain.TabPages.RemoveAt(1);
                    mainForm.tabControlMain.TabPages.RemoveAt(0);
                }
                else if (jobTitleNumber == 4)
                {
                    
                }

                mainForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
    }


}
