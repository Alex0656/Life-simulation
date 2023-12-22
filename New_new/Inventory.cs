using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace New_new
{
    public class Inventory
    {
        public int _meat { get; set; } = 0;
        public int _fruit { get; set; } = 0;
        public int _plant { get; set; } = 0;
        public int _inventory_limits { get; set; }  = 15;

        public bool sum() // если что-то есть в инвентаре, что можно скушать
        {
            if (_plant + _fruit + _meat > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool Limit() // если есть место, то можно добавить
        {
            if (_plant + _fruit + _meat <= _inventory_limits)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        

        /*
        protected override void Walk()
        {
            if (inventory.CollectMeat())
            {
                FoodWalkMeat();
            }
            if (inventory.CollectFruit())
            {
                FoodWalkFruit();
            }
            if (inventory.CollectPlant())
            {

            }

            else
            {
                int value = GameLogic.Random(0, 4);
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
            }
        }

        

        public bool CollectFruit()
        {
            if (_fruit < _fruit_limits)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool CollectMeat()
        {
            if (_meat < _meat_limits)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool CollectPlant()
        {
            if (_plant < _plant_limits)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

*/
    }
}