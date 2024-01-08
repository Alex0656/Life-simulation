using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace New_new
{
    public class Hog : Omnivorous
    {
        public Hog(int x, int y, bool female, Map map, bool child)
            : base(x, y, female, map, child)
        {

            Image = Image.FromFile(@"C:\Users\Алексей\source\repos\Life-simulation_02\New_new\img\hog_2.png");
            _hp = 100;
            _foodscale = 100;
            foodwalk_scale = 70;
            walk_scale = 170;
            limit_life = 600;
        }
        protected override void GetNewChild()
        {
            int value = GameLogic.Random(0, 1);
            if (value == 0)
            {
                _map._createdAnimals.Add(new Hog(_x, _y, true, _map, true));
            }
            else
            {
                _map._createdAnimals.Add(new Hog(_x, _y, false, _map, true));
            }
        }
        protected override Animal FindPartner(int x, int y, bool female)
        {
            return _map._createdAnimals.FirstOrDefault(p => p._x == x && p._y == y && p._gender != female &&
            p._ticksToNextReproduction <= 0 && p is Hog);
        }
        protected override void Walk()
        {
            int value = GameLogic.Random(1, 5);
            if ((value == 1) && (_x < cols))
            {
                _x = _x + 1;
            }
            else if ((value == 2) && (_y < row))
            {
                _y = _y + 1;
            }
            else if ((value == 3) && (_x > 0))
            {
                _x = _x - 1;
            }
            else if ((value == 4) && (_y > 0))
            {
                _y = _y - 1;
            }
            else if (value >= 5)
            {
                Point _point_mid = new Point();
                int i = 0;
                var _list_hogs = _map._createdAnimals.OfType<Hog>().ToList();
                foreach (Hog hogs in _list_hogs)
                {
                    _point_mid.X = _point_mid.X + hogs._x;
                    _point_mid.Y = _point_mid.Y + hogs._y;
                    i++;
                }
                _point_mid.X = _point_mid.X / i;
                _point_mid.Y = _point_mid.Y / i;
                if (_point_mid.X > _x && _point_mid.Y < _y && flag1 == true)
                {
                    _x++;
                    flag1 = false;
                }
                else if (_point_mid.X > _x && _point_mid.Y < _y && flag1 == false)
                {
                    _y--;
                    flag1 = true;
                }

                else if (_point_mid.X < _x && _point_mid.Y < _y && flag1 == true)
                {
                    _x--;
                    flag1 = false;
                }
                else if (_point_mid.X < _x && _point_mid.Y < _y && flag1 == false)
                {
                    _y--;
                    flag1 = true;
                }

                else if (_point_mid.X < _x && _point_mid.Y > _y && flag1 == true)
                {
                    _x--;
                    flag1 = false;
                }
                else if (_point_mid.X < _x && _point_mid.Y > _y && flag1 == false)
                {
                    _y++;
                    flag1 = true;
                }

                else if (_point_mid.X > _x && _point_mid.Y > _y && flag1 == true)
                {
                    _x++;
                    flag1 = false;
                }
                else if (_point_mid.X > _x && _point_mid.Y > _y && flag1 == false)
                {
                    _y++;
                    flag1 = true;
                }

            }
        }
    }
}
