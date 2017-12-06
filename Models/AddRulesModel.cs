using System.Windows;



namespace ArmyArranger.Models
{
    class AddRulesModel
    {
        public int SaveModeCheckbox()
        { 
                MessageBoxResult result = MessageBox.Show("Would you like to override Rule (yes) or to save as new (No)?", "Save", MessageBoxButton.YesNoCancel);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        MessageBox.Show("Rule changed!", "Save");
                        return 1;
                    case MessageBoxResult.No:
                        MessageBox.Show("Rule with that name alredy exist, please create new name!", "Save");
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
    }
}
