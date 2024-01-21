using System;
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
    public class Warehouse : Building
    {
        public Warehouse(int x, int y)
            : base(x, y)
        {
            Image = Image.FromFile(Path.Combine(Environment.CurrentDirectory, "img\\warehouse.png"));
        }
    }
}
