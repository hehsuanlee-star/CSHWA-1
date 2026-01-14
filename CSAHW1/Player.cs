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
        public virtual void UseRollSkill() { }
        public virtual void UseNormalAttack() { }
        public virtual void UseSpecialAttack() { }

        public virtual void UseClassSkill() { }

    }

    internal class Paladin : Player, IDivinity
    {
        private string _name;
        private string _tag;
        private int _hp;
        private int _mp;
        private int _atk;
        private int maxHP = 100;
        private int maxMP = 100;
        private int baseATK = 10;
        private string _damageType;
        public string damageType { get => _damageType; set => _damageType = value; }
        public override string name { get => _name; }
        public string tag { get => _tag; set => _tag = value; }
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
            _damageType = "Physical";
            int amount = atk;
            if (amount < 0) { amount = 0; }
            UseAttack -= NormalAttack;
            Console.WriteLine($"{Attacker.name}Use Normal Attack!");
            return amount;
        }
        public int DivineStrike(Entity Attacker)
        {
            _damageType = "Divine";
            int amount = atk * 2;
            if (amount < 0) { amount = 0; }
            UseAttack -= DivineStrike;
            Console.WriteLine($"{Attacker.name}Used Divine Strike!");
            return amount;
        }

        public override void UseNormalAttack()
        {
            UseAttack += NormalAttack;
        }

        public override void UseSpecialAttack()
        {
            UseAttack += DivineStrike;
        }
        // Skills

        public override void UseClassSkill()
        {
            UseSkill += DivineHeal;
        }
        public int DivineHeal(Entity target)
        {
            UseSkill -= DivineHeal;
            int amount = 20;
            if (amount < 0) { amount = 0; }
            target.hp += amount;
            return amount;
        }

        public override void UseRollSkill()
        {
            UseSkill += DivineIntervention;
        }
        public int DivineIntervention(Entity target)
        {
            UseSkill -= DivineIntervention;
            int amount = 3;
            return amount;
        }
    }

    internal class DemonicKnight : Player, IUndead
    {
        private string _name;
        private string _tag;
        private string _damageType;
        private int _hp;
        private int _mp;
        private int _atk;
        private int maxHP = 50;
        private int maxMP = 50;
        private int baseATK = 10;
        public override string name { get => _name; }
        public string tag { get => _tag; set => _tag = value; }

        public string damageType { get => _damageType; set => _damageType = value; }
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

        // Class-Specific Methods
        // Attack Methods
        public int NormalAttack(Entity Attacker)
        {
            _damageType = "Physical";
            int amount = atk;
            if (amount < 0) { amount = 0; }
            UseAttack -= NormalAttack;
            Console.WriteLine($"{Attacker.name}Used Normal Attack!");
            return amount;
        }

        public int NecroAttack(Entity target)
        {
            UseAttack -= NecroAttack;
            if (mp >= 50)
            {
                _damageType = "Necrotic";
                int necroDamage = 5;
                int amount = atk + necroDamage;
                if (amount < 0) { amount = 0; }
                target.hp -= amount;
                Console.WriteLine("NecroAttack!");
                return amount;
            }
            else
            {
                Console.WriteLine("\n Not Enough MP!");
                return 0;
            }
        }

        //Skills
        public int NecroRevive(Entity target)
        {
            UseSkill -= NecroRevive;
            if (target.hp <= 0)
            {
                target.hp = maxHP;
                Console.WriteLine($"{target.name} has revived with {hp} hp");
                return target.hp;
            }
            else
            {
                Console.WriteLine("Revive cannot be used on live entities.");
                return target.hp;
            }
        }

        public override void UseClassSkill()
        {
            UseSkill += NecroRevive;
        }
        public override void UseRollSkill()
        {
            UseSkill += NecroticRoll;
        }
        public int NecroticRoll(Entity target)
        {
            UseSkill -= NecroticRoll;

            if (mp >= 50)
            {
                Random rand = new Random();
                int amount = rand.Next(1, 6);
                return amount;
            }
            else
            {
                Console.WriteLine("\n Not Enough MP!");
                return 0;
            }

        }
    }
}
