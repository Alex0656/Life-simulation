using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace New_new
{
    public class Mine<T> where T : Resources, new()
    {
        
        private int _mine_limits;
        private int _x;
        private int _y;

        public bool death { get; set; } = false;
        public Mine(int mine_limits, int x, int y)
        {
            //Image = Image.FromFile(@"C:\Users\wayww\source\repos\New_new\New_new\img\Human.png");
            _mine_limits = mine_limits;
            _x = x;
            _y = y;
        }


        

        public T GetResource()
        {
            _mine_limits--;
            return new T();
        }
        
        private void RaiseP()
        {
            _mine_limits = _mine_limits - 5;

        }
        public void Raise() // сбор без инструментов
        {
            RaiseP();
        }
        private void Die()
        {
            death = true;
        }
        public void Update() // ссылка в GameLogic
        {
            if (_mine_limits <= 0)
            {
                Die();
            }
        }
    }
}

