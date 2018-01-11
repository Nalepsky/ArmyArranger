using System.Windows.Input;
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
 
        private int _inexperienced;
        public int Inexperienced
        {
            get { return _inexperienced; }
            set
            {
                _inexperienced = value;
                RaisePropertyChanged(nameof(Inexperienced));
            }
        }

        private int _regular;
        public int Regular
        {
            get { return _regular; }
            set
            {
                _regular = value;
                RaisePropertyChanged(nameof(Regular));
            }
        }

        private int _veteran;
        public int Veteran
        {
            get { return _veteran; }
            set
            {
                _veteran = value;
                RaisePropertyChanged(nameof(Veteran));
            }
        }

        private int _pointsInexp;
        public int PointsInexp
        {
            get { return _pointsInexp; }
            set
            {
                _pointsInexp = value;
                RaisePropertyChanged(nameof(PointsInexp));
            }
        }

        private int _pointsReg;
        public int PointsReg
        {
            get { return _pointsReg; }
            set
            {
                _pointsReg = value;
                RaisePropertyChanged(nameof(PointsReg));
            }
        }

        private int _pointsVet;
        public int PointsVet
        {
            get { return _pointsVet; }
            set
            {
                _pointsVet = value;
                RaisePropertyChanged(nameof(PointsVet));
            }
        }

        private int _baseSize;
        public int BaseSize
        {
            get { return _baseSize; }
            set
            {
                _baseSize= value;
                RaisePropertyChanged(nameof(BaseSize));
            }
        }

        private int _maxSize;
        public int MaxSize
        {
            get { return _maxSize; }
            set
            {
                _maxSize = value;
                RaisePropertyChanged(nameof(MaxSize));
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
        public ICommand AddOption { get; set; }

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
            AddOption = new DelegateCommand(OpenAddOptions);
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

        private void OpenAddOptions()
        {
            if (SelectedUnit != null)
            {                
                Window OptionsWindow = new AddOptionWindow(SelectedUnit.ID);
                OptionsWindow.Show();
            }
        }

        private void FunctionOnClick()
        {
            if (thisModel.ChosenUnitEqualsSelected(SelectedUnit) && SelectedUnit != null)
            {
                Name = SelectedUnit.Name;
                SelectedNation = NationsList.Where(x => x.ID == SelectedUnit.NationID).FirstOrDefault();
                Type = SelectedUnit.Type;
                Composition = SelectedUnit.Composition;
                WeaponDescription = SelectedUnit.WeaponDescription;
                ArmourClass = SelectedUnit.ArmourClass;
                Inexperienced = SelectedUnit.Inexperienced;
                Regular = SelectedUnit.Regular;
                Veteran = SelectedUnit.Veteran;
                PointsInexp = SelectedUnit.PointsInexp;
                PointsReg = SelectedUnit.PointsReg;
                PointsVet = SelectedUnit.PointsVet;
                BaseSize = SelectedUnit.BaseSize;
                MaxSize = SelectedUnit.MaxSize;

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
            WeaponDescription = null;
            ArmourClass = 0;
            Inexperienced = 0;
            Regular = 0;
            Veteran = 0;
            PointsInexp = 0;
            PointsReg = 0;
            PointsVet = 0;
            BaseSize = 0;
            MaxSize = 0;
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
            PrepareToAddNew();
        }

        private void ConfirmChanges()
        {
            ObservableCollection<GameRule> SelectedRulesList = new ObservableCollection<GameRule>(RulesList.Where(w => (w.IsSelected == true)));
            ObservableCollection<Weapon> SelectedWeaponsList = new ObservableCollection<Weapon>(WeaponsList.Where(w => (w.IsSelected == true)));
            int SelectedNationID = (SelectedNation != null) ? SelectedNation.ID : 0;

            thisModel.ConfirmChanges(Name, SelectedNationID, Type, Composition, WeaponDescription, ArmourClass, Inexperienced, Regular, Veteran, PointsInexp, PointsReg, PointsVet, BaseSize, MaxSize, SelectedUnit, UnitsList, SelectedRulesList, SelectedWeaponsList);
            if (SelectedUnit != null)
            {
                Name = SelectedUnit.Name;
                SelectedNation = NationsList.Where(x => x.ID == SelectedUnit.NationID).FirstOrDefault();
                Type = SelectedUnit.Type;
                Composition = SelectedUnit.Composition;
                WeaponDescription = SelectedUnit.WeaponDescription;
                ArmourClass = SelectedUnit.ArmourClass;
                Inexperienced = SelectedUnit.Inexperienced;
                Regular = SelectedUnit.Regular;
                Veteran = SelectedUnit.Veteran;
                PointsInexp = SelectedUnit.PointsInexp;
                PointsReg = SelectedUnit.PointsReg;
                PointsVet = SelectedUnit.PointsVet;
                BaseSize = SelectedUnit.BaseSize;
                MaxSize = SelectedUnit.MaxSize;
            }
            else
            {
                PrepareToAddNew();
            }
        }

        #endregion
    }
}
