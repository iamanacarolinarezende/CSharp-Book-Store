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


        private void buttonLogin_Click(object sender, EventArgs e)
        {
            User user = new User();
            string username = textBoxLoginUser.Text.Trim();
            string password = textBoxLoginPass.Text;

            if (user.UserLogin(username, password))
            {
                Main mainForm = new Main();
                mainForm.Show();
                this.Hide(); // Optionally hide the login form
                return;
            }
            else
            {
                // Login failed, show an error message
                MessageBox.Show("Invalid username or password", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
