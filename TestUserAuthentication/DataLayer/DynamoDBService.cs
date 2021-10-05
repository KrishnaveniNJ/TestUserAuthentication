using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;

namespace TestUserAuthentication.DataLayer
{
    public class DynamoDBService:IDynamoDBService
    {
        private readonly IDynamoDBContext _DynamoDBContext;

        public DynamoDBService(IDynamoDBContext dynamoDBContext)
        {
            try
            {
                _DynamoDBContext = dynamoDBContext;
            }
            catch (Exception ex)
            {          
                new ErrorLog().WriteToLog(DateTime.Now.ToString() + "  " + "Error occured in constructor Data Layer :  " + ex.Message);             
            }
        }
        public async Task<string> GetLogin(string Username, string password)
        {
            string returnstring = string.Empty;

            try
            {
                returnstring = "Invalid Username and Password!!";
                var UserObj = _DynamoDBContext.LoadAsync<User>(Username, password).Result;
                if (UserObj != null)
                {
                    if (UserObj.Username != "" || UserObj.Username != null && UserObj.Password != "" || UserObj.Password != null)
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
                new ErrorLog().WriteToLog(DateTime.Now.ToString() + "  " + "Error occured in GetLogin request Method in Data Layer :  " + returnstring  );
                new ErrorLog().WriteToLog(DateTime.Now.ToString() + "  " + "Error occured in GetLogin request Method in Data Layer :  " + ex.Message  );
                return returnstring;
            }
        }

        public async Task<List<User>> GetUserRecords(string SessionToken, string username)
        {
            try
            {
                var UserObj = _DynamoDBContext.LoadAsync<UserSession>(username, SessionToken).Result;

                if (UserObj != null)
                {
                    if (UserObj.Username != "" || UserObj.Username != null && UserObj.SessionToken != "" || UserObj.SessionToken != null)
                    {
                        return await _DynamoDBContext

                    .QueryAsync<User>(username)
                    .GetRemainingAsync();
                    }
                }

                return null;

            }
            catch (Exception ex)
            {
                new ErrorLog().WriteToLog(DateTime.Now.ToString() + "  " + "Error occured in GetUserRecords request Method in Data Layer :  " + ex.Message);
                return null;
            }
        }

        public async Task<List<User>> GetAllUsers()
        {
            try
            {
               // var user = new User();
               // string password = user.EnryptString("12345"); // Added it to test the encrypted password stored in DynamoDB               
                var conditions = new List<ScanCondition>();
                return await _DynamoDBContext.ScanAsync<User>(conditions).GetRemainingAsync();
            }
            catch (Exception ex)           
            {
                new ErrorLog().WriteToLog(DateTime.Now.ToString() + "  " + "Error occured in GetAllUsers request Method in Data Layer :  " + ex.Message);
                return null;
            }
        }

    }
}
