using ArmyArranger.Global;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmyArranger.Models
{
    class AddUnitOptionModel
    {
        public GameRule EmptyRule = new GameRule();
        public Weapon EmptyWeapon = new Weapon();
        public UnitOption EmptyUnitOption = new UnitOption();

        int unitID;

        public AddUnitOptionModel(int unitID)
        {
            this.unitID = unitID;
        }

    }
}
