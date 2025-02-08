using System;
using System.Windows.Forms;
using System.Drawing;

namespace FlowersLife
{
    public partial class index : Form
    {
        public index()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            registration form3 = new registration();
            form3.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            autorization form2 = new autorization();
            form2.Show();
            this.Hide();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            index index = new index();
            panel1.Location = new Point(
                (index.Width - panel1.Width) / 2,
                (index.Height - panel1.Height) / 2
            );
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
