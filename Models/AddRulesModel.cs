using ArmyArranger.Global;
using System.Collections.ObjectModel;
using System.Windows;



namespace ArmyArranger.Models
{
    class AddRulesModel
    {
        #region Properties

        public GameRule EmptyGameRule = new GameRule();
        public GameRule lastChoosenRule;

        private GameRule _selectedRule = new GameRule();
        public GameRule SelectedRule
        {
            get { return _selectedRule; }
            set
            {
                _selectedRule = value;
                //RaisePropertyChanged(nameof(SelectedRule));
            }
        }

        #endregion

        #region Constructors

        public AddRulesModel()
        {

        }
        
#endregion

        #region Actions   

        public bool ChosenEqualsSelected()
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
            SelectedRule = EmptyGameRule;
            lastChoosenRule = EmptyGameRule;
        }

        public int SaveModeCheckbox()
        { 
                MessageBoxResult result = MessageBox.Show("Would you like to override Rule (yes) or to save as new (No)?", "Save", MessageBoxButton.YesNoCancel);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        MessageBox.Show("Rule changed!", "Save");
                        return 1;
                    case MessageBoxResult.No:
                        return -1;
                    default:
                        return 0;
                }
        }

        public int OverrideCheckbox(string RuleName)
        {
            MessageBoxResult result = MessageBox.Show("Rule: '"+RuleName+"' alredy exist, do you want to override it?", "Save", MessageBoxButton.YesNoCancel);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    MessageBox.Show("Rule changed!", "Save");
                    return 1;
                default:
                    return 0;
            }

        }

        public void ConfirmChanges(string Name, string Description, string Type, string Source, ObservableCollection<GameRule> RulesList) //this whole code will go to Model
        {
            if (SelectedRule == null || SelectedRule.isEmpty || SelectedRule.Name != Name) //new rule, or overriding unselected rule
            {
                bool flag = true;
                foreach (GameRule x in RulesList) //check if rule with this name alredy exist
                {
                    if (x.Name == Name)
                    {
                        if (OverrideCheckbox(Name) == 1)
                        {
                            SelectedRule = x;
                            SelectedRule.UpdateInDB(Name, Description, Type, Source);
                            flag = false;
                            break;
                        }
                    }
                }
                if (flag)
                    EmptyGameRule.CreateNewAndSaveToDB(Name, Description, Type, Source);
            }
            else
            {
                int saveOption = SaveModeCheckbox();
                if (saveOption == 1)
                    SelectedRule.UpdateInDB(Name, Description, Type, Source);
                else 
                {
                    MessageBox.Show("Please, enter new name: <text input will be added here, I hope...>", "Save");
                    //EmptyGameRule.CreateNewAndSaveToDB(Name, Description, Type, Source);
                }
            }
        }

        #endregion
    }
}
