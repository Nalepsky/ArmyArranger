using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmyArranger.Global
{
    class Nation
    {
        string Name;

        public Nation(string name)
        {
            Name = (name != null) ? "'" + name + "'" : "null";
        }
    }
}
