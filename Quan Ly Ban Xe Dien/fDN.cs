
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
    public partial class fDN : Form
    {
        
        public fDN()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=desktop-cdsngvk\may1;Initial Catalog=quanlybanxe;Integrated Security=True;Encrypt=False");
            try
            {
                con.Open();
                string tk = txttaikhoan.Text;
                string mk = txtmatkhau.Text;
                string sql = "Select * from taikhoan where tk='" + tk + "' and mk='" + mk + "'";
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader dta = cmd.ExecuteReader();
                if (dta.Read() == true)
                {
                    MessageBox.Show("Đăng nhập thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                    fMain mainForm = new fMain();
                    mainForm.ShowDialog();
                    this.Close();
                }
                else if (txttaikhoan.Text == "" || txtmatkhau.Text == "")
                {
                    MessageBox.Show("Nhập Tài khoản hoặc Mật khẩu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (dta.Read() == false)
                {
                    MessageBox.Show("Sai tài khoản hoặc mật khẩu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void fDN_Load(object sender, EventArgs e)
        {

        }

        private void btntaotk_Click(object sender, EventArgs e)
        {
            ftaotk f = new ftaotk();

            f.ShowDialog();
        }
    }
}
