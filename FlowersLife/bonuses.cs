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
    public partial class bonuses : Form
    {
        public bonuses()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            profile profile = new profile();
            bonuses bonuses = new bonuses();
            bonuses.Close();
            profile.Show();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
