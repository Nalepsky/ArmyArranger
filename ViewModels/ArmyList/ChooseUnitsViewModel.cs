using ArmyArranger.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmyArranger.ViewModels.ArmyList
{
    class ChooseUnitsViewModel
    {
        public ChooseUnitsViewModel(Nation choosenNation, Selector choosenSelector)
        {
            Console.WriteLine(choosenNation);
            Console.WriteLine(choosenSelector);
        }
    }
}
