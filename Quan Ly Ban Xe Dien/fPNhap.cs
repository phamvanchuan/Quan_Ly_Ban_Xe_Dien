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
using Quan_Ly_Ban_Xe_Dien;

namespace Quan_Ly_Ban_Xe_Dien
{
    public partial class fPNhap : Form
    {
        string chuoiketnoi = @"Data Source=desktop-cdsngvk\may1;Initial Catalog=quanlybanxe;Integrated Security=True"; string sql;
        SqlConnection ketnoi;
        SqlCommand thuchien;
        SqlDataReader docdulieu;
        int i = 0;
        public fPNhap()
        {
            InitializeComponent();
        }

        

        private void fPNhap_Load(object sender, EventArgs e)
        {
            ketnoi = new SqlConnection(chuoiketnoi);
            hienthi();
        }
        public void hienthi()
        {
            lvpnhap.Items.Clear();
            ketnoi.Open();
            sql = @"SELECT * FROM lapphieunhap ";
            thuchien = new SqlCommand(sql, ketnoi);
            docdulieu = thuchien.ExecuteReader();

            i = 0;
            while (docdulieu.Read())
            {
                lvpnhap.Items.Add(docdulieu[0].ToString());
                lvpnhap.Items[i].SubItems.Add(docdulieu[1].ToString());
                lvpnhap.Items[i].SubItems.Add(docdulieu[2].ToString());
                lvpnhap.Items[i].SubItems.Add(docdulieu[3].ToString());
                lvpnhap.Items[i].SubItems.Add(docdulieu[4].ToString());
                lvpnhap.Items[i].SubItems.Add(docdulieu[5].ToString());
                lvpnhap.Items[i].SubItems.Add(docdulieu[6].ToString());
                lvpnhap.Items[i].SubItems.Add(docdulieu[7].ToString());
                i++;
            }
            ketnoi.Close();

        }
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
        private void txtmancc_TextChanged(object sender, EventArgs e)
        {

        }
        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void btntimkiem_Click(object sender, EventArgs e)
        {
            
                
            

        }
        private void label8_Click(object sender, EventArgs e)
        {

        }     
        private void btnThem_Click(object sender, EventArgs e)
        {
            lvpnhap.Items.Clear();
            ketnoi.Open();
            sql = "insert into lapphieunhap(mapn, maxe, ngaynhap, manv,mancc,slnhap,gianhap,thue) " +
                "values(@mapn, @maxe, @ngaynhap, @manv,@mancc,@slnhap,@gianhap,@thue)";
            thuchien = new SqlCommand(sql, ketnoi);
            thuchien.Parameters.AddWithValue("mapn", txtmapn.Text);
            thuchien.Parameters.AddWithValue("maxe", txtmaxe.Text);
            thuchien.Parameters.AddWithValue("ngaynhap", txtngaynhap.Text);
            thuchien.Parameters.AddWithValue("manv", txtmanv.Text);
            thuchien.Parameters.AddWithValue("mancc", txtmancc.Text);
            thuchien.Parameters.AddWithValue("slnhap", txtslnhap.Text);
            thuchien.Parameters.AddWithValue("gianhap", txtgianhap.Text);
            thuchien.Parameters.AddWithValue("thue", txtthue.Text);
            thuchien.ExecuteNonQuery();
            ketnoi.Close();
            hienthi();
            XoaThongTin();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            lvpnhap.Items.Clear();
            ketnoi.Open();
            sql = "update lapphieunhap set maxe=@maxe,ngaynhap=@ngaynhap,manv=@manv,mancc=@mancc,slnhap=@slnhap,gianhap=@gianhap,thue=@thue where mapn=@mapn ";
            thuchien = new SqlCommand(sql, ketnoi);
            thuchien.Parameters.AddWithValue("mapn", txtmapn.Text);
            thuchien.Parameters.AddWithValue("maxe", txtmaxe.Text);
            thuchien.Parameters.AddWithValue("ngaynhap", txtngaynhap.Text);
            thuchien.Parameters.AddWithValue("manv", txtmanv.Text);
            thuchien.Parameters.AddWithValue("mancc", txtmancc.Text);
            thuchien.Parameters.AddWithValue("slnhap", txtslnhap.Text);
            thuchien.Parameters.AddWithValue("gianhap", txtgianhap.Text);
            thuchien.Parameters.AddWithValue("thue", txtthue.Text);
            thuchien.ExecuteNonQuery();
            ketnoi.Close();
            txtmapn.Enabled = true;
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
                lvpnhap.Items.Clear();
                ketnoi.Open();
                sql = "delete from lapphieunhap where mapn= @mapn";
                thuchien = new SqlCommand(sql, ketnoi);
                thuchien.Parameters.AddWithValue("mapn", txtmapn.Text);
                thuchien.ExecuteNonQuery();
                ketnoi.Close();
                txtmapn.Enabled = true;
                btnThem.Enabled = true;
                hienthi();
            }
            XoaThongTin();
        }
        private void XoaThongTin()
        {
            txtmapn.Text = "";
            txtmaxe.Text = "";
            txtngaynhap.Text = "";
            txtmanv.Text = "";
            txtmancc.Text = "";
            txtslnhap.Text = "";
            txtgianhap.Text = "";
            txtthue.Text = "";
        }
        private void lvpnhap_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtmapn.Text = lvpnhap.SelectedItems[0].SubItems[0].Text;
                txtmaxe.Text = lvpnhap.SelectedItems[0].SubItems[1].Text;
                txtngaynhap.Text = lvpnhap.SelectedItems[0].SubItems[2].Text;
                txtmanv.Text = lvpnhap.SelectedItems[0].SubItems[3].Text;
                txtmancc.Text = lvpnhap.SelectedItems[0].SubItems[4].Text;
                txtslnhap.Text = lvpnhap.SelectedItems[0].SubItems[5].Text;
                txtgianhap.Text = lvpnhap.SelectedItems[0].SubItems[6].Text;
                txtthue.Text = lvpnhap.SelectedItems[0].SubItems[7].Text;
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
                txtmapn.Enabled = false;
            }
            catch
            {

            }
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            lvpnhap.Items.Clear();
            ketnoi.Open();
            sql = @"SELECT * FROM lapphieunhap where (mapn like @mapn) and (manv like @manv) and (mancc like @mancc) and (maxe like @maxe)";
            thuchien = new SqlCommand(sql, ketnoi);
            thuchien.Parameters.Add("@mapn", SqlDbType.NChar);
            thuchien.Parameters["@mapn"].Value = "%" + txtmapn.Text + "%";

            thuchien.Parameters.Add("@manv", SqlDbType.NChar);
            thuchien.Parameters["@manv"].Value = "%" + txtmanv.Text + "%";

            thuchien.Parameters.Add("@mancc", SqlDbType.NVarChar);
            thuchien.Parameters["@mancc"].Value = "%" + txtmancc.Text + "%";

            thuchien.Parameters.Add("@maxe", SqlDbType.NVarChar);
            thuchien.Parameters["@maxe"].Value = "%" + txtmaxe.Text + "%";
            docdulieu = thuchien.ExecuteReader();
            i = 0;
            while (docdulieu.Read())
            {
                lvpnhap.Items.Add(docdulieu[0].ToString());
                lvpnhap.Items[i].SubItems.Add(docdulieu[1].ToString());
                lvpnhap.Items[i].SubItems.Add(docdulieu[2].ToString());
                lvpnhap.Items[i].SubItems.Add(docdulieu[3].ToString());
                lvpnhap.Items[i].SubItems.Add(docdulieu[4].ToString());
                lvpnhap.Items[i].SubItems.Add(docdulieu[5].ToString());
                lvpnhap.Items[i].SubItems.Add(docdulieu[6].ToString());
                lvpnhap.Items[i].SubItems.Add(docdulieu[7].ToString());
                i++;
            }
            ketnoi.Close();
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
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
            exTitle.Value = "THÔNG TIN NHẬP KHO";

            //In tiêu đề
            exSheet.Range["A6:I6"].Font.Size = 14;
            exSheet.Range["A6:I6"].Font.Bold = true;
            exSheet.Range["A6:i6"].Borders.LineStyle = Excel.Constants.xlSolid;
            exSheet.Range["A6:I6"].Interior.Color = Color.Aqua;

            exSheet.Range["A6"].Value = "STT";
            exSheet.Range["A6"].ColumnWidth = 5;

            exSheet.Range["B6"].Value = "Mã phiếu nhập";
            exSheet.Range["B6"].ColumnWidth = 10;

            exSheet.Range["C6"].Value = "Mã xe";
            exSheet.Range["C6"].ColumnWidth = 20;

            exSheet.Range["D6"].Value = "Ngày nhập";
            exSheet.Range["D6"].ColumnWidth = 20;

            exSheet.Range["E6"].Value = "Mã nhân viên";
            exSheet.Range["E6"].ColumnWidth = 10;

            exSheet.Range["F6"].Value = "Mã ncc";
            exSheet.Range["F6"].ColumnWidth = 30;

            exSheet.Range["G6"].Value = "Số lượng nhập";
            exSheet.Range["G6"].ColumnWidth = 20;

            exSheet.Range["H6"].Value = "Giá nhập";
            exSheet.Range["H6"].ColumnWidth = 10;

            exSheet.Range["I6"].Value = "Thuế";
            exSheet.Range["I6"].ColumnWidth = 30;

            //In nội dung
            int dong = 7;
            for (int i = 0; i < lvpnhap.Items.Count - 1; i++)
            {
                string dongs = (dong + i).ToString();
                exSheet.Range["A" + dongs].Value = (i + 1).ToString();
                exSheet.Range["B" + dongs].Value = lvpnhap.Items[i].SubItems[0].Text;
                exSheet.Range["C" + dongs].Value = lvpnhap.Items[i].SubItems[1].Text;
                exSheet.Range["D" + dongs].Value = lvpnhap.Items[i].SubItems[2].Text;
                exSheet.Range["E" + dongs].Value = lvpnhap.Items[i].SubItems[3].Text;
                exSheet.Range["F" + dongs].Value = lvpnhap.Items[i].SubItems[4].Text;
                exSheet.Range["G" + dongs].Value = lvpnhap.Items[i].SubItems[5].Text;
                exSheet.Range["H" + dongs].Value = lvpnhap.Items[i].SubItems[6].Text;
                exSheet.Range["I" + dongs].Value = lvpnhap.Items[i].SubItems[7].Text;
                exSheet.Range["A" + dongs + ":" + "I" + dongs].Borders.LineStyle = Excel.Constants.xlSolid;
                exSheet.Range["A" + dongs + ":" + "C" + dongs].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            }
            exSheet.Name = "Nhập kho";
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
