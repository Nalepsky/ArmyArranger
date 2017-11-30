using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ArmyArranger.Global
{
    class GameRule
    {
        public static ObservableCollection<GameRule> RulesCollection = new ObservableCollection<GameRule>();

        int ID;
        public string Name { get; set; }
        string Description;
        string Type;
        string Source;

        public GameRule(){ }

        public GameRule(int id, string name, string description, string type, string source)
        {
            ID = id;
            Name = name;
            Description = description;
            Type = type;
            Source = source;

            RulesCollection.Add(this);
        }

        public void SaveToDB(string name, string description, string type, string source)
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
