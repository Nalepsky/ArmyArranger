using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SQLite;
using System.Runtime.CompilerServices;
using System.Windows;

namespace ArmyArranger.Global
{
    class Weapon : INotifyPropertyChanged
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


        int ID;
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged<Weapon>(); }
        }
        public int Range { get; set; }
        public int Shots { get; set; }
        public int Penetration { get; set; }
        public bool RequiresLoader { get; set; }
        public bool isEmpty;

        public Weapon()
        {
            isEmpty = true;
        }

        public Weapon(int id, string name, int range, int shots, int penetration, bool requiresLoader)
        {
            ID = id;
            Name = name;
            Range = range;
            Shots = shots;
            Penetration = penetration;
            RequiresLoader = requiresLoader;

            WeaponsCollection.Add(this);

            isEmpty = false;
        }

        public void CreateNewAndSaveToDB(string name, int range, int shots, int penetration, bool requiresLoader, int ruleID)
        {
            string sql_name = (String.IsNullOrWhiteSpace(name)) ? "null" : "'" + name + "'";
            int sql_range = range;
            int sql_shots = shots;
            int sql_penetration = penetration;
            string sql_requiresLoader = "'"+requiresLoader+"'";

            try
            {
                Console.WriteLine("INSERT INTO Weapon (Name, Range, Shots, Penetration, RequiresLoader) VALUES (" + sql_name + "," + sql_range + "," + sql_shots + "," + sql_penetration + "," + sql_requiresLoader + ")");
                Database.ExecuteCommand("INSERT INTO Weapon (Name, Range, Shots, Penetration, RequiresLoader) VALUES (" + sql_name + "," + sql_range + "," + sql_shots + "," + sql_penetration + "," + sql_requiresLoader + ")");
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if(ruleID != -1)
                MessageBox.Show("(Weapon.cs) TODO: save record with WeaponID = " + WeaponsCollection.Count + " and RuleID = " + ruleID);

            new Weapon(WeaponsCollection.Count, name, range, shots, penetration, requiresLoader);
        }
        public void UpdateInDB(string name, int range, int shots, int penetration, bool requiresLoader, int ruleID)
        {
            string sql_name = (String.IsNullOrWhiteSpace(name)) ? "null" : "'" + name + "'";
            int sql_range = range;
            int sql_shots = shots;
            int sql_penetration = penetration;
            string sql_requiresLoader = "'" + requiresLoader + "'";

            try
            {
                Database.ExecuteCommand("UPDATE Weapon SET Name = " + sql_name + ", Range = " + sql_range + ", Shots = " + sql_shots + ", Penetration = " + sql_penetration + ", RequiresLoader = " + sql_requiresLoader + " WHERE ID = " + ID);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (ruleID != -1)
                MessageBox.Show("(Weapon.cs) TODO: save record with WeaponID = " + ID + " and RuleID = " + ruleID);

            Name = name;
            Range = range;
            Shots = shots;
            Penetration = penetration;
            RequiresLoader = requiresLoader;
        }
        public void Remove()
        {
            try
            {
                Database.ExecuteCommand("DELETE FROM Weapon WHERE ID = " + ID);
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
            string Name;
            int Range,
                Shots,
                Penetration;
            bool RequiresLoader;
            while (result.Read())
            {
                ID = result.GetInt32(0);
                Name = result.GetString(1);
                Range = result.GetInt32(2);
                Shots = result.GetInt32(3);
                Penetration = (!result.IsDBNull(4)) ? result.GetInt32(4) : 0;
                RequiresLoader = result["RequiresLoader"] as bool? ?? false;

                new Weapon(ID, Name, Range, Shots, Penetration, RequiresLoader);
            }
            result.Close();
        }
    }
}
