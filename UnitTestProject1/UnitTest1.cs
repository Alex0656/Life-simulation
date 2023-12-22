using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using New_new;

namespace UnitTestProject1
{
    [TestClass()]
    public class InventoryTests
    {
        [TestMethod()]
        public void sum_2Plus2Plus2_TrueReturned()
        {
            Inventory Food = new Inventory();
            Food._meat = 2;
            Food._fruit = 2;
            Food._plant = 2;

            bool actual = Food.sum();
            Assert.AreEqual(true, actual);
        }

        [TestMethod()]
        public void sum_0Plus0Plus0_FalseReturned()
        {
            Inventory Food = new Inventory();
            Food._meat = 0;
            Food._fruit = 0;
            Food._plant = 0;

            bool actual = Food.sum();
            Assert.AreEqual(false, actual);
        }

        [TestMethod()]
        public void sum_Minus5Plus0Plus0_FalseReturned()
        {
            Inventory Food = new Inventory();
            Food._meat = -5;
            Food._fruit = 0;
            Food._plant = 0;

            bool actual = Food.sum();
            Assert.AreEqual(false, actual);
        }

        [TestMethod()]
        public void sum_1Plus1Plus1_NotNull()
        {
            Inventory Food = new Inventory();
            Food._meat = 1;
            Food._fruit = 1;
            Food._plant = 1;

            bool actual = Food.sum();
            Assert.IsNotNull(actual);
        }


        [TestMethod()]
        public void Limit_2Plus2Plus2_TrueReturned()
        {
            Inventory Food = new Inventory();
            Food._meat = 2;
            Food._fruit = 2;
            Food._plant = 2;
            Food._inventory_limits = 15;

            bool actual = Food.Limit();
            Assert.AreEqual(true, actual);
        }

        [TestMethod()]
        public void Limit_0Plus0Plus0_TrueReturned()
        {
            Inventory Food = new Inventory();
            Food._meat = 0;
            Food._fruit = 0;
            Food._plant = 0;
            Food._inventory_limits = 15;

            bool actual = Food.Limit();
            Assert.AreEqual(true, actual);
        }

        [TestMethod()]
        public void Limit_5Plus5Plus5_TrueReturned()
        {
            Inventory Food = new Inventory();
            Food._meat = 5;
            Food._fruit = 5;
            Food._plant = 5;
            Food._inventory_limits = 15;

            bool actual = Food.Limit();
            Assert.AreEqual(true, actual);
        }

        [TestMethod()]
        public void Limit_5Plus5Plus6_FalseReturned()
        {
            Inventory Food = new Inventory();
            Food._meat = 5;
            Food._fruit = 5;
            Food._plant = 6;
            Food._inventory_limits = 15;

            bool actual = Food.Limit();
            Assert.AreEqual(false, actual);
        }

        [TestMethod()]
        public void Limit_500Plus500Plus600_FalseReturned()
        {
            Inventory Food = new Inventory();
            Food._meat = 500;
            Food._fruit = 500;
            Food._plant = 600;
            Food._inventory_limits = 15;

            bool actual = Food.Limit();
            Assert.AreEqual(false, actual);
        }


        [TestMethod()]
        public void Limit_1Plus1Plus1_NotNull()
        {
            Inventory Food = new Inventory();
            Food._meat = 1;
            Food._fruit = 1;
            Food._plant = 1;
            Food._inventory_limits = 15;

            bool actual = Food.Limit();
            Assert.IsNotNull(actual);
        }

        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        [TestMethod()]
        public void Limit_WhenAmountIsLessThanZero_ShouldThrowArgumentOutOfRange()
        {
            Inventory Food = new Inventory();
            Food._meat = -5;
            Food._fruit = 0;
            Food._plant = 0;
            Food._inventory_limits = 15;

            bool actual = Food.Limit();
        }
    }
}
