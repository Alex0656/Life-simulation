using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace New_new
{
    public abstract class PlantInedibleWichFruits : PlantInedible
    {
        public PlantInedibleWichFruits(int x, int y, Map map)
            : base(x, y, map)
        {
            _fruit = true;
        }
        
    }
}
