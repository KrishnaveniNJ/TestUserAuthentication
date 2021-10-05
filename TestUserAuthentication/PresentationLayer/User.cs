using System;

namespace TestUserAuthentication
{
    public class User
    {

        public string Username { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }


        //public string DecryptString(string encrString)
        //{
        //    byte[] b;
        //    string decrypted;
        //    try
        //    {
        //        b = Convert.FromBase64String(encrString);
        //        decrypted = System.Text.ASCIIEncoding.ASCII.GetString(b);
        //    }
        //    catch (FormatException fe)
        //    {
        //        decrypted = "";
        //    }
        //    return decrypted;
        //}

        //public string EnryptString(string strEncrypted)
        //{
        //    byte[] b = System.Text.ASCIIEncoding.ASCII.GetBytes(strEncrypted);
        //    string encrypted = Convert.ToBase64String(b);
        //    return encrypted;
        //}
    }
}
