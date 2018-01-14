using ArmyArranger.Global;
using ArmyArranger.Models.ArmyList;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ArmyArranger.ViewModels.ArmyList
{
    public class Experience
    {
        public int Cost { get; set; }
        public int ExpValue;
        public String ExpDescribtion { get; set; }

        public Experience(int cost, string expSescribtion, int expValue)
        {
            Cost = cost;
            ExpValue = expValue;
            ExpDescribtion = expSescribtion + cost + " points";
        }
    }

    public class EditUnitViewModel : BindableBase
    {
        #region Propeties

        UnitDetailed SelectedUnit;
        EditUnitModel thisModel = new EditUnitModel();

        private String _extraModels;
        public String ExtraModels
        {
            get { return _extraModels; }
            set { _extraModels = value; RaisePropertyChanged(nameof(ExtraModels)); }
        }

        private int _numberOfExtraModels;
        public int NumberOfExtraModels
        {
            get { return _numberOfExtraModels; }
            set { _numberOfExtraModels = value; RaisePropertyChanged(nameof(NumberOfExtraModels)); }
        }

        private int _points;
        public int Points
        {
            get { return _points; }
            set { _points = value; RaisePropertyChanged(nameof(Points)); }
        }
                
        private String _name;
        public String Name
        {
            get { return _name; }
            set { _name = value; RaisePropertyChanged(nameof(Name)); }
        }
                
        private Experience _selectedExperience;
        public Experience SelectedExperience
        {
            get { return _selectedExperience; }
            set {
                _selectedExperience = value;
                setExtraModels();
                RaisePropertyChanged(nameof(SelectedExperience));
            }
        }               

        private ObservableCollection<ArmyListOption> _optionsList;
        public ObservableCollection<ArmyListOption> OptionsList
        {
            get { return _optionsList; }
            set { _optionsList = value; RaisePropertyChanged(nameof(OptionsList)); }
        }

        private ObservableCollection<Experience> _experienceList;
        public ObservableCollection<Experience> ExperienceList
        {
            get { return _experienceList; }
            set { _experienceList = value; RaisePropertyChanged(nameof(ExperienceList)); }
        }

        private ObservableCollection<int> _additionalModels;
        public ObservableCollection<int> AdditionalModels
        {
            get { return _additionalModels; }
            set { _additionalModels = value; RaisePropertyChanged(nameof(AdditionalModels)); }
        }

        private String _composition;
        public String Composition
        {
            get { return _composition; }
            set { _composition = value; RaisePropertyChanged(nameof(Composition)); }
        }

        private String _weapons;
        public String Weapons
        {
            get { return _weapons; }
            set { _weapons = value; RaisePropertyChanged(nameof(Weapons)); }
        }

        private String _armourClass;
        public String ArmourClass
        {
            get { return _armourClass; }
            set { _armourClass = value; RaisePropertyChanged(nameof(ArmourClass)); }
        }

        private String _rulesList;
        public String RulesList
        {
            get { return _rulesList; }
            set { _rulesList = value; RaisePropertyChanged(nameof(RulesList)); }
        }

        private String _status;
        public String Status
        {
            get { return _status; }
            set { _status = value; RaisePropertyChanged(nameof(Status)); }
        }

        #endregion

        #region Commands

        public ICommand Clear { get; set; }
        public ICommand Save { get; set; }
        public ICommand SelectUnit { get; set; }
        public ICommand DeselectUnit { get; set; }

    #endregion

    #region Constructors

    public EditUnitViewModel(UnitDetailed SelectedUnit)
        {

            Clear = new DelegateCommand(FunctionClear);
            Save = new DelegateCommand(FunctionSave);
            SelectUnit = new DelegateCommand(OnUnitSelect);
            DeselectUnit = new DelegateCommand(OnUnitDeselect);

            this.SelectedUnit = SelectedUnit;
            Points = 0;
            Name = SelectedUnit.Name;
            Status = (SelectedUnit.Selected) ? "Selected" : "Not selected";
            ExperienceList = new ObservableCollection<Experience>();
            AdditionalModels = new ObservableCollection<int>();

            if (SelectedUnit.Inexperienced != 0)
                ExperienceList.Add(new Experience(SelectedUnit.Inexperienced, "Inexperienced for : ", 0));
            if (SelectedUnit.Regular != 0)
                ExperienceList.Add(new Experience(SelectedUnit.Regular, "Regular for : ", 1));
            if (SelectedUnit.Veteran != 0)
                ExperienceList.Add(new Experience(SelectedUnit.Veteran, "Veteran for : ", 2));
            AdditionalModels.Add(SelectedUnit.PointsInexp);
            AdditionalModels.Add(SelectedUnit.PointsReg);
            AdditionalModels.Add(SelectedUnit.PointsVet);
            Composition = SelectedUnit.Composition;
            Weapons = SelectedUnit.WeaponDescription;
            ArmourClass = SelectedUnit.ArmourClass + "+";

            SelectedExperience = ExperienceList.First();
            OptionsList = SelectedUnit.ListOfOptions;
            
            RulesList = "";
            foreach (var rule in SelectedUnit.ListOfActiveRules)
            {
                RulesList += thisModel.GetRules(rule) + "\n\n";
            }

            foreach (var weapon in SelectedUnit.ListOfActiveWeapons)
            {
                RulesList += thisModel.GetWeaponsRules(weapon) + "\n\n"; ;
            }

            CountPoints();
        }

        #endregion

        #region Actions

        private void CountPoints()
        {
            Points = 0;

            foreach (var o in OptionsList)
                if (o.IsChecked)
                    Points += o.Cost;
            Points += SelectedExperience.Cost;

            SelectedUnit.Points = Points;
        }

        private void FunctionSave()
        {
            CountPoints();
        }

        private void FunctionClear()
        {
            SelectedUnit.ListOfOptions.Clear();
            SelectedExperience = ExperienceList.First();
            Points = 0;
        }

        private void OnUnitSelect()
        {
            SelectedUnit.Selected = true;
            Status = "Selected";
        }

        private void OnUnitDeselect()
        {
            SelectedUnit.Selected = false;
            Status = "Not selected";
        }

        private void setExtraModels()
        {
            int temp = SelectedUnit.MaxSize - SelectedUnit.BaseSize;
            

            if (temp > 0)
                ExtraModels = "you can add up to " + temp + "models, for " + AdditionalModels.ElementAt<int>(SelectedExperience.ExpValue) + " points each";
            else
                ExtraModels = "";
        }

        #endregion

    }
}
