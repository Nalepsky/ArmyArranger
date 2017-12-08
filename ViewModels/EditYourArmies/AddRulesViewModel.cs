using System;
﻿using System.Windows.Input;
using Prism.Commands;
using ArmyArranger.ViewModels.Menu;
using System.Windows;
using Prism.Mvvm;
using ArmyArranger.Global;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using ArmyArranger.Models;

namespace ArmyArranger.ViewModels.EditYourArmies
{
    class AddRulesViewModel : BindableBase
    {
        #region Propeties

        AddRulesModel AddRules_Model = new AddRulesModel();

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
            AddRules_Model.EmptyGameRule.LoadAll();
            RulesList = GameRule.RulesCollection;
        }

        private void FunctionOnClick()
        {
            if (AddRules_Model.ChosenEqualsSelected(SelectedRule))
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

            AddRules_Model.ClearRules();
        }

        private void ChangeViewToEditYourArmies()
        {
            AddRules_Model.EmptyGameRule.ClearRulesCollection();
            App.Current.MainWindow.DataContext = new EditYourArmiesViewModel();
        }

        private void RemoveSelectedRule()
        {
            AddRules_Model.RemoveRule(SelectedRule);
        }

        private void ConfirmChanges() 
        {
            AddRules_Model.ConfirmChanges(Name, Description, Type, Source, SelectedRule, RulesList);
            Name = SelectedRule.Name;
            Description = SelectedRule.Description;
            Type = SelectedRule.Type;
            Source = SelectedRule.Source;
        }

        #endregion
    }
}
