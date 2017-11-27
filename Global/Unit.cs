using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmyArranger.Global
{   

    class Unit
    {
        string Name;
        string Type;
        string Composition;
        string Experience;
        string EquipmentDescription;
        string DamageValue;
        string BasePoints;
        string NationID;

        public Unit(string name, string type, string composition, string experience, string equipmentDescription, string damageValue, string basePoints, string nationID)
        {
            Name = (name != null) ? "'" + name + "'" : "null";
            Type = (type != null) ? "'" + type + "'" : "null";
            Composition = (composition != null) ? "'" + composition + "'" : "null";
            Experience = (experience != null) ? "'" + experience + "'" : "null";
            EquipmentDescription = (equipmentDescription != null) ? "'" + equipmentDescription + "'" : "null";
            DamageValue = (damageValue != null) ? "'" + damageValue + "'" : "null";
            BasePoints = (basePoints != null) ? "'" + basePoints + "'" : "null";
            NationID = (nationID != null) ? "'" + nationID + "'" : "null";
        }
    }
}
