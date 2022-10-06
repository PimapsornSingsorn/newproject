using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace newproject
{
    public partial class statistics : Form
    {
        public statistics()
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
            Mainmenu mainmenu = new Mainmenu();
            mainmenu.Show();
        }

        private void pictureegg_Click(object sender, EventArgs e)
        {
            this.Hide();
            eggcollect_statistics eggcollect_Statistics = new eggcollect_statistics();
            eggcollect_Statistics.ShowDialog();
        }

        private void picturefeed_Click(object sender, EventArgs e)
        {
            this.Hide();
            feedchicken_statistics feedchicken_Statistics = new feedchicken_statistics();
            feedchicken_Statistics.ShowDialog();
        }

        private void picturesell_Click(object sender, EventArgs e)
        {
            this.Hide();
            order_satatistics order_Satatistics = new order_satatistics();
            order_Satatistics.ShowDialog();
        }
    }
}
