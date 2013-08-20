using System.Data.Entity;
using System.Linq;
using Xunit;

namespace DemoEntityFramework
{
    public class CustomerContext : DbContext
    {
        private const string CONN_STR = "Data Source=.;Initial Catalog=DemoSimpleData;Integrated Security=True";
        public CustomerContext() : base(CONN_STR) { }

        public DbSet<Customer> Customers { get; set; }
    }

    public class Customer
    {
        public int ID { get; set; }
        public int Age { get; set; }
        public string Name { get; set; }
    }

    public class EFTests
    {
        [Fact]
        public void should_get_all_customers()
        {
            // Arrange
            using (var ctx = new CustomerContext())
            {
                // Act
                var customers = from c in ctx.Customers
                                select c;

                // Assert
                Assert.Equal(4, customers.Count());
            }
        }

        [Fact]
        public void should_get_all_old_guys()
        {
            // Arrange
            using (var ctx = new CustomerContext())
            {
                // Act
                var customers = from c in ctx.Customers
                                where c.Age > 40
                                select c;

                // Assert
                Assert.Equal(3, customers.Count());
                Assert.False(customers.Any(x => x.Name == "Marcus"));
            }
        }

    }
}
