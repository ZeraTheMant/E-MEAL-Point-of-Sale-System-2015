using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsApplication1
{
    public partial class Sup : Form
    {
        int id;
        SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\fape\Desktop\EMEAL\EMEAL\bin\Debug\DB.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");
        public Sup()
        {
            InitializeComponent();
        }

        private void Sup_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dBDataSet.Supplies' table. You can move, or remove it, as needed.
            this.suppliesTableAdapter.Fill(this.dBDataSet.Supplies);
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
        }


        private void button1_Click(object sender, EventArgs e)
        {
                this.Hide();
        }

        private void suppliesBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            try
            {
                if ((nameTextBox.Text.Trim().Length != 0) && (quantity_WeightTextBox.Text.Trim().Length != 0))
                {
                    this.Validate();
                    this.suppliesBindingSource.EndEdit();
                    this.tableAdapterManager.UpdateAll(this.dBDataSet);
                    MessageBox.Show("Modifications saved.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Please fill in all the fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void quantity_WeightTextBox_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (nameTextBox.Text.Length == 0 && e.KeyChar == ' ')
            {
                e.Handled = true;
                MessageBox.Show("Spaces are not allowed in the beginning.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
                e.Handled = false;
        }

        private void nameTextBox_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (nameTextBox.Text.Length == 0 && e.KeyChar == ' ')
            {
                e.Handled = true;
                MessageBox.Show("Spaces are not allowed in the beginning.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
                e.Handled = false;
        }

        private void date_AcquiredDateTimePicker_ValueChanged_1(object sender, EventArgs e)
        {
            int res = DateTime.Compare(expiryDateTimePicker.Value, date_AcquiredDateTimePicker.Value);
            if (res < 0)
            {
                MessageBox.Show("Acquisition date cannot be on or after the expiry date.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                date_AcquiredDateTimePicker.Value = DateTime.Today.AddDays(1 - DateTime.Today.Day);
            }
            else if (res == 0)
            {
                MessageBox.Show("Acquisition date cannot be on or after the expiry date.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                date_AcquiredDateTimePicker.Value = DateTime.Today.AddDays(1 - DateTime.Today.Day);
            }
        }

        private void expiryDateTimePicker_ValueChanged_1(object sender, EventArgs e)
        {
            int res = DateTime.Compare(expiryDateTimePicker.Value, date_AcquiredDateTimePicker.Value);
            if (res < 0)
            {
                MessageBox.Show("Expiry date cannot be on or before the acquisition date.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                expiryDateTimePicker.Value = DateTime.Today.AddDays(1 + DateTime.Today.Day);
            }
            else if (res == 0)
            {
                MessageBox.Show("Expiry date cannot be on or before the acquisition date.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                expiryDateTimePicker.Value = DateTime.Today.AddDays(1 + DateTime.Today.Day);
            }
        }

    }
}
