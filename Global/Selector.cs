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

        public string Name { get; set; }
        public string Date { get; set; }
        public string Mandatory { get; set; }
        public string Headquarters{ get; set; }
        public string Infantry { get; set; }
        public string ArmouredCars { get; set; }
        public string Artilery { get; set; }
        public string Tanks { get; set; }
        public string Transport { get; set; }
        public string NationId { get; set; }
        public List<int> ListOfActiveRules = new List<int>();


        public Selector()
        {
        }

        public Selector(string name, string date, string mandatory, string headquarters, string infantry, string armouredCars, string artilery, string tanks, string transport, string nationId, List<int> listOfActiveRules)
        {
            Name = (name != null) ? "'" + name + "'" : "null";
            Date = (date != null) ? "'" + date + "'" : "null";
            Mandatory = (mandatory != null) ? "'" + mandatory + "'" : "null";
            Headquarters = (headquarters != null) ? "'" + headquarters + "'" : "null";
            Infantry = (infantry != null) ? "'" + infantry + "'" : "null";
            ArmouredCars = (armouredCars != null) ? "'" + armouredCars + "'" : "null";
            Artilery = (artilery != null) ? "'" + artilery + "'" : "null";
            Tanks = (tanks != null) ? "'" + tanks + "'" : "null";
            Transport = (transport != null) ? "'" + transport + "'" : "null";
            NationId = (nationId != null) ? "'" + nationId + "'" : "null";
            ListOfActiveRules = listOfActiveRules;
            Console.WriteLine(ListOfActiveRules.Count);
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
             Artilery,
             Tanks,
             Transport,
             NationID;

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
                Artilery = (!result.IsDBNull(7)) ? result.GetString(7) : "";
                Tanks = (!result.IsDBNull(8)) ? result.GetString(8) : "";
                Transport = (!result.IsDBNull(9)) ? result.GetString(9) : "";
                NationID = (!result.IsDBNull(10)) ? result.GetString(10) : "";

                newListOfActiveRules = new List<int>();
                SQLiteDataReader ruleResult = Database.ExecuteCommand("SELECT RuleID FROM Rule_Selector WHERE SelectorID = " + ID);
                while (ruleResult.Read())
                {
                    newListOfActiveRules.Add(ruleResult.GetInt32(0));
                }

                new Selector(Name, Date, Mandatory, Headquarters, Infantry, ArmouredCars, Artilery, Tanks, Transport, NationId, newListOfActiveRules);
            }
            result.Close();
        }
    }
}
