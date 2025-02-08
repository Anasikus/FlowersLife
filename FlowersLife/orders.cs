using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlowersLife
{
    public partial class orders : Form
    {
        public orders()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            orders orders = new orders();
            purchases purchases = new purchases();
            orders.Close();
            purchases.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            orders orders = new orders();
            profile profile = new profile();
            orders.Hide();
            profile.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void orders_Load(object sender, EventArgs e)
        {
            panel1.Location = new Point(
                (ClientSize.Width - panel1.Width) / 2,
                (ClientSize.Height - panel1.Height) / 2
            );
        }
    }
}
