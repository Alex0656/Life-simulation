using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace New_new
{
    public class Hunter : Profession
    {
        public Hunter(Map map)
            : base(map)
        {

        }
        public override void GetProfession()
        {

        }
        private Animal CellAnimal(int x, int y)
        {
            return _map._createdAnimals.FirstOrDefault(h => h._x == x && h._y == y && (h is Herbivores || h is Predator || h is Omnivorous));
        }

    }
}
