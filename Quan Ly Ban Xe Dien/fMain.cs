using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quan_Ly_Ban_Xe_Dien
{
    public partial class fMain : Form
    {
        public fMain()
        {
            InitializeComponent();
        }

        private void đăngNhậpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fDN f = new fDN();
            f.ShowDialog();
        }

        private void cấuHìnhToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cậpNhậtPhòngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fKH f = new fKH();
            f.ShowDialog();
        }

        private void thôngTinPhòngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fNV f = new fNV();
            f.ShowDialog();
        }

        private void quảnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fNCC f = new fNCC();
            f.ShowDialog();
        }

        private void quảnLýMặtHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fmathang f = new fmathang();
            f.ShowDialog();
        }

        
        private void cậpNhậtKháchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fPNhap f = new fPNhap();
            f.ShowDialog();
        }

        private void lậpPhiếuXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fPXuat f = new fPXuat();
            f.ShowDialog();
        }

        private void menuChinh_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void fMain_Load(object sender, EventArgs e)
        {

        }

        private void báoCáoNgàyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            baocao f = new baocao();
            f.ShowDialog();
        }
    }
}
