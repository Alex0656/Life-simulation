using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSimulation
{
    class Inventory_new
    {

        //public List<Resources> _createdResources { get; set; } = new List<Resources>(); // Есть шахта
        private List<Plant> _createdEdibles { get; } = new List<Plant>(); // есть абстракт класс Plant

        private int _food_limits = 25;
        //private int _resources_limits = 25;
        /*
        public int EatInventoryy(int _foodscale)
        {
            Food food = new Food(_foodscale);
            var temp = _createdEdibles[_createdEdibles.Count - 1];
            _createdEdibles.RemoveAt(_createdEdibles.Count - 1);
            
            if(temp == Plant)
            {
                return _foodscale = food.Replenish_plant();
            }
            else
            {
                return _foodscale = food.Replenish_maet();
            }
            
        }
         */
        public bool sum_food()
        {
            if (_createdEdibles.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool Limit_food()
        {
            if (_createdEdibles.Count < _food_limits)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
