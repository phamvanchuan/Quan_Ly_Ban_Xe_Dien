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
    
    public partial class fNCC : Form
    {
        string chuoiketnoi = @"Data Source=desktop-cdsngvk\may1;Initial Catalog=quanlybanxe;Integrated Security=True"; string sql;
    SqlConnection ketnoi;
    SqlCommand thuchien;
    SqlDataReader docdulieu;
    int i = 0;
        public fNCC()
        {
            InitializeComponent();
        }

        

        private void fNCC_Load(object sender, EventArgs e)
        {
            ketnoi = new SqlConnection(chuoiketnoi);
            hienthi();
        }
        public void hienthi()
        {
            lvNCC.Items.Clear();
            ketnoi.Open();
            sql = @"SELECT * FROM nhacungcap";
            thuchien = new SqlCommand(sql, ketnoi);
            docdulieu = thuchien.ExecuteReader();
            i = 0;
            while (docdulieu.Read())
            {
                lvNCC.Items.Add(docdulieu[0].ToString());
                lvNCC.Items[i].SubItems.Add(docdulieu[1].ToString());
                lvNCC.Items[i].SubItems.Add(docdulieu[2].ToString());
                lvNCC.Items[i].SubItems.Add(docdulieu[3].ToString());
                lvNCC.Items[i].SubItems.Add(docdulieu[4].ToString());
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

        private void lvNCC_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtmancc.Text = lvNCC.SelectedItems[0].SubItems[0].Text;
                txttenncc.Text = lvNCC.SelectedItems[0].SubItems[1].Text;
                txtdiachi.Text = lvNCC.SelectedItems[0].SubItems[2].Text;
                txtdt.Text = lvNCC.SelectedItems[0].SubItems[3].Text;
                txtemail.Text = lvNCC.SelectedItems[0].SubItems[4].Text;
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
                txtmancc.Enabled = false;
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
                lvNCC.Items.Clear();
                ketnoi.Open();
                sql = "delete from nhacungcap where mancc= @mancc";
                thuchien = new SqlCommand(sql, ketnoi);
                thuchien.Parameters.AddWithValue("mancc", txtmancc.Text);
                thuchien.ExecuteNonQuery();
                ketnoi.Close();
                txtmancc.Enabled = true;
                btnThem.Enabled = true;
                hienthi();
            }
            XoaThongTin();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            lvNCC.Items.Clear();
            ketnoi.Open();
            sql = "insert into nhacungcap(mancc, tenncc, diachi, dt,email) " +
                "values(@mancc,@tenncc,@diachi,@dt,@email)";
            thuchien = new SqlCommand(sql, ketnoi);
            thuchien.Parameters.AddWithValue("mancc", txtmancc.Text);
            thuchien.Parameters.AddWithValue("tenncc", txttenncc.Text);
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
            lvNCC.Items.Clear();
            ketnoi.Open();
            sql = "update nhacungcap set tenncc=@tenncc,diachi=@diachi,dt=@dt,email=@email where mancc=@mancc";
            thuchien = new SqlCommand(sql, ketnoi);
            thuchien.Parameters.AddWithValue("tenncc", txttenncc.Text);
            thuchien.Parameters.AddWithValue("diachi", txtdiachi.Text);
            thuchien.Parameters.AddWithValue("dt", txtdt.Text);
            thuchien.Parameters.AddWithValue("email", txtemail.Text);
            thuchien.Parameters.AddWithValue("mancc", txtmancc.Text);

            thuchien.ExecuteNonQuery();
            ketnoi.Close();
            txtmancc.Enabled = true;
            btnThem.Enabled = true;
            hienthi();
            XoaThongTin();
        }
        private void XoaThongTin()
        {
            txtmancc.Text = "";
            txttenncc.Text = "";
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
            exTitle.Value = "DANH SÁCH NHÀ CUNG CẤP";



            //In tiêu đề
            exSheet.Range["A6:F6"].Font.Size = 14;
            exSheet.Range["A6:F6"].Font.Bold = true;
            exSheet.Range["A6:F6"].Borders.LineStyle = Excel.Constants.xlSolid;
            exSheet.Range["A6:F6"].Interior.Color = Color.Aqua;

            exSheet.Range["A6"].Value = "STT";
            exSheet.Range["A6"].ColumnWidth = 5;

            exSheet.Range["B6"].Value = "Mã nhà cung cấp";
            exSheet.Range["B6"].ColumnWidth = 10;

            exSheet.Range["C6"].Value = "Tên nhà cung cấp";
            exSheet.Range["C6"].ColumnWidth = 20;

            exSheet.Range["D6"].Value = "Địa chỉ";
            exSheet.Range["D6"].ColumnWidth = 20;

            exSheet.Range["E6"].Value = "Số điện thoại";
            exSheet.Range["E6"].ColumnWidth = 10;

            exSheet.Range["F6"].Value = "Email";
            exSheet.Range["F6"].ColumnWidth = 30;

            //In nội dung
            int dong = 7;
            for (int i = 0; i < lvNCC.Items.Count - 1; i++)
            {
                string dongs = (dong + i).ToString();
                exSheet.Range["A" + dongs].Value = (i + 1).ToString();
                exSheet.Range["B" + dongs].Value = lvNCC.Items[i].SubItems[0].Text;
                exSheet.Range["C" + dongs].Value = lvNCC.Items[i].SubItems[1].Text;
                exSheet.Range["D" + dongs].Value = lvNCC.Items[i].SubItems[2].Text;
                exSheet.Range["E" + dongs].Value = lvNCC.Items[i].SubItems[3].Text;
                exSheet.Range["F" + dongs].Value = lvNCC.Items[i].SubItems[4].Text;
                exSheet.Range["A" + dongs + ":" + "F" + dongs].Borders.LineStyle = Excel.Constants.xlSolid;
                exSheet.Range["A" + dongs + ":" + "C" + dongs].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            }
            exSheet.Name = "nhà cung cấp";
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
