using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Globalization;

namespace WindowsFormsApplication1
{
    public partial class Quan : Form
    {
        string a;
        string b;
        string c;
        string d;
        string ez;
        string f;
        string gz;
        SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\fape\Desktop\EMEAL\EMEAL\bin\Debug\DB.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");
        public Form1 MyMainForm { get; set; }
        public Quan()
        {
            InitializeComponent();
        }

        private void Quan_Load(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            Namess.Text = MyMainForm.Dummy.Text.ToString();
            label2.Text = MyMainForm.Dumbo.Text.ToString();
            SqlCommand cho = con.CreateCommand();
            cho.CommandType = CommandType.Text;
            cho.CommandText = "select * from PDF where NAME='" + MyMainForm.Dummy.Text + "'";
            cho.ExecuteNonQuery();
            DataTable dx = new DataTable();
            SqlDataAdapter dg = new SqlDataAdapter(cho);
            dg.Fill(dx);
            foreach (DataRow dd in dx.Rows)
            {
                a = dd["ing1"].ToString();
                b = dd["ing2"].ToString();
                c = dd["ing3"].ToString();
                d = dd["ing4"].ToString();
                ez = dd["ing5"].ToString();
                f = dd["ing6"].ToString();
                gz = dd["ing7"].ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            int cout = 0;
            for (int h = 0; h <= 6; h++)
            {
                if (h == 0)
                {
                    double gogo = 0;
                    SqlCommand comm = con.CreateCommand();
                    comm.CommandType = CommandType.Text;
                    comm.CommandText = "UPDATE Supplies SET [Quantity/Weight] = [Quantity/Weight] - '" + numericUpDown1.Value + "' WHERE  Name='" + a + "'";
                    comm.ExecuteNonQuery();
                    SqlCommand seca = con.CreateCommand();
                    seca.CommandType = CommandType.Text;
                    seca.CommandText = "SELECT * FROM Supplies WHERE Name='" + a + "'";
                    seca.ExecuteNonQuery();
                    DataTable df = new DataTable();
                    SqlDataAdapter dm = new SqlDataAdapter(seca);
                    dm.Fill(df);
                    foreach (DataRow dd in df.Rows)
                    {
                        gogo = float.Parse(dd["Quantity/Weight"].ToString(), CultureInfo.InvariantCulture.NumberFormat);
                    }

                    if (gogo < 0)
                    {
                        SqlCommand comm1 = con.CreateCommand();
                        comm1.CommandType = CommandType.Text;
                        comm1.CommandText = "UPDATE Supplies SET [Quantity/Weight] = [Quantity/Weight] + '" + numericUpDown1.Value + "' WHERE  Name='" + a + "'";
                        comm1.ExecuteNonQuery();
                        comm1.CommandText = "UPDATE Supplies SET [Quantity/Weight] = [Quantity/Weight] + '" + numericUpDown1.Value + "' WHERE  Name='" + b + "'";
                        comm1.ExecuteNonQuery();
                        comm1.CommandText = "UPDATE Supplies SET [Quantity/Weight] = [Quantity/Weight] + '" + numericUpDown1.Value + "' WHERE  Name='" + c + "'";
                        comm1.ExecuteNonQuery();
                        comm1.CommandText = "UPDATE Supplies SET [Quantity/Weight] = [Quantity/Weight] + '" + numericUpDown1.Value + "' WHERE  Name='" + d + "'";
                        comm1.ExecuteNonQuery();
                        comm1.CommandText = "UPDATE Supplies SET [Quantity/Weight] = [Quantity/Weight] + '" + numericUpDown1.Value + "' WHERE  Name='" + ez + "'";
                        comm1.ExecuteNonQuery();
                        comm1.CommandText = "UPDATE Supplies SET [Quantity/Weight] = [Quantity/Weight] + '" + numericUpDown1.Value + "' WHERE  Name='" + f + "'";
                        comm1.ExecuteNonQuery();
                        comm1.CommandText = "UPDATE Supplies SET [Quantity/Weight] = [Quantity/Weight] + '" + numericUpDown1.Value + "' WHERE  Name='" + gz + "'";
                        comm1.ExecuteNonQuery();
                        MessageBox.Show("Supplies of " + a + " are insufficient. Please restock.");
                        cout = 0;
                    }
                    else
                        cout++;
                }

                else if (h == 1)
                {
                    double gogo = 0;
                    SqlCommand c1 = con.CreateCommand();
                    c1.CommandType = CommandType.Text;
                    c1.CommandText = "UPDATE Supplies SET [Quantity/Weight] = [Quantity/Weight] - '" + numericUpDown1.Value + "' WHERE  Name='" + b + "'";
                    c1.ExecuteNonQuery();
                    SqlCommand seca = con.CreateCommand();
                    seca.CommandType = CommandType.Text;
                    seca.CommandText = "SELECT * FROM Supplies WHERE Name='" + b + "'";
                    seca.ExecuteNonQuery();
                    DataTable df = new DataTable();
                    SqlDataAdapter dm = new SqlDataAdapter(seca);
                    dm.Fill(df);
                    foreach (DataRow dd in df.Rows)
                    {
                        gogo = float.Parse(dd["Quantity/Weight"].ToString(), CultureInfo.InvariantCulture.NumberFormat);
                    }

                    if (gogo < 0)
                    {
                        SqlCommand comm1 = con.CreateCommand();
                        comm1.CommandType = CommandType.Text;
                        comm1.CommandText = "UPDATE Supplies SET [Quantity/Weight] = [Quantity/Weight] + '" + numericUpDown1.Value + "' WHERE  Name='" + a + "'";
                        comm1.ExecuteNonQuery();
                        comm1.CommandText = "UPDATE Supplies SET [Quantity/Weight] = [Quantity/Weight] + '" + numericUpDown1.Value + "' WHERE  Name='" + b + "'";
                        comm1.ExecuteNonQuery();
                        comm1.CommandText = "UPDATE Supplies SET [Quantity/Weight] = [Quantity/Weight] + '" + numericUpDown1.Value + "' WHERE  Name='" + c + "'";
                        comm1.ExecuteNonQuery();
                        comm1.CommandText = "UPDATE Supplies SET [Quantity/Weight] = [Quantity/Weight] + '" + numericUpDown1.Value + "' WHERE  Name='" + d + "'";
                        comm1.ExecuteNonQuery();
                        comm1.CommandText = "UPDATE Supplies SET [Quantity/Weight] = [Quantity/Weight] + '" + numericUpDown1.Value + "' WHERE  Name='" + ez + "'";
                        comm1.ExecuteNonQuery();
                        comm1.CommandText = "UPDATE Supplies SET [Quantity/Weight] = [Quantity/Weight] + '" + numericUpDown1.Value + "' WHERE  Name='" + f + "'";
                        comm1.ExecuteNonQuery();
                        comm1.CommandText = "UPDATE Supplies SET [Quantity/Weight] = [Quantity/Weight] + '" + numericUpDown1.Value + "' WHERE  Name='" + gz + "'";
                        comm1.ExecuteNonQuery();
                        MessageBox.Show("Supplies of " + b + " are insufficient. Please restock.");
                    }
                    else
                        cout++;
                }

                else if (h == 2)
                {
                    double gogo = 0;
                    SqlCommand c2 = con.CreateCommand();
                    c2.CommandType = CommandType.Text;
                    c2.CommandText = "UPDATE Supplies SET [Quantity/Weight] = [Quantity/Weight] - '" + numericUpDown1.Value + "' WHERE  Name='" + c + "'";
                    c2.ExecuteNonQuery();
                    SqlCommand seca = con.CreateCommand();
                    seca.CommandType = CommandType.Text;
                    seca.CommandText = "SELECT * FROM Supplies WHERE Name='" + c + "'";
                    seca.ExecuteNonQuery();
                    DataTable df = new DataTable();
                    SqlDataAdapter dm = new SqlDataAdapter(seca);
                    dm.Fill(df);
                    foreach (DataRow dd in df.Rows)
                    {
                        gogo = float.Parse(dd["Quantity/Weight"].ToString(), CultureInfo.InvariantCulture.NumberFormat);
                    }

                    if (gogo < 0)
                    {
                        SqlCommand comm1 = con.CreateCommand();
                        comm1.CommandType = CommandType.Text;
                        comm1.CommandText = "UPDATE Supplies SET [Quantity/Weight] = [Quantity/Weight] + '" + numericUpDown1.Value + "' WHERE  Name='" + a + "'";
                        comm1.ExecuteNonQuery();
                        comm1.CommandText = "UPDATE Supplies SET [Quantity/Weight] = [Quantity/Weight] + '" + numericUpDown1.Value + "' WHERE  Name='" + b + "'";
                        comm1.ExecuteNonQuery();
                        comm1.CommandText = "UPDATE Supplies SET [Quantity/Weight] = [Quantity/Weight] + '" + numericUpDown1.Value + "' WHERE  Name='" + c + "'";
                        comm1.ExecuteNonQuery();
                        comm1.CommandText = "UPDATE Supplies SET [Quantity/Weight] = [Quantity/Weight] + '" + numericUpDown1.Value + "' WHERE  Name='" + d + "'";
                        comm1.ExecuteNonQuery();
                        comm1.CommandText = "UPDATE Supplies SET [Quantity/Weight] = [Quantity/Weight] + '" + numericUpDown1.Value + "' WHERE  Name='" + ez + "'";
                        comm1.ExecuteNonQuery();
                        comm1.CommandText = "UPDATE Supplies SET [Quantity/Weight] = [Quantity/Weight] + '" + numericUpDown1.Value + "' WHERE  Name='" + f + "'";
                        comm1.ExecuteNonQuery();
                        comm1.CommandText = "UPDATE Supplies SET [Quantity/Weight] = [Quantity/Weight] + '" + numericUpDown1.Value + "' WHERE  Name='" + gz + "'";
                        comm1.ExecuteNonQuery();
                        MessageBox.Show("Supplies of " + c + " are insufficient. Please restock.");
                    }
                    else
                        cout++;
              
                }

                else if (h == 3)
                {
                    double gogo = 0;
                    SqlCommand c3 = con.CreateCommand();
                    c3.CommandType = CommandType.Text;
                    c3.CommandText = "UPDATE Supplies SET [Quantity/Weight] = [Quantity/Weight] - '" + numericUpDown1.Value + "' WHERE  Name='" + d + "'";
                    c3.ExecuteNonQuery();
                    SqlCommand seca = con.CreateCommand();
                    seca.CommandType = CommandType.Text;
                    seca.CommandText = "SELECT * FROM Supplies WHERE Name='" + d + "'";
                    seca.ExecuteNonQuery();
                    DataTable df = new DataTable();
                    SqlDataAdapter dm = new SqlDataAdapter(seca);
                    dm.Fill(df);
                    foreach (DataRow dd in df.Rows)
                    {
                        gogo = float.Parse(dd["Quantity/Weight"].ToString(), CultureInfo.InvariantCulture.NumberFormat);
                    }

                    if (gogo < 0)
                    {
                        SqlCommand comm1 = con.CreateCommand();
                        comm1.CommandType = CommandType.Text;
                        comm1.CommandText = "UPDATE Supplies SET [Quantity/Weight] = [Quantity/Weight] + '" + numericUpDown1.Value + "' WHERE  Name='" + a + "'";
                        comm1.ExecuteNonQuery();
                        comm1.CommandText = "UPDATE Supplies SET [Quantity/Weight] = [Quantity/Weight] + '" + numericUpDown1.Value + "' WHERE  Name='" + b + "'";
                        comm1.ExecuteNonQuery();
                        comm1.CommandText = "UPDATE Supplies SET [Quantity/Weight] = [Quantity/Weight] + '" + numericUpDown1.Value + "' WHERE  Name='" + c + "'";
                        comm1.ExecuteNonQuery();
                        comm1.CommandText = "UPDATE Supplies SET [Quantity/Weight] = [Quantity/Weight] + '" + numericUpDown1.Value + "' WHERE  Name='" + d + "'";
                        comm1.ExecuteNonQuery();
                        comm1.CommandText = "UPDATE Supplies SET [Quantity/Weight] = [Quantity/Weight] + '" + numericUpDown1.Value + "' WHERE  Name='" + ez + "'";
                        comm1.ExecuteNonQuery();
                        comm1.CommandText = "UPDATE Supplies SET [Quantity/Weight] = [Quantity/Weight] + '" + numericUpDown1.Value + "' WHERE  Name='" + f + "'";
                        comm1.ExecuteNonQuery();
                        comm1.CommandText = "UPDATE Supplies SET [Quantity/Weight] = [Quantity/Weight] + '" + numericUpDown1.Value + "' WHERE  Name='" + gz + "'";
                        comm1.ExecuteNonQuery();
                        MessageBox.Show("Supplies of " + d + " are insufficient. Please restock.");
                    }
                    else
                        cout++;
                }

                else if (h == 4)
                {
                    double gogo = 0;
                    SqlCommand c4 = con.CreateCommand();
                    c4.CommandType = CommandType.Text;
                    c4.CommandText = "UPDATE Supplies SET [Quantity/Weight] = [Quantity/Weight] - '" + numericUpDown1.Value + "' WHERE  Name='" + ez + "'";
                    c4.ExecuteNonQuery();
                    SqlCommand seca = con.CreateCommand();
                    seca.CommandType = CommandType.Text;
                    seca.CommandText = "SELECT * FROM Supplies WHERE Name='" + ez + "'";
                    seca.ExecuteNonQuery();
                    DataTable df = new DataTable();
                    SqlDataAdapter dm = new SqlDataAdapter(seca);
                    dm.Fill(df);
                    foreach (DataRow dd in df.Rows)
                    {
                        gogo = float.Parse(dd["Quantity/Weight"].ToString(), CultureInfo.InvariantCulture.NumberFormat);
                    }

                    if (gogo < 0)
                    {
                        SqlCommand comm1 = con.CreateCommand();
                        comm1.CommandType = CommandType.Text;
                        comm1.CommandText = "UPDATE Supplies SET [Quantity/Weight] = [Quantity/Weight] + '" + numericUpDown1.Value + "' WHERE  Name='" + a + "'";
                        comm1.ExecuteNonQuery();
                        comm1.CommandText = "UPDATE Supplies SET [Quantity/Weight] = [Quantity/Weight] + '" + numericUpDown1.Value + "' WHERE  Name='" + b + "'";
                        comm1.ExecuteNonQuery();
                        comm1.CommandText = "UPDATE Supplies SET [Quantity/Weight] = [Quantity/Weight] + '" + numericUpDown1.Value + "' WHERE  Name='" + c + "'";
                        comm1.ExecuteNonQuery();
                        comm1.CommandText = "UPDATE Supplies SET [Quantity/Weight] = [Quantity/Weight] + '" + numericUpDown1.Value + "' WHERE  Name='" + d + "'";
                        comm1.ExecuteNonQuery();
                        comm1.CommandText = "UPDATE Supplies SET [Quantity/Weight] = [Quantity/Weight] + '" + numericUpDown1.Value + "' WHERE  Name='" + ez + "'";
                        comm1.ExecuteNonQuery();
                        comm1.CommandText = "UPDATE Supplies SET [Quantity/Weight] = [Quantity/Weight] + '" + numericUpDown1.Value + "' WHERE  Name='" + f + "'";
                        comm1.ExecuteNonQuery();
                        comm1.CommandText = "UPDATE Supplies SET [Quantity/Weight] = [Quantity/Weight] + '" + numericUpDown1.Value + "' WHERE  Name='" + gz + "'";
                        comm1.ExecuteNonQuery();
                        MessageBox.Show("Supplies of " + ez + " are insufficient. Please restock.");
                    }
                    else
                        cout++;
                }

                else if (h == 6)
                {
                    double gogo = 0;
                    SqlCommand c5 = con.CreateCommand();
                    c5.CommandType = CommandType.Text;
                    c5.CommandText = "UPDATE Supplies SET [Quantity/Weight] = [Quantity/Weight] - '" + numericUpDown1.Value + "' WHERE  Name='" + f + "'";
                    c5.ExecuteNonQuery();
                    SqlCommand seca = con.CreateCommand();
                    seca.CommandType = CommandType.Text;
                    seca.CommandText = "SELECT * FROM Supplies WHERE Name='" + f + "'";
                    seca.ExecuteNonQuery();
                    DataTable df = new DataTable();
                    SqlDataAdapter dm = new SqlDataAdapter(seca);
                    dm.Fill(df);
                    foreach (DataRow dd in df.Rows)
                    {
                        gogo = float.Parse(dd["Quantity/Weight"].ToString(), CultureInfo.InvariantCulture.NumberFormat);
                    }

                    if (gogo < 0)
                    {
                        SqlCommand comm1 = con.CreateCommand();
                        comm1.CommandType = CommandType.Text;
                        comm1.CommandText = "UPDATE Supplies SET [Quantity/Weight] = [Quantity/Weight] + '" + numericUpDown1.Value + "' WHERE  Name='" + a + "'";
                        comm1.ExecuteNonQuery();
                        comm1.CommandText = "UPDATE Supplies SET [Quantity/Weight] = [Quantity/Weight] + '" + numericUpDown1.Value + "' WHERE  Name='" + b + "'";
                        comm1.ExecuteNonQuery();
                        comm1.CommandText = "UPDATE Supplies SET [Quantity/Weight] = [Quantity/Weight] + '" + numericUpDown1.Value + "' WHERE  Name='" + c + "'";
                        comm1.ExecuteNonQuery();
                        comm1.CommandText = "UPDATE Supplies SET [Quantity/Weight] = [Quantity/Weight] + '" + numericUpDown1.Value + "' WHERE  Name='" + d + "'";
                        comm1.ExecuteNonQuery();
                        comm1.CommandText = "UPDATE Supplies SET [Quantity/Weight] = [Quantity/Weight] + '" + numericUpDown1.Value + "' WHERE  Name='" + ez + "'";
                        comm1.ExecuteNonQuery();
                        comm1.CommandText = "UPDATE Supplies SET [Quantity/Weight] = [Quantity/Weight] + '" + numericUpDown1.Value + "' WHERE  Name='" + f + "'";
                        comm1.ExecuteNonQuery();
                        comm1.CommandText = "UPDATE Supplies SET [Quantity/Weight] = [Quantity/Weight] + '" + numericUpDown1.Value + "' WHERE  Name='" + gz + "'";
                        comm1.ExecuteNonQuery();
                        MessageBox.Show("Supplies of " + f + " are insufficient. Please restock.");
                    }
                    else
                        cout++;
                }

                else
                {

                    double gogo = 0;
                    SqlCommand c6 = con.CreateCommand();
                    c6.CommandType = CommandType.Text;
                    c6.CommandText = "UPDATE Supplies SET [Quantity/Weight] = [Quantity/Weight] - '" + numericUpDown1.Value + "' WHERE  Name='" + gz + "'";
                    c6.ExecuteNonQuery();
                    SqlCommand seca = con.CreateCommand();
                    seca.CommandType = CommandType.Text;
                    seca.CommandText = "SELECT * FROM Supplies WHERE Name='" + gz + "'";
                    seca.ExecuteNonQuery();
                    DataTable df = new DataTable();
                    SqlDataAdapter dm = new SqlDataAdapter(seca);
                    dm.Fill(df);
                    foreach (DataRow dd in df.Rows)
                    {
                        gogo = float.Parse(dd["Quantity/Weight"].ToString(), CultureInfo.InvariantCulture.NumberFormat);
                    }

                    if (gogo < 0)
                    {
                        SqlCommand comm1 = con.CreateCommand();
                        comm1.CommandType = CommandType.Text;
                        comm1.CommandText = "UPDATE Supplies SET [Quantity/Weight] = [Quantity/Weight] + '" + numericUpDown1.Value + "' WHERE  Name='" + a + "'";
                        comm1.ExecuteNonQuery();
                        comm1.CommandText = "UPDATE Supplies SET [Quantity/Weight] = [Quantity/Weight] + '" + numericUpDown1.Value + "' WHERE  Name='" + b + "'";
                        comm1.ExecuteNonQuery();
                        comm1.CommandText = "UPDATE Supplies SET [Quantity/Weight] = [Quantity/Weight] + '" + numericUpDown1.Value + "' WHERE  Name='" + c + "'";
                        comm1.ExecuteNonQuery();
                        comm1.CommandText = "UPDATE Supplies SET [Quantity/Weight] = [Quantity/Weight] + '" + numericUpDown1.Value + "' WHERE  Name='" + d + "'";
                        comm1.ExecuteNonQuery();
                        comm1.CommandText = "UPDATE Supplies SET [Quantity/Weight] = [Quantity/Weight] + '" + numericUpDown1.Value + "' WHERE  Name='" + ez + "'";
                        comm1.ExecuteNonQuery();
                        comm1.CommandText = "UPDATE Supplies SET [Quantity/Weight] = [Quantity/Weight] + '" + numericUpDown1.Value + "' WHERE  Name='" + f + "'";
                        comm1.ExecuteNonQuery();
                        comm1.CommandText = "UPDATE Supplies SET [Quantity/Weight] = [Quantity/Weight] + '" + numericUpDown1.Value + "' WHERE  Name='" + gz + "'";
                        comm1.ExecuteNonQuery();
                        MessageBox.Show("Supplies of " + gz + " are insufficient. Please restock.");
                    }
                    else
                        cout++;
                }
            }
            

                    if (cout==7)
                    {
                        Class1.pop = decimal.Parse(label2.Text.ToString()) * decimal.Parse(numericUpDown1.Value.ToString());
                        string[] row = new string[] { Namess.Text.ToString(), numericUpDown1.Value.ToString(), "" + Class1.pop };
                        //MessageBox.Show("Supplies of " + Namess.Text.ToString() + " left: "+x.ToString()+"");
                        MyMainForm.dataGridView3.Rows.Add(row);
                        this.Hide();
                    }
                
        }
    }
}
