using ArmyArranger.Global;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;



namespace ArmyArranger.Models 
{
    class AddRulesModel
    {
        #region Properties

        public GameRule EmptyGameRule = new GameRule();
        public GameRule lastChoosenRule;


        #endregion

        #region Constructors

        public AddRulesModel()
        {

        }

        #endregion

        #region Actions   

        public void AddRule(string name, string description, string type, string source)
        {
            try
            {
                EmptyGameRule.CreateNewAndSaveToDB(name, description, type, source);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            MessageBox.Show("Successfully added");
        }

        public void UpdateRule(GameRule SelectedRule, string name, string description, string type, string source)
        {
            if (SelectedRule == null)
                return;
            if((name == SelectedRule.Name) && (description == SelectedRule.Description) && (type == SelectedRule.Type) && (source == SelectedRule.Source))
                return;


            try
            {
                SelectedRule.UpdateInDB(name, description, type, source);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            MessageBox.Show("Successfully updated");
        }

        public void RemoveRule(GameRule SelectedRule)
        {
            if (SelectedRule == null)
                return;


            try
            {
                SelectedRule.Remove();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            MessageBox.Show("Successfully removed");
        }


        public bool ChosenEqualsSelected(GameRule SelectedRule)
        {
            if(lastChoosenRule != SelectedRule)
            {
                lastChoosenRule = SelectedRule;
                return true;
            }
            return false;
        }

        public void ClearRules()
        {
            lastChoosenRule = EmptyGameRule;
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

        public void ConfirmChanges(string Name, string Description, string Type, string Source, GameRule SelectedRule, ObservableCollection<GameRule> RulesList)
        {
            if (String.IsNullOrWhiteSpace(Name))
                return;


            if(SelectedRule == null)
            {
                AddRule(Name, Description, Type, Source);
                return;
            }
            else if(SelectedRule.Name == Name)
            {
                UpdateRule(SelectedRule, Name, Description, Type, Source);
            }
            else
            {
                GameRule RuleWithThisName = RulesList.FirstOrDefault(rule => rule.Name == Name);
                if (RuleWithThisName != null)
                {
                    if (PromptQuestion("Rule '" + Name + "' alredy exist, do you want to override it?"))
                    {
                        UpdateRule(RuleWithThisName, Name, Description, Type, Source);
                    }
                }
                else
                {
                    if (PromptQuestion("Do you wish to update name of choosen rule? \nOtherwise new rule with given name will by added."))
                    {
                        UpdateRule(SelectedRule, Name, Description, Type, Source);
                    }
                    else
                    {
                        EmptyGameRule.CreateNewAndSaveToDB(Name, Description, Type, Source);
                    }
                }
            }
        }

        #endregion
    }
}
