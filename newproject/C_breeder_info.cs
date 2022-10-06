using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using MySql.Data.MySqlClient;

namespace newproject
{
    public partial class breeder_info : Form
    {
        private MySqlConnection databaseConnection()
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=farm_dtb;";
            MySqlConnection connection = new MySqlConnection(connectionString);
            return connection;
        }

        private void Breeder_info() //ตารางข้อมูลแม่พันธุ์
        {
            try
            {
                MySqlConnection connection = databaseConnection();
                DataSet ds = new DataSet();
                connection.Open();
                MySqlCommand cmd;
                cmd = connection.CreateCommand();
                cmd.CommandText = "select * from ข้อมูลแม่พันธุ์ไก่	";
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(ds);
                connection.Close();
                tarang.DataSource = ds.Tables[0].DefaultView;
            }
            catch (Exception EX)
            {
                MessageBox.Show(EX.Message);
            }
        }

        public breeder_info()
        {
            
            InitializeComponent();
        }

        private void breeder_info_Load(object sender, EventArgs e)
        {
            Breeder_info();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            this.Hide();
            informationform info = new informationform();
            info.ShowDialog();
        }

        private void pictureclose_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("ออกจากโปรแกรมหรือไม่ ?", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.OK)
            {
                
                this.Close();
                Application.Exit();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (breeder_txt.Text == "" || price_txt.Text == "" || amount_txt.Text == "")
            {
                MessageBox.Show("กรุณากรอกข้อมูลให้ครบ !", "ตรวจพบข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    MySqlConnection conn = databaseConnection();
                    string sql = "INSERT INTO ข้อมูลแม่พันธุ์ไก่ (ชื่อแม่พันธุ์,ราคา,จำนวน,วันนำเข้า) VALUES('" + breeder_txt.Text + "','" + price_txt.Text + "','" + amount_txt.Text + "','" + this.dateTimePicker1.Value.ToString("dd-MM-yyyy") + "')";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (rows > 0)
                    {
                        MessageBox.Show("เพิ่มข้อมูลสำเร็จ", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        Breeder_info();
                        resettext();
                    }
                }
                catch (Exception EX)
                {
                    MessageBox.Show(EX.Message);
                }               
            }     
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("ลบหรือไม่ ?", "ยีนยันการลบ", MessageBoxButtons.OKCancel,MessageBoxIcon.Question);
            if (dialogResult == DialogResult.OK)
            {
                try
                {
                    int selectedRow = tarang.CurrentCell.RowIndex;
                    int deleteId = Convert.ToInt32(tarang.Rows[selectedRow].Cells["รหัสแม่พันธุ์"].Value);
                    MySqlConnection conn = databaseConnection();
                    string sql = "DELETE FROM ข้อมูลแม่พันธุ์ไก่ WHERE รหัสแม่พันธุ์ = '" + deleteId + "'";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (rows > 0)
                    {
                        MessageBox.Show("ลบข้อมูลสำเร็จ", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        Breeder_info();
                        price_txt.ResetText();
                        breeder_txt.ResetText();
                        amount_txt.ResetText();
                    }
                }
                catch (Exception EX)
                {
                    MessageBox.Show(EX.Message);
                }    
            }   
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            if (breeder_txt.Text == "" || price_txt.Text == "" || amount_txt.Text == "")
            {
                MessageBox.Show("กรุณากรอกข้อมูลให้ครบ !", "ตรวจพบข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    int selectedRow = tarang.CurrentCell.RowIndex;
                    int editId = Convert.ToInt32(tarang.Rows[selectedRow].Cells["รหัสแม่พันธุ์"].Value);
                    MySqlConnection conn = databaseConnection();
                    string sql = "UPDATE ข้อมูลแม่พันธุ์ไก่ SET ชื่อแม่พันธุ์ = '" + breeder_txt.Text + "',ราคา = '" + price_txt.Text + "',จำนวน = '" + amount_txt.Text + "' WHERE รหัสแม่พันธุ์ = '" + editId + "'";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (rows > 0)
                    {
                        MessageBox.Show("แก้ไขข้อมูลสำเร็จ", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        Breeder_info();
                        resettext();
                    }
                }
                catch (Exception EX)
                {
                    MessageBox.Show(EX.Message);
                }
                
            }    
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                MessageBox.Show("ไม่สามารถใส่อักษรได้ !", "ตรวจพบข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Handled = true;
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                MessageBox.Show("ไม่สามารถใส่อักษรได้ !", "ตรวจพบข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Handled = true;
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar >= 161 || (int)e.KeyChar == 8 || (int)e.KeyChar == 13 || (int)e.KeyChar == 46 || (int)e.KeyChar == 32)
            {
                e.Handled = false;
            }
            
            else 
            {
                MessageBox.Show("กรอกได้เฉพาะภาษาไทยเท่านั้น !", "ตรวจพบข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Handled = true;
            }
        }

        private void resettext()
        {
            price_txt.ResetText();
            breeder_txt.ResetText();
            amount_txt.ResetText();
        }

        private void button2_Click(object sender, EventArgs e)
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
                cmd.CommandText = "select รหัสแม่พันธุ์,ชื่อแม่พันธุ์,ราคา,จำนวน,วันนำเข้า from ข้อมูลแม่พันธุ์ไก่ where ชื่อแม่พันธุ์ like '%" + search_txt.Text + "%'";
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read() == true)
                {
                    tarang.DataSource = ds.Tables[0].DefaultView;
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
            Breeder_info();
            search_txt.Clear();
        }

        private void tarang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            tarang.CurrentRow.Selected = true;
            breeder_txt.Text = tarang.Rows[e.RowIndex].Cells["ชื่อแม่พันธุ์"].FormattedValue.ToString();
            price_txt.Text = tarang.Rows[e.RowIndex].Cells["ราคา"].FormattedValue.ToString();
            amount_txt.Text = tarang.Rows[e.RowIndex].Cells["จำนวน"].FormattedValue.ToString();
        }

        private void tarang_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                tarang.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.SandyBrown;
                tarang.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
            }
        }

        private void tarang_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                tarang.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                tarang.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
            }
        }
    }
}
