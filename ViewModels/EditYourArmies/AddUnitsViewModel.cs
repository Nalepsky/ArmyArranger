using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;
using ArmyArranger.ViewModels.Menu;
using ArmyArranger.Models;
using System.Collections.ObjectModel;
using ArmyArranger.Global;

namespace ArmyArranger.ViewModels.EditYourArmies
{
    class AddUnitsViewModel : BindableBase
    {

        #region Propeties

        AddUnitsModel thisModel = new AddUnitsModel();
        AddWeaponsModel WeaponsModel = new AddWeaponsModel();
        AddRulesModel rulesModel = new AddRulesModel();

        private ObservableCollection<GameRule> _rulesList;
        public ObservableCollection<GameRule> RulesList
        {
            get { return _rulesList; }
            set
            {
                _rulesList = value;
                RaisePropertyChanged(nameof(RulesList));
            }
        }

        private GameRule _selectedRule;
        public GameRule SelectedRule
        {
            get { return _selectedRule; }
            set
            {
                _selectedRule = value;
                RaisePropertyChanged(nameof(SelectedRule));
            }
        }

        #endregion

        #region Commands

        public ICommand OnLoad { get; set; }
        public ICommand Back { get; set; }
        public ICommand Next { get; set; }

        #endregion

        #region Constructors

        public AddUnitsViewModel()
        {
            OnLoad = new DelegateCommand(FunctionOnLoad);
            Back = new DelegateCommand(ChangeViewToEditYourArmies);
            Next = new DelegateCommand(ChangeViewToAddOptions);
        }

        #endregion

        #region Actions       

        private void ChangeViewToEditYourArmies()
        {
            App.Current.MainWindow.DataContext = new EditYourArmiesViewModel();
        }

        private void ChangeViewToAddOptions()
        {
            App.Current.MainWindow.DataContext = new AddOptionsViewModel();
        }

        private void FunctionOnLoad()
        {
            thisModel.EmptyRule.LoadAll("Unit");
            RulesList = GameRule.RulesCollection;
        }

        #endregion
    }
}