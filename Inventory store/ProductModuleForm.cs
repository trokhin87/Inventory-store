using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Inventory_store
{
    
    public partial class ProductModuleForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\Documents\Store.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        public ProductModuleForm()
        {
            InitializeComponent();
            LoadCategory();
        }
        public void LoadCategory()
        {
            comboQty.Items.Clear();
            cm = new SqlCommand("SELECT CatName From tbCategory", con);
            con.Open();
            dr= cm.ExecuteReader();
            while(dr.Read())
            {
                comboQty.Items.Add(dr[0].ToString());
            }
            dr.Close();
            con.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Вы уверены, что хотите сохранить изменения", "Подтверждение сохранения", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cm = new SqlCommand("INSERT INTO tbProduct (pName,pQty,pPrice,pDescription,pCategory)VALUES(@pName,@pQty,@pPrice,@pDescription,@pCategory)", con);
                    cm.Parameters.AddWithValue("@pName", txtName.Text);
                    cm.Parameters.AddWithValue("@pQty", Convert.ToInt16(txtQuantity.Text));
                    cm.Parameters.AddWithValue("@pPrice", Convert.ToInt16(txtPrice.Text));
                    cm.Parameters.AddWithValue("@pDescription", txtDescription.Text);
                    cm.Parameters.AddWithValue("@pCategory", comboQty.Text);
                    con.Open();
                    cm.ExecuteNonQuery();
                    MessageBox.Show("Таблица успешно сохраненна");
                    Clear();
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
        public void Clear()
        {
            txtDescription.Clear();
            txtName.Clear();
            txtQuantity.Clear();
            txtPrice.Clear();
            comboQty.Text = "";
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
            btnSave.Enabled = false;
            btnUpdate.Enabled = false;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Вы уверены, что хотите внести изменения", "Подтверждение изменений", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cm = new SqlCommand("UPDATE tbProduct SET pName=@pName,pQty=@pQty,pPrice=@pPrice, pDescription=@pDescription, pCategory=@pCategory WHERE pID LIKE '" + lblId.Text + "'", con);
                    cm.Parameters.AddWithValue("@pName", txtName.Text);
                    cm.Parameters.AddWithValue("@pQty", txtQuantity.Text);
                    cm.Parameters.AddWithValue("@pPrice", txtPrice.Text);
                    cm.Parameters.AddWithValue("@pDescription", txtDescription.Text);
                    cm.Parameters.AddWithValue("@pCategory", comboQty.Text);
                    con.Open();
                    cm.ExecuteNonQuery();
                    MessageBox.Show("Таблица успешно обновлена");
                    Clear();
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
    }
}
