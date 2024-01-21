using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using New_new;

namespace New_new.Tests
{
    internal class PlantMock: Plant
    {
        public PlantMock(int x, int y, Map map, int ticksToNextSpread = 3) : base(x, y, map)
        {
            _ticksToNextSpread = ticksToNextSpread;
        }

        public int CounterSpreadCalls = 0;

        public int CounterIsFreeCellFruitCalls = 0;

        protected override void Spread()
        {
            CounterSpreadCalls += 1;
        }

        protected override bool IsFreeCellFruit(int x, int y)
        {
            CounterIsFreeCellFruitCalls += 1;

            return base.IsFreeCellFruit(x, y);
        }
    }

    [TestClass()]
    public class PlantUpdateTests
    {
        [TestMethod()]
        public void UpdateMap_OnePlantTicksToNextSpread1Stage3_SpreadOnce()
        {
            var map = new Map(20, 200);

            var plantMock = new PlantMock(1, 2, map, 1);
            plantMock.stage = 3;

            map._createdPlants.Add(plantMock);

            var gameLogic = new GameLogic(map);
            gameLogic.Update_map();

            Assert.AreEqual(plantMock.CounterSpreadCalls, 1);
        }

        [TestMethod()]
        public void UpdateMap_OnePlantTicksToNextSpread0Stage3_DidNotSpread()
        {
            var map = new Map(20, 200);

            var plantMock = new PlantMock(1, 2, map, 0);
            plantMock.stage = 3;

            map._createdPlants.Add(plantMock);

            var gameLogic = new GameLogic(map);
            gameLogic.Update_map();

            Assert.AreEqual(plantMock.CounterSpreadCalls, 0);
        }

        [TestMethod()]
        public void UpdateMap_OnePlantTicksToNextSpread1Stage2_DidNotSpread()
        {
            var map = new Map(20, 200);

            var plantMock = new PlantMock(1, 2, map, 1);
            plantMock.stage = 2;

            map._createdPlants.Add(plantMock);

            var gameLogic = new GameLogic(map);
            gameLogic.Update_map();

            Assert.AreEqual(plantMock.CounterSpreadCalls, 0);
        }

        [TestMethod()]
        public void UpdateMap_OnePlantTicksToNextSpread43Stage2_DidNotSpread()
        {
            var map = new Map(20, 200);

            var plantMock = new PlantMock(1, 2, map, 43);
            plantMock.stage = 2;

            map._createdPlants.Add(plantMock);

            var gameLogic = new GameLogic(map);
            gameLogic.Update_map();

            Assert.AreEqual(plantMock.CounterSpreadCalls, 0);
        }

        // При вызове метода по распространению плодов проверка "свободности" клетки исходя из алгоритма должна вызываться только единожды.
        // Такой тест кейс приобретает большой смысл,
        // если эта проверка тяжелая для вычисления, и нам нужно проследить, что она лишний раз не вызывается
        [TestMethod()]
        public void SpreadFruitNoPoison_IsFreeCellFruit_calledOnce()
        {
            var map = new Map(20, 200);

            var plantMock = new PlantMock(1, 2, map, 43);

            plantMock.SpreadFruitNoPoison();

            Assert.AreEqual(plantMock.CounterIsFreeCellFruitCalls, 1);
        }
    }
}
