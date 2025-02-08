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
    public partial class Incoming : Form
    {
        public Incoming()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            Incoming incoming = new Incoming();
            profile profile = new profile();
            incoming.Close();
            profile.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            purchases purchases = new purchases();
            this.Close();
            purchases.Show();

        }

        private void button7_Click(object sender, EventArgs e)
        {
            purchases purchases = new purchases();
            this.Close();
            purchases.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            purchases purchases = new purchases();
            this.Close();
            purchases.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            compilations form7 = new compilations();
            form7.Show();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            profile profile = new profile();
            profile.Show();
            this.Close();
        }

    }
}
