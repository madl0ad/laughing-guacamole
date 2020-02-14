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
    public partial class Form3 : Form
    {
        public Form1 f;
        public Program.Planet p;

        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            f.RemovePlanet(p);
            if (!f.AddPlanet(new Program.Planet(Int32.Parse(textBox3.Text), Int32.Parse(textBox4.Text), Double.Parse(textBox2.Text), textBox1.Text)))
                f.AddPlanet(p);
            Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            f.RemovePlanet(new Program.Planet(Int32.Parse(textBox3.Text), Int32.Parse(textBox4.Text), Double.Parse(textBox2.Text), textBox1.Text));
            Dispose();
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void Form3_Shown(object sender, EventArgs e)
        {
            textBox1.Text = p.name;
            textBox2.Text = p.mass.ToString("e5");
            textBox3.Text = p.x.ToString();
            textBox4.Text = p.y.ToString();
        }
    }
}
