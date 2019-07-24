using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyLibrary.Responses
{

    // CM 07/23/2019 
    // Response to be sent upon login success
    public class LoginResponse
    {
        public LoginResponse(string token, string userName, string email)
        {
            Token = token;
            UserName = userName;
            Email = email;
        }

        public string Token { get; private set; }
        public string UserName { get; private set; }
        public string Email { get; private set; }
    }

    // CM 07/23/2019 
    // Response to be sent upon registration success
    public class SignUpResponse
    {
        public SignUpResponse(string token, string userName, string email)
        {
            Token = token;
            UserName = userName;
            Email = email;
        }

        public string Token { get; private set; }
        public string UserName { get; private set; }
        public string Email { get; private set; }
    }

    // CM 07/23/2019 
    // User data response upon success
    public class UserDataResponse
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
