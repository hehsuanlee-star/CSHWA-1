using CSAHW1.MyInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CSAHW1.MyMonsters
{
    internal abstract class Monster : Entity
    {
    }
    internal class MonsterManager
    {
        public List<Monster> LiveMonsters { get; } = new();
        public List<Monster> DeadMonsters { get; } = new();

        public Monster angel = new Angel();
        public Monster zombie = new Zombie();

        public MonsterManager() 
        { 
            LiveMonsters.Add(angel);
            LiveMonsters.Add(zombie);
        }

        public Monster GenerateMonster(MonsterManager mm)
        {
            if (mm.LiveMonsters.Count == 0)
                throw new InvalidOperationException("No live monsters");

            int index = Random.Shared.Next(mm.LiveMonsters.Count);
            return mm.LiveMonsters[index];
        }

        public void MonsterOnDeath(Monster monster, MonsterManager mm)
        {
            if (mm.LiveMonsters.Remove(monster))
            {
                if (!mm.DeadMonsters.Contains(monster))
                    mm.DeadMonsters.Add(monster);
            }
        }
    }

}
