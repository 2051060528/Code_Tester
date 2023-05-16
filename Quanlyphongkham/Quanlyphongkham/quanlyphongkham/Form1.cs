using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
namespace quanlyphongkham
{
    
    public partial class Form1 : Form
    {
        // tạo các bảng dữ liệu
        DataTable dtdv = new DataTable();
        DataTable dtnv = new DataTable();
        DataTable dtpk = new DataTable();
        DataTable dtcthd = new DataTable();
        DataTable dtkh = new DataTable();
        // khai báo biến toàn cục để xác định dòng trong datagirdview
        int dongdangchon;
        int dongdangchonnv;
        int dongdangchonpk;
        int dongdangchonkh;
        public Form1()
        {
            InitializeComponent();
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

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

        private void Form1_Load(object sender, EventArgs e)
        {
            // kiểm tra bảng dtdv xem có dữ liệu không
            if (System.IO.File.Exists(@"dtdv.json") )
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

                dtdv.Columns.Add("Tên dịch vụ");
                dtdv.Columns.Add("Giá dich vụ");
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
            if (System.IO.File.Exists(@"dtkh.json"))
            {
                //doc file
                System.IO.StreamReader reader = new System.IO.StreamReader("dtkh.json");
                string str = reader.ReadToEnd();
                dtkh = JsonConvert.DeserializeObject<DataTable>(str);
                reader.Close();
            }
            else
            {
                dtkh.Columns.Add("Mã khách hàng");
                dtkh.Columns.Add("Tên khách hàng");
                dtkh.Columns.Add("Ngày sinh");
                dtkh.Columns.Add("Số điện thoại");
                dtkh.Columns.Add("Địa chỉ");
            }

    






            // kiểm tra bảng data hoá đơn phòng khám

            if (System.IO.File.Exists(@"dtpk.json"))
            {
                //doc file
                System.IO.StreamReader reader = new System.IO.StreamReader("dtpk.json");
                string strpk = reader.ReadToEnd();
                dtpk = JsonConvert.DeserializeObject<DataTable>(strpk);
                reader.Close();

            }
            else
            {
                dtpk.Columns.Add("Mã hoá đơn");
                dtpk.Columns.Add("Mã khách hàng");
                dtpk.Columns.Add("Ngày khám");
                dtpk.Columns.Add("Ngáy hẹn khám lại");
                dtpk.Columns.Add("Tổng hoá đơn");
            }
        



            // bảng chi tiết hoá đơn
            if (System.IO.File.Exists(@"dtcthd.json"))
            {
                //doc file
                System.IO.StreamReader reader = new System.IO.StreamReader("dtcthd.json");
                string str = reader.ReadToEnd();
                dtcthd = JsonConvert.DeserializeObject<DataTable>(str);
                reader.Close();

            }
            else
            {
                dtcthd.Columns.Add("Mã hoá đơn");
                dtcthd.Columns.Add("Dịch vụ");
                dtcthd.Columns.Add("Mã nhân viên");
                dtcthd.Columns.Add("Đơn giá");
            }
           







            // cho các cột trong datagridview fill co dãn theo chiều dài chiều rộng của form
            datadichvu.AutoSizeColumnsMode =DataGridViewAutoSizeColumnsMode.Fill;
            datahoadon.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            datanhanvien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            datachitiethoadon.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            datakhachhang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            datathongke.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;




            // gắn liên kết từ bảng datagridview với các bảng datatable tương ứng
            datakhachhang.DataSource = dtkh;
            datanhanvien.DataSource = dtnv;
            datadichvu.DataSource = dtdv;
            datahoadon.DataSource = dtpk;
            datachitiethoadon.DataSource = dtcthd;

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
                combodichvu.Items.Add(datadichvu[0, i].Value);
            }
            combonhanvien.Items.Clear();
            for (int i = 0; i < datanhanvien.Rows.Count; i++)
            {
                combonhanvien.Items.Add(datanhanvien[0, i].Value);

            }
            combomakh.Items.Clear();
            for (int i = 0; i < datanhanvien.Rows.Count; i++)
            {
               combomakh.Items.Add(datakhachhang[0, i].Value);

            }
            cbxtktheoloaidv.Items.Clear(); // nằm trong bảng thống kê
            for (int i = 0; i < datadichvu.Rows.Count; i++)
            {
                cbxtktheoloaidv.Items.Add(datadichvu[0, i].Value);
            }


        }

