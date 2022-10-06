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
    public partial class stock : Form
    {
        private MySqlConnection databaseConnection()
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=farm_dtb;";
            MySqlConnection connection = new MySqlConnection(connectionString);
            return connection;
        }

        public stock()
        {
            InitializeComponent();
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
            eggcollect eggcollect = new eggcollect();
            eggcollect.ShowDialog();
        }

        private void stock_egg() //แสดงในในคลัง
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

        private void stock_Load(object sender, EventArgs e)
        {
            stock_egg();
        }

        private void edit_btn_Click(object sender, EventArgs e)
        {
            if(amount_txt.Text == ""|| comboBox1.Text == "")
            {
                MessageBox.Show("กรุณากรอกข้อมูลให้ครบ !", "ตรวจพบข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    MySqlConnection connection = databaseConnection();
                    string sql = "";
                    if (comboBox1.Text == "เบอร์0")
                    {
                        sql = "UPDATE egg_stock SET เบอร์0 = '" + amount_txt.Text + "'";
                    }
                    else if (comboBox1.Text == "เบอร์1")
                    {
                        sql = "UPDATE egg_stock SET เบอร์1 = '" + amount_txt.Text + "'";
                    }
                    else if (comboBox1.Text == "เบอร์2")
                    {
                        sql = "UPDATE egg_stock SET เบอร์2 = '" + amount_txt.Text + "'";
                    }
                    else if (comboBox1.Text == "เบอร์3")
                    {
                        sql = "UPDATE egg_stock SET เบอร์3 = '" + amount_txt.Text + "'";
                    }
                    else if (comboBox1.Text == "เบอร์4")
                    {
                        sql = "UPDATE egg_stock SET เบอร์4 = '" + amount_txt.Text + "'";
                    }
                    else if (comboBox1.Text == "คละไซต์")
                    {
                        sql = "UPDATE egg_stock SET คละไซต์ = '" + amount_txt.Text + "'";
                    }
                    MySqlCommand cmds = new MySqlCommand(sql, connection);
                    connection.Open();
                    cmds.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception EX)
                {
                    MessageBox.Show(EX.Message);
                }
                MessageBox.Show("แก้ไขข้อมูลสำเร็จ", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                stock_egg();
                resettext();
            }     
        }

        private void resettext()
        {
            amount_txt.ResetText();
            comboBox1.ResetText();
        }

        private void clear_btn_Click(object sender, EventArgs e)
        {
            resettext();
        }
    }
}
