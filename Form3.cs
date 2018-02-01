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
    public partial class Form3 : Form
    {
        int id;
        SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\fape\Desktop\EMEAL\EMEAL\bin\Debug\DB.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");
        public Form3()
        {
            InitializeComponent();
        }

        private void uSERSBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            if (check() == false)
            {
                try
                {
                    if (nameTextBox.Text.Length > 4 && passwordTextBox.Text.Length > 4)
                    {
                        if (passwordTextBox.Text.Equals(textBox1.Text))
                        {
                                                       DialogResult dr = MessageBox.Show("Do you want to save the changes? This process is not reversible.", "Confirm Modifications", MessageBoxButtons.YesNo,
MessageBoxIcon.Question);
                                                       if (dr == DialogResult.Yes)
                                                       {
                                                           this.Validate();
                                                           this.uSERSBindingSource.EndEdit();
                                                           this.tableAdapterManager.UpdateAll(this.dBDataSet);
                                                           levelComboBox.SelectedIndex = -1;
                                                       }
                                                       else
                                                           MessageBox.Show("Continue your work.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                            MessageBox.Show("Password and Confirm Password values do not match.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                        MessageBox.Show("Username and Password have a minimum of 5 characters.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Null values are not allowed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
                MessageBox.Show("Username already taken. Choose a unique one.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dBDataSet.USERS' table. You can move, or remove it, as needed.
            this.uSERSTableAdapter.Fill(this.dBDataSet.USERS);

        }

        private void uSERSDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            nameTextBox.Enabled = false;
            try
            {
                id = Convert.ToInt32(uSERSDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString());
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from [TABLE] where ID=" + id + "";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    levelComboBox.SelectedItem = dr["Category"].ToString();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void passwordTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (passwordTextBox.Text.Length == 0 && e.KeyChar == ' ')
            {
                e.Handled = true;
                MessageBox.Show("Spaces are not allowed in the password field.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
                e.Handled = false;
        }

        private void nameTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (nameTextBox.Text.Length == 0 && e.KeyChar == ' ')
            {
                e.Handled = true;
                MessageBox.Show("Spaces are not allowed in the name field.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
                e.Handled = false;
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            levelComboBox.SelectedIndex = -1;
        }

        public bool check()
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT Name, Password FROM USERS";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            ds.Tables.Add(dt);
            da.Fill(dt);

            foreach (DataRow r in dt.Rows)
            {
                if (r[0].ToString() == nameTextBox.Text)
                    return true;
            }
            return false;
        }
    }
}
