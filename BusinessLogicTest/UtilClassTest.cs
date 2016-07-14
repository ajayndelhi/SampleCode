using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessLogic;
using BusinessLogic.CollectionClasses;
using System.Collections.Generic;
using System.Linq;
using BusinessLogic.Configuration;
using Rhino.Mocks;

namespace BusinessLogicTest
{
    [TestClass]
    public class UtilClassTest
    {
        public UtilClassTest()
        {
            this.globalTestDataCollection = TestHelper.GetTestDataCollection(45);

            // set the configuration to use fake config class
            IConfiguration configurationMock = MockRepository.GenerateMock<IConfiguration>();
            configurationMock.Stub(x => x.GetAppSetting("MaxItemForInMemoryCollection")).Return("100");
            Dependency.BLConfig = configurationMock;
        }

        /// <summary>
        /// Exception thrown in Act
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetPageItems_NullDataManager()
        {
            // Arrange
            IData data = null;
            int pageNumber = 5;
            int itemsPerPage = 10;

            // Act
            UtilClass.GetPageItems(data, pageNumber, itemsPerPage);
        }

        /// <summary>
        /// Exception thrown by ctor of InMemoryData in Arrange phase only
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetPageItems_NullCtorArgument()
        {
            // Arrange
            IData data = new InMemoryData(null);
        }

