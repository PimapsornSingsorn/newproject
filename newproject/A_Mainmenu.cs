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
    public partial class Mainmenu : Form
    {
        informationform information = new informationform();
        eggcollect eggcol = new eggcollect();
        feedchicken feed = new feedchicken();
        order order = new order();
        statistics statistics = new statistics();

        public Mainmenu()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("ออกจากโปรแกรมหรือไม่ ?", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.OK)
            {
                
                this.Close();
                Application.Exit();
            }
        }
        
        private void pictureinformation_Click(object sender, EventArgs e)
        {
            this.Hide();
            information.ShowDialog();
            
        }

        private void Mainmenu_Load(object sender, EventArgs e)
        {

        }

        private void pictureegg_Click(object sender, EventArgs e)
        {
            this.Hide();
            eggcol.ShowDialog();
            
        }

        private void picturefeed_Click(object sender, EventArgs e)
        {
            this.Hide();
            feed.ShowDialog();
            
        }

        private void picturesell_Click(object sender, EventArgs e)
        {
            this.Hide();
            order.ShowDialog();
            
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
            statistics.ShowDialog();
        }
    }
}
