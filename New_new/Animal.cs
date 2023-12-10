using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace New_new
{
    public abstract class Animal
    {
        protected Map _map;
        public int _x { get; set; }
        public int _y { get; set; }
        //private Point _x_y = new Point();
        protected int cols;
        protected int row;
        protected int _lifetime;
        protected int _foodscale = 100; // текущий запас еды
        protected int _hp = 20; // если нет запаса еды, то тратится hp

        protected int foodwalk_scale = 70; // при этом лимите начинается поиск еды
        protected int walk_scale = 120; // животное есть не хочет, ищет партнёра
        protected int limit_life = 1000; // каждый тик лимит уменьшается

        public Brush Color { get; set; }
        public bool death = false;
        protected Point _point_eat = new Point(-2, -2);
        protected Point _point_partner = new Point();

        public bool _female;
        protected int _appearanceRate = 100; // сколько тиков нужно до следующего размножения
        public int _ticksToNextReproduction = 100; 

        public Animal(int x, int y, bool female, Map map)
        {
            _x = x;
            _y = y;
            _female = female;
            _map = map;
            cols = map._cols - 1;
            row = map._row - 1;
        }
        protected abstract void GetNewChild();
        protected abstract Animal FindPartner(int x, int y, bool female);
        protected void PartnerSearchCell()
        {

            List<Point> free_cell = new List<Point>();
            Point point = new Point(_x, _y);
            free_cell.Add(point);

            int[,] matrix = new int[cols + 1, row + 1];
            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < row; j++)
                {
                    matrix[i, j] = 0;
                }
            }

            matrix[_x, _y] = 1;

            while (free_cell.Count != 0)
            {
                int x1 = free_cell[0].X;
                int y1 = free_cell[0].Y;
                free_cell.RemoveAt(0);


                if (FindPartner(x1, y1, _female) != null)
                {
                    _point_partner.X = x1;
                    _point_partner.Y = y1;
                    break;
                }
                if (x1 < cols && matrix[x1 + 1, y1] == 0)
                {
                    Point new_point = new Point(x1 + 1, y1);
                    free_cell.Add(new_point);
                    matrix[x1 + 1, y1] = 1;
                }
                if (y1 < row && matrix[x1, y1 + 1] == 0)
                {
                    Point new_point = new Point(x1, y1 + 1);
                    free_cell.Add(new_point);
                    matrix[x1, y1 + 1] = 1;
                }
                if (x1 > 0 && matrix[x1 - 1, y1] == 0)
                {
                    Point new_point = new Point(x1 - 1, y1);
                    free_cell.Add(new_point);
                    matrix[x1 - 1, y1] = 1;
                }
                if (y1 > 0 && matrix[x1, y1 - 1] == 0)
                {
                    Point new_point = new Point(x1, y1 - 1);
                    free_cell.Add(new_point);
                    matrix[x1, y1 - 1] = 1;
                }
            }

        }
        protected void Reproduction()
        {
            GetNewChild();
        }
        protected abstract void PartnerSearch();
        protected abstract void Walk();
        protected abstract bool IsFreeCell(int x, int y);
        protected void FoodSearch()
        {

            List<Point> free_cell = new List<Point>();
            Point point = new Point(_x, _y);
            free_cell.Add(point);

            int[,] matrix = new int[cols + 1, row + 1];
            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < row; j++)
                {
                    matrix[i, j] = 0;
                }
            }

            matrix[_x, _y] = 1;

            while (free_cell.Count != 0)
            {
                int x1 = free_cell[0].X;
                int y1 = free_cell[0].Y;
                free_cell.RemoveAt(0);


                if (IsFreeCell(x1, y1))
                {
                    _point_eat.X = x1;
                    _point_eat.Y = y1;
                    break;
                }
                if (x1 < cols && matrix[x1 + 1, y1] == 0)
                {
                    Point new_point = new Point(x1 + 1, y1);
                    free_cell.Add(new_point);
                    matrix[x1 + 1, y1] = 1;
                }
                if (y1 < row && matrix[x1, y1 + 1] == 0)
                {
                    Point new_point = new Point(x1, y1 + 1);
                    free_cell.Add(new_point);
                    matrix[x1, y1 + 1] = 1;
                }
                if (x1 > 0 && matrix[x1 - 1, y1] == 0)
                {
                    Point new_point = new Point(x1 - 1, y1);
                    free_cell.Add(new_point);
                    matrix[x1 - 1, y1] = 1;
                }
                if (y1 > 0 && matrix[x1, y1 - 1] == 0)
                {
                    Point new_point = new Point(x1, y1 - 1);
                    free_cell.Add(new_point);
                    matrix[x1, y1 - 1] = 1;
                }
            }

        }
        protected abstract void FoodWalk();
        protected void Die()
        {
            death = true;
        }
        public void Update()
        {
            _lifetime++;
            _ticksToNextReproduction--;
            if (_foodscale == 0)
            {
                _hp--;
                FoodWalk();
            }
            else if (_foodscale <= foodwalk_scale)
            {
                _foodscale--;
                FoodWalk();
            }
            else if (_foodscale <= walk_scale)
            {
                _foodscale--;
                if (_ticksToNextReproduction <= 0)
                {
                    PartnerSearch();
                }
                else
                {
                    Walk();
                }
            }
            if (_hp == 0) // можно объединить
            {
                Die();
            }
            if (_lifetime == limit_life)
            {
                Die();
            }
        }
    }

}
