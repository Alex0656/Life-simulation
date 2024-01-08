using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSimulation
{
    class Food
    {
        private int _satiety_plant = 50;
        private int _satiety_meat = 100;

        private int _satiety_scale;

        public Food(int satiety_scale)
        {
            _satiety_scale = satiety_scale;
        }

        private int Update_satiety_meat()
        {
            return _satiety_scale = _satiety_scale + _satiety_meat;
        }
        public int Replenish_maet()
        {
            return Update_satiety_meat();
        }
        private int Update_satiety_plant()
        {
            return _satiety_scale = _satiety_scale + _satiety_plant;
        }
        public int Replenish_plant()
        {
            return Update_satiety_plant();
        }


    }
}
