using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Quanlyphongkham
{
    public partial class Form2 : Form
    {
        private string username;

        public Form2(string username)
        {
            InitializeComponent();
            this.username = username;

            // Nếu người dùng là HoangThuHong, cho phép truy cập vào tabPageNhanvien
            if (username == "Admin")
            {
                tabControl1.TabPages["tabPageNhanvien"].Enabled = true;
            }
            // Nếu người dùng là Nhanvien, không cho phép truy cập vào tabPageNhanvien
            else if (username == "Nhanvien")
            {
                tabControl1.TabPages["tabPageNhanvien"].Enabled = false;
            }
        }
        // tạo các bảng dữ liệu
        DataTable dtdv = new DataTable();
        DataTable dtnv = new DataTable();
        DataTable dthd = new DataTable();
        DataTable dtcthdon = new DataTable();
        DataTable dtbenhnhan = new DataTable();
        // khai báo biến toàn cục để xác định dòng trong datagirdview
        int dongdangchon;
        int dongdangchonnv;
        int dongdangchonpk;
        int dongdangchonkh;

        int dongia = 0;
        int thanhtien = 0;
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // kiểm tra bảng dtdv xem có dữ liệu không
            if (System.IO.File.Exists(@"dtdv.json"))
            {
                //doc file
                System.IO.StreamReader reader = new System.IO.StreamReader("dtdv.json");
                string str = reader.ReadToEnd();
                dtdv = JsonConvert.DeserializeObject<DataTable>(str);
                reader.Close();
            }
            // nếu không có thì tạo cột trong bảng tương ứng.
            else
            {
                dtdv.Columns.Add("Mã dịch vụ");
                dtdv.Columns.Add("Tên dịch vụ");
                dtdv.Columns.Add("Giá dịch vụ");
            }
            //kiem tra bang data nhan vien
            if (System.IO.File.Exists(@"dtnv.json"))
            {
                //doc file
                System.IO.StreamReader reader = new System.IO.StreamReader("dtnv.json");
                string str = reader.ReadToEnd();
                dtnv = JsonConvert.DeserializeObject<DataTable>(str);
                reader.Close();
            }
            else
            {
                dtnv.Columns.Add("Mã nhân viên");
                dtnv.Columns.Add("Tên nhân viên");
                dtnv.Columns.Add("Ngày sinh");
                dtnv.Columns.Add("Số điện thoại");
                dtnv.Columns.Add("Địa chỉ");
            }

            //kiem tra trong bang khach hang
            if (System.IO.File.Exists(@"dtbenhnhan.json"))
            {
                //doc file
                System.IO.StreamReader reader = new System.IO.StreamReader("dtbenhnhan.json");
                string str = reader.ReadToEnd();
                dtbenhnhan = JsonConvert.DeserializeObject<DataTable>(str);
                reader.Close();
            }
            else
            {
                dtbenhnhan.Columns.Add("Mã bệnh nhân");
                dtbenhnhan.Columns.Add("Tên bệnh nhân");
                dtbenhnhan.Columns.Add("Ngày sinh");
                dtbenhnhan.Columns.Add("Số điện thoại");
                dtbenhnhan.Columns.Add("Địa chỉ");
                dtbenhnhan.Columns.Add("BHYT");
            }

            // kiểm tra bảng data hoá đơn phòng khám

            if (System.IO.File.Exists(@"dthd.json"))
            {
                //doc file
                System.IO.StreamReader reader = new System.IO.StreamReader("dthd.json");
                string strpk = reader.ReadToEnd();
                dthd = JsonConvert.DeserializeObject<DataTable>(strpk);
                reader.Close();

            }
            else
            {
                dthd.Columns.Add("Mã hoá đơn");
                dthd.Columns.Add("Mã bệnh nhân");
                dthd.Columns.Add("Ngày khám");
                dthd.Columns.Add("Ngáy hẹn khám lại");
                dthd.Columns.Add("Tổng hoá đơn");
                dthd.Columns.Add("BHYT");
            }

            // bảng chi tiết hoá đơn
            if (System.IO.File.Exists(@"dtcthdon.json"))
            {
                //doc file
                System.IO.StreamReader reader = new System.IO.StreamReader("dtcthdon.json");
                string str = reader.ReadToEnd();
                dtcthdon = JsonConvert.DeserializeObject<DataTable>(str);
                reader.Close();

            }
            else
            {
                dtcthdon.Columns.Add("Mã hoá đơn");
                dtcthdon.Columns.Add("Dịch vụ");
                dtcthdon.Columns.Add("Mã nhân viên");
                dtcthdon.Columns.Add("Đơn giá");
            }

            // cho các cột trong datagridview fill co dãn theo chiều dài chiều rộng của form
            datadichvu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            datahoadon.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            datanhanvien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            datachitiethoadon.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            databenhnhan.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            datathongke.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // gắn liên kết từ bảng datagridview với các bảng datatable tương ứng
            databenhnhan.DataSource = dtbenhnhan;
            datanhanvien.DataSource = dtnv;
            datadichvu.DataSource = dtdv;
            datahoadon.DataSource = dthd;
            datachitiethoadon.DataSource = dtcthdon;

            //chọn để thống kê
            // ban đầu khi chạy form cho tất cả các textbox và button đóng lại
            tbxtimkiem.Enabled = false;
            btntimkiem.Enabled = false;
            cbxtktheoloaidv.Enabled = false;
            btnthongke.Enabled = false;
            tktheongay.Enabled = false;
            btnhienthi.Enabled = false;
            // thêm dữ liệu từ dịch vụ vào combobox
            combodichvu.Items.Clear(); // xoá hết các item trong  combobox dịch vụ dùng trong tab bệnh nhân
            for (int i = 0; i < datadichvu.Rows.Count; i++)
            {
                //chạy từ đầu đến cuối bảng datadichvu để đưa vào combobox
                combodichvu.Items.Add(datadichvu[1, i].Value);
            }
            combonhanvien.Items.Clear();
            for (int i = 0; i < datanhanvien.Rows.Count; i++)
            {
                combonhanvien.Items.Add(datanhanvien[0, i].Value);

            }
            combomabn.Items.Clear();
            for (int i = 0; i < databenhnhan.Rows.Count; i++)
            {
                combomabn.Items.Add(databenhnhan[0, i].Value);
            }
            cbxtktheoloaidv.Items.Clear(); // nằm trong bảng thống kê
            for (int i = 0; i < datadichvu.Rows.Count; i++)
            {
                cbxtktheoloaidv.Items.Add(datadichvu[1, i].Value);
            }
        }
        private void btnthemdv_Click(object sender, EventArgs e)
        {
            // Kiểm tra các textbox có dữ liệu không
            if (tbxmadv.Text != "" && tbxtendichvu.Text != "" && tbxgiadichvu.Text != "")
            {
                try // Kiểm tra ngoại lệ
                {
                    // Kiểm tra mã dịch vụ
                    string maDichVu = tbxmadv.Text.Trim();
                    Regex maDichVuRegex = new Regex("^D\\d+$");
                    if (!maDichVuRegex.IsMatch(maDichVu))
                    {
                        MessageBox.Show("Mã dịch vụ không hợp lệ. Mã dịch vụ phải bắt đầu bằng chữ 'D' và theo sau là các số nguyên dương.");
                        tbxmadv.Focus();
                        return;
                    }

                    int dongia = Convert.ToInt32(tbxgiadichvu.Text); // Chuyển đổi kiểu dữ liệu của textbox giá dịch vụ sang int
                    if (dongia <= 0)
                    {
                        MessageBox.Show("Giá dịch vụ phải là số nguyên dương.");
                        tbxgiadichvu.Focus();
                        return;
                    }
                    if (dongia < 10000)
                    {
                        MessageBox.Show("Giá dịch vụ phải lớn hơn hoặc bằng 10000.");
                        tbxgiadichvu.Focus();
                        return;
                    }

                    // Kiểm tra tên dịch vụ chỉ chứa tiếng Việt và dấu cách
                    string tenDichVu = tbxtendichvu.Text.Trim();
                    Regex regex = new Regex("^[\\p{L}\\s]+$");
                    if (!regex.IsMatch(tenDichVu))
                    {
                        MessageBox.Show("Tên dịch vụ không hợp lệ, vui lòng nhập lại chỉ chứa tiếng Việt và dấu cách.");
                        tbxtendichvu.Focus();
                        return;
                    }

                    dtdv.Rows.Add(tbxmadv.Text, tbxtendichvu.Text, tbxgiadichvu.Text); // Đưa các dữ liệu vào bảng datatable tương ứng
                    tbxmadv.Text = "";
                    tbxtendichvu.Text = "";
                    tbxgiadichvu.Text = "";
                }
                catch (FormatException)
                {
                    MessageBox.Show("Nhập sai dữ liệu giá dịch vụ");
                    tbxgiadichvu.Focus();
                }
            }
            else if (tbxmadv.Text == "")
            {
                MessageBox.Show("Không được để mã dịch vụ trống!");
                tbxmadv.Focus();
            }
            else if (tbxtendichvu.Text == "")
            {
                MessageBox.Show("Không được để tên dịch vụ trống");
                tbxtendichvu.Focus();
            }
            else if (tbxgiadichvu.Text == "")
            {
                MessageBox.Show("Không được để giá dịch vụ trống");
                tbxgiadichvu.Focus();
            }
        }

        private void btnsuadv_Click(object sender, EventArgs e)
        {
            string maDichVu = tbxmadv.Text.Trim();
            Regex maDichVuRegex = new Regex("^D\\d+$");
            if (!maDichVuRegex.IsMatch(maDichVu))
            {
                MessageBox.Show("Mã dịch vụ không hợp lệ. Mã dịch vụ phải bắt đầu bằng chữ 'D' và theo sau là các số nguyên dương.");
                tbxmadv.Focus();
                return;
            }
            // Kiểm tra tên dịch vụ chỉ chứa tiếng Việt và dấu cách
            string tenDichVu = tbxtendichvu.Text.Trim();
            Regex regex = new Regex("^[\\p{L}\\s]+$");
            if (regex.IsMatch(tenDichVu))
            {
                MessageBox.Show("Tên dịch vụ không hợp lệ, vui lòng nhập lại chỉ chứa tiếng việt và dấu cách.");
                tbxtendichvu.Focus();
                return;
            }
            // kiểm tra giá trị của textbox giá dịch vụ
            if (tbxgiadichvu.Text == "" || !int.TryParse(tbxgiadichvu.Text, out int dongia) || dongia <= 0 || dongia < 10000)
            {
                MessageBox.Show("Giá dịch vụ phải là số nguyên dương và lớn hơn hoặc bằng 10000");
                tbxgiadichvu.Focus();
                return;
            }

            // gắn dữ liệu tương ứng trong bảng với textbox tương ứng
            datadichvu.Rows[dongdangchon].Cells[0].Value = tbxmadv.Text;
            datadichvu.Rows[dongdangchon].Cells[1].Value = tbxtendichvu.Text;
            datadichvu.Rows[dongdangchon].Cells[2].Value = tbxgiadichvu.Text;
        }

        private void btnxoadv_Click(object sender, EventArgs e)
        {
            // hiển thị thông báo bạn muốn xoá ko nếu nhấn yes sẽ thực hiện 
            if (MessageBox.Show("Bạn có muốn xoá thông tin này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                datadichvu.Rows.RemoveAt(dongdangchon); // xoá dòng đang chọn
                tbxtendichvu.Text = ""; // đưa các thông tin về null
                tbxgiadichvu.Text = "";
            }
        }

        private void btnluudv_Click(object sender, EventArgs e)
        {
            // hiển thị thông báo bạn có muốn lưu ko nếu nhấn yes sẽ thực hiện lưu vào datatable tương ứng
            if (MessageBox.Show("Bạn có muốn lưu không", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string jsonstr;
                jsonstr = JsonConvert.SerializeObject(dtdv); //chuyen doi chuoi sang json de luu

                System.IO.File.WriteAllText("dtdv.json", jsonstr);
            }
            // đưa các dữ liệu trong bảng dịch vụ vào combobox
            combodichvu.Items.Clear();
            for (int i = 0; i < datadichvu.Rows.Count; i++)
            {
                combodichvu.Items.Add(datadichvu[0, i].Value); // thuoc trong tab bệnh nhân

            }
            cbxtktheoloaidv.Items.Clear();
            for (int i = 0; i < datadichvu.Rows.Count; i++)
            {
                cbxtktheoloaidv.Items.Add(datadichvu[0, i].Value); // thuộc trong tab thống kê
            }
        }

        private void datadichvu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dongdangchon = e.RowIndex;
            tbxmadv.Text = datadichvu.Rows[dongdangchon].Cells[0].Value.ToString();
            tbxtendichvu.Text = datadichvu.Rows[dongdangchon].Cells[1].Value.ToString();
            tbxgiadichvu.Text = datadichvu.Rows[dongdangchon].Cells[2].Value.ToString();
        }

        private void tbxmadv_Leave(object sender, EventArgs e)
        {
            if (tbxmadv.Text == "")
            {
                MessageBox.Show("Không được để mã dịch vụ trống!");
                tbxmadv.Focus();
            }
            else
            {
                for (int i = 0; i < datadichvu.Rows.Count; i++)
                {
                    if (Convert.ToString(tbxmadv.Text) == Convert.ToString(datadichvu[0, i].Value))
                    {
                        MessageBox.Show("Mã dịch vụ đã tồn tại");
                        tbxmadv.Focus();
                    }
                }
            }
        }

        private void btnthemnv_Click(object sender, EventArgs e)
        {
            if (tbxmanv.Text != "" && tbxtennv.Text != "" && ngaysinhnv.Text != "" && tbxsdtnv.Text != "" && tbxdiachinv.Text != "")
            {
                // Kiểm tra mã nhân viên duy nhất
                bool trungmanv = false;
                foreach (DataRow row in dtnv.Rows)
                {
                    if (row["Mã nhân viên"].ToString() == tbxmanv.Text)
                    {
                        trungmanv = true;
                        break;
                    }
                }
                if (!trungmanv)
                {
                    // Kiểm tra độ dài và định dạng mã nhân viên
                    if (tbxmanv.Text.Length == 5 && tbxmanv.Text.StartsWith("NV") && Regex.IsMatch(tbxmanv.Text, @"^[a-zA-Z0-9]+$"))
                    {
                        // Kiểm tra họ tên
                        tbxtennv.Text = tbxtennv.Text.Trim();
                        if (Regex.IsMatch(tbxtennv.Text, @"^[\p{L}\s]+$") && tbxtennv.Text != "")
                        {
                            // Kiểm tra số điện thoại
                            if (tbxsdtnv.Text.Length == 10 && tbxsdtnv.Text.StartsWith("0"))
                            {
                                if (int.TryParse(tbxsdtnv.Text, out int sdtnv) && sdtnv > 0)
                                {
                                    // Kiểm tra ngày sinh
                                    if (DateTime.TryParseExact(ngaysinhnv.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime ngay))
                                    {
                                        if (ngay.Year >= 1963 && ngay.Year <= 2002)
                                        {
                                            dtnv.Rows.Add(tbxmanv.Text, tbxtennv.Text, ngay.ToString("dd/MM/yyyy"), tbxsdtnv.Text, tbxdiachinv.Text);
                                            tbxmanv.Text = "";
                                            tbxtennv.Text = "";
                                            ngaysinhnv.Text = "";
                                            tbxsdtnv.Text = "";
                                            tbxdiachinv.Text = "";
                                            MessageBox.Show("Thêm thông tin thành công.");
                                        }
                                        else
                                        {
                                            MessageBox.Show("Năm sinh không hợp lệ! Vui lòng chọn năm từ 1963 đến 2002.");
                                            ngaysinhnv.Focus();
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Năm sinh không hợp lệ! Vui lòng chọn năm từ 1963 đến 2002.");
                                        ngaysinhnv.Focus();
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Vui kiểm tra lại SĐT!");
                                    tbxsdtnv.Focus();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Vui kiểm tra lại SĐT!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Họ tên không hợp lệ, vui lòng nhập lại!");
                            tbxtenbn.Focus();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Mã nhân viên không hợp lệ. Mã nhân viên phải có 5 ký tự và bắt đầu bằng 'NV'.");
                        tbxmabn.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Mã nhân viên đã tồn tại, vui lòng nhập lại!");
                    tbxmabn.Focus();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin nhân viên.");
            }
        }

        private void btnsuanv_Click(object sender, EventArgs e)
        {
            // Sửa thông tin nhân viên
            if (tbxmanv.Text.Length != 5 || !tbxmanv.Text.StartsWith("NV") || !Regex.IsMatch(tbxmanv.Text, @"^[a-zA-Z0-9]+$"))
            {
                MessageBox.Show("Mã nhân viên không hợp lệ, vui lòng nhập lại! (Tối đa 5 ký tự, bắt đầu bằng 'NV', chỉ sử dụng chữ và số)");
                tbxmanv.Focus();
                return;
            }

            // Kiểm tra và thông báo lỗi nếu tên nhân viên không phù hợp
            if (!Regex.IsMatch(tbxtennv.Text, @"^[\p{L}\s]+$") || tbxtennv.Text == "")
            {
                MessageBox.Show("Họ tên không hợp lệ, vui lòng nhập lại!");
                tbxtenbn.Focus();
                return;
            }

            // Kiểm tra và thông báo lỗi nếu ngày sinh không hợp lệ
            if (!DateTime.TryParseExact(ngaysinhnv.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime ngay) || ngay.Year < 1963 || ngay.Year > 2002)
            {
                MessageBox.Show("Ngày sinh không hợp lệ! Vui lòng chọn dữ liệu theo định dạng dd/MM/yyyy (ngày/tháng/năm) và năm giới hạn trong khoảng từ 1963 đến 2002.");
                ngaysinhnv.Focus();
                return;
            }

            // Kiểm tra và thông báo lỗi nếu số điện thoại không hợp lệ
            if (!int.TryParse(tbxsdtnv.Text, out int sdtnv) || tbxsdtnv.Text.Length != 10 || !tbxsdtnv.Text.StartsWith("0"))
            {
                MessageBox.Show("Số điện thoại không hợp lệ! Vui lòng kiểm tra lại.");
                tbxsdtnv.Focus();
                return;
            }

            // Cập nhật thông tin nhân viên
            datanhanvien.Rows[dongdangchonnv].Cells[0].Value = tbxmanv.Text;
            datanhanvien.Rows[dongdangchonnv].Cells[1].Value = tbxtennv.Text;
            datanhanvien.Rows[dongdangchonnv].Cells[2].Value = ngaysinhnv.Text;
            datanhanvien.Rows[dongdangchonnv].Cells[3].Value = tbxsdtnv.Text;
            datanhanvien.Rows[dongdangchonnv].Cells[4].Value = tbxdiachinv.Text;
        }

        private void btnxoanv_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn xoá thông tin này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                datanhanvien.Rows.RemoveAt(dongdangchonnv);
                tbxmanv.Text = "";
                tbxtennv.Text = "";
                ngaysinhnv.Text = "";
                tbxsdtnv.Text = "";
                tbxdiachinv.Text = "";
            }
        }

        private void btnluunv_Click(object sender, EventArgs e)
        {
            // lưu thông tin vào bảng
            if (MessageBox.Show("Bạn có muốn lưu không", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string jsonstr;
                jsonstr = JsonConvert.SerializeObject(dtnv); //chuyen doi chuoi sang json de luu

                System.IO.File.WriteAllText("dtnv.json", jsonstr);
            }
            // đưa các dữ liệu trong bảng nhan viên vào combobox
            combonhanvien.Items.Clear();
            for (int i = 0; i < datanhanvien.Rows.Count; i++)
            {
                combonhanvien.Items.Add(datanhanvien[0, i].Value);
            }
        }

        private void datanhanvien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // xác định chỉ số dòng đang chọn
            dongdangchonnv = e.RowIndex;
            tbxmanv.Text = datanhanvien.Rows[dongdangchonnv].Cells[0].Value.ToString();
            tbxtennv.Text = datanhanvien.Rows[dongdangchonnv].Cells[1].Value.ToString();
            ngaysinhnv.Text = datanhanvien.Rows[dongdangchonnv].Cells[2].Value.ToString();
            tbxsdtnv.Text = datanhanvien.Rows[dongdangchonnv].Cells[3].Value.ToString();
            tbxdiachinv.Text = datanhanvien.Rows[dongdangchonnv].Cells[4].Value.ToString();
        }

        private void tbxmanv_Leave(object sender, EventArgs e)
        {
            if (tbxmanv.Text == "")
            {
                MessageBox.Show("Không được để mã nhân viên trống");
                tbxmanv.Focus();
            }
            else
            {
                for (int i = 0; i < datanhanvien.Rows.Count; i++)
                {
                    if (Convert.ToString(tbxmanv.Text) == Convert.ToString(datanhanvien[0, i].Value))
                    {
                        MessageBox.Show("Mã nhân viên đã tồn tại");
                        tbxmanv.Focus();
                    }
                }
            }
        }

        private void btnthembn_Click(object sender, EventArgs e)
        {
            if (tbxmabn.Text != "" && tbxtenbn.Text != "" && ngaysinhbn.Text != "" && tbxsdtbn.Text != "" && BHYT.Text != "")
            {
                // Kiểm tra mã bệnh nhân duy nhất
                bool trungmabn = false;
                foreach (DataRow row in dtbenhnhan.Rows)
                {
                    if (row["Mã bệnh nhân"].ToString() == tbxmabn.Text)
                    {
                        trungmabn = true;
                        break;
                    }
                }
                if (!trungmabn)
                {
                    int mabn;
                    if (int.TryParse(tbxmabn.Text, out mabn) && mabn > 0)
                    {
                        // Kiểm tra nếu mã bệnh nhân bắt đầu bằng số 0
                        if (tbxmabn.Text[0] == '0')
                        {
                            MessageBox.Show("Mã bệnh nhân phải là số nguyên dương");
                            tbxmabn.Focus();
                        }
                        else
                        {
                            // Kiểm tra họ tên
                            tbxtenbn.Text = tbxtenbn.Text.Trim();
                            if (Regex.IsMatch(tbxtenbn.Text, @"^[\p{L}\s]+$") && tbxtenbn.Text != "")
                            {
                                // Kiểm tra số điện thoại
                                if (tbxsdtbn.Text.Length == 10 && tbxsdtbn.Text.StartsWith("0"))
                                {
                                    int sdtkh;
                                    if (int.TryParse(tbxsdtbn.Text, out sdtkh))
                                    {
                                        // Kiểm tra ngày sinh
                                        DateTime ngay;
                                        if (DateTime.TryParseExact(ngaysinhbn.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out ngay))
                                        {
                                            if (ngay <= DateTime.Now.Date)
                                            {
                                                // Kiểm tra thông tin Bảo hiểm
                                                bool isValidBHYT = false;
                                                string bhyt = BHYT.Text.ToUpper();
                                                if (bhyt == "CÓ" || bhyt == "KHÔNG")
                                                {
                                                    isValidBHYT = true;
                                                    dtbenhnhan.Rows.Add(mabn, tbxtenbn.Text, ngay.ToString("dd/MM/yyyy"), tbxsdtbn.Text, tbxdiachibn.Text, isValidBHYT ? bhyt : "");
                                                    tbxmabn.Text = "";
                                                    tbxtenbn.Text = "";
                                                    ngaysinhbn.Text = "";
                                                    tbxsdtbn.Text = "";
                                                    tbxdiachibn.Text = "";
                                                    BHYT.Text = "";
                                                    MessageBox.Show("Thêm thông tin thành công.");
                                                }
                                                else
                                                {
                                                    MessageBox.Show("Thông tin Bảo hiểm không hợp lệ. Vui lòng nhập 'CÓ' hoặc 'KHÔNG'.");
                                                    BHYT.Focus();
                                                }
                                            }
                                            else
                                            {
                                                MessageBox.Show("Ngày sinh không hợp lệ, vui lòng nhập lại ngày tháng năm trước hoặc bằng ngày hiện tại.");
                                                ngaysinhbn.Focus();
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("Ngày sinh không hợp lệ, vui lòng nhập lại theo định dạng dd/MM/yyyy.");
                                            ngaysinhbn.Focus();
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Số điện thoại chỉ được chứa dữ liệu số");
                                        tbxsdtbn.Focus();
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Số điện thoại phải độ dài là 10 và bắt đầu bằng số 0", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Họ tên không hợp lệ, vui lòng nhập lại chỉ chứa tiếng Việt và dấu cách.");
                                tbxtenbn.Focus();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Mã bệnh nhân phải là số nguyên dương");
                        tbxmabn.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Mã bệnh nhân đã tồn tại");
                    tbxmabn.Focus();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin bệnh nhân");
            }
        }

        private void btnsuabn_Click(object sender, EventArgs e)
        {
            tbxmabn.Enabled = false; // không cho phép sửa tbxmabn
                                     // Kiểm tra và thông báo lỗi nếu và tbxtenbn không phù hợp
            if (!Regex.IsMatch(tbxtenbn.Text, @"^[\p{L}\s]+$") && tbxtenbn.Text != "")
            {
                MessageBox.Show("Tên bệnh nhân không hợp lệ, vui lòng nhập lại.");
                tbxtenbn.Focus();
                return;
            }
            if (DateTime.TryParseExact(ngaysinhbn.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime ngay))
            {
                if (ngay > DateTime.Now) // nếu ngày sinh sau thời điểm hiện tại
                {
                    MessageBox.Show("Ngày sinh không hợp lệ, vui lòng nhập lại ngày tháng năm trước hoặc bằng ngày hiện tại.");
                    ngaysinhbn.Focus();
                    return;
                }
                databenhnhan.Rows[dongdangchonkh].Cells[2].Value = ngay.ToString("dd/MM/yyyy");
            }
            else
            {
                MessageBox.Show("Ngày sinh không hợp lệ, vui lòng nhập lại theo định dạng dd/MM/yyyy.");
                ngaysinhbn.Focus();
                return;
            }
            if (tbxsdtbn.Text.Length != 10 || !tbxsdtbn.Text.StartsWith("0") || !int.TryParse(tbxsdtbn.Text, out _))
            {
                MessageBox.Show("Số điện thoại không hợp lệ, vui lòng nhập lại số có 10 chữ số và bắt đầu bằng số 0.");
                tbxsdtbn.Focus();
                return;
            }
            // Kiểm tra thông tin Bảo hiểm
            bool isValidBHYT = false;
            string bhyt = BHYT.Text.ToUpper();
            if (bhyt == "CÓ" || bhyt == "KHÔNG")
            {
                isValidBHYT = true;
            }
            else
            {
                MessageBox.Show("Thông tin Bảo hiểm không hợp lệ. Vui lòng nhập 'Có' hoặc 'Không'.");
                BHYT.Focus();
                return;
            }

            databenhnhan.Rows[dongdangchonkh].Cells[1].Value = tbxtenbn.Text;
            databenhnhan.Rows[dongdangchonkh].Cells[3].Value = tbxsdtbn.Text;
            databenhnhan.Rows[dongdangchonkh].Cells[4].Value = tbxdiachibn.Text;
            databenhnhan.Rows[dongdangchonkh].Cells[5].Value = BHYT.Text;
            MessageBox.Show("Sửa thành công!");
        }

        private void btnxoabn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn xoá bệnh nhân này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                databenhnhan.Rows.RemoveAt(dongdangchonkh);
                tbxmabn.Text = "";
                tbxtenbn.Text = "";
                ngaysinhbn.Text = "";
                tbxsdtbn.Text = "";
                tbxdiachibn.Text = "";
                BHYT.Text = "";
                MessageBox.Show("Xóa thành công!");
            }
        }

        private void btnluubn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn lưu không", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string jsonstr;
                jsonstr = JsonConvert.SerializeObject(dtbenhnhan); //chuyen doi chuo sang json de luu
                System.IO.File.WriteAllText("dtbenhnhan.json", jsonstr);
                MessageBox.Show("Lưu thành công!");
            }
            // đưa các dữ liệu trong bảng nhan viên vào combobox
            combomabn.Items.Clear();
            for (int i = 0; i < databenhnhan.Rows.Count; i++)
            {
                combomabn.Items.Add(databenhnhan[0, i].Value);
            }
        }

        private void databenhnhan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dongdangchonkh = e.RowIndex;
            tbxmabn.Text = databenhnhan.Rows[dongdangchonkh].Cells[0].Value.ToString();
            tbxtenbn.Text = databenhnhan.Rows[dongdangchonkh].Cells[1].Value.ToString();
            ngaysinhbn.Text = databenhnhan.Rows[dongdangchonkh].Cells[2].Value.ToString();
            tbxsdtbn.Text = databenhnhan.Rows[dongdangchonkh].Cells[3].Value.ToString();
            tbxdiachibn.Text = databenhnhan.Rows[dongdangchonkh].Cells[4].Value.ToString();
            BHYT.Text = databenhnhan.Rows[dongdangchonkh].Cells[5].Value.ToString();
        }

        private void tbxmabn_Leave(object sender, EventArgs e)
        {
            if (tbxmabn.Text == "")
            {
                MessageBox.Show("Không được để mã bệnh nhân trống");
                tbxmabn.Focus();
            }
            else
            {
                for (int i = 0; i < databenhnhan.Rows.Count; i++)
                {
                    if (Convert.ToString(tbxmabn.Text) == Convert.ToString(databenhnhan[0, i].Value))
                    {
                        MessageBox.Show("Mã bệnh nhân đã tồn tại");
                        tbxmabn.Focus();
                    }
                }
            }
        }

        private void btnthem_Click(object sender, EventArgs e)
        {
            // Kiểm tra các textbox và combobox có rỗng không, nếu rỗng thì hiển thị thông báo tương ứng
            if (tbxmahoadon.Text == "")
            {
                MessageBox.Show("Vui lòng nhập mã hoá đơn");
                tbxmahoadon.Focus();
                return;
            }
            if (combomabn.Text == "")
            {
                MessageBox.Show("Vui lòng chọn tên bệnh nhân");
                combomabn.Focus();
                return;
            }
            if (combodichvu.Text == "")
            {
                MessageBox.Show("Vui lòng chọn dịch vụ");
                combodichvu.Focus();
                return;
            }
            if (combonhanvien.Text == "")
            {
                MessageBox.Show("Vui lòng chọn nhân viên");
                combonhanvien.Focus();
                return;
            }

            // Kiểm tra xem mã hoá đơn đã được sử dụng hay chưa, nếu đã sử dụng thì hiển thị thông báo tương ứng
            int mahd;
            if (!int.TryParse(tbxmahoadon.Text, out mahd) || mahd <= 0)
            {
                MessageBox.Show("Mã hoá đơn phải là số nguyên dương");
                tbxmahoadon.Focus();
                return;
            }
            DateTime ngaykham, ngaykhamlai;
            if (DateTime.TryParseExact(timengaykham.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out ngaykham))
            {
                if (ngaykham > DateTime.Now)
                {
                    MessageBox.Show("Ngày khám không hợp lệ, vui lòng nhập lại ngày tháng năm hợp lệ.");
                    timengaykham.Focus();
                    return;
                }
            }
            else
            {
                MessageBox.Show("Ngày khám không hợp lệ, vui lòng nhập lại theo định dạng dd/MM/yyyy.");
                timengaykham.Focus();
                return;
            }
            if (DateTime.TryParseExact(timengaykhamlai.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out ngaykhamlai))
            {
                if (ngaykhamlai < DateTime.Today) 
                {
                    MessageBox.Show("Ngày khám lại không hợp lệ, vui lòng nhập lại ngày tháng năm hợp lệ.");
                    timengaykhamlai.Focus();
                    return;
                }
            }
            else
            {
                MessageBox.Show("Ngày khám lại không hợp lệ, vui lòng nhập lại theo định dạng dd/MM/yyyy.");
                timengaykhamlai.Focus();
                return;
            }

            // Thêm thông tin vào bảng
            dongia = Convert.ToInt32(tbxdongia.Text);
            datasokham.Rows.Add(tbxmahoadon.Text, combomabn.Text, ngaykham.ToString("dd/MM/yyyy"), ngaykhamlai.ToString("dd/MM/yyyy"), combodichvu.Text, combonhanvien.Text, dongia.ToString(), combobaohiem.Text);

            // Đóng các textbox và combobox tương ứng
            tbxmahoadon.Enabled = false;
            combomabn.Enabled = false;
            timengaykham.Enabled = false;
            timengaykhamlai.Enabled = false;
            combodichvu.Text = null;
            combonhanvien.Text = null;
            tbxdongia.Text = "";
            combobaohiem.Enabled = false;
            MessageBox.Show("Thêm thành công");
        }

        private void btnsua_Click(object sender, EventArgs e)
        {
            tbxmahoadon.Enabled = false;
            // sửa các dữ liệu trong bảng
            datasokham.Rows[dongdangchonpk].Cells[4].Value = combodichvu.Text;
            datasokham.Rows[dongdangchonpk].Cells[5].Value = combonhanvien.Text;
            datasokham.Rows[dongdangchonpk].Cells[6].Value = tbxdongia.Text;
        }

        private void btnxoa_Click(object sender, EventArgs e)
        {
            // hỏi xem bạn có muốn xoá không nếu yes thì xoá xong cho các combox về null
            if (MessageBox.Show("Bạn có muốn xoá thông tin này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                datasokham.Rows.RemoveAt(dongdangchonpk);
                tbxdongia.Text = "";
                combodichvu.Text = null;
                combonhanvien.Text = null;
                combobaohiem.Text = null;
                MessageBox.Show("Xóa thành công!");
            }
        }

        private void btnthanhtoan_Click(object sender, EventArgs e)
        {
            double thanhtien = 0;
            for (int j = 0; j < datasokham.Rows.Count; j++) // duyệt từng dòng trong bảng để tính hoá đơn
            {
                int dongiakh = 0;
                int.TryParse(datasokham[6, j].Value.ToString(), out dongiakh); // chuyển đổi giá trị dòng giá khám sang kiểu số nguyên
                thanhtien += dongiakh;
            }
            // giảm giá nếu bhyt1 = "Có"
            if (combobaohiem.Text == "CÓ")
            {
                thanhtien *= 0.2; // giảm giá 80%
            }
            tbxtonghoadon.Text = thanhtien.ToString(); // hiển thị lên textbox tổng hoá đơn
                                                       // đóng lại hết các textboox khi thanh toán
            btnthanhtoan.Enabled = false;
            btnthem.Enabled = false;
            btnsua.Enabled = false;
            btnxoa.Enabled = false;
            combonhanvien.Enabled = false;
            combodichvu.Enabled = false;

            //thêm bảng hoá đơn phòng khám

            dthd.Rows.Add(tbxmahoadon.Text, combomabn.Text, timengaykham.Text, timengaykhamlai.Text, thanhtien.ToString(), combobaohiem.Text);
            //luu dữ liệu vào trong bảng hoá đơn
            string jsonstrhd;
            jsonstrhd = JsonConvert.SerializeObject(dthd); //chuyen doi chuo sang json de luu
            System.IO.File.WriteAllText("dthd.json", jsonstrhd);
            // đưa vào bảng chi tiết hoá đơn
            for (int i = 0; i < datasokham.Rows.Count; i++)
            {
                if (combobaohiem.Text == "CÓ")
                {
                    int dongiagiam = 0;
                    int.TryParse(datasokham[6, i].Value.ToString(), out dongiagiam); // chuyển đổi giá trị dòng giá khám sang kiểu số nguyên
                    dongiagiam = (int)(dongiagiam * 0.2); // giảm giá 80%
                    dtcthdon.Rows.Add(datasokham[0, i].Value.ToString(), datasokham[4, i].Value.ToString(), datasokham[5, i].Value.ToString(), dongiagiam.ToString());
                }
                else
                {
                    dtcthdon.Rows.Add(datasokham[0, i].Value.ToString(), datasokham[4, i].Value.ToString(), datasokham[5, i].Value.ToString(), datasokham[6, i].Value.ToString());
                }
            }
            string jsonstrcthdon;
            jsonstrcthdon = JsonConvert.SerializeObject(dtcthdon); //chuyen doi chuo sang json de luu
            System.IO.File.WriteAllText("dtcthdon.json", jsonstrcthdon);
        }

        private void btntaomoi_Click(object sender, EventArgs e)
        {
            datasokham.Rows.Clear();
            thanhtien = 0;
            tbxmahoadon.Text = "";
            tbxdongia.Text = "";
            tbxtonghoadon.Text = "";
            tbxmahoadon.Enabled = true;
            combomabn.Text = null;
            combomabn.Enabled = true;
            timengaykham.Enabled = true;
            timengaykhamlai.Enabled = true;
            combodichvu.Text = null;
            combodichvu.Enabled = true;
            combonhanvien.Text = null;
            combonhanvien.Enabled = true;
            combobaohiem.Text = null;
            combobaohiem.Enabled = true;
            btnthanhtoan.Enabled = true;
            btnthem.Enabled = true;
            btnsua.Enabled = true;
            btnxoa.Enabled = true;
        }

        private void datasokham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // xác định chỉ mục dòng chọn trên bảng
            dongdangchonpk = e.RowIndex;
            // hiển thị các giữ liệu trong bảng lên textbox tương ứng
            combodichvu.Text = datasokham.Rows[dongdangchonpk].Cells[4].Value.ToString();
            combonhanvien.Text = datasokham.Rows[dongdangchonpk].Cells[5].Value.ToString();
            tbxdongia.Text = datasokham.Rows[dongdangchonpk].Cells[6].Value.ToString();
            combobaohiem.Text = datasokham.Rows[dongdangchonpk].Cells[7].Value.ToString();
        }

        private void tbxmahoadon_Leave(object sender, EventArgs e)
        {
            // kiểm tra goá đơn đã tồn tại chưa khi chọn ra ngoài textbox
            if (tbxmahoadon.Text == "")
            {
                MessageBox.Show("Không được để mã hoá đơn trống");
                tbxmahoadon.Focus();
            }
            else
            {
                for (int i = 0; i < datahoadon.Rows.Count; i++)
                {
                    if (Convert.ToString(tbxmahoadon.Text) == Convert.ToString(datahoadon[0, i].Value))
                    {
                        MessageBox.Show("Mã hoá đơn đã tồn tại");
                        tbxmahoadon.Focus();
                    }
                }
            }
        }

        private void combodichvu_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Hiển thị giá dịch vụ khi chọn vào một 1 tên dịch trong combobox
            for (int i = 0; i < datadichvu.Rows.Count; i++)
            {
                // tìm kiếm dịch vụ chọn trong combobox có nằm trong bảng lưu thông tin dịch vụ không
                if (combodichvu.Text == datadichvu[1, i].Value.ToString())
                {
                    // nếu có hiển thi đơn giá lên textbox dơn giá
                    tbxdongia.Text = datadichvu[2, i].Value.ToString();
                }
            }
        }
        private void combomabn_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < databenhnhan.Rows.Count; i++)
            {
                // tìm kiếm ma benh nhan chọn trong combobox có nằm trong bảng lưu thông tin benh nhan không
                if (combomabn.Text == databenhnhan[0, i].Value.ToString())
                {
                    // nếu có hiển thi BHYT lên combobaohiem
                    combobaohiem.Text = databenhnhan[5, i].Value.ToString();
                }
            }
        }
        private void radiobenhnhan_CheckedChanged(object sender, EventArgs e)
        {
            tbxtimkiembn.Enabled = true;
            tbxtimkiemnv.Enabled = false;
        }

        private void radionhanvien_CheckedChanged(object sender, EventArgs e)
        {
            tbxtimkiembn.Enabled = false;
            tbxtimkiemnv.Enabled = true;
        }

        private void btntimkiemTK_Click(object sender, EventArgs e)
        {
            if (radiobenhnhan.Checked)
            {
                datatimkiemthongtin.Rows.Clear();
                for (int i = 0; i < databenhnhan.Rows.Count; i++)
                {
                    if (tbxtimkiembn.Text == databenhnhan[0, i].Value.ToString())
                    {
                        datatimkiemthongtin.Rows.Add(databenhnhan[0, i].Value, databenhnhan[1, i].Value, databenhnhan[2, i].Value, databenhnhan[3, i].Value, databenhnhan[4, i].Value);
                    }

                }
                if (datatimkiemthongtin.Rows.Count == 0)
                {
                    MessageBox.Show("Không có thông tin bệnh nhân cần tìm kiếm, vui lòng nhập lại mã");
                }
            }
            if (radionhanvien.Checked)
            {
                if (username == "Admin")
                {
                    datatimkiemthongtin.Rows.Clear();
                    for (int i = 0; i < datanhanvien.Rows.Count; i++)
                    {
                        if (tbxtimkiemnv.Text == datanhanvien[0, i].Value.ToString())
                        {
                            datatimkiemthongtin.Rows.Add(datanhanvien[0, i].Value, datanhanvien[1, i].Value, datanhanvien[2, i].Value, datanhanvien[3, i].Value, datanhanvien[4, i].Value);
                        }

                    }
                    if (datatimkiemthongtin.Rows.Count == 0)
                    {
                        MessageBox.Show("Không có thông tin nhân viên cần tìm kiếm, vui lòng nhập lại mã");
                    }
                }
                else
                {
                    MessageBox.Show("Chỉ có Admin mới xem được chức năng này");
                }
            }
        }

        private void timkiem_CheckedChanged(object sender, EventArgs e)
        {
            // chọn chức năng muốn thống kê
            if (timkiem.Checked)
            {
                // đóng mở các textbox . button tương ứng cho chức năng tìm kiếm
                tbxtimkiem.Enabled = true;
                btntimkiem.Enabled = true;
                cbxtktheoloaidv.Enabled = false;
                btnthongke.Enabled = false;
                tktheongay.Enabled = false;
                btnhienthi.Enabled = false;
            }
        }

        private void tkloaidv_CheckedChanged(object sender, EventArgs e)
        {
            if (tkloaidv.Checked)
            {
                tbxtimkiem.Enabled = false;
                btntimkiem.Enabled = false;
                cbxtktheoloaidv.Enabled = true;
                btnthongke.Enabled = true;
                tktheongay.Enabled = false;
                btnhienthi.Enabled = false;
            }
        }

        private void checktktheongay_CheckedChanged(object sender, EventArgs e)
        {
            if (checktktheongay.Checked)
            {
                tbxtimkiem.Enabled = false;
                btntimkiem.Enabled = false;
                cbxtktheoloaidv.Enabled = false;
                btnthongke.Enabled = true;
                tktheongay.Enabled = true;
                btnhienthi.Enabled = false;
            }
        }

        private void checkkhachsapdenngayhen_CheckedChanged(object sender, EventArgs e)
        {
            if (checkkhachsapdenngayhen.Checked)
            {
                tbxtimkiem.Enabled = false;
                btntimkiem.Enabled = false;
                cbxtktheoloaidv.Enabled = false;
                btnthongke.Enabled = false;
                tktheongay.Enabled = false;
                btnhienthi.Enabled = true;
            }
        }

        private void btntimkiem_Click(object sender, EventArgs e)
        {
            if (timkiem.Checked)
            {
                datathongke.Rows.Clear(); // xoá hết các dữ liệu trong bảng datathongke

                for (int i = 0; i < datahoadon.Rows.Count; i++)
                {
                    for (int j = 0; j < datachitiethoadon.Rows.Count; j++)
                    {
                        if (datahoadon[0, i].Value.ToString() == datachitiethoadon[0, j].Value.ToString()) // ghép 2 bảng thông qua mã hoá đơn
                        {
                            // tìm kiếm theo mã hoá đơn 
                            if (tbxtimkiem.Text == datachitiethoadon[0, j].Value.ToString())
                            {
                                datathongke.Rows.Add(datahoadon[0, i].Value, datahoadon[1, i].Value, datahoadon[2, i].Value, datahoadon[3, i].Value, datachitiethoadon[1, j].Value, datachitiethoadon[2, j].Value, datachitiethoadon[3, j].Value);
                            }
                        }

                    }
                }
            }   // đém nếu không có dòng nào tức không có mã hoá đơn đó
            if (datathongke.Rows.Count == 0)
            {
                MessageBox.Show("Không có hoá đơn này");

            }
        }

        private void btnthongke_Click(object sender, EventArgs e)
        {
            if (tkloaidv.Checked)
            {
                datathongke.Rows.Clear();
                for (int i = 0; i < datahoadon.Rows.Count; i++)
                {
                    for (int j = 0; j < datachitiethoadon.Rows.Count; j++)
                    {
                        if (datahoadon[0, i].Value.ToString() == datachitiethoadon[0, j].Value.ToString()) // ghép bảngh
                        {
                            // tìm kiếm thep loại dịch vụ
                            if (cbxtktheoloaidv.Text == datachitiethoadon[1, j].Value.ToString())
                            {
                                datathongke.Rows.Add(datahoadon[0, i].Value, datahoadon[1, i].Value, datahoadon[2, i].Value, datahoadon[3, i].Value, datachitiethoadon[1, j].Value, datachitiethoadon[2, j].Value, datachitiethoadon[3, j].Value);
                            }
                        }
                    }
                }

                if (datathongke.Rows.Count == 0)
                {
                    MessageBox.Show("Dịch vụ này không có khách hàng");
                }
            }
            // nếu chọn thống kê theo ngày
            else if (checktktheongay.Checked)
            {
                datathongke.Rows.Clear();
                for (int i = 0; i < datahoadon.Rows.Count; i++)
                {
                    // tìm thời gian theo ngày 
                    if (tktheongay.Text == datahoadon[2, i].Value.ToString())
                    {
                        for (int j = 0; j < datachitiethoadon.Rows.Count; j++)
                            if (datahoadon[0, i].Value.ToString() == datachitiethoadon[0, j].Value.ToString())
                            {
                                datathongke.Rows.Add(datahoadon[0, i].Value, datahoadon[1, i].Value, datahoadon[2, i].Value, datahoadon[3, i].Value, datachitiethoadon[1, j].Value, datachitiethoadon[2, j].Value, datachitiethoadon[3, j].Value);
                            }
                    }
                }
                if (datathongke.Rows.Count == 0)
                {
                    MessageBox.Show("Ngày này không có khách hàng");
                }
            }
        }

        private void btnhienthi_Click(object sender, EventArgs e)
        {
            datathongke.Rows.Clear();
            for (int i = 0; i < datahoadon.Rows.Count; i++)
            {
                DateTime now = DateTime.Now; // khai báo biến now thể hiện thời gian hiện tại
                for (int k = 0; k < datachitiethoadon.Rows.Count; k++)
                {
                    if (datahoadon[0, i].Value.ToString() == datachitiethoadon[0, k].Value.ToString())
                    {
                        for (int j = 1; j < 15; j++) // cho now cộng thêm 15 ngày tiếp theo
                        {
                            if (now.AddDays(j).ToString("dd/MM/yyyy") == datahoadon[3, i].Value.ToString()) // đối chiếu trong 15 ngày nữa có ngày khám lại bằng không.
                            {
                                datathongke.Rows.Add(datahoadon[0, i].Value, datahoadon[1, i].Value, datahoadon[2, i].Value, datahoadon[3, i].Value, datachitiethoadon[1, k].Value, datachitiethoadon[2, k].Value, datachitiethoadon[3, k].Value);
                            }
                        }
                    }
                }
            }
            if (datathongke.Rows.Count == 0)
            {
                MessageBox.Show("Không có khách hàng chuẩn bị tới khám lại!");
            }
        }

        private void btndangxuat_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Hãy chắc chắn rằng toàn bộ dữ liệu bạn nhập đã bấm 'Lưu' nếu không dữ liệu sẽ mất đi!", "Thông báo");
            DialogResult traloi;
            traloi = MessageBox.Show("Bạn vẫn muốn đăng xuất chứ?", "Trả lời", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (traloi == DialogResult.OK)
            {
                this.Close(); // Đóng Form2
                Form1 form1 = new Form1();
                form1.Show(); // Hiển thị Form1
            }
        }
    }
}
