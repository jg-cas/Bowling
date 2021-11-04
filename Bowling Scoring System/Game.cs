using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bowling_Scoring_System
{
    public class Game
    {
        //Declaration of Variables
        List<Player> players;
        int[] arrayThrows = new int[21];
        int[] arrayFrames = new int[10];
        //Constructor for new game
        public Game(IEnumerable<Player> getPlayers)
        {
            players = getPlayers.ToList();
            foreach (var player in players)
            {
                player.Throws = new List<int>(arrayThrows); //21 Throws per player per game
                player.Frames = new List<int>(arrayFrames); //10 Frames per player per game
            }
        }
        public IEnumerable<Player> GetPlayers()
        {
            return players.ToList();
        }
        //Passing a single value for throw
        public int Throw(int id, int knockedPins)
        {
            var player = players.Find(e => e.ID == id);
            player.Throws[player.currentThrow++] = knockedPins; //On current throw assign score then auto increment
            return knockedPins;
        }
        //Getting score for each player
        public List<Player> GetScore()
        {
            foreach (var player in players)
            {
                int score = 0;
                int pointer = 0;
                for (int i = 0; i < player.Frames.Capacity; i++) //Iterating through each frame
                {
                    if (Spare(pointer, player)) //Spare = Current + Following throw equals 10
                    {
                        score += 10 + player.Throws[pointer + 2]; //Score = 10 + next frame first throw
                        pointer += 2;
                    }
                    else if (Strike(pointer, player)) //Strike = Current throw equals 10
                    {
                        try
                        {
                            //Strike = Current + Following frame two shots unless another strike is thrown
                            score += 10 + player.Throws[pointer + 2] + player.Throws[pointer + 3];
                            //If another strike is thrown then strike += 10 = next frame first throw
                            if (player.Throws[pointer + 2] == 10)
                            {
                                score += player.Throws[pointer + 4];
                            }
                            pointer += 2;
                        }
                        //Last frame rules throw out of range exception if all throws are strikes
                        catch (ArgumentOutOfRangeException)
                        {
                            score += 20;
                        }
                    }
                    else //Open Frame = No strike or Spare
                    {
                        score += player.Throws[pointer] + player.Throws[pointer + 1]; //Score = Current plus following throw
                        pointer += 2;
                    }
                }
                player.Score = score;
            }
            return players;
        }
        //Evaluate for spare equal first throw was not a 10, first plus second throw was a 10 and the pointer is at an even position
        private bool Spare(int pointer, Player player) { return player.Throws[pointer] + player.Throws[pointer + 1] == 10 && player.Throws[pointer] != 10 && pointer % 2 == 0; }

        private bool Strike(int pointer, Player player) { return player.Throws[pointer] == 10; }
    }
}
