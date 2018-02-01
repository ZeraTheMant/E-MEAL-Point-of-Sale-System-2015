using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.IO;


namespace WindowsFormsApplication1
{
    public partial class Register : Form
    {

        int var = 1;
        int id;
        SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\fape\Desktop\EMEAL\EMEAL\bin\Debug\DB.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");
        public Register()
        {
            InitializeComponent();
 
        }

        private void Register_Load(object sender, EventArgs e)
        {
                nameTextBox.ReadOnly = true;
                passwordTextBox.ReadOnly = true;
                textBox1.ReadOnly = true;
                levelComboBox.Enabled = false;         
            // TODO: This line of code loads data into the 'dBDataSet.USERS' table. You can move, or remove it, as needed.
            //this.uSERSTableAdapter.Fill(this.dBDataSet.USERS);
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
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

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            iDTextBox.Clear();
            nameTextBox.Clear();
            passwordTextBox.Clear();
            textBox1.Clear();
            levelComboBox.SelectedIndex = -1;
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            levelComboBox.SelectedIndex = -1;
        }

        private void passwordTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (textBox1.Text.Length == 0 && e.KeyChar == ' ')
            {
                e.Handled = true;
                MessageBox.Show("Spaces are not allowed in the password fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
                e.Handled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
                                    DialogResult dr = MessageBox.Show("Do you want to cancel registration? All unsaved data will be lost.", "Cancel and Return", MessageBoxButtons.YesNo,
MessageBoxIcon.Question);
                                    if (dr == DialogResult.Yes)
                                    {
                                        this.Hide();
                                    }
                                    else
                                        MessageBox.Show("Continue your work.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Accounts ac = new Accounts();
            ac.ShowDialog();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                e.Handled = true;
                MessageBox.Show("Spaces are not allowed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void bindingNavigatorAddNewItem_Click_1(object sender, EventArgs e)
        {
            levelComboBox.SelectedIndex = -1;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (check() == false)
            {
                try
                {
                    if (nameTextBox.Text.Length > 4 && passwordTextBox.Text.Length > 4)
                    {
                        if (passwordTextBox.Text.Equals(textBox1.Text) && nameTextBox.Text.Trim().Length !=0  && passwordTextBox.Text.Trim().Length !=0 && levelComboBox.SelectedIndex !=-1)
                        {
                            DialogResult dr = MessageBox.Show("Do you want to save the account?", "Confirm Modifications", MessageBoxButtons.YesNo,
MessageBoxIcon.Question);
                            if (dr == DialogResult.Yes)
                            {
                                this.Validate();
                                this.uSERSBindingSource.EndEdit();
                                this.tableAdapterManager.UpdateAll(this.dBDataSet);
                                MessageBox.Show("Account creation successful.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                iDTextBox.Text = "";
                                nameTextBox.Text = "";
                                passwordTextBox.Text = "";
                                textBox1.Text = "";
                                levelComboBox.SelectedIndex = -1;
                            }
                            else
                                MessageBox.Show("Continue your work.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else if ((nameTextBox.Text.Trim().Length == 0) || (passwordTextBox.Text.Trim().Length == 0))
                        {
                            MessageBox.Show("Don't leave the Username or Password values blank", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else if (levelComboBox.SelectedIndex == -1)
                        {
                            MessageBox.Show("Please select a user level.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else if (!nameTextBox.Text.Equals(textBox1.Text))
                        {
                            MessageBox.Show("Password and Confirm password values don't match.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Username and Password have a minimum of 5 characters.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Null values are not allowed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
                MessageBox.Show("Username already taken. Choose a unique one.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void passwordTextBox_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                e.Handled = true;
                MessageBox.Show("Spaces are not allowed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void nameTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                e.Handled = true;
                MessageBox.Show("Spaces are not allowed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void bindingNavigatorPositionItem_TextChanged(object sender, EventArgs e)
        {
            if (bindingNavigatorPositionItem.Text.ToString().Equals("0"))
            {
                nameTextBox.ReadOnly = true;
                passwordTextBox.ReadOnly = true;
                textBox1.ReadOnly = true;
                levelComboBox.Enabled = false;
            }
            else
            {
                nameTextBox.ReadOnly = false;
                passwordTextBox.ReadOnly = false;
                textBox1.ReadOnly = false;
                levelComboBox.Enabled = true;
            }
        }
    }
}
