using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSAHW1.MyInterfaces
{
    internal interface IDivinity
    {
        string tag { get; set; }
        int DivineHeal(Entity target);
    }
}
