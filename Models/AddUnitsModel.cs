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

        public void AddUnit(string name, int nationID, string type, string composition, int experience, string weaponDescription, int armourClass, int basePoints, ObservableCollection<GameRule> rulesList)
        {
            try
            {
                EmptyUnit.CreateNewAndSaveToDB(name, nationID, type, composition, experience, weaponDescription, armourClass, basePoints, rulesList);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            MessageBox.Show("Successfully added");
        }

        public void UpdateUnit(Unit selectedUnit, string name, int nationID, string type, string composition, int experience, string weaponDescription, int armourClass, int basePoints, ObservableCollection<GameRule> rulesList)
        {
            if (selectedUnit == null)
                return;


            try
            {
                selectedUnit.UpdateInDB(name, nationID, type, composition, experience, weaponDescription, armourClass, basePoints, rulesList);
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
            foreach (GameRule tempRule in GameRule.RulesCollection)
            {
                tempRule.IsSelected = false;
            }
        }

        public void CheckActiveRules(Unit selectedUnit)
        {
            foreach (GameRule tempRule in GameRule.RulesCollection)
            {
                if (selectedUnit.ListOfActiveRules.Contains(tempRule.ID))
                    tempRule.IsSelected = true;
                else
                    tempRule.IsSelected = false;
            }
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

        public void ConfirmChanges(string name, int nationID, string type, string composition, int experience, string weaponDescription, int armourClass, int basePoints, Unit selectedUnit, ObservableCollection<Unit> unitsList, ObservableCollection<GameRule> selectedRulesList)
        {
            if (String.IsNullOrWhiteSpace(name))
                return;

            Unit UnitWithThisName = unitsList.FirstOrDefault(unit => unit.Name == name);

            if (selectedUnit == null)
            {
                if (UnitWithThisName != null)
                {
                    if (PromptQuestion("Unit '" + name + "' alredy exist, do you want to override it?"))
                        UpdateUnit(UnitWithThisName, name, nationID, type, composition, experience, weaponDescription, armourClass, basePoints, selectedRulesList);
                }
                else
                {
                    AddUnit(name, nationID, type, composition, experience, weaponDescription, armourClass, basePoints, selectedRulesList);
                }
            }
            else
            {
                if (selectedUnit.Name == name)
                {
                    UpdateUnit(selectedUnit, name, nationID, type, composition, experience, weaponDescription, armourClass, basePoints, selectedRulesList);
                }
                else if (UnitWithThisName != null)
                {
                    if (PromptQuestion("Unit '" + name + "' alredy exist, do you want to override it?"))
                        UpdateUnit(UnitWithThisName, name, nationID, type, composition, experience, weaponDescription, armourClass, basePoints, selectedRulesList);
                }
                else
                {
                    if (PromptQuestion("Do you wish to update name of choosen unit? \nOtherwise new unit with given name will by added."))
                        UpdateUnit(selectedUnit, name, nationID, type, composition, experience, weaponDescription, armourClass, basePoints, selectedRulesList);
                    else
                        AddUnit(name, nationID, type, composition, experience, weaponDescription, armourClass, basePoints, selectedRulesList);
                }
            }
        }

        #endregion
    }
}
