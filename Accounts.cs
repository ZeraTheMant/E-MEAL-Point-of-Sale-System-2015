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
    public partial class Accounts : Form
    {
        string xa;
        SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\fape\Desktop\EMEAL\EMEAL\bin\Debug\DB.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");
        public Accounts()
        {
            InitializeComponent();
        }

        private void Accounts_Load(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1)
            {
                textBox1.Enabled = false;
                textBox2.Enabled = false;
                button1.Enabled = false;
                button2.Enabled = false;
                comboBox2.Enabled = false;
            }
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();

            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select Name from USERS";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                comboBox1.Items.Add(dr["Name"].ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand gg = con.CreateCommand();
            gg.CommandType = CommandType.Text;
            gg.CommandText = "select Password from USERS where Name='"+label5.Text.ToString()+"'";
            gg.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(gg);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                xa = dr["Password"].ToString();
            }
            if (textBox2.Text.ToString().Equals(xa))
            {
                if (textBox1.Text.Length > 4)
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "UPDATE USERS SET Password = '" + textBox1.Text + "' WHERE Name= '" + comboBox1.SelectedItem.ToString() + "'";
                    cmd.ExecuteNonQuery();
                    textBox3.Text = textBox1.Text;
                    MessageBox.Show("Password change successful.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Password has a minimum of 5 characters.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
                MessageBox.Show("Old password is incorrect.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                e.Handled = true;
                MessageBox.Show("Spaces are not allowed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE USERS SET Level = '" + comboBox2.SelectedItem.ToString() + "' WHERE Name= '" + comboBox1.SelectedItem.ToString() + "'";
                cmd.ExecuteNonQuery();
                label9.Text = comboBox2.SelectedItem.ToString();
                MessageBox.Show("User level change successful.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please select a user level.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!comboBox1.SelectedItem.ToString().Equals(Class1.user1.ToString()))
            {
                DialogResult dr = MessageBox.Show("Do you want to delete the account? Remember that deletion is permanent.", "Delete Accounts", MessageBoxButtons.YesNo,
    MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    string x = comboBox1.SelectedItem.ToString();
                    comboBox1.SelectedIndex = -1;
                    cmd.CommandText = "DELETE FROM USERS WHERE Name = '" + x + "'";
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Account deletion successful.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox1.Enabled = false;
                    textBox2.Enabled = false;
                    button1.Enabled = false;
                    button2.Enabled = false;
                    comboBox2.Enabled = false;
                    label5.Text = "";
                    textBox3.Text = "";
                    label9.Text = "";
                    comboBox1.Items.Clear();

                    SqlCommand cmd1 = con.CreateCommand();
                    cmd1.CommandType = CommandType.Text;
                    cmd1.CommandText = "select Name from USERS";
                    cmd1.ExecuteNonQuery();
                    DataTable dt1 = new DataTable();
                    SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                    da1.Fill(dt1);
                    foreach (DataRow drs in dt1.Rows)
                    {
                        comboBox1.Items.Add(drs["Name"].ToString());
                    }
                }
                else
                    MessageBox.Show("Continue your work.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Cannot delete an account that is currently in use.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from USERS WHERE Name= '" + comboBox1.SelectedItem.ToString() + "'";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    label5.Text = dr["Name"].ToString();
                    textBox3.Text = dr["Password"].ToString();
                    label9.Text = dr["Level"].ToString();
                }
                textBox1.Enabled = true;
                textBox2.Enabled = true;
                button1.Enabled = true;
                button2.Enabled = true;
                comboBox2.Enabled = true;
                comboBox2.SelectedIndex = -1;
            }
        }

        private void button4_Click(object sender, EventArgs e)
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


        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString().Equals(Class1.user1))
            {
                comboBox2.SelectedIndex = -1;
            }
        }
    }
}
