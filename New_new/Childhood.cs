using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSimulation
{
    public class Childhood : Human
    {
        const int voracity = 5;
        private int time = 0;
        private int ticks_growing = 50;
        public Childhood(int x, int y, bool gender_female, Map map, bool child)
            : base(x, y, gender_female, map, child)
        {
            _hp = 100;
            _foodscale = 200;
            foodwalk_scale = 100;
            walk_scale = 200;
            limit_life = 6000;
            _appearanceRate = 50;
            _ticksToNextReproduction = 50; ;
        }
        public bool Check()
        {
            if (time >= ticks_growing)
            {
                if (_gender_female)
                {
                    Image = Image.FromFile(@"C:\Users\wayww\source\repos\New_new\New_new\img\Human_2.png");
                }
                else if (!_gender_female)
                {
                    Image = Image.FromFile(@"C:\Users\wayww\source\repos\New_new\New_new\img\Human.png");
                }
                // profession.GetProfession();
                return true;
            }
            else
            {
                return false;
            }
        }
        private void Growing_log()
        {
            time++;
            if (_foodscale <= 0)
            {
                _hp--;
                if (FindHouse(_x, _y) != null)
                {
                    Food food = new Food(_foodscale);
                    _foodscale = food.Replenish_plant();
                }
                else
                {
                    WalkAnimalDefault(house_my_parants._x, house_my_parants._y);
                }
            }
            else if (_foodscale <= foodwalk_scale)
            {
                _foodscale = _foodscale - voracity;
                if (FindHouse(_x, _y) != null)
                {
                    Food food = new Food(_foodscale);
                    _foodscale = food.Replenish_plant();
                }
                else
                {
                    WalkAnimalDefault(house_my_parants._x, house_my_parants._y);
                }
            }
            else if (_foodscale <= walk_scale)
            {
                _foodscale = _foodscale - voracity;
                Walk();
            }
        }
        public void GrowingUp()
        {
            Growing_log();
        }


    }
}
