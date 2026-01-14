using CSAHW1.MyInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSAHW1.MyMonsters
{
    internal class Angel : Monster, IDivinity
    {
        private int _id = 1;
        private string _name;
        private string _tag;
        private int _hp;
        private int _mp;
        private int _atk;
        public int maxHP = 100;
        public int maxMP = 100;
        private int baseATK = 10;
        public override int id { get => _id; }
        public override string name { get => _name; }
        public override string tag { get => _tag; set => _tag = value; }
        public override int hp { get => _hp; set => _hp = Math.Max(0, Math.Min(value, maxHP)); }
        public override int mp { get => _mp; set => _mp = Math.Max(0, Math.Min(value, maxMP)); }
        public override int atk { get => _atk; set => _atk = value; }
       
        // Constructor
        public Angel()
        {
            // intialization
            _hp = maxHP;
            _mp = maxMP;
            _atk = baseATK;
            _name = "Angel";
            _tag = "Divine";
        }

        // Class-Specific Methods
        // Attack Methods
        public int NormalAttack(Entity Attacker)
        {
            UseAttack -= NormalAttack;
            int amount = atk;
            if (amount < 0) { amount = 0; }
            Console.WriteLine($"{Attacker.name} Used Normal Attack!");
            return amount;
        }
        public int DivineStrike(Entity Attacker)
        {
            UseAttack -= DivineStrike;
            if (Attacker.mp >= 50)
            {
                int amount = atk * 2;
                if (amount < 0) { amount = 0; }
                Console.WriteLine($"{Attacker.name} Used Divine Strike!");
                Attacker.mp -= 50;
                return amount;
            }
            else
            { 
                Console.WriteLine("Not enough MP!"); 
                return 0;  
            }
        }
        public override void SetNormalAttack()
        {
            UseAttack += NormalAttack;
        }

        public override void SetSpecialAttack()
        {
            UseAttack += DivineStrike;
        }
        public override void SetClassSkill()
        {
            UseSkill += DivineHeal;
        }
        // Skills
        public int DivineHeal(Entity target)
        {
            UseSkill -= DivineHeal;
            if (target.hp <= 0)
            {
                Console.WriteLine("You Cannot Heal Dead Creatures");
                return 0;
            }
            else if (target.mp >= 50)
            {
                UseSkill -= DivineHeal;
                int amount = 20;
                if (amount < 0) { amount = 0; }
                target.hp += amount;
                target.mp -= 50;
                return amount;
            }
            else
            {
                Console.WriteLine("Not Enough MP");
                return 0;
            }
        }
        public override void UseRollSkill() 
        {
            UseSkill += DivineIntervention;
        }
        public int DivineIntervention(Entity target)
        {
            UseSkill -= DivineIntervention;
            Console.WriteLine("Used Divine Intervention. Set Roll to 3.");
            int amount = 3;
            return amount;
        }
    }
}
