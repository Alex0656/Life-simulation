using Microsoft.VisualStudio.TestTools.UnitTesting;
using New_new;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace New_new.Tests
{
    internal class LocalConstants
    {
        public static int ANIMAL_LIFE_LIMIT = 1000;
    }

    internal class AnimalMock: Animal
    {
        public AnimalMock(int x, int y, Map map, int lifeTime = 0): base(x, y, false, map, false) {
            limit_life = LocalConstants.ANIMAL_LIFE_LIMIT;
            _lifetime = lifeTime;
        }

        public int CounterUpdateCalls = 0;

        public int CounterDieCalls = 0;

        public override void Update()
        {
            CounterUpdateCalls += 1;

            base.Update();
        }

        protected override void Die()
        {
            CounterDieCalls += 1;

            base.Die();
        }

        protected override void FoodWalk() { }

        protected override bool IsFreeCell(int x, int y) { return true; }

        protected override AnimalMock FindPartner(int x, int y, bool female) { return this; }

        protected override void Walk() { }

        protected override void GetNewChild() { }
    }

    [TestClass()]
    public class AnimalUpdateTests
    {
        // Убеждаемся, что за один тик игры Animal обновляется один раз
        // Нельзя допустить, чтобы за один такт игрового времени объекты обновлялись несколько раз
        [TestMethod()]
        public void UpdateMap_OneAnimal_UpdatedOnce()
        {
            var map = new Map(20, 200);

            var animalMock = new AnimalMock(1, 2, map);

            map._createdAnimals.Add(animalMock);

            var gameLogic = new GameLogic(map);
            gameLogic.Update_map();

            Assert.AreEqual(animalMock.CounterUpdateCalls, 1);
        }

        // Убеждаемся, что за один тик игры у пяти животных происходит суммарно пять обновлений 
        [TestMethod()]
        public void UpdateMap_FiveAnimals_UpdatedFiveTimes()
        {
            var map = new Map(20, 200);

            var animalMocksList = new List<AnimalMock>() {
                new AnimalMock(1, 2, map),
                new AnimalMock(2, 2, map),
                new AnimalMock(3, 2, map),
                new AnimalMock(4, 2, map),
                new AnimalMock(5, 2, map),
            };

            map._createdAnimals.AddRange(animalMocksList);

            var gameLogic = new GameLogic(map);
            gameLogic.Update_map();

            int totalUpdates = animalMocksList.Select(x => x.CounterUpdateCalls).Sum();

            Assert.AreEqual(totalUpdates, 5);
        }

        // Животное с изначальным временем жизни = 0 должно вызвать метод Die 0 раз
        [TestMethod()]
        public void UpdateMap_OneAnimalInitialLifeTime0_DidNotDie()
        {
            var map = new Map(20, 200);

            var animalMock = new AnimalMock(1, 2, map);

            map._createdAnimals.Add(animalMock);

            var gameLogic = new GameLogic(map);
            gameLogic.Update_map();

            Assert.AreEqual(animalMock.CounterDieCalls, 0);
        }

        // Животное с изначальным временем жизни = 1000 должно вызвать метод Die 1 раз
        [TestMethod()]
        public void UpdateMap_OneAnimalInitialLifeTimeMax_Dead()
        {
            var map = new Map(20, 200);

            var animalMock = new AnimalMock(1, 2, map, LocalConstants.ANIMAL_LIFE_LIMIT);

            map._createdAnimals.Add(animalMock);

            var gameLogic = new GameLogic(map);
            gameLogic.Update_map();

            Assert.AreEqual(animalMock.CounterDieCalls, 1);
        }

        // Животное с изначальным временем жизни = 999 должно вызвать метод Die 1 раз
        // (так как метод Update должен увеличивать время жизни на 1)
        [TestMethod()]
        public void UpdateMap_OneAnimalInitialLifeTimeMaxMinusOne_Dead()
        {
            var map = new Map(20, 200);

            var animalMock = new AnimalMock(1, 2, map, LocalConstants.ANIMAL_LIFE_LIMIT - 1);

            map._createdAnimals.Add(animalMock);

            var gameLogic = new GameLogic(map);
            gameLogic.Update_map();

            Assert.AreEqual(animalMock.CounterDieCalls, 1);
        }

        // Животное с изначальным временем жизни = 998 должно вызвать метод Die 0 раз
        [TestMethod()]
        public void UpdateMap_OneAnimalInitialLifeTimeMaxMinusTwo_DidNotDie()
        {
            var map = new Map(20, 200);

            var animalMock = new AnimalMock(1, 2, map, LocalConstants.ANIMAL_LIFE_LIMIT - 2);

            map._createdAnimals.Add(animalMock);

            var gameLogic = new GameLogic(map);
            gameLogic.Update_map();

            Assert.AreEqual(animalMock.CounterDieCalls, 0);
        }

        [TestMethod()]
        public void UpdateMap_FiveAnimalsInitialLifeTimeMaxMidMidZeroMax_DiedTwice()
        {
            var map = new Map(20, 200);

            var animalMocksList = new List<AnimalMock>() {
                new AnimalMock(1, 2, map, LocalConstants.ANIMAL_LIFE_LIMIT),
                new AnimalMock(2, 2, map, LocalConstants.ANIMAL_LIFE_LIMIT / 2),
                new AnimalMock(3, 2, map, LocalConstants.ANIMAL_LIFE_LIMIT / 2),
                new AnimalMock(4, 2, map, 0),
                new AnimalMock(5, 2, map, LocalConstants.ANIMAL_LIFE_LIMIT),
            };

            map._createdAnimals.AddRange(animalMocksList);

            var gameLogic = new GameLogic(map);
            gameLogic.Update_map();

            int totalDeaths = animalMocksList.Select(x => x.CounterDieCalls).Sum();

            Assert.AreEqual(totalDeaths, 2);
        }

        [TestMethod()]
        public void UpdateMap_OneAnimalHp0_Dead()
        {
            var map = new Map(20, 200);

            var animalMock = new AnimalMock(1, 2, map);
            animalMock._hp = 0;

            map._createdAnimals.Add(animalMock);

            var gameLogic = new GameLogic(map);
            gameLogic.Update_map();

            Assert.AreEqual(animalMock.CounterDieCalls, 1);
        }

        // Животное с изначальным временем жизни = 1000 и Hp = 0 должно вызвать метод Die только один раз
        [TestMethod()]
        public void UpdateMap_OneAnimalInitialLifeTimeMaxHp0_DeadOnce()
        {
            var map = new Map(20, 200);

            var animalMock = new AnimalMock(1, 2, map, LocalConstants.ANIMAL_LIFE_LIMIT);
            animalMock._hp = 0;

            map._createdAnimals.Add(animalMock);

            var gameLogic = new GameLogic(map);
            gameLogic.Update_map();

            Assert.AreEqual(animalMock.CounterDieCalls, 1);
        }

        // Тестируем неадекватные варианты. Изначальное время жизни = -124124, Hp = -345345
        [TestMethod()]
        public void UpdateMap_OneAnimalInitialLifeTimeNegativeHpNegative_DidNotDead()
        {
            var map = new Map(20, 200);

            var animalMock = new AnimalMock(1, 2, map, -124124);
            animalMock._hp = -345345;

            map._createdAnimals.Add(animalMock);

            var gameLogic = new GameLogic(map);
            gameLogic.Update_map();

            Assert.AreEqual(animalMock.CounterDieCalls, 0);
        }

        // 100 животных должны с дефолтным временем жизни и hp не должны умереть за первый игровой тик
        [TestMethod()]
        public void UpdateMap_100AnimalDefaultInitialLifeDefaultHp_DeadZero()
        {
            var map = new Map(20, 200);

            var animalMocks = new List<AnimalMock>() { };

            for (var i = 0; i < 100; i++)
            {
                animalMocks.Add(new AnimalMock(1, 2, map));
            }

            map._createdAnimals.AddRange(animalMocks);

            var gameLogic = new GameLogic(map);
            gameLogic.Update_map();

            int totalDeaths = animalMocks.Select(x => x.CounterDieCalls).Sum();

            Assert.AreEqual(totalDeaths, 0);
        }
    }
}
