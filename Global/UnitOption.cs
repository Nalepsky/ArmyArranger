using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmyArranger.Global
{
    class UnitOption
    {
        private int id;
        private String Describtion;
        private int maxNumber;
        private int cost;
        private int ruleId;
        private int weaponId;
        private int unitID;

        public UnitOption(int id, string describtion, int maxNumber, int cost, int ruleId, int weaponId, int unitID)
        {
            this.id = id;
            this.Describtion = describtion;
            this.maxNumber = maxNumber;
            this.cost = cost;
            this.ruleId = ruleId;
            this.weaponId = weaponId;
            this.unitID = unitID;
        }
    }
}
