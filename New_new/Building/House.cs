using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace New_new
{
    public class House : Building
    {
        public Village village;
        public House(int x, int y)
            : base(x, y)
        {
            if (village == null)
            {
                Image = Image.FromFile(@"C:\Users\Алексей\source\repos\Life-simulation_02\New_new\img\House_2.png");
            }
            else if(village != null)
            {
                Image = Image.FromFile(@"C:\Users\Алексей\source\repos\Life-simulation_02\New_new\img\House_3.png");
            }
            
        }
        //public bool village = false;

        
    }
}
