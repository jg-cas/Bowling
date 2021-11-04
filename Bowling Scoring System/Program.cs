using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bowling_Scoring_System
{
    class Program
    {
        static void Main(string[] args)
        {
            var program = new Program();
            bool running = true;
            List<Player> results = null;
            while (running)
            {
                Console.WriteLine("Enter Number of Players");
                if (int.TryParse(Console.ReadLine(), out int numberOfPlayers) && numberOfPlayers > 0) //Validation
                {
                        results = program.Run(program.CreatePlayers(numberOfPlayers));
                        running = false;
                }
                else { Console.WriteLine("Enter a valid Number"); }
            }
            foreach (var player in results)
            {
                Console.WriteLine($"Name: {player.Name} Score: {player.Score}");
            };
            Console.WriteLine("Bye!");
            Console.ReadLine();
        }
        Game CreatePlayers(int numberOfPlayers)
        {
            var players = new List<Player>();
            for (int i = 0; i < numberOfPlayers;)
            {
                Console.WriteLine($"Enter Player {i + 1} Name");
                var name = Console.ReadLine();
                Console.WriteLine($"Enter Player {i + 1} Age");
                if (int.TryParse(Console.ReadLine(), out int age) && age <= 100) //Validation
                {
                    int id;
                    try
                    {
                        id = players.Max(e => e.ID) + 1; //Auto Incremented assignment of ID
                    }
                    catch (ArgumentNullException)
                    {
                        id = 0;
                    }
                    catch (InvalidOperationException)
                    {
                        id = 0;
                    }
                    var player = new Player(name, age, id);
                    players.Add(player); //Add player to list
                    i++; //Move to next player only if information is correct
                }
                else { Console.WriteLine("Enter Information Again"); }
            }
            Console.Clear();
            return new Game(players); //Start a new game with added players
        }
        List<Player> Run(Game game)
        {
            var players = game.GetPlayers();
            var pins = new Random(); //Random throws as test case that would be substituted for actual game play entries
            var playing = true;
            while (playing)
            {
                foreach (var player in players)
                {
                    if (player.currentThrow <= 17) //Last 3 throws (last frame) has a different set of rules
                    {
                        var flag = 0;
                        do
                        {
                            int roll;
                            if (flag == 1)
                            {
                                //Number generated can only be between 0 and the remaining number of pins standing
                                roll = game.Throw(player.ID, pins.Next(0, 10 - (player.Throws[player.currentThrow - 1])));
                                flag++;
                            }
                            else
                            {
                                roll = game.Throw(player.ID, pins.Next(0, 10));
                                flag++;
                            }
                            Console.WriteLine($"Player: {player.Name}    Roll#: {player.currentThrow}    Pins: {roll}");
                        } while (flag < 2);
                    }
                    else if (player.currentThrow >= 18 && player.currentThrow <= 20) //Last frame rules
                    {
                        var flag = 0;
                        int maxFlag = 2;
                        bool allowThreeRolls = false;
                        do
                        {
                            int roll = 0;
                            if (allowThreeRolls)
                            {
                                maxFlag = 3;
                            }

                            if (flag == 0)
                            {
                                roll = game.Throw(player.ID, pins.Next(0, 10));
                                //If a strike happens in the first roll then allow for an extra throw otherwise the 21st throw is 0
                                var result = roll == 10 ? allowThreeRolls = true : allowThreeRolls = false;
                                flag++;
                            }
                            //If not a strike on first roll then allow only remaining pins to be knocked down
                            else if (flag == 1 && !allowThreeRolls)
                            {
                                roll = game.Throw(player.ID, pins.Next(0, 10 - (player.Throws[player.currentThrow - 1])));
                                flag++;
                            }
                            //If a strike on first roll then reset all pins
                            else if (flag == 1 && allowThreeRolls)
                            {
                                roll = game.Throw(player.ID, pins.Next(0, 10));
                                flag++;
                            }
                            //Reset all pins if second throw was a strike
                            else if (flag == 2 && player.Throws[player.currentThrow - 1] == 10)
                            {
                                roll = game.Throw(player.ID, pins.Next(0, 10));
                                flag++;
                            }
                            //Only allow for pins left standing to be left
                            else if (flag == 2 && player.Throws[player.currentThrow - 1] != 10)
                            {
                                roll = game.Throw(player.ID, pins.Next(0, 10 - (player.Throws[player.currentThrow - 1])));
                                flag++;
                            }
                            Console.WriteLine($"Player: {player.Name}    Roll#: {player.currentThrow}    Pins: {roll}");
                        } while (flag < maxFlag);
                        if (!allowThreeRolls)
                        {
                            player.Throws[20] = 0;
                            player.currentThrow++;
                        }
                    }
                }
                //When the last player current throw equals 21 end the game
                if (players.Last().currentThrow == 21)
                {
                    playing = false;
                }
            }
            Console.WriteLine("Press enter for total score");
            Console.ReadLine();
            return game.GetScore();
        }
    }
}
