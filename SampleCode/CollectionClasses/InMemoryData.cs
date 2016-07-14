using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.CollectionClasses
{
    /// <summary>
    /// In memory data that is to be shown as pages
    /// </summary>
    public class InMemoryData : IData
    {
        public InMemoryData(IEnumerable<DataClass> dataCollection)
        {
            if (dataCollection == null)
            {
                throw new ArgumentNullException("dataCollection");
            }

            this._maxAllowedValuesInCollection = UtilClass.GetMaxInMemoryManagedCollectionSize();

            // check limits
            if (this._maxAllowedValuesInCollection < 1)
            {
                throw new ApplicationException(Resources.InvalidConfigForMaxInMemoryItemSize);
            }

            // collection size must not be too large to manage
            if (dataCollection.LongCount() > this._maxAllowedValuesInCollection)
            {
                throw new ApplicationException(Resources.CollectionSizeTooLargeForInMemoryDataManager);
            }

            this._dataCollection = dataCollection;
        }

        public long GetItemCount()
        {
            return _dataCollection.LongCount();
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

            if (rowsToSkip > this._maxAllowedValuesInCollection)
            {
                throw new ApplicationException(Resources.SkipRowCountTooHighForInMemoryCollection);
            }

            return _dataCollection.Skip((int)rowsToSkip).Take(rowsToGet);
        }

        private IEnumerable<DataClass> _dataCollection;
        private long _maxAllowedValuesInCollection;
    }
}
