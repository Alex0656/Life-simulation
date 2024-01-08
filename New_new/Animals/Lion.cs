using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace New_new
{
    public class Lion : Predator
    {
        public Lion(int x, int y, bool female, Map map, bool child)
            : base(x, y, female, map, child)
        {
            Image = Image.FromFile(@"C:\Users\Алексей\source\repos\Life-simulation_02\New_new\img\lion_2.png");
            _hp = 100;
            _foodscale = 200;
            foodwalk_scale = 140;
            walk_scale = 240;
            limit_life = 1200;
        }
        protected override void GetNewChild()
        {
            int value = GameLogic.Random(0, 1);
            if (value == 0)
            {
                _map._createdAnimals.Add(new Lion(_x, _y, true, _map, true));
            }
            else
            {
                _map._createdAnimals.Add(new Lion(_x, _y, false, _map, true));
            }
        }
        protected override Animal FindPartner(int x, int y, bool female)
        {
            return _map._createdAnimals.FirstOrDefault(p => p._x == x && p._y == y && p._gender != female &&
            p._ticksToNextReproduction <= 0 && p is Lion);
        }
    }
}
