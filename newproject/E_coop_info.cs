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
    public partial class coop_info : Form
    {
        
        public coop_info()
        {
            InitializeComponent();
        }
        private MySqlConnection databaseConnection()
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=farm_dtb;";
            MySqlConnection connection = new MySqlConnection(connectionString);
            return connection;
        }
        private void coopp_info()
        {
            try
            {
                MySqlConnection connection = databaseConnection();
                DataSet ds = new DataSet();
                connection.Open();
                MySqlCommand cmd;
                cmd = connection.CreateCommand();
                cmd.CommandText = "select * from ข้อมูลคอกเลี้ยง	";
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

        private void coop_info_Load(object sender, EventArgs e)
        {
            coopp_info();
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
            try
            {
                if (coopname_txt.Text == "" || num_of_chic_txt.Text == "" )
                {
                    MessageBox.Show("กรุณากรอกข้อมูลให้ครบ !", "ตรวจพบข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MySqlConnection conn = databaseConnection();
                    string sql = "INSERT INTO ข้อมูลคอกเลี้ยง (ชื่อคอกเลี้ยง,จำนวนไก่ในคอก,วันที่เพิ่มข้อมูล) VALUES('" + coopname_txt.Text + "','" + num_of_chic_txt.Text + "','" + this.dateTimePicker1.Value.ToString("dd-MM-yyyy") + "')";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (rows > 0)
                    {
                        MessageBox.Show("เพิ่มข้อมูลสำเร็จ", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        coopp_info();
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
                DialogResult dialogResult = MessageBox.Show("ลบหรือไม่ ?", "ยีนยันการลบ", MessageBoxButtons.OKCancel,MessageBoxIcon.Question);
                if (dialogResult == DialogResult.OK)
                {
                    int selectedRow = dataGridView1.CurrentCell.RowIndex;
                    int deleteId = Convert.ToInt32(dataGridView1.Rows[selectedRow].Cells["รหัสคอกเลี้ยง"].Value);
                    MySqlConnection conn = databaseConnection();
                    string sql = "DELETE FROM ข้อมูลคอกเลี้ยง WHERE รหัสคอกเลี้ยง = '" + deleteId + "'";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (rows > 0)
                    {
                        MessageBox.Show("ลบข้อมูลสำเร็จ", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        coopp_info();
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
                if (coopname_txt.Text == "" || num_of_chic_txt.Text == "")
                {
                    MessageBox.Show("กรุณากรอกข้อมูลให้ครบ !", "ตรวจพบข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    int selectedRow = dataGridView1.CurrentCell.RowIndex;
                    int editId = Convert.ToInt32(dataGridView1.Rows[selectedRow].Cells["รหัสคอกเลี้ยง"].Value);

                    MySqlConnection conn = databaseConnection();

                    string sql = "UPDATE ข้อมูลคอกเลี้ยง SET ชื่อคอกเลี้ยง = '" + coopname_txt.Text + "',จำนวนไก่ในคอก = '" + num_of_chic_txt.Text + "' WHERE รหัสคอกเลี้ยง = '" + editId + "'";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    conn.Open();

                    int rows = cmd.ExecuteNonQuery();
                    conn.Close();

                    if (rows > 0)
                    {
                        MessageBox.Show("แก้ไขข้อมูลสำเร็จ", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        coopp_info();
                        resettext();
                    }
                }
            }
            catch (Exception EX)
            {
                MessageBox.Show(EX.Message);
            } 
        }

        private void resettext()
        {
            coopname_txt.ResetText();
            num_of_chic_txt.ResetText();
            
        }

        private void clear_btn_Click(object sender, EventArgs e)
        {
            resettext();
        }

        private void num_of_chic_txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                MessageBox.Show("ไม่สามารถใส่อักษรได้ !", "ตรวจพบข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Handled = true;
            }
        }

        private void coopname_txt_KeyPress(object sender, KeyPressEventArgs e)
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

        private void search_btn_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection connection = databaseConnection();
                connection.Open();
                MySqlCommand cmd;
                cmd = connection.CreateCommand();
                cmd.CommandText = "select รหัสคอกเลี้ยง,ชื่อคอกเลี้ยง,จำนวนไก่ในคอก,วันที่เพิ่มข้อมูล from ข้อมูลคอกเลี้ยง where ชื่อคอกเลี้ยง like '%" + search_txt.Text + "%'";
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
            coopp_info();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.CurrentRow.Selected = true;
            coopname_txt.Text = dataGridView1.Rows[e.RowIndex].Cells["ชื่อคอกเลี้ยง"].FormattedValue.ToString();
            num_of_chic_txt.Text = dataGridView1.Rows[e.RowIndex].Cells["จำนวนไก่ในคอก"].FormattedValue.ToString();
            
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
