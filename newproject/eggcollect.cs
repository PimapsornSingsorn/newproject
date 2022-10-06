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
    public partial class eggcollect : Form
    {
        public eggcollect()
        {
            InitializeComponent();
            
        }

        private MySqlConnection databaseConnection()
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=farm_dtb;";
            MySqlConnection connection = new MySqlConnection(connectionString);
            return connection;
        }

        private void stock_egg()
        {
            try
            {
                MySqlConnection connection = databaseConnection();
                DataSet ds = new DataSet();
                connection.Open();
                MySqlCommand cmd;
                cmd = connection.CreateCommand();
                cmd.CommandText = "select * from egg_stock";
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(ds);
                connection.Close();
                dataGridView2.DataSource = ds.Tables[0].DefaultView;
            }
            catch (Exception EX)
            {
                MessageBox.Show(EX.Message);
            }           
        }

        private void egg_collection()
        {
            try
            {
                MySqlConnection connection = databaseConnection();
                DataSet ds = new DataSet();
                connection.Open();
                MySqlCommand cmd;
                cmd = connection.CreateCommand();
                cmd.CommandText = "select * from เก็บไข่	";
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

        private void egg_stock()
        {
            int tungmod = Convert.ToInt32(amount_chic_txt.Text);
            int ruomkai = Convert.ToInt32(amount_size0_txt.Text) + Convert.ToInt32(amount_size1_txt.Text) + Convert.ToInt32(amount_size2_txt.Text) + Convert.ToInt32(amount_size3_txt.Text) + Convert.ToInt32(amount_size4_txt.Text);
            int klasize = tungmod - ruomkai;
            try
            {
                MySqlConnection connection = databaseConnection();
                connection.Open();
                MySqlCommand cmd;
                cmd = connection.CreateCommand();
                cmd.CommandText = "select * FROM egg_stock ";
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                MySqlDataReader reader = cmd.ExecuteReader();
                reader.Close();
                connection.Close();
                int total_amount0 = Convert.ToInt32(amount_size0_txt.Text) + Convert.ToInt32(dt.Rows[0][0].ToString());
                int total_amount1 = Convert.ToInt32(amount_size1_txt.Text) + Convert.ToInt32(dt.Rows[0][1].ToString());
                int total_amount2 = Convert.ToInt32(amount_size2_txt.Text) + Convert.ToInt32(dt.Rows[0][2].ToString());
                int total_amount3 = Convert.ToInt32(amount_size3_txt.Text) + Convert.ToInt32(dt.Rows[0][3].ToString());
                int total_amount4 = Convert.ToInt32(amount_size4_txt.Text) + Convert.ToInt32(dt.Rows[0][4].ToString());
                int total_klasize = klasize + Convert.ToInt32(dt.Rows[0][5].ToString());
                MySqlConnection conn = databaseConnection();
                string sql = "UPDATE egg_stock SET เบอร์0 = '" + total_amount0 + "',เบอร์1 = '" + total_amount1 + "',เบอร์2 = '" + total_amount2 + "',เบอร์3 = '" + total_amount3 + "',เบอร์4 = '" + total_amount4 + "',คละไซต์ = '" + total_klasize + "'";
                MySqlCommand cmds = new MySqlCommand(sql, conn);
                conn.Open();
                cmds.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception EX)
            {
                MessageBox.Show(EX.Message);
            }
            
        }

        private void eggcollection_Load(object sender, EventArgs e)
        {
            egg_collection();
            stock_egg();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            this.Hide();
            Mainmenu mainmenu = new Mainmenu();
            mainmenu.Show();
        }
        private void pictureclose_Click(object sender, EventArgs e)
        {
            MessageBox.Show("ออกจากโปรแกรม");
            this.Close();
            Application.Exit();
        }

        private void submit_btn_Click(object sender, EventArgs e)
        {
            try
            {
                stock_egg();
                if (amount_chic_txt.Text == "" || amount_size0_txt.Text == "" || amount_size1_txt.Text == "" || amount_size2_txt.Text == "" || amount_size3_txt.Text == "" || amount_size4_txt.Text == "")
                {
                    MessageBox.Show("กรุณากรอกข้อมูลให้ครบ !", "ตรวจพบข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if ((Convert.ToInt32(amount_size0_txt.Text) + Convert.ToInt32(amount_size1_txt.Text) + Convert.ToInt32(amount_size2_txt.Text) + Convert.ToInt32(amount_size3_txt.Text) + Convert.ToInt32(amount_size4_txt.Text)) > Convert.ToInt32(amount_chic_txt.Text))
                {
                    MessageBox.Show("กรุณาใส่จำนวนให้ถูกต้อง !", "ตรวจพบข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MySqlConnection conn = databaseConnection();
                    string sql = "INSERT INTO เก็บไข่ (วันที่เก็บ,จำนวนไข่ที่เก็บได้ทั้งหมด,เบอร์0,เบอร์1,เบอร์2,เบอร์3,เบอร์4,คละไซต์) VALUES('" + this.dateTimePicker1.Value.ToString("dd-MM-yyyy") + "','" + amount_chic_txt.Text + "','" + amount_size0_txt.Text + "','" + amount_size1_txt.Text + "','" + amount_size2_txt.Text + "','" + amount_size3_txt.Text + "','" + amount_size4_txt.Text + "','" + (Convert.ToInt32(amount_chic_txt.Text) - (Convert.ToInt32(amount_size0_txt.Text) + Convert.ToInt32(amount_size1_txt.Text) + Convert.ToInt32(amount_size2_txt.Text) + Convert.ToInt32(amount_size3_txt.Text) + Convert.ToInt32(amount_size4_txt.Text))) + "')";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (rows > 0)
                    {
                        MessageBox.Show("เพิ่มข้อมูลสำเร็จ");
                        egg_collection();
                        egg_stock();
                    }
                }
                
                stock_egg();
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
                DialogResult dialogResult = MessageBox.Show("ลบหรือไม่ ?", "ยีนยันการลบ", MessageBoxButtons.OKCancel);
                if (dialogResult == DialogResult.OK)
                {
                    stock_egg();
                    int selectedRow = dataGridView1.CurrentCell.RowIndex;
                    int deleteId = Convert.ToInt32(dataGridView1.Rows[selectedRow].Cells["รหัสการเก็บไข่"].Value);
                    MySqlConnection conn = databaseConnection();
                    string sql = "DELETE FROM เก็บไข่ WHERE รหัสการเก็บไข่ = '" + deleteId + "'";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (rows > 0)
                    {
                        MessageBox.Show("ลบข้อมูลสำเร็จ");
                        egg_collection();
                    
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
                if (amount_chic_txt.Text == "" || amount_size0_txt.Text == "" || amount_size1_txt.Text == "" || amount_size2_txt.Text == "" || amount_size3_txt.Text == "" || amount_size4_txt.Text == "")
                {
                    MessageBox.Show("กรุณากรอกข้อมูลให้ครบ !", "ตรวจพบข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if ((Convert.ToInt32(amount_size0_txt.Text) + Convert.ToInt32(amount_size1_txt.Text) + Convert.ToInt32(amount_size2_txt.Text) + Convert.ToInt32(amount_size3_txt.Text) + Convert.ToInt32(amount_size4_txt.Text)) > Convert.ToInt32(amount_chic_txt.Text))
                {
                    MessageBox.Show("กรุณาใส่จำนวนให้ถูกต้อง !", "ตรวจพบข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    stock_egg();
                    int selectedRow = dataGridView1.CurrentCell.RowIndex;
                    int editId = Convert.ToInt32(dataGridView1.Rows[selectedRow].Cells["รหัสการเก็บไข่"].Value);
                    MySqlConnection conn = databaseConnection();
                    string sql = "UPDATE เก็บไข่ SET จำนวนไข่ที่เก็บได้ทั้งหมด = '" + amount_chic_txt.Text + "',เบอร์0 = '" + amount_size0_txt + "',เบอร์1 = '" + amount_size1_txt + "',เบอร์2 = '" + amount_size2_txt + "',เบอร์3 = '" + amount_size3_txt + "',เบอร์4 = '" + amount_size4_txt + "',คละไซต์ = '" + (Convert.ToInt32(amount_chic_txt.Text) - (Convert.ToInt32(amount_size0_txt.Text) + Convert.ToInt32(amount_size1_txt.Text) + Convert.ToInt32(amount_size2_txt.Text) + Convert.ToInt32(amount_size3_txt.Text) + Convert.ToInt32(amount_size4_txt.Text))) + "' WHERE รหัสการเก็บไข่ = '" + editId + "'";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (rows > 0)
                    {
                        MessageBox.Show("แก้ไขข้อมูลสำเร็จ");
                        egg_collection();
                    
                    }
                }
            }
            catch (Exception EX)
            {
                MessageBox.Show(EX.Message);
            }
        }

        private void clear_btn_Click(object sender, EventArgs e)
        {
            this.amount_chic_txt.Clear();
            this.amount_size0_txt.Clear();
            this.amount_size1_txt.Clear();
            this.amount_size2_txt.Clear();
            this.amount_size3_txt.Clear();
            this.amount_size4_txt.Clear();
        }

        private void amount_chic_txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                MessageBox.Show("ไม่สามารถใส่อักษรได้ !", "ตรวจพบข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Handled = true;
            }
        }

        private void amount_size0_txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                MessageBox.Show("ไม่สามารถใส่อักษรได้ !", "ตรวจพบข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Handled = true;
            }
        }

        private void amount_size1_txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                MessageBox.Show("ไม่สามารถใส่อักษรได้ !", "ตรวจพบข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Handled = true;
            }
        }

        private void amount_size2_txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                MessageBox.Show("ไม่สามารถใส่อักษรได้ !", "ตรวจพบข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Handled = true;
            }
        }

        private void amount_size3_txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                MessageBox.Show("ไม่สามารถใส่อักษรได้ !", "ตรวจพบข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Handled = true;
            }
        }

        private void amount_size4_txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                MessageBox.Show("ไม่สามารถใส่อักษรได้ !", "ตรวจพบข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                cmd.CommandText = "select รหัสการเก็บไข่,วันที่เก็บ,จำนวนไข่ที่เก็บได้ทั้งหมด,เบอร์0,เบอร์1,เบอร์2,เบอร์3,เบอร์4,คละไซต์ from เก็บไข่ where วันที่เก็บ = '" + this.dateTimePicker2.Value.ToString("dd-MM-yyyy") + "'";
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
            egg_collection();
            
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.CurrentRow.Selected = true;
            amount_chic_txt.Text = dataGridView1.Rows[e.RowIndex].Cells["จำนวนไข่ที่เก็บได้ทั้งหมด"].FormattedValue.ToString();
            amount_size0_txt.Text = dataGridView1.Rows[e.RowIndex].Cells["เบอร์0"].FormattedValue.ToString();
            amount_size1_txt.Text = dataGridView1.Rows[e.RowIndex].Cells["เบอร์1"].FormattedValue.ToString();
            amount_size2_txt.Text = dataGridView1.Rows[e.RowIndex].Cells["เบอร์2"].FormattedValue.ToString();
            amount_size3_txt.Text = dataGridView1.Rows[e.RowIndex].Cells["เบอร์3"].FormattedValue.ToString();
            amount_size4_txt.Text = dataGridView1.Rows[e.RowIndex].Cells["เบอร์4"].FormattedValue.ToString();
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
