using ArmyArranger.Global;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;



namespace ArmyArranger.Models
{
    class ChooseSelectorModel
    {
        #region Properties

        public Nation EmptyNation = new Nation();
        public Selector EmptySelector = new Selector();
        public Nation lastChoosenNation;

        #endregion

        #region Constructors

        public ChooseSelectorModel()
        {
        }

        #endregion

        #region Actions

        public bool ChosenNationEqualsSelected(Nation selectedNation)
        {
            if (lastChoosenNation != selectedNation)
            {
                lastChoosenNation = selectedNation;
                return true;
            }
            return false;
        }


        public void ClearSelectors()
        {
            lastChoosenNation = EmptyNation;
        }

        #endregion
    }
}
