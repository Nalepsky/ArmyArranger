using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmyArranger.Models.EditYourArmies
{
    class Rule
    {
        string Name { get; set; }
        string Describtion { get; set; }
        string Type { get; set; }
        string Source { get; set; }

        public Rule(string name, string source, string type, string describtion)
        {
            Name = name;
            Describtion = describtion;
            Type = type;
            Source = source;
        }

        public string Query()
        {
            return "temporary string";
        }
    }

    class AddRulesModel
    {

    }
}
