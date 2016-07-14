using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.CollectionClasses
{
    /// <summary>
    /// DB data that is to be shown as pages
    /// </summary>
    public class DbData : IData
    {
        public DbData()
        {
        }

        public long GetItemCount()
        {
            // it has to make db call to get item count
            throw new NotImplementedException();
        }

        public IEnumerable<DataClass> GetData(long rowsToSkip, int rowsToGet)
        {
            if (rowsToSkip < 0)
            {
                throw new ArgumentException(Resources.InvalidRowsSkipCount);
            }

            if (rowsToGet < 1)
            {
                throw new ArgumentException(Resources.InvalidRowsToGetCount);
            }

            // here make a db call to get only required number of rows
            throw new NotImplementedException();
        }
    }
}
