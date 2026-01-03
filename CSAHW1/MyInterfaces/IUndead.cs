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
        int Revive(Entity target);
    }
}
