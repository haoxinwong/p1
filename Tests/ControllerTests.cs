using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using WebUI.Controllers;
using BL;
using Models;
using Microsoft.AspNetCore.Mvc;
using WebUI.Models;

namespace Tests
{
    public class ControllerTests
    {
        [Fact]
        public void CustomerStoreControllerIndexShouldReturnListOfStores()
        {
            //Arrange
            var mockBL = new Mock<IBL>();
            mockBL.Setup(x => x.GetAllStore()).Returns(
                    new List<Store>()
                    {
                        new Store() {
                            Id = 1,
                            Name = "Store1",
                            Address = "first st"
                        },
                        new Store()
                        {
                            Id = 2,
                            Name = "Store2",
                            Address = "last st"
                        }
                    }
                );
            var controller = new CustomerStoreController(mockBL.Object);

            //Act
            var result = controller.Index();

            //Assert
            //First, make sure we are getting the right type of the result obj
            var viewResult = Assert.IsType<ViewResult>(result);

            //Next, we wanna make sure, that in this viewresult, the model we have for it
            //is list of RestaurantVM's
            var model = Assert.IsAssignableFrom<IEnumerable<StoreVM>>(viewResult.ViewData.Model);

            //lastly, let's make sure there're two of them
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public void StoreControllerIndexShouldReturnListOfStores()
        {
            //Arrange
            var mockBL = new Mock<IBL>();
            mockBL.Setup(x => x.GetAllStore()).Returns(
                    new List<Store>()
                    {
                        new Store() {
                            Id = 1,
                            Name = "Store1",
                            Address = "first st"
                        },
                        new Store()
                        {
                            Id = 2,
                            Name = "Store2",
                            Address = "last st"
                        }
                    }
                );
            var controller = new StoreController(mockBL.Object);

            //Act
            var result = controller.Index();

            //Assert
            //First, make sure we are getting the right type of the result obj
            var viewResult = Assert.IsType<ViewResult>(result);

            //Next, we wanna make sure, that in this viewresult, the model we have for it
            //is list of RestaurantVM's
            var model = Assert.IsAssignableFrom<IEnumerable<StoreVM>>(viewResult.ViewData.Model);

            //lastly, let's make sure there're two of them
            Assert.Equal(2, model.Count());
        }

    }
}
