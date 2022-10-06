using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace newproject
{
    

    public partial class order_history : Form
    {
        private MySqlConnection databaseConnection()
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=farm_dtb;";
            MySqlConnection connection = new MySqlConnection(connectionString);
            return connection;
        }
        public order_history()
        {
            InitializeComponent();
        }

        private void order_history_Load(object sender, EventArgs e)
        {
            Order_history();
        }

        private void Order_history()
        {
            try
            {
                MySqlConnection connection = databaseConnection();
                DataSet ds = new DataSet();
                connection.Open();
                MySqlCommand cmd;
                cmd = connection.CreateCommand();
                cmd.CommandText = "select * from จัดการคำสั่งซื้อ";
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(ds);
                connection.Close();
                dataGridView1.DataSource = ds.Tables[0].DefaultView;
            }
            catch (Exception EX)
            {
                MessageBox.Show(EX.Message);
            }           
        }

        private void close_btn_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("ออกจากโปรแกรมหรือไม่ ?", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.OK)
            {
                
                this.Close();
                Application.Exit();
            }
        }

        private void button1_Click(object sender, EventArgs e) //อันนี้คลิกตอนพิมพ์ใบเสร็จ
        {
            if (richTextBox1.Text == "")
            {
                MessageBox.Show("กรุณาเลือกแถวที่ต้องการพิมพ์ !", "ตรวจพบข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                printPreviewDialog1.Document = printDocument1;
                printPreviewDialog1.ShowDialog();
                resettext();
            }           
        }

        private void edit_btn_Click(object sender, EventArgs e)
        {
            if (comboBox2.Text == "" || price_txt.Text == "" || amount_txt.Text == "" || (radioButton1.Checked == false && radioButton2.Checked == false))
            {
                MessageBox.Show("กรุณากรอกข้อมูลให้ครบ !", "ตรวจพบข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    string status_ = "";
                    if (radioButton1.Checked == true)
                    {
                        status_ = "คำสั่งซื้อ";
                    }
                    else if (radioButton2.Checked == true)
                    {
                        status_ = "ขายแล้ว";
                    }

                    int selectedRow = dataGridView1.CurrentCell.RowIndex;
                    int editId = Convert.ToInt32(dataGridView1.Rows[selectedRow].Cells["รหัสคำสั่งซื้อ"].Value);
                    MySqlConnection conn = databaseConnection();
                    string sql = "UPDATE จัดการคำสั่งซื้อ SET รหัสลูกค้า = '" + comboBox2.Text + "',รายการ = '" + amount_txt.Text + "',รวมเงิน = '" + price_txt.Text + "',สถานะ = '" + status_ + "' WHERE รหัสคำสั่งซื้อ = '" + editId + "'";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (rows > 0)
                    {
                        MessageBox.Show("แก้ไขข้อมูลสำเร็จ", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        Order_history();
                        resettext();
                    }
                }
                catch (Exception EX)
                {
                    MessageBox.Show(EX.Message);
                }

            }
        }

        private void delete_btn_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("ลบหรือไม่ ?", "ยีนยันการลบ", MessageBoxButtons.OKCancel);
            if (dialogResult == DialogResult.OK)
            {
                try
                {
                    int selectedRow = dataGridView1.CurrentCell.RowIndex;
                    int deleteId = Convert.ToInt32(dataGridView1.Rows[selectedRow].Cells["รหัสคำสั่งซื้อ"].Value);
                    MySqlConnection conn = databaseConnection();
                    string sql = "DELETE FROM จัดการคำสั่งซื้อ WHERE รหัสคำสั่งซื้อ = '" + deleteId + "'";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (rows > 0)
                    {
                        MessageBox.Show("ลบข้อมูลสำเร็จ", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        Order_history();
                        resettext();
                    }
                }
                catch (Exception EX)
                {
                    MessageBox.Show(EX.Message);
                }
            }            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.CurrentRow.Selected = true;
            comboBox2.Text = dataGridView1.Rows[e.RowIndex].Cells["รหัสลูกค้า"].FormattedValue.ToString();
            textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells["ชื่อลูกค้า"].FormattedValue.ToString();
            amount_txt.Text = dataGridView1.Rows[e.RowIndex].Cells["รายการ"].FormattedValue.ToString();
            price_txt.Text = dataGridView1.Rows[e.RowIndex].Cells["รวมเงิน"].FormattedValue.ToString();
            if (dataGridView1.Rows[e.RowIndex].Cells["สถานะ"].FormattedValue.ToString() == "คำสั่งซื้อ")
            {
                radioButton1.Checked = true;
            }
            else if (dataGridView1.Rows[e.RowIndex].Cells["สถานะ"].FormattedValue.ToString() == "ขายแล้ว")
            {
                radioButton2.Checked = true;
            }
            else
            {
                radioButton1.Checked = false;
                radioButton2.Checked = false;
            }
            richTextBox1.Clear();
            richTextBox1.Text += "**********************************************\n";
            richTextBox1.Text += "***********    ใบเสร็จฟาร์มไข่ไก่    ************\n";
            richTextBox1.Text += "**********************************************\n";
            richTextBox1.Text += "วันที่ : " + DateTime.Now + "\n\n";

            richTextBox1.Text += "ชื่อลูกค้า : " + textBox1.Text + "\tรหัส :" + comboBox2.Text + "\n\n";
            richTextBox1.Text += "รายการ\n" + amount_txt.Text + "\n\n";
            richTextBox1.Text += "**********************************************\n";
            richTextBox1.Text += "รวมเงิน :     " + price_txt.Text + "      บาท\n";
            richTextBox1.Text += "**********************************************\n\n";

            richTextBox1.Text += "                ขอบคุณครับจากใจคนเลี้ยงไก่                  \n";
            richTextBox1.Text += "                     ฟาร์มไก่ลุงเรือง                  \n";
            richTextBox1.Text += "                   ติดต่อ 0610962132                  \n";
        }  

        private void turnback_btn_Click(object sender, EventArgs e)
        {
            this.Hide();
            order order = new order();
            order.ShowDialog();

        }

        private void resettext()
        {
            richTextBox1.Clear();
            this.comboBox2.ResetText();
            this.price_txt.ResetText();
            this.amount_txt.ResetText();
            textBox1.ResetText();
            radioButton1.Checked = false;
            radioButton2.Checked = false;
        }

        private void clear_btn_Click(object sender, EventArgs e)
        {
            resettext();
        }

        private void search_btn_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection connection = databaseConnection();
                connection.Open();
                MySqlCommand cmd;
                cmd = connection.CreateCommand();
                cmd.CommandText = "select รหัสคำสั่งซื้อ,รหัสลูกค้า,ชื่อลูกค้า,รายการ,รวมเงิน,วันที่เพิ่มข้อมูล,สถานะ from จัดการคำสั่งซื้อ where วันที่เพิ่มข้อมูล = '" + this.dateTimePicker2.Value.ToString("dd-MM-yyyy") + "'";
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read() == true)
                {
                    dataGridView1.DataSource = ds.Tables[0].DefaultView;
                    connection.Close();
                }
                else
                {
                    MessageBox.Show("ไม่มีข้อมูลนี้ในฐานข้อมูล !", "ตรวจพบข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    connection.Close();
                }
            }
            catch (Exception EX)
            {
                MessageBox.Show(EX.Message);
            }
           
        }

        private void refresh_btn_Click(object sender, EventArgs e)
        {
            
            Order_history();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString(richTextBox1.Text,new Font("FC Flexica [Non-commercial]",25,FontStyle.Bold),Brushes.Black,new Point(75,75));
        }

        private void dataGridView1_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.SandyBrown;
                dataGridView1.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
            }
        }

        private void dataGridView1_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                dataGridView1.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
            }
        }
    }
}
