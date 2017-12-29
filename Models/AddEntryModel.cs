using ArmyArranger.Global;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmyArranger.Models
{
    class AddEntryModel
    {
        #region Properties
        String EntryString;
        //i.e. 0-1;unitID,exclusionFlag;unitID,exclusionFlag|

        public Unit EmptyUnit = new Unit();
        public ObservableCollection<Unit> SelectedunitsList = new ObservableCollection<Unit>();

        public AddEntryModel()
        {
            EntryString = "";
        }
        #endregion

        #region Constructors

        #endregion

        #region Actions

        public ObservableCollection<Unit> Add(Unit NewUnit)
        {
            if(NewUnit != null)
                SelectedunitsList.Add(NewUnit);
            return SelectedunitsList;
        }

        public ObservableCollection<Unit> AddAll(ObservableCollection<Unit> UnitsList)
        {
            SelectedunitsList = UnitsList;
                
            return SelectedunitsList;
        }

        #endregion

    }
}
