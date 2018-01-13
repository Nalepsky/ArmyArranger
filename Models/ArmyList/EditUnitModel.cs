using ArmyArranger.Global;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmyArranger.Models.ArmyList
{
    class EditUnitModel
    {
        private ObservableCollection<int> UsedRulesID = new ObservableCollection<int>();

        public String GetWeaponsRules(int weaponID)
        {
            SQLiteDataReader result = Database.ExecuteCommand("SELECT RuleID FROM Rule_Weapon WHERE WeaponID =" + weaponID);
            String temp = "";

            while (result.Read())
            {
                temp = GetRules(result.GetInt32(0));

                Console.WriteLine(temp);
            }
            result.Close();
            return temp;
        }

        public String GetRules(int ruleID)
        {
            if (!UsedRulesID.Contains(ruleID))
            {
                SQLiteDataReader result = Database.ExecuteCommand("SELECT * FROM Rule WHERE ID =" + ruleID);
                String Name = "", Description = "", Source = "";

                while (result.Read())
                {
                    Name = result.GetString(1);
                    Description = result.GetString(2);
                    Source = result.GetString(4);
                }
                result.Close();
                return Name + "(" + Source + ")\n" + Description;
            }
            return "";
        }
    }
}
