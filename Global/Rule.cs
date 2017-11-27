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
        string Name;
        string Description;
        string Type;
        string Source;

        public Rule(string name, string description, string type, string source)
        {
            Name = (name != null) ? "'" + name + "'" : "null";

            Description = (description != null) ? "'" + description + "'" : "null";

            Type = (type != null) ? "'" + type + "'" : "null";

            Source = (source != null) ? "'" + source + "'" : "null";          
        }

        public void SaveToDB()
        {
            
            Database.ExecuteCommand("INSERT INTO Rule (Name, Description, Type, Source) VALUES (" + Name + "," + Description + "," + Type + "," + Source + ")");
        }
    }
}
