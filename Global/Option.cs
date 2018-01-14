using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SQLite;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ArmyArranger.Global
{
    public class Option : INotifyPropertyChanged
    {
        public static ObservableCollection<Option> OptionsCollection = new ObservableCollection<Option>();
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged<T>([CallerMemberName]string caller = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
        }

        public int ID { get; set; }
        private string _description;
        public String Description
        {
            get { return _description; }
            set { _description = value; OnPropertyChanged<Unit>(); }
        }
        public int Count { get; set; }
        public int Cost { get; set; }
        public int RuleID { get; set; }
        public int WeaponID { get; set; }
        public int UnitID { get; set; }

        public Option(int id, string description, int count, int cost, int weaponId, int ruleId, int unitID)
        {
            this.ID = id;
            this.Description = description;
            this.Count = count;
            this.Cost = cost;
            this.UnitID = unitID;
            this.WeaponID = weaponId;
            this.RuleID = ruleId;

            OptionsCollection.Add(this);
        }

        public Option()
        {
        }

        public void ClearCollection()
        {
            OptionsCollection.Clear();
        }

        public void CreateNewAndSaveToDB(string description, int count, int cost, int weaponID, int ruleID, int unitID)
        {
            int id;
            string sql_description = (String.IsNullOrWhiteSpace(description)) ? "null" : "'" + description + "'";
            string sql_cost = (cost == 0) ? "null" : "'" + cost + "'";
            string sql_count = (count < 1) ? "1" : "'" + count + "'";
            string sql_weaponID = (weaponID == 0) ? "null" : "'" + weaponID + "'"; 
            string sql_ruleID = (ruleID == 0) ? "null" : "'" + ruleID + "'";
            string sql_unitID = (unitID == 0) ? "null" : "'" + unitID + "'";

            try
            {
                Database.ExecuteCommand("INSERT INTO Option (Description, Cost, Count, WeaponID, RuleID, UnitID) VALUES (" + sql_description + "," + sql_count + "," + sql_cost + "," + sql_weaponID + "," + sql_ruleID + "," + sql_unitID + ")");
                id = Database.GetLastInsertedID();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            new Option(id, description, count, cost, weaponID, ruleID, unitID);
        }
        public void UpdateInDB(string description, int count, int cost, int weaponID, int ruleID, int unitID)
        {
            string sql_description = (String.IsNullOrWhiteSpace(description)) ? "null" : "'" + description + "'";
            string sql_cost = (cost == 0) ? "null" : "'" + cost + "'";
            string sql_count = (count < 1) ? "1" : "'" + count + "'";
            string sql_weaponID = (weaponID == 0) ? "null" : "'" + weaponID + "'";
            string sql_ruleID = (ruleID == 0) ? "null" : "'" + ruleID + "'";
            string sql_unitID = (unitID == 0) ? "null" : "'" + unitID + "'";

            try
            {
                Database.ExecuteCommand("UPDATE Option SET Description = " + sql_description + ", Cost = " + sql_cost + ", Count = " + sql_count + ", WeaponID = " + sql_weaponID + ", RuleID = " + sql_ruleID + ", UnitID = " + sql_unitID + " WHERE ID = " + ID);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            Description = description;
            Cost = cost;
            Count = count;
            WeaponID = weaponID;
            RuleID = ruleID;
            UnitID = unitID;
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
                
                new Option(ID, Description, Count, Cost, WeaponID, RuleID, UnitID);
            }
            result.Close();
        }

        public void Remove()
        {
            try
            {
                Database.ExecuteCommand("DELETE FROM Option WHERE ID = " + ID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            OptionsCollection.Remove(this);
        }
    }
}
