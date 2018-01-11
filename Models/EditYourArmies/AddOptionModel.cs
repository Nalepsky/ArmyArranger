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
    class AddOptionModel
    {
        #region Properties

        public GameRule EmptyRule = new GameRule();
        public Weapon EmptyWeapon = new Weapon();
        public Option EmptyOption = new Option();
        public Option lastChoosenOption;
        public int UnitID;

        #endregion

        #region Constructors

        public AddOptionModel(int unitID)
        {
            this.UnitID = unitID;
        }

        #endregion

        #region Actions

        public void AddOption(string description, int count, int cost, int weaponID, int ruleID, int unitID)
        {
            try
            {
                EmptyOption.CreateNewAndSaveToDB(description, count, cost, weaponID, ruleID, unitID);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            MessageBox.Show("Successfully added");
        }

        public void UpdateOption(Option selectedOption, string description, int count, int cost, int weaponID, int ruleID, int unitID)
        {
            if (selectedOption == null)
                return;

            try
            {
                selectedOption.UpdateInDB(description, count, cost, weaponID, ruleID, unitID);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            MessageBox.Show("Successfully updated");
        }

        public void RemoveOption(Option selectedOption)
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

        public bool ChosenEqualsSelected(Option SelectedOption)
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

        public void ConfirmChanges(string description, int count, int cost, int weaponID, int ruleID, int unitID, Option selectedOption, ObservableCollection<Option> optionsList)
        {
            if (String.IsNullOrWhiteSpace(description))
                return;

            Option OptionWithThisDescr = optionsList.FirstOrDefault(option => option.Description == description);

            if (selectedOption == null)
            {
                if (OptionWithThisDescr != null)
                {
                    if (PromptQuestion("Option with this description alredy exist, do you want to override it?"))
                        UpdateOption(OptionWithThisDescr, description, count, cost, weaponID, ruleID, unitID);
                }
                else
                {
                    AddOption(description, count, cost, weaponID, ruleID, unitID);
                }
            }
            else
            {
                if (selectedOption.Description == description)
                {
                    UpdateOption(selectedOption, description, count, cost, weaponID, ruleID, unitID);
                }
                else if (OptionWithThisDescr != null)
                {
                    if (PromptQuestion("Option with this description alredy exist, do you want to override it?"))
                        UpdateOption(OptionWithThisDescr, description, count, cost, weaponID, ruleID, unitID);
                }
                else
                {
                    if (PromptQuestion("Do you wish to update description of choosen option? \nOtherwise new option with given description  will be added."))
                        UpdateOption(selectedOption, description, count, cost, weaponID, ruleID, unitID);
                    else
                        AddOption(description, count, cost, weaponID, ruleID, unitID);
                }
            }
        }

        #endregion
    }
}
