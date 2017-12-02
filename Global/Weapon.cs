using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ArmyArranger.Global
{
    class Weapon
    {
        string Name;
        string Range;
        string Shots;
        string Penetration;
        string RequiresLoader;
        string Rules;

        public Weapon() { }

        public Weapon(string name, string range, string shots, string penetration, string requiresLoader, string rules)
        {
            Name = (name != null) ? "'" + name + "'" : "null";
            Range = (range != null) ? "'" + range + "'" : "null";
            Shots = (shots != null) ? "'" + shots + "'" : "null";
            Penetration = (penetration != null) ? "'" + penetration + "'" : "null";
            RequiresLoader = (requiresLoader != null) ? "'" + requiresLoader + "'" : "null";
            Rules = (rules != null) ? "'" + rules + "'" : "null";
        }

        public void SaveToDB()
        {
            
        }
    }
}
