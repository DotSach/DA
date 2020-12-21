using DA.Properties;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Menu;

namespace DA
{
    public partial class frmAdmin : Form
    {
        public frmAdmin()
        {
            InitializeComponent();
            // add column 
            
            dtgvBanHang.ColumnCount = 8;
            dtgvBanHang.Columns[0].HeaderText = "IDHD";
            dtgvBanHang.Columns[0].Width = 50;
            dtgvBanHang.Columns[1].HeaderText = "Loại";
            dtgvBanHang.Columns[1].Width = 120;
            dtgvBanHang.Columns[2].HeaderText = "Mã SP";
            dtgvBanHang.Columns[2].Width = 90;
            dtgvBanHang.Columns[3].HeaderText = "Tên Sản Phẩm";
            dtgvBanHang.Columns[3].Width = 180;
            dtgvBanHang.Columns[4].HeaderText = "DV Tính";
            dtgvBanHang.Columns[4].Width = 100;
            dtgvBanHang.Columns[5].HeaderText = "Giá";
            dtgvBanHang.Columns[5].Width = 100;
            dtgvBanHang.Columns[6].HeaderText = "Số Lượng";
            dtgvBanHang.Columns[6].Width = 100;
            dtgvBanHang.Columns[7].HeaderText = "Thành Tiền";
            dtgvBanHang.Columns[7].Width = 120;
            
        }
        SqlConnection Connection;
        SqlCommand Command;

        //string chuoiketnoi = @"Data Source=DESKTOP-5OL6P9R\SQLEXPRESS2019;Initial Catalog=DEMODA;Integrated Security=True";
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();
        DataTable table1 = new DataTable();
        DataTable table2 = new DataTable();
        public int id;
        string sql, trax, sledit;
        DataSet dtset = new DataSet("dsVTNN");
        string chuoiketnoi = @"Data Source=DESKTOP-5OL6P9R\SQLEXPRESS2019;Initial Catalog=DEMODA;Integrated Security=True";
        SqlConnection conn;
        SqlCommand thuchien;
        SqlDataAdapter dtKhachHang;
        SqlDataAdapter dtKho;
        SqlDataAdapter dtLoai;
        SqlDataAdapter dtThongTinHoaDon;
        //DataTable table = new DataTable();
        int thanhtien = 0;
        int kad, tong, test, IDTT;

        public int sotientra= 0;
        // KẾT NỐI SQL DS DOANH THU
        public void Ketnoi()
        {
            
            Command = Connection.CreateCommand();
            Command.CommandText = "select *from HoaDon";
            adapter.SelectCommand = Command;
            table1.Clear();
            adapter.Fill(table1);
            DSDoanhThu.DataSource = table1;
        }
        // KẾT NỐI 
        void loaddata()
        {
            Command = Connection.CreateCommand();
            Command.CommandText = "select *from KhachHang";
            adapter.SelectCommand = Command;
            table.Clear();
            adapter.Fill(table);
            DSKhachHang.DataSource = table;
        }
        
        void loadTTHD()
        {
            Command = Connection.CreateCommand();
            Command.CommandText = "select *from ThongTinHoaDon";
            adapter.SelectCommand = Command;


        }
        //KẾT NỐI SQL KHO
        void LoadKho()
        {
            Command = Connection.CreateCommand();
            Command.CommandText = "select *from Kho";
            adapter.SelectCommand = Command;
            table2.Clear();
            adapter.Fill(table2);
            DSKho.DataSource = table2;
        }
        // loatlaij khach hang
        public void LoadKH()
        {
            Command = Connection.CreateCommand();
            Command.CommandText = "select *from KhachHang where Tenkh like '%"+txttim.Text+"%'";
            adapter.SelectCommand = Command;
            table.Clear();
            adapter.Fill(table);
            DSKhachHang.DataSource = table;
        }
        //click vào để show
        private void DSKhachHang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = DSKhachHang.CurrentRow.Index;
            txtEdit_ID_KH.Text = DSKhachHang.Rows[i].Cells[0].Value.ToString();
            txtEdit_NameKH.Text = DSKhachHang.Rows[i].Cells[1].Value.ToString();
            txtEdit_AdressKH.Text = DSKhachHang.Rows[i].Cells[3].Value.ToString();
            txtEdit_SDT_KH.Text = DSKhachHang.Rows[i].Cells[2].Value.ToString();
            txtKH_NoCu.Text = DSKhachHang.Rows[i].Cells[4].Value.ToString();
            txtKH_NoMoi.Text = DSKhachHang.Rows[i].Cells[4].Value.ToString();
        }
        
