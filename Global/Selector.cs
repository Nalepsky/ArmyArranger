using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmyArranger.Global
{
    class Selector
    {
        public static ObservableCollection<Selector> SelectorsCollection = new ObservableCollection<Selector>();

        public int ID { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public string Mandatory { get; set; }
        public string Headquarters{ get; set; }
        public string Infantry { get; set; }
        public string ArmouredCars { get; set; }
        public string Artillery { get; set; }
        public string Tanks { get; set; }
        public string Transport { get; set; }
        public int NationId { get; set; }
        public List<int> ListOfActiveRules = new List<int>();


        public Selector()
        {
            //Name = "";
            //Date = "";
            //Mandatory = "";
            //Headquarters = "";
            //Infantry = "";
            //ArmouredCars = "";
            //Artillery = "";
            //Tanks = "";
            //Transport = "";
            //NationId = 0;
        }

        public Selector(int id, string name, string date, string mandatory, string headquarters, string infantry, string armouredCars, string artillery, string tanks, string transport, int nationId, List<int> listOfActiveRules)
        {
            ID = id;
            Name = (name != null) ? name : "null";
            Date = (date != null) ? date : "null";
            Mandatory = (mandatory != null) ? mandatory : "null";
            Headquarters = (headquarters != null) ? headquarters : "null";
            Infantry = (infantry != null) ? infantry : "null";
            ArmouredCars = (armouredCars != null) ? armouredCars : "null";
            Artillery = (artillery != null) ? artillery : "null";
            Tanks = (tanks != null) ? tanks : "null";
            Transport = (transport != null) ? transport : "null";
            NationId = nationId;
            ListOfActiveRules = listOfActiveRules;
            SelectorsCollection.Add(this);
        }

        public void CreateNewAndSaveToDB(string name, string date, string mandatory, string headquarters, string infantry, string armouredCars, string artillery, string tanks, string transport, int nationID)
        {
            //temp
            List<int> newListOfActiveRules = new List<int>();
            int id;
            string sql_name = (String.IsNullOrWhiteSpace(name)) ? "null" : "'" + name + "'";
            string sql_date = (String.IsNullOrWhiteSpace(name)) ? "null" : "'" + date + "'";
            string sql_mandatory = (String.IsNullOrWhiteSpace(name)) ? "null" : "'" + mandatory + "'";
            string sql_headquarters = (String.IsNullOrWhiteSpace(name)) ? "null" : "'" + headquarters + "'";
            string sql_infantry = (String.IsNullOrWhiteSpace(name)) ? "null" : "'" + infantry + "'";
            string sql_armouredCars = (String.IsNullOrWhiteSpace(name)) ? "null" : "'" + armouredCars + "'";
            string sql_artillery = (String.IsNullOrWhiteSpace(name)) ? "null" : "'" + artillery + "'";
            string sql_tanks = (String.IsNullOrWhiteSpace(name)) ? "null" : "'" + tanks + "'";
            string sql_transport = (String.IsNullOrWhiteSpace(name)) ? "null" : "'" + transport + "'";

            try
            {
                Database.ExecuteCommand("INSERT INTO Selector (Name, Date, Mandatory, Headquarters, Infantry, ArmouredCars, Artillery, Tanks, Transport, NationID) VALUES ("
                    + sql_name + "," + sql_date + "," + sql_mandatory + "," + sql_headquarters + "," + sql_infantry + "," + sql_armouredCars + "," + sql_artillery + "," + sql_tanks + "," + sql_transport + "," + nationID + ")");
                id = Database.GetLastInsertedID();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            new Selector(id, name, date, mandatory, headquarters, infantry, armouredCars, artillery, tanks, transport, nationID, newListOfActiveRules);
        }

        public void ClearSelectorsCollection()
        {
            SelectorsCollection.Clear();
        }

        public void LoadAll()
        {
            SQLiteDataReader result = Database.ExecuteCommand("SELECT * FROM Selector");
            int ID;
            String Name,
             Date,
             Mandatory,
             Headquarters,
             Infantry,
             ArmouredCars,
             Artillery,
             Tanks,
             Transport;
            int NationID;

            List<int> newListOfActiveRules;

            while (result.Read())
            {
                ID = result.GetInt32(0);
                Name = result.GetString(1);
                Date = result.GetString(2);
                Mandatory = (!result.IsDBNull(3)) ? result.GetString(3) : "";
                Headquarters = (!result.IsDBNull(4)) ? result.GetString(4) : "";
                Infantry = (!result.IsDBNull(5)) ? result.GetString(5) : "";
                ArmouredCars = (!result.IsDBNull(6)) ? result.GetString(6) : "";
                Artillery = (!result.IsDBNull(7)) ? result.GetString(7) : "";
                Tanks = (!result.IsDBNull(8)) ? result.GetString(8) : "";
                Transport = (!result.IsDBNull(9)) ? result.GetString(9) : "";
                NationID = (!result.IsDBNull(10)) ? result.GetInt32(10) : -1;

                newListOfActiveRules = new List<int>();
                SQLiteDataReader ruleResult = Database.ExecuteCommand("SELECT RuleID FROM Rule_Selector WHERE SelectorID = " + ID);
                while (ruleResult.Read())
                {
                    newListOfActiveRules.Add(ruleResult.GetInt32(0));
                }

                new Selector(ID, Name, Date, Mandatory, Headquarters, Infantry, ArmouredCars, Artillery, Tanks, Transport, NationID, newListOfActiveRules);
            }
            result.Close();
        }
    }
}
