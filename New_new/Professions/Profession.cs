using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace New_new
{
    public abstract class Profession
    {
        protected Map _map;
        public Profession(Map map)
        {
            _map = map;
        }
        public abstract void GetProfession();
    }
}
