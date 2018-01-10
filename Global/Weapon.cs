using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SQLite;
using System.Runtime.CompilerServices;
using System.Windows;

namespace ArmyArranger.Global
{
    public class Weapon : INotifyPropertyChanged
    {
        public static ObservableCollection<Weapon> WeaponsCollection = new ObservableCollection<Weapon>();
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged<T>([CallerMemberName]string caller = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
        }

        public void ClearWeaponsCollection()
        {
            WeaponsCollection.Clear();
        }


        public int ID;
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged<Weapon>(); }
        }
        public String Range { get; set; }
        public String Shots { get; set; }
        public int Penetration { get; set; }
        public bool RequiresLoader { get; set; }
        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set { _isSelected = value; OnPropertyChanged<Weapon>(); }
        }
        public bool isEmpty;
        public List<int> ListOfActiveRules = new List<int>();

        public Weapon()
        {
            isEmpty = true;
        }

        public Weapon(int id, string name, String range, String shots, int penetration, bool requiresLoader, List<int> listOfActiveRules)
        {
            ID = id;
            Name = name;
            Range = range;
            Shots = shots;
            Penetration = penetration;
            RequiresLoader = requiresLoader;
            ListOfActiveRules = listOfActiveRules;

            WeaponsCollection.Add(this);

            isEmpty = false;
        }

        public void CreateNewAndSaveToDB(string name, String range, String shots, int penetration, bool requiresLoader, ObservableCollection<GameRule> selectedRulesList)
        {
            int id;
            string sql_name = (String.IsNullOrWhiteSpace(name)) ? "null" : "'" + name + "'";
            string sql_range = (range == "0") ? "null" : "'" + range + "'";
            string sql_shots = (shots == "0") ? "null" : "'" + shots + "'";
            string sql_penetration = (penetration == 0) ? "null" : "'" + penetration + "'";
            string sql_requiresLoader = "'"+requiresLoader+"'";
            List<int> newListOfActiveRules = new List<int>();

            try
            {
                Database.ExecuteCommand("INSERT INTO Weapon (Name, Range, Shots, Penetration, RequiresLoader) VALUES (" + sql_name + "," + sql_range + "," + sql_shots + "," + sql_penetration + "," + sql_requiresLoader + ")");
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
                    Database.ExecuteCommand("INSERT INTO Rule_Weapon (RuleID, WeaponID) VALUES (" + rule.ID + "," + id + ")");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                newListOfActiveRules.Add(rule.ID);
            }
            new Weapon(id, name, range, shots, penetration, requiresLoader, newListOfActiveRules);
        }

        public void UpdateInDB(string name, String range, String shots, int penetration, bool requiresLoader, ObservableCollection<GameRule> selectedRulesList)
        {
            String sql_name = (String.IsNullOrWhiteSpace(name)) ? "null" : "'" + name + "'";
            String sql_range = range;
            String sql_shots = shots;
            int sql_penetration = penetration;
            string sql_requiresLoader = "'" + requiresLoader + "'";
            List<int> newListOfActiveRules = new List<int>();

            try
            {
                Database.ExecuteCommand("UPDATE Weapon SET Name = " + sql_name + ", Range = " + sql_range + ", Shots = " + sql_shots + ", Penetration = " + sql_penetration + ", RequiresLoader = " + sql_requiresLoader + " WHERE ID = " + ID);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            try
            {
                Database.ExecuteCommand("DELETE FROM Rule_Weapon WHERE WeaponID = " + ID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            foreach (GameRule rule in selectedRulesList)
            {
                try
                {
                    Database.ExecuteCommand("INSERT INTO Rule_Weapon (RuleID, WeaponID) VALUES (" + rule.ID + "," + ID + ")");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                newListOfActiveRules.Add(rule.ID);
            }


            Name = name;
            Range = range;
            Shots = shots;
            Penetration = penetration;
            RequiresLoader = requiresLoader;
            ListOfActiveRules = newListOfActiveRules;
        }
        public void Remove()
        {
            try
            {
                Database.ExecuteCommand("DELETE FROM Weapon WHERE ID = " + ID);
                Database.ExecuteCommand("DELETE FROM Rule_Weapon WHERE WeaponID = " + ID);
                Database.ExecuteCommand("DELETE FROM Unit_Weapon WHERE WeaponID = " + ID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            WeaponsCollection.Remove(this);
        }

        public void LoadAll()
        {
            SQLiteDataReader result = Database.ExecuteCommand("SELECT * FROM Weapon");
            int ID;
            String Name, Shots, Range;
            int  Penetration;
            bool RequiresLoader;
            List<int> newListOfActiveRules;

            while (result.Read())
            {
                ID = result.GetInt32(0);
                Name = result.GetString(1);
                Range = result.GetString(2);
                Shots = result.GetString(3);
                Penetration = (!result.IsDBNull(4)) ? result.GetInt32(4) : 0;
                RequiresLoader = result["RequiresLoader"] as bool? ?? false;

                newListOfActiveRules = new List<int>();
                SQLiteDataReader ruleResult = Database.ExecuteCommand("SELECT RuleID FROM Rule_Weapon WHERE WeaponID = "+ID);
                while (ruleResult.Read())
                {
                    newListOfActiveRules.Add(ruleResult.GetInt32(0));
                }

                new Weapon(ID, Name, Range, Shots, Penetration, RequiresLoader, newListOfActiveRules);
            }
            result.Close();
        }
    }
}
