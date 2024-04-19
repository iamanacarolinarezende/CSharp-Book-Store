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
using System.Xml.Linq;

namespace Final_Project.GUI
{
    public partial class AddJobTitle : Form
    {
        public AddJobTitle()
        {
            InitializeComponent();
        }

        private void buttonExitE_Click(object sender, EventArgs e)
        {
            Main mainForm = new Main();
            var answer = MessageBox.Show("Do you really want to exit the application?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (answer == DialogResult.Yes)
            {
                mainForm.Show();
                this.Hide();
            }
        }

        private void buttonAddNewRole_Click(object sender, EventArgs e)
        {
            string position = textBoxAddNewRole.Text.Trim();

            if (!Validator.IsValidJobRole(position))
            {
                MessageBox.Show("Invalid Job Name. Please enter a valid job role containing only letters, spaces, hyphens, and apostrophes.", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxAddNewRole.Focus();
                return;

            }

            if (textBoxAddNewRole != null)
            {
                // Save Job role
                Positions job = new Positions();
                job.PositionName = textBoxAddNewRole.Text.Trim();
                job.AddPosition(job);
                MessageBox.Show("The new Position has been saved successfully.", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error to save this new position.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            textBoxAddNewRole.Clear();
        }
    }
}
