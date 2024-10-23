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
    public partial class fNV : Form
    {
        string chuoiketnoi = @"Data Source=desktop-cdsngvk\may1;Initial Catalog=quanlybanxe;Integrated Security=True"; string sql;
        SqlConnection ketnoi;
        SqlCommand thuchien;
        SqlDataReader docdulieu;
        int i = 0;

        public fNV()
        {
            InitializeComponent();
        }

       

        private void fNV_Load(object sender, EventArgs e)
        {
            ketnoi = new SqlConnection(chuoiketnoi);
            hienthi();
        }
        public void hienthi()
        {
            lvnhanvien.Items.Clear();
            ketnoi.Open();
            sql = @"SELECT * FROM nhanvien";
            thuchien = new SqlCommand(sql, ketnoi);
            docdulieu = thuchien.ExecuteReader();
            i = 0;
            while (docdulieu.Read())
            {
                lvnhanvien.Items.Add(docdulieu[0].ToString());
                lvnhanvien.Items[i].SubItems.Add(docdulieu[1].ToString());
                lvnhanvien.Items[i].SubItems.Add(docdulieu[2].ToString());
                lvnhanvien.Items[i].SubItems.Add(docdulieu[3].ToString());
                lvnhanvien.Items[i].SubItems.Add(docdulieu[4].ToString());
                lvnhanvien.Items[i].SubItems.Add(docdulieu[5].ToString());
                lvnhanvien.Items[i].SubItems.Add(docdulieu[6].ToString());
                lvnhanvien.Items[i].SubItems.Add(docdulieu[7].ToString());
                i++;
            }
            ketnoi.Close();
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
        }
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lvnhanvien_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtmanv.Text = lvnhanvien.SelectedItems[0].SubItems[0].Text;
                txttennv.Text = lvnhanvien.SelectedItems[0].SubItems[1].Text;
                txtnamsinh.Text = lvnhanvien.SelectedItems[0].SubItems[2].Text;
                txtgioitinh.Text = lvnhanvien.SelectedItems[0].SubItems[3].Text;
                txtdiachi.Text = lvnhanvien.SelectedItems[0].SubItems[4].Text;
                txtchucvu.Text = lvnhanvien.SelectedItems[0].SubItems[5].Text;
                txtluongcb.Text = lvnhanvien.SelectedItems[0].SubItems[6].Text;
                txtdt.Text = lvnhanvien.SelectedItems[0].SubItems[7].Text;
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
                txtmanv.Enabled = false;
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
                lvnhanvien.Items.Clear();
                ketnoi.Open();
                sql = "delete from nhanvien where manv= @manv";
                thuchien = new SqlCommand(sql, ketnoi);
                thuchien.Parameters.AddWithValue("manv", txtmanv.Text);
                thuchien.ExecuteNonQuery();
                ketnoi.Close();
                txtmanv.Enabled = true;
                btnThem.Enabled = true;
                hienthi();
            }
            XoaThongTin();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            lvnhanvien.Items.Clear();
            ketnoi.Open();
            sql = "insert into nhanvien(manv, tennv, namsinh, gioitinh,diachi,chucvu,luongcb,dt) " +
                "values(@manv,@tennv,@namsinh,@gioitinh,@diachi,@chucvu,@luongcb,@dt)";
            thuchien = new SqlCommand(sql, ketnoi);
            thuchien.Parameters.AddWithValue("manv", txtmanv.Text);
            thuchien.Parameters.AddWithValue("tennv", txttennv.Text);
            thuchien.Parameters.AddWithValue("namsinh", txtnamsinh.Text);
            thuchien.Parameters.AddWithValue("gioitinh", txtgioitinh.Text);
            thuchien.Parameters.AddWithValue("diachi", txtdiachi.Text);
            thuchien.Parameters.AddWithValue("chucvu", txtchucvu.Text);
            thuchien.Parameters.AddWithValue("luongcb", txtluongcb.Text);
            thuchien.Parameters.AddWithValue("dt", txtdt.Text);
            thuchien.ExecuteNonQuery();
            ketnoi.Close();
            hienthi();
            XoaThongTin();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            lvnhanvien.Items.Clear();
            ketnoi.Open();
            sql = "update nhanvien set tennv=@tennv,namsinh=@namsinh,gioitinh=@gioitinh,diachi=@diachi,chucvu=@chucvu,luongcb=@luongcb,dt=@dt where manv=@manv";
            thuchien = new SqlCommand(sql, ketnoi);
            thuchien.Parameters.AddWithValue("manv", txtmanv.Text);
            thuchien.Parameters.AddWithValue("tennv", txttennv.Text);
            thuchien.Parameters.AddWithValue("namsinh", txtnamsinh.Text);
            thuchien.Parameters.AddWithValue("gioitinh", txtgioitinh.Text);
            thuchien.Parameters.AddWithValue("diachi", txtdiachi.Text);
            thuchien.Parameters.AddWithValue("chucvu", txtchucvu.Text);
            thuchien.Parameters.AddWithValue("luongcb", txtluongcb.Text);
            thuchien.Parameters.AddWithValue("dt", txtdt.Text);

            thuchien.ExecuteNonQuery();
            ketnoi.Close();
            txtmanv.Enabled = true;
            btnThem.Enabled = true;
            hienthi();
            XoaThongTin();
        }
        private void XoaThongTin()
        {
            txtmanv.Text = "";
            txttennv.Text = "";
            txtnamsinh.Text = "";
            txtgioitinh.Text = "";
            txtdiachi.Text = "";
            txtchucvu.Text = "";
            txtluongcb.Text = "";
            txtdt.Text = "";
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
            exTitle.Value = "DANH SÁCH NHÂN VIÊN";

            //In tiêu đề
            exSheet.Range["A6:I6"].Font.Size = 14;
            exSheet.Range["A6:I6"].Font.Bold = true;
            exSheet.Range["A6:i6"].Borders.LineStyle = Excel.Constants.xlSolid;
            exSheet.Range["A6:I6"].Interior.Color = Color.Aqua;

            exSheet.Range["A6"].Value = "STT";
            exSheet.Range["A6"].ColumnWidth = 5;

            exSheet.Range["B6"].Value = "Mã nhân viên";
            exSheet.Range["B6"].ColumnWidth = 10;

            exSheet.Range["C6"].Value = "Tên nhân viên";
            exSheet.Range["C6"].ColumnWidth = 20;

            exSheet.Range["D6"].Value = "Năm sinh";
            exSheet.Range["D6"].ColumnWidth = 20;

            exSheet.Range["E6"].Value = "Giới tính";
            exSheet.Range["E6"].ColumnWidth = 10;

            exSheet.Range["F6"].Value = "Địa chỉ";
            exSheet.Range["F6"].ColumnWidth = 30;

            exSheet.Range["G6"].Value = "Chức vụ";
            exSheet.Range["G6"].ColumnWidth = 20;

            exSheet.Range["H6"].Value = "Lương cơ bản";
            exSheet.Range["H6"].ColumnWidth = 10;

            exSheet.Range["I6"].Value = "Số điện thoại";
            exSheet.Range["I6"].ColumnWidth = 30;

            //In nội dung
            int dong = 7;
            for (int i = 0; i < lvnhanvien.Items.Count - 1; i++)
            {
                string dongs = (dong + i).ToString();
                exSheet.Range["A" + dongs].Value = (i + 1).ToString();
                exSheet.Range["B" + dongs].Value = lvnhanvien.Items[i].SubItems[0].Text;
                exSheet.Range["C" + dongs].Value = lvnhanvien.Items[i].SubItems[1].Text;
                exSheet.Range["D" + dongs].Value = lvnhanvien.Items[i].SubItems[2].Text;
                exSheet.Range["E" + dongs].Value = lvnhanvien.Items[i].SubItems[3].Text;
                exSheet.Range["F" + dongs].Value = lvnhanvien.Items[i].SubItems[4].Text;
                exSheet.Range["G" + dongs].Value = lvnhanvien.Items[i].SubItems[5].Text;
                exSheet.Range["H" + dongs].Value = lvnhanvien.Items[i].SubItems[6].Text;
                exSheet.Range["I" + dongs].Value = lvnhanvien.Items[i].SubItems[7].Text;
                exSheet.Range["A" + dongs + ":" + "I" + dongs].Borders.LineStyle = Excel.Constants.xlSolid;
                exSheet.Range["A" + dongs + ":" + "C" + dongs].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            }
            exSheet.Name = "Nhân viên";
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
