using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace New_new
{
    public class Herbivores : Animal
    {
        public Herbivores(int x, int y, bool female, Map map)
            : base(x, y, female, map)
        {
            _hp = 120;
            Color = Brushes.Yellow;
            _foodscale = 100;
            foodwalk_scale = 70;
            walk_scale = 120;
            limit_life = 1000;
        }

        protected override void GetNewChild()
        {
            Random rnd = new Random();
            int value = rnd.Next(0, 2);
            if (value == 1)
            {
                _ticksToNextReproduction = _appearanceRate;
                _map._createdAnimals.Add(new Herbivores(_x, _y, true, _map));
            }
            else
            {
                _ticksToNextReproduction = _appearanceRate;
                _map._createdAnimals.Add(new Herbivores(_x, _y, false, _map));
            }
        }
        protected override Animal FindPartner(int x, int y, bool female)
        {
            return _map._createdAnimals.FirstOrDefault(p => p._x == x && p._y == y && p._female != female &&
            p._ticksToNextReproduction <= 0 && p is Herbivores);
        }

        private void MoveHerbivore(int x, int y)
        {
            if (x == _x && y < _y)
            {
                _y--;
            }
            else if (x == _x && y > _y)
            {
                _y++;
            }
            else if (y == _y && x > _x)
            {
                _x++;
            }
            else if (y == _y && x < _x)
            {
                _x--;
            }

            else if (x > _x && y < _y)
            {
                _x++;
                _y--;
            }
            else if (x < _x && y < _y)
            {
                _x--;
                _y--;
            }
            else if (x < _x && y > _y)
            {
                _x--;
                _y++;
            }
            else if (x > _x && y > _y)
            {
                _x++;
                _y++;
            }
        }
        protected override void PartnerSearch()
        {
            PartnerSearchCell();

            if (FindPartner(_x, _y, _female) != null)
            {
                Reproduction();
            }
            else if (FindPartner(_point_partner.X, _point_partner.Y, _female) != null)
            {
                MoveHerbivore(_point_partner.X, _point_partner.Y);
            }
            else
            {
                Walk();
            }
        }
        protected override void Walk()
        {
            FoodSearch();
            Random rnd = new Random();
            int value = rnd.Next(1, 7);
            if (value == 1 && _x < cols)
            {
                _x = _x + 1;
            }
            else if (value == 2 && _y < row)
            {
                _y = _y + 1;
            }
            else if (value == 3 && _x > 0)
            {
                _x = _x - 1;
            }
            else if (value == 4 && _y > 0)
            {
                _y = _y - 1;
            }
            else if (value >= 5)
            {
                MoveHerbivore(_point_eat.X, _point_eat.Y);
            }
        }
        protected override bool IsFreeCell(int x, int y)
        {
            return _map._createdPlants.FirstOrDefault(p => p._x == x && p._y == y) != null;
        }
        protected override void FoodWalk()
        {
            FoodSearch();
            if (IsFreeCell(_x, _y))
            {

                Food food = new Food(_foodscale);
                _foodscale = food.Replenish_plant();

                foreach (Plant plant in _map._createdPlants.ToList())
                {
                    if (plant._x == _x && plant._y == _y)
                    {
                        plant.death = true;
                    }

                }
            }
            //else if (_point_eat.X == 0 && _point_eat.Y == 0 && !IsFreeCell(_point_eat.X, _point_eat.Y, new_map))
            //{
            //    Walk(new_map);
            //}

            else
            {
                MoveHerbivore(_point_eat.X, _point_eat.Y);
            }
        }

    }
}
