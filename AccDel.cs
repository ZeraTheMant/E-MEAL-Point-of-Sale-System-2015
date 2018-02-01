using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class AccDel : Form
    {
        public AccDel()
        {
            InitializeComponent();
        }

        private void uSERSBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
                                        DialogResult dr = MessageBox.Show("Do you want to save the changes? This process is not reversible.", "Confirm Modifications", MessageBoxButtons.YesNo,
MessageBoxIcon.Question);
                                        if (dr == DialogResult.Yes)
                                        {
                                            this.Validate();
                                            this.uSERSBindingSource.EndEdit();
                                            this.tableAdapterManager.UpdateAll(this.dBDataSet);
                                            MessageBox.Show("Account deletion(s) successful.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        }
                                        else
                                            MessageBox.Show("Continue your work.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void AccDel_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dBDataSet.USERS' table. You can move, or remove it, as needed.
            this.uSERSTableAdapter.Fill(this.dBDataSet.USERS);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to cancel the changes? All unsaved data will be lost.", "Cancel and Return", MessageBoxButtons.YesNo,
MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                this.Hide();
            }
            else
                MessageBox.Show("Continue your work.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
