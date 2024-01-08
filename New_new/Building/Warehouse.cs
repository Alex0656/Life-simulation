using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace LifeSimulation
{
    public class Warehouse : Building
    {
        public Warehouse(int x, int y)
            : base(x, y)
        {
            Image = Image.FromFile(@"C:\Users\Алексей\source\repos\Life-simulation_02\New_new\img\warehouse.png");
        }
    }
}