        /// <summary>
        /// Data manager managing an empty collection
        /// </summary>
        [TestMethod]
        public void GetPageItems_DataCollectionWithEmptyCollection()
        {
            // Arrange
            IEnumerable<DataClass> dataCollection = new List<DataClass>(); ;

            IData data = new InMemoryData(dataCollection);
            int pageNumber = 1;
            int itemsPerPage = 10;

            // Act
            var outputSet = UtilClass.GetPageItems(data, pageNumber, itemsPerPage);

            // Assert
            Assert.IsNull(outputSet, "output set must be null");
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetPageItems_InvalidPageNumber()
        {
            // Arrange
            IEnumerable<DataClass> dataCollection = globalTestDataCollection;

            IData data = new InMemoryData(dataCollection);
            int pageNumber = 0;
            int itemsPerPage = 10;

            // Act
            UtilClass.GetPageItems(data, pageNumber, itemsPerPage);

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetPageItems_InvalidItemCount()
        {
            // Arrange
            IEnumerable<DataClass> dataCollection = globalTestDataCollection;

            IData data = new InMemoryData(dataCollection);
            int pageNumber = 5;
            int itemsPerPage = 0;

            // Act
            UtilClass.GetPageItems(data, pageNumber, itemsPerPage);

            // Assert
        }

        [TestMethod]
        public void GetPageItems_GetFirstPage()
        {
            // Arrange
            IEnumerable<DataClass> dataCollection = globalTestDataCollection;

            IData data = new InMemoryData(dataCollection);
            int pageNumber = 1;
            int itemsPerPage = 10;

            int[] expectedId = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            // Act
            IEnumerable<DataClass> outputDataSet = UtilClass.GetPageItems(data, pageNumber, itemsPerPage);

            // Assert
            Assert.IsNotNull(outputDataSet, "output data set is null");
            Assert.AreEqual<int>(itemsPerPage, outputDataSet.Count(), string.Format("Returned items count does not match expected item count - Expected: {0}; Actual:{1}", itemsPerPage, outputDataSet.Count()));
            // check the ids of items returned
            int[] actualId = outputDataSet.Select(x => x.SerialNumber).ToArray();

            var exceptList = expectedId.Except(actualId);
            Assert.AreEqual<int>(0, exceptList.Count(), "Mismatch in expected and actual results");
        }

        [TestMethod]
        public void GetPageItems_GetThirdPage()
        {
            // Arrange
            IEnumerable<DataClass> dataCollection = globalTestDataCollection;

            IData data = new InMemoryData(dataCollection);
            int pageNumber = 3;
            int itemsPerPage = 10;

            int[] expectedId = new int[] { 21, 22, 23, 24, 25, 26, 27, 28, 29, 30 };

            // Act
            IEnumerable<DataClass> outputDataSet = UtilClass.GetPageItems(data, pageNumber, itemsPerPage);

            // Assert
            Assert.IsNotNull(outputDataSet, "output data set is null");
            Assert.AreEqual<int>(itemsPerPage, outputDataSet.Count(), string.Format("Returned items count does not match expected item count - Expected: {0}; Actual:{1}", itemsPerPage, outputDataSet.Count()));
            // check the ids of items returned
            int[] actualId = outputDataSet.Select(x => x.SerialNumber).ToArray();

            var exceptList = expectedId.Except(actualId);
            Assert.AreEqual<int>(0, exceptList.Count(), "Mismatch in expected and actual results");
        }

        [TestMethod]
        public void GetPageItems_GetLastPage()
        {
            // Arrange
            IEnumerable<DataClass> dataCollection = globalTestDataCollection;

            IData data = new InMemoryData(dataCollection);
            int pageNumber = 5;
            int itemsPerPage = 10;

            int[] expectedId = new int[] { 41, 42, 43, 44, 45 };

            // Act
            IEnumerable<DataClass> outputDataSet = UtilClass.GetPageItems(data, pageNumber, itemsPerPage);

            // Assert
            Assert.IsNotNull(outputDataSet, "output data set is null");
            Assert.AreEqual<int>(5, outputDataSet.Count(), string.Format("Returned items count does not match expected item count - Expected: {0}; Actual:{1}", itemsPerPage, outputDataSet.Count()));
            // check the ids of items returned
            int[] actualId = outputDataSet.Select(x => x.SerialNumber).ToArray();

            var exceptList = expectedId.Except(actualId);
            Assert.AreEqual<int>(0, exceptList.Count(), "Mismatch in expected and actual results");
        }

        /// <summary>
        /// Collection has 50 items, 10 items per page and user is asking for 5th page
        /// </summary>
        [TestMethod]
        public void GetPageItems_GetLastPageAtBoundary()
        {
            // Arrange
            IEnumerable<DataClass> dataCollection = TestHelper.GetTestDataCollection(50);

            IData data = new InMemoryData(dataCollection);
            int pageNumber = 5;
            int itemsPerPage = 10;

            int[] expectedId = new int[] { 41, 42, 43, 44, 45,46,47,48,49,50 };

            // Act
            IEnumerable<DataClass> outputDataSet = UtilClass.GetPageItems(data, pageNumber, itemsPerPage);

            // Assert
            Assert.IsNotNull(outputDataSet, "output data set is null");
            Assert.AreEqual<int>(10, outputDataSet.Count(), string.Format("Returned items count does not match expected item count - Expected: {0}; Actual:{1}", itemsPerPage, outputDataSet.Count()));
            // check the ids of items returned
            int[] actualId = outputDataSet.Select(x => x.SerialNumber).ToArray();

            var exceptList = expectedId.Except(actualId);
            Assert.AreEqual<int>(0, exceptList.Count(), "Mismatch in expected and actual results");
        }

        /// <summary>
        /// Collection has 50 items, 10 items per page and user is asking for 50th page
        /// expected - last page items from 41 to 50
        /// </summary>
        [TestMethod]
        public void GetPageItems_GetLastPageAtBoundary_AskPageTooHigh()
        {
            // Arrange
            IEnumerable<DataClass> dataCollection = TestHelper.GetTestDataCollection(50);

            IData data = new InMemoryData(dataCollection);
            int pageNumber = 50;
            int itemsPerPage = 10;

            int[] expectedId = new int[] { 41, 42, 43, 44, 45, 46, 47, 48, 49, 50 };

            // Act
            IEnumerable<DataClass> outputDataSet = UtilClass.GetPageItems(data, pageNumber, itemsPerPage);

            // Assert
            Assert.IsNotNull(outputDataSet, "output data set is null");
            Assert.AreEqual<int>(10, outputDataSet.Count(), string.Format("Returned items count does not match expected item count - Expected: {0}; Actual:{1}", itemsPerPage, outputDataSet.Count()));
            // check the ids of items returned
            int[] actualId = outputDataSet.Select(x => x.SerialNumber).ToArray();

            var exceptList = expectedId.Except(actualId);
            Assert.AreEqual<int>(0, exceptList.Count(), "Mismatch in expected and actual results");
        }


        [TestMethod]
        public void GetPageItems_AskPageTooHigh()
        {
            // Arrange
            IEnumerable<DataClass> dataCollection = globalTestDataCollection;

            IData data = new InMemoryData(dataCollection);
            int pageNumber = 55;
            int itemsPerPage = 10;

            int[] expectedId = new int[] { 41, 42, 43, 44, 45 };

            // Act
            IEnumerable<DataClass> outputDataSet = UtilClass.GetPageItems(data, pageNumber, itemsPerPage);

            // Assert
            Assert.IsNotNull(outputDataSet, "output data set is null");
            Assert.AreEqual<int>(5, outputDataSet.Count(), string.Format("Returned items count does not match expected item count - Expected: {0}; Actual:{1}", itemsPerPage, outputDataSet.Count()));
            // check the ids of items returned
            int[] actualId = outputDataSet.Select(x => x.SerialNumber).ToArray();

            var exceptList = expectedId.Except(actualId);
            Assert.AreEqual<int>(0, exceptList.Count(), "Mismatch in expected and actual results");
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void GetPageItems_DataManagerNotImplemented()
        {
            // Arrange
            IData data = new DbData();
            int pageNumber = 5;
            int itemsPerPage = 10;

            int[] expectedId = new int[] { 41, 42, 43, 44, 45 };

            // Act
            IEnumerable<DataClass> outputDataSet = UtilClass.GetPageItems(data, pageNumber, itemsPerPage);
        }

        /// <summary>
        /// Collection passed to in memory data collection manager is too large
        /// for it to manage.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void InMemoryData_CollectionTooLargeToManage()
        {
            // Arrange
            IData data = new InMemoryData(TestHelper.GetTestDataCollection(200));
        }

        /// <summary>
        /// Max In memory data collection size not defined
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void InMemoryData_MaxCollectionSizeNotDefined()
        {
            // Arrange

            // set the configuration to use fake config class that returns null
            // for max Items in in memory collection size
            IConfiguration configurationMock = MockRepository.GenerateMock<IConfiguration>();
            configurationMock.Stub(x => x.GetAppSetting("MaxItemForInMemoryCollection")).Return("");
            Dependency.BLConfig = configurationMock;

            IData data = new InMemoryData(TestHelper.GetTestDataCollection(200));
        }

        private IEnumerable<DataClass> globalTestDataCollection;
    }
}
