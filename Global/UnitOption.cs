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

        public int ID { get; set; }
        public String Description { get; set; }
        public int Count { get; set; }
        public int Cost { get; set; }
        public int RuleId { get; set; }
        public int WeaponId { get; set; }
        public int UnitID { get; set; }

        public UnitOption(int id, string description, int count, int cost, int weaponId, int ruleId, int unitID)
        {
            this.ID = id;
            this.Description = description;
            this.Count = count;
            this.Cost = cost;
            this.UnitID = unitID;
            this.WeaponId = weaponId;
            this.RuleId = ruleId;             
        }

        public UnitOption()
        {
        }

        public void ClearCollection()
        {
            UnitOptionsCollection.Clear();
        }

        public void CreateNewAndSaveToDB(string description, int count, int cost, int weaponID, int ruleID, int unitID,  bool WeaponOrRule)
        {
            int id;
            //if WeaponOrRule == true, save WeaponID, else save RuleID
            string sql_desctibtion = (String.IsNullOrWhiteSpace(description)) ? "null" : "'" + description + "'";
            string sql_count = (count < 1) ? "1" : "'" + count + "'";
            string sql_cost = (cost == 0) ? "null" : "'" + cost + "'";
            string sql_unitID = (unitID == 0) ? "null" : "'" + unitID + "'";
            string sql_weaponID = (weaponID == 0) ? "null" : "'" + weaponID + "'";            
            string sql_ruleID = (ruleID == 0) ? "null" : "'" + ruleID + "'";

            try
            {
                Database.ExecuteCommand("INSERT INTO Option (Description, Cost, Count, WeaponID, RuleID, UnitID) " +
                    "VALUES (" + sql_desctibtion + "," + sql_cost + "," + sql_count + "," + sql_weaponID + "," + sql_ruleID + "," + sql_unitID + ")");
                id = Database.GetLastInsertedID();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            new UnitOption(id, description, count, cost, weaponID, RuleId, unitID);
        }

        public void LoadAll(int unit_id)
        {
            SQLiteDataReader result = Database.ExecuteCommand("SELECT * FROM Option WHERE UnitID = " + unit_id);

            String Description;
            int ID,
                Cost,
                Count,
                WeaponID,
                RuleID,
                UnitID;

            while (result.Read())
            {             
                ID = result.GetInt32(0);
                Description = result.GetString(1);
                Cost = (!result.IsDBNull(2)) ? result.GetInt32(2) : 0;
                Count = (!result.IsDBNull(3)) ? result.GetInt32(3) : 0;
                WeaponID = (!result.IsDBNull(4)) ? result.GetInt32(4) : 0;
                RuleID = (!result.IsDBNull(5)) ? result.GetInt32(5) : 0;
                UnitID = (!result.IsDBNull(6)) ? result.GetInt32(6) : 0;
                
                UnitOptionsCollection.Add(new UnitOption(ID, Description, Count, Cost, RuleID, WeaponID, UnitID));
            }
            result.Close();
        }
    }
}
