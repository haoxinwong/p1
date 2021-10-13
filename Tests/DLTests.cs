using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;
using DL;
using Models;

namespace Tests
{
    //To Test repo methods
    //you need Microsoft.EntityFrameworkCore
    //Microsoft.EntityFrameworkCore.Design
    //Microsoft.EntityFrameworkCore.Sqlite packages installed in your Test project
    public class DLTests
    {
        private readonly DbContextOptions<P0DBContext> options;

        public DLTests()
        {
            //we are constructing db context options
            //using options builder everytime we instantiate DLTests class
            //and using SQlite's in memory db
            //instead of our real db
            options = new DbContextOptionsBuilder<P0DBContext>()
                        .UseSqlite("Filename=Test.db").Options;
            Seed();
        }

        //Testing Read ops
        [Fact]
        public void GetAllStoreShouldGetAllStores()
        {
            using(var context = new P0DBContext(options))
            {
                //Arrange
                IRepo repo = new CustomerDBRepo(context);

                //Act
                var restaurants = repo.GetAllStore();
                
                //Assert
                Assert.Equal(2, restaurants.Count);
            }
        }


        //For anything that modifies a data set
        //like Write, Update, Delete
        //Test using 2 contexts
        //1 to arrange and act
        //another to directly access the data with context (bypassing repo methods)
        //to assert/ensure that the operations are being performed correctly.
        [Fact]
        public void AddingARestaurantShouldAddARestaurant()
        {
            using(var context = new P0DBContext(options))
            {
                //Arrange with my repo and the item i'm going to add
                IRepo repo = new CustomerDBRepo(context);
                Models.Store restoToAdd = new Models.Store(){
                    Id = 3,
                    Name = "Store3",
                    Address = "Address3"
                };

                //Act
                repo.Add(restoToAdd);
            }

            using(var context = new P0DBContext(options))
            {
                //Assert
                Store resto = context.Stores.FirstOrDefault(r => r.Id == 3);

                Assert.NotNull(resto);
                Assert.Equal("Store3", resto.Name);
                Assert.Equal("Address3", resto.Address);
            }
        }
        [Fact]
        public void GetAllCustomerShouldGetAllCustomer()
        {
            using (var context = new P0DBContext(options))
            {
                //Arrange
                IRepo repo = new CustomerDBRepo(context);

                //Act
                var customers = repo.GetAll();

                //Assert
                Assert.Equal(2, customers.Count);
            }
        }
        [Fact]
        public void AddingACustomerShouldAddACustomer()
        {
            using (var context = new P0DBContext(options))
            {
                //Arrange with my repo and the item i'm going to add
                IRepo repo = new CustomerDBRepo(context);
                Models.Customer custoToAdd = new Models.Customer()
                {
                    Id = 3,
                    Name = "Store3",
                    Address = "Address3",
                    PhoneNumber = "1234567890"
                };

                //Act
                repo.Add(custoToAdd);
            }

            using (var context = new P0DBContext(options))
            {
                //Assert
                Customer custoToAdd = context.Customers.FirstOrDefault(r => r.Id == 3);
                Assert.NotNull(custoToAdd);
                Assert.Equal("Store3", custoToAdd.Name);
                Assert.Equal("Address3", custoToAdd.Address);
                Assert.Equal("1234567890", custoToAdd.PhoneNumber);

            }
        }

        
        [Fact]
        public void FindCustomerShouldGetThatCustomer()
        {
            using (var context = new P0DBContext(options))
            {
                //Arrange
                IRepo repo = new CustomerDBRepo(context);

                //Act
                var customers = repo.GetOneCustomerById(1);

                //Assert
                Assert.Equal("Customer1", customers.Name);
            }
        }

        [Fact]
        public void FindStoreShouldGetThatStore()
        {
            using (var context = new P0DBContext(options))
            {
                //Arrange
                IRepo repo = new CustomerDBRepo(context);

                //Act
                var Store = repo.GetOneStoreById(1);

                //Assert
                Assert.Equal("Store1", Store.Name);
            }
        }




        private void Seed()
        {
            using(var context = new P0DBContext(options))
            {
                //first, we are going to make sure
                //that the DB is in clean slate
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                context.Stores.AddRange(
                    new Store(){
                        Id = 1,
                        Name = "Store1",
                        Address = "Address1"
                    },
                    new Store(){
                        Id = 2,
                        Name = "Store1",
                        Address = "Address1"
                    }
                );

                context.Customers.AddRange(
                    new Customer()
                    {
                        Id = 1,
                        Name = "Customer1",
                        Address = "Address1",
                        PhoneNumber = "1234567890"
                    },
                    new Customer()
                    {
                        Id = 2,
                        Name = "Customer2",
                        Address = "Address2",
                        PhoneNumber = "1234567890"
                    }
                    );

                context.SaveChanges();
            }
        }
    }
}