        private void btnthemdv_Click(object sender, EventArgs e)
        {
            // kiểm tra các textbox có dữ liệu không
           if(tbxtendichvu.Text!="" && tbxgiadichvu.Text!="")
            {
                try // kiểm tra ngoại lệ
                {
                   int dongia = Convert.ToInt32(tbxgiadichvu.Text); // chuyển đổi kiểu dữ liệu của textbox giá dịch vụ sang int
                   dtdv.Rows.Add(tbxtendichvu.Text, tbxgiadichvu.Text); // đưa các dữ liệu vào bảng datatable tương ứng
                    tbxtendichvu.Text = "";
                    tbxgiadichvu.Text = "";
                }
                catch(FormatException)
                {
                    MessageBox.Show("Nhập sai dữ liệu giá dich vụ");
                    tbxgiadichvu.Focus();
                }
            }
           else if(tbxtendichvu.Text=="")
            {
                MessageBox.Show("Không được để tên dịch vụ trống");
                tbxtendichvu.Focus();
            }
            else if (tbxgiadichvu.Text=="")
            {
                MessageBox.Show("Không được để giá dịch vụ trống");
                tbxgiadichvu.Focus();
            }
        }
     

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dongdangchon = e.RowIndex;
            tbxtendichvu.Text = datadichvu.Rows[dongdangchon].Cells[0].Value.ToString();
            tbxgiadichvu.Text = datadichvu.Rows[dongdangchon].Cells[1].Value.ToString();
        }

        private void btnsuadv_Click(object sender, EventArgs e)
        {
            // gắn dữ liệu tương ứng trong bảng với textbox tương ứng
            datadichvu.Rows[dongdangchon].Cells[0].Value = tbxtendichvu.Text;
            datadichvu.Rows[dongdangchon].Cells[1].Value = tbxgiadichvu.Text;
     
        }

        private void btnxoadv_Click(object sender, EventArgs e)
        {
            // hiển thị thông báo bạn muốn xoá ko nếu nhấn yes sẽ thực hiện 
            if(MessageBox.Show("Bạn có muốn xoá thông tin này không?","Thông báo",MessageBoxButtons.YesNo,MessageBoxIcon.Question)== DialogResult.Yes)
            {
                datadichvu.Rows.RemoveAt(dongdangchon); // xoá dòng đang chọn
                tbxtendichvu.Text = ""; // đưa các thông tin về null
                tbxgiadichvu.Text = "";
            }    
        }

