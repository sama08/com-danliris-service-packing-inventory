using Com.Danliris.Service.Packing.Inventory.Application.Product;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.WebApi.Controllers;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc;

namespace Com.Danliris.Service.Packing.Inventory.Test.Controllers
{
    
    public class ProductControllerTest
    {
        [Fact]
        public async Task Should_Succes_OnPost()
        {
            var serviceMock = new Mock<IProductService>();
            serviceMock.Setup(service => service.CreateProductPackAndSKU(It.IsAny<CreateProductPackAndSKUViewModel>())).ReturnsAsync(new ProductPackingBarcodeInfo("", 1, 2, "", "", 1));
            var identityProviderMock = new Mock<IIdentityProvider>();
            var controller = new ProductController(serviceMock.Object, identityProviderMock.Object);

            var user = new Mock<ClaimsPrincipal>();
            var claims = new Claim[]
            {
                new Claim("username", "unittestusername")
            };
            user.Setup(u => u.Claims).Returns(claims);
            var controllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext()
                {
                    User = user.Object
                }
            };
            controller.ControllerContext.HttpContext.Request.Headers["Authorization"] = "Bearer Token";
            controller.ControllerContext.HttpContext.Request.Headers["x-timezone-offset"] = "7";
            controller.ControllerContext.HttpContext.Request.Path = new PathString("/v1/unit-test");

            var response = controller.Post(new CreateProductPackAndSKUViewModel());

            Assert.NotNull(response);
        }
        

    }
}
