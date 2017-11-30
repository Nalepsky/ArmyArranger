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
        GameRule GameRule = new GameRule();
        #region Propeties

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

        #endregion

        #region Commands

        public ICommand OnLoad { get; set; }
        public ICommand Back { get; set; }
        public ICommand Confirm { get; set; }


        #endregion

        #region Constructors

        public AddRulesViewModel()
        {
            OnLoad = new DelegateCommand(FunctionOnLoad);
            Back = new DelegateCommand(ChangeViewToEditYourArmies);
            Confirm = new DelegateCommand(ConfirmChanges);
        }

        #endregion

        #region Actions

        private void FunctionOnLoad()
        {
            GameRule.LoadAll();
            RulesList = GameRule.RulesCollection;
        }

        private void ChangeViewToEditYourArmies()
        {
            App.Current.MainWindow.DataContext = new EditYourArmiesViewModel();
        }

        private void ConfirmChanges()
        {
            GameRule.SaveToDB(Name, Description, Type, Source);
        }

        #endregion
    }
}
