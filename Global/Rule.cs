using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ArmyArranger.Global
{
    class Rule
    {
        string Name = "null";
        string Description = "null";
        string Type = "null";
        string Source = "null";

        public Rule(string name, string description, string type, string source)
        {
            if (name != null)
                Name = "'" + name + "'";

            if (description != null)
                Description = "'" + description + "'";

            if (type != null)
                Type = "'" + type + "'";

            if (source != null)
                Source = "'" + source + "'";
        }

        public void SaveToDB()
        {
            Database.ExecuteCommand("INSERT INTO Rule (Name, Description, Type, Source) VALUES (" + Name + "," + Description + "," + Type + "," + Source + ")");
        }
    }
}
