using ArmyArranger.Global;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;




namespace ArmyArranger.Models
{
    class AddRulesModel
    {
        #region Properties

        public GameRule EmptyRule = new GameRule();
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
                EmptyRule.CreateNewAndSaveToDB(name, description, type, source);
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
            if (lastChoosenRule != SelectedRule)
            {
                lastChoosenRule = SelectedRule;
                return true;
            }
            return false;
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

        public void ConfirmChanges(string Name, string Description, string Type, string Source, GameRule SelectedRule, ObservableCollection<GameRule> RulesList)
        {
            if (String.IsNullOrWhiteSpace(Name))
                return;

            GameRule RuleWithThisName = RulesList.FirstOrDefault(rule => rule.Name == Name);

            if (SelectedRule == null)
            {
                if (RuleWithThisName != null)
                {
                    if (PromptQuestion("Rule '" + Name + "' alredy exist, do you want to override it?"))
                        UpdateRule(RuleWithThisName, Name, Description, Type, Source);
                }
                else
                {
                    AddRule(Name, Description, Type, Source);
                }
            }
            else
            {
                if (SelectedRule.Name == Name)
                {
                    UpdateRule(SelectedRule, Name, Description, Type, Source);
                }
                else if (RuleWithThisName != null)
                {
                    if (PromptQuestion("Rule '" + Name + "' alredy exist, do you want to override it?"))
                        UpdateRule(RuleWithThisName, Name, Description, Type, Source);
                }
                else
                {
                    if (PromptQuestion("Do you wish to update name of choosen rule? \nOtherwise new rule with given name will by added."))
                        UpdateRule(SelectedRule, Name, Description, Type, Source);
                    else
                        AddRule(Name, Description, Type, Source);
                }
            }
        }

        #endregion
    }
}
