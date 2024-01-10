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
    public class Map
    {
        public List<Plant> _createdPlants { get; set; } = new List<Plant>();
        public List<Animal> _createdAnimals { get; set; } = new List<Animal>();
        public List<KilledAnimal> _createdKilledAnimals { get; set; } = new List<KilledAnimal>();
        public List<Building> _createdBuilding { get; set; } = new List<Building>();

        const int min_range = 0;
        const int max_range = 100;
        const int mid_range = 60;

        private bool[,] field;

        private int _density;
        private int _resolution;
        public int _row { get; } = 50;
        public int _cols { get; } = 50;

        public Map(int resolution, int density)
        {
            _resolution = resolution;
            _density = density;
        }

        private void Create_map()
        {
          field = new bool[_cols + 1, _row + 1];
          for (int x = 0; x < _cols; x++)
          {
              for (int y = 0; y < _row; y++)
              {
                  int map_density = GameLogic.GenerateNumber(0, _density);
                  field[x, y] = map_density == 0;
                  if (field[x, y] == true)
                  {
                      int random_number = GameLogic.GenerateNumber(0, 1);
                      bool flag;
                      if (random_number == 0)
                      {
                          flag = false;
                      }
                      else
                      {
                          flag = true;
                      }
                      int number_from_0_to_100 = GameLogic.GenerateNumber(0, 100);
                      int number_from_0_to_10 = GameLogic.GenerateNumber(0, 10);
                      if ((number_from_0_to_100 >= min_range) && (number_from_0_to_100 <= mid_range))
                      {
                          if (number_from_0_to_10 == 1 || number_from_0_to_10 == 0)
                          {
                              var plant = new PlantInedibleWithoutFruits(x, y, this);
                              _createdPlants.Add(plant);
                          }
                          else if (number_from_0_to_10 == 2)
                          {
                              var plant = new PlantInedibleWichFruitsNoPoison(x, y, this);
                              _createdPlants.Add(plant);
                          }
                          else if (number_from_0_to_10 == 3)
                          {
                              var plant = new PlantInedibleWichFruitsPoison(x, y, this);
                              _createdPlants.Add(plant);
                          }

                          else if (number_from_0_to_10 == 4)
                          {
                              var plant = new PlantEdibleWichFruitsNoPoison(x, y, this);
                              _createdPlants.Add(plant);
                          }
                          else if (number_from_0_to_10 == 5)
                          {
                              var plant = new PlantEdibleWichFruitsPoison(x, y, this);
                              _createdPlants.Add(plant);
                          }
                          else if (number_from_0_to_10 == 6 || number_from_0_to_10 == 7)
                          {
                              var plant = new PlantEdibleWithoutFruitsNoPoison(x, y, this);
                              _createdPlants.Add(plant);
                          }
                          else if (number_from_0_to_10 == 8)
                          {
                              var plant = new PlantEdibleWithoutFruitsPoison(x, y, this);
                              _createdPlants.Add(plant);
                          }
                      }
                      else if ((number_from_0_to_100 >= mid_range + 1) && (number_from_0_to_100 <= max_range))
                      {
                          if (number_from_0_to_10 == 0)
                          {
                              var wolf = new Wolf(x, y, flag, this, false);
                              _createdAnimals.Add(wolf);
                          }
                          else if (number_from_0_to_10 == 1)
                          {
                              var lion = new Lion(x, y, flag, this, false);
                              _createdAnimals.Add(lion);
                          }
                          else if (number_from_0_to_10 == 2)
                          {
                              var lynx = new Lynx(x, y, flag, this, false);
                              _createdAnimals.Add(lynx);
                          }

                          else if (number_from_0_to_10 == 3)
                          {
                              var bear = new Bear(x, y, flag, this, false);
                              _createdAnimals.Add(bear);
                          }
                          else if (number_from_0_to_10 == 4)
                          {
                              var hog = new Hog(x, y, flag, this, false);
                              _createdAnimals.Add(hog);
                          }
                          else if (number_from_0_to_10 == 5)
                          {
                              var fox = new Fox(x, y, flag, this, false);
                              _createdAnimals.Add(fox);
                          }

                          else if (number_from_0_to_10 == 6)
                          {
                              var zebra = new Zebra(x, y, flag, this, false);
                              _createdAnimals.Add(zebra);
                          }
                          else if (number_from_0_to_10 == 7)
                          {
                              var deer = new Deer(x, y, flag, this, false);
                              _createdAnimals.Add(deer);
                          }
                          else if (number_from_0_to_10 == 8)
                          {
                              var bunny = new Bunny(x, y, flag, this, false);
                              _createdAnimals.Add(bunny);
                          }
                          else if (number_from_0_to_10 == 9 || number_from_0_to_10 == 10)
                          {
                              var human = new Human(x, y, flag, this, false);
                              _createdAnimals.Add(human);
                          }
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
