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
    public partial class fmathang : Form
    {
        string chuoiketnoi = @"Data Source=desktop-cdsngvk\may1;Initial Catalog=quanlybanxe;Integrated Security=True"; string sql;
        SqlConnection ketnoi;
        SqlCommand thuchien;
        SqlDataReader docdulieu;
        int i = 0;
        public fmathang()
        {
            InitializeComponent();
        }

        private void fmathang_Load(object sender, EventArgs e)
        {
            ketnoi = new SqlConnection(chuoiketnoi);
            hienthi();
        }
        public void hienthi()
        {
            lvmathang.Items.Clear();
            ketnoi.Open();
            sql = @"SELECT * FROM mathang";
            thuchien = new SqlCommand(sql, ketnoi);
            docdulieu = thuchien.ExecuteReader();
            i = 0;
            while (docdulieu.Read())
            {
                lvmathang.Items.Add(docdulieu[0].ToString());
                lvmathang.Items[i].SubItems.Add(docdulieu[1].ToString());
                lvmathang.Items[i].SubItems.Add(docdulieu[2].ToString());
                lvmathang.Items[i].SubItems.Add(docdulieu[3].ToString());
                lvmathang.Items[i].SubItems.Add(docdulieu[4].ToString());
                lvmathang.Items[i].SubItems.Add(docdulieu[5].ToString());
                lvmathang.Items[i].SubItems.Add(docdulieu[6].ToString());
                lvmathang.Items[i].SubItems.Add(docdulieu[7].ToString());
                i++;
            }
            ketnoi.Close();
           
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lvmathang_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtmaxe.Text = lvmathang.SelectedItems[0].SubItems[0].Text;
                txtmauxe.Text = lvmathang.SelectedItems[0].SubItems[2].Text;
                txtloaixe.Text = lvmathang.SelectedItems[0].SubItems[1].Text;
                txtslxe.Text = lvmathang.SelectedItems[0].SubItems[3].Text;
                txtslacquy.Text = lvmathang.SelectedItems[0].SubItems[4].Text;
                txtnhasx.Text = lvmathang.SelectedItems[0].SubItems[5].Text;
                txtttbaohanh.Text = lvmathang.SelectedItems[0].SubItems[6].Text;
                txtgia.Text = lvmathang.SelectedItems[0].SubItems[7].Text;
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
                txtmaxe.Enabled = false;
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
                lvmathang.Items.Clear();
                ketnoi.Open();
                sql = "delete from mathang where maxe= @maxe";
                thuchien = new SqlCommand(sql, ketnoi);
                thuchien.Parameters.AddWithValue("maxe", txtmaxe.Text);
                thuchien.ExecuteNonQuery();
                ketnoi.Close();
                txtmaxe.Enabled = true;
                btnThem.Enabled = true;
                hienthi();
            }
            XoaThongTin();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            lvmathang.Items.Clear();
            ketnoi.Open();
            sql = "insert into mathang(maxe, loaixe, mauxe, slacquy,slxe,nhasx,ttbaohanh,gia) " +
                "values(@maxe, @loaixe, @mauxe, @slacquy,@slxe,@nhasx,@ttbaohanh,@gia)";
            thuchien = new SqlCommand(sql, ketnoi);
            thuchien.Parameters.AddWithValue("maxe", txtmaxe.Text);
            thuchien.Parameters.AddWithValue("loaixe", txtloaixe.Text);
            thuchien.Parameters.AddWithValue("mauxe", txtmauxe.Text);
            thuchien.Parameters.AddWithValue("slacquy", txtslacquy.Text);
            thuchien.Parameters.AddWithValue("slxe", txtslxe.Text);
            thuchien.Parameters.AddWithValue("nhasx", txtnhasx.Text);
            thuchien.Parameters.AddWithValue("ttbaohanh", txtttbaohanh.Text);
            thuchien.Parameters.AddWithValue("gia", txtgia.Text);
            thuchien.ExecuteNonQuery();
            ketnoi.Close();
            hienthi();
            XoaThongTin();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            lvmathang.Items.Clear();
            ketnoi.Open();
            sql = "update mathang set loaixe=@loaixe,mauxe=@mauxe,slacquy=@slacquy,slxe=@slxe,nhasx=@nhasx,ttbaohanh=@ttbaohanh,gia=@gia where maxe=@maxe";
            thuchien = new SqlCommand(sql, ketnoi);
            thuchien.Parameters.AddWithValue("maxe", txtmaxe.Text);
            thuchien.Parameters.AddWithValue("loaixe", txtloaixe.Text);
            thuchien.Parameters.AddWithValue("mauxe", txtmauxe.Text);
            thuchien.Parameters.AddWithValue("slacquy", txtslacquy.Text);
            thuchien.Parameters.AddWithValue("slxe", txtslxe.Text);
            thuchien.Parameters.AddWithValue("nhasx", txtnhasx.Text);
            thuchien.Parameters.AddWithValue("ttbaohanh", txtttbaohanh.Text);
            thuchien.Parameters.AddWithValue("gia", txtgia.Text);
            thuchien.ExecuteNonQuery();
            ketnoi.Close();
            txtmaxe.Enabled = true;
            btnThem.Enabled = true;
            hienthi();
            XoaThongTin();
        }
        private void XoaThongTin()
        {
            txtmaxe.Text = "";
            txtmauxe.Text = "";
            txtloaixe.Text = "";
            txtttbaohanh.Text = "";
            txtslacquy.Text = "";
            txtnhasx.Text = "";
            txtslxe.Text = "";
            txtgia.Text = "";
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
            exTitle.Value = "DANH SÁCH MẶT HÀNG";

            //In tiêu đề
            exSheet.Range["A6:I6"].Font.Size = 14;
            exSheet.Range["A6:I6"].Font.Bold = true;
            exSheet.Range["A6:i6"].Borders.LineStyle = Excel.Constants.xlSolid;
            exSheet.Range["A6:I6"].Interior.Color = Color.Aqua;

            exSheet.Range["A6"].Value = "STT";
            exSheet.Range["A6"].ColumnWidth = 5;

            exSheet.Range["B6"].Value = "Mã xe";
            exSheet.Range["B6"].ColumnWidth = 10;

            exSheet.Range["C6"].Value = "Loại xe";
            exSheet.Range["C6"].ColumnWidth = 20;

            exSheet.Range["D6"].Value = "Màu xe";
            exSheet.Range["D6"].ColumnWidth = 20;

            exSheet.Range["E6"].Value = "Số lượng xe";
            exSheet.Range["E6"].ColumnWidth = 10;

            exSheet.Range["F6"].Value = "Số lượng ắc quy";
            exSheet.Range["F6"].ColumnWidth = 30;

            exSheet.Range["G6"].Value = "Nhà sản xuất";
            exSheet.Range["G6"].ColumnWidth = 20;

            exSheet.Range["H6"].Value = "TT bảo hành";
            exSheet.Range["H6"].ColumnWidth = 10;

            exSheet.Range["I6"].Value = "Giá";
            exSheet.Range["I6"].ColumnWidth = 30;

            //In nội dung
            int dong = 7;
            for (int i = 0; i < lvmathang.Items.Count - 1; i++)
            {
                string dongs = (dong + i).ToString();
                exSheet.Range["A" + dongs].Value = (i + 1).ToString();
                exSheet.Range["B" + dongs].Value = lvmathang.Items[i].SubItems[0].Text;
                exSheet.Range["C" + dongs].Value = lvmathang.Items[i].SubItems[1].Text;
                exSheet.Range["D" + dongs].Value = lvmathang.Items[i].SubItems[2].Text;
                exSheet.Range["E" + dongs].Value = lvmathang.Items[i].SubItems[3].Text;
                exSheet.Range["F" + dongs].Value = lvmathang.Items[i].SubItems[4].Text;
                exSheet.Range["G" + dongs].Value = lvmathang.Items[i].SubItems[5].Text;
                exSheet.Range["H" + dongs].Value = lvmathang.Items[i].SubItems[6].Text;
                exSheet.Range["I" + dongs].Value = lvmathang.Items[i].SubItems[7].Text;
                exSheet.Range["A" + dongs + ":" + "I" + dongs].Borders.LineStyle = Excel.Constants.xlSolid;
                exSheet.Range["A" + dongs + ":" + "C" + dongs].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            }
            exSheet.Name = "Mặt hàng";
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
