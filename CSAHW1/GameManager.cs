using System;
using System.Collections.Generic;
using CSAHW1.MyMonsters;
using CSAHW1.MyInterfaces;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Metadata.Ecma335;
using System.Reflection.Metadata;

namespace CSAHW1
{
    internal class GameManager
    {
        private static readonly Random rand = new Random();
        private MonsterManager mm = new MonsterManager();
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

        //Menu
        void AttackMenu(Player player, Monster target, out int amount)
        {
            bool validAttack = false;
            amount = 0;
            Console.Clear();
            Console.WriteLine("1) Normal Attack");
            Console.WriteLine("2) Special Attack");

            while (!validAttack)
            {
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        player.SetNormalAttack();
                        amount = player.UseAttack(player);
                        DamageCalculation(player, target, amount);
                        validAttack = true;
                        break;

                    case "2":
                        player.SetSpecialAttack();
                        amount = player.UseAttack(player);
                        DamageCalculation(player, target, amount);
                        validAttack = true;
                        break;

                    default:
                        Console.WriteLine("Invalid Input");
                        Console.ReadLine();
                        break;
                }
            }
        }
        void ShowPlayerStats(Player player)
        {
            Console.WriteLine($"{player.name} has {player.hp} hp and {player.mp} mp!");
        }

        void ShowEnemyStats(Entity enemy)
        {
            Console.WriteLine($"{enemy.name} has {enemy.hp} hp and {enemy.mp} mp!");
        }

        public void DamageCalculation(Entity attacker, Entity target, int amount)
        {
            if (attacker.tag != target.tag)
            {
                target.SetWeaknessDamage();
            }
            else
            { 
                target.SetNormalDamage();
            }
            target.TakeDamage(target, amount);

            // check undead revive
            if (target.tag == "Undead")
            {
                if (target.hp <= 0)
                {
                    target.SetClassSkill();
                    target.UseSkill(target);
                }
            }
        }
        
        // battle loop
        public void PlayerBattleLoop(Player player, Monster target)
        {
            ShowPlayerStats(player);
            ShowEnemyStats(target);
            bool validInput = false;
            Console.WriteLine("1) Attack");
            Console.WriteLine("2) Skill");
            Console.WriteLine("3) Skip");
            int amount;
            while (!validInput)
            {
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        AttackMenu(player, target, out amount);
                        validInput = true;
                        break;

                    case "2":
                        Console.Clear();
                        Console.WriteLine("Used Skill.");
                        player.SetClassSkill();
                        player.UseSkill(player);
                        validInput = true;
                        break;

                    case "3":
                        Console.Clear();
                        Console.WriteLine("Skipped");
                        validInput = true;
                        break;

                    default:
                        Console.WriteLine("Invalid Input");
                        Console.ReadLine();
                        break;
                }
            }
        }

        public void EnemyBattleLoop(Player player, Monster enemy)
        {
            Console.WriteLine("Enemy Turn");
            ShowEnemyStats(enemy);
            int amount;
            string type = enemy.tag;
            switch (type) 
            {
                case "Divine":
                    if (enemy.mp >= 50)
                    {
                        if (enemy.hp <= 10)
                        {
                            enemy.SetClassSkill();
                            enemy.UseSkill(enemy);
                        }
                        else
                        {
                            enemy.SetSpecialAttack();
                            amount = enemy.UseAttack(enemy);
                            DamageCalculation(enemy, player, amount);
                        }

                    }
                    else
                    {
                        enemy.SetNormalAttack();
                        amount = enemy.UseAttack(enemy);
                        DamageCalculation(enemy, player, amount);
                    }
                    break;
                case "Undead":
                    if (enemy.mp >= 50)
                    {
                            enemy.SetSpecialAttack();
                            amount = enemy.UseAttack(enemy);
                    }
                    else
                    {
                        enemy.SetNormalAttack();
                        amount = enemy.UseAttack(enemy);
                    }
                    DamageCalculation(enemy, player, amount);
                    break;
            }
            ShowPlayerStats(player);
            ShowEnemyStats(enemy);
        }
        public void StartBattle(Player player)
        {
            while (mm.LiveMonsters.Count > 0)
            {
                player.hp = player.maxHP;
                player.mp = player.maxMP;
                Monster enemy = mm.GenerateMonster(mm);
                Console.WriteLine($"Encountered {enemy.name}. Ready to Fight.");

                while (player.hp > 0 && enemy.hp > 0)
                {
                    Entity winner = DiceCompare(player, enemy);

                    if (winner == player)
                    {
                        Console.WriteLine("Player Turn");
                        PlayerBattleLoop(player, enemy);
                    }
                    else
                    {
                        EnemyBattleLoop(player, enemy);
                    }
                }

                if (player.hp <= 0)
                {
                    player.hp = 0;
                    Console.WriteLine("You Died.");
                    return; // End battle system entirely
                }

                Console.WriteLine("Enemy Slain");
                mm.MonsterOnDeath(enemy,mm);
            }
            Console.WriteLine("All monsters defeated!");
        }

        public Entity DiceCompare(Player player, Monster enemy)
        {
            int a = 0;
            int b = 0;
            Entity winner = player;
            while (a == b)
            {
                Console.WriteLine("\nRoll Dice to Determine Turn");
                bool validInput = false;
                a = player.DiceRoll();
                b = enemy.EnemyRoll(enemy);
                while (a == b)
                {
                    Console.WriteLine("No Winner. Redo Dice Roll.");
                    a = player.DiceRoll();
                    b = enemy.EnemyRoll(enemy);
                }

                // Check Dice Altering
                while (validInput == false)
                {
                    Console.WriteLine("Use Skill to Alter Result? Y or N");
                    string input = Console.ReadLine().ToLower();
                    switch (input)
                    {
                        case "y":
                            player.SetRollSkill();
                            a = player.UseSkill(player);
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
                int activationSeed = rand.Next(1, 100);
                if (enemy.mp >= 50 && a > b && activationSeed > 25)
                {
                    enemy.SetRollSkill();
                    b = enemy.UseSkill(enemy);
                    Console.WriteLine("Enemy Used Skill.");
                    Console.WriteLine($"{enemy.name} Rolled {b}");
                }

                // Calculate Result
                if (a > b)
                {
                    winner = player;
                    Console.WriteLine($"{winner.name} Wins");
                    return winner;
                }
                else if (a < b)
                {
                    winner = enemy;
                    Console.WriteLine($"{winner.name} Wins");
                    return winner;
                }
                else
                {
                    Console.WriteLine("No Winner. Roll Recommence.");
                }
            }
            throw new InvalidOperationException("DiceCompare exited without determining a winner.");
        }
    }
}
