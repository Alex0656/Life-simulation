using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSimulation
{
    public abstract class Omnivorous : Animal
    {
        public Omnivorous(int x, int y, bool female, Map map, bool child)
            : base(x, y, female, map, child)
        {
            _hp = 100;
            _foodscale = 100;
            foodwalk_scale = 70;
            walk_scale = 170;
            limit_life = 600;
        }
        
        protected override Boolean IsFreeCell(int x, int y)
        {
            return (_map._createdPlants.FirstOrDefault(p => p._x == x && p._y == y && p is PlantEdible && p.stage > 1) != null) || (_map._createdKilledAnimals.FirstOrDefault(h => h._x == x && h._y == y && h is KilledAnimal) != null) || (_map._createdAnimals.FirstOrDefault(h => h._x == x && h._y == y && (h is Herbivores || h is Predator)) != null);
        }
        private Animal CellAnimal(int x, int y)
        {
            return _map._createdAnimals.FirstOrDefault(h => h._x == x && h._y == y && (h is Herbivores || h is Predator));
        }
        private Plant CellPlant(int x, int y)
        {
            return _map._createdPlants.FirstOrDefault(h => h._x == x && h._y == y && h is PlantEdible && h.stage > 1);
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
            var plant = CellPlant(_x, _y);
            var maet = CellKilledAnimal(_x, _y);
            var animal = CellAnimal(_x, _y);
            Food food = new Food(_foodscale);

            if (maet != null)
            {
                _foodscale = food.Replenish_maet();
                maet.TakePiece();
            }
            else if (plant != null)
            {
                _foodscale = food.Replenish_plant();
                plant.death = true;
            }
            else if (animal != null)
            {
                _foodscale = food.Replenish_maet();
                animal.death = true;
                var killed_animal = new KilledAnimal(_x, _y, _map);
                _map._createdKilledAnimals.Add(killed_animal);
            }
            else
            {
                if (_point_eat.Y == _y && _x < _point_eat.X)
                {
                    _x++;
                }
                else if (_point_eat.Y == _y && _x > _point_eat.X)
                {
                    _x--;
                }
                else if (_point_eat.X == _x && _y > _point_eat.Y)
                {
                    _y--;
                }
                else if (_point_eat.X == _x && _y < _point_eat.Y)
                {
                    _y++;
                }

                else if (_point_eat.X > _x && _point_eat.Y < _y && flag1 == true)
                {
                    _x++;
                    flag1 = false;
                }
                else if (_point_eat.X > _x && _point_eat.Y < _y && flag1 == false)
                {
                    _y--;
                    flag1 = true;
                }

                else if (_point_eat.X < _x && _point_eat.Y < _y && flag1 == true)
                {
                    _x--;
                    flag1 = false;
                }
                else if (_point_eat.X < _x && _point_eat.Y < _y && flag1 == false)
                {
                    _y--;
                    flag1 = true;
                }

                else if (_point_eat.X < _x && _point_eat.Y > _y && flag1 == true)
                {
                    _x--;
                    flag1 = false;
                }
                else if (_point_eat.X < _x && _point_eat.Y > _y && flag1 == false)
                {
                    _y++;
                    flag1 = true;
                }

                else if (_point_eat.X > _x && _point_eat.Y > _y && flag1 == true)
                {
                    _x++;
                    flag1 = false;
                }
                else if (_point_eat.X > _x && _point_eat.Y > _y && flag1 == false)
                {
                    _y++;
                    flag1 = true;
                }
            }
        }
    }
}
