﻿using System.Windows.Input;
using Prism.Commands;
using ArmyArranger.ViewModels.Menu;
using Prism.Mvvm;
using ArmyArranger.Global;
using System.Collections.ObjectModel;
using ArmyArranger.Models;

namespace ArmyArranger.ViewModels.EditYourArmies
{
    class AddRulesViewModel : BindableBase
    {
        #region Propeties

        AddRulesModel thisModel = new AddRulesModel();

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

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged(nameof(Name));
            }
        }

        private string _source;
        public string Source
        {
            get { return _source; }
            set
            {
                _source = value;
                RaisePropertyChanged(nameof(Source));
            }
        }

        private string _type;
        public string Type
        {
            get { return _type; }
            set
            {
                _type = value;
                RaisePropertyChanged(nameof(Type));
            }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                RaisePropertyChanged(nameof(Description));
            }
        }

        private string _confirmButtonText;
        public string ConfirmButtonText
        {
            get { return _confirmButtonText; }
            set
            {
                _confirmButtonText = value;
                RaisePropertyChanged(nameof(ConfirmButtonText));
            }
        }

        #endregion

        #region Commands

        public ICommand OnLoad { get; set; }
        public ICommand MouseClick { get; set; }
        public ICommand AddNew { get; set; }
        public ICommand Back { get; set; }
        public ICommand Remove { get; set; }
        public ICommand Confirm { get; set; }

        #endregion

        #region Constructors

        public AddRulesViewModel()
        {
            OnLoad = new DelegateCommand(FunctionOnLoad);
            MouseClick = new DelegateCommand(FunctionOnClick);
            AddNew = new DelegateCommand(PrepareToAddNew);
            Back = new DelegateCommand(ChangeViewToEditYourArmies);
            Remove = new DelegateCommand(RemoveSelectedRule);
            Confirm = new DelegateCommand(ConfirmChanges);
        }

        #endregion

        #region Actions

        private void FunctionOnLoad()
        {
            ConfirmButtonText = "Save New";
            thisModel.EmptyRule.LoadAll();
            RulesList = GameRule.RulesCollection;
        }

        private void FunctionOnClick()
        {
            if (thisModel.ChosenEqualsSelected(SelectedRule) && SelectedRule != null)
            {
                Name = SelectedRule.Name;
                Description = SelectedRule.Description;
                Type = SelectedRule.Type;
                Source = SelectedRule.Source;

                ConfirmButtonText = "Update";
            }
        }

        private void PrepareToAddNew()
        {
            Name = "";
            Description = "";
            Type = "";
            Source = "";            
            ConfirmButtonText = "Save New";
            thisModel.ClearRules();
            SelectedRule = null;
        }

        private void ChangeViewToEditYourArmies()
        {
            thisModel.EmptyRule.ClearRulesCollection();
            App.Current.MainWindow.DataContext = new EditYourArmiesViewModel();
        }

        private void RemoveSelectedRule()
        {
            thisModel.RemoveRule(SelectedRule);
            PrepareToAddNew();
        }

        private void ConfirmChanges() 
        {
            thisModel.ConfirmChanges(Name, Description, Type, Source, SelectedRule, RulesList);
            if (SelectedRule != null)
            {
                Name = SelectedRule.Name;
                Description = SelectedRule.Description;
                Type = SelectedRule.Type;
                Source = SelectedRule.Source;
            }
            else
            {
                PrepareToAddNew();
            }
        }

        #endregion
    }
}
