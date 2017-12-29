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
        public ObservableCollection<Unit> SelectedUnitsList = new ObservableCollection<Unit>();
        public ObservableCollection<Unit> ExcludingUnitsList = new ObservableCollection<Unit>();

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
                SelectedUnitsList.Add(NewUnit);
            return SelectedUnitsList;
        }

        public ObservableCollection<Unit> AddAll(ObservableCollection<Unit> UnitsList)
        {                   
            return SelectedUnitsList = UnitsList;
        }

        public ObservableCollection<Unit> RemoveAll()
        {
            return SelectedUnitsList = null;
        }

        public ObservableCollection<Unit> Remove(Unit DeleteUnit)
        {
            SelectedUnitsList.Remove(SelectedUnitsList.Where(i => i.Name == DeleteUnit.Name).Single());
            
            return SelectedUnitsList;
        }

        public ObservableCollection<Unit> AddToExcluding(Unit NewUnit)
        {
            if (NewUnit != null)
                ExcludingUnitsList.Add(NewUnit);
            return ExcludingUnitsList;
        }

        public ObservableCollection<Unit> RemoveFromExcluding(Unit DeleteUnit)
        {
            ExcludingUnitsList.Remove(SelectedUnitsList.Where(i => i.Name == DeleteUnit.Name).Single());

            return ExcludingUnitsList;
        }

        public void Clear()
        {
            SelectedUnitsList.Clear();
            ExcludingUnitsList.Clear();
        }

        #endregion

    }
}
