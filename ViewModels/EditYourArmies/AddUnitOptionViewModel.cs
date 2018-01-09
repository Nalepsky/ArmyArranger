﻿using ArmyArranger.Global;
using ArmyArranger.Models;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ArmyArranger.ViewModels.EditYourArmies
{
    class AddUnitOptionViewModel : BindableBase
    {
        #region Properties

        AddUnitOptionModel thisModel;

        private ObservableCollection<UnitOption> _optionsList;
        public ObservableCollection<UnitOption> OptionsList
        {
            get { return _optionsList; }
            set
            {
                _optionsList = value;
                RaisePropertyChanged(nameof(OptionsList));
            }
        }

        private ObservableCollection<GameRule> _possibleRulesList;
        public ObservableCollection<GameRule> PossibleRulesList
        {
            get { return _possibleRulesList; }
            set
            {
                _possibleRulesList = value;
                RaisePropertyChanged(nameof(PossibleRulesList));
            }
        }

        private ObservableCollection<Weapon> _possibleWeaponsList;
        public ObservableCollection<Weapon> PossibleWeaponsList
        {
            get { return _possibleWeaponsList; }
            set
            {
                _possibleWeaponsList = value;
                RaisePropertyChanged(nameof(PossibleWeaponsList));
            }
        }

        private UnitOption _selectedOption;
        public UnitOption SelectedOption
        {
            get { return _selectedOption; }
            set
            {
                _selectedOption = value;
                RaisePropertyChanged(nameof(SelectedOption));
            }
        }

        private Weapon _selectedPossibleWeapon;
        public Weapon SelectedPossibleWeapon
        {
            get { return _selectedPossibleWeapon; }
            set
            {
                _selectedPossibleWeapon = value;
                RaisePropertyChanged(nameof(SelectedPossibleWeapon));
            }
        }

        private GameRule _selectedPossibleRule;
        public GameRule SelectedPossibleRule
        {
            get { return _selectedPossibleRule; }
            set
            {
                _selectedPossibleRule = value;
                RaisePropertyChanged(nameof(SelectedPossibleRule));
            }
        }

        private String _describtion;
        public String Describtion
        {
            get { return _describtion; }
            set
            {
                _describtion = value;
                RaisePropertyChanged(nameof(Describtion));
            }
        }

        private String _selectedAddition;
        public String SelectedAddition
        {
            get { return _selectedAddition; }
            set
            {
                _selectedAddition = value;
                RaisePropertyChanged(nameof(SelectedAddition));
            }
        }

        private int _cost;
        public int Cost
        {
            get { return _cost; }
            set
            {
                _cost = value;
                RaisePropertyChanged(nameof(Cost));
            }
        }

        private int _maxNumber;
        public int MaxNumber
        {
            get { return _maxNumber; }
            set
            {
                _maxNumber = value;
                RaisePropertyChanged(nameof(MaxNumber));
            }
        }

        #endregion

        #region Commands
        public ICommand OnLoad { get; set; }
        public ICommand MouseClick { get; set; }
        public ICommand Cancel { get; set; }
        public ICommand Confirm { get; set; }
        public ICommand AddNewOption { get; set; }

        #endregion

        #region Constructors

        public AddUnitOptionViewModel(int id)
        {
            thisModel = new AddUnitOptionModel(id);

            OnLoad = new DelegateCommand(FunctionOnLoad);
            MouseClick = new DelegateCommand(FunctionOnClick);
            Cancel = new DelegateCommand(FunctionCancel);
            Confirm = new DelegateCommand(FunctionConfirm);
            AddNewOption = new DelegateCommand(FunctionAddNewOption);
        }

        #endregion

        #region Actions
        private void FunctionConfirm()
        {
            //throw new NotImplementedException();
        }

        private void FunctionCancel()
        {
            //throw new NotImplementedException();
        }

        private void FunctionOnClick()
        {
            //throw new NotImplementedException();
        }

        private void FunctionAddNewOption()
        {
            SelectedOption = null;
            SelectedPossibleRule = null;
            SelectedPossibleWeapon = null;
            Describtion = "";
            Cost = 0;
            MaxNumber = 1;
        }

        private void FunctionOnLoad()
        {
            PossibleRulesList = GameRule.RulesCollection;
            PossibleWeaponsList = Weapon.WeaponsCollection;
            OptionsList = thisModel.LoadOptions();

            Describtion = "";
            Cost = 0;
            MaxNumber = 1;
        }
        #endregion
    }
}
