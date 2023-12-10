using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace New_new
{
    public class Predator : Animal
    {

        public Predator(int x, int y, bool female, Map map)
            : base(x, y, female, map)
        {
            _hp = 100;
            Color = Brushes.Red;
            _foodscale = 100;
            foodwalk_scale = 140;
            walk_scale = 240;
            limit_life = 1200;
        }

        protected override void GetNewChild()
        {
            Random rnd = new Random();
            int value = rnd.Next(0, 2);
            if (value == 1)
            {
                _ticksToNextReproduction = _appearanceRate;
                _map._createdAnimals.Add(new Predator(_x, _y, true, _map));
            }
            else
            {
                _ticksToNextReproduction = _appearanceRate;
                _map._createdAnimals.Add(new Predator(_x, _y, false, _map));
            }
        }
        protected override Animal FindPartner(int x, int y, bool female)
        {
            return _map._createdAnimals.FirstOrDefault(p => p._x == x && p._y == y && p._female != female &&
            p._ticksToNextReproduction <= 0 && p is Predator);
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
                if (_point_partner.X < _x)
                {
                    _x--;
                }
                else if (_point_partner.X > _x)
                {
                    _x++;
                }
                else if (_point_partner.Y < _y)
                {
                    _y--;
                }
                else if (_point_partner.Y > _y)
                {
                    _y++;
                }
            }
            else
            {
                Walk();
            }
        }
        protected override void Walk()
        {
            Random rnd = new Random();
            int value = rnd.Next(1, 5);
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
        }
        protected override bool IsFreeCell(int x, int y)
        {
            return _map._createdAnimals.FirstOrDefault(h => h._x == x && h._y == y && (h is Herbivores || h is Omnivorous)) != null;
        }
        private Animal CellAnimal(int x, int y)
        {
            return _map._createdAnimals.FirstOrDefault(h => h._x == x && h._y == y && (h is Herbivores || h is Omnivorous));
        }
        protected override void FoodWalk()
        {
            FoodSearch();
            var temp = CellAnimal(_x, _y);
            if (temp != null)
            {
                Food food = new Food(_foodscale);
                _foodscale = food.Replenish_maet();
                temp.death = true;
            }
            //else if (_point_eat.X == 0 && _point_eat.Y == 0)
            //{
            //    Walk();
            //}
            else
            {
                if (_point_eat.X < _x)
                {
                    _x--;
                }
                else if (_point_eat.X > _x)
                {
                    _x++;
                }
                else if (_point_eat.Y < _y)
                {
                    _y--;
                }
                else if (_point_eat.Y > _y)
                {
                    _y++;
                }
            }
        }


    }
}
