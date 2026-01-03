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

        //self-referential generic
        public Func<Entity, int> Act;

        // Polymorphic attack method
        public virtual int NormalAttack(Entity target)
        {
            if (Act != null)
            {
                int amount;
                amount = Act(target);
                return amount;
            }
            else
            {
                Console.WriteLine($"no action assigned!");
                return 0;
            }
        }
        

    }
}
