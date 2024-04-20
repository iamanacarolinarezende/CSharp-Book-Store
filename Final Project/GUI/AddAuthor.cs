using Final_Project.BLL;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Xml.Linq;

namespace Final_Project.GUI
{
    public partial class AddAuthor : Form
    {
        public AddAuthor()
        {
            InitializeComponent();
        }

        private void buttonExitE_Click(object sender, EventArgs e)
        {
            Main mainForm = new Main();
            var answer = MessageBox.Show("Do you really want to exit?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (answer == DialogResult.Yes)
            {
                mainForm.tabControlMain.SelectedTab = mainForm.ShowTab("Book");
                mainForm.Show();
                this.Hide();
            }
        }

        private void buttonAddAuthor_Click(object sender, EventArgs e)
        {
            string AFN = textBoxAuthorFirstName.Text.Trim();
            string ALN = textBoxAuthorLastName.Text.Trim();
            string AEmail = textBoxAuthorEmail.Text.Trim();

            if (string.IsNullOrEmpty(AFN) ||
            string.IsNullOrEmpty(ALN) ||
            string.IsNullOrEmpty(AEmail))
            {
                MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //Validate the email
            if (!Validator.IsValidEmailFormat(AEmail))
            {
                MessageBox.Show("Invalid Email format. Use the format abd@email.com", "Invalid Customer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxAuthorEmail.Clear();
                textBoxAuthorEmail.Focus();
                return;
            }

            //Validate First Name
            if (!Validator.IsValidName(AFN))
            {
                MessageBox.Show("Invalid First Name.", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxAuthorFirstName.Clear();
                textBoxAuthorFirstName.Focus();
                return;

            }

            //Validate Last Name
            if (!Validator.IsValidName(ALN))
            {
                MessageBox.Show("Invalid Last Name.", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxAuthorLastName.Clear();
                textBoxAuthorLastName.Focus();
                return;

            }

                // Save Job role
                Author aut = new Author();
                aut.FirstName = textBoxAuthorFirstName.Text.Trim();
                aut.LastName = textBoxAuthorLastName.Text.Trim();
                aut.Email = textBoxAuthorEmail.Text.Trim();
                aut.AddAuthor(aut);
                MessageBox.Show("The new Author has been saved successfully.", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);

            textBoxAuthorFirstName.Clear();
            textBoxAuthorLastName.Clear() ;
            textBoxAuthorEmail.Clear() ;

        }
    }
}
