using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sales_and_Inventory_System__Gadgets_Shop_
{
    public partial class outofstock : Form
    {

        OleDbDataReader rdr = null;
        OleDbConnection con = null;
        OleDbCommand cmd = null;
        String cs = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\SIS_DB.accdb;";
        public outofstock()
        {
            InitializeComponent();
        }

        private void outofstock_Load(object sender, EventArgs e)
        {
            GetData();
        }
         public void GetData()
        {
            try
            {
                con = new OleDbConnection(cs);
                con.Open();
                String sql = "SELECT StockID,Config.ConfigID,ProductName,Features,Price,Quantity,Totalprice,Stockdate,size1,color,barcode from Stock,Config where Stock.ConfigID=Config.ConfigID and Quantity <= 0 order by ProductName";
                cmd = new OleDbCommand(sql, con);
                rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dataGridView1.Rows.Clear();
                while (rdr.Read() == true)
                {
                    dataGridView1.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3], rdr[4], rdr[5], rdr[6], rdr[7], rdr[8], rdr[9], rdr[10]);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

         private void txtProductname_TextChanged(object sender, EventArgs e)
         {
             try
             {
                 con = new OleDbConnection(cs);
                 con.Open();
                 String sql = "SELECT StockID,Config.ConfigID,ProductName,Features,Price,Quantity,Totalprice,Stockdate,size1,color,barcode from Stock,Config where Stock.ConfigID=Config.ConfigID and productname like '" + txtProductname.Text + "%' and Quantity <= 0 order by ProductName";
                 cmd = new OleDbCommand(sql, con);
                 rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                 dataGridView1.Rows.Clear();
                 while (rdr.Read() == true)
                 {
                     dataGridView1.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3], rdr[4], rdr[5], rdr[6], rdr[7], rdr[8], rdr[9], rdr[10]);
                 }
                 con.Close();
             }
             catch (Exception ex)
             {
                 MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
             }
         }

         private void textBox1_TextChanged(object sender, EventArgs e)
         {
             try
             {
                 con = new OleDbConnection(cs);
                 con.Open();
                 String sql = "SELECT StockID,Config.ConfigID,ProductName,Features,Price,Quantity,Totalprice,Stockdate,size1,color,barcode from Stock,Config where Stock.ConfigID=Config.ConfigID and barcode like '" + textBox1.Text + "%' and Quantity <= 0 order by ProductName";
                 cmd = new OleDbCommand(sql, con);
                 rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                 dataGridView1.Rows.Clear();
                 while (rdr.Read() == true)
                 {
                     dataGridView1.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3], rdr[4], rdr[5], rdr[6], rdr[7], rdr[8], rdr[9], rdr[10]);
                 }
                 con.Close();
             }
             catch (Exception ex)
             {
                 MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
             }

         }
    }
}
