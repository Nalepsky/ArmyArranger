using ArmyArranger.Global;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;



namespace ArmyArranger.Models
{
    class AddNationsModel
    {
        #region Properties

        public Nation EmptyNation = new Nation();
        public Nation lastChoosenNation;


        #endregion

        #region Constructors

        public AddNationsModel()
        {

        }

        #endregion

        #region Actions

        public void AddNation(string name)
        {
            try
            {
                EmptyNation.CreateNewAndSaveToDB(name);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            MessageBox.Show("Successfully added");
        }

        public void UpdateNation(Nation SelectedNation, string name)
        {
            if (SelectedNation == null)
                return;


            try
            {
                SelectedNation.UpdateInDB(name);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            MessageBox.Show("Successfully updated");
        }

        public void RemoveNation(Nation SelectedNation)
        {
            if (SelectedNation == null)
                return;


            try
            {
                SelectedNation.Remove();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            MessageBox.Show("Successfully removed");
        }


        public bool ChosenEqualsSelected(Nation SelectedNation)
        {
            if (lastChoosenNation != SelectedNation)
            {
                lastChoosenNation = SelectedNation;
                return true;
            }
            return false;
        }

        public void ClearNations()
        {
            lastChoosenNation = EmptyNation;
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

        public void ConfirmChanges(string Name, Nation SelectedNation, ObservableCollection<Nation> NationsList)
        {
            if (String.IsNullOrWhiteSpace(Name))
                return;

            Nation NationWithThisName = NationsList.FirstOrDefault(nation => nation.Name == Name);


            if (SelectedNation == null)
            {
                if (NationWithThisName != null)
                {
                    if (PromptQuestion("Nation '" + Name + "' alredy exist, do you want to override it?"))
                        UpdateNation(NationWithThisName, Name);
                }
                else
                {
                    AddNation(Name);
                }
            }
            else
            {
                if (SelectedNation.Name == Name)
                {
                    UpdateNation(SelectedNation, Name);
                }
                else if (NationWithThisName != null)
                {
                    if (PromptQuestion("Nation '" + Name + "' alredy exist, do you want to override it?"))
                        UpdateNation(NationWithThisName, Name);
                }
                else
                {
                    if (PromptQuestion("Do you wish to update name of choosen nation? \nOtherwise new nation with given name  will be added."))
                        UpdateNation(SelectedNation, Name);
                    else
                        AddNation(Name);
                }
            }


        }

        #endregion
    }
}
