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
    public partial class eggcollect_statistics : Form
    {
        private MySqlConnection databaseConnection()
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=farm_dtb;";
            MySqlConnection connection = new MySqlConnection(connectionString);
            return connection;
        }

        public eggcollect_statistics()
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

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            this.Hide();
            statistics statistics = new statistics();
            statistics.ShowDialog();
        }
        

        private void button1_Click(object sender, EventArgs e) //แสดง
        {
            foreach (var series in chart1.Series)
            {
                series.Points.Clear();
            }

            try
            {
                label6.Text = this.dateTimePicker1.Value.ToString("dd-MM-yyyy");
                label7.Text = this.dateTimePicker2.Value.ToString("dd-MM-yyyy");
                string fromDateStringISO = Convert.ToDateTime(dateTimePicker1.Value).ToString("dd-MM-yyyy");
                string toDateStringISO = Convert.ToDateTime(dateTimePicker2.Value).ToString("dd-MM-yyyy");

                MySqlConnection connection = databaseConnection();
                connection.Open();
                MySqlCommand cmd;
                cmd = connection.CreateCommand();
                

                if (comboBox1.Text == "จำนวนไข่ทั้งหมด")
                {
                
                    cmd.CommandText = "select วันที่เก็บ,จำนวนไข่ที่เก็บได้ทั้งหมด from เก็บไข่ where วันที่เก็บ BETWEEN '" + fromDateStringISO + "' and '" + toDateStringISO + "'";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read() == false)
                    {
                        MessageBox.Show("ไม่มีข้อมูลระหว่างวันที่ที่เลือก", "ตรวจพบข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        while (reader.Read())
                        {
                            this.chart1.Series["จำนวนไข่ทั้งหมด"].Points.AddXY(reader.GetString(0), reader.GetString(1)); 
                        }
                        dataGridView1.DataSource = ds.Tables[0].DefaultView;
                    }
                    
                    connection.Close();
                }
                else if (comboBox1.Text == "เบอร์0")
                {
                
                    cmd.CommandText = "select วันที่เก็บ,เบอร์0 from เก็บไข่ where วันที่เก็บ BETWEEN '" + fromDateStringISO + "' and '" + toDateStringISO + "'"; ;
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);

                    MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read() == false)
                    {
                        MessageBox.Show("ไม่มีข้อมูลระหว่างวันที่ที่เลือก", "ตรวจพบข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        while (reader.Read())
                        {
                            this.chart1.Series["เบอร์0"].Points.AddXY(reader.GetString(0), reader.GetString(1));
                        }

                        dataGridView1.DataSource = ds.Tables[0].DefaultView;
                    }

                    connection.Close();
                }
                else if (comboBox1.Text == "เบอร์1")
                {
                
                    cmd.CommandText = "select วันที่เก็บ,เบอร์1 from เก็บไข่ where วันที่เก็บ BETWEEN '" + fromDateStringISO + "' and '" + toDateStringISO + "'"; ;
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read() == false)
                    {
                        MessageBox.Show("ไม่มีข้อมูลระหว่างวันที่ที่เลือก", "ตรวจพบข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        while (reader.Read())
                        {
                            this.chart1.Series["เบอร์1"].Points.AddXY(reader.GetString(0), reader.GetString(1));
                        }

                        dataGridView1.DataSource = ds.Tables[0].DefaultView;
                    }
                    
                    connection.Close();
                }
                else if (comboBox1.Text == "เบอร์2")
                {
                
                    cmd.CommandText = "select วันที่เก็บ,เบอร์2 from เก็บไข่ where วันที่เก็บ BETWEEN '" + fromDateStringISO + "' and '" + toDateStringISO + "'"; ;
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read() == false)
                    {
                        MessageBox.Show("ไม่มีข้อมูลระหว่างวันที่ที่เลือก", "ตรวจพบข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        while (reader.Read())
                        {
                            this.chart1.Series["เบอร์2"].Points.AddXY(reader.GetString(0), reader.GetString(1));
                        }

                        dataGridView1.DataSource = ds.Tables[0].DefaultView;
                    }
                    
                    connection.Close();
                }
                else if (comboBox1.Text == "เบอร์3")
                {
                
                    cmd.CommandText = "select วันที่เก็บ,เบอร์3 from เก็บไข่ where วันที่เก็บ BETWEEN '" + fromDateStringISO + "' and '" + toDateStringISO + "'"; ;
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read() == false)
                    {
                        MessageBox.Show("ไม่มีข้อมูลระหว่างวันที่ที่เลือก", "ตรวจพบข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        while (reader.Read())
                        {
                            this.chart1.Series["เบอร์3"].Points.AddXY(reader.GetString(0), reader.GetString(1));
                        }

                        dataGridView1.DataSource = ds.Tables[0].DefaultView;
                    }
                    
                    connection.Close();
                }
                else if (comboBox1.Text == "เบอร์4")
                {

                
                    cmd.CommandText = "select วันที่เก็บ,เบอร์4 from เก็บไข่ where วันที่เก็บ BETWEEN '" + fromDateStringISO + "' and '" + toDateStringISO + "'"; ;
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read() == false)
                    {
                        MessageBox.Show("ไม่มีข้อมูลระหว่างวันที่ที่เลือก", "ตรวจพบข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        while (reader.Read())
                        {
                            this.chart1.Series["เบอร์4"].Points.AddXY(reader.GetString(0), reader.GetString(1));
                        }

                        dataGridView1.DataSource = ds.Tables[0].DefaultView;
                    }
                   
                    connection.Close();
                }
                else if (comboBox1.Text == "คละไซต์")
                {
                
                    cmd.CommandText = "select วันที่เก็บ,คละไซต์ from เก็บไข่ where วันที่เก็บ BETWEEN '" + fromDateStringISO + "' and '" + toDateStringISO + "'"; ;
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read() == false)
                    {
                        MessageBox.Show("ไม่มีข้อมูลระหว่างวันที่ที่เลือก", "ตรวจพบข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        while (reader.Read())
                        {
                            this.chart1.Series["คละไซต์"].Points.AddXY(reader.GetString(0), reader.GetString(1));
                        }
                        dataGridView1.DataSource = ds.Tables[0].DefaultView;
                    }
                    
                    connection.Close();
                }
                else
                {
                    MessageBox.Show("โปรดเลือกขนาดที่ต้องการค้นหา !", "ตรวจพบข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ตรวจพบข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            
        }

        private void eggcollect_statistics_Load(object sender, EventArgs e)
        {

        } 
    }
}
