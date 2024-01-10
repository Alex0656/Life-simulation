using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace LifeSimulation
{
    public class Bear : Omnivorous
    {
        public Bear(int x, int y, bool female, Map map, bool child)
            : base(x, y, female, map, child)
        {
            Image = Image.FromFile(@"C:\Users\Алексей\source\repos\Life-simulation_02\New_new\img\bear_2.png");
            _hp = 100;
            _foodscale = 100;
            foodwalk_scale = 70;
            walk_scale = 170;
            limit_life = 600;
        }
        protected override void GetNewChild()
        {
            int value = GameLogic.GenerateNumber(0, 1);
            if (value == 0)
            {
                _map._createdAnimals.Add(new Bear(_x, _y, true, _map, true));
            }
            else
            {
                _map._createdAnimals.Add(new Bear(_x, _y, false, _map, true));
            }
        }
        protected override Animal FindPartner(int x, int y, bool female)
        {
            return _map._createdAnimals.FirstOrDefault(p => p._x == x && p._y == y && p._gender_female != female &&
            p._ticksToNextReproduction <= 0 && p is Bear);
        }
        protected override void Walk()
        {
            int value = GameLogic.GenerateNumber(1, 5);
            GeneratingRandomValue(value);
            if (value >= 5)
            {
                Point _point_mid = new Point();
                int i = 0;
                var _list_bears = _map._createdAnimals.OfType<Bear>().ToList();
                foreach (Bear bears in _list_bears)
                {
                    _point_mid.X = _point_mid.X + bears._x;
                    _point_mid.Y = _point_mid.Y + bears._y;
                    i++;
                }
                _point_mid.X = _point_mid.X / i;
                _point_mid.Y = _point_mid.Y / i;
                WalkOmnivorous(_point_mid.X, _point_mid.Y);
            }
        }
    }
}
