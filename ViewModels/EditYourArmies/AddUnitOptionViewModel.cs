using ArmyArranger.Global;
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

        private int UnitID;

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
                _selectedPossibleRule = null;
                RaisePropertyChanged(nameof(SelectedPossibleWeapon));
                if (value != null)
                    SelectedAddition = SelectedPossibleWeapon.Name;
                else
                    SelectedAddition = "";
            }
        }

        private GameRule _selectedPossibleRule;
        public GameRule SelectedPossibleRule
        {
            get { return _selectedPossibleRule; }
            set
            {
                _selectedPossibleRule = value;
                _selectedPossibleWeapon = null;
                RaisePropertyChanged(nameof(SelectedPossibleRule));
                if(value != null)
                    SelectedAddition = SelectedPossibleRule.Name;
                else
                    SelectedAddition = "";
            }
        }

        private String _description;
        public String Description
        {
            get { return _description; }
            set
            {
                _description = value;
                RaisePropertyChanged(nameof(Description));
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

        private int _count;
        public int MaxNumber
        {
            get { return _count; }
            set
            {
                _count = value;
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
            UnitID = id;

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
            bool WeaponOrRule;
            int RuleID;
            int WeaponID;

            if (_selectedPossibleWeapon != null)
            {
                WeaponOrRule = true;
                RuleID = _selectedPossibleWeapon.ID;
                WeaponID = 0;
            }
            else
            {
                WeaponOrRule = false;
                RuleID = 0;
                WeaponID = _selectedPossibleRule.ID;
            }             
                      
            thisModel.EmptyUnitOption.CreateNewAndSaveToDB(Description, MaxNumber, Cost, WeaponID, RuleID, UnitID, WeaponOrRule);

            thisModel.EmptyUnitOption.clearCollection();
            thisModel.EmptyUnitOption.LoadAll(UnitID);
            OptionsList = UnitOption.UnitOptionsCollection;

        }

        private void FunctionCancel()
        {
            thisModel.EmptyUnitOption.clearCollection();
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
            Description = "";
            Cost = 0;
            MaxNumber = 1;
        }

        private void FunctionOnLoad()
        {
            PossibleRulesList = GameRule.RulesCollection;
            PossibleWeaponsList = Weapon.WeaponsCollection;
            thisModel.EmptyUnitOption.LoadAll(UnitID);
            OptionsList = UnitOption.UnitOptionsCollection;            

            Description = "";
            Cost = 0;
            MaxNumber = 1;
        }
        #endregion
    }
}
