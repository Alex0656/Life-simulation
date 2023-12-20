using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace New_new
{
    class PlantInedibleWichFruitsPoison : PlantInedible
    {
        public PlantInedibleWichFruitsPoison(int x, int y, Map map)
            : base(x, y, map)
        {
            Image = Image.FromFile(@"C:\Users\Алексей\source\repos\Life-simulation_02\New_new\img\InediblePoison.png");
            _poison = true;
            _appearanceRate = 25;
            _ticksToNextSpread = 25;
            limit_life = 3000;
            _ticksToNextStage = 25;
            _appearanceRateStage = 25;
            _ticksToNextFruit = 5;
            _appearanceFruit = 5;
        }
        protected override void Spread()
        {
            int value = GameLogic.Random(1, 4);
            if ((value == 1) && (_x < cols))
            {
                if (!IsFreeCell(_x + 1, _y))
                {
                    var plant = new PlantInedibleWichFruitsPoison(_x + 1, _y, _map);
                    _map._createdPlants.Add(plant);
                }
            }
            else if ((value == 2) && (_y < row))
            {
                if (!IsFreeCell(_x, _y + 1))
                {
                    var plant = new PlantInedibleWichFruitsPoison(_x, _y + 1, _map);
                    _map._createdPlants.Add(plant);
                }

            }
            else if ((value == 3) && (_x > 0))
            {
                if (!IsFreeCell(_x - 1, _y))
                {
                    var plant = new PlantInedibleWichFruitsPoison(_x - 1, _y, _map);
                    _map._createdPlants.Add(plant);
                }

            }
            else if ((value == 4) && (_y > 0))
            {
                if (!IsFreeCell(_x, _y - 1))
                {
                    var plant = new PlantInedibleWichFruitsPoison(_x, _y - 1, _map);
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
                    SpreadFruitPoison();
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
            //base.Update();
        }
    }
}
