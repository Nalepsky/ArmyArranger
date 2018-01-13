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
                    SelectedUnit.Color = "pink";
                }
            }
        }

        #endregion

        #region Propeties

        ChooseUnitsModel thisModel = new ChooseUnitsModel();
        Nation ChoosenNation;
        Selector ChoosenSelector;

        private int _pointsLimit;
        public int PointsLimit
        {
            get { return _pointsLimit; }
            set { _pointsLimit = value; RaisePropertyChanged(nameof(PointsLimit)); }
        }

        private int _pointsCurrent;
        public int PointsCurrent
        {
            get { return _pointsCurrent; }
            set { _pointsCurrent = value; RaisePropertyChanged(nameof(PointsCurrent)); }
        }

        private int _pointsLeft;
        public int PointsLeft
        {
            get { return _pointsLeft; }
            set { _pointsLeft = value; RaisePropertyChanged(nameof(PointsLeft)); }
        }

        private string _selectorTitle;
        public string SelectorTitle
        {
            get { return _selectorTitle; }
            set { _selectorTitle = value; RaisePropertyChanged(nameof(SelectorTitle)); }
        }

        private ObservableCollection<SelectorUnits> _mandatoryListsList;
        public ObservableCollection<SelectorUnits> MandatoryListsList
        {
            get { return _mandatoryListsList; }
            set { _mandatoryListsList = value; RaisePropertyChanged(nameof(MandatoryListsList)); }
        }

        private ObservableCollection<SelectorUnits> _headquartersListsList;
        public ObservableCollection<SelectorUnits> HeadquartersListsList
        {
            get { return _headquartersListsList; }
            set {  _headquartersListsList = value; RaisePropertyChanged(nameof(HeadquartersListsList)); }
        }

        private ObservableCollection<SelectorUnits> _infantryListsList;
        public ObservableCollection<SelectorUnits> InfantryListsList
        {
            get { return _infantryListsList; }
            set { _infantryListsList = value; RaisePropertyChanged(nameof(InfantryListsList)); }
        }

        private ObservableCollection<SelectorUnits> _armouredCarsListsList;
        public ObservableCollection<SelectorUnits> ArmouredCarsListsList
        {
            get { return _armouredCarsListsList; }
            set { _armouredCarsListsList = value; RaisePropertyChanged(nameof(ArmouredCarsListsList)); }
        }

        private ObservableCollection<SelectorUnits> _artilleryListsList;
        public ObservableCollection<SelectorUnits> ArtilleryListsList
        {
            get { return _artilleryListsList; }
            set { _artilleryListsList = value; RaisePropertyChanged(nameof(ArtilleryListsList)); }
        }

        private ObservableCollection<SelectorUnits> _tanksListsList;
        public ObservableCollection<SelectorUnits> TanksListsList
        {
            get { return _tanksListsList; }
            set { _tanksListsList = value; RaisePropertyChanged(nameof(TanksListsList)); }
        }

        private ObservableCollection<SelectorUnits> _transportListsList;
        public ObservableCollection<SelectorUnits> TransportListsList
        {
            get { return _transportListsList; }
            set { _transportListsList = value; RaisePropertyChanged(nameof(TransportListsList)); }
        }

        #endregion

        #region Commands

        public ICommand OnLoad { get; set; }
        public ICommand MouseClick { get; set; }
        public ICommand PointsAccept { get; set; }
        public ICommand Back { get; set; }
        public ICommand Confirm { get; set; }

        #endregion

        #region Constructors

        public ChooseUnitsViewModel(Nation choosenNation, Selector choosenSelector)
        {
            OnLoad = new DelegateCommand(FunctionOnLoad);
            Back = new DelegateCommand(ChangeViewToChooseSelector);
            PointsAccept = new DelegateCommand(AcceptPoints);
            Confirm = new DelegateCommand(ChangeViewToChooseUnits);
            ChoosenNation = choosenNation;
            ChoosenSelector = choosenSelector;
        }

        #endregion

        #region Actions

        private void FunctionOnLoad()
        {
            SelectorTitle = ChoosenSelector.Name + " - " + ChoosenSelector.Date;
            PointsCurrent = 0;
            PointsLeft = 0;

            MandatoryListsList = LoadSelectorListByType(ChoosenSelector.Mandatory);
            HeadquartersListsList = LoadSelectorListByType(ChoosenSelector.Headquarters);
            InfantryListsList = LoadSelectorListByType(ChoosenSelector.Infantry);
            ArmouredCarsListsList = LoadSelectorListByType(ChoosenSelector.ArmouredCars);
            ArtilleryListsList = LoadSelectorListByType(ChoosenSelector.Artillery);
            TanksListsList = LoadSelectorListByType(ChoosenSelector.Tanks);
            TransportListsList = LoadSelectorListByType(ChoosenSelector.Transport);
        }

        private void UpdatePoints()
        {
            PointsLeft = PointsLimit - PointsCurrent;
        }

        private void AcceptPoints()
        {
            UpdatePoints();
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
