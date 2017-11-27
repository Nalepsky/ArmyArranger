using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmyArranger.Global
{
    class Option
    {
        string Description;
        string Cost;
        string Count;
        string WeaponID;
        string UnitID;

        public Option(string description, string cost, string count, string weaponID, string unitID)
        {
            Description = (description != null) ? "'" + description + "'" : "null";
            Cost = (cost != null) ? "'" + cost + "'" : "null";
            Count = (count != null) ? "'" + count + "'" : "null";
            WeaponID = (weaponID != null) ? "'" + weaponID + "'" : "null";
            UnitID = (unitID != null) ? "'" + unitID + "'" : "null";
        }
    }
}
