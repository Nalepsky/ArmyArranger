using ArmyArranger.Global;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ArmyArranger.Models
{
    class AddUnitOptionModel
    {
        #region Properties

        public GameRule EmptyRule = new GameRule();
        public Weapon EmptyWeapon = new Weapon();
        public UnitOption EmptyUnitOption = new UnitOption();
        public UnitOption lastChoosenOption;
        public int UnitID;

        #endregion

        #region Constructors

        public AddUnitOptionModel(int unitID)
        {
            this.UnitID = unitID;
        }

        #endregion

        #region Actions

        public void AddUnitOption(string description, int count, int cost, int weaponID, int ruleID, int unitID)
        {
            try
            {
                EmptyUnitOption.CreateNewAndSaveToDB(description, count, cost, weaponID, ruleID, unitID);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            MessageBox.Show("Successfully added");
        }

        public void UpdateUnitOption(UnitOption selectedUnitOption, string description, int count, int cost, int weaponID, int ruleID, int unitID)
        {
            if (selectedUnitOption == null)
                return;

            try
            {
                selectedUnitOption.UpdateInDB(description, count, cost, weaponID, ruleID, unitID);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            MessageBox.Show("Successfully updated");
        }

        public void RemoveOption(UnitOption selectedOption)
        {
            if (selectedOption == null)
                return;


            try
            {
                selectedOption.Remove();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            MessageBox.Show("Successfully removed");
        }

        public bool ChosenEqualsSelected(UnitOption SelectedOption)
        {
            if (lastChoosenOption != SelectedOption)
            {
                lastChoosenOption = SelectedOption;
                return true;
            }
            return false;
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

        public void ConfirmChanges(string description, int count, int cost, int weaponID, int ruleID, int unitID, UnitOption selectedUnitOption, ObservableCollection<UnitOption> optionsList)
        {
            if (String.IsNullOrWhiteSpace(description))
                return;

            UnitOption OptionWithThisDescr = optionsList.FirstOrDefault(unitOption => unitOption.Description == description);

            if (selectedUnitOption == null)
            {
                if (OptionWithThisDescr != null)
                {
                    if (PromptQuestion("Option with this description alredy exist, do you want to override it?"))
                        UpdateUnitOption(OptionWithThisDescr, description, count, cost, weaponID, ruleID, unitID);
                }
                else
                {
                    AddUnitOption(description, count, cost, weaponID, ruleID, unitID);
                }
            }
            else
            {
                if (selectedUnitOption.Description == description)
                {
                    UpdateUnitOption(selectedUnitOption, description, count, cost, weaponID, ruleID, unitID);
                }
                else if (OptionWithThisDescr != null)
                {
                    if (PromptQuestion("Option with this description alredy exist, do you want to override it?"))
                        UpdateUnitOption(OptionWithThisDescr, description, count, cost, weaponID, ruleID, unitID);
                }
                else
                {
                    if (PromptQuestion("Do you wish to update description of choosen option? \nOtherwise new option with given description  will be added."))
                        UpdateUnitOption(selectedUnitOption, description, count, cost, weaponID, ruleID, unitID);
                    else
                        AddUnitOption(description, count, cost, weaponID, ruleID, unitID);
                }
            }
        }

        #endregion
    }
}
