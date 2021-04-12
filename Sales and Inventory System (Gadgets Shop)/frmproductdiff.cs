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
    public partial class frmproductdiff : Form
    {
        DataTable dTable;
        OleDbConnection con = null;
        OleDbDataAdapter adp;
        DataSet ds;
        OleDbCommand cmd = null;
        DataTable dt = new DataTable();
        OleDbDataReader rdr;
        String cs = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\SIS_DB.accdb;";
        public frmproductdiff()
        {
            InitializeComponent();
           
        }

        private void cmbCustomerName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GroupBox4.Visible = true;
                con = new OleDbConnection(cs);
                con.Open();
                cmd = new OleDbCommand("SELECT (ProductName) as [Product Name],(ProductSold.Quantity) as [Product Quantity],(APrice) as [Actual Price/pu],(Config.Price) as [Sales Price/pu],(APrice*ProductSold.Quantity) as [Actual Price/Total],(Config.Price*ProductSold.Quantity) as [Sales Price/Total],((Config.Price*ProductSold.Quantity)-(APrice*ProductSold.Quantity)) as [Profit/Loss],(InvoiceDate) as [Date Sold] from Config,ProductSold,Sales where ProductSold.ConfigID=Config.ConfigID and ProductName='" + cmbCustomerName.Text + "' and ProductSold.InvoiceNo=Sales.InvoiceNo order by ProductName,InvoiceDate", con);
//                cmd = new OleDbCommand("SELECT (invoiceNo) as [Invoice No],(InvoiceDate) as [Invoice Date],(Sales.CustomerID) as [Customer ID],(CustomerName) as [Customer Name],(GrandTotal) as [Grand Total],(TotalPayment) as [Total Payment],(PaymentDue) as [Payment Due] from Sales,Customer where Sales.CustomerID=Customer.CustomerID and Customername='" + cmbCustomerName.Text + "' order by CustomerName,InvoiceDate", con);
                OleDbDataAdapter myDA = new OleDbDataAdapter(cmd);
                DataSet myDataSet = new DataSet();
                myDA.Fill(myDataSet, "Config");
                myDA.Fill(myDataSet, "ProductSold");
                myDA.Fill(myDataSet, "Sales");
                DataGridView3.DataSource = myDataSet.Tables["Config"].DefaultView;
                DataGridView3.DataSource = myDataSet.Tables["ProductSold"].DefaultView;
                DataGridView3.DataSource = myDataSet.Tables["Sales"].DefaultView;
                Int64 sum = 0;
                Int64 sum1 = 0;
                Int64 sum2 = 0;

                foreach (DataGridViewRow r in this.DataGridView3.Rows)
                {
                    Int64 i = Convert.ToInt64(r.Cells[4].Value);
                    Int64 j = Convert.ToInt64(r.Cells[5].Value);
                    Int64 k = Convert.ToInt64(r.Cells[6].Value);
                    sum = sum + i;
                    sum1 = sum1 + j;
                    sum2 = sum2 + k;
                }
                TextBox6.Text = sum.ToString();
                TextBox5.Text = sum1.ToString();
                TextBox4.Text = sum2.ToString();
                
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmproductdiff_Load(object sender, EventArgs e)
        {
            FillCombo();
        }
        public void FillCombo()
        {

            try
            {
                con = new OleDbConnection(cs);
                con.Open();
                adp = new OleDbDataAdapter();
                adp.SelectCommand = new OleDbCommand("SELECT distinct ProductName FROM Config,ProductSold where Config.ConfigID=ProductSold.ConfigID", con);
                ds = new DataSet("ds");
                adp.Fill(ds);
                dTable = ds.Tables[0];
                cmbCustomerName.Items.Clear();
                foreach (DataRow drow in dTable.Rows)
                {
                    cmbCustomerName.Items.Add(drow[0].ToString());

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

    }
}
