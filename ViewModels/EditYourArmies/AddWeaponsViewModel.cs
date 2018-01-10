using System.Windows.Input;
using Prism.Commands;
using ArmyArranger.ViewModels.Menu;
using Prism.Mvvm;
using ArmyArranger.Global;
using System.Collections.ObjectModel;
using ArmyArranger.Models;
using System;
using System.Linq;

namespace ArmyArranger.ViewModels.EditYourArmies
{
    class AddWeaponsViewModel : BindableBase
    {
        #region Propeties

        AddWeaponsModel thisModel = new AddWeaponsModel();

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

        private Weapon _selectedWeapon;
        public Weapon SelectedWeapon
        {
            get { return _selectedWeapon; }
            set
            {
                _selectedWeapon = value;
                RaisePropertyChanged(nameof(SelectedWeapon));
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

        private String _range;
        public String Range
        {
            get { return _range; }
            set
            {
                _range = value;
                RaisePropertyChanged(nameof(Range));
            }
        }

        private int _shots;
        public int Shots
        {
            get { return _shots; }
            set
            {
                _shots = value;
                RaisePropertyChanged(nameof(Shots));
            }
        }

        private int _penetration;
        public int Penetration
        {
            get { return _penetration; }
            set
            {
                _penetration = value;
                RaisePropertyChanged(nameof(Penetration));
            }
        }

        private bool _requiresLoader;
        public bool RequiresLoader
        {
            get { return _requiresLoader; }
            set
            {
                _requiresLoader = value;
                RaisePropertyChanged(nameof(RequiresLoader));
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

        #endregion

        #region Constructors

        public AddWeaponsViewModel()
        {
            OnLoad = new DelegateCommand(FunctionOnLoad);
            MouseClick = new DelegateCommand(FunctionOnClick);
            AddNew = new DelegateCommand(PrepareToAddNew);
            Back = new DelegateCommand(ChangeViewToEditYourArmies);
            GoToRules = new DelegateCommand(ChangeViewToAddRules);
            Remove = new DelegateCommand(RemoveSelectedWeapon);
            Confirm = new DelegateCommand(ConfirmChanges);
        }

        #endregion

        #region Actions

        private void FunctionOnLoad()
        {
            ConfirmButtonText = "Save New";
            thisModel.EmptyWeapon.LoadAll();
            WeaponsList = Weapon.WeaponsCollection;
            thisModel.EmptyRule.LoadAll("Weapon");
            RulesList = GameRule.RulesCollection;
        }

        private void FunctionOnClick()
        {
            if (thisModel.ChosenWeaponEqualsSelected(SelectedWeapon) && SelectedWeapon != null)
            {
                Name = SelectedWeapon.Name;
                Range = SelectedWeapon.Range;
                Shots = SelectedWeapon.Shots;
                Penetration = SelectedWeapon.Penetration;
                RequiresLoader = SelectedWeapon.RequiresLoader;
                thisModel.CheckActiveRules(SelectedWeapon);

                ConfirmButtonText = "Update";
            }
        }

        private void PrepareToAddNew()
        {
            Name = "";
            Range = "0";
            Shots = 0;
            Penetration = 0;
            RequiresLoader = false;
            thisModel.ClearWeapons();
            thisModel.ClearRules();
            SelectedWeapon = null;
            ConfirmButtonText = "Save New";
        }

        private void ChangeViewToEditYourArmies()
        {
            thisModel.EmptyWeapon.ClearWeaponsCollection();
            thisModel.EmptyRule.ClearRulesCollection();
            App.Current.MainWindow.DataContext = new EditYourArmiesViewModel();
        }

        private void ChangeViewToAddRules()
        {
            thisModel.EmptyWeapon.ClearWeaponsCollection();
            thisModel.EmptyRule.ClearRulesCollection();
            App.Current.MainWindow.DataContext = new AddRulesViewModel();
        }

        private void RemoveSelectedWeapon()
        {
            thisModel.RemoveWeapon(SelectedWeapon);
            Name = "";
            Range = "0";
            Shots = 0;
            Penetration = 0;
            RequiresLoader = false;
            thisModel.ClearWeapons();
            thisModel.ClearRules();
            SelectedWeapon = null;
            ConfirmButtonText = "Save New";
        }

        private void ConfirmChanges()
        {
            ObservableCollection<GameRule> SelectedRulesList = new ObservableCollection<GameRule>(RulesList.Where(w => (w.IsSelected == true)));
            thisModel.ConfirmChanges(Name, Range, Shots, Penetration, RequiresLoader, SelectedWeapon, WeaponsList, SelectedRulesList);
            if (SelectedWeapon != null)
            {
                Name = SelectedWeapon.Name;
                Range = SelectedWeapon.Range;
                Shots = SelectedWeapon.Shots;
                Penetration = SelectedWeapon.Penetration;
                RequiresLoader = SelectedWeapon.RequiresLoader;
            }
            else
            {
                Name = "";
                Range = "0";
                Shots = 0;
                Penetration = 0;
                RequiresLoader = false;
                thisModel.ClearWeapons();
                thisModel.ClearRules();
            }
        }

        #endregion
    }
}
