using System;
﻿using System.Windows.Input;
using Prism.Commands;
using ArmyArranger.ViewModels.Menu;
using System.Windows;
using Prism.Mvvm;
using ArmyArranger.Global;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace ArmyArranger.ViewModels.EditYourArmies
{
    class AddRulesViewModel : BindableBase
    {
        #region Propeties

        GameRule EmptyGameRule = new GameRule();
        GameRule lastChoosenRule;

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
        public ICommand Confirm { get; set; }


        #endregion

        #region Constructors

        public AddRulesViewModel()
        {
            OnLoad = new DelegateCommand(FunctionOnLoad);
            MouseClick = new DelegateCommand(FunctionOnClick);
            AddNew = new DelegateCommand(PrepareToAddNew);
            Back = new DelegateCommand(ChangeViewToEditYourArmies);
            Confirm = new DelegateCommand(ConfirmChanges);
        }

        #endregion

        #region Actions

        private void FunctionOnLoad()
        {
            ConfirmButtonText = "Confirm";
            EmptyGameRule.LoadAll();
            RulesList = GameRule.RulesCollection;
        }

        private void FunctionOnClick()
        {
            if (lastChoosenRule != SelectedRule)
            {
                Name = SelectedRule.Name;
                Description = SelectedRule.Description;
                Type = SelectedRule.Type;
                Source = SelectedRule.Source;

                ConfirmButtonText = "Update";

                lastChoosenRule = SelectedRule;
            }
        }

        private void PrepareToAddNew()
        {
            Name = "";
            Description = "";
            Type = "";
            Source = "";
            SelectedRule = EmptyGameRule;
            lastChoosenRule = EmptyGameRule;

            ConfirmButtonText = "Confirm";
        }

        private void ChangeViewToEditYourArmies()
        {
            EmptyGameRule.ClearRulesCollection();
            App.Current.MainWindow.DataContext = new EditYourArmiesViewModel();
        }

        private void ConfirmChanges()
        {
            if (SelectedRule == null || SelectedRule.isEmpty)
            {
                EmptyGameRule.CreateNewAndSaveToDB(Name, Description, Type, Source);
            }
            else
                SelectedRule.UpdateInDB(Name, Description, Type, Source);
        }

        #endregion
    }
}
