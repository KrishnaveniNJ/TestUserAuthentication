using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;

namespace TestUserAuthentication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
    

        private readonly IDynamoDBContext _DynamoDBContext;
       
        public UserController( IDynamoDBContext dynamoDBContext)
        {
         
            _DynamoDBContext = dynamoDBContext;
        }
        [Route("GetLogin")]
        [HttpGet]
        public async Task<string> GetLogin(string Username,string password)
        {
            string returnstring = "Invalid Username and Password";

            try
            {


                var UserObj = _DynamoDBContext.LoadAsync<User>(Username, password).Result;
                if (UserObj != null)
                {
                    if (UserObj.Username != "" || UserObj.Username != null)
                    {
                        Guid g = Guid.NewGuid();
                        var usersessionObj = _DynamoDBContext.LoadAsync<UserSession>(Username).Result;
                        usersessionObj.Username = Username;
                        usersessionObj.SessionToken = g.ToString();
                        returnstring = g.ToString();
                        await _DynamoDBContext.SaveAsync(usersessionObj);
                    }
                }
                return returnstring;
            }
            catch (Exception ex)
            {
                new ErrorLog().WriteToLog(returnstring);
               
                return returnstring;

            }
        }

        [Route("GetUserInfo")]
        [HttpGet]
        public async Task<List<User>> GetUserRecords(string SessionToken,string username)
        {



            try
            {
                var UserObj = _DynamoDBContext.LoadAsync<UserSession>(username, SessionToken).Result;

                if (UserObj != null)
                {
                    return await _DynamoDBContext

                    .QueryAsync<User>(username)
                    .GetRemainingAsync();
                }

                else
                {
                    
                 //   List<User> UserList = new List<User>();
                
                    return null;
                }

            }
            catch (Exception ex)
            {
                username = "";
                new ErrorLog().WriteToLog(ex.Message);

             //   List<User> UserList = new List<User>();

                return null;
            }
        }


        [Route("GetAllUsers")]
        [HttpGet]
        public async Task<List<User>> GetAllUsers()
        {



            try
            {
                var user = new User();
             string password=   user.EnryptString("12345");
              //  string decrypt = DecryptString("12345");

                var conditions = new List<ScanCondition>();

                return await _DynamoDBContext.ScanAsync<User>(conditions).GetRemainingAsync();

            }
            catch (Exception ex)
            {
                
                new ErrorLog().WriteToLog(ex.Message);

                List<User> UserList = new List<User>();

                return UserList;
            }
        }


        //[Route("GetAllUserSessions")]
        //[HttpGet]
        //public async Task<List<UserSession>> GetAllUserSessions()
        //{



        //    try
        //    {


        //        var conditions = new List<ScanCondition>();

        //        return await _DynamoDBContext.ScanAsync<UserSession>(conditions).GetRemainingAsync();

        //    }
        //    catch (Exception ex)
        //    {

        //        new ErrorLog().WriteToLog(ex.Message);

        //        List<UserSession> UserList = new List<UserSession>();

        //        return UserList;
        //    }
        //}

       

    }
}
