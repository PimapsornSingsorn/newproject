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
    public partial class receipt : Form
    {
        private MySqlConnection databaseConnection()
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=farm_dtb;";
            MySqlConnection connection = new MySqlConnection(connectionString);
            return connection;
        }

        public receipt()
        {
            InitializeComponent();
        }

        private void order_list()
        {
            try
            {
                int n = 0;
                MySqlConnection connection = databaseConnection();
                connection.Open();
                MySqlCommand cmd;
                cmd = connection.CreateCommand();
                cmd.CommandText = "select * FROM ตะกร้า ";
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    listBox1.Items.Add("เบอร์ " + dt.Rows[n][1].ToString() + " \tจำนวน " + dt.Rows[n][2].ToString() + "\tแผง " + "\t ราคา " + dt.Rows[n][3].ToString() + "\tบาท ");
                    n++;
                }

                reader.Close();
                connection.Close();
            }
            catch (Exception EX)
            {
                MessageBox.Show(EX.Message);
            }
        }
        

        private void receipt_Load(object sender, EventArgs e)
        {
            
        }
    }
}
