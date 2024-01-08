using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace New_new
{
    public class GameLogic
    {
        private Map _map;
        private int cols;
        private int row;
        private int max_radius = 20;
        private int vilage_value = 5;
        static Random rnd = new Random();
        public GameLogic(Map map)
        {
            _map = map;
            cols = map._cols - 1;
            row = map._row - 1;
        }
        private int _season_time = 50;
        private int _update_season = 50;
        public bool _summer = true;

        private void IsVilage()
        {
            var _list_house = _map._createdBuilding.OfType<House>().ToList();
            for (int i = 0; i < _list_house.Count; i++)
            {
                if(_list_house[i].village == null)
                {
                    CheckDistance(_list_house[i]._x, _list_house[i]._y);
                }
            }
        }
        private void CheckDistance(int _x, int _y)
        {
            List<House> house = new List<House>();
            List<Point> free_cell = new List<Point>();
            Point point = new Point(_x, _y);
            free_cell.Add(point);
            int count = 0;

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
                    house.Add(temp);
                }

                if ((x1 < cols) && (matrix[x1 + 1, y1] == 0) && count <= max_radius)
                {
                    Point new_point = new Point(x1 + 1, y1);
                    free_cell.Add(new_point);
                    matrix[x1 + 1, y1] = 1;
                }
                if ((y1 < row) && (matrix[x1, y1 + 1] == 0) && count <= max_radius)
                {
                    Point new_point = new Point(x1, y1 + 1);
                    free_cell.Add(new_point);
                    matrix[x1, y1 + 1] = 1;
                }
                if ((x1 > 0) && (matrix[x1 - 1, y1] == 0) && count <= max_radius)
                {
                    Point new_point = new Point(x1 - 1, y1);
                    free_cell.Add(new_point);
                    matrix[x1 - 1, y1] = 1;
                }
                if ((y1 > 0) && (matrix[x1, y1 - 1] == 0) && count <= max_radius)
                {
                    Point new_point = new Point(x1, y1 - 1);
                    free_cell.Add(new_point);
                    matrix[x1, y1 - 1] = 1;
                }
                count++;
            }
            if(house.Count >= vilage_value)
            {
                Village village = new Village();
                foreach (House temp_house in house)
                {
                    temp_house.village = village;
                }
            }
            else
            {
                foreach (House temp_house in house)
                {
                    if(temp_house.village != null)
                    {
                        foreach (House temp_house2 in house)
                        {
                            temp_house2.village = temp_house.village;
                        }
                    }
                    
                }
            }
        }
        private House FindHouse(int x, int y)
        {
            var _list_house = _map._createdBuilding.OfType<House>().ToList();
            return _list_house.FirstOrDefault(p => p._x == x && p._y == y && p is House);
        }









        private void Update()
        {
            _season_time--;
            if (_season_time == 0)
            {
                _summer = !_summer;
                _season_time = _update_season; 
            }

            foreach (Plant plant in _map._createdPlants.ToList())
            {
                plant.Update();
            }
            _map._createdPlants = _map._createdPlants.Where(plant => !plant.death).ToList();

            foreach (KilledAnimal killed_animal in _map._createdKilledAnimals.ToList())
            {
                killed_animal.Update();
            }
            _map._createdKilledAnimals = _map._createdKilledAnimals.Where(killed_animal => !killed_animal.death).ToList();

            foreach (Animal animal in _map._createdAnimals.ToList())
            {
                animal.Update();
            }
            _map._createdAnimals = _map._createdAnimals.Where(animal => !animal.death).ToList();
            IsVilage();
        }
        public void Update_map()
        {
            Update();
        }

        public static void SeasonSelection()
        {

        }

        public static int Random(int x, int y)
        {
            int value = rnd.Next(x, y + 1);
            return value;
        }
    }
}
