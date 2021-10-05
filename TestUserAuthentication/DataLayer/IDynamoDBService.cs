using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestUserAuthentication.DataLayer
{
   public interface IDynamoDBService
    {
        Task<string> GetLogin(string Username, string password);
       
        Task<List<User>> GetUserRecords(string SessionToken, string username);

        Task<List<User>> GetAllUsers();
    }
}
