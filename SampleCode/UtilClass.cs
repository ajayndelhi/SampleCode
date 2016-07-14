using BusinessLogic.CollectionClasses;
using BusinessLogic.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace BusinessLogic
{
    /// <summary>
    /// Utility class for helper methods
    /// </summary>
    public static class UtilClass
    {
        /// <summary>
        /// Method to get items for display on a specific page. The method takes an interface that
        /// manages the data Based on its three parameters, the method decides how many 
        /// rows to skip;  If user asks for a page number that is not feasible in given data set,
        /// the method will return the last page items.
        /// </summary>
        /// <param name="dataManager">Input data source manager</param>
        /// <param name="pageNumber">Page Number to get</param>
        /// <param name="itemsPerPage">Number of items on each page</param>
        public static IEnumerable<DataClass> GetPageItems(IData dataManager, int pageNumber, int itemsPerPage)
        {
            // Validate input parameters 

            // there are no rows in input data source
            if (dataManager == null)
            {
                throw new ArgumentNullException("dataManager");
            }

            // ensure that page number is not zero
            if (pageNumber < 1)
            {
                throw new ArgumentException(Resources.InvalidPageNumber);
            }

            // Items Per page can not be 0 
            if (itemsPerPage < 1)
            {
                throw new ArgumentException(Resources.InvalidItemsPerPage);
            }

            try
            {
                long dataItemCount = dataManager.GetItemCount();

                if (dataItemCount < 1)
                {
                    // no rows in input data collection
                    return null;
                }

                int maxPages = CalculateMaxPages(itemsPerPage, dataItemCount);

                // if input has specified an out of bound page number, set it to last page;
                if (pageNumber > maxPages)
                {
                    pageNumber = maxPages;
                }

                // calculate items to skip
                long itemsToSkip = (pageNumber - 1) * itemsPerPage;

                // invoke data manager to get data
                return dataManager.GetData(itemsToSkip, itemsPerPage);
            }
            catch (Exception ex)
            {
                // log the exception here and throw it
                throw;
            }
        }

        /// <summary>
        /// Calculate max pages that can be formed
        /// </summary>
        /// <param name="itemsPerPage"></param>
        /// <param name="dataItemCount"></param>
        /// <returns></returns>
        private static int CalculateMaxPages(int itemsPerPage, long dataItemCount)
        {
            // max pages that can be formed for dataSourceRowCount
            int maxPages = (int)((dataItemCount / itemsPerPage));

            // with page size as 10, 45 items will form 5 pages, 
            // also 50 items will form 5 pages;
            if (dataItemCount % itemsPerPage > 0)
            {
                maxPages++;
            }

            return maxPages;
        }

        /// <summary>
        /// Read the value for maximum in memory managed collection size, from configuration
        /// </summary>
        /// <returns></returns>
        internal static long GetMaxInMemoryManagedCollectionSize()
        {
            string v = Dependency.BLConfig.GetAppSetting("MaxItemForInMemoryCollection");

            long returnValue = 0;
            bool convertStatus = long.TryParse(v, out returnValue);

            return (convertStatus) ? returnValue : -1;
        }
    }
}
