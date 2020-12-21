using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DA
{
    public partial class frmLongin : Form
    {
        public frmLongin()
        {
            InitializeComponent();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            if(txtUsername.Text == "kieukhang" && txtPass.Text == "1230")
            {
                //this.Hide();
                
                frmAdmin fma = new frmAdmin();
                fma.id = 0;

                fma.ShowDialog();
                this.Close();
                txtPass.Clear();
                txtUsername.Clear();
                txtUsername.Focus();
                
                
            }else if(txtUsername.Text =="caoan" && txtPass.Text == "2535") 
            {
                //this.Hide();
                
                frmAdmin fma = new frmAdmin();
                fma.id = 1;
                
                fma.ShowDialog();
                this.Close();
                txtPass.Clear();
                txtUsername.Clear();
                txtUsername.Focus();
                
            }
            else
                MessageBox.Show("Sai Thông Tin Tài Khoản Hoặc Mật Khẩu!", "Thông Báo", MessageBoxButtons.OKCancel);

        }

        private void btnCloseDN_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
