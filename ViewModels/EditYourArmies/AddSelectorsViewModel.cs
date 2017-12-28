using System;
using System.Windows.Input;
using Prism.Commands;
using ArmyArranger.ViewModels.Menu;
using ArmyArranger.Models;
using System.Collections.ObjectModel;
using ArmyArranger.Global;
using Prism.Mvvm;
using ArmyArranger.Views.EditYourArmies;
using System.Windows;

namespace ArmyArranger.ViewModels.EditYourArmies
{
    class AddSelectorsViewModel : BindableBase
    {
        #region Propeties

        AddSelectorsModel thisModel = new AddSelectorsModel();
        AddRulesModel rulesModel = new AddRulesModel();

        private ObservableCollection<Selector> _selectorsList;
        public ObservableCollection<Selector> SelectorsList
        {
            get { return _selectorsList; }
            set
            {
                _selectorsList = value;
                RaisePropertyChanged(nameof(SelectorsList));
            }
        }

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

        private ObservableCollection<Nation> _nationsList;
        public ObservableCollection<Nation> NationsList
        {
            get { return _nationsList; }
            set
            {
                _nationsList = value;
                RaisePropertyChanged(nameof(NationsList));
            }
        }

        private ObservableCollection<Nation> _nationsComboBox;
        public ObservableCollection<Nation> NationsComboBox
        {
            get { return _nationsComboBox; }
            set
            {
                _nationsComboBox = value;
                RaisePropertyChanged(nameof(NationsComboBox));
            }
        }

        private string _ruleName;
        public string RuleName
        {
            get { return _ruleName; }
            set
            {
                _ruleName = value;
                RaisePropertyChanged(nameof(RuleName));
            }
        }

        #endregion

        #region Commands

        public ICommand OnLoad { get; set; }
        public ICommand Back { get; set; }
        public ICommand AddNextMandatory { get; set; }

        #endregion

        #region Constructors

        public AddSelectorsViewModel()
        {
            OnLoad = new DelegateCommand(FunctionOnLoad);
            Back = new DelegateCommand(ChangeViewToEditYourArmies);
            AddNextMandatory = new DelegateCommand(OpenEntryWindow);
        }

        #endregion

        #region Actions

        private void FunctionOnLoad()
        {
            //ConfirmButtonText = "Save New";
            thisModel.EmptyNation.LoadAll();
            NationsList = Nation.NationsCollecion;
            NationsComboBox = NationsList;
            thisModel.EmptySelector.LoadAll();            
            SelectorsList = Selector.SelectorsCollection;
            thisModel.EmptyRule.LoadAll("Selector");
            RulesList = GameRule.RulesCollection;
        }

        private void ChangeViewToEditYourArmies()
        {
            thisModel.EmptyNation.ClearNationsCollecion();
            thisModel.EmptyRule.ClearRulesCollection();
            
            App.Current.MainWindow.DataContext = new EditYourArmiesViewModel();
        }

        private void OpenEntryWindow()
        {
            Window EntryWindow = new AddEntryWindow();
            EntryWindow.Show();            
        }

        #endregion
    }
}
