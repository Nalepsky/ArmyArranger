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
        #region Propeties

        ChooseUnitsModel thisModel = new ChooseUnitsModel();
        Nation ChoosenNation;
        Selector ChoosenSelector;

        public class SelectorUnits {
            public static ObservableCollection<SelectorUnits> SelectorUnitsCollections = new ObservableCollection<SelectorUnits>();
            public event PropertyChangedEventHandler PropertyChanged;
            private void OnPropertyChanged<T>([CallerMemberName]string caller = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
            }
            public string Count { get; set; }
            public ObservableCollection<UnitDetailed> UnitsList { get; set; }

            public SelectorUnits(String count, ObservableCollection<UnitDetailed> unitsList)
            {
                Count = count;
                UnitsList = unitsList;
                SelectorUnitsCollections.Add(this);
            }
            private UnitDetailed _selectedUnit;
            public UnitDetailed SelectedUnit {
                get { return _selectedUnit; }
                set
                {
                    _selectedUnit = value;
                    MessageBox.Show("selected: " + SelectedUnit.Name + " to get all informations about selected unit use SelectedUnit from SelectorUnits class from ChooseUnitsViewModel.cs like: SelectedUnit.[every needed property]");
                }
            }
        }







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

        private ObservableCollection<UnitDetailed> _mandatoryList;
        public ObservableCollection<UnitDetailed> MandatoryList
        {
            get { return _mandatoryList; }
            set
            {
                _mandatoryList = value;
                RaisePropertyChanged(nameof(MandatoryList));
            }
        }

        public string SelectorTitle { get; set; }

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
            string[] MandatoryEntries = ChoosenSelector.Mandatory.Split(new char[] {'|'});
            string last = MandatoryEntries.Last();
            foreach (string mandatoryEntry in MandatoryEntries)
            {
                if (!mandatoryEntry.Equals(last))
                {
                    string[] MandatoryStringInfo = mandatoryEntry.Split(new char[] { ';' }, 2);
                    string Count = MandatoryStringInfo[0];

                    ObservableCollection<UnitDetailed> temp_UnitsCollection = new ObservableCollection<UnitDetailed>();
                    UnitDetailed.LoadFromStringToCollection(MandatoryStringInfo[1], temp_UnitsCollection);

                    SelectorUnits selectorUnits = new SelectorUnits(Count, temp_UnitsCollection);
                    MandatoryListsList = SelectorUnits.SelectorUnitsCollections;
                }
            }
        }

        private void ChangeViewToChooseSelector()
        {
            //thisModel.EmptyUnitDetailed.ClearUnitsCollection();
            App.Current.MainWindow.DataContext = new ArmyList.ChooseSelectorViewModel();
        }

        private void ChangeViewToChooseUnits()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
