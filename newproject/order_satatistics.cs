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
    public partial class order_satatistics : Form
    {
        private MySqlConnection databaseConnection()
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=farm_dtb;";
            MySqlConnection connection = new MySqlConnection(connectionString);
            return connection;
        }

        public order_satatistics()
        {
            InitializeComponent();
        }

        private void pictureclose_Click(object sender, EventArgs e)
        {
            MessageBox.Show("ออกจากโปรแกรม");
            this.Close();
            Application.Exit();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            this.Hide();
            statistics statistics = new statistics();
            statistics.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (var series in chart2.Series)
            {
                series.Points.Clear();
            }

            label6.Text = this.dateTimePicker1.Value.ToString("dd-MM-yyyy");
            label7.Text = this.dateTimePicker2.Value.ToString("dd-MM-yyyy");
            string fromDateStringISO = Convert.ToDateTime(dateTimePicker1.Value).ToString("dd-MM-yyyy");
            string toDateStringISO = Convert.ToDateTime(dateTimePicker2.Value).ToString("dd-MM-yyyy");

            try
            {
                MySqlConnection connection = databaseConnection();
                connection.Open();
                MySqlCommand cmd;
                cmd = connection.CreateCommand();


                if (comboBox1.Text == "จำนวนไข่ทั้งหมด")
                {
                    cmd.CommandText = "select ยอดขายวันที่,ไซต์,จำนวน,ราคา from ยอดขาย where ยอดขายวันที่ BETWEEN '" + fromDateStringISO + "' and '" + toDateStringISO + "'";
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
                            this.chart2.Series["จำนวนไข่ทั้งหมด"].Points.AddXY(reader.GetString(0), reader.GetString(2));
                        }
                        dataGridView2.DataSource = ds.Tables[0].DefaultView;
                    }

                    connection.Close();
                }
                else if (comboBox1.Text == "เบอร์0")
                {

                    cmd.CommandText = "select ยอดขายวันที่,ไซต์,จำนวน,ราคา from ยอดขาย where ยอดขายวันที่ BETWEEN '" + fromDateStringISO + "' and '" + toDateStringISO + "' and ไซต์ = 0"; ;
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
                            this.chart2.Series["เบอร์0"].Points.AddXY(reader.GetString(0), reader.GetString(2));
                        }

                        dataGridView2.DataSource = ds.Tables[0].DefaultView;
                    }

                    connection.Close();
                }
                else if (comboBox1.Text == "เบอร์1")
                {

                    cmd.CommandText = "select ยอดขายวันที่,ไซต์,จำนวน,ราคา from ยอดขาย where ยอดขายวันที่ BETWEEN '" + fromDateStringISO + "' and '" + toDateStringISO + "' and ไซต์ = 1"; ;
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
                            this.chart2.Series["เบอร์1"].Points.AddXY(reader.GetString(0), reader.GetString(2));
                        }

                        dataGridView2.DataSource = ds.Tables[0].DefaultView;
                    }

                    connection.Close();
                }
                else if (comboBox1.Text == "เบอร์2")
                {

                    cmd.CommandText = "select ยอดขายวันที่,ไซต์,จำนวน,ราคา from ยอดขาย where ยอดขายวันที่ BETWEEN '" + fromDateStringISO + "' and '" + toDateStringISO + "' and ไซต์ = 2"; ;
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
                            this.chart2.Series["เบอร์2"].Points.AddXY(reader.GetString(0), reader.GetString(2));
                        }

                        dataGridView2.DataSource = ds.Tables[0].DefaultView;
                    }

                    connection.Close();
                }
                else if (comboBox1.Text == "เบอร์3")
                {

                    cmd.CommandText = "select ยอดขายวันที่,ไซต์,จำนวน,ราคา from ยอดขาย where ยอดขายวันที่ BETWEEN '" + fromDateStringISO + "' and '" + toDateStringISO + "' and ไซต์ = 3"; ;
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
                            this.chart2.Series["เบอร์3"].Points.AddXY(reader.GetString(0), reader.GetString(2));
                        }

                        dataGridView2.DataSource = ds.Tables[0].DefaultView;
                    }

                    connection.Close();
                }
                else if (comboBox1.Text == "เบอร์4")
                {
                    cmd.CommandText = "select ยอดขายวันที่,ไซต์,จำนวน,ราคา from ยอดขาย where ยอดขายวันที่ BETWEEN '" + fromDateStringISO + "' and '" + toDateStringISO + "' and ไซต์ = 4"; ;
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
                            this.chart2.Series["เบอร์4"].Points.AddXY(reader.GetString(0), reader.GetString(2));
                        }

                        dataGridView2.DataSource = ds.Tables[0].DefaultView;
                    }

                    connection.Close();
                }
                else if (comboBox1.Text == "คละไซต์")
                {
                    cmd.CommandText = "select ยอดขายวันที่,ไซต์,จำนวน,ราคา from ยอดขาย where ยอดขายวันที่ BETWEEN '" + fromDateStringISO + "' and '" + toDateStringISO + "' and ไซต์ = คละไซต์"; ;
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
                            this.chart2.Series["คละไซต์"].Points.AddXY(reader.GetString(0), reader.GetString(2));
                        }
                        dataGridView2.DataSource = ds.Tables[0].DefaultView;
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
            /*MySqlConnection connection = databaseConnection();
            connection.Open();
            MySqlCommand cmd, commandd;
            cmd = connection.CreateCommand();
            commandd = connection.CreateCommand();
            
            cmd.CommandText = "select ยอดขายวันที่,ไซต์,จำนวน,ราคา from ยอดขาย where ยอดขายวันที่ BETWEEN '" + fromDateStringISO + "' and '" + toDateStringISO + "'";
            MySqlDataReader reader = cmd.ExecuteReader();
            label4.Text = "may";
            if (reader.Read() == false)
            {
                MessageBox.Show("ไม่มีข้อมูลระหว่างวันที่ที่เลือก2325", "ตรวจพบข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else
            {
                while (reader.Read())
                {
                    if (comboBox1.Text == reader.GetString(1))
                    {
                        this.chart2.Series[reader.GetString(1)].Points.AddXY(reader.GetString(0), reader.GetString(2));
                        commandd.CommandText = "select ยอดขายวันที่,ไซต์,ราคา from ยอดขาย where ยอดขายวันที่ BETWEEN '" + fromDateStringISO + "' and '" + toDateStringISO + "' and ไซต์ = '" + reader.GetString(1) + "'";
                    }
                    else if (comboBox1.Text == "จำนวนไข่ทั้งหมด")
                    {
                        this.chart2.Series[reader.GetString(1)].Points.AddXY(reader.GetString(0), reader.GetString(2));
                        commandd.CommandText = "select ยอดขายวันที่,ไซต์,ราคา from ยอดขาย where ยอดขายวันที่ BETWEEN '" + fromDateStringISO + "' and '" + toDateStringISO + "'";
                    }*/

            /*if (radioButton1.Checked == true)//ยอดขาย
            {
                if (comboBox1.Text == reader.GetString(1))
                {
                    this.chart2.Series[reader.GetString(1)].Points.AddXY(reader.GetString(0), reader.GetString(2));
                    commandd.CommandText = "select ยอดขายวันที่,ไซต์,ราคา from ยอดขาย where ยอดขายวันที่ BETWEEN '" + fromDateStringISO + "' and '" + toDateStringISO + "' and ไซต์ = '" + reader.GetString(1) + "'";
                }
                else if (comboBox1.Text == "จำนวนไข่ทั้งหมด")
                {
                    this.chart2.Series[reader.GetString(1)].Points.AddXY(reader.GetString(0), reader.GetString(2));
                    commandd.CommandText = "select ยอดขายวันที่,ไซต์,ราคา from ยอดขาย where ยอดขายวันที่ BETWEEN '" + fromDateStringISO + "' and '" + toDateStringISO + "'";
                }
            }
            else if (radioButton2.Checked == true)//ไข่
            {
                if (comboBox1.Text == reader.GetString(1))
                {
                    this.chart2.Series[reader.GetString(1)].Points.AddXY(reader.GetString(0), reader.GetString(2));
                    commandd.CommandText = "select ยอดขายวันที่,ไซต์,จำนวน from ยอดขาย where ยอดขายวันที่ BETWEEN '" + fromDateStringISO + "' and '" + toDateStringISO + "' and ไซต์ = '" + reader.GetString(1) + "'";
                }
                else if (comboBox1.Text == "จำนวนไข่ทั้งหมด")
                {
                    this.chart2.Series[reader.GetString(1)].Points.AddXY(reader.GetString(0), reader.GetString(2));
                    commandd.CommandText = "select ยอดขายวันที่,ไซต์,จำนวน from ยอดขาย where ยอดขายวันที่ BETWEEN '" + fromDateStringISO + "' and '" + toDateStringISO + "'";
                }
            } 
        }
        reader.Close();
        MySqlDataAdapter adapter = new MySqlDataAdapter(commandd);
        DataSet ds = new DataSet();
        adapter.Fill(ds);
        dataGridView2.DataSource = ds.Tables[0].DefaultView;
    }
    connection.Close();*/
        }
    }
}
