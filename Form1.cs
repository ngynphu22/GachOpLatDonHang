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

namespace formDonhang
{
    public partial class FormDonHang : Form
    {
        string chuoiKetnoi = @"Data Source=TUF\SQLEXPRESS;Initial Catalog=DonHang;Integrated Security=True";

        public FormDonHang()
        {
            InitializeComponent();
        }


        //load ra gridview
        public void load()
        {
            SqlConnection conn = new SqlConnection(chuoiKetnoi);
            try
            {
                conn.Open();
                string sql = "select * from DonHang";
                SqlDataAdapter dt = new SqlDataAdapter(sql,conn);
                DataTable tb = new DataTable();
                dt.Fill(tb);
                dataGridView1.DataSource = tb;
                conn.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Lỗi kết nối:"+ex.Message);
            }
        }
        


        private void Form1_Load(object sender, EventArgs e)
        {
            
            load();
        }

        private void btnNhaplai_Click(object sender, EventArgs e)
        {
            txtMaHang.Clear();
            txtTenHang.Clear();
            txtGia.Clear();
        }

        private void btnCapnhat_Click(object sender, EventArgs e)
        {
            load();
        }
        public bool kiemTraMaHang(string mahang)
        {
            SqlConnection conn = new SqlConnection(chuoiKetnoi);
            conn.Open();
            string sql = "select Mahang from DonHang where MaHang='" + mahang + "'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            if(dr.Read()==true)
            {
                conn.Close();
                return true;
            }
            conn.Close();
            return false;
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(chuoiKetnoi);
            try
            {
                if (txtMaHang.Text !="" && txtTenHang.Text !="" && txtGia.Text !="")
                {
                    if (kiemTraMaHang(txtMaHang.Text) == true)
                        MessageBox.Show("Mã hàng đã tồn tại");
                    else
                    {
                        conn.Open();
                        string sql = "insert into DonHang values('" + txtMaHang.Text + "',N'" + txtTenHang.Text + "','"+txtGia.Text+"')";
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        int kq = (int) cmd.ExecuteNonQuery();
                        if (kq > 0)
                        {
                            MessageBox.Show("Thêm thành công!");
                            load();
                        }
                        else
                            MessageBox.Show("Thêm thất bại");
                        conn.Close();
                    }
                    
                }
                else
                    MessageBox.Show("Chưa nhập đủ thông tin");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối:" + ex.Message);
            }
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(chuoiKetnoi);
            try
            {
                conn.Open();
                string sql = "update DonHang set Tenhang=N'"+txtTenHang.Text+"', Gia='"+txtGia.Text+"' where Mahang='"+txtMaHang.Text+"'";
                SqlCommand cmd = new SqlCommand(sql, conn);
                int kq = (int)cmd.ExecuteNonQuery();
                if (kq > 0)
                {
                    MessageBox.Show("Sửa thành công!");
                    load();
                }
                else
                    MessageBox.Show("Sửa thất bại");
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối:" + ex.Message);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult thongbao;
            thongbao = MessageBox.Show("Bạn có muốn xóa hay không?","Thông báo",MessageBoxButtons.OKCancel,MessageBoxIcon.Question);
            if(thongbao==DialogResult.OK)
            {
                SqlConnection conn = new SqlConnection(chuoiKetnoi);
                conn.Open();
                string sql = "delete from DonHang where Mahang='" + txtMaHang.Text + "' ";
                SqlCommand cmd = new SqlCommand(sql,conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Xóa thành công!");
                load();
                conn.Close();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaHang.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtTenHang.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtGia.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
        }

        private void btnThanhtoan_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
