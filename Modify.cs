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
    public partial class Modify : Form
    {
        int id;
        SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\fape\Desktop\EMEAL\EMEAL\bin\Debug\DB.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");
        public Form1 MyMainForm { get; set; }
        public Modify()
        {
            InitializeComponent();
        }

        private void tABLEBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            try
            {
                if ((nameTextBox.Text.Trim().Length != 0) && 
                    (priceTextBox.Text.Trim().Length != 0) && 
                    (categoryComboBox.SelectedIndex != -1) && 
                    (iMGPictureBox.Image != null))
                {
                   this.Validate();
                   this.tABLEBindingSource.EndEdit();
                   this.tableAdapterManager.UpdateAll(this.dBDataSet);
                   MessageBox.Show("Modifications saved.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Please fill in all the fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (System.Exception ex)
            {

            }
        }

        private void Modify_Load(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            // TODO: This line of code loads data into the 'dBDataSet.TABLE' table. You can move, or remove it, as needed.
            this.tABLETableAdapter.Fill(this.dBDataSet.TABLE);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = @"C:\";
            openFileDialog1.Title = "Browse Images";
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.CheckPathExists = true;
            openFileDialog1.Filter = "Image Files(*.png; *.jpg; *.jpeg; *.gif; *.bmp)|*.png; *.jpg; *.jpeg; *.gif; *.bmp";
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.ReadOnlyChecked = true;
            openFileDialog1.ShowReadOnly = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                iMGPictureBox.Image = new Bitmap(openFileDialog1.FileName);

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure you want to go back? All unsaved data will be lost", "Return to Main Page", MessageBoxButtons.YesNo,
MessageBoxIcon.Information);
            if (dr == DialogResult.Yes)
            {
                string fd = "Food";
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from [TABLE] where Category='" + fd + "'";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                MyMainForm.DGV.DataSource = dt;
                this.Hide();
            }
            else
            {
                MessageBox.Show("Return cancelled.", "Cancelled", MessageBoxButtons.OK,
MessageBoxIcon.Information);
            }

        }

        private void tABLEDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                id = Convert.ToInt32(tABLEDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString());
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from [TABLE] where ID=" + id + "";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    categoryComboBox.SelectedItem = dr["Category"].ToString();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            categoryComboBox.SelectedIndex = -1;
        }

  




    }
}
