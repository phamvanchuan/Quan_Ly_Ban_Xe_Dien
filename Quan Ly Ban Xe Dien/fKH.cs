
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
using Excel = Microsoft.Office.Interop.Excel;
namespace Quan_Ly_Ban_Xe_Dien
{
    public partial class fKH : Form
    {
        string chuoiketnoi = @"Data Source=desktop-cdsngvk\may1;Initial Catalog=quanlybanxe;Integrated Security=True"; 
        string sql;
        SqlConnection ketnoi;
        SqlCommand thuchien;
        SqlDataReader docdulieu;
        int i = 0;

        public fKH()
        {
            InitializeComponent();
        }

        private void fKH_Load(object sender, EventArgs e)
        {
            ketnoi = new SqlConnection(chuoiketnoi);
            hienthi();

        }
        public void hienthi()
        {
            lvkhachhang.Items.Clear();
            ketnoi.Open();
            sql = @"SELECT * FROM khachhang";
            thuchien = new SqlCommand(sql, ketnoi);
            docdulieu = thuchien.ExecuteReader();
            i = 0;
            while (docdulieu.Read())
            {
                lvkhachhang.Items.Add(docdulieu[0].ToString());
                lvkhachhang.Items[i].SubItems.Add(docdulieu[1].ToString());
                lvkhachhang.Items[i].SubItems.Add(docdulieu[2].ToString());
                lvkhachhang.Items[i].SubItems.Add(docdulieu[3].ToString());
                lvkhachhang.Items[i].SubItems.Add(docdulieu[4].ToString());
                i++;
            }
            ketnoi.Close();
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
        }
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult kq = MessageBox.Show("Bạn có muốn thoát khỏi chương trình?",
                 "Thông báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (kq == DialogResult.OK)
                this.Close();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
        private void lvPhong_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtmakh.Text = lvkhachhang.SelectedItems[0].SubItems[0].Text;
                txttenkh.Text = lvkhachhang.SelectedItems[0].SubItems[1].Text;
                txtdiachi.Text = lvkhachhang.SelectedItems[0].SubItems[2].Text;
                txtdt.Text = lvkhachhang.SelectedItems[0].SubItems[3].Text;
                txtemail.Text = lvkhachhang.SelectedItems[0].SubItems[4].Text;
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
                txtmakh.Enabled = false;
            }
            catch
            {
            }
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult d = MessageBox.Show("Bạn có muốn xóa hay không?", "Thông báo",
                MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (d == DialogResult.Yes)
            {
                lvkhachhang.Items.Clear();
                ketnoi.Open();
                sql = "delete from khachhang where makh= @makh";
                thuchien = new SqlCommand(sql, ketnoi);
                thuchien.Parameters.AddWithValue("makh", txtmakh.Text);
                thuchien.ExecuteNonQuery();
                ketnoi.Close();
                txtmakh.Enabled = true;
                btnThem.Enabled = true;
                hienthi();
            }
            XoaThongTin();
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            lvkhachhang.Items.Clear();
            ketnoi.Open();
            sql = "insert into khachhang(makh, tenkh, diachi, dt,email) " +
                "values(@makh,@tenkh,@diachi,@dt,@email)";
            thuchien = new SqlCommand(sql, ketnoi);
            thuchien.Parameters.AddWithValue("makh", txtmakh.Text);
            thuchien.Parameters.AddWithValue("tenkh", txttenkh.Text);
            thuchien.Parameters.AddWithValue("diachi", txtdiachi.Text);
            thuchien.Parameters.AddWithValue("dt", txtdt.Text);
            thuchien.Parameters.AddWithValue("email", txtemail.Text);
            thuchien.ExecuteNonQuery();
            ketnoi.Close();
            hienthi();
            XoaThongTin();
        }


