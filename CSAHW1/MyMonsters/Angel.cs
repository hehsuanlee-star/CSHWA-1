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
        private int maxHP = 100;
        private int maxMP = 100;
        private int baseATK = 10;
        public override int id { get => _id; }
        public override string name { get => _name; }
        public string tag { get => _tag; set => _tag = value; }
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
            Act = NormalAttack;
        }

        // Class-Specific Methods
       
        public override int NormalAttack(Entity target)
        {
            int amount = atk;
            if (amount < 0) { amount = 0; }
            target.hp -= amount;
            return amount;
        }
        public int DivineStrike(Entity target)
        {
            int amount = atk * 2;
            if (amount < 0) { amount = 0; }
            target.hp -= amount;
            return amount;
        }

        public int DivineHeal(Entity target)
        {
            int amount = 10;
            if (amount < 0) { amount = 0; }
            target.hp += amount;
            return amount;
        }
    }
}
