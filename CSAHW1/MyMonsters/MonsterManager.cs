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
        public virtual void UseRollSkill() { }
        public virtual void UseNormalAttack() { }
        public virtual void UseSpecialAttack() {}
    }
    internal class MonsterManager
    { 
        
    }
}
