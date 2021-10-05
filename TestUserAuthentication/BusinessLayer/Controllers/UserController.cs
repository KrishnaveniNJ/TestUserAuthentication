using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using TestUserAuthentication.DataLayer;

namespace TestUserAuthentication.Controllers
{

    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IDynamoDBService DynamoService;

        public UserController(IDynamoDBService _Dynamoservice)
        {
            try
            {
                DynamoService = _Dynamoservice;
            }
            catch (Exception ex)
            {
                new ErrorLog().WriteToLog(DateTime.Now.ToString() + "  " + "Error occured in UserController :  " + ex.Message);
            }
        }


        [Route("GetLogin")]
        [HttpGet]
        public async Task<string> GetLogin(string Username,string password)
        {
            try
            {
                return (await DynamoService.GetLogin(Username, password));
             }
            catch (Exception ex)
            {
                new ErrorLog().WriteToLog(DateTime.Now.ToString() + "  " + "Error occured in Getlogin UserController :  " + ex.Message);
                return null;
            }
        }

        [Route("GetUserInfo")]
        [HttpGet]
        public async Task<List<User>> GetUserRecords(string SessionToken,string username)
        {
            try
            {
                return (await DynamoService.GetUserRecords(SessionToken, username));
            }
            catch (Exception ex)
            {
                new ErrorLog().WriteToLog(DateTime.Now.ToString() + "  " + "Error occured in GetUserRecords UserController :  " + ex.Message);
                return null;
            }
        }


        [Route("GetAllUsers")]
        [HttpGet]
        public async Task<List<User>> GetAllUsers()
        {
            try
            {
                return (await DynamoService.GetAllUsers());
            }
            catch (Exception ex)
            {
                new ErrorLog().WriteToLog(DateTime.Now.ToString() + "  " + "Error occured GetAllUsers in UserController :  " + ex.Message);
                return null;
            }
        }      

    }
}
