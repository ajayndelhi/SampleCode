using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.CollectionClasses
{
    /// <summary>
    /// Interface that data classes can implement
    /// </summary>
    public interface IData
    {
        long GetItemCount();
        IEnumerable<DataClass> GetData(long rowsToSkip, int rowsToGet);
    }
}
