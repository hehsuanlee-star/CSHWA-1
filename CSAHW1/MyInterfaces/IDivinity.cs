using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSAHW1.MyInterfaces
{
    internal interface IDivinity
    {
        public string tag { get; set; }
        public string damageType { get; set; }
        int DivineHeal(Entity target);
        public int DivineIntervention(Entity target);
    }
}
