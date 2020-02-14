using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GravityYouHeartless
{
    public partial class Form2 : Form
    {
        public Form1 f;

        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            f.AddPlanet(new Program.Planet(Int32.Parse(textBox3.Text), Int32.Parse(textBox4.Text), Double.Parse(textBox2.Text), textBox1.Text));
            Dispose();
        }
    }
}
