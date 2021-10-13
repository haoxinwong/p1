using System;
using Xunit;
using Models;


namespace Tests
{
    public class UnitTest1
    {
        [Fact]
        public void CustomerCreate()
        {
            Customer c = new Customer();

            Assert.NotNull(c);
        }
        [Fact]
        public void CustomerData()
        {
            //Given
            Customer c = new Customer();
            string testname = "test";
            //When
            c.Name = testname;
            //Then
            Assert.Equal(testname, c.Name);
        }

        [Fact]
        public void InventoryPrice()
        {
            Inventory i = new Inventory();
            decimal test = 1;
            //When
            i.Price = test;
            //Then
            Assert.Equal(test, i.Price);
        }

        [Fact]
        public void InventoryName()
        {
            Inventory i = new Inventory();
            string test = "name";
            //When
            i.Name = test;
            //Then
            Assert.Equal(test, i.Name);
        }
        [Fact]
        public void InventoryQuantity()
        {
            Inventory i = new Inventory();
            int test = 5;
            //When
            i.Quantity = test;
            //Then
            Assert.Equal(test, i.Quantity);
        }
        [Theory, MemberData(nameof(CorrectData))]
        public void OrderTime(DateTime time)
        {
            Order o = new Order();
            o.Time = time;
            Assert.Equal(time, o.Time);
        }

        public static readonly object[][] CorrectData =
        {
            new object[] { new DateTime()},
            new object[] { new DateTime(2017,3,1)},
            new object[] { new DateTime(2012, 12, 25, 10, 30, 50)},
            new object[] { DateTime.MaxValue}
        };

        [Theory]
        [InlineData("13545")]
        [InlineData("%$@^^")]
        [InlineData("safdw")]
        public void CheckThephonenumber(string input)
        {
            Customer i = new Customer();
            Assert.Equal("-1", i.CheckPhoneNumber(input));
        }

    }
}
