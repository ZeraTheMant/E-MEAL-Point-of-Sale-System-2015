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
    public partial class Login : Form
    {
        int tik = 30;
        SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\fape\Desktop\EMEAL\EMEAL\bin\Debug\DB.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");
        public Login()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (check() == true)
                {
                    if (label3.Text.ToString().Equals("Admin"))
                    {
                        Class1.isAdmin = true;
                    }
                    Class1.ctr = 1;
                    if (label3.Text.ToString().Equals("Admin"))
                    {
                        MessageBox.Show("Welcome " + userTxt.Text + ".\n\nYour user level is [" + label3.Text.ToString() + "]\n\nYou have full access to all system's modules.", "Log-in Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Class1.user1 = userTxt.Text;
                    }
                    else
                    {
                        MessageBox.Show("Welcome " + userTxt.Text + ".\n\nYour user level is [" + label3.Text.ToString() + "]\n\nYour access is limited to customer transactions.", "Log-in Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Class1.user1 = userTxt.Text;
                    }
                    Form1 f1 = new Form1();
                    f1.Show();
                    this.Hide();
                }
                else
                {
                    if (Class1.ctr < 4)
                    {
                        MessageBox.Show("Invalid log-in. Attempt number " + Class1.ctr + ". Maximum of 3 invalid attempts.\n\nTIP: Make sure that the username and password are registered.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        Class1.ctr++;
                    }
                    else
                    {
                        button1.Enabled = false;
                        DialogResult dr = MessageBox.Show("You have surpassed the 3 invalid log-in attempts allocated to you. You will have to wait for 30 seconds to log-in again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        if (dr == DialogResult.OK)
                        {
                            timer1.Start();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error. Try logging in again later.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
        }

        public bool check()
        {
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT Name, Password, Level FROM USERS";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            ds.Tables.Add(dt);
            da.Fill(dt);

            foreach (DataRow r in dt.Rows)
            {
                if (r[0].ToString() == userTxt.Text && r[1].ToString() == passTxt.Text)
                {
                    Class1.user = r[0].ToString();
                    label3.Text = r[2].ToString();
                    return true;
                }

            }
            return false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Countdown.Text = tik + " Seconds Remaining";
            if (tik > 0)
            {
                tik--;
            }
            else
            {
                Countdown.Text = "";
                timer1.Stop();
                MessageBox.Show("You may now log-in again.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                button1.Enabled = true;
                tik = 30;
                Class1.ctr = 1;
            }
        }

    }
}
