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

        public virtual string tag { get; set; }
        public virtual int hp { get; set; }
        public virtual int mp { get; set; }
        public virtual int atk { get; set; }

        public virtual void UseRollSkill() { }
        public virtual void SetNormalAttack() { }
        public virtual void SetSpecialAttack() { }
        public virtual void SetClassSkill() { }

        private static readonly Random rand = new Random();

        //self-referential generic
        public Func<Entity, int> UseAttack;
        public Func<Entity, int> UseSkill;
        public Func<Entity, int, int> TakeDamage;

        // Take Damage Methods

        public void SetWeaknessDamage()
        {
            Console.WriteLine("Weakness Detected. Take Double Damage");
            TakeDamage += TakeDoubleDamage;
        }

        public void SetNormalDamage()
        {
            TakeDamage += TakeNormalDamage;
        }

        public int TakeNormalDamage(Entity target, int amount)
        {
            target.hp -= amount;
            if (target.hp < 0)
            {
                target.hp = 0;
            }
            TakeDamage -= TakeNormalDamage;
            Console.WriteLine($"{target.name} take {amount} damage!");
            Console.WriteLine($"{target.name} has {target.hp} hp left!");
            return target.hp;
        }

        public int TakeDoubleDamage(Entity target, int amount)
        {
            int newAmount = amount * 2;
            target.hp -= newAmount;
            if (target.hp < 0)
            {
                target.hp = 0;
            }
            TakeDamage -= TakeDoubleDamage;
            Console.WriteLine($"{target.name} take {newAmount} damage!");
            Console.WriteLine($"{target.name} has {target.hp} hp left!");
            return target.hp;
        }

        //Dice Roll
        public int DiceRoll()
        {
            Console.WriteLine("Press Any Key to Roll Dice.");
            Console.ReadLine();
            int result = rand.Next(1, 7);
            Console.WriteLine($"You Rolled {result}!");
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
