using System.Collections.Generic;
using System.Linq;
using Simple.Data;
using Xunit;

namespace DemoSimpleData
{
    public class SimpleDataDemos
    {
        private string CONN_STR = "Data Source=.;Initial Catalog=DemoSimpleData;Integrated Security=True";

        public SimpleDataDemos()
        {
            // Set up database
        }

        [Fact]
        public void should_get_all_customers()
        {
            var _db = Database.OpenConnection(CONN_STR);
            var allCustomers = _db.Customers.All();
            var oldCustomers = _db.Customers.FindAll(_db.Customers.Age > 40);

            Assert.Equal(4, allCustomers.Count());
            Assert.Equal(3, oldCustomers.Count());
        }

        [Fact]
        public void should_get_all_old_guys()
        {
            var _db = Database.OpenConnection(CONN_STR);
            List<Customer> oldCustomers = _db.Customers.FindAll(_db.Customers.Age > 40);

            Assert.Equal(3, oldCustomers.Count);
            Assert.True(!oldCustomers.Any(x => x.Name == "Marcus"));
        }

        [Fact]
        public void should_find_specific_customer()
        {
            var _db = Database.OpenConnection(CONN_STR);
            var marcus = _db.Customers.Find(_db.Customers.Name == "Marcus");

            Assert.Equal("Marcus", marcus.Name);
            Assert.Equal(40, marcus.Age);
        }

        private dynamic GetCustomer(int id)
        {
            var db = Database.Open();
            return db.Test.FindById(id);
        }

        [Fact]
        public void should_get_a_customer()
        {
            // Arrange
            // Set up the InMemoryAdapter
            var adapter = new InMemoryAdapter();
            Database.UseMockAdapter(adapter);

            // Insert some test data
            var db = Database.Open();
            db.Test.Insert(Id: 1, Name: "Alice");

            // Act
            var record = GetCustomer(1);

            // Assert
            Assert.NotNull(record);
            Assert.Equal(1, record.Id);
            Assert.Equal("Alice", record.Name);

            // Clean up
            Database.StopUsingMockAdapter();
        }
    }

    public class Customer
    {
        public int ID { get; set; }
        public int Age { get; set; }
        public string Name { get; set; }
    }
}
