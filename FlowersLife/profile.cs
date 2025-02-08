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
    public partial class profile : Form
    {
        public profile()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            bonuses bonuses = new bonuses();
            this.Close();
            bonuses.Show();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            orders orders = new orders();
            this.Close();
            orders.Show();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            index ar = new index();
            this.Close();
            ar.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Incoming incoming = new Incoming();
            this.Close();
            incoming.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            purchases purchases = new purchases();
            this.Close();
            purchases.Show();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            panel2.Visible = !panel2.Visible;
        }

        private void profile_Load(object sender, EventArgs e)
        {
            panel1.Location = new Point(
                (ClientSize.Width - panel1.Width) / 2,
                (ClientSize.Height - panel1.Height) / 2
            );
        }
    }
}
    