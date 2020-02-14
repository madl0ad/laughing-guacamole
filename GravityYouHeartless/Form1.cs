using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using static GravityYouHeartless.Program;

namespace GravityYouHeartless
{
    public partial class Form1 : Form
    {
        List<Planet> t = new List<Planet>();
        Bitmap b;
        Dictionary<int, Dictionary<int, double>> dict;
        private static Object locker = new Object();
        const double G = 6.67191 * 1e-11;
        double max, min, speed;
        int w, h;
        double mx = 0, m = 0, my = 0;
        public Form1()
        {
            InitializeComponent();

        }

        public bool AddPlanet(Planet p)
        {
            if (t.FindIndex(x => x.name == p.name) > -1)
            {
                MessageBox.Show("Имя должно быть уникальным!");
                return false;
            }
            t.Add(p);
            listBox1.Items.Add(p.name);
            return true;
        }

        public void RemovePlanet(Planet p)
        {
            t.RemoveAt(t.FindIndex(x => x.name == p.name));
            listBox1.Items.Remove(p.name);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2();
            f.f = this;
            f.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = this.listBox1.IndexFromPoint(e.Location);
            if (index != System.Windows.Forms.ListBox.NoMatches)
            {
                Form3 f = new Form3();
                f.f = this;
                f.p = t.Find(x => x.name == listBox1.Items[index].ToString());
                f.Show();
            }
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        double map(double x, double a1, double a2, double b1, double b2)
        {
            if (x == 0) return 0;
            if (a1 == 0) a1 = 1e-5;
            if (a2 == 0) a2 = 1e-5;
            if (x > a2) return b2;
            if (x < a1) return b1;
            return b1 + b2 * (Math.Log10(x) - Math.Log10(a1)) / (Math.Log10(a2) - Math.Log10(a1));
        }

        private void doStuff(int a)
        {
            lock (locker)
                dict.Add(a, new Dictionary<int, double>());
            for (int i = 0; i < h; i++)
            {
                double Fx = 0, Fy = 0;
                double xv = a - mx;
                double yv = i - my;
                double Fv = speed * speed * Math.Sqrt(xv * xv + yv * yv);
                foreach (var p in t)
                {
                    double xx = (p as Planet).x - a;
                    double yy = (p as Planet).y - i;
                    if ((xx == 0) && (yy == 0)) yy = 0.5;
                    double F = G * (p as Planet).mass / (xx * xx + yy * yy);
                    double aa = Math.Atan2(yy, xx);
                    Fx += F * Math.Cos(aa);
                    Fy += F * Math.Sin(aa);
                }
                double av = Math.Atan2(yv, xv);
                Fx += Fv * Math.Cos(av);
                Fy += Fv * Math.Sin(av);
                double res = Math.Sqrt(Fx * Fx + Fy * Fy);
                lock (locker)
                    dict[a].Add(i, res);
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        void SetPixels(KeyValuePair<int, Dictionary<int, double>> a)
        {
            foreach (var t in a.Value)
            {
                int tt = (int)map(t.Value, min, max, 0, 510);
                lock (locker)
                    if (tt > 255)
                        b.SetPixel(a.Key, t.Key, Color.FromArgb(tt - 255, 510 - tt, 0));
                    else b.SetPixel(a.Key, t.Key, Color.FromArgb(0, tt, 255 - tt));
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        void paintMaxMin(double min, double max)
        {
            for (int j = 2 * h / 3; j > h / 3; j--)
            {
                int tt = (int)map(j, h / 3, 2 * h / 3, 0, 510);
                for (int i = w + 5; i < w + 5 + 10; i++)
                    if (tt > 255)
                        b.SetPixel(i, j, Color.FromArgb(tt - 255, 510 - tt, 0));
                    else b.SetPixel(i, j, Color.FromArgb(0, tt, 255 - tt));
            }
            Bitmap newBitmap;
            using (var bitmap = new Bitmap(b))//load the image file
            {
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    using (Font arialFont = new Font("Arial", 10))
                    {
                        graphics.DrawString(min.ToString("0.0e0"), arialFont, Brushes.Black, new Point(w + 5 + 10 + 5, 2 * h / 3 - 5));
                        graphics.DrawString(max.ToString("0.0e0"), arialFont, Brushes.Black, new Point(w + 5 + 10 + 5, h / 3 - 5));
                    }
                }
                newBitmap = new Bitmap(bitmap);
            }
            b = newBitmap;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double prescaler_def = Double.Parse(textBox4.Text);
            if (prescaler_def >= 1 || prescaler_def <= 0)
            {
                MessageBox.Show("Предделитель должен быть меньше единицы и больше нуля");
                return;
            }
            h = Int32.Parse(textBox2.Text);
            w = Int32.Parse(textBox1.Text);
            b = new Bitmap(w + 70, h);
            speed = Double.Parse(textBox3.Text);
            speed = Double.Parse(textBox3.Text);
            min = Double.MaxValue;
            max = Double.MinValue;
            dict = new Dictionary<int, Dictionary<int, double>>();
            if (t.Count > 0)
            {
                foreach (var y in t)
                {
                    mx += y.x * y.mass;
                    my += y.y * y.mass;
                    m += y.mass;
                }
                my /= m;
                mx /= m;
            }
            else
            {
                my = h / 2;
                mx = w / 2;
            }
            Parallel.For(0, w, doStuff);
            foreach (var a in dict)
                foreach (var t in a.Value)
                {
                    if (t.Value > max)
                        max = t.Value;
                    if (t.Value < min)
                        min = t.Value;
                }
            double prescaler;
            if (min != 0 && max != 0)
                prescaler = (Math.Log10(max) - Math.Log10(min));
            else prescaler = 0;
            max = max / Math.Pow(10, prescaler_def * prescaler);
            Parallel.ForEach(dict, SetPixels);
            Form4 f = new Form4();
            f.Size = new Size(b.Width + 20, b.Height + 80);
            f.pictureBox1.SetBounds(5, 5, b.Width, b.Height);
            f.button1.SetBounds(5, b.Height + 5, 70, 20);
            paintMaxMin(max, min);
            f.pictureBox1.Image = b;
            f.Show();
            MessageBox.Show("Done!");
        }
    }
}
