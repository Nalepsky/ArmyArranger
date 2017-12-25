using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SQLite;
using System.Runtime.CompilerServices;

namespace ArmyArranger.Global
{
    class Nation : INotifyPropertyChanged
    {
        public static ObservableCollection<Nation> NationsCollecion = new ObservableCollection<Nation>();
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged<T>([CallerMemberName]string caller = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
        }

        public void ClearNationsCollecion()
        {
            NationsCollecion.Clear();
        }


        public int ID;
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged<Nation>(); }
        }
        public Boolean isEmpty;

        public Nation()
        {
            isEmpty = true;
        }

        public Nation(int id, string name)
        {
            ID = id;
            Name = name;
            NationsCollecion.Add(this);
            isEmpty = false;
        }

        public void CreateNewAndSaveToDB(string name)
        {
            string sql_name = (String.IsNullOrWhiteSpace(name)) ? "null" : "'" + name + "'";

            try
            {
                Database.ExecuteCommand("INSERT INTO Nation (Name) VALUES (" + sql_name + ")");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            new Nation(NationsCollecion.Count, name);
        }
        public void UpdateInDB(string name)
        {
            string sql_name = (String.IsNullOrWhiteSpace(name)) ? "null" : "'" + name + "'";

            try
            {
                Database.ExecuteCommand("UPDATE Nation SET Name = " + sql_name + " WHERE ID = " + ID);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            Name = name;
        }
        public void Remove()
        {
            try
            {
                Database.ExecuteCommand("DELETE FROM Nation WHERE ID = " + ID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            NationsCollecion.Remove(this);
        }

        public void LoadAll()
        {
            SQLiteDataReader result = Database.ExecuteCommand("SELECT * FROM Nation");
            while (result.Read())
            {
                new Nation(result.GetInt32(0), result.GetString(1));
            }
            result.Close();
        }
    }
}
