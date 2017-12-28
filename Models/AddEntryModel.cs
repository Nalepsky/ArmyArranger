using ArmyArranger.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmyArranger.Models
{
    class AddEntryModel
    {
        #region Properties
        String EntryString;
        //i.e. 0-1;unitID,exclusionFlag;unitID,exclusionFlag|

        public Unit EmptyUnit = new Unit();

        public AddEntryModel()
        {
            EntryString = "";
        }
        #endregion

        #region Constructors

        #endregion
    }
}
