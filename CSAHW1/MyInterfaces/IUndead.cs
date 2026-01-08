using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSAHW1.MyInterfaces
{
    internal interface IUndead
    {
        string tag { get; set; }
        public string damageType { get; set; }
        int NecroRevive(Entity target);
        int NecroticRoll(Entity target);
    }
}
