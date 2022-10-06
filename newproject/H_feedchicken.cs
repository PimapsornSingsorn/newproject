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
    public partial class feedchicken : Form
    {
        public feedchicken()
        {
            InitializeComponent();
        }

        private MySqlConnection databaseConnection()
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=farm_dtb;";
            MySqlConnection connection = new MySqlConnection(connectionString);
            return connection;
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            this.Hide();
            Mainmenu mainmenu = new Mainmenu();
            mainmenu.Show();
        }

        private void Feedchicken() 
        {
            try
            {
                MySqlConnection connection = databaseConnection();
                DataSet ds = new DataSet();
                connection.Open();
                MySqlCommand cmd;
                cmd = connection.CreateCommand();
                cmd.CommandText = "select * from ให้อาหารไก่";
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

        private void food_code() //ไอเทมคอมโบ
        {
            try
            {
                MySqlConnection connection = databaseConnection();
                connection.Open();
                MySqlCommand cmd;
                cmd = connection.CreateCommand();
                cmd.CommandText = "select รหัส from ข้อมูลอาหาร";
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    food_id_txt.Items.Add(reader.GetString(0));
                }
                connection.Close();
            }
            catch (Exception EX)
            {
                MessageBox.Show(EX.Message);
            }           
        }

        private void coop_code()
        {
            try
            {
                MySqlConnection connection = databaseConnection();
                connection.Open();
                MySqlCommand cmd;
                cmd = connection.CreateCommand();
                cmd.CommandText = "select รหัสคอกเลี้ยง from ข้อมูลคอกเลี้ยง";
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    coop_id_txt.Items.Add(reader.GetString(0));
                }
                connection.Close();
            }
            catch (Exception EX)
            {
                MessageBox.Show(EX.Message);
            }
            
        }

        private void feedchicken_Load(object sender, EventArgs e)
        {
            Feedchicken();
            food_code();
            coop_code();
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
                
                if (food_id_txt.Text == "" || food_name_txt.Text == "" || food_amount_txt.Text == "" || coop_id_txt.Text == "" || coop_amount_txt.Text == "" || coop_name_txt.Text == "")
                {
                    MessageBox.Show("กรุณากรอกข้อมูลให้ครบ !", "ตรวจพบข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (Convert.ToInt32(food_amount_kong_txt.Text) < (Convert.ToInt32(food_amount_txt.Text)))
                {
                    MessageBox.Show("โปรดใส่จำนวนอาหารให้ถูกต้อง !", "ตรวจพบข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {   
                    MySqlConnection conn = databaseConnection();
                    string sql = "INSERT INTO ให้อาหารไก่ (วันที่ให้อาหาร,รหัสคอกเลี้ยง,ชื่อคอกเลี้ยง,จำนวนไก่ในคอก,รหัสอาหาร,ชื่ออาหาร,จำนวนอาหารที่ให้) VALUES('" + this.dateTimePicker1.Value.ToString("dd-MM-yyyy") + "','" + coop_id_txt.Text + "','" + coop_name_txt.Text + "','" + coop_amount_txt.Text + "','" + food_id_txt.Text +"','"+ food_name_txt.Text + "','" + food_amount_txt.Text +"')";
                    MySqlCommand com;
                    com = conn.CreateCommand();
                    
                    com.CommandText = " UPDATE ข้อมูลอาหาร SET จำนวนคงเหลือ = '" + (Convert.ToInt32(food_amount_kong_txt.Text) - Convert.ToInt32(food_amount_txt.Text)) + "' WHERE รหัส = '" + food_id_txt.Text + "' ";           
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    conn.Open(); 
                    com.ExecuteNonQuery();
                    int rows = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (rows > 0)
                    {
                        MessageBox.Show("เพิ่มข้อมูลสำเร็จ", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        Feedchicken();
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
                    int deleteId = Convert.ToInt32(dataGridView1.Rows[selectedRow].Cells["รหัสการให้อาหาร"].Value);
                    MySqlConnection conn = databaseConnection();
                    string sql = "DELETE FROM ให้อาหารไก่ WHERE รหัสการให้อาหาร = '" + deleteId + "'";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (rows > 0)
                    {
                        MessageBox.Show("ลบข้อมูลสำเร็จ", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        Feedchicken();
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
                if (food_id_txt.Text == "" || food_name_txt.Text == "" || food_amount_txt.Text == "" || coop_id_txt.Text == "" || coop_amount_txt.Text == "" || coop_name_txt.Text == "")
                {
                    MessageBox.Show("กรุณากรอกข้อมูลให้ครบ !", "ตรวจพบข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    int selectedRow = dataGridView1.CurrentCell.RowIndex;
                    int editId = Convert.ToInt32(dataGridView1.Rows[selectedRow].Cells["รหัสการให้อาหาร"].Value);
                    MySqlConnection conn = databaseConnection();
                    string sql = "UPDATE ให้อาหารไก่ SET รหัสคอกเลี้ยง = '" + coop_id_txt.Text + "', ชื่อคอกเลี้ยง = '" + coop_name_txt.Text + "', จำนวนไก่ในคอก = '" + coop_amount_txt.Text + "', รหัสอาหาร = '" + food_id_txt.Text + "',ชื่ออาหาร = '"+ food_name_txt.Text + "', จำนวนอาหารที่ให้ = '" + food_amount_txt.Text + "' WHERE รหัสการให้อาหาร = '" + editId + "' ";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (rows > 0)
                    {
                        MessageBox.Show("แก้ไขข้อมูลสำเร็จ", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        Feedchicken();
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
            food_name_txt.ResetText();
            food_amount_kong_txt.ResetText();
            coop_amount_txt.ResetText();
            coop_name_txt.ResetText();
            coop_id_txt.SelectedIndex = -1;
            food_id_txt.SelectedIndex = -1;
            food_amount_txt.ResetText();
        }

        private void clear_btn_Click(object sender, EventArgs e)
        {
            resettext();
        }

        

        private void food_amount_txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                MessageBox.Show("ไม่สามารถใส่อักษรได้ !", "ตรวจพบข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Handled = true;
            }
        }        
        private void refresh_btn_Click(object sender, EventArgs e)
        {
            Feedchicken();
        }
        private void search_btn_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection connection = databaseConnection();
                connection.Open();
                MySqlCommand cmd;
                cmd = connection.CreateCommand();
                cmd.CommandText = "select รหัสการให้อาหาร,วันที่ให้อาหาร,รหัสคอกเลี้ยง,ชื่อคอกเลี้ยง,จำนวนไก่ในคอก,รหัสอาหาร,จำนวนอาหารที่ให้ from ให้อาหารไก่ where วันที่ให้อาหาร = '" + this.dateTimePicker2.Value.ToString("dd-MM-yyyy") + "'";
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

        private void food_id_txt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection connection = databaseConnection();
                connection.Open();
                MySqlCommand cmd;
                cmd = connection.CreateCommand();
                cmd.CommandText = "select * from ข้อมูลอาหาร where รหัส = '" + food_id_txt.Text + "'";
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read() == true)
                {                
                    food_name_txt.Text = dt.Rows[0][1].ToString(); //ชื่อ
                    food_amount_kong_txt.Text = dt.Rows[0][2].ToString(); //คงเหลือ
                    connection.Close();
                }
                
            }
            catch (Exception EX)
            {
                MessageBox.Show(EX.Message);
            }
            
        }

        private void coop_id_txt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection connection = databaseConnection();
                connection.Open();
                MySqlCommand cmd;
                cmd = connection.CreateCommand();
                cmd.CommandText = "select * from ข้อมูลคอกเลี้ยง where รหัสคอกเลี้ยง = '" + coop_id_txt.Text + "'";
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read() == true)
                {               
                    coop_name_txt.Text = dt.Rows[0][1].ToString(); //ชื่อ
                    coop_amount_txt.Text = dt.Rows[0][2].ToString(); //จำนวนไก่
                    connection.Close();
                }
               
            }
            catch (Exception EX)
            {
                MessageBox.Show(EX.Message);
            }
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.CurrentRow.Selected = true;
            coop_id_txt.Text = dataGridView1.Rows[e.RowIndex].Cells["รหัสคอกเลี้ยง"].FormattedValue.ToString();
            food_id_txt.Text = dataGridView1.Rows[e.RowIndex].Cells["รหัสอาหาร"].FormattedValue.ToString();
            food_amount_txt.Text = dataGridView1.Rows[e.RowIndex].Cells["จำนวนอาหารที่ให้"].FormattedValue.ToString();
            
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
