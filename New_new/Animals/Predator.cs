using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace New_new
{
    public abstract class Predator : Animal
    {
        
        public Predator(int x, int y, bool female, Map map, bool child)
            : base(x, y, female, map, child)
        {
            _hp = 100;
            _foodscale = 200;
            foodwalk_scale = 140;
            walk_scale = 240;
            limit_life = 1200;
        }

        protected override void Walk()
        {
            int value = GameLogic.GenerateNumber(0, 4);
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
        }
        protected override Boolean IsFreeCell(int x, int y)
        {
            return (_map._createdAnimals.FirstOrDefault(h => h._x == x && h._y == y && (h is Herbivores || h is Omnivorous)) != null) || (_map._createdKilledAnimals.FirstOrDefault(h => h._x == x && h._y == y && h is KilledAnimal) != null);
        } 
        private Animal CellAnimal(int x, int y)
        {
            return _map._createdAnimals.FirstOrDefault(h => h._x == x && h._y == y && (h is Herbivores || h is Omnivorous));
        }
        private KilledAnimal CellKilledAnimal(int x, int y)
        {
            return _map._createdKilledAnimals.FirstOrDefault(h => h._x == x && h._y == y);
        }
        protected override void FoodWalk()
        {
            if (IsFreeCell(_point_eat.X, _point_eat.Y) == false)
            {
                FoodSearch();
            }

            var maet = CellKilledAnimal(_x, _y);
            var animal = CellAnimal(_x, _y);
            if (maet != null)
            {
                Food food = new Food(_foodscale);
                _foodscale = food.Replenish_maet();
                maet.TakePiece();
            }
            else if (animal != null)
            {
                Food food = new Food(_foodscale);
                _foodscale = food.Replenish_maet();
                animal.death = true;
                var killed_animal = new KilledAnimal(_x, _y, _map);
                _map._createdKilledAnimals.Add(killed_animal);
            }
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
