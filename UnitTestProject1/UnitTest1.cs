using System;
using System.Collections.Generic;
using System.Linq;
using GA_KP;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            List<Item> items = new List<Item>{
                new Item() { Name="1",weight=23,Value=505},
                new Item() { Name="2",weight=26,Value=352},
                new Item() { Name="3",weight=20,Value=458},
                new Item() { Name="4",weight=18,Value=220},
                new Item() { Name="5",weight=32,Value=354},
                new Item() { Name="6",weight=27,Value=414},
                new Item() { Name="7",weight=29,Value=498},
                new Item() { Name="8",weight=26,Value=545},
                new Item() { Name="9",weight=30,Value=473},
                new Item() { Name="10",weight=27,Value=543},
            };

            GAKP kp = new GAKP(items, 67);
            kp.solve(10);
            //kp.Get1stInGenePool().Gene.insideItems.Sum(n => n.Value);
            //Assert.AreEqual(1270, kp.Get1stInGenePool().Gene.insideItems.Sum(n => n.Value));


        }
    }
}
