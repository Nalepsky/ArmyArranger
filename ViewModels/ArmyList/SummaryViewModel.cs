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
    public class SummaryViewModel : BindableBase
    {
        #region Properties

        SummaryModel thisModel = new SummaryModel();

        private ObservableCollection<SummaryUnit> _summaryUnitsList;
        public ObservableCollection<SummaryUnit> SummaryUnitsList
        {
            get { return _summaryUnitsList; }
            set
            {
                _summaryUnitsList = value;
                RaisePropertyChanged(nameof(SummaryUnitsList));
            }
        }

        #endregion

        #region Commands

        public ICommand Back { get; set; }
        public ICommand Confirm { get; set; }

        #endregion

        #region Constructors

        public SummaryViewModel()
        {            
            Back = new DelegateCommand(FunctionBack);
            Confirm = new DelegateCommand(FunctionConfirm);

            SummaryUnitsList = new ObservableCollection<SummaryUnit>();

            SummaryUnitsList = thisModel.GetSummaryUnitList(UnitDetailed.AllUnitsCollection);

            foreach (var e in UnitDetailed.AllUnitsCollection)
                Console.WriteLine(e.ID);
        }

        #endregion

        #region Actions

        private void FunctionConfirm()
        {
           // throw new NotImplementedException();
        }

        private void FunctionBack()
        {
            //
        }

        #endregion

    }
}
