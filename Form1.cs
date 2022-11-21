using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//tạo thư viện kết nối cơ sở dữ liệu
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Linq
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection sc = new SqlConnection("Data Source=DESKTOP-KMNS09Q\\SQLEXPRESS;Initial Catalog=QUAN_LY_SACH;Integrated Security=True");
        private void hienthi()
        {
            // tạo một đối tượng sda để đọc dữ liệu và đổ vào bảng
            SqlDataAdapter sda = new SqlDataAdapter("select * from T_T_Sach", sc);
            // tạo ra bảng trống để đổ dữ liệu
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
            
            // tạo ra để khi người dùng click vào trường trong datagr thì sẽ hiện lên texbox
            textBox1.DataBindings.Clear();
            textBox2.DataBindings.Clear();
            textBox3.DataBindings.Clear();
            textBox4.DataBindings.Clear();

            textBox1.DataBindings.Add("text", dataGridView1.DataSource, "maSach");
            textBox2.DataBindings.Add("text", dataGridView1.DataSource, "tieuDe");
            textBox3.DataBindings.Add("text", dataGridView1.DataSource, "gia");
            textBox4.DataBindings.Add("text", dataGridView1.DataSource, "SL");
            
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            sc.Open();
            hienthi();
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            textBox4.Enabled = false;
            button1.Focus();
            button1.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //bắt lỗi nhập sai dữ liệu
           if(textBox1.Text == "" 
              || textBox2.Text == ""
              || textBox3.Text == ""
              || textBox4.Text == ""){
                MessageBox.Show("chưa nhập đủ dữ liệu!! nhập lại", "thông báo");
            }else
            {
                textBox1.Enabled = true;
                textBox2.Enabled = true;
                textBox3.Enabled = true;
                textBox4.Enabled = true;
                // sqlcmd dùng để truy 
                SqlCommand sqlCommand = new SqlCommand("insert into T_T_SACH values(@maSach,@tieuDe,@gia,@SL)",sc);
                sqlCommand.Parameters.AddWithValue("maSach", textBox1.Text);
                sqlCommand.Parameters.AddWithValue("tieuDe", textBox2.Text);
                sqlCommand.Parameters.AddWithValue("gia", textBox3.Text);
                sqlCommand.Parameters.AddWithValue("SL", textBox4.Text);
                sqlCommand.ExecuteNonQuery();
                hienthi();
                MessageBox.Show("thêm thành công");
            }
        }
   
        private void button4_Click(object sender, EventArgs e)
        {
            // bỏ vô hiệu hóa texbox và butt sau đó hiện thị lại dữ liệu ra màn hình
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            textBox3.Enabled = true;
            textBox4.Enabled = true;

            button1.Enabled = true;
            
            hienthi();


        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == ""
              || textBox2.Text == ""
              || textBox3.Text == ""
              || textBox4.Text == "")
            {
                MessageBox.Show("chưa nhập đủ thông tin nhập lại", "thông báo");
            } 
            SqlCommand sqlCommand = new SqlCommand("update T_T_SACH set maSach = @maSach, tieuDe = @tieuDe, gia = @gia, SL = @SL where maSach = @maSach",sc);
            sqlCommand.Parameters.AddWithValue("maSach", textBox1.Text);
            sqlCommand.Parameters.AddWithValue("tieuDe", textBox2.Text);
            sqlCommand.Parameters.AddWithValue("gia", textBox3.Text);
            sqlCommand.Parameters.AddWithValue("SL", textBox4.Text);
            sqlCommand.ExecuteNonQuery();
            hienthi();
            MessageBox.Show("sửa thành công");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult hoi = MessageBox.Show("bạn có muốn xóa dữ liệu không?", "thông báo! ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (hoi == DialogResult.Yes)
            {
            SqlCommand sqlCommand = new SqlCommand("delete from T_T_SACH where maSach =@maSach", sc);
            sqlCommand.Parameters.AddWithValue("maSach", textBox1.Text);
            sqlCommand.Parameters.AddWithValue("tieuDe", textBox2.Text);
            sqlCommand.Parameters.AddWithValue("gia", textBox3.Text);
            sqlCommand.Parameters.AddWithValue("SL", textBox4.Text);
                //phương thức thực thi câu lệnh truy vấn
            sqlCommand.ExecuteNonQuery();
            hienthi();
            }
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SqlCommand sqlCommand = new SqlCommand("select * from T_T_SACH where maSach = @maSach", sc);
            sqlCommand.Parameters.AddWithValue("maSach", textBox5.Text);
            sqlCommand.Parameters.AddWithValue("tieuDe", textBox2.Text);
            sqlCommand.Parameters.AddWithValue("gia", textBox3.Text);
            sqlCommand.Parameters.AddWithValue("SL", textBox4.Text);
            SqlDataAdapter sda = new SqlDataAdapter(sqlCommand);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;

        }
    }
}
