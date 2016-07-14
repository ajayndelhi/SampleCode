using BusinessLogic;
using System.Collections.Generic;

namespace BusinessLogicTest
{
    internal static class TestHelper
    {
        #region Internal Helper Methods
        /// <summary>
        /// Gets data collection for testing with specified number of items;
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        internal static IEnumerable<DataClass> GetTestDataCollection(int numberOfItems)
        {
            List<DataClass> dataCollection = new List<DataClass>();

            for (int i = 1; i <= numberOfItems; i++)
            {
                DataClass dc = new DataClass()
                {
                    SerialNumber = i,
                    DisplayText = string.Format("This is Display Text for item number {0}", i),
                    LinkText = string.Format("This is Link text for item number {0}", i)
                };

                dataCollection.Add(dc);
            }

            return dataCollection;
        }
        #endregion
    }
}
