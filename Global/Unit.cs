﻿using System;
using System.Collections.Generic;
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
        public string Composition { get; set; }
        public int Experience { get; set; }
        public string WeaponDescription { get; set; }
        public int ArmourClass { get; set; }
        public int BasePoints { get; set; }
        public bool isEmpty;
        public List<int> ListOfActiveRules = new List<int>();

        public Unit()
        {
            isEmpty = true;
        }

        public Unit(int id, string name, int nationID, string type, string composition, int experience, string weaponDescription, int armourClass, int basePoints)
        {
            ID = id;
            Name = name;
            NationID = nationID;
            Type = type;
            Composition = composition;
            Experience = experience;
            WeaponDescription = weaponDescription;
            ArmourClass = armourClass;
            BasePoints = basePoints;

            UnitsCollection.Add(this);

            isEmpty = false;
        }

        public void CreateNewAndSaveToDB(string name, int nationID, string type, string composition, int experience, string weaponDescription, int armourClass, int basePoints, ObservableCollection<GameRule> selectedRulesList)
        {
            int id;
            string sql_name = (String.IsNullOrWhiteSpace(name)) ? "null" : "'" + name + "'";
            int sql_nationID = nationID;
            string sql_type = (String.IsNullOrWhiteSpace(type)) ? "null" : "'" + type + "'";
            string sql_composition = (String.IsNullOrWhiteSpace(composition)) ? "null" : "'" + composition + "'";
            int sql_experience = experience;
            string sql_weaponDescription = (String.IsNullOrWhiteSpace(weaponDescription)) ? "null" : "'" + weaponDescription + "'";
            int sql_armourClass = armourClass;
            int sql_basePoints = basePoints;
            List<int> newListOfActiveRules = new List<int>();

            try
            {
                Database.ExecuteCommand("INSERT INTO Unit (Name, NationID, Type, Composition, Experience, WeaponDescription, ArmourClass, BasePoints) VALUES (" + sql_name + "," + sql_nationID + "," + sql_type + "," + sql_composition + "," + sql_experience + "," + sql_weaponDescription + "," + sql_armourClass + "," + sql_basePoints + ")");
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
                    MessageBox.Show("(Unit.cs) TODO: make able to add multiple rulest to one unit");
                    Database.ExecuteCommand("INSERT INTO Rule_Unit (RuleID, UnitID) VALUES (" + rule.ID + "," + UnitsCollection.Count + ")");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                newListOfActiveRules.Add(rule.ID);
            }
            new Unit(UnitsCollection.Count, name, nationID, type, composition, experience, weaponDescription, armourClass, basePoints);
        }

        public void UpdateInDB(string name, int nationID, string type, string composition, int experience, string weaponDescription, int armourClass, int basePoints, ObservableCollection<GameRule> selectedRulesList)
        {
            string sql_name = (String.IsNullOrWhiteSpace(name)) ? "null" : "'" + name + "'";
            int sql_nationID = nationID;
            string sql_type = (String.IsNullOrWhiteSpace(type)) ? "null" : "'" + type + "'";
            string sql_composition = composition;
            int sql_experience = experience;
            string sql_weaponDescription = (String.IsNullOrWhiteSpace(weaponDescription)) ? "null" : "'" + weaponDescription + "'";
            int sql_armourClass = armourClass;
            int sql_basePoints = basePoints;
            List<int> newListOfActiveRules = new List<int>();

            try
            {
                Database.ExecuteCommand("UPDATE Unit SET Name = " + sql_name + ", NationID = " + sql_nationID + ", Type = " + sql_type + ", Composition = " + sql_composition + ", Experience = " + sql_experience + ", WeaponDescription = " + sql_weaponDescription + ", ArmourClass = " + sql_armourClass + ", BasePoints = " + sql_basePoints);
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

            Name = name;
            NationID = nationID;
            Type = type;
            Composition = composition;
            Experience = experience;
            WeaponDescription = weaponDescription;
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
                Composition,
                WeaponDescription;
            int NationID,
                Experience,
                ArmourClass,
                BasePoints;
            while (result.Read())
            {
                ID = result.GetInt32(0);
                Name = result.GetString(1);
                NationID = (!result.IsDBNull(2)) ? result.GetInt32(2) : 0;
                Type = result.GetString(3);
                BasePoints = (!result.IsDBNull(4)) ? result.GetInt32(4) : 0;
                Composition = result.GetString(5);
                WeaponDescription = result.GetString(6);
                Experience = (!result.IsDBNull(7)) ? result.GetInt32(7) : 0;
                ArmourClass = (!result.IsDBNull(8)) ? result.GetInt32(8) : 0;


                new Unit(ID, Name, NationID, Type, Composition, Experience, WeaponDescription, ArmourClass, BasePoints);
            }
            result.Close();
        }
    }
}
