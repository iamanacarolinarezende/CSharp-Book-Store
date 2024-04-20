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

namespace Final_Project.GUI
{
    public partial class AddPublisher : Form
    {
        public AddPublisher()
        {
            InitializeComponent();
        }

        private void buttonExitPublisher_Click(object sender, EventArgs e)
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

        private void buttonAddNewPublisher_Click(object sender, EventArgs e)
        {
            string publisher = textBoxAddNewPublisher.Text.Trim();

            if (textBoxAddNewPublisher != null)
            {
                // Save Publisher
                Publisher pub = new Publisher();
                pub.PublisherName = textBoxAddNewPublisher.Text.Trim();
                pub.AddPublisher(pub);
                MessageBox.Show("The new Publisher has been saved successfully.", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error to save this new publisher.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            textBoxAddNewPublisher.Clear();
        }
    }
}
