using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LifeSimulation
{
    public partial class Form1 : Form
    {
        public Map new_map;
        private Graphics graphics;
        public MyGraphics _my_graphics;
        public GameLogic my_logic;
        public Form1()
        {
            InitializeComponent();
            pictureBox1.Width = 4000;
            pictureBox1.Height = 4000;
            pictureBox1.Image = new Bitmap(4000, 4000); //_pictureBox1.Width, _pictureBox1.Height
            graphics = Graphics.FromImage(pictureBox1.Image);

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            my_logic.Update_map();
            _my_graphics.DrawMap(new_map._createdAnimals, new_map._createdPlants, new_map._createdKilledAnimals, my_logic, new_map._createdBuilding);
        }

        private void bStart_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            {
                return;
            }

            nudResolution.Enabled = false;
            nubDensity.Enabled = false;

            var resolution = (int)nudResolution.Value;
            var density = (int)nubDensity.Value;
            new_map = new Map(resolution, density);
            new_map.Create_new_map();

            _my_graphics = new MyGraphics(resolution, pictureBox1, graphics);
            my_logic = new GameLogic(new_map);
            timer1.Start();
        }

        private void bStop_Click(object sender, EventArgs e)
        {
            if (!timer1.Enabled)
            {
                return;

            }
            timer1.Stop();
            nudResolution.Enabled = true;
            nubDensity.Enabled = true;

        }



    }

}
