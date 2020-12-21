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

namespace DA
{
    public partial class frmThanhToan : Form
    {
        public frmThanhToan()
        {
            InitializeComponent();
        }
        public int thanhtoan = 0;
        int tra, con;
        string sql;
        int i = 0;
        DataSet dtset = new DataSet("dsVTNN");
        //SqlDataAdapter dtKhachHang;
        string chuoiketnoi = @"Data Source=DESKTOP-5OL6P9R\SQLEXPRESS2019;Initial Catalog=DEMODA;Integrated Security=True";
        SqlConnection conn;
        SqlCommand thuchien;
        public string makh, mahd;
        private void Form1_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(chuoiketnoi); 
            lb_ThanhToan.Text = "Tổng Hóa Đơn: "+thanhtoan.ToString();
            txtConLai.Text = thanhtoan.ToString();
            txtConLai.Enabled = false;
        }

        private void btnXacNhan_TT_Click(object sender, EventArgs e)
        {

            conn.Open();
            sql = @"update khachhang set CongNo += N'" + txtConLai.Text + @"' where Makh = N'" +makh+ @"' ";
            thuchien = new SqlCommand(sql, conn);
            thuchien.ExecuteNonQuery();
            conn.Close();
            if(txtTraTruoc.Text != "")
            {
                frmAdmin fm = new frmAdmin();
                fm.sotientra = int.Parse(txtTraTruoc.Text);
            }
                
            
            
            

            conn.Open();
            sql = @"update HoaDon set Tong = '"+thanhtoan.ToString()+@"' where mahd = N'"+mahd+@"'";
            thuchien = new SqlCommand(sql, conn);
            thuchien.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Thanh Toán Thành Công!");
            this.Close();
        }

        private void txtTraTruoc_TextChanged(object sender, EventArgs e)
        {
            if(txtTraTruoc.Text != "")
            {
                tra = Convert.ToInt32(txtTraTruoc.Text);
                con = thanhtoan - tra;
                txtConLai.Text = con.ToString();
            }

        }
    }
}
