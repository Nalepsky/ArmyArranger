using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data.SQLite;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ArmyArranger.Global
{
    class GameRule : INotifyPropertyChanged
    {
        public static ObservableCollection<GameRule> RulesCollection = new ObservableCollection<GameRule>();
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged<T>([CallerMemberName]string caller = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
        }

        public void ClearRulesCollection()
        {
            RulesCollection.Clear();
        }


        int ID;
        private string _name;
        public string Name
        {
            get { return _name; }
            set {  _name = value;  OnPropertyChanged<GameRule>(); }
        }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Source { get; set; }
        public Boolean isEmpty;

        public GameRule(){
            isEmpty = true;
        }

        public GameRule(int id, string name, string description, string type, string source)
        {
            ID = id;
            Name = name;
            Description = description;
            Type = type;
            Source = source;

            RulesCollection.Add(this);

            isEmpty = false;
        }

        public void CreateNewAndSaveToDB(string name, string description, string type, string source)
        {
            string sql_name = (String.IsNullOrWhiteSpace(name)) ? "null" : "'" + name + "'";
            string sql_description = (String.IsNullOrWhiteSpace(description)) ? "null" : "'" + description + "'";
            string sql_type = (String.IsNullOrWhiteSpace(type)) ? "null" : "'" + type + "'";
            string sql_source = (String.IsNullOrWhiteSpace(source)) ? "null" : "'" + source + "'";

            try
            {
                Database.ExecuteCommand("INSERT INTO Rule (Name, Description, Type, Source) VALUES (" + sql_name + "," + sql_description + "," + sql_type + "," + sql_source + ")");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            new GameRule(RulesCollection.Count, name, description, type, source);
        }
        public void UpdateInDB(string name, string description, string type, string source)
        {
            string sql_name = (String.IsNullOrWhiteSpace(name)) ? "null" : "'" + name + "'";
            string sql_description = (String.IsNullOrWhiteSpace(description)) ? "null" : "'" + description + "'";
            string sql_type = (String.IsNullOrWhiteSpace(type)) ? "null" : "'" + type + "'";
            string sql_source = (String.IsNullOrWhiteSpace(source)) ? "null" : "'" + source + "'";

            try
            {
                Database.ExecuteCommand("UPDATE Rule SET Name = " + sql_name + ", Description = " + sql_description + ", Type = " + sql_type + ", Source = " + sql_source + " WHERE ID = " + ID);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            Name = name;
            Description = description;
            Type = type;
            Source = source;
        }

        public void LoadAll()
        {
            SQLiteDataReader result = Database.ExecuteCommand("SELECT * FROM rule");
            while (result.Read())
            {
                new GameRule(result.GetInt32(0), result.GetString(1), result.GetString(2), result.GetString(3), result.GetString(4));
            }
            result.Close();
        }
    }
}
