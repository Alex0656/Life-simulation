using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSimulation
{
    public abstract class Herbivores : Animal
    {
        public Herbivores(int x, int y, bool gender_female, Map map, bool child)
            : base(x, y, gender_female, map, child)
        {
            _hp = 120;
            _foodscale = 100;
            foodwalk_scale = 70;
            walk_scale = 120;
            limit_life = 1000;
        }

        protected override void Walk()
        {
            if (IsFreeCell(_point_eat.X, _point_eat.Y) == false)
            {
                Search(eat);
            }
            int value = GameLogic.GenerateNumber(1, 6);
            if ((value == 1) && (_x < cols))
            {
                _x = _x + 1;
            }
            else if ((value == 2) && (_y < row))
            {
                _y = _y + 1;
            }
            else if ((value == 3) && (_x > 0))
            {
                _x = _x - 1;
            }
            else if ((value == 4) && (_y > 0))
            {
                _y = _y - 1;
            }
            else if (value >= 5)
            {
                if (_point_eat.X == _x && _point_eat.Y < _y)
                {
                    _y--;
                }
                else if (_point_eat.X == _x && _point_eat.Y > _y)
                {
                    _y++;
                }
                else if (_point_eat.Y == _y && _point_eat.X > _x)
                {
                    _x++;
                }
                else if (_point_eat.Y == _y && _point_eat.X < _x)
                {
                    _x--;
                }

                else if (_point_eat.X > _x && _point_eat.Y < _y)
                {
                    _x++;
                    _y--;
                }
                else if (_point_eat.X < _x && _point_eat.Y < _y)
                {
                    _x--;
                    _y--;
                }
                else if (_point_eat.X < _x && _point_eat.Y > _y)
                {
                    _x--;
                    _y++;
                }
                else if (_point_eat.X > _x && _point_eat.Y > _y)
                {
                    _x++;
                    _y++;
                }

            }
        }
        protected override Boolean IsFreeCell(int x, int y)
        {
            return _map._createdPlants.FirstOrDefault(p => p._x == x && p._y == y && p is PlantEdible && p.stage > 1) != null;
        }
        private Plant CellPlant(int x, int y)
        {
            return _map._createdPlants.FirstOrDefault(h => h._x == x && h._y == y && h is PlantEdible && h.stage > 1);
        }
        protected override void FoodWalk()
        {
            if (IsFreeCell(_point_eat.X, _point_eat.Y) == false)
            {
                Search(eat);
            }
            var edible_plant = CellPlant(_x, _y);
            if (edible_plant != null)
            {
                Food food = new Food(_foodscale);
                _foodscale = food.Replenish_plant();
                edible_plant.death = true;
            }

            else if (_point_eat.X == _x && _point_eat.Y < _y)
            {
                _y--;
            }
            else if (_point_eat.X == _x && _point_eat.Y > _y)
            {
                _y++;
            }
            else if (_point_eat.Y == _y && _point_eat.X > _x)
            {
                _x++;
            }
            else if (_point_eat.Y == _y && _point_eat.X < _x)
            {
                _x--;
            }

            else if (_point_eat.X > _x && _point_eat.Y < _y)
            {
                _x++;
                _y--;
            }
            else if (_point_eat.X < _x && _point_eat.Y < _y)
            {
                _x--;
                _y--;
            }
            else if (_point_eat.X < _x && _point_eat.Y > _y)
            {
                _x--;
                _y++;
            }
            else if (_point_eat.X > _x && _point_eat.Y > _y)
            {
                _x++;
                _y++;
            }
        }
    }
}
