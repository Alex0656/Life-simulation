using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSimulation
{
    public class Human : Animal
    {
        private Human my_partner;
        private House my_house;
        public House house_my_parants;
        private Profession profession;
        public Childhood childhood;
        private Human my_child;
        private Inventory inventory = new Inventory();
        private int _good_distance = 15;
        private int _normal_distance = 50;
        private Point _my_neighbor;
        private bool flag_child = false;
        public Human(int x, int y, bool female, Map map, bool child)
            : base(x, y, female, map, child)
        {
            if (child)
            {
                Image = Image.FromFile(@"C:\Users\Алексей\source\repos\Life-simulation_02\New_new\img\child.png");
            }
            else if (female)
            {
                Image = Image.FromFile(@"C:\Users\Алексей\source\repos\Life-simulation_02\New_new\img\Human_2.png");
            }
            else if (!female)
                Image = Image.FromFile(@"C:\Users\Алексей\source\repos\Life-simulation_02\New_new\img\Human.png");

            _hp = 100;
            _foodscale = 200;
            foodwalk_scale = 100;
            walk_scale = 200;
            limit_life = 6000;
            _appearanceRate = 50;
            _ticksToNextReproduction = 50;
        }
        
        protected override Boolean IsFreeCell(int x, int y)
        {
            return (_map._createdPlants.FirstOrDefault(p => p._x == x && p._y == y && p is PlantEdible && p.stage > 2 && !(p is PlantEdibleWichFruitsPoison || p is PlantEdibleWithoutFruitsPoison || p is FruitPoison)) != null) 
                || (_map._createdAnimals.FirstOrDefault(h => h._x == x && h._y == y && (h is Herbivores || h is Predator || h is Omnivorous)) != null) || (_map._createdKilledAnimals.FirstOrDefault(h => h._x == x && h._y == y) != null);
        }
        private Animal CellAnimal(int x, int y)
        {
            return _map._createdAnimals.FirstOrDefault(h => h._x == x && h._y == y && (h is Herbivores || h is Predator || h is Omnivorous));
        }
        private Plant CellFruit(int x, int y)
        {
            return _map._createdPlants.FirstOrDefault(h => h._x == x && h._y == y && h is Fruit);
        }
        private Plant CellPlant(int x, int y)
        {
            return _map._createdPlants.FirstOrDefault(h => h._x == x && h._y == y && h.stage > 2 && (h is PlantEdibleWichFruitsNoPoison || h is PlantEdibleWithoutFruitsNoPoison));
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
            var fruit = CellFruit(_x, _y);
            var plant = CellPlant(_x, _y);
            var maet = CellKilledAnimal(_x, _y);
            var animal = CellAnimal(_x, _y);

            if (maet != null)
            {
                Food food = new Food(_foodscale);
                _foodscale = food.Replenish_maet();
                maet.TakePiece();
            }
            else if (fruit != null)
            {
                Food food = new Food(_foodscale);
                _foodscale = food.Replenish_plant();
                fruit.death = true;
            }
            else if (plant != null)
            {
                Food food = new Food(_foodscale);
                _foodscale = food.Replenish_plant();
                plant.death = true;
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
                WalkLog(_point_eat.X, _point_eat.Y);
            }
        }
        protected void FoodExtraction()
        {
            if (IsFreeCell(_point_eat.X, _point_eat.Y) == false)
            {
                FoodSearch();
            }
            var fruit = CellFruit(_x, _y);
            var plant = CellPlant(_x, _y);
            var carrion = CellKilledAnimal(_x, _y);
            var animal = CellAnimal(_x, _y);

            if (carrion != null)
            {
                inventory._meat++;
                carrion.TakePiece();
            }
            else if (fruit != null)
            {
                inventory._fruit++;
                fruit.death = true;
            }
            else if (plant != null)
            {
                inventory._plant++;
                plant.death = true;
            }
            else if (animal != null)
            {
                inventory._meat++;
                animal.death = true;
                var killed_animal = new KilledAnimal(_x, _y, _map);
                _map._createdKilledAnimals.Add(killed_animal);
            }
            else
            {
                WalkLog(_point_eat.X, _point_eat.Y);
            }
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
        private void EatInventory()
        {
            
            Food food = new Food(_foodscale);
            if (inventory._fruit > 0)
            {
                _foodscale = food.Replenish_plant();
                inventory._fruit--;
            }
            else if (inventory._plant > 0)
            {
                _foodscale = food.Replenish_plant();
                inventory._plant--;
            }
            else if (inventory._meat > 0)
            {
                _foodscale = food.Replenish_maet();
                inventory._meat--;
            }
        }
        public void WalkLog(int x, int y)
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
        public override void Update()
        {
            _lifetime++;
            _ticksToNextReproduction--;
            if (_child == true)
            {
                if(flag_child == false)
                {
                    childhood = new Childhood(this);
                    flag_child = true;
                }
                
                if (childhood.Check())
                {
                    _child = false;
                }
                else
                {
                    childhood.GrowingUp();
                }
            }
            else if (_foodscale <= 0)
            {
                _hp--;
                if (inventory.CheckForEmptiness())
                {
                    EatInventory();
                }
                else
                {
                    FoodWalk();
                }
            }
            else if (_foodscale <= foodwalk_scale)
            {
                _foodscale--;
                if (inventory.CheckForEmptiness())
                {
                    EatInventory();
                }
                else
                {
                    FoodWalk();
                }
            }
            else if (_foodscale <= walk_scale)
            {
                _foodscale = _foodscale - 1;

                if (_ticksToNextReproduction <= 0)
                {
                    Reproduction();
                }
                else if (my_house == null && !_gender && my_partner != null)
                {
                    BuildHouse();
                }
                else  
                {
                    if (inventory.CheckForOverflow())
                    {
                        FoodExtraction();
                    }
                    else if (!inventory.CheckForOverflow())
                    {
                        Walk();
                    }
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

        private int CheckDistance(int _x, int _y)
        {
            List<Point> free_cell = new List<Point>();
            Point point = new Point(_x, _y);
            free_cell.Add(point);
            int count = 1;

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


                if (FindHouse(x1, y1) != null)
                {
                    var temp = FindHouse(x1, y1);
                    _my_neighbor.X = temp._x;
                    _my_neighbor.Y = temp._y;
                    break;
                }

                if ((x1 < cols) && (matrix[x1 + 1, y1] == 0))
                {
                    Point new_point = new Point(x1 + 1, y1);
                    free_cell.Add(new_point);
                    matrix[x1 + 1, y1] = count;
                }
                if ((y1 < row) && (matrix[x1, y1 + 1] == 0))
                {
                    Point new_point = new Point(x1, y1 + 1);
                    free_cell.Add(new_point);
                    matrix[x1, y1 + 1] = count;
                }
                if ((x1 > 0) && (matrix[x1 - 1, y1] == 0))
                {
                    Point new_point = new Point(x1 - 1, y1);
                    free_cell.Add(new_point);
                    matrix[x1 - 1, y1] = count;
                }
                if ((y1 > 0) && (matrix[x1, y1 - 1] == 0))
                {
                    Point new_point = new Point(x1, y1 - 1);
                    free_cell.Add(new_point);
                    matrix[x1, y1 - 1] = count;
                }
                count++;
            }
            return count;
        }
        public House FindHouse(int x, int y)
        {
            var _list_house = _map._createdBuilding.OfType<House>().ToList();
            return _list_house.FirstOrDefault(p => p._x == x && p._y == y && p is House);
        }
        private void BuildHouse()
        {
            var _list_house = _map._createdBuilding.OfType<House>().ToList();
            if (_list_house.Count == 0)
            {
                var house = new House(_x, _y);
                _map._createdBuilding.Add(house);
                my_house = house;
                my_partner.my_house = house;
            }
            else if (CheckDistance(_x, _y) <= _good_distance && FindHouse(_x, _y) == null)
            {
                var house = new House(_x, _y);
                _map._createdBuilding.Add(house);
                my_house = house;
                my_partner.my_house = house;
            }
            else if (CheckDistance(_x, _y) <= _normal_distance)
            {
                WalkLog(_my_neighbor.X, _my_neighbor.Y);
            }
            else if (FindHouse(_x, _y) == null)
            {
                var house = new House(_x, _y);
                _map._createdBuilding.Add(house);
                my_house = house;
                my_partner.my_house = house;
            }
        }
        protected override void Reproduction()
        {
            if (my_house == null)
            {
                if (my_partner != null)
                {
                    if (my_partner._x == _x && my_partner._y == _y)
                    {
                        _ticksToNextReproduction = _appearanceRate;
                    }
                    else
                    {
                        WalkLog(my_partner._x, my_partner._y);
                    }
                }
                else
                {
                    PartnerSearch();
                }
            }
            else if (my_house != null)
            {
                if (my_partner != null)
                {
                    if (my_house._x == _x && my_house._y == _y)
                    {
                        if (my_partner._x == my_house._x && my_partner._y == my_house._y)
                        {
                            _ticksToNextReproduction = _appearanceRate;
                            if (_gender == true)
                            {
                                GetNewChild();
                            }
                        }
                    }
                    else
                    {
                        WalkLog(my_house._x, my_house._y);
                    }
                }
                else
                {
                    PartnerSearch();
                }
            }
            else
            {
                Walk();
            }
        }
        protected override void PartnerSearch()
        {
            PartnerSearchCell();

            if (FindPartner(_x, _y, _gender) != null)
            {
                my_partner = (Human)FindPartner(_x, _y, _gender);
                my_partner.my_partner = this;
                if(my_partner.my_house != null)
                {
                    my_house = my_partner.my_house;
                }
                else
                {
                    my_partner.my_house = my_house;
                }
            }
            else if (FindPartner(_point_partner.X, _point_partner.Y, _gender) != null)
            {
                WalkLog(_point_partner.X, _point_partner.Y);
            }
            else
            {
                Walk();
            }
        }
        protected override void GetNewChild()
        {
            int value = GameLogic.GenerateNumber(0, 1);
            if (value == 0)
            {
                my_child = new Human(_x, _y, true, _map, true);
            }
            else
            {
                my_child = new Human(_x, _y, false, _map, true);
            }
            my_child.house_my_parants = my_house;
            _map._createdAnimals.Add(my_child);
        }
        protected override Animal FindPartner(int x, int y, bool female)
        {
            var _list_human = _map._createdAnimals.OfType<Human>();

            return _list_human.FirstOrDefault(p => p._x == x && p._y == y && p._gender != female &&
            p._ticksToNextReproduction <= 0 && p is Human && p.my_partner == null); 
        }

    }
}
