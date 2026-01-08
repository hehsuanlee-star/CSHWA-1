using System;
using System.Collections.Generic;
using CSAHW1.MyMonsters;
using CSAHW1.MyInterfaces;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CSAHW1
{
    internal class GameManager
    {
        public Player player { get => _player ; set => _player = value; }
        private Player _player; 
        //init
        public GameManager ()
        {
            _player = GeneratePlayer ();
        }

        //Character Selection
        public Player GeneratePlayer()
        {
            Player player = null;
            int CharacterSelect()
            {
                int output = 0;
                while (output == 0)
                {
                    Console.WriteLine("Please Select Your Class!");
                    Console.WriteLine("1) Demonic Knight " + "2) Paladin ");
                    string Input = Console.ReadLine();
                    switch (Input)
                    {
                        case "1":
                            output = 1;
                            break;
                        case "2":
                            output = 2;
                            break;
                        default:
                            Console.WriteLine("Unknown Input. Please type in 1 or 2.");
                            break;
                    }
                }
                return output;
            }
            int classID;  
            classID = CharacterSelect();
            switch (classID)
            {
                case 1:
                    player = new DemonicKnight();
                    break;
                case 2:
                    player = new Paladin();
                    break;
                default:
                    throw new InvalidOperationException("Invalid classID: " + classID);
            }
            Console.WriteLine($"You have selected {player.name}!");
            return player;
        }

        //DiceRoll

        //Menu
        public void ShowBattleMenu()
        { 
        }
        //BattleLoop
        public void StartBattle(ref Player Player, ref Monster Enemy)
        {
            DiceCompare(Player, Enemy);
        }

        public Entity DiceCompare(Player Player, Monster Enemy)
        {
            int a = 0;
            int b = 0;
            bool validInput = false;
            Entity winner;
            while (a == b)
                {
                    Console.WriteLine("Commence Dice Roll.");
                    a = Player.DiceRoll();
                    b = Enemy.EnemyRoll(Enemy);
                    if (a == b)
                    {
                        Console.WriteLine("No Winner. Redo Dice Roll.");
                        Console.ReadLine();
                        Console.Clear();
                    }
                }

            // Check Dice Altering
            while (validInput == false)
            {
                Console.WriteLine("Use Skill to Alter Result? Y or N");
                string input = Console.ReadLine().ToLower();
                switch (input)
                {
                    case "y":
                        Player.UseRollSkill();
                        a = Player.UseSkill(Player);
                        Console.WriteLine($"You Rolled {a}");
                        validInput = true;
                        break;
                    case "n":
                        validInput = true;
                        break;
                    default:
                        Console.WriteLine("Invalid input. Please type Y or N.");
                        break;
                }
            }

            // Check Enemy Dice Altering
            Random rand = new Random();
            int activationSeed = rand.Next(1, 100);
            if (Enemy.mp >= 50 && a>b && activationSeed > 25)
            {
                Enemy.UseRollSkill();
                b = Enemy.UseSkill(Enemy);
                Console.WriteLine("Enemy Used Skill.");
                Console.WriteLine($"{Enemy.name} Rolled {b}");
            }

            // Calculate Result
            if (a > b)
            {
                winner = Player;
            }
            else
            {
                winner = Enemy;
            }
            Console.WriteLine($"{winner.name} wins");
            return winner;
        }
        public void PlayerTurn()
        {
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine("Player Turn:");
            ShowBattleMenu();
        }



    }
}
