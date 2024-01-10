using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSimulation
{
    public class KilledAnimal
    {
        private Map _map;
        public int _x { get; set; }
        public int _y { get; set; }
        private int _amount_of_meat = 10;
        public Image Image { get; set; } = Image.FromFile(@"C:\Users\Алексей\source\repos\Life-simulation_02\New_new\img\meat.png");
        public bool death { get; set; } = false;
        private int _lifetime = 0; 
        private int limit_life = 100;
        public KilledAnimal(int x, int y, Map map)
        {
            _x = x;
            _y = y;
            _map = map;
            //_amount_of_meat = amount_of_meat;
        }
        private void TakePiecePrivate()
        {
            _amount_of_meat--;
        }
        public void TakePiece()
        {
            TakePiecePrivate();
        }
        protected void Die()
        {
            death = true;
        }
        public void Update()
        {
            _lifetime++;
            if (TimeToDie())
            {
                Die();
            }
        }
        protected bool TimeToDie()
        {
            if ((_lifetime == limit_life) || (_amount_of_meat <= 0))
            {
                return true;
            }
            else { return false; }
        }
    }
}
