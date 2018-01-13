using ArmyArranger.Global;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmyArranger.ViewModels.ArmyList
{
    public class Experience
    {
        public int Cost { get; set; }
        public String ExpSescribtion { get; set; }

        public Experience(int cost, string expSescribtion)
        {
            Cost = cost;
            ExpSescribtion = expSescribtion;
        }
    }

    public class EditUnitViewModel : BindableBase
    {
        #region Propeties

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

        public EditUnitViewModel(UnitDetailed SlectedUnit)
        {
            Points = 0;
            Name = SlectedUnit.Name;
            if (SlectedUnit.Inexperienced != 0)
                ExperienceList.Add(new Experience(SlectedUnit.Inexperienced, "Inexperienced for :"));
            if (SlectedUnit.Regular != 0)
                ExperienceList.Add(new Experience(SlectedUnit.Regular, "Regular for :"));
            if (SlectedUnit.Veteran != 0)
                ExperienceList.Add(new Experience(SlectedUnit.Veteran, "Veteran for :"));
            AdditionalModels.Add(SlectedUnit.PointsInexp);
            AdditionalModels.Add(SlectedUnit.PointsReg);
            AdditionalModels.Add(SlectedUnit.PointsVet);
            Composition = SlectedUnit.Composition;
            Weapons = SlectedUnit.WeaponDescription;
            ArmourClass = SlectedUnit.ArmourClass + "+";

            RulesList = "";
            foreach(var rule in SlectedUnit.ListOfActiveRules)
            {
                RulesList += rule + " ";
            }
        }

        #endregion

        #region Constructors



        #endregion

    }
}
