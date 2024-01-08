﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSimulation
{
    public abstract class PlantEdible : Plant
    {

        public PlantEdible(int x, int y, Map map)
            : base(x, y, map)
        {
            stage = 1;
            
        }

    }
}
