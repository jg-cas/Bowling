using Bowling_Scoring_System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestPerfectGame()
        {
            var player = new Player("test", 1, 1);
            int[] scores = { 10, 0, 10, 0, 10, 0, 10, 0, 10, 0, 10, 0, 10, 0, 10, 0, 10, 0, 10, 10, 10 };
            player.Throws = new List<int>(scores);
            var players = new List<Player>();
            players.Add(player);
            // Create an instance to test:
            var test = new Game(players);
            // Define a test output value:
            var expectedResult = player.Score == 300;
            // Run the method under test:
            bool actualResult = false;
            foreach (var score in test.GetScore())
            {
                actualResult = score.Score == 300;
            }
            // Verify the result:
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestGutterGame()
        {
            var player = new Player("test", 1, 1);
            int[] scores = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            player.Throws = new List<int>(scores);
            var players = new List<Player>();
            players.Add(player);
            // Create an instance to test:
            var test = new Game(players);
            // Define a test output value:
            var expectedResult = player.Score == 0;
            // Run the method under test:
            bool actualResult = false;
            foreach (var score in test.GetScore())
            {
                actualResult = score.Score == 0;
            }
            // Verify the result:
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestOnesGame()
        {
            var player = new Player("test", 1, 1);
            int[] scores = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0 };
            player.Throws = new List<int>(scores);
            var players = new List<Player>();
            players.Add(player);
            // Create an instance to test:
            var test = new Game(players);
            // Define a test output value:
            var expectedResult = player.Score == 20;
            // Run the method under test:
            bool actualResult = false;
            foreach (var score in test.GetScore())
            {
                actualResult = score.Score == 20;
            }
            // Verify the result:
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestRandomGame()
        {
            var player = new Player("test", 1, 1);
            int[] scores = { 0, 1, 2, 3, 4, 5, 6, 4, 7, 3, 8, 2, 9, 1, 10, 0, 0, 0, 10, 0, 0 };
            player.Throws = new List<int>(scores);
            var players = new List<Player>();
            players.Add(player);
            // Create an instance to test:
            var test = new Game(players);
            // Define a test output value:
            var expectedResult = player.Score == 109;
            // Run the method under test:
            bool actualResult = false;
            foreach (var score in test.GetScore())
            {
                actualResult = score.Score == 109;
            }
            // Verify the result:
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
