using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmyArranger.Global
{    class Entry
    {
        public int Min { get; set; }
        public int Max { get; set; }
        public string Name { get; set; }

        public ObservableCollection<Unit> UnitList = new ObservableCollection<Unit>();
        public ObservableCollection<Unit> ExcludingUnitList = new ObservableCollection<Unit>();

    }
}
