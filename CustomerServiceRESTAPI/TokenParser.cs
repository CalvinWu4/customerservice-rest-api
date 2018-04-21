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
    }

    public class Token
    {
        public string Username { get; set; }
        public string AccountType { get; set; }
        public int Id { get; set; }
        public int Iat { get; set; }
    }
}
