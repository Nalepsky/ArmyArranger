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
    class AddEntryViewModel : BindableBase
    {        
        #region Propeties
        AddEntryModel thisModel = new AddEntryModel();

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

        private Unit _currentUnit1;
        public Unit CurrentUnit1
        {
            get { Console.WriteLine("dupaget"); return _currentUnit1; }
            set
            {
                Console.WriteLine("dupaset");
                _currentUnit1 = value;
                RaisePropertyChanged(nameof(CurrentUnit1));
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

        #endregion

        #region Constructors

        public AddEntryViewModel()
        {
            OnLoad = new DelegateCommand(FunctionOnLoad);
            MouseClick = new DelegateCommand(FunctionOnClick);
            Add = new DelegateCommand(FunctionAdd);

        }

        #endregion

        #region Actions

        private void FunctionOnLoad()
        {
            thisModel.EmptyUnit.LoadAll();
            UnitsList = Unit.UnitsCollection;
            Console.WriteLine("onload");
        }

        private void FunctionOnClick()
        {
            
        }

        private void FunctionAdd()
        {
            if(CurrentUnit1 != null)
                SelectedUnitsList.Add(CurrentUnit1);
        }

        #endregion
    }
}
