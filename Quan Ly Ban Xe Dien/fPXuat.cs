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
    public partial class fPXuat : Form
    {
        string chuoiketnoi = @"Data Source=desktop-cdsngvk\may1;Initial Catalog=quanlybanxe;Integrated Security=True"; string sql;
        SqlConnection ketnoi;
        SqlCommand thuchien;
        SqlDataReader docdulieu;
        int i = 0;
        public fPXuat()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        

        private void fPXuat_Load(object sender, EventArgs e)
        {
            ketnoi = new SqlConnection(chuoiketnoi);
            hienthi();
        }
        public void hienthi()
        {
            lvpxuat.Items.Clear();
            ketnoi.Open();
            sql = @"SELECT * FROM lapphieuxuat";
            thuchien = new SqlCommand(sql, ketnoi);
            docdulieu = thuchien.ExecuteReader();
            i = 0;
            while (docdulieu.Read())
            {
                lvpxuat.Items.Add(docdulieu[0].ToString());
                lvpxuat.Items[i].SubItems.Add(docdulieu[1].ToString());
                lvpxuat.Items[i].SubItems.Add(docdulieu[2].ToString());
                lvpxuat.Items[i].SubItems.Add(docdulieu[3].ToString());
                lvpxuat.Items[i].SubItems.Add(docdulieu[4].ToString());
                lvpxuat.Items[i].SubItems.Add(docdulieu[5].ToString());
                lvpxuat.Items[i].SubItems.Add(docdulieu[6].ToString());
                lvpxuat.Items[i].SubItems.Add(docdulieu[7].ToString());
                i++;
            }
            ketnoi.Close();
            
        }
        private void btnThoat_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
        private void lvpxuat_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtmapx.Text = lvpxuat.SelectedItems[0].SubItems[0].Text;
                txtmaxe.Text = lvpxuat.SelectedItems[0].SubItems[1].Text;
                txtngayxuat.Text = lvpxuat.SelectedItems[0].SubItems[2].Text;
                txtmanv.Text = lvpxuat.SelectedItems[0].SubItems[3].Text;
                txtmakh.Text = lvpxuat.SelectedItems[0].SubItems[4].Text;
                txtslxuat.Text = lvpxuat.SelectedItems[0].SubItems[5].Text;
                txtgiaxuat.Text = lvpxuat.SelectedItems[0].SubItems[6].Text;
                txtthue.Text = lvpxuat.SelectedItems[0].SubItems[7].Text;
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
                txtmapx.Enabled = false;
            }
            catch
            {

            }
        }
        
        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void txtmapn_TextChanged(object sender, EventArgs e)
        {

        }
        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
        private void XoaThongTin()
        {
            txtmapx.Text = "";
            txtmaxe.Text = "";
            txtngayxuat.Text = "";
            txtmanv.Text = "";
            txtmakh.Text = "";
            txtslxuat.Text = "";
            txtgiaxuat.Text = "";
            txtthue.Text = "";
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            lvpxuat.Items.Clear();
            ketnoi.Open();
            sql = "insert into lapphieuxuat(mapx, maxe, ngayxuat, manv,makh,slxuat,giaxuat,thue) " +
                "values(@mapx, @maxe, @ngayxuat, @manv,@makh,@slxuat,@giaxuat,@thue)";
            thuchien = new SqlCommand(sql, ketnoi);
            thuchien.Parameters.AddWithValue("mapx", txtmapx.Text);
            thuchien.Parameters.AddWithValue("maxe", txtmaxe.Text);
            thuchien.Parameters.AddWithValue("ngayxuat", txtngayxuat.Text);
            thuchien.Parameters.AddWithValue("manv", txtmanv.Text);
            thuchien.Parameters.AddWithValue("makh", txtmakh.Text);
            thuchien.Parameters.AddWithValue("slxuat", txtslxuat.Text);
            thuchien.Parameters.AddWithValue("giaxuat", txtgiaxuat.Text);
            thuchien.Parameters.AddWithValue("thue", txtthue.Text);
            thuchien.ExecuteNonQuery();
            ketnoi.Close();
            hienthi();
            XoaThongTin();
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            lvpxuat.Items.Clear();
            ketnoi.Open();
            sql = @"SELECT * FROM lapphieuxuat where (mapx like @mapx) and (manv like @manv) and (makh like @makh) and (maxe like @maxe)";
            thuchien = new SqlCommand(sql, ketnoi);
            thuchien.Parameters.Add("@mapx", SqlDbType.NChar);
            thuchien.Parameters["@mapx"].Value = "%" + txtmapx.Text + "%";

            thuchien.Parameters.Add("@manv", SqlDbType.NChar);
            thuchien.Parameters["@manv"].Value = "%" + txtmanv.Text + "%";

            thuchien.Parameters.Add("@makh", SqlDbType.NVarChar);
            thuchien.Parameters["@makh"].Value = "%" + txtmakh.Text + "%";

            thuchien.Parameters.Add("@maxe", SqlDbType.NVarChar);
            thuchien.Parameters["@maxe"].Value = "%" + txtmaxe.Text + "%";
            docdulieu = thuchien.ExecuteReader();
            i = 0;
            while (docdulieu.Read())
            {
                lvpxuat.Items.Add(docdulieu[0].ToString());
                lvpxuat.Items[i].SubItems.Add(docdulieu[1].ToString());
                lvpxuat.Items[i].SubItems.Add(docdulieu[2].ToString());
                lvpxuat.Items[i].SubItems.Add(docdulieu[3].ToString());
                lvpxuat.Items[i].SubItems.Add(docdulieu[4].ToString());
                lvpxuat.Items[i].SubItems.Add(docdulieu[5].ToString());
                lvpxuat.Items[i].SubItems.Add(docdulieu[6].ToString());
                lvpxuat.Items[i].SubItems.Add(docdulieu[7].ToString());
                i++;
            }
            ketnoi.Close();
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            lvpxuat.Items.Clear();
            ketnoi.Open();
            sql = "update lapphieuxuat set maxe=@maxe,ngayxuat=@ngayxuat,manv=@manv,makh=@makh,slxuat=@slxuat,giaxuat=@giaxuat,thue=@thue where mapx=@mapx";
            thuchien = new SqlCommand(sql, ketnoi);
            thuchien.Parameters.AddWithValue("mapx", txtmapx.Text);
            thuchien.Parameters.AddWithValue("maxe", txtmaxe.Text);
            thuchien.Parameters.AddWithValue("ngayxuat", txtngayxuat.Text);
            thuchien.Parameters.AddWithValue("manv", txtmanv.Text);
            thuchien.Parameters.AddWithValue("makh", txtmakh.Text);
            thuchien.Parameters.AddWithValue("slxuat", txtslxuat.Text);
            thuchien.Parameters.AddWithValue("giaxuat", txtgiaxuat.Text);
            thuchien.Parameters.AddWithValue("thue", txtthue.Text);

            thuchien.ExecuteNonQuery();
            ketnoi.Close();
            txtmapx.Enabled = true;
            btnThem.Enabled = true;
            hienthi();
            XoaThongTin();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult d = MessageBox.Show("Bạn có muốn xóa hay không?", "Thông báo",
                MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (d == DialogResult.Yes)
            {
                lvpxuat.Items.Clear();
                ketnoi.Open();
                sql = "delete from lapphieuxuat where mapx= @mapx";
                thuchien = new SqlCommand(sql, ketnoi);
                thuchien.Parameters.AddWithValue("mapx", txtmapx.Text);
                thuchien.ExecuteNonQuery();
                ketnoi.Close();
                txtmapx.Enabled = true;
                btnThem.Enabled = true;
                hienthi();
            }
            XoaThongTin();
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
            exTitle.Value = "THÔNG TIN XUẤT KHO";

            //In tiêu đề
            exSheet.Range["A6:I6"].Font.Size = 14;
            exSheet.Range["A6:I6"].Font.Bold = true;
            exSheet.Range["A6:i6"].Borders.LineStyle = Excel.Constants.xlSolid;
            exSheet.Range["A6:I6"].Interior.Color = Color.Aqua;

            exSheet.Range["A6"].Value = "STT";
            exSheet.Range["A6"].ColumnWidth = 5;

            exSheet.Range["B6"].Value = "Mã phiếu xuất";
            exSheet.Range["B6"].ColumnWidth = 10;

            exSheet.Range["C6"].Value = "Mã xe";
            exSheet.Range["C6"].ColumnWidth = 20;

            exSheet.Range["D6"].Value = "Ngày xuất";
            exSheet.Range["D6"].ColumnWidth = 20;

            exSheet.Range["E6"].Value = "Mã nhân viên";
            exSheet.Range["E6"].ColumnWidth = 10;

            exSheet.Range["F6"].Value = "Mã khách hàng";
            exSheet.Range["F6"].ColumnWidth = 30;

            exSheet.Range["G6"].Value = "Số lượng xuất";
            exSheet.Range["G6"].ColumnWidth = 20;

            exSheet.Range["H6"].Value = "Giá xuất";
            exSheet.Range["H6"].ColumnWidth = 10;

            exSheet.Range["I6"].Value = "Thuế";
            exSheet.Range["I6"].ColumnWidth = 30;

            //In nội dung
            int dong = 7;
            for (int i = 0; i < lvpxuat.Items.Count - 1; i++)
            {
                string dongs = (dong + i).ToString();
                exSheet.Range["A" + dongs].Value = (i + 1).ToString();
                exSheet.Range["B" + dongs].Value = lvpxuat.Items[i].SubItems[0].Text;
                exSheet.Range["C" + dongs].Value = lvpxuat.Items[i].SubItems[1].Text;
                exSheet.Range["D" + dongs].Value = lvpxuat.Items[i].SubItems[2].Text;
                exSheet.Range["E" + dongs].Value = lvpxuat.Items[i].SubItems[3].Text;
                exSheet.Range["F" + dongs].Value = lvpxuat.Items[i].SubItems[4].Text;
                exSheet.Range["G" + dongs].Value = lvpxuat.Items[i].SubItems[5].Text;
                exSheet.Range["H" + dongs].Value = lvpxuat.Items[i].SubItems[6].Text;
                exSheet.Range["I" + dongs].Value = lvpxuat.Items[i].SubItems[7].Text;
                exSheet.Range["A" + dongs + ":" + "I" + dongs].Borders.LineStyle = Excel.Constants.xlSolid;
                exSheet.Range["A" + dongs + ":" + "C" + dongs].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            }
            exSheet.Name = "Xuất kho";
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
