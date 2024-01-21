﻿using System;
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
    public class PlantInedibleWithoutFruits : PlantInedible
    {
        public PlantInedibleWithoutFruits(int x, int y, Map map)
            : base(x, y, map)
        {
            Image = Image.FromFile(Path.Combine(Environment.CurrentDirectory, "img\\Inedible.png"));
            _fruit = false;
            _appearanceRate = 25;
            _ticksToNextSpread = 25;
            limit_life = 3000;
            _ticksToNextStage = 25;
            _appearanceRateStage = 25;
        }
        protected override void Spread()
        {
            int value = GameLogic.Random(1, 4);
            if ((value == 1) && (_x < cols))
            {
                if (!IsFreeCell(_x + 1, _y))
                {
                    var plant = new PlantInedibleWithoutFruits(_x + 1, _y, _map);
                    _map._createdPlants.Add(plant);
                }
            }
            else if ((value == 2) && (_y < row))
            {
                if (!IsFreeCell(_x, _y + 1))
                {
                    var plant = new PlantInedibleWithoutFruits(_x, _y + 1, _map);
                    _map._createdPlants.Add(plant);
                }

            }
            else if ((value == 3) && (_x > 0))
            {
                if (!IsFreeCell(_x - 1, _y))
                {
                    var plant = new PlantInedibleWithoutFruits(_x - 1, _y, _map);
                    _map._createdPlants.Add(plant);
                }

            }
            else if ((value == 4) && (_y > 0))
            {
                if (!IsFreeCell(_x, _y - 1))
                {
                    var plant = new PlantInedibleWithoutFruits(_x, _y - 1, _map);
                    _map._createdPlants.Add(plant);
                }
            }
        }
    }
}