        private void btnSua_Click(object sender, EventArgs e)
        {
            lvkhachhang.Items.Clear();
            ketnoi.Open();
            sql = "update khachhang set tenkh=@tenkh,diachi=@diachi,dt=@dt,email=@email where makh=@makh";
            thuchien = new SqlCommand(sql, ketnoi);
            thuchien.Parameters.AddWithValue("tenkh", txttenkh.Text);
            thuchien.Parameters.AddWithValue("diachi", txtdiachi.Text);
            thuchien.Parameters.AddWithValue("dt", txtdt.Text);
            thuchien.Parameters.AddWithValue("email", txtemail.Text);
            thuchien.Parameters.AddWithValue("makh", txtmakh.Text);

            thuchien.ExecuteNonQuery();
            ketnoi.Close();
            txtmakh.Enabled = true;
            btnThem.Enabled = true;
            hienthi();
            XoaThongTin();
        }
        private void XoaThongTin()
        {
            txtmakh.Text = "";
            txttenkh.Text = "";
            txtdiachi.Text = "";
            txtdt.Text = "";
            txtemail.Text = "";
        }

        private void btnxuat_Click(object sender, EventArgs e)
        {
            
                Excel.Application exApp = new Excel.Application();
                Excel.Workbook exBook = exApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
                Excel.Worksheet exSheet = (Excel.Worksheet)exBook.Worksheets[1];

               

                exSheet.Range["A4:E4"].MergeCells = true;
                exSheet.Range["A4:E4"].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                Excel.Range exTitle = (Excel.Range)exSheet.Cells[4, 1];
                exTitle.Font.Size = 15;
                exTitle.Font.Bold = true;
                exTitle.Font.Color = Color.Red;
                exTitle.Value = "DANH SÁCH KHÁCH HÀNG";



                //In tiêu đề
                exSheet.Range["A6:F6"].Font.Size = 14;
                exSheet.Range["A6:F6"].Font.Bold = true;
                exSheet.Range["A6:F6"].Borders.LineStyle = Excel.Constants.xlSolid;
                exSheet.Range["A6:F6"].Interior.Color = Color.Aqua;

                exSheet.Range["A6"].Value = "STT";
                exSheet.Range["A6"].ColumnWidth = 5;

                exSheet.Range["B6"].Value = "Mã khách hàng";
                exSheet.Range["B6"].ColumnWidth = 10;

                exSheet.Range["C6"].Value = "Tên khách hàng";
                exSheet.Range["C6"].ColumnWidth = 20;

                exSheet.Range["D6"].Value = "Địa chỉ";
                exSheet.Range["D6"].ColumnWidth = 20;

                exSheet.Range["E6"].Value = "Số điện thoại";
                exSheet.Range["E6"].ColumnWidth = 10;

                exSheet.Range["F6"].Value = "Email";
                exSheet.Range["F6"].ColumnWidth = 30;
         
            //In nội dung
            int dong = 7;
                for (int i = 0; i < lvkhachhang.Items.Count - 1; i++)
                {
                    string dongs = (dong + i).ToString();
                    exSheet.Range["A" + dongs].Value = (i + 1).ToString();
                    exSheet.Range["B" + dongs].Value = lvkhachhang.Items[i].SubItems[0].Text;
                    exSheet.Range["C" + dongs].Value = lvkhachhang.Items[i].SubItems[1].Text;
                    exSheet.Range["D" + dongs].Value = lvkhachhang.Items[i].SubItems[2].Text;
                    exSheet.Range["E" + dongs].Value = lvkhachhang.Items[i].SubItems[3].Text;
                    exSheet.Range["F" + dongs].Value = lvkhachhang.Items[i].SubItems[4].Text;
                    exSheet.Range["A" + dongs + ":" + "F" + dongs].Borders.LineStyle = Excel.Constants.xlSolid;
                    exSheet.Range["A" + dongs + ":" + "C" + dongs].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                }
                exSheet.Name = "Khách hàng";
                exBook.Activate();
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "Excel|*.xlsx";
                if (save.ShowDialog() == DialogResult.OK)
                {
                    exBook.SaveAs(save.FileName);
                }
                exApp.Quit();
            
        }
    }
}
