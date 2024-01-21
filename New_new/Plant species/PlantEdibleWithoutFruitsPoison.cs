﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace New_new
{
    public class PlantEdibleWithoutFruitsPoison : PlantEdibleWithoutFruits
    {
        public PlantEdibleWithoutFruitsPoison(int x, int y, Map map)
            : base(x, y, map)
        {
            Image = Image.FromFile(Path.Combine(Environment.CurrentDirectory, "img\\Ediblepoison.png"));
            _poison = true;
            _appearanceRate = 10;
            _ticksToNextSpread = 10;
            limit_life = 3000;
            _ticksToNextStage = 10;
            _appearanceRateStage = 10;
        }
        protected override void Spread()
        {
            int value = GameLogic.Random(1, 4);
            if ((value == 1) && (_x < cols))
            {
                if (!IsFreeCell(_x + 1, _y))
                {
                    var plant = new PlantEdibleWithoutFruitsPoison(_x + 1, _y, _map);
                    _map._createdPlants.Add(plant);
                }
            }
            else if ((value == 2) && (_y < row))
            {
                if (!IsFreeCell(_x, _y + 1))
                {
                    var plant = new PlantEdibleWithoutFruitsPoison(_x, _y + 1, _map);
                    _map._createdPlants.Add(plant);
                }

            }
            else if ((value == 3) && (_x > 0))
            {
                if (!IsFreeCell(_x - 1, _y))
                {
                    var plant = new PlantEdibleWithoutFruitsPoison(_x - 1, _y, _map);
                    _map._createdPlants.Add(plant);
                }

            }
            else if ((value == 4) && (_y > 0))
            {
                if (!IsFreeCell(_x, _y - 1))
                {
                    var plant = new PlantEdibleWithoutFruitsPoison(_x, _y - 1, _map);
                    _map._createdPlants.Add(plant);
                }
            }
        }
    }
}
