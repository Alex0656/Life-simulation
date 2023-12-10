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
    public class Map
    {
        public List<Plant> _createdPlants { get; set; } = new List<Plant>();
        public List<Animal> _createdAnimals { get; set; } = new List<Animal>();

        private bool[,] field;

        private int _density;
        private int _resolution;
        public int _row { get; } = 100;
        public int _cols { get; } = 100;

        public Map(int resolution, int density)
        {
            _resolution = resolution;
            _density = density;
        }

        private void Create_map()
        {
            field = new bool[_cols + 1, _row + 1];
            Random random = new Random();
            //_my_graphics = new MyGraphics(_resolution, _pictureBox1);
            //_my_graphics.CreatPictureBox();
             



            //var plant = new Plant(30, 30);
            //_createdPlants.Add(plant);

            // var herbivores = new Herbivores(1, 1, true);
            // _createdHerbivores.Add(herbivores);
            //var herbivores1 = new Herbivores(30, 30, false);
            // _createdHerbivores.Add(herbivores1);

            //var predator = new Predator(38, 38);
            // _createdPredator.Add(predator);

            // var omnivorous = new Omnivorous(30, 30, true);
            //  _createdOmnivorous.Add(omnivorous);

            // var omnivorous1 = new Omnivorous(1, 1, false);
            //  _createdOmnivorous.Add(omnivorous1);

            for (int x = 0; x < _cols; x++)
            {
                for (int y = 0; y < _row; y++)
                {
                    Random rnd = new Random();
                    int value = rnd.Next(0, 2);
                    bool temp_0;
                    if (value == 0)
                    {
                        temp_0 = false;
                    }
                    else {
                        temp_0 = true;
                    }
                    
                    field[x, y] = random.Next(_density) == 0;
                    if (field[x, y] == true)
                    {
                        int temp = random.Next(0, 101);
                        if ((temp >= 0) && (temp <= 40))
                        {
                            var plant = new Plant(x, y, this);
                            _createdPlants.Add(plant);
                        }
                        else if ((temp >= 41) && (temp <= 60)) 
                        {
                            var herbivores = new Herbivores(x, y, temp_0, this);
                            _createdAnimals.Add(herbivores);
                        }
                        else if ((temp >= 61) && (temp <= 80))
                        {
                            var predator = new Predator(x, y, temp_0, this);
                            _createdAnimals.Add(predator);
                        }
                        else if ((temp >= 81) && (temp <= 100))
                        {
                            var omnivorous = new Omnivorous(x, y, temp_0, this);
                            _createdAnimals.Add(omnivorous);
                        }
                    }

                }
            }
           
        }
        public void Create_new_map()
        {
            Create_map();
        }

    }
}
