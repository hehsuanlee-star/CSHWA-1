using CSAHW1.MyInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CSAHW1
{
    internal abstract class Player : Entity
    {
        private string _name;
        private string _tag;
        private int _hp;
        private int _mp;
        private int _atk;
        public int maxHP = 100;
        public int maxMP = 100;
        private int baseATK;
        public override string name { get => _name; }
        public override string tag { get => _tag; set => _tag = value; }
        public override int hp { get => _hp; set => _hp = Math.Max(0, Math.Min(value, maxHP)); }
        public override int mp { get => _mp; set => _mp = Math.Max(0, Math.Min(value, maxMP)); }
        public override int atk { get => _atk; set => _atk = value; }
    }

    internal class Paladin : Player, IDivinity
    {
        private string _name;
        private string _tag;
        private int _hp;
        private int _mp;
        private int _atk;
        private int baseATK = 20;
        public override string name { get => _name; }
        public override string tag { get => _tag; set => _tag = value; }
        public override int hp { get => _hp; set => _hp = Math.Max(0, Math.Min(value, maxHP)); }
        public override int mp { get => _mp; set => _mp = Math.Max(0, Math.Min(value, maxMP)); }
        public override int atk { get => _atk; set => _atk = value; }

        // Constructor
        public Paladin()
        {
            // intialization
            _hp = maxHP;
            _mp = maxMP;
            _atk = baseATK;
            _name = "Paladin";
            _tag = "Divine";
        }

        // Class-Specific Methods
        // Attack Methods
        public int NormalAttack(Entity Attacker)
        {
            int amount = atk;
            if (amount < 0) { amount = 0; }
            UseAttack -= NormalAttack;
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
        // Skills

        public override void SetClassSkill()
        {
            UseSkill += DivineHeal;
        }
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

        public override void SetRollSkill()
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

    internal class DemonicKnight : Player, IUndead
    {
        private string _name;
        private string _tag;
        private int _hp;
        private int _mp;
        private int _atk;
        private int baseATK = 20;
        public bool hasRevived { get; set; } = false;
        public override string name { get => _name; }
        public override string tag { get => _tag; set => _tag = value; }
        public override int hp { get => _hp; set => _hp = Math.Max(0, Math.Min(value, maxHP)); }
        public override int mp { get => _mp; set => _mp = Math.Max(0, Math.Min(value, maxMP)); }
        public override int atk { get => _atk; set => _atk = value; }

        // Constructor
        public DemonicKnight()
        {
            // intialization
            _hp = maxHP;
            _mp = maxMP;
            _atk = baseATK;
            _name = "DemonicKnight";
            _tag = "Undead";
        }

        public override void SetNormalAttack()
        {
            UseAttack += NormalAttack;
        }

        public override void SetSpecialAttack()
        {
            UseAttack += NecroAttack;
        }
        // Class-Specific Methods
        // Attack Methods
        public int NormalAttack(Entity Attacker)
        {
            int amount = atk;
            if (amount < 0) { amount = 0; }
            UseAttack -= NormalAttack;
            Console.WriteLine($"{Attacker.name} Used Normal Attack!");
            return amount;
        }

        public int NecroAttack(Entity Attacker)
        {
            UseAttack -= NecroAttack;
            if (mp >= 50)
            {
                int necroDamage = 5;
                int amount = atk + necroDamage;
                if (amount < 0) { amount = 0; }
                Console.WriteLine($"{Attacker.name} Used Necro Attack!");
                mp -= 50;
                return amount;
            }
            else
            {
                Console.WriteLine("Not Enough MP!");
                return 0;
            }
        }

        //Skills
        public int NecroRevive(Entity target)
        {
            UseSkill -= NecroRevive;
            if (hasRevived == false)
            {
                if (target.hp <= 0)
                {
                    target.hp = maxHP;
                    Console.WriteLine($"{target.name} has revived with {target.hp} hp");
                    hasRevived = true;
                }
                else
                {
                    Console.WriteLine("Revive cannot be used on live entities.");
                }
            }
            else
            {
                target.hp = 0;
                Console.WriteLine("Already Revived Once. Cannot Revive Again.");
            }
            return target.hp;
        }

        public override void SetClassSkill()
        {
            UseSkill += NecroRevive;
        }
        public override void SetRollSkill()
        {
            UseSkill += NecroticRoll;
        }
        public int NecroticRoll(Entity target)
        {
            UseSkill -= NecroticRoll;
            Random rand = new Random();
            int amount = rand.Next(1, 6);
            Console.WriteLine("Used Necrotic Roll.");
            return amount;
        }
    }
}
