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
    public partial class feedchicken_statistics : Form
    {
        private MySqlConnection databaseConnection()
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=farm_dtb;";
            MySqlConnection connection = new MySqlConnection(connectionString);
            return connection;
        }

        public feedchicken_statistics()
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

        private void feedchicken_statistics_Load(object sender, EventArgs e)
        {
            customer_code();
        }

        private void customer_code() 
        {
            try
            {
                int n=0;
                MySqlConnection connection = databaseConnection();
                connection.Open();
                MySqlCommand cmd;
                cmd = connection.CreateCommand();
                cmd.CommandText = "select ชื่อคอกเลี้ยง from ข้อมูลคอกเลี้ยง";
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    comboBox1.Items.Add(reader.GetString(0)); //แอดชื่อคอก
                    chart1.Series.Add(reader.GetString(0)); //***เพิ่มชาร์ต
                    chart1.Series[n].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                    chart1.Series[n].BorderWidth = 2;
                    n++;
                }
                connection.Close();
            }
            catch (Exception EX)
            {
                MessageBox.Show(EX.Message);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (var series in chart1.Series)
            {                                           //*****เคลียร์ชาร์ต
                series.Points.Clear();
            }
            label6.Text = this.dateTimePicker1.Value.ToString("dd-MM-yyyy");
            label7.Text = this.dateTimePicker2.Value.ToString("dd-MM-yyyy");

            try
            {
                string fromDateStringISO = Convert.ToDateTime(dateTimePicker1.Value).ToString("dd-MM-yyyy");
                string toDateStringISO = Convert.ToDateTime(dateTimePicker2.Value).ToString("dd-MM-yyyy");
                
                MySqlConnection connection = databaseConnection();
                connection.Open();
                MySqlCommand cmd, commandd;
                cmd = connection.CreateCommand();
                commandd = connection.CreateCommand();
                cmd.CommandText = "select วันที่ให้อาหาร,ชื่อคอกเลี้ยง,จำนวนอาหารที่ให้ from ให้อาหารไก่ where วันที่ให้อาหาร BETWEEN '" + fromDateStringISO + "' and '" + toDateStringISO + "'";
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read() == false)
                {
                    MessageBox.Show("ไม่มีข้อมูลระหว่างวันที่ที่เลือก", "ตรวจพบข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    while (reader.Read())
                    {
                        
                        if (comboBox1.Text == reader.GetString(1))
                        {
                            this.chart1.Series[reader.GetString(1)].Points.AddXY(reader.GetString(0), reader.GetString(2));
                            commandd.CommandText = "select วันที่ให้อาหาร,ชื่อคอกเลี้ยง,จำนวนอาหารที่ให้ from ให้อาหารไก่ where วันที่ให้อาหาร BETWEEN '" + fromDateStringISO + "' and '" + toDateStringISO + "' and ชื่อคอกเลี้ยง = '" + reader.GetString(1) + "'";
                        }
                        else if (comboBox1.Text == "โดยรวม")
                        {
                            this.chart1.Series[reader.GetString(1)].Points.AddXY(reader.GetString(0), reader.GetString(2));
                            commandd.CommandText = "select วันที่ให้อาหาร,ชื่อคอกเลี้ยง,จำนวนอาหารที่ให้ from ให้อาหารไก่ where วันที่ให้อาหาร BETWEEN '" + fromDateStringISO + "' and '" + toDateStringISO + "'";
                        } 
                    }
                    reader.Close();
                    MySqlDataAdapter adapter = new MySqlDataAdapter(commandd);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    dataGridView1.DataSource = ds.Tables[0].DefaultView;
                }                
                connection.Close();

            }
            catch (Exception EX)
            {
                MessageBox.Show(EX.Message);
            }
        }
    }
}
