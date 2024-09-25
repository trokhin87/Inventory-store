using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inventory_store
{
    public partial class login_form : Form
    {
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
    }
}
