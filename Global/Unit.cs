using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SQLite;
using System.Runtime.CompilerServices;
using System.Windows;

namespace ArmyArranger.Global
{
    class Unit : INotifyPropertyChanged
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


        int ID;
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged<Unit>(); }
        }
        public int NationID { get; set; }
        public string Type { get; set; }
        public int Composition { get; set; }
        public int Experience { get; set; }
        public string WeaponDescription { get; set; }
        public string Weapons { get; set; }
        public int ArmourClass { get; set; }
        public int BasePoints { get; set; }
        public bool isEmpty;

        public Unit()
        {
            isEmpty = true;
        }

        public Unit(int id, string name, int nationID, string type, int composition, int experience, string weaponDescription, string weapons, int armourClass, int basePoints)
        {
            ID = id;
            Name = name;
            NationID = nationID;
            Type = type;
            Composition = composition;
            Experience = experience;
            WeaponDescription = weaponDescription;
            Weapons = weapons;
            ArmourClass = armourClass;
            BasePoints = basePoints;
            UnitsCollection.Add(this);

            isEmpty = false;
        }

        public void CreateNewAndSaveToDB(string name, int nationID, string type, int composition, int experience, string weaponDescription, string weapons, int armourClass, int basePoints, int ruleID)
        {
            string sql_name = (String.IsNullOrWhiteSpace(name)) ? "null" : "'" + name + "'";
            int sql_nationID = nationID;
            string sql_type = (String.IsNullOrWhiteSpace(type)) ? "null" : "'" + type + "'";
            int sql_composition = composition;
            int sql_experience = experience;
            string sql_weaponDescription = (String.IsNullOrWhiteSpace(weaponDescription)) ? "null" : "'" + weaponDescription + "'";
            string sql_weapons = (String.IsNullOrWhiteSpace(weapons)) ? "null" : "'" + weapons + "'";
            int sql_armourClass = armourClass;
            int sql_basePoints = basePoints;

            try
            {
                Database.ExecuteCommand("INSERT INTO Unit (Name, NationID, Type, Composition, Experience, WeaponDescription, Weapons, ArmourClass, BasePoints) VALUES (" + sql_name + "," + sql_nationID + "," + sql_type + "," + sql_composition + "," + sql_experience + "," + sql_weaponDescription + "," + sql_weapons + "," + sql_armourClass + "," + sql_basePoints + ")");
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (ruleID != -1)
            {
                try
                {
                    MessageBox.Show("(Unit.cs) TODO: make able to add multiple rulest to one unit");
                    Database.ExecuteCommand("INSERT INTO Rule_Unit (RuleID, UnitID) VALUES (" + ruleID + "," + UnitsCollection.Count + ")");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            new Unit(UnitsCollection.Count, name, nationID, type, composition, experience, weaponDescription, weapons, armourClass, basePoints);
        }

        public void UpdateInDB(string name, int nationID, string type, int composition, int experience, string weaponDescription, string weapons, int armourClass, int basePoints, int ruleID)
        {
            string sql_name = (String.IsNullOrWhiteSpace(name)) ? "null" : "'" + name + "'";
        int sql_nationID = nationID;
        string sql_type = (String.IsNullOrWhiteSpace(type)) ? "null" : "'" + type + "'";
        int sql_composition = composition;
        int sql_experience = experience;
        string sql_weaponDescription = (String.IsNullOrWhiteSpace(weaponDescription)) ? "null" : "'" + weaponDescription + "'";
        string sql_weapons = (String.IsNullOrWhiteSpace(weapons)) ? "null" : "'" + weapons + "'";
        int sql_armourClass = armourClass;
        int sql_basePoints = basePoints;

            try
            {
                Database.ExecuteCommand("UPDATE Unit SET Name = " + sql_name + ", NationID = " + sql_nationID + ", Type = " + sql_type + ", Composition = " + sql_composition + ", Experience = " + sql_experience + ", WeaponDescription = " + sql_weaponDescription + ", Weapons = " + sql_weapons + ", ArmourClass = " + sql_armourClass + ", BasePoints = " + sql_basePoints);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (ruleID != -1)
            {
                try
                {
                    MessageBox.Show("(Unit.cs) TODO: make able to add multiple rulest to one unit");
                    Database.ExecuteCommand("DELETE FROM Rule_Unit WHERE UnitID = " + ID); // delete every rule for this unit
                    Database.ExecuteCommand("INSERT INTO Rule_Unit (RuleID, UnitID) VALUES (" + ruleID + "," + ID + ")"); // then add only selected one
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            Name = name;
            NationID = nationID;
            Type = type;
            Composition = composition;
            Experience = experience;
            WeaponDescription = weaponDescription;
            Weapons = weapons;
            ArmourClass = armourClass;
            BasePoints = basePoints;
        }
        public void Remove()
{
    try
    {
        Database.ExecuteCommand("DELETE FROM Unit WHERE ID = " + ID);
        Database.ExecuteCommand("DELETE FROM Rule_Unit WHERE UnitID = " + ID);
        Database.ExecuteCommand("DELETE FROM Unit_Unit WHERE UnitID = " + ID);
    }
    catch (Exception ex)
    {
        throw ex;
    }
    UnitsCollection.Remove(this);
}

public void LoadAll()
{
    SQLiteDataReader result = Database.ExecuteCommand("SELECT * FROM Unit");
    int ID;
    string Name,
        Type,
        WeaponDescription,
        Weapons;
    int NationID,
        Composition,
        Experience,
        ArmourClass,
        BasePoints;
    while (result.Read())
    {
        ID = result.GetInt32(0);
        Name = result.GetString(1);
        NationID = (!result.IsDBNull(2)) ? result.GetInt32(2) : 0;
        Type = result.GetString(3);
        Composition = (!result.IsDBNull(4)) ? result.GetInt32(4) : 0;
        Experience = (!result.IsDBNull(5)) ? result.GetInt32(5) : 0;
        WeaponDescription = result.GetString(6);
        Weapons = result.GetString(7);
        ArmourClass = (!result.IsDBNull(8)) ? result.GetInt32(8) : 0;
        BasePoints = (!result.IsDBNull(9)) ? result.GetInt32(9) : 0;

        new Unit(ID, Name, NationID, Type, Composition, Experience, WeaponDescription, Weapons, ArmourClass, BasePoints);
    }
    result.Close();
}
    }
}
