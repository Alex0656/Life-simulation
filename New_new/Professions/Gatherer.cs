using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace New_new
{
    public class Gatherer : Profession
    {
        public Gatherer(Map map)
             : base(map)
        {

        }
        public override void GetProfession()
        {

        }
        private Plant CellFruit(int x, int y)
        {
            return _map._createdPlants.FirstOrDefault(h => h._x == x && h._y == y && h is Fruit);
        }
        private Plant CellPlant(int x, int y)
        {
            return _map._createdPlants.FirstOrDefault(h => h._x == x && h._y == y && h.stage > 2 && (h is PlantEdibleWichFruitsNoPoison || h is PlantEdibleWithoutFruitsNoPoison));
        }
    }
}
