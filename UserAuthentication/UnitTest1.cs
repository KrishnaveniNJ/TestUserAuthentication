using System;
using Xunit;
using Amazon.DynamoDBv2.DataModel;
using TestUserAuthentication.Controllers;
using Microsoft.Extensions.Logging;
using Moq;

namespace UserAuthentication
{
    public class UnitTest1
    {


        public Mock<IDynamoDBContext> mockDynamoDB = new Mock<IDynamoDBContext>();

        [Fact]
        public async void GetLoginTest()
        {
   

        var instance = new UserController(mockDynamoDB.Object);


        var result = instance.GetLogin("user1", "MTIzNDU=");


            //asserting

         Assert.NotNull(result); 


        }


        [Fact]
        public async void GetLoginUserTest()
        {


            var instance = new UserController(mockDynamoDB.Object);


            var result = instance.GetUserRecords("048e2a91-8d27-4c13-a648-5cedb6aa182e", "user1");


            //asserting

            Assert.NotNull(result);
            //Assert.NotEqual("200", instance.Response.StatusCode.ToString());

        }
    }
}
