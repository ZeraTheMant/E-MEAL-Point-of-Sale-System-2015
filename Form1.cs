using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Diagnostics;
using System.Globalization;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        int id;
        string temp;
        string a;
        string b;
        string c;
        string d;
        string ez;
        string f;
        string gz;
        SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\fape\Desktop\EMEAL\EMEAL\bin\Debug\DB.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");
        public Form1()
        {
            InitializeComponent();
            //this.KeyPreview = true;
            //this.KeyPress += new KeyPressEventHandler(dataGridView1_KeyPress);
            timer1.Start();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            if (Class1.isAdmin == false)
            {
                button1.Enabled = false;
                button3.Enabled = false;
                button5.Enabled = false;
                button7.Enabled = false;
            }

            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();

            string fd = "Food";
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from PDF where CATEGORY='"+fd+"'";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            DGV.DataSource = dt;
            this.dataGridView3.Columns["Price"].DefaultCellStyle.Format = "0.00";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form4 m = new Form4() { MyMainForm = this };
            m.ShowDialog();
        }

        private void tABLEDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                id = Convert.ToInt32(DGV.Rows[e.RowIndex].Cells[0].Value.ToString());
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from PDF where ID=" + id + "";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    Dummy.Text = dr["NAME"].ToString();
                    Dumbo.Text = dr["PRICE"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error. Only select cells with inputted values.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void tABLEDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            SqlCommand cho = con.CreateCommand();
            cho.CommandType = CommandType.Text;
            cho.CommandText = "select * from PDF where NAME='" + Dummy.Text + "'";
            cho.ExecuteNonQuery();
            DataTable dos = new DataTable();
            SqlDataAdapter dpa = new SqlDataAdapter(cho);
            dpa.Fill(dos);
            foreach (DataRow dd in dos.Rows)
            {
                a = dd["ing1"].ToString();
                b = dd["ing2"].ToString();
                c = dd["ing3"].ToString();
                d = dd["ing4"].ToString();
                ez = dd["ing5"].ToString();
                f = dd["ing6"].ToString();
                gz = dd["ing7"].ToString();
            }
            SqlCommand s = con.CreateCommand();
            s.CommandType = CommandType.Text;
            s.CommandText = "select Expiry from Supplies where Name='" + a + "' OR  Name = '" + b + "' OR  Name = '" + c + "' OR  Name = '" + d + "' OR  Name = '" + ez + "' OR  Name = '" + f + "' OR  Name = '" + gz + "' ";
            s.ExecuteNonQuery();
            DataTable dz = new DataTable();
            SqlDataAdapter db = new SqlDataAdapter(s);
            db.Fill(dz);
            int res = 0;
            int jug = 0;
            foreach (DataRow dd in dz.Rows)
            {
                res = DateTime.Compare(DateTime.Now, Convert.ToDateTime(dd["Expiry"].ToString()));
                if (res > 0)
                {
                    jug++;
                }
   
            }
            if (jug == 0)
            {
                int o = 0;
                double x = 0;
                SqlCommand second = con.CreateCommand();
                second.CommandType = CommandType.Text;
                second.CommandText = "SELECT * FROM Supplies WHERE Name='" + a + "' OR  Name = '" + b + "' OR  Name = '" + c + "' OR  Name = '" + d + "' OR  Name = '" + ez + "' OR  Name = '" + f + "' OR  Name = '" + gz + "' ";
                second.ExecuteNonQuery();
                DataTable dq = new DataTable();
                SqlDataAdapter dg = new SqlDataAdapter(second);
                dg.Fill(dq);
                foreach (DataRow dd in dq.Rows)
                {
                    //label1.Text = dd["MainProduct"].ToString();
                    x = float.Parse(dd["Quantity/Weight"].ToString(), CultureInfo.InvariantCulture.NumberFormat);
                }
                if (x < 1)
                {
                    MessageBox.Show("Supplies are insufficient. Please restock.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    bool jo = false;
                    for (int i = 0; i < dataGridView3.Rows.Count; i++)
                    {
                        for (int j = 0; j < dataGridView3.Columns.Count; j++)
                        {
                            if (dataGridView3.Rows[i].Cells[j].Value != null && Dummy.Text == dataGridView3.Rows[i].Cells[j].Value.ToString())
                            {
                                Class1.f = 1;
                                MessageBox.Show("Multiple inputting of the same product is not allowed.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                o = 1;
                                jo = true;
                                break;
                            }
                            else if (dataGridView3.Rows[i].Cells[j].Value != null && jo != true && Dummy.Text != dataGridView3.Rows[i].Cells[j].Value.ToString())
                                Class1.f = 0;
                        }
                    }
                    if (Class1.f != 1)
                    {
                        Quan m = new Quan() { MyMainForm = this };
                        m.ShowDialog();
                    }
                }
            }
            else
                MessageBox.Show("An ingredient of "+Dummy.Text.ToString()+" has expired. Please restock as soon as possible. \n\nGo to the supplies module to change the expiry date once new ingredients are bought", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }



        private void button2_Click(object sender, EventArgs e)
        {
            if (TotalPrice.Text.ToString().Equals("0") || TotalPrice.Text.ToString().Equals("0.00"))
                MessageBox.Show("Select a product before paying.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                PayForm pf = new PayForm(TotalPrice.Text) { MyMainForm = this };
                pf.ShowDialog();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                int selectedIndex = dataGridView3.CurrentCell.RowIndex;
                if (selectedIndex > -1)
                {
                    int selectedrowindex = dataGridView3.SelectedCells[0].RowIndex;
                    DataGridViewRow selectedRow = dataGridView3.Rows[selectedrowindex];
                    string a = Convert.ToString(selectedRow.Cells["Price"].Value);
                    SqlCommand cho = con.CreateCommand();
                    cho.CommandType = CommandType.Text;
                    cho.CommandText = "select * from PDF where NAME='" + Convert.ToString(selectedRow.Cells["NameS"].Value) + "'";
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
                    for (int h = 0; h <= 6; h++)
                    {
                        if (h == 0)
                        {
                            SqlCommand comm = con.CreateCommand();
                            comm.CommandType = CommandType.Text;
                            comm.CommandText = "UPDATE Supplies SET [Quantity/Weight] = [Quantity/Weight] + '" + selectedRow.Cells["Quantity"].Value + "' WHERE  Name='" + a + "'";
                            comm.ExecuteNonQuery();
                        }

                        else if (h == 1)
                        {
                            SqlCommand c1 = con.CreateCommand();
                            c1.CommandType = CommandType.Text;
                            c1.CommandText = "UPDATE Supplies SET [Quantity/Weight] = [Quantity/Weight] + '" + selectedRow.Cells["Quantity"].Value + "' WHERE  Name='" + b + "'";
                            c1.ExecuteNonQuery();
                        }

                        else if (h == 2)
                        {
                            SqlCommand c2 = con.CreateCommand();
                            c2.CommandType = CommandType.Text;
                            c2.CommandText = "UPDATE Supplies SET [Quantity/Weight] = [Quantity/Weight] + '" + selectedRow.Cells["Quantity"].Value + "' WHERE  Name='" + c + "'";
                            c2.ExecuteNonQuery();
                        }

                        else if (h == 3)
                        {
                            SqlCommand c3 = con.CreateCommand();
                            c3.CommandType = CommandType.Text;
                            c3.CommandText = "UPDATE Supplies SET [Quantity/Weight] = [Quantity/Weight] + '" + selectedRow.Cells["Quantity"].Value + "' WHERE  Name='" + d + "'";
                            c3.ExecuteNonQuery();
                        }

                        else if (h == 4)
                        {
                            SqlCommand c4 = con.CreateCommand();
                            c4.CommandType = CommandType.Text;
                            c4.CommandText = "UPDATE Supplies SET [Quantity/Weight] = [Quantity/Weight] + '" + selectedRow.Cells["Quantity"].Value + "' WHERE  Name='" + ez + "'";
                            c4.ExecuteNonQuery();
                        }

                        else if (h == 5)
                        {
                            SqlCommand c5 = con.CreateCommand();
                            c5.CommandType = CommandType.Text;
                            c5.CommandText = "UPDATE Supplies SET [Quantity/Weight] = [Quantity/Weight] + '" + selectedRow.Cells["Quantity"].Value + "' WHERE  Name='" + f + "'";
                            c5.ExecuteNonQuery();
                        }

                        else
                        {
                            SqlCommand c6 = con.CreateCommand();
                            c6.CommandType = CommandType.Text;
                            c6.CommandText = "UPDATE Supplies SET [Quantity/Weight] = [Quantity/Weight] + '" + selectedRow.Cells["Quantity"].Value + "' WHERE  Name='" + gz + "'";
                            c6.ExecuteNonQuery();
                        }
                    }
                    /*
                    SqlCommand comm = con.CreateCommand();
                    comm.CommandType = CommandType.Text;
                    comm.CommandText = "UPDATE Supplies SET [Quantity/Weight] = [Quantity/Weight] + '" + selectedRow.Cells["Quantity"].Value + "' WHERE  MainProduct='" + Convert.ToString(selectedRow.Cells["NameS"].Value) + "'";
                    comm.ExecuteNonQuery();
                    */
                    dataGridView3.Rows.RemoveAt(selectedIndex);
                    dataGridView3.Refresh(); // if needed
                    Class1.f = 0;
                    Class1.g = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Only select rows with values in them.", "Warining", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Register r = new Register();
            r.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure you want to log-out?", "Log-out", MessageBoxButtons.YesNo,
MessageBoxIcon.Information);
            if (dr == DialogResult.Yes)
            {
                Login f = new Login();
                f.Show();
                this.Hide();
                Class1.isAdmin = false;
            }
            else
            {
                MessageBox.Show("Log-out cancelled.", "Log-out", MessageBoxButtons.OK,
MessageBoxIcon.Information);
            }
        }

        private void tabControl1_TabIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabControl1.TabPages["Drinks"])//your specific tabname
            {
                string d = "Drinks";
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from PDF where CATEGORY='" + d + "'";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                dataGridView2.DataSource = dt;
            }
        }



        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabControl1.TabPages["Drinks"])//your specific tabname
            {
                string fd = "Drinks";
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from PDF where Category='" + fd + "'";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                dataGridView2.DataSource = dt;
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                id = Convert.ToInt32(dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString());
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from PDF where ID=" + id + "";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    Dummy.Text = dr["NAME"].ToString();
                    Dumbo.Text = dr["PRICE"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error. Only select cells with inputted values.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            SqlCommand cho = con.CreateCommand();
            cho.CommandType = CommandType.Text;
            cho.CommandText = "select * from PDF where NAME='" + Dummy.Text + "'";
            cho.ExecuteNonQuery();
            DataTable dos = new DataTable();
            SqlDataAdapter dpa = new SqlDataAdapter(cho);
            dpa.Fill(dos);
            foreach (DataRow dd in dos.Rows)
            {
                a = dd["ing1"].ToString();
                b = dd["ing2"].ToString();
                c = dd["ing3"].ToString();
                d = dd["ing4"].ToString();
                ez = dd["ing5"].ToString();
                f = dd["ing6"].ToString();
                gz = dd["ing7"].ToString();
            }
            SqlCommand s = con.CreateCommand();
            s.CommandType = CommandType.Text;
            s.CommandText = "select Expiry from Supplies where Name='" + a + "' OR  Name = '" + b + "' OR  Name = '" + c + "' OR  Name = '" + d + "' OR  Name = '" + ez + "' OR  Name = '" + f + "' OR  Name = '" + gz + "' ";
            s.ExecuteNonQuery();
            DataTable dz = new DataTable();
            SqlDataAdapter db = new SqlDataAdapter(s);
            db.Fill(dz);
            int res = 0;
            int jug = 0;
            foreach (DataRow dd in dz.Rows)
            {
                res = DateTime.Compare(DateTime.Now, Convert.ToDateTime(dd["Expiry"].ToString()));
                if (res > 0)
                {
                    jug++;
                }

            }
            if (jug == 0)
            {
                int o = 0;
                double x = 0;
                SqlCommand second = con.CreateCommand();
                second.CommandType = CommandType.Text;
                second.CommandText = "SELECT * FROM Supplies WHERE Name='" + a + "' OR  Name = '" + b + "' OR  Name = '" + c + "' OR  Name = '" + d + "' OR  Name = '" + ez + "' OR  Name = '" + f + "' OR  Name = '" + gz + "' ";
                second.ExecuteNonQuery();
                DataTable dq = new DataTable();
                SqlDataAdapter dg = new SqlDataAdapter(second);
                dg.Fill(dq);
                foreach (DataRow dd in dq.Rows)
                {
                    //label1.Text = dd["MainProduct"].ToString();
                    x = float.Parse(dd["Quantity/Weight"].ToString(), CultureInfo.InvariantCulture.NumberFormat);
                }
                if (x < 1)
                {
                    MessageBox.Show("Supplies are insufficient. Please restock.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    bool jojo1 = false;
                    for (int i = 0; i < dataGridView3.Rows.Count; i++)
                    {
                        for (int j = 0; j < dataGridView3.Columns.Count; j++)
                        {
                            if (dataGridView3.Rows[i].Cells[j].Value != null && Dummy.Text == dataGridView3.Rows[i].Cells[j].Value.ToString())
                            {
                                Class1.f = 1;
                                MessageBox.Show("Multiple inputting of the same product is not allowed.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                jojo1 = true;
                                o = 1;
                                break;
                            }
                            else if (dataGridView3.Rows[i].Cells[j].Value != null && jojo1 != true && Dummy.Text != dataGridView3.Rows[i].Cells[j].Value.ToString())
                                Class1.f = 0;
                        }
                    }
                    if (Class1.f != 1)
                    {
                        Quan m = new Quan() { MyMainForm = this };
                        m.ShowDialog();
                    }
                }
            }
            else
                MessageBox.Show("An ingredient of " + Dummy.Text.ToString() + " has expired. Please restock as soon as possible. \n\nGo to the supplies module to change the expiry date once new ingredients are bought", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.Now;
            this.dateTimePicker1.Value = dateTime;
            this.dateTimePicker2.Value = dateTime;
            this.label5.Text = dateTime.ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Sup su = new Sup();
            su.ShowDialog();
        }

        private void dataGridView3_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            double sum = 0.00;
                foreach (DataGridViewRow row in dataGridView3.Rows)
                {
                    sum += Convert.ToDouble(row.Cells["Price"].Value);
                }
                TotalPrice.Text = "" + sum;
                if (TotalPrice.Text.Equals("0.00") || TotalPrice.Text.Equals("0"))
                {
                    this.ControlBox = true;
                    button6.Enabled = true;
                }
                else
                {
                    this.ControlBox = false;
                    button6.Enabled = false;
                }
        }

        private void dataGridView3_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            double sum = 0.00;
            foreach (DataGridViewRow row in dataGridView3.Rows)
            {
                sum += Convert.ToDouble(row.Cells["Price"].Value);
            }
            TotalPrice.Text = "" + sum;
            if (TotalPrice.Text.Equals("0.00") || TotalPrice.Text.Equals("0"))
            {
                this.ControlBox = true;
                button6.Enabled = true;
            }
            else
            {
                this.ControlBox = false;
                button6.Enabled = false;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2();
            f.ShowDialog();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker1.Value = DateTime.Now;
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker2.Value = DateTime.Now;
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Supplies";
            cmd.ExecuteNonQuery();
            int wew = 0;
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            StringBuilder message = new StringBuilder();
            foreach (DataRow dr in dt.Rows)
            {
                message.AppendLine(dr[1].ToString() + " = " + dr[3].ToString());
                wew++;
            }
            MessageBox.Show("Please resupply when stocks run low\n\n" + message.ToString(), "* Inventory Quantity Notification *");

            SqlCommand cmd5 = con.CreateCommand();
            cmd5.CommandType = CommandType.Text;
            cmd5.CommandText = "select * from Supplies where Expiry<='" + DateTime.Now.AddDays(7)+"'";
            cmd5.ExecuteNonQuery();
            int jej = 0;
            DataTable data = new DataTable();
            SqlDataAdapter daga = new SqlDataAdapter(cmd5);
            daga.Fill(data);
            StringBuilder mensaje = new StringBuilder();
            foreach (DataRow dr in data.Rows)
            {
                    mensaje.AppendLine(dr[1].ToString() + " = " + dr[5].ToString() + "\n");
                jej++;
            }
            if (mensaje.ToString().Equals(""))
            {
                MessageBox.Show("No items are set to expire within 7 days.", "* Inventory Expiry Notification *");
            }
            else
                MessageBox.Show("The following are the items set to expire within 7 days:\n\n" + mensaje.ToString(), "* Inventory Expiry Notification *");
        }
    }
}
