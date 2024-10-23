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
    public partial class baocao : Form
    {
        string chuoiketnoi = @"Data Source=desktop-cdsngvk\may1;Initial Catalog=quanlybanxe;Integrated Security=True";
        string sql;
        SqlConnection ketnoi;
        SqlCommand thuchien;
        SqlDataReader docdulieu;
        int i = 0;
        public baocao()
        {
            InitializeComponent();
        }

        private void lvNCC_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void baocao_Load(object sender, EventArgs e)
        {
            ketnoi = new SqlConnection(chuoiketnoi);
            hienthi();
        }
        public void hienthi()
        {
            lvbaocao.Items.Clear();
            ketnoi.Open();
            sql = @"SELECT baocaongay.ngaybaocao sum(baocaongay.slnhap) sum(baocaongay.slxuat) sum(baocaongay.gianhap) sum(baocaongay.giaxuat) FROM baocaongay where (ngaybaocao like @ngaynhap) or (ngaybaocao like @ngayxuat)";
            thuchien = new SqlCommand(sql, ketnoi);
            docdulieu = thuchien.ExecuteReader();
            i = 0;
            while (docdulieu.Read())
            {
                lvbaocao.Items.Add(docdulieu[0].ToString());
                lvbaocao.Items[i].SubItems.Add(docdulieu[1].ToString());
                lvbaocao.Items[i].SubItems.Add(docdulieu[2].ToString());
                lvbaocao.Items[i].SubItems.Add(docdulieu[3].ToString());
                lvbaocao.Items[i].SubItems.Add(docdulieu[4].ToString());
                i++;
            }
            ketnoi.Close();

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            lvbaocao.Items.Clear();
            ketnoi.Open();
            sql = "insert into baocaongay(ngaybaocao,soluongnhap,soluongxuat,tongtiennhap,tongtienxuat) " +
                "values(@ngaybaocao,@soluongnhap,@soluongxuat,@tongtiennhap,@tongtienxuat)";
            thuchien = new SqlCommand(sql, ketnoi);
            thuchien.Parameters.AddWithValue("ngaybaocao", txtngaybc.Text);
            
            thuchien.ExecuteNonQuery();
            ketnoi.Close();
            hienthi();
            XoaThongTin();
        }
        private void XoaThongTin()
        {
            txtngaybc.Text = "";
            
        }
    }
}
