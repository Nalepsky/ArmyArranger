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

        UnitDetailed Selectedunit;
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

        #endregion

        #region Commands
        
        public ICommand Clear { get; set; }
        public ICommand Save { get; set; }

        #endregion

        #region Constructors

        public EditUnitViewModel(UnitDetailed Selectedunit)
        {

            Clear = new DelegateCommand(FunctionClear);
            Save = new DelegateCommand(FunctionSave);

            this.Selectedunit = Selectedunit;
            Points = 0;
            Name = Selectedunit.Name;
            ExperienceList = new ObservableCollection<Experience>();
            AdditionalModels = new ObservableCollection<int>();

            if (Selectedunit.Inexperienced != 0)
                ExperienceList.Add(new Experience(Selectedunit.Inexperienced, "Inexperienced for : ", 0));
            if (Selectedunit.Regular != 0)
                ExperienceList.Add(new Experience(Selectedunit.Regular, "Regular for : ", 1));
            if (Selectedunit.Veteran != 0)
                ExperienceList.Add(new Experience(Selectedunit.Veteran, "Veteran for : ", 2));
            AdditionalModels.Add(Selectedunit.PointsInexp);
            AdditionalModels.Add(Selectedunit.PointsReg);
            AdditionalModels.Add(Selectedunit.PointsVet);
            Composition = Selectedunit.Composition;
            Weapons = Selectedunit.WeaponDescription;
            ArmourClass = Selectedunit.ArmourClass + "+";

            SelectedExperience = ExperienceList.First();
            OptionsList = Selectedunit.ListOfOptions;
            
            RulesList = "";
            foreach (var rule in Selectedunit.ListOfActiveRules)
            {
                RulesList += thisModel.GetRules(rule) + "\n\n";
            }

            foreach (var weapon in Selectedunit.ListOfActiveWeapons)
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
        }

        private void FunctionSave()
        {
            CountPoints();
        }

        private void FunctionClear()
        {
            Selectedunit.ListOfOptions.Clear();
            SelectedExperience = ExperienceList.First();
            Points = 0;
        }

        private void setExtraModels()
        {
            int temp = Selectedunit.MaxSize - Selectedunit.BaseSize;
            

            if (temp > 0)
                ExtraModels = "you can add up to " + temp + "models, for " + AdditionalModels.ElementAt<int>(SelectedExperience.ExpValue) + " points each";
            else
                ExtraModels = "";
        }

        #endregion

    }
}
