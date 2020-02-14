using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GravityYouHeartless
{
    public static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        public class Planet
        {
            public int x, y;
            public double mass;
            public string name { get; set; }
            public Planet(int x, int y, double mass, string name)
            {
                this.x = x;
                this.y = y;
                this.mass = mass;
                this.name = name;
            }
        }
    }
}
