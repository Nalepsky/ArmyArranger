using ArmyArranger.Global;
using ArmyArranger.Models;
using ArmyArranger.ViewModels.Menu;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace ArmyArranger.ViewModels.ArmyList
{
    class ChooseUnitsViewModel : BindableBase
    {
        #region SelectorUnits Class

        public class SelectorUnits
        {
            //public event PropertyChangedEventHandler PropertyChanged;
            //private void OnPropertyChanged<T>([CallerMemberName]string caller = null)
            //{
            //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
            //}
            public string Count { get; set; }
            public ObservableCollection<UnitDetailed> UnitsList { get; set; }

            public SelectorUnits(String count, ObservableCollection<UnitDetailed> unitsList)
            {
                Count = count;
                UnitsList = unitsList;
            }
            private UnitDetailed _selectedUnit;
            public UnitDetailed SelectedUnit
            {
                get { return _selectedUnit; }
                set
                {
                    _selectedUnit = value;
                    MessageBox.Show("selected: " + SelectedUnit.Name + "  -- to get all informations about selected unit use SelectedUnit from SelectorUnits class from ChooseUnitsViewModel.cs like: SelectedUnit.[every needed property]");
                }
            }
        }

        #endregion

        #region Propeties

        ChooseUnitsModel thisModel = new ChooseUnitsModel();
        Nation ChoosenNation;
        Selector ChoosenSelector;

        public string SelectorTitle { get; set; }

        private ObservableCollection<SelectorUnits> _mandatoryListsList;
        public ObservableCollection<SelectorUnits> MandatoryListsList
        {
            get { return _mandatoryListsList; }
            set
            {
                _mandatoryListsList = value;
                RaisePropertyChanged(nameof(MandatoryListsList));
            }
        }

        private ObservableCollection<SelectorUnits> _headquartersListsList;
        public ObservableCollection<SelectorUnits> HeadquartersListsList
        {
            get { return _headquartersListsList; }
            set
            {
                _headquartersListsList = value;
                RaisePropertyChanged(nameof(HeadquartersListsList));
            }
        }

        #endregion

        #region Commands

        public ICommand OnLoad { get; set; }
        public ICommand MouseClick { get; set; }
        public ICommand Back { get; set; }
        public ICommand Confirm { get; set; }

        #endregion

        #region Constructors

        public ChooseUnitsViewModel(Nation choosenNation, Selector choosenSelector)
        {
            OnLoad = new DelegateCommand(FunctionOnLoad);
            Back = new DelegateCommand(ChangeViewToChooseSelector);
            Confirm = new DelegateCommand(ChangeViewToChooseUnits);
            ChoosenNation = choosenNation;
            ChoosenSelector = choosenSelector;
        }

        #endregion

        #region Actions

        private void FunctionOnLoad()
        {
            SelectorTitle = ChoosenSelector.Name + " - " + ChoosenSelector.Date;

            MandatoryListsList = LoadSelectorListByType(ChoosenSelector.Mandatory);
            HeadquartersListsList = LoadSelectorListByType(ChoosenSelector.Headquarters);
        }


        private ObservableCollection<SelectorUnits> LoadSelectorListByType(string encodedUnits)
        {
            ObservableCollection<SelectorUnits> temp_ListsList = new ObservableCollection<SelectorUnits>();
            string[] Entries = encodedUnits.Split(new char[] { '|' });
            string last = Entries.Last();
            foreach (string Entry in Entries)
            {
                if (!Entry.Equals(last))
                {
                    string[] StringInfo = Entry.Split(new char[] { ';' }, 2);
                    string Count = StringInfo[0];

                    temp_ListsList.Add(new SelectorUnits(Count, UnitDetailed.LoadFromStringToCollection(StringInfo[1])));
                }
            }
            return temp_ListsList;
        }


        private void ChangeViewToChooseSelector()
        {
            MandatoryListsList = new ObservableCollection<SelectorUnits>();
            App.Current.MainWindow.DataContext = new ArmyList.ChooseSelectorViewModel();
        }


        private void ChangeViewToChooseUnits()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
