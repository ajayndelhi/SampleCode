# Sample Code
Code for custom pagination of list view.

Task:
we want to implement a custom pagination logic to display large set of items.

Implementation:
The source collection can be an in memory collection or in some other data store.
The code demonstates the code with an in memory collection (as used in test cases), and shows how other data connectors
can be added easily, by implementing an interface on them.

For in-memory implementation, we are assuming that the max number of items will be as defined in config

The heart of the code is in SampleCode/UtilClass.cs

It expects a class reference implementing IData interface (that is acting as data manager), request page number and items per page.

Beside regular validations and regular usage scenario, we have test cases for following corner cases as well:
a. Ask page is too high - with 45 items in collection, 10 items per page, user is asking for 50th page;  We are reading user's 
intent and sending him/her to last page with items 41 to 45

b. Ask page is too high - with 50 items in collection, 10 items per page, user is asking for 50th page; we are reading user's 
intent and sending hi/her to last page with items 41 to 50

On the core method and implemented data manager for in memory we get close to 90% code coverage.

Eventually it is the responsibility of data manager (that implements IData interface) to return selected portion of rows. 
For in-memory data manager, we use LINQ, for Database data manager, the query can be sent to underlying database so as to
get only selected rows - useful, if the number of rows is very large.

For testability, we have implemented an interface IConfiguration.  Dependencies are set using Dependency class
defined in SampleCode/Dependency.cs

The code sample also shows following:
- in the same test dll, we can use MS Test and NUnit
- Rhino mocks can be used with MS Test as well

*** End of Document ***
