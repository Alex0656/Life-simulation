using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace New_new
{
    public class Plant
    {
        private Map _map;
        private int _appearanceRate = 10;
        private int _ticksToNextSpread = 10;
        public int _x;
        public int _y;
        private int _lifetime;
        private int cols;
        private int row;
        public bool death = false;


        public Plant(int x, int y, Map map)
        {
            _x = x;
            _y = y;
            _map = map;
            cols = map._cols;
            row = map._row;
        }

        private Boolean IsFreeCell(int x, int y)
        {
            return _map._createdPlants.FirstOrDefault(p => p._x == x && p._y == y) != null;
        }
        private void Spread()
        {

            Random rnd = new Random();
            int value = rnd.Next(1, 5);
            if ((value == 1) && (_x < cols))
            {
                
                if (!IsFreeCell(_x + 1, _y))
                {
                    var plant = new Plant(_x + 1, _y, _map);
                    _map._createdPlants.Add(plant);
                }
                    
            }
            else if ((value == 2) && (_y < row))
            {
                if (!IsFreeCell(_x, _y + 1))
                {
                    var plant = new Plant(_x, _y + 1, _map);
                    _map._createdPlants.Add(plant);
                }
                
            }
            else if ((value == 3) && (_x > 0))
            {
                if (!IsFreeCell(_x - 1, _y))
                {
                    var plant = new Plant(_x - 1, _y, _map);
                    _map._createdPlants.Add(plant);
                }
                
            }
            else if ((value == 4) && (_y > 0))
            {
                if (!IsFreeCell(_x, _y - 1))
                {
                    var plant = new Plant(_x, _y - 1, _map);
                    _map._createdPlants.Add(plant);
                }  
            }
        }

        private void Die()
        {
            death = true;
            
        }


        public void Update()
        {
            _ticksToNextSpread--;
            _lifetime++;

            if (_ticksToNextSpread == 0)
            {
                Spread();
                _ticksToNextSpread = _appearanceRate;
            }

            if (_lifetime == 400)
            {
                Die();
            }
        }

    }
}
