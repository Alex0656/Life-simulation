using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace New_new
{
    public class Omnivorous : Animal
    {
        private bool flag1 = true;
        public Omnivorous(int x, int y, bool female, Map map)
            : base(x, y, female, map)
        {
            _hp = 100;
            Color = Brushes.Brown;
            _foodscale = 100;
            foodwalk_scale = 70;
            walk_scale = 170;
            limit_life = 600;
        }

        protected override void GetNewChild()
        {
            Random rnd = new Random();
            int value = rnd.Next(0, 2);
            if (value == 1)
            {
                _ticksToNextReproduction = _appearanceRate;
                _map._createdAnimals.Add(new Omnivorous(_x, _y, true, _map));
            }
            else
            {
                _ticksToNextReproduction = _appearanceRate;
                _map._createdAnimals.Add(new Omnivorous(_x, _y, true, _map));
            }
        }
        protected override Animal FindPartner(int x, int y, bool female)
        {
            return _map._createdAnimals.FirstOrDefault(p => p._x == x && p._y == y && p._female != female &&
            p._ticksToNextReproduction <= 0 && p is Omnivorous);
        }

        private void MoveOmnivorous(int x, int y, bool flag)
        {
            if (y == _y && _x < x)
            {
                _x++;
            }
            else if (y == _y && _x > x)
            {
                _x--;
            }
            else if (x == _x && _y > y)
            {
                _y--;
            }
            else if (x == _x && _y < y)
            {
                _y++;
            }

            else if (x > _x && y < _y && flag1 == true)
            {
                _x++;
                flag1 = false;
            }
            else if (x > _x && y < _y && flag1 == false)
            {
                _y--;
                flag1 = true;
            }

            else if (x < _x && y < _y && flag1 == true)
            {
                _x--;
                flag1 = false;
            }
            else if (x < _x && y < _y && flag1 == false)
            {
                _y--;
                flag1 = true;
            }

            else if (x < _x && y > _y && flag1 == true)
            {
                _x--;
                flag1 = false;
            }
            else if (x < _x && y > _y && flag1 == false)
            {
                _y++;
                flag1 = true;
            }

            else if (x > _x && y > _y && flag1 == true)
            {
                _x++;
                flag1 = false;
            }
            else if (x > _x && y > _y && flag1 == false)
            {
                _y++;
                flag1 = true;
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
                MoveOmnivorous(_point_partner.X, _point_partner.Y, flag1);
            }
            else
            {
                Walk();
            }
        }
        protected override void Walk()
        {
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
                Point _point_mid = new Point();
                int i = 0;
                var _list_omnivores = _map._createdAnimals.OfType<Omnivorous>().ToList();
                foreach (Omnivorous omnivorous in _list_omnivores)
                {
                    _point_mid.X = _point_mid.X + omnivorous._x;
                    _point_mid.Y = _point_mid.Y + omnivorous._y;
                    i++;
                }
                _point_mid.X = _point_mid.X / i;
                _point_mid.Y = _point_mid.Y / i;
                if (_point_mid.X > _x && _point_mid.Y < _y && flag1 == true)
                {
                    _x++;
                    flag1 = false;
                }
                else if (_point_mid.X > _x && _point_mid.Y < _y && flag1 == false)
                {
                    _y--;
                    flag1 = true;
                }

                else if (_point_mid.X < _x && _point_mid.Y < _y && flag1 == true)
                {
                    _x--;
                    flag1 = false;
                }
                else if (_point_mid.X < _x && _point_mid.Y < _y && flag1 == false)
                {
                    _y--;
                    flag1 = true;
                }

                else if (_point_mid.X < _x && _point_mid.Y > _y && flag1 == true)
                {
                    _x--;
                    flag1 = false;
                }
                else if (_point_mid.X < _x && _point_mid.Y > _y && flag1 == false)
                {
                    _y++;
                    flag1 = true;
                }

                else if (_point_mid.X > _x && _point_mid.Y > _y && flag1 == true)
                {
                    _x++;
                    flag1 = false;
                }
                else if (_point_mid.X > _x && _point_mid.Y > _y && flag1 == false)
                {
                    _y++;
                    flag1 = true;
                }

            }
        }
        protected override bool IsFreeCell(int x, int y)
        {
            return _map._createdPlants.FirstOrDefault(p => p._x == x && p._y == y) != null || _map._createdAnimals.FirstOrDefault(h => h._x == x && h._y == y && (h is Herbivores || h is Predator)) != null;
        }
        private Animal CellAnimal(int x, int y)
        {
            return _map._createdAnimals.FirstOrDefault(h => h._x == x && h._y == y && (h is Herbivores || h is Predator));
        }
        private Plant CellPlant(int x, int y)
        {
            return _map._createdPlants.FirstOrDefault(h => h._x == x && h._y == y && h is Plant);
        }
        protected override void FoodWalk()
        {
            FoodSearch();
            var temp_animal = CellAnimal(_x, _y);
            var temp_plant = CellPlant(_x, _y);
            if (temp_plant != null)
            {
                Food food = new Food(_foodscale);
                _foodscale = food.Replenish_plant();
                temp_plant.death = true;
            }
            else if (temp_animal != null)
            {
                Food food = new Food(_foodscale);
                _foodscale = food.Replenish_maet();
                temp_animal.death = true;
            }
            else
            {
                MoveOmnivorous(_point_eat.X, _point_eat.Y, flag1);
            }
        }
    }
}
