using System;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StarsWars_IG.Test
{
    [TestClass]
    public class UnitTest1
    {
       
        [TestMethod]
        public void Test_GetAllStarships()
        {
            // arrange
            JsonModel model = new JsonModel();
            var result = model.GetAllStarships(100);
            StringBuilder str = new StringBuilder();
            str.AppendLine("Executor: 40");
            str.AppendLine("Sentinel-class landing craft: 70");
            var expected = str.ToString();
           
            // assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [DataRow(100)]
        public void Test_GetAllStarshipsByInput(int MGLT)
        {
            // arrange
            JsonModel model = new JsonModel();
            var result = model.GetAllStarships(MGLT);
            JsonModel model1 = new JsonModel();
            var expected = model1.GetAllStarships(MGLT);

            // assert
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        [DataRow(100,200)]
        public void Test_GetAllStarshipsByDiffInput(int MGLT,int MGLT1)
        {
            // arrange
            JsonModel model = new JsonModel();
            var result = model.GetAllStarships(MGLT);
            JsonModel model1 = new JsonModel();
            var expected = model1.GetAllStarships(MGLT1);

            // assert
            Assert.AreNotEqual(expected, result);
        }
    }
}
