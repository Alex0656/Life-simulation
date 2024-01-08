using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace New_new
{
    public class Inventory
    {
        public List<Resources> MyResources = new List<Resources>();
        public int _meat { get; set; } = 0;
        public int _fruit { get; set; } = 0;
        public int _plant { get; set; } = 0;

        private int _inventory_limits = 10;

        public bool CheckForEmptiness()
        {
            if (_plant + _fruit + _meat > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool CheckForOverflow()
        {
            if (_plant + _fruit + _meat < _inventory_limits)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}