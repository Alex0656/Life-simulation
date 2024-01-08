using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSimulation
{
    public class Childhood
    {
        private int time = 0;
        private int ticks_growing = 50;
        private Human _human;
        public Childhood(Human human)
        {
            _human = human;
        }

        protected void Walk()
        {
            int value = GameLogic.GenerateNumber(0, 4);
            if ((value == 1) && (_human._x < _human.cols))
            {
                _human._x = _human._x + 1;
            }
            else if ((value == 2) && (_human._y < _human.row))
            {
                _human._y = _human._y + 1;
            }
            else if ((value == 3) && (_human._x > 0))
            {
                _human._x = _human._x - 1;
            }
            else if ((value == 4) && (_human._y > 0))
            {
                _human._y = _human._y - 1;
            }
        }
        public bool Check()
        {
            if (time >= ticks_growing)
            {
                if (_human._gender)
                {
                    _human.Image = Image.FromFile(@"C:\Users\wayww\source\repos\New_new\New_new\img\Human_2.png");
                }
                else if (!_human._gender)
                {
                    _human.Image = Image.FromFile(@"C:\Users\wayww\source\repos\New_new\New_new\img\Human.png");
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
            if (_human._foodscale <= 0)
            {
                _human._hp--;
                if (_human.FindHouse(_human._x, _human._y) != null)
                {
                    Food food = new Food(_human._foodscale);
                    _human._foodscale = food.Replenish_plant();
                }
                else
                {
                    _human.WalkLog(_human.house_my_parants._x, _human.house_my_parants._y);
                }
            }
            else if (_human._foodscale <= _human.foodwalk_scale)
            {
                _human._foodscale = _human._foodscale - 5;
                if (_human.FindHouse(_human._x, _human._y) != null)
                {
                    Food food = new Food(_human._foodscale);
                    _human._foodscale = food.Replenish_plant();
                }
                else
                {
                    _human.WalkLog(_human.house_my_parants._x, _human.house_my_parants._y);
                }
            }
            else if (_human._foodscale <= _human.walk_scale)
            {
                _human._foodscale = _human._foodscale - 5;
                Walk();
            }
        }
        public void GrowingUp()
        {
            Growing_log();
        }


    }
}
