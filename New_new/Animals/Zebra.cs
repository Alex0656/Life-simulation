using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace New_new
{
    public class Zebra : Herbivores
    {
        public Zebra(int x, int y, bool female, Map map, bool child)
            : base(x, y, female, map, child)
        {
            Image = Image.FromFile(@"C:\Users\Алексей\source\repos\Life-simulation_02\New_new\img\zebra_3.png");
            _hp = 120;
            _foodscale = 100;
            foodwalk_scale = 70;
            walk_scale = 120;
            limit_life = 1000;
        }
        protected override void GetNewChild()
        {
            int value = GameLogic.Random(0, 1);
            if (value == 0)
            {
                _map._createdAnimals.Add(new Zebra(_x, _y, true, _map, true));
            }
            else
            {
                _map._createdAnimals.Add(new Zebra(_x, _y, false, _map, true));
            }
        }
        protected override Animal FindPartner(int x, int y, bool female)
        {
            return _map._createdAnimals.FirstOrDefault(p => p._x == x && p._y == y && p._female != female &&
            p._ticksToNextReproduction <= 0 && p is Zebra);
        }
    }
}
