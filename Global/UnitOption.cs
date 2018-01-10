using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmyArranger.Global
{
    class UnitOption
    {
        public static ObservableCollection<UnitOption> UnitOptionsCollection = new ObservableCollection<UnitOption>();

        public int id { get; set; }
        public String Describtion { get; set; }
        public int maxNumber { get; set; }
        public int cost { get; set; }
        public int ruleId { get; set; }
        public int weaponId { get; set; }
        public int unitID { get; set; }

        public UnitOption(int id, string describtion, int maxNumber, int cost, int weaponId, int ruleId, int unitID)
        {
            this.id = id;
            this.Describtion = describtion;
            this.maxNumber = maxNumber;
            this.cost = cost;
            this.unitID = unitID;
            this.weaponId = weaponId;
            this.ruleId = ruleId;             
        }

        public UnitOption()
        {
        }

        public void clearCollection()
        {
            UnitOptionsCollection.Clear();
        }

        public void CreateNewAndSaveToDB(string describtion, int maxNumber, int cost, int weaponID, int ruleID, int unitID,  bool WeaponOrRule)
        {
            int id;
            //if WeaponOrRule == true, save WeaponID, else save RuleID
            string sql_desctibtion = (String.IsNullOrWhiteSpace(describtion)) ? "null" : "'" + describtion + "'";
            string sql_maxNumber = (maxNumber < 1) ? "1" : "'" + maxNumber + "'";
            string sql_cost = "'" + cost + "'";
            string sql_unitID =  "'" + unitID + "'";
            string sql_weaponID =  "'" + weaponID + "'";            
            string sql_ruleID = "'" + ruleID + "'";

            try
            {
                Database.ExecuteCommand("INSERT INTO Option (Description, Cost, Count, WeaponID, RuleID, UnitID) " +
                    "VALUES (" + sql_desctibtion + "," + sql_cost + "," + sql_maxNumber + "," + sql_weaponID + "," + sql_ruleID + "," + sql_unitID + ")");
                id = Database.GetLastInsertedID();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            new UnitOption(id, describtion, maxNumber, cost, weaponID, ruleId, unitID);
        }

        public void LoadAll(int unit_id)
        {
            SQLiteDataReader result = Database.ExecuteCommand("SELECT * FROM Option WHERE UnitID = " + unit_id);

            String Description;
            int id,
                Cost,
                Count,
                WeaponID,
                RuleID,
                UnitID;

            while (result.Read())
            {             
                id = result.GetInt32(0);
                Description = result.GetString(1);
                Cost = result.GetInt32(2);
                Count = result.GetInt32(3);
                WeaponID = result.GetInt32(4);
                RuleID = result.GetInt32(4);
                UnitID = result.GetInt32(6);
                
                UnitOptionsCollection.Add(new UnitOption(id, Description, Count, Cost, RuleID, WeaponID, UnitID));
            }
            result.Close();
        }
    }
}
