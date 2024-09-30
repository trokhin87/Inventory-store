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
    public partial class login_form : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\Documents\Store.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        public login_form()
        {
            InitializeComponent();
        }

        private void name_magazine_Click(object sender, EventArgs e)
        {

        }

        private void checkBoxPass_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBoxPass.Checked ) 
            {
                textBox_password.UseSystemPasswordChar = true;
            }
            else
            {
                textBox_password.UseSystemPasswordChar=false;
            }
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            textBox_password.Clear();
            textBox_username.Clear();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Exit Application","Confirm",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes) { Application.Exit(); }
        }

        private void LogIn_Click(object sender, EventArgs e)
        {
            try
            {
                cm=new SqlCommand("SELECT * FROM tbUser WHERE username=@username AND password=@password",con);
                cm.Parameters.AddWithValue("@username", textBox_username.Text);
                cm.Parameters.AddWithValue("@password", textBox_password.Text);
                con.Open();
                dr=cm.ExecuteReader();
                dr.Read();
                if(dr.HasRows) 
                {
                    MessageBox.Show("Добро пожаловать" + dr["fullname"].ToString()+ " | ", "Доступ получен", MessageBoxButtons.OK,MessageBoxIcon.Information);
                    MainForm main=new MainForm();
                    main.ShowDialog();
                    this.Hide();
                }
                else
                 {
                    MessageBox.Show("Введенны неверные данные", " Доступ запрещен", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                con.Close();
            }
            catch (Exception ex)  
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
