using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace New_new
{
    public class Building
    {
        public int _x { get; set; }
        public int _y { get; set; }
        public Image Image { get; set; }
        public Building(int x, int y)
        {
            _x = x;
            _y = y;
        }
    }
}
