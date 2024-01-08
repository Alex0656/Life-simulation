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
    
        public int _foodscale = 100; 
        public int cols;
        public int row;
        protected int _lifetime = 0; 
        public int _hp = 20;
        protected bool flag1 = true;

        public int foodwalk_scale = 70;
        public int walk_scale = 120;
        protected int limit_life = 1000;

        public Image Image { get; set; }
        public bool death { get; set; } = false;
        protected Point _point_eat = new Point(0,0);


        protected Point _point_partner = new Point();
        public bool _female;
        public bool _child;
        protected int _appearanceRate = 30;
        public int _ticksToNextReproduction { get; set; } = 30; 

        public Animal(int x, int y, bool female, Map map, bool child)
        {
            _x = x;
            _y = y;
            _female = female;
            _map = map;
            _child = child;
            cols = map._cols - 1;
            row = map._row - 1;
        }
        protected abstract void GetNewChild();
        protected abstract Animal FindPartner(int x, int y, bool female);

        private void CreateZeroMatrix(int[,] matrix, int cols, int row)
        {
            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < row; j++)
                {
                    matrix[i, j] = 0;
                }
            }
        }
        protected void PartnerSearchCell()
        {
            List<Point> free_cell = new List<Point>();
            Point point = new Point(_x, _y);
            free_cell.Add(point);

            int[,] matrix = new int[cols + 1, row + 1];
            CreateZeroMatrix(matrix, cols, row);
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
                if ((x1 < cols) && (matrix[x1 + 1, y1] == 0))
                {
                    Point new_point = new Point(x1 + 1, y1);
                    free_cell.Add(new_point);
                    matrix[x1 + 1, y1] = 1;
                }
                if ((y1 < row) && (matrix[x1, y1 + 1] == 0))
                {
                    Point new_point = new Point(x1, y1 + 1);
                    free_cell.Add(new_point);
                    matrix[x1, y1 + 1] = 1;
                }
                if ((x1 > 0) && (matrix[x1 - 1, y1] == 0))
                {
                    Point new_point = new Point(x1 - 1, y1);
                    free_cell.Add(new_point);
                    matrix[x1 - 1, y1] = 1;
                }
                if ((y1 > 0) && (matrix[x1, y1 - 1] == 0))
                {
                    Point new_point = new Point(x1, y1 - 1);
                    free_cell.Add(new_point);
                    matrix[x1, y1 - 1] = 1;
                }
            }

        }
        protected virtual void Reproduction()
        {
            
            _ticksToNextReproduction = _appearanceRate;
            if (_female == true)
            {
                GetNewChild();
            }
        }
        protected virtual void PartnerSearch()
        {
            PartnerSearchCell();

            if (FindPartner(_x, _y, _female) != null)
            {
                Reproduction();
            }
            else if (FindPartner(_point_partner.X, _point_partner.Y, _female) != null)
            {
                if (_point_partner.Y == _y && _x < _point_partner.X)
                {
                    _x++;
                }
                else if (_point_partner.Y == _y && _x > _point_partner.X)
                {
                    _x--;
                }
                else if (_point_partner.X == _x && _y > _point_partner.Y)
                {
                    _y--;
                }
                else if (_point_partner.X == _x && _y < _point_partner.Y)
                {
                    _y++;
                }

                else if (_point_partner.X > _x && _point_partner.Y < _y && flag1 == true)
                {
                    _x++;
                    flag1 = false;
                }
                else if (_point_partner.X > _x && _point_partner.Y < _y && flag1 == false)
                {
                    _y--;
                    flag1 = true;
                }

                else if (_point_partner.X < _x && _point_partner.Y < _y && flag1 == true)
                {
                    _x--;
                    flag1 = false;
                }
                else if (_point_partner.X < _x && _point_partner.Y < _y && flag1 == false)
                {
                    _y--;
                    flag1 = true;
                }

                else if (_point_partner.X < _x && _point_partner.Y > _y && flag1 == true)
                {
                    _x--;
                    flag1 = false;
                }
                else if (_point_partner.X < _x && _point_partner.Y > _y && flag1 == false)
                {
                    _y++;
                    flag1 = true;
                }

                else if (_point_partner.X > _x && _point_partner.Y > _y && flag1 == true)
                {
                    _x++;
                    flag1 = false;
                }
                else if (_point_partner.X > _x && _point_partner.Y > _y && flag1 == false)
                {
                    _y++;
                    flag1 = true;
                }
            }
            else
            {
                Walk();
            }
        }
        protected abstract void Walk();
        protected abstract Boolean IsFreeCell(int x, int y);
        protected void FoodSearch()
        {

            List<Point> free_cell = new List<Point>();
            Point point = new Point(_x, _y);
            free_cell.Add(point);

            int[,] matrix = new int[cols + 1, row + 1];
            CreateZeroMatrix(matrix, cols, row);
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
                if ((x1 < cols) && (matrix[x1 + 1, y1] == 0))
                {
                    Point new_point = new Point(x1 + 1, y1);
                    free_cell.Add(new_point);
                    matrix[x1 + 1, y1] = 1;
                }
                if ((y1 < row) && (matrix[x1, y1 + 1] == 0))
                {
                    Point new_point = new Point(x1, y1 + 1);
                    free_cell.Add(new_point);
                    matrix[x1, y1 + 1] = 1;
                }
                if ((x1 > 0) && (matrix[x1 - 1, y1] == 0))
                {
                    Point new_point = new Point(x1 - 1, y1);
                    free_cell.Add(new_point);
                    matrix[x1 - 1, y1] = 1;
                }
                if ((y1 > 0) && (matrix[x1, y1 - 1] == 0))
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
        public virtual void Update()
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
            if (_hp == 0)
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
