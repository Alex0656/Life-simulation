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
        public List<KilledAnimal> _createdKilledAnimals { get; set; } = new List<KilledAnimal>();
        public List<Building> _createdBuilding { get; set; } = new List<Building>();

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
                  int value_2 = GameLogic.Random(0, _density);
                  field[x, y] = value_2 == 0;
                  if (field[x, y] == true)
                  {
                      int value = GameLogic.Random(0, 1);
                      bool temp_0;
                      if (value == 0)
                      {
                          temp_0 = false;
                      }
                      else
                      {
                          temp_0 = true;
                      }
                      int temp = GameLogic.Random(0, 100);
                      int temp_2 = GameLogic.Random(0, 10);
                      if ((temp >= 0) && (temp <= 60))
                      {
                          if (temp_2 == 1 || temp_2 == 0)
                          {
                              var plant = new PlantInedibleWithoutFruits(x, y, this);
                              _createdPlants.Add(plant);
                          }
                          else if (temp_2 == 2)
                          {
                              var plant = new PlantInedibleWichFruitsNoPoison(x, y, this);
                              _createdPlants.Add(plant);
                          }
                          else if (temp_2 == 3)
                          {
                              var plant = new PlantInedibleWichFruitsPoison(x, y, this);
                              _createdPlants.Add(plant);
                          }

                          else if (temp_2 == 4)
                          {
                              var plant = new PlantEdibleWichFruitsNoPoison(x, y, this);
                              _createdPlants.Add(plant);
                          }
                          else if (temp_2 == 5)
                          {
                              var plant = new PlantEdibleWichFruitsPoison(x, y, this);
                              _createdPlants.Add(plant);
                          }
                          else if (temp_2 == 6 || temp_2 == 7)
                          {
                              var plant = new PlantEdibleWithoutFruitsNoPoison(x, y, this);
                              _createdPlants.Add(plant);
                          }
                          else if (temp_2 == 8)
                          {
                              var plant = new PlantEdibleWithoutFruitsPoison(x, y, this);
                              _createdPlants.Add(plant);
                          }
                      }
                      else if ((temp >= 61) && (temp <= 100))
                      {
                          if (temp_2 == 0)
                          {
                              var wolf = new Wolf(x, y, temp_0, this, false);
                              _createdAnimals.Add(wolf);
                          }
                          else if (temp_2 == 1)
                          {
                              var lion = new Lion(x, y, temp_0, this, false);
                              _createdAnimals.Add(lion);
                          }
                          else if (temp_2 == 2)
                          {
                              var lynx = new Lynx(x, y, temp_0, this, false);
                              _createdAnimals.Add(lynx);
                          }

                          else if (temp_2 == 3)
                          {
                              var bear = new Bear(x, y, temp_0, this, false);
                              _createdAnimals.Add(bear);
                          }
                          else if (temp_2 == 4)
                          {
                              var hog = new Hog(x, y, temp_0, this, false);
                              _createdAnimals.Add(hog);
                          }
                          else if (temp_2 == 5)
                          {
                              var fox = new Fox(x, y, temp_0, this, false);
                              _createdAnimals.Add(fox);
                          }

                          else if (temp_2 == 6)
                          {
                              var zebra = new Zebra(x, y, temp_0, this, false);
                              _createdAnimals.Add(zebra);
                          }
                          else if (temp_2 == 7)
                          {
                              var deer = new Deer(x, y, temp_0, this, false);
                              _createdAnimals.Add(deer);
                          }
                          else if (temp_2 == 8)
                          {
                              var bunny = new Bunny(x, y, temp_0, this, false);
                              _createdAnimals.Add(bunny);
                          }
                          else if (temp_2 == 9 || temp_2 == 10)
                          {
                              var human = new Human(x, y, temp_0, this, false);
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
