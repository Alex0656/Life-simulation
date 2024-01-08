using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace New_new
{
    public class FruitPoison : PlantEdible
    {
        public FruitPoison(int x, int y, Map map)
            : base(x, y, map)
        {
            Image = Image.FromFile(@"C:\Users\Алексей\source\repos\Life-simulation_02\New_new\img\FruitPoison.png");
            _appearanceRate = 20;
            _ticksToNextSpread = 20;
            limit_life = 50;
            stage = 10;
        }

        protected override void Spread()
        {
            int value = GameLogic.GenerateNumber(1, 4);
            if ((value == 1) && (_x < cols))
            {
                if (!IsFreeCell(_x + 1, _y))
                {
                    var plant = new FruitPoison(_x + 1, _y, _map);
                    _map._createdPlants.Add(plant);
                }
            }
            else if ((value == 2) && (_y < row))
            {
                if (!IsFreeCell(_x, _y + 1))
                {
                    var plant = new FruitPoison(_x, _y + 1, _map);
                    _map._createdPlants.Add(plant);
                }
            }
            else if ((value == 3) && (_x > 0))
            {
                if (!IsFreeCell(_x - 1, _y))
                {
                    var plant = new FruitPoison(_x - 1, _y, _map);
                    _map._createdPlants.Add(plant);
                }

            }
            else if ((value == 4) && (_y > 0))
            {
                if (!IsFreeCell(_x, _y - 1))
                {
                    var plant = new FruitPoison(_x, _y - 1, _map);
                    _map._createdPlants.Add(plant);
                }
            }
        }
        public override void Update()
        {
            _lifetime++;
            if (_lifetime == limit_life)
            {
                Die();
            }
        }
    }
}
