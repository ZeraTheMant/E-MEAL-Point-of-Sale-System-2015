using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Web;
using System.IO;
using System.Diagnostics;


namespace WindowsFormsApplication1
{
    public partial class Form2 : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\fape\Desktop\EMEAL\EMEAL\bin\Debug\DB.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\fape\Desktop\EMEAL\EMEAL\bin\Debug\DB.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True"))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("select PRODUCT from [TRANSACTION]  where id='" + idTextBox1.Text + "' ", cn);
                    byte[] buffer = (byte[])cmd.ExecuteScalar();
                    cn.Close();
                    FileStream fs = new FileStream("C:\\Users\\fape\\Desktop\\REAL\\REAL\\bin\\Debug\\Test.pdf", FileMode.Create);
                    fs.Write(buffer, 0, buffer.Length);
                    fs.Close();
                    System.Diagnostics.Process.Start("C:\\Users\\fape\\Desktop\\REAL\\REAL\\bin\\Debug\\Test.pdf");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error. Only select cells with inputted values.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dBDataSet.TRANSACTION' table. You can move, or remove it, as needed.
            this.tRANSACTIONTableAdapter.Fill(this.dBDataSet.TRANSACTION);
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
        }

        private void tRANSACTIONBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.tRANSACTIONBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dBDataSet);
            MessageBox.Show("Modifications saved.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1 || comboBox2.SelectedIndex != -1)
            {
                try
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "select ID, DATE, TOTAL, CASHIERNAME from [TRANSACTION] where Month='" + comboBox1.SelectedItem.ToString() + "' AND Year='" + comboBox2.SelectedItem.ToString() + "'";
                    cmd.ExecuteNonQuery();
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    tRANSACTIONDataGridView.DataSource = dt;
                    MessageBox.Show("The table is now filtered.", "Filter table", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception)
                {
                    MessageBox.Show("Please select both a month and a year.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
                MessageBox.Show("Please select both a month and a year.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select id, DATE, TOTAL, CashierName from [TRANSACTION]";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            tRANSACTIONDataGridView.DataSource = dt;
            MessageBox.Show("All records selected.", "Select All Records", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
        }

        private void tRANSACTIONDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int Row = tRANSACTIONDataGridView.CurrentRow.Index;
            idTextBox1.Text = tRANSACTIONDataGridView[0, Row].Value.ToString();
        }
    }
}

