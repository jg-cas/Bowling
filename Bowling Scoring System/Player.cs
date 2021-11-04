using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bowling_Scoring_System
{
    //Player Entity
    public class Player
    {
        //Constructor
        public Player(string name, int age, int id)
        {
            Name = name;
            Age = age;
            ID = id;
        }

        //Auto Properties
        public int ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int Score { get; set; }
        public int currentThrow { get; set; }
        public List<int> Throws { get; set; }
        public List<int> Frames { get; set; }
    }
}
