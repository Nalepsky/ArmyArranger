using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SQLite;
using System.Runtime.CompilerServices;
using System.Windows;

namespace ArmyArranger.Global
{
    public class Unit : INotifyPropertyChanged
    {
        public static ObservableCollection<Unit> UnitsCollection = new ObservableCollection<Unit>();
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged<T>([CallerMemberName]string caller = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
        }

        public void ClearUnitsCollection()
        {
            UnitsCollection.Clear();
        }


        public int ID { get; set; }
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged<Unit>(); }
        }
        public int NationID { get; set; }
        public string Type { get; set; }
        public string Composition { get; set; }
        public int Experience { get; set; }
        public string WeaponDescription { get; set; }
        public int ArmourClass { get; set; }
        public int BasePoints { get; set; }
        public int Inexperienced { get; set; }
        public int Regular { get; set; }
        public int Veteran { get; set; }
        public int PointsInexp { get; set; }
        public int PointsReg { get; set; }
        public int PointsVet { get; set; }
        public int BaseSize { get; set; }
        public int MaxSize { get; set; }
        public bool isEmpty;
        public List<int> ListOfActiveRules = new List<int>();
        public List<int> ListOfActiveWeapons = new List<int>();

        public Unit()
        {
            isEmpty = true;
        }

        public Unit(int id)
        {
            LoadID(id);
        }

        public Unit(int id, string name, int nationID, string type, string composition, string weaponDescription, int armourClass, int basePoints, int inexperienced, int regular, int veteran, int pointsInexp, int pointsReg, int pointsVet, int baseSize, int maxSize, List<int> listOfActiveRules, List<int> listOfActiveWeapons)
        {
            ID = id;
            Name = name;
            NationID = nationID;
            Type = type;
            Composition = composition;
            WeaponDescription = weaponDescription;
            ArmourClass = armourClass;
            BasePoints = basePoints;
            Inexperienced = inexperienced;
            Regular = regular;
            Veteran = veteran;
            PointsInexp = pointsInexp;
            PointsReg = pointsReg;
            PointsVet = pointsVet;
            BaseSize = baseSize;
            MaxSize = maxSize;
            ListOfActiveRules = listOfActiveRules;
            ListOfActiveWeapons = listOfActiveWeapons;

            UnitsCollection.Add(this);

            isEmpty = false;
        }

        public void CreateNewAndSaveToDB(string name, int nationID, string type, string composition, string weaponDescription, int armourClass, int basePoints, int inexperienced, int regular, int veteran, int pointsInexp, int pointsReg, int pointsVet, int baseSize, int maxSize, ObservableCollection<GameRule> selectedRulesList, ObservableCollection<Weapon> selectedWeaponsList)
        {
            int id;
            string sql_name = (String.IsNullOrWhiteSpace(name)) ? "null" : "'" + name + "'";
            string sql_nationID = (nationID == 0) ? "null" : "'" + nationID + "'";
            string sql_type = (String.IsNullOrWhiteSpace(type)) ? "null" : "'" + type + "'";
            string sql_composition = (String.IsNullOrWhiteSpace(composition)) ? "null" : "'" + composition + "'";
            string sql_weaponDescription = (String.IsNullOrWhiteSpace(weaponDescription)) ? "null" : "'" + weaponDescription + "'";
            string sql_armourClass = (armourClass == 0) ? "null" : "'" + armourClass + "'";
            string sql_basePoints = (basePoints == 0) ? "null" : "'" + basePoints + "'";
            string sql_inexperienced = (inexperienced == 0) ? "null" : "'" + inexperienced + "'";
            string sql_regular = (regular == 0) ? "null" : "'" + regular + "'";
            string sql_veteran = (veteran == 0) ? "null" : "'" + veteran + "'";
            string sql_pointsInexp = (pointsInexp == 0) ? "null" : "'" + pointsInexp + "'";
            string sql_pointsReg = (pointsReg == 0) ? "null" : "'" + pointsReg + "'";
            string sql_pointsVet = (pointsVet == 0) ? "null" : "'" + pointsVet + "'";
            string sql_baseSize = (baseSize == 0) ? "null" : "'" + baseSize + "'";
            string sql_maxSize = (maxSize == 0) ? "null" : "'" + maxSize + "'";
            List<int> newListOfActiveRules = new List<int>();
            List<int> newListOfActiveWeapons = new List<int>();

            try
            {
                Database.ExecuteCommand("INSERT INTO Unit (Name, NationID, Type, Composition, WeaponDescription, ArmourClass, BasePointsInexperienced, Regular, Veteran, PointsPerInexp, PointsPerReg, PointsPerVet, BaseSize, MaxSize,) VALUES (" + sql_name + "," + sql_nationID + "," + sql_type + "," + sql_composition + "," + sql_weaponDescription + "," + sql_armourClass + "," + sql_basePoints + "," + sql_inexperienced + "," + sql_regular + "," + sql_veteran + "," + sql_pointsInexp + "," + sql_pointsReg + "," + sql_pointsVet + "," + sql_baseSize + "," + sql_maxSize + ")");
                id = Database.GetLastInsertedID();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            foreach (GameRule rule in selectedRulesList)
            {
                try
                {
                    Database.ExecuteCommand("INSERT INTO Rule_Unit (RuleID, UnitID) VALUES (" + rule.ID + "," + id + ")");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                newListOfActiveRules.Add(rule.ID);
            }

            foreach (Weapon weapon in selectedWeaponsList)
            {
                try
                {
                    Database.ExecuteCommand("INSERT INTO Unit_Weapon (WeaponID, UnitID) VALUES (" + weapon.ID + "," + id + ")");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                newListOfActiveWeapons.Add(weapon.ID);
            }

            new Unit(id, name, nationID, type, composition, weaponDescription, armourClass, basePoints, inexperienced, regular, veteran, pointsInexp, pointsReg, pointsVet, baseSize, maxSize, newListOfActiveRules, newListOfActiveWeapons);
        }

        public void UpdateInDB(string name, int nationID, string type, string composition, string weaponDescription, int armourClass, int basePoints, int inexperienced, int regular, int veteran, int pointsInexp, int pointsReg, int pointsVet, int baseSize, int maxSize, ObservableCollection<GameRule> selectedRulesList, ObservableCollection<Weapon> selectedWeaponsList)
        {
            string sql_name = (String.IsNullOrWhiteSpace(name)) ? "null" : "'" + name + "'";
            int sql_nationID = nationID;
            string sql_type = (String.IsNullOrWhiteSpace(type)) ? "null" : "'" + type + "'";
            string sql_composition = (String.IsNullOrWhiteSpace(composition)) ? "null" : "'" + composition + "'";
            string sql_weaponDescription = (String.IsNullOrWhiteSpace(weaponDescription)) ? "null" : "'" + weaponDescription + "'";
            int sql_armourClass = armourClass;
            int sql_basePoints = basePoints;
            int sql_inexperienced = inexperienced;
            int sql_regular = regular;
            int sql_veteran = veteran;
            int sql_pointsInexp = pointsInexp;
            int sql_pointsReg = pointsReg;
            int sql_pointsVet = pointsVet;
            int sql_baseSize = baseSize;
            int sql_maxSize = maxSize;
            List<int> newListOfActiveRules = new List<int>();
            List<int> newListOfActiveWeapons = new List<int>();

            try
            {
                Database.ExecuteCommand("UPDATE Unit SET Name = " + sql_name + ", NationID = " + sql_nationID + ", Type = " + sql_type + ", Composition = " + sql_composition + ", WeaponDescription = " + sql_weaponDescription + ", ArmourClass = " + sql_armourClass + ", BasePoints = " + sql_basePoints + ", Inexperienced = " + sql_inexperienced + "Regular = " + sql_regular + "Veteran = " + sql_veteran + "PointsPerInexp = " + sql_pointsInexp + "PointsPerReg = " + sql_pointsReg + "PointsPerVet = " + sql_pointsVet + "baseSize = " + sql_baseSize + "maxSize" + sql_maxSize + "WHERE ID = " + ID);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            try
            {
                Database.ExecuteCommand("DELETE FROM Rule_Unit WHERE UnitID = " + ID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            foreach (GameRule rule in selectedRulesList)
            {
                try
                {
                    Database.ExecuteCommand("INSERT INTO Rule_Unit (RuleID, UnitID) VALUES (" + rule.ID + "," + ID + ")");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                newListOfActiveRules.Add(rule.ID);
            }

            try
            {
                Database.ExecuteCommand("DELETE FROM Unit_Weapon WHERE UnitID = " + ID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            foreach (Weapon weapon in selectedWeaponsList)
            {
                try
                {
                    Database.ExecuteCommand("INSERT INTO Unit_Weapon (WeaponID, UnitID) VALUES (" + weapon.ID + "," + ID + ")");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                newListOfActiveWeapons.Add(weapon.ID);
            }

            Name = name;
            NationID = nationID;
            Type = type;
            Composition = composition;
            WeaponDescription = weaponDescription;
            ArmourClass = armourClass;
            BasePoints = basePoints;
            Inexperienced = inexperienced;
            Regular = regular;
            Veteran = veteran;
            PointsInexp = pointsInexp;
            PointsReg = pointsReg;
            PointsVet = pointsVet;
            BaseSize = baseSize;
            MaxSize = maxSize;
            ListOfActiveRules = newListOfActiveRules;
            ListOfActiveWeapons = newListOfActiveWeapons;
        }
        public void Remove()
        {
            try
            {
                Database.ExecuteCommand("DELETE FROM Unit WHERE ID = " + ID);
                Database.ExecuteCommand("DELETE FROM Rule_Unit WHERE UnitID = " + ID);
                Database.ExecuteCommand("DELETE FROM Unit_Weapon WHERE UnitID = " + ID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            UnitsCollection.Remove(this);
        }

        public void LoadID(int id)
        {
            SQLiteDataReader result = Database.ExecuteCommand("SELECT * FROM Unit WHERE ID = " + id);

            while (result.Read())
            {
              ID = result.GetInt32(0);
              Name = result.GetString(1);
              NationID = (!result.IsDBNull(2)) ? result.GetInt32(2) : 0;
              Type = (!result.IsDBNull(3)) ? result.GetString(3) : "";
              BasePoints = (!result.IsDBNull(4)) ? result.GetInt32(4) : 0;
              WeaponDescription = (!result.IsDBNull(5)) ? result.GetString(5) : "";
              Composition = (!result.IsDBNull(6)) ? result.GetString(6) : "";
              Inexperienced =(!result.IsDBNull(7)) ? result.GetInt32(7): 0;
              Regular =(!result.IsDBNull(8)) ? result.GetInt32(8): 0;
              Veteran =(!result.IsDBNull(9)) ? result.GetInt32(9): 0;
              PointsInexp =(!result.IsDBNull(10)) ? result.GetInt32(10): 0;
              PointsReg =(!result.IsDBNull(11)) ? result.GetInt32(11): 0;
              PointsVet =(!result.IsDBNull(12)) ? result.GetInt32(12): 0;
              BaseSize =(!result.IsDBNull(13)) ? result.GetInt32(13): 0;
              MaxSize =(!result.IsDBNull(14)) ? result.GetInt32(14): 0;
              ArmourClass = (!result.IsDBNull(15)) ? result.GetInt32(15) : 0;
            }
        }

        public void LoadAll()
        {
            SQLiteDataReader result = Database.ExecuteCommand("SELECT * FROM Unit");
            int ID;
            string Name,
                Type,
                Composition,
                WeaponDescription;
            int NationID,
                ArmourClass,
                BasePoints,
                Inexperienced,
                Regular,
                Veteran,
                PointsInexp,
                PointsReg,
                PointsVet,
                BaseSize,
                MaxSize;
            List<int> newListOfActiveRules;
            List<int> newListOfActiveWeapons;

            while (result.Read())
            {
                ID = result.GetInt32(0);
                Name = result.GetString(1);
                NationID = (!result.IsDBNull(2)) ? result.GetInt32(2) : 0;
                Type = (!result.IsDBNull(3)) ? result.GetString(3) : "";
                BasePoints = (!result.IsDBNull(4)) ? result.GetInt32(4) : 0;
                WeaponDescription = (!result.IsDBNull(5)) ? result.GetString(5) : "";
                Composition = (!result.IsDBNull(6)) ? result.GetString(6) : "";
                Inexperienced =(!result.IsDBNull(7)) ? result.GetInt32(7): 0;
                Regular =(!result.IsDBNull(8)) ? result.GetInt32(8): 0;
                Veteran =(!result.IsDBNull(9)) ? result.GetInt32(9): 0;
                PointsInexp =(!result.IsDBNull(10)) ? result.GetInt32(10): 0;
                PointsReg =(!result.IsDBNull(11)) ? result.GetInt32(11): 0;
                PointsVet =(!result.IsDBNull(12)) ? result.GetInt32(12): 0;
                BaseSize =(!result.IsDBNull(13)) ? result.GetInt32(13): 0;
                MaxSize =(!result.IsDBNull(14)) ? result.GetInt32(14): 0;
                ArmourClass = (!result.IsDBNull(15)) ? result.GetInt32(15) : 0;

                newListOfActiveRules = new List<int>();
                SQLiteDataReader ruleResult = Database.ExecuteCommand("SELECT RuleID FROM Rule_Unit WHERE UnitID = " + ID);
                while (ruleResult.Read())
                {
                    newListOfActiveRules.Add(ruleResult.GetInt32(0));
                }

                newListOfActiveWeapons = new List<int>();
                SQLiteDataReader weaponResult = Database.ExecuteCommand("SELECT WeaponID FROM Unit_Weapon WHERE UnitID = " + ID);
                while (weaponResult.Read())
                {
                    newListOfActiveWeapons.Add(weaponResult.GetInt32(0));
                }

                new Unit(ID, Name, NationID, Type, Composition, WeaponDescription, ArmourClass, BasePoints, Inexperienced, Regular, Veteran, PointsInexp, PointsReg, PointsVet, BaseSize, MaxSize, newListOfActiveRules, newListOfActiveWeapons);
            }
            result.Close();
        }
    }
}
