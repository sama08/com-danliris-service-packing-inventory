using Com.Danliris.Service.Packing.Inventory.Application.InventoryDocumentPacking;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.WebApi.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Controllers
{
    public class InventoryDocumentPackingControllerTest
    {

        [Fact]
        public async Task Should_Succes_OnPost()
        {
            var serviceMock = new Mock<IInventoryDocumentPackingService>();
            serviceMock.Setup(s => s.Create(It.IsAny<CreateInventoryDocumentPackingViewModel>())).Returns(Task.FromResult(0));
            var identityProviderMock = new Mock<IIdentityProvider>();
            var controller = new InventoryDocumentPackingController(serviceMock.Object, identityProviderMock.Object);
            
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
                    User = user.Object, 
                }
            };
            controller.ControllerContext = controllerContext;
            controller.ControllerContext.HttpContext.Request.Headers["Authorization"] = "Bearer unittesttoken";
            controller.ControllerContext.HttpContext.Request.Path = new PathString("/v1/unit-test");

            var response = await controller.Post(new CreateInventoryDocumentPackingViewModel());

            var statusCode = (HttpStatusCode) response.GetType().GetProperty("StatusCode").GetValue(response, null);

            Assert.Equal(HttpStatusCode.Created, statusCode);
        }
    }
}