        private void frmAdmin_Load(object sender, EventArgs e)
        {
            // An
            //data khachhang kho doanh thu
            Connection = new SqlConnection(chuoiketnoi);
            Connection.Open();
            loaddata();
            Ketnoi();
            LoadKho();
            DSKhachHang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DSDoanhThu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //DSKhachHang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            DSKho.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            txtKH_NoCu.Enabled = false;
            txtKH_NoMoi.Enabled = false;
            dtgvBanHang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Connection.Close();

            /// khang
            /// data ban hang
            conn = new SqlConnection(chuoiketnoi);
            conn.Open();
            string ql = @"select sum(Tong) from HoaDon";
            thuchien = new SqlCommand(ql, conn);
            int TongHD = int.Parse(thuchien.ExecuteScalar().ToString());
            conn.Close();
            lbTongHD.Text = "Tổng Doanh Thu: " + TongHD.ToString();

            //dữ liệu sản phẩm
            string queryBH = @"select * from kho";

            // mã sp
            dtKho = new SqlDataAdapter(queryBH, conn);
            dtKho.Fill(dtset, "tblKho");
            cbbSale_Id_SP.DataSource = dtset.Tables["tblKho"];
            cbbSale_Id_SP.DisplayMember = "Masp";

            // tên sp
            cbbSale_NamSP.DataSource = dtset.Tables["tblKho"];
            cbbSale_NamSP.DisplayMember = "Tensp";

            // đơn vị tính 
            cbbSale_DVTinh.DataSource = dtset.Tables["tblKho"];
            cbbSale_DVTinh.DisplayMember = "DVT";

            // gia
            cpbSale_Price.DataSource = dtset.Tables["tblKho"];
            cpbSale_Price.DisplayMember = "Giaban";



            // dữ liệu khách hàng lên datagridview khách hàng
            string queryKH = @"select * from khachhang";
            dtKhachHang = new SqlDataAdapter(queryKH, conn);
            dtKhachHang.Fill(dtset, "tblKhachHang");

            // mã khách hàng
            cpbKH_ID.DataSource = dtset.Tables["tblKhachHang"];
            cpbKH_ID.DisplayMember = "Makh";

            // tên khach hang
            cpbKH_Name.DataSource = dtset.Tables["tblKhachHang"];
            cpbKH_Name.DisplayMember = "Tenkh";

            // dia chi khach hang
            cpbKH_Address.DataSource = dtset.Tables["tblKhachHang"];
            cpbKH_Address.DisplayMember = "DiaChi";

            // số diện thoại 
            cpbKH_SDT.DataSource = dtset.Tables["tblKhachHang"];
            cpbKH_SDT.DisplayMember = "SDT";

            // loai
            string queryL = "select * from Loai";
            dtLoai = new SqlDataAdapter(queryL, conn);
            dtLoai.Fill(dtset, "tblLoai");
            cpbSale_Loai.DataSource = dtset.Tables["tblLoai"];
            cpbSale_Loai.DisplayMember = "TenLoai";

            // id tthd
            string queryatt = @"select max(id) as tt from ThongTinHoaDon";
            dtThongTinHoaDon = new SqlDataAdapter(queryatt, conn);
            //do du lieu vao data set
            dtThongTinHoaDon.Fill(dtset, "tblThongTinHoaDon");
            cpbSale_ID.DataSource = dtset.Tables["tblThongTinHoaDon"];
            cpbSale_ID.DisplayMember = "tt";
            IDTT = Convert.ToInt32(cpbSale_ID.Text);
            
            // 0 là admin 1 la nhân viên
            // phân quyền
            if(id==1)
            {
                btnEdit_Add.Enabled = false;
                btnEdit_Delete.Enabled = false;
                btnEdit_Edit.Enabled = false;
                btnKH_thanhtoan.Enabled = false;
                btnSua.Enabled = false;
                btnTHem.Enabled = false;
                btnXoa.Enabled = false;
            } 
        }
        // tạo mới kho
        // datagridview bán hàng
        private void dtgvBanHang_Click_1(object sender, EventArgs e)
        {
            // Hiện thị dữ liệu datagridview lên cac combobox
            DataGridViewRow dr = dtgvBanHang.SelectedRows[0];
            cpbSale_IDaa.Text = dr.Cells[0].Value.ToString();
            cpbSale_Loai.Text = dr.Cells[1].Value.ToString();
            cbbSale_Id_SP.Text = dr.Cells[2].Value.ToString();
            cbbSale_NamSP.Text = dr.Cells[3].Value.ToString();
            cbbSale_DVTinh.Text = dr.Cells[4].Value.ToString();
            cpbSale_Price.Text = dr.Cells[5].Value.ToString();
            //string s1 = dr.Cells[5].Value.ToString(); // convert sang decimal
            nmSale_SL.Value = Convert.ToDecimal(dr.Cells[6].Value.ToString());
            trax = dr.Cells[7].Value.ToString();
            // lấy số lượng sau khi chỉ sửa
            sledit = dr.Cells[6].Value.ToString();
            // lấy id
            kad = Convert.ToInt32(dr.Cells[0].Value.ToString());
        }
        private void dtgvBanHang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = dtgvBanHang.CurrentRow.Index;
            cpbSale_IDaa.Text = dtgvBanHang.Rows[i].Cells[0].Value.ToString();
            cpbSale_Loai.Text = dtgvBanHang.Rows[i].Cells[1].Value.ToString();
            cbbSale_Id_SP.Text = dtgvBanHang.Rows[i].Cells[2].Value.ToString();
            cbbSale_NamSP.Text = dtgvBanHang.Rows[i].Cells[3].Value.ToString();
            cbbSale_DVTinh.Text = dtgvBanHang.Rows[i].Cells[4].Value.ToString();
            cpbSale_Price.Text = dtgvBanHang.Rows[i].Cells[5].Value.ToString();
            //string s1 = dr.Cells[5].Value.ToString(); // convert sang decimal
            nmSale_SL.Value = Convert.ToDecimal(dtgvBanHang.Rows[i].Cells[6].Value.ToString());
            trax = dtgvBanHang.Rows[i].Cells[7].Value.ToString();
            // lấy số lượng sau khi chỉ sửa
            sledit = dtgvBanHang.Rows[i].Cells[6].Value.ToString();
            // lấy id
            kad = Convert.ToInt32(dtgvBanHang.Rows[i].Cells[0].Value.ToString());
        }
        // thêm thông tin hóa đơn bán hàng
        private void btnSale_Add_Click(object sender, EventArgs e)
        {
            int sslm = Convert.ToInt32(nmSale_SL.Value);
            if (txtSale_Mahd.Text != "")
            {
                // ktra so luong trong kho
                conn.Open();
                sql = @"select soluong from kho where masp = '" + cbbSale_Id_SP.Text + @"'";
                thuchien = new SqlCommand(sql, conn);
                int slcon = int.Parse(thuchien.ExecuteScalar().ToString());
                conn.Close();
                MessageBox.Show(slcon.ToString());
                if (slcon < sslm)
                {
                    MessageBox.Show("Số lượng sản phẩm trong kho nhỏ hơn số lượng mua!", "Thông Báo");

                }
                else
                {
                    int sl1 = Convert.ToInt32(nmSale_Count.Value);
                    //int soluongcon = 
                    IDTT += 1;
                    int t, sl;
                    t = Int32.Parse(cpbSale_Price.Text);
                    sl = Convert.ToInt32(nmSale_SL.Value);
                    thanhtien = t * sl;
                    tong += thanhtien;
                    //MessageBox.Show(tong.ToString());
                    //loadTTHD();
                    dtgvBanHang.Rows.Add(cpbSale_ID.Text, cpbSale_Loai.Text, cbbSale_Id_SP.Text, cbbSale_NamSP.Text, cbbSale_DVTinh.Text, cpbSale_Price.Text, nmSale_SL.Value, thanhtien);
                    cpbSale_ID.Text = (IDTT).ToString();
                    
                    conn.Open();
                    sql = @"insert into ThongTinHoaDon values ('" + IDTT + @"',N'" + txtSale_Mahd.Text + @"', N'" + cbbSale_Id_SP.Text + @"',N'" + cbbSale_NamSP.Text + @"',N'" + cpbSale_Price.Text + @"',N'" + nmSale_SL.Value + @"',N'" + thanhtien + @"', N'" + cpbKH_ID.Text + @"')";
                    thuchien = new SqlCommand(sql, conn);
                    thuchien.ExecuteReader();
                    conn.Close();
                    // cap nhat lai so luong hang trong kho 
                    conn.Open();
                    sql = @"Update Kho set SoLuong -= N'" + nmSale_SL.Value + @"' where Masp = N'" + cbbSale_Id_SP.Text + @"' ";
                    thuchien = new SqlCommand(sql, conn);
                    thuchien.ExecuteNonQuery();
                    conn.Close();
                }


            }
            else
            {
                MessageBox.Show("Vui Lòng Nhập Mã Hóa Đơn Hoặc Tạo Mới Hóa Đơn!", "Thông Báo");
            }
        }
        // sửa bán hàng
        private void btnSale_Edit_Click(object sender, EventArgs e)
        {
            int gia, sl;
            int slsauedit = Convert.ToInt32(nmSale_SL.Value);
            gia = Int32.Parse(cpbSale_Price.Text);
            sl = Convert.ToInt32(nmSale_SL.Value);
            // tinh thanh tien
            thanhtien = gia * sl;
            test = thanhtien;
            // tra la thanh tien
            tong -= Convert.ToInt32(tra);
            tong += test;
            
            //dtgvBanHang.Rows.(cpbSale_Loai.Text, cbbSale_NameSP.Text, cbbSale_NameSP.Text, cbbSale_DVTinh.Text, cpbSale_Price.Text, nmSale_Count.Value, thanhtien);
            dtgvBanHang.BeginEdit(true);
            dtgvBanHang.SelectedRows[0].Cells[1].Value = cpbSale_Loai.Text;
            dtgvBanHang.SelectedRows[0].Cells[2].Value = cbbSale_Id_SP.Text;
            dtgvBanHang.SelectedRows[0].Cells[3].Value = cbbSale_NamSP.Text;
            dtgvBanHang.SelectedRows[0].Cells[4].Value = cbbSale_DVTinh.Text;
            dtgvBanHang.SelectedRows[0].Cells[5].Value = cpbSale_Price.Text;
            dtgvBanHang.SelectedRows[0].Cells[6].Value = nmSale_SL.Value;
            dtgvBanHang.SelectedRows[0].Cells[7].Value = thanhtien.ToString();
            
            int idhdedit = kad + 1;
            // cập nhật lại thông tin hóa đơn
            conn.Open();
            sql = @"Update ThongTinHoaDon set Soluong=N'" + nmSale_SL.Value + @"',Thanhtien=N'" + thanhtien + @"',Makh=N'" + cpbKH_ID.Text + @"' where id = N'" + idhdedit.ToString() + @"'";
            thuchien = new SqlCommand(sql, conn);
            thuchien.ExecuteNonQuery();
            conn.Close();
            

            // cập nhật số luong sua khi chi sua
            int edit = slsauedit - Convert.ToInt32(sledit);
            //MessageBox.Show(edit.ToString());
            // id hóa đơn mỗi lần thên vào tăng id lên để lấy id xóa
            
            // cập nhật lại số lương trong kho
            conn.Open();
            sql = @"update Kho set soluong -= '" + edit.ToString() + @"' where Masp = N'" + cbbSale_Id_SP.Text + @"'";
            thuchien = new SqlCommand(sql, conn);
            thuchien.ExecuteNonQuery();
            conn.Close();
        }
        // xóa bán hàng
        private void btnSale_Delete_Click(object sender, EventArgs e)
        {
            int slcong;
            if (dtgvBanHang.Rows.Count > 0)
            {
                if (dtgvBanHang.SelectedRows != null)
                {
                    int x = Convert.ToInt32(dtgvBanHang.SelectedRows[0].Cells[7].Value.ToString());
                    MessageBox.Show(x.ToString());
                    tong -= x;// khi xoa se trừ thanh tiền của row đó
                    dtgvBanHang.Rows.RemoveAt(dtgvBanHang.SelectedRows[0].Index);

                    conn.Open();
                    sql = @"delete from ThongTinHoaDon where (id = N'" + cpbSale_ID.Text + @"')";
                    thuchien = new SqlCommand(sql, conn);
                    thuchien.ExecuteNonQuery();
                    conn.Close();

                    conn.Open();
                    slcong = Convert.ToInt32(nmSale_SL.Value.ToString());
                    sql = @"update kho set soluong+='" + slcong.ToString() + @"' where masp = N'" + cbbSale_Id_SP.Text + @"'";
                    thuchien = new SqlCommand(sql, conn);
                    thuchien.ExecuteNonQuery();
                    conn.Close();
                }

            }
            else
                MessageBox.Show("Không có sản phẩm để xóa!", "Thông Báo");
        }
        //thêm hóa đơn.  bán hàng
        private void btnSale_addHD_Click(object sender, EventArgs e)
        {
            // tạo mới hóa đơn
            conn.Open();
            string queryhd = @"insert into HoaDon values (N'" + txtSale_Mahd.Text + @"', N'" + dtSale_Day.Value + @"','0')";
            thuchien = new SqlCommand(queryhd, conn);
            thuchien.ExecuteReader();
            conn.Close();
            MessageBox.Show("Tạo Mới Hóa Đơn Thành Công!");
        }
        // thanh
        private void btnThanhToanBH_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn Có Thực Sự Muốn Thanh Toán?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                frmThanhToan f1 = new frmThanhToan();
                f1.thanhtoan = tong;
                f1.makh = cpbKH_ID.Text;
                f1.mahd = txtSale_Mahd.Text;
                f1.ShowDialog();
                dtgvBanHang.Rows.Clear();
                tong = 0;
                //MessageBox.Show(sotientra.ToString());
            }
        }
        string account;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
                if (id == 1)
                {
                    account = "Admin";
                }
                else
                    account = "Nhân Viên";
                Image ima = Resources.top;

            e.Graphics.DrawImage(ima, 0, 0, ima.Width, ima.Height);
            //e.Graphics.DrawString("Cửa Hàng Vật Tư Nông Nghiệp", new Font("Arial", 22, FontStyle.Regular), Brushes.Black, new Point(220, 0));
            //e.Graphics.DrawString("Đồ Án Môn Học .Net", new Font("Arial", 18, FontStyle.Regular), Brushes.Black, new Point(250, 60));
            //e.Graphics.DrawString("Nhóm: 3", new Font("Arial", 22, FontStyle.Regular), Brushes.Black, new Point(350, 80));
            e.Graphics.DrawString("Hóa Đơn Bán Hàng", new Font("Arial", 22, FontStyle.Regular), Brushes.Black, new Point(300, 130));
            e.Graphics.DrawString("Ngày: " + DateTime.Now.ToShortDateString(), new Font("Arial", 14, FontStyle.Regular), Brushes.Black, new Point(350, 160));
            e.Graphics.DrawString("Tên Khách Hàng: " + cpbKH_Name.Text.Trim(), new Font("Arial", 14, FontStyle.Regular), Brushes.Black, new Point(0, 240));
            e.Graphics.DrawString("Số Điện Thoại: " + cpbKH_SDT.Text, new Font("Arial", 14, FontStyle.Regular), Brushes.Black, new Point(0, 270));
            e.Graphics.DrawString("Địa Chỉ: " + cpbKH_Address.Text.Trim(), new Font("Arial", 14, FontStyle.Regular), Brushes.Black, new Point(0, 300));
            e.Graphics.DrawString("Tài Khoản: " + account.Trim(), new Font("Arial", 14, FontStyle.Regular), Brushes.Black, new Point(0, 330));
            e.Graphics.DrawString("-----------------------------------------------------------------------------------------------------------------------------------", new Font("Arial", 14, FontStyle.Regular), Brushes.Black, new Point(0, 350));
            e.Graphics.DrawString("DVT", new Font("Arial", 14, FontStyle.Regular), Brushes.Black, new Point(280, 370));
            e.Graphics.DrawString("Tên SP", new Font("Arial", 14, FontStyle.Regular), Brushes.Black, new Point(30, 370));
            e.Graphics.DrawString("Giá", new Font("Arial", 14, FontStyle.Regular), Brushes.Black, new Point(480, 370));
            e.Graphics.DrawString("Số Lượng", new Font("Arial", 14, FontStyle.Regular), Brushes.Black, new Point(550, 370));
            e.Graphics.DrawString("Thành Tiền", new Font("Arial", 14, FontStyle.Regular), Brushes.Black, new Point(700, 370));
            e.Graphics.DrawString("-----------------------------------------------------------------------------------------------------------------------------------", new Font("Arial", 14, FontStyle.Regular), Brushes.Black, new Point(0, 390));

            
        }
        private void btnRefresh_BH_Click(object sender, EventArgs e)
        {
            cpbKH_Address.DataSource = null;
            cpbKH_Address.Items.Clear();

            SqlDataAdapter test = new SqlDataAdapter();
            SqlDataAdapter kk = new SqlDataAdapter();
            
            string queryBH = @"select * from kho";
            
            // mã sp
            test = new SqlDataAdapter(queryBH, conn);
            test.Fill(dtset, "tblKho");
            cbbSale_Id_SP.DataSource = dtset.Tables["tblKho"];
            cbbSale_Id_SP.DisplayMember = "Masp";

            // tên sp
            cbbSale_NamSP.DataSource = dtset.Tables["tblKho"];
            cbbSale_NamSP.DisplayMember = "Tensp";

            // đơn vị tính 
            cbbSale_DVTinh.DataSource = dtset.Tables["tblKho"];
            cbbSale_DVTinh.DisplayMember = "DVT";

            cpbSale_Price.DataSource = dtset.Tables["tblKho"];
            cpbSale_Price.DisplayMember = "Giaban";

            // dữ liệu khách hàng lên datagridview khách hàng
            string queryKH = @"select * from khachhang";
            kk = new SqlDataAdapter(queryKH, conn);
            kk.Fill(dtset, "tblKhachHang");

            // mã khách hàng
            cpbKH_ID.DataSource = dtset.Tables["tblKhachHang"];
            cpbKH_ID.DisplayMember = "Makh";

            // tên khach hang
            cpbKH_Name.DataSource = dtset.Tables["tblKhachHang"];
            cpbKH_Name.DisplayMember = "Tenkh";

            // dia chi khach hang
            cpbKH_Address.DataSource = dtset.Tables["tblKhachHang"];
            cpbKH_Address.DisplayMember = "DiaChi";

            // số diện thoại 
            cpbKH_SDT.DataSource = dtset.Tables["tblKhachHang"];
            cpbKH_SDT.DisplayMember = "SDT";

            //loadTTHD();
            MessageBox.Show("Tạo Mới Thông Tin Khách Hàng Và Sản Phẩm Thành Công!", "Thông Báo");
        }
        private void btnIn_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
            //printDocument1.Print();
        }
        /// <summary>
        /// /////
        /// // thêm khách hàng
        private void btnEdit_Add_Click(object sender, EventArgs e)
        {
            Connection.Open();
            Command.Connection.CreateCommand();
            Command.CommandText = "insert into KhachHang values('" + txtEdit_ID_KH.Text + "',N'" + txtEdit_NameKH.Text + "','" + txtEdit_SDT_KH.Text + "',N'" + txtEdit_AdressKH.Text + "','" + txtKH_NoCu.Text + "')";
            Command.ExecuteNonQuery();
            loaddata();
            Connection.Close();
        }
        // sửa khach hàng
        private void btnEdit_Edit_Click(object sender, EventArgs e)
        {
            Connection.Open();
            Command = Connection.CreateCommand();
            Command.CommandText = "update KhachHang set  Tenkh = N'" + txtEdit_NameKH.Text + "', SDT = " + txtEdit_SDT_KH.Text + ",DiaChi = N'" + txtEdit_AdressKH.Text + "', CongNo = " + txtKH_NoCu.Text + " where Makh = '" + txtEdit_ID_KH.Text + "'";
            Command.ExecuteNonQuery();

            loaddata();
            Connection.Close();
        }

        private void btnEdit_Delete_Click(object sender, EventArgs e)
        {
            Connection.Open();
            Command = Connection.CreateCommand();
            Command.CommandText = "delete from KhachHang where Makh = '" + txtEdit_ID_KH.Text + "'";
            Command.ExecuteNonQuery();
            loaddata();
            Connection.Close();
        }
        // thoát kho
        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn Có Muốn Thoát", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();

                
            }
        }
        
        //THÊM SAP TRONG KHO
        private void btnTHem_Click(object sender, EventArgs e)
        {
            Connection.Open();
            Command.Connection.CreateCommand();
            Command.CommandText = "insert into Kho values(N'" + txtMaSP.Text + "',N'" + txtTenSP.Text + "','" + nmSale_Count.Text + "','" + txtGiaMua.Text + "','" + txtGiaBan.Text + "',N'" +txtLoai.Text + "',N'" + txtDVT.Text + "','" + txtTinhTrang.Text + "')";
            Command.ExecuteNonQuery();
            LoadKho();
            Connection.Close();
        }
       
        //DSKHO
        private void DSKho_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = DSKho.CurrentRow.Index;
            txtMaSP.Text = DSKho.Rows[i].Cells[0].Value.ToString();
            txtTenSP.Text = DSKho.Rows[i].Cells[1].Value.ToString();
            nmSale_Count.Value = Convert.ToDecimal(DSKho.Rows[i].Cells[2].Value.ToString());
            txtGiaMua.Text = DSKho.Rows[i].Cells[3].Value.ToString();
            txtGiaBan.Text = DSKho.Rows[i].Cells[4].Value.ToString();
            txtLoai.Text = DSKho.Rows[i].Cells[5].Value.ToString();
            txtDVT.Text = DSKho.Rows[i].Cells[6].Value.ToString();
            txtTinhTrang.Text = DSKho.Rows[i].Cells[7].Value.ToString();
        }
        //SỮA SAN PHẨM TRONG KHO
        private void btnSua_Click(object sender, EventArgs e)
        {
            Connection.Open();
            Command = Connection.CreateCommand();
            Command.CommandText = "update Kho set  Tensp = N'" + txtTenSP.Text + "', SoLuong = " + nmSale_Count.Text + ",GiaMua = " + txtGiaMua.Text + ", GiaBan = " + txtGiaBan.Text + ",idLoai = N'" +txtLoai.Text + "',DVT = N'" + txtDVT.Text + "',TinhTrang = " + txtTinhTrang.Text + " where Masp = '" + txtMaSP.Text + "'";
            Command.ExecuteNonQuery();
            LoadKho();
            Connection.Close();
        }
        // XÓA SẢN PHẨM TRONG KHO
        private void btnXoa_Click(object sender, EventArgs e)
        {
            Connection.Open();
            Command = Connection.CreateCommand();
            Command.CommandText = "delete from ThongTinHoaDon where Masp = '" + txtMaSP.Text + "' delete from kho where masp = '"+txtMaSP.Text+@"'";
            Command.ExecuteNonQuery();
            LoadKho();
            Connection.Close();
        }
        // THOÁT
        private void btnThoat1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn Có Muốn Thoát", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();

            }
        }
        int tra, con, No;

        private void btnTaoMoiK_Click(object sender, EventArgs e)
        {
            LoadKho();
            MessageBox.Show("Tạo Mới Thông Tin Kho Thành Công!", "Thông Báo");
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnTaoMoi_Click(object sender, EventArgs e)
        {
            loaddata();
            MessageBox.Show("Tạo Mới Thông Tin Khách Hàng Thành Công!", "Thông Báo");
        }

        

        // tim khach hang
        private void txttim_TextChanged(object sender, EventArgs e)
        {
            LoadKH();
        }
        // tạo mới khách hàng

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Application.Exit();
            this.Close();
            frmLongin fl = new frmLongin();
            fl.ShowDialog();
        }
        // tạo mới doanh thu
        private void btnTaoMoiDT_Click(object sender, EventArgs e)
        {
            Ketnoi();
            conn.Open();
            string ql = @"select sum(Tong) from HoaDon";
            thuchien = new SqlCommand(ql, conn);
            int TongHD = int.Parse(thuchien.ExecuteScalar().ToString());
            conn.Close();
            lbTongHD.Text = "Tổng Doanh Thu: " + TongHD.ToString();
            MessageBox.Show("Tạo Mới Doanh Thu Thành Công!");
        }


        // thoát BH 
        private void btnThoatBH_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Bạn Có Muốn Thoát", "Thông Báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)== DialogResult.OK)
            {
                Application.Exit();
            }    
        }

        //thoát daonh thu
        private void btnDT_close_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn Có Muốn Thoát", "Thông Báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                Application.Exit();
            }
        }
        //update nợ
        private void btnKH_thanhtoan_Click(object sender, EventArgs e)
        {
            Connection.Open();
            Command = Connection.CreateCommand();
            Command.CommandText = "update KhachHang set  CongNo = " + txtKH_NoMoi.Text + " where Makh = '" + txtEdit_ID_KH.Text + "'";
            MessageBox.Show("Thanh Toán Thành Công!", "Thông Báo");
            Command.ExecuteNonQuery();
            loaddata();
            txtKH_Tra.Clear();
            Connection.Close();
        }
        // Thanh toán nợ
        private void txtKH_Tra_TextChanged(object sender, EventArgs e)
        {
            if (txtKH_Tra.Text != "")
            {
                tra = Convert.ToInt32(txtKH_Tra.Text);
                No = Convert.ToInt32(txtKH_NoCu.Text);
                con = No - tra;
                txtKH_NoMoi.Text = con.ToString();
            }
            else
            {
                txtKH_NoMoi.Text = txtKH_NoCu.Text;
            }
        }
        //private List<CartItem> khang 
        
    }
    /// chuyen doi số sang chuoi
    
}
