using ArmyArranger.Global;
using ArmyArranger.Models;
using ArmyArranger.ViewModels.Menu;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ArmyArranger.ViewModels.ArmyList
{
    class ChooseSelectorViewModel : BindableBase
    {
        #region Propeties

        ChooseSelectorModel thisModel = new ChooseSelectorModel();

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

        private Nation _selectedRule;
        public Nation SelectedNation
        {
            get { return _selectedRule; }
            set
            {
                _selectedRule = value;
                RaisePropertyChanged(nameof(SelectedNation));
            }
        }

        private ObservableCollection<Selector> _selectorsList;
        public ObservableCollection<Selector> SelectorsList
        {
            get { return _selectorsList; }
            set
            {
                _selectorsList = value;
                RaisePropertyChanged(nameof(SelectorsList));
            }
        }

        private Selector _selectedSelector;
        public Selector SelectedSelector
        {
            get { return _selectedSelector; }
            set
            {
                _selectedSelector = value;
                RaisePropertyChanged(nameof(SelectedSelector));
            }
        }

        #endregion

        #region Commands

        public ICommand OnLoad { get; set; }
        public ICommand MouseClick { get; set; }
        public ICommand GoToNations { get; set; }
        public ICommand GoToSelectors { get; set; }
        public ICommand Back { get; set; }
        public ICommand Confirm { get; set; }

        #endregion

        #region Constructors

        public ChooseSelectorViewModel()
        {
            OnLoad = new DelegateCommand(FunctionOnLoad);
            GoToNations = new DelegateCommand(ChangeViewToAddNations);
            GoToSelectors = new DelegateCommand(ChangeViewToAddSelectors);
            Back = new DelegateCommand(ChangeViewToMenuView);
            Confirm = new DelegateCommand(ChangeViewToChooseUnits);
        }

        #endregion

        #region Actions

        private void FunctionOnLoad()
        {
            thisModel.EmptyNation.LoadAll();
            thisModel.EmptySelector.LoadAll();
            NationsList = Nation.NationsCollection;
            SelectorsList = Selector.SelectorsCollection;
        }

        private void ChangeViewToAddNations()
        {
            thisModel.EmptyNation.ClearNationsCollection();
            thisModel.EmptySelector.ClearSelectorsCollection();
            App.Current.MainWindow.DataContext = new EditYourArmies.AddNationsViewModel();
        }

        private void ChangeViewToAddSelectors()
        {
            thisModel.EmptyNation.ClearNationsCollection();
            thisModel.EmptySelector.ClearSelectorsCollection();
            App.Current.MainWindow.DataContext = new EditYourArmies.AddSelectorsViewModel();
        }

        private void ChangeViewToMenuView()
        {
            thisModel.EmptyNation.ClearNationsCollection();
            thisModel.EmptySelector.ClearSelectorsCollection();
            App.Current.MainWindow.DataContext = new EditYourArmiesViewModel();
        }

        private void ChangeViewToChooseUnits()
        {
            if((SelectedNation != null) && (SelectedSelector != null))
                App.Current.MainWindow.DataContext = new ChooseUnitsViewModel(SelectedNation, SelectedSelector);
        }

        #endregion
    }
}