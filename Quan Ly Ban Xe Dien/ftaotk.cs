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

namespace Quan_Ly_Ban_Xe_Dien
{
    public partial class ftaotk : Form
    {
        string chuoiketnoi = @"Data Source=desktop-cdsngvk\may1;Initial Catalog=quanlybanxe;Integrated Security=True";
        string sql;
        SqlConnection ketnoi;
        SqlCommand thuchien;
        SqlDataReader docdulieu;
        public ftaotk()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btntaotk_Click(object sender, EventArgs e)
        {
            ketnoi.Open();
            sql = "insert into taikhoan(tk,mk) " + "values(@tk,@mk)";
            thuchien = new SqlCommand(sql, ketnoi);
            thuchien.Parameters.AddWithValue("tk", txttaikhoanmoi.Text);
            thuchien.Parameters.AddWithValue("mk", txtmatkhaumoi.Text);
            thuchien.ExecuteNonQuery();
            ketnoi.Close();
            XoaThongTin();
            MessageBox.Show("Tạo tài khoản thành công!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void fdoitk_Load(object sender, EventArgs e)
        {
            ketnoi = new SqlConnection(chuoiketnoi);
        }
        private void XoaThongTin()
        {
            txttaikhoanmoi.Text = "";
            txtmatkhaumoi.Text = "";
            
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            fDN mainForm = new fDN();
            mainForm.ShowDialog();
        }
    }
}
