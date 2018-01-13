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

        public string CreateString(int min, int max)
        {
            //i.e. 0-1;unitID,exclusionFlag;unitID,exclusionFlag|

            EntryString = min + "-" + max;

            foreach(var unit in SelectedUnitsList)
            {
                EntryString += ";" + unit.ID + ",";
                if (ExcludingUnitsList.Contains(SelectedUnitsList.Where(i => i.Name == unit.Name).First()))
                    EntryString += "true";
                else
                    EntryString += "false";

            }
            EntryString += "|";
            return EntryString;
        }

        public ObservableCollection<Unit> Add(Unit NewUnit)
        {
            bool t_itemIsNotOnList = false;
            if (NewUnit != null)
            {
                try { SelectedUnitsList.First(i => i.ID == NewUnit.ID); }
                catch (Exception) { t_itemIsNotOnList = true; }

                if (t_itemIsNotOnList)
                    SelectedUnitsList.Add(NewUnit);
            }
            return SelectedUnitsList;
        }

        public ObservableCollection<Unit> AddAll(ObservableCollection<Unit> UnitsList)
        {                   
            return SelectedUnitsList = new ObservableCollection<Unit>(UnitsList);
        }

        public ObservableCollection<Unit> RemoveAll()
        {
            //ExcludingUnitsList.Clear();
            //while(SelectedUnitsList.Count > 0) { }
            ObservableCollection<int> IDsToRemove = new ObservableCollection<int>();
            foreach (Unit unit in SelectedUnitsList) {
                IDsToRemove.Add(unit.ID);
            }

            foreach (int ID in IDsToRemove)
            {
                SelectedUnitsList.Remove(SelectedUnitsList.Where(i => i.ID == ID).First());
            }

            return SelectedUnitsList;
        }

        public ObservableCollection<Unit> Remove(Unit DeleteUnit)
        {
            if (DeleteUnit != null)
            {
                SelectedUnitsList.Remove(SelectedUnitsList.Where(i => i.Name == DeleteUnit.Name).First());
            }
            return SelectedUnitsList;
        }

        public ObservableCollection<Unit> AddToExcluding(Unit NewUnit)
        {
            if (NewUnit != null && !ExcludingUnitsList.Contains(NewUnit))
                ExcludingUnitsList.Add(NewUnit);
            return ExcludingUnitsList;
        }

        public ObservableCollection<Unit> RemoveFromExcluding(Unit DeleteUnit)
        {
            if (DeleteUnit != null)
            {
                ExcludingUnitsList.Remove(SelectedUnitsList.Where(i => i.Name == DeleteUnit.Name).First());
            }
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
