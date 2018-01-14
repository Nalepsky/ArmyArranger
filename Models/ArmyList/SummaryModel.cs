using ArmyArranger.Global;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmyArranger.Models.ArmyList
{
    public class SummaryUnit
    {
        public String Content { get; set; }
        private String Name;
        private int points,
            size;


        public SummaryUnit(UnitDetailed unit)
        {
            Name = unit.Name;
            points = unit.Points;
            size = unit.BaseSize + unit.SelectedAditionalUnits;

            Content = Name + "...." + points + " points" + "\n" + size + " men\n";

            Content += "Selected upgrades:\n";
            foreach (var option in unit.ListOfOptions)
                Content += option.Description = "\n";          

        }
    }


    class SummaryModel
    {
        public ObservableCollection<SummaryUnit> GetSummaryUnitList(ObservableCollection<UnitDetailed> AllUnitsCollection)
        {
            ObservableCollection<SummaryUnit> temp = new ObservableCollection<SummaryUnit>();
            foreach (var u in AllUnitsCollection)
                if(u.Selected)
                    temp.Add(new SummaryUnit(u));

            return temp;
        }
    }
}
