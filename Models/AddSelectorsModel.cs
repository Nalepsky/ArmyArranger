using ArmyArranger.Global;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmyArranger.Models
{
    class AddSelectorsModel
    {
        #region Properties

        public Selector EmptySelector = new Selector();
        public GameRule EmptyRule = new GameRule();
        public Nation EmptyNation = new Nation();
        public ObservableCollection<Entry> MandatoryEntriesList = new ObservableCollection<Entry>();

        private string MandatoryString;

        #endregion

        #region Constructors

        public AddSelectorsModel()
        {
            MandatoryString = EmptySelector.Mandatory;
        }

        #endregion

        #region Actions       
        
        public ObservableCollection<Entry> GetMandatoryentries()
        {
            String[] MandatoryEntries = MandatoryString.Split('|');
            

            foreach(var Entry in MandatoryEntries)
            {
                String[] Elements = Entry.Split(';');
                Entry E = new Entry();
                bool flag = true;
                bool nameFlag = true;

                foreach (var El in Elements)
                {
                    if(flag == true)
                    {
                        flag = false;
                        String[] MinMax = El.Split('-');
                        E.Min = Int32.Parse(MinMax[0]);
                        E.Max = Int32.Parse(MinMax[1]);
                    }
                    else
                    {
                        String[] UnitFlag = El.Split(',');
                        if (nameFlag == true)
                            E.Name = UnitFlag[0] + "...";
                        Unit newUnit = new Unit(Int32.Parse(UnitFlag[0]));
                        E.UnitList.Add(newUnit);

                        if (UnitFlag[1] == "true")
                            E.ExcludingUnitList.Add(newUnit);
                    }                   
                }
                
                MandatoryEntriesList.Add(E);
            }
            
            return MandatoryEntriesList;
        }        

        #endregion
    }
}
