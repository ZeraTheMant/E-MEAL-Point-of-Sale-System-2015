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
    public partial class Form4 : Form
    {
        int id;
        SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\fape\Desktop\EMEAL\EMEAL\bin\Debug\DB.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");
        public Form1 MyMainForm { get; set; }
        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dBDataSet.PDF' table. You can move, or remove it, as needed.
            this.pDFTableAdapter.Fill(this.dBDataSet.PDF);
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();

            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select Name from Supplies";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                ing1ComboBox.Items.Add(dr["Name"].ToString());
                ing2ComboBox.Items.Add(dr["Name"].ToString());
                ing3ComboBox.Items.Add(dr["Name"].ToString());
                ing4ComboBox.Items.Add(dr["Name"].ToString());
                ing5ComboBox.Items.Add(dr["Name"].ToString());
                ing6ComboBox.Items.Add(dr["Name"].ToString());
                ing7ComboBox.Items.Add(dr["Name"].ToString());
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
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


        private void pDFDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                id = Convert.ToInt32(pDFDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString());
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from [TABLE] where ID=" + id + "";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    cATEGORYComboBox.SelectedItem = dr["Category"].ToString();
                    ing1ComboBox.SelectedItem = dr["ing1"].ToString();
                    ing2ComboBox.SelectedItem = dr["ing2"].ToString();
                    ing3ComboBox.SelectedItem = dr["ing3"].ToString();
                    ing4ComboBox.SelectedItem = dr["ing4"].ToString();
                    ing5ComboBox.SelectedItem = dr["ing5"].ToString();
                    ing6ComboBox.SelectedItem = dr["ing6"].ToString();
                    ing7ComboBox.SelectedItem = dr["ing7"].ToString();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void nAMETextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (nAMETextBox.Text.Length == 0 && e.KeyChar == ' ')
            {
                e.Handled = true;
                MessageBox.Show("Spaces are not allowed at the start.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
                e.Handled = false;
        }

        private void pRICETextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (pRICETextBox.Text.Length == 0 && e.KeyChar == ' ')
            {
                e.Handled = true;
                MessageBox.Show("Spaces are not allowed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
                e.Handled = false;
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
                    cmd.CommandText = "select * from PDF where CATEGORY='" + fd + "'";
                    cmd.ExecuteNonQuery();
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    MyMainForm.DGV.DataSource = dt;
    
                    string zz = "Drinks";
                    SqlCommand cmd1 = con.CreateCommand();
                    cmd1.CommandType = CommandType.Text;
                    cmd1.CommandText = "select * from PDF where CATEGORY='" + zz + "'";
                    cmd1.ExecuteNonQuery();
                    DataTable dt1 = new DataTable();
                    SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                    da1.Fill(dt1);
                    MyMainForm.dataGridView2.DataSource = dt1;
                    this.Hide();
  
                 
            }
            else
            {
                MessageBox.Show("Return cancelled.", "Cancelled", MessageBoxButtons.OK,
MessageBoxIcon.Information);
            }
        }

        private void pDFBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            try
            {
                if ((nAMETextBox.Text.Trim().Length != 0) &&
    (pRICETextBox.Text.Trim().Length != 0) &&
    (cATEGORYComboBox.SelectedIndex != -1) &&
    (iMGPictureBox.Image != null) &&
    (ing1ComboBox.SelectedIndex != -1 || ing2ComboBox.SelectedIndex != -1 || ing3ComboBox.SelectedIndex != -1
    || ing4ComboBox.SelectedIndex != -1 || ing5ComboBox.SelectedIndex != -1 || ing6ComboBox.SelectedIndex != -1 || ing7ComboBox.SelectedIndex != -1))
                {
                    this.Validate();
                    this.pDFBindingSource.EndEdit();
                    this.tableAdapterManager.UpdateAll(this.dBDataSet);
                    MessageBox.Show("Modifications saved.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Please fill in all the fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception)
            {

            }

        }

        private void bindingNavigatorAddNewItem_Click_1(object sender, EventArgs e)
        {
            cATEGORYComboBox.SelectedIndex = -1;
            ing1ComboBox.SelectedIndex = -1;
            ing2ComboBox.SelectedIndex = -1;
            ing3ComboBox.SelectedIndex = -1;
            ing4ComboBox.SelectedIndex = -1;
            ing5ComboBox.SelectedIndex = -1;
            ing6ComboBox.SelectedIndex = -1;
            ing7ComboBox.SelectedIndex = -1;
        }

        private void nAMETextBox_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (nAMETextBox.Text.Length == 0 && e.KeyChar == ' ')
            {
                e.Handled = true;
                MessageBox.Show("Spaces are not allowed in the beginning.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
                e.Handled = false;
        }

        private void pDFDataGridView_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                id = Convert.ToInt32(pDFDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString());
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from PDF where ID=" + id + "";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    cATEGORYComboBox.SelectedItem = dr["Category"].ToString();
                    ing1ComboBox.SelectedItem = dr["ing1"].ToString();
                    ing2ComboBox.SelectedItem = dr["ing2"].ToString();
                    ing3ComboBox.SelectedItem = dr["ing3"].ToString();
                    ing4ComboBox.SelectedItem = dr["ing4"].ToString();
                    ing5ComboBox.SelectedItem = dr["ing5"].ToString();
                    ing6ComboBox.SelectedItem = dr["ing6"].ToString();
                    ing7ComboBox.SelectedItem = dr["ing7"].ToString();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void TestUniqueSelection(object sender, System.EventArgs e)
        {
             var controls = new List<System.Windows.Forms.ComboBox>();
             controls.Add(ing1ComboBox); // <-- Add all of your controls here.
             controls.Add(ing2ComboBox);
             controls.Add(ing3ComboBox);
             controls.Add(ing4ComboBox);
             controls.Add(ing5ComboBox);
             controls.Add(ing6ComboBox);
             controls.Add(ing7ComboBox);

             ComboBox changedBox = (ComboBox) sender;

             if (controls
                .Where(a => a != changedBox && a.SelectedItem == changedBox.SelectedItem)
                .Count() > 0) changedBox.SelectedIndex = -1;
        } 

        private void ing1ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            TestUniqueSelection(sender, e);
        }

        private void ing2ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            TestUniqueSelection(sender, e);
        }

        private void ing3ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            TestUniqueSelection(sender, e);
        }

        private void ing4ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            TestUniqueSelection(sender, e);
        }

        private void ing5ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            TestUniqueSelection(sender, e);
        }

        private void ing6ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            TestUniqueSelection(sender, e);
        }

        private void ing7ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            TestUniqueSelection(sender, e);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString().Equals("Food") || comboBox1.SelectedItem.ToString().Equals("Drinks"))
                this.pDFTableAdapter.FillByCat(this.dBDataSet.PDF, comboBox1.SelectedItem.ToString());
            else
                this.pDFTableAdapter.Fill(this.dBDataSet.PDF);
        }
    }
}
