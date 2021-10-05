using System;
using Xunit;
using Amazon.DynamoDBv2.DataModel;
using TestUserAuthentication.Controllers;
using Microsoft.Extensions.Logging;
using Moq;
using TestUserAuthentication.DataLayer;

namespace UserAuthentication
{
    public class UnitTest1
    {


       // public Mock<IDynamoDBContext> mockDynamoDB = new Mock<IDynamoDBContext>();
        public Mock<IDynamoDBService> mockDynamoDBService = new Mock<IDynamoDBService>();
        [Fact]
        public async void GetLoginTest()
        {


            var instance = new UserController(mockDynamoDBService.Object);


            var result = instance.GetLogin("user1", "MTIzNDU=");


            //asserting

            Assert.NotNull(result);


        }

        [Fact]
        public async void GetLoginTestNullValues()
        {


            var instance = new UserController(mockDynamoDBService.Object);


            var result = instance.GetLogin("", "");


            //asserting

            Assert.NotNull(result);


        }
        [Fact]
        public async void GetLoginUserTest()
        {


            var instance = new UserController(mockDynamoDBService.Object);


            var result = instance.GetUserRecords("048e2a91-8d27-4c13-a648-5cedb6aa182e", "user1");


            //asserting

            Assert.NotNull(result);
           

        }
        [Fact]
        public async void GetLoginUserTestNull()
        {


            var instance = new UserController(mockDynamoDBService.Object);


            var result = instance.GetUserRecords("", "");


            //asserting

            Assert.NotNull(result);
          

        }
    }
}
