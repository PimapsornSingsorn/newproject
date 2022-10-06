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
    public partial class order : Form
    {
        public order()
        {
            InitializeComponent();           
        }

        private MySqlConnection databaseConnection()
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=farm_dtb;";
            MySqlConnection connection = new MySqlConnection(connectionString);
            return connection;
        }

        private void stock_egg() //คลังไข่
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
                dataGridView1.DataSource = ds.Tables[0].DefaultView;
            }
            catch (Exception EX)
            {
                MessageBox.Show(EX.Message);
            }
        }

        private void stock() //ตะกร้า
        {
            try
            {
                MySqlConnection connection = databaseConnection();
                DataSet ds = new DataSet();
                connection.Open();
                MySqlCommand cmd;
                cmd = connection.CreateCommand();
                cmd.CommandText = "select * from ตะกร้า";
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
        private void customer_code() 
        {
            try
            {
                MySqlConnection connection = databaseConnection();
                connection.Open();
                MySqlCommand cmd;
                cmd = connection.CreateCommand();
                cmd.CommandText = "select รหัสลูกค้า from ข้อมูลลูกค้า";
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    comboBox2.Items.Add(reader.GetString(0));
                }
                connection.Close();
            }
            catch (Exception EX)
            {
                MessageBox.Show(EX.Message);
            }
            
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

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            this.Hide();
            Mainmenu mainmenu = new Mainmenu();
            mainmenu.Show();
        }

        private void order_Load(object sender, EventArgs e)
        {
            resettakra();
            customer_code();
            stock();
            stock_egg();
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                MessageBox.Show("ไม่สามารถใส่อักษรได้ !", "ตรวจพบข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Handled = true;
            }
        }

        private void button8_Click(object sender, EventArgs e) //รับรายการ
        {            
            if (comboBox2.Text == "" || comboBox1.Text == "" || textBox7.Text == "0" || textBox7.Text == "" || price.Text == "")
            {
                MessageBox.Show("กรุณากรอกข้อมูลให้ครบ !", "ตรวจพบข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    int total_price = Convert.ToInt32(price.Text) + Convert.ToInt32(Total_price.Text);
                    Total_price.Text = Convert.ToString(total_price);
                    egg_stock_delete(); //ลบไข่ในคลัง
                    MySqlConnection conn = databaseConnection();
                    string sql = "INSERT INTO ตะกร้า (ไซต์,จำนวน,ราคา) VALUES('" + comboBox1.Text + "','" + textBox7.Text + "','" + price.Text + "')";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    conn.Close();

                    if (rows > 0)
                    {
                        
                        MessageBox.Show("เพิ่มลงตะกร้าแล้ว");
                        stock(); //แสดงตะกร้า
                        stock_egg(); //แสดงคลังไข่ล่าสุด
                        resettext();
                    }
                    
                }
                catch (Exception EX)
                {
                    MessageBox.Show(EX.Message);
                }
            }
            
        }
        private void comboBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection connection = databaseConnection();
                connection.Open();
                MySqlCommand cmd;
                cmd = connection.CreateCommand();
                cmd.CommandText = "select ชื่อ from ข้อมูลลูกค้า where รหัสลูกค้า = '" + comboBox2.Text + "'";
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read() == true)
                {
                    label2.Text = dt.Rows[0][0].ToString();
                    label16.Text = dt.Rows[0][0].ToString();
                    connection.Close();
                }
                
            }
            catch (Exception EX)
            {
                MessageBox.Show(EX.Message);
            }           
        }

        private void delete_btn_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("ลบหรือไม่ ?", "ยีนยันการลบ", MessageBoxButtons.OKCancel ,MessageBoxIcon.Question);
            if (dialogResult == DialogResult.OK)
            {
                try
                {
                    int selectedRow = dataGridView2.CurrentCell.RowIndex;
                    int deleteId = Convert.ToInt32(dataGridView2.Rows[selectedRow].Cells["รหัสรายการ"].Value);
                    MySqlConnection conn = databaseConnection();
                    string sql = "DELETE FROM ตะกร้า WHERE รหัสรายการ = '" + deleteId + "'";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (rows > 0)
                    {
                        Total_price.Text = Convert.ToString(price.Text);
                        MessageBox.Show("ลบข้อมูลสำเร็จ", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        stock();
                        stock_egg();
                    }
                }
                catch (Exception EX)
                {
                    MessageBox.Show(EX.Message);
                }
            }
            

        }

        private void edit_btn_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "" || textBox7.Text == "" || price.Text == "")
            {
                MessageBox.Show("กรุณากรอกข้อมูลให้ครบ !", "ตรวจพบข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    int selectedRow = dataGridView2.CurrentCell.RowIndex;
                    int editId = Convert.ToInt32(dataGridView2.Rows[selectedRow].Cells["รหัสรายการ"].Value);
                    MySqlConnection conn = databaseConnection();
                    string sql = "UPDATE ตะกร้า SET ไซต์ = '" + comboBox1.Text + "',จำนวน = '" + textBox7.Text + "',ราคา = '" + price.Text + "' WHERE รหัสรายการ = '" + editId + "'";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (rows > 0)
                    {
                        Total_price.Text = Convert.ToString(price.Text);
                        MessageBox.Show("แก้ไขข้อมูลสำเร็จ", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        stock();
                        stock_egg();
                        resettext();
                    }
                }
                catch (Exception EX)
                {
                    MessageBox.Show(EX.Message);
                }

            }
        }

        private void resettakra()
        {
            try
            {
                MySqlConnection conn = databaseConnection();
                string sql = "DELETE FROM ตะกร้า";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                conn.Close();
                if (rows > 0)
                {
                    
                    stock();
                    stock_egg();
                }
            }
            catch (Exception EX)
            {
                MessageBox.Show(EX.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("ล้างตะกร้าหรือไม่ ?", "ยีนยันการรีเซต", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.OK)
            {
                resettakra();
                price.ResetText();
                Total_price.Text = "0";
            }
        }

        private void order_list() //เพื่อรวมรายการ
        {
            try
            {               
                MySqlConnection connection = databaseConnection();                
                connection.Open();
                MySqlCommand cmd;
                cmd = connection.CreateCommand();
                cmd.CommandText = "select * FROM ตะกร้า ";                
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    listBox1.Items.Add("เบอร์ " + reader.GetString(1) + "\tจำนวน " + reader.GetString(2) + " แผง\t" + "ราคา " + reader.GetString(3) + " บาท");
                }
                reader.Close();
                connection.Close();
            }
            catch (Exception EX)
            {
                MessageBox.Show(EX.Message);
            }
        }


        private void button9_Click(object sender, EventArgs e) //บันทึก
        {
            
            if (comboBox2.Text == "" || comboBox2.Text == "" || (radioButton1.Checked == false && radioButton2.Checked == false) || listBox1.Items == null)
            {
                MessageBox.Show("กรุณากรอกข้อมูลให้ครบ !", "ตรวจพบข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {              
                order_list(); //ตะกร้าไปลิสบ็อก
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

                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    foreach (object item in listBox1.Items)
                    {
                        sb.Append(item.ToString());
                        sb.Append("\n");
                    }

                    
                    MySqlConnection conn = databaseConnection();
                    string sql = "INSERT INTO จัดการคำสั่งซื้อ (รหัสลูกค้า,ชื่อลูกค้า,รายการ,รวมเงิน,วันที่เพิ่มข้อมูล,สถานะ) VALUES('" + comboBox2.Text + "','" + label2.Text + "','" + sb.ToString() + "','" + Total_price.Text + "','" + this.dateTimePicker1.Value.ToString("dd-MM-yyyy") + "','" + status_ + "')";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (rows > 0)
                    {
                        MessageBox.Show("บันทึกเรียบร้อย", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                        listBox1.Items.Clear();
                        label2.ResetText();
                        Total_price.Text = "0";
                        comboBox2.ResetText();
                        radioButton1.Checked = false;
                        radioButton2.Checked = false;
                        resettext();
                    }
                    resettakra();
                }
                catch (Exception EX)
                {
                    MessageBox.Show(EX.Message);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            order_history order_History = new order_history();
            order_History.ShowDialog();

        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView2.CurrentRow.Selected = true;
            comboBox1.Text = dataGridView2.Rows[e.RowIndex].Cells["ไซต์"].FormattedValue.ToString();
            textBox7.Text = dataGridView2.Rows[e.RowIndex].Cells["จำนวน"].FormattedValue.ToString();
            price.Text = dataGridView2.Rows[e.RowIndex].Cells["ราคา"].FormattedValue.ToString();
        }

        private void resettext()
        {
            comboBox1.ResetText();
            textBox7.Value = 0;
            price.ResetText();
            label15.Text = "0";
            
        }

        private void clear_btn_Click(object sender, EventArgs e)
        {
            resettext();          
        }
        private void egg_stock_delete() //ลบไข่ออกจากคลัง
        {
            try
            {
                MySqlConnection connection = databaseConnection();
                connection.Open();
                MySqlCommand cmd;
                cmd = connection.CreateCommand();
                if (comboBox1.Text == "0")
                {
                    cmd.CommandText = "select เบอร์0 FROM egg_stock ";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    reader.Close();
                    connection.Close();
                    int total = Convert.ToInt32(dt.Rows[0][0].ToString()) - (Convert.ToInt32(textBox7.Text) * 30);
                    MySqlConnection conn = databaseConnection();
                    string sql = "UPDATE egg_stock SET เบอร์0 = '" + total + "'";
                    MySqlCommand cmds = new MySqlCommand(sql, conn);
                    conn.Open();
                    cmds.ExecuteNonQuery();
                    conn.Close();
                }
                else if (comboBox1.Text == "1")
                {               
                    cmd.CommandText = "select เบอร์1 FROM egg_stock ";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    reader.Close();
                    connection.Close();
                    int total = Convert.ToInt32(dt.Rows[0][0].ToString()) - (Convert.ToInt32(textBox7.Text) * 30);
                    MySqlConnection conn = databaseConnection();
                    string sql = "UPDATE egg_stock SET เบอร์1 = '" + total + "'";
                    MySqlCommand cmds = new MySqlCommand(sql, conn);
                    conn.Open();
                    cmds.ExecuteNonQuery();
                    conn.Close();
                }
                else if (comboBox1.Text == "2")
                {               
                    cmd.CommandText = "select เบอร์2 FROM egg_stock ";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    reader.Close();
                    connection.Close();
                    int total = Convert.ToInt32(dt.Rows[0][0].ToString()) - (Convert.ToInt32(textBox7.Text) * 30);
                    MySqlConnection conn = databaseConnection();
                    string sql = "UPDATE egg_stock SET เบอร์2 = '" + total + "'";
                    MySqlCommand cmds = new MySqlCommand(sql, conn);
                    conn.Open();
                    cmds.ExecuteNonQuery();
                    conn.Close();
                }
                else if (comboBox1.Text == "3")
                {               
                    cmd.CommandText = "select เบอร์3 FROM egg_stock ";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    reader.Close();
                    connection.Close();
                    int total = Convert.ToInt32(dt.Rows[0][0].ToString()) - (Convert.ToInt32(textBox7.Text) * 30);
                    MySqlConnection conn = databaseConnection();
                    string sql = "UPDATE egg_stock SET เบอร์3 = '" + total + "'";
                    MySqlCommand cmds = new MySqlCommand(sql, conn);
                    conn.Open();
                    cmds.ExecuteNonQuery();
                    conn.Close();
                }
                else if (comboBox1.Text == "4")
                {               
                    cmd.CommandText = "select เบอร์4 FROM egg_stock ";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    reader.Close();
                    connection.Close();
                    int total = Convert.ToInt32(dt.Rows[0][0].ToString()) - (Convert.ToInt32(textBox7.Text) * 30);
                    MySqlConnection conn = databaseConnection();
                    string sql = "UPDATE egg_stock SET เบอร์4 = '" + total + "'";
                    MySqlCommand cmds = new MySqlCommand(sql, conn);
                    conn.Open();
                    cmds.ExecuteNonQuery();
                    conn.Close();
                }
                else if (comboBox1.Text == "คละไซต์")
                {
                    cmd.CommandText = "select คละไซต์ FROM egg_stock ";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    reader.Close();
                    connection.Close();
                    int total = Convert.ToInt32(dt.Rows[0][0].ToString()) - (Convert.ToInt32(textBox7.Text) * 30);
                    MySqlConnection conn = databaseConnection();
                    string sql = "UPDATE egg_stock SET คละไซต์ = '" + total + "'";
                    MySqlCommand cmds = new MySqlCommand(sql, conn);
                    conn.Open();
                    cmds.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception EX)
            {
                MessageBox.Show(EX.Message);
            }
            
        }


        private void comboBox1_TextChanged(object sender, EventArgs e) //จำนวนไข่คงเหลือ
        {
            try
            {
                if (comboBox1.Text != "")
                {
                    MySqlConnection connection = databaseConnection();
                    connection.Open();
                    MySqlCommand cmd;
                    cmd = connection.CreateCommand();
                    if (comboBox1.Text == "0")
                    {
                        cmd.CommandText = "select เบอร์0 FROM egg_stock ";
                    }
                    else if (comboBox1.Text == "1")
                    {
                        cmd.CommandText = "select เบอร์1 FROM egg_stock ";
                    }
                    else if (comboBox1.Text == "2")
                    {
                        cmd.CommandText = "select เบอร์2 FROM egg_stock ";
                    }
                    else if (comboBox1.Text == "3")
                    {
                        cmd.CommandText = "select เบอร์3 FROM egg_stock ";
                    }
                    else if (comboBox1.Text == "4")
                    {
                        cmd.CommandText = "select เบอร์4 FROM egg_stock ";
                    }
                    else if (comboBox1.Text == "คละไซต์")
                    {
                        cmd.CommandText = "select คละไซต์ FROM egg_stock ";
                    }
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    reader.Close();
                    connection.Close();
                    int ch_total = Convert.ToInt32(dt.Rows[0][0].ToString()) / 30;
                    label15.Text = ch_total.ToString();
                }
                
            }
            catch (Exception EX)
            {
                MessageBox.Show(EX.Message);
            }           
        }
        private void textBox7_ValueChanged(object sender, EventArgs e) //แสดงราคา
        {
            if (Convert.ToInt32(textBox7.Value) > Convert.ToInt32(label15.Text))
            {
                MessageBox.Show("จำนวนไข่ในคลังไม่เพียงพอ !", "ตรวจพบข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (comboBox1.Text == "0")
                {
                    price.Text = Convert.ToString(Convert.ToInt32(textBox7.Value) * 96);
                }
                else if (comboBox1.Text == "1")
                {
                    price.Text = Convert.ToString(Convert.ToInt32(textBox7.Value) * 90);
                }
                else if (comboBox1.Text == "2")
                {
                    price.Text = Convert.ToString(Convert.ToInt32(textBox7.Value) * 84);
                }
                else if (comboBox1.Text == "3")
                {
                    price.Text = Convert.ToString(Convert.ToInt32(textBox7.Value) * 81);
                }
                else if (comboBox1.Text == "4")
                {
                    price.Text = Convert.ToString(Convert.ToInt32(textBox7.Value) * 69);
                }
                else if (comboBox1.Text == "คละไซต์")
                {
                    price.Text = Convert.ToString(Convert.ToInt32(textBox7.Value) * 84);
                }
            }
        }
        private void label15_TextChanged(object sender, EventArgs e)
        {
            textBox7.Value = 0;
            textBox7.Maximum = Convert.ToInt32(label15.Text);
            textBox7.Minimum = 0;
        }

        private void dataGridView2_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                dataGridView2.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.SandyBrown;
                dataGridView2.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
            }
        }

        private void dataGridView2_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                dataGridView2.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                dataGridView2.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
            }
        }
    }    
}
