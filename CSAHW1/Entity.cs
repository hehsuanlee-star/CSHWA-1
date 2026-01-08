using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CSAHW1
{
    internal abstract class Entity
    { 
        public virtual int id { get; set; }
        public virtual string name { get; set; }
        public virtual int hp { get; set; }
        public virtual int mp { get; set; }
        public virtual int atk { get; set; }

        private static readonly Random rand = new Random();

        //self-referential generic
        public Func<Entity, int> UseAttack;
        public Func<Entity, int> UseSkill;
        public Func<Entity, int, int> TakeDamage;

        //Dice Roll
        public int DiceRoll()
        {
            Console.WriteLine("Press Any Key to Roll Dice.");
            Console.ReadLine();
            int result = rand.Next(1, 7);
            Console.WriteLine($"You Rolled {result}!");
            Console.ReadLine();
            return result;
        }

        public int EnemyRoll(Entity Roller)
        {
            int result = rand.Next(1, 7);
            Console.WriteLine($"{Roller.name} Rolled {result}!");
            return result;
        }

    }
}