        private void btnluu_Click(object sender, EventArgs e)
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
        private void combodichvu_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Hiển thị giá dịch vụ khi chọn vào một 1 tên dịch trong combobox
            for (int i = 0; i < datadichvu.Rows.Count; i++)
               {
                // tìm kiếm dịch vụ chọn trong combobox có nằm trong bảng lưu thông tin dịch vụ không
                    if(combodichvu.Text==datadichvu[0,i].Value.ToString())
                    {
                    // nếu có hiển thi đơn giá lên textbox dơn giá
                        tbxdongia.Text = datadichvu[1, i].Value.ToString();
                      
                    }
               }

        }

        private void combodichvu_SelectedValueChanged(object sender, EventArgs e)
        {
 
          



        }

        private void tbxtendichvu_TextChanged(object sender, EventArgs e)
        {

        }

        private void combodichvu_Click(object sender, EventArgs e)
        {
     
        }

        private void btnthem_Click(object sender, EventArgs e)
        {
            // kiểm tra các textbox  có rỗng không nếu không giỗng thì add các thông tin vào bảng
            if (tbxmahoadon.Text!="" && combomakh.Text!=null && combodichvu.Text != "" && combonhanvien.Text!="")
            {
                    dongia = Convert.ToInt32(tbxdongia.Text);
                    databenhnhan.Rows.Add(tbxmahoadon.Text , combomakh.Text, timengaykham.Text, timengaykhamlai.Text, combodichvu.Text, combonhanvien.Text , dongia.ToString());
                    // sau khi thêm xong đóng các textbox tương ứng lại vì đó là chi tiết của 1 khách hàng không cho thay đổi thông tin
                    tbxmahoadon.Enabled = false;
                    combomakh.Enabled = false;
                    timengaykham.Enabled = false;
                    timengaykhamlai.Enabled = false;
                    combodichvu.Text = null;
                    combonhanvien.Text = null;
                    tbxdongia.Text = "";
                
               
            }
            // ngược lại thì hiển thị lên các thong báo không được để trống
            else if (tbxmahoadon.Text == "")
            {
                MessageBox.Show("Không được để mã hoá đơn trống");
                tbxmahoadon.Focus();
            }
            else if (combomakh.Text == "")
            {
                MessageBox.Show("Không được để tên khách hàng trống");
                combomakh.Focus();
            }
            else if (combodichvu.Text == "")
            {
                MessageBox.Show("Vui lòng chọn dịch vụ");
                combodichvu.Focus();
            }
            else if (combonhanvien.Text == "")
            {
                MessageBox.Show("Vui lòng chọn nhân viên");
                combonhanvien.Focus();
            }

        }

        int dongia = 0;
        int thanhtien = 0;

        private void button1_Click(object sender, EventArgs e)
        { 
            // tính tổng tiền hoá đơn của khách hàng
                            for (int j = 0; j < databenhnhan.Rows.Count; j++) // duyệt từng dòng trong bảng để tính hoá đơn
                            {
                                int dongiakh = 0;
                                dongiakh = Convert.ToInt32(databenhnhan[6, j].Value.ToString());
                                thanhtien += dongiakh;
                            }
                            tbxtonghoadon.Text = thanhtien.ToString(); // hiển thị lên textbox tổng hoá đơn
                           // đóng lại hết các textboox khi thanh toán
                            button1.Enabled = false;
                            btnthem.Enabled = false;
                            btnsua.Enabled = false;
                            btnxoa.Enabled = false;
                            combonhanvien.Enabled = false;
                            combodichvu.Enabled = false;




                            //thêm bảng hoá đơn phòng khám

                            dtpk.Rows.Add(tbxmahoadon.Text, combomakh.Text , timengaykham.Text, timengaykhamlai.Text, thanhtien.ToString());



                            //luu dữ liệu vào trong bảng hoá đơn
                            string jsonstrpk;
                            jsonstrpk = JsonConvert.SerializeObject(dtpk); //chuyen doi chuo sang json de luu

                            System.IO.File.WriteAllText("dtpk.json", jsonstrpk);



                            // đưa vào bảng chi tiết hoá đơn
                            for(int i=0;i< databenhnhan.Rows.Count;i++)
                             {
                                dtcthd.Rows.Add(databenhnhan[0, i].Value.ToString(), databenhnhan[4, i].Value.ToString(), databenhnhan[5, i].Value.ToString(), databenhnhan[6, i].Value.ToString());
                             }

                            string jsonstrcthd;
                            jsonstrcthd = JsonConvert.SerializeObject(dtcthd); //chuyen doi chuo sang json de luu

                            System.IO.File.WriteAllText("dtcthd.json", jsonstrcthd);
                         
                        
                   }

    

        

        private void btnsua_Click(object sender, EventArgs e)
        {
            // sửa các dữ liệu trong bảng
            databenhnhan.Rows[dongdangchonpk].Cells[4].Value = combodichvu.Text;
            databenhnhan.Rows[dongdangchonpk].Cells[5].Value = combonhanvien.Text;
            databenhnhan.Rows[dongdangchonpk].Cells[6].Value = tbxdongia.Text;
        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // xác định chỉ mục dòng chọn trên bảng
            dongdangchonpk = e.RowIndex;
            // hiển thị các giữ liệu trong bảng lên textbox tương ứng
            combodichvu.Text = databenhnhan.Rows[dongdangchonpk].Cells[4].Value.ToString();
            combonhanvien.Text = databenhnhan.Rows[dongdangchonpk].Cells[5].Value.ToString();
            tbxdongia.Text = databenhnhan.Rows[dongdangchonpk].Cells[6].Value.ToString();
        }

        private void btnxoa_Click(object sender, EventArgs e)
        {
            // hỏi xem bạn có muốn xoá không nếu yes thì xoá xong cho các combox về null
            if (MessageBox.Show("Bạn có muốn xoá thông tin này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                databenhnhan.Rows.RemoveAt(dongdangchonpk);
                tbxdongia.Text = "";
                combodichvu.Text = null;
                combonhanvien.Text = null;
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
                        if (datahoadon[0,i].Value.ToString() == datachitiethoadon[0, j].Value.ToString()) // ghép 2 bảng thông qua mã hoá đơn
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

        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void btnthongke_Click(object sender, EventArgs e)
        {
         
            if(tkloaidv.Checked)
            {
                datathongke.Rows.Clear();
                for(int i=0;i<datahoadon.Rows.Count;i++)
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
              
                if(datathongke.Rows.Count==0)
                {
                    MessageBox.Show("Dịch vụ này không có khách hàng");

                }
            }
            // nếu chọn thống kê theo ngày
            else if(checktktheongay.Checked)
            {
                datathongke.Rows.Clear();
               for(int i=0;i<datahoadon.Rows.Count;i++)
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
                    for (int k=0;k<datachitiethoadon.Rows.Count;k++)
                     {
                        if (datahoadon[0, i].Value.ToString() == datachitiethoadon[0, k].Value.ToString())
                        {
                            for (int j = 1; j < 15; j++) // cho now cộng thêm 15 ngày tiếp theo
                            {
                                if (now.AddDays(j).ToString("MM/dd/yyyy") == datahoadon[3, i].Value.ToString()) // đối chiếu trong 15 ngày nữa có ngày khám lại bằng không.
                                {
                                    datathongke.Rows.Add(datahoadon[0, i].Value, datahoadon[1, i].Value, datahoadon[2, i].Value, datahoadon[3, i].Value, datachitiethoadon[1, k].Value, datachitiethoadon[2, k].Value, datachitiethoadon[3, k].Value);

                                }

                            }
                        }
                       
                    }
                   

                }

                 

            
        }

        private void tktheongay_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
         
        }

        private void timengaysinh_ValueChanged(object sender, EventArgs e)
        {

        }

        private void timengaykham_ValueChanged(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tbxgiadichvu_TextChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void tbxsdt_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbxtenkh_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void tbxthanhtien_TextChanged(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void timengaykhamlai_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void cbxtktheoloaidv_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tbxtimkiem_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click_1(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            // sưa thông tin nhân viên
            datanhanvien.Rows[dongdangchonnv].Cells[0].Value = tbxmanv.Text;
            datanhanvien.Rows[dongdangchonnv].Cells[1].Value = tbxtennv.Text;
            datanhanvien.Rows[dongdangchonnv].Cells[2].Value = ngaysinhnv.Text;
            datanhanvien.Rows[dongdangchonnv].Cells[3].Value = tbxsdtnv.Text;
            datanhanvien.Rows[dongdangchonnv].Cells[4].Value = tbxdiachinv.Text;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void btnthemnv_Click(object sender, EventArgs e)
        {
            // thêm thông tin nhân viên vào bảng
            if (tbxmanv.Text!="" && tbxtennv.Text != "" && ngaysinhnv.Text != "" && tbxsdtnv.Text!="" && tbxdiachinv.Text!="")
            {
                    dtnv.Rows.Add(tbxmanv.Text, tbxtennv.Text, ngaysinhnv.Text, tbxsdtnv.Text, tbxdiachinv.Text);
                tbxmanv.Text = "";
                tbxtennv.Text = "";
                ngaysinhnv.Text = "";
                tbxsdtnv.Text = "";
                tbxdiachinv.Text = "";
              
            }
            else if (tbxmanv.Text == "")
            {
                MessageBox.Show("Không được để mã nhân viên trống");
                tbxmanv.Focus();
            }
            else if (tbxtennv.Text == "")
            {
                MessageBox.Show("Không được để tên nhân viên trống");
                tbxtennv.Focus();
            }
            else if (tbxsdtnv.Text == "")
            {
                MessageBox.Show("Không được để số điện thoại trống");
                tbxsdtnv.Focus();
            }
            else if (tbxdiachinv.Text == "")
            {
                MessageBox.Show("Không được để địa chỉ trống");
                tbxdiachinv.Focus();
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            // thoát chương trình
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // lưu thông tin vào bảng
            if (MessageBox.Show("Bạn có muốn lưu không", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string jsonstr;
                jsonstr = JsonConvert.SerializeObject(dtnv); //chuyen doi chuo sang json de luu

                System.IO.File.WriteAllText("dtnv.json", jsonstr);
            }
            // đưa các dữ liệu trong bảng nhan viên vào combobox
            combonhanvien.Items.Clear();
            for (int i = 0; i < datanhanvien.Rows.Count; i++)
            {
                combonhanvien.Items.Add(datanhanvien[0, i].Value);

            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // xác định chỉ số dòng đang chọn
            dongdangchonnv = e.RowIndex;
            tbxmanv.Text = datanhanvien.Rows[dongdangchonnv].Cells[0].Value.ToString();
            tbxtennv.Text = datanhanvien.Rows[dongdangchonnv].Cells[1].Value.ToString();
            ngaysinhnv.Text = datanhanvien.Rows[dongdangchonnv].Cells[2].Value.ToString();
            tbxsdtnv.Text = datanhanvien.Rows[dongdangchonnv].Cells[3].Value.ToString();
            tbxdiachinv.Text = datanhanvien.Rows[dongdangchonnv].Cells[4].Value.ToString();

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

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void tbxmahoadon_TextChanged(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void combonhanvien_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void tbxmahoadon_TextChanged_1(object sender, EventArgs e)
        {
           
    }

        private void label17_Click_1(object sender, EventArgs e)
        {

        }

        private void tbxtonghoadon_TextChanged(object sender, EventArgs e)
        {

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tabPage5_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            databenhnhan.Rows.Clear();
            thanhtien = 0;
            tbxmahoadon.Text = "";
            tbxdongia.Text = "";
            tbxtonghoadon.Text = "";
            tbxmahoadon.Enabled = true;
            combomakh.Enabled = true;
            timengaykham.Enabled = true;
            timengaykhamlai.Enabled = true;
            combodichvu.Text = null;
            combodichvu.Enabled = true;
            combonhanvien.Text = null;
            combonhanvien.Enabled = true;
            button1.Enabled = true;
            btnthem.Enabled = true;
            btnsua.Enabled = true;
            btnxoa.Enabled = true;
        }

        private void combonhanvien_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void dataGridView6_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            
        }

        private void tbxmahoadon_Leave(object sender, EventArgs e)
        {
            // kiểm tra goá đơn đã tồn tại chưa khi chọn ra ngoài textbox
            if(tbxmahoadon.Text=="")
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

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void datahoadon_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            
        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void tbxmanv_TextChanged(object sender, EventArgs e)
        {

        }

        private void ngaysinhnv_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (tbxmakh.Text != "" && tbxtenkh.Text != "" && ngaysinhkh.Text != "" && tbxsdtkh.Text != "" && tbxdiachikh.Text != "")
            {
                dtkh.Rows.Add(tbxmakh.Text, tbxtenkh.Text, ngaysinhkh.Text, tbxsdtkh.Text, tbxdiachikh.Text);
                tbxmakh.Text = "";
                tbxtenkh.Text = "";
                ngaysinhkh.Text = "";
                tbxsdtkh.Text = "";
                tbxdiachikh.Text = "";

            }
            else if (tbxmakh.Text == "")
            {
                MessageBox.Show("Không được để mã khách hàng trống");
                tbxmakh.Focus();
            }
            else if (tbxtenkh.Text == "")
            {
                MessageBox.Show("Không được để tên khách hàng trống");
                tbxtenkh.Focus();
            }
            else if (tbxsdtkh.Text == "")
            {
                MessageBox.Show("Không được để số điện thoại khách hàng trống");
                tbxsdtkh.Focus();
            }
            else if (tbxdiachinv.Text == "")
            {
                MessageBox.Show("Không được để địa chỉ trống");
                tbxdiachikh.Focus();
            }
        }

        private void datakhachhang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dongdangchonkh = e.RowIndex;
            tbxmakh.Text = datakhachhang.Rows[dongdangchonkh].Cells[0].Value.ToString();
            tbxtenkh.Text = datakhachhang.Rows[dongdangchonkh].Cells[1].Value.ToString();
            ngaysinhkh.Text = datakhachhang.Rows[dongdangchonkh].Cells[2].Value.ToString();
            tbxsdtkh.Text = datakhachhang.Rows[dongdangchonkh].Cells[3].Value.ToString();
            tbxdiachikh.Text = datakhachhang.Rows[dongdangchonkh].Cells[4].Value.ToString();
        }

        private void btnsuakh_Click(object sender, EventArgs e)
        {
            datakhachhang.Rows[dongdangchonkh].Cells[0].Value = tbxmakh.Text;
            datakhachhang.Rows[dongdangchonkh].Cells[1].Value = tbxtenkh.Text;
            datakhachhang.Rows[dongdangchonkh].Cells[2].Value = ngaysinhkh.Text;
            datakhachhang.Rows[dongdangchonkh].Cells[3].Value = tbxsdtkh.Text;
            datakhachhang.Rows[dongdangchonkh].Cells[4].Value = tbxdiachikh.Text;
        }

        private void btnxoakh_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn xoá thông tin này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                datakhachhang.Rows.RemoveAt(dongdangchonkh);
                tbxmakh.Text = "";
                tbxtenkh.Text = "";
                ngaysinhkh.Text = "";
                tbxsdtkh.Text = "";
                tbxdiachikh.Text = "";
            }
        }

        private void btnluukh_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn lưu không", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string jsonstr;
                jsonstr = JsonConvert.SerializeObject(dtkh); //chuyen doi chuo sang json de luu

                System.IO.File.WriteAllText("dtkh.json", jsonstr);
            }
            // đưa các dữ liệu trong bảng nhan viên vào combobox
            combomakh.Items.Clear();
            for (int i = 0; i < datanhanvien.Rows.Count; i++)
            {
                combomakh.Items.Add(datakhachhang[0, i].Value);

            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if(radioButton1.Checked)
            {
                datatimkiemthongtin.Rows.Clear();
                for (int i=0;i< datakhachhang.Rows.Count;i++)
                {
                    if (tbxtimkiemkh.Text == datakhachhang[0, i].Value.ToString())
                    {
                        datatimkiemthongtin.Rows.Add(datakhachhang[0, i].Value, datakhachhang[1, i].Value, datakhachhang[2, i].Value, datakhachhang[3, i].Value, datakhachhang[4, i].Value);
                    }

                }
                if (datatimkiemthongtin.Rows.Count == 0)
                {
                    MessageBox.Show("Không có thông tin khách hàng này");
                }
            }
            if(radioButton2.Checked)
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
                    MessageBox.Show("Không có thông tin khách hàng này");
                }
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            tbxtimkiemkh.Enabled = true;
            tbxtimkiemnv.Enabled = false;
           
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            tbxtimkiemkh.Enabled = false;
            tbxtimkiemnv.Enabled = true;

        }

        private void tbxtimkiemnv_TextChanged(object sender, EventArgs e)
        {

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
     
        }

        private void radioButton3_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        private void tbxtendichvu_Leave(object sender, EventArgs e)
        {
            if ( tbxtendichvu.Text == "")
            {
                MessageBox.Show("Không được để tên dịch vụ trống trống");
                tbxtendichvu.Focus();
            }
            else
            {
                for (int i = 0; i < datadichvu.Rows.Count; i++)
                {
                    if (Convert.ToString(tbxtendichvu.Text) == Convert.ToString(datadichvu[0, i].Value))
                    {
                        MessageBox.Show("Dịch vụ đã tồn tại");
                        tbxtendichvu.Focus();
                    }
                }
            }
        }

        private void tbxmanv_Leave(object sender, EventArgs e)
        {
            if (tbxmanv.Text == "")
            {
                MessageBox.Show("Không được để mã nhân viên vụ trống trống");
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

        private void tbxmakh_Leave(object sender, EventArgs e)
        {
            if (tbxmakh.Text == "")
            {
                MessageBox.Show("Không được để mã Khách hàng trống trống");
                tbxmakh.Focus();
            }
            else
            {
                for (int i = 0; i < datakhachhang.Rows.Count; i++)
                {
                    if (Convert.ToString(tbxmakh.Text) == Convert.ToString(datakhachhang[0, i].Value))
                    {
                        MessageBox.Show("Mã khách hàng đã tồn tại");
                        tbxmakh.Focus();
                    }
                }
            }
        }

        private void combomakh_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
