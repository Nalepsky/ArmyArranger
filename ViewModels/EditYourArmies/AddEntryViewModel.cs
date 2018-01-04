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
using System.Windows;
using System.Windows.Input;

namespace ArmyArranger.ViewModels.EditYourArmies
{
    class AddEntryViewModel : BindableBase, IShareString
    {        
        #region Propeties
        AddEntryModel thisModel = new AddEntryModel();
        public WindowsService WindowsService { get; set; }              

        private ObservableCollection<Unit> _unitsList;
        public ObservableCollection<Unit> UnitsList
        {
            get { return _unitsList; }
            set
            {
                _unitsList = value;
                RaisePropertyChanged(nameof(UnitsList));
            }
        }

        private ObservableCollection<Unit> _selectedUnitsList;
        public ObservableCollection<Unit> SelectedUnitsList
        {
            get { return _selectedUnitsList; }
            set
            {
                _selectedUnitsList = value;
                RaisePropertyChanged(nameof(SelectedUnitsList));
            }
        }

        private ObservableCollection<Unit> _excludingUnitsList;
        public ObservableCollection<Unit> ExcludingUnitsList
        {
            get { return _excludingUnitsList; }
            set
            {
                _excludingUnitsList = value;
                RaisePropertyChanged(nameof(ExcludingUnitsList));
            }
        }

        private Unit _currentUnit1;
        public Unit CurrentUnit1
        {
            get { return _currentUnit1; }
            set
            {
                _currentUnit1 = value;
                RaisePropertyChanged(nameof(CurrentUnit1));
            }
        }

        private Unit _currentUnit2;
        public Unit CurrentUnit2
        {
            get { return _currentUnit2; }
            set
            {
                _currentUnit2 = value;
                RaisePropertyChanged(nameof(CurrentUnit2));
            }
        }

        private Unit _excludingUnit;
        public Unit ExcludingUnit
        {
            get { return _excludingUnit; }
            set
            {
                _excludingUnit = value;
                RaisePropertyChanged(nameof(ExcludingUnit));
            }
        }

        private int _min;
        public int Min
        {
            get { return _min; }
            set
            {
                _min = value;
                RaisePropertyChanged(nameof(Min));
            }
        }

        private int _max;
        public int Max
        {
            get { return _max; }
            set
            {
                _max = value;
                RaisePropertyChanged(nameof(Max));
            }
        }

        #endregion

        #region Commands
        public ICommand OnLoad { get; set; }
        public ICommand MouseClick { get; set; }
        public ICommand Add { get; set;}
        public ICommand AddAll { get; set; }
        public ICommand Remove { get; set; }
        public ICommand RemoveAll { get; set; }
        public ICommand Cancel { get; set; }
        public ICommand Next { get; set; }
        public ICommand AddToExcluding { get; set; }
        public ICommand RemoveFromExcluding { get; set; }
                
        #endregion

        #region Constructors

        public AddEntryViewModel(WindowsService service)
        {
            WindowsService = service;
            Max = 1;
            
            OnLoad = new DelegateCommand(FunctionOnLoad);
            MouseClick = new DelegateCommand(FunctionOnClick);
            Add = new DelegateCommand(FunctionAdd);
            AddAll = new DelegateCommand(FunctionAddAll);
            Remove = new DelegateCommand(FunctionRemove);
            RemoveAll = new DelegateCommand(FunctionRemoveAll);
            Cancel = new DelegateCommand(FunctionCancel);
            Next = new DelegateCommand(FunctionNext);
            AddToExcluding = new DelegateCommand(FunctionAddToExcluding);
            RemoveFromExcluding = new DelegateCommand(FunctionRemoveFromExcluding);
        }

        public AddEntryViewModel(WindowsService service, Entry E)
        {
            WindowsService = service;
            Max = E.Max;
            Min = E.Min;

            thisModel.SelectedUnitsList = E.UnitList;
            thisModel.ExcludingUnitsList = E.ExcludingUnitList;

            OnLoad = new DelegateCommand(FunctionOnLoad);
            MouseClick = new DelegateCommand(FunctionOnClick);
            Add = new DelegateCommand(FunctionAdd);
            AddAll = new DelegateCommand(FunctionAddAll);
            Remove = new DelegateCommand(FunctionRemove);
            RemoveAll = new DelegateCommand(FunctionRemoveAll);
            Cancel = new DelegateCommand(FunctionCancel);
            Next = new DelegateCommand(FunctionNext);
            AddToExcluding = new DelegateCommand(FunctionAddToExcluding);
            RemoveFromExcluding = new DelegateCommand(FunctionRemoveFromExcluding);
        }

        #endregion

        #region Actions

        private void FunctionOnLoad()
        {
            thisModel.EmptyUnit.LoadAll();
            UnitsList = Unit.UnitsCollection;
            SelectedUnitsList = thisModel.SelectedUnitsList;
            ExcludingUnitsList = thisModel.ExcludingUnitsList;
        }

        private void FunctionOnClick()
        {
            
        }

        private void FunctionAdd()
        {
            SelectedUnitsList = thisModel.Add(CurrentUnit1);
            CurrentUnit1 = null;
        }
        
        private void FunctionAddAll()
        {
            SelectedUnitsList = thisModel.AddAll(UnitsList);
            CurrentUnit1 = null;
        }

        private void FunctionRemoveAll()
        {
            SelectedUnitsList = thisModel.RemoveAll();
            CurrentUnit2 = null;
        }

        private void FunctionRemove()
        {
            ExcludingUnitsList = thisModel.RemoveFromExcluding(CurrentUnit2);
            SelectedUnitsList = thisModel.Remove(CurrentUnit2);
            
            CurrentUnit2 = null;
        }

        private void FunctionNext()
        {
            WindowsService.SendMessageToSubscribers(thisModel.CreateString(Min, Max));
            FunctionCancel();
        }

        private void FunctionCancel()
        {
            UnitsList.Clear();
            SelectedUnitsList.Clear();
            ExcludingUnitsList.Clear();
        }

        private void FunctionAddToExcluding()
        {
            ExcludingUnitsList = thisModel.AddToExcluding(CurrentUnit2);
        }

        private void FunctionRemoveFromExcluding()
        {
            ExcludingUnitsList = thisModel.RemoveFromExcluding(ExcludingUnit);
        }

        public void OnMessageSend(string message)
        {
            Console.WriteLine(message);
        }
        
        #endregion
    }
}
