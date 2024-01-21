using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace New_new
{
    public class Fruit : PlantEdible
    {
        public Fruit(int x, int y, Map map)
            : base(x, y, map)
        {
            Image = Image.FromFile(Path.Combine(Environment.CurrentDirectory, "img\\Fruit.png"));
            _appearanceRate = 20;
            _ticksToNextSpread = 20;
            limit_life = 50;
            stage = 10;
        }
        
        protected override void Spread()
        {
            int value = GameLogic.Random(1, 4);
            if ((value == 1) && (_x < cols))
            {
                if (!IsFreeCell(_x + 1, _y))
                {
                    var plant = new Fruit(_x + 1, _y, _map);
                    _map._createdPlants.Add(plant);
                }
            }
            else if ((value == 2) && (_y < row))
            {
                if (!IsFreeCell(_x, _y + 1))
                {
                    var plant = new Fruit(_x, _y + 1, _map);
                    _map._createdPlants.Add(plant);
                }
            }
            else if ((value == 3) && (_x > 0))
            {
                if (!IsFreeCell(_x - 1, _y))
                {
                    var plant = new Fruit(_x - 1, _y, _map);
                    _map._createdPlants.Add(plant);
                }

            }
            else if ((value == 4) && (_y > 0))
            {
                if (!IsFreeCell(_x, _y - 1))
                {
                    var plant = new Fruit(_x, _y - 1, _map);
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
