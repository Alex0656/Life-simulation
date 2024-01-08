using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Microsoft.VisualBasic.ApplicationServices;

namespace New_new
{
    public class PlantEdibleWichFruitsNoPoison : PlantEdibleWichFruits
    {
        public PlantEdibleWichFruitsNoPoison(int x, int y, Map map)
            : base(x, y, map)
        {
            Image = Image.FromFile(@"C:\Users\Алексей\source\repos\Life-simulation_02\New_new\img\EdibleWichFruit.png");
            _poison = false;
            _appearanceRate = 10;
            _ticksToNextSpread = 10;
            limit_life = 3000;
            _ticksToNextStage = 10;
            _appearanceRateStage = 10;
            _ticksToNextFruit = 15;
            _appearanceFruit = 15;
        }
        protected override void Spread()
        {
            int value = GameLogic.GenerateNumber(1, 4);
            if ((value == 1) && (_x < cols))
            {
                if (!IsFreeCell(_x + 1, _y))
                {
                    var plant = new PlantEdibleWichFruitsNoPoison(_x + 1, _y, _map);
                    _map._createdPlants.Add(plant);
                }
            }
            else if ((value == 2) && (_y < row))
            {
                if (!IsFreeCell(_x, _y + 1))
                {
                    var plant = new PlantEdibleWichFruitsNoPoison(_x, _y + 1, _map);
                    _map._createdPlants.Add(plant);
                }

            }
            else if ((value == 3) && (_x > 0))
            {
                if (!IsFreeCell(_x - 1, _y))
                {
                    var plant = new PlantEdibleWichFruitsNoPoison(_x - 1, _y, _map);
                    _map._createdPlants.Add(plant);
                }

            }
            else if ((value == 4) && (_y > 0))
            {
                if (!IsFreeCell(_x, _y - 1))
                {
                    var plant = new PlantEdibleWichFruitsNoPoison(_x, _y - 1, _map);
                    _map._createdPlants.Add(plant);
                }
            }
        }
        public override void Update()
        {
            if (stage == 3)
            {
                _ticksToNextSpread--;
                _ticksToNextFruit--;
                _lifetime++;

                if (_ticksToNextFruit == 0)
                {
                    SpreadFruitNoPoison();
                    _ticksToNextFruit = _appearanceFruit;
                }
                if (_ticksToNextSpread == 0)
                {
                    Spread();
                    _ticksToNextSpread = _appearanceRate;
                }

                if (_lifetime == limit_life)
                {
                    Die();
                }
            }
            else if (stage == 1 || stage == 2)
            {
                Grow();
            }
        }
    }
}
