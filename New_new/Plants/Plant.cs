using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace New_new
{
    public abstract class Plant
    {
        protected Map _map;
        public int _x { get; set; }
        public int _y { get; set; }
        public Image Image { get; set; }
        public bool death { get; set; } = false;
        public bool _poison;
        public int stage;
        protected bool _fruit; // аккуратнее 
        protected int _ticksToNextStage;
        protected int _appearanceRateStage;
        protected int _ticksToNextFruit;
        protected int _appearanceFruit;

        protected int _appearanceRate;
        protected int _ticksToNextSpread;
        protected int _lifetime = 0;
        protected int limit_life = 1000;
        protected int cols;
        protected int row;
        public Plant(int x, int y, Map map)
        {
            _x = x;
            _y = y;
            _map = map;
            cols = map._cols;
            row = map._row;
        }
        protected Boolean IsFreeCell(int x, int y)
        {
            return _map._createdPlants.FirstOrDefault(p => p._x == x && p._y == y && !(p is Fruit || p is FruitPoison)) != null;
        }
        protected Boolean IsFreeCellFruit(int x, int y)
        {
            return _map._createdPlants.FirstOrDefault(p => p._x == x && p._y == y && (p is Fruit || p is FruitPoison)) != null; // 
        }
        protected abstract void Spread();
        //protected abstract Plant GetNewPlant();
        /*
        protected void Spread()
        {
            int value = GameLogic.Random(1, 4);
            if ((value == 1) && (_x < cols))
            {
                if (!IsFreeCell(_x + 1, _y))
                {
                    var plant = new EdiblePlants(_x + 1, _y, _map, _poison, _fruit);
                    //var plant = new GetNewPlant()(_x + 1, _y, _map, _poison, _fruit);
                    _map._createdPlants.Add(plant);
                }
            }
            else if ((value == 2) && (_y < row))
            {
                if (!IsFreeCell(_x, _y + 1))
                {
                    var plant = new EdiblePlants(_x, _y + 1, _map, _poison, _fruit);
                    _map._createdPlants.Add(plant);
                }

            }
            else if ((value == 3) && (_x > 0))
            {
                if (!IsFreeCell(_x - 1, _y))
                {
                    var plant = new EdiblePlants(_x - 1, _y, _map, _poison, _fruit);
                    _map._createdPlants.Add(plant);
                }

            }
            else if ((value == 4) && (_y > 0))
            {
                if (!IsFreeCell(_x, _y - 1))
                {
                    var plant = new EdiblePlants(_x, _y - 1, _map, _poison, _fruit);
                    _map._createdPlants.Add(plant);
                }
            }
        }
        */
        protected void Die()
        {
            death = true;  
        }
        protected void Grow()
        {
            if (_ticksToNextStage == 0)
            {
                stage++;
                if ((stage == 2) || (stage == 3))
                {
                    _ticksToNextStage = _appearanceRateStage;
                }
            }
            else
            {
                _ticksToNextStage--;
            }
        }

        protected void SpreadFruitPoison()
        {
            int value = GameLogic.Random(1, 4);
            if ((value == 1) && (_x < cols))
            {
                if (!IsFreeCellFruit(_x + 1, _y))
                {
                    var fruit = new FruitPoison(_x + 1, _y, _map);
                    _map._createdPlants.Add(fruit);
                }
            }
            else if ((value == 2) && (_y < row))
            {
                if (!IsFreeCellFruit(_x, _y + 1))
                {
                    var fruit = new FruitPoison(_x, _y + 1, _map);
                    _map._createdPlants.Add(fruit);
                }

            }
            else if ((value == 3) && (_x > 0))
            {
                if (!IsFreeCellFruit(_x - 1, _y))
                {
                    var fruit = new FruitPoison(_x - 1, _y, _map);
                    _map._createdPlants.Add(fruit);
                }

            }
            else if ((value == 4) && (_y > 0))
            {
                if (!IsFreeCellFruit(_x, _y - 1))
                {
                    var fruit = new FruitPoison(_x, _y - 1, _map);
                    _map._createdPlants.Add(fruit);
                }
            }
        }
        protected void SpreadFruitNoPoison()
        {

            int value = GameLogic.Random(1, 4);
            if ((value == 1) && (_x < cols))
            {
                if (!IsFreeCellFruit(_x + 1, _y))
                {
                    var fruit = new Fruit(_x + 1, _y, _map);
                    _map._createdPlants.Add(fruit);
                }
            }
            else if ((value == 2) && (_y < row))
            {
                if (!IsFreeCellFruit(_x, _y + 1))
                {
                    var fruit = new Fruit(_x, _y + 1, _map);
                    _map._createdPlants.Add(fruit);
                }

            }
            else if ((value == 3) && (_x > 0))
            {
                if (!IsFreeCellFruit(_x - 1, _y))
                {
                    var fruit = new Fruit(_x - 1, _y, _map);
                    _map._createdPlants.Add(fruit);
                }

            }
            else if ((value == 4) && (_y > 0))
            {
                if (!IsFreeCellFruit(_x, _y - 1))
                {
                    var fruit = new Fruit(_x, _y - 1, _map);
                    _map._createdPlants.Add(fruit);
                }
            }
        }
        public virtual void Update()
        {
            if (stage == 3)
            {
                _ticksToNextSpread--;
                _lifetime++;

                if (_ticksToNextSpread == 0)
                {
                    Spread();
                    _ticksToNextSpread = _appearanceRate;
                }

                if (_lifetime == limit_life)
                {
                    Die();
                }
            }
            else if (stage == 1 || stage == 2)
            { 
                Grow();
            }
            //base.Update();
        }
        
    }
}
