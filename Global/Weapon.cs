using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmyArranger.Global
{
    class Weapon
    {
        string Name;
        string Range;
        string Shots;
        string Penetration;
        string RequiresLoader;

        public Weapon(string name, string range, string shots, string penetration, string requiresLoader)
        {
            Name = (name != null) ? "'" + name + "'" : "null";
            Range = (range != null) ? "'" + range + "'" : "null";
            Shots = (shots != null) ? "'" + shots + "'" : "null";
            Penetration = (penetration != null) ? "'" + penetration + "'" : "null";
            RequiresLoader = (requiresLoader != null) ? "'" + requiresLoader + "'" : "null";

            Name = name;
            Range = range;
            Shots = shots;
            Penetration = penetration;
            RequiresLoader = requiresLoader;
        }
    }
}
