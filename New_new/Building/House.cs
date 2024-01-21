using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Reflection;
using System.IO;
using System.Windows.Forms;

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
                Image = Image.FromFile(Path.Combine(Environment.CurrentDirectory, "img\\House_2.png"));
            }
            else if(village != null)
            {
                Image = Image.FromFile(Path.Combine(Environment.CurrentDirectory, "img\\House_3.png"));
            }
            
        }
        //public bool village = false;

        
    }
}
