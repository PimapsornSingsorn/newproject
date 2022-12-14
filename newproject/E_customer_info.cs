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
    public partial class customer_info : Form
    {
        private MySqlConnection databaseConnection()
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=farm_dtb;";
            MySqlConnection connection = new MySqlConnection(connectionString);
            return connection;
        }

        private void Customer_info()
        {
            try
            {
                MySqlConnection connection = databaseConnection();
                DataSet ds = new DataSet();
                connection.Open();
                MySqlCommand cmd;
                cmd = connection.CreateCommand();
                cmd.CommandText = "select * from ข้อมูลลูกค้า";
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

        public customer_info()
        {
            InitializeComponent();
        }

        private void customer_info_Load(object sender, EventArgs e)
        {
            Customer_info();
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

        private void submit_btn_Click(object sender, EventArgs e)
        {
            try
            {
                if (fname_txt.Text == "" || lname_txt.Text == "" || phone_txt.Text == "" || address_txt.Text == "")
                {
                    MessageBox.Show("กรุณากรอกข้อมูลให้ครบ !", "ตรวจพบข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (phone_txt.Text.Length != 10)
                {
                    MessageBox.Show("กรุณากรอกเบอร์10ตัวเท่านั้น !", "ตรวจพบข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MySqlConnection conn = databaseConnection();
                    string sql = "INSERT INTO ข้อมูลลูกค้า (ชื่อ,สกุล,เบอร์โทร,ที่อยู่,วันนำเข้า) VALUES('" + fname_txt.Text + "','" + lname_txt.Text + "','" + phone_txt.Text + "','" + address_txt.Text + "','" + this.dateTimePicker1.Value.ToString("dd-MM-yyyy") + "')";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (rows > 0)
                    {
                        MessageBox.Show("เพิ่มข้อมูลสำเร็จ", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        Customer_info();
                        resettext();
                    } 
                }
            }
            catch (Exception EX)
            {
                MessageBox.Show(EX.Message);
            }
        }

        private void delete_btn_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dialogResult = MessageBox.Show("ลบหรือไม่ ?", "ยีนยันการลบ", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.OK)
                {
                    int selectedRow = dataGridView1.CurrentCell.RowIndex;
                    int deleteId = Convert.ToInt32(dataGridView1.Rows[selectedRow].Cells["รหัสลูกค้า"].Value);
                    MySqlConnection conn = databaseConnection();
                    string sql = "DELETE FROM ข้อมูลลูกค้า WHERE รหัสลูกค้า = '" + deleteId + "'";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (rows > 0)
                    {
                        MessageBox.Show("ลบข้อมูลสำเร็จ", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        Customer_info();
                        resettext();
                    } 
                } 
            }
            catch (Exception EX)
            {
                MessageBox.Show(EX.Message);
            }
            
        }

        private void edit_btn_Click(object sender, EventArgs e)
        {
            try
            {
                if (fname_txt.Text == "" || lname_txt.Text == "" || phone_txt.Text == "" || address_txt.Text == "")
                {
                    MessageBox.Show("กรุณากรอกข้อมูลให้ครบ !", "ตรวจพบข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (phone_txt.Text.Length != 10)
                {
                    MessageBox.Show("กรุณากรอกเบอร์10ตัวเท่านั้น !", "ตรวจพบข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    int selectedRow = dataGridView1.CurrentCell.RowIndex;
                    int editId = Convert.ToInt32(dataGridView1.Rows[selectedRow].Cells["รหัสลูกค้า"].Value);

                    MySqlConnection conn = databaseConnection();

                    string sql = "UPDATE ข้อมูลลูกค้า SET ชื่อ = '" + fname_txt.Text + "',สกุล = '" + lname_txt.Text + "',เบอร์โทร = '" + phone_txt.Text + "',ที่อยู่ = '" + address_txt.Text + "' WHERE รหัสลูกค้า = '" + editId + "'";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    conn.Open();

                    int rows = cmd.ExecuteNonQuery();
                    conn.Close();

                    if (rows > 0)
                    {
                        MessageBox.Show("แก้ไขข้อมูลสำเร็จ", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        Customer_info();
                        resettext();
                    } 
                }
            }
            catch (Exception EX)
            {
                MessageBox.Show(EX.Message);
            } 
        }

        private void fname_txt_KeyPress(object sender, KeyPressEventArgs e)
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

        private void lname_txt_KeyPress(object sender, KeyPressEventArgs e)
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

        private void phone_txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                MessageBox.Show("ไม่สามารถใส่อักษรได้ !", "ตรวจพบข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Handled = true;
            }
            
        }

        private void resettext()
        {
            fname_txt.ResetText();
            lname_txt.ResetText();
            phone_txt.ResetText();
            address_txt.ResetText();
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
                cmd.CommandText = "select รหัสลูกค้า,ชื่อ,สกุล,เบอร์โทร,ที่อยู่,วันนำเข้า from ข้อมูลลูกค้า where ชื่อ like '%" + search_txt.Text + "%'";
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
            this.search_txt.Clear();
            Customer_info();
        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.CurrentRow.Selected = true;
            fname_txt.Text = dataGridView1.Rows[e.RowIndex].Cells["ชื่อ"].FormattedValue.ToString();
            lname_txt.Text = dataGridView1.Rows[e.RowIndex].Cells["สกุล"].FormattedValue.ToString();
            phone_txt.Text = dataGridView1.Rows[e.RowIndex].Cells["เบอร์โทร"].FormattedValue.ToString();
            address_txt.Text = dataGridView1.Rows[e.RowIndex].Cells["ที่อยู่"].FormattedValue.ToString();
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
