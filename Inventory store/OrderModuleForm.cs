using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace Inventory_store
{
    public partial class OrderModuleForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\Documents\Store.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        int qty = 0;

        public OrderModuleForm()
        {
            InitializeComponent();
            LoadCustomer();
            LoadProduct();
        }
        public void LoadCustomer()
        {
            int i = 0;
            dgvCustomers.Rows.Clear();
            cm = new SqlCommand("SELECT * FROM tbCustomer WHERE CONCAT(cID,cname) LIKE '%" +searchCust.Text + "%'", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvCustomers.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString());
            }
            dr.Close();
            con.Close();
        }
        public void LoadProduct()
        {
            int i = 0;
            dgvProduct.Rows.Clear();
            cm = new SqlCommand("SELECT * FROM tbProduct WHERE CONCAT(pID, pName, pQty, pPrice,  pCategory) LIKE '%" + searchProd.Text + "%'", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvProduct.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString());
            }
            dr.Close();
            con.Close();
        }

        private void UserModuleForm_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void searchCust_TextChanged(object sender, EventArgs e)
        {
            LoadCustomer();
        }

        private void searchProd_TextChanged(object sender, EventArgs e)
        {
            LoadProduct();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void numericpqty_ValueChanged(object sender, EventArgs e)
        {
            GETQty();
            if(Convert.ToInt16(numericpqty.Value)>qty)
            {
                MessageBox.Show("Не хватает товара на сккладе","Опасность!!!",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                numericpqty.Value = numericpqty.Value-1;
                return;            
            }
            if (Convert.ToInt16(numericpqty.Value) > 0)
            {
                int total = Convert.ToInt16(txtpprice.Text) * Convert.ToInt16(numericpqty.Value);
                txtptotal.Text = total.ToString();
            }
        }

        private void dgvCustomers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtCid.Text = dgvCustomers.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtcName.Text = dgvCustomers.Rows[e.RowIndex].Cells[2].Value.ToString();
        }

        private void dgvProduct_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtpid.Text = dgvProduct.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtpname.Text = dgvProduct.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtpprice.Text = dgvProduct.Rows[e.RowIndex].Cells[4].Value.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Вы уверены, что хотите сохранить изменения", "Подтверждение сохранения", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cm = new SqlCommand("INSERT INTO tbOrder (odate,pid,cid,qty, price,total)VALUES(@odate,@pid,@cid,@qty,@price,@total)", con);
                    cm.Parameters.AddWithValue("@odate",dateTimePicker1.Value);
                    cm.Parameters.AddWithValue("@pid", Convert.ToInt16(txtpid.Text));
                    cm.Parameters.AddWithValue("@cid", Convert.ToInt16(txtCid.Text));
                    cm.Parameters.AddWithValue("@qty", Convert.ToInt16(numericpqty.Value));
                    cm.Parameters.AddWithValue("@price", Convert.ToInt16(txtpprice.Text));
                    cm.Parameters.AddWithValue("@total", Convert.ToInt16(txtptotal.Text));
                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Таблица успешно сохраненна");

                    cm = new SqlCommand("UPDATE tbProduct SET pQty =(pQty-@pQty) WHERE pID LIKE '" + txtpid.Text + "'", con);

                    cm.Parameters.AddWithValue("@pQty", Convert.ToInt16(numericpqty.Value));
                    
                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    Clear();
                    LoadProduct();
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
        public void Clear()
        {
            txtCid.Clear();
            txtcName.Clear();
            txtpname.Clear();
            txtpprice.Clear();
            txtpid.Clear();
            txtptotal.Clear();
            dateTimePicker1.Value=DateTime.Now;
            numericpqty.Value = 1;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

        }
        public void GETQty()
        {
            cm = new SqlCommand("SELECT pQty FROM tbProduct WHERE pID ='" + txtpid.Text + "'", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                qty = Convert.ToInt32(dr[0].ToString());
            }
            dr.Close();
            con.Close();
        }
    }
}
