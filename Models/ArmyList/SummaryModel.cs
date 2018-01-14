using ArmyArranger.Global;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ArmyArranger.Models.ArmyList
{
    public class SummaryUnit
    {
        public String Content { get; set; }
        private String Name;
        private int points,
            size;
        public string line1, line2, line3;

        private List<String> OutputString = new List<string>();

        public SummaryUnit(UnitDetailed unit)
        {
            Name = unit.Name;
            points = unit.Points;
            size = unit.BaseSize + unit.SelectedAditionalUnits;            

            line1 = Name + "...." + points + " points" + "\n" + size + " model(s)\n";
            
            line2 = "Selected upgrades:\n";
            line3 = "";
            foreach (var option in unit.ListOfOptions)
            {
                line3 += option.Description + "\n";
            }
            Content = line1 + line2 + line3;       

        }
    }


    class SummaryModel
    {
        private ObservableCollection<SummaryUnit> SelectedUnitsList = new ObservableCollection<SummaryUnit>();

        public ObservableCollection<SummaryUnit> GetSummaryUnitList(ObservableCollection<UnitDetailed> AllUnitsCollection)
        {
            foreach (var u in AllUnitsCollection)
                if(u.Selected)
                    SelectedUnitsList.Add(new SummaryUnit(u));

            return SelectedUnitsList;
        }

        public void SaveToTxt()
        {
            using (StreamWriter outputFile = new StreamWriter("ArmyList.txt"))
                foreach (var u in SelectedUnitsList)
                {
                    outputFile.WriteLine(u.line1);
                    Console.WriteLine(u.line1);
                    outputFile.WriteLine(u.line2);
                    Console.WriteLine(u.line2);
                    outputFile.WriteLine(u.line3);
                    Console.WriteLine(u.line3);
                }
            MessageBox.Show("File ArmyList.txt created.\nYou can find it next to ArmyArranger.exe file.");
        }
    }    
}
