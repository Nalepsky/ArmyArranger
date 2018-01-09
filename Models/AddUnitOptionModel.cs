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

        int unitID;

        public AddUnitOptionModel(int unitID)
        {
            this.unitID = unitID;
        }

        public ObservableCollection<UnitOption> LoadOptions()
        {
            SQLiteDataReader result = Database.ExecuteCommand("SELECT * FROM Option WHERE UnitID = " + unitID);

            String Description;
            int id,
                Cost,
                Count,
                WeaponID,
                RuleID,
                UnitID;

            ObservableCollection<UnitOption> Options = new ObservableCollection<UnitOption>();

            while (result.Read())
            {
                id = result.GetInt32(0);
                Description = result.GetString(1);
                Cost = result.GetInt32(2);
                Count = result.GetInt32(3);
                WeaponID = result.GetInt32(4);
                RuleID = result.GetInt32(4);
                UnitID = result.GetInt32(6);

                Options.Add(new UnitOption(id, Description, Count, Cost, RuleID, WeaponID, UnitID));
            }

            return Options;
        }
    }
}
