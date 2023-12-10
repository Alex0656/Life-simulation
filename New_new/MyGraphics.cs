using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace New_new
{
    public class MyGraphics
    {
        private Graphics _graphics;
        private PictureBox _pictureBox1;
        private int _resolution;
        
        
        public MyGraphics(int resolution, PictureBox pictureBox1, Graphics graphics)
        {
            _resolution = resolution;
            _pictureBox1 = pictureBox1;
            _graphics = graphics;
        }

        public void DrawMap(List<Animal> animal_list, List<Plant> plant_list)
        {
            _graphics.Clear(Color.Black);
            DrawObj_p(plant_list);
            DrawObj_a(animal_list);
            _pictureBox1.Refresh();
        }
        private void DrawObj_a(List<Animal> animal_list)
        {
            foreach (Animal animal in animal_list)
            {
                _graphics.FillRectangle(animal.Color, animal._x * _resolution, animal._y * _resolution, _resolution, _resolution);

            }
        }
        private void DrawObj_p(List<Plant> plant_list)
        {
            foreach (Plant plant in plant_list)
            {
                _graphics.FillRectangle(Brushes.Green, plant._x * _resolution, plant._y * _resolution, _resolution, _resolution);
            }
        }
    }
}
