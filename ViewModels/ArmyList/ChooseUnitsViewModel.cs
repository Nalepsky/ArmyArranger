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
    class ChooseUnitsViewModel : BindableBase
    {
        #region Propeties

        ChooseUnitsModel thisModel = new ChooseUnitsModel();
        
        private ObservableCollection<ObservableCollection<Unit>> _mandatoryListsList;
        public ObservableCollection<ObservableCollection<Unit>> MandatoryListsList
        {
            get { return _mandatoryListsList; }
            set
            {
                _mandatoryListsList = value;
                RaisePropertyChanged(nameof(MandatoryListsList));
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
            SelectorTitle = choosenSelector.Name + " - " + choosenSelector.Date;
            Console.WriteLine(choosenNation.Name);
            Console.WriteLine(choosenSelector.Name);
        }

        private void ChangeViewToChooseUnits()
        {
            throw new NotImplementedException();
        }


        #endregion

        #region Actions

        private void FunctionOnLoad()
        {
            thisModel.EmptyUnit.LoadAll();
        }

        private void ChangeViewToChooseSelector()
        {
            thisModel.EmptyUnit.ClearUnitsCollection();
            App.Current.MainWindow.DataContext = new ArmyList.ChooseSelectorViewModel();
        }

        #endregion

    }
}
