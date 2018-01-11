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
        public int Count
        {
            get { return _count; }
            set
            {
                _count = value;
                RaisePropertyChanged(nameof(Count));
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
        public ICommand Cancel { get; set; }
        public ICommand Remove { get; set; }
        public ICommand Confirm { get; set; }


        #endregion

        #region Constructors

        public AddUnitOptionViewModel(int id)
        {
            thisModel = new AddUnitOptionModel(id);
            UnitID = id;

            OnLoad = new DelegateCommand(FunctionOnLoad);
            MouseClick = new DelegateCommand(FunctionOnClick);
            AddNew = new DelegateCommand(PrepareToAddNew);
            Cancel = new DelegateCommand(FunctionCancel);
            Remove = new DelegateCommand(RemoveSelectedOption);
            Confirm = new DelegateCommand(ConfirmChanges);
        }

        #endregion

        #region Actions

        private void FunctionOnLoad()
        {
            ConfirmButtonText = "Save New";
            PossibleRulesList = GameRule.RulesCollection;
            PossibleWeaponsList = Weapon.WeaponsCollection;
            thisModel.EmptyUnitOption.LoadAll(UnitID);
            OptionsList = UnitOption.UnitOptionsCollection;

            Description = "";
            Cost = 0;
            Count = 1;
        }

        private void FunctionOnClick()
        {
            if (thisModel.ChosenEqualsSelected(SelectedOption) && SelectedOption != null)
            {
                Description = SelectedOption.Description;
                Cost = SelectedOption.Cost;
                Count = SelectedOption.Count;
                if (SelectedOption.WeaponID != 0)
                    SelectedPossibleWeapon = PossibleWeaponsList.Where(x => x.ID == SelectedOption.WeaponID).FirstOrDefault();
                else if (SelectedOption.RuleID != 0)
                    SelectedPossibleRule = PossibleRulesList.Where(x => x.ID == SelectedOption.RuleID).FirstOrDefault();
                else
                    SelectedAddition = null;

                ConfirmButtonText = "Update";
            }
        }

        private void PrepareToAddNew()
        {
            SelectedOption = null;
            SelectedPossibleRule = null;
            SelectedPossibleWeapon = null;
            Description = "";
            Cost = 0;
            Count = 1;
            ConfirmButtonText = "Save New";
        }

        private void RemoveSelectedOption()
        {
            thisModel.RemoveOption(SelectedOption);
            PrepareToAddNew();
        }

        private void ConfirmChanges()
        {
            int WeaponID = (SelectedPossibleWeapon != null) ? SelectedPossibleWeapon.ID : 0;
            int RuleID = (SelectedPossibleRule != null) ? SelectedPossibleRule.ID : 0;

            thisModel.ConfirmChanges(Description, Count, Cost, WeaponID, RuleID, UnitID, SelectedOption, OptionsList);
            if (SelectedOption != null)
            {
                if (SelectedOption.WeaponID != 0)
                    SelectedPossibleWeapon = PossibleWeaponsList.Where(x => x.ID == SelectedOption.WeaponID).FirstOrDefault();
                if (SelectedOption.RuleID != 0)
                    SelectedPossibleRule = PossibleRulesList.Where(x => x.ID == SelectedOption.RuleID).FirstOrDefault();
                Description = SelectedOption.Description;
                Cost = SelectedOption.Cost;
                Count = SelectedOption.Count;
            }
            else
            {
                PrepareToAddNew();
            }
        }

        private void FunctionCancel()
        {
            thisModel.EmptyUnitOption.ClearCollection();
        }
        #endregion
    }
}
