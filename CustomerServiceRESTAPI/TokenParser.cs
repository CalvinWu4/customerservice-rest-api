using System;
using System.IdentityModel.Tokens.Jwt;

namespace CustomerServiceRESTAPI
{
    public class TokenParser
    {
        static JwtSecurityTokenHandler _handler = new JwtSecurityTokenHandler();

        public static JwtPayload Parse(string token)
        {
            var jwt = _handler.ReadToken(token) as JwtSecurityToken;
            return jwt.Payload;
        }

        public static int GetClientIdFromToken(string token)
        {
            try
            {
                var jwt = Parse(token);
                var clientId = Convert.ToInt32(jwt["id"]);
                return clientId;
            }
            catch(Exception)
            {
                return -1;
            }
        }
    }
    }

    public class Token
    {
        public string Username { get; set; }
        public string AccountType { get; set; }
        public int Id { get; set; }
        public int Iat { get; set; }
    }
}
