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

        private Image seed_image = Image.FromFile(@"C:\Users\Алексей\source\repos\Life-simulation_02\New_new\img\seed.png");
        private Image sprout_image = Image.FromFile(@"C:\Users\Алексей\source\repos\Life-simulation_02\New_new\img\sprout.png");
        public MyGraphics(int resolution, PictureBox pictureBox1, Graphics graphics)
        {
            _resolution = resolution;
            _pictureBox1 = pictureBox1;
            _graphics = graphics;
        }

        public void DrawMap(List<Animal> animal_list, List<Plant> plant_list, List<KilledAnimal> killed_animal_list, GameLogic game_logic, List<Building> building_list)
        {
            if (game_logic._summer)
            {
                _graphics.Clear(Color.Black);
            }
            else
            {
                _graphics.Clear(Color.LightGray);
            }
            
            DrawObj_p(plant_list);
            DrawObj_k_a(killed_animal_list);
            DrawObj_a(animal_list);
            DrawBuilding(building_list);
            _pictureBox1.Refresh();
        }

        private void DrawObj_a(List<Animal> animal_list)
        {
            foreach (Animal animal in animal_list)
            {
                _graphics.DrawImage(animal.Image, animal._x * _resolution, animal._y * _resolution);
            }
        }

        private void DrawObj_p(List<Plant> plant_list)
        {
            foreach (Plant plant in plant_list)
            {
                if (plant.stage == 1)
                {
                    _graphics.DrawImage(seed_image, plant._x * _resolution, plant._y * _resolution);
                }
                else if(plant.stage == 2)
                {
                    _graphics.DrawImage(sprout_image, plant._x * _resolution, plant._y * _resolution);
                }
                else
                {
                    _graphics.DrawImage(plant.Image, plant._x * _resolution, plant._y * _resolution);
                }
                //_graphics.FillRectangle(plant.Color, plant._x * _resolution, plant._y * _resolution, _resolution, _resolution);
            }
        }

        private void DrawObj_k_a(List<KilledAnimal> killed_animal_list)
        {
            foreach (KilledAnimal k_animal in killed_animal_list)
            {
                _graphics.DrawImage(k_animal.Image, k_animal._x * _resolution, k_animal._y * _resolution);
            }
        }
        private void DrawBuilding(List<Building> building_list)
        {
            foreach (Building building in building_list)
            {
                _graphics.DrawImage(building.Image, building._x * _resolution, building._y * _resolution);
            }
        }
    }
}
