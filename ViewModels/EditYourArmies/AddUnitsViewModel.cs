﻿using System.Windows.Input;
using Prism.Commands;
using ArmyArranger.ViewModels.Menu;
using Prism.Mvvm;
using ArmyArranger.Global;
using System.Collections.ObjectModel;
using ArmyArranger.Models;
using System;
using System.Linq;
using System.Windows;
using ArmyArranger.Views;

namespace ArmyArranger.ViewModels.EditYourArmies
{
    class AddUnitsViewModel : BindableBase
    {
        #region Propeties

        AddUnitsModel thisModel = new AddUnitsModel();

        private ObservableCollection<Unit> _unitsList;
        public ObservableCollection<Unit> UnitsList
        {
            get { return _unitsList; }
            set
            {
                _unitsList = value;
                RaisePropertyChanged(nameof(UnitsList));
            }
        }

        private Unit _selectedUnit;
        public Unit SelectedUnit
        {
            get { return _selectedUnit; }
            set
            {
                _selectedUnit = value;
                RaisePropertyChanged(nameof(SelectedUnit));
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

        private ObservableCollection<Weapon> _weaponsList;
        public ObservableCollection<Weapon> WeaponsList
        {
            get { return _weaponsList; }
            set
            {
                _weaponsList = value;
                RaisePropertyChanged(nameof(WeaponsList));
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

        public Nation _selectedNation;
        public Nation SelectedNation
        {
            get { return _selectedNation; }
            set
            {
                _selectedNation = value;
                RaisePropertyChanged(nameof(SelectedNation));
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

        private string _composition;
        public string Composition
        {
            get { return _composition; }
            set
            {
                _composition = value;
                RaisePropertyChanged(nameof(Composition));
            }
        }

        private int _experience;
        public int Experience
        {
            get { return _experience; }
            set
            {
                _experience = value;
                RaisePropertyChanged(nameof(Experience));
            }
        }

        private string _weaponDescription;
        public string WeaponDescription
        {
            get { return _weaponDescription; }
            set
            {
                _weaponDescription = value;
                RaisePropertyChanged(nameof(WeaponDescription));
            }
        }

        private int _armourClass;
        public int ArmourClass
        {
            get { return _armourClass; }
            set
            {
                _armourClass = value;
                RaisePropertyChanged(nameof(ArmourClass));
            }
        }

        private int _basePoints;
        public int BasePoints
        {
            get { return _basePoints; }
            set
            {
                _basePoints = value;
                RaisePropertyChanged(nameof(BasePoints));
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
        public ICommand GoToRules { get; set; }
        public ICommand GoToWeapons { get; set; }
        public ICommand Remove { get; set; }
        public ICommand Confirm { get; set; }
        public ICommand AddUnitOption { get; set; }

        #endregion

        #region Constructors

        public AddUnitsViewModel()
        {
            OnLoad = new DelegateCommand(FunctionOnLoad);
            MouseClick = new DelegateCommand(FunctionOnClick);
            AddNew = new DelegateCommand(PrepareToAddNew);
            Back = new DelegateCommand(ChangeViewToEditYourArmies);
            GoToRules = new DelegateCommand(ChangeViewToAddRules);
            GoToWeapons = new DelegateCommand(ChangeViewToAddWeapons);
            Remove = new DelegateCommand(RemoveSelectedUnit);
            Confirm = new DelegateCommand(ConfirmChanges);
            AddUnitOption = new DelegateCommand(OpenAddUnitOptions);
        }

        #endregion

        #region Actions

        private void FunctionOnLoad()
        {
            ConfirmButtonText = "Save New";
            thisModel.EmptyUnit.LoadAll();
            UnitsList = Unit.UnitsCollection;
            thisModel.EmptyRule.LoadAll("Unit");
            RulesList = GameRule.RulesCollection;
            thisModel.EmptyWeapon.LoadAll();
            WeaponsList = Weapon.WeaponsCollection;
            thisModel.EmptyNation.LoadAll();
            NationsList = Nation.NationsCollecion;
        }

        private void OpenAddUnitOptions()
        {
            if (SelectedUnit != null)
            {
                Window OptionsWindow = new AddUnitOptionWindow();
                OptionsWindow.Show();
            }
        }

        private void FunctionOnClick()
        {               
            if (thisModel.ChosenUnitEqualsSelected(SelectedUnit) && SelectedUnit != null)
            {
                Name = SelectedUnit.Name;
                SelectedNation = NationsList.Single(x => x.ID == SelectedUnit.NationID);
                Type = SelectedUnit.Type;
                Composition = SelectedUnit.Composition;
                Experience = SelectedUnit.Experience;
                WeaponDescription = SelectedUnit.WeaponDescription;
                ArmourClass = SelectedUnit.ArmourClass;
                BasePoints = SelectedUnit.BasePoints;
                thisModel.CheckActiveRules(SelectedUnit);
                thisModel.CheckActiveWeapons(SelectedUnit);

                ConfirmButtonText = "Update";
            }
        }

        private void PrepareToAddNew()
        {
            Name = "";
            SelectedNation = null;
            Type = "";
            Composition = "";
            Experience = 0;
            WeaponDescription = null;
            ArmourClass = 0;
            BasePoints = 0;
            thisModel.ClearUnits();
            thisModel.ClearRules();
            thisModel.ClearWeapons();
            SelectedUnit = null;
            ConfirmButtonText = "Save New";
        }

        private void ChangeViewToEditYourArmies()
        {
            thisModel.EmptyUnit.ClearUnitsCollection();
            thisModel.EmptyRule.ClearRulesCollection();
            thisModel.EmptyWeapon.ClearWeaponsCollection();
            thisModel.EmptyNation.ClearNationsCollecion();
            App.Current.MainWindow.DataContext = new EditYourArmiesViewModel();
        }

        private void ChangeViewToAddRules()
        {
            thisModel.EmptyUnit.ClearUnitsCollection();
            thisModel.EmptyRule.ClearRulesCollection();
            thisModel.EmptyWeapon.ClearWeaponsCollection();
            thisModel.EmptyNation.ClearNationsCollecion();
            App.Current.MainWindow.DataContext = new AddRulesViewModel();
        }

        private void ChangeViewToAddWeapons()
        {
            thisModel.EmptyUnit.ClearUnitsCollection();
            thisModel.EmptyRule.ClearRulesCollection();
            thisModel.EmptyWeapon.ClearWeaponsCollection();
            thisModel.EmptyNation.ClearNationsCollecion();
            App.Current.MainWindow.DataContext = new AddWeaponsViewModel();
        }

        private void RemoveSelectedUnit()
        {
            thisModel.RemoveUnit(SelectedUnit);
            Name = "";
            SelectedNation = null;
            Type = "";
            Composition = "";
            Experience = 0;
            WeaponDescription = null;
            ArmourClass = 0;
            BasePoints = 0;
            thisModel.ClearUnits();
            thisModel.ClearRules();
            thisModel.ClearWeapons();
            SelectedUnit = null;
            ConfirmButtonText = "Save New";
        }

        private void ConfirmChanges()
        {
            ObservableCollection<GameRule> SelectedRulesList = new ObservableCollection<GameRule>(RulesList.Where(w => (w.IsSelected == true)));
            ObservableCollection<Weapon> SelectedWeaponsList = new ObservableCollection<Weapon>(WeaponsList.Where(w => (w.IsSelected == true)));
            int SelectedNationID = (SelectedNation != null) ? SelectedNation.ID : 0;

            thisModel.ConfirmChanges(Name, SelectedNationID, Type, Composition, Experience, WeaponDescription, ArmourClass, BasePoints, SelectedUnit, UnitsList, SelectedRulesList, SelectedWeaponsList);
            if (SelectedUnit != null)
            {
                Name = SelectedUnit.Name;
                SelectedNation = NationsList.Single(x => x.ID == SelectedUnit.NationID);
                Type = SelectedUnit.Type;
                Composition = SelectedUnit.Composition;
                Experience = SelectedUnit.Experience;
                WeaponDescription = SelectedUnit.WeaponDescription;
                ArmourClass = SelectedUnit.ArmourClass;
                BasePoints = SelectedUnit.BasePoints;
            }
            else
            {
                Name = "";
                SelectedNation = null;
                Type = "";
                Composition = "";
                Experience = 0;
                WeaponDescription = null;
                ArmourClass = 0;
                BasePoints = 0;
                thisModel.ClearUnits();
                thisModel.ClearRules();
                thisModel.ClearWeapons();
            }
        }

        #endregion
    }
}
