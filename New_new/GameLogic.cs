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
    public class GameLogic
    {
        private Map _map;
        public GameLogic(Map map)
        {
            _map = map;
        }

        private void Update()
        {
            foreach (Animal animal in _map._createdAnimals.ToList())
            {
                animal.Update();
            }
            _map._createdAnimals = _map._createdAnimals.Where(animal => !animal.death).ToList();


            foreach (Plant plant in _map._createdPlants.ToList())
            {
                plant.Update();
            }
            _map._createdPlants = _map._createdPlants.Where(plant => !plant.death).ToList();
        }
        public void Update_map()
        {
            Update();
        }

        public int MyRandom(int x, int y)
        {
            Random rnd = new Random();
            int value = rnd.Next(x, y + 1);
            return value;
        }
    }
}
