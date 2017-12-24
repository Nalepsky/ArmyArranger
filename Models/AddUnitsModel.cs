using ArmyArranger.Global;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;



namespace ArmyArranger.Models
{
    class AddUnitsModel
    {
        #region Properties

        public Unit EmptyUnit = new Unit();
        public GameRule EmptyRule = new GameRule();
        public Unit lastChoosenUnit;
        public GameRule lastChoosenRule;


        #endregion

        #region Constructors

        public AddUnitsModel()
        {

        }

        #endregion

        #region Actions

        public void AddUnit(string name, int nationID, string type, int composition, int experience, string weaponDescription, string weapons, int armourClass, int basePoints, int ruleID)
        {
            try
            {
                EmptyUnit.CreateNewAndSaveToDB(name, nationID, type, composition, experience, weaponDescription, weapons, armourClass, basePoints, ruleID);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            MessageBox.Show("Successfully added");
        }

        public void UpdateUnit(Unit selectedUnit, string name, int nationID, string type, int composition, int experience, string weaponDescription, string weapons, int armourClass, int basePoints, int ruleID)
        {
            if (selectedUnit == null)
                return;


            try
            {
                selectedUnit.UpdateInDB(name, nationID, type, composition, experience, weaponDescription, weapons, armourClass, basePoints, ruleID);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            MessageBox.Show("Successfully updated");
        }

        public void RemoveUnit(Unit selectedUnit)
        {
            if (selectedUnit == null)
                return;


            try
            {
                selectedUnit.Remove();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            MessageBox.Show("Successfully removed");
        }


        public bool ChosenUnitEqualsSelected(Unit selectedUnit)
        {
            if (lastChoosenUnit != selectedUnit)
            {
                lastChoosenUnit = selectedUnit;
                return true;
            }
            return false;
        }

        public bool ChosenRuleEqualsSelected(GameRule selectedRule)
        {
            if (lastChoosenRule != selectedRule)
            {
                lastChoosenRule = selectedRule;
                return true;
            }
            return false;
        }


        public void ClearUnits()
        {
            lastChoosenUnit = EmptyUnit;
        }

        public void ClearRules()
        {
            lastChoosenRule = EmptyRule;
        }


        public bool PromptQuestion(string question)
        {
            switch (MessageBox.Show(question, "Save", MessageBoxButton.YesNo))
            {
                case MessageBoxResult.Yes:
                    return true;
                case MessageBoxResult.No:
                    return false;
                default:
                    return false;
            }

        }

        public void ConfirmChanges(string name, int nationID, string type, int composition, int experience, string weaponDescription, string weapons, int armourClass, int basePoints, Unit selectedUnit, int ruleID, ObservableCollection<Unit> unitsList)
        {
            if (String.IsNullOrWhiteSpace(name))
                return;

            Unit UnitWithThisname = unitsList.FirstOrDefault(unit => unit.Name == name);

            if (selectedUnit == null)
            {
                if (UnitWithThisname != null)
                {
                    if (PromptQuestion("Unit '" + name + "' alredy exist, do you want to override it?"))
                        UpdateUnit(UnitWithThisname, name, nationID, type, composition, experience, weaponDescription, weapons, ruleID, armourClass, basePoints);
                }
                else
                {
                    AddUnit(name, nationID, type, composition, experience, weaponDescription, weapons, ruleID, armourClass, basePoints);
                }
            }
            else
            {
                if (selectedUnit.Name == name)
                {
                    UpdateUnit(selectedUnit, name, nationID, type, composition, experience, weaponDescription, weapons, ruleID, armourClass, basePoints);
                }
                else if (UnitWithThisname != null)
                {
                    if (PromptQuestion("Unit '" + name + "' alredy exist, do you want to override it?"))
                        UpdateUnit(UnitWithThisname, name, nationID, type, composition, experience, weaponDescription, weapons, ruleID, armourClass, basePoints);
                }
                else
                {
                    if (PromptQuestion("Do you wish to update name of choosen unit? \nOtherwise new unit with given name will by added."))
                        UpdateUnit(selectedUnit, name, nationID, type, composition, experience, weaponDescription, weapons, ruleID, armourClass, basePoints);
                    else
                        AddUnit(name, nationID, type, composition, experience, weaponDescription, weapons, ruleID, armourClass, basePoints);
                }
            }
        }

        #endregion
    }
}
