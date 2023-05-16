using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quanlyphongkham
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btndangnhap_Click(object sender, EventArgs e)
        {
            if ((this.txttendangnhap.Text == "Admin") && (this.txtmatkhau.Text == "HoangThuHong"))
            {
                this.Hide();
                Form2 frm = new Form2("Admin"); // truyền tên đăng nhập vào Form2
                frm.Show();
            }
            else if ((this.txttendangnhap.Text == "Nhanvien") && (this.txtmatkhau.Text == "HoangThuHong"))
            {
                this.Hide();
                Form2 frm = new Form2("Nhanvien"); // truyền tên đăng nhập vào Form2
                frm.Show();
            }
            else
            {
                MessageBox.Show("Tài khoản hoặc mật khẩu không hợp lệ!!!", "Thông báo");
                this.txttendangnhap.Focus();
            }
        }

        private void btnthoat_Click(object sender, EventArgs e)
        {
            DialogResult traloi;
            traloi = MessageBox.Show("Bạn có chắc muốn thoát không?", "Trả lời",
            MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (traloi == DialogResult.OK)
                Application.Exit();
        }
    }
}
