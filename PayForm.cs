using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Diagnostics;
using System.Data.SqlClient;

namespace WindowsFormsApplication1
{
    public partial class PayForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\fape\Desktop\EMEAL\EMEAL\bin\Debug\DB.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");
        public Form1 MyMainForm { get; set; }
        public PayForm(string x)
        {
            InitializeComponent();
            label3.Text = x;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            decimal total = 0;
            try
            {
                total = decimal.Parse(textBox1.Text) - decimal.Parse(label3.Text);
            }

            catch (Exception ex)
            {
                MessageBox.Show("An error has occured, please enter a valid amount", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (total >= 0)
            {
                MessageBox.Show("Transaction successful. \n\nChange is " + String.Format("{0:c}", -total), "Transaction Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);
                PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream("Test.pdf", FileMode.Create));
                int wowo = 0;
                for (int i = 0; i < MyMainForm.dataGridView3.RowCount; i++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        if (MyMainForm.dataGridView3[k, i].Value != null)
                        {
                           wowo++; 
                        }
                    }
                }
                int wowo2 = wowo / 3;
                if (wowo2 < 7)
                {
                    var pgSize = new iTextSharp.text.Rectangle(600, 450);
                    doc.SetPageSize(pgSize);
                }
                else if (wowo2 > 6 && wowo2 < 11)
                {
                    var pgSize = new iTextSharp.text.Rectangle(600, 520);
                    doc.SetPageSize(pgSize);
                }
                else if (wowo2 > 10 && wowo2 < 13)
                {
                    var pgSize = new iTextSharp.text.Rectangle(600, 550);
                    doc.SetPageSize(pgSize);
                }
                else if (wowo2 > 12 && wowo2 < 17)
                {
                    var pgSize = new iTextSharp.text.Rectangle(600, 610);
                    doc.SetPageSize(pgSize);
                }
                else
                {
                    var pgSize = new iTextSharp.text.Rectangle(600, 675);
                    doc.SetPageSize(pgSize);
                }
                doc.Open();
                Paragraph paragraph = new Paragraph("                                                                    E-MEAL FOODS INC.\n\n                                                 Barangay San Agustin III, Dasmariñas, Cavite\n\n                                                                     TEMPORARY RECEIPT\n\n                                                                                                                   Date: " + MyMainForm.label5.Text.ToString() + "\n\n                  Product(s) Ordered\n\n");
                doc.Add(paragraph);
                PdfPTable table = new PdfPTable(3);
                PdfPCell cell = new PdfPCell(new Phrase("Transaction Complete"));
                cell.Colspan = 3;
                cell.HorizontalAlignment = 1;
                string[] res = new string[100];

                for (int j = 0; j < 3; j++)
                {
                    table.AddCell(new Phrase(MyMainForm.dataGridView3.Columns[j].HeaderText));
                }
                table.HeaderRows = 1;
                for (int i = 0; i < MyMainForm.dataGridView3.RowCount; i++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        if (MyMainForm.dataGridView3[k, i].Value != null)
                        {
                            table.AddCell(new Phrase(MyMainForm.dataGridView3[k, i].Value.ToString()));
                        }
                    }
                }
                doc.Add(table);
                Paragraph go = new Paragraph("                  Cashier name: "+ Class1.user);
                doc.Add(go);
                Paragraph popo = new Paragraph("                                                                                                Total: Php " + MyMainForm.TotalPrice .Text.ToString()+ "");
                doc.Add(popo);
                Paragraph popo1 = new Paragraph("                                                                            Amount Received: Php " + textBox1.Text.ToString() + "\n");
                doc.Add(popo1);
                Paragraph popo2 = new Paragraph("                                                                                            Change: Php " + total + "");
                doc.Add(popo2);
                doc.Close();
                System.Diagnostics.Process.Start("C:\\Users\\fape\\Desktop\\REAL\\REAL\\bin\\Debug\\Test.pdf");
                FileStream fStream = File.OpenRead("C:\\Users\\fape\\Desktop\\REAL\\REAL\\bin\\Debug\\Test.pdf");
                byte[] contents = new byte[fStream.Length];
                fStream.Read(contents, 0, (int)fStream.Length);
                fStream.Close();
                DateTime f = DateTime.Now;
                string g = DateTime.Now.ToString("MMMM");
                using (SqlCommand second = new SqlCommand("insert into [TRANSACTION] " + "(DATE, TOTAL, PRODUCT, CashierName, Month, Year)values('" + MyMainForm.label5.Text.ToString() + "','" + label3.Text.ToString() + "', @data, '" + Class1.user + "', '" + g + "', '" + f.Year.ToString() + "')", con))
                {
                    second.Parameters.Add("@data", contents);
                    second.ExecuteNonQuery();
                }
    
                MyMainForm.dataGridView3.Rows.Clear();
                MyMainForm.dataGridView3.Refresh();
                MyMainForm.TotalPrice.Text = "0.00";
                this.Hide();
            }
            else
                MessageBox.Show("Payment is insufficient. Please input an amount more than or equal to the total amount required");
        }

        static string ConvertStringArrayToStringJoin(string[] res)
        {
            //
            // Use string Join to concatenate the string elements.
            //
            string resz = string.Join(" ", res);
            return resz;
        }

        private void PayForm_Load(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
        }

    }
}
