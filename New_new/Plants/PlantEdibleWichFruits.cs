using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace New_new
{
    public abstract class PlantEdibleWichFruits : PlantEdible
    {
        
        public PlantEdibleWichFruits(int x, int y, Map map)
            : base(x, y, map)
        {
            _fruit = true;
        }
        

    }      
}